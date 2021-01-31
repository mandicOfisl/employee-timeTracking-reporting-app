using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Report
{
	 public partial class ManageTim : System.Web.UI.Page
	 {
		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (!IsPostBack)
				{
					 FillTimoviListBox();
				}
		  }

		  private void FillTimoviListBox()
		  {
				LbTimovi.DataSource = Repo.GetTimovi();
				LbTimovi.DataTextField = "Naziv";
				LbTimovi.DataValueField = "IDTim";
				LbTimovi.DataBind();
		  }

		  protected void LbTimovi_SelectedIndexChanged(object sender, EventArgs e)
		  {
				if (LbTimovi.SelectedIndex > -1)
				{
					 hiddenIdTim.Value = null;
					 Tim t = Repo.SelectTim(int.Parse(LbTimovi.SelectedValue));
					 txtIme.Text = t.Naziv;
					 txtDatum.Text = t.DatumKreiranja.ToString("yyyy-MM-dd");

					 FillClanoviTimaListBox(t.IDTim);
					 ToggleInputFieldsEnabled(false);

					 BtnEdit.Enabled = true;
					 BtnSave.Enabled = false;

					 BtnDeaktiviraj.Enabled = t.IsActive;
					 BtnAktiviraj.Enabled = !t.IsActive;
				}
		  }

		  private void ToggleInputFieldsEnabled(bool isEnabled)
		  {
				txtIme.Enabled = isEnabled;
				txtDatum.Enabled = isEnabled;
		  }

		  private void FillClanoviTimaListBox(int iDTim)
		  {
				lbClanoviTima.DataSource = Repo.GetClanoviTima(iDTim);
				lbClanoviTima.DataTextField = "FullName";
				lbClanoviTima.DataValueField = "IDDjelatnik";
				lbClanoviTima.DataBind();
		  }

		  protected void BtnDeaktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectTim(int.Parse(LbTimovi.SelectedValue)).IDTim;

				if (Repo.DeaktivirajTim(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }

		  protected void BtnSave_Click(object sender, EventArgs e)
		  {
				Tim t = new Tim
				{
					 Naziv = txtIme.Text,
					 DatumKreiranja = DateTime.Parse(txtDatum.Text)
				};

				if (string.IsNullOrEmpty(hiddenIdTim.Value))
				{
					 _ = Repo.AddTim(t);
				}
				else
				{
					 t.IDTim = int.Parse(hiddenIdTim.Value);
					 _ = Repo.UpdateTim(t);
				}

				FillTimoviListBox();
				ToggleInputFieldsEnabled(false);
				BtnAdd.Enabled = true;
				BtnSave.Enabled = false;
				
		  }

		  protected void BtnAdd_Click(object sender, EventArgs e)
		  {
				hiddenIdTim.Value = null;
				ToggleInputFieldsEnabled(true);
				ClearAllFields();
				BtnAktiviraj.Enabled = false;
				BtnDeaktiviraj.Enabled = false;
		  }

		  private void ClearAllFields()
		  {
				txtIme.Text = "";
				txtDatum.Text = DateTime.Now.ToString("yyyy-MM-dd");
		  }

		  protected void BtnEdit_Click(object sender, EventArgs e)
		  {
				hiddenIdTim.Value = int.Parse(LbTimovi.SelectedValue).ToString();

				ToggleInputFieldsEnabled(true);

				BtnEdit.Enabled = false;
				BtnSave.Enabled = true;
		  }

		  protected void BtnAktiviraj_Click(object sender, EventArgs e)
		  {
				int id = Repo.SelectTim(int.Parse(LbTimovi.SelectedValue)).IDTim;

				if (Repo.AktivirajTim(id) > 0)
				{
					 BtnAktiviraj.Enabled = true;
					 BtnDeaktiviraj.Enabled = false;
				}
		  }
	 }
}