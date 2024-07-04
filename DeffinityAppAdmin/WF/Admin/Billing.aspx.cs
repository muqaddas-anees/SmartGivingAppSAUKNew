using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Admin
{
    public partial class Billing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindBilling();
            }

        }

        private void BindBilling()
        {
            try {

                var bList = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_SelectAll().Where(o => o.IsPaid == true).ToList(); ;
                var pList = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll();

                GridBilling.DataSource = (from b in bList
                                          join p in pList on b.PortfolioID equals p.ID
                                          select new
                                          {
                                              b.ID,
                                              b.Amount,
                                              b.BillingFrom,
                                              b.BillingTo,
                                              b.Currency,
                                              b.InvoiceSetDate,
                                              b.IsPaid,
                                              b.MonthlyPaymentDate,
                                              b.Notes,
                                              b.NumberofUsers,
                                              b.PaymentDate,
                                              b.PaymentProfile,
                                              b.PaymentReference,
                                              b.PortfolioID,
                                              p.PortFolio,
                                              b.SendInvoice,
                                              b.TransationEndDate,
                                              b.TransationStartDate,
                                              b.PaymentTerm,

                                          }).OrderByDescending(o => o.ID).ToList();
                GridBilling.DataBind();
                        
            
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridBilling_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "send")
                {
                    var id = Convert.ToInt32(e.CommandArgument.ToString());

                    var bEntity = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Select(id);

                    string ToEmail = string.Empty;
                    var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(bEntity.PortfolioID);
                    ToEmail = pEntity.EmailAddress;
                    var cEntity = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(pEntity.Owner.HasValue ? pEntity.Owner.Value : 0);
                    if (cEntity != null)
                        ToEmail = cEntity.EmailAddress;

                    Email em = new Email();
                    string Body = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/Admin/EmailTemplates/InvoiceMail.html"));
                    Body = Body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    Body = Body.Replace("[Logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                    Body = Body.Replace("[date]", bEntity.MonthlyPaymentDate.HasValue?bEntity.MonthlyPaymentDate.Value.ToShortDateString():"");
                    Body = Body.Replace("[invoiceno]", bEntity.ID.ToString());
                    Body = Body.Replace("[tobill]", bEntity.BillingTo.Replace(",",", <br>"));
                    Body = Body.Replace("[frombill]", bEntity.BillingFrom.Replace(",", ", <br>"));
                    Body = Body.Replace("[itemdescription]", bEntity.PaymentTerm == PortfolioMgt.BAL.PaymentTerm.Monthly ? "Monthly Subscription" : "Yearly Subscription");
                    Body = Body.Replace("[period]", bEntity.TransationStartDate.ToShortDateString() + " - " + bEntity.TransationEndDate.ToShortDateString());
                    Body = Body.Replace("[qty]", "1");
                    Body = Body.Replace("[price]", string.Format("{0:N2}", bEntity.Amount));
                    Body = Body.Replace("[subtotal]", string.Format("{0:N2}", bEntity.Amount));
                    Body = Body.Replace("[tax]", "0.00");
                    Body = Body.Replace("[total]", string.Format("{0:N2}", bEntity.Amount));
                   

                    // Body = Body.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
                    em.SendingMail("info@123servicepro.com","Invoice", Body,ToEmail,"");




                    lblMsg.Text = "Mail has been sent successfully";

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}