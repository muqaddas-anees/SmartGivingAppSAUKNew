using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_viewcoursefeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BookingID = Request.QueryString["ID"];
        lblTitle.InnerText = "View course feedback";
        try
        {

            CourseFeedBackEntity courseFeedback = CourseFeedBack.CourseFeedback_Details(int.Parse(BookingID));
            lblName.Text = courseFeedback.Name;
            lblCourse.Text = courseFeedback.CourseTitle;
            lblDate.Text = string.Format("{0:d}", courseFeedback.DatesOfAttendance);
            lblJobTitle.Text = courseFeedback.JobTitle;
            lblLearningPoints.Text = courseFeedback.LearningPoints;
            lblObjectives.Text = courseFeedback.Objectives;
            lblObjects.Text = courseFeedback.Objects;
            lblOrganisedBy.Text = courseFeedback.OrganisedBy;
            lblRecommend.Text = courseFeedback.Recommend;
            lblUseful.Text = courseFeedback.Useful;
            BindActionPalnGrid(courseFeedback.ID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindActionPalnGrid(int feedBackID)
    {
        grActionPlan.DataSource = CourseFeedBack.ActionPlan_Selected(feedBackID);
        grActionPlan.DataBind();
    }
}
