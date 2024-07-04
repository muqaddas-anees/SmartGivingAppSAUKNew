using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Incidents.Entity;
using Deffinity.EmailService;
using System.Collections;
using Incidents.DAL;
using Incidents.StateManager;

public partial class CCScheduledTasks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Change Control";
        if (sessionKeys.ChangeControlID == 0)
        {
            lblPageTitle.InnerText = "Change Control Tasks -  Log New Change Control Request";
           
        }
        else
        {
            lblPageTitle.InnerText = "Change Control Tasks - Reference " + sessionKeys.ChangeControlID.ToString();
           
        }
    }

    private void InsertChangeTasks()
    {
        try
        {
            if (sessionKeys.ChangeControlID == 0)
            {
                lblTaskMessage.Text = "Needs to add the Change Control, before you assign the Task.";
            }
            else
            {
                Task task = new Task();
                task.ChangeControlID = sessionKeys.ChangeControlID;
                task.TaskDescription = txtTask.Text.Trim();
                task.Change = string.Empty; //txtChange.Text.Trim();
                task.NewDate = (!string.IsNullOrEmpty(txtNewDate.Text)) ? Convert.ToDateTime(txtNewDate.Text.Trim()) : Convert.ToDateTime("1/1/1900"); ;
                task.OriginalDate = (!string.IsNullOrEmpty(txtOriginalDate.Text)) ? Convert.ToDateTime(txtOriginalDate.Text.Trim()) : Convert.ToDateTime("1/1/1900");
                TaskHelper.Insert(task);
                GridView1.DataBind();
                ClearTask();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ClearTask()
    {
        txtTask.Text = string.Empty;
        //txtChange.Text = string.Empty;
        txtOriginalDate.Text = string.Empty;
        txtNewDate.Text = string.Empty;
    }

    protected void btnAddChange_Click(object sender, EventArgs e)
    {
        InsertChangeTasks();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Task task = new Task();
        task.Id = Convert.ToInt32(((Label)GridView1.Rows[e.RowIndex].FindControl("lblID")).Text);
        task.Change = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtChange")).Text;
        task.ChangeControlID = sessionKeys.ChangeControlID;
        string newDate = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNewDate")).Text;
        task.NewDate = string.IsNullOrEmpty(newDate) ? Convert.ToDateTime("1/1/1900") : Convert.ToDateTime(newDate);
        task.OriginalDate = Convert.ToDateTime(((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtOriginalDate")).Text);
        task.TaskDescription = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTaskDescription")).Text;
        TaskHelper.Update(task);
    }

    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
            e.ExceptionHandled = true;
    }

    protected string validateDate(object date)
    {
        DateTime alteredDate = Convert.ToDateTime(date);
        if (alteredDate.Year == 1900)
            return "Not Specified";
        else
            return alteredDate.ToShortDateString();
    }
    protected string validateDateForMakingEmpty(object date)
    {
        DateTime alteredDate = Convert.ToDateTime(date);
        if (alteredDate.Year == 1900)
            return "";
        else
            return alteredDate.ToShortDateString();
    }
    
}
