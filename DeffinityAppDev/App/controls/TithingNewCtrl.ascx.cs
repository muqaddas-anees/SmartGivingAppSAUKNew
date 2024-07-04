using DC.BLL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TuesPechkin;

namespace DeffinityAppDev.App.controls
{
    public partial class TithingNewCtrl : System.Web.UI.UserControl
    {
        public static int step=1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
{
                    hunid.Value = Guid.NewGuid().ToString();
                    step = 1;
                    PanelChange();
                    txtRecurringDate.Text = DateTime.Now.ToShortDateString();
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
                    //  BindReligion();
                    //if (Request.QueryString["mid"] != null)
                    //{
                    // var uid = Convert.ToInt32(Request.QueryString["mid"].ToString());

                    IPortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail> pRep = new PortfolioRepository<PortfolioMgt.Entity.TithingDefaultDetail>();
                    var tithingDetail = pRep.GetAll().Where(o => o.OrganizationID == 0).FirstOrDefault();

                    if (tithingDetail != null)
                    {
                        var vValues = tithingDetail.DefaultValues.Split(',').Where(o => o.Trim().Length > 0).ToList();
                        listamount.DataSource = vValues;
                        listamount.DataBind();
                        string curr = tithingDetail.Currency;
                    }



                    //var tithinglist = pRep.GetAll().Where(o => o.OrganizationID > 0).ToList();

                    //if (tithinglist != null)
                    //{
                    //    var vValues = tithinglist.OrderByDescending(o => o.ID).ToList();
                    //    ListFaithGiving.DataSource = vValues;
                    //    ListFaithGiving.DataBind();
                    //    //string curr = tithingDetail.Currency;
                    //}
                }

              

                // }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public class DisplayAmountClass
        {
            public double svalue { set; get; }
        }
        protected void listamount_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                string v = e.CommandArgument.ToString();

                txtOtherAmount.Text = e.CommandArgument.ToString().Trim();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void panelVisibility(bool amout,bool is_recurring,bool recurring,bool card)
        {
            pnlAmount.Visible = amout;
            pnlIsRecurring.Visible = is_recurring;
            pnlRecurring.Visible = recurring;
            pnlCardDetails.Visible = card;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (step == 4)

                {
                    //add pament code
                    var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                    var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                    var retval = QuickPayBAL.TithingCardConnectPay(txtCardName.Text.Trim(), sessionKeys.PortfolioID, tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                        ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), TxtCSV.Text.Trim(), ddlCurrencyCard.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtOtherAmount.Text.Trim()),"","","",0,0, hunid.Value);

                    sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                    Response.Redirect(Request.RawUrl, false);
                    return;
                }

                    step++;

              
                if (step > 4)
                {
                    step = 1;
                }
                PanelChange();
                //Response.Redirect("~/App/AddEducationalVideo.aspx", false);
                //  pnlIsRecurring.Visible = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnWeely_click(object sender, EventArgs e)
        {
            try
            {
                step = 4;
                hRecurring.Value = "Weekly";
                PanelChange();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnMontly_click(object sender, EventArgs e)
        {
            try
            {
                step = 4;
                hRecurring.Value = "Montly";
                PanelChange();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //btnIsRecurring_click
        protected void btnIsRecurring_click(object sender, EventArgs e)
        {
            try
            {
                step = 3;
                hRecurring.Value = "";
                PanelChange();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnNoRecurring_click(object sender, EventArgs e)
        {
            try
            {
                step = 4;
                hRecurring.Value = "";
                PanelChange();
        }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                step--;
                if (step < 1)
                {
                    step = 1;
                }
                PanelChange();



                //if (step == 4)
                //{
                //    step = 1;
                //}
                //Response.Redirect("~/App/AddEducationalVideo.aspx", false);
                //  pnlIsRecurring.Visible = true;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void PanelChange()
        {

            if (step == 1)
            {
                lblheader.Text = "How much would you like to donate?";
                panelVisibility(true, false, false, false);
                btnBack.Visible = false;
                btnNext.Text = "Next";
            }
            else if (step == 2)
            {
                lblheader.Text = "Shall we make this recurring?";
                panelVisibility(false, true, false, false);
                btnBack.Visible = true;
                btnNext.Text = "Next";
            }
            else if (step == 3)
            {
                lblheader.Text = "Great! How often would you like to donate?";
                panelVisibility(false, false, true, false);
                btnBack.Visible = true;
                btnNext.Text = "Next";
            }
            else if (step == 4)
            {
                lblheader.Text = "Payment Information";
                panelVisibility(false, false, false, true);
                btnBack.Visible = true;
                btnNext.Text = "Process Payment";
            }
        }
        private string GetDonationCategories(string details)
        {
            string retval = "";
            if (details != null)
            {
                if (details.Length > 0)
                {
                    var caList = details.Split(';');
                    foreach (string f in caList)
                    {
                        if (f.Length > 1)
                        {
                            retval = retval + f.Split(':').First() + " ";
                        }
                    }
                }
            }

            return retval;

        }

        private void sendThankyouMailTracker(int donationid, string mailcontent,string mailsubject,string mailto,int templateid )
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker> rpNew = new PortfolioRepository<PortfolioMgt.Entity.ThankYouMailTracker>();

                rpNew.Add(new PortfolioMgt.Entity.ThankYouMailTracker()
                {
                    DonationID = donationid, MailContent = mailcontent,
                    MailSubject = mailsubject, MailTo = mailto, PortfolioID = sessionKeys.PortfolioID, TemplateID = templateid
                    , SentOn=DateTime.Now, SendBy=sessionKeys.UID
                });
            }
            catch(Exception ex)
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
                var tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).Where(o=>o.SetAsDefault ==  true).FirstOrDefault();

                if(tn == null)
                    tn = rp.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                String body = "";
                if (tn != null)
                {
                    body = tn.EmailContent;
                    //{{currentyear}}
                    body = body.Replace("{{currentyear}}", DateTime.Now.Year.ToString());
                    body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));
                    body = body.Replace("{{name}}", dItem.DonerName);
                    body = body.Replace("{{category}}", GetDonationCategories( dItem.MoreDetails));
                    body = body.Replace("{{signature}}", sessionKeys.PortfolioName);
                    body = body.Replace("{{date}}", dItem.PaidDate.Value.ToShortDateString());


                  //  body = body.Replace("{{amount}}", string.Format("{0:F2}", dItem.PaidAmount));

                    body = body.Replace("{{donorfirstname}}", dItem.DonerName);
                    body = body.Replace("{{donorsurname}}", dItem.DonerName);
                    //donorcompany
                  //  body = body.Replace("{{category}}", dItem.CategoryList);

                    body = body.Replace("{{donorcompany}}", sessionKeys.PortfolioName);


                    body = body.Replace("{{categorydonationamount}}", string.Format("{0:F2}", dItem.PaidAmount.HasValue ? dItem.PaidAmount.Value : 0));

                    body = body.Replace("{{categorydonationdate}}", dItem.PaidDate.Value.ToShortDateString());
                    body = body.Replace("{{todaysdate}}", DateTime.Now.ToShortDateString());
                    //logo

                    body = body.Replace("{{logo}}", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()) + "' />");

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
                    sendThankyouMailTracker(dItem.ID,body,subject, toid,tn.ID);
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