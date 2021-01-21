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
                Projekti = Repo.GetProjektiDjelatnika(id).ToList()
            };

            return View(model);
        }
    }
}