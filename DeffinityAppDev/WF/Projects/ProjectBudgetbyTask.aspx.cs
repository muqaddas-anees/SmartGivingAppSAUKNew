using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.ServiceCatalogManager;
using System.Collections;
using Deffinity.ProgrammeManagers;

public partial class ProjectBudgetbyTask : BasePage
{
    SqlConnection conn = new SqlConnection(Constants.DBString);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // Master.PageHead = Resources.DeffinityRes.ProjectManagement;//"Project Management";
            if (!IsPostBack)
            {

                BindGrid();
                CheckUserRole();

            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void BindGrid()
    {
        grdProjectBudget.DataSource = ServiceCatalogManager.ProjectBudget_SelectByRef(QueryStringValues.Project);
        grdProjectBudget.DataBind();
    }
    private void BindBOM(int TaskID)
    {
        GridView2.DataSource = ServiceCatalogManager.ProjectBudget_SelectBOM(QueryStringValues.Project, TaskID);
        GridView2.DataBind();
    }

    private void BindSingleTask(int ID)
    {
        if (Page.IsPostBack)
        {
            grdProjectBudget.DataSource = ServiceCatalogManager.ProjectBudget_SelectTaskItem(ID, QueryStringValues.Project);
            grdProjectBudget.DataBind();
        }

    }

  

    protected void grdProjectBudget_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                //BindSingleTask(ID);
                int i = grdProjectBudget.EditIndex;
                GridViewRow row = grdProjectBudget.Rows[i];
                TextBox txtProjectTask = (TextBox)row.FindControl("txtProjectTask");
                TextBox txtEstimatedHrs = (TextBox)row.FindControl("txtEstimatedHrs");
                TextBox txtFee = (TextBox)row.FindControl("txtFee");
                TextBox txtCost = (TextBox)row.FindControl("txtCost");
                TextBox txtResourceFee = (TextBox)row.FindControl("txtResourceFee");
                TextBox txtResourceCost = (TextBox)row.FindControl("txtResourceCost");
                //TextBox txtResourceProfit = (TextBox)row.FindControl("txtResourceProfit");

                ServiceCatalogManager.ProjectBudget_UpdateTaskItems(ID, Convert.ToDouble(string.IsNullOrEmpty(txtFee.Text) ? "0" : txtFee.Text),
                    Convert.ToDouble(string.IsNullOrEmpty(txtCost.Text) ? "0" : txtCost.Text),
                    Convert.ToDouble(string.IsNullOrEmpty(txtResourceFee.Text) ? "0" : txtResourceFee.Text),
                    Convert.ToDouble(string.IsNullOrEmpty(txtResourceCost.Text) ? "0" : txtResourceCost.Text),
                      ConvertHoursToMinutes(Convert.ToDouble(string.IsNullOrEmpty(txtEstimatedHrs.Text) ? "0" : GetHours(txtEstimatedHrs.Text).ToString())));
            }
            //if (e.CommandName == "UpdateBOM")
            //{
            //    int ID = Convert.ToInt32(e.CommandArgument.ToString());


            //    //int i = grdProjectBudget.EditIndex;
            //    //GridViewRow row = grdProjectBudget.Rows[i];

            //    //Label lblProjectTask = (Label)row.FindControl("lblProjectTask");
            //    //Hd_TaskName.Value = lblProjectTask.Text;
            //    //lblTaskName.Text = Hd_TaskName.Value;
            //    lblErr.Visible = false;
            //    hd_TaskID.Value = e.CommandArgument.ToString();

            //    mpopBOM.Show();
            //    BindBOM(int.Parse(e.CommandArgument.ToString()));
            //}
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    public double ConvertHoursToMinutes(double hours)
    {
        return TimeSpan.FromHours(hours).TotalMinutes;
    }

    public double ConvertMinutesToHours(string minutes)
    {

        double m = Convert.ToDouble(string.IsNullOrEmpty(minutes) ? "0" : GetHours(minutes).ToString());
        return TimeSpan.FromMinutes(m).TotalHours;
    }
    protected void grdProjectBudget_RowEditing(object sender, GridViewEditEventArgs e)
    {

        int i = e.NewEditIndex; ;
        GridViewRow row = grdProjectBudget.Rows[i];

        ////GridViewRow row = grdProjectBudget.Rows[e.NewEditIndex];
        LinkButton lblID = (LinkButton)row.FindControl("LinkButtonEdit");


        grdProjectBudget.EditIndex = 0;// e.NewEditIndex;
        BindSingleTask(int.Parse(lblID.CommandArgument)); // BindGrid();

    }
    protected void grdProjectBudget_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdProjectBudget.EditIndex = -1;
        BindGrid();
    }
    protected void imgItemEdit1_Click(object sender, EventArgs e)
    {
        try
        {
            mpopBOM.Show();
            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
            Label lblID1 = (Label)row.FindControl("lblID1");
            // Label lblServiceDescription = (Label)row.FindControl("Label2");
            Label lblProjectTask = (Label)row.FindControl("lblProjectTask");
            //Hd_TaskName.Value = lblProjectTask.Text;
            //lblTaskName.Text ="Items required for Task: "+ lblProjectTask.Text;
            lblTaskName.Text = Resources.DeffinityRes.ItemsrequiredforTask + lblProjectTask.Text;
            lblTaskName.Width = Unit.Pixel(500);
            lblErr.Visible = false;
            hd_TaskID.Value = lblID1.Text;

            mpopBOM.Show();
            BindBOM(int.Parse(hd_TaskID.Value));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
  

    //Update BOM
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView2.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("chkbox");
                if (chkRow.Checked)
                {

                    Label lblAvil = (Label)row.FindControl("lblAvil");
                    Label lblID = (Label)row.FindControl("lblID");
                    TextBox txtQtyReq = (TextBox)row.FindControl("txtQtyReq");
                    //Label lblType = (Label)row.FindControl("lblType");
                    if (int.Parse(lblAvil.Text) >= int.Parse(txtQtyReq.Text))
                    {
                        ServiceCatalogManager.ProjectBudget_UpdatInsertBOM(QueryStringValues.Project, int.Parse(hd_TaskID.Value),
                            int.Parse(lblID.Text), int.Parse(string.IsNullOrEmpty(txtQtyReq.Text) ? "0" : txtQtyReq.Text), "");
                    }
                    else
                    {
                        lblErr.Visible = true;
                        lblErr.Text = Resources.DeffinityRes.QtyReqGrthannum;//"The quantity you have requested is greater than the number available.";
                        lblErr.ForeColor = System.Drawing.Color.Red;

                        mpopBOM.Show();
                        BindBOM(int.Parse(hd_TaskID.Value));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void grdProjectBudget_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        LinkButton lblID = (LinkButton)grdProjectBudget.Rows[e.RowIndex].FindControl("LinkButtonEdit");

        grdProjectBudget.EditIndex = -1;

        BindGrid();

    }
   
    //task title based on indent value
    protected string getItemDes(string indent, string desc)
    {
        return Deffinity.ProjectTasksManagers.ProjectTasksManager.DisplayIndentLevel(desc, Convert.ToInt32(string.IsNullOrEmpty(indent) ? "0" : indent));
    }

    public string Changehours(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);

        if (displayTime.Length > 1)
        {
            GetActivity = displayTime[0] + ":" + (string.IsNullOrEmpty(displayTime[1]) ? "00" : displayTime[1]);
        }
        else
        {
            GetActivity = displayTime[0] + ":" + "00";
        }



        return GetActivity;
    }

    public double GetHours(string getmin)
    {
        double Hours = 0;
        string val = getmin;
        char[] comm = { ':' };
        string[] getva = val.Split(comm);

        string newval = "";
        if (getva.Length > 1)
        {
            newval = getva[0] + "." + (string.IsNullOrEmpty(getva[1]) ? "00" : getva[1]);
        }
        else
        {
            newval = getva[0] + "." + "00";
        }
        //newval = getva[0] + "." + getva[1];
        Hours = Convert.ToDouble(newval);
        return Hours;
    }
    protected void grdProjectBudget_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }
    protected void grdProjectBudget_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        BindBOM(int.Parse(hd_TaskID.Value));
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        BindBOM(int.Parse(hd_TaskID.Value));
    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView2.EditIndex = -1;
        BindBOM(int.Parse(hd_TaskID.Value));
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    public string ChangeHoues(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.', ',' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }


    #region Check Permission
    //03/06/2011 by sani

    private void CheckUserRole()
    {
        try
        {
            if ((Request.QueryString["Project"] != null))
            {
                if (sessionKeys.SID != 1)
                {
                    int role = 0;
                    role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {
                        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                        Disable();

                    }
                    role = Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                        Disable();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void Disable()
    {
        //imgSave.Enabled = false;
        imgUpdate.Enabled = false;


    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
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
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    #endregion

    protected void grdProjectBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        grdProjectBudget.PageIndex = e.NewPageIndex;
        grdProjectBudget.DataBind();
    }
}
