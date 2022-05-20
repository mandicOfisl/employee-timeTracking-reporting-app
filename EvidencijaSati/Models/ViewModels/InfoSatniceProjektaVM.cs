using ModelsLibrary;
using System.Collections.Generic;

namespace EvidencijaSati.Models.ViewModels
{
    public class InfoSatniceProjektaVM
	 {
		  public List<SatnicaProjekta> SatniceProjekta { get; set; }
		  public string Total { get; set; }
		  public string ProjectName { get; set; }
	 }
}