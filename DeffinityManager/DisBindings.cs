using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections.Generic;


/// <summary>
/// Summary description for DisBindings
/// </summary>
public class DisBindings
{
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    //public Database db;
	public DisBindings()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //Parameter data table
    //public DataTable GetDataTable(string sqlQry, bool SP_yes_no,object[] ParameterList)
    //{ 
    
    
    
    //}

    //return's table
    public DataTable GetDatatable(string sqlQry, bool SP_yes_no)
    {
        
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand(sqlQry, myConnection);
        //check text or storedprocedure
        if (SP_yes_no)
        {
            cmd.CommandType = CommandType.StoredProcedure;
        }
        else
        {
            cmd.CommandType = CommandType.Text;
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);
        da.Dispose();
        cmd.Dispose();

        return dt;
    
    }
 
    //bind dropdown
    public void DdlBind(DropDownList ddl, string sqlQry, string Vfeild, string Tfield, bool SP_yes_no)
    {
        try
        {
            DataTable DT_dropdown = new DataTable();
            DT_dropdown = GetDatatable(sqlQry, SP_yes_no);
            ddl.DataSource = DT_dropdown;
            ddl.DataTextField = Tfield;
            ddl.DataValueField = Vfeild;
            ddl.DataBind();
            DT_dropdown.Clear();           
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Display Bindings class file");
        } 
    
    }
    //bind dropdown with select..
    public void DdlBindSelect(DropDownList ddl, string sqlQry, string Vfeild, string Tfield, bool SP_yes_no,bool addSelect)
    {
        try
        {
            ddl.Items.Clear();
            DdlBind(ddl, sqlQry, Vfeild, Tfield, SP_yes_no);

            if (addSelect)
            {
                ddl.Items.Insert(0, "Please select...");
            }
        }
        catch (Exception ex)
        {
                      if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, "Please select..."); 
            }
        }
    }
    public void DdlBindSelect(DropDownList ddl, string sqlQry, string Vfeild, string Tfield, bool SP_yes_no, bool addSelect,bool AddListItems)
    {
        try
        {
            ddl.Items.Clear();
            DdlBind(ddl, sqlQry, Vfeild, Tfield, SP_yes_no);

            if (AddListItems)
            {
                ddl.Items.Insert(0, Constants.ddlDefaultBind(true));
            }
        }
        catch (Exception ex)
        {            
            if (ddl.Items.Count == 0)
            {
                ddl.Items.Insert(0, Constants.ddlDefaultBind(true));
            }
        }
    }    
    public string exeScalar(string sqlQry, bool SP_yes_no)
    {
        
        string retval = "";        
        
        try
        {            
            SqlCommand cmd =  new SqlCommand(sqlQry,myConnection);
            //check text or storedprocedure
            if (SP_yes_no)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }
            myConnection.Open();
            retval = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            
        }
        catch (Exception ex)
        {
            string s = ex.Message; 
        }
        finally
        {
            myConnection.Close();
        }

        return retval;
    }
    //Incident state
    public List<object> IncidentStatus()
    {
        List<object> listStatus= new List<object>();
        listStatus.Add("Open");
        listStatus.Add("Closed");
        listStatus.Add("On_Hold");
        return listStatus;
    }

    #region conversions
    public string getDate(string dt)
    {
        string retval = "";
        // string retval = null ;
        if (dt != "")
        {
            retval = Convert.ToDateTime(dt).ToShortDateString();
        }
        return retval;
    }

    public int getInt(string txt)
    {
        int i = 0;
        if (txt != "")
        {
            i = Convert.ToInt32(txt);
        }
        return i;

    }

    public double getDouble(string val)
    {
        double st = 0;
        if (string.IsNullOrEmpty(val))
        {
            st = 0;
        }
        else
        {
            try
            {
                st = Convert.ToDouble(val);
            }
            catch (Exception ex) { st = 0; }
        }
        return st;
    }
    public int getDdlval(string st)
    {
        int j = 0;
        if (st == "Please select...")
        {
            j = 0;
        }
        else
        {
            try
            {
                j = Convert.ToInt32(st);
            }
            catch (Exception ex)
            { j = 0; }
        }
        return j;
    }
    public string setDdlval(string st)
    {
        string j = "";
        if (st == "0")
        {
            j = "Please select...";
        }
        else
        {
            try
            {
                j = st;
            }
            catch (Exception ex)
            { j = "Please select..."; }
        }
        return j;
    }
    public void selectDdlNewEntry(string Item, DropDownList ddl)
    {
        try
        {
            if (Item != "")
            {
                ddl.SelectedItem.Text = Item;
            }
            else
            {
                ddl.SelectedIndex = 0;
            }

        }
        catch { }
    }

    #endregion
  
}



