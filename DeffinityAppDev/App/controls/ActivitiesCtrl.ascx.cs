using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DocumentFormat.OpenXml.Office2010.Excel;
using PortfolioMgt.DAL;

namespace DeffinityAppDev.App.controls
{
    public partial class ActivitiesCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindActiviteis();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        private void BindActiviteis()
        {
            try
            {
                var currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var alist = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectAll();
                if (sessionKeys.PortfolioID > 0)
                    alist = alist.Where(o=>o.StartDateTime >= currentDate).Where(o => o.OrganizationID == sessionKeys.PortfolioID);

                //if(txtDate.Text.Length >0)
                //{
                //    var sdate = Convert.ToDateTime(txtDate.Text.Trim());
                //    alist = alist.Where(o => o.StartDateTime >= sdate && sdate <= o.EndDateTime);
                //}
                var retval = (from a in alist
                              select new
                              {
                                  a.ActivityCategoryID,
                                  a.ActivitySubCategoryID,
                                  a.Address1,
                                  a.Address2,
                                  a.BookingEndDate,
                                  a.BookingStartDate,
                                  a.CategoryName,
                                  a.City,
                                  a.CreatedDate,
                                  Description = Deffinity.Utility.RemoveHTMLTags(a.Description== null?"":a.Description),
                                  a.EndDateTime,
                                  a.Event_Capacity,
                                  a.OrganizationID,
                                  a.OrganizationName,
                                  isInPerson=GetStatus(a.unid),
                                  a.Title,
                                  a.unid,
                                  a.StartDateTime,
                                  



                              }).ToList();

                ListActivites.DataSource = retval.OrderBy(o => o.StartDateTime).ToList();
                ListActivites.DataBind();
                //lblCount.Text = alist.Count().ToString();
                //txtDate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public bool GetStatus(string unid)
        {
            using(var context=new PortfolioDataContext())
            {
                var activity = context.ActivityDetails.FirstOrDefault(o => o.unid == unid);
                if (activity == null)
                {
                    return true;
                }
                else
                {
                    return activity.isInPerson ?? false;
                }
            }
        }
        protected static string GetAddress(object description)
        {
            string retval = "";
            if (description != null)
            {

                if (description.ToString().Length > 300)
                {
                    retval = description.ToString().Substring(0, 290) + "...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            BindActiviteis();
        }
        protected void listamount_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
           
        }
        protected string GetImmage(object activityid)
        {
            //string retval = string.Empty;
            //if (activityid != null)
            //{
            //    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid.ToString() + "/0.png")))
            //    {
            //        // retval = "../../WF/UploadData/Events/" + activityid + "/0.png";
            //        retval = "~/WF/UploadData/Events/" + activityid + "/0.png";
            //    }
            //    else
            //    {

            //        retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //    }
            //}
            //else
            //   retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //return retval;
            return "../../ImageHandler.ashx?id=" + activityid + "&s=" + ImageManager.file_section_event; //
        }

        protected void ListActivites_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string v = e.CommandArgument.ToString();
                if (v.Length > 0)
                {

                    Response.Redirect("~/EventDetailsNew.aspx?unid=" + v, false);
                }
                //txtOtherAmount.Text = e.CommandArgument.ToString().Trim();
                // mdlManageOptions.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}