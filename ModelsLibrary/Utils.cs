﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
	 public class Utils
	 {
		  public static string ParseMinutesToString(float zabiljezeno)
		  {
				double num = Math.Floor(zabiljezeno);
				int h = (int)(num / 60);
				int m = (int)(num % 60);
				return h.ToString().PadLeft(2, '0') + ":" + m.ToString().PadLeft(2, '0');
		  }

		  public static float CalculateProjectMinutes(List<SatnicaProjekta> lists)
		  {
				float total = 0;

				foreach (var sp in lists)
				{
					 total += sp.StartEnd;
				}

				return total;
		  }
	 }
}