using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
public partial class Training_trGapAnalysis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Master.PageHead = "Training management";
          //  lblTitle.InnerText = "Dashboard";

            BindDepartments();
            BindArea();
            GapAnalysisDesign(int.Parse(ddlDepartment.SelectedValue),
                int.Parse(string.IsNullOrEmpty(ddlArea.SelectedValue) ? "0" : ddlArea.SelectedValue));
        }
    }

   #region Bind Data
    public void BindDepartments()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField= "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
    }
    public void BindArea()
    {
        ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));// Area.Area_SelectAll();
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();
       
    }
    #endregion


    #region Gap Analysis
    public void GapAnalysisDesign(int DeptId,int areaID)
    {
        string text = string.Empty;
        string Details = string.Empty;
        IEnumerable<EmployeeEntity> ss = areaID>0?Bookings.Bookings_SelectEmployee(DeptId, areaID):Bookings.Bookings_SelectEmployee(DeptId);
        IEnumerable<EmployeeEntity> ss3 = Bookings.Bookings_SelectEmployee1(DeptId, areaID);
        IEnumerable<CourseEntity> ss1 = areaID>0?Bookings.Bookings_SelectCourse(DeptId, areaID):Bookings.Bookings_SelectCourse(DeptId);
        IEnumerable<StatusEntity> ss2 = Bookings.Booking_GetCount();
        if (ss3.Count() != 0 && ss1.Count() != 0)
        {
            text += "<table width='900px' id='mainTable' class='scrolltable' bordercolor='black' border='1' cellpadding='1' cellspacing='0' style='color:black;font-size:10px;font-weight:bold'><tr><th style='width:25%;align:left'><b>Name</b></th>";
            int rowspan = 2;

            foreach (CourseEntity se in ss1)
            {
                rowspan++;
            }

            //foreach (CourseEntity se in ss1)
            //{
                foreach (EmployeeEntity empl in ss3)
                {
                    text += "<td style='width:10px'><b>" + empl.Name + "</b></td>";
                }
            //}
           
            text += "<th rowspan='" + rowspan + "' style='background-color:silver;width:5px'></th>";
            //right header columns
            text += "<td style='width:50px'><b>" + "No.of Trained Support Staff" + "</b></td>";
            text += "<td style='width:50px'><b>" + "No.of people Trained" + "</b></td>";
            text += "<td style='width:50px'><b>" + "No.of people in Training" + "</b></td>";
            text += "<td style='width:50px'><b>" + "No.of People not Trained" + "</b></td>";
            text += "<td style='width:50px'><b>" + "Target " + "</b></td>";

           
            text += "</tr>";
            text += "<tr><th style='width:25%'>Classification</th>";
            foreach (EmployeeEntity empl in ss3)
            {
                text += "<td style='width:10px'><b>" + empl.ClassificationName+ "</b></td>";
            }
            text += "<td colspan='5' style='background-color:silver'></td>";
           
            text += "</tr>";
            foreach (CourseEntity s in ss1)
            {
                text += "<tr><th style='width:25%' > <b>" + s.Title + "</b></th>";
                IEnumerable<EmployeeEntity> ss12 = areaID > 0 ? Bookings.Bookings_SelectEmployee12(DeptId, areaID,s.ID) : Bookings.Bookings_SelectEmployee(DeptId);
                foreach (EmployeeEntity empl in ss3)
                {
                    string color = Bookings.Booking_GetStatusColor(s.ID, empl.ID, DeptId, areaID);
                    string statusName = Bookings.Booking_GetStatusName(s.ID, empl.ID, DeptId, areaID);
                    DateTime bookingDat = Bookings.Booking_GetBookingDate(s.ID, empl.ID, DeptId, areaID);
                    if (color == "")
                    {
                        text += "<td>&nbsp;</td>";
                    }
                    else
                    {
                        // BookingsEntity empl=Bookings
                        Details += empl.Name.Replace(" ", "_") + "$";
                        Details += s.Title.Replace(" ", "_") + "$";
                        Details += string.Format("{0:d}",bookingDat) + "$";

                        Details +=statusName.Replace(" ", "_") + "$";

                        text += "<td onmouseover=javascript:employeeDetails('" + Details + "'); class='showTip L2' style='border-color:black;background-color:" + ((areaID > 0) ? Bookings.Booking_GetStatusColor(s.ID, empl.ID, DeptId, areaID) : Bookings.Booking_GetStatusColor(s.ID, empl.ID, DeptId)) + "'>&nbsp;</td>";
                        Details = string.Empty;
                    }
                }
               
                foreach (StatusEntity se in Status.GetStatus())
                {

                    text += "<td style='border-width:1px;border-style:solid;border-color:black'>" + ((areaID>0)?Bookings.Booking_GetStatusOfCourse(s.ID, se.StatusID, DeptId, areaID):Bookings.Booking_GetStatusOfCourse(s.ID, se.StatusID, DeptId)) + "</td>";

                }
                text += "<td style='border-width:1px;border-style:solid;border-color:black'>" + ((areaID>0)?DepartmentToCourse.DepartmentToCourse_GetMinReqDays(DeptId, s.ID, areaID):DepartmentToCourse.DepartmentToCourse_GetMinReqDays(DeptId, s.ID)) + "</td>";

                
                text += "</tr>";

            }

            int count = 2;

            foreach (EmployeeEntity empl in ss3)
            {
                //counting number of employees to get value for colspan.
                count++;
            }
            text += "<tr style='height:20px;'><th colspan='" + count + "' style='background-color:silver'></th>";
            foreach (StatusEntity se in Status.GetStatus())
            {
                text += "<td><b>" + ((areaID>0)?Bookings.Booking_GetCourseTotalResult(se.StatusID, DeptId,areaID):Bookings.Booking_GetCourseTotalResult(se.StatusID, DeptId)) + "</b></td>";
            }
            text += "<td style='border-width:1px;border-style:solid;border-color:black'><b>" + ((areaID>0)?DepartmentToCourse.DepartmentToCourse_GetMinReqDays_sum(DeptId, areaID):DepartmentToCourse.DepartmentToCourse_GetMinReqDays_sum(DeptId)) + "</b></td>";
            //foreach (StatusEntity se in ss2)
            //{
            //    text += "<td><b>" + Bookings.Booking_GetCourseTotalResult(se.StatusID, DeptId) + "</b></td>";
            //}
            text += "</tr>";
            foreach (StatusEntity s in Status.GetStatus())
            {
                text += "<tr><th style='background-color:" + s.StatusColor + ";width:25%'><b>" + s.StatusName + "</b></th>";
                foreach (EmployeeEntity empl in ss3)
                {
                    text += "<td style='border-width:1px;border-color:black'>" + ((areaID>0)?Bookings.Booking_GetTotalStatus(empl.ID, s.StatusID, DeptId, areaID):Bookings.Booking_GetTotalStatus(empl.ID, s.StatusID, DeptId)) + "</td>";
                }
                text += "</tr>";
            }
            // text += "<td style='width:5px;border-width:1px;border-style:solid;border-color:black'>" + DepartmentToCourse.DepartmentToCourse_GetMinReqDays_sum(DeptId) + "</td>";

            text += "</table>";
            text += " <script type='text/javascript'>if (typeof tableScroll == 'function') { tableScroll('mainTable'); }</script>";
        }
        else
        {
            text += "<b>No records found<b>";
        }
        ltlGapAnalysis.Text = text;
         
    }
    #endregion

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));// Area.Area_SelectAll();
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();
        GapAnalysisDesign(int.Parse(ddlDepartment.SelectedValue),
            int.Parse(string.IsNullOrEmpty(ddlArea.SelectedValue) ? "0" : ddlArea.SelectedValue));
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GapAnalysisDesign(int.Parse(ddlDepartment.SelectedValue), 
            int.Parse(string.IsNullOrEmpty(ddlArea.SelectedValue) ? "0" : ddlArea.SelectedValue));
    }
}
