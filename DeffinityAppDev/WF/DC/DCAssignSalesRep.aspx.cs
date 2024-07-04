using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using DC.DAL;
using AssetsMgr.DAL;
using PortfolioMgt.DAL;
using System.Xml;
using DC.BLL;
using DC.BAL;
using DC.SRV;
using System.Globalization;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCAssignSalesRep : System.Web.UI.Page
    {
        IDCRespository<CallDetail> cReporsitory = null;
        IDCRespository<FLSDetail> fReporsitory = null;
        IDCRespository<CallResourceSchedule> CrRepository = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //add default service providers
                    //AddDefaultServiceProvider(QueryStringValues.CallID);
                    //if user is Resource hide the buttons 
                    if (sessionKeys.SID == 4)
                        pnlButtons.Visible = false;
                    else
                        pnlButtons.Visible = true;

                    if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                    {
                        link_return.HRef = "FLSResourceList.aspx?type=FLS";
                    }
                    else
                    {
                        link_return.HRef = "FLSJlist.aspx?type=FLS";
                        ////}
                    }

                    if (QueryStringValues.CCID > 0)
                        lblTitle.InnerText = "Sales Rep - Job Reference " + QueryStringValues.CCID.ToString() + " : " + FLSDetailsBAL.GetJobDetails(QueryStringValues.CallID); 
                    else
                        lblTitle.InnerText = "Sales Rep";
                    //display call details  
                    TicketDetails(QueryStringValues.CallID);
                    //bind product type / associated 
                    BindExpertdata();
                    cReporsitory = new DCRepository<CallDetail>();
                    fReporsitory = new DCRepository<FLSDetail>();
                    CrRepository = new DCRepository<CallResourceSchedule>();
                    var fd = fReporsitory.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                    var rd = CrRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID && o.UserType == CallResourceScheduleBAL.Usertype_SmartRep && ((o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true || (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == false)).ToList();
                    txtScheduledEndDateTime.Value = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                    txtSeheduledDateTime.Value = string.Format(Deffinity.systemdefaults.GetDateformat(), fd.ScheduledDate.ToString());
                    txtDate.Text = fd.ScheduledDate.HasValue ? fd.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                    txtTime.Text = fd.ScheduledDate.HasValue ? fd.ScheduledDate.Value.ToString(Deffinity.systemdefaults.GetTimeformat12()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat12());
                    ddlStartTime.SelectedValue = GetAm_PM(fd.ScheduledDate.HasValue ? fd.ScheduledDate.Value : DateTime.Now);

                    if (rd.Count > 0)
                        hstatus.Value = rd.FirstOrDefault().ResourceID.Value.ToString();
                    else
                        hstatus.Value = "0";


                    BindPreferedDate();

                    SetPopuplablevalues();
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
        }
        public DateTime GetDateTime(string d_date, string t_time, string t_am_pm)
        {

            string s = string.Format("{0} {1} {2}", d_date, t_time, t_am_pm);
            DateTimeFormatInfo fi = new CultureInfo("en-US", false).DateTimeFormat;
            DateTime myDate = DateTime.ParseExact(s, "MM/dd/yyyy hh:mm:ss tt", fi);

            return myDate;
        }

        public string GetAm_PM(DateTime d_datetime)
        {
            string s = d_datetime.ToString("tt");
            return s;
        }
        private void SetPopuplablevalues()
        {
            var fieldsList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).OrderBy(g => g.Position).ToList();

            if (fieldsList.Count > 0)
            {
                WebService wb = new WebService();
                foreach (var item in fieldsList)
                {
                    if (item.DefaultName.ToLower().Contains("scheduled date/time"))
                    {
                        lblScheduledDateTime.Text = item.InstanceName;
                    }

                    else if (item.DefaultName.ToLower().Contains("scheduled end date/time"))
                    {
                        lblScheduledEndDateTime.Text = item.InstanceName;
                    }
                }
            }
        }

        private void BindPreferedDate()
        {
            try
            {
                txtpdate1.Visible = false;
                chkP1.Visible = false;
                txtpdate2.Visible = false;
                chkP2.Visible = false;
                txtpdate3.Visible = false;
                chkP3.Visible = false;

                IDCRespository<FLSDetail> fdRepository = new DCRepository<FLSDetail>();
                var fdata = fdRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                if (fdata != null)
                {
                    if (fdata.ScheduledDate.HasValue)
                    {
                        //txtpdate1.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), fdata.ScheduledDate) + (!string.IsNullOrEmpty(fdata.ScheduledDatetotime) ? " - " + fdata.ScheduledDatetotime : string.Empty);
                        txtpdate1.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), fdata.ScheduledDate) + " - " + string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), fdata.ScheduledEndDateTime);
                        hdate.Value = string.Format(Deffinity.systemdefaults.GetStringDateformat(), fdata.ScheduledDate);
                        txtpdate1.Visible = true;
                        chkP1.Visible = true;
                    }
                    if (fdata.Preferreddate2.HasValue)
                    {
                        txtpdate2.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), fdata.Preferreddate2) + (!string.IsNullOrEmpty(fdata.Preferreddatetotime2) ? " - " + fdata.Preferreddatetotime2 : string.Empty);
                        txtpdate2.Visible = true;
                        chkP2.Visible = true;
                    }
                    if (fdata.Preferreddate3.HasValue)
                    {
                        txtpdate3.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), fdata.Preferreddate3) + (!string.IsNullOrEmpty(fdata.Preferreddatetotime3) ? " - " + fdata.Preferreddatetotime3 : string.Empty);
                        txtpdate3.Visible = true;
                        chkP3.Visible = true;
                    }

                }
                else
                {
                    hdate.Value = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void TicketDetails(int callid)
        {
            var f = FLSDetailsBAL.Jqgridlist(callid).FirstOrDefault();
            if (f != null)
            {
                lbladdress.Text = f.RequestersAddress + ", " + f.RequestersCity + ", " + f.RequestersTown + ", " + f.RequestersPostCode;
                lblCustomertitle.Text = f.RequesterName;
                lbldetails.Text = f.Details;
                lbledate.Text = f.ScheduledEndDateTime;
                lblsdate.Text = f.ScheduledDateTime;
                lbltref.Text = "" + f.CCID.ToString();
            }
        }

        #region Resource postcodes
        public class resourcePostcodes
        {
            public int ResourceID { set; get; }
            public string Postcode { set; get; }
        }


        #endregion
        #region Gmap
        public class geocodes
        {
            public string lat { set; get; }
            public string lng { set; get; }
        }
        public string getLatLong(string Zip)
        {
            string latlong = "", address = "";
            address = "http://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + Zip.Trim() + "&sensor=false";
            var result = new System.Net.WebClient().DownloadString(address);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            XmlNodeList parentNode = doc.GetElementsByTagName("location");
            var lat = "";
            var lng = "";
            foreach (XmlNode childrenNode in parentNode)
            {
                lat = childrenNode.SelectSingleNode("lat").InnerText;
                lng = childrenNode.SelectSingleNode("lng").InnerText;
            }
            latlong = Zip + "," + Convert.ToString(lat) + "," + Convert.ToString(lng) + "," + "" + Zip;
            return latlong;
        }

        #endregion


        public void UpdateResource(int assignedUser)
        {
            cReporsitory = new DCRepository<CallDetail>();
            var cd = cReporsitory.GetAll().Where(o => o.ID == QueryStringValues.CallID).FirstOrDefault();
            fReporsitory = new DCRepository<FLSDetail>();
            var fd = fReporsitory.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();




            //assign ticket to user
            if (assignedUser == 1)
            {

                if (hrid.Value.ToString().Length > 0)
                {
                   //FLSDetailsBAL.UpdateTicketStatus(QueryStringValues.CallID, sessionKeys.UID, 43);
                    //43	Assigned Technician
                    //cd.StatusID = 43;
                    //cReporsitory.Edit(cd);
                  //  fd.UserID = Convert.ToInt32(hrid.Value.ToString().Contains(",") ? hrid.Value.ToString().Split(',').ToArray().FirstOrDefault() : hrid.Value.ToString());
                    fd.TicketManagerID = Convert.ToInt32(hrid.Value.ToString().Contains(",") ? hrid.Value.ToString().Split(',').ToArray().FirstOrDefault() : hrid.Value.ToString());

                    fd.ScheduledDate = GetDateTime(txtDate.Text, (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00"), ddlStartTime.SelectedValue);  //Convert.ToDateTime(txtDate.Text + " " + (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00"));
                    fd.ScheduledEndDateTime = GetDateTime(txtDate.Text, (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00"), ddlStartTime.SelectedValue).AddHours(8); //Convert.ToDateTime(txtDate.Text + " " + (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00")).AddHours(8);
                    if (chkP2.Checked)
                        fd.ScheduledDate = fd.Preferreddate2;
                    if (chkP3.Checked)
                        fd.ScheduledDate = fd.Preferreddate2;
                    fReporsitory.Edit(fd);
                    using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                    {
                        var userlist = Pdc.Contractors.Where(p => hrid.Value.Trim().Split(',').ToArray().Contains(p.ID.ToString())).ToList();

                        foreach (var c in userlist)
                        {
                            //update to associated callids
                            UpdateAssignResources(fd, cd, null, false, false, 0, c, true,CallResourceScheduleBAL.Usertype_SmartRep);
                        }
                       // MailSendingToAssignResource(fd, cd);
                        //update 
                      //  sendmailtoCustomer(QueryStringValues.CallID.ToString(), fd.TicketManagerID.Value.ToString());
                    }
                }


            }
            //update the status
            //44	Awaiting Technician
            if (assignedUser == 2)
            {
                //cd.StatusID = 44;
                //cReporsitory.Edit(cd);
                //FLSDetailsBAL.UpdateTicketStatus(QueryStringValues.CallID, sessionKeys.UID, 44);
               //MailSendingToAllRequestersWithPincode(fd, cd);
            }
        }
        public void sendmailtoCustomer(string ticketno, string resourceid)
        {

            try
            {
                var flsdata = FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                EmailFooter ef = new EmailFooter();
                //6 FLS
                ef = FooterEmail.EmailFooter_selectByID(6, sessionKeys.PortfolioID);
                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/CalltoResource.htm");
                string subject = string.Empty;
                subject = "Job Reference:" + flsdata.CCID.ToString();
                body = body.Replace("[mail_head]", "Job");

                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                body = body.Replace("[user]", flsdata.RequesterName);
                body = body.Replace("[date]", string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), flsdata.ScheduledDateTime));
                body = body.Replace("[name]", flsdata.AssignedTechnician);
                // LogExceptions.LogException(Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
                body = body.Replace("[img]", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/Admin/ImageHandler.ashx?type=user&id={0}", flsdata.AssignedTechnicianID) + "'/>");
                body = body.Replace("[email]", flsdata.AssignedTechnicianEmail);
                body = body.Replace("[mobile]", flsdata.AssignedTechnicianContact);
                body = body.Replace("[img_arrived]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_serviceproviderhasarrived.png");

                body = body.Replace("[Url_arrived]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + resourceid + "&cid=" + ticketno + "&sta=false&type=arrived");

                body = body.Replace("[instancename]", Deffinity.systemdefaults.GetInstanceTitle());
                body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
                foreach (var s in flsdata.RequestersEmailAddress.Split(','))
                {
                    if (!string.IsNullOrEmpty(s))
                        em.SendingMail(fromemailid, subject, body, s.Trim());
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public void MailSendingToAssignResource(FLSDetail fd, CallDetail cd)
        {
            try
            {
                string rids = hrid.Value;
                if (rids.Length > 0)
                {
                    //44	Awaiting Technician
                    //cd.StatusID = 44;
                    //CallDetailsBAL.CallDetailsUpdate(cd);
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSAssignedResourceMail.html");
                    body = body.Replace("[mail_head]", "Job");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    //body = body.Replace("[img]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_yes-accept-and-schedule.png");
                    body = body.Replace("[img_reject]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_reject-this-job.png");
                    body = body.Replace("[img_imhere]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_im-here-let-the-customer-know.png");
                    body = body.Replace("[img_rate]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_submit-an-invoice.png");
                    //body = body.Replace("[img_invoice]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_submit-an-invoice.png");
                    body = body.Replace("[img_close]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_close-this-job.png");
                    body = body.Replace("[img_customernot]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_customer-not-responding.png");
                    body = body.Replace("[img_arrived]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_arrived.png");
                    body = body.Replace("[img_direction]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/GoogleDirections.png");
                    //FlsTicketAcceptMail.html[img]
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        if (fd.ContactAddressID.HasValue)
                        {
                            var pr = pd.PortfolioContactAddresses.Where(o => o.ID == fd.ContactAddressID.Value).FirstOrDefault();
                            body = body.Replace("[address]", pr.Address + ", " + pr.City + ", " + pr.State + ", " + pr.PostCode);
                        }
                        else
                        {
                            var pr = pd.PortfolioContacts.Where(o => o.ID == cd.RequesterID).FirstOrDefault();
                            body = body.Replace("[address]", pr.Address1 + ", " + pr.Town + ", " + pr.City + ", " + pr.Postcode);
                        }
                    }

                    string ProductsTable = string.Empty;
                    using (DCDataContext Dc = new DCDataContext())
                    {
                        CallIdAssociatedProduct callidProducts = Dc.CallIdAssociatedProducts.Where(c => c.Callid == fd.CallID).FirstOrDefault();
                        if (callidProducts != null)
                        {
                            string[] ProductIds = callidProducts.ProductIds.Split(',');
                            ProductsTable = " <table style='width:100%'><thead><tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'><th>Product Type</th><th>Make </th><th>Model </th></tr></thead>";
                            using (AssetsToSoftwareDataContext Asset = new AssetsToSoftwareDataContext())
                            {
                                var AssetList = Asset.V_Assets.ToList();
                                foreach (string pId in ProductIds)
                                {
                                    var aSingle = AssetList.Where(v => v.ID == int.Parse(pId)).FirstOrDefault();
                                    if (aSingle != null)
                                    {
                                        ProductsTable = ProductsTable
                                            + "<tr><td>" + aSingle.TypeName + "</td><td>" + aSingle.MakeName
                                            + "</td><td>" + aSingle.ModelName + "</td> </tr>";
                                    }
                                }
                            }
                            ProductsTable = ProductsTable + " </table>";
                            body = body.Replace("[ProductsTable]", ProductsTable);
                        }
                        else
                        {
                            body = body.Replace("[ProductsTable]", string.Empty);
                        }
                    }
                    //ticketref
                    body = body.Replace("[ticketref]", "" + FLSDetailsBAL.GetCallIDByCustomer(fd.CallID.Value).ToString());
                    body = body.Replace("[ticketdescription]", fd.Details);

                    string cbody = string.Empty;
                    using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                    {
                        var dfRepository = new DCRepository<ServiceProviderScheduling>();

                        var userlist = Pdc.Contractors.Where(p => rids.Split(',').ToArray().Contains(p.ID.ToString())).ToList();
                        int cnt = 0;
                        foreach (var c in userlist)
                        {
                            cnt = cnt + 1;
                            cbody = body;

                            cbody = cbody.Replace("[resourcename]", c.ContractorName);

                            cbody = cbody.Replace("[ArrivalUrl]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=true&type=arrival");
                            cbody = cbody.Replace("[RejectUrl]",
                                Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=reject");
                            cbody = cbody.Replace("[Url_imhere]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=imhere");
                            cbody = cbody.Replace("[Url_rate]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=rate");
                            cbody = cbody.Replace("[Url_invoice]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=invoice");
                            cbody = cbody.Replace("[Url_close]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=close");
                            cbody = cbody.Replace("[Url_arrived]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=arrived");
                            cbody = cbody.Replace("[Url_customernot]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=customernot");
                            cbody = cbody.Replace("[Url_direction]",
                           Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/Gmap.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=direction");

                            try
                            {

                                em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), "Resource Email", cbody, c.EmailAddress);
                            }
                            catch (Exception ex)
                            { LogExceptions.WriteExceptionLog(ex); }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void MailSendingToAllRequestersWithPincode(FLSDetail fd, CallDetail cd)
        {
            try
            {
                string rids = hrid.Value;
                if (rids.Length > 0)
                {
                    //44	Awaiting Technician
                    //cd.StatusID = 44;
                    //CallDetailsBAL.CallDetailsUpdate(cd);
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/FlsTicketAcceptMail.html");
                    body = body.Replace("[mail_head]", "Job Request");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    body = body.Replace("[img]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_yes-accept-and-schedule.png");
                    body = body.Replace("[img_reject]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_reject-this-job.png");
                    body = body.Replace("[img_imhere]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_im-here-let-the-customer-know.png");
                    body = body.Replace("[img_rate]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_submit-an-invoice.png");
                    //body = body.Replace("[img_invoice]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_submit-an-invoice.png");
                    body = body.Replace("[img_close]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_close-this-job.png");
                    body = body.Replace("[img_customernot]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_customer-not-responding.png");
                    body = body.Replace("[img_arrived]", Deffinity.systemdefaults.GetWebUrl() + "/Content/images/button_arrived.png");
                    //FlsTicketAcceptMail.html[img]
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        if (fd.ContactAddressID.HasValue)
                        {
                            var pr = pd.PortfolioContactAddresses.Where(o => o.ID == fd.ContactAddressID.Value).FirstOrDefault();
                            body = body.Replace("[address]", pr.Address + ", " + pr.City + ", " + pr.State + ", " + pr.PostCode);
                        }
                        else
                        {
                            var pr = pd.PortfolioContacts.Where(o => o.ID == cd.RequesterID).FirstOrDefault();
                            body = body.Replace("[address]", pr.Address1 + ", " + pr.Town + ", " + pr.City + ", " + pr.Postcode);
                        }
                    }

                    string ProductsTable = string.Empty;
                    using (DCDataContext Dc = new DCDataContext())
                    {
                        CallIdAssociatedProduct callidProducts = Dc.CallIdAssociatedProducts.Where(c => c.Callid == fd.CallID).FirstOrDefault();
                        if (callidProducts != null)
                        {
                            string[] ProductIds = callidProducts.ProductIds.Split(',');
                            ProductsTable = " <table style='width:100%'><thead><tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'><th>Product Type</th><th>Make </th><th>Model </th></tr></thead>";
                            using (AssetsToSoftwareDataContext Asset = new AssetsToSoftwareDataContext())
                            {
                                var AssetList = Asset.V_Assets.ToList();
                                foreach (string pId in ProductIds)
                                {
                                    var aSingle = AssetList.Where(v => v.ID == int.Parse(pId)).FirstOrDefault();
                                    if (aSingle != null)
                                    {
                                        ProductsTable = ProductsTable
                                            + "<tr><td>" + aSingle.TypeName + "</td><td>" + aSingle.MakeName
                                            + "</td><td>" + aSingle.ModelName + "</td> </tr>";
                                    }
                                }
                            }
                            ProductsTable = ProductsTable + " </table>";
                            body = body.Replace("[ProductsTable]", ProductsTable);
                        }
                        else
                        {
                            body = body.Replace("[ProductsTable]", string.Empty);
                        }
                    }
                    //ticketref
                    body = body.Replace("[ticketref]", "" + FLSDetailsBAL.GetCallIDByCustomer(fd.CallID.Value).ToString());
                    body = body.Replace("[ticketdescription]", fd.Details);

                    string cbody = string.Empty;
                    using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                    {
                        var dfRepository = new DCRepository<ServiceProviderScheduling>();
                        var blastValues = dfRepository.GetAll().FirstOrDefault();
                        bool tocheck = false;
                        bool smail = true;
                        if (blastValues != null)
                        {
                            if (blastValues.SchedulType == "Blast")
                            {
                                if (blastValues.SecondBatchQty > 0)
                                {
                                    tocheck = true;
                                }
                            }
                        }


                        // int[] sid = new int[] { 1, 2, 3, 4, 9 };
                        var userlist = Pdc.Contractors.Where(p => rids.Split(',').ToArray().Contains(p.ID.ToString())).ToList();
                        int cnt = 0;
                        foreach (var c in userlist)
                        {
                            cnt = cnt + 1;
                            cbody = body;
                            //update to associated callids
                            smail = UpdateAssignResources(fd, cd, blastValues, tocheck, smail, cnt, c, false, CallResourceScheduleBAL.Usertype_SmartRep);
                            cbody = cbody.Replace("[resourcename]", c.ContractorName);

                            cbody = cbody.Replace("[AcceptUrl]",
                                Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=true");
                            cbody = cbody.Replace("[ArrivalUrl]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=true&type=arrival");
                            cbody = cbody.Replace("[RejectUrl]",
                                Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=reject");
                            cbody = cbody.Replace("[Url_imhere]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=imhere");
                            cbody = cbody.Replace("[Url_rate]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=rate");
                            cbody = cbody.Replace("[Url_invoice]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=invoice");
                            cbody = cbody.Replace("[Url_close]",
                               Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=close");
                            cbody = cbody.Replace("[Url_arrived]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=arrived");
                            cbody = cbody.Replace("[Url_customernot]",
                              Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/TicketAcceptMsg.aspx?rid=" + c.ID + "&cid=" + fd.CallID + "&sta=false&type=customernot");

                            try
                            {
                                if (smail)
                                    em.SendingMail(Deffinity.systemdefaults.GetFromEmail(), "Resource Email", cbody, c.EmailAddress);
                            }
                            catch (Exception ex)
                            { LogExceptions.WriteExceptionLog(ex); }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private static bool UpdateAssignResources(FLSDetail fd, CallDetail cd, ServiceProviderScheduling blastValues, bool tocheck, bool smail, int cnt, UserMgt.Entity.Contractor c, bool IsAssigned,string userType="Smart Tech")
        {
            using (DCDataContext Dc = new DCDataContext())
            {
                var cdResource = Dc.CallResourceSchedules.Where(o => o.CallID == cd.ID && o.ResourceID == c.ID && o.UserType != CallResourceScheduleBAL.Usertype_SmartTech).FirstOrDefault();
                if (cdResource == null)
                {
                    cdResource = new CallResourceSchedule();
                    cdResource.IsActive = false;
                    cdResource.ResourceID = c.ID;
                    cdResource.CallID = cd.ID;
                    cdResource.StartDate = fd.ScheduledDate;
                    cdResource.EndDate = fd.ScheduledEndDateTime;
                    cdResource.IsAssigned = IsAssigned;
                    cdResource.UserType = CallResourceScheduleBAL.Usertype_SmartRep;
                    if (tocheck)
                    {
                        if (cnt > blastValues.InitialBatchQty)
                        {
                            smail = false;
                            cdResource.MailSent = false;
                            cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                        }
                        else
                        {
                            cdResource.MailSent = true;
                            cdResource.MailSentDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        cdResource.MailSent = true;
                        cdResource.MailSentDateTime = DateTime.Now;
                    }

                    Dc.CallResourceSchedules.InsertOnSubmit(cdResource);
                    Dc.SubmitChanges();
                }
                else
                {
                    cdResource.StartDate = fd.ScheduledDate;
                    cdResource.EndDate = fd.ScheduledEndDateTime;
                    cdResource.IsAssigned = IsAssigned;
                    cdResource.UserType = CallResourceScheduleBAL.Usertype_SmartRep;
                    if (tocheck)
                    {
                        if (cnt > blastValues.InitialBatchQty)
                        {
                            smail = false;
                            cdResource.MailSent = false;
                            cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                        }
                        else
                        {
                            cdResource.MailSent = true;
                            cdResource.MailSentDateTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        cdResource.MailSent = true;
                        cdResource.MailSentDateTime = DateTime.Now;
                    }


                    Dc.SubmitChanges();
                }
            }

            return smail;
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            var cid = QueryStringValues.CallID;
            string s = hrid.Value;
            //assign value
            if (hrid.Value != string.Empty)
            {
                UpdateResource(1);

                Response.Redirect("~/WF/DC/DCAssignSalesRep.aspx?CCID=" + QueryStringValues.CCID.ToString() + "&callid=" + cid.ToString() + "&SDID=" + cid.ToString());
            }

        }

        protected void btnEditDate_Click(object sender, EventArgs e)
        {
            if (QueryStringValues.CallID > 0)
            {
                FLSDetail fd = FLSDetailsBAL.SelectbyId(QueryStringValues.CallID);
                if (fd != null)
                {
                    txtScheduledEndDateTime1.Text = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetDateformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetDateformat());
                    txtScheduledEndTime.Text = fd.ScheduledEndDateTime.HasValue ? fd.ScheduledEndDateTime.Value.ToString(Deffinity.systemdefaults.GetTimeformat()) : DateTime.Now.ToString(Deffinity.systemdefaults.GetTimeformat());
                    txtSeheduledDateTime1.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), fd.ScheduledDate);
                    txtScheduledTime.Text = string.Format(Deffinity.systemdefaults.GetStringTimeformat(), fd.ScheduledDate);

                    mdlExnter.Show();
                }
            }
        }
        protected void btnUpdateDates_Click(object sender, EventArgs e)
        {
            if (QueryStringValues.CallID > 0)
            {
                FLSDetail fd = FLSDetailsBAL.SelectbyId(QueryStringValues.CallID);
                FLSDetail fdj = fd;
                if (fd != null)
                {
                    fd.ScheduledDate = Convert.ToDateTime(txtSeheduledDateTime1.Text + " " + (string.IsNullOrEmpty(txtScheduledTime.Text) ? "00:00:00" : txtScheduledTime.Text + ":00"));
                    fd.ScheduledEndDateTime = Convert.ToDateTime(txtScheduledEndDateTime1.Text + " " + (string.IsNullOrEmpty(txtScheduledEndTime.Text) ? "00:00:00" : txtScheduledEndTime.Text + ":00"));
                    FLSDetailsBAL.FLSDetailsUpdate(fd);

                    Response.Redirect(Request.RawUrl);
                }
            }
        }
        protected void btnEmailRequest_Click(object sender, EventArgs e)
        {
            var cid = QueryStringValues.CallID;
            string s = hrid.Value;
            //assign value
            if (hrid.Value != string.Empty)
            {
                UpdateResource(2);
                Response.Redirect("~/WF/DC/DCAssignSalesRep.aspx?CCID=" + QueryStringValues.CCID.ToString() + "&callid=" + cid.ToString() + "&SDID=" + cid.ToString());
            }
        }

        private void BindExpertdata()
        {
            try
            {
                IAssetRespository<AssetsMgr.Entity.Assetstype> atypeRepository = new AssetRespository<AssetsMgr.Entity.Assetstype>();
                var typeList = atypeRepository.GetAll().OrderBy(o => o.Type).ToList();
                chkExpert.DataSource = typeList;
                chkExpert.DataValueField = "TypeID";
                chkExpert.DataTextField = "Type";
                chkExpert.DataBind();
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void btnReopen_Click(object sender, EventArgs e)
        {
            try
            {
                cReporsitory = new DCRepository<CallDetail>();
                var cd = cReporsitory.GetAll().Where(o => o.ID == QueryStringValues.CallID).FirstOrDefault();
                fReporsitory = new DCRepository<FLSDetail>();
                var fd = fReporsitory.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                cd.StatusID = 22;
                cReporsitory.Edit(cd);
                fd.UserID = null;
                fReporsitory.Edit(fd);
                //delete the customer data

                var rReporsitory = new DCRepository<CallResourceSchedule>();
                var rlist = rReporsitory.GetAll().Where(o => o.CallID == QueryStringValues.CallID && o.UserType == CallResourceScheduleBAL.Usertype_SmartRep).ToList();
                if (rlist != null)
                {
                    if (rlist.Count > 0)
                    {
                        rReporsitory.DeleteAll(rlist);
                    }
                }

                Response.Redirect("~/WF/DC/DCAssignSalesRep.aspx?CCID=" + QueryStringValues.CCID.ToString() + "&callid=" + QueryStringValues.CallID + "&SDID=" + QueryStringValues.CallID);
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }

        }

        private void AddDefaultServiceProvider(int callid)
        {
            try
            {
                using (UserMgt.DAL.UserDataContext Pdc = new UserMgt.DAL.UserDataContext())
                {
                    using (DCDataContext Dc = new DCDataContext())
                    {
                        //var dfRepository = new DCRepository<ServiceProviderScheduling>();
                        //var blastValues = dfRepository.GetAll().FirstOrDefault();
                        //bool tocheck = false;
                        //bool smail = true;
                        //if (blastValues != null)
                        //{
                        //    if (blastValues.SchedulType == "Blast")
                        //    {
                        //        if (blastValues.SecondBatchQty > 0)
                        //        {
                        //            tocheck = true;
                        //        }
                        //    }
                        //}

                        var cdResourceCnt = Dc.CallResourceSchedules.Where(o => o.CallID == callid).Count();
                        if (cdResourceCnt == 0)
                        {
                            var fd = Dc.FLSDetails.Where(o => o.CallID == callid).FirstOrDefault();
                            var userlist = Pdc.v_contractors.Where(o => o.SID == 4 && o.Status.ToLower() == "active" && o.CompanyID == sessionKeys.PortfolioID).ToList();
                            {
                                int cnt = 0;
                                foreach (var c in userlist)
                                {
                                    cnt = cnt + 1;
                                    //cbody = body;
                                    //update to associated callids

                                    var cdResource = Dc.CallResourceSchedules.Where(o => o.CallID == callid && o.ResourceID == c.ID).FirstOrDefault();
                                    if (cdResource == null)
                                    {
                                        cdResource = new CallResourceSchedule();
                                        cdResource.IsActive = false;
                                        cdResource.ResourceID = c.ID;
                                        cdResource.CallID = callid;
                                        cdResource.StartDate = fd.ScheduledDate;
                                        cdResource.EndDate = fd.ScheduledDate.Value.AddMinutes(1);
                                        cdResource.MailSent = false;
                                        cdResource.MailSentDateTime = null;// DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);

                                        //    if (tocheck)
                                        //{
                                        //    if (cnt > blastValues.InitialBatchQty)
                                        //    {
                                        //        smail = false;
                                        //        cdResource.MailSent = false;
                                        //        cdResource.MailSentDateTime = DateTime.Now.AddMinutes(blastValues.MinBeforeNextBlast.HasValue ? blastValues.MinBeforeNextBlast.Value : 0);
                                        //    }
                                        //    else
                                        //    {
                                        //        cdResource.MailSent = true;
                                        //        cdResource.MailSentDateTime = DateTime.Now;
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    cdResource.MailSent = true;
                                        //    cdResource.MailSentDateTime = DateTime.Now;
                                        //}

                                        Dc.CallResourceSchedules.InsertOnSubmit(cdResource);
                                        Dc.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}