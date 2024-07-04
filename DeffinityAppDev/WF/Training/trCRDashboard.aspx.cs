using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
public partial class Training_trCRDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          // lblMessage.Visible = false;
            BindDepartment();
            BindCourse();
            DashBoard(int.Parse(ddlDepartment.SelectedValue),int.Parse(ddlCourse.SelectedValue),
                Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "1/1/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "1/1/1900" : txtToDate.Text));
        }

        lblTitle.InnerText = "Dashboard";
        //Master.PageHead="Training Manager";

    }

    #region Data Bind


    private void BindDepartment()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();


    }
    private void BindCourse()
    {
        ddlCourse.DataSource = Course.Course_ByOrderAsc();// Course.Course_SelectAll();
        ddlCourse.DataValueField = "ID";
        ddlCourse.DataTextField = "Title";
        ddlCourse.DataBind();
        //ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));
    }



    #endregion



    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        if (txtFromDate.Text != "" && txtToDate.Text == "")
        {
            lblMessage.Visible = true;
            ltlCourseReOccurrence.Visible = false;
            lblMessage.Text = "Please enter to date";
        }
        else if (txtFromDate.Text == "" && txtToDate.Text != "")
        {
            ltlCourseReOccurrence.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "Please enter from date";
        }
        else
        {
            lblMessage.Visible = false;
            DashBoard(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlCourse.SelectedValue),
                           Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "1/1/1900" : txtFromDate.Text),
                           Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "1/1/1900" : txtToDate.Text));
        }
    }

    private void DashBoard(int deptID,int courseID,DateTime fromDate,DateTime toDate)
    {
        ltlCourseReOccurrence.Visible = true;
        string Details = string.Empty;
        string text = string.Empty;
        int rowspan = 2;
        IEnumerable<CourseReOccurrence> employee = CourseRe_Occurr.CourseReOccurr_SelectByFilter(deptID, courseID, fromDate, toDate);
        if (employee.Count()!= 0)
        {
            text += "<table bordercolor='black' border='1' cellpadding='1' cellspacing='0'" +
            "style='color:black;font-size:10px;font-weight:bold'><tr><td style='width:50px'><b>Operator</b></td>" +
            "<td style='width:50px'><b>Status</b></td><td style='width:50px'><b>Initial 3 successful validation</b></td>" +
            "<td style='width:50px'><b>Date last validated</b></td>";
            foreach (CourseReOccurrence s in employee)
            {
                rowspan++;
            }

            text += "<td rowspan='" + rowspan + "' style='background-color:silver'></td>";
            //right header columns
            // text += "<td><table border='1' cellpadding='1' cellspacing='0'><tr><td colspan='12'>Date Validation Next Due<td></tr><tr>";
            text += "<td style='width:50px' align='center'><b>" + "January " + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "February " + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "March" + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "April" + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "May " + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "June " + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "July " + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "August" + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "September" + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "October " + "</b></td>";
            text += "<td style='width:50px' align='center'><b>" + "November" + "</b></td>";
            text += "<td style='width:50px align='center''><b>" + "December " + "</b></td>";
            text += "</tr>";
            //text += "</tr></table></td>";

            foreach (CourseReOccurrence s in employee)
            {
                int bookingID = int.Parse(s.BookingID.ToString());
                string status = CourseRe_Occurr.CourseReOccurr_SelectStatusName(s.BookingID,s.CourseID);
                IEnumerable<CourseReOccurrence> Dates = CourseRe_Occurr.CourseReOccurr_SelectSuccessfulDates(bookingID,s.CourseID);
                text += "<tr><td style='width:50px'><b>" + s.EmployeeName + "</b></td>";
                text += "<td style='width:50px'><b>" + status + "</b> </td>";
                text += "<td style='width:50px'>";
                foreach (CourseReOccurrence date in Dates)
                {
                    text += "<table border='0' cellpadding='1' cellspacing='0'><tr><td style='width:50px'>" +string.Format("{0:d}", date.BookingDate) + "<td></tr></table>";
                }
                text += "</td>";
               // text += "<td style='width:50px'><b>&nbsp;</b></td>";




                for (int i = 1; i <= 13; i++)
                {
                    string date = "";
                    if (CourseRe_Occurr.CourseReOccurr_SelectMonth(s.BookingID, i) == Convert.ToDateTime("1/1/1900"))
                    {
                        date = "";
                    }
                    else
                    {
                        date = string.Format("{0:d}", CourseRe_Occurr.CourseReOccurr_SelectMonth(s.BookingID, i));
                    }

                    if (date != "")
                    {
                        CourseReOccurrence details = CourseRe_Occurr.CourseReOccurr_SelectDetails(s.BookingID,i);
                        Details += details.EmployeeName.Replace(" ", "_") + "$";
                        Details += details.CourseName.Replace(" ", "_") + "$";
                        Details += string.Format("{0:d}", details.BookingDate) + "$";
                        Details += details.StatusName.Replace(" ", "_") + "$";
                        text += "<td style='width:50px' onmouseover=javascript:employeeDetails('" + Details + "'); class='showTip L2' style='background-color:" + CourseRe_Occurr.CourseReOccurr_SelectStatusColor(s.BookingID, i) + "'><b>" + date + "</b></td>";
                        Details = string.Empty;
                    }
                    else
                    {
                        text += "<td>&nbsp;</td>";
                    }
                }


                text += "</tr>";
            }


            text += "</table>";
        }
        else
        {
            text += "<b>No records found<b>";
        }
        ltlCourseReOccurrence.Text = text;
    }
}
