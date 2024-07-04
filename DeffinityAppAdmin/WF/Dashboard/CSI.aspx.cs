using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Deffinity.LessonsLearntManagers;
using Deffinity.Bindings;
using Deffinity.LessonsLearntEntitys;
using Deffinity.ProgrammeManagers;

public partial class CIP1 : System.Web.UI.Page
{
    int AC2PID, Project;
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    string Stemp = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        //Master.PageHead = "Dashboard";
 
        if (!Page.IsPostBack)
        {
            fillgrid();
            //Bind lesson learnt data
            BindLessonLearnt();
            fillgrid2();
        }

    }
    public void fillgrid()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("DN_DashbordCIP", con);
           // SqlParameter UID = new SqlParameter("@UserID", SqlDbType.Int);
           //UID.Value=sessionKeys.UID;
           //cmd.Parameters.Add(UID);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Program", int.Parse(string.IsNullOrEmpty(ddlProjGroups.SelectedValue) ? "0" : ddlProjGroups.SelectedValue));
            cmd.Parameters.AddWithValue("@ProjectRef", int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue));
            cmd.Parameters.AddWithValue("@SubProgram", int.Parse(string.IsNullOrEmpty(ddlsubprogram.SelectedValue) ? "0" : ddlsubprogram.SelectedValue));
            cmd.Parameters.AddWithValue("@Portfolio", int.Parse(string.IsNullOrEmpty(ddlPortfolio.SelectedValue) ? "0" : ddlPortfolio.SelectedValue));
            cmd.Parameters.AddWithValue("@UID", sessionKeys.UID);
            SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
            myadapter.Fill(dt);
            cmd.Connection.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    public void fillgrid2()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("DEFFINITY_CUSTSAT_REPORT", con);
            // SqlParameter UID = new SqlParameter("@UserID", SqlDbType.Int);
            //UID.Value=sessionKeys.UID;
            //cmd.Parameters.Add(UID);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Program", int.Parse(string.IsNullOrEmpty(ddlProjGroups.SelectedValue) ? "0" : ddlProjGroups.SelectedValue));
            cmd.Parameters.AddWithValue("@ProjectRef", int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue));
            cmd.Parameters.AddWithValue("@SubProgram", int.Parse(string.IsNullOrEmpty(ddlsubprogram.SelectedValue) ? "0" : ddlsubprogram.SelectedValue));
            cmd.Parameters.AddWithValue("@Portfolio", int.Parse(string.IsNullOrEmpty(ddlPortfolio.SelectedValue) ? "0" : ddlPortfolio.SelectedValue));
            cmd.Parameters.AddWithValue("@UID", sessionKeys.UID);
            SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
            myadapter.Fill(dt);
            cmd.Connection.Close();
            gridCustViews.DataSource = dt;
            gridCustViews.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    //Bind lesson learnt data
    private void BindLessonLearnt()
    {
        try
        {
            DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_LessonsLearntSelectAll",
                new SqlParameter("@UID",sessionKeys.UID),
                new SqlParameter ("@ProjectRef",ddlProjects.SelectedValue),
                new SqlParameter("@Program",ddlProjGroups.SelectedValue),
                new SqlParameter("@SubProgram",ddlsubprogram.SelectedValue),
                new SqlParameter("@Portfolio",ddlPortfolio.SelectedValue)).Tables[0];
            GridLessonsLearnt.DataSource = dt;// LessonsLearntManager.LessonsLearntSelectAll();
            GridLessonsLearnt.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            int ID = Convert.ToInt32(e.CommandArgument.ToString());
            int i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];
            string Improvement = ((TextBox)Row.FindControl("txtImprovement")).Text;
            string ResultExpected = ((TextBox)Row.FindControl("txtResultExpected")).Text;
            int ContractorID = Convert.ToInt32(((DropDownList)Row.FindControl("ddresource1")).SelectedValue);
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("updateProjectCIPUpdation");
                db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@Improvement", DbType.String, Improvement);
                db.AddInParameter(cmd, "@ContractorID", DbType.Int32, ContractorID);
                db.AddInParameter(cmd, "@ResultExpected", DbType.String, ResultExpected);

                db.ExecuteNonQuery(cmd);
                fillgrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           

        }

        if (e.CommandName == "Delete")
        {

            string id = e.CommandArgument.ToString();

            string delete = "delete from AC2PID_Improvement where ID='" + id + "'";

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
            fillgrid();
        }
       
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
        
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillgrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gridCustViews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ConvertCSI")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            
                //Make the field complete
                try
                {
                    //convert to csi
                    Database db = DatabaseFactory.CreateDatabase("DBstring");
                    DataKey data = gridCustViews.DataKeys[index];
                    int SurveyID = Convert.ToInt32(data.Value);
                    DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_TURNTOCSI");
                    db.AddInParameter(cmd, "@ID", DbType.Int32, SurveyID);
                    db.ExecuteNonQuery(cmd);
                    cmd.Dispose();
                    //gridCustViews.DataBind();
                    fillgrid2();
                    fillgrid();
                 }
                catch (genericException ex)
                {
                    lblConfirmation.Text = ex.Message;

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                    lblConfirmation.Text = "Error occured during conversion";
                }
           
        }
    }
    
    protected void GridLessonsLearnt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            
            //string i1 =  GridLessonsLearnt.SelectedDataKey.Value.ToString();
        }
    }
    protected void GridLessonsLearnt_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        GridLessonsLearnt.EditIndex = e.NewEditIndex;
        BindLessonLearnt();

    }
    protected void GridLessonsLearnt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridLessonsLearnt.EditIndex = -1;
        BindLessonLearnt();


    }
    protected void GridLessonsLearnt_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
                Label lblProjectReference = e.Row.FindControl("lblProjectReference") as Label;
                lblProjectReference.Text = sessionKeys.Prefix + objList[1].ToString();
                Label lblDescription = e.Row.FindControl("lblDescription") as Label;
                lblDescription.Text = objList[2].ToString();
                Label lblBusiness = e.Row.FindControl("lblBusiness") as Label;
                lblBusiness.Text = objList[4].ToString();
                Label lblIdentifiedBy = e.Row.FindControl("lblIdentifiedBy") as Label;
                lblIdentifiedBy.Text = objList[11].ToString();
                if (e.Row.FindControl("lblRemediation") != null)
                {
                    Label lblRemediation = e.Row.FindControl("lblRemediation") as Label;
                    lblRemediation.Text = objList[3].ToString();
                    Label lblAssignedTo = e.Row.FindControl("lblAssignedTo") as Label;
                    lblAssignedTo.Text = objList[12].ToString();
                    Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                    lblStatus.Text = objList[13].ToString();
                }
                else
                {
                    TextBox txtRemediation = e.Row.FindControl("txtRemediation") as TextBox;
                    txtRemediation.Text = objList[3].ToString();
                    DropDownList ddlAssignedTo = e.Row.FindControl("ddlAssignedTo") as DropDownList;
                    ddlAssignedTo.DataSource = DefaultDatabind.UserSelectAll(true);
                    ddlAssignedTo.DataTextField = "ContractorName";
                    ddlAssignedTo.DataValueField = Constants.ddlValField;
                    ddlAssignedTo.DataBind();
                    ddlAssignedTo.SelectedValue = objList[6].ToString();
                    DropDownList ddlStatus = e.Row.FindControl("ddlStatus") as DropDownList;
                    ddlStatus.DataSource = DefaultDatabind.b_ItemStatus();
                    ddlStatus.DataTextField = "Status";
                    ddlStatus.DataValueField = Constants.ddlValField;
                    ddlStatus.DataBind();
                    ddlStatus.SelectedValue = objList[7].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridLessonsLearnt_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int id = int.Parse(GridLessonsLearnt.DataKeys[e.RowIndex].Values[1].ToString());
            LessonsLearntEntity LL = new LessonsLearntEntity();
            LL = LessonsLearntManager.LessonsLearntSelect(id);
            //update 
            LL.AssignedTo = int.Parse(((DropDownList)GridLessonsLearnt.Rows[e.RowIndex].FindControl("ddlAssignedTo")).SelectedValue);
            LL.RemediationActions = ((TextBox)GridLessonsLearnt.Rows[e.RowIndex].FindControl("txtRemediation")).Text.Trim();
            LL.Status = int.Parse(((DropDownList)GridLessonsLearnt.Rows[e.RowIndex].FindControl("ddlStatus")).SelectedValue);
            LessonsLearntManager.LearnInsert(LL);

            GridLessonsLearnt.EditIndex = -1;
            BindLessonLearnt();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #region "Permission Code Here"
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    vis = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    vis = false;

                    // Disable();

                }

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    private void CheckUserRole()
    {

        if (sessionKeys.SID != 1)
        {
            int role = 0;
            role = Admin.CheckLoginUserPermission(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }
            role = Admin.GetTeamID(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }

        }

    }
    private void Disable()
    {
        //.Enabled = false;


    }
    #endregion 
    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        BindLessonLearnt();
        fillgrid2();
    }
    protected void ddlProjGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        BindLessonLearnt();
        fillgrid2();
    }
    protected void ddlsubprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        BindLessonLearnt();
        fillgrid2();
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        BindLessonLearnt();
        fillgrid2();
    }
    protected void GridLessonsLearnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridLessonsLearnt.PageIndex = e.NewPageIndex;
        BindLessonLearnt();
    }
}
