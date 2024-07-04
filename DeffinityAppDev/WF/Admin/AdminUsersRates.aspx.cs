using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.Bindings;
using Deffinity.BE;
using Deffinity.BLL;
using Certifications;
using VT.Entity;
using VT.DAL;
using System.Text;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class AdminUsersRates : System.Web.UI.Page
{
    DisBindings getData = new DisBindings();
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Admin";
        int uid = Convert.ToInt32(Request.QueryString["uid"]);
        getUserId.Value = Request.QueryString["uid"];
        if (!this.IsPostBack)
        {
            BindReqData();
            SelectUserData(uid);
        }

        if (getUserId.Value != string.Empty)
        {
            DisplayUserRate(uid);
        }
        
    }
    public void DisplayUserRate(int ContractorID)
    {
        SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlCommand myCommand = new SqlCommand("DN_DisplayRate", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add("@ContractorsId", SqlDbType.Int, 32).Value = ContractorID;
        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
        DataSet ds = new DataSet();
        myadapter.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void img_RateSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DbCommand cmd = db.GetStoredProcCommand("DN_UserRate");
            db.AddInParameter(cmd, "@ContractorsId", DbType.Int32, Convert.ToInt32(getUserId.Value));


            db.AddInParameter(cmd, "@Entrytype", DbType.Int32, Convert.ToInt32(ddlentry.SelectedValue));

            db.AddInParameter(cmd, "@Hourlyrate_Buying ", DbType.String, getDouble(txthourly_Buying.Text.Trim()));
            db.AddInParameter(cmd, "@Hourlyrate_Selling", DbType.String, getDouble(txthourly_Selling.Text.Trim()));
            if (txt_minimundailyhours.Text == "")
            {
                db.AddInParameter(cmd, "@Minimumdailyhours", DbType.Int32, 0);
                db.AddInParameter(cmd, "@Minimumdailyhours", DbType.String, "0");
            }
            else
            {
                string val = txt_minimundailyhours.Text.Trim();
                char[] comm = { ':' };
                string[] getva = val.Split(comm);

                string newval = "";

                newval = (getva[0]) + "." + getva[1];

                int GetTotal = (Convert.ToInt32(getva[0])) * 60;
                int Totalmin = 0;
                Totalmin = Convert.ToInt32(getva[1]);
                GetTotal = GetTotal + Totalmin;

                db.AddInParameter(cmd, "@Minimumdailyhours", DbType.String, getDouble(newval));
                db.AddInParameter(cmd, "@MinimulDailyhours1", DbType.Int32, GetTotal);
            }

            //@Hourly_Selling




            //if getVal = 1 sucess 2 for already item exist
            int getVal = (int)db.ExecuteNonQuery(cmd);
            cmd.Dispose();
            if (getVal == 1)
            {
                //lbluserrate.Visible = true;
                lblMsg.Text = "User rate applied successfully";
                //lbluserrate.ForeColor = System.Drawing.Color.Green;
                //DisplayUserRate();
                txtExpensetype.Text = "";
                txthourly_Buying.Text = "";
                txthourly_Selling.Text = "";
                ddlentry.SelectedIndex = 0;
                txt_minimundailyhours.Text = "";
                DisplayUserRate(Convert.ToInt32(getUserId.Value));
                BindReqData();
            }
            else
            {
                lbluserrate.Visible = true;
                lbluserrate.Text = "Rate already exists";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }



    }

    private void BindReqData()
    {

        getData.DdlBindSelect(ddlentry, "select ID as EntryTypeID,EntryType from TimesheetEntryType order by EntryType ", "EntryTypeID", "EntryType", false, true);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            int newval = 0;
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            int i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];
            int contractorID = 0;
            contractorID = Convert.ToInt32(getUserId.Value);
            int EntryType1 = Convert.ToInt32(((DropDownList)Row.FindControl("ddlEntry")).SelectedItem.Value);

            double Hoursbuying = 0;
            if (((TextBox)Row.FindControl("txtHoursBuying")).Text != "")
            {
                Hoursbuying = Convert.ToDouble(((TextBox)Row.FindControl("txtHoursBuying")).Text);
            }
            double HoursSelling = 0;
            if (((TextBox)Row.FindControl("txtHoursselling")).Text != "")
            {
                HoursSelling = Convert.ToDouble(((TextBox)Row.FindControl("txtHoursselling")).Text);
            }
            decimal Minimumdailyhours = 0;
            if (((TextBox)Row.FindControl("txtMinimumdailyhours")).Text != "")
            {
                string val = ((TextBox)Row.FindControl("txtMinimumdailyhours")).Text;
                char[] comm = { ':' };
                string[] getva = val.Split(comm);

                string gethours = "";
                gethours = (getva[0]) + "." + getva[1];

                newval = (Convert.ToInt32(getva[0])) * 60;
                //+ getva[1];
                int Totalmin = 0;
                Totalmin = Convert.ToInt32(getva[1]);
                newval = newval + Totalmin;

                // Minimumdailyhours = Convert.ToDecimal(((TextBox)Row.FindControl("txtMinimumdailyhours")).Text);

                Minimumdailyhours = Convert.ToDecimal(gethours);

            }

            GetupdateRate(ID, contractorID, EntryType1, Hoursbuying, HoursSelling, Minimumdailyhours, Convert.ToDecimal(newval));

        }
        if (e.CommandName == "Delete")
        {

            string id = e.CommandArgument.ToString();

            string delete = "delete from TimeSheetRate where ID='" + id + "'";

            SqlCommand cmd = new SqlCommand(delete, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            finally
            {
                con.Close();

            }
            GridView1.EditIndex = -1;
           
            DisplayUserRate(Convert.ToInt32(getUserId.Value));
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        
        DisplayUserRate(Convert.ToInt32(getUserId.Value));
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        
        DisplayUserRate(Convert.ToInt32(getUserId.Value));
    }
     public void GetupdateRate(int ID, int contractorID, int EntryTypeID, double Hourly_Buying, double Hourly_Selling, decimal Minimumdailyhours, decimal minimumdailyhours1)
    {
        DbCommand cmd = db.GetStoredProcCommand("DN_UpdateUserRate");
        db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
        db.AddInParameter(cmd, "@ContractorsId", DbType.Int32, contractorID);
        db.AddInParameter(cmd, "@Entrytype", DbType.Int32, EntryTypeID);
        db.AddInParameter(cmd, "@Hourlyrate_Buying ", DbType.String, Hourly_Buying);
        db.AddInParameter(cmd, "@Hourlyrate_Selling", DbType.String, Hourly_Selling);
        db.AddInParameter(cmd, "@Minimumdailyhours", DbType.Decimal, Minimumdailyhours);
        db.AddInParameter(cmd, "@MinimulDailyhours1", DbType.Decimal, minimumdailyhours1);
        //if getVal = 1 sucess 2 for already item exist
        int getVal = (int)db.ExecuteNonQuery(cmd);
        cmd.Dispose();
        if (getVal == 1)
        {
            //lbluserrate.Visible = true;
            lblMsg.Text = "User rates updated successfully";
            //lbluserrate.ForeColor = System.Drawing.Color.Green;
        }
    }
    
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        DisplayUserRate(Convert.ToInt32(getUserId.Value));
    }
    private double getDouble(string st)
    {
        double t = 0;
        try
        {
            if (st != "")
            {
                t = Convert.ToDouble(st);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return t;
    }
    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";


        //   string GetHours1 = GetDisplay.ToString();
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }
    protected void btnadd_entry_Click(object sender, EventArgs e)
    {
        txtExpensetype.Visible = true;
        btnadd1.Visible = true;
        btncancel1.Visible = true;
        ddlentry.Visible = false;
        btnadd_entry.Visible = false;
    }
    protected void btnadd1_Click(object sender, EventArgs e)
    {
        DbCommand cmd = db.GetStoredProcCommand("DN_InsertEntryType");
        db.AddInParameter(cmd, "@Entrytype", DbType.String, txtExpensetype.Text);
        int getVal = (int)db.ExecuteNonQuery(cmd);

        BindReqData();

        txtExpensetype.Visible = false;
        btnadd1.Visible = false;
        btncancel1.Visible = false;
        ddlentry.Visible = true;
        btnadd_entry.Visible = true;

    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        txtExpensetype.Visible = false;
        btnadd1.Visible = false;
        btncancel1.Visible = false;
        ddlentry.Visible = true;
        btnadd_entry.Visible = true;
    }
    private void SelectUserData(int cid)
    {

        try
        {
            //edit name panel
            DbCommand cmd = db.GetStoredProcCommand("DN_SelectResource");
            db.AddInParameter(cmd, "@ID", DbType.Int32, cid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    lblUserratename.Text = dr["ContractorName"].ToString();
                    lblusername.Text = dr["ContractorName"].ToString();
                   
                }
                dr.Close();
            }
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected void btngohome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Adminusers.aspx");
    }
}
