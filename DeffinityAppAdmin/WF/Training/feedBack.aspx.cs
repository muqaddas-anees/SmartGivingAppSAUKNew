using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_trfeedBack : System.Web.UI.Page
{
    string BookingID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BookingID = Request.QueryString["ID"];
        //if (!IsPostBack)
        //{
            
        //}

       // string ID = Request.QueryString["ID"];
        trid.Value = BookingID;
        HtmlLink css = new HtmlLink();
        css.Href = ResolveClientUrl("~/App_Themes/Default/Ratings.css");
        css.Attributes["rel"] = "stylesheet";
        css.Attributes["type"] = "text/css";
        //css.Attributes["media"] = "all";
        Page.Header.Controls.Add(css);
        RatingKnowledge.Attributes.Add("onclick", "return false");
        RatingLrnClimate.Attributes.Add("onclick", "return false");
        RatingObviousPrep.Attributes.Add("onclick", "return false");
        RatingPerformance.Attributes.Add("onclick", "return false");
        RatingResponsiveness.Attributes.Add("onclick", "return false");
        RatingStyle.Attributes.Add("onclick", "return false");
        lblTitle.InnerText = "Feedback";
    }
    protected void TextBox14_TextChanged(object sender, EventArgs e)
    {

    }
   
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FeedbackEntity feedback = new FeedbackEntity();
            feedback.BookingID = int.Parse(trid.Value);
            feedback.Date = Convert.ToDateTime(txtDate.Text);
            feedback.Department = txtDepartment.Text;
            feedback.FacilitatorsName = txtFacilitators.Text;
            feedback.JobTitle = txtJobTitle.Text;
            feedback.Location = txtLocation.Text;
            feedback.Name = txtName.Text;
            feedback.peComments1 = txtMostUseful.Text;
            feedback.peComments2 = txtLeastUseful.Text;
            feedback.peComments3 = txtAddNeeds3.Text;
            feedback.peRating = RatingPerformance.CurrentRating;
            feedback.peRatingComments = txtRateComments.Text;
            feedback.programmeTitle = txtProgrammTitle.Text;
            feedback.ptComments1 = txtPractice.Text;
            feedback.ptComments2 = txtSupport.Text;
            feedback.teKnowledgeSub = RatingKnowledge.CurrentRating;
            feedback.teLearningClimate = RatingLrnClimate.CurrentRating;
            if (radLength.SelectedValue == "")
            {
                feedback.teLength = 0;
            }
            else
            {
                feedback.teLength = int.Parse(radLength.SelectedValue);
            }
            feedback.teObviousPrep = RatingObviousPrep.CurrentRating;
            feedback.teOtherComments = txtAnyOtherComments.Text;
            if (radPacing.SelectedValue == "")
            {
                feedback.tePacing = 0;
            }
            else
            {
                feedback.tePacing = int.Parse(radPacing.SelectedValue.ToString());
            }

           
            feedback.teResponsiveness = RatingResponsiveness.CurrentRating;
            feedback.teStyleDelivery = RatingStyle.CurrentRating;

            FeedBack.feedback_Insert(feedback);
            lblMsg.Text = "Feedback updated successfully";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void SendMail()
    {
        try
        {
            Email mail = new Email();
            BookingsEntity booking = Bookings.Bookings_GetEmployees(Convert.ToInt32(BookingID));
            List<NotificationEntity> ne = Notification.Notification_GetManagers(booking.DepartmentID);
            CourseEntity ce = Course.Course_Select(booking.CourseID);
            string Weburl = System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString();
            string style = mailStyles.MailCss();

            foreach (NotificationEntity Dmanager in ne)
            {

                string Url = "/Training/viewcoursefeedback.aspx?ID=" + booking.CFeedBackID.ToString();
                string body = string.Format(@"</head>
                                <body>
                                <table align='center' width='600' style='border:1px solid #8595a6; margin-top:10px;' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td height='30' valign='top' class='style1'><img src='{4}'  style='float:left'/>
                                    <table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'>
                                  <tr>
                                    <td class='hdr1'>Course feedback</td>
                                  </tr>
                                </table> 
                                   </td>
                                  </tr>
                                  <tr>
                                    <td height='9' class='style1' ><img src='{5}'  style='float:left'/></td>
                                  </tr>
                                <tr>
                                <td>
                                Dear <b>Training Manager</b>
                                </td>
                                </tr>
                                 <tr><td>&nbsp;</td></tr>
                            <tr><td>{0} has submitted feedback for course {1}  which was taken on {2}</td></tr>
                              <tr><td>&nbsp;</td></tr>
            <tr><td>Please <a href='{3}'>click here </a>for the completed feedback form.</td></tr>
               <tr><td>&nbsp;</td></tr>
               <tr><td>Thank You</td></tr>
                                </table>
                                </body>
                                </html>", booking.EmployeeName
                                       , ce.Title, string.Format("{0:d}", booking.DateofCourse), Weburl + Url,
                                       System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"],
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                string htmlBody = style + body;
                mail.SendingMail(Dmanager.Email, booking.EmployeeName + "has submitted feedback for course:  " + ce.Title, htmlBody);
            }
            foreach (NotificationEntity manager in ne)
            {
                string Url = "/Training/viewcoursefeedback.aspx?ID=" + booking.CFeedBackID.ToString();
                string body = string.Format(@"</head>
                                <body>
                                <table align='center' width='600' style='border:1px solid #8595a6; margin-top:10px;' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td height='30' valign='top' class='style1'><img src='{4}'  style='float:left'/>
                                    <table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'>
                                  <tr>
                                    <td class='hdr1'>Course feedback</td>
                                  </tr>
                                </table> 
                                   </td>
                                  </tr>
                                  <tr>
                                    <td height='9' class='style1' ><img src='{5}'  style='float:left'/></td>
                                  </tr>
                                <tr>
                                <td>
                                Dear <b>Training Manager</b>
                                </td>
                                </tr>
                                 <tr><td>&nbsp;</td></tr>
                            <tr><td>{0} has submitted feedback for course {1}  which was taken on {2}</td></tr>
                              <tr><td>&nbsp;</td></tr>
            <tr><td>Please <a href='{3}'>click here </a>for the completed feedback form.</td></tr>
               <tr><td>&nbsp;</td></tr>
               <tr><td>Thank You</td></tr>
                                </table>
                                </body>
                                </html>", booking.EmployeeName
                                       , ce.Title, string.Format("{0:d}", booking.DateofCourse), Weburl + Url,
                                       System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"],
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                string htmlBody = style + body;
                //http://nhs.deffinity.com/training/feedback.aspx?ID=
                mail.SendingMail(manager.ManagerEmail, booking.EmployeeName + "has submitted feedback for course:  " + ce.Title, htmlBody);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
}
