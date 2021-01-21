using EvidencijaSati.Models;
using EvidencijaSati.Models.ViewModels;
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
				return View(new Djelatnik());
		  }

		  [HttpPost]
		  public ActionResult AuthorizeUser(Djelatnik d)
		  {
				try
				{
					 Djelatnik djelatnik = Repo.GetDjelatnikByEmail(d.Email);
					 if (djelatnik.Zaporka == d.Zaporka)
					 {						  
						  return RedirectToAction("UnosSati", "Satnica", new { id = djelatnik.IDDjelatnik });
					 }
					 else
					 {
						  return View("Error", new ErrorVM { Msg = "Please check your email and password." });
					 }
				}
				catch (Exception)
				{
					 return View("Error", new ErrorVM { Msg = "There has been an error. Please try again later." });
				}
		  }
	 }
}