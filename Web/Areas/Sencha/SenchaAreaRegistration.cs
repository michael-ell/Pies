using System.Web.Mvc;

namespace Codell.Pies.Web.Areas.Sencha
{
    public class SenchaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Sencha"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Sencha_default", "Sencha/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
