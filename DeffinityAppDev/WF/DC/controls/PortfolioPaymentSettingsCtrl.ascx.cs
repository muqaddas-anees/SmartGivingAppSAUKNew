using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class PortfolioPaymentSettingsCtrl : System.Web.UI.UserControl
    {
        string paypal = "paypal";
        string mxmarchant = "mxmerchant";
        string cardconnect = "cardconnect";
        string stripe = "stripe";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                if(d !=  null)
                {
                    SetDefaultValues(d.PayType);

                }
                else
                {
                    SetDefaultValues(ddlpaytype.SelectedValue);
                }
               
                SetVisibility();

                if(Request.QueryString["admin"] != null)
                {
                    pnlHost.Visible = true;
                }
                else
                {
                    pnlHost.Visible = false;
                }
            }
        }
        private void SetVisibility()
        {
            lblConsumerSecret.Text = "Merchant Key";
            lblConsumerKey.Text = "Salt Passphrase";
            pnlPriceid_stripe_monthly.Visible = false;
            pnlPriceID_stripe_yearly.Visible = false;
            if (ddlpaytype.SelectedValue == mxmarchant)
            {
                pnlKey.Visible = true;
                pnlSecret.Visible = true;
                pnlPartner.Visible = false;
                pnlUsername.Visible = false;
                pnlPassword.Visible = false;
                lblVendor.Text = "Merchant";
            }
            //else if (ddlpaytype.SelectedValue == cardconnect)
            //{
            //    pnlKey.Visible = true;
            //    pnlSecret.Visible = true;
            //    pnlPartner.Visible = false;
            //    pnlUsername.Visible = false;
            //    pnlPassword.Visible = false;
            //    pnlHost.Visible = false;
            //    lblVendor.Text = "Merchant ID";
            //    pnlVendor.Visible = true;
                
            //    //lblPartner.Text = "MID";
            //}
            else if (ddlpaytype.SelectedValue == cardconnect)
            {
                pnlKey.Visible = true;
                pnlSecret.Visible = true;
                pnlPartner.Visible = false;
                pnlUsername.Visible = false;
                pnlPassword.Visible = false;
                pnlVendor.Visible = false;
                pnlHost.Visible = false;
                lblVendor.Text = "MID";
                pnlPriceid_stripe_monthly.Visible = false;
                pnlPriceID_stripe_yearly.Visible = false;
                lblConsumerSecret.Text = "Secret key";
                lblConsumerKey.Text = "Publishable key";
                //lblPartner.Text = "MID";
            }
            else
            {
                pnlKey.Visible = false;
                pnlSecret.Visible = true;
                pnlPartner.Visible = false;
                pnlUsername.Visible = false;
                pnlPassword.Visible = false;
                pnlHost.Visible = false;
                lblVendor.Text = "MID";
            }
        }

            private void SetDefaultValues(string paytype)
        {
            try
            {
                //.Where(o=>(o.IsActive.HasValue?true:false)== true)
                var pDetails = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o=>o.PortfolioID == sessionKeys.PortfolioID).Where(o=>o.PayType == paytype).FirstOrDefault();
                if (pDetails != null)
                {
                    txtPartner.Text = pDetails.Partner;
                    txtPassword.Text = pDetails.Password;
                    txtVendor.Text = pDetails.Vendor;
                    txtHost.Text = pDetails.Host;
                    txtUsername.Text = pDetails.Username;
                    txtConsumerKey.Text = pDetails.consumerKey;
                    txtConsumerSecret.Text = pDetails.consumerSecret;
                    ddlpaytype.SelectedValue = pDetails.PayType;
                    txtPriceIDWeekly.Text = pDetails.WeekPriceID;
                    txtPriceIDMonthly.Text = pDetails.MonthlyPriceID;
                    chkActive.Checked = pDetails.IsActive.HasValue ? pDetails.IsActive.Value : false;
                   // txtFee.Text = string.Format("{0:F2}", pDetails.CardFee.HasValue ? pDetails.CardFee.Value : 3);
                    //txtFixedPrice.Text = string.Format("{0:F2}", pDetails.FixedPrice.HasValue ? pDetails.FixedPrice.Value : 0);
                    SetVisibility();
                }
                else
                {
                    txtPartner.Text ="";
                    txtPassword.Text = "";
                    txtVendor.Text = "";
                    txtHost.Text = "";
                    txtUsername.Text = "";
                    txtConsumerKey.Text = "";
                    txtConsumerSecret.Text = "";
                    
                    txtPriceIDWeekly.Text = "";
                    txtPriceIDMonthly.Text = "";
                    chkActive.Checked = false;
                  //  txtFee.Text = string.Format("{0:F2}", 3);
                   // txtFixedPrice.Text = string.Format("{0:F2}", 0);
                    SetVisibility();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (sessionKeys.PortfolioID > 0)
                {
                    PortfolioMgt.Entity.PortfolioPaymentSetting p = new PortfolioMgt.Entity.PortfolioPaymentSetting();
                    p.PortfolioID = sessionKeys.PortfolioID;
                    p.Host = txtHost.Text.Trim();
                    p.Notes = string.Empty;
                    p.Partner = txtPartner.Text.Trim();
                    p.Password = txtPassword.Text.Trim();
                    p.Username = txtUsername.Text.Trim();
                    p.Vendor = txtVendor.Text.Trim();
                    p.consumerKey = txtConsumerKey.Text.Trim();
                    p.consumerSecret = txtConsumerSecret.Text.Trim();
                   // p.PayType = ddlpaytype.SelectedValue;
                    //p.CardFee = Convert.ToDouble(txtFee.Text.Trim());
                    p.MonthlyPriceID = txtPriceIDMonthly.Text.Trim();
                    p.WeekPriceID = txtPriceIDWeekly.Text.Trim();
                    p.IsActive = chkActive.Checked;
                    //if (txtFixedPrice.Text.Trim().Trim() != "")
                    //{
                    //    p.FixedPrice = Convert.ToDouble(txtFixedPrice.Text.ToString());
                    //}
                    var r = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(p);
                    if (r)
                    {
                       //lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;

                        DeffinityManager.ShowMessages.ShowSuccessError(this.Page, Resources.DeffinityRes.UpdatedSuccessfully, "Ok");
                        if (chkActive.Checked)
                        {
                            UpdatePaymentIsActive(ddlpaytype.SelectedValue);

                        }
                    }

                    //update active 

                    SetVisibility();

                    PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.Clear_IsPaymentActive_Session();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //update active status
        private void UpdatePaymentIsActive( string paytype)
        {
            try
            {
                var updatelist = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID && o.PayType != paytype).ToList();

                foreach (var u in updatelist)
                {
                    u.IsActive = false;
                    PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_AddUpdate(u);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultValues(ddlpaytype.SelectedValue);

            SetVisibility();
        }

        protected void btnValidateMID_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var retval = DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro.CardConnectRestClientExt.authMID(txtVendor.Text.Trim(), txtUsername.Text.Trim(), txtPassword.Text.Trim());
               // mdlManageOptions.Show();
                if (retval != null)
                {
                    if (retval == "True")
                    {
                        lblMsg.Text = "MID Successfully Validated";
                    }
                    else
                    {
                        lblMsg.Text = "MID Validation Unsuccessful";
                    }
                }
                else
                {
                    lblMsg.Text = "MID Validation Unsuccessful";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "MID Validation Unsuccessful";
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}