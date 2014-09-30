using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using MoneyTrack;
using MoneyTrack.Controllers;
using MoneyTrack.Services;
using SimpleInjector;
using WebActivatorEx;
using Container = SimpleInjector.Container;

namespace MoneyTrack
{
    public static class DependencyConfig
    {
        public static void Initialize()
        {
            var container = BuildContainer();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
        }

        public static Container BuildContainer(Action<Container> createCustomRegistrations = null)
        {
            var container = new Container();

            var modules = from t in Assembly.GetExecutingAssembly().GetTypes()
                          where t.GetInterfaces().Contains(typeof(IModule))
                                && t.GetConstructor(Type.EmptyTypes) != null
                          select Activator.CreateInstance(t) as IModule;
            foreach (var module in modules)
                module.Load(container);

            if (createCustomRegistrations != null) 
                createCustomRegistrations(container);
            container.Verify();

            return container;
        }
    }
}