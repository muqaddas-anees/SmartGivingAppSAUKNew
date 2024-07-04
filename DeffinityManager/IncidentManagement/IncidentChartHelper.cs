using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for IncidentChartHelper
/// </summary>
public class IncidentChartHelper
{
	public IncidentChartHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable ChartDisplay_byMastercatalogue(int Portfolio, string sdate, string edate)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Incident_getCountByMcategory", new SqlParameter("@PortfolioID", Portfolio), new SqlParameter("@StartDate", Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate)), new SqlParameter("@EndDate", Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate))).Tables[0];
    }
     public static DataTable ChartDisplay_byCatalogue(int Portfolio,string sr_type,string sdate,string edate,int site)
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Incident_GetCountByCategory", new SqlParameter("@PortfolioID", Portfolio), new SqlParameter("@SDtype", sr_type), new SqlParameter("@StartDate", Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? "01/01/1900" : sdate)), new SqlParameter("@EndDate", Convert.ToDateTime(string.IsNullOrEmpty(edate) ? "01/01/1900" : edate))
            ,new SqlParameter("@SiteID",site)).Tables[0];
    }
}
