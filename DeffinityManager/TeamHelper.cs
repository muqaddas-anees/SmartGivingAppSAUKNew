using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TeamHelper
/// </summary>
public class TeamHelper:IDisposable
{

    #region Fields

    string _connStr;
    string _spName;
    SqlParameterCollection _parameters;
    SqlCommand _cmd;

    #endregion

    #region Contructors

    public TeamHelper()
	{
        _connStr = ConfigurationManager.ConnectionStrings["DBString"].ToString() ;
        _spName = string.Empty;
        _cmd = new SqlCommand();
    }

    public TeamHelper(string sp_Name)
    {
        _spName = sp_Name;
    }

    #endregion

    #region Properties

    public string ConnectionString
    {
        get
        {
            return _connStr;
        }
        set
        {
            _connStr = value;
        }
    }

    public string StoredProcedure
    {
        get
        {
            return _spName;
        }
        set
        {
            _spName = value;
        }
    }

    public SqlParameterCollection Parameters
    {
        get
        {
            return _parameters;
        }
        set
        {
            _parameters = value;
        }
    }

    public SqlCommand SqlCommand
    {
        set
        {
            _cmd = value;
        }
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// This method gets all the required data as a datatable
    /// </summary>
    /// <returns>Data Table. Which can be set as datasource for list items and grid items</returns>
    public DataTable GetData()
    {
        DataTable table = new DataTable();
        try
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                _cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
                conn.Close();
                
            }
        }
        catch
        {
            throw;
        }
        return table;
    }

    /// <summary>
    /// Updates the data base values
    /// </summary>
    /// <returns>Returns success or failure of the updates</returns>

    public bool UpdateOrInsertOrDeleteData()
    {
        int rowsAffected = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                _cmd.Connection = conn;
                conn.Open();
                rowsAffected = _cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch
        {
            throw;
        }

        if (rowsAffected <= 0)
            return false;

        return true;
    }

    public int CopyData()
    {
        int rowsAffected = 0;

        try
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                _cmd.Connection = conn;
                conn.Open();
                rowsAffected = Convert.ToInt32(_cmd.ExecuteScalar());
                conn.Close();

            }
        }
        catch
        {
            throw;
        }

        return rowsAffected;
    }

    #region Control Helpers

    public void BindDDLData(string sql, ListControl lstControl, string dataValueField, string dataTextField)
    {
        using (TeamHelper helper = new TeamHelper())
        {
            _cmd.CommandText = sql;
            helper.SqlCommand = _cmd;
            ListItem firstItem = new ListItem("Please Select", "0");
            lstControl.Items.Clear();
            lstControl.Items.Add(firstItem);
            lstControl.DataSource = helper.GetData();
            lstControl.DataTextField = dataTextField;
            lstControl.DataValueField = dataValueField;
            lstControl.DataBind();
        }
    }

    #endregion

    #endregion

    #region Grid Helper Methods

    ////Finds the grid index that matches the datasource
    //private int GridIndexFinder(GridViewCommandEventArgs e)
    //{
    //    int index = Convert.ToInt32(e.CommandArgument);
    //    return index;
    //}

    ////Does the specified operation
    //public void DataGridOperations(GridViewCommandEventArgs e)
    //{
    //    int dataKey = GridIndexFinder(e);
    //    if(e.CommandName.Contains("Select"))
    //    {
        
    //    }
    //}

    ///// <summary>
    ///// This is the helper method for downloading the files
    ///// </summary>
    ///// <param name="downLoadFile"></param>
    //public void downLoadFile(DataTable downLoadFile)
    //{
    //    HttpContext.Current.Response.ContentType = "image";
    //    byte[] getContent = (byte[])downLoadFile.Rows[0][0];
    //    HttpContext.Current.Response.BinaryWrite(getContent);
    //    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + downLoadFile.Rows[0][1].ToString());
    //    HttpContext.Current.Response.End();
    //}


    #endregion

    #region IDisposable Members

    public void Dispose()
    {
        GC.Collect();
        if (_cmd != null)
        {
            _cmd.Connection.Close();
            _cmd.Dispose();
        }
    }

    #endregion
}
