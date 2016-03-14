using System.Configuration;
using System.Web.Http;

namespace Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents(ConfigurationManager.AppSettings["Repository.Strategy"] ?? UnityConfig.Mock);  
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}