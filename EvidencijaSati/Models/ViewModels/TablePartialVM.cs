using ModelsLibrary;
using System.Collections.Generic;

namespace EvidencijaSati.Models.ViewModels
{
    public class TablePartialVM
	 {
		  public int SatnicaId { get; set; }
		  public Dictionary<Projekt, List<string>> Projekti { get; set; }
		  public List<string> Totals { get; set; }
		  public string Komentar { get; set; }
	 }
}