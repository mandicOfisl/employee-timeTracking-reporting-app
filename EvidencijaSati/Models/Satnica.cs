using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class Satnica
	 {
		  public int IDSatnica { get; set; }
		  public int DjelatnikID { get; set; }
		  public DateTime Datum { get; set; }
		  public string ProjektID { get; set; }
		  public DateTime Start { get; set; }
		  public DateTime End { get; set; }
		  public string Komentar { get; set; }

	 }
}