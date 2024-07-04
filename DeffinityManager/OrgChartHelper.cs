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
/// This is a class that is useful to interact with database and the Organizationchart page
/// </summary>
public class OrgChartHelper:IDisposable
{
	public OrgChartHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Gets all the records based on the reference.
    /// If reference is not specified all the records will be retrieved with out reference
    /// </summary>
    /// <param name="Reference"></param>
    /// <returns>DataTable</returns>
    public DataTable getFlowChartRecords(int portfolioID)
    {
        DataTable table = new DataTable();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBstring"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DN_Select_GetOrganizationChartTableInfo";
                    cmd.Parameters.AddWithValue("PortfolioID", portfolioID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        return table;
    }

    /// <summary>
    /// Inserts new record into the database
    /// </summary>
    /// <param name="ReferenceNumber"></param>
    /// <param name="Title"></param>
    /// <param name="Description"></param>
    /// <param name="bytes"></param>
    public void InsertNewRecord(string Reference, string Title, string Description, byte[] bytes, string fileName, string portfolioID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBstring"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.CommandText = "DN_Ins_OrganizationChart";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Reference", Reference);
                    cmd.Parameters.AddWithValue("@Title", Title);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@OrganizationChartFile", bytes);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@PortFolioID", portfolioID);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    #region Grid Methods

    //Finds the grid index that matches the datasource
    private int GridIndexFinder(GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        return index;
    }

    //Does the specified operation
    public object DataGridOperations(GridViewCommandEventArgs e)
    {
        int dataKey = GridIndexFinder(e);

        //Do the required operations based on the command.
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBString"].ToString()))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", dataKey);
                    cmd.Parameters.AddWithValue("@CommandName", e.CommandName);
                    cmd.CommandText = "DN_Org_PortfolioOperations";
                    if (!e.CommandName.Contains("DownLoad"))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            return table;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //If the command is not the download, then return this string as object.
        return "NoDownLoad";
    }

    /// <summary>
    /// This is the helper method for downloading the files
    /// </summary>
    /// <param name="downLoadFile"></param>
    public void downLoadFile(DataTable downLoadFile)
    {
        HttpContext.Current.Response.ContentType = "image";
        byte[] getContent = (byte[])downLoadFile.Rows[0][0];
        HttpContext.Current.Response.BinaryWrite(getContent);
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + downLoadFile.Rows[0][1].ToString());
        HttpContext.Current.Response.End();
    }


    #endregion

    #region IDisposable Members

    public void Dispose()
    {
        GC.Collect();
    }

    #endregion
}
