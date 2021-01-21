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

		  public ActionResult AuthorizeUser(string username, string password)
		  {
				string action, controller;
				try
				{
					 Djelatnik djelatnik = Repo.GetDjelatnikByName(username);
					 if (djelatnik.Password == password)
					 {
						  switch (djelatnik.TipDjelatnika)
						  {
								case TipDjelatnikaEnum.DIREKTOR:
									 break;
								case TipDjelatnikaEnum.VODITELJ_TIMA:
									 break;
								case TipDjelatnikaEnum.ZAPOSLENIK:
									 break;
								case TipDjelatnikaEnum.HONORARNI_DJELATNIK:
									 break;
								case TipDjelatnikaEnum.STUDENT:
									 break;
								default:
									 break;
						  }
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