using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class Training_trCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindCategory();
                BindCourse();
                BindRequirement();
                Bindclassification();
                BindTrainingType();
                BindVendor();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        lblTitle.InnerText = "Admin";
        //Master.PageHead = "Training Management";
    }
    #region "BindData"
    private void Bindclassification()
    {
        lstboxClassification.DataSource = Course.Classification_selectAll();
        lstboxClassification.DataTextField = "ExpClassification";
        lstboxClassification.DataValueField = "ID";
        lstboxClassification.DataBind();
    }
    private void BindCourse()
    {
        IEnumerable<CourseEntity> CE = Course.Course_SelectAll();
        ddlCourse.DataSource = CE;
        ddlCourse.DataValueField = "ID";
        ddlCourse.DataTextField = "Title";
        ddlCourse.DataBind();

        ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));

    }
    private void BindRequirement()
    {
        ddlRequirement.DataSource= Requirement.SelectAll(true);
        ddlRequirement.DataTextField = "text";
        ddlRequirement.DataValueField = "value";
        ddlRequirement.DataBind();
    }
    private void BindVendor()
    {
        ddlVendor.DataSource = TrainingVendor.SelectAll();
        ddlVendor.DataValueField = "ID";
        ddlVendor.DataTextField = "Name";
        ddlVendor.DataBind();

        ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCategory()
    {
        ddlCategory.DataSource = Category.Category_SelectAll();
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));
        //ClearInputs();
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
    private void ClearInputs()
    {
        try
        {
            txtCourseDesc.Text = string.Empty;
            txtCourseTitle.Text = string.Empty;
            ddlCourse.SelectedIndex = 0;
            txtDuration.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtVenue.Text = string.Empty;
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    
    private void BindCategoryGrid(int CategoryID)
    {
        if (CategoryID == 0)
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

        ddlTrainingType.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    #endregion
    #region ModelPopup Category
    protected void btnModelCategoryInsert_Click(object sender, EventArgs e)
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
    #region ModelPopup Training Type
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


    #endregion 

    protected void btnCategoryAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnCategoryEdit_Click(object sender, EventArgs e)
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
    protected void btnDeleteCategory_Click(object sender, EventArgs e)
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
    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        ClearCourseDetails();
    }
    private void ClearCourseDetails()
    {

        txtCourseTitle.Text = string.Empty;
        txtDuration.Text = string.Empty;
        txtVenue.Text = string.Empty;
        txtRate.Text = string.Empty;
        ddlRequirement.SelectedIndex = 0;
        ddlVendor.SelectedIndex = 0;
        //ddlCategory.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        txtCourseDesc.Text = string.Empty;
        Bindclassification();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearCourseDetails();
    }
    protected void btnDeleteCourse_Click(object sender, EventArgs e)
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
    protected void btnSubmitCourse_Click(object sender, EventArgs e)
    {
        try
        {
            lblException.Visible = false;
            string classificationIDs="";
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
                    ce.RequirementID = int.Parse(ddlRequirement.SelectedValue);
                   // ce.RequirementName = ddlRequirement.SelectedItem.ToString();
                    ce.CourseDesc = txtCourseDesc.Text.Trim();
                    int courseID=Course.Course_InsertUpdate(ce);
                      
                    classificationIDs=InsertToClassification();
                    if (classificationIDs != "" && courseID != 0)
                    {
                        Course.Classification_InsertDelete(courseID, classificationIDs);
                    }
                    else if (ddlCourse.SelectedIndex != 0 && classificationIDs != "")
                    {
                        Course.Classification_InsertDelete(int.Parse(ddlCourse.SelectedValue.ToString()), classificationIDs);
                    }
                    else
                    {
                        //lblException.Visible = true;
                        //lblException.Text = "Please select one or more classifications";
                    }
                    //ClearCourseDetails();
                    //Bind the course
                   

                    if (chkTrue.Checked == true && classificationIDs != "" && courseID != 0)
                    {
                        //BookingsEntity be = new BookingsEntity();
                        int count;
                        IEnumerable<ContratorsEntity> ce1 = Deffinity.TrainingManager.Contractors.Contractors_ByClassification(classificationIDs);
                        foreach (var booking in ce1)
                        {
                            count = Bookings.Booking_ClassifictionToBooking(booking.ID,courseID,
                                int.Parse(ddlCategory.SelectedValue.ToString()), booking.DepartmentID);
                            if (count == 0)
                            {
                                // if (ddlRequirement.SelectedValue == "1" || ddlRequirement.SelectedValue == "3")
                                if (ddlRequirement.SelectedValue == "1")
                                {
                                    InsertIntoBooking(booking.ID, booking.DepartmentID, booking.DepartmentName,
                                        booking.Name,4, courseID);
                                }
                                else
                                {
                                    InsertIntoBooking(booking.ID, booking.DepartmentID, booking.DepartmentName,
                                        booking.Name,3, courseID);
                                }
                            }
                        }
                    }
                    else if (chkTrue.Checked == true && classificationIDs != "" && ddlCourse.SelectedIndex != 0)
                    {
                        int count;
                        IEnumerable<ContratorsEntity> ce1 = Deffinity.TrainingManager.Contractors.Contractors_ByClassification(classificationIDs);
                        foreach (var booking in ce1)
                        {
                            count = Bookings.Booking_ClassifictionToBooking(booking.ID, int.Parse(ddlCourse.SelectedValue.ToString()),
                                int.Parse(ddlCategory.SelectedValue.ToString()), booking.DepartmentID);
                            if (count == 0)
                            {
                               // if (ddlRequirement.SelectedValue == "1" || ddlRequirement.SelectedValue == "3")
                                if (ddlRequirement.SelectedValue == "1")
                                {
                                    InsertIntoBooking(booking.ID, booking.DepartmentID, booking.DepartmentName,
                                        booking.Name,4, int.Parse(ddlCourse.SelectedValue.ToString()));
                                }
                                else
                                {
                                    InsertIntoBooking(booking.ID, booking.DepartmentID, booking.DepartmentName,
                                       booking.Name,3, int.Parse(ddlCourse.SelectedValue.ToString()));
                                }
                            }
                        }

                    }
                    else
                    {
                        //lblException.Visible = true;
                        //lblException.Text = "Please select course and one or more classification";
                    }
                   
                }
                else
                {
                    lblException.Visible = true;
                    lblException.Text = "Please enter course title";
                }
            }
            else
            {
                lblException.Visible = true;
                lblException.Text = "Please select category";
            }
            BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
            BindCourse(int.Parse(ddlCategory.SelectedValue));
             lblException.Visible = true;
             lblException.Text = "Training directory updated";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void DisplayClassifieds(int courseID)
    {
        ClassificationEntity ce = Course.Course_ClassficationSelect(courseID);

        string strValues = (string.IsNullOrEmpty(ce.classficationID) ? "0" : ce.classficationID).ToString();
        if (strValues == "0")
        {
            Bindclassification();
        }
        else
        {
            string[] words = strValues.Split(',');

            Bindclassification();
            foreach (ListItem i in lstboxClassification.Items)
            {

                if (words.Length > 0)
                {
                    for (int k = 0; k < words.Length; k++)
                    {
                        if (words[k] == i.Value)
                        {
                            i.Selected = true;
                        }
                    }
                }

            }         
        }
    }
    protected string InsertToClassification()
    {
        string classificationID = "";
        try
        {
           
            
            foreach (ListItem i in lstboxClassification.Items)
            {
                if (i.Selected)
                {
                    classificationID += i.Value + ",";

                }
               
            }
           // Course.Classification_InsertDelete(courseID, classificationID);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return classificationID;
    }
    protected void btnCancelCourse_Click(object sender, EventArgs e)
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
            txtCourseDesc.Text = ce.CourseDesc;
            BindRequirement();
            ddlRequirement.SelectedValue = ce.RequirementID.ToString();
            BindCategory();
            ddlCategory.SelectedValue = ce.CategoryID.ToString();
            BindVendor();
            ddlVendor.SelectedValue = ce.VendorID.ToString();
           
            
            DisplayClassifieds(id);
            
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(ddlCourse.SelectedValue) > 0)
        {
            SetCourseDetails(int.Parse(ddlCourse.SelectedValue));
        }
       
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCourse(int.Parse(ddlCategory.SelectedValue));
        //ClearCourseDetails();
        BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
    }


    protected void InsertIntoBooking(int employee,int deptId,string DeptName,string employeeName,int status,int courseID)
    {

        BookingsEntity booking = new BookingsEntity();
        CourseEntity course = Course.Course_Select(int.Parse(ddlCourse.SelectedValue));
        DateTime date = DateTime.Now.Date;
        booking.ID = 0; //Convert.ToInt32(e.CommandArgument);
        booking.BookingDate = date;// Convert.ToDateTime(txtBookingDate.Text);
        booking.CategoryID = int.Parse(ddlCategory.SelectedValue);
        booking.CategoryName = ddlCategory.SelectedItem.ToString();
        booking.CheckedBy = 0;// int.Parse(ddlCheckedBy.SelectedValue);
        booking.CheckedByName = "";// ddlCheckedBy.SelectedItem.ToString();
        booking.CheckedDate = date;// Convert.ToDateTime(txtCheckedDate.Text);
        booking.Comments ="";

        booking.CostofCourse = course.Rate;// Convert.ToDouble(txtCostOfCourse.Text);
        booking.CourseID = courseID;
        booking.CourseTitle = txtCourseTitle.Text;
        booking.CourseVenue = "";// txtCourseVenue.Text;
        booking.DateofCourse = date;// Convert.ToDateTime(txtDate.Text);
        booking.DepartmentID = deptId;
        booking.DepartmentName = DeptName;
        booking.DurationInDays = 0;// int.Parse(txtDurationInDays.Text);
        booking.Employee = employee;
        booking.EmployeeName =employeeName ;
        booking.EndDate = date;// Convert.ToDateTime(txtEndDate.Text);
        booking.EndTime = ""; //txtEndOfTime.Text;
        booking.Expenses = 0.00;// Convert.ToDouble(txtExpenses.Text);
        booking.FeedBack = "";// txtFeedBack.Text;
        booking.FileID = 0;
        booking.Instructor = "";// txtInstructor.Text;
        booking.NotifyDaysPrior = 0;// int.Parse(txtNotifyDaysPrior.Text);
        booking.NotifyUser = "";// int.Parse(ddlNotifyUsers.SelectedValue);
        booking.NotifyUserName = "";// ddlNotifyUsers.SelectedItem.ToString();
        booking.Outcome = 0;// int.Parse(ddlOutCome.SelectedValue);
        booking.OutcomeName = "";// ddlOutCome.SelectedItem.ToString();
        booking.Penalties = 0.00;// Convert.ToDouble(txtPenalities.Text);
        booking.RequiredByDate = date;// Convert.ToDateTime(txtDate.Text);//value to be verify
        booking.StartTime = "";// txtStartTime.Text;
        booking.StatusID =status;
        booking.StatusName ="";
        if (txtRate.Text != "")
        {
            booking.Budget = int.Parse(string.IsNullOrEmpty(txtRate.Text) ? "0" : txtRate.Text);
        }
        else
        {
            booking.Budget = course.Rate;
        }
        int exist = Bookings.Bookings_InsertUpdate(booking);
    }

    
}
