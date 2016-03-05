using System.Web.Http;

using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterRepositories(container);
            RegisterServices(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
        }

        private static void RegisterServices(IUnityContainer container)
        {
        }
    }
}