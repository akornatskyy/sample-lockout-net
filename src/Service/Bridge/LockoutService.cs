using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Bridge
{
    public sealed class LockoutService : ILockoutService
    {
        private static readonly LockoutQuota StatusLockoutQuota = new LockoutQuota(new[]
        {
            new LockoutDef { Key = "Status-Q1: {ip}", Threshold = 100, Expiration = 15 * 60 },
            new LockoutDef { Key = "Status-Q2: {name}", Threshold = 20, Expiration = 5 * 60 },
            new LockoutDef { Key = "Status-Q3: {ip}-{name}", Threshold = 10, Expiration = 60 }
        });

        private static readonly LockoutGuard StatusLockoutGuard = new LockoutGuard(new[]
        {
            new LockoutDef { Key = "Status-G1: {name}", Threshold = 10, Expiration = 5 * 60 },
            new LockoutDef { Key = "Status-G2: {ip}-{name}", Threshold = 4, Expiration = 60 }
        });

        private readonly ICounterRepository counterRepository;

        public LockoutService(ICounterRepository counterRepository)
        {
            this.counterRepository = counterRepository;
        }

        public async Task<bool> StatusQuotaOrCheck(IDictionary<string, string> context)
        {
            Debug.Assert(context.ContainsKey("name"), "name");
            Debug.Assert(context.ContainsKey("ip"), "ip");
            return await LockoutService.StatusLockoutQuota.Quota(this.counterRepository, context) || 
                await LockoutService.StatusLockoutGuard.Check(this.counterRepository, context);
        }

        public Task<bool> StatusGuard(IDictionary<string, string> context)
        {
            Debug.Assert(context.ContainsKey("name"), "name");
            Debug.Assert(context.ContainsKey("ip"), "ip");
            return LockoutService.StatusLockoutGuard.Guard(this.counterRepository, context);
        }
    }
}