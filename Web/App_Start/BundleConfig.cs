using System.Web.Optimization;

namespace Codell.Pies.Web.App_Start
{
    public class BundleConfig
    {
        public static class Names
        {
            public const string Scripts = "~/scripts";
            public const string Styles = "~/content/styles";
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle(Names.Styles).Include("~/content/styles/*.css"));
            bundles.Add(new ScriptBundle(Names.Scripts).Include("~/scripts/*.js"));
        }
    }
}