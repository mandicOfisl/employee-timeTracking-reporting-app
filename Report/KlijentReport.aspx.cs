using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Report
{
	 public partial class KlijentReport : System.Web.UI.Page
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
				if (!IsPostBack)
				{
					 FillKlijentiDdl();
				}
		  }

		  private void FillKlijentiDdl()
		  {
				ddlKlijenti.DataSource = Repo.GetKlijenti();
				ddlKlijenti.DataTextField = "Naziv";
				ddlKlijenti.DataValueField = "IDKlijent";
				ddlKlijenti.DataBind();
		  }

		  protected void BtnSearch_Click(object sender, EventArgs e)
		  {
				if (ddlKlijenti.SelectedIndex > -1)
				{
					 gvTable.Visible = false;

					 int klijentId = int.Parse(ddlKlijenti.SelectedValue);
					 DateTime from = DateTime.Parse(DtpFrom.Text);
					 DateTime to = DateTime.Parse(DtpTo.Text);

					 List<KlijentReportModel> satnice = Repo.GetKlijentReport(klijentId, from, to).ToList();

					 if (satnice.Count > 0)
					 {
						  CreateTable(satnice);
						  gvTable.Visible = true;
						  BtnCsv.Enabled = true;
					 }

				}
		  }

		  private void CreateTable(List<KlijentReportModel> satnice)
		  {
				DataTable tb = new DataTable();
				DataRow dr;

				tb.Columns.Add("Naziv projekta", typeof(string));
				tb.Columns.Add("Ukupno", typeof(double));

				foreach (KlijentReportModel s in satnice)
				{
					 dr = tb.NewRow();

					 dr["Naziv projekta"] = s.NazivProjekta;
					 dr["Ukupno"] = Math.Round((double)((s.Total)/ 60), 2);

					 tb.Rows.Add(dr);
				}

				gvTable.DataSource = tb;
				gvTable.DataBind();

				gvTable.FooterRow.Cells[0].Text = "Ukupno";
				gvTable.FooterRow.Cells[0].Font.Bold = true;

				gvTable.FooterRow.Cells[1].Text =
					 tb.AsEnumerable().Sum(row => row.Field<double>(tb.Columns[1].ToString())).ToString();
				gvTable.FooterRow.Cells[1].Font.Bold = true;
		  }

	 }
}