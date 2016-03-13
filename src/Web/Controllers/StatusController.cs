using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

using Web.Constants;
using Web.Infrastructure;
using Web.Facade;
using Web.Models;

namespace Web.Controllers
{
    [Route(RoutePatterns.Status)]
    public sealed class StatusController : Infrastructure.ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            var response = await this.GetService<StatusFacade>().RefreshStatus(new StatusRequest
            {
                User = (ClaimsPrincipal)User,
                LockoutContext = new Dictionary<string, string>
                {
                    { "ip", this.Request.ClientIpAddress() }
                }
            });

            if (response == StatusResponse.Locked)
            {
                return this.ResponseMessage(this.Request.CreateErrorResponse(
                    HttpStatusCode.Forbidden, "The service is temporarily unavailable."));
            }
            else if (response == null)
            {
                return this.BadRequest();
            }

            return this.Ok(response);
        }
    }
}