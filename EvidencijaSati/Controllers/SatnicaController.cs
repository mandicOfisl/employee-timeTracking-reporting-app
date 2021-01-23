using EvidencijaSati.Models;
using EvidencijaSati.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaSati.Controllers
{
    public class SatnicaController : Controller
    {
        // GET: Satnica
        public ActionResult UnosSati(int id)
        {
				if (HttpContext.Session["id"] != null)
				{
					 if (JsonConvert.DeserializeObject<int>(HttpContext.Session["id"].ToString()) == id)
					 {
						  UnosSatiVM model = new UnosSatiVM
						  {
								Djelatnik = Repo.SelectDjelatnik(id),
								Satnica = new Satnica
								{
									 IDSatnica = DateTime.Now.AddDays((double)id).GetHashCode(),
									 Datum = DateTime.Now,
									 DjelatnikID = id,
									 Satnice = new Dictionary<string, List<SatnicaProjekta>>(),
									 Staus = SatnicaStatusEnum.WAITING_APPROVAL
								},
								Projekti = Repo.GetProjektiDjelatnika(id).ToList()
						  };

						  foreach (var p in model.Projekti)
						  {
								model.Satnica.Satnice.Add(p.Naziv, new List<SatnicaProjekta>
									 {
										  new SatnicaProjekta {
												ProjektID = p.IDProjekt.ToString(),
										  }
									 });
						  }

						  List<Satnica> satniceDjelatnika = Repo.GetSatniceDjelatnika(id).ToList();

						  if (satniceDjelatnika.Where(s => 
								DateTime.Parse(s.Datum.ToString()).Date == DateTime.Now.Date).Count() > 0)
						  {
								model.Satnica = satniceDjelatnika.Where(s =>
									 DateTime.Parse(s.Datum.ToString()).Date == DateTime.Now.Date).First();
								List<SatnicaProjekta> sps = Repo.GetSatniceProjekata(model.Satnica.IDSatnica);
						  }
						  else
						  {
								model.Satnica.IDSatnica = Repo.DodajNovuSatnicu(model.Satnica);
						  }
						  string key = "satnica" + model.Satnica.IDSatnica.ToString();
						  HttpContext.Session.Add(key, JsonConvert.SerializeObject(model.Satnica));

						  return View(model);
					 }
				}
				return RedirectToAction("Login", "Home");
		  }

        [HttpPost]
        public ActionResult SpremiTempSatnicu(SatnicaProjekta satnica)
		  {
            string key = "satnica" + satnica.SatnicaID.ToString();
            Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[key].ToString());

            Projekt p = Repo.SelectProjekt(int.Parse(satnica.ProjektID));

				SatnicaProjekta sp = new SatnicaProjekta
				{
					 IDSatnicaProjekta = (sat.IDSatnica + satnica.ProjektID).GetHashCode(),
					 SatnicaID = sat.IDSatnica,
					 ProjektID = satnica.ProjektID,
					 Start = satnica.Start,
					 End = satnica.End,
					 StartEnd = float.Parse((satnica.End - satnica.Start).TotalMinutes.ToString())
				};

				sat.Satnice[p.Naziv].Add(sp);
				
				Repo.SpremiSatnicuProjekta(sat.Satnice[p.Naziv].Last());
            HttpContext.Session.Add(key, JsonConvert.SerializeObject(sat));

				int row = sat.Satnice.Keys.ToList().IndexOf(p.Naziv);

				float zabiljezeno = CalculateProjectMinutes(sat.Satnice[p.Naziv]);


				string zab = Utils.ParseMinutesToString(zabiljezeno);
				string[] res =
				{
					 row.ToString(), zab, (zabiljezeno / 60 > 8).ToString()
				};

            return Json(res);
		  }

		  private float CalculateProjectMinutes(List<SatnicaProjekta> lists)
		  {
				float total = 0;

				foreach (var sp in lists)
				{
					 total += sp.StartEnd;
				}

				return total;
		  }

		  private double CalculateTotalMinutes(Dictionary<string, List<SatnicaProjekta>> satnice)
		  {
				double total = 0;

				foreach (var lista in satnice)
				{
					 foreach (var s in lista.Value)
					 {
						  total += double.Parse(s.StartEnd.ToString());
					 }
				}

				return total;
		  }

	 }
}