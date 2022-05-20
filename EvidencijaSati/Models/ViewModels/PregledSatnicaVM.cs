using ModelsLibrary;
using System.Collections.Generic;

namespace EvidencijaSati.Models.ViewModels
{
    public class PregledSatnicaVM
	 {
		  public List<Satnica> Satnice { get; set; }
		  public Djelatnik Djelatnik { get; set; }
		  public List<TimClanovi> TimoviClanovi { get; set; }
	 }
}