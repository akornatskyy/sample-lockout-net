using System.Web.Http;

using Microsoft.Practices.Unity;
using Unity.WebApi;

using Repository.Interface;
using Repository.Mock;
using Service.Bridge;
using Service.Interface;

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

        private static void RegisterRepositories(IUnityContainer c)
        {
            c.RegisterType<ICounterRepository, CounterRepository>(new ContainerControlledLifetimeManager());
        }

        private static void RegisterServices(IUnityContainer c)
        {
            c.RegisterType<ILockoutService, LockoutService>(new HierarchicalLifetimeManager());
        }
    }
}