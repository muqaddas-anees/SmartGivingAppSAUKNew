using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CustomerTeamViewer
/// </summary>
public class CustomerScheduleViewer
{

    public DataTable GetDataBetweenDates(DateTime fromDate, DateTime toDate)
    {
        string cacheKey = "CustomerView" + sessionKeys.SID.ToString();
        if (HttpContext.Current.Cache[cacheKey] == null)
            HttpContext.Current.Cache[cacheKey] = getDataFromDB(fromDate, toDate);
        return (DataTable)HttpContext.Current.Cache[cacheKey];
    }

    private DataTable getDataFromDB(DateTime fromDate, DateTime toDate)
    {
        DataTable table;
        using(SqlConnection conn=new SqlConnection(connectionString.retrieveConnString()))
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.Parameters.AddWithValue("@PortfolioID", sessionKeys.PortfolioID);
                
                cmd.CommandText = "DN_RPT_ShiftDetails_Client";
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    table = new DataTable();
                    table.Load(reader);
                }
            }
        }
        return table;
    }
}
