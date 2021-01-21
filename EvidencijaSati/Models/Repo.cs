using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaSati.Models
{
	 public class Repo
	 {
		  public static DataSet Ds { get; set; }
		  private static readonly string cs =
				ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

		  internal static Djelatnik GetDjelatnikByEmail(string email)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "GetDjelatnikByEmail";
						  cmd.Parameters.AddWithValue("@Email", email);
						  using (SqlDataReader dr = cmd.ExecuteReader())
						  {
								if (dr.Read())
								{
									 return new Djelatnik
									 { 
										  IDDjelatnik = (int)dr[nameof(Djelatnik.IDDjelatnik)],
										  Ime = dr[nameof(Djelatnik.Ime)].ToString(),
										  Prezime = dr[nameof(Djelatnik.Prezime)].ToString(),
										  Email = dr[nameof(Djelatnik.Email)].ToString(),
										  DatumZaposlenja = DateTime.Parse(dr[nameof(Djelatnik.DatumZaposlenja)].ToString()),
										  Zaporka = dr[nameof(Djelatnik.Zaporka)].ToString(),
										  TipDjelatnikaID = (TipDjelatnikaEnum)(int)dr[nameof(Djelatnik.TipDjelatnikaID)],
										  TimID = (int)dr[nameof(Djelatnik.TimID)]
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  internal static Djelatnik SelectDjelatnik(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "SelectDjelatnik";
						  cmd.Parameters.AddWithValue("@Id", id);
						  using (SqlDataReader dr = cmd.ExecuteReader())
						  {
								if (dr.Read())
								{
									 return new Djelatnik
									 {
										  IDDjelatnik = (int)dr[nameof(Djelatnik.IDDjelatnik)],
										  Ime = dr[nameof(Djelatnik.Ime)].ToString(),
										  Prezime = dr[nameof(Djelatnik.Prezime)].ToString(),
										  Email = dr[nameof(Djelatnik.Email)].ToString(),
										  DatumZaposlenja = DateTime.Parse(dr[nameof(Djelatnik.DatumZaposlenja)].ToString()),
										  Zaporka = dr[nameof(Djelatnik.Zaporka)].ToString(),
										  TipDjelatnikaID = (TipDjelatnikaEnum)(int)dr[nameof(Djelatnik.TipDjelatnikaID)],
										  TimID = (int)dr[nameof(Djelatnik.TimID)]
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  internal static IEnumerable<Projekt> GetProjektiDjelatnika(int idDjelatnik)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure,
					 "GetProjektiDjelatnika", new SqlParameter("@Id", idDjelatnik)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Projekt
						  {
								IDProjekt = (int)row[nameof(Projekt.IDProjekt)],
								Naziv = row[nameof(Projekt.Naziv)].ToString(),
								KlijentID = (int)row[nameof(Projekt.KlijentID)],
								DatumOtvaranja = DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								VoditeljProjektaID = (int)row[nameof(Projekt.VoditeljProjektaID)]
						  };
					 }
				}
		  }
	 }
}