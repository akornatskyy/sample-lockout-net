using System.Threading.Tasks;

namespace Models
{
    public interface ICounterOperations
    {
        Task<int> Increment(string key, int delta, int initial, int expiration);

        Task<bool> Reset(string key);
    }
}