using System.Security.Claims;

namespace Web.Models
{
    public sealed class StatusRequest
    {
        public ClaimsPrincipal User { get; set; }
    }
}