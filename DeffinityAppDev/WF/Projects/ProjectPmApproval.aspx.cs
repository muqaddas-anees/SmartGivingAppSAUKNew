using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class ProjectPmApproval : System.Web.UI.Page
{
    string error = "";
    DisBindings getData = new DisBindings();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        //Master.PageHead = "Check Points";
        if (!IsPostBack)
        {
            //new bindings
            
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
        }
    }
    
   
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {        

    }
    private void UpdateAssignTo(string AssingTo, string ID)
    {
         try
        {
            Database db = DatabaseFactory.CreateDatabase("DBstring");
            DbCommand cmd = db.GetStoredProcCommand("Deffinity_AssignToQASchedule");            
            db.AddInParameter(cmd, "@AssingTo", DbType.Int32, Convert.ToInt32(AssingTo));
            db.AddInParameter(cmd, "@ID", DbType.Int32, Convert.ToInt32(ID));     
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int index = GridView1.EditIndex;
            GridViewRow Grow = GridView1.Rows[index];
            TextBox txtSDate = (TextBox)Grow.FindControl("txtEdate");
            TextBox txtNote = (TextBox)Grow.FindControl("txtNotes");
            DropDownList ddl = (DropDownList)Grow.FindControl("ddlResources");            
            SqlDataSource1.UpdateParameters["ID"].DefaultValue = e.Keys["ID"].ToString();
            SqlDataSource1.UpdateParameters["ScheduledDate"].DefaultValue = txtSDate.Text.Trim();
            SqlDataSource1.UpdateParameters["Notes"].DefaultValue = txtNote.Text.Trim();
            SqlDataSource1.UpdateParameters["assignto"].DefaultValue = ddl.SelectedValue;           
            SqlDataSource1.Update();
        }
        catch(Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }       
      
       
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        if (e.CommandName == "Delete")
        {
            Page.MaintainScrollPositionOnPostBack = true;
            string str1 = "";
            string i = e.CommandArgument.ToString();            
            SqlDataReader dr;
            try
            {
                SqlCommand cmd = new SqlCommand("Deffinity_ProjectQAScheduleDelete", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", Convert.ToInt32(i)));
                myConnection.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                myConnection.Close();
            }

            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);

            }
            finally
            {
                myConnection.Close();

            }
        }
    }
   
    
    #region grid_functions
    protected string getIssue(string status)
    {
        string stemp = "";
        if (string.IsNullOrEmpty(status))
        {
            stemp = "1";
        }
        else
        {
            stemp = status;
        }
        return stemp;
    }

   
    #endregion
        
    protected void btnBack_Click(object sender, EventArgs  e)
    {
        if (Request.QueryString["Project"] != null)
        {
            //Response.Redirect("~/ProjectRisks.aspx?Project=" + Request.QueryString["Project"].ToString());
            Response.Redirect(PermissionManager.GetBackURL(PermissionManager.Feature.ProjectCheckPoint) + "?Project=" + QueryStringValues.Project.ToString());
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Project"] != null)
        {
            //Response.Redirect("~/ProjectAsset.aspx?Project=" + Request.QueryString["Project"].ToString());
            Response.Redirect(PermissionManager.GetNextURL(PermissionManager.Feature.ProjectCheckPoint) + "?Project=" + QueryStringValues.Project.ToString());
        }
    }
}
