using Microsoft.Extensions.DependencyInjection;
using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Logic.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SchoolLibrary
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



            var serviceProvider = new ServiceCollection();

            serviceProvider.AddHttpClient();
            var service = serviceProvider.AddHttpClient().BuildServiceProvider();


            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();


            container.Register<IAuthorService, AuthorRepository>(Lifestyle.Scoped);
            container.Register<IBookServices, BookRepository>(Lifestyle.Scoped);
            container.Register<ICategoryService, CategoryRepository>(Lifestyle.Scoped);

            container.Register<SchoolProjectDatabase>(Lifestyle.Scoped);

           // container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
           // container.Register<ApplicationUserManager>(Lifestyle.Scoped);
           // container.Register<ApplicationUserStore>(Lifestyle.Scoped);

            container.Register<IRepositoryWrapper, RepositoryWrapper>(Lifestyle.Scoped);



            container.RegisterWebApiControllers(GlobalConfiguration.Configuration, Assembly.GetExecutingAssembly());

            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

           // DependencyResolver.SetResolver(new SimpleInjectorWebApiDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
