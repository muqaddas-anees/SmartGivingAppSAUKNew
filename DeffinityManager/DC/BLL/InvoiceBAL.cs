using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Entity;
using DC.DAL;
using DC.BAL;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.IncidentService_Price_Manager;
using Deffinity.IncidentService;
using PortfolioMgt.Entity;
using System.Web;
using System.IO;

namespace DC.Entity
{
    public class InvoiceItemType
    {
        public int ID { set; get; }
        public string Name { set; get; }
    }

    public class InvoiceDashboard
    {
        public double TotalPending  { set; get; }
        public double TotalUnpaid  { set; get; }
        public double TotalPaidThisMonth  { set; get; }
    }
}

namespace DC.BLL
{
    public static class InvoiceStatus
    {
        public const string Pending = "Pending";
        public const string Sent_to_Customer = "Sent to Customer";
        public const string Paid = "Paid";
    }

    public class InvoiceBAL
    {
        public static InvoiceDashboard GetDashboradValues()
        {
            InvoiceDashboard retval = new InvoiceDashboard();
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            IDCRespository<DC.Entity.CallDetail> cRep = new DCRepository<DC.Entity.CallDetail>();

            var clist = cRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID && o.StatusID != JobStatus.Cancelled).ToList();

            if(clist.Count >0)
            {
                var Plist = dRep.GetAll().Where(o => clist.Select(p => p.ID).ToList().Contains(o.IncidentID.HasValue ? o.IncidentID.Value : 0)).ToList();
                if (Plist.Count > 0)
                {
                    var a = Plist.Where(o => o.Status == "Pending").Select(o => o.RevicedPrice.HasValue ? o.RevicedPrice.Value : 0.00).Sum();
                    var b = Plist.Where(o => (o.Status == null ? string.Empty : o.Status) == string.Empty).Select(o => o.RevicedPrice.HasValue ? o.RevicedPrice.Value : 0.00).Sum();

                    retval.TotalPending = a +b;
                    retval.TotalUnpaid = Plist.Where(o => o.Status == "Sent to Customer").Select(o => o.RevicedPrice.HasValue ? o.RevicedPrice.Value : 0.00).Sum();
                    retval.TotalPaidThisMonth = Plist.Where(o => o.Status == "Paid").Where(o=>o.LoggedDate >= Deffinity.Utility.StartDateOfMonth(DateTime.Now) && o.LoggedDate < Deffinity.Utility.EndDateOfMonth(DateTime.Now)).Select(o => o.RevicedPrice.HasValue ? o.RevicedPrice.Value : 0.00).Sum();
                }
            }


            return retval;
        }

        public static List<DC.Entity.Incident_ServicePrice> GetInvoiceList()
        {
            List<DC.Entity.Incident_ServicePrice> retval = new List<DC.Entity.Incident_ServicePrice>();
            
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            IDCRespository<DC.Entity.CallDetail> cRep = new DCRepository<DC.Entity.CallDetail>();
            //33- cancelled status
            var clist = cRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID && o.StatusID != JobStatus.Cancelled).ToList();

            if (clist.Count > 0)
            {
                retval = dRep.GetAll().Where(o => clist.Select(p => p.ID).ToList().Contains(o.IncidentID.HasValue ? o.IncidentID.Value : 0)).ToList() as List<DC.Entity.Incident_ServicePrice>;
               
            }


            return retval;
        }

        public static List<DC.Entity.Incident_ServicePrice> GetInvoiceList(int PortfolioID)
        {
            List<DC.Entity.Incident_ServicePrice> retval = new List<DC.Entity.Incident_ServicePrice>();

            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            IDCRespository<DC.Entity.CallDetail> cRep = new DCRepository<DC.Entity.CallDetail>();
            //33- cancelled status
            var clist = cRep.GetAll().Where(o => o.CompanyID == PortfolioID && o.StatusID != JobStatus.Cancelled).ToList();

            if (clist.Count > 0)
            {
                retval = dRep.GetAll().Where(o => clist.Select(p => p.ID).ToList().Contains(o.IncidentID.HasValue ? o.IncidentID.Value : 0)).ToList() as List<DC.Entity.Incident_ServicePrice>;

            }

            return retval;
        }
        public static List<InvoiceItemType> GetInvoiceItemType()
        {
            var ftypeRepository = new DCRepository<DC.Entity.FixedRateType>();
            var getvals = ftypeRepository.GetAll().OrderBy(o => o.FixedRateTypeName).ToList();
            List<InvoiceItemType> ivTypes = (from iv in getvals
                                             select new InvoiceItemType()
                                             {
                                                 ID = iv.FixedRateTypeID,
                                                 Name = iv.FixedRateTypeName
                                             }).ToList();
            return ivTypes;
            
        }
        public static IQueryable<DC.Entity.Incident_ServicePrice> SelectInvoiceList()
        {
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            return dRep.GetAll();
        }
        public static List<DC.Entity.Incident_ServicePrice> SelectInvoiceList(int CallID)
        {
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            return dRep.GetAll().Where(o => o.IncidentID == CallID).ToList();
        }

        public static DC.Entity.Incident_ServicePrice AddInvoiceList(int CallID,int CompanyID,int userid=0)
        {
            return InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = CallID }, CompanyID,userid);
        }

        public static void DeleteInvoiceList(int id)
        {
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            var d = dRep.GetAll().Where(o => o.ID == id).FirstOrDefault();
            if (d != null)
                dRep.Delete(d);
        }
        public static List<DC.Entity.Incident_Service> SelectInvoiceItems(int CallID, int InvoiceRef)
        {
            IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            return dRep.GetAll().Where(o => o.IncidentID == CallID && o.Incident_ServicePriceID == InvoiceRef).ToList();
        }

        public static DC.Entity.Incident_Service SelectInvoiceItems(int ID)
        {
            IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            return dRep.GetAll().Where(o => o.IncidentID == ID).FirstOrDefault();
        }
        public static int DeleteInvoiceItem(int ID,int CallID,int InvoiceRef)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Service_Delete",
                new SqlParameter("@ID", ID));

            //update the price
            IncidentService_Price.IncidentService_Price_Select(CallID, "FLS", InvoiceRef);

            return 1;
        }

        public static int DeleteInvoiceItem(int ID)
        {
            var invoiceitem = InvoiceBAL.SelectInvoiceItems(ID);
            if (invoiceitem != null)
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Service_Delete",
                    new SqlParameter("@ID", ID));
                //update the price
                IncidentService_Price.IncidentService_Price_Select(invoiceitem.IncidentID.Value, "FLS", invoiceitem.Incident_ServicePriceID.Value);
            }

            return 1;
        }
        //update invoice item
        public static int UpdateInvoiceItem(int portfolioID,int ID,double QTY,double SellingPrice,double units, string Notes, string desc,int servicetype,double VAT,int invoiceref)
        {
            //check qty or amount is updated
            using (DCDataContext dc = new DCDataContext())
            {
                var applyVAT = true;
                var qitem = dc.Incident_Services.Where(o => o.ID == ID).FirstOrDefault();
                if (qitem != null)
                {
                    if ((qitem.QTY != QTY) || (qitem.SellingPrice != SellingPrice))
                    {
                        var vt = 0.00;
                        if (applyVAT)
                        {
                            vt = VATByCustomerBAL.VATByCustomer_select(portfolioID);
                        }
                        var v = (QTY * SellingPrice);
                        if (vt > 0)
                            VAT = (QTY * SellingPrice) * (vt / 100);
                        //else
                        //    v = 0.00;
                    }
                }

                //update
                ServiceManager.Services_Update(ID, QTY, SellingPrice, units, Notes, desc, servicetype, VAT);

                //update the price
                IncidentService_Price.IncidentService_Price_Select(qitem.IncidentID.Value, "FLS", invoiceref);
            }
            return 1;
        }
        public static int AddIvoiceItem(int PortfolioID, int ServiceID, int IncidentID, double QTY, int ServiceTypeID, string type, string servicetext, int servicetypeid, double cost, int invoiceRef, bool applyVAT = true)
        {
            var vt = 0.00;
            if (applyVAT)
            {
                vt = VATByCustomerBAL.VATByCustomer_select(PortfolioID);
            }
            var v = (QTY * cost);
            if (vt > 0)
                v = (QTY * cost) * (vt / 100);
            else
                v = 0.00;
            SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 8);
            OutVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Service_Insert",
                new SqlParameter("@ServiceID", ServiceID),
                new SqlParameter("@IncidentID", IncidentID),
                new SqlParameter("@QTY", QTY),
                new SqlParameter("@Type", type),
                new SqlParameter("@ServiceTypeID", ServiceTypeID),
                new SqlParameter("@ServiceDescription", servicetext),
            new SqlParameter("@FixedRateTypeID", servicetypeid),
            new SqlParameter("@cost", cost), new SqlParameter("@VAT", v),
            new SqlParameter("@PriceID", invoiceRef)
                , OutVal);

            //update the price
            IncidentService_Price.IncidentService_Price_Select(IncidentID, "FLS", invoiceRef);


            return int.Parse(OutVal.Value.ToString());
        }
            public static DC.Entity.DefaultData DefaultData_Update(DC.Entity.DefaultData d)
        {
            IDCRespository<DC.Entity.DefaultData> dRep = new DCRepository<DC.Entity.DefaultData>();
            var dEntity = dRep.GetAll().Where(o => o.ID == d.CustomerID).FirstOrDefault();
            if (dEntity != null)
            {
                dEntity.InvoiceSeed = d.InvoiceSeed;
                if ((d.SiteID.HasValue ? d.SiteID.Value : 0) > 0)
                    dEntity.SiteID = d.SiteID;
                dEntity.CustomerID = d.CustomerID;
                dRep.Add(dEntity);
            }
            else
            {
                dEntity = new DefaultData();
                dEntity.InvoiceSeed = d.InvoiceSeed;
                dEntity.SiteID = d.SiteID;
                dEntity.CustomerID = d.CustomerID;
                dRep.Add(dEntity);
            }
            return d;
        }
        public static DC.Entity.DefaultData DefaultData_Select(int CustomerID)
        {
            IDCRespository<DC.Entity.DefaultData> dRep = new DCRepository<DC.Entity.DefaultData>();
            var dEntity = dRep.GetAll().Where(o => o.CustomerID == CustomerID).FirstOrDefault();
            return dEntity;
        }
        public static int DefaultData_GetNewInvoiceRef(int CustomerID)
        {
            int retval = 0;
            IDCRespository<DC.Entity.DefaultData> dRep = new DCRepository<DC.Entity.DefaultData>();
            var dEntity = dRep.GetAll().Where(o => o.CustomerID == CustomerID).FirstOrDefault();

            using (DCDataContext d = new DCDataContext())
            {
                var IList = from p in d.Incident_ServicePrices
                            join c in d.CallDetails on p.IncidentID equals c.ID
                            where c.CompanyID == CustomerID
                            select p;

                var cnt = IList.Count();
                if (dEntity != null)
                    retval = (dEntity.InvoiceSeed.HasValue ? dEntity.InvoiceSeed.Value : 0) + (cnt + 1);
                else
                    retval = 0 + (cnt + 1);
            }

            return retval;

        }
        //update invoice information on Incident_ServicePrice
        public static DC.Entity.Incident_ServicePrice Incident_ServicePrice_SelectByID(int ID)
        {
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            var dEntity = dRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            return dEntity;

        }
        public static List<DC.Entity.Incident_ServicePrice> Incident_ServicePrice_SelectByCallID(int CallID)
        {
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            var dlist = dRep.GetAll().Where(o => o.IncidentID == CallID).ToList();
            return dlist;

        }

        public static List<DC.Entity.Incident_Service> Incident_Service_SelectByCallID(int CallID)
        {
            IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            var dlist = dRep.GetAll().Where(o => o.IncidentID == CallID).ToList();
            return dlist;

        }
        public static List<DC.Entity.Incident_Service> Incident_Service_SelectByCallID(int CallID,int ServicePriceID)
        {
            IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            var dlist = dRep.GetAll().Where(o => o.IncidentID == CallID && o.Incident_ServicePriceID == ServicePriceID).ToList();
            return dlist;

        }
        public static List<DC.Entity.Incident_Service> Incident_Service_SelectByPriceID(int PriceID)
        {
            IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            var dlist = dRep.GetAll().Where(o => o.Incident_ServicePriceID == PriceID).ToList();
            return dlist;

        }
        public static DC.Entity.Incident_Service Incident_Service_SelectByID(int ID)
        {
            IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            var dlist = dRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
            return dlist;

        }
        public static DC.Entity.Incident_ServicePrice Incident_ServicePrice_AddInvoiceReference(DC.Entity.Incident_ServicePrice s, int CustomerID,int userid=0)
        {

            var invoiceRef = DefaultData_GetNewInvoiceRef(CustomerID);
            //IDCRespository<DC.Entity.Incident_Service> dRep = new DCRepository<DC.Entity.Incident_Service>();
            IDCRespository<DC.Entity.Incident_ServicePrice> dRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            var d = dRep.GetAll().Where(o => o.ID == s.ID).FirstOrDefault();
            if (d == null)
            {
                d = new Incident_ServicePrice();
                d.IncidentID = s.IncidentID;
                d.InvoiceDescription = s.InvoiceDescription;
                d.InvoiceRef = invoiceRef.ToString();
                if (userid == 0)
                    d.LoggedBy = sessionKeys.UID;
                else
                    d.LoggedBy = userid;
                d.LoggedDate = DateTime.Now;
                d.DiscountPercent = 0;
                d.DiscountPrice = 0;
                d.ModifiedDate = DateTime.Now;
                d.OriginalPrice = 0;
                d.RevicedPrice = 0;
                //default value
                d.Type = "FLS";
                d.UnitConsumption = 0;
                dRep.Add(d);
            }
            return d;

        }


        public static void SendMailToCustomer(int PriceID, List<ToEmailCalss> tlist = null)
        {
            try
            {
                var aRepository = new DCRepository<DC.Entity.FixedPriceApproval>();
                var inv = aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                if (inv == null)
                {
                    var ent = new DC.Entity.FixedPriceApproval();
                    ent.CallID = QueryStringValues.CallID;
                    ent.ModifiedDate = DateTime.Now;
                    ent.DeniedBy = sessionKeys.UID;
                    aRepository.Add(ent);
                }
                else
                {
                    inv.DeniedBy = sessionKeys.UID;
                    inv.ModifiedDate = DateTime.Now;
                    aRepository.Edit(inv);
                }
                IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
                var inDetails = inRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();

                if (inDetails != null)
                {
                    // lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
                }
                else
                {
                    inDetails = new CallInvoice();
                    inDetails.CreatedDate = DateTime.Now;
                    inDetails.CallID = QueryStringValues.CallID;
                    inRepository.Add(inDetails);
                }

                FLS_SendMailtoRequester(inDetails, PriceID, tlist);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void SendMailToCustomerFromApp(int portfolioid,int callid,int userid, int priceid)
        {
            try
            {
                var aRepository = new DCRepository<DC.Entity.FixedPriceApproval>();
                var inv = aRepository.GetAll().Where(o => o.CallID == callid).FirstOrDefault();
                if (inv == null)
                {
                    var ent = new DC.Entity.FixedPriceApproval();
                    ent.CallID = callid;
                    ent.ModifiedDate = DateTime.Now;
                    ent.DeniedBy = userid;
                    aRepository.Add(ent);
                }
                else
                {
                    inv.DeniedBy = userid;
                    inv.ModifiedDate = DateTime.Now;
                    aRepository.Edit(inv);
                }
                IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
                var inDetails = inRepository.GetAll().Where(o => o.CallID == callid).FirstOrDefault();

                if (inDetails != null)
                {
                    // lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
                }
                else
                {
                    inDetails = new CallInvoice();
                    inDetails.CreatedDate = DateTime.Now;
                    inDetails.CallID =callid;
                    inRepository.Add(inDetails);
                }

                FLS_SendMailtoRequesterNew(portfolioid, callid, userid,inDetails, priceid, null);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void FLS_SendMailtoRequester(CallInvoice invoice, int PriceID, List<ToEmailCalss> tlist = null)
        {
            try
            {
                int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                DC.SRV.WebService ws = new DC.SRV.WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, sessionKeys.PortfolioID);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DC.DAL.DCDataContext dc = new DCDataContext())
                    {
                        var fls = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                        var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid && c.Incident_ServicePriceID == PriceID).ToList();
                        var pricelist = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid && c.ID == PriceID).OrderByDescending(o => o.ID).FirstOrDefault();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Invoice from "+ portfolio.PortFolio;//"Job Reference: " + fls.CCID.ToString();
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoice.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                        body = body.Replace("[noteslist]", GetNotesList(noteslist, stypelist));



                        body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        var sp = 0.00;
                        var vat = 0.00;
                        if (pricelist != null)
                            sp = pricelist.OriginalPrice.HasValue ? pricelist.OriginalPrice.Value : 0.00;

                        //var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));

                        body = body.Replace("[discount]", "--------");
                        body = body.Replace("[vat]", string.Format("{0:F2}", vat));
                        body = body.Replace("[sub]", string.Format("{0:F2}", sp));
                        body = body.Replace("[invno]", pricelist.InvoiceRef.ToString());
                        body = body.Replace("[refno]", "" + fls.CCID.ToString());
                        body = body.Replace("[date]", invoice.CreatedDate.Value.ToShortDateString());
                        body = body.Replace("[company]", portfolio.PortFolio);
                        body = body.Replace("[bank]", portfolio.BankName);
                        body = body.Replace("[account]", portfolio.AccountNumber);
                        body = body.Replace("[taxreg]", portfolio.TaxReg);
                        body = body.Replace("[sortcode]", portfolio.SortCode);
                        body = body.Replace("[iban]", portfolio.IBAN);
                        body = body.Replace("[swiftcode]", portfolio.SwiftCode);
                        if ((pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) > 0)
                        {
                            var tPrice = string.Format("{0:F2}", (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tTax = string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) - (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tStr = string.Format("<tr><td>Your Price:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tPrice);
                            tStr = tStr + string.Format("<tr><td>Tax:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tTax);
                            body = body.Replace("[specialprice]", tStr);

                            body = body.Replace("[gtotal]", string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00)));
                        }
                        else
                        {
                            body = body.Replace("[specialprice]", string.Empty);
                            body = body.Replace("[gtotal]", string.Format("{0:F2}", sp));
                        }

                        var uqid = GenerateButtonUrl(sessionKeys.PortfolioID, sessionKeys.UID, QueryStringValues.CallID, QueryStringValues.CCID, PriceID);
                        body = body.Replace("[link]", string.Format("{0}/WF/payinvoice/PaymentProcess.aspx?uqid={1}", Deffinity.systemdefaults.GetWebUrl(), uqid));

                        //[date]

                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester

                        Email er = new Email();
                        List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                        if (Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString() + "-" + PriceID.ToString())))
                        {
                            string[] S1 = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString() + "-" + PriceID.ToString()));
                            foreach (string fileName in S1)
                            {
                                a.Add(new System.Net.Mail.Attachment(fileName));
                            }
                        }
                        if (tlist != null)
                        {
                            if (tlist.Count > 0)
                            {
                                foreach (var t in tlist)
                                {
                                    var body1 = body;
                                    body1 = body1.Replace("[user]", pcontact.Name);
                                    //em.SendingMail(fromemailid, subject, body1, t.email);
                                    er.SendingMail(t.email, subject, body1, fromemailid, a);
                                }
                            }
                            else
                            {
                                body = body.Replace("[user]", pcontact.Name);
                                // em.SendingMail(fromemailid, subject, body, pcontact.Email);
                                er.SendingMail(pcontact.Email, subject, body, fromemailid, a);
                            }
                        }
                        else
                        {
                            body = body.Replace("[user]", pcontact.Name);
                            // em.SendingMail(fromemailid, subject, body, pcontact.Email);
                            er.SendingMail(pcontact.Email, subject, body, fromemailid, a);


                        }

                       

                        UpdateInvoiceStatus_SendtoCustomer(callid, PriceID);

                        addtocomunications(fromemailid, fls, pcontact, subject, body);

                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void FLS_SendMailtoRequesterNew(int portfolioID,int callid,int userid,CallInvoice invoice, int PriceID, List<ToEmailCalss> tlist = null)
        {
            try
            {
                //int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail(portfolioID);
                DC.SRV.WebService ws = new DC.SRV.WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(portfolioID);

                ef = FooterEmail.EmailFooter_selectByID(6, portfolioID);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DC.DAL.DCDataContext dc = new DCDataContext())
                    {
                        var fls = FLSDetailsBAL.Jqgridlist(callid,portfolioID).FirstOrDefault();
                        var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == portfolioID).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid && c.Incident_ServicePriceID == PriceID).ToList();
                        var pricelist = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid && c.ID == PriceID).OrderByDescending(o => o.ID).FirstOrDefault();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Invoice from " + portfolio.PortFolio;//"Job Reference: " + fls.CCID.ToString();
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoice.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(portfolioID));
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                        body = body.Replace("[noteslist]", GetNotesList(noteslist, stypelist));



                        body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        var sp = 0.00;
                        var vat = 0.00;
                        if (pricelist != null)
                            sp = pricelist.OriginalPrice.HasValue ? pricelist.OriginalPrice.Value : 0.00;

                        //var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));

                        body = body.Replace("[discount]", "--------");
                        body = body.Replace("[vat]", string.Format("{0:F2}", vat));
                        body = body.Replace("[sub]", string.Format("{0:F2}", sp));
                        body = body.Replace("[invno]", pricelist.InvoiceRef.ToString());
                        body = body.Replace("[refno]", "" + fls.CCID.ToString());
                        body = body.Replace("[date]", invoice.CreatedDate.Value.ToShortDateString());
                        body = body.Replace("[company]", portfolio.PortFolio);
                        body = body.Replace("[bank]", portfolio.BankName);
                        body = body.Replace("[account]", portfolio.AccountNumber);
                        body = body.Replace("[taxreg]", portfolio.TaxReg);
                        body = body.Replace("[sortcode]", portfolio.SortCode);
                        body = body.Replace("[iban]", portfolio.IBAN);
                        body = body.Replace("[swiftcode]", portfolio.SwiftCode);
                        if ((pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) > 0)
                        {
                            var tPrice = string.Format("{0:F2}", (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tTax = string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00) - (pricelist.FinalPrice.HasValue ? pricelist.FinalPrice.Value : 0.00));
                            var tStr = string.Format("<tr><td>Your Price:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tPrice);
                            tStr = tStr + string.Format("<tr><td>Tax:</td><td style='text-align:right'><strong> {0}</strong></td></tr>", tTax);
                            body = body.Replace("[specialprice]", tStr);

                            body = body.Replace("[gtotal]", string.Format("{0:F2}", (pricelist.FinalPriceIncludeTax.HasValue ? pricelist.FinalPriceIncludeTax.Value : 0.00)));
                        }
                        else
                        {
                            body = body.Replace("[specialprice]", string.Empty);
                            body = body.Replace("[gtotal]", string.Format("{0:F2}", sp));
                        }

                        var uqid = GenerateButtonUrl(portfolioID,userid, callid, fls.CCID, PriceID);
                        body = body.Replace("[link]", string.Format("{0}/WF/payinvoice/PaymentProcess.aspx?uqid={1}", Deffinity.systemdefaults.GetWebUrl(), uqid));

                        //[date]

                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester


                        if (tlist != null)
                        {
                            if (tlist.Count > 0)
                            {
                                foreach (var t in tlist)
                                {
                                    var body1 = body;
                                    body1 = body1.Replace("[user]", pcontact.Name);
                                    em.SendingMail(fromemailid, subject, body1, t.email);
                                }
                            }
                            else
                            {
                                body = body.Replace("[user]", pcontact.Name);
                                em.SendingMail(fromemailid, subject, body, pcontact.Email);
                            }
                        }
                        else
                        {
                            body = body.Replace("[user]", pcontact.Name);
                            em.SendingMail(fromemailid, subject, body, pcontact.Email);

                        }



                        UpdateInvoiceStatus_SendtoCustomer(callid, PriceID);

                        addtocomunications(fromemailid, fls, pcontact, subject, body);

                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public static void addtocomunications(string fromemailid, Jqgrid fls, PortfolioContact pcontact, string subject, string body)
        {
            try
            {
                var p = new PortfolioMgt.Entity.EquipmentClientCommunication();
                p.AssetID = 0;
                p.ClientID = fls.RequesterID;
                p.DateandTimeEmailSent = DateTime.Now;
                p.FromEmail = fromemailid;
                p.MailSubject = subject;
                p.MailSentByID = sessionKeys.UID;
                p.ToEmail = pcontact.Email;
                p.MailBody = HttpContext.Current.Server.HtmlEncode(body);

                PortfolioMgt.BAL.EquipmentClientCommunicationBAL.EquipmentClientCommunicationBAL_Add(p);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static string GenerateButtonUrl(int portfolioid, int userid, int callid, int ccid, int invoiceid)
        {
            string retval = string.Empty;
            try
            {
                var udate = string.Format("portfolioid:{0},userid:{1},callid:{2},ccid:{3},invid:{4}", portfolioid, userid, callid, ccid, invoiceid);
                var uqid = Guid.NewGuid().ToString();
                var d = DC.BLL.MailDataProcessBAL.MailDataProcess_Add(new MailDataProcess() { UQID = uqid, IsActive = true, UQValues = udate });
                if (d != null)
                {
                    retval = d.UQID;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }
        public static string GetNotesList(List<Incident_Service> noteslist, List<FixedRateType> stypelist)
        {
            StringBuilder sbuild = new StringBuilder();
            if (noteslist.Count > 0)
            {
                UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();

                sbuild.Append("<table style='width:100%'>");
                sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                sbuild.Append("<td style='width:50%'>Item</td><td>Type</td><td>Quantity</td><td> Unit Price</td><td>TAX</td><td>Total</td>");
                sbuild.Append("</tr>");
                foreach (var n in noteslist)
                {
                    double vTotal = 0;
                    if (n.VAT.HasValue)
                    {
                        if (n.VAT.Value > 0)
                        {
                            vTotal = (n.QTY.Value * n.SellingPrice.Value) + n.VAT.Value;
                        }
                        else
                        {
                            vTotal = (n.QTY.Value * n.SellingPrice.Value);
                        }
                    }
                    string FixedRateTypeName = string.Empty;
                    var f = stypelist.Where(o => o.FixedRateTypeID == (n.FixedRateTypeID.HasValue ? n.FixedRateTypeID.Value : 0)).FirstOrDefault();
                    if (f != null)
                        FixedRateTypeName = f.FixedRateTypeName;

                    sbuild.Append("<tr>");
                    sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td><td style='text-align:right'>{4}</td><td style='text-align:right'>{5}</td>", n.ServiceDescription, FixedRateTypeName, n.QTY, string.Format("{0:F2}", n.SellingPrice), string.Format("{0:F2}", n.VAT), string.Format("{0:F2}", vTotal)));

                    sbuild.Append("</tr>");
                }
                sbuild.Append("</table>");
            }

            return sbuild.ToString();
        }

        public static List<InvoiceStatus> GetInvoiveStatus()
        {
            List<InvoiceStatus> sList = new List<InvoiceStatus>();
            sList.Add(new InvoiceStatus() { ID = 0, Name = "Please select..." });
            sList.Add(new InvoiceStatus() { ID = 1, Name = "Pending" });
            sList.Add(new InvoiceStatus() { ID = 2, Name = "Sent to Customer" });
            sList.Add(new InvoiceStatus() { ID = 3, Name = "Paid" });
            sList.Add(new InvoiceStatus() { ID = 4, Name = "Cancelled" });
            sList.Add(new InvoiceStatus() { ID = 5, Name = "Credit Note Issued" });
            return sList;
        }

        public class InvoiceStatus
        {

            public int ID { set; get; }
            public string Name { set; get; }
        }
        public static void UpdateInvoiceStatusJournal(int CallID,int PriceID,string statusname)
        {
            bool vDate = false;
            IDCRespository<DC.Entity.CallInvoice> crep = new DCRepository<DC.Entity.CallInvoice>();
            var g = crep.GetAll().Where(o => o.PriceID == PriceID).OrderByDescending(o => o.ID).FirstOrDefault();
            if (g == null)
                vDate = true;
            else
            {
                if (g.StatusValue.ToLower() != statusname.ToLower())
                    vDate = true;
            }

            if(vDate)
            {
                var d = new CallInvoice();
                d.CallID = CallID;
                d.CreatedDate = DateTime.Now;
                d.PriceID = PriceID;
                d.StatusValue = statusname;
                crep.Add(d);
            }
        }

        public static void UpdateInvoiceStatus_SendtoCustomer(int callid,int invoiceref)
        {
            try
            {
                using (DC.DAL.DCDataContext dc = new DCDataContext())
                {
                    var dentity = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid && c.ID == invoiceref).OrderByDescending(o => o.ID).FirstOrDefault();
                    if (dentity != null)
                    {
                        if (dentity.Status != null)
                        {
                            if (dentity.Status.ToLower() != "paid")
                            {
                                dentity.Status = "Sent to Customer";
                                dc.SubmitChanges();
                            }
                        }
                        else
                        {
                            dentity.Status = "Sent to Customer";
                            dc.SubmitChanges();

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        public static Incident_ServicePrice Incident_ServicePrice_UpdateFinalPrice(int callid, int servicePriceID, double finalPrice)
        {
            IDCRespository<DC.Entity.Incident_ServicePrice> qiRep = new DCRepository<DC.Entity.Incident_ServicePrice>();
            var p = qiRep.GetAll().Where(o => o.IncidentID == callid && o.ID == servicePriceID).FirstOrDefault();
            
            if (p != null)
            {
                var slist = InvoiceBAL.Incident_Service_SelectByCallID(callid, p.ID);

                p.FinalPrice = finalPrice - (slist.Count > 0 ? slist.Where(o => o.Incident_ServicePriceID == p.ID).Sum(o => o.VAT.HasValue ? o.VAT.Value : 0) : 0);
                p.FinalPriceIncludeTax = finalPrice ;
                qiRep.Edit(p);
            }

            return p;
        }

    }
}
