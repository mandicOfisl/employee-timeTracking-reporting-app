﻿using Microsoft.ApplicationBlocks.Data;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Report.Models
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
										  TimID = int.TryParse(dr[nameof(Djelatnik.TimID)].ToString(), out _) ?
														  (int)dr[nameof(Djelatnik.TimID)] : 0
									 };
								}
						  }
					 }
					 throw new Exception("No can do!");
				}
		  }

		  internal static IEnumerable<Djelatnik> GetDjelatnici()
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
														  (int)row[nameof(Djelatnik.TimID)] : 0
						  };
					 }
				}
		  }
	 }
}