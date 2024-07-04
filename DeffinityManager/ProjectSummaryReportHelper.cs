using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ProjectSummaryReportHelper
/// </summary>
public static class ProjectSummaryReportHelper
{
    public static DataView fillGrid(string Portfolio, string ProjectOwner, string Programme, string Site)
    {
        DataView view = new DataView();
        DataSet data = new DataSet();
        getDataFromDatabase(data);
        getFilteredData(view, data,Portfolio,Programme,ProjectOwner,Site);
        return view;
    }

    private static void getFilteredData(DataView view, DataSet data,string Portfolio,string Programme, string ProjectOwner, string Site)
    {
        view.Table = data.Tables[0];

        string filterGenerator = string.Empty;
        string filterPortfolio = string.Empty;
        string filterProgramme = string.Empty;
        string filterProjectOwner = string.Empty;
        string filterSite = string.Empty;


        //No condition specified.
        if (Portfolio == "0" && Programme == "0" && ProjectOwner == "0" && Site == "0")
        {
            view.RowFilter = string.Empty;
        }
        else
        {
            if (Portfolio != "0")
                filterPortfolio = "Portfolio='" + Portfolio + "'";
            else
                filterPortfolio = "1=2";
            if (Programme != "0")
                filterProgramme = "Programme='" + Programme + "'";
            else
                filterProgramme = "1=2";
            if (ProjectOwner != "0")
                filterProjectOwner = "Owner='" + ProjectOwner + "'";
            else
                filterProjectOwner = "1=2";
            if (Site != "0")
                filterSite = "Site='" + Site + "'";
            else
                filterSite = "1=2";

            filterGenerator = filterPortfolio 
                                + " OR " + filterProgramme 
                                + " OR " + filterProjectOwner 
                                + " OR " + filterSite;
            view.RowFilter = filterGenerator;
        }
    }


    //Gets and fills the data to the dataset object
    public static void getDataFromDatabase(DataSet data)
    {
        using(SqlConnection conn=new SqlConnection(ConfigurationManager.ConnectionStrings["DBString"].ToString()))
        {
            using(SqlCommand cmd=new SqlCommand())
            {
                cmd.CommandText="DN_ProjectDetails_ForSummaryReport";
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", sessionKeys.UID);
                

                cmd.Connection=conn;
                conn.Open();
                using(SqlDataReader reader=cmd.ExecuteReader())
                {
                    data.Tables.Add(new DataTable());
                    data.Tables[0].Load(reader);
                }
            }
        }
    }
}
