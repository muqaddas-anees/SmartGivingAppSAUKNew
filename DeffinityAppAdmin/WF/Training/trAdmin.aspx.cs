using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;


public partial class Training_Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindCategory();
                BindDepartment();
                BindTrainingType();
                BindCourse();
                BindCourse(int.Parse(ddlCategory.SelectedValue));
                BindVendor();
                BindCustomer();
                BindSite(0);
                BindArea();
                //BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
                //BindDepartmentUsers(int.Parse(ddlDepartment.SelectedValue));

                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        lblTitle.InnerText = "Admin";
        //Master.PageHead = "Training Management";
    }
    

    #region Bind data
    private void BindArea()
    {
       
            ddlArea.DataSource = Area.Area_SelectAll();
            ddlArea.DataValueField = "ID";
            ddlArea.DataTextField = "Name";
            ddlArea.DataBind();
       
        ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindArea(int departmentID)
    {

        ddlArea.DataSource = Area.Area_SelectByDepartment(departmentID);
        ddlArea.DataValueField = "ID";
        ddlArea.DataTextField = "Name";
        ddlArea.DataBind();

        ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCategory()
    {
        ddlCategory.DataSource = Category.Category_SelectAll();
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindDepartment()
    {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();

        ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCourse()
    {
        IEnumerable<CourseEntity> CE = Course.Course_SelectAll();
        ddlCourse.DataSource = CE;
        ddlCourse.DataValueField = "ID";
        ddlCourse.DataTextField = "Title";
        ddlCourse.DataBind();

        ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));

        ddlDepCourse.DataSource = CE;
        ddlDepCourse.DataValueField = "ID";
        ddlDepCourse.DataTextField = "Title";
        ddlDepCourse.DataBind();

        ddlDepCourse.Items.Insert(0, new ListItem("Please select...", "0"));

    }
    private void BindVendor()
    {
        ddlVendor.DataSource = TrainingVendor.SelectAll();
        ddlVendor.DataValueField = "ID";
        ddlVendor.DataTextField = "Name";
        ddlVendor.DataBind();

        ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));
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
    private void BindDepartmentUsers(int DepartmentID)
    {
        Grid_DepartmentUsers.DataSource = Department.DepartmentCustomer_select(DepartmentID);
        Grid_DepartmentUsers.DataBind();
    }
    private void BindCourse(int categoryid)
    {
        IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(categoryid);
        ddlCourse.DataSource = CE;
        ddlCourse.DataValueField = "ID";
        ddlCourse.DataTextField = "Title";
        ddlCourse.DataBind();

        ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCategoryGrid(int CategoryID)
    {
        if(CategoryID == 0)
        Grid_Category.DataSource = Course.Course_SelectAll();
        else
        Grid_Category.DataSource = Course.Course_SelectByCategory(CategoryID);
        Grid_Category.DataBind();
    }

    private void BindTrainingType()
    {
        ddlTrainingType.DataSource = TrainingType.TrainingType_SelectAll();
        ddlTrainingType.DataValueField = "ID";
        ddlTrainingType.DataTextField = "Name";
        ddlTrainingType.DataBind();

        ddlTrainingType.Items.Insert(0,new ListItem("Please select...","0"));
    }
    #endregion
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlCustomer.SelectedValue) > 0)
            BindSite(int.Parse(ddlCustomer.SelectedValue));

    }
    #region ModelPopup Category
    protected void btnModelCategoryInsert_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CategoryEntity ce = new CategoryEntity();
            if (int.Parse(H_Category.Value) > 0)
            {
                ce.ID = int.Parse(H_Category.Value);
                ce.Name = txtModelCategoryInsert.Text.Trim();
                //reset the value
                H_Category.Value = "0";
            }
            else
            {
                ce.ID = 0;
                ce.Name = txtModelCategoryInsert.Text.Trim();
            }
            Category.Category_InsertUpdate(ce);

            txtModelCategoryInsert.Text = string.Empty;
            //Bind category dropdown
            BindCategory();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    #endregion

    #region "ModelPopup Training Type"
    protected void imgBtnTrainingTypeOk_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            TrainingTypeEntity te = new TrainingTypeEntity();
            if (int.Parse(H_TrainingType.Value) > 0)
            {
                te.ID = int.Parse(H_TrainingType.Value);
                te.Name = txtModelTrainingType.Text.Trim();
                
                H_TrainingType.Value = "0";
            }
            else
            {
                te.ID = 0;
                te.Name = txtModelTrainingType.Text.Trim();
            }
            TrainingType.TrainingType_InsertUpdate(te);
            txtModelTrainingType.Text = string.Empty;
            BindTrainingType();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);

        }
    }


    #endregion "ModelPopup Training Type"

    #region "ModelPopup  Area"
    protected void imgAreaSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AreaEntity ae = new AreaEntity();
            if (int.Parse(H_Area.Value) > 0)
            {
                ae.ID = int.Parse(H_Area.Value);
                ae.Name = txtArea.Text.Trim();
                //ae.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
                
            }
            else
            {
                ae.ID = 0;
                ae.Name = txtArea.Text.Trim();
                //ae.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
                
            }
            Area.Area_InsertUpdate(ae);
            txtArea.Text = string.Empty;

            BindArea();
            ClearDepartmentDetails();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion "ModelPopUp Area"

    #region ModelPopup Department
    protected void btnModelDepartmentInsert_Click(object sender, ImageClickEventArgs e)
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
    protected void btnCategoryEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (int.Parse(ddlCategory.SelectedValue) > 0)
        {
            H_Category.Value = ddlCategory.SelectedValue;
            txtModelCategoryInsert.Text = ddlCategory.SelectedItem.Text;
            mdlCategory.Show();
        }
        else 
        {
            H_Category.Value = "0";
            //msg
        }
    }
    protected void btnEditDepartment_Click(object sender, ImageClickEventArgs e)
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
    protected void btnSubmitCourse_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (int.Parse(ddlCategory.SelectedValue) > 0)
            {
                if (!string.IsNullOrEmpty(txtCourseTitle.Text.Trim()))
                {
                    CourseEntity ce = new CourseEntity();
                    if (int.Parse(ddlCourse.SelectedValue) == 0)
                        ce.ID = 0;
                    else
                    ce.ID = int.Parse(ddlCourse.SelectedValue);
                    ce.VendorID = int.Parse(ddlVendor.SelectedValue);
                    ce.Venue = txtVenue.Text.Trim();
                    ce.Title = txtCourseTitle.Text.Trim();
                    ce.Rate = double.Parse(string.IsNullOrEmpty(txtRate.Text.Trim()) ? "0" : txtRate.Text.Trim());
                    ce.CategoryID = int.Parse(ddlCategory.SelectedValue);
                    ce.Duration = txtDuration.Text.Trim();
                    ce.TrainingTypeID = int.Parse(ddlTrainingType.SelectedValue);
                    ce.TrainingTypeName = ddlTrainingType.SelectedItem.ToString();
                    Course.Course_InsertUpdate(ce);

                    //ClearCourseDetails();
                    //Bind the course
                    BindCourse(int.Parse(ddlCategory.SelectedValue));
                    BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
                }
                else
                {
                    lblException.Text = "Please enter course title";
                }
            }
            else
            {
                lblException.Text = "Please select category";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCancelCourse_Click(object sender, ImageClickEventArgs e)
    {
        ClearCourseDetails();
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(int.Parse(ddlCourse.SelectedValue) >0)
        {
            SetCourseDetails(int.Parse(ddlCourse.SelectedValue));
        }
    }
    protected void btnAddCourse_Click(object sender, ImageClickEventArgs e)
    {
        ClearCourseDetails();
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearCourseDetails();
    }

    private void SetCourseDetails(int id)
    {
        try
        {
            CourseEntity ce = Course.Course_Select(id);

            txtCourseTitle.Text = ce.Title;
            txtDuration.Text = ce.Duration;
            txtVenue.Text = ce.Venue;
            txtRate.Text = ce.Rate.ToString();
            ddlVendor.SelectedValue = ce.VendorID.ToString();
            ddlCategory.SelectedValue = ce.CategoryID.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void ClearCourseDetails()
    {
       
        txtCourseTitle.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtVenue.Text = string.Empty;
        txtRate.Text = string.Empty;
        ddlVendor.SelectedIndex = 0;
        //ddlCategory.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentUsers(int.Parse(ddlDepartment.SelectedValue));
       //BindArea(int.Parse(ddlDepartment.SelectedValue));
       SetDepartmentdetails(int.Parse(ddlDepartment.SelectedValue), 0, 0);
        
    }

    protected void btnDepSubmit_Click(object sender, ImageClickEventArgs e)
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


                    DepartmentToCourse.Category_InsertUpdate(dce);
               

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
    protected void btnDeptCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearDepartmentDetails();
    }
    private void SetDepartmentdetails(int departmentID,int areaID,int courseID)
    {
        try
        {
            DepartmentToCourseEntity de = new DepartmentToCourseEntity();
            if (courseID > 0)
            {
                de = DepartmentToCourse.DepartmentToCourseCollection_SelectByCourseID(departmentID,areaID,courseID);
               //clear if empty 
                if (de == null)
                {
                    ClearDepCourseFields();
                }
            }
            else if (areaID > 0)
            { 
                de = DepartmentToCourse.DepartmentToCourseCollection_SelectByAreaID(departmentID,areaID);
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
            txtTarget.Text = de.Target.ToString();
            ddlCustomer.SelectedValue = de.CustomerID.ToString();
            BindSite(de.CustomerID);
            ddlSite.SelectedValue = de.SiteID.ToString();
            ddlDepCourse.SelectedValue = de.CourseID.ToString();
            ddlArea.SelectedIndex = ddlArea.Items.IndexOf(ddlArea.Items.FindByValue(de.AreaID.ToString()));
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
    }
    private void ClearDepCourseFields()
    {
        txtNumberReq.Text = string.Empty;
        txtTarget.Text = string.Empty;
        H_DeptoCus.Value = "0";
        
    }
    private void ClearDepAreaFields()
    {
        ddlCustomer.SelectedIndex = 0;
        ddlSite.SelectedIndex = 0;
        ddlDepCourse.SelectedIndex = 0;
        txtNumberReq.Text = string.Empty;
        txtTarget.Text = string.Empty;
        H_DeptoCus.Value = "0";
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
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse(int.Parse(ddlCategory.SelectedValue));
        //ClearCourseDetails();
        BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
    }
    protected void btnDeleteCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (int.Parse(ddlCategory.SelectedValue) > 0)
            {
                Category.Category_Delete(int.Parse(ddlCategory.SelectedValue));
                BindCategory();
            }
            else
            {
                lblException.Text = "Please select category";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void btnDeleteDepartment_Click(object sender, ImageClickEventArgs e)
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
    protected void btnDeleteArea_Click(object sender, ImageClickEventArgs e)
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
    protected void btnDeleteCourse_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (int.Parse(ddlCourse.SelectedValue) > 0)
            {
                Course.Course_Delete(int.Parse(ddlCourse.SelectedValue));
                BindCourse(int.Parse(ddlCategory.SelectedValue));
                ClearCourseDetails();
            }
            else
            {
                lblException.Text = "Please select course";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (int.Parse(ddlArea.SelectedValue)>0)
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
