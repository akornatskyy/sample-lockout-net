using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public sealed class LockoutRuntime
    {
        private readonly LockoutDef[] defs;

        public LockoutRuntime(LockoutDef[] defs)
        {
            this.defs = defs;
        }

        public async Task<bool> Check(ICounterOperations counterOperations, IDictionary<string, string> context)
        {
            foreach (var d in this.defs)
            {
                var l = new Lockout(
                    new Counter(counterOperations, this.MakeKey(d.Key, context), d.Expiration),
                    d.Threshold);
                if (await l.Check())
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Guard(ICounterOperations counterOperations, IDictionary<string, string> context)
        {
            var result = await Task.WhenAll(
                from d in this.defs
                select new Lockout(
                    new Counter(counterOperations, this.MakeKey(d.Key, context), d.Expiration),
                    d.Threshold).Guard());
            return result.Any(r => r);
        }

        private string MakeKey(string format, IDictionary<string, string> d)
        {
            var r = format;
            foreach (var pair in d)
            {
                r = r.Replace("{" + pair.Key + "}", pair.Value);
            }

            return r;
        }
    }
}