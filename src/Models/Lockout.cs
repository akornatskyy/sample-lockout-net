using System.Diagnostics;
using System.Threading.Tasks;

namespace Models
{
    public sealed class Lockout
    {
        private readonly int threshold;

        public Lockout(Counter counter, int threshold)
        {
            this.threshold = threshold;
            this.Counter = counter;
        }

        public Counter Counter { get; private set; }

        [DebuggerStepThrough]
        public async Task<bool> Check()
        {
            var value = await this.Counter.GetValue();
            return value > this.threshold;
        }

        [DebuggerStepThrough]
        public async Task<bool> Guard()
        {
            var value = await this.Counter.Increment();
            return value > this.threshold;
        }
    }
}