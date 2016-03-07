using System.Diagnostics;
using System.Threading.Tasks;

namespace Models
{
    public sealed class Counter
    {
        private readonly ICounterOperations counterOperations;
        private readonly string name;
        private readonly int expiration;

        public Counter(ICounterOperations counterOperations, string name, int expiration)
        {
            this.counterOperations = counterOperations;
            this.name = name;
            this.expiration = expiration;
        }

        [DebuggerStepThrough]
        public async Task<int> GetValue()
        {
            return await this.counterOperations.Increment(this.name, 0, 0, this.expiration);
        }

        [DebuggerStepThrough]
        public async Task<int> Increment()
        {
            return await this.counterOperations.Increment(this.name, 1, 1, this.expiration);
        }

        [DebuggerStepThrough]
        public async Task<bool> Reset()
        {
            return await this.counterOperations.Reset(this.name);
        }
    }
}