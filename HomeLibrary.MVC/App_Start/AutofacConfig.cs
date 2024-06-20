using Autofac;
using Autofac.Integration.Mvc;
using HomeLibrary.DAL.DataAccess;
using HomeLibrary.DAL.Domain.Interfaces;
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