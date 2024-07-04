using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App
{
    public partial class TithingProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (sessionKeys.Message.Length >0)
                {
                    pnlPrice.Visible = false;
                    pnlResult.Visible = true;
                    lblMsgResult.Text = sessionKeys.Message;

                    sessionKeys.Message = string.Empty;
                }
                else
                {
                    pnlResult.Visible = false;
                    pnlPrice.Visible = true;
                    BindListviewData();
                    pamentFieldsDefaults();
                    BindListviewData();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindExistingCardDetails()
        {
            try
            {
                var plist = PortfolioMgt.BAL.PaymentCardDetailBAL.PPaymentCardDetailBAL_SelectAll().Where(o => o.UserID == sessionKeys.UID).ToList();

                if (plist.Count > 0)
                {

                    listCards.DataSource = plist;
                    listCards.DataBind();
                  //  pnl
                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindListviewData()
        {
            try
            {
                var plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == sessionKeys.PortfolioID && o.IsActive == true && o.IsHidden == false).ToList();


                listCategory.DataSource = plist;
                listCategory.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSaveRegion_Click(object sender, EventArgs e)
        {

            try
            {
                var cardDetails = PortfolioMgt.BAL.PaymentCardDetailBAL.PPaymentCardDetailBAL_SelectAll().Where(o => o.CardNumber == txtCardConnectNumber.Text.Trim() && o.UserID == sessionKeys.UID).FirstOrDefault();
                //save card details
                if (cardDetails == null)
                {
                    cardDetails = PortfolioMgt.BAL.PaymentCardDetailBAL.PaymentCardDetailBAL_Add(new PortfolioMgt.Entity.PaymentCardDetail()
                    {
                        CardDisplayNumber = txtCardNumber.Text.Trim(),
                        CardNumber = txtCardConnectNumber.Text.Trim(),
                        CardType = ddlCardType.SelectedValue,
                        cvv = txtCvv.Text.Trim(),
                        ExpiryMonth = ddlMonth.SelectedItem.Text.Trim(),
                        ExpiryYear = ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2),
                        Name = txtNameOnCard.Text.Trim(),
                        UserID = sessionKeys.UID,
                        IsActive = true

                    });
                }

                if (cardDetails != null)
                {
                    var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                    var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                    //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), sessionKeys.PortfolioID, tithing.ID, txtCardConnectNumber.Text, "10",
                    //    "25", txtCvv.Text.Trim(), ddlCardType.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()));

                    sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                    Response.Redirect(Request.RawUrl, false);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mdlManageOptions.Hide();
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), sessionKeys.PortfolioID, tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                //    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()));

                sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void pamentFieldsDefaults()
        {
            try
            {
                //populate month
                string[] Month = new string[] { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
                ddlMonth.DataSource = Month;
                ddlMonth.DataBind();
                //pre-select one for testing
                ddlMonth.SelectedIndex = 4;

                //populate year
                ddlYear.Items.Add("");
                int Year = DateTime.Now.Year;
                for (int i = 0; i < 10; i++)
                {
                    ddlYear.Items.Add((Year + i).ToString());
                }
                //pre-select one for testing
                ddlYear.SelectedIndex = 3;

                // set the url for iframe for card connect payment
                var payDetials = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectByCompany();
                if (payDetials != null)
                {
                    if (payDetials.Host.ToLower().Contains("cardconnect"))
                    {
                        tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Host);
                        pnlCardConnect.Visible = true;
                        pnlCreditCard.Visible = false;
                        rfCardnumber.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/App/TaithingDashboardV2.aspx", false);
        }
    }
}