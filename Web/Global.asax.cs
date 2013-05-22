using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Codell.Pies.Common.Configuration;
using Codell.Pies.Common.Security;
using Codell.Pies.Web.App_Start;
using Codell.Pies.Web.Configuration;
using Codell.Pies.Web.Security;
using Elmah;
using StackExchange.Profiling;

namespace Codell.Pies.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            RegisterDependencyResolver();
            RegisterViewEngines();
            RegisterModelBinders();
            SetupProfiler();
        }

        private void RegisterDependencyResolver()
        {
            var container = Configure.With<AutofacConfigurationModule>();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
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