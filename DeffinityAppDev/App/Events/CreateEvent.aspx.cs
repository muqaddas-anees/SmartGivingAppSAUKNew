using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class CreateEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["unid"] == null)
                    {
                        huid.Value = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        huid.Value = Request.QueryString["unid"].ToString();
                    }


                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


       


        protected void Submit(object sender, EventArgs e)
        {
            try
            {
                var uqid = huid.Value;
                var ActivityDetail = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(uqid);
                if (ActivityDetail == null)
                {


                    txtEventName.Value = ActivityDetail.Title;
                    txtDescription.Text = ActivityDetail.Description;
                   
                    txtStartDate.Text = ActivityDetail.StartDateTime.ToShortDateString();
                    txtEndDate.Text = ActivityDetail.EndDateTime.ToShortDateString();
                    txtStartTime.Text = ActivityDetail.StartDateTime.ToShortTimeString().Substring(0, 5);
                    txtEndTime.Text = ActivityDetail.EndDateTime.ToShortTimeString().Substring(0, 5);



                    txtBookingStartDate.Text = ActivityDetail.StartDateTime.ToShortDateString();
                    txtBookingEndDate.Text = ActivityDetail.EndDateTime.ToShortDateString();
                    txtBookingStartTime.Text = ActivityDetail.StartDateTime.ToShortTimeString().Substring(0, 5);
                    txtBookingEndTime.Text = ActivityDetail.EndDateTime.ToShortTimeString().Substring(0, 5);

                    //ckbIsActive.Checked = ActivityDetail.IsActive;
                    txtSlot.Value = ActivityDetail.Slots.ToString();
                    txtPrice.Value = string.Format("{0:F2}", ActivityDetail.Price);

                    txtvenuename.Value = ActivityDetail.Venue_Name;
                    txtAddress1.Value = ActivityDetail.Address1;
                    txtAddress2.Value = ActivityDetail.Address2;
                    txtCity.Value = ActivityDetail.City;
                    txtCountry.Value = ActivityDetail.Country;
                    txtZipcode.Value = ActivityDetail.Postalcode;
                    txtState.Value = ActivityDetail.state_Province;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
        }

        protected void EventDisplay()
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveData(true);
        }

        public void saveData(bool nav_Showspeckers)
        {
            try
            {

                var uqid = huid.Value;

                var eEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(uqid);
                if (eEntity == null)
                {
                    eEntity = new PortfolioMgt.Entity.ActivityDetail();
                    eEntity.unid = uqid;
                    eEntity.OrganizationID = sessionKeys.PortfolioID;
                    eEntity.ActivityCategoryID = 0;
                    eEntity.ActivitySubCategoryID = 0;
                    eEntity.StartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : txtStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.EndDateTime = Convert.ToDateTime(txtEndDate.Text + " " + (string.IsNullOrEmpty(txtEndTime.Text) ? "00:00:00" : txtEndTime.Text + ":00"));//Convert.ToDateTime(!string.IsNullOrEmpty( TextEndDate.Text.Trim())? TextEndDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.StartDateTime = Convert.ToDateTime(txtBookingStartDate.Text + " " + (string.IsNullOrEmpty(txtBookingStartTime.Text) ? "00:00:00" : txtBookingStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.EndDateTime = Convert.ToDateTime(txtBookingEndDate.Text + " " + (string.IsNullOrEmpty(txtBookingEndTime.Text) ? "00:00:00" : txtBookingEndTime.Text + ":00"));
                    eEntity.Title = txtEventName.Value.Trim();
                    eEntity.Address1 = txtAddress1.Value.Trim();
                    eEntity.Address2 = txtAddress2.Value.Trim();
                    eEntity.City = txtCity.Value.Trim();
                    eEntity.Country = txtCountry.Value.Trim();
                    eEntity.state_Province = txtState.Value.Trim();
                    eEntity.Venue_Name = txtvenuename.Value.Trim();
                    eEntity.Postalcode = txtZipcode.Value.Trim();

                    eEntity.Description = txtDescription.Text;
                    // value.Notes = txtNotes.Text;
                    eEntity.ModifiedDate = DateTime.Now;
                    eEntity.IsActive = true;
                    eEntity.CreatedDate = DateTime.Now;
                    eEntity.LoggedBy = sessionKeys.UID;
                    eEntity.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Value.Trim()) ? txtSlot.Value.Trim() : "0");
                    eEntity.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Value.Trim()) ? txtPrice.Value.Trim() : "0.00");
                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Add(eEntity);

                    sessionKeys.Message = "Event added successfully";
                    if( nav_Showspeckers)
                        Response.Redirect("~/App/Events/ManageSpeakers.aspx?unid="+eEntity.unid, false);

                    else
                    Response.Redirect("~/App/Events/EventList.aspx", false);

                }
                else
                {
                    eEntity.OrganizationID = sessionKeys.PortfolioID;
                    eEntity.ActivityCategoryID = 0;
                    eEntity.ActivitySubCategoryID = 0;
                    eEntity.StartDateTime = Convert.ToDateTime(txtStartDate.Text + " " + (string.IsNullOrEmpty(txtStartTime.Text) ? "00:00:00" : txtStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.EndDateTime = Convert.ToDateTime(txtEndDate.Text + " " + (string.IsNullOrEmpty(txtEndTime.Text) ? "00:00:00" : txtEndTime.Text + ":00"));//Convert.ToDateTime(!string.IsNullOrEmpty( TextEndDate.Text.Trim())? TextEndDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.StartDateTime = Convert.ToDateTime(txtBookingStartDate.Text + " " + (string.IsNullOrEmpty(txtBookingStartTime.Text) ? "00:00:00" : txtBookingStartTime.Text + ":00"));// Convert.ToDateTime(!string.IsNullOrEmpty( txtStartDate.Text.Trim())? txtStartDate.Text.Trim():DateTime.Now.ToShortDateString());
                    eEntity.EndDateTime = Convert.ToDateTime(txtBookingEndDate.Text + " " + (string.IsNullOrEmpty(txtBookingEndTime.Text) ? "00:00:00" : txtBookingEndTime.Text + ":00"));
                    eEntity.Title = txtEventName.Value.Trim();
                    eEntity.Address1 = txtAddress1.Value.Trim();
                    eEntity.Address2 = txtAddress2.Value.Trim();
                    eEntity.City = txtCity.Value.Trim();
                    eEntity.Country = txtCountry.Value.Trim();
                    eEntity.state_Province = txtState.Value.Trim();
                    eEntity.Venue_Name = txtvenuename.Value.Trim();
                    eEntity.Postalcode = txtZipcode.Value.Trim();

                    eEntity.Description = txtDescription.Text;
                    // value.Notes = txtNotes.Text;
                    eEntity.ModifiedDate = DateTime.Now;
                    eEntity.IsActive = true;
                    eEntity.CreatedDate = DateTime.Now;
                    eEntity.LoggedBy = sessionKeys.UID;
                    eEntity.Slots = Convert.ToInt32(!string.IsNullOrEmpty(txtSlot.Value.Trim()) ? txtSlot.Value.Trim() : "0");
                    eEntity.Price = Convert.ToDouble(!string.IsNullOrEmpty(txtPrice.Value.Trim()) ? txtPrice.Value.Trim() : "0.00");
                    PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_Update(eEntity);
                    sessionKeys.Message = "Event updated successfully";
                    if (nav_Showspeckers)
                        Response.Redirect("~/App/Events/ManageSpeakers.aspx?unid=" + eEntity.unid, false);

                    else
                        Response.Redirect("~/App/Events/EventList.aspx", false);
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddSpeakers_Click(object sender, EventArgs e)
        {
            saveData(true);
           // Response.Redirect("~/App/Events/EventList.aspx", false);
        }
    }
}