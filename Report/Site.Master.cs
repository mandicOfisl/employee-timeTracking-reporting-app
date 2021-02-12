using System;
using System.Web;
using System.Web.UI;

namespace Report
{
	 public partial class SiteMaster : MasterPage
	 {
		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (Request.Cookies["djelatnik"] != null)
				{

				}
		  }

		  protected void BtnHrv_Click(object sender, EventArgs e)
		  {
				HttpCookie cookie = new HttpCookie("CultureInfo")
				{
					 Value = "hr"
				};
				Response.Cookies.Add(cookie);
								
				Response.Redirect(Request.Path);
		  }

		  protected void BtnEng_Click(object sender, EventArgs e)
		  {
				HttpCookie cookie = new HttpCookie("CultureInfo")
				{
					 Value = "en"
				};
				Response.Cookies.Add(cookie);
								
				Response.Redirect(Request.Path);
		  }

		  protected void BtnOdjava_Click(object sender, EventArgs e)
		  {
				Session["djelatnik"] = null;
				Session["tipDjelatnika"] = null;
				Session["idTim"] = null;

				Response.Redirect("Default.aspx");
		  }
	 }
}