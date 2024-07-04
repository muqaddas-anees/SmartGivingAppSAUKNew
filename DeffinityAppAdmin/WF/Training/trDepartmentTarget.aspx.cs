using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_trDepartmentTarget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Master.PageHead = "Training Management";
            Label1.InnerText = "Department/Area Progress Summary";
            BindDepartment();
            BindArea();
            BindDepartmentSummery(int.Parse(ddlDepartment.SelectedValue), 
                int.Parse(string.IsNullOrEmpty(ddlArea.SelectedValue)?"0":ddlArea.SelectedValue));
                    
        }
    }
    #region "BindData"
    private void BindDepartment()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataBind();
        //ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindArea()
    {
        ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));// Area.Area_SelectAll();
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();
        //ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    #endregion "BindData"
   
    private void BindDepartmentSummery(int deptID,int areaID)
    {
        string text = string.Empty;
        text += "<table   cellpadding='1' cellspacing='0' style='color:black'>";
        //int rowspan = 2;
       // text += "<tr><td style='font-weight:bold'>Department/Area Progress Summary</td></tr>";

        text += "<tr><td width='300px'>Minimum Required</td><td>" + DepartmentToCourse.DepartmentToCourse_GetMinReq_sum(deptID, areaID).ToString("N2") + "</td></tr>";
        text += "<tr><td width='300px'>Target(%)</td><td>" + DepartmentToCourse.DepartmentToCourse_GetTarget_Avg(deptID, areaID).ToString("N2") + "%</td></tr>";
        text += "<tr><td width='300px'>Current Progress</td><td>" + Bookings.Booking_CurrentProgressSum(deptID, areaID, Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text)).ToString("N2") + "%</td></tr>";
        text += "<tr><td width='300px'>Passed</td><td>" + Bookings.Booking_PassedPercentage(deptID, areaID, Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text)).ToString("N2") + "%</td></tr>";
        text += "<tr><td width='300px'>Pending</td><td>" + Bookings.Booking_PendingPercentage(deptID, areaID, Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text)).ToString("N2") + "%</td></tr>";
        text += "<tr><td width='300px'>Failed/Retake</td><td>" + Bookings.Booking_FailedPercentage(deptID, areaID, Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text)).ToString("N2") + "%</td></tr>";
        text += "</table>";
        ltlDeptAreaTarget.Text = text;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        BindDepartmentSummery(int.Parse(ddlDepartment.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlArea.SelectedValue) ? "0" : ddlArea.SelectedValue));
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));// Area.Area_SelectAll();
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();
    }
}
