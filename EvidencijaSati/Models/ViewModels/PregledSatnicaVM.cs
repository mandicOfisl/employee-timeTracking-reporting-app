using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models.ViewModels
{
	 public class PregledSatnicaVM
	 {
		  public List<Satnica> Satnice { get; set; }
		  public Djelatnik Djelatnik { get; set; }
		  public List<Projekt> Projekti { get; set; }
	 }
}