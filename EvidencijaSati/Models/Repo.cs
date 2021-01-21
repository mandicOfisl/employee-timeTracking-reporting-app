using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace EvidencijaSati.Models
{
	 public class Repo
	 {
		  public static DataSet Ds { get; set; }
		  private static readonly string cs =
				ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

		  internal static Djelatnik GetDjelatnikByName(string username)
		  {
				throw new NotImplementedException();
		  }
	 }
}