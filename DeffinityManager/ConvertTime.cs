using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConvertTime
/// </summary>
public class ConvertTime
{
	public ConvertTime()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int ConvertToMinuts(string hours)
    {
        int mins = 0;
        char[] comm = { ':' };
        string[] getval = hours.Split(comm);
        mins = Convert.ToInt32(getval[0]) * 60;
        mins=mins +Convert.ToInt32(getval[1]);

        return mins;
    }
    public static string ConvertToHours(int mins)
    {
        int hours = mins / 60;
        int minuts = mins % 60;
        string s = hours.ToString("00") + ":" + minuts.ToString("00");
        //return s.ToString("00:00");
        return s;
    }
}
