using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using QRCoder;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Drawing;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using static DC_FLS_ctrl;
using DeffinityManager.BLL ;
using Incidents.Entity;
using static DeffinityManager.DAL.ServiceCatalogueMaterials;
using DeffinityAppDev.App.WebService;
using System.Web.Security;
using UserMgt.BAL;
using UserMgt.Entity;
using System.Net;
using DeffinityManager.WF.CustomerAdmin.PayPalPayflowPro;
using System.Data;

namespace DeffinityAppDev.App.Events
{
    public partial class ManageTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    SetCardFeePercentage();

                    string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                    ddlMonth.DataSource = Month;
                    ddlMonth.DataBind();
                    //pre-select one for testing
                    ddlMonth.SelectedIndex = 4;

                    //populate year
                    ddlYear.Items.Add("");
                    int Year = DateTime.Now.Year;
                    for (int i = 0; i < 10; i++)
                    {
                        ddlYear.Items.Add((Year + i).ToString());
                    }
                    //pre-select one for testing
                    ddlYear.SelectedIndex = 3;



                    BingListview();

                    hbook_unid.Value = Guid.NewGuid().ToString();
                    GetImmage();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void SetCardFeePercentage()
        {
            try
            {
                hfeepercent.Value = "0.00";
                var pSettings = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(p => p.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                if (pSettings != null)
                {
                    //hfeepercent.Value = string.Format("{0:F2}", (pSettings.CardFee.HasValue ? pSettings.CardFee.Value : 0));
                    hfeepercent.Value = string.Format("{0:F2}", (0));
                    hplatformfee.Value = string.Format("{0:F2}", (pSettings.TransactionFee.HasValue ? pSettings.TransactionFee.Value : 0));
                    hplatformfeepercent.Value = string.Format("{0:F2}", (pSettings.TransactionFee.HasValue ? pSettings.TransactionFee.Value : 0));
                    // hfixedamount.Value = string.Format("{0:F2}", (pSettings.FixedPrice.HasValue ? pSettings.FixedPrice.Value : 0));
                    hfixedamount.Value = string.Format("{0:F2}", (0));
                    LogExceptions.LogException("Platform fee percent:" + hplatformfeepercent.Value);

                    lblplatformfee.Text = string.Format("Platform Fee ({0}%)", (pSettings.TransactionFee.HasValue ? pSettings.TransactionFee.Value : 0));
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void GetImmage()
        {
            object activityid = hunid.Value;
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
            //    retval = "~/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png";
            // return retval;
            img_event.ImageUrl = "~/ImageHandler.ashx?id=" + activityid.ToString() + "&s=" + ImageManager.file_section_event; //retval;

        }
        protected static string GetDescription(object description)
        {
            string retval = "";
            if (description != null)
            {
                if (description.ToString().Length > 500)
                {
                    retval = description.ToString().Substring(0, 490) + "...";
                }
                else
                    retval = description.ToString();
            }

            return retval;
        }
        protected void BingListview()
        {
            try
            {
                if (Request.QueryString["unid"] != null)
                {
                    var id = Request.QueryString["unid"].ToString();
                    hunid.Value = id;
                    IPortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail>();

                    var ActivityDetail = pRep.GetAll().Where(o => o.unid == id).FirstOrDefault();

                    IPortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting> aRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityTicketSetting>();

                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> pbRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> bRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                    var bookinglist = bRep.GetAll().Where(o => o.ActivityID == ActivityDetail.ID).ToList();

                    var pblist = pbRep.GetAll().Where(o => bookinglist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "").Where(o => (o.BookingPrice.HasValue ? o.BookingPrice.Value : 0) == 0).ToList();

                    var bNewlist = bookinglist.Where(o => (o.TotalAmount.HasValue ? o.TotalAmount.Value : 0) > 0).Where(o => (o.IsPaid.HasValue ? o.IsPaid.Value : false) == true).ToList();
                    if (bNewlist.Count() > 0)
                    {
                        pblist.AddRange(pbRep.GetAll().Where(o => bNewlist.Select(b => b.unid).Contains(o.BookingUNID)).Where(o => o.UserName != "").Where(o => o.UserEmail != "").ToList());
                    }


                  
                    sessionKeys.PortfolioID = ActivityDetail.OrganizationID;
                    lblTitle.Text = ActivityDetail.Title;

                    var tlist = aRep.GetAll().Where(o => o.unid == id).ToList();

                    if (tlist.Count >0)
                    {

                        var rlist = (from r in tlist
                                     select new
                                     {
                                         r.ActivityID,
                                         r.BookingEndDate,
                                         r.BookingStartDate,
                                         r.ID,
                                         r.MoreDetails,
                                         r.Price,
                                         r.Solts,
                                         r.TypeOfTicket,
                                         r.unid,
                                         BookedSlots = pblist.Where(o => o.TicketType == r.TypeOfTicket).Count(),
                                         RemainingSlots = r.Solts ?? 0 - pblist.Where(o => o.TicketType == r.TypeOfTicket).Count(),
                                         RemainingSlotsText = (r.Solts ?? 0 - pblist.Where(o => o.TicketType == r.TypeOfTicket).Count()).ToString() + " slot(s) available",
                                     }).ToList();

                        // hamount.Value = ActivityDetail.FirstOrDefault().Price.ToString();
                        //hunid.Value = ActivityDetail.FirstOrDefault().unid.ToString();
                        listCategory.DataSource = rlist;
                        listCategory.DataBind();
                    }

                   // var payRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                    LogExceptions.LogException("sessionKeys.PortfolioID: " + sessionKeys.PortfolioID);
                    //var payDetials = payRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                    //if (payDetials != null)
                    //    tokenFrame.Src = payDetials.Host + "/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void listCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Find the TextBox control in the current row
              //  TextBox txtQuantity = e.Item.FindControl("txtQuantity") as TextBox;
                RangeValidator rg = e.Item.FindControl("rangeValidator") as RangeValidator;

                if (rg != null)
                {
                    // Retrieve the data item for the current row
                    dynamic dataItem = e.Item.DataItem as dynamic;

                    // Assuming you have a column named "ConditionColumn" in your database
                    // You can replace this with your actual column name
                    var d = dataItem.RemainingSlots.ToString();
                    int RemainingSlots = Convert.ToInt32(d);

                    rg.MaximumValue = RemainingSlots.ToString();

                    if(rg.MaximumValue != "0")
                    {
                        if (Convert.ToInt32(RemainingSlots) > 1)
                        {
                            rg.ErrorMessage = "Please enter a number between 1 and " + RemainingSlots.ToString() + ".";
                        }
                    }

                  
                }
            }
        }
        protected void btnSaveRegion_Click(object sender, EventArgs e)
        {

        }
        public string gettoalcost(int bookingid)
        {
            string retval = "";

            try
            {
               // double retval = ValueTuple;
                foreach (ListViewItem item in listCategory.Items)
                {
                    // Label lbl = (Label)item.FindControl("lblCategory");
                    HtmlInputGenericControl txtamount = (HtmlInputGenericControl)item.FindControl("txtamount_list");
                    HtmlInputText txtqty = (HtmlInputText)item.FindControl("hprice_ctrl");

                    try
                    {
                        IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem>();

                        var ab = new PortfolioMgt.Entity.ActivityBooking();
                        if (Convert.ToDouble(txtamount.Value.Trim()) > 0)


                        {

                          // lblTotal = Convert.ToDouble(txtamount.Value.Trim()) * Convert.ToDouble(txtqty.Value.Trim())
                        }
                        // retval = txtamount.Value ;
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    // Response.Write(str);

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }

        public string UpdateBookingSlots(int bookingid)
        {
            string retval = "";

            try
            {
                int personid = 0;
                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();
                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItem>();
                foreach (ListViewItem item in listCategory.Items)
                {

                    // Label lbl = (Label)item.FindControl("lblCategory");
                    HtmlInputGenericControl txtamount = (HtmlInputGenericControl)item.FindControl("txtamount_list");
                    HtmlInputHidden txtqty = (HtmlInputHidden)item.FindControl("hprice_ctrl");
                    Label lbltypeofticket = (Label)item.FindControl("lblCategory");

                    try
                    {
                       

                        var ab = new PortfolioMgt.Entity.ActivityBooking();
                        try
                        {
                            if ((Convert.ToInt32(txtamount.Value.Trim())) > 0)
                            {
                                var pslots = pRep.GetAll().Where(o => o.unid == hbook_unid.Value.ToString() && o.TypeOfTicket == lbltypeofticket.Text.Trim()).FirstOrDefault();

                                if (pslots == null)
                                {
                                     pslots = new PortfolioMgt.Entity.ActivityBookingItem()
                                    {
                                        BookedSolts = Convert.ToInt32(txtamount.Value.Trim()),
                                        BookingID = bookingid,
                                        TypeOfTicket = lbltypeofticket.Text,
                                        Price = Convert.ToDouble(txtqty.Value.Trim()),
                                        unid = hbook_unid.Value.ToString(),
                                        TotalAmount = Convert.ToDouble(txtqty.Value.Trim()) * Convert.ToInt32(txtamount.Value.Trim()),
                                        IsPaid = false

                                    };
                                    pRep.Add(pslots);
                                }
                                else
                                {
                                    pslots.BookedSolts = Convert.ToInt32(txtamount.Value.Trim());
                                    pslots.Price = Convert.ToDouble(txtqty.Value.Trim());
                                    pslots.TotalAmount = Convert.ToDouble(txtqty.Value.Trim()) * Convert.ToInt32(txtamount.Value.Trim());
                                    pRep.Edit(pslots);
                                }


                                //update the user details

                                var totalTickets = puRep.GetAll().Where(o => o.BookingUNID == hbook_unid.Value.ToString() && o.TicketType == lbltypeofticket.Text).Count();

                                var bookedTickets = Convert.ToInt32(txtamount.Value.Trim());
                                var deleteTickets = 0;

                                if (totalTickets == 0)
                                    totalTickets = bookedTickets;
                                else
                                {
                                    if (bookedTickets > totalTickets)
                                        totalTickets = bookedTickets - totalTickets;
                                    else
                                    {
                                        totalTickets = 0;
                                        deleteTickets = bookedTickets - totalTickets;
                                    }
                                }
                                //Delete the exes entities
                                if(deleteTickets >0)
                                {
                                    for (int i = 1; i <= deleteTickets; i++)
                                    {
                                        var delEntity = puRep.GetAll().Where(o => o.BookingUNID == hbook_unid.Value.ToString() && o.TicketType == lbltypeofticket.Text).LastOrDefault();

                                        if (delEntity != null)
                                        {
                                            puRep.Delete(delEntity);
                                        }
                                    }
                                }
                                //add tickets
                                for (int i=1;i<= totalTickets; i++ )
                                {

                                    var userUnid = Guid.NewGuid().ToString();
                                    var uEntity = new PortfolioMgt.Entity.ActivityBookingItemUserDetail()
                                    {
                                        TicketType = lbltypeofticket.Text,
                                        BookingID = bookingid,
                                        BookingUNID = hbook_unid.Value.ToString(),
                                        UserBookingUNID = userUnid,
                                        EventStatus = "Pending",
                                        UserName = "",
                                        UserEmail = "",
                                        UserContact = "+27",
                                        BookingPrice = Convert.ToDouble(txtqty.Value.Trim())

                                    };
                                    puRep.Add(uEntity);


                                    string code = Deffinity.systemdefaults.GetWebUrl() + "/App/Events/TicketDetails.aspx?type=validate&unid=" + uEntity.UserBookingUNID; //ab.MoreDetails;


                                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                                    QRCode qrCode = new QRCode(qrCodeData);
                                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                                    imgBarCode.Height = 150;
                                    imgBarCode.Width = 150;
                                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                            byte[] byteImage = ms.ToArray();
                                            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                            img.Save(Server.MapPath("~/WF/UploadData/Events/") + uEntity.UserBookingUNID + ".png", System.Drawing.Imaging.ImageFormat.Png);
                                          //  imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                        }
                                       // plBarCode.Controls.Add(imgBarCode);
                                    }
                                }

                            }

                          
                        }
                        catch(Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                           // retval = txtamount.Value ;
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    // Response.Write(str);

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            //Update the tickets
            try
            {
                if (Request.QueryString["unid"] != null)
                {
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    string retval = "";
                    string rettext = "";
                    var id = Request.QueryString["unid"].ToString();
                    var uDetails = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);

                    IPortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail> aRep = new PortfolioRepository<PortfolioMgt.Entity.V_ActivityDetail>();

                    var ActivityDetail = aRep.GetAll().Where(o => o.unid == id).FirstOrDefault();
                    sessionKeys.PortfolioID = ActivityDetail.OrganizationID;
                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                    var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o => o.IsActive == true).FirstOrDefault();


                    var bookingDetails = pRep.GetAll().Where(o => o.unid == hbook_unid.Value).FirstOrDefault();
                    LogExceptions.LogException("total amount :" + hbook_unid.Value.ToString());
                    LogExceptions.LogException("total amount :" + (bookingDetails.TotalAmount.HasValue ? bookingDetails.TotalAmount.Value : 0).ToString());
                    if ((bookingDetails.TotalAmount.HasValue ? bookingDetails.TotalAmount.Value : 0) > 0)
                    {
                        // var ab = new PortfolioMgt.Entity.ActivityBooking();

                        var payRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioPaymentSetting>();
                        var payDetials = payRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();

                        var payref = Deffinity.Utility.GetSevenCharRandomString();

                        LogExceptions.LogException("start credit card processign :");

                        string retref = "";
                        string profileid = "";
                        LogExceptions.LogException("my token :" + mytoken.Value);
                        var ret = DC.BLL.QuickPayBAL.FundrisersProcessPayment(ActivityDetail.OrganizationID, ddlCardType.SelectedValue, mytoken.Value, ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2),
                           Convert.ToDouble((bookingDetails.TotalAmount.HasValue ? bookingDetails.TotalAmount.Value : 0)), txtCvv.Text.Trim(),
                           hbook_unid.Value, txtNameOnCard.Text.Trim(),payref,"","");

                        Response.Redirect("~/PayProcess.aspx?frm=tickets&refid=" + ret + "&type=" + d.PayType+"&bookingid=" + hbook_unid.Value, false);

                        LogExceptions.LogException("end credit card process");

                        LogExceptions.LogException("retval: " + ret);


                        //if (ret == "Approved")
                        //{
                        //    bookingDetails.CTracker = string.Format("mid:{0};cnumber:{1};cardtype:{2};expiry:{3};cvv:{4}", payDetials.Vendor, txtCardConnectNumber.Text.Trim(), ddlCardType.SelectedValue, ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text);

                        //    bookingDetails.PaymentDatetime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                        //    bookingDetails.PaymentRef = "retref:" + retref + ";profileid:" + profileid;
                        //    bookingDetails.IsPaid = true;
                        //    pRep.Edit(bookingDetails);
                        //    //send mail to user
                        //    SendEmailTOClient(hbook_unid.Value);

                        //    rettext = ret;
                        //}
                        //else
                        //{
                        //    bookingDetails.CTracker = string.Format("mid:{0};cnumber:{1};cardtype:{2};expiry:{3};cvv:{4}", payDetials.Vendor, txtCardConnectNumber.Text.Trim(), ddlCardType.SelectedValue, ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text);
                        //    bookingDetails.IsPaid = false;
                        //    bookingDetails.PaymentRef = "rettext:" + ret;
                        //    pRep.Edit(bookingDetails);
                        //    //Display faild process status
                        //    rettext = ret;
                        //}

                        //LogExceptions.LogException(retval);
                        pnlList.Visible = false;
                        pnlContactDetails.Visible = false;
                        pnlAddCustomerDetails.Visible = false;
                        pnlPaymentDetails.Visible = false;

                        pnlResultShow.Visible = true;
                        //if (rettext == "Approved")
                        //{
                        //    lblMsgResult.Text = rettext + "<br><br>" + "Ticket(s) booked successfully";

                        //}
                        //else
                        //{
                        //    lblMsgResult.Text = rettext + "<br><br>" + "Faild to process the payment.Please try again.";
                        //}
                    }
                    else
                    {
                        pnlList.Visible = false;
                        pnlContactDetails.Visible = false;
                        pnlAddCustomerDetails.Visible = false;
                        pnlPaymentDetails.Visible = false;
                        pnlResultShow.Visible = true;
                        //mail will send to all users
                        SendEmailTOClient(hbook_unid.Value);
                        lblMsgResult.Text = "Ticket(s) booked successfully";
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void SendEmailTOClient(string unid)
        {
            try
            {

                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                IPortfolioRepository<PortfolioMgt.Entity.ActivityDetail> auRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityDetail>();

                var ulist = puRep.GetAll().Where(o => o.BookingUNID == unid).ToList();

                //get booking details
                var bEntity = pRep.GetAll().Where(o => o.unid == ulist.FirstOrDefault().BookingUNID).FirstOrDefault();

                //get activity or event detsils
                var aEntity = auRep.GetAll().Where(o => o.ID == bEntity.ActivityID).FirstOrDefault();
                sessionKeys.PortfolioID = aEntity.OrganizationID;
                //get customer details 
                var portfolioDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == aEntity.OrganizationID).FirstOrDefault();

                // int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(sessionKeys.PortfolioID);
                string subject = "Here's your tickets";
                Emailer em = new Emailer();
                string bodyNew = em.ReadFile("~/WF/DC/EmailTemplates/eventtickets.htm");

                foreach (var u in ulist)
                {
                    string body = bodyNew;

                    body = body.Replace("[mail_head]", " Ticket(s)");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID));
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());

                    body = body.Replace("[noteslist]", GetQuoteItemList(u.UserBookingUNID));

                    body = body.Replace("[eventname]", aEntity.Title);
                    body = body.Replace("[eventdatetime]", string.Format("{0:dd/MM/yyyy hh:mm tt}", aEntity.StartDateTime));
                    body = body.Replace("[eventvenue]", string.Format("{0} {1} {2} {3} {4} {5}", aEntity.Venue_Name, aEntity.Address1, aEntity.Address2, aEntity.City, aEntity.state_Province, aEntity.Postalcode));
                    body = body.Replace("[orgname]", portfolioDetails.PortFolio);

                    body = body.Replace("[eventimage]", Deffinity.systemdefaults.GetWebUrl() + "/ImageHandler.ashx?id=" + aEntity.unid.ToString() + "&s=" + ImageManager.file_section_event);// "/WF/UploadData/Events/" + aEntity.unid.ToString() + "/0.png");

                    //[date]
                    string Dis_body = body;
                    bool ismailsent = false;
                    // mail to requester
                    //if help desk or assign users are changed then mail should go to requester
                    body = body.Replace("[user]", u.UserName);



                    //var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                    //string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                    //if (File.Exists(pname))
                    //{
                    //    var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                    //    Email ToEmail = new Email();
                    //    Attachment attachment1 = new Attachment(pname);
                    //    attachment1.Name = q.CurrentTemplateName + ".pdf";

                    //    ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                    //}
                    //else
                    //{
                    em.SendingMail(fromemailid, subject, body, u.UserEmail);
                    //}
                    ismailsent = true;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetQuoteItemList(string unid)
        {
            StringBuilder sbuild = new StringBuilder();

            sbuild.Append("<table style='width:70%'>");
            var b = Deffinity.systemdefaults.GetWebUrl() + string.Format("/App/Events/TicketDetails.aspx?unid={0}", unid);
            sbuild.Append(string.Format("<tr><td><b> {1} </b><br></td><td>{0}</td></tr>", getButton(b, "View Ticket"), ""));
            sbuild.Append("</table>");




            return sbuild.ToString();
        }


        private string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#7239EA'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #7239EA; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/App/Donations.aspx", false);
        }
        protected string getPrice(string price)
        {
            if (Convert.ToDouble(price) > 0)
            {
                return string.Format("{0:F2}", Convert.ToDouble(price));
            }
            else
            {
                return "Free";
            }
        }

        protected void btnAddContacts_Click(object sender, EventArgs e)
        {
            try
            {
                int user_count = 0;

                string tickets = "";

                foreach (ListViewItem item in listCategory.Items)
                {

                     Label lbl = (Label)item.FindControl("lblCategory");
                    HtmlInputGenericControl txtamount = (HtmlInputGenericControl)item.FindControl("txtamount_list");

                    user_count = user_count + Convert.ToInt32(txtamount.Value.Trim());

                    tickets = tickets + lbl.Text + ":" + Convert.ToInt32(txtamount.Value.Trim()) + ";";

                }

                if (user_count > 0)
                {

                    pnlList.Visible = false;
                    pnlContactDetails.Visible = false;
                    pnlAddCustomerDetails.Visible = true;

                    var id = Request.QueryString["unid"].ToString();
                    var uDetails = PortfolioMgt.BAL.ActivityDetailsBAL.ActivityDetailsBAL_SelectByUNID(id);


                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();
                    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                    var ab = pRep.GetAll().Where(o => o.unid == hbook_unid.Value).FirstOrDefault();
                    if (ab == null)
                    {
                         ab = new PortfolioMgt.Entity.ActivityBooking();
                        ab.ActivityID = uDetails.ID;
                        ab.BookedBy = sessionKeys.UID;
                        ab.BookedSolts = user_count;
                        ab.TotalAmount = Convert.ToDouble(txtTotal.Text.Trim());
                        ab.IsPaid = false;
                        ab.BookingDate = DateTime.Now;
                        ab.MoreDetails = hbook_unid.Value;
                        ab.ContactEmail = txtemail.Text.Trim();
                        ab.ContactName = txtName.Text.Trim();
                        ab.ContactNumber = "+1";
                        ab.unid = hbook_unid.Value;
                        ab.TicketsByCategory = tickets;

                        pRep.Add(ab);
                    }
                    else
                    {
                       
                        ab.BookedBy = sessionKeys.UID;
                        ab.BookedSolts = user_count;
                        ab.TotalAmount = Convert.ToDouble(txtTotal.Text.Trim());
                        ab.IsPaid = false;
                        ab.BookingDate = DateTime.Now;
                        ab.MoreDetails = hbook_unid.Value;
                       
                        ab.unid = hbook_unid.Value;
                        ab.TicketsByCategory = tickets;
                    }

                    txtTotal.Text = string.Format("{0:F2}", ab.TotalAmount);

                    //add items
                    UpdateBookingSlots(ab.ID);

                    var gulist = puRep.GetAll().Where(o => o.BookingUNID == hbook_unid.Value).ToList();

                    var rulist = (from r in gulist
                                 select new
                                 {
                                     FirstName = getFirstName(r.UserName) ,
                                     LastName = getLastName( r.UserName),
                                     r.AdminNotes,
                                     r.BookingID,
                                     r.BookingPrice,
                                     r.BookingUNID,
                                     r.Company,
                                     r.EventStatus,
                                     r.ID,
                                     r.State,
                                     r.TicketType,
                                     r.UserBookingUNID,
                                     r.UserContact,
                                     r.UserEmail,
                                     r.UserName,
                                     r.UserNotes,
                                     r.ValidatedDateTime
                                 }).ToList();
                    //gridusers.DataSource = gulist;
                    //gridusers.DataBind();
                    listUsers.DataSource = rulist;
                    listUsers.DataBind();
                }
                else
                {
                    DeffinityManager.ShowMessages.ShowErrorAlert(this.Page, "Can you please enter the number of tickets you require before moving to the next step. Thank you", "Ok");
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string getFirstName(string name)
        {
            var f= name.Split(' ').FirstOrDefault();
            if (f == null)
                f = "";

            return f;
        }
        private string getLastName(string name)
        {
            var l= name.Split(' ').LastOrDefault();
            if (l == null)
                l = "";

            return l;
        }
        public string updateListview()
        {
            string retval = "";

            try
            {

                foreach (ListViewItem item in listUsers.Items)
                {


                    try
                    {
                        Label lblID = (Label)item.FindControl("lblID");
                        int ID = Convert.ToInt32(lblID.Text);

                        TextBox txtSellingPrice = (TextBox)item.FindControl("txttickettype");
                        TextBox txtuser = (TextBox)item.FindControl("txtuser");
                        TextBox txtlastname = (TextBox)item.FindControl("txtlastname");
                        TextBox txtuseremail = (TextBox)item.FindControl("txtuseremail");
                        TextBox txtusercontact = (TextBox)item.FindControl("txtusercontact");
                        TextBox txtState = (TextBox)item.FindControl("txtState");
                        TextBox txtCompany = (TextBox)item.FindControl("txtCompany");
                        IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                        var ab = puRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                        if (ab != null)
                        {
                            ab.UserContact = txtusercontact.Text.Trim();
                            ab.UserEmail = txtuseremail.Text.Trim();
                            ab.UserName = txtuser.Text.Trim() + "   " + txtlastname.Text.Trim();
                            ab.State = txtState.Text;
                            ab.Company = txtCompany.Text.Trim();
                            // ab.UserNotes = txtnotes.Text.Trim();
                            puRep.Edit(ab);


                            AddOrUpdateMembers(txtuseremail.Text.Trim(), txtuser.Text.Trim() + " " + txtlastname.Text.Trim(), "", txtusercontact.Text.Trim(), "", "", txtState.Text.Trim(), "", lblTitle.Text, "", txtCompany.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    // Response.Write(str);

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }

        public void AddOrUpdateMembers(string email, string firstname, string lastname, string contactno, string address, string town, string state, string zipcode, string eventname, string eventstatus, string compnay)
        {

            try
            {
                int userid = 0;

                if (email.Length > 0)
                {
                    var cRep = new UserRepository<Contractor>();
                    var uRep = new UserRepository<UserDetail>();
                    var cvRep = new UserRepository<v_contractor>();
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());
                    var cDetails = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.LoginName.ToLower().Trim() == email.ToLower().Trim() && o.Status == "Active").FirstOrDefault();
                    if (cDetails == null)
                    {

                        cRep = new UserRepository<Contractor>();
                        cvRep = new UserRepository<v_contractor>();

                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = firstname.Trim() + " " + lastname.Trim();
                        value.EmailAddress = email;
                        value.LoginName = email.ToLower().Trim();
                        var pw = "SMG@2022";
                        value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.SID = 2;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = compnay;
                        value.ContactNumber = contactno;

                        cRep.Add(value);


                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();


                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = address;
                        cdEntity.Country = 190;
                        cdEntity.PostCode = zipcode;
                        cdEntity.State = state;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = town;
                        cdEntity.UserId = value.ID;
                        cdEntity.DenominationDetailsID = 0;
                        cdEntity.SubDenominationDetailsID = 0;

                        cdRep.Add(cdEntity);

                        userid = value.ID;
                    }
                    else
                    {
                        userid = cDetails.ID;
                    }



                    //update company
                    var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    if (urRep.GetAll().Where(o => o.UserID == userid && o.CompanyID == sessionKeys.PortfolioID).Count() == 0)
                    {
                        var urEntity = new UserMgt.Entity.UserToCompany();
                        urEntity.CompanyID = sessionKeys.PortfolioID;
                        urEntity.UserID = userid;
                        urRep.Add(urEntity);
                    }

                    var tags = "";
                   
                    var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                    if (ud == null)
                    {
                        string toadd = "All,";
                        //if (eventstatus == "Pending")
                        //{
                        //    toadd = eventname + " - Not Attended";
                        //}
                        //else if (eventstatus == "Attended")
                        //{
                        //    toadd = eventname + " - Attended";
                        //}
                        var notes = toadd;// "[{\"value\":\"" + toadd + "\"}]";


                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = notes, UserId = userid });
                    }
                    else
                    {
                        var exitingNotes = ud.Notes;
                        if (!exitingNotes.Contains("All"))
                        {
                            string toadd = "All";
                            //if (eventstatus == "Pending")
                            //{
                            //    toadd = eventname + " - Not Attended";
                            //}
                            //else if (eventstatus == "Attended")
                            //{
                            //    toadd = eventname + " - Attended";
                            //}

                            exitingNotes = exitingNotes.Contains("All") == false ? exitingNotes + "All," : exitingNotes;
                        }


                        ud.Notes = exitingNotes;
                        UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSaveUsers_Click(object sender, EventArgs e)
        {
            try
            {

                updateListview();

                //int countRow = gridusers.Rows.Count;

                //for (int i = 0; i < countRow; i++)
                //{
                //    GridViewRow row = gridusers.Rows[i];
                //    Label lblID = (Label)row.FindControl("lblID");
                //    int ID = Convert.ToInt32(lblID.Text);

                //    TextBox txtSellingPrice = (TextBox)row.FindControl("txttickettype");
                //    TextBox txtuser = (TextBox)row.FindControl("txtuser");
                //    TextBox txtuseremail = (TextBox)row.FindControl("txtuseremail");
                //    TextBox txtusercontact = (TextBox)row.FindControl("txtusercontact");
                //    //TextBox txtnotes = (TextBox)row.FindControl("txtnotes");


                //    IPortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail> puRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBookingItemUserDetail>();

                //    var ab = puRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                //    if(ab != null)
                //    {
                //        ab.UserContact = txtusercontact.Text.Trim();
                //        ab.UserEmail = txtuseremail.Text.Trim();
                //        ab.UserName = txtuser.Text.Trim();
                //       // ab.UserNotes = txtnotes.Text.Trim();
                //        puRep.Edit(ab);
                //    }
                //}


                pnlList.Visible = false;
                pnlContactDetails.Visible = false;
                pnlAddCustomerDetails.Visible = false;
                IPortfolioRepository<PortfolioMgt.Entity.ActivityBooking> pRep = new PortfolioRepository<PortfolioMgt.Entity.ActivityBooking>();

                var bookingDetails = pRep.GetAll().Where(o => o.unid == hbook_unid.Value).FirstOrDefault();


                if ((bookingDetails.TotalAmount.HasValue ? bookingDetails.TotalAmount.Value : 0) > 0)
                {
                    pnlPaymentDetails.Visible = true;

                }
                else
                {
                    pnlPaymentDetails.Visible = false;
                    pnlResultShow.Visible = true;
                    lblMsgResult.Text = "Ticket(s) booked successfully";
                    SendEmailTOClient(hbook_unid.Value);
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}