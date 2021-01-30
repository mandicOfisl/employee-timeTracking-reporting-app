using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Report
{
	 public partial class _Default : Page
	 {
		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (Request.Cookies["djelatnik"] != null) Response.Redirect("NoviIzvjestaj.aspx");
				txtEmail.Focus();
		  }

		  protected void BtnLogin_Click(object sender, EventArgs e)
		  {
				Djelatnik d = AutorizirajDjelatnika(txtEmail.Text, txtPass.Text);

				if (d != null)
				{
					 HttpCookie cookie = new HttpCookie("djelatnik");

					 cookie["IdDjelatnik"] = d.IDDjelatnik.ToString();
					 cookie["Ime"] = d.Ime;
					 cookie["Prezime"] = d.Prezime;
					 cookie["TipDjelatnika"] = ((int)d.TipDjelatnikaID).ToString();
					 cookie["TimId"] = d.TimID.ToString();

					 cookie.Expires = DateTime.Now.AddHours(1);

					 Response.Cookies.Add(cookie);
					 Response.Redirect("NewReport.aspx");
				}
		  }

		  private Djelatnik AutorizirajDjelatnika(string email, string pass)
		  {
				Djelatnik d = Repo.GetDjelatnikByEmail(email);

				return d.Zaporka == pass ? d : null;
		  }
	 }
}