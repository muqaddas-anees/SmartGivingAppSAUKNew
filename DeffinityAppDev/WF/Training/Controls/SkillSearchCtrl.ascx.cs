using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.Entity;
using Training.DAL;
using Training.Entity;
using Training.BAL;

public partial class Training_controls_SkillSearchCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindUser();
                BindCourse();
                BindCountry();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindUser()
    {
        ddlUser.DataSource = TrainingManagerBAL.GetUserList();
        ddlUser.DataValueField = "ID";
        ddlUser.DataTextField = "ContractorName";
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem("Please select...", "0"));

    }

    private void BindCourse()
    {
        //lstBoxCourse.DataSource = TrainingManagerBAL.CourseList();
        //lstBoxCourse.DataValueField = "ID";
        //lstBoxCourse.DataTextField = "Title";
        //lstBoxCourse.DataBind();


        chkSkills.DataSource = TrainingManagerBAL.CourseList();
        chkSkills.DataValueField = "ID";
        chkSkills.DataTextField = "Title";
        chkSkills.DataBind();

    }

    private void BindSkills()
    {

    }

    private void BindCountry()
    {
        try
        {
            using (Location.DAL.LocationDataContext db = new Location.DAL.LocationDataContext())
            {
                var Data = (from c in (db.CountryClasses)
                            select new
                            {
                                c.ID,
                                c.Country1

                            }).Distinct();
                ddlCountry.DataSource = Data.OrderBy(o => o.Country1);
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
            
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);


        }

    }

    private void BindGrid()
    {
       

        try
        {
            List<int> courseIds = new List<int>();
            foreach (ListItem i in chkSkills.Items)
            {
                if (i.Selected)
                    courseIds.Add(Convert.ToInt32(i.Value));
            }
            using (TrainingDataContext td = new TrainingDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    using (Location.DAL.LocationDataContext ld = new Location.DAL.LocationDataContext())
                    {

                        int userId = Convert.ToInt32(ddlUser.SelectedValue);
                        int countryId = Convert.ToInt32(ddlCountry.SelectedValue);
                        var userList = ud.Contractors.Where(c => c.Status.ToLower() == "active").ToList();
                        var contractorDetailsList = ud.ContractorDetails.ToList();
                        var countryList = ld.CountryClasses.ToList();
                        countryList.Add(new Location.Entity.CountryClass { ID = 0, Country1 = "" });
                        var bookingList = td.Bookings.ToList();
                        var courseList = td.Courses.ToList();
                        var result = (from b in bookingList
                                      join u in userList on b.Employee equals u.ID
                                      join c in courseList on b.CourseID equals c.ID
                                      join cd in contractorDetailsList on b.Employee equals cd.UserID into f
                                      from cod in f.DefaultIfEmpty()
                                      join cn in countryList on cod == null ? 0 : cod.Country equals cn.ID
                                      //where b.Employee == Convert.ToInt32(ddlUser.SelectedValue)
                                      select new
                                      {
                                          ID = b.ID,
                                          CourseID = b.CourseID,
                                          CountryID = cod == null ? 0 : cod.Country,
                                          Country = cn.Country1,
                                          CourseSkill = c.Title,
                                          From = b.DateofCourse,
                                          To = b.EndDate,
                                          UserID = b.Employee,
                                          User = u.ContractorName
                                      }).ToList();
                        if (userId > 0)
                            result = result.Where(r => r.UserID == userId).ToList();
                        if (courseIds.Count > 0)
                            result = result.Where(r => courseIds.Contains(Convert.ToInt32(r.CourseID))).ToList();

                        if (countryId > 0)
                            result = result.Where(r => r.CountryID == countryId).ToList();

                        if (!string.IsNullOrEmpty(txtFromDate.Text))
                            result = result.Where(r => r.From >= Convert.ToDateTime(txtFromDate.Text)).ToList();

                        if (!string.IsNullOrEmpty(txtToDate.Text))
                            result = result.Where(r => r.To <= Convert.ToDateTime(txtToDate.Text)).ToList();


                        gvSkills.DataSource = result;
                        gvSkills.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);            
        }

       
        
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void gvSkills_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSkills.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}