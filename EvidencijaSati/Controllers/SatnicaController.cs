using EvidencijaSati.Models;
using EvidencijaSati.Models.ViewModels;
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
                Projekti = Repo.GetProjektiDjelatnika(id).ToList(),
                Satnica = new Satnica()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SpremiTempSatnicu(Satnica satnica)
		  {
            //Repo.SaveTempSatnica(satnica);
            
            double diff = satnica.End.Subtract(satnica.Start).TotalSeconds;

            string s = satnica.Start.Hour.ToString().PadLeft(2, '0') + ":" 
                + satnica.Start.Minute.ToString().PadLeft(2, '0');

            string e = satnica.End.Hour.ToString().PadLeft(2, '0') + ":" 
                + satnica.End.Minute.ToString().PadLeft(2, '0');

            diff = Math.Ceiling(diff / 60);

            string[] res =
            {
                s, e, diff.ToString()
            };

            return Json(res);
		  }
    }
}