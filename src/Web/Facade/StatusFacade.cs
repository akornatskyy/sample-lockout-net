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
            if (await this.lockoutService.StatusQuotaOrCheck(request.LockoutContext))
            {
                return StatusResponse.Locked;
            }

            // Simulate an error
            if (Rnd.Next(10) > 5)
            {
                if (await this.lockoutService.StatusGuard(request.LockoutContext))
                {
                    return StatusResponse.Locked;
                }

                return null;
            }

            return new StatusResponse
            {
                IsAuthenticated = request.User.Identity.IsAuthenticated,
                IpAddress = request.LockoutContext["ip"]
            };
        }
    }
}