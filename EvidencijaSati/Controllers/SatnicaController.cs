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
		  private readonly double RADNI_SATI_U_DANU = 8 * 60;

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
									 Datum = DateTime.Now,
									 DjelatnikID = id,
									 Satnice = new Dictionary<int, List<SatnicaProjekta>>(),
									 ProjektZabiljezeno = new Dictionary<int, string>(),
									 Staus = SatnicaStatusEnum.WAITING_SUBMIT
								},
								Projekti = Repo.GetProjektiDjelatnika(id).ToList()
						  };

						  foreach (var p in model.Projekti)
						  {
								model.Satnica.Satnice.Add(p.IDProjekt, new List<SatnicaProjekta>
									 {
										  new SatnicaProjekta {
												ProjektID = p.IDProjekt,
										  }
									 });
						  }

						  List<Satnica> satniceDjelatnika = Repo.GetSatniceDjelatnika(id).ToList();

						  if (satniceDjelatnika.Where(s => 
								DateTime.Parse(s.Datum.ToString()).Date == DateTime.Now.Date).Count() > 0)
						  {
								model.Satnica = satniceDjelatnika.Where(s =>
									 DateTime.Parse(s.Datum.ToString()).Date == DateTime.Now.Date).First();
								List<SatnicaProjekta> sps = Repo.GetSatniceProjekata(model.Satnica.IDSatnica).ToList();
								float total = 0;
								foreach (var p in model.Projekti)
								{
									 model.Satnica.Satnice.Add(p.IDProjekt, sps.Where(s => s.ProjektID == p.IDProjekt).ToList());
									 if (model.Satnica.Satnice[p.IDProjekt].Count > 0)
									 {
										  float t = Utils.CalculateProjectMinutes(model.Satnica.Satnice[p.IDProjekt]);
										  total += t;
										  model.Satnica.ProjektZabiljezeno.Add(p.IDProjekt, 
												Utils.ParseMinutesToString(t));
									 }
									 else
									 {
										  model.Satnica.ProjektZabiljezeno.Add(p.IDProjekt, "00:00");
									 }
								}

								model.Satnica.Total = double.Parse(total.ToString());

								if (model.Satnica.Total > RADNI_SATI_U_DANU)
								{
									 model.Satnica.TotalRedovni = RADNI_SATI_U_DANU;
									 model.Satnica.TotalPrekovremeni = model.Satnica.Total - RADNI_SATI_U_DANU;
								}
								else
								{									 
									 model.Satnica.TotalRedovni = model.Satnica.Total;
								}
						  }
						  else
						  {
								model.Projekti.ForEach(p => model.Satnica.ProjektZabiljezeno.Add(p.IDProjekt, "00:00"));
								model.Satnica.IDSatnica = Repo.DodajNovuSatnicu(model.Satnica);
						  }
						  string key = model.Satnica.IDSatnica.ToString();
						  HttpContext.Session.Add(key, JsonConvert.SerializeObject(model.Satnica));
						  HttpContext.Session.Add("satnicaId", JsonConvert.SerializeObject(model.Satnica.IDSatnica));

						  return View(model);
					 }
				}
				return RedirectToAction("Login", "Home");
		  }

        [HttpPost]
        public ActionResult SpremiTempSatnicu(SatnicaProjekta satProj)
		  {				
				string key = satProj.SatnicaID.ToString();
            Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[key].ToString());

				SatnicaProjekta sp = new SatnicaProjekta
				{
					 SatnicaID = sat.IDSatnica,
					 ProjektID = satProj.ProjektID,
					 Start = satProj.Start,
					 End = satProj.End,
					 StartEnd = 0
				};

				sp.IDSatnicaProjekta = Repo.SpremiSatnicuProjekta(sp);

				sat.Satnice[satProj.ProjektID].Add(sp);

				foreach (var s in sat.Satnice)
				{
					 sat.ProjektZabiljezeno[s.Value.First().ProjektID] =
								Utils.ParseMinutesToString(Utils.CalculateProjectMinutes(s.Value));
				}
								
            HttpContext.Session.Add(key, JsonConvert.SerializeObject(sat));

				int row = sat.Satnice.Keys.ToList().IndexOf(satProj.ProjektID);
								
				string[] res =
				{
					 row.ToString(),
					 Utils.ParseMinutesToString(sp.StartEnd)
				};

            return Json(res);
		  }

		  [HttpPost]
		  public ActionResult SpremiZaPredaju(SatnicaProjekta sps)
		  {
				int satId = JsonConvert.DeserializeObject<int>(HttpContext.Session["satnicaId"].ToString());

				string key = satId.ToString();
				Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[key].ToString());

				SatnicaProjekta sp = new SatnicaProjekta
				{
					 IDSatnicaProjekta = sps.IDSatnicaProjekta,
					 SatnicaID = satId,
					 ProjektID = sps.ProjektID,
					 Start = sps.Start,
					 End = sps.End,
					 StartEnd = sps.StartEnd					 
				};

				Repo.SpremiSatnicuProjekta(sp);
				

				//int r = Repo.ChangeSatnicaStatus(satId, SatnicaStatusEnum.WAITING_APPROVAL);

				return Json("ok");
		  }

		  [HttpPost]
		  public ActionResult ObrisiUnosSatniceProjekta(int id)
		  {
				int i = Repo.DeleteUnosSatniceProjekta(id);

				return i != 1 ? Json("error") : Json(i);
		  }

		  public ActionResult PrikaziInfoProjekta(int projId, int satId)
		  {
				Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[satId.ToString()].ToString());

				return PartialView("SatnicaProjektaInfo", sat.Satnice[projId]);
		  }

		  public ActionResult UpdateUnosSatniceProjekta(SatnicaProjekta satnica)
		  {
				int i = Repo.UpdateSatnicaProjekta(satnica);

				



				return i != 1 ? Json("error") : Json(i);
		  }
	 }
}