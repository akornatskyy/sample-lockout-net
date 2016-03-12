using System.Collections.Generic;
using System.Security.Claims;

namespace Web.Models
{
    public sealed class StatusRequest
    {
        public ClaimsPrincipal User { get; set; }

        public IDictionary<string, string> LockoutContext { get; set; }
    }
}