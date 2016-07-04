using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public sealed class LockoutGuard
    {
        private readonly LockoutDef[] defs;

        public LockoutGuard(LockoutDef[] defs)
        {
            this.defs = defs;
        }

        public async Task<bool> Check(ICounterOperations counterOperations, IDictionary<string, string> context)
        {
            foreach (var d in this.defs)
            {
                var c = new Counter(counterOperations, LockoutKey.MakeKey(d.Key, context), d.Expiration);
                if (await c.GetValue() >= d.Threshold)
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
                select this.IncrementAndCheck(
                    new Counter(counterOperations, LockoutKey.MakeKey(d.Key, context), d.Expiration),
                    d.Threshold));
            return result.Any(r => r);
        }

        private async Task<bool> IncrementAndCheck(Counter counter, int threshold)
        {
            return await counter.Increment() >= threshold;
        }
    }
}