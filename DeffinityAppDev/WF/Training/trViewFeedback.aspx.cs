using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_trViewFeedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = Request.QueryString["ID"];
        HtmlLink css = new HtmlLink();
        css.Href = ResolveClientUrl("~/App_Themes/Default/Ratings.css");
        css.Attributes["rel"] = "stylesheet";
        css.Attributes["type"] = "text/css";
        Page.Header.Controls.Add(css);

        RatingKnowledge.Attributes.Add("onclick", "return false");
        RatingLrnClimate.Attributes.Add("onclick", "return false");
        RatingObviousPrep.Attributes.Add("onclick", "return false");
        RatingPerformance.Attributes.Add("onclick", "return false");
        RatingResponsiveness.Attributes.Add("onclick", "return false");
        RatingStyle.Attributes.Add("onclick", "return false");

        lblTitle.InnerText = "View Feedback";
        if (!IsPostBack)
        {
            View_Feedback(int.Parse(ID));
        }
       
    }
    private void View_Feedback(int bookingID)
    {
        FeedbackEntity dep = FeedBack.viewFeedBack(bookingID);
        lblDate.Text = string.Format("{0:d}", dep.Date);
        
        lblDept.Text = dep.Department;
        lblFacilitatorsName.Text = dep.FacilitatorsName;
        lblJobTitle.Text = dep.JobTitle;
        lblLocation.Text = dep.Location;
        lblName.Text = dep.Name;
        lblPeComments1.Text = dep.peComments1;
        lblPeComments2.Text = dep.peComments2;
        lblPeComments3.Text = dep.peComments3;
        lblpeRatingComments.Text = dep.peRatingComments;
        lblProgrammTitle.Text = dep.programmeTitle;
        lblPtComments1.Text = dep.ptComments1;
        lblptComments2.Text = dep.ptComments2;
        lblAnyOtherComments.Text = dep.teOtherComments;
        radLength.SelectedValue = dep.teLength.ToString();
        radPacing.SelectedValue = dep.tePacing.ToString();

        RatingKnowledge.CurrentRating = dep.teKnowledgeSub;
        RatingLrnClimate.CurrentRating = dep.teLearningClimate;
        RatingObviousPrep.CurrentRating = dep.teObviousPrep;
        RatingPerformance.CurrentRating = dep.peRating;
        RatingResponsiveness.CurrentRating = dep.teResponsiveness;
        RatingStyle.CurrentRating = dep.teStyleDelivery;
    }
}
