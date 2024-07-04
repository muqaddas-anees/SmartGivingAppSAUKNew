using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;

namespace DeffinityAppDev.WF.DC
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindDashboardValues();
            }
        }

        #region Dashboard display
        private void BindDashboardValues()
        {
            try
            {
                using (DCDataContext DC = new DCDataContext())
                {
                    //New Status
                    //22	New
                    //35	Closed
                    var c = DC.ServiceDesk_DashboardDisplay(sessionKeys.PortfolioID).FirstOrDefault();

                    if (c != null)
                    {
                        lblInvoicedToday.InnerText = string.Format("{0:N2}", c.InvoicedToday);
                        lblInvoicedWeek.InnerText = string.Format("{0:N2}", c.InvoicedWeek);
                        lblInvoicedMonth.InnerText = string.Format("{0:N2}", c.InvoicedMonth);
                        lblInvoicedYear.InnerText = string.Format("{0:N2}", c.InvoicedYear);
                        lblInvoicedAwaiting.InnerText = string.Format("{0:N2}", c.InvoicedAwaiting);
                        lblInvoicedPaid.InnerText = string.Format("{0:N2}", c.InvoicedPaid);
                        //lblInvoicedToday.InnerText = string.Format("{0:N2}", c );
                        //lblNewJobs.InnerText = c.NewCount.ToString();
                        //lblCallback.InnerText = string.Format("{0:N2}", c.MaintenancePlansThisMonth);
                        //lblQuoteAccepted.InnerText = string.Format("{0:N2}", c.quoteValueOfJobs);
                        //lblCompletedJobsAmount.InnerText = string.Format("{0:N2}", c.revenueClosedJobs);
                        //lblThisMonthCompletedJobsAmount.InnerText = string.Format("{0:N2}", c.revenuedueClosedJobs);
                        //lblThisMonthDueAmount.InnerText = string.Format("{0:N2}", c.revenuedueMaintenancePlan);
                    }


                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        #endregion
    }
}