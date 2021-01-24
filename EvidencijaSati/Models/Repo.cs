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
										  TimID =  int.TryParse(dr[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)dr[nameof(Djelatnik.TimID)] : 0
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  internal static IEnumerable<SatnicaProjekta> GetSatniceProjekata(int iDSatnica)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure,
					 "GetSatniceProjektaSatnice", new SqlParameter("@IdSatnica", iDSatnica)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new SatnicaProjekta
						  {
								IDSatnicaProjekta = (int)row[nameof(SatnicaProjekta.IDSatnicaProjekta)],
								ProjektID = (int)row[nameof(SatnicaProjekta.ProjektID)],
								SatnicaID = (int)row[nameof(SatnicaProjekta.SatnicaID)],
								Start = DateTime.Parse(row[nameof(SatnicaProjekta.Start)].ToString()),
								End = DateTime.Parse(row[nameof(SatnicaProjekta.End)].ToString()),
								StartEnd = float.Parse(row[nameof(SatnicaProjekta.StartEnd)].ToString())
						  };
					 }

				}
				
		  }

		  internal static IEnumerable<Satnica> GetSatniceDjelatnika(int id)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure,
					 "GetSatniceDjelatnika", new SqlParameter("@Id", id)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Satnica
						  {
								IDSatnica = (int)row[nameof(Satnica.IDSatnica)],
								DjelatnikID = (int)row[nameof(Satnica.DjelatnikID)],
								Datum = DateTime.Parse(row[nameof(Satnica.Datum)].ToString()),
								Komentar = row[nameof(Satnica.Komentar)].ToString(),
								Satnice = new Dictionary<int, List<SatnicaProjekta>>(),
								ProjektZabiljezeno = new Dictionary<int, string>(),
								Total = double.Parse(row[nameof(Satnica.Total)].ToString()),
								TotalPrekovremeni = double.Parse(row[nameof(Satnica.TotalPrekovremeni)].ToString()),
								TotalRedovni = double.Parse(row[nameof(Satnica.TotalRedovni)].ToString())
						  };
					 }

				}
		  }

		  internal static int DodajNovuSatnicu(Satnica satnica)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddSatnica";
						  cmd.Parameters.AddWithValue("@DjelatnikId", satnica.DjelatnikID);
						  cmd.Parameters.AddWithValue("@Datum", satnica.Datum);
						  cmd.Parameters.AddWithValue("@Komentar", satnica.Komentar ?? "nema komentara");
						  cmd.Parameters.AddWithValue("@TotalRedovni", satnica.TotalRedovni);
						  cmd.Parameters.AddWithValue("@TotalPrekovremeni", satnica.TotalPrekovremeni);
						  cmd.Parameters.AddWithValue("@Total", satnica.Total);
						  cmd.Parameters.AddWithValue("@Status", (int)satnica.Staus);
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
				}
		  }

		  internal static Projekt SelectProjekt(int iDProjekt)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "SelectProjekt";
						  cmd.Parameters.AddWithValue("@Id", iDProjekt);
						  using (SqlDataReader dr = cmd.ExecuteReader())
						  {
								if (dr.Read())
								{
									 return new Projekt
									 {
										  IDProjekt = (int)dr[nameof(Projekt.IDProjekt)],
										  Naziv = dr[nameof(Projekt.Naziv)].ToString(),
										  KlijentID = (int)dr[nameof(Projekt.KlijentID)],
										  DatumOtvaranja = DateTime.Parse(dr[nameof(Projekt.DatumOtvaranja)].ToString()),
										  VoditeljProjektaID = (int)dr[nameof(Projekt.VoditeljProjektaID)]
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  internal static int ChangeSatnicaStatus(int satId, SatnicaStatusEnum status)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddSatnica";
						  cmd.Parameters.AddWithValue("@IdSatnica", satId);
						  cmd.Parameters.AddWithValue("@Status", (int)status);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  internal static int DeleteUnosSatniceProjekta(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DeleteSatnicaProjekta";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  internal static int SpremiSatnicuProjekta(SatnicaProjekta satnicaProjekta)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddSatnicaProjekta";
						  cmd.Parameters.AddWithValue("@SatnicaID", satnicaProjekta.SatnicaID);
						  cmd.Parameters.AddWithValue("@ProjektID", satnicaProjekta.ProjektID);
						  cmd.Parameters.AddWithValue("@Start", satnicaProjekta.Start);
						  cmd.Parameters.AddWithValue("@End", satnicaProjekta.End);
						  cmd.Parameters.AddWithValue("@StartEnd", satnicaProjekta.StartEnd);
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
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
										  TimID = int.TryParse(dr[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)dr[nameof(Djelatnik.TimID)] : 0
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