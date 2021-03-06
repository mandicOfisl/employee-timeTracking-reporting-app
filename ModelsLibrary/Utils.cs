using System;
using System.Collections.Generic;

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

		  public static float ParseStingToMinutes(string minutes)
		  {
				if (minutes == "") return 0;

				string[] s = minutes.Split(':');

				float h = float.Parse(s[0]);
				float m = float.Parse(s[1]);

				return (h * 60) + m;
		  }

		  public static float CalculateProjectMinutes(List<SatnicaProjekta> lists)
		  {
				float total = 0;

				foreach (var sp in lists)
				{
					 total += sp.TotalMin + sp.Prekovremeni;
				}

				return total;
		  }

		  public static string AddStringHoursMinutes(string first, string second)
		  {
				int fH = int.Parse(first.Split(':')[0]);
				int fM = int.Parse(first.Split(':')[1]);
				int sH = int.Parse(second.Split(':')[0]);
				int sM = int.Parse(second.Split(':')[1]);

				int h = fH + sH;
				int m = fM + sM;

				if (m > 59)
				{
					 h += 1;
					 m -= 60;
				}

				return h.ToString().PadLeft(2, '0') + ":" + m.ToString().PadLeft(2, '0');
		  }
	 }
}
