using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public static class LockoutKey
    {
        public static string MakeKey(string format, IDictionary<string, string> d)
        {
            return d.Aggregate(format, (current, pair) => current.Replace("{" + pair.Key + "}", pair.Value));
        }
    }
}