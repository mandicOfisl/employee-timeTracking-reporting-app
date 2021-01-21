using EvidencijaSati.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaSati.Controllers
{
	 public class HomeController : Controller
	 {
		  public ActionResult Login()
		  {
				return View();
		  }

		  public ActionResult AuthorizeUser(string email, string password)
		  {
				string action, controller;
				try
				{
					 Djelatnik djelatnik = Repo.GetDjelatnikByEmail(email);
					 if (djelatnik.Zaporka == password)
					 {

						  return View("UnosSati", model)
					 }
				}
				catch (Exception)
				{
					 return View("Error");
				}


				return RedirectToAction(action, controller);
		  }
	 }
}