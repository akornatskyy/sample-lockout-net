using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public sealed class LockoutQuota
    {
        private readonly LockoutDef[] defs;

        public LockoutQuota(LockoutDef[] defs)
        {
            this.defs = defs;
        }

        public async Task<bool> Quota(ICounterOperations counterOperations, IDictionary<string, string> context)
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
            var value = await counter.Increment();
            return value > threshold;
        }
    }
}