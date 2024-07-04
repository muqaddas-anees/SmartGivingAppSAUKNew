using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class UpgradeModules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    BindDecription();
                    BindListview();
                    //Get the price from admin
                    var p = DeffinityManager.Portfolio.BAL.PortfolioDefaultsBAL.PortfolioDefaultsBAL_Select();
                    lblPriceperuser.Text = String.Format("{0:C}", p.MonthlyPrice) + " Per User/ Month";
                    //set number of users
                    var uRep = new UserMgt.BAL.ContractorsBAL();
                    int[] sids = { 1, 4 };
                    txtNoofUsers.Text = uRep.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).Count().ToString();
                    LogExceptions.LogException("No.of user:"+ uRep.Contractor_Select_Active().Count().ToString());

                    hmonthprice.Value = (Convert.ToDouble(p.MonthlyPrice) * Convert.ToInt32(txtNoofUsers.Text.Trim())).ToString();
                    hyearprice.Value = ((Convert.ToDouble(p.MonthlyPrice) * Convert.ToInt32(txtNoofUsers.Text.Trim())) * 12).ToString();
                    lblMonthly.InnerHtml = String.Format("{0:C}", Convert.ToDouble(p.MonthlyPrice) * Convert.ToInt32(txtNoofUsers.Text.Trim()));
                    lblYearly.InnerHtml = String.Format("{0:C}", (Convert.ToDouble(p.MonthlyPrice) * Convert.ToInt32(txtNoofUsers.Text.Trim())) * 12);
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

            HttpContext.Current.Response.AddHeader("Set-Cookie", "HttpOnly;Secure;SameSite=Strict");
        }

        private void BindDecription()
        {
            try
            {
                var c = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();

                if (c.UpgradDescription != null)
                {
                    if (!string.IsNullOrEmpty(c.UpgradDescription))
                    {
                        pnlDescription.Visible = true;
                        lblDescription.Text = Server.HtmlDecode( c.UpgradDescription);
                    }
                    else
                    {
                        pnlDescription.Visible = false;
                    }

                }
                else
                {
                    pnlDescription.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindListview()
        {
            try
            {
                var modulelist = PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect().Where(o => o.IsPaid).ToList();
                list_modules.DataSource = modulelist;
                list_modules.DataBind();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected string GetCssImage(dynamic img)
        {
            string retval = "fa-soccer-ball-o";
            if (img != null)
            {
                var t = img;
                retval = t as string;
            }
            return string.Format("<i class='{0}'></i>", retval);
        }

        protected void list_modules_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "view")
                {
                    var arg = e.CommandArgument.ToString();
                    //var optionid = Convert.ToInt32(e.CommandArgument.ToString());
                    lblOptions.Text = arg;
                    if (arg == "Marketing")
                    {
                        viframe.Src = "https://player.vimeo.com/video/436471193";
                        mdlManageOptions.Show();
                    }
                    //else if (arg == "Scheduling in Jobs")
                    //{

                    //}
                    //else if (arg == "Dispatch Board")
                    //{

                    //}
                    else if (arg == "Inventory")
                    {
                        viframe.Src = "https://player.vimeo.com/video/436471336";
                        mdlManageOptions.Show();
                    }
                    else if (arg == "Service Plans")
                    {
                        viframe.Src = "https://player.vimeo.com/video/436471221";
                        mdlManageOptions.Show();
                    }
                    else if (arg == "Equipment")
                    {
                        viframe.Src = "https://player.vimeo.com/video/436471193";
                        mdlManageOptions.Show();
                    }
                    else if (arg == "Reminders")
                    {
                       // viframe.Src = "https://player.vimeo.com/video/436471193";
                    }
                    else if (arg == "Forms")
                    {
                        viframe.Src = "https://player.vimeo.com/video/436471300";
                        mdlManageOptions.Show();
                    }
                    else if (arg == "App")
                    {
                        viframe.Src = "https://player.vimeo.com/video/436471262";
                        mdlManageOptions.Show();
                    }
                    

                    //Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                int noofusers = Convert.ToInt32(!string.IsNullOrEmpty(txtNoofUsers.Text.Trim()) ? txtNoofUsers.Text.Trim() : "0");
                if (noofusers > 0)
                {
                    var p = DeffinityManager.Portfolio.BAL.PortfolioDefaultsBAL.PortfolioDefaultsBAL_Select();
                    Session["usercount"] = noofusers.ToString();
                    if (chkmonth.Checked)
                    {
                        Session["amount"] = Convert.ToDouble(hmonthprice.Value);
                        Session["term"] = PortfolioMgt.BAL.PaymentTerm.Monthly;
                    }

                    if (chkyear.Checked)
                    {
                        Session["amount"] = Convert.ToDouble(hyearprice.Value);
                        Session["term"] = PortfolioMgt.BAL.PaymentTerm.Yearly;
                    }
                    Response.Redirect("~/WF/CustomerAdmin/UpgradePayment.aspx");
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}