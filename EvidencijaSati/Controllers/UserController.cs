using EvidencijaSati.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaSati.Controllers
{
    public class UserController : Controller
    {
        public ActionResult UserProfile()
		  {
            if (HttpContext.Session["id"] != null)
				{
                var id = JsonConvert.DeserializeObject<int>(HttpContext.Session["id"].ToString());
                return View(Repo.SelectDjelatnik(id));
				}
				else  return RedirectToAction("Login", "Home");						
		  }
    }
}