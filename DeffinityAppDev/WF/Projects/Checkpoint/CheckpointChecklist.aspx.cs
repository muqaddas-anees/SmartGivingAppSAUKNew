using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.IO;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

public partial class CheckpointChecklist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sessionKeys.SID == 7)
        {
            CheckpointCustomerTab1.Visible = true;
            checkpoint_admin1.Visible = false;
        }
        else
        {
            CheckpointCustomerTab1.Visible = false;
            checkpoint_admin1.Visible = true;
        }
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Health Check";
                if (Request.QueryString["Project"] != null)
                {
                    int projectRef = int.Parse(Convert.ToString(Request.QueryString["Project"]));
                    lblTitle.InnerText = "Health Check - Project Reference " + projectRef;
                    BindTasks();
                    BindData(int.Parse(Request.QueryString["Project"].ToString()), int.Parse(ddlTasks.SelectedValue));
                  
                }
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindData(int projectRef,int taskId)
    {
        try
        {
            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                gvchecklist.DataSource = pt.CheckPointCheckListItems.Where(c => c.ProjectReference == projectRef && c.TaskID ==taskId).Select(c => c).ToList();
                gvchecklist.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvchecklist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Label lblstatus = (Label)gvchecklist.Rows[e.NewEditIndex].FindControl("lblstatus");
                Label lblDescription = (Label)gvchecklist.Rows[e.NewEditIndex].FindControl("lblDescription");
                Label lblNotes = (Label)gvchecklist.Rows[e.NewEditIndex].FindControl("lblNotes");
            
                gvchecklist.EditIndex = e.NewEditIndex; 
                gvchecklist.ShowFooter = false;
                BindData(int.Parse(Request.QueryString["Project"].ToString()),int.Parse(ddlTasks.SelectedValue));
              
                DropDownList ddlstatus = (DropDownList)gvchecklist.Rows[e.NewEditIndex].FindControl("ddlstatus");
                TextBox txtDescription = (TextBox)gvchecklist.Rows[e.NewEditIndex].FindControl("txtDescription");
                TextBox txtNotes = (TextBox)gvchecklist.Rows[e.NewEditIndex].FindControl("txtNotes");
                ddlstatus.SelectedValue = lblstatus.Text;  
            txtDescription.Text = lblDescription.Text;
            txtNotes.Text = lblNotes.Text;
          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
           
        }
    }
    protected void gvchecklist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvchecklist.EditIndex = -1;
            gvchecklist.ShowFooter = true;
            BindData(int.Parse(Request.QueryString["Project"].ToString()), int.Parse(ddlTasks.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
   
    protected void gvchecklist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvchecklist.PageIndex = e.NewPageIndex;
            BindData(int.Parse(Request.QueryString["Project"].ToString()), int.Parse(ddlTasks.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
    protected void gvchecklist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                int projectRef=int.Parse(Request.QueryString["Project"].ToString());
                string ID = gvchecklist.DataKeys[e.RowIndex].Value.ToString();
                int i = gvchecklist.EditIndex;
                GridViewRow Row = gvchecklist.Rows[i];
                DropDownList ddlstatus = (DropDownList)Row.FindControl("ddlstatus");
                TextBox txtDescription = (TextBox)Row.FindControl("txtDescription");
                TextBox txtNotes = (TextBox)Row.FindControl("txtNotes");
                Label lblTastID = (Label)Row.FindControl("lblTaskId");
                CheckPointCheckListItem cl = pt.CheckPointCheckListItems.Where(c => c.ID == int.Parse(ID)).FirstOrDefault();
                if (cl != null)
                {
                    cl.ItemDescription = txtDescription.Text;
                    cl.Status = ddlstatus.SelectedValue;
                    cl.Notes = txtNotes.Text;

                    if (ddlstatus.SelectedValue == "Closed")
                    {
                        cl.ClosedDate = DateTime.Now;
                        cl.ModifiedBy = sessionKeys.UID;
                    }
                    pt.SubmitChanges();

                }
                gvchecklist.EditIndex = -1;
                gvchecklist.ShowFooter = true;
                BindData(int.Parse(Request.QueryString["Project"].ToString()), int.Parse(ddlTasks.SelectedValue));
                bool status = true;
                var result = pt.CheckPointCheckListItems.Where(p => p.ProjectReference == projectRef && p.TaskID == int.Parse(lblTastID.Text)).ToList();
                if (result.Count > 0)
                {
                    foreach (var s in result)
                    {
                        if (s.Status != "Closed")
                        {
                            status = false;
                        }
                    }
                    if (status)
                    {
                        // Update project task item as 'Completed'
                            ProjectTaskItem projectTaskItem = pt.ProjectTaskItems.Where(p => p.ProjectReference == projectRef && p.ID == int.Parse(lblTastID.Text)).FirstOrDefault();
                            if (projectTaskItem != null)
                            {
                                projectTaskItem.ItemStatus = 3;
                                projectTaskItem.PercentComplete = "100";
                                pt.SubmitChanges();
                            }
                       
                    }
                }

               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindTasks()
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var listTasks = (from r in db.ProjectTaskItems
                                 join o in db.MasterTemplates on r.CheckPointID equals o.ID
                                 where r.ProjectReference == QueryStringValues.Project && r.isMilestone == true && r.QA.ToLower() == "y" && r.ItemStatus !=3
                                 orderby r.StartDate descending
                                 select new { r.ID, r.ItemDescription, r.StartDate, o.Description }).ToList();
                var inMemCollection = listTasks.AsEnumerable().Select(c => new
                {
                    ID = c.ID,
                    Name = c.ItemDescription + " - QA Due: " + string.Format(Deffinity.systemdefaults.GetStringDateformat(), c.StartDate.ToString().Remove(10)) + " - Associated QA List: " + c.Description
                }).ToList();

                if (listTasks.Count > 0)
                {
                    ddlTasks.DataSource = inMemCollection;
                    ddlTasks.DataTextField = "Name";
                    ddlTasks.DataValueField = "ID";
                    ddlTasks.DataBind();
                   
                    
                }
                else
                {
                    ddlTasks.Items.Insert(0, new ListItem("Please select...", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public bool CommentsVisible(string status)
    {
        bool visible = true;
        if (status != "Pending")
            visible = false;
        return visible;

    }
    protected void gvchecklist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                if (e.CommandName == "Insert")
                {
                    CheckPointCheckListItem cl = new CheckPointCheckListItem();
                    cl.ProjectReference = int.Parse(Request.QueryString["Project"].ToString());
                    cl.ItemDescription = ((TextBox)gvchecklist.FooterRow.FindControl("txtDescriptionFooter")).Text;
                    cl.Status = ((DropDownList)gvchecklist.FooterRow.FindControl("ddlstatusFooter")).SelectedValue;
                    cl.Notes = ((TextBox)gvchecklist.FooterRow.FindControl("txtNotesFooter")).Text;
                    cl.TaskID = Convert.ToInt32(ddlTasks.SelectedValue);
                    if (cl.Status == "Closed")
                    {
                        cl.ClosedDate = DateTime.Now;
                        cl.ModifiedBy = sessionKeys.UID;
                    }
                    pt.CheckPointCheckListItems.InsertOnSubmit(cl);
                    pt.SubmitChanges();
                    BindData(int.Parse(Request.QueryString["Project"].ToString()), int.Parse(ddlTasks.SelectedValue));
                }
               
            }
            if (e.CommandName == "Comments")
            {

                //hfItemId.Value = e.CommandArgument.ToString();
                //BindCommentsGrid(int.Parse(hfItemId.Value));
               
              
               


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvchecklist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton LinkButtonEdit = (ImageButton)e.Row.FindControl("LinkButtonEdit");
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                if (status == "Complete")
                {
                    LinkButtonEdit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(int.Parse(Request.QueryString["Project"].ToString()), int.Parse(ddlTasks.SelectedValue));
    }
    //public void BindCommentsGrid(int itemId )
    //{
    //    try
    //    {
    //        popupComment.Show();
    //        using (projectTaskDataContext pi = new projectTaskDataContext())
    //        {
    //            using (UserDataContext ud = new UserDataContext())
    //            {
    //                int projectRef = sessionKeys.Project;
    //                var x = ud.Contractors.Select(c => c).ToList();
    //                var y = pi.ChecklistCustomerComments.Where(p => p.ItemID == itemId).Select(p => p).ToList();

    //                var result = (from p in y
    //                              join c in x on p.SubmittedBy equals c.ID
    //                              where p.ItemID == itemId
    //                              select new { p.ID, p.Comments, p.CommentDate, c.ContractorName }).ToList();
    //                gvComments.DataSource = result;
    //                gvComments.DataBind();
                    
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }


    //}
    //protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        using (projectTaskDataContext db = new projectTaskDataContext())
    //        {
    //            int hfItemID = int.Parse(hfItemId.Value);
    //            ChecklistCustomerComment checklistCustomerComment = new ChecklistCustomerComment();
    //            checklistCustomerComment.CommentDate = DateTime.Now;
    //            checklistCustomerComment.Comments = txtComments.Text;
    //            checklistCustomerComment.ItemID = hfItemID;
    //            checklistCustomerComment.SubmittedBy = sessionKeys.UID;
    //            db.ChecklistCustomerComments.InsertOnSubmit(checklistCustomerComment);
    //            db.SubmitChanges();
    //            txtComments.Text = string.Empty;
    //            BindCommentsGrid(hfItemID);
                
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    //protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    //{
    //    popupComment.Hide();
    //}
}