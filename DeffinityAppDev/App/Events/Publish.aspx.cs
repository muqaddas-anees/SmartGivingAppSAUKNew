using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.NetworkInformation;

namespace DeffinityAppDev.App.Events
{
    public partial class Publish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["unid"] == null)
                {
                    huid.Value = Guid.NewGuid().ToString();
                }
                else
                {
                    huid.Value = Request.QueryString["unid"].ToString();
                    displaydata();
                }
            }
        }
        private void displaydata()
        {
            try
            {
                var uqid = huid.Value;
                var ActivityDetail = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(uqid);
                if (ActivityDetail != null)
                {

                    lblEventTitle.Text = ActivityDetail.Title;
                    lblStartDate.Text = ActivityDetail.StartDateTime.ToString("MM/dd/yyyy h:mm tt");
                    lblVenue.Text = ActivityDetail.Venue_Name + " " + ActivityDetail.Address1 + " " + ActivityDetail.Address2 + " " + ActivityDetail.City + " " + ActivityDetail.state_Province + " " + ActivityDetail.Postalcode;

                    //lblTickets.Text = 

                    var p = ActivityDetail.IsPublicEvent;
                    if(p != null)
                    {
                        rdlist.SelectedValue = p.Value ? "1" : "0";
                    }
                    else
                    {
                        rdlist.SelectedValue = "1";
                    }
                    if (ActivityDetail.PublishDate.HasValue)
                    {
                        rplist.SelectedValue = "0";
                        txtStartDate.Text = ActivityDetail.PublishDate.Value.ToShortDateString();
                        txtStartTime.Text = ActivityDetail.PublishDate.Value.ToShortTimeString().Substring(0, 5);
                    }
                    else
                    {
                        rplist.SelectedValue = "1";
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected string GetImmage(object activityid)
        {
            string retval = string.Empty;
            if (activityid != null)
            {
                if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + activityid.ToString() + "/0.png")))
                {
                    retval = "../../WF/UploadData/Events/" + activityid + "/0.png?v=" + DateTime.Now.Ticks;
                }
                else
                {

                    retval = "";
                }
            }
            else
                retval = "";
            return retval;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
        }

        public void saveData()
        {
            try
            {

                var uqid = huid.Value;

                var eEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(uqid);
                if (eEntity == null)
                {
                  

                }
                else
                {
                   
                    if(txtStartDate.Text.Length >0)
                    eEntity.PublishDate = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : txtStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());


                    if (rdlist.SelectedValue == "1")
                        eEntity.IsPublicEvent = true;
                    else
                        eEntity.IsPublicEvent = false;
                    //eEntity.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Value.Trim()) ? txtSlot.Value.Trim() : "0");
                    //eEntity.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Value.Trim()) ? txtPrice.Value.Trim() : "0.00");
                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(eEntity);
                    sessionKeys.Message = "Event updated successfully";
                    Response.Redirect("~/App/Events/Publish.aspx?unid=" + eEntity.unid, false);
                    //if (nav_Showspeckers)
                    //    Response.Redirect("~/App/Events/ManageSpeakers.aspx?unid=" + eEntity.unid, false);

                    //else
                    //    Response.Redirect("~/App/Events/EventList.aspx", false);
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}