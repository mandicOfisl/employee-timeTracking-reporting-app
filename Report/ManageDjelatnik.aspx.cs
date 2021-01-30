using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Report
{
	 public partial class ManageDjelatnik : System.Web.UI.Page
	 {
		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (!IsPostBack)
				{
					 FillDjelatniciListBox();
					 FillTimoviDdl();
					 FillTipDjelatnikaDdl();
				}
		  }
		  private void FillDjelatniciListBox()
		  {
				LbDjelatnici.DataSource = Repo.GetDjelatnici();
				LbDjelatnici.DataTextField = "FullName";
				LbDjelatnici.DataValueField = "IDDjelatnik";
				LbDjelatnici.DataBind();
		  }

		  private void FillTimoviDdl()
		  {
				ddlTim.DataSource = Repo.GetTimovi().ToList();
				ddlTim.DataTextField = "Naziv";
				ddlTim.DataValueField = "IDTim";
				ddlTim.DataBind();
		  }

		  private void FillTipDjelatnikaDdl()
		  {
				foreach (TipDjelatnikaEnum e in Enum.GetValues(typeof(TipDjelatnikaEnum)))
				{
					 ddlTipDjelatnika.Items.Add(
						  new ListItem(
								Enum.GetName(typeof(TipDjelatnikaEnum), e), ((int)e).ToString()));
				}
		  }

		  protected void LbDjelatnici_SelectedIndexChanged(object sender, EventArgs e)
		  {
				if (LbDjelatnici.SelectedIndex > -1)
				{
					 hiddenIdDjelatnik.Value = null;
					 Djelatnik d = Repo.SelectDjelatnik(int.Parse(LbDjelatnici.SelectedValue));
					 txtIme.Text = d.Ime;
					 txtPrezime.Text = d.Prezime;
					 txtEmail.Text = d.Email;
					 txtDatum.Text = d.DatumZaposlenja.ToString("yyyy-MM-dd");
					 txtZaporka.Text = d.Zaporka;

					 ChangeDdlTimSelection(d.TimID);
					 ChangeDdlTipDjelatnikaSelection(d.TipDjelatnikaID);
					 FillProjektiListBox(d.IDDjelatnik);
					 ToggleInputFieldsEnabled(false);

					 BtnEdit.Enabled = true;
					 BtnSave.Enabled = false;
					 					 
					 BtnDeaktiviraj.Enabled = d.IsActive;
					 BtnAktiviraj.Enabled = !d.IsActive;
				}
		  }

		  private void ChangeDdlTimSelection(int timID)
		  {
				ddlTim.SelectedValue = null;
				ListItem li = ddlTim.Items.FindByValue(timID.ToString());
				if (li != null) li.Selected = true;
		  }

		  private void ChangeDdlTipDjelatnikaSelection(TipDjelatnikaEnum tipDjelatnikaID)
		  {
				ddlTipDjelatnika.SelectedValue = null;
				ListItem li = ddlTipDjelatnika.Items.FindByValue(((int)tipDjelatnikaID).ToString());
				if (li != null) li.Selected = true;
		  }

		  private void FillProjektiListBox(int djelatnikId)
		  {
				lbProjekti.DataSource = Repo.GetProjektiDjelatnika(djelatnikId);
				lbProjekti.DataTextField = "Naziv";
				lbProjekti.DataValueField = "IDProjekt";
				lbProjekti.DataBind();
		  }

		  private void ToggleInputFieldsEnabled(bool isEnabled)
		  {
				txtIme.Enabled = isEnabled;
				txtPrezime.Enabled = isEnabled;
				txtEmail.Enabled = isEnabled;
				txtDatum.Enabled = isEnabled;
				txtZaporka.Enabled = isEnabled;
				ddlTim.Enabled = isEnabled;
				ddlTipDjelatnika.Enabled = isEnabled;
		  }

		  protected void BtnEdit_Click(object sender, EventArgs e)
		  {
				hiddenIdDjelatnik.Value = int.Parse(LbDjelatnici.SelectedValue).ToString();

				ToggleInputFieldsEnabled(true);

				BtnEdit.Enabled = false;
				BtnSave.Enabled = true;

		  }

		  protected void BtnSave_Click(object sender, EventArgs e)
		  {
				Djelatnik d = new Djelatnik
				{
					 Ime = txtIme.Text,
					 Prezime = txtPrezime.Text,
					 Email = txtEmail.Text,
					 DatumZaposlenja = DateTime.Parse(txtDatum.Text),
					 TipDjelatnikaID = (TipDjelatnikaEnum)int.Parse(ddlTipDjelatnika.SelectedValue),
					 TimID = int.Parse(ddlTim.SelectedValue)
				};

				if (string.IsNullOrEmpty(hiddenIdDjelatnik.Value))
				{
					 d.Zaporka = txtZaporka.Text == "" ? 
						  d.IDDjelatnik.GetHashCode().ToString().Substring(0, 8) : txtZaporka.Text;
					 _ = Repo.DodajDjelatnika(d);
				}
				else
				{
					 Djelatnik stari = Repo.SelectDjelatnik(int.Parse(hiddenIdDjelatnik.Value));
					 d.Zaporka = stari.Zaporka;
					 d.IDDjelatnik = stari.IDDjelatnik;
					 _ = Repo.UpdateDjelatnik(d);
				}

				FillDjelatniciListBox();
				ToggleInputFieldsEnabled(false);
				BtnAdd.Enabled = true;
				BtnSave.Enabled = false;				
		  }

		  protected void BtnAdd_Click(object sender, EventArgs e)
		  {
				hiddenIdDjelatnik.Value = null;
				ToggleInputFieldsEnabled(true);
				ClearAllFields();
				BtnAktiviraj.Enabled = false;
				BtnDeaktiviraj.Enabled = false;
		  }

		  private void ClearAllFields()
		  {
				txtIme.Text = "";
				txtPrezime.Text = "";
				txtEmail.Text = "";
				txtDatum.Text = DateTime.Now.ToString("yyyy-MM-dd");
				txtZaporka.Text = "";
				ChangeDdlTipDjelatnikaSelection(TipDjelatnikaEnum.ZAPOSLENIK);
				BtnAdd.Enabled = false;
				BtnSave.Enabled = true;
		  }

		  protected void BtnDeaktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectDjelatnik(int.Parse(LbDjelatnici.SelectedValue)).IDDjelatnik;

				if (Repo.DeaktivirajDjelatnika(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }

		  protected void BtnAktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectDjelatnik(int.Parse(LbDjelatnici.SelectedValue)).IDDjelatnik;

				if (Repo.AktivirarDjelatnika(id) > 0)
				{
					 BtnDeaktiviraj.Enabled = true;
					 BtnAktiviraj.Enabled = false;
				}
		  }
	 }
}