using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BAL;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using DC.SRV;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCQuoteContactMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["cid"] != null)
                    {
                        sessionKeys.PortfolioID = Convert.ToInt32(Request.QueryString["cid"].ToString());
                    }

                    lblTitle.InnerText = "Job Reference " + Deffinity.systemdefaults.GetCallPrefix() + FLSDetailsBAL.GetCallIDByCustomer(QueryStringValues.CallID).ToString();
                    if (Request.QueryString["callid"] != null)
                    {
                        sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);

                    }
                    if (Request.QueryString["planid"] != null)
                    {
                        UpdateMail();
                        lblMsg.Text = "Thank you for your interest in one of our plans. One of our members in the team will be in touch with you shortly.";
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           
        }
        private void UpdateMail()
        {
            try
            {
                var PlanID = int.Parse(Request.QueryString["planid"]);
                //IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ptRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();

                //var pEntity = prRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                var plEntity = ptRep.GetAll().Where(o => o.ID == PlanID).FirstOrDefault();
                int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                WebService ws = new WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, sessionKeys.PortfolioID);
                using (UserMgt.DAL.UserDataContext ud = new UserMgt.DAL.UserDataContext())
                {

                    using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                    {
                        using (DCDataContext dc = new DCDataContext())
                        {
                            var fls = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                            //var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                            var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();
                            var fdetails = dc.FLSDetails.Where(c => c.CallID == callid).FirstOrDefault();

                            var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                            var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                            var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                            var mlist = dc.Managers.Where(o => o.CustomerID == cdetails.CompanyID).ToList();
                            List<UserMgt.Entity.v_contractor> ulist = new List<UserMgt.Entity.v_contractor>();
                            if (mlist.Count > 0)
                                ulist = ud.v_contractors.Where(u => mlist.Select(o => o.UserID).ToArray().Contains(u.ID)).ToList();
                            //var subject = dc
                            //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                            string subject = "Customer Name has expressed interest in a Maintenance Plan";
                            Emailer em = new Emailer();
                            string body = em.ReadFile("~/WF/DC/EmailTemplates/PlanOffer.htm");

                            body = body.Replace("[mail_head]", "Maintenance Plan Offers");
                            body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                            body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                            body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                            body = body.Replace("[CustomerName]", fls.RequesterName);

                            body = body.Replace("[MaintenancePlanName]", plEntity.Title);
                            body = body.Replace("[Customermobilenumber]", fls.RequestersTelephoneNo);
                            body = body.Replace("[Customeremailaddress]", fls.RequestersEmailAddress);

                            body = body.Replace("[Address]", fls.RequestersAddress + " ," + fls.RequestersCity + " ," + fls.RequestersTown + " , " + fls.RequestersPostCode + " , " + fls.RequestersTelephoneNo);

                            //[date]



                            string Dis_body = body;
                            bool ismailsent = false;
                            // mail to requester
                            //if help desk or assign users are changed then mail should go to requester
                            //body = body.Replace("[user]", pcontact.Name);
                            em.SendingMail(fromemailid, subject, body, "indra@deffinity.com");
                            foreach (var v in ulist)
                            {
                                em.SendingMail(fromemailid, subject, body, v.EmailAddress);
                            }
                            ismailsent = true;


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