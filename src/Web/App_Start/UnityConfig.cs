using System;
using System.Reflection;
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
        public const string Mock = "Mock";

        public static void RegisterComponents(string strategy)
        {
            var c = new UnityContainer();
            RegisterRepositories(c, strategy);
            RegisterServices(c);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(c);
        }

        private static void RegisterRepositories(IUnityContainer c, string strategy)
        {
            var mi = typeof(UnityConfig).GetMethod("Register" + strategy, BindingFlags.NonPublic | BindingFlags.Static);
            if (mi == null)
            {
                throw new ArgumentOutOfRangeException("strategy", strategy, null);
            }

            mi.Invoke(null, new object[] { c });
        }

        private static void RegisterMock(IUnityContainer c)
        {
            c.RegisterType<ICounterRepository, CounterRepository>(new ContainerControlledLifetimeManager());
        }

        private static void RegisterServices(IUnityContainer c)
        {
            c.RegisterType<ILockoutService, LockoutService>(new HierarchicalLifetimeManager());
        }
    }
}