using System.Net.Http;

namespace Web.Infrastructure
{
    public class ApiController : System.Web.Http.ApiController
    {
        protected T GetService<T>()
        {
            return (T)this.Request.GetDependencyScope().GetService(typeof(T));
        }
    }
}
