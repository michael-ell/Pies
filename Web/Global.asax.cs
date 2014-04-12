using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Codell.Pies.Common;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Web.App_Start;
using Codell.Pies.Web.Configuration;
using Codell.Pies.Web.Security;
using Elmah;
using StackExchange.Profiling;

namespace Codell.Pies.Web
{
    public class MvcApplication : HttpApplication
    {
        private HttpConfiguration WebApiConfiguration { get { return GlobalConfiguration.Configuration; } }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            RegisterDependencyResolver();
            RegisterViewEngines();
            RegisterModelBinders();
            RegisterWebApiModelBinders();
            SetupProfiler();
        }

        private void RegisterDependencyResolver()
        {
            var container = Configure.With<AutofacConfigurationModule>();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            WebApiConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            Bootstrap(container);
        }

        private void Bootstrap(IContainer container)
        {
            IEnumerable<IBootstrapper> bootstrappers;
            if (container.TryResolve(out bootstrappers))
            {
                foreach (var bootstrapper in bootstrappers)
                {
                    bootstrapper.Run();
                }
            }            
        }

        private void RegisterViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private void RegisterModelBinders()
        {
            ModelBinders.Binders.Add(typeof(IPiesIdentity), new IdentityModelBinder());
        }

        private void RegisterWebApiModelBinders()
        {
            WebApiConfiguration.BindParameter(typeof(IPiesIdentity), new IdentityModelBinder());
        }

        private void SetupProfiler()
        {
            var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();
            ignored.Add("Glimpse.axd");
            MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();  
        }

        protected void Application_BeginRequest()
        {
            var store = new AppSettings();
            if (store.Get<bool>(Keys.Profile))
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

        protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }

        private void FilterError404(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                var ex = (HttpException)e.Exception.GetBaseException();
                if (ex.GetHttpCode() == 404)
                    e.Dismiss();
            }
        }
    }
}