﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KonwerterJedn.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IContainer container = BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly);
            containerBuilder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            if (ConfigurationManager.AppSettings["StatisticsRepository"] == "AzureStorage")
            {
                containerBuilder.RegisterType<StatisticsAzureStorageRepository>().As<IStatisticsRepository>();
            }
            else
            {
                containerBuilder.RegisterType<StatisticsSqlRepository>().As<IStatisticsRepository>();
            }

            containerBuilder.RegisterType<KonwerterJednostek>();


            var assembly = typeof(KonwerterJednostek).Assembly; //Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().AsSelf();

            RegisterPlugins(containerBuilder);

            return containerBuilder.Build();
        }

        private static void RegisterPlugins(ContainerBuilder containerBuilder)
        {
            // string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pluginDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin", "Plugins");

            var assemblies = Directory.GetFiles(pluginDirectory, "*Plugin.dll").Select(Assembly.LoadFrom).ToList();

            foreach (Assembly assembly in assemblies)
            {
                containerBuilder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            }
        }
    }
}
