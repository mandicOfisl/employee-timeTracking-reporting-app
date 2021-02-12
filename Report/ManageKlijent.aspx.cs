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
	 public partial class ManageKlijent : System.Web.UI.Page
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

				if (!IsPostBack)
				{
					 FillKlijentiListBox();
				}
		  }

		  private void FillKlijentiListBox()
		  {
				LbKlijenti.DataSource = Repo.GetKlijenti();
				LbKlijenti.DataTextField = "Naziv";
				LbKlijenti.DataValueField = "IDKlijent";
				LbKlijenti.DataBind();
		  }

		  protected void LbKlijenti_SelectedIndexChanged(object sender, EventArgs e)
		  {
				if (LbKlijenti.SelectedIndex > -1)
				{
					 hiddenIdKlijent.Value = null;
					 Klijent k = Repo.SelectKlijent(int.Parse(LbKlijenti.SelectedValue));
					 txtNaziv.Text = k.Naziv;
					 txtEmail.Text = k.Email;
					 txtTelefon.Text = k.Telefon;

					 FillProjektiListBox(k.IDKlijent);
					 ToggleInputFieldsEnabled(false);

					 BtnEdit.Enabled = true;
					 BtnSave.Enabled = false;

					 BtnDeaktiviraj.Enabled = k.IsActive;
					 BtnAktiviraj.Enabled = !k.IsActive;
				}
		  }

		  private void ToggleInputFieldsEnabled(bool isEnabled)
		  {
				txtNaziv.Enabled = isEnabled;
				txtEmail.Enabled = isEnabled;
				txtTelefon.Enabled = isEnabled;
		  }

		  private void FillProjektiListBox(int iDKlijent)
		  {
				lbProjekti.DataSource = Repo.GetProjektiKlijenta(iDKlijent);
				lbProjekti.DataTextField = "Naziv";
				lbProjekti.DataValueField = "IDProjekt";
				lbProjekti.DataBind();
		  }

		  protected void BtnEdit_Click(object sender, EventArgs e)
		  {
				hiddenIdKlijent.Value = int.Parse(LbKlijenti.SelectedValue).ToString();

				ToggleInputFieldsEnabled(true);

				BtnEdit.Enabled = false;
				BtnSave.Enabled = true;
		  }

		  protected void BtnAdd_Click(object sender, EventArgs e)
		  {
				hiddenIdKlijent.Value = null;
				ToggleInputFieldsEnabled(true);
				ClearAllFields();
				BtnAktiviraj.Enabled = false;
				BtnDeaktiviraj.Enabled = false;
		  }

		  private void ClearAllFields()
		  {
				txtNaziv.Text = "";
				txtEmail.Text = "";
				txtTelefon.Text = "";
		  }

		  protected void BtnSave_Click(object sender, EventArgs e)
		  {
				Klijent k = new Klijent
				{
					 Naziv = txtNaziv.Text,
					 Email = txtEmail.Text,
					 Telefon = txtTelefon.Text
				};

				if (string.IsNullOrEmpty(hiddenIdKlijent.Value))
				{
					 _ = Repo.DodajKlijenta(k);
				}
				else
				{
					 k.IDKlijent = int.Parse(hiddenIdKlijent.Value);
					 _ = Repo.UpdateKlijent(k);
				}

				FillKlijentiListBox();
				ToggleInputFieldsEnabled(false);
				BtnAdd.Enabled = true;
				BtnSave.Enabled = false;
		  }

		  protected void BtnDeaktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectKlijent(int.Parse(LbKlijenti.SelectedValue)).IDKlijent;

				if (Repo.DeaktivirajKlijenta(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }

		  protected void BtnAktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectKlijent(int.Parse(LbKlijenti.SelectedValue)).IDKlijent;

				if (Repo.AktivirajKlijenta(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }
	 }
}