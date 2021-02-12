using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Report
{
	 public class Global : HttpApplication
	 {
		  void Application_Start(object sender, EventArgs e)
		  {
				// Code that runs on application startup
				RouteConfig.RegisterRoutes(RouteTable.Routes);
				BundleConfig.RegisterBundles(BundleTable.Bundles);
		  }

		  void Application_BeginRequest(object sender, EventArgs e)
		  {
				if (Request.Cookies["CultureInfo"] != null)
				{
					 string culture = Request.Cookies["CultureInfo"].Value;

					 Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
					 Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
				}
		  }
	 }
}