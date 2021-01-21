using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class Satnica
	 {
		  public int IDSatnica { get; set; }
		  public DateTime Datum { get; set; }
		  public Dictionary<Projekt, float> ProjektSati { get; set; }
		  public string Komentar { get; set; }
	 }
}