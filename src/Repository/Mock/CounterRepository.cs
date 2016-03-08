using System.Collections.Generic;
using System.Threading.Tasks;

using Repository.Interface;

namespace Repository.Mock
{
    public sealed class CounterRepository : ICounterRepository
    {
        private const string Prefix = "Counter: ";

        private readonly IDictionary<string, int> counters = new Dictionary<string, int>(); 

        public Task<int> Increment(string key, int delta, int initial, int expiration)
        {
            int value;
            key = Prefix + key;
            lock (this.counters)
            {
                if (this.counters.ContainsKey(key))
                {
                    value = this.counters[key] = this.counters[key] + delta;
                }
                else
                {
                    value = this.counters[key] = initial;
                }
            }

            return Task.FromResult(value);
        }

        public Task<bool> Reset(string key)
        {
            lock (this.counters)
            {
                this.counters.Remove(Prefix + key);
            }

            return Task.FromResult(true);
        }
    }
}