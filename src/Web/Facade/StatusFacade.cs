using System.Threading.Tasks;

using Web.Models;

namespace Web.Facade
{
    public class StatusFacade
    {
        public Task<StatusResponse> RefreshStatus(StatusRequest request)
        {
            return Task.FromResult(new StatusResponse
            {
                IsAuthenticated = request.User.Identity.IsAuthenticated
            });
        }
    }
}