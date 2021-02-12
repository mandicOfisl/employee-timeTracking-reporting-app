using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Report
{
	 public partial class Administracija : System.Web.UI.Page
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

				if (Session["tipDjelatnika"] != null)
				{
					 int tip = int.Parse(Session["tipDjelatnika"].ToString());

					 if (tip != (int)TipDjelatnikaEnum.DIREKTOR)
					 {
						  BtnEditTim.Enabled = false;
					 }
				}

		  }
	 }
}