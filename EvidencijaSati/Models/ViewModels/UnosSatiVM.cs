using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models.ViewModels
{
	 public class UnosSatiVM
	 {
		  public Djelatnik Djelatnik { get; set; }
		  public List<Projekt> Projekti { get; set; }
		  public Satnica Satnica { get; set; }
	 }
}