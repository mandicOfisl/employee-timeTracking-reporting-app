using System.Web;
using System.Web.Optimization;

namespace EvidencijaSati
{
	 public class BundleConfig
	 {
		  // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		  public static void RegisterBundles(BundleCollection bundles)
		  {
				bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
								"~/Scripts/jquery-{version}.js"));

				bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
								"~/Scripts/jquery.validate*"));

				bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
								"~/Scripts/jquery.dataTables.min.js"));
				
				bundles.Add(new ScriptBundle("~/bundles/unosSati").Include(
								"~/Scripts/unosSati.js"));

				// Use the development version of Modernizr to develop with and learn from. Then, when you're
				// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
				bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
								"~/Scripts/modernizr-*"));

				bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
							 "~/Scripts/bootstrap.js"));

				bundles.Add(new ScriptBundle("~/bundles/swalert").Include(
							 "~/lib/sweetalert2/dist/sweetalert2.all.min.js"));

				bundles.Add(new StyleBundle("~/Content/css").Include(
							 "~/Content/bootstrap.css", "~/Content/font-awesome.min.css"));

				bundles.Add(new StyleBundle("~/Content/Login").Include(
							 "~/Content/login.css"));

				bundles.Add(new StyleBundle("~/Content/dataTables").Include(
							 "~/Content/jquery.dataTables.css"));

				bundles.Add(new StyleBundle("~/Content/unosSati").Include(
							 "~/Content/unosSati.css"));

				bundles.Add(new StyleBundle("~/Content/userProfile").Include(
							 "~/Content/userProfile.css"));

				bundles.Add(new StyleBundle("~/Content/swalert").Include(
							 "~/lib/sweetalert2/dist/sweetalert2.all.min.css"));
		  }
	 }
}
