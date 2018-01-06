using System.Web;
using System.Web.Optimization;

namespace ProjectBluefox
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Create the script bundles
            ScriptBundle vendorScriptsBundle = new ScriptBundle("~/ScriptBundles/Vendors");

            // Add the scripts to the script bundles
            vendorScriptsBundle.Include("~/Scripts/Vendors/jQuery/jquery-1.10.2.min.js",
                                        "~/Scripts/Vendors/KnockoutJs/knockout-3.4.2.js");

            // Register the script and stylesheet bundles
            bundles.Add(vendorScriptsBundle);
        }
    }
}
