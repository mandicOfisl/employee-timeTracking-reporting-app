﻿using EvidencijaSati.Models;
using EvidencijaSati.Models.ViewModels;
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

        public ActionResult PromijeniZaporku(int id) => PartialView("PromijeniZaporku", Repo.SelectDjelatnik(id));
		  
		  [HttpPost]
        public ActionResult UpdateZaporka(Djelatnik d)
		  {
            int i = Repo.UpdateZaporka(d.IDDjelatnik, d.Zaporka);

				if (i > 0)
				{
					 ViewBag.Id = d.IDDjelatnik;
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