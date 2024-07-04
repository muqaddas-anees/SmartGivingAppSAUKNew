using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_feedBack : System.Web.UI.Page
{
    string BookingID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        BookingID = Request.QueryString["ID"];
        if (!IsPostBack)
        {
            BindCoures();
            BindActionPlan();
            DateTime date = DateTime.Now;
            txtDate.Text = string.Format("{0:d}", date);
        
        }

        lblMsg.Visible = false;
        lblTitle.InnerText = "Course feedback";
    }
    protected void TextBox14_TextChanged(object sender, EventArgs e)
    {

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
                
                string Url="/WF/Training/viewcoursefeedback.aspx?ID="+booking.CFeedBackID.ToString();
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
                                       , ce.Title, string.Format("{0:d}", booking.DateofCourse),Weburl+Url,
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
   
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CourseFeedBackEntity ce = new CourseFeedBackEntity();
            lblMsg.Visible = true;
            ce.Name = txtUserName.Text;
            ce.Objectives = txtObjectives.Text;
            ce.Objects = txtachieve.Text;
            ce.OrganisedBy = txtOrgnisedBy.Text;
            ce.CourseTitle = txtCourseTitle.Text;
            ce.JobTitle = txtJobTitle.Text;
            ce.LearningPoints = txtlrningPoints.Text;
            ce.Recommend = txtcolleagues.Text;
            ce.Useful = txtUseful.Text;
            ce.BookingID = int.Parse(Request.QueryString["ID"].ToString());
            ce.DatesOfAttendance = Convert.ToDateTime(string.IsNullOrEmpty(txtDate.Text) ? "1/1/1900" : txtDate.Text);
            CourseFeedBack.courseFeedBack_InsertUpdate(ce);
            BindActionPlan();
            SendMail();
            lblMsg.Text = "Feedback updated successfully";

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindCoures()
    {
        ddlCourseTitle.DataSource = Course.Course_ByOrderAsc();
        ddlCourseTitle.DataTextField = "Title";
        ddlCourseTitle.DataValueField = "ID";
        ddlCourseTitle.DataBind();
        ddlCourseTitle.Items.Insert(0, new ListItem("Please select...", "0"));
    }

    private void BindActionPlan()
    {
        int feedBackID=0;
        IEnumerable<CourseFeedBackEntity> ce = CourseFeedBack.CourseFeedback_ID(int.Parse(Request.QueryString["ID"].ToString()));
         foreach (CourseFeedBackEntity booking in ce)
         {
             feedBackID = booking.ID;
         }
        grActionPlan.DataSource = CourseFeedBack.ActionPlan_Selected(feedBackID);
        grActionPlan.DataBind();

    }

    protected void grActionPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddNew")
        {
            try
            {
                TextBox txtActionPaln = (TextBox)grActionPlan.FooterRow.FindControl("txtActionFooter");
                TextBox txtSupport = (TextBox)grActionPlan.FooterRow.FindControl("txtSupportFooter");
                TextBox txtTimeScale = (TextBox)grActionPlan.FooterRow.FindControl("txtTimeScaleFooter");

                CourseFeedBackEntity ce = new CourseFeedBackEntity();
                ActionPlanEntity ae = new ActionPlanEntity();
                ae.ActionPlan = txtActionPaln.Text;
                ae.SupportRequired = txtSupport.Text;
                ae.TimeScale = txtTimeScale.Text;
                ce.Name = txtUserName.Text;
                ce.Objectives = txtObjectives.Text;
                ce.Objects = txtachieve.Text;
                ce.OrganisedBy = txtOrgnisedBy.Text;
                ce.CourseTitle = txtCourseTitle.Text;//ddlCourseTitle.SelectedItem.ToString();
                ce.JobTitle = txtJobTitle.Text;
                ce.LearningPoints = txtlrningPoints.Text;
                ce.Recommend = txtcolleagues.Text;
                ce.Useful = txtUseful.Text;
                ce.BookingID = int.Parse(Request.QueryString["ID"].ToString());
                ae.BookingID = int.Parse(Request.QueryString["ID"].ToString());
                ce.DatesOfAttendance = Convert.ToDateTime(string.IsNullOrEmpty(txtDate.Text) ? "1/1/1900" : txtDate.Text);
                string message=validation();
                if (message == "")
                {
                    CourseFeedBack.courseFeedBack_InsertUpdate(ce);


                    CourseFeedBack.actionPlan_InsertUpdate(ae);
                    BindActionPlan();
                }
                else
                {
                    lblValidation.Text = message;
                    BindActionPlan();
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        if (e.CommandName == "Clear")
        {
            TextBox txtActionPaln = (TextBox)grActionPlan.FooterRow.FindControl("txtActionFooter");
            TextBox txtSupport = (TextBox)grActionPlan.FooterRow.FindControl("txtSupportFooter");
            TextBox txtTimeScale = (TextBox)grActionPlan.FooterRow.FindControl("txtTimeScaleFooter");

            txtActionPaln.Text = "";
            txtSupport.Text = "";
            txtTimeScale.Text = "";

        }
    }
    protected void grActionPlan_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            TextBox txtActionPaln = (TextBox)grActionPlan.Rows[e.RowIndex].FindControl("txtAction");
            TextBox txtSupport = (TextBox)grActionPlan.Rows[e.RowIndex].FindControl("txtSupport");
            TextBox txtTimeScale = (TextBox)grActionPlan.Rows[e.RowIndex].FindControl("txtTimeScale");
            ImageButton lnkButton = (ImageButton)grActionPlan.Rows[e.RowIndex].FindControl("LinkButtonUpdate");
            ActionPlanEntity ae = new ActionPlanEntity();
            ae.ID = int.Parse(lnkButton.CommandArgument);
            ae.ActionPlan = txtActionPaln.Text;
            ae.TimeScale = txtTimeScale.Text;
            ae.SupportRequired = txtSupport.Text;
            ae.BookingID = int.Parse(Request.QueryString["ID"].ToString());
           // ae.DatesOfAttendance = Convert.ToDateTime(string.IsNullOrEmpty(txtDate.Text) ? "1/1/1900" : txtDate.Text);
            string message=validation();
            if (message == "")
            {
                CourseFeedBack.actionPlan_InsertUpdate(ae);
                grActionPlan.EditIndex = -1;
                BindActionPlan();
            }
            else
            {
                lblValidation.Text = message;
                grActionPlan.EditIndex = -1;
                BindActionPlan();
                txtUserName.Focus();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }
    protected void grActionPlan_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grActionPlan.EditIndex = e.NewEditIndex;
        BindActionPlan();
    }
    protected void grActionPlan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grActionPlan.EditIndex = -1;
        BindActionPlan();
    }

    private string validation()
    {
        string message = "";
        if (txtUserName.Text == "")
        {
            message += "Please enter name</br>";
        }
         if (txtJobTitle.Text == "")
        {
            message += "Please enter job title</br>";
        }
         if (txtCourseTitle.Text == "")
        {
            message += "Please enter course title</br>";
        }
        if (txtDate.Text == "") 
        {
            message += "Please enter date</br>";
        }
        if (txtOrgnisedBy.Text == "")
        {
            message += "Please courses organised by</br>";
        }
        return message;
    }
}
