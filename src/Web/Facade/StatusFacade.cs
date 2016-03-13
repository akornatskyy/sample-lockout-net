using System;
using System.Threading.Tasks;

using Service.Interface;
using Web.Models;

namespace Web.Facade
{
    public sealed class StatusFacade
    {
        private static readonly Random Rnd = new Random();

        private readonly ILockoutService lockoutService;

        public StatusFacade(ILockoutService lockoutService)
        {
            this.lockoutService = lockoutService;
        }

        public async Task<StatusResponse> RefreshStatus(StatusRequest request)
        {
            var identity = request.User.Identity;
            request.LockoutContext["name"] = identity.Name;
            if (await this.lockoutService.StatusQuota(request.LockoutContext))
            {
                return StatusResponse.Locked;
            }

            if (await this.lockoutService.StatusCheck(request.LockoutContext))
            {
                return StatusResponse.Locked;
            }

            // Simulate an error
            if (Rnd.Next(10) > 5)
            {
                await this.lockoutService.StatusGuard(request.LockoutContext);
                return null;
            }

            return new StatusResponse
            {
                IsAuthenticated = identity.IsAuthenticated,
                IpAddress = request.LockoutContext["ip"]
            };
        }
    }
}