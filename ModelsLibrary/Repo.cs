﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
	 public class Repo
	 {
		  public static DataSet Ds { get; set; }
		  private static readonly string cs =
				ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

		  public static Djelatnik GetDjelatnikByEmail(string email)
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
										  TimID = int.TryParse(dr[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)dr[nameof(Djelatnik.TimID)] : 0,
										  IsActive = (int)dr[nameof(Djelatnik.IsActive)] == 1
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  public static IEnumerable<Projekt> GetProjektiKlijenta(int iDKlijent)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure,
					 "GetProjektiKlijenta", new SqlParameter("@IdKlijent", iDKlijent)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  DateTime dz;

						  yield return new Projekt
						  {
								IDProjekt = (int)row[nameof(Projekt.IDProjekt)],
								Naziv = row[nameof(Projekt.Naziv)].ToString(),
								KlijentID = (int)row[nameof(Projekt.KlijentID)],
								DatumOtvaranja = DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								VoditeljProjektaID = (int)row[nameof(Projekt.VoditeljProjektaID)],
								DatumZatvaranja = DateTime.TryParse(row[nameof(Projekt.DatumZatvaranja)].ToString(), out dz) ?
									 dz : DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								IsActive = (int)row[nameof(Projekt.IsActive)] == 1
						  };
					 }

				}
		  }

		  public static int UpdateKlijent(Klijent k)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateKlijent";
						  cmd.Parameters.AddWithValue("@Id", k.IDKlijent);
						  cmd.Parameters.AddWithValue("@Naziv", k.Naziv);
						  cmd.Parameters.AddWithValue("@Telefon", k.Telefon);
						  cmd.Parameters.AddWithValue("@Email", k.Email);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int DodajKlijenta(Klijent k)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddKlijent";
						  cmd.Parameters.AddWithValue("@Naziv", k.Naziv);
						  cmd.Parameters.AddWithValue("@Telefon", k.Telefon);
						  cmd.Parameters.AddWithValue("@Email", k.Email);
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
				}
		  }

		  public static int AktivirajKlijenta(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AktivirajKlijenta";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int DeaktivirajKlijenta(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DeaktivirajKlijenta";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static Klijent SelectKlijent(int klijentId)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "SelectKlijent";
						  cmd.Parameters.AddWithValue("@Id", klijentId);
						  using (SqlDataReader dr = cmd.ExecuteReader())
						  {
								if (dr.Read())
								{
									 return new Klijent
									 {
										  IDKlijent = (int)dr[nameof(Klijent.IDKlijent)],
										  Naziv = dr[nameof(Klijent.Naziv)].ToString(),
										  Email = dr[nameof(Klijent.Email)].ToString(),
										  Telefon = dr[nameof(Klijent.Telefon)].ToString(),
										  IsActive = (int)dr[nameof(Klijent.IsActive)] == 1
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  public static IEnumerable<Djelatnik> GetVoditeljiProjekta()
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetVoditeljiProjekata"))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Djelatnik
						  {
								IDDjelatnik = (int)row[nameof(Djelatnik.IDDjelatnik)],
								Ime = row[nameof(Djelatnik.Ime)].ToString(),
								Prezime = row[nameof(Djelatnik.Prezime)].ToString(),
								Email = row[nameof(Djelatnik.Email)].ToString(),
								DatumZaposlenja = DateTime.Parse(row[nameof(Djelatnik.DatumZaposlenja)].ToString()),
								Zaporka = row[nameof(Djelatnik.Zaporka)].ToString(),
								TipDjelatnikaID = (TipDjelatnikaEnum)(int)row[nameof(Djelatnik.TipDjelatnikaID)],
								TimID = int.TryParse(row[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)row[nameof(Djelatnik.TimID)] : 0,
								IsActive = (int)row[nameof(Djelatnik.IsActive)] == 1
						  };
					 }
				}
		  }

		  public static IEnumerable<Djelatnik> GetZaposleniNaProjektu(int iDProjekt)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetZaposleniNaProjektu", new SqlParameter("@IdProjekt", iDProjekt)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Djelatnik
						  {
								IDDjelatnik = (int)row[nameof(Djelatnik.IDDjelatnik)],
								Ime = row[nameof(Djelatnik.Ime)].ToString(),
								Prezime = row[nameof(Djelatnik.Prezime)].ToString(),
								Email = row[nameof(Djelatnik.Email)].ToString(),
								DatumZaposlenja = DateTime.Parse(row[nameof(Djelatnik.DatumZaposlenja)].ToString()),
								Zaporka = row[nameof(Djelatnik.Zaporka)].ToString(),
								TipDjelatnikaID = (TipDjelatnikaEnum)(int)row[nameof(Djelatnik.TipDjelatnikaID)],
								TimID = int.TryParse(row[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)row[nameof(Djelatnik.TimID)] : 0,
								IsActive = (int)row[nameof(Djelatnik.IsActive)] == 1
						  };
					 }
				}
		  }

		  public static IEnumerable<Klijent> GetKlijenti()
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetKlijenti"))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Klijent
						  {
								IDKlijent = (int)row[nameof(Klijent.IDKlijent)],
								Naziv = row[nameof(Klijent.Naziv)].ToString(),
								Email = row[nameof(Klijent.Email)].ToString(),
								Telefon = row[nameof(Klijent.Telefon)].ToString(),
								IsActive = (int)row[nameof(Klijent.IsActive)] == 1
						  };
					 }
				}
		  }

		  public static IEnumerable<Projekt> GetProjekti()
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetProjekti"))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {						  
						  DateTime dz;
						  
						  yield return new Projekt
						  {
								IDProjekt = (int)row[nameof(Projekt.IDProjekt)],
								Naziv = row[nameof(Projekt.Naziv)].ToString(),
								KlijentID = (int)row[nameof(Projekt.KlijentID)],
								DatumOtvaranja = DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								VoditeljProjektaID = (int)row[nameof(Projekt.VoditeljProjektaID)],
								DatumZatvaranja = DateTime.TryParse(row[nameof(Projekt.DatumZatvaranja)].ToString(), out dz) ?
									 dz : DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								IsActive = (int)row[nameof(Projekt.IsActive)] == 1
						  };
					 }

				}
		  }

		  public static int UpdateProjekt(Projekt p)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateProjekt";
						  cmd.Parameters.AddWithValue("@Id", p.IDProjekt);
						  cmd.Parameters.AddWithValue("@Naziv", p.Naziv);
						  cmd.Parameters.AddWithValue("@KlijentId", p.KlijentID);
						  cmd.Parameters.AddWithValue("@DatumOtvaranja", p.DatumOtvaranja);
						  cmd.Parameters.AddWithValue("@VoditeljProjektaId", p.VoditeljProjektaID);
						  if (p.DatumOtvaranja == p.DatumZatvaranja)
						  {
								cmd.Parameters.AddWithValue("@DatumZatvaranja", DBNull.Value);
						  }
						  else
						  {
								cmd.Parameters.AddWithValue("@DatumZatvaranja", p.DatumZatvaranja);
						  }

						  return cmd.ExecuteNonQuery();						  
					 }
				}
		  }

		  public static int DodajProjekt(Projekt p)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddProjekt";
						  cmd.Parameters.AddWithValue("@Naziv", p.Naziv);
						  cmd.Parameters.AddWithValue("@KlijentId", p.KlijentID);
						  cmd.Parameters.AddWithValue("@DatumOtvaranja", p.DatumOtvaranja);
						  cmd.Parameters.AddWithValue("@voditeljProjektaId", p.VoditeljProjektaID);
						  if (p.DatumOtvaranja == p.DatumZatvaranja)
						  {
								cmd.Parameters.AddWithValue("@DatumZatvaranja", null);
						  }
						  else
						  {
								cmd.Parameters.AddWithValue("@DatumZatvaranja", p.DatumZatvaranja);
						  }
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
				}
		  }

		  public static int DodajDjelatnikaNaProjekt(int projektId, int djelatnikId)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddProjektDjelatnik";
						  cmd.Parameters.AddWithValue("@IdProjekt", projektId);
						  cmd.Parameters.AddWithValue("@IdDjelatnik", djelatnikId);
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int UkloniDjelatnikaSProjekta(int projId, int djelatnikId)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DeleteProjektDjelatnik";
						  cmd.Parameters.AddWithValue("@IdProjekt", projId);
						  cmd.Parameters.AddWithValue("@IdDjelatnik", djelatnikId);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int AktivirajProjekt(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AktivirajProjekt";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int DeaktivirajProjekt(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DeaktivirajProjekt";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int DeaktivirajTim(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DeaktivirajTim";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int UpdateTim(Tim tim)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateTim";
						  cmd.Parameters.AddWithValue("@Id", tim.IDTim);
						  cmd.Parameters.AddWithValue("@Naziv", tim.Naziv);
						  cmd.Parameters.AddWithValue("@DatumKreiranja", tim.DatumKreiranja);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int AddTim(Tim tim)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AddTim";
						  cmd.Parameters.AddWithValue("@Naziv", tim.Naziv);
						  cmd.Parameters.AddWithValue("@DatumKreiranja", tim.DatumKreiranja);
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
				}
		  }

		  public static IEnumerable<Djelatnik> GetClanoviTima(int iDTim)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetClanoviTima", new SqlParameter("@IdTim", iDTim)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Djelatnik
						  {
								IDDjelatnik = (int)row[nameof(Djelatnik.IDDjelatnik)],
								Ime = row[nameof(Djelatnik.Ime)].ToString(),
								Prezime = row[nameof(Djelatnik.Prezime)].ToString(),
								Email = row[nameof(Djelatnik.Email)].ToString(),
								DatumZaposlenja = DateTime.Parse(row[nameof(Djelatnik.DatumZaposlenja)].ToString()),
								Zaporka = row[nameof(Djelatnik.Zaporka)].ToString(),
								TipDjelatnikaID = (TipDjelatnikaEnum)(int)row[nameof(Djelatnik.TipDjelatnikaID)],
								TimID = int.TryParse(row[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)row[nameof(Djelatnik.TimID)] : 0,
								IsActive = (int)row[nameof(Djelatnik.IsActive)] == 1
						  };
					 }
				}
		  }

		  public static int AktivirajTim(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AktivirajTim";
						  cmd.Parameters.AddWithValue("@Id", id);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static Tim SelectTim(int id)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "SelectTim";
						  cmd.Parameters.AddWithValue("@Id", id);
						  using (SqlDataReader dr = cmd.ExecuteReader())
						  {
								if (dr.Read())
								{
									 return new Tim
									 {
										  IDTim = (int)dr[nameof(Tim.IDTim)],
										  Naziv = dr[nameof(Tim.Naziv)].ToString(),
										  DatumKreiranja = DateTime.Parse(dr[nameof(Tim.DatumKreiranja)].ToString()),
										  IsActive = (int)dr[nameof(Tim.IsActive)] == 1
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  public static IEnumerable<Tim> GetTimovi()
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetTimovi"))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Tim
						  {
								IDTim = (int)row[nameof(Tim.IDTim)],
								Naziv = row[nameof(Tim.Naziv)].ToString(),
								DatumKreiranja	= DateTime.Parse(row[nameof(Tim.DatumKreiranja)].ToString()),
								IsActive = (int)row[nameof(Tim.IsActive)] == 1
						  };
					 }
				}

		  }

		  public static IEnumerable<Djelatnik> GetDjelatnici()
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "GetDjelatnici"))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Djelatnik
						  {
								IDDjelatnik = (int)row[nameof(Djelatnik.IDDjelatnik)],
								Ime = row[nameof(Djelatnik.Ime)].ToString(),
								Prezime = row[nameof(Djelatnik.Prezime)].ToString(),
								Email = row[nameof(Djelatnik.Email)].ToString(),
								DatumZaposlenja = DateTime.Parse(row[nameof(Djelatnik.DatumZaposlenja)].ToString()),
								Zaporka = row[nameof(Djelatnik.Zaporka)].ToString(),
								TipDjelatnikaID = (TipDjelatnikaEnum)(int)row[nameof(Djelatnik.TipDjelatnikaID)],
								TimID = int.TryParse(row[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)row[nameof(Djelatnik.TimID)] : 0,
								IsActive = (int)row[nameof(Djelatnik.IsActive)] == 1
						  };
					 }
				}
		  }

		  public static int UpdateZaporka(int iDDjelatnik, string zaporka)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateZaporka";
						  cmd.Parameters.AddWithValue("@Id", iDDjelatnik);
						  cmd.Parameters.AddWithValue("@Pass", zaporka);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static IEnumerable<SatnicaProjekta> GetSatniceProjekata(int iDSatnica)
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
								End = row[nameof(SatnicaProjekta.End)].GetType().Equals(typeof(DBNull)) ?
										  new DateTime(0) : DateTime.Parse(row[nameof(SatnicaProjekta.End)].ToString()),
								StartEnd = float.Parse(row[nameof(SatnicaProjekta.StartEnd)].ToString()),
								Komentar = row[nameof(SatnicaProjekta.Komentar)].ToString()
						  };
					 }

				}

		  }

		  public static int DodajDjelatnika(Djelatnik d)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DodajDjelatnika";
						  cmd.Parameters.AddWithValue("@Id", d.IDDjelatnik);
						  cmd.Parameters.AddWithValue("@Ime", d.Ime);
						  cmd.Parameters.AddWithValue("@Prezime", d.Prezime);
						  cmd.Parameters.AddWithValue("@Email", d.Email);
						  cmd.Parameters.AddWithValue("@Zaporka", d.Zaporka);
						  cmd.Parameters.AddWithValue("@DatumZaposlenja", d.DatumZaposlenja);
						  cmd.Parameters.AddWithValue("@TipDjelatnika", (int)d.TipDjelatnikaID);
						  cmd.Parameters.AddWithValue("@TimId", d.TimID);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int DeaktivirajDjelatnika(int iDDjelatnik)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "DeaktivirajDjelatnika";
						  cmd.Parameters.AddWithValue("@Id", iDDjelatnik);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int AktivirarDjelatnika(int iDDjelatnik)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "AktivirajDjelatnika";
						  cmd.Parameters.AddWithValue("@Id", iDDjelatnik);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int UpdateDjelatnik(Djelatnik d)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateDjelatnik";
						  cmd.Parameters.AddWithValue("@Id", d.IDDjelatnik);
						  cmd.Parameters.AddWithValue("@Ime", d.Ime);
						  cmd.Parameters.AddWithValue("@Prezime", d.Prezime);
						  cmd.Parameters.AddWithValue("@Email", d.Email);
						  cmd.Parameters.AddWithValue("@Zaporka", d.Zaporka);
						  cmd.Parameters.AddWithValue("@DatumZaposlenja", d.DatumZaposlenja);
						  cmd.Parameters.AddWithValue("@TipDjelatnika", (int)d.TipDjelatnikaID);
						  cmd.Parameters.AddWithValue("@TimID", d.TimID);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static IEnumerable<Satnica> GetSatniceDjelatnika(int id)
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
								Satnice = new Dictionary<int, List<SatnicaProjekta>>(),
								ProjektZabiljezeno = new Dictionary<int, string>(),
								Total = double.Parse(row[nameof(Satnica.Total)].ToString()),
								TotalPrekovremeni = double.Parse(row[nameof(Satnica.TotalPrekovremeni)].ToString()),
								TotalRedovni = double.Parse(row[nameof(Satnica.TotalRedovni)].ToString())
						  };
					 }

				}
		  }

		  public static int DodajNovuSatnicu(Satnica satnica)
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
						  cmd.Parameters.AddWithValue("@TotalRedovni", satnica.TotalRedovni);
						  cmd.Parameters.AddWithValue("@TotalPrekovremeni", satnica.TotalPrekovremeni);
						  cmd.Parameters.AddWithValue("@Total", satnica.Total);
						  cmd.Parameters.AddWithValue("@Status", (int)satnica.Staus);
						  cmd.Parameters.AddWithValue("@Komentar", satnica.Komentar ?? "");
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
				}
		  }

		  public static Projekt SelectProjekt(int iDProjekt)
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
									 DateTime dz;

									 return new Projekt
									 {
										  IDProjekt = (int)dr[nameof(Projekt.IDProjekt)],
										  Naziv = dr[nameof(Projekt.Naziv)].ToString(),
										  KlijentID = (int)dr[nameof(Projekt.KlijentID)],
										  DatumOtvaranja = DateTime.Parse(dr[nameof(Projekt.DatumOtvaranja)].ToString()),
										  VoditeljProjektaID = (int)dr[nameof(Projekt.VoditeljProjektaID)],
										  DatumZatvaranja = DateTime.TryParse(dr[nameof(Projekt.DatumZatvaranja)].ToString(), out dz) ?
												dz : DateTime.Parse(dr[nameof(Projekt.DatumOtvaranja)].ToString()),
										  IsActive = (int)dr[nameof(Projekt.IsActive)] == 1
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  public static int SpremiSatnicu(Satnica satnica)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateSatnica";
						  cmd.Parameters.AddWithValue("@DjelatnikId", satnica.DjelatnikID);
						  cmd.Parameters.AddWithValue("@TotalRedovni", satnica.TotalRedovni);
						  cmd.Parameters.AddWithValue("@TotalPrekovremeni", satnica.TotalPrekovremeni);
						  cmd.Parameters.AddWithValue("@Total", satnica.Total);
						  cmd.Parameters.AddWithValue("@Komentar", satnica.Komentar ?? "");

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int ChangeSatnicaStatus(int satId, SatnicaStatusEnum status)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "ChangeSatnicaStatus";
						  cmd.Parameters.AddWithValue("@IdSatnica", satId);
						  cmd.Parameters.AddWithValue("@Status", (int)status);

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static SatnicaProjekta SelectRadnaSatnicaProjekta(int projektID)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "SelectRadnaSatnicaProjekta";
						  cmd.Parameters.AddWithValue("@IdProjekt", projektID);
						  using (SqlDataReader dr = cmd.ExecuteReader())
						  {
								if (dr.Read())
								{
									 return new SatnicaProjekta
									 {
										  IDSatnicaProjekta = (int)dr[nameof(SatnicaProjekta.IDSatnicaProjekta)],
										  ProjektID = (int)dr[nameof(SatnicaProjekta.ProjektID)],
										  SatnicaID = (int)dr[nameof(SatnicaProjekta.SatnicaID)],
										  Start = DateTime.Parse(dr[nameof(SatnicaProjekta.Start)].ToString()),
										  End = dr[nameof(SatnicaProjekta.End)].GetType().Equals(typeof(DBNull)) ?
										  new DateTime(0) : DateTime.Parse(dr[nameof(SatnicaProjekta.End)].ToString()),
										  StartEnd = float.Parse(dr[nameof(SatnicaProjekta.StartEnd)].ToString()),
										  Komentar = dr[nameof(SatnicaProjekta.Komentar)].ToString()
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  public static IEnumerable<Satnica> GetSatniceProjektaZaVoditeljaDirektora(int idVoditeljDirektor, int tipDjelatnika, int status)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure,
					 "GetSatniceProjektaZaVoditelja",
						  new SqlParameter[]{
								new SqlParameter("@IdVoditelj", idVoditeljDirektor),
								new SqlParameter("@IdTip", tipDjelatnika),
								new SqlParameter("@IdStatus", status)
						  }))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  yield return new Satnica
						  {
								IDSatnica = (int)row[nameof(Satnica.IDSatnica)],
								DjelatnikID = (int)row[nameof(Satnica.DjelatnikID)],
								Datum = DateTime.Parse(row[nameof(Satnica.Datum)].ToString()),
								Satnice = new Dictionary<int, List<SatnicaProjekta>>(),
								ProjektZabiljezeno = new Dictionary<int, string>(),
								Total = double.Parse(row[nameof(Satnica.Total)].ToString()),
								TotalPrekovremeni = double.Parse(row[nameof(Satnica.TotalPrekovremeni)].ToString()),
								TotalRedovni = double.Parse(row[nameof(Satnica.TotalRedovni)].ToString()),
								Komentar = row[nameof(Satnica.Komentar)].ToString()
						  };
					 }

				}
		  }

		  public static int UpdateEndSatniceProjekta(DateTime end, int iDSatnicaProjekta, float startEnd, string komentar)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateEndSatniceProjekta";
						  cmd.Parameters.AddWithValue("@Id", iDSatnicaProjekta);
						  cmd.Parameters.AddWithValue("@End", end);
						  cmd.Parameters.AddWithValue("@StartEnd", startEnd);
						  cmd.Parameters.AddWithValue("@Komentar", komentar ?? "");

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int UpdateSatnicaProjekta(SatnicaProjekta satnica)
		  {
				using (SqlConnection con = new SqlConnection(cs))
				{
					 con.Open();
					 using (SqlCommand cmd = con.CreateCommand())
					 {
						  cmd.CommandType = CommandType.StoredProcedure;
						  cmd.CommandText = "UpdateSatnicaProjekta";
						  cmd.Parameters.AddWithValue("@Id", satnica.IDSatnicaProjekta);
						  cmd.Parameters.AddWithValue("@Start", satnica.Start);
						  cmd.Parameters.AddWithValue("@End", satnica.End);
						  cmd.Parameters.AddWithValue("@StartEnd", satnica.StartEnd);
						  cmd.Parameters.AddWithValue("@Komentar", satnica.Komentar ?? "");

						  return cmd.ExecuteNonQuery();
					 }
				}
		  }

		  public static int DeleteUnosSatniceProjekta(int id)
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

		  public static int SpremiSatnicuProjekta(SatnicaProjekta satnicaProjekta)
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
						  cmd.Parameters.AddWithValue("@StartEnd", satnicaProjekta.StartEnd);
						  cmd.Parameters.AddWithValue("@Komentar", satnicaProjekta.Komentar ?? "");
						  cmd.Parameters.Add("@Id", SqlDbType.Int);
						  cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

						  _ = cmd.ExecuteNonQuery();
						  return int.Parse(cmd.Parameters["@Id"].Value.ToString());
					 }
				}
		  }

		  public static Djelatnik SelectDjelatnik(int id)
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
														  (int)dr[nameof(Djelatnik.TimID)] : 0,
										  IsActive = (int)dr[nameof(Djelatnik.IsActive)] == 1
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  public static IEnumerable<Projekt> GetProjektiDjelatnika(int idDjelatnik)
		  {
				using (Ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure,
					 "GetProjektiDjelatnika", new SqlParameter("@Id", idDjelatnik)))
				{
					 foreach (DataRow row in Ds.Tables[0].Rows)
					 {
						  DateTime dz;

						  yield return new Projekt
						  {
								IDProjekt = (int)row[nameof(Projekt.IDProjekt)],
								Naziv = row[nameof(Projekt.Naziv)].ToString(),
								KlijentID = (int)row[nameof(Projekt.KlijentID)],
								DatumOtvaranja = DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								VoditeljProjektaID = (int)row[nameof(Projekt.VoditeljProjektaID)],
								DatumZatvaranja = DateTime.TryParse(row[nameof(Projekt.DatumZatvaranja)].ToString(), out dz) ?
									 dz : DateTime.Parse(row[nameof(Projekt.DatumOtvaranja)].ToString()),
								IsActive = (int)row[nameof(Projekt.IsActive)] == 1
						  };
					 }

				}
		  }

	 }
}
