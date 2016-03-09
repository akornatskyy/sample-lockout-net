using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ILockoutService
    {
        Task<bool> StatusQuota(IDictionary<string, string> context);

        Task<bool> StatusCheck(IDictionary<string, string> context);

        Task<bool> StatusGuard(IDictionary<string, string> context);
    }
}