using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class Djelatnik
    {		  
		  public int IDDjelatnik { get; set; }
		  public string Ime { get; set; }
		  public string Prezime { get; set; }
		  public string Email { get; set; }
		  public DateTime DatumZaposlenja { get; set; }
		  public string Zaporka { get; set; }
		  public TipDjelatnikaEnum TipDjelatnikaID { get; set; }
		  public int TimID { get; set; }
		  public string FullName { get { return Ime + " " + Prezime; } }

		  public override string ToString() => Ime + " " + Prezime;
	 }
}
