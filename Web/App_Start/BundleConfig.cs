using System.Web.Optimization;

namespace Codell.Pies.Web.App_Start
{
    public class BundleConfig
    {
        public static class Names
        {
            public const string Scripts = "~/scripts";
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(Names.Scripts).Include("~/Scripts/*.js"));
        }
    }
}