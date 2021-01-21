using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class Projekt
	 {
		  public int IDProjekt { get; set; }
		  public string Naziv { get; set; }
		  public int KlijentID { get; set; }
		  public DateTime DatumOtvaranja { get; set; }
		  public int VoditeljProjektaID { get; set; }
	 }
}