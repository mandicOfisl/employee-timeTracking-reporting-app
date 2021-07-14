using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Report
{
	 public partial class TimReport : System.Web.UI.Page
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
					 FillTimoviDdl();
				}
		  }

		  private void FillTimoviDdl()
		  {
				ddlTimovi.DataSource = Repo.GetTimovi();
				ddlTimovi.DataTextField = "Naziv";
				ddlTimovi.DataValueField = "IDTim";
				ddlTimovi.DataBind();
		  }

		  protected void BtnSearch_Click(object sender, EventArgs e)
		  {
				if (ddlTimovi.SelectedIndex > -1)
				{
					 gvTable.Visible = false;

					 int timId = int.Parse(ddlTimovi.SelectedValue);
					 DateTime from = DateTime.Parse(DtpFrom.Text);
					 DateTime to = DateTime.Parse(DtpTo.Text);

					 List<Djelatnik> djelatnici = Repo.GetClanoviTima(timId).ToList();

					 List<Satnica> satnice = Repo.GetTimReport(timId, from, to).ToList();

					 if (satnice.Count > 0)
					 {
						  CreateTable(djelatnici, satnice);
						  gvTable.Visible = true;
						  BtnCsv.Enabled = true;
					 }

				}

		  }

		  private void CreateTable(List<Djelatnik> djelatnici, List<Satnica> satnice)
		  {
				DataTable tb = new DataTable();
				DataRow dr;

				tb.Columns.Add("Ime i prezime", typeof(string));
				tb.Columns.Add("Tip djelatnika", typeof(string));
				tb.Columns.Add("Redovni sati", typeof(double));
				tb.Columns.Add("Prekovremeni sati", typeof(double));
				tb.Columns.Add("Ukupno", typeof(double));

				foreach (Satnica s in satnice)
				{
					 dr = tb.NewRow();

					 var djelatnik = djelatnici.First(d => d.IDDjelatnik == s.DjelatnikID);

					 dr["Ime i prezime"] = djelatnik.Ime + " " + djelatnik.Prezime;

					 switch (djelatnik.TipDjelatnikaID)
					 {
						  case TipDjelatnikaEnum.DIREKTOR:
								dr["Tip djelatnika"] = "Direktor";
								break;
						  case TipDjelatnikaEnum.VODITELJ_TIMA:
								dr["Tip djelatnika"] = "Voditelj tima";
								break;
						  case TipDjelatnikaEnum.ZAPOSLENIK:
								dr["Tip djelatnika"] = "Zaposlenik";
								break;
						  case TipDjelatnikaEnum.HONORARNI_DJELATNIK:
								dr["Tip djelatnika"] = "Honorarni djelatnik";
								break;
						  case TipDjelatnikaEnum.STUDENT:
								dr["Tip djelatnika"] = "Student";
								break;
						  default:
								dr["Tip djelatnika"] = "";
								break;
					 }

					 dr["Redovni sati"] = Math.Round((double)((s.TotalRedovni) / 60), 2);
					 dr["Prekovremeni sati"] = Math.Round((double)((s.TotalPrekovremeni) / 60), 2);
					 dr["Ukupno"] = Math.Round((double)((s.Total) / 60), 2);

					 tb.Rows.Add(dr);

				}

				gvTable.DataSource = tb;
				gvTable.DataBind();

				gvTable.FooterRow.Cells[0].Text = "Ukupno";
				gvTable.FooterRow.Cells[0].Font.Bold = true;

				double total = 0;

				for (int i = 2; i < tb.Columns.Count; i++)
				{
					 total = tb.AsEnumerable().Sum(row => row.Field<double>(tb.Columns[i].ToString()));
					 gvTable.FooterRow.Cells[i].Text = total.ToString();
					 gvTable.FooterRow.Cells[i].Font.Bold = true;
				}
		  }

	 }
}