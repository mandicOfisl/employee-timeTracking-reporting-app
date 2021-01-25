using EvidencijaSati.Models;
using EvidencijaSati.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaSati.Controllers
{
	 public class HomeController : Controller
	 {
		  public ActionResult Login()
		  {
				if (HttpContext.Session["id"] != null) HttpContext.Session.Remove("id");
				ViewBag.Site = "Login";
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
						  ViewBag.Id = djelatnik.IDDjelatnik;
						  ViewBag.TipDjelatnika = djelatnik.TipDjelatnikaID;
						  HttpContext.Session.Add("id", JsonConvert.SerializeObject(djelatnik.IDDjelatnik));
						  HttpContext.Session.Add("tipDjelatnika", JsonConvert.SerializeObject((int)djelatnik.TipDjelatnikaID));
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


		  public ActionResult SetLanguage(string culture, string url)
		  {
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

				Response.Cookies.Add(new HttpCookie("Language")
				{
					 Value = culture,
					 Expires = DateTime.Now.AddYears(1)
				});
				
				return Redirect(url);								
		  }

		  public ActionResult Logout()
		  {
				if (HttpContext.Session["id"] != null) HttpContext.Session.Remove("id");
				ViewBag.Site = "Login";
				return View("Login", new Djelatnik());
		  }
	 }
}