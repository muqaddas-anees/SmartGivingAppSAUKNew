using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingManager;
using Deffinity.TrainingEntity;

public partial class Training_trDeptReq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepartment();
            BindCustomer();
            BindCourse();
            BindSite(0);
            BindArea();
        }
        lblTitle.InnerText = "Admin";
        //Master.PageHead = "Training Management";
    }

 #region "Bind Data"
    private void BindDepartment()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();

        ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindDepartmentUsers(int DepartmentID)
    {
        Grid_DepartmentUsers.DataSource = Department.DepartmentCustomer_select(DepartmentID);
        Grid_DepartmentUsers.DataBind();
    }
    private void BindDeptReq(int DepartmentID)
    {
        GridDeptReq.DataSource = DepartmentToCourse.DepartmentToCourseCollection_ToSecondGrid(DepartmentID);
        GridDeptReq.DataBind();
    }
    private void BindCourse()
    {
        IEnumerable<CourseEntity> CE = Course.Course_ByOrderAsc();
       
        ddlDepCourse.DataSource = CE;
        ddlDepCourse.DataValueField = "ID";
        ddlDepCourse.DataTextField = "Title";
        ddlDepCourse.DataBind();

        ddlDepCourse.Items.Insert(0, new ListItem("Please select...", "0"));

    }
    private void BindArea()
    {
        ddlArea.DataSource = Area.Area_SelectByDepartment(int.Parse(ddlDepartment.SelectedValue));
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();

        ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
        //ddlArea.DataSource = Area.Area_OrderByAsc();// Area_SelectAll();
        //ddlArea.DataValueField = "ID";
        //ddlArea.DataTextField = "Name";
        //ddlArea.DataBind();

       // ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindArea(int departmentID)
    {
        ddlArea.DataSource = Area.Area_SelectByDepartment(int.Parse(ddlDepartment.SelectedValue));
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();
        //ddlArea.DataSource = Area.Area_SelectByDepartment(departmentID);
        //ddlArea.DataValueField = "ID";
        //ddlArea.DataTextField = "Name";
        //ddlArea.DataBind();

        ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCustomer()
    {
        ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlCustomer.DataTextField = "PortFolio";
        ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataBind();

        ddlCustomer.Items.RemoveAt(0);
        ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindSite(int customerid)
    {
        ddlSite.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio_withSelect(customerid);
        ddlSite.DataTextField = "Site";
        ddlSite.DataValueField = "ID";
        ddlSite.DataBind();

    }
    #endregion
 #region "ModelPopup  Area"
    protected void imgAreaSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlDepartment.SelectedValue) > 0)
            {
                AreaEntity ae = new AreaEntity();
                if (int.Parse(H_Area.Value) > 0)
                {
                    ae.ID = int.Parse(H_Area.Value);
                    ae.Name = txtArea.Text.Trim();
                    ae.DepartmentID = int.Parse(ddlDepartment.SelectedValue);

                }
                else
                {
                    ae.ID = 0;
                    ae.Name = txtArea.Text.Trim();
                    ae.DepartmentID = int.Parse(ddlDepartment.SelectedValue);

                }
                Area.Area_InsertUpdate(ae);
                txtArea.Text = string.Empty;

                BindArea();
                ClearDepartmentDetails();
            }
            else
            {
                lblException.Text = "Please select Department";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion "ModelPopUp Area"
 #region ModelPopup Department
    protected void btnModelDepartmentInsert_Click(object sender, EventArgs e)
    {
        try
        {
            DepartmentEntity de = new DepartmentEntity();
            if (int.Parse(H_Department.Value) > 0)
            {
                de.ID = int.Parse(H_Department.Value);
                de.Name = txtModelDepartment.Text.Trim();
                //reset the value
                H_Department.Value = "0";
            }
            else
            {
                de.ID = 0;
                de.Name = txtModelDepartment.Text.Trim();
            }
            Department.Department_InsertUpdate(de);

            txtModelDepartment.Text = string.Empty;
            //Bind category dropdown
            BindDepartment();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    protected void btnEditDepartment_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlDepartment.SelectedValue) > 0)
        {
            H_Department.Value = ddlDepartment.SelectedValue;
            txtModelDepartment.Text = ddlDepartment.SelectedItem.Text;
            mdlDepartment.Show();
        }
        else
        {
            H_Department.Value = "0";
            //msg
        }
    }
    protected void btnDeleteDepartment_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlDepartment.SelectedValue) > 0)
            {
                Department.Department_Delete(int.Parse(ddlDepartment.SelectedValue));
                BindDepartment();
            }

            else
            {
                lblException.Text = "Please select department";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        BindDeptReq(int.Parse(ddlDepartment.SelectedValue));
     
        BindDepartmentUsers(int.Parse(ddlDepartment.SelectedValue));
        BindArea();
        //BindArea(int.Parse(ddlDepartment.SelectedValue));
        SetDepartmentdetails(int.Parse(ddlDepartment.SelectedValue), 0, 0);
    }
    private void SetDepartmentdetails(int departmentID, int areaID, int courseID)
    {
        try
        {
            DepartmentToCourseEntity de = new DepartmentToCourseEntity();
            if (courseID > 0)
            {
                de = DepartmentToCourse.DepartmentToCourseCollection_SelectByCourseID(departmentID, areaID, courseID);
                //clear if empty 
                if (de == null)
                {
                    ClearDepCourseFields();
                }
            }
            else if (areaID > 0)
            {
                de = DepartmentToCourse.DepartmentToCourseCollection_SelectByAreaID(departmentID, areaID);
                //clear if empty 
                if (de == null)
                {
                    ClearDepAreaFields();
                }
            }
            else if (departmentID > 0)
            {
                de = DepartmentToCourse.DepartmentToCourseCollection_SelectByDeparmentID(departmentID);
                //clear if empty 
                if (de == null)
                {
                    ClearDepFields();
                }
            }
            else
            {
                ClearDepartmentDetails();
            }

            SetDepartmentdetails(de);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void SetDepartmentdetails(DepartmentToCourseEntity de)
    {
        if (de != null)
        {
            H_DeptoCus.Value = de.ID.ToString();
            txtNumberReq.Text = de.MinRequired.ToString();
            txtTarget.Text =de.Target.ToString();
            ddlCustomer.SelectedValue = de.CustomerID.ToString();
            BindSite(de.CustomerID);
            ddlSite.SelectedValue = de.SiteID.ToString();
            ddlDepCourse.SelectedValue = de.CourseID.ToString();
            ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(de.AreaID.ToString()));
            if (string.Format("{0:d}", de.FromDate) == "01/01/1900")
            {
                txtBookingDate.Text = string.Empty;
            }
            else
            {
                txtBookingDate.Text = string.Format("{0:d}", de.FromDate);
            }

            if (string.Format("{0:d}", de.ToDate) == "01/01/1900")
            {
                txtEndDate.Text = string.Empty;
            }
            else
            {
                txtEndDate.Text = string.Format("{0:d}", de.ToDate);
            }
            //txtEndDate.Text = string.Format("{0:d}", de.ToDate);

        }


    }
    private void ClearDepartmentDetails()
    {
        ddlDepartment.SelectedIndex = 0;
        txtNumberReq.Text = string.Empty;
        txtTarget.Text = string.Empty;
        ddlCustomer.SelectedIndex = 0;
        ddlSite.SelectedIndex = 0;
        ddlDepCourse.SelectedIndex = 0;
        ddlArea.SelectedIndex = 0;
        H_DeptoCus.Value = "0";
        txtBookingDate.Text=string.Empty;
        txtEndDate.Text = string.Empty;
    }
    private void ClearDepCourseFields()
    {
        txtNumberReq.Text = string.Empty;
        txtTarget.Text = string.Empty;
        H_DeptoCus.Value = "0";
        txtBookingDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;

    }
    private void ClearDepAreaFields()
    {
        ddlCustomer.SelectedIndex = 0;
        ddlSite.SelectedIndex = 0;
        ddlDepCourse.SelectedIndex = 0;
        txtNumberReq.Text = string.Empty;
        txtTarget.Text = string.Empty;
        H_DeptoCus.Value = "0";
        txtBookingDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
    }
    private void ClearDepFields()
    {
        ddlCustomer.SelectedIndex = 0;
        ddlSite.SelectedIndex = 0;
        ddlDepCourse.SelectedIndex = 0;
        ddlArea.SelectedIndex = 0;
        txtNumberReq.Text = string.Empty;
        txtTarget.Text = string.Empty;
        H_DeptoCus.Value = "0";
        txtBookingDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlCustomer.SelectedValue) > 0)
            BindSite(int.Parse(ddlCustomer.SelectedValue));
    }
    protected void btnDeleteArea_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlArea.SelectedValue) > 0)
            {
                Area.Area_Delete(int.Parse(ddlArea.SelectedValue));
                BindArea();
            }
            else
            {
                lblException.Text = "Please select area";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnDepSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlDepartment.SelectedValue) > 0)
            {

                DepartmentToCourseEntity dce = new DepartmentToCourseEntity();
                if (int.Parse(H_DeptoCus.Value) > 0)
                    dce.ID = int.Parse(H_DeptoCus.Value);
                else
                    dce.ID = 0;
                dce.CourseID = int.Parse(ddlDepCourse.SelectedValue);
                dce.CustomerID = int.Parse(ddlCustomer.SelectedValue);
                dce.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
                dce.SiteID = int.Parse(ddlSite.SelectedValue);
                dce.Target =int.Parse(txtTarget.Text);
                dce.MinRequired = int.Parse(string.IsNullOrEmpty(txtNumberReq.Text.Trim()) ? "0" : txtNumberReq.Text.Trim());
                dce.AreaID = int.Parse(ddlArea.SelectedValue);
                dce.FromDate=Convert.ToDateTime(string.IsNullOrEmpty(txtBookingDate.Text)?"01/01/1900":txtBookingDate.Text);
                dce.ToDate=Convert.ToDateTime(string.IsNullOrEmpty(txtEndDate.Text)?"01/01/1900":txtEndDate.Text);

                DepartmentToCourse.Category_InsertUpdate(dce);
             
                BindDeptReq(int.Parse(ddlDepartment.SelectedValue));
                lblException.Text = "Department requirement added";

            }
            else
            {
                lblException.Text = "Please select department";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnDeptCancel_Click(object sender, EventArgs e)
    {
        ClearDepartmentDetails();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (int.Parse(ddlArea.SelectedValue) > 0)
            {
                SetDepartmentdetails(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlArea.SelectedValue), 0);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlDepCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlDepCourse.SelectedValue) > 0)
        {
            SetDepartmentdetails(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlArea.SelectedValue), int.Parse(ddlDepCourse.SelectedValue));
        }
    }
}
