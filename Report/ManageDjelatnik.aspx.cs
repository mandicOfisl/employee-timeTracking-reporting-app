using Report.Models;
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
				}
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