using ModelsLibrary;
using System.Collections.Generic;

namespace EvidencijaSati.Models.ViewModels
{
    public class UnosSatiVM
	 {
		  public Djelatnik Djelatnik { get; set; }
		  public Satnica Satnica { get; set; }
		  public List<Projekt> Projekti { get; set; }
		  public int AktivanProjektId { get; set; }

	 }
}