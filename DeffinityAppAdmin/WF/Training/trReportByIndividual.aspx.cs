using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_trReportByIndividual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
            //Master.PageHead = "Training Management";
            lblTitle.InnerText = "Report by Individual";
            //BindPendingCourses();
            //BindCompletedCourses();
            BindEmployees();
           // ddlByType.Visible = false;
           // divType.Visible = false;
           /// divUserName.Visible = false;
            BindCategory();
            //search data
            FilterBy();
            ActionsPlan(int.Parse(ddlUser.SelectedValue));
            tblReport.Visible =true;
        }
    }
    #region "Bind Data"
    private void BindEmployees()
    {
        IEnumerable<ContratorsEntity> BE = Deffinity.TrainingManager.Contractors.ContractorsAll_OrderByAsc();
        ddlUser.DataSource = BE;
        ddlUser.DataTextField = "Name";
        ddlUser.DataValueField = "ID";
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));

        if (ddlUser.Items.Count > 1)
        {
            ddlUser.SelectedIndex = 1;
        }
    }
    private void BindCategory()
    {
        ddlByType.DataSource = Category.Category_SelectAll();
        ddlByType.DataTextField = "Name";
        ddlByType.DataValueField = "ID";
        ddlByType.DataBind();
        ddlByType.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindPendingCourses(int userID,DateTime fromDate,DateTime toDate)
    {
        List<BookingsEntity> BE = Bookings.Bookings_GetPendingCourses(userID, fromDate, toDate);
        grdCoursePending.DataSource = BE;
        grdCoursePending.DataBind();

    }
    private void BindPendingCourses()
    {
        List<BookingsEntity> BE = Bookings.Bookings_GetPendingCourses();
        grdCoursePending.DataSource = BE;
        grdCoursePending.DataBind();

    }
    private void BindCompletedCourses(int userID, DateTime fromDate, DateTime toDate)
    {
        List<BookingsEntity> BE = Bookings.Bookings_GetCompletedCourses(userID, fromDate, toDate);
        grdCourseCompleted.DataSource = BE;
        grdCourseCompleted.DataBind();

    }
    private void BindCompletedCourses()
    {
        List<BookingsEntity> BE = Bookings.Bookings_GetCompletedCourses();
        grdCourseCompleted.DataSource = BE;
        grdCourseCompleted.DataBind();

    }
    private void BindPendingCoursesByType(int userID, DateTime fromDate, DateTime toDate,int typeID)
    {
        List<BookingsEntity> BE = Bookings.Bookings_GetPendingCoursesByType(userID, fromDate, toDate, typeID);
        grdCoursePending.DataSource = BE;
        grdCoursePending.DataBind();

    }

    private void BindCompletedCoursesByType(int userID, DateTime fromDate, DateTime toDate, int typeID)
    {
       // List<BookingsEntity> BE = Bookings.Bookings_GetCompletedCoursesByType(userID, fromDate, toDate,typeID);
        grdCourseCompleted.DataSource = Bookings.Bookings_GetCompletedCoursesByType(userID, fromDate, toDate, typeID); 
        grdCourseCompleted.DataBind();

    }
    #endregion
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        FilterBy();
        ActionsPlan(int.Parse(ddlUser.SelectedValue));
    }
    private void FilterBy()
    {
        try
        {
            ddlByType.SelectedValue = "0";
            tblReport.Visible = true;
            divType.Visible = true;
            //divUserName.Visible = true;
            GetUserDetails(int.Parse(ddlUser.SelectedValue));
            BindCompletedCourses(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text));
            BindPendingCourses(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlByType_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblReport.Visible = true;

        BindCompletedCoursesByType(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text)?"01/01/1900":txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text)?"01/01/1900":txtToDate.Text),int.Parse(ddlByType.SelectedValue));
        BindPendingCoursesByType(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text), int.Parse(ddlByType.SelectedValue));
    }
    private void GetUserDetails(int UserID)
    {
        try
        {
            ltrUserImg.Text = GetUserImage(UserID.ToString());
            
            
            ltrUserdetails.Text = string.Empty;
            if (UserID > 0)
            {
                string name = string.Empty, DepartmentName = string.Empty, ExperienceName = string.Empty, telephone = string.Empty;
                Bookings.UserDetails(UserID, out name, out DepartmentName, out ExperienceName, out telephone);
                string str = "<b>User Name : </b>" + name + "<br/>";
                str = str + "<b>Department : </b>" + DepartmentName + "<br/>";
                str = str + "<b>Level : </b>" + ExperienceName + "<br/>";
                str = str + "<b>Telephone : </b>" + telephone + "<br/>";
                ltrUserdetails.Text = str;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected string GetUserImage(string UserID)
    {
        string img = string.Empty;

        string filepath = Server.MapPath("../UploadData/Users/ThumbNails/") + "user_" + UserID.ToString() + ".png";

        if (System.IO.File.Exists(filepath))
        {
            string imgurl = string.Format("../UploadData/Users/ThumbNails/user_{0}.png", UserID.ToString());
            string navUrl = string.Format("../DisplayUser.aspx?userid={0}", UserID.ToString());
            img = string.Format("<a href='{1}' target='_black'> <img  src='{0}' /></a>", imgurl, navUrl);
            //img = string.Format("<table><tr><td style='border:1px;color:Black'><a href='{1}' target='_black'> <img  src='{0}' /></a><td><tr></table>", imgurl, navUrl);
        }
        return img;
    }

    protected void grdCoursePending_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCoursePending.PageIndex = e.NewPageIndex;
        if (ddlByType.SelectedIndex != 0)
        {
            BindPendingCoursesByType(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text), int.Parse(ddlByType.SelectedValue));
        }
        else
        {
            BindPendingCourses(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text));
        }
    }
    protected void grdCourseCompleted_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCourseCompleted.PageIndex = e.NewPageIndex;
        if (ddlByType.SelectedIndex != 0)
        {
            BindCompletedCoursesByType(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text), int.Parse(ddlByType.SelectedValue));
        }
        else
        {
            BindCompletedCourses(int.Parse(ddlUser.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text));
        }
    }

    public  void ActionsPlan(int userID)
    {
     
        grdPostCourseAction.DataSource= (from r in Bookings.Bookings_SelectAll()
                                         where r.Employee == userID
                   
                   join cp in CourseFeedBack.CourseFeedBack_All() on r.CFeedBackID equals cp.BookingID
                   join pp in CourseFeedBack.ActionPlan_All() on cp.ID equals pp.FeedBackID
                 
                   select new {cp.CourseTitle,r.DateofCourse,pp.ActionPlan,r.EndDate,r.StatusName }).ToList();

        grdPostCourseAction.DataBind();
    }

}
