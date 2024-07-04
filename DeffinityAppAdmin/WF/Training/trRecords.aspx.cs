using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
using System.Data;

public partial class Training_trRecords : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        grid_training.DataSource = Bookings.Bookings_SelectAll();
        grid_training.DataBind();
    }
    protected void grid_training_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
            if (objList != null)
            {
                if (objList[0].ToString() == "-99")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    
                    if (e.Row.FindControl("ddlStatus") != null)
                    {

                    }
                    else
                    { 
                    
                    
                    }

                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList ddlcategory = (DropDownList)e.Row.FindControl("ddlCategory_footer");
            DropDownList ddlcourse = (DropDownList)e.Row.FindControl("ddlCourse_footer");
            ddlcourse.Items.Insert(0, new ListItem("Please select..", "0"));
            DropDownList ddlemp = (DropDownList)e.Row.FindControl("ddlEmployee_footer");
            DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlStatus_footer");

            BindCategory(ddlcategory, 0);
            BindCourse(ddlcourse, 0, 0);
            BindStatus(ddlstatus,0);
        }
    }
    #region Grid databindings
    private void BindCategory(DropDownList ddlCategory,int setval)
    {
        ddlCategory.DataSource = Category.Category_SelectAll();
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));

        ddlCategory.SelectedValue = setval.ToString();
    }
    private void BindDepartment()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();

        ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCourse(DropDownList ddlCourse,int categoryid,int setval)
    {
        IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(categoryid);
        ddlCourse.DataSource = CE;
        ddlCourse.DataValueField = "ID";
        ddlCourse.DataTextField = "Title";
        ddlCourse.DataBind();

        ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));

        ddlCourse.SelectedValue = setval.ToString();
    }
    private void BindStatus(DropDownList ddlStatus,int setval)
    {
        ddlStatus.DataSource = Status.SelectAll(true);
        ddlStatus.DataValueField = "ID";
        ddlStatus.DataTextField = "Name";
        ddlStatus.DataBind();

        ddlStatus.Items.Insert(0, new ListItem("Please select...", "0"));

        ddlStatus.SelectedValue = setval.ToString();
    }
    #endregion

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnMoreOptions_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCategory_footer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
