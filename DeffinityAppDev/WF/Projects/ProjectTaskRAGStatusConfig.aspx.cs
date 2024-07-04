using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;

public partial class ProjectTaskRAGStatusConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindListTasks(ddlltTasks);
            if (ddlltTasks.Items.Count > 0)
            {
                ddlltTasks.SelectedIndex = 1;
                BindRAGStatusConfig(int.Parse(ddlltTasks.SelectedValue));
            }
          
        }
    }
    private void BindListTasks(DropDownList ddl)
    {
        using (projectTaskDataContext _db = new projectTaskDataContext())
        {

            var listTasks = (from r in _db.ProjectTaskItems
                             where r.ProjectReference == QueryStringValues.Project
                             orderby r.ListPosition
                             select new { ID = r.ID, Name = (r.ListPosition) + "-" + r.ItemDescription }).ToList();
            if (listTasks != null)
            {
                ddl.DataSource = listTasks;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "ID";
                ddl.DataBind();
               
            }
            ddl.Items.Insert(0, new ListItem("Please select...", "0"));
        }


    }
    protected void ddlltTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrMsg.Visible = false;
        BindRAGStatusConfig(int.Parse(ddlltTasks.SelectedValue));

        

       // BindGrid();
        // mdlPopTaskDocs.Show();
    }
    private void BindRAGStatusConfig(int taskId)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var projectTaskItem = db.ProjectTaskItems.Where(p => p.ID == taskId).FirstOrDefault();
                if (projectTaskItem != null)
                {
                    txtAmberDays.Text = projectTaskItem.AmberDays.ToString();
                    txtRedDays.Text = projectTaskItem.RedDays.ToString();
                    ddlAmberPercent.SelectedValue = projectTaskItem.AmberPercent.ToString();
                    ddlRedPercent.SelectedValue = projectTaskItem.RedPercent.ToString();
                }
                else
                {
                    txtAmberDays.Text = string.Empty;
                    txtRedDays.Text = string.Empty;
                    ddlAmberPercent.SelectedValue = "0";
                    ddlRedPercent.SelectedValue = "0";
                }

               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);   
        }
    }
    protected void btnUpdateItem_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                  var projectTaskItem = db.ProjectTaskItems.Where(p => p.ID == int.Parse(ddlltTasks.SelectedValue)).FirstOrDefault();
                  if (projectTaskItem != null)
                  {
                      projectTaskItem.AmberDays = Convert.ToInt32(txtAmberDays.Text);
                      projectTaskItem.RedDays = Convert.ToInt32(txtRedDays.Text);
                      projectTaskItem.AmberPercent = Convert.ToInt32(ddlAmberPercent.SelectedValue);
                      projectTaskItem.RedPercent = Convert.ToInt32(ddlRedPercent.SelectedValue);
                      db.SubmitChanges();
                      lblErrMsg.Visible = true;
                      lblErrMsg.Text = "Task updated successfully";
                      lblErrMsg.ForeColor = System.Drawing.Color.Green;
                 
                  }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);   
        }
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        txtAmberDays.Text = string.Empty;
        txtRedDays.Text = string.Empty;
        ddlAmberPercent.SelectedValue = "0";
        ddlRedPercent.SelectedValue = "0";
         ddlltTasks.SelectedValue="0";
    }
}