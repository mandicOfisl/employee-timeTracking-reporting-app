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
				try
				{
					 Djelatnik djelatnik = Repo.GetDjelatnikByEmail(email);
					 if (djelatnik.Zaporka == password)
					 {
						  UnosSatiVM model = new UnosSatiVM
						  {
								Djelatnik = djelatnik,
								Projekti = Repo.GetProjektiDjelatnika(djelatnik.IDDjelatnik)
						  };
						  return View("UnosSati", model);
					 }
				}
				catch (Exception)
				{
					 return View("Error");
				}
		  }
	 }
}