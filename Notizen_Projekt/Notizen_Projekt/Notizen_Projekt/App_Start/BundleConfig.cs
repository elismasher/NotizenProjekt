﻿using System.Web;
using System.Web.Optimization;

namespace Notizen_Projekt
{
    public class BundleConfig
    {
        // Weitere Informationen zur Bündelung finden Sie unter https://go.microsoft.com/fwlink/?LinkId=301862.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // bereit ist für die Produktion, verwenden Sie das Buildtool unter https://modernizr.com, um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/semanticUi").Include(
                      "~/Scripts/semantic.min.js",
                      "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/boodstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Scripts/background-script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      // "~/Content/bootstrap.css",
                      "~/Content/semantic.min.css",
                      "~/Content/site.css"
                      ));
        }
    }
}
