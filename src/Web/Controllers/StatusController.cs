using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

using Web.Constants;
using Web.Facade;
using Web.Models;

namespace Web.Controllers
{
    [Route(RoutePatterns.Status)]
    public sealed class StatusController : Infrastructure.ApiController
    {
        public async Task<StatusResponse> Get()
        {
            return await this.GetService<StatusFacade>().RefreshStatus(new StatusRequest
            {
                User = (ClaimsPrincipal)User
            });
        }
    }
}