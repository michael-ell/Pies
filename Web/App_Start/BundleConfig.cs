using System.Web.Optimization;

namespace Codell.Pies.Web.App_Start
{
    public class BundleConfig
    {
        public static class Names
        {
            public const string Scripts = "~/scripts/bundle";
            public const string Styles = "~/content/styles/bundle";
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle(Names.Styles).Include("~/content/styles/jquery-ui.css",
                                                              "~/content/styles/spectrum.css",         
                                                              "~/content/styles/site.css"));
            bundles.Add(new ScriptBundle(Names.Scripts).Include("~/scripts/jquery-{version}.js",
                                                                "~/scripts/jquery-ui-{version}.js",
                                                                "~/scripts/jquery.linq.js",
                                                                "~/scripts/jquery.mHub.js",
                                                                "~/scripts/jquery.signalR-{version}.js",
                                                                "~/scripts/knockout-{version}.js",
                                                                "~/scripts/linq.js",
                                                                "~/scripts/modernizr-{version}.js",
                                                                "~/scripts/highcharts.js",
                                                                //"~/scripts/highcharts-exporting.js",
                                                                "~/scripts/spectrum.js",
                                                                "~/scripts/knockout-pies.js",
                                                                "~/scripts/jquery.mHub.signalR.js",
                                                                "~/scripts/pies.js"));
        }
    }
}