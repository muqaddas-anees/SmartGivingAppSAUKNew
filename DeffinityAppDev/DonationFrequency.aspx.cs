using DC.BLL;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace DeffinityAppDev
{
    public partial class DonationFrequency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from query string
                string unid = Request.QueryString["unid"] ?? string.Empty;
                bool chkAnonymously = bool.Parse(Request.QueryString["chkAnonymously"] ?? "false");
                bool chkgift = bool.Parse(Request.QueryString["chkgift"] ?? "false");
                double amountWithoutCharges = double.Parse(Request.QueryString["AmountWithoutCharges"] ?? "0.00");
                double total = double.Parse(Request.QueryString["Total"] ?? "0.00");
                string percentageValueStr = Request.QueryString["PercentageValue"] ?? string.Empty;

                // Convert the string to an integer, default to 0 if conversion fails
                int percentageValue;
                if (!int.TryParse(percentageValueStr, out percentageValue))
                {
                    percentageValue = 0; // Default value if parsing fails
                }
                string code = string.Empty; // Assuming code is not provided in the query string

                // Default values for parameters not available in query string
                DateTime? startdate = null;
                DateTime? enddate = null;
                int dayStart = 0;
                string moredetails = string.Empty;
                string name = string.Empty; // Placeholder value
                var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.FundPortfolioID).Where(o => o.IsActive == true).FirstOrDefault();

                string CardNumber = string.Empty; // Placeholder value
                string month = "";
                string year = "";
                string CVV = string.Empty; // Placeholder value
                bool IsCoverFee = false;
                var hrec = chkRecurring.Checked ? 2 : 1;
                if (hrec == 2)
                {
                    startdate = DateTime.Now;
                    enddate = DateTime.Now;
                    dayStart = 1;
                }

                string cardtype = "";
                int userid = sessionKeys.UID;
                double transactionfee = 0.00; // Assuming default value
                double platformfee = (percentageValue / 100) * amountWithoutCharges; // Assuming default value
                int RecurringPayID = 0; // Placeholder value
                string notes = string.Empty;
                bool giftaid = chkgift; // Assuming giftaid is the same as chkgift

                // Determine if it's recurring or one-time payment
                string recurringtype = chkRecurring.Checked ? (true ? "Monthly" : "Weekly") : string.Empty;
                if (chkRecurring.Checked)
                {
                    startdate = DateTime.Now; // Example value, adjust as needed
                    enddate = DateTime.Now; // Example value, adjust as needed
                    dayStart = 1; // Example value, adjust as needed
                }

                // Call the TithingCardConnectPay method
                var retval = QuickPayBAL.TithingCardConnectPay(name, sessionKeys.PortfolioID, tithing.ID, CardNumber, month, year, CVV, cardtype, userid, total, "Name" + " " + "LastName", "Email", "Phone", transactionfee, platformfee, recurringtype, startdate, enddate, dayStart, notes, chkAnonymously, RecurringPayID, moredetails, unid, QueryStringValues.UNID, code, IsCoverFee, amountWithoutCharges, giftaid);

                // Redirect to PayProcess page
                Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval + "&type=" + d.PayType, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        /*
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
                }*/
  /*      protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? startdate = null;
                DateTime? enddate = null;
                int daystart = 0;
                string moredetails = GetMoreDetails();
                var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.FundPortfolioID).Where(o => o.IsActive == true).FirstOrDefault();
                var pfee = 0.00;
                var tfee = 0.00;
                bool IsCoverFee = false;

                var amout_withoutfee = Convert.ToDouble(txtAmountTotal.Text.Trim());
                var code = hcode.Value;
                if (chkfee.Checked)
                {
                    IsCoverFee = true;
                    pfee = Convert.ToDouble(hplatformfee.Value);
                    tfee = Convert.ToDouble(hfee.Value);
                }
                else
                {
                    IsCoverFee = false;
                }
                var total = hamount.Value;
                if (hrec.Value == "2")
                {

                    startdate = DateTime.Now;
                    enddate = DateTime.Now;
                    daystart = 1;


                    var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                      ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                      sessionKeys.UID, Convert.ToDouble(total), txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), tfee, pfee, hrec.Value == "2" ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                      , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value, QueryStringValues.UNID, code, chkfee.Checked, amout_withoutfee, chkgift.Checked);
                    Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval + "&type=" + d.PayType, false);
                }
                else
                {

                    var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                     ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                     sessionKeys.UID, Convert.ToDouble(total), txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), tfee, pfee, chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                     , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value, QueryStringValues.UNID, code, chkfee.Checked, amout_withoutfee, chkgift.Checked);
                    Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval + "&type=" + d.PayType, false);
       

                }

        
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }*/
/*        public string GetMoreDetails()
        {
            string retval = "";

            try
            {

                foreach (ListViewItem item in listCategory.Items)
                {
                    Label lbl = (Label)item.FindControl("lblCategory");
                    HtmlInputText txtamount = (HtmlInputText)item.FindControl("txtamount_list");

                    try
                    {
                        if (Convert.ToDouble(txtamount.Value.Trim()) > 0)
                            retval = retval + lbl.Text + ":" + txtamount.Value + ";";
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    // Response.Write(str);

                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;
        }*/
        private void sendThankyouMailTracker(int donationid, string mailcontent, string mailsubject, string mailto, int templateid)
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker>();

                rpNew.Add(new PortfolioMgt.Entity.ThankYouMailTracker()
                {
                    DonationID = donationid,
                    MailContent = mailcontent,
                    MailSubject = mailsubject,
                    MailTo = mailto,
                    PortfolioID = sessionKeys.FundPortfolioID,
                    TemplateID = templateid
                    ,
                    SentOn = DateTime.Now,
                    SendBy = sessionKeys.UID
                });
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private void sendThankyouMail(string _unid)
        {

            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.TithingPaymentTracker>();
                var dItem = rpNew.GetAll().Where(o => o.unid == _unid).FirstOrDefault();


                IPortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting> rp = new PortfolioRepository<PortfolioMgt.Entity.TithingThankyouSetting>();
                var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.FundPortfolioID).Where(o => o.AmountGrater >= (dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0) && o.AmountLesser <= (dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0)).FirstOrDefault();
                if (tn == null)
                {
                    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.FundPortfolioID).FirstOrDefault();
                }


                String body = "";
                if (tn != null)
                {
                    body = tn.EmailContent;
                    //{{currentyear}}
                    //{{instancename}}
                    body = body.Replace("{{instancename}}", sessionKeys.FundPortfolioName);
                    body = body.Replace("{{fundraiserdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{currentmonth}}", DateTime.Now.ToString("MMMM"));
                    body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                    body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));
                    body = body.Replace("{{name}}", dItem.DonerName);
                  //  body = body.Replace("{{category}}", GetDonationCategories(dItem.MoreDetails));
                    body = body.Replace("{{signature}}", sessionKeys.FundPortfolioName);
                    body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                    //  body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount));

                    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                    body = body.Replace("{{donorsurname}}", dItem.DonerName);
                    //donorcompany
                    //  body = body.Replace("{{category}}", dItem.CategoryList);

                    body = body.Replace("{{donorcompany}}", sessionKeys.FundPortfolioName);



                    body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));

                    body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                    //logo

                    body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.FundPortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

                }



                if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                {
                    Emailer em = new Emailer();
                    string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                    html_body = html_body.Replace("[table]", body);
                    body = html_body;

                    string fromid = Deffinity.systemdefaults.GetFromEmail();

                    string toid = dItem.DonerEmail;
                    string subject = "Donation";
                    //htomail.Value = toid;
                    //hsubject.Value = subject;
                    //CKEditor1.Text = body;
                    Email ToEmail = new Email();


                    ToEmail.SendingMail(fromid, subject, body, toid, "");

                    sendThankyouMailTracker(dItem.ID, body, subject, toid, tn.ID);
                    // sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                    //Email ToEmail = new Email();


                    //ToEmail.SendingMail(fromid, subject,body,toid,"");

                    //sessionKeys.Message = "Your message is on it's way!";

                    //Response.Redirect(Request.RawUrl, false);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}