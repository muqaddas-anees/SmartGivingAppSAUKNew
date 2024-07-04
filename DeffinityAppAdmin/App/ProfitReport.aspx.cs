using PortfolioMgt.Entity;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class ProfitReport : System.Web.UI.Page
    {
        private const int DelayDays = 3; // Delay period in days
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // await DisplayStripeBalance();
                Page.RegisterAsyncTask(new PageAsyncTask(DisplayStripeBalance));

                BindGrid();
            }
        }

        private async Task DisplayStripeBalance()
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            //"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"; 
            StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey();

            // Create a new instance of the BalanceService
            var balanceService = new BalanceService();

            // Retrieve the balance
            Balance balance = await balanceService.GetAsync();

            // Display the available balance
            string availableBalance = "";
            foreach (var available in balance.Available)
            {
                availableBalance += $"Amount: {available.Amount / 100.0} {available.Currency.ToUpper()}<br />";
            }
            lblAvailableBalance.Text = "Available Balance: <br />" + availableBalance;

            // Display the pending balance
            string pendingBalance = "";
            foreach (var pending in balance.Pending)
            {
                pendingBalance += $"Amount: {pending.Amount / 100.0} {pending.Currency.ToUpper()}<br />";
            }
            lblPendingBalance.Text = "Pending Balance: <br />" + pendingBalance;
        }


        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!IsPostBack)
        //        {
        //            BindGrid();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}

        protected bool GetVisible(string status)
        {
            if (status == "Pending")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void BindGrid()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
                var prList = prRep.GetAll().ToList();

                IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> tRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                var tList = tRep.GetAll().ToList();
                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var pList = pRep.GetAll().ToList();
                var rList = (from r in pList
                             join p in prList on r.OrganizationID equals p.ID
                             join t in tList on r.TithingID equals t.ID
                             select new
                             {
                                 r.ID,
                                 r.TithingID,
                                 Details = t.Title,
                                 CharityName = p.PortFolio,

                                 r.unid,


                                 r.PaidAmount,
                                 PaidDate = r.PaidDate.HasValue ? r.PaidDate.Value.ToShortDateString() : "",
                                 r.IsPaid,
                                 r.PlegitAmount,
                                 CompanyAccountID = r.CompanyAccountID ?? "",
                                 r.ComapanyAmount,
                                 r.ComapanyFixedFee,
                                 r.CommissionPercent,
                                 r.CommissionValue,

                                 r.StripeSessionID,
                                 StripeStatus = r.StripTransfterStatue ?? "Pending",
                                 StripeDate = r.StripTransfterDate.HasValue ? r.StripTransfterDate.Value.ToShortDateString() : "",
                             }).ToList();



                grid_display.DataSource = rList.ToList().Where(o => o.CompanyAccountID.Length > 0).Where(o => o.StripeStatus != "Pending").Where(o => o.PaidDate.Length > 0).OrderByDescending(j => j.ID).ToList();
                grid_display.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void grid_display_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CheckAndTransfer")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                    var pdata = pRep.GetAll().Where(o => o.ID == index).FirstOrDefault();
                    if (pdata != null)
                    {
                        // GridViewRow row = grid_display.Rows[index];
                        string accountId = pdata.CompanyAccountID; //row.Cells[0].Text;
                        Page.RegisterAsyncTask(new PageAsyncTask(() => CheckAndTransferFunds(accountId, index)));

                    }

                }

                if (e.CommandName == "del")
                {
                    //var id = Convert.ToInt32(e.CommandArgument);
                    //if (id > 0)
                    //{
                    //    IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> pRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
                    //    var p = pRep.GetAll().Where(o => o.ID == id).FirstOrDefault();

                    //    if (p != null)
                    //    {
                    //        pRep.Delete(p);
                    //    }
                    //    // PortfolioTrackerLoginBAL.PortfolioTrackerLoginBAL_Delete(id);
                    //    sessionKeys.Message = Resources.DeffinityRes.Deletedsuccessfully;
                    //    //BindGrid();
                    //    Response.Redirect(Request.RawUrl, false);
                    //}
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        //protected void gvTransfers_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "CheckAndTransfer")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = gvTransfers.Rows[index];
        //        string accountId = row.Cells[0].Text;
        //        Page.RegisterAsyncTask(new PageAsyncTask(() => CheckAndTransferFunds(accountId)));
        //    }
        //}

        private async Task CheckAndTransferFunds(string accountId, int transactionid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var pEntity = pRep.GetAll().Where(o => o.ID == transactionid).FirstOrDefault();

                var amt = pEntity.ComapanyAmount;

                // Set your secret key. Remember to switch to your live secret key in production!
                StripeConfiguration.ApiKey = Deffinity.systemdefaults.GetStripeSecreatKey();

                // Create a new instance of the BalanceService
                var balanceService = new BalanceService();

                // Retrieve the balance
                Balance balance = await balanceService.GetAsync();

                // Check available balance
                long availableAmount = 0;
                foreach (var available in balance.Available)
                {
                    if (available.Currency == "gbp")
                    {
                        //availableAmount = available.Amount;
                        availableAmount = (long)(long?)((amt ?? 0.00) * 100);
                        break;
                    }
                }

                // Retrieve charges (transactions) older than the delay period
                var chargeService = new ChargeService();
                var options = new ChargeListOptions
                {
                    Created = new DateRangeOptions
                    {
                        LessThan = DateTime.UtcNow.AddDays(-DelayDays)
                    },
                    Limit = 100
                };
                var charges = await chargeService.ListAsync(options);
                var eligibleCharges = charges.Where(c => c.Status == "succeeded").ToList();

                if (eligibleCharges.Count > 0 && availableAmount > 1) // Replace with your desired threshold
                {
                    // Create a transfer to a connected Stripe account
                    var transferService = new TransferService();
                    var transferOptions = new TransferCreateOptions
                    {
                        Amount = availableAmount, // amount in cents
                        Currency = "gbp",
                        Destination = accountId, // use the account ID from the GridView
                    };

                    Transfer transfer = await transferService.CreateAsync(transferOptions);

                    if (transfer.Id.Length > 0)
                    {
                        pEntity.StripTransfterStatue = "Processed";
                        pEntity.StripTransfterDate = DateTime.Now;
                        pRep.Edit(pEntity);
                    }
                    // Display transfer details
                    lblTransferDetails.Text = $"Transfer ID: {transfer.Id}, Amount: {transfer.Amount / 100.0} {transfer.Currency.ToUpper()} to {accountId}";
                }
                else
                {
                    lblTransferDetails.Text = "No sufficient available funds to transfer or no eligible charges found.";
                }

                // Display the available balance
                string availableBalance = "";
                foreach (var available in balance.Available)
                {
                    availableBalance += $"Amount: {available.Amount / 100.0} {available.Currency.ToUpper()}<br />";
                }
                lblAvailableBalance.Text = "Available Balance: <br />" + availableBalance;

                // Display the pending balance
                string pendingBalance = "";
                foreach (var pending in balance.Pending)
                {
                    pendingBalance += $"Amount: {pending.Amount / 100.0} {pending.Currency.ToUpper()}<br />";
                }
                lblPendingBalance.Text = "Pending Balance: <br />" + pendingBalance;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }






        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {

        }
    }
}