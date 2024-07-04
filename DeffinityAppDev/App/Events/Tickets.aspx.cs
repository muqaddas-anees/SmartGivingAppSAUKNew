using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;
using UserMgt.BAL;
using UserMgt.Entity;

namespace DeffinityAppDev.App.Events
{
    public partial class Tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["unid"] != null)
                    {
                        var id = Request.QueryString["unid"].ToString();



//  IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();


// var ActivityDetail = pRep.GetAll().Where(o => o.ID == Aid).FirstOrDefault();
                        var uEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);
                        if (uEntity != null)
                        {
                            hUnid.Value = id;
                            hEventID.Value = uEntity.ID.ToString();
                        }
                        BindSpeakerDetails(id);




                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindSpeakerDetails(string unid)
        {


            var uEntity = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(unid);

            var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();


            var list = cRep.GetAll().Where(o => o.ActivityID == uEntity.ID).ToList();
            hEventID.Value = uEntity.ID.ToString();

            var rList = (from r in list
                         select new
                         {
                             r.ActivityID,
                             r.BookingEndDate,
                             r.BookingStartDate,
                             r.ID,
                             r.MoreDetails,
                             r.Price,
                             PriceDisplay = string.Format("{1}{0:N2}", r.Price, Deffinity.Utility.GetCurrencySymbol()),
            r.Solts,
                             r.TypeOfTicket,
                             r.unid
                         }).ToList();


            BannerList.DataSource = rList;
            BannerList.DataBind();



        }

       

       
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblModelHeading.Text = "Add Ticket";
            txtPrice.Text = "0.00";
            txtSlots.Text = "0";
            txtTicketType.Text = string.Empty;

            mdlAddSpeaker.Show();

            //lblModelHeading.Text = "Edit Banner";
        }
        protected void EditSpeakerinList(object sender, EventArgs e)
        {
            mdlAddSpeaker.Show();

            if (HiddenSpeakerID.Value != null && HiddenSpeakerID.Value != "")
            {



                var sid = HiddenSpeakerID.Value;

                int id = Convert.ToInt32(sid);

                var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                var list = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();



                // "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + list.BannerID + ".png";




                if (list != null)
                {
                    lblModelHeading.Text = "Edit Ticket";
                    txtPrice.Text = string.Format("{0:F2}", list.Price.HasValue ? list.Price.Value : 0);
                    txtSlots.Text = (list.Solts.HasValue ? list.Solts.Value : 0).ToString();
                    txtTicketType.Text = list.TypeOfTicket;
                }


            }







        }
        protected void UploadBanner_Click(object sender, EventArgs e)
        {
            try
            {
                var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();



                var pEntity = new PortfolioMgt.Entity.ActivityTicketSetting();


                var unicId = Guid.NewGuid();

                // Add New Banner

                if (lblModelHeading.Text == "Add Ticket")
                {

                    pEntity.ActivityID = Convert.ToInt32(hEventID.Value);
                    pEntity.unid = hUnid.Value;
                    pEntity.TypeOfTicket = txtTicketType.Text;
                    pEntity.Price = Convert.ToDouble(txtPrice.Text.Trim());
                    pEntity.Solts = Convert.ToInt32(txtSlots.Text);
                    cRep.Add(pEntity);

                }
                else
                {

                    try
                    {

                        int id = Convert.ToInt32(HiddenSpeakerID.Value);

                        var list = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                        if (list != null)
                        {
                            list.TypeOfTicket = txtTicketType.Text;
                            list.Price = Convert.ToDouble(txtPrice.Text.Trim());
                            list.Solts = Convert.ToInt32(txtSlots.Text);

                            cRep.Edit(list);
                        }





                    }

                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }





                }
                if (Request.QueryString["unid"] != null)
                {
                    var id = Request.QueryString["unid"].ToString();
                    BindSpeakerDetails(id);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        protected void BannerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit1")
                {
                    mdlAddSpeaker.Show();

                    var id = Convert.ToInt32(e.CommandArgument.ToString());

                    if (id > 0)
                    {
                        // var sid = HiddenSpeakerID.Value;

                        //int id = Convert.ToInt32(sid);

                        var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                        var list = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                        if (list != null)
                        {
                            // "~/WF/UploadData/OrganizationImages/OrganizationImages_100/" + list.BannerID + ".png";
                            lblModelHeading.Text = "Edit Ticket";
                            txtPrice.Text = string.Format("{0:F2}", list.Price.HasValue ? list.Price.Value : 0);
                            txtSlots.Text = (list.Solts.HasValue ? list.Solts.Value : 0).ToString();
                            txtTicketType.Text = list.TypeOfTicket;
                            HiddenSpeakerID.Value = list.ID.ToString();
                        }


                    }
                }
                else if (e.CommandName == "del")
                {
                    var id = Convert.ToInt32(e.CommandArgument.ToString());
                    var cRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                    var list = cRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
                    if (list != null)
                    {
                        cRep.Delete(list);
                    }
                    
                    }
                BindSpeakerDetails(hUnid.Value);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }



        }


       

    }
}
