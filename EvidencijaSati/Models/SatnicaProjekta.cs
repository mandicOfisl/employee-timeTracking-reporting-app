using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class SatnicaProjekta
	 {
		  public int IDSatnicaProjekta { get; set; }
		  public int SatnicaID { get; set; }
		  public int ProjektID { get; set; }
		  public DateTime Start { get; set; }
		  public DateTime End { get; set; }
		  public float StartEnd { get; set; }

		  public string GetTimeString (string key)
		  {
				string s = key == "start" ? Start.TimeOfDay.ToString() : End.TimeOfDay.ToString();

				var hourMin = s.Split(':');

				return hourMin[0].ToString().PadLeft(2, '0') + ":" + hourMin[1].ToString().PadLeft(2, '0');
		  }
	 }

	 
}