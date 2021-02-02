using EvidencijaSati.Models.ViewModels;
using EvidencijaSati.Resources;
using ModelsLibrary;
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

		  public ActionResult UnosSatiFromNav()
		  {
				try
				{
					 int id = JsonConvert.DeserializeObject<int>(HttpContext.Session["id"].ToString());
					 ViewBag.TipDjelatnika = 
						  JsonConvert.DeserializeObject<int>(HttpContext.Session["tipDjelatnika"].ToString());
					 return UnosSati(id);
				}
				catch (Exception)
				{
					 return RedirectToAction("Login", "Home");
				}
		  }

		  public ActionResult UnosSati(int id)
        {
				if (HttpContext.Session["id"] != null)
				{
					 if (JsonConvert.DeserializeObject<int>(HttpContext.Session["id"].ToString()) == id)
					 {
						  ViewBag.TipDjelatnika = 
								JsonConvert.DeserializeObject<int>(HttpContext.Session["tipDjelatnika"].ToString());
						  UnosSatiVM model = new UnosSatiVM
						  {
								Djelatnik = Repo.SelectDjelatnik(id),
								Satnica = new Satnica
								{
									 Datum = DateTime.Now,
									 DjelatnikID = id,
									 Satnice = new Dictionary<int, List<SatnicaProjekta>>(),
									 ProjektZabiljezeno = new List<Zapis>(),
									 Staus = SatnicaStatusEnum.WAITING_SUBMIT,
									 Komentar = ""
								},
								Projekti = Repo.GetProjektiDjelatnika(id).ToList(),
								AktivanProjektId = -1
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
								DateTime.Parse(s.Datum.ToString()).Date == DateTime.Now.Date).Any())
						  {
								model.Satnica = satniceDjelatnika.Where(s =>
									 DateTime.Parse(s.Datum.ToString()).Date == DateTime.Now.Date).First();

								if (model.Satnica.Staus == SatnicaStatusEnum.WAITING_SUBMIT)
								{
									 RedirectToAction("", "User");
								}

								List<SatnicaProjekta> sps = Repo.GetSatniceProjekata(model.Satnica.IDSatnica).ToList();

								foreach (var s in sps)
								{
									 if (s.End == new DateTime(0)) model.AktivanProjektId = s.ProjektID;								 
								}

								float total = 0;
								foreach (var p in model.Projekti)
								{
									 model.Satnica.Satnice.Add(p.IDProjekt, sps.Where(s => s.ProjektID == p.IDProjekt).ToList());

									 if (model.Satnica.Satnice[p.IDProjekt].Count > 0)
									 {
										  float t = Utils.CalculateProjectMinutes(model.Satnica.Satnice[p.IDProjekt]);
										  total += t;
										  model.Satnica.ProjektZabiljezeno.Add(new Zapis {
												ProjectId = p.IDProjekt,
												RedovniPrekovremeni = new string[] { Utils.ParseMinutesToString(t), "00:00" }});		
									 }
									 else
									 {
										  model.Satnica.ProjektZabiljezeno.Add(new Zapis
										  {
												ProjectId = p.IDProjekt,
												RedovniPrekovremeni = new string[] { "00:00", "00:00" }
										  });
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
								model.Projekti.ForEach(p => model.Satnica.ProjektZabiljezeno.Add(new Zapis
								{
									 ProjectId = p.IDProjekt,
									 RedovniPrekovremeni = new string[] { "00:00", "00:00" }
								}));
								model.Satnica.IDSatnica = Repo.DodajNovuSatnicu(model.Satnica);
						  }
						  string key = model.Satnica.IDSatnica.ToString();
						  HttpContext.Session.Add(key, JsonConvert.SerializeObject(model.Satnica));
						  HttpContext.Session.Add("satnicaId", JsonConvert.SerializeObject(model.Satnica.IDSatnica));

						  return View("UnosSati", model);
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
					 End = satProj.End
				};

				sp.IDSatnicaProjekta = Repo.SpremiSatnicuProjekta(sp);

				sat.Satnice[satProj.ProjektID].Add(sp);




				foreach (var s in sat.Satnice)
				{
					 if (s.Value.Any())
					 {
						  sat.ProjektZabiljezeno
								.Single(z => z.ProjectId == s.Value.First().ProjektID)
								.RedovniPrekovremeni[0] = 
									 Utils.ParseMinutesToString(Utils.CalculateProjectMinutes(s.Value));

					 }
				}




				HttpContext.Session.Add(key, JsonConvert.SerializeObject(sat));

				int row = sat.Satnice.Keys.ToList().IndexOf(satProj.ProjektID);
								
				string[] res =
				{
					 row.ToString(),
					 Utils.ParseMinutesToString(sp.TotalMin),
					 sp.IDSatnicaProjekta.ToString()
				};

            return Json(res);
		  }

		  [HttpPost]
		  public ActionResult SpremiZaPredaju(Satnica sps)
		  {
				int satId = JsonConvert.DeserializeObject<int>(HttpContext.Session["satnicaId"].ToString());

				Repo.UpdateSatnicaProjektaZaPredaju(sps.IDSatnica, sps.ProjektZabiljezeno);

				string key = satId.ToString();
				Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[key].ToString());

				sat.Komentar = sps.Komentar;
				sat.Total = sps.Total;
				sat.TotalRedovni = sps.TotalRedovni;
				sat.TotalPrekovremeni = sps.TotalPrekovremeni;

				int i = Repo.SpremiSatnicu(sat);
				
				return Json(i);
		  }

		  [HttpPost]
		  public ActionResult ObrisiUnosSatniceProjekta(int id) => Json(Repo.DeleteUnosSatniceProjekta(id));

		  public ActionResult PrikaziInfoProjekta(int projId, int satId)
		  {
				Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[satId.ToString()].ToString());

				if (sat.Satnice[projId].Any())
				{
					 //InfoSatniceProjektaVM model = new InfoSatniceProjektaVM
					 //{
						//  SatniceProjekta = sat.Satnice[projId],
						//  Total = sat.ProjektZabiljezeno[projId][0]
					 //};

					 //return PartialView("SatnicaProjektaInfo", model); 
				}
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
		  }

		  public ActionResult UpdateUnosSatniceProjekta(SatnicaProjekta satnica)
		  {
				int i = Repo.UpdateSatnicaProjekta(satnica);

				string[] res =
				{
					 satnica.IDSatnicaProjekta.ToString(),
					 satnica.ProjektID.ToString()
				};

				return i != 1 ? Json("error") : Json(res);
		  }

		  public ActionResult ZabiljeziKraj(SatnicaProjekta satnica)
		  {
				SatnicaProjekta s = Repo.SelectRadnaSatnicaProjekta(satnica.ProjektID);
				Satnica sat = 
					 JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[s.SatnicaID.ToString()].ToString());
				
				s.End = satnica.End;

				var startEnd = float.Parse((s.End - s.Start).TotalMinutes.ToString());
				s.TotalMin = startEnd;

				if (Repo.UpdateEndSatniceProjekta(s.End, s.IDSatnicaProjekta, startEnd) > 0)
				{
					 sat.Satnice[s.ProjektID][sat.Satnice[s.ProjektID].Count - 1] = s;
				}

				HttpContext.Session.Add(s.SatnicaID.ToString(), JsonConvert.SerializeObject(sat));

				return Json(Utils.ParseMinutesToString(s.TotalMin));
		  }

		  [HttpPost]
		  public ActionResult PredajNaProvjeru(int id)
		  {
				int i = Repo.ChangeSatnicaStatus(id, SatnicaStatusEnum.WAITING_APPROVAL);
				ViewBag.TipDjelatnika =
						  JsonConvert.DeserializeObject<int>(HttpContext.Session["tipDjelatnika"].ToString());
				if (i > 0) return PartialView("SuccessPartial");
				else return View("Error");
		  }

		  public ActionResult PregledSatnica()
		  {
				try
				{
					 int id = JsonConvert.DeserializeObject<int>(HttpContext.Session["id"].ToString());
					 int tipDjelatnika = JsonConvert.DeserializeObject<int>(HttpContext.Session["tipDjelatnika"].ToString());
					 ViewBag.TipDjelatnika = tipDjelatnika;

					 PregledSatnicaVM model = new PregledSatnicaVM
					 {
						  Djelatnik = Repo.SelectDjelatnik(id),
						  Satnice =
								Repo.GetSatniceProjektaZaVoditeljaDirektora(
									 id,
									 tipDjelatnika,
									 (int)SatnicaStatusEnum.WAITING_APPROVAL).ToList(),
						  Projekti = Repo.GetProjektiDjelatnika(id).ToList()
					 };

					 if (model.Satnice.Count() > 0)
					 {
						  return View(model);
					 }
					 else
					 {
						  return View("Error", new ErrorVM
						  {
								Msg = Common.Nema_satnica
						  });
					 }

				}
				catch (Exception)
				{
					 return RedirectToAction("Login", "Home");
				}
		  }
	 }
}