using EvidencijaSati.Models;
using EvidencijaSati.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            UnosSatiVM model = new UnosSatiVM
            {
                Djelatnik = Repo.SelectDjelatnik(id),
                Satnica = new Satnica {
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
            model.Satnica.IDSatnica = Repo.DodajNovuSatnicu(model.Satnica);
            string key = "satnica" + model.Satnica.IDSatnica.ToString();
            HttpContext.Session.Add(key, JsonConvert.SerializeObject(model.Satnica));

            return View(model);
        }

        [HttpPost]
        public ActionResult SpremiTempSatnicu(SatnicaProjekta satnica)
		  {
            string key = "satnica" + satnica.SatnicaID.ToString();
            Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[key].ToString());

            Projekt p = Repo.SelectProjekt(int.Parse(satnica.ProjektID));

            sat.Satnice[p.Naziv].Add(new SatnicaProjekta
            {
                IDSatnicaProjekta = (sat.IDSatnica + satnica.ProjektID).GetHashCode(),
                SatnicaID = sat.IDSatnica,
                ProjektID = satnica.ProjektID,
                Start = satnica.Start,
                End = satnica.End,
                StartEnd = float.Parse((satnica.End - satnica.Start).TotalMinutes.ToString())
            });

            Repo.SpremiSatnicuProjekta(sat.Satnice[p.Naziv].Last());
            HttpContext.Session.Add(key, JsonConvert.SerializeObject(sat));


            return Json("ok");
		  }
    }
}