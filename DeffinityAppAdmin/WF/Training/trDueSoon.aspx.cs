using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
public partial class Training_trDueSoon_DashBoard_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTitle.InnerText = "Dashboard";
            //Master.PageHead = "Training Management";
            BindCourse();
            BindFilter();
        }
    }

    private void BindData()
    {
        grd_trBookingDueSoon.DataSource = Bookings.Bookings_SelectByDates(Convert.ToDateTime(txtFromDate.Text),Convert.ToDateTime(txtFromDate.Text)).ToList();
        grd_trBookingDueSoon.DataBind();
    }
    private void BindCourse()
    {

        ddlCourse.DataSource = Course.Course_ByOrderAsc();
        ddlCourse.DataTextField = "Title";
        ddlCourse.DataValueField = "ID";
        ddlCourse.DataBind();
        ddlCourse.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindFilter();
    }
    private void BindGrid()
    {
        grd_trBookingDueSoon.DataSource = Bookings.Bookings_SelectByCourses(int.Parse(ddlCourse.SelectedValue.ToString())).ToList();
        grd_trBookingDueSoon.DataBind();
    }
    private void BindFilter()
    {
        try
        {
            if (txtFromDate.Text == "" && txtToDate.Text == "" && ddlCourse.SelectedIndex!=0)
            {
                grd_trBookingDueSoon.DataSource = Bookings.Bookings_SelectByCourses(int.Parse(ddlCourse.SelectedValue.ToString())).ToList();
                grd_trBookingDueSoon.DataBind();
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ddlCourse.SelectedIndex == 0)
            {
                grd_trBookingDueSoon.DataSource = Bookings.Bookings_SelectByDates(Convert.ToDateTime(txtFromDate.Text),Convert.ToDateTime(txtToDate.Text)).ToList();
                grd_trBookingDueSoon.DataBind();
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ddlCourse.SelectedIndex != 0)
            {
                grd_trBookingDueSoon.DataSource = Bookings.Bookings_SelectByDatesCourse(Convert.ToDateTime(txtFromDate.Text),
                    Convert.ToDateTime(txtToDate.Text), int.Parse(ddlCourse.SelectedValue.ToString())).ToList();
                grd_trBookingDueSoon.DataBind();
            }
            else
            {
                grd_trBookingDueSoon.DataSource = Bookings.Bookings_SelectAll().ToList();
                grd_trBookingDueSoon.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grd_trBookingDueSoon_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            BookingsEntity de = (BookingsEntity)e.Row.DataItem;
            if (de.ID == -99)
            {
                e.Row.Visible = false;
            }
        }
    }
    protected void grd_trBookingDueSoon_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_trBookingDueSoon.PageIndex = e.NewPageIndex;
        BindFilter();
    }
}
