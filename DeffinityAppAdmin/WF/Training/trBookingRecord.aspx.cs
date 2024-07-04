using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;
using System.IO;

public partial class Training_trBookingRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Email e = new Email();
        //e.SendingMail(
        try
        {
            if (!IsPostBack)
            {

              //  lblTitle.InnerText = "Booking";
               // Master.PageHead = "Training Management";
                btnFeedBack.Visible = false;
                btnCourseFeedback.Visible = false;
                lblException.Visible = false;

                divCourseReOccurence.Visible = false;//to make course reoccurrence to invisible
                BindDepartment();
                BindArea();
                BindFilteredEmployee(int.Parse(ddlDepartment.SelectedValue));
                BindFilteredCourses();
                BindCategory(0);
                BindCourseUpdate(0, 0);
                BindData();
                BindCheckedBy();
                BindNotifyUsers();
                BindStatus();
                BindOutCome();
                BindReoccurrenceFrequencey();
               
            }
            for (int i = 0; i < chkDays.Items.Count; i++)
            {
                chkDays.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
      
    }
    #region "Bind Data"
    private void BindData()
    {

        grd_trBookingRecord.DataSource = Bookings.Bookings_SelectByDepartment_list(int.Parse(ddlDepartment.SelectedValue));
        grd_trBookingRecord.DataBind();
    }
    private void BindReoccurrenceFrequencey()
    {
        try
        {
        ddlReoccursFrequencey.DataSource = ReoccurrenceFrequencey.SelectAll(true);
        ddlReoccursFrequencey.DataValueField = "value";
        ddlReoccursFrequencey.DataTextField = "text";
        ddlReoccursFrequencey.DataBind();
        }
        
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
    }

    
    private void BindDepartment()
    {
        try
        {
        ddlDepartment.DataSource = Department.Department_SelectAll();
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("Please select...", "0"));
        if (ddlDepartment.Items.Count > 1)
        {
            ddlDepartment.SelectedIndex = 1;
        }
        }
        
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }

      
    }
    private void BindNotifyUsers()
    {
        try
        {
        lstNotifyUser.DataSource = Deffinity.TrainingManager.Contractors.Contractors_OrderByAsc();
        lstNotifyUser.DataTextField = "Name";
        lstNotifyUser.DataValueField = "ID";
        lstNotifyUser.DataBind();
        }
        
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }

       // lstNotifyUser.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    private void BindCheckedBy()
    {
        try
        {
        ddlCheckedBy.DataSource = Department.DepartmentCustomer_select(0);
        ddlCheckedBy.DataValueField = "ID";
        ddlCheckedBy.DataTextField = "ContractorName";
        ddlCheckedBy.DataBind();

        ddlCheckedBy.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
    }

    //Bind course and type in update area.
    private void BindCourseUpdate(int categoryid, int setValue)
    {
        try
        {
        ddlCourseUpdate.Items.Clear();
        IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(categoryid);
        ddlCourseUpdate.DataSource = CE;
        ddlCourseUpdate.DataValueField = "ID";
        ddlCourseUpdate.DataTextField = "Title";
        ddlCourseUpdate.DataBind();

        ddlCourseUpdate.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlCourseUpdate.SelectedValue = setValue.ToString();
        ddlCourseUpdate.AppendDataBoundItems = true;
        }
        
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
    }
    private void BindCategory(int setValue)
    {
        try
        {
        //ddlCategoryUpdate.Items.Clear();
        ddlCategoryUpdate.DataSource = Category.Category_OrderByAsc();// Category.Category_SelectAll();
        ddlCategoryUpdate.DataValueField = "ID";
        ddlCategoryUpdate.DataTextField = "Name";
        ddlCategoryUpdate.DataBind();

        ddlCategoryUpdate.Items.Insert(0, new ListItem("Please select...", "0"));
        ddlCategoryUpdate.SelectedValue = setValue.ToString();
        }
        
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
       // ddlCategoryUpdate.AppendDataBoundItems = true;
    }

   private void BindCourse(DropDownList ddlCourse,int categoryid,int setValue)
   {
       try
       {
       ddlCourse.Items.Clear();
       IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(categoryid);
       ddlCourse.DataSource = CE;
       ddlCourse.DataValueField = "ID";
       ddlCourse.DataTextField = "Title";
       ddlCourse.DataBind();

       ddlCourse.Items.Insert(0, new ListItem("Please select...", "0"));
       ddlCourse.SelectedValue = setValue.ToString();
       }
       
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }
   private void BindCategory(DropDownList ddlCategory,int setValue)
   {
       try
       {
       ddlCategory.DataSource = Category.Category_OrderByAsc();// Category.Category_SelectAll();
       ddlCategory.DataValueField = "ID";
       ddlCategory.DataTextField = "Name";
       ddlCategory.DataBind();

       ddlCategory.Items.Insert(0,new ListItem("Please select...","0"));
       ddlCategory.SelectedValue = setValue.ToString();
}
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }

   private void BindEmployee(DropDownList ddlEmployee, int setValue,int ID)
   {
       try
       {
       ddlEmployee.DataSource = Department.DepartmentCustomer_select(ID);
       ddlEmployee.DataValueField = "ID";
       ddlEmployee.DataTextField = "ContractorName";
       ddlEmployee.DataBind();

       ddlEmployee.Items.Insert(0, new ListItem("Please select...", "0"));
       ddlEmployee.SelectedValue = setValue.ToString();
       }
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }
   private void BindStatus()
   {
       try
       {
       ddlStatus.DataSource =Status.SelectAll(true);
       ddlStatus.DataValueField = "value";
       ddlStatus.DataTextField = "text";
       ddlStatus.DataBind();
       }
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }

      
   }
   private void BindOutCome()
   {
       try
       {
       ddlOutCome.DataSource = Outcome.SelectAll(true);
       ddlOutCome.DataValueField = "value";
       ddlOutCome.DataTextField = "text";
       ddlOutCome.DataBind();
       }
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }

    #endregion "Bind Data"
   #region "Bind Filters data"
   private void BindFilteredEmployee(int deptId)
   {
       try
       {
       ddlEmployees.DataSource = Department.DepartmentCustomer_select(deptId);
       ddlEmployees.DataValueField = "ID";
       ddlEmployees.DataTextField = "ContractorName";
       ddlEmployees.DataBind();

       ddlEmployees.Items.Insert(0, new ListItem("Please select...", "0"));
       }
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
      // ddlEmployees.SelectedValue = setValue.ToString();
   }
   private void BindFilteredCourses()
   {
       try
       {
       ddlEmpCourses.DataSource =Course.Course_ByOrderAsc();// Course.Course_SelectAll();
       ddlEmpCourses.DataValueField = "ID";
       ddlEmpCourses.DataTextField = "Title";
       ddlEmpCourses.DataBind();
       ddlEmpCourses.Items.Insert(0, new ListItem("Please select...", "0"));
       }
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }

   //private void BindFilteredData(int deptId, int empId, int courseId, DateTime fromDate, DateTime toDate)
   //{
   //    grd_trBookingRecord.SelectedIndex = -1;
   //    grd_trBookingRecord.DataSource = Bookings.Bookings_SelectByFilters(deptId, empId, courseId, fromDate, toDate);
   //    grd_trBookingRecord.DataBind();
   //}
   private void BindFilteredData(int deptId, int empId, int courseId,int areaId)
   {
       try
       {
           grd_trBookingRecord.SelectedIndex = -1;
           grd_trBookingRecord.DataSource = Bookings.Bookings_SelectByFilter(deptId, empId, courseId, areaId);
           grd_trBookingRecord.DataBind();
       }
        
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }

   private void BindArea()
   {
       try
       {
           ddlArea.DataSource = Area.Area_OrderByAsc(int.Parse(ddlDepartment.SelectedValue));
           ddlArea.DataTextField = "Name";
           ddlArea.DataValueField = "ID";
           ddlArea.DataBind();
           ddlArea.Items.Insert(0, new ListItem("Please select...", "0"));
       }
       catch (Exception ex)
       {
           LogExceptions.WriteExceptionLog(ex);
       }
   }


   private void BindDownloadFiles(int id)
   {
       grdDownloadFiles.DataSource = Bookings.Bookings_SelectFiles(id);
       grdDownloadFiles.DataBind();
   }
   #endregion
   protected void grd_trBookingRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BookingsEntity de = (BookingsEntity)e.Row.DataItem;
            if (de.ID == -99)
            {
                e.Row.Visible = false;
            }
            //de.ID=-99
            //    e.Row.Visible=false;
            //e.Row.Attributes.Add("onclick", "this.style.backgroundColor='BlanchedAlmond'");
            if (e.Row.FindControl("ddlEmployee") != null)
            {
                DropDownList ddlEmployee = (DropDownList)e.Row.FindControl("ddlEmployee");
                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                DropDownList ddlCourse = (DropDownList)e.Row.FindControl("ddlCourse");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                try
                {
                    BindEmployee(ddlEmployee, de.Employee, int.Parse(ddlDepartment.SelectedValue));
                   
                    BindCategory(ddlCategory, de.CategoryID);
                    BindCourse(ddlCourse, de.CategoryID, de.CourseID);

                    ddlStatus.DataSource = Status.SelectAll(true);
                    ddlStatus.DataValueField = "value";
                    ddlStatus.DataTextField = "text";
                    ddlStatus.DataBind();

                    ddlStatus.SelectedValue = de.StatusID.ToString();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
                
                     

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            try
            {

                TextBox txtDateFooter = (TextBox)e.Row.FindControl("txtDateFooter");
                txtDateFooter.Text = DateTime.Now.ToShortDateString();
                DropDownList ddlCategory_footer = (DropDownList)e.Row.FindControl("ddlCategory_footer");
                DropDownList ddlEmployee_footer = (DropDownList)e.Row.FindControl("ddlEmployee_footer");
                DropDownList ddlCourse_footer = (DropDownList)e.Row.FindControl("ddlCourse_footer");
                DropDownList ddlStatus_footer = (DropDownList)e.Row.FindControl("ddlStatus_footer");
                BindCourse(ddlCourse_footer, 0, 0);
                BindCategory(ddlCategory_footer, 0);
                BindEmployee(ddlEmployee_footer, 0, int.Parse(ddlDepartment.SelectedValue));

                ddlStatus_footer.DataSource = Status.SelectAll(true);
                ddlStatus_footer.DataValueField = "value";
                ddlStatus_footer.DataTextField = "text";
                ddlStatus_footer.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
       
        
    }
    protected void ddlCategory_footer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlCategory_footer = (DropDownList)grd_trBookingRecord.FooterRow.FindControl("ddlCategory_footer");
            DropDownList ddlCourse_footer = (DropDownList)grd_trBookingRecord.FooterRow.FindControl("ddlCourse_footer");
            int id = Convert.ToInt32(ddlCategory_footer.SelectedValue);
            IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(id);
            ddlCourse_footer.DataSource = CE;
            ddlCourse_footer.DataValueField = "ID";
            ddlCourse_footer.DataTextField = "Title";
            ddlCourse_footer.DataBind();
            ddlCourse_footer.Items.Insert(0, new ListItem("Please select...", "0"));
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int index=grd_trBookingRecord.EditIndex;
            GridViewRow row=grd_trBookingRecord.Rows[index];

            DropDownList ddlCategory = (DropDownList)row.FindControl("ddlCategory");
            DropDownList ddlCourse = (DropDownList)row.FindControl("ddlCourse");
            int id = Convert.ToInt32(ddlCategory.SelectedValue);
            IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(id);
            ddlCourse.DataSource = CE;
            ddlCourse.DataValueField = "ID";
            ddlCourse.DataTextField = "Title";
            ddlCourse.DataBind();

            //ddlCourse.SelectedValue = booking.CourseID.ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divCourseReOccurence.Visible = false;
            grd_trBookingRecord.SelectedIndex = -1;
            ResetAll();
           
            BindFilteredEmployee(int.Parse(ddlDepartment.SelectedValue));
            BindArea();
            BindFilteredCourses();

            BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue)); 
           // BindData();          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void grdDownloadFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblSlNo = (Label)e.Row.FindControl("lblSlNo");
            //int rowCount = grdDownloadFiles.PageIndex;
            //lblSlNo.Text = (e.Row.RowIndex + 1).ToString();

        }
    }
    protected string  GetFileName(string filePath)
    {
        string Name = Path.GetFileName(filePath);
        return Name;
    }

    protected void grdDownloadFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            try
            {
                IEnumerable<UploadFileEntity> path = Bookings.Bookings_selectFile(int.Parse(e.CommandArgument.ToString()));
                string fileName = "";
                string filePath = "";
                foreach (UploadFileEntity s in path)
                {
                    fileName = s.FileName;
                    filePath = s.FilePath;


                    if (fileName != "" && filePath != "")
                    {
                        string name = Path.GetFileName(filePath);
                        //string extension = Path.GetExtension(filePath);
                        string type = "";
                        string ext = Path.GetExtension(name);
                        switch (ext.ToLower())
                        {
                            case ".txt":
                                type = "text/plain";
                                break;
                            case ".doc":
                            case ".rtf":
                                type = "Application/msword";
                                break;
                            case ".pdf":
                                type = "Application/pdf";
                                break;

                            case ".jpeg":
                                type = "image/pjpeg";
                                break;
                            case ".png":
                                type = "image/png";
                                break;
                            case ".gif":
                                type = "image/gif";
                                break;
                        }


                        HttpContext.Current.Response.ContentType = type;

                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + fileName);

                        HttpContext.Current.Response.WriteFile(MapPath(filePath));

                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "erorr",
                            "<script language='javascript'>alert('Error');</script>");
                    }
                }
            }

            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

    }
    protected void grd_trBookingRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DropDownList ddlCategory_footer = (DropDownList)grd_trBookingRecord.FooterRow.FindControl("ddlCategory_footer");
        DropDownList ddlEmployee_footer = (DropDownList)grd_trBookingRecord.FooterRow.FindControl("ddlEmployee_footer");
        DropDownList ddlCourse_footer = (DropDownList)grd_trBookingRecord.FooterRow.FindControl("ddlCourse_footer");
        DropDownList ddlStatus_footer = (DropDownList)grd_trBookingRecord.FooterRow.FindControl("ddlStatus_footer");
        TextBox txtComments = (TextBox)grd_trBookingRecord.FooterRow.FindControl("txtComments_footer");
        TextBox txtDate = (TextBox)grd_trBookingRecord.FooterRow.FindControl("txtDateFooter");

        if (e.CommandName == "AddNew")
        {

            try
            {
                ResetAll();
                //grd_trBookingRecord.EditIndex = -1;
                BookingsEntity booking = new BookingsEntity();
                CourseEntity course = Course.Course_Select(int.Parse(ddlCourse_footer.SelectedValue));
                //DateTime date = DateTime.Now.Date;
                booking.ID = 0; //Convert.ToInt32(e.CommandArgument);
                booking.BookingDate = Convert.ToDateTime(txtDate.Text);// Convert.ToDateTime(txtBookingDate.Text);
                booking.CategoryID = int.Parse(ddlCategory_footer.SelectedValue);
                booking.CategoryName = ddlCategory_footer.SelectedItem.ToString();
                booking.CheckedBy = 0;// int.Parse(ddlCheckedBy.SelectedValue);
                booking.CheckedByName = "";// ddlCheckedBy.SelectedItem.ToString();
                booking.CheckedDate = Convert.ToDateTime(txtDate.Text);
                booking.Comments = txtComments.Text;

                booking.CostofCourse = course.Rate;// Convert.ToDouble(txtCostOfCourse.Text);
                booking.CourseID = int.Parse(ddlCourse_footer.SelectedValue);
                booking.CourseTitle = ddlCourse_footer.SelectedItem.ToString();
                booking.CourseVenue = "";// txtCourseVenue.Text;
                booking.DateofCourse =Convert.ToDateTime(txtDate.Text);
                booking.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
                booking.DepartmentName = ddlDepartment.SelectedItem.ToString();
                booking.DurationInDays = 0;// int.Parse(txtDurationInDays.Text);
                booking.Employee = int.Parse(ddlEmployee_footer.SelectedValue);
                booking.EmployeeName = ddlEmployee_footer.SelectedItem.ToString();
                booking.EndDate = Convert.ToDateTime(txtDate.Text);// Convert.ToDateTime(txtEndDate.Text);
                booking.EndTime = ""; //txtEndOfTime.Text;
                booking.Expenses = 0.00;// Convert.ToDouble(txtExpenses.Text);
                booking.FeedBack = "";// txtFeedBack.Text;
                booking.FileID = 0;
                booking.Instructor = "";// txtInstructor.Text;
                booking.NotifyDaysPrior = 10;// int.Parse(txtNotifyDaysPrior.Text);
                booking.NotifyUser = "";// int.Parse(lstNotifyUser.SelectedValue);
                booking.NotifyUserName = "";// lstNotifyUser.SelectedItem.ToString();
                booking.Outcome = 0;// int.Parse(ddlOutCome.SelectedValue);
                booking.OutcomeName = "";// ddlOutCome.SelectedItem.ToString();
                booking.Penalties = 0.00;// Convert.ToDouble(txtPenalities.Text);
                booking.RequiredByDate =Convert.ToDateTime(txtDate.Text);//value to be verify
                booking.StartTime = "";// txtStartTime.Text;
                booking.StatusID = int.Parse(ddlStatus_footer.SelectedValue);
                booking.StatusName = ddlStatus_footer.SelectedItem.ToString();
                booking.Budget = course.Rate;
                booking.CourseReoccurs = 0;// int.Parse(txtCourseReoccurs.Text);
                booking.ReFrequencey = 0;// int.Parse(ddlReoccursFrequencey.SelectedValue);
                booking.Day = "";// chkDays.SelectedValue;
                booking.UntilDate = Convert.ToDateTime(txtDate.Text); 
                int exist = Bookings.Bookings_InsertUpdate(booking);
                
                if (exist == 1)
                {
                    lblException.Visible = true;
                    lblException.Text = "Adding new booking failed";
                    lblException.ForeColor = System.Drawing.Color.Red; ;
                    BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
                }
                else
                {
                    lblException.Visible = true;
                    lblException.ForeColor = System.Drawing.Color.Green;
                    lblException.Text = "Your training has been confirmed";
                    SendMail(exist, int.Parse(ddlDepartment.SelectedValue), ddlEmployee_footer.SelectedItem.ToString(), int.Parse(ddlCourse_footer.SelectedValue), Convert.ToDateTime(txtDate.Text));
                    BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
                }
                //BindData();

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        if (e.CommandName == "Select")
        {
            try
            {
                divCourseReOccurence.Visible =false;
                ResetAll();
                grd_trBookingRecord.EditIndex = -1;
                // BindData();
                BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
                //int i =grd_trBookingRecord.EditIndex;
                //GridViewRow row
                lblMessage.Visible = false;
                // BookingsEntity booking = Bookings.Bookings_Select(int.Parse(e.CommandArgument.ToString()));
                BookingsEntity booking = Bookings.Bookings_GetEmployees(int.Parse(e.CommandArgument.ToString()));
                trid.Value = booking.ID.ToString();
                txtBookingDate.Text = string.Format("{0:d}", booking.BookingDate);
                txtCheckedDate.Text = string.Format("{0:d}", booking.CheckedDate);
                txtCostOfCourse.Text = booking.CostofCourse.ToString();
                txtCourseVenue.Text = booking.CourseVenue.ToString();
                txtDurationInDays.Text = booking.DurationInDays.ToString();
                txtEndDate.Text = string.Format("{0:d}", booking.EndDate);
                txtEndOfTime.Text = booking.EndTime;
                txtExpenses.Text = booking.Expenses.ToString();
                txtFeedBack.Text = booking.FeedBack;
                txtInstructor.Text = booking.Instructor;
                txtNotifyDaysPrior.Text = booking.NotifyDaysPrior.ToString();
                txtPenalities.Text = booking.Penalties.ToString();
                txtStartTime.Text = booking.StartTime;
                ddlCheckedBy.SelectedValue = booking.CheckedBy.ToString();
                BindCategory(booking.CategoryID);
                BindCourseUpdate(booking.CategoryID, booking.CourseID);

                //Bind the multiple notify users
                string notifyUsers = string.IsNullOrEmpty(booking.NotifyUser) ? "0" : booking.NotifyUser;
                if (notifyUsers == "0")
                {
                    BindNotifyUsers();
                }
                else
                {
                    string[] ID = notifyUsers.Split(',');
                    BindNotifyUsers();
                    foreach (ListItem i in lstNotifyUser.Items)
                    {
                        if (ID.Length > 0)
                        {
                            for (int k = 0; k < ID.Length; k++)
                            {
                                if (ID[k] == i.Value)
                                {
                                    i.Selected = true;
                                }
                            }
                        }
                    }
                }

                //  lstNotifyUser.SelectedValue = booking.NotifyUser.ToString();//
                ddlOutCome.SelectedValue = booking.Outcome.ToString();
                ddlStatus.SelectedValue = booking.StatusID.ToString();
                txtBudget.Text = booking.Budget.ToString();

                //foreach (ListItem chk in chkDays.Items)
                //{
                //    if (days.Length > 0)
                //    {
                //        for (int k = 0; k < days.Length; k++)
                //        {
                //            if (days[k] == chk.Value)
                //            {
                //                chk.Selected = true;
                //            }
                //        }
                //    }
                //}

                if (booking.StatusID == 2 && booking.FeedBackMail != 1 && booking.BookingID == 0 && booking.CFeedBackID == 0)
                {
                    btnFeedBack.Visible = true;
                    btnCourseFeedback.Visible = true;
                }
                else
                {
                    
                    btnFeedBack.Visible = false;
                    //  btnFeedBack.Visible = true;
                    btnCourseFeedback.Visible = false;
                }

                imgSubmitButton.Focus();
               // CourseReOccurrence ce = CourseRe_Occurr.CourseReOccurr_selectByBooking(booking.ID);
               // if (ce.ID ==0)
               // {
               //     crId.Value ="0";
               // }
               // else
               // {
               //     crId.Value = ce.ID.ToString();

               // }
               //// crId.Value = ce.ID.ToString();
               // txtUntilDate.Text = string.Format("{0:d}", ce.UntilDate);
               // txtCourseReoccurs.Text = ce.CourseReOccurres.ToString();
               // ddlReoccursFrequencey.SelectedValue = ce.ReOccuerFrequencey.ToString();
               // chkDays.SelectedValue = ce.WeekDay.ToString(); 

               
            }

            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        if (e.CommandName == "Clear")
        {
            try
            {
                ddlCategory_footer.SelectedIndex = 0;
                //ddlCourse_footer.SelectedItem = null;
                ddlEmployee_footer.SelectedIndex = 0;
                ddlStatus_footer.SelectedIndex = 0;
                txtComments.Text = "";
                txtDate.Text = "";
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        if (e.CommandName == "Download")
        {

            grd_trBookingRecord.SelectedIndex = -1;
           
            mdlPopUpDownload.Show();
            BindDownloadFiles(int.Parse(e.CommandArgument.ToString()));

        }
        //AddCourse
        if (e.CommandName == "AddCourse")
        {
            modelPopupAddCourse.Show();
        }

        //View feedback form
        if (e.CommandName == "View")
        {
            BookingsEntity booking = Bookings.Bookings_GetEmployees(int.Parse(e.CommandArgument.ToString()));
            if(booking.BookingID!=0)
            {
            //grd_trBookingRecord.SelectedIndex = -1;
            string url="../Training/trViewFeedback.aspx?ID="+e.CommandArgument.ToString();
            ClientScript.RegisterClientScriptBlock(this.GetType(),"PopUp",

                "<script language='javascript'> window.open('"+url+"');</script>");
            }
            if (booking.CFeedBackID!= 0)
            {
                //grd_trBookingRecord.SelectedIndex = -1;
                string url = "../Training/viewcoursefeedback.aspx?ID=" + e.CommandArgument.ToString();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "PopUp",

                    "<script language='javascript'> window.open('" + url + "');</script>");
            }
        }

    }
    protected void grd_trBookingRecord_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ResetAll();
        btnFeedBack.Visible = false;
        btnCourseFeedback.Visible = false;
        grd_trBookingRecord.EditIndex = e.NewEditIndex;

        BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue)); 
       
        
    }
    protected void grd_trBookingRecord_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
           
            DropDownList ddlEmployee = (DropDownList)grd_trBookingRecord.Rows[e.RowIndex].FindControl("ddlEmployee");
            DropDownList ddlCategory = (DropDownList)grd_trBookingRecord.Rows[e.RowIndex].FindControl("ddlCategory");
            DropDownList ddlCourse = (DropDownList)grd_trBookingRecord.Rows[e.RowIndex].FindControl("ddlCourse");
            DropDownList ddlStatus = (DropDownList)grd_trBookingRecord.Rows[e.RowIndex].FindControl("ddlStatus");
            TextBox txtDate = (TextBox)grd_trBookingRecord.Rows[e.RowIndex].FindControl("txtDate");
            TextBox txtComments = (TextBox)grd_trBookingRecord.Rows[e.RowIndex].FindControl("txtComments");
            LinkButton LinkButtonUpdate = (LinkButton)grd_trBookingRecord.Rows[e.RowIndex].FindControl("LinkButtonUpdate");
            
           // BookingsEntity booking = Bookings.Bookings_Select(Convert.ToInt32(LinkButtonUpdate.CommandArgument));

            BookingsEntity booking = Bookings.Bookings_GetEmployees(Convert.ToInt32(LinkButtonUpdate.CommandArgument));

            booking.ID = Convert.ToInt32(LinkButtonUpdate.CommandArgument);
            booking.DateofCourse = Convert.ToDateTime(txtDate.Text);
            booking.CategoryID = int.Parse(ddlCategory.SelectedValue);
            booking.CategoryName = ddlCategory.SelectedItem.ToString();
            booking.CategoryID = int.Parse(ddlCategory.SelectedValue);
            booking.CourseID = int.Parse(ddlCourse.SelectedValue);
            booking.CourseTitle = ddlCourse.SelectedItem.ToString();
            booking.Comments = txtComments.Text;
            booking.StatusID = int.Parse(ddlStatus.SelectedValue);
            booking.StatusName = ddlStatus.SelectedItem.ToString();
            booking.Employee = Convert.ToInt32(ddlEmployee.SelectedValue);
            booking.EmployeeName = ddlEmployee.SelectedItem.ToString();

                      

            int exist=Bookings.Bookings_InsertUpdate(booking);
            if (exist == 1)
            {
                lblException.Visible = true;
                
                lblException.Text = "Updation failed";
                lblException.ForeColor =System.Drawing.Color.Red;
                grd_trBookingRecord.EditIndex = -1;
                ResetAll();
                BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
            }
            else
            {
                lblException.Visible = true;
                lblException.ForeColor = System.Drawing.Color.Green;
                lblException.Text = "Updated successfully";
                grd_trBookingRecord.EditIndex = -1;
                ResetAll();
                BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
            }
            //lblException.Visible = true;
            
           
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

      
    }
    protected void grd_trBookingRecord_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ResetAll();
        grd_trBookingRecord.EditIndex = -1;
       // BindData();
        BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
    }
    protected void imgSubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
           
           

            lblException.Visible = false;
            BookingsEntity booking = Bookings.Bookings_GetEmployees((Convert.ToInt32(trid.Value)));

            booking.ID = Convert.ToInt32(trid.Value);
            booking.BookingDate = Convert.ToDateTime(txtBookingDate.Text);
            booking.CategoryID = int.Parse(ddlCategoryUpdate.SelectedValue);
            booking.CategoryName = ddlCategoryUpdate.SelectedItem.ToString();
            booking.CheckedBy = int.Parse(ddlCheckedBy.SelectedValue);
            booking.CheckedByName = ddlCheckedBy.SelectedItem.ToString();
            booking.CheckedDate = Convert.ToDateTime(txtCheckedDate.Text);
            //booking.Comments = txtComments.Text;
            booking.CostofCourse = Convert.ToDouble(txtCostOfCourse.Text);
            booking.CourseID = int.Parse(ddlCourseUpdate.SelectedValue);
            booking.CourseTitle = ddlCourseUpdate.SelectedItem.ToString();
            booking.CourseVenue = txtCourseVenue.Text;
            //booking.DateofCourse = Convert.ToDateTime(txtDate.Text);
            booking.DepartmentID = int.Parse(ddlDepartment.SelectedValue);
            booking.DepartmentName = ddlDepartment.SelectedItem.ToString();
            booking.DurationInDays = int.Parse(txtDurationInDays.Text);
            //booking.Employee = int.Parse(ddlEmployee.SelectedValue);
            //booking.EmployeeName = ddlEmployee.SelectedItem.ToString();
            booking.EndDate = Convert.ToDateTime(txtEndDate.Text);
            booking.EndTime = txtEndOfTime.Text;
            booking.Expenses = Convert.ToDouble(txtExpenses.Text);
            booking.FeedBack = txtFeedBack.Text;
            booking.FileID = 0;
            booking.Instructor = txtInstructor.Text;
            booking.NotifyDaysPrior = int.Parse(txtNotifyDaysPrior.Text);
            //Multiple selection notify user.
            string notifyUserID = "";
            string notifyUserName = "";
            foreach(ListItem id in lstNotifyUser.Items)
            {
                if (id.Selected)
                {
                    notifyUserID += id.Value +",";
                    notifyUserName += id.Text + ",";
                }
            }


            booking.NotifyUser = notifyUserID;
            booking.NotifyUserName = notifyUserName;
            booking.Outcome = int.Parse(ddlOutCome.SelectedValue);
            booking.OutcomeName = ddlOutCome.SelectedItem.ToString();
            booking.Penalties = Convert.ToDouble(txtPenalities.Text);
            //booking.RequiredByDate = Convert.ToDateTime(txtDate.Text);//value to be verify
            booking.StartTime = txtStartTime.Text;
            booking.StatusID = int.Parse(ddlStatus.SelectedValue);
            booking.StatusName = ddlStatus.SelectedItem.ToString();
            booking.Budget = double.Parse(txtBudget.Text);
            string filePath="";
            //filePath = booking.FileName;
            //if (filePath != "")
            //{
            //    File.Delete(Server.MapPath(filePath));
            //}
            if (bookingFileUpload.HasFile)  //File upload
            {
                UploadFileEntity upFile = Bookings.Booking_select_Files(booking.ID);
                int cnt = Bookings.Bookings_TotalFilesUpload(booking.ID);
                string ext = Path.GetExtension(bookingFileUpload.FileName);
                string fileName = Path.GetFileName(bookingFileUpload.FileName);
                if (cnt == 0)
                {
                    filePath = "~\\WF\\UploadData\\Training\\Booking_" + booking.ID+ext;
                }
                else
                {
                    filePath = "~\\WF\\UploadData\\Training\\Booking_" + booking.ID + "_" + cnt + ext;
                }


                upFile.BookingID = booking.ID;
                upFile.FilePath = filePath;
                upFile.FileName = fileName;
                Bookings.Bookings_UploadFilePaths(upFile);
                bookingFileUpload.SaveAs(Server.MapPath(filePath));
            }
            
            booking.FileName = filePath;
            
            
            //Course re-occurrence 
           
            //CourseReOccurrence ce = new CourseReOccurrence();
            //ce.BookingID = Convert.ToInt32(trid.Value);
            //ce.BoolVal = 0;
            //ce.CourseReOccurres =int.Parse(txtCourseReoccurs.Text);
            //ce.ID = int.Parse(crId.Value);
            //ce.ReOccuerFrequencey = int.Parse(ddlReoccursFrequencey.SelectedValue);
            //ce.UntilDate = Convert.ToDateTime(txtUntilDate.Text);
            //ce.WeekDay = int.Parse(chkDays.SelectedValue);
            //ce.CourseID = booking.CourseID;
            //ce.BookingDate = booking.BookingDate;
            //CourseRe_Occurr.CourseReOccurr_InsertUpdate(ce);
            Bookings.Bookings_InsertUpdate(booking);


          
           
            lblMessage.Visible = true;
            lblException.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Updated successfully";

            int index = grd_trBookingRecord.EditIndex;
            GridViewRow row = grd_trBookingRecord.Rows[index];
            row.BackColor = System.Drawing.Color.BlanchedAlmond;

            //grd_trBookingRecord.EditIndex = -1;
            BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
            imgSubmitButton.Focus();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void ResetAll()
    {
        try
        {
            txtBookingDate.Text = "";
            txtCheckedDate.Text = "";
            txtCostOfCourse.Text = "";
            txtCourseVenue.Text = "";
            txtDurationInDays.Text = "";
            txtEndDate.Text = "";
            txtEndOfTime.Text = "";
            txtExpenses.Text = "";
            txtFeedBack.Text = "";
            txtInstructor.Text = "";
            txtNotifyDaysPrior.Text = "";
            txtPenalities.Text = "";
            txtStartTime.Text = "";
            ddlCheckedBy.SelectedValue = "0";
            //BindNotifyUsers();
            lstNotifyUser.SelectedIndex = -1;
            ddlOutCome.SelectedValue = "0";
            ddlStatus.SelectedValue = "0";
            ddlCategoryUpdate.SelectedIndex = 0;
            ddlCourseUpdate.SelectedIndex = 0;

            chkDays.SelectedIndex = -1;
            ddlReoccursFrequencey.SelectedIndex = -1;
            txtCourseReoccurs.Text = "";
            txtUntilDate.Text = "";
            //BindCategory(0);
            //BindCourseUpdate(0, 0);
            //ddlCourseUpdate.SelectedValue="0";
            txtBudget.Text = "";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgCancel_Click(object sender, EventArgs e)
    {
        grd_trBookingRecord.SelectedIndex = -1;
        grd_trBookingRecord.EditIndex = -1;
        btnFeedBack.Visible = false;
        //  btnFeedBack.Visible = true;
        btnCourseFeedback.Visible = false;
        lblMessage.Visible = false;
        ResetAll();
    }
    protected bool isFileExist(string name)
    {
        bool btnImg = false;
        if (name != "")
        {
            btnImg = true;
        }
        return btnImg;
    }

    protected bool isFeedbackExist(string ID)
    {
        bool btnImg = false;
        btnImg = Bookings.IsFeedBackExist(int.Parse(ID));
        return btnImg;
    }

    protected bool IsMailSent(string ID)
    {
        bool btnImg = false;
      IEnumerable<EmployeeEntity> be= Bookings.Bookings_MailSent(int.Parse(ID));
      foreach (EmployeeEntity mail in be)
      {
          if (mail.FeedBackMail == 1)
          {
              btnImg = true;
              
          }
      }

        return btnImg;
    }


    protected void grd_trBookingRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_trBookingRecord.SelectedIndex = -1;
        grd_trBookingRecord.PageIndex = e.NewPageIndex;
        BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
    }
    protected void imgbtnView_Click(object sender, EventArgs e)
    {
        lblException.Visible = false;
        grd_trBookingRecord.EditIndex = -1;
        //BindFilteredData(int.Parse(ddlDepartment.SelectedValue),int.Parse(ddlEmployees.SelectedValue),int.Parse(ddlEmpCourses.SelectedValue), Convert.ToDateTime(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/1900" : txtFromDate.Text), Convert.ToDateTime(string.IsNullOrEmpty(txtToDate.Text) ? "01/01/1900" : txtToDate.Text));
        BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue),int.Parse(ddlArea.SelectedValue));
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        
        DropDownList ddlCategory=null;
        DropDownList ddlCourse = null;
        try
        {
           
            int courseID = 0;
            int index = grd_trBookingRecord.EditIndex;
            if (index == -1)
            {
                GridViewRow row = grd_trBookingRecord.FooterRow;
                ddlCategory = (DropDownList)row.FindControl("ddlCategory_footer");
                ddlCourse = (DropDownList)row.FindControl("ddlCourse_footer");
            }
            else
            {
                GridViewRow row = grd_trBookingRecord.Rows[index];
                ddlCategory = (DropDownList)row.FindControl("ddlCategory");
                 ddlCourse = (DropDownList)row.FindControl("ddlCourse");
            }
            
            if (int.Parse(ddlCategory.SelectedValue) > 0)
            {
                if (!string.IsNullOrEmpty(txtAddCourse.Text.Trim()))
                {
                    CourseEntity ce = new CourseEntity();
                    //if (int.Parse(ddlCourse.SelectedValue) == 0)
                    //    ce.ID = 0;
                    //else
                    //    ce.ID = int.Parse(ddlCourse.SelectedValue);
                    ce.ID = 0;
                    ce.VendorID = 0;
                    ce.Venue = "";
                    ce.Title = txtAddCourse.Text.Trim();
                    ce.Rate = 0.0;
                    ce.CategoryID = int.Parse(ddlCategory.SelectedValue);
                    ce.Duration = "";
                    ce.TrainingTypeID = 0;
                    ce.TrainingTypeName ="";
                    ce.RequirementID = 0;
                    // ce.RequirementName = ddlRequirement.SelectedItem.ToString();
                     courseID = Course.Course_InsertUpdate(ce);
                                                      

                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Please enter course title";
                }
            }
            else
            {
               lblException.Visible = true;
               lblException.Text = "Please select category";
            }
            //BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
            txtAddCourse.Text = "";
            BindCourse(ddlCourse, int.Parse(ddlCategory.SelectedValue),courseID);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void btnAddFooterCourse_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    int courseID = 0;
        //    int index = grd_trBookingRecord.EditIndex;
        //    GridViewRow row = grd_trBookingRecord.FooterRow;
        //    DropDownList ddlCategory = (DropDownList)row.FindControl("ddlCategory_footer");
        //    DropDownList ddlCourse = (DropDownList)row.FindControl("ddlCourse_footer");
        //    if (int.Parse(ddlCategory.SelectedValue) > 0)
        //    {
        //        if (!string.IsNullOrEmpty(txtAddCourse.Text.Trim()))
        //        {
        //            CourseEntity ce = new CourseEntity();
        //            //if (int.Parse(ddlCourse.SelectedValue) == 0)
        //            //    ce.ID = 0;
        //            //else
        //            //    ce.ID = int.Parse(ddlCourse.SelectedValue);
        //            ce.ID = 0;
        //            ce.VendorID = 0;
        //            ce.Venue = "";
        //            ce.Title = txtAddCourse.Text.Trim();
        //            ce.Rate = 0.0;
        //            ce.CategoryID = int.Parse(ddlCategory.SelectedValue);
        //            ce.Duration = "";
        //            ce.TrainingTypeID = 0;
        //            ce.TrainingTypeName = "";
        //            ce.RequirementID = 0;
        //            // ce.RequirementName = ddlRequirement.SelectedItem.ToString();
        //            courseID = Course.Course_InsertUpdate(ce);


        //        }
        //        else
        //        {
        //            lblError.Visible = true;
        //            lblError.Text = "Please enter course title";
        //        }
        //    }
        //    else
        //    {
        //        lblError.Visible = true;
        //        lblError.Text = "Please select category";
        //    }
        //    //BindCategoryGrid(int.Parse(ddlCategory.SelectedValue));
        //    BindCourse(ddlCourse, int.Parse(ddlCategory.SelectedValue), courseID);

        //}
        //catch (Exception ex)
        //{
        //    LogExceptions.WriteExceptionLog(ex);
        //}
    }

    protected void ddlCategoryUpdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(ddlCategoryUpdate.SelectedValue);
            IEnumerable<CourseEntity> CE = Course.Course_SelectByCategory(id);
            ddlCourseUpdate.DataSource = CE;
            ddlCourseUpdate.DataValueField = "ID";
            ddlCourseUpdate.DataTextField = "Title";
            ddlCourseUpdate.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   
    protected void  btnFeedBack_Click(object sender, EventArgs e)
    {
        try
        {
            string Weburl = System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString();
            string style = mailStyles.MailCss();
            Email mail = new Email();
            BookingsEntity booking = Bookings.Bookings_GetEmployees(Convert.ToInt32(trid.Value));
            string url = "/training/feedback.aspx?ID=" + booking.ID.ToString();
            string body = string.Empty;

            body = string.Format(@"</head>
                                <body>
                                <table align='center' width='600' style='border:1px solid #8595a6; margin-top:10px;' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td height='30' valign='top' class='style1'><img src='{3}'  style='float:left'/>
                                    <table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'>
                                  <tr>
                                    <td class='hdr1'>Course feedback form</td>
                                  </tr>
                                </table> 
                                   </td>
                                  </tr>
                                  <tr>
                                    <td height='9' class='style1' ><img src='{4}' /></td>
                                  </tr>
                                <tr>
                                <td>
                                Dear <b>{0}</b>
                                </td>
                                </tr>
                             <tr><td>Thank you for attending the course on <b>{1}</b> </td></tr>
                            <tr><td>It’s important for us to receive your feedback so that we can improve the way we deliver courses to our staff.</td></tr>
                            <tr><td>We would appreciate if you can complete the following feedback form:</td></tr>
                            <tr><td>For feedback form  <a href='{2}'>Click Here</a></td></tr>
                            <tr><td>Thank you for your feedback</td></tr>
                                                            </table>
                                </body>
                                </html>", booking.EmployeeName, string.Format("{0:d}", booking.DateofCourse), Weburl + url,
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"],
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);


            //body += "<table><tr><td>Dear " + booking.EmployeeName + "</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr><tr><td>Thank you for attending the course on " + string.Format("{0:d}", booking.DateofCourse) + "</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr><tr><td>It’s important for us to receive your feedback so that we can improve the way we deliver courses to our staff.</td></tr>";
            //body += "<tr><td>We would appreciate if you can complete the following feedback form:</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr>";
            //body += "<tr><td>For feedback form  <a href='" + url + "/training/feedback.aspx?ID=" + booking.ID.ToString() + "'>Click Here</a></td></tr>";
            //body += "<tr><td>&nbsp;</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr>";
            //body += "<tr><td>Thank you for your feedback</td></tr>";
            ////http://nhs.deffinity.com/training/feedback.aspx?ID=


            string htmlBody = style + body;
            mail.SendingMail(booking.EmailAddress, "Feedback Form", htmlBody);
            //Update table to indicate mail has been sent.
            Bookings.Bookings_UpdateFeedBackMail(Convert.ToInt32(trid.Value), 1);
            
                btnFeedBack.Visible = false;
                btnCourseFeedback.Visible = false;

            BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnCourseFeedback_Click(object sender, EventArgs e)
    {
        try
        {
            string Weburl = System.Configuration.ConfigurationManager.AppSettings["Weburl"].ToString();
            string style = mailStyles.MailCss();
            Email mail = new Email();
            BookingsEntity booking = Bookings.Bookings_GetEmployees(Convert.ToInt32(trid.Value));
            string url="/training/coursefeedback.aspx?ID="+booking.ID.ToString();
            string body = string.Empty;
            body = string.Format(@"</head>
                                <body>
                                <table align='center' width='600' style='border:1px solid #8595a6; margin-top:10px;' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td height='30' valign='top' class='style1'><img src='{3}'  style='float:left'/> 
                                    <table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'>
                                  <tr>
                                    <td class='hdr1'>Course feedback form</td>
                                  </tr>
                                </table> 
                                   </td>
                                  </tr>
                                  <tr>
                                    <td height='9' class='style1' ><img src='{4}' /></td>
                                  </tr>
                                <tr>
                                <td>
                                Dear <b>{0}</b>
                                </td>
                                </tr>
                             <tr><td>Thank you for attending the course on <b>{1}</b> </td></tr>
                            <tr><td>It’s important for us to receive your feedback so that we can improve the way we deliver courses to our staff.</td></tr>
                            <tr><td>We would appreciate if you can complete the following feedback form:</td></tr>
                            <tr><td>For feedback form  <a href='{2}'>Click Here</a></td></tr>
                            <tr><td>Thank you for your feedback</td></tr>
                                                            </table>
                                </body>
                                </html>", booking.EmployeeName, string.Format("{0:d}", booking.DateofCourse),Weburl+url,
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"],
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

            //body += "<table><tr><td>Dear " + booking.EmployeeName + "</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr><tr><td>Thank you for attending the course on " + string.Format("{0:d}", booking.DateofCourse) + "</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr><tr><td>It’s important for us to receive your feedback so that we can improve the way we deliver courses to our staff.</td></tr>";
            //body += "<tr><td>We would appreciate if you can complete the following feedback form:</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr>";
            //body += "<tr><td>For feedback form  <a href='" + url + "/training/coursefeedback.aspx?ID=" + booking.ID.ToString() + "'>Click Here</a></td></tr>";
            //body += "<tr><td>&nbsp;</td></tr>";
            //body += "<tr><td>&nbsp;</td></tr>";
            //body += "<tr><td>Thank you for your feedback</td></tr>";

            string htmlBody = style + body;
            //http://nhs.deffinity.com/training/feedback.aspx?ID=
            mail.SendingMail(booking.EmailAddress, "Feedback Form", htmlBody);
            //Update table to indicate mail has been sent.
            Bookings.Bookings_UpdateFeedBackMail(Convert.ToInt32(trid.Value), 1);
           
                btnFeedBack.Visible = false;
                btnCourseFeedback.Visible = false;
           
            BindFilteredData(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmployees.SelectedValue), int.Parse(ddlEmpCourses.SelectedValue), int.Parse(ddlArea.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void SendMail(int BookingID,int deptID,string EmployeeName,int courseID,DateTime date )
    {
        try
        {
            string style = mailStyles.MailCss();
            Email mail = new Email();
            BookingsEntity booking = Bookings.Bookings_GetEmployees(BookingID);
            List<NotificationEntity> ne = Notification.Notification_GetManagers(deptID);
            //string ContactNumber = Contractors.Contractors_Contact(employeeID);
            CourseEntity ce = Course.Course_Select(courseID);
            foreach (NotificationEntity Dmanager in ne)
            {

                string body = string.Empty;
                body = string.Format(@"</head>
                                <body>
                                <table align='center' width='600' style='border:1px solid #8595a6; margin-top:10px;' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td height='30' valign='top' class='style1'><img src='{12}'  style='float:left'/> 
                                    <table width='300' border='0' cellspacing='0' cellpadding='0' align='right' style='float:right'>
                                  <tr>
                                    <td class='hdr1'>Training Reminder</td>
                                  </tr>
                                </table> 
                                   </td>
                                  </tr>
                                  <tr>
                                    <td height='9' class='style1' ><img src='{13}' /> </td>
                                  </tr>
                                <tr>
                                <td>
                                Dear <b>{0}</b>
                                </td>
                                </tr>
                                 <tr><td>We'd like to remind you that <b>{1}</b> will be attending the following course due on:{2} </td></tr>
                           <tr><td>Course:{3}</td></tr>
                            <tr><td>This will be held at :<b>{4}</b></td></tr>
                            <tr><td>The cost of this course is :<b>{5}</b></td></tr>
                            <tr><td> {6} will be off for <b>{7}</b> days </td></tr>
                            <tr><td>To contact {8} please call <b>{9}</b> or you can contact the   </td></tr>
                <tr><td>Training Manager <b>{10}</b> on  <b>{11}</b></td></tr>
                                </table>
                                </body>
                                </html>", Dmanager.UserName, EmployeeName, string.Format("{0:d}", date), ce.Title, ce.Venue, ce.Rate,EmployeeName , booking.DurationInDays,
                                        EmployeeName, booking.ContactNumber, Dmanager.ManagerName, Dmanager.ContactNumber,
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["maillogo"],
                                        System.Configuration.ConfigurationManager.AppSettings["Weburl"] + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                string htmlBody = style + body;
               
                //body += "<table><tr><td>Dear " + Dmanager.UserName + "</td></tr>";
                //body += "<tr><td>We'd like to remind you that   " + EmployeeName + "    will be attending the following course due on:  " + string.Format("{0:d}", date) + "   </td></tr>";
                //body += "<tr><td>Course: " + ce.Title + "</td></tr>";
                //body += "<tr><td>This will be held at : " + ce.Venue + "</td></tr>";
                //body += "<tr><td>The cost of this course is : " + ce.Rate + "</td></tr>";
                //body += "<tr><td>  " + EmployeeName + "  will be off for  " + booking.DurationInDays + "  days </td></tr>";
                //body += "<tr><td>To contact  " + EmployeeName + "  please call " + booking.ContactNumber + " or you can contact the   </td></tr>";
                //body += "<tr><td>Training Manager  " + Dmanager.ManagerName + " on " + Dmanager.ContactNumber + "</td></tr>";

                //http://nhs.deffinity.com/training/feedback.aspx?ID=
                mail.SendingMail(Dmanager.Email, "Training Reminder for " + EmployeeName, htmlBody);
            }
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
   
}
