using System;
using System.Reflection;
using System.Web.Http;
using Couchbase;
using Couchbase.Core;
using Microsoft.Practices.Unity;
using Unity.WebApi;

using Repository.Interface;
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
            c.RegisterType<ICounterRepository, Repository.Mock.CounterRepository>(new ContainerControlledLifetimeManager());
        }

        private static void RegisterCouchbase(IUnityContainer c)
        {
            ICluster cluster = new Cluster("couchbaseClient/couchbase");
            c.RegisterInstance(cluster);
            c.RegisterInstance("cache", cluster.OpenBucket());

            c.RegisterType<ICounterRepository, Repository.Couchbase.CounterRepository>(
                 new ContainerControlledLifetimeManager(),
                 new InjectionConstructor(new ResolvedParameter<IBucket>("cache")));
        }

        private static void RegisterServices(IUnityContainer c)
        {
            c.RegisterType<ILockoutService, LockoutService>(new HierarchicalLifetimeManager());
        }
    }
}