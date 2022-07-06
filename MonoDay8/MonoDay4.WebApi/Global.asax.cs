using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using MonoDay4.Model;
using MonoDay4.Model.Common;
using MonoDay4.Repository;
using MonoDay4.Repository.Common;
using MonoDay4.Service;
using MonoDay4.Service.Common;

namespace MonoDay4.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(ContainerConfig.Configure());
        }
    }

    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DiModel>();
            builder.RegisterModule<DiService>();
            builder.RegisterModule<DiRepository>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }
}
