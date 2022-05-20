using EvidencijaSati.Models.ViewModels;
using ModelsLibrary;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace EvidencijaSati.Controllers
{
    public class UserController : Controller
    {
        public ActionResult UserProfile()
        {
            if (HttpContext.Session["id"] == null) 
                return RedirectToAction("Login", "Home");

            var id = JsonConvert.DeserializeObject<int>(HttpContext.Session["id"].ToString());
            var djelatnik = Repo.SelectDjelatnik(id);
            ViewBag.TipDjelatnika = djelatnik.TipDjelatnikaID;

            return View(djelatnik);
        }

        public ActionResult PromijeniZaporku(int id) => PartialView("PromijeniZaporku", Repo.SelectDjelatnik(id));

        [HttpPost]
        public ActionResult UpdateZaporka(Djelatnik d)
        {
            int i = Repo.UpdateZaporka(d.IDDjelatnik, d.Zaporka);

            if (i > 0)
            {
                ViewBag.Id = d.IDDjelatnik;
                ViewBag.TipDjelatnika = d.TipDjelatnikaID;
                return View("Success");
            }
            else
            {
                ErrorVM model = new ErrorVM
                {
                    Msg = Resources.Common.Greska
                };
                return View("Error", model);
            }
        }
    }
}