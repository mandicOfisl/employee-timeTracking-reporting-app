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
                    DjelatnikID = id,
                    Satnice = new List<SatnicaProjekta>()
                }
            };
            foreach (var p in Repo.GetProjektiDjelatnika(id))
				{
                model.Satnica.Satnice.Add(new SatnicaProjekta
                {
                    Projekt = p
                });
				}
            string key = "satnica" + id.ToString();
            HttpContext.Session.Add(key, JsonConvert.SerializeObject(model.Satnica));

            return View(model);
        }

        [HttpPost]
        public ActionResult SpremiTempSatnicu(SatnicaProjekta satnica)
		  {
            string key = "satnica" + satnica.SatnicaID.ToString();
            Satnica sat = JsonConvert.DeserializeObject<Satnica>(HttpContext.Session[key].ToString());
            sat.Satnice.Add(new SatnicaProjekta
            {
                IDSatnicaProjekta = (sat.IDSatnica + satnica.Projekt.IDProjekt).GetHashCode(),
                SatnicaID = sat.IDSatnica,
                Projekt = Repo.SelectProjekt(satnica.Projekt.IDProjekt),
                StartEnd = satnica.StartEnd
            });



            return Json("ok");
		  }
    }
}