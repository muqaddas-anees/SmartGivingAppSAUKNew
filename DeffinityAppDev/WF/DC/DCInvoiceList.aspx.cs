using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;
using DC.Entity;


using Microsoft.ApplicationBlocks.Data;
using static DeffinityAppDev.WF.DC.DCQuotationCompare;
//using CardConnectRestClientExample;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCInvoiceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //try
                    //{
                    //    //CardConnectRestClientExample.CardConnectRestClient
                    //    var workingdata = CardConnectRestClientExample.CardConnectRestClientExample.authTransaction();// authTransaction();
                                                                                                                      
                    //}
                    //catch(Exception ex)
                    //{
                    //    LogExceptions.WriteExceptionLog(ex);
                    //}
                    if (Request.QueryString["callid"] != null)
                    {
                        sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                    }
                    if (QueryStringValues.CCID > 0)
                        lblTitle.InnerText = "Invoice for "+sessionKeys.JobDisplayName+" Ref: " + QueryStringValues.CCID + " : " + FLSDetailsBAL.GetJobDetails(QueryStringValues.CallID); 
                    else
                        lblTitle.InnerText = "List of Invoices for " + sessionKeys.JobDisplayName;

                    if (sessionKeys.SID == 4 || sessionKeys.SID == 9)
                    {
                        link_return.HRef = "FLSResourceList.aspx?type=FLS";
                    }
                    else
                    {
                        link_return.HRef = "FLSJlist.aspx?type=FLS";
                        ////}
                    }

                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected bool SetButtonVisible(string status)
        {
            bool retval = false;
            if (status == null)
            {
                retval = true;
            }
            else   
            {
                if (status == "")
                    retval = true;
                else if (status.ToLower() == "pending")
                    retval = true;
            }

            return retval;

        }
        protected void btnRaiseInvoice_Click(object sender, EventArgs e)
        {
            var d = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = QueryStringValues.CallID }, sessionKeys.PortfolioID);
            if (d != null)
                Response.Redirect(string.Format("~/WF/DC/DCServices.aspx?CCID={0}&callid={1}&SDID={1}&ivref={2}", QueryStringValues.CCID, QueryStringValues.CallID, d.ID), false);
            
                
        }

        private void BindGrid()
        {
            try
            {
                var dList = InvoiceBAL.Incident_ServicePrice_SelectByCallID(QueryStringValues.CallID);
                var slist = InvoiceBAL.Incident_Service_SelectByCallID(QueryStringValues.CallID);

                var rlist = (from d in dList
                             select new
                             {
                                 d.ID,
                                 d.InvoiceDescription,
                                 d.IncidentID,
                                 d.InvoiceRef,
                                 d.LoggedBy,
                                 d.LoggedDate,
                                 d.ModifiedDate,
                                 d.Notes,
                                 d.OriginalPrice,
                                 d.RevicedPrice,
                                 FinalPrice = (d.FinalPrice.HasValue ? d.FinalPrice.Value : 0.00),
                                 FinalPriceIncludeTax = (d.FinalPriceIncludeTax.HasValue ? d.FinalPriceIncludeTax.Value : 0.00),
                                 d.Type,
                                 d.UnitConsumption,
                                 d.Status,
                                 VAT = slist.Count > 0 ? slist.Where(o => o.Incident_ServicePriceID == d.ID).Sum(o => o.VAT.HasValue ? o.VAT.Value : 0) : 0,
                                 SubTotal =  slist.Count > 0 ? slist.Where(o => o.Incident_ServicePriceID == d.ID).Sum(o => (o.SellingPrice.HasValue ? o.SellingPrice.Value : 0)* (o.QTY.HasValue ? o.QTY.Value : 0)) : 0
                             }
                             ).ToList();


                var rlistnew = (from d in rlist
                                select new
                                {
                                    d.ID,
                                    d.InvoiceDescription,
                                    d.IncidentID,
                                    d.InvoiceRef,
                                    d.LoggedBy,
                                    d.LoggedDate,
                                    d.ModifiedDate,
                                    d.Notes,
                                    d.OriginalPrice,
                                    RevicedPrice = d.FinalPriceIncludeTax > 0 ? (d.FinalPriceIncludeTax) : d.RevicedPrice,
                                    d.FinalPrice,
                                    d.FinalPriceIncludeTax,
                                    d.Type,
                                    d.UnitConsumption,
                                    d.Status,
                                    VAT  = d.FinalPriceIncludeTax > 0 ? d.FinalPriceIncludeTax -d.FinalPrice : d.VAT,
                                    SubTotal = (d.FinalPrice)>0? (d.FinalPrice):  d.SubTotal


                                }).ToList();

                Grid_services.DataSource = rlistnew.Where(o => o.RevicedPrice > 0).OrderByDescending(o=>o.ID).ToList();
                Grid_services.DataBind();

                if(Grid_services.Rows.Count == 0)
                {
                    try
                    {
                        var olist= QuotationOptionsBAL.QuotationOption_SelectAll().Where(o => o.CallID == QueryStringValues.CallID).ToList();
                        if (olist.Count == 1)
                        {
                            var oEntity = olist.FirstOrDefault();
                            if(oEntity != null)
                            RiseInvoice(oEntity.ID, oEntity.Description);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void RiseInvoice(int OPTIONID, string details)
        {
            IDCRespository<QuotationItem> qrep = new DCRepository<QuotationItem>();
            IDCRespository<QuotationPrice> qprep = new DCRepository<QuotationPrice>();
            //get quote items
            var qlist = qrep.GetAll().Where(o => o.CallidID == QueryStringValues.CallID && o.QuotationOptionID == OPTIONID).ToList();
            //get quote total price
            var qrEntity = qprep.GetAll().Where(o => o.CallID == QueryStringValues.CallID && o.QuotationOptionID == OPTIONID).FirstOrDefault();

            if (qlist.Count > 0)
            {

                var d = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = QueryStringValues.CallID, InvoiceDescription = details }, sessionKeys.PortfolioID);
                if (d != null)
                {
                    int invRef = d.ID;


                    IDCRespository<Incident_Service> irep = new DCRepository<Incident_Service>();
                    IDCRespository<Incident_ServicePrice> iprep = new DCRepository<Incident_ServicePrice>();

                    //insert quote items to invoice
                    foreach (var q in qlist)
                    {
                        var i = new Incident_Service();
                        i.BuyingPrice = q.SellingPrice;
                        i.FixedRateTypeID = q.FixedRateTypeID;
                        i.IncidentID = q.CallidID;
                        i.Incident_ServicePriceID = invRef;
                        i.Notes = q.Notes;
                        i.QTY = q.QTY;
                        i.SellingPrice = Convert.ToDouble(q.QTY > 0 ? q.SalesPrice / q.QTY : 0.00);
                        i.ServiceDescription = q.ServiceDescription;
                        i.ServiceID = q.ServiceID;
                        i.ServiceTypeID = q.ServiceTypeID;
                        i.Type = q.Type;
                        i.VAT = q.VAT;

                        irep.Add(i);
                    }

                    if (d != null)
                    {
                        var dEntity = iprep.GetAll().Where(o => o.ID == d.ID).FirstOrDefault();


                        dEntity.DiscountPercent = qrEntity.DiscountPercent;
                        dEntity.DiscountPrice = qrEntity.DiscountPrice;
                        //d.IncidentID = qrEntity.CallID;
                        //d.InvoiceRef = d.InvoiceRef;
                        dEntity.LoggedBy = sessionKeys.UID;
                        dEntity.LoggedDate = DateTime.Now;
                        dEntity.ModifiedDate = DateTime.Now;
                        dEntity.Notes = qrEntity.Notes;
                        dEntity.OriginalPrice = qrEntity.OriginalPrice;
                        dEntity.RevicedPrice = qrEntity.RevicedPrice;
                        dEntity.Type = qrEntity.Type;
                        dEntity.FinalPrice = qrEntity.FinalPrice;
                        dEntity.FinalPriceIncludeTax = qrEntity.FinalPriceIncludeTax;
                        dEntity.Status = "Pending";
                        iprep.Edit(dEntity);
                    }

                    HttpContext.Current.Response.Redirect(string.Format("~/WF/DC/DCInvoiceList.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID), false);

                }
            }
        }

        protected void grid_delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnDelete = sender as LinkButton;
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Incident_Incident_ServicePrice_Delete",
                     new SqlParameter("@ID", int.Parse(btnDelete.CommandArgument.ToString())));

                //rebind the data
                BindGrid();

            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void Grid_services_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "pay")
                {
                    Response.Redirect(string.Format("~/WF/CustomerAdmin/PayPalPayflowPro/ProcessPayment.aspx?invid={0}&CCID={1}&callid={2}&SDID={2}", e.CommandArgument.ToString(),QueryStringValues.CCID,QueryStringValues.CallID), false);
                }else
                if (e.CommandName == "inv")
                {
                    Response.Redirect(string.Format("~/WF/DC/DCServices.aspx?CCID={0}&callid={1}&SDID={1}&ivref={2}", QueryStringValues.CCID, QueryStringValues.CallID, e.CommandArgument.ToString()), false);
                }
                else if(e.CommandName == "send")
                {
                    var priceID = Convert.ToInt32(e.CommandArgument.ToString());
                    List<ToEmailCalss> tlist = new List<ToEmailCalss>();
                    BindContacts();
                    if (gridContacts.Rows.Count > 1)
                    {
                        hpriceid.Value = priceID.ToString();
                        mdlContacts.Show();
                    }
                    else
                    {


                        InvoiceBAL.SendMailToCustomer(priceID);
                        BindGrid();
                        lblmsg.Text = "Mail sent successfully";
                    }
                  
                   
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void btnSendMailContacts_Click(object sender, EventArgs e)
        {
            try
            {
                List<ToEmailCalss> tlist = new List<ToEmailCalss>();

                if (gridContacts.Rows.Count > 1)
                {
                    for (int i = 0; i < gridContacts.Rows.Count; i++)
                    {
                        GridViewRow grow = gridContacts.Rows[i];

                        Label lblContact = (Label)grow.FindControl("lblContact");
                        Label lblContactEmail = (Label)grow.FindControl("lblContactEmail");

                        CheckBox GridCheckBox = (CheckBox)grow.FindControl("chkContact");
                        if (GridCheckBox.Checked)
                        {
                            tlist.Add(new ToEmailCalss() { name = lblContact.Text, email = lblContactEmail.Text });
                        }
                    }

                    InvoiceBAL.SendMailToCustomer(Convert.ToInt32(hpriceid.Value),tlist);
                    BindGrid();
                    lblmsg.Text = "Mail sent successfully";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void BindContacts()
        {
            try
            {
                var jEntity = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                var gList = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_SelectAll(jEntity.RequesterID);
                if (gList.Count > 0)
                {
                    gList.Add(new PortfolioMgt.Entity.CustomerKeyContact() { Name = jEntity.RequesterName, EmailAddress = jEntity.RequestersEmailAddress });
                    gridContacts.DataSource = gList.OrderBy(o => o.Name).ToList();
                    gridContacts.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}