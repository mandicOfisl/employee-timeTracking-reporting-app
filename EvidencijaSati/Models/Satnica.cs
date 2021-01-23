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
		  public Dictionary<string, List<SatnicaProjekta>> Satnice { get; set; }
		  public string Komentar { get; set; }
		  public double Total { get; set; }
		  public double TotalRedovni { get; set; }
		  public double TotalPrekovremeni { get; set; }
		  public SatnicaStatusEnum Staus { get; set; }

	 }
}