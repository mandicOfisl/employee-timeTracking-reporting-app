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
	 public partial class _Default : Page
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
				if (Session["djelatnik"] != null) Response.Redirect("NewReport.aspx");

				txtEmail.Focus();
		  }

		  protected void BtnLogin_Click(object sender, EventArgs e)
		  {
				Djelatnik d = AutorizirajDjelatnika(txtEmail.Text, txtPass.Text);

				if (d != null)
				{
					 if (d.TipDjelatnikaID == TipDjelatnikaEnum.DIREKTOR)
					 {
						  Session["djelatnik"] = d.IDDjelatnik.ToString();
						  Session["tipDjelatnika"] = (int) d.TipDjelatnikaID;

						  Response.Redirect("NewReport.aspx");

					 }
					 else if (d.TipDjelatnikaID == TipDjelatnikaEnum.VODITELJ_TIMA)
					 {
						  Session["djelatnik"] = d.IDDjelatnik.ToString();
						  Session["tipDjelatnika"] = (int)d.TipDjelatnikaID;
						  Session["idTim"] = d.TimID;

						  Response.Redirect("NewReport.aspx");
					 }
				}
		  }

		  private Djelatnik AutorizirajDjelatnika(string email, string pass)
		  {
				Djelatnik d = Repo.GetDjelatnikByEmail(email);

				return d.Zaporka == pass ? d : null;
		  }
	 }
}