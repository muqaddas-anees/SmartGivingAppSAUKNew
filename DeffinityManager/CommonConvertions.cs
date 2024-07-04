using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for CommonConvertions
/// </summary>
public class CommonConvertions
{
	public CommonConvertions()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static int GetInt(string InVal)
    {
        int StringInt = 0;
        try 
        {
            if (!string.IsNullOrEmpty(InVal))
            {
                StringInt = int.Parse(InVal);
            }
        
        }
        catch (Exception ex)
        { }
        return StringInt;
    }
    //public static double GetDouble(string InVal)
    //{
    //    int StringDouble = 0;
    //    try
    //    {
    //        if (!string.IsNullOrEmpty(InVal))
    //        {
    //            StringDouble = double.Parse(InVal);
    //        }

    //    }
    //    catch (Exception ex)
    //    { }
    //    return StringDouble;
    //}
    public static string GetString(string InVal)
    {
        return InVal;
    }
    public static string GetStringFirstInt(string Inval)
    {
        string stringFirst = "";
        if (!string.IsNullOrEmpty(Inval))
            stringFirst = Inval.Substring(0, Inval.Length - 1);
        else
            stringFirst = "0";
        return stringFirst;
    }
    public static string GetStringLastChar(string Inval)
    {
        string stringLast ="";
        if(!string.IsNullOrEmpty(Inval))
            stringLast = Inval.Substring(Inval.Length - 1, 1).ToUpper();
        else
        stringLast = string.Empty;

        return stringLast;
    }
    public static string GetStringZeroToEmpty(string InVal)
    {
        string StringEmpty = "";
        if (InVal == "0")
        {
            StringEmpty = "";
        }
        return StringEmpty;                        
    }
}
