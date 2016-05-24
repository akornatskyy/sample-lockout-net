using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ILockoutService
    {
        Task<bool> StatusQuotaOrCheck(IDictionary<string, string> context);

        Task<bool> StatusGuard(IDictionary<string, string> context);
    }
}