using Autofac;
using Autofac.Integration.Mvc;
using HomeLibrary.Infrastructure.DataAccess;
using HomeLibrary.Infrastructure.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeLibrary.MVC.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<DatabaseContext>().As<IDbContext>().WithParameter("connectionString", System.Configuration.ConfigurationManager.ConnectionStrings["DefaultDatabase"].ConnectionString);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}