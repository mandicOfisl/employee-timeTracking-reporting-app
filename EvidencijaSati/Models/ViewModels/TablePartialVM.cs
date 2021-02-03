using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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