using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Training.BAL;
using UserMgt.Entity;
using Deffinity.TrainingManager;
using Deffinity.TrainingEntity;

public partial class Training_controls_ManageUserSkillsCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindUser();
                if (Request.QueryString["uid"] != null)
                {
                    ddlUser.SelectedValue = Request.QueryString["uid"].ToString();
                    pnlUser.Visible = false;
                }

                BindUserSkillsGrid();
                BindTrainingBookingGrid();
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);   
        }
        
    }
    private void BindUser()
    {
        ddlUser.DataSource = TrainingManagerBAL.GetUserList();
        ddlUser.DataValueField = "ID";
        ddlUser.DataTextField = "ContractorName";
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));

    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindUserSkillsGrid();
            BindTrainingBookingGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region User Skills

    private void BindUserSkillsGrid()
    {
        int userId = Convert.ToInt32(ddlUser.SelectedValue);
        var userSkills = TrainingManagerBAL.BindUserSkills(userId).ToList();
        userSkills.Add(new UserSkill { Id = -99, Skills = "" });
        gvUserSkills.DataSource = userSkills;
        gvUserSkills.DataBind();
    }

    protected void gvUserSkills_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvUserSkills.EditIndex = e.NewEditIndex;
            BindUserSkillsGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gvUserSkills_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        gvUserSkills.EditIndex = -1;
        BindUserSkillsGrid();
    }

    protected void gvUserSkills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "InsertSkills")
            {
                string skills = ((TextBox)gvUserSkills.FooterRow.FindControl("txtfSkills")).Text;
                string skillLevel = ((DropDownList)gvUserSkills.FooterRow.FindControl("ddlfSkillLevel")).SelectedItem.Text;
                string notes = ((TextBox)gvUserSkills.FooterRow.FindControl("txtfNotes")).Text;

                UserSkill userSkill = new UserSkill();
                userSkill.UserId = Convert.ToInt32(ddlUser.SelectedValue);
                userSkill.Skills = skills;
                userSkill.SkillLevel = skillLevel;
                userSkill.Notes = notes;
                TrainingManagerBAL.InsertUserSkills(userSkill);
                BindUserSkillsGrid();
            }
            if (e.CommandName == "Update")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                int i = gvUserSkills.EditIndex;
                GridViewRow row = gvUserSkills.Rows[i];
                string skills = ((TextBox)row.FindControl("txtSkills")).Text;
                string skillLevel = ((DropDownList)row.FindControl("ddlSkillLevel")).SelectedItem.Text;
                string notes = ((TextBox)row.FindControl("txtNotes")).Text;


                UserSkill userSkill = new UserSkill();
                userSkill.Id = id;
                userSkill.Skills = skills;
                userSkill.SkillLevel = skillLevel;
                userSkill.Notes = notes;
                TrainingManagerBAL.UpdateUserSkills(userSkill);

            }
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                TrainingManagerBAL.DeleteUserSkills(id);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gvUserSkills_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            gvUserSkills.EditIndex = -1;
            BindUserSkillsGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

   
    protected void gvUserSkills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(((Label)e.Row.FindControl("lblID")).Text);
                if (id == -99)
                {
                    e.Row.Visible = false;
                }
                DropDownList ddlSkillLevel = (DropDownList)e.Row.FindControl("ddlSkillLevel");
                if (ddlSkillLevel != null)
                {
                    HiddenField hfSkillLevel = (HiddenField)e.Row.FindControl("hfSkillLevel");
                    string skillId;
                    GetSkillLevelId(hfSkillLevel.Value, out skillId);
                    ddlSkillLevel.SelectedValue = skillId;
                }
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void gvUserSkills_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            gvUserSkills.EditIndex = -1;
            BindUserSkillsGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void GetSkillLevelId(string skillType, out string  skillId)
    {
        skillId = "1";
        if (skillType == "Basics")
            skillId = "1";
        else if (skillType == "Intermediate")
            skillId = "2";
        else if (skillType == "Advanced")
            skillId = "3";
    }

    #endregion

    #region "Training Booking record"


    private void BindTrainingBookingGrid()
    {
        var bookingList = TrainingManagerBAL.GetTrainingBookingList(Convert.ToInt32(ddlUser.SelectedValue)).ToList();
        bookingList.Add(new TrainingBookingEntity { ID = -99, Course = "", Category = "" });
        gvTrainingBooking.DataSource = bookingList;
        gvTrainingBooking.DataBind();
    }

    protected void gvTrainingBooking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
           if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TrainingBookingEntity de = (TrainingBookingEntity)e.Row.DataItem;

                if (de.ID == -99)
                {
                    e.Row.Visible = false;
                }
                //de.ID=-99
                //    e.Row.Visible=false;
                //e.Row.Attributes.Add("onclick", "this.style.backgroundColor='BlanchedAlmond'");


                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                DropDownList ddlCourse = (DropDownList)e.Row.FindControl("ddlCourse");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                try
                {


                    BindCategory(ddlCategory, Convert.ToInt32(de.CategoryID));
                    BindCourse(ddlCourse, Convert.ToInt32(de.CategoryID), Convert.ToInt32(de.CourseID));

                    int[] ids = new int[] { 5, 6, 7 };
                    ddlStatus.DataSource = Status.SelectAll(true).Where(s=> ids.Contains(Convert.ToInt32(s.Value))).ToList();
                    ddlStatus.DataValueField = "value";
                    ddlStatus.DataTextField = "text";
                    ddlStatus.DataBind();

                    ddlStatus.SelectedValue = de.StatusID.ToString();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }




            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                try
                {

                    TextBox txtDateFooter = (TextBox)e.Row.FindControl("txtDateFooter");
                    txtDateFooter.Text = DateTime.Now.ToShortDateString();
                    DropDownList ddlCategory_footer = (DropDownList)e.Row.FindControl("ddlCategory_footer");
                    DropDownList ddlEmployee_footer = (DropDownList)e.Row.FindControl("ddlEmployee_footer");
                    DropDownList ddlCourse_footer = (DropDownList)e.Row.FindControl("ddlCourse_footer");
                    DropDownList ddlStatus_footer = (DropDownList)e.Row.FindControl("ddlStatus_footer");
                    BindCourse(ddlCourse_footer, 0, 0);
                    BindCategory(ddlCategory_footer, 0);


                    int[] ids = new int[] { 5, 6, 7 };
                    ddlStatus_footer.DataSource = Status.SelectAll(true).Where(s => ids.Contains(Convert.ToInt32(s.Value))).ToList();
                    ddlStatus_footer.DataValueField = "value";
                    ddlStatus_footer.DataTextField = "text";
                    ddlStatus_footer.DataBind();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
           
        }
       
    }

    protected void ddlCategory_footer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlCategory_footer = (DropDownList)gvTrainingBooking.FooterRow.FindControl("ddlCategory_footer");
            DropDownList ddlCourse_footer = (DropDownList)gvTrainingBooking.FooterRow.FindControl("ddlCourse_footer");
            int id = Convert.ToInt32(ddlCategory_footer.SelectedValue);
            IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(id);
            ddlCourse_footer.DataSource = CE;
            ddlCourse_footer.DataValueField = "ID";
            ddlCourse_footer.DataTextField = "Title";
            ddlCourse_footer.DataBind();
            ddlCourse_footer.Items.Insert(0, new ListItem("Please select...", "0"));

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int index = gvTrainingBooking.EditIndex;
            GridViewRow row = gvTrainingBooking.Rows[index];

            DropDownList ddlCategory = (DropDownList)row.FindControl("ddlCategory");
            DropDownList ddlCourse = (DropDownList)row.FindControl("ddlCourse");
            int id = Convert.ToInt32(ddlCategory.SelectedValue);
            IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(id);
            ddlCourse.DataSource = CE;
            ddlCourse.DataValueField = "ID";
            ddlCourse.DataTextField = "Title";
            ddlCourse.DataBind();

            //ddlCourse.SelectedValue = booking.CourseID.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindCourse(DropDownList ddlCourse, int categoryid, int setValue)
    {
        try
        {
            ddlCourse.Items.Clear();
            IEnumerable<Deffinity.TrainingEntity.CourseEntity> CE = Course.Course_SelectByCategory(categoryid);
            ddlCourse.DataSource = CE;
            ddlCourse.DataValueField = "ID";
            ddlCourse.DataTextField = "Title";
            ddlCourse.DataBind();

            ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlCourse.SelectedValue = setValue.ToString();
        }


        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindCategory(DropDownList ddlCategory, int setValue)
    {
        try
        {
            ddlCategory.DataSource = Category.Category_OrderByAsc();// Category.Category_SelectAll();
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));
            ddlCategory.SelectedValue = setValue.ToString();
        }

        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gvTrainingBooking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            DropDownList ddlCategory_footer = (DropDownList)gvTrainingBooking.FooterRow.FindControl("ddlCategory_footer");
            DropDownList ddlCourse_footer = (DropDownList)gvTrainingBooking.FooterRow.FindControl("ddlCourse_footer");
            DropDownList ddlStatus_footer = (DropDownList)gvTrainingBooking.FooterRow.FindControl("ddlStatus_footer");
            TextBox txtDate = (TextBox)gvTrainingBooking.FooterRow.FindControl("txtDateFooter");
            TextBox txtCompletedDate = (TextBox)gvTrainingBooking.FooterRow.FindControl("txtfCompletedDate");
            if (e.CommandName == "AddNew")
            {

                Training.Entity.Booking booking = new Training.Entity.Booking();
                CourseEntity course = Course.Course_Select(int.Parse(ddlCourse_footer.SelectedValue));
                booking.BookingDate = Convert.ToDateTime(txtDate.Text);
                booking.CategoryID = int.Parse(ddlCategory_footer.SelectedValue);
                booking.CheckedBy = 0;
                booking.CheckedDate = Convert.ToDateTime(txtDate.Text);
                booking.Comments = "";
                booking.CostofCourse = course.Rate;
                booking.CourseID = int.Parse(ddlCourse_footer.SelectedValue);
                booking.CourseVenue = "";
                booking.DateofCourse = Convert.ToDateTime(txtDate.Text);
                booking.DepartmentID = 0;
                booking.DurationInDays = 0;
                booking.Employee = int.Parse(ddlUser.SelectedValue);
                booking.StatusID = int.Parse(ddlStatus_footer.SelectedValue);
                if (!string.IsNullOrEmpty(txtCompletedDate.Text))
                {
                    booking.EndDate = Convert.ToDateTime(txtCompletedDate.Text);
                    // If complete date exists set booking status to complete
                    booking.StatusID = 6;
                }
                booking.EndTime = "";
                booking.Expenses = 0.00;
                booking.FeedBack = "";
                booking.FileID = 0;
                booking.Instructor = "";
                booking.NotifyDaysPrior = 10;
                booking.NotifyUser = 0;
                booking.Outcome = 0;
                booking.Penalties = 0.00;
                booking.RequiredByDate = Convert.ToDateTime(txtDate.Text);
                booking.StartTime = "";
              
                booking.Budget = course.Rate;
                booking.CourseReoccurs = 0;
                booking.ReFrequencey = 0;
                booking.Day = "";
                booking.UntilDate = Convert.ToDateTime(txtDate.Text);
                TrainingManagerBAL.InsertTrainingBooking(booking);
                BindTrainingBookingGrid();
            }
            if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                TrainingManagerBAL.DeleteTrainingBooking(id);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void gvTrainingBooking_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            DropDownList ddlCategory = (DropDownList)gvTrainingBooking.Rows[e.RowIndex].FindControl("ddlCategory");
            DropDownList ddlCourse = (DropDownList)gvTrainingBooking.Rows[e.RowIndex].FindControl("ddlCourse");
            DropDownList ddlStatus = (DropDownList)gvTrainingBooking.Rows[e.RowIndex].FindControl("ddlStatus");
            TextBox txtDate = (TextBox)gvTrainingBooking.Rows[e.RowIndex].FindControl("txtDate");
            TextBox txtCompletedDate = (TextBox)gvTrainingBooking.Rows[e.RowIndex].FindControl("txtCompletedDate");
            ImageButton LinkButtonUpdate = (ImageButton)gvTrainingBooking.Rows[e.RowIndex].FindControl("LinkButtonUpdate");

            Training.Entity.Booking booking = new Training.Entity.Booking();
            booking.ID = Convert.ToInt32(LinkButtonUpdate.CommandArgument);
            booking.CategoryID = int.Parse(ddlCategory.SelectedValue);
            booking.CourseID = int.Parse(ddlCourse.SelectedValue);
            booking.StatusID = int.Parse(ddlStatus.SelectedValue);
            if (!string.IsNullOrEmpty(txtCompletedDate.Text))
            {
                // If complete date exists set booking status to complete
                booking.EndDate = Convert.ToDateTime(txtCompletedDate.Text);
                booking.StatusID = 6;
            }
            booking.DateofCourse = Convert.ToDateTime(txtDate.Text);
           
            TrainingManagerBAL.UpdateTrainingBooking(booking);
            gvTrainingBooking.EditIndex = -1;
            BindTrainingBookingGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    protected void gvTrainingBooking_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTrainingBooking.EditIndex = e.NewEditIndex;
        BindTrainingBookingGrid();
    }

    protected void gvTrainingBooking_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTrainingBooking.EditIndex = -1;
        BindTrainingBookingGrid();
    }

    protected void gvTrainingBooking_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BindTrainingBookingGrid();
    }

    protected void gvTrainingBooking_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTrainingBooking.PageIndex = e.NewPageIndex;
        BindTrainingBookingGrid();
    }

    #endregion

}