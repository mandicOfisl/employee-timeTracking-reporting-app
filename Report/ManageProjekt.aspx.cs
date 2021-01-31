using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Report
{
	 public partial class ManageProjekt : Page
	 {
		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (!IsPostBack)
				{
					 FillProjektiListBox();
					 FillKlijentDdl();
					 FillVoditeljProjektaDdl();
				}
		  }

		  private void FillProjektiListBox()
		  {
				LbProjekti.DataSource = Repo.GetProjekti();
				LbProjekti.DataTextField = "Naziv";
				LbProjekti.DataValueField = "IDProjekt";
				LbProjekti.DataBind();
		  }

		  private void FillKlijentDdl()
		  {
				ddlKlijent.DataSource = Repo.GetKlijenti();
				ddlKlijent.DataTextField = "Naziv";
				ddlKlijent.DataValueField = "IDKlijent";
				ddlKlijent.DataBind();
		  }

		  private void FillVoditeljProjektaDdl()
		  {
				ddlVoditeljProjekta.DataSource = Repo.GetVoditeljiProjekta();
				ddlVoditeljProjekta.DataTextField = "FullName";
				ddlVoditeljProjekta.DataValueField = "IDDjelatnik";
				ddlVoditeljProjekta.DataBind();
		  }

		  protected void LbProjekti_SelectedIndexChanged(object sender, EventArgs e)
		  {
				if (LbProjekti.SelectedIndex > -1)
				{
					 hiddenIdProjekt.Value = null;
					 Projekt p = Repo.SelectProjekt(int.Parse(LbProjekti.SelectedValue));
					 txtNaziv.Text = p.Naziv;
					 txtDatumOtvaranja.Text = p.DatumOtvaranja.ToString("yyyy-MM-dd");

					 if (p.DatumZatvaranja == p.DatumOtvaranja)
					 {
						  txtDatumZatvaranja.Text = string.Empty;
					 }
					 else
					 {
						  txtDatumZatvaranja.Text = p.DatumZatvaranja.ToString("yyyy-MM-dd");
					 }					 
					 
					 ChangeDdlKlijentSelection(p.KlijentID);
					 ChangeDdlVoditeljProjektaSelection(p.VoditeljProjektaID);
					 FillZaposleniNaProjektuListBox(p.IDProjekt);
					 FillDdlDjelatnik(p.IDProjekt);
					 ToggleInputFieldsEnabled(false);

					 BtnEdit.Enabled = true;
					 BtnSave.Enabled = false;

					 BtnDodajDjelatnika.Enabled = false;
					 BtnUkloniDjelatnika.Enabled = false;

					 BtnDeaktiviraj.Enabled = p.IsActive;
					 BtnAktiviraj.Enabled = !p.IsActive;
				}
		  }

		  private void FillDdlDjelatnik(int iDProjekt)
		  {
				List<Djelatnik> zaposleni = Repo.GetZaposleniNaProjektu(iDProjekt).ToList();
				List<Djelatnik> nezaposleni = 
					 Repo.GetDjelatnici().Where(d => (int)d.TipDjelatnikaID > 2 && !zaposleni.Contains(d) && d.IsActive).ToList();

				DdlDjelatnik.DataSource = nezaposleni;
				DdlDjelatnik.DataTextField = "FullName";
				DdlDjelatnik.DataValueField = "IDDjelatnik";
				DdlDjelatnik.DataBind();
		  }

		  private void FillZaposleniNaProjektuListBox(int iDProjekt)
		  {
				LbZaposleniNaProjektu.DataSource = Repo.GetZaposleniNaProjektu(iDProjekt);
				LbZaposleniNaProjektu.DataTextField = "FullName";
				LbZaposleniNaProjektu.DataValueField = "IDDjelatnik";
				LbZaposleniNaProjektu.DataBind();
		  }

		  private void ChangeDdlKlijentSelection(int klijentID)
		  {
				ddlKlijent.SelectedValue = null;
				ListItem li = ddlKlijent.Items.FindByValue(klijentID.ToString());
				if (li != null) li.Selected = true;
		  }

		  private void ChangeDdlVoditeljProjektaSelection(int voditeljProjektaID)
		  {
				ddlVoditeljProjekta.SelectedValue = null;
				ListItem li = ddlVoditeljProjekta.Items.FindByValue(voditeljProjektaID.ToString());
				if (li != null) li.Selected = true;
		  }

		  protected void BtnEdit_Click(object sender, EventArgs e)
		  {
				hiddenIdProjekt.Value = int.Parse(LbProjekti.SelectedValue).ToString();

				ToggleInputFieldsEnabled(true);

				BtnEdit.Enabled = false;
				BtnSave.Enabled = true;
		  }

		  private void ToggleInputFieldsEnabled(bool isEnabled)
		  {
				txtNaziv.Enabled = isEnabled;
				txtDatumOtvaranja.Enabled = isEnabled;
				txtDatumZatvaranja.Enabled = isEnabled;
				ddlKlijent.Enabled = isEnabled;
				ddlVoditeljProjekta.Enabled = isEnabled;
		  }

		  protected void BtnAdd_Click(object sender, EventArgs e)
		  {
				hiddenIdProjekt.Value = null;
				ToggleInputFieldsEnabled(true);
				ClearAllFields();
				BtnAktiviraj.Enabled = false;
				BtnDeaktiviraj.Enabled = false;
		  }

		  private void ClearAllFields()
		  {
				txtNaziv.Text = "";
				txtDatumOtvaranja.Text = DateTime.Now.ToString("yyyy-MM-dd");
				txtDatumZatvaranja.Text = string.Empty;
		  }

		  protected void BtnSave_Click(object sender, EventArgs e)
		  {
				DateTime dz;

				Projekt p = new Projekt
				{
					 Naziv = txtNaziv.Text,
					 KlijentID = int.Parse(ddlKlijent.SelectedValue),
					 DatumOtvaranja = DateTime.Parse(txtDatumOtvaranja.Text),
					 DatumZatvaranja = DateTime.TryParse(txtDatumZatvaranja.Text, out dz) ?
						  dz : DateTime.Parse(txtDatumOtvaranja.Text),
					 VoditeljProjektaID = int.Parse(ddlVoditeljProjekta.SelectedValue)
				};

				if (string.IsNullOrEmpty(hiddenIdProjekt.Value))
				{
					 _ = Repo.DodajProjekt(p);
				}
				else
				{
					 Projekt stari = Repo.SelectProjekt(int.Parse(hiddenIdProjekt.Value));
					 p.IDProjekt = stari.IDProjekt;
					 int i = Repo.UpdateProjekt(p);
				}

				FillProjektiListBox();
				ToggleInputFieldsEnabled(false);
				BtnAdd.Enabled = true;
				BtnSave.Enabled = false;
		  }

		  protected void BtnDeaktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectProjekt(int.Parse(LbProjekti.SelectedValue)).IDProjekt;

				if (Repo.DeaktivirajProjekt(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }

		  protected void BtnAktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectProjekt(int.Parse(LbProjekti.SelectedValue)).IDProjekt;

				if (Repo.AktivirajProjekt(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }

		  protected void BtnDodajDjelatnika_Click(object sender, EventArgs e)
		  {
				int projId = int.Parse(LbProjekti.SelectedValue);
				int djelatnikId = int.Parse(DdlDjelatnik.SelectedValue);

				if (Repo.DodajDjelatnikaNaProjekt(projId, djelatnikId) > 0)
				{
					 FillZaposleniNaProjektuListBox(projId);
					 FillDdlDjelatnik(projId);
					 BtnDodajDjelatnika.Enabled = false;
					 BtnUkloniDjelatnika.Enabled = false;
				}
		  }

		  protected void BtnUkloniDjelatnika_Click(object sender, EventArgs e)
		  {
				int projId = int.Parse(LbProjekti.SelectedValue);
				int djelatnikId = int.Parse(LbZaposleniNaProjektu.SelectedValue);

				if (Repo.UkloniDjelatnikaSProjekta(projId, djelatnikId) > 0)
				{
					 FillZaposleniNaProjektuListBox(projId);
					 FillDdlDjelatnik(projId);
					 BtnUkloniDjelatnika.Enabled = false;
					 BtnDodajDjelatnika.Enabled = false;
				}
		  }

		  protected void DdlDjelatnik_SelectedIndexChanged(object sender, EventArgs e)
		  {
				BtnDodajDjelatnika.Enabled = true;
		  }

		  protected void LbZaposleniNaProjektu_SelectedIndexChanged(object sender, EventArgs e)
		  {
				BtnUkloniDjelatnika.Enabled = true;
		  }
	 }
}