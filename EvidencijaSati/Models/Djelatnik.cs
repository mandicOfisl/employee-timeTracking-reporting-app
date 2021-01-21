using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class Djelatnik
	 {
		  public int Id { get; set; }
		  public string Ime { get; set; }
		  public string Prezime { get; set; }
		  public string Email { get; set; }
		  public DateTime DatumZaposlenja { get; set; }
		  public string Password { get; set; }
		  public TipDjelatnikaEnum TipDjelatnika { get; set; }
		  public int TimId { get; set; }

		  public override string ToString() => Ime + " " + Prezime;
	 }
}