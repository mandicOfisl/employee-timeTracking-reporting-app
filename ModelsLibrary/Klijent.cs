using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
	 public class Klijent
	 {
		  public int IDKlijent { get; set; }
		  public string Naziv { get; set; }
		  public string Telefon { get; set; }
		  public string Email { get; set; }
		  public bool IsActive { get; set; }
	 }
}
