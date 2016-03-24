using System;
using System.Threading.Tasks;

using Couchbase.Core;

using Repository.Interface;

namespace Repository.Couchbase
{
    public sealed class CounterRepository : ICounterRepository
    {
        private const string Prefix = "Counter: ";

        private readonly IBucket bucket;

        [CLSCompliant(false)]
        public CounterRepository(IBucket bucket)
        {
            this.bucket = bucket;
        }

        public async Task<int> Increment(string key, int delta, int initial, int expiration)
        {
            var r = await this.bucket.IncrementAsync(Prefix + key, (ulong)delta, (ulong)initial, (uint)expiration);
            if (!r.Success)
            {
                return 0;
            }

            return (int)r.Value;
        }

        public async Task<bool> Reset(string key)
        {
            var r = await this.bucket.RemoveAsync(Prefix + key);
            return r.Success;
        }
    }
}