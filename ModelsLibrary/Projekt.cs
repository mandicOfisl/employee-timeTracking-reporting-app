using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
	 public class Projekt
	 {
		  public int IDProjekt { get; set; }
		  public string Naziv { get; set; }
		  public int KlijentID { get; set; }
		  public DateTime DatumOtvaranja { get; set; }
		  public int VoditeljProjektaID { get; set; }
		  //public DateTime DatumZatvaranja { get; set; }
		  public bool IsActive { get; set; }
	 }
}
