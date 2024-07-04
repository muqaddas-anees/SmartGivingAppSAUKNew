using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev
{
    public partial class CheckBalance : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // await DisplayStripeBalance();
                Page.RegisterAsyncTask(new PageAsyncTask(DisplayStripeBalance));
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
    }
}