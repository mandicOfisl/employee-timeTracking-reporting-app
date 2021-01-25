using EvidencijaSati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaSati.Controllers
{
    public class UserController : Controller
    {
        public ActionResult UserProfile(int id)
		  {
				if (HttpContext.Session["id"] != null) return View(Repo.SelectDjelatnik(id));
				
            return RedirectToAction("Login", "Home");
		  }
    }
}