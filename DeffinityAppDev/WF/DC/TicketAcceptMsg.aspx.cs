using DC.BAL;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC
{
    public partial class TicketAcceptMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                pnlmsg.Visible = false;
                pnlAccept.Visible = false;
                CheckingInPageLoad();
            }
           
        }
        public void CheckingInPageLoad()
        {
            try
            {
                if (Request.QueryString["rid"] != null && Request.QueryString["cid"] != null && Request.QueryString["sta"] != null)
                {
                    int callid = Convert.ToInt32(Request.QueryString["cid"].ToString());
                    int resourceid = Convert.ToInt32(Request.QueryString["rid"].ToString());
                    string Accept = Request.QueryString["sta"].ToString();
                    using (DCDataContext dc = new DCDataContext())
                    {
                        if (Request.QueryString["type"] == "arrival")
                        {

                            var clist = FLSDetailsBAL.Jqgridlist(int.Parse(Request.QueryString["cid"])).ToList();
                            var cdetails = clist.FirstOrDefault();
                            if (cdetails.AssignedTechnicianID > 0)
                            {
                                pnlAccept.Visible = false;
                                pnlError.Visible = true;
                               
                                CallResourceSchedule callRes = dc.CallResourceSchedules.
                                            Where(a => a.CallID == int.Parse(Request.QueryString["cid"]) && a.IsActive == true).FirstOrDefault();
                                if (callRes != null)
                                {
                                    if (callRes.ResourceID == Convert.ToInt32(Request.QueryString["rid"].ToString()))
                                    {
                                        var datestr = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), callRes.StartDate);
                                        lblErrorpnlError.Text = string.Format( "You’ve already scheduled this job on {0}", datestr);
                                    }
                                    else{
                                        lblErrorpnlError.Text = string.Format("Job already scheduled");
                                    }
                                }
                            }
                            else
                            {
                                // LblMsg.Text = string.Empty;
                                pnlAccept.Visible = true;


                                lblCity.Text = cdetails.RequestersCity;
                                lblDetails.Text = cdetails.Details;
                                lblJob.Text = "" + cdetails.CCID;

                                lblRequester.Text = cdetails.RequesterName;
                                lblTown.Text = cdetails.RequestersTown;
                                lblZipcode.Text = cdetails.RequestersPostCode;
                                lblPreferreddate.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cdetails.ScheduledDateTime);
                                if (!string.IsNullOrEmpty(cdetails.Preferreddate2))
                                {
                                    pnlpre2.Visible = true;
                                    lblPre2.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cdetails.Preferreddate2);
                                }
                                else
                                    pnlpre2.Visible = false;
                                if (!string.IsNullOrEmpty(cdetails.Preferreddate3))
                                {
                                    pnlpre3.Visible = true;
                                    lblpre3.Text = string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), cdetails.Preferreddate3);
                                }
                                else
                                    pnlpre3.Visible = false;
                            }

                        }
                        else if (Request.QueryString["type"] == "reject")
                        {
                            CallResourceSchedule callRes = dc.CallResourceSchedules.
                                         Where(a => a.CallID == int.Parse(Request.QueryString["cid"]) && a.ResourceID == int.Parse(Request.QueryString["rid"])).FirstOrDefault();

                            if (callRes != null)
                            {
                                callRes.IsActive = false;
                                dc.SubmitChanges();

                                pnlStatus.Visible = true;
                                lblMsgStatus.Text = "Thank you. Your request has been acknowledged.";
                            }
                        }
                        else if (Request.QueryString["type"] == "imhere")
                        {
                            onwaymail(Request.QueryString["cid"].ToString());

                            pnlStatus.Visible = true;
                            lblMsgStatus.Text = "Mail has been sent to the customer.";
                        }
                        else if (Request.QueryString["type"] == "rate")
                        {
                            try {
                                int UserID = 0;
                                if(Request.QueryString["rid"].ToString() != null)
                                {
                                    UserID = Convert.ToInt32(Request.QueryString["rid"].ToString());
                                }
                            var clist = FLSDetailsBAL.Jqgridlist(int.Parse(Request.QueryString["cid"])).ToList();
                            var cdetails = clist.FirstOrDefault();
                            var f = CallDetailsBAL.SelectbyId(int.Parse(Request.QueryString["cid"]));
                            var d = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = cdetails.CallID }, f.CompanyID.Value, UserID);
                            if (d != null)
                                Response.Redirect(string.Format("~/WF/DC/DCServicesMail.aspx?CCID={0}&callid={1}&SDID={1}&ivref={2}", cdetails.CCID, cdetails.CallID, d.ID), false);
                                //                            Response.Redirect("~/WF/DC/DCServicesMail.aspx?CallID=" + Request.QueryString["cid"].ToString());
                            }
                            catch (Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                        }
                        else if (Request.QueryString["type"] == "invoice")
                        {
                            try
                            {
                                var clist = FLSDetailsBAL.Jqgridlist(int.Parse(Request.QueryString["cid"])).ToList();
                                var cdetails = clist.FirstOrDefault();
                                var f = CallDetailsBAL.SelectbyId(int.Parse(Request.QueryString["cid"]));
                                var d = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = cdetails.CallID }, f.CompanyID.Value);
                                if (d != null)
                                    Response.Redirect(string.Format("~/WF/DC/DCInvoiceMail.aspx?CCID={0}&callid={1}&SDID={1}&ivref={2}", cdetails.CCID, cdetails.CallID, d.ID), false);
                                //Response.Redirect("~/WF/DC/DCInvoiceMail.aspx?CallID=" + Request.QueryString["cid"].ToString(),false);
                            }
                            catch(Exception ex)
                            {
                                LogExceptions.WriteExceptionLog(ex);
                            }
                        }
                        else if (Request.QueryString["type"] == "arrived")
                        {
                            //45 arrived
                            FLSDetailsBAL.UpdateTicketStatus(callid, resourceid, 45);
                            pnlStatus.Visible = true;
                            lblMsgStatus.Text = "Status updated successfully";
                        }
                        else if (Request.QueryString["type"] == "customernot")
                        {
                            //46	Customer Not Responding
                            FLSDetailsBAL.UpdateTicketStatus(callid, resourceid, 46);
                            pnlStatus.Visible = true;
                            lblMsgStatus.Text = "Status updated successfully";
                        }
                        else if (Request.QueryString["type"] == "close")
                        {
                            //35 close
                            FLSDetailsBAL.UpdateTicketStatus(callid, resourceid, 35);
                            //UpdateTicketStatus(dc);
                            pnlStatus.Visible = true;
                            CallCloseMail(Request.QueryString["cid"].ToString());

                            lblMsgStatus.Text = "Job has been closed.";
                        }
                    }
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int callid = Convert.ToInt32(Request.QueryString["cid"].ToString());
                int resourceid = Convert.ToInt32(Request.QueryString["rid"].ToString());
                if (!string.IsNullOrEmpty(txtDate.Text.Trim()) && !string.IsNullOrEmpty(txtTime.Text.Trim()))
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        //CallDetail calldetails = dc.CallDetails.
                        //             Where(a => a.ID == int.Parse(Request.QueryString["cid"])).FirstOrDefault();
                        ////43	Assigned Technician	
                        //calldetails.StatusID = 43;
                        ////Update call status
                        //dc.SubmitChanges();
                        //43	Scheduled	
                        FLSDetailsBAL.UpdateTicketStatus(callid, resourceid, 43);

                        FLSDetail flsdetailsList = dc.FLSDetails.
                            Where(a => a.CallID == int.Parse(Request.QueryString["cid"])).FirstOrDefault();
                        CallResourceSchedule callRes = dc.CallResourceSchedules.
                                         Where(a => a.CallID == int.Parse(Request.QueryString["cid"]) && a.ResourceID == int.Parse(Request.QueryString["rid"])).FirstOrDefault();
                        flsdetailsList.ScheduledDate = Convert.ToDateTime(txtDate.Text + " " + (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00"));
                        flsdetailsList.ScheduledEndDateTime = (Convert.ToDateTime(txtDate.Text + " " + (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00")).AddHours(2));
                        flsdetailsList.UserID = int.Parse(Request.QueryString["rid"]);
                        dc.SubmitChanges();

                        callRes.StartDate = Convert.ToDateTime(txtDate.Text + " " + (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00")); ;
                        callRes.EndDate = (Convert.ToDateTime(txtDate.Text + " " + (string.IsNullOrEmpty(txtTime.Text) ? "00:00:00" : txtTime.Text + ":00")).AddHours(2));
                        callRes.IsActive = true;
                        callRes.IsAssigned = true;

                        dc.SubmitChanges();


                        //update start date and end date
                        var resList = dc.CallResourceSchedules.Where(o => o.CallID == callid && (o.IsAssigned.HasValue ? o.IsAssigned.Value : false) == true).ToList();
                        if (flsdetailsList != null)
                        {
                            if (resList.Count > 0)
                            {
                                flsdetailsList.ScheduledDate = resList.Select(o => o.StartDate).Min();
                                flsdetailsList.ScheduledEndDateTime = resList.Select(o => o.EndDate).Max();
                                //cDetails.UserID = ResId;
                                //Update start date and end date
                                dc.SubmitChanges();
                            }
                        }


                        //send mail
                        sendmailtoCustomer(Request.QueryString["cid"].ToString(), Request.QueryString["rid"].ToString());
                        lblSuccessMsg.Text = "Updated successfully";
                        //lblerror.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblerror.Text = "Please enter date and time";
                    //lblerror.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        public void sendmailtoCustomer(string ticketno,string resourceid)
        {

            try
            {
                var flsdata =FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                EmailFooter ef = new EmailFooter();
                //6 FLS
                ef = FooterEmail.EmailFooter_selectByID(6,1);
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
                //LogExceptions.LogException(Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
                //body = body.Replace("[src]", Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
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

        public void CallCloseMail(string ticketno)
        {

            try
            {
                var flsdata = FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                var cdetails = CallDetailsBAL.SelectbyId(Convert.ToInt32(ticketno));
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                Emailer em = new Emailer();
                string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSClosedSurvey.htm");
                string subject = string.Empty;
                subject = "Job Reference:" + flsdata.CCID.ToString();
                body = body.Replace("[mail_head]", "Job");
                
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                body = body.Replace("[Status]", flsdata.Status);
                body = body.Replace("[user]", flsdata.RequesterName);
                body = body.Replace("[ref]", ""+flsdata.CCID.ToString());
                body = body.Replace("[linksurvey]", Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/Feedback/FeedbackFrontpage.aspx?cid=" + cdetails.RequesterID.Value.ToString() + "&callid=" + flsdata.CallID.ToString());

                body = body.Replace("[instancename]", Deffinity.systemdefaults.GetInstanceTitle());

                foreach (var s in flsdata.RequestersEmailAddress.Split(','))
                {
                    if (!string.IsNullOrEmpty(s))
                        em.SendingMail(fromemailid, subject, body, s);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public void onwaymail(string ticketno)
        {
            try
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    var flsdata = FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/CallOntheWay.htm");
                    string subject = string.Empty;
                    subject = "I’m on my way" + "- Job Reference:" + flsdata.CCID.ToString();
                    body = body.Replace("[mail_head]", "Job");
                    CallResourceSchedule callRes = dc.CallResourceSchedules.
                                             Where(a => a.CallID == Convert.ToInt32(ticketno) && a.IsActive == true).FirstOrDefault();

                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    body = body.Replace("[user]", flsdata.RequesterName);
                    body = body.Replace("[instancename]", Deffinity.systemdefaults.GetInstanceTitle());

                    body = body.Replace("[name]", flsdata.AssignedTechnician);
                    body = body.Replace("[date]", callRes.StartDate.Value.ToShortDateString());
                    //LogExceptions.LogException(Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
                    //body = body.Replace("[src]", Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/UploadData/Users/ThumbNails/user_{0}.png", flsdata.AssignedTechnicianID));
                    body = body.Replace("[img]", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/Admin/ImageHandler.ashx?type=user&id={0}", flsdata.AssignedTechnicianID) + "'/>");
                    body = body.Replace("[email]", flsdata.AssignedTechnicianEmail);
                    body = body.Replace("[mobile]", flsdata.AssignedTechnicianContact);

                    foreach (var s in flsdata.RequestersEmailAddress.Split(','))
                    {
                        if (!string.IsNullOrEmpty(s))
                            em.SendingMail(fromemailid, subject, body, s);
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