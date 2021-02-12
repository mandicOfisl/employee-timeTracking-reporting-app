using System;
using System.Globalization;
using System.Threading;

namespace Report
{
	 public partial class NewReport : System.Web.UI.Page
	 {
		  protected override void InitializeCulture()
		  {
				if (Request.Cookies["CultureInfo"] != null)
				{
					 string culture = Request.Cookies["CultureInfo"].Value;
					 Page.Culture = culture;
					 Page.UICulture = culture;
					 Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
					 Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
				}

				base.InitializeCulture();
		  }

		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (Session["djelatnik"] == null)
				{
					 Response.Redirect("Default.aspx");
				}
		  }
	 }
}