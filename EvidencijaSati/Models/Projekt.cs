using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class Projekt
	 {
		  public int Id { get; set; }
		  public string Naziv { get; set; }
		  public int KlijentId { get; set; }
		  public DateTime DatumOtvaranja { get; set; }
		  public int VoditeljProjektaId { get; set; }
	 }
}