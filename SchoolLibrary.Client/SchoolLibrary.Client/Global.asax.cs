using Microsoft.Extensions.DependencyInjection;
using SchoolLibrary.Client.Domain.Interfaces;
using SchoolLibrary.Client.Logic;
using SchoolLibrary.Client.Logic.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Net.Http;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SchoolLibrary.Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var service = new ServiceCollection();

            service.AddHttpClient().BuildServiceProvider();

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register<HttpClientProvider>(Lifestyle.Scoped);
            container.Register<IHttpClientProvider, HttpClientProvider>(Lifestyle.Scoped);
            container.Register<BookRepository>(Lifestyle.Scoped);
            container.Register<IBookRepository, BookRepository>(Lifestyle.Scoped);


            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
