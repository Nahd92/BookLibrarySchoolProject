using SchoolLibrary.Data.Database;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Logic.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
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


            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<IBookServices, BookRepository>(Lifestyle.Scoped);
            container.Register<IAuthorService, AuthorRepository>(Lifestyle.Scoped);
            container.Register<ICategoryService, CategoryRepository>(Lifestyle.Scoped);
            container.Register<SchoolProjectDatabase>(Lifestyle.Singleton);
            container.Register<IRepositoryWrapper, RepositoryWrapper>(Lifestyle.Scoped);



            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
