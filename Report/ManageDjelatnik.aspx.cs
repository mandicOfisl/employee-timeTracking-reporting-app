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
		  public List<Tim> Timovi { get; set; }
		  protected void Page_Load(object sender, EventArgs e)
		  {
				if (!IsPostBack)
				{
					 FillDjelatniciListBox();
					 FillTimoviDdl();
				}
		  }

		  private void FillTimoviDdl()
		  {
				Timovi = Repo.GetTimovi().ToList();
				ddlTim.DataSource = Timovi;
				ddlTim.DataTextField = "Naziv";
				ddlTim.DataValueField = "IDTim";
				ddlTim.DataBind();
		  }

		  private void FillDjelatniciListBox()
		  {
				LbDjelatnici.DataSource = Repo.GetDjelatnici();
				LbDjelatnici.DataTextField = "FullName";
				LbDjelatnici.DataValueField = "IDDjelatnik";
				LbDjelatnici.DataBind();
		  }

		  protected void LbDjelatnici_SelectedIndexChanged(object sender, EventArgs e)
		  {
				if (LbDjelatnici.SelectedIndex > -1)
				{
					 Djelatnik d = Repo.SelectDjelatnik(int.Parse(LbDjelatnici.SelectedValue));
					 txtIme.Text = d.Ime;
					 txtPrezime.Text = d.Prezime;
					 txtEmail.Text = d.Email;

					 ChangeDdlTimSelection(d.TimID);

					 FillProjektiListBox(d.IDDjelatnik);
				}
		  }

		  private void ChangeDdlTimSelection(int timID)
		  {
				ddlTim.SelectedValue = null;
				ListItem li = ddlTim.Items.FindByValue(timID.ToString());
				if (li != null) li.Selected = true;
		  }

		  private void FillProjektiListBox(int djelatnikId)
		  {
				lbProjekti.DataSource = Repo.GetProjektiDjelatnika(djelatnikId);
				lbProjekti.DataTextField = "Naziv";
				lbProjekti.DataValueField = "IDProjekt";
				lbProjekti.DataBind();
		  }

		  protected void BtnEdit_Click(object sender, EventArgs e)
		  {

		  }

		  protected void BtnSave_Click(object sender, EventArgs e)
		  {

		  }

		  protected void BtnAdd_Click(object sender, EventArgs e)
		  {

		  }

		  protected void LbProjekti_SelectedIndexChanged(object sender, EventArgs e)
		  {

		  }

		  protected void BtnChangePass_Click(object sender, EventArgs e)
		  {

		  }

		  protected void BtnDeaktiviraj_Click(object sender, EventArgs e)
		  {

		  }
	 }
}