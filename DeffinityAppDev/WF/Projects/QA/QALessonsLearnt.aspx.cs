using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Deffinity.Bindings;
using System.Linq;
using Deffinity.LessonsLearntEntitys;
using Deffinity.LessonsLearntManagers;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

public partial class QALessonsLearnt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Master.PageHead = "QA";
            try
            {
                
                dropdownBindings();
                BindGrid();
                //select a project data
                //if (QueryStringValues.Project != 0)
                //    selectLessons(QueryStringValues.Project);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }

    protected void BindGrid()
    {
        using (projectTaskDataContext pt = new projectTaskDataContext())
        {

            GVLessonLearnt.DataSource = pt.LessonLearntSelectByProjectRef(QueryStringValues.Project);
            GVLessonLearnt.DataBind();

           
        }
       
    }
    #region bindigs
    protected void dropdownBindings()
    {
                
        //only active use should bind
        ddlAssignedTo.DataSource = DefaultDatabind.UserSelectAll(true);
        ddlAssignedTo.DataTextField = Constants.ddlContractorTextField;
        ddlAssignedTo.DataValueField = Constants.ddlValField;
        ddlAssignedTo.DataBind();
        

        //only active use should bind
        ddlIdentified.DataSource = DefaultDatabind.UserSelectAll(true);
        ddlIdentified.DataTextField = Constants.ddlContractorTextField;
        ddlIdentified.DataValueField = Constants.ddlValField;
        ddlIdentified.DataBind();

        //bind staus
        ddlStatus.DataSource = DefaultDatabind.b_ItemStatus();
        ddlStatus.DataTextField = Constants.ddlItemStatusTextField;
        ddlStatus.DataValueField = Constants.ddlValField;
        ddlStatus.DataBind();
        //add default
        ddlAssignedTo.Items.Insert(0,Constants.ddlDefaultBind(true));        
        ddlIdentified.Items.Insert(0,Constants.ddlDefaultBind(true));
        ddlStatus.Items.Insert(0, Constants.ddlDefaultBind(true));  

    }
    private void selectLessons(int proejctReference)
    {
        LessonsLearntEntity ll = LessonsLearntManager.LessonsLearntSelect(proejctReference);
        txtBusinessImapact.Text = ll.BusinessImpact;
        txtLessons.Text = ll.Description;
        txtRemedationActions.Text = ll.RemediationActions;
        ddlAssignedTo.SelectedValue = ll.AssignedTo.ToString();
        ddlIdentified.SelectedValue = ll.IdentifiedBy.ToString();
        ddlStatus.SelectedIndex = ll.Status;

    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //clear fields
        txtBusinessImapact.Text = string.Empty;
        txtLessons.Text = string.Empty;
        txtRemedationActions.Text = string.Empty;
        ddlAssignedTo.SelectedIndex = 0;
        ddlIdentified.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                LessonsLearnt lL = pt.LessonsLearnts.Where(l =>l.ProjectReference == QueryStringValues.Project && l.AssignedTo == int.Parse(ddlAssignedTo.SelectedValue) && l.IdentifiedBy == int.Parse(ddlIdentified.SelectedValue) && l.Status == int.Parse(ddlStatus.SelectedValue) && l.Description == txtLessons.Text ).FirstOrDefault();

                if (lL == null)
                {
                    lblErrorMsg.Visible = false;

                    LessonsLearnt lessonsLearnt = new LessonsLearnt();
                    lessonsLearnt.AssignedTo = int.Parse(ddlAssignedTo.SelectedValue);
                    lessonsLearnt.IdentifiedBy = int.Parse(ddlIdentified.SelectedValue);
                    lessonsLearnt.Status = int.Parse(ddlStatus.SelectedValue);
                    lessonsLearnt.Description = txtLessons.Text;
                    lessonsLearnt.BusinessImpact = txtBusinessImapact.Text;
                    lessonsLearnt.ProjectReference = QueryStringValues.Project;
                    lessonsLearnt.RemediationActions = txtRemedationActions.Text;
                    pt.LessonsLearnts.InsertOnSubmit(lessonsLearnt);
                    pt.SubmitChanges();
                }
                else
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = "Item already exists. Please check and try again.";
                }
            }


            //LessonsLearntEntity ll = new LessonsLearntEntity();

            //ll.AssignedTo = int.Parse(ddlAssignedTo.SelectedValue);
            //ll.BusinessImpact = txtBusinessImapact.Text.Trim();
            //ll.Description = txtLessons.Text.Trim();
            //ll.IdentifiedBy = int.Parse(ddlIdentified.SelectedValue);
            //ll.ProjectReference = QueryStringValues.Project;
            //ll.RemediationActions = txtRemedationActions.Text.Trim();
            //ll.Status = int.Parse(ddlStatus.SelectedValue);
            ////if(txtLessons.Text.Trim() !=string)

            //LessonsLearntManager.LearnInsert(ll);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

        BindGrid();

    }
    protected void GVLessonLearnt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        using (projectTaskDataContext pt = new projectTaskDataContext())
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblId = e.Row.FindControl("lblID") as Label;
                LessonsLearnt lessonsLearnt = pt.LessonsLearnts.Where(l => l.ID == int.Parse(lblId.Text)).FirstOrDefault();

                if (e.Row.FindControl("ddlEIdentifiedBy") != null)
                {
                    DropDownList ddlEIdentifiedBy = e.Row.FindControl("ddlEIdentifiedBy") as DropDownList;
                    ddlEIdentifiedBy.DataSource = DefaultDatabind.UserSelectAll(true);
                    ddlEIdentifiedBy.DataValueField = Constants.ddlValField;
                    ddlEIdentifiedBy.DataTextField = Constants.ddlContractorTextField;
                    ddlEIdentifiedBy.SelectedValue = lessonsLearnt.IdentifiedBy.ToString();
                    ddlEIdentifiedBy.DataBind();

                }
                if (e.Row.FindControl("ddlEAssignedTo") != null)
                {
                    DropDownList ddlEAssignedTo = e.Row.FindControl("ddlEAssignedTo") as DropDownList;
                    ddlEAssignedTo.DataSource = DefaultDatabind.UserSelectAll(true);
                    ddlEAssignedTo.DataTextField = Constants.ddlContractorTextField;
                    ddlEAssignedTo.DataValueField = Constants.ddlValField;
                    ddlEAssignedTo.SelectedValue = lessonsLearnt.AssignedTo.ToString();
                    ddlEAssignedTo.DataBind();
                }
                if (e.Row.FindControl("ddlEStatus") != null)
                {
                    DropDownList ddlEStatus = e.Row.FindControl("ddlEStatus") as DropDownList;
                    ddlEStatus.DataSource = DefaultDatabind.b_ItemStatus();
                    ddlEStatus.DataTextField = Constants.ddlItemStatusTextField;
                    ddlEStatus.DataValueField = Constants.ddlValField;
                    ddlEStatus.SelectedValue = lessonsLearnt.Status.ToString();
                    ddlEStatus.DataBind();
                }

            }
        }
    }
    protected void GVLessonLearnt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {

        }
        if (e.CommandName == "Update")
        {
             int id = int.Parse(e.CommandArgument.ToString());

             using (projectTaskDataContext pt = new projectTaskDataContext())
             {
                 int i = GVLessonLearnt.EditIndex;
                 GridViewRow Row = GVLessonLearnt.Rows[i];

                 string description = ((TextBox)Row.FindControl("txtEDesciption")).Text;
                 string identifiedBy = ((DropDownList)Row.FindControl("ddlEIdentifiedBy")).SelectedValue;
                 string assignedTo = ((DropDownList)Row.FindControl("ddlEAssignedTo")).SelectedValue;
                 string status = ((DropDownList)Row.FindControl("ddlEStatus")).SelectedValue;
                 string remediationActions = ((TextBox)Row.FindControl("txtERemediationActions")).Text;
                 string businessImpact = ((TextBox)Row.FindControl("txtEBusinessImpact")).Text;

                 LessonsLearnt lL = pt.LessonsLearnts.Where(l => l.ProjectReference == QueryStringValues.Project && l.AssignedTo == int.Parse(assignedTo) && l.IdentifiedBy == int.Parse(identifiedBy) && l.Status == int.Parse(status) && l.Description == description).FirstOrDefault();
                 if (lL == null)
                 {
                     Label1.Visible = false;
                     LessonsLearnt lessonLearnt = pt.LessonsLearnts.Where(l => l.ID == id).FirstOrDefault();
                     lessonLearnt.IdentifiedBy = int.Parse(identifiedBy);
                     lessonLearnt.AssignedTo = int.Parse(assignedTo);
                     lessonLearnt.Status = int.Parse(status);
                     lessonLearnt.RemediationActions = remediationActions;
                     lessonLearnt.BusinessImpact = businessImpact;
                     lessonLearnt.Description = description;
                     pt.SubmitChanges();
                 }
                 else
                 {
                     Label1.Visible = true;
                     Label1.Text = "Item already exists. Please check and try again.";
                 }
             }
             
        }
        if (e.CommandName == "delete1")
        {
            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                int id = int.Parse(e.CommandArgument.ToString());

                LessonsLearnt lessonsLearnt = pt.LessonsLearnts.Where(l => l.ID == id).FirstOrDefault();
                if (lessonsLearnt != null)
                {
                    pt.LessonsLearnts.DeleteOnSubmit(lessonsLearnt);
                    pt.SubmitChanges();
                }
            }
            BindGrid();
        }
        
    }
    protected void GVLessonLearnt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVLessonLearnt.EditIndex = -1;
        BindGrid();
    }
    protected void GVLessonLearnt_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVLessonLearnt.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void GVLessonLearnt_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GVLessonLearnt.EditIndex = -1;
        BindGrid();
    }
}
