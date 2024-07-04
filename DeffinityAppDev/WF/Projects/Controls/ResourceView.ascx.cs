using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ProjectMgt.DAL;
using UserMgt.DAL;
using Deffinity.TrainingEntity;
using Deffinity.TrainingManager;

public partial class controls_ResourceView : System.Web.UI.UserControl
{
    int UID;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            lblYear.Text = "Annual Leave Summary for " + DateTime.Now.Year.ToString();
            if (Request.QueryString["uid"] != null)
            {
                UID = int.Parse(Request.QueryString["uid"]);
                BaseBindings();
                BindTrainingGrid(UID);
                grdUserSkillsBind();
            }
        }

    }
    #region Training Grid
    private void BindTrainingGrid(int Uid)
    {
        try
        {
            //List<v_Training_Booking> trainingRecords = (from r in AdmnCntxt.v_Training_Bookings
            //                                            where r.Employee == Uid
            //                                            select new v_Training_Booking {CategoryName = r.CategoryName, CourseTitle = r.CourseTitle, 
            //                                                StatusName = r.StatusName, DateofCourse = r.DateofCourse, EndDate = r.EndDate }).ToList();


            List<BookingsEntity> trainingRecords = (from r in Bookings.Bookings_SelectAll()
                                                    where r.Employee == Uid
                                                    select new BookingsEntity
                                                    {
                                                        CategoryName = r.CategoryName,
                                                        CourseTitle = r.CourseTitle,
                                                        StatusName = r.StatusName,
                                                        DateofCourse = r.DateofCourse,
                                                        EndDate = r.EndDate
                                                    }).ToList();
            if (trainingRecords != null)
            {
                grdTrainingRecords.DataSource = trainingRecords;
                grdTrainingRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion
    private void BaseBindings()
    {
        grdLeaveBind();
        DisplayImage();
        grdLiveProjects();
        grdPendingProjects();
        GetContractorName();
        AddressBind();
    }
    private void grdLeaveBind()
    {
        try
        {
            projectTaskDataContext ResSearchCntxt = new projectTaskDataContext();
            int ResourceId = int.Parse(Request.QueryString["uid"].ToString());
            grdLeave.DataSource = ResSearchCntxt.ResourceView_Leave(ResourceId);
            grdLeave.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void DisplayImage()
    {
        try
        {
            int UserID = int.Parse(Request.QueryString["uid"].ToString());
            string filepath = Server.MapPath("~/WF/UploadData/Users/ThumbNails/") + "user_" + UID.ToString() + ".png";
        if (System.IO.File.Exists(filepath))
        {
            imgUser.Visible = true;
            imgUser.ImageUrl = string.Format("~/WF/UploadData/Users/ThumbNails/user_{0}.png?date={1}", UserID.ToString(), DateTime.Now.Second.ToString());
            //imgUser.NavigateUrl = string.Format("~/WF/DisplayUser.aspx?userid={0}", UID.ToString());
            imgUser.Target = "_black";
        }
        else
        {
            imgUser.Visible = true;
           
            imgUser.ImageUrl = string.Format("~/WF/UploadData/Users/ThumbNailsMedium/user_0.png?date={0}", DateTime.Now.Second.ToString());
            //imgUser.NavigateUrl = string.Format("~/DisplayUser.aspx?userid={0}", UID.ToString());
            imgUser.Target = "_black";

        }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            
            grdLeave.PageIndex = e.NewPageIndex;
            grdLeaveBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void grdLiveProjects()
    {
        try
        {
            projectTaskDataContext ResSearchCntxt = new projectTaskDataContext();
            int ResourceId = int.Parse(Request.QueryString["uid"].ToString());
            grdLProjects.DataSource = ResSearchCntxt.ResourceView_Projects(UID, 2);
            grdLProjects.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void grdPendingProjects()
    {
        try
        {
            projectTaskDataContext ResSearchCntxt = new projectTaskDataContext();
            int ResourceId = int.Parse(Request.QueryString["uid"].ToString());
            grdPenProjects.DataSource = ResSearchCntxt.ResourceView_Projects(UID, 1);
            grdPenProjects.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdLProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdLProjects.PageIndex = e.NewPageIndex;
        grdLiveProjects();
    }
    private void GetContractorName()
    {
        try
        {
            //int UID = Convert.ToInt32(Request.QueryString["uid"]);
            UserDataContext AdminULnqcntxt = new UserDataContext();
            var Data = (from UA in AdminULnqcntxt.Contractors
                        where UA.ID == UID
                        select new
                        {
                            UA.ContractorName
                        }).Distinct();
            if (Data.Count() > 0)
            {
                foreach (var d in Data)
                {
                    lblResName.Text = d.ContractorName.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void AddressBind()
    {
        try
        {
            //int UID = Convert.ToInt32(Request.QueryString["uid"]);
            UserDataContext AdminULnqcntxt = new UserDataContext();

            var Data = (from UA in AdminULnqcntxt.UserDetails
                        where UA.UserId == UID
                        select new
                        {
                            UA.Address1,
                            UA.Address2,
                            UA.Town,
                            UA.County,
                            UA.PostCode,
                            UA.Country
                        }).Distinct();
           
            if (Data.Count() > 0)
            {
                foreach (var d in Data)
                {
                    lblAddress1.Text = d.Address1.ToString();
                    lblAddress2.Text = d.Address2.ToString();
                    lblTown.Text = d.Town.ToString();
                    lblCounty.Text = d.County.ToString();
                    lblPostcode.Text = d.PostCode.ToString();
                    lbllCountry.Text = Convert.ToString(d.Country.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPenProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPenProjects.PageIndex = e.NewPageIndex;
        grdPendingProjects();
    }
    protected void grdTrainingRecords_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
       
    }
    protected void grdTrainingRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTrainingRecords.PageIndex = e.NewPageIndex;
        if (Request.QueryString["uid"] != null)
        {
            BindTrainingGrid(Convert.ToInt32(Request.QueryString["uid"]));
        }
    }

    private void grdUserSkillsBind()
    {
        try
        {

            UserDataContext AdmnCntxt = new UserDataContext();
            if (Request.QueryString["uid"] != null)
            {
                grdUserSkills.DataSource = AdmnCntxt.UserSkills_SelectByUserid(Convert.ToInt32(Request.QueryString["uid"])).Where(x=> x.Id != -99).ToList();
                grdUserSkills.DataBind();
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
