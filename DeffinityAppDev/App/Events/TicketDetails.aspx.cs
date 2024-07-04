using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DeffinityAppDev.App.Events
{
    public partial class TicketDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    var booking_unid = QueryStringValues.UNID;

                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                    var pblist = pbRep.GetAll().Where(o => o.UserBookingUNID == booking_unid).FirstOrDefault();


                    booking_unid = pblist.BookingUNID;
                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem>();

                    var blist = pRep.GetAll().Where(o => o.unid == booking_unid).ToList();

                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> bRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                    var bookentity = bRep.GetAll().Where(o => o.unid == booking_unid).FirstOrDefault();


                    IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> eRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                    var eventdetails = eRep.GetAll().Where(o => o.ID == bookentity.ActivityID).FirstOrDefault();

                    hevent_unid.Value = eventdetails.unid;

                    img_event.Src= "~/ImageHandler.ashx?id=" + hevent_unid.Value.ToString() + "&s=" + ImageManager.file_section_event;

                    var portfolioDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.v_ProjectPortfolioBAL_SelectAll().Where(o => o.ID == eventdetails.OrganizationID).FirstOrDefault();
                    lblCompany.Text = portfolioDetails.PortFolio;


                    lblUserName1.Text = pblist.UserName;
                    lblDateTime1.Text = string.Format("{0:dd/MM/yyyy hh:mm a}", eventdetails.StartDateTime);


                    lblEvent.Text = eventdetails.Title;
                    lblEventName.Text = eventdetails.Title;
                    lblDateTime.Text = string.Format("{0:dd/MM/yyyy hh:mm tt} - {1:dd/MM/yyyy hh:mm tt}", eventdetails.StartDateTime, eventdetails.EndDateTime);
                    lblVenue.Text = string.Format("{0} {1} {2} {3} {4} {5}", eventdetails.Venue_Name, eventdetails.Address1, eventdetails.Address2, eventdetails.City, eventdetails.state_Province, eventdetails.Postalcode);
                    lblEventTickets.Text = eventdetails.Title;
                    lblContactName.Text = pblist.UserName;
                    var email = bookentity.ContactEmail;
                    var list = pbRep.GetAll().Where(o => o.UserBookingUNID == pblist.UserBookingUNID).ToList();
                    grid.DataSource = list;
                    grid.DataBind();
                    lblbookingid.Text = bookentity.ID.ToString("D6");

                    imgqrcode.ImageUrl = "~/WF/UploadData/Events/" + pblist.UserBookingUNID.ToString() + ".png";


                    if (QueryStringValues.Type == "validate")
                    {
                        var p = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();
                        var pupdate = pbRep.GetAll().Where(o => o.UserBookingUNID == QueryStringValues.UNID).FirstOrDefault();
                        if (pupdate != null)
                        {
                            pupdate.ValidatedDateTime = DateTime.Now;
                            pupdate.EventStatus = "Attended";

                            pbRep.Edit(pupdate);

                            pnlVlidateDetails.Visible = true;
                            pnlTicketDetails.Visible = false;
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected string GetImmage()
        {
            //string retval = string.Empty;
          
            //    if (File.Exists(Server.MapPath("~/WF/UploadData/Events/" + hevent_unid.Value.ToString() + "/0.png")))
            //    {
            //        retval = "../../WF/UploadData/Events/" + hevent_unid.Value + "/0.png";
            //    }
            //    else
            //    {

            //        retval = "../../WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            //    }
           
            //return retval;
            return "~/ImageHandler.ashx?id=" + hevent_unid.Value.ToString() + "&s=" + ImageManager.file_section_event;
        }
    }
}