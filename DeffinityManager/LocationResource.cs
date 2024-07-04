using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Linq;
using AjaxControlToolkit;
using Location.Entity;
using Location.DAL;


/// <summary>
/// Summary description for LocationResource
/// </summary>
public class LocationResource
{
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
	public LocationResource()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable GetCityListByCountyId(int CountyId)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_City", new SqlParameter("@CID", CountyId)).Tables[0];
    }

    public static CascadingDropDownNameValue[] City_CaseCade(int CID)
    {
        LocationDataContext locCntxt = new LocationDataContext();
        List<CascadingDropDownNameValue> GetCityList = new List<CascadingDropDownNameValue>();
        
        try
        {
            GetCityList = (from c in locCntxt.Cities
                           where c.CountryID == CID 
                           orderby c.City1
                           select new CascadingDropDownNameValue
                           {
                               name = c.City1, value = c.ID.ToString()
                           }).ToList();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return GetCityList.ToArray();
        
        //DataTable dt = GetCityListByCountyId(CID);

        //List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    values.Add(new CascadingDropDownNameValue((string)dr["City"], dr["ID"].ToString()));
        //}
        //return values.ToArray();
    }



    }

    

