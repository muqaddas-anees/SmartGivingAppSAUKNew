
using DC.BLL;
using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.App.controls
{
    public partial class FundraiserPayCtrl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    cpStartDate.ValueToCompare = DateTime.Now.ToShortDateString();
                    SetCardFeePercentage();

                    // BindListviewPaytype();

                    if (Request.RawUrl.ToLower().Contains("orghomenew"))
                    {
                        btnSaveRegion.Visible = false;
                    }
                    hunid.Value = Guid.NewGuid().ToString();
                    var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();
                    var plist = "";
                    if (tithing != null)
                    {
                        sessionKeys.FundPortfolioID = tithing.OrganizationID.HasValue ? tithing.OrganizationID.Value : 0;
                        hPortfolioid.Value = sessionKeys.PortfolioID.ToString();
                        lblTitle.Text = tithing.Title;
                        lblDescription.Text = tithing.Title;


                    }
                    var pdetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.ID == sessionKeys.FundPortfolioID).FirstOrDefault();
                    if (pdetails != null)
                    {
                        // lblOrg.Text = pdetails.PortFolio;
                        huid.Value = pdetails.OrgarnizationGUID.ToString();
                        var tmp = lblplatform.Text;
                        lblplatform.Text = tmp.Replace("<charity name>", pdetails.PortFolio);
                        // lblCompany.Text = " to " + pdetails.PortFolio;
                    }




                    //lblTitle
                    List<int> ld = new List<int>();
                    for (int i = 0; i <= 31; i++)
                        ld.Add(i);

                    txtStartDay.DataSource = ld;
                    txtStartDay.DataBind();
                    //if (sessionKeys.IsService)
                    //{
                    //    BindOrg();
                    //    pnlOrg.Visible = true;
                    //}
                    //if (sessionKeys.SID == 4)
                    //{
                    //    BindOrg();
                    //    pnlOrg.Visible = true;
                    //}

                    if (sessionKeys.Message.Length > 0)
                    {
                        pnlPrice.Visible = false;
                        pnlResult.Visible = true;
                        lblMsgResult.Text = sessionKeys.Message;

                        sessionKeys.Message = string.Empty;
                        // changePanel(false, false, false, false, false, true);
                    }
                    else if (sessionKeys.ErrorMessage.Length > 0)
                    {
                        pnlPrice.Visible = false;
                        pnlResult.Visible = true;
                        lblMsgResult.Text = sessionKeys.ErrorMessage;

                        sessionKeys.ErrorMessage = string.Empty;
                        // changePanel(false, false, false, false, false, true);
                    }
                    else
                    {
                        // changePanel(true, false, false, false, false, false);
                        chkonetime2.Checked = true;
                        chkMontly.Checked = true;

                        pnlResult.Visible = false;
                        pnlPrice.Visible = true;
                        BindListviewData();
                        pamentFieldsDefaults();
                        BindExistingCardDetails();

                        //BindListviewData();
                    }


                }
                CheckGiftAid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void CheckGiftAid()
        {
            try
            {
                using (var cont = new PortfolioDataContext())
                {
                    var settings = cont.InternationalSettings.FirstOrDefault(o => o.PortfolioID == sessionKeys.PortfolioID);
                    if (settings != null)
                    {
                        giftaid.Visible = settings.IsGiftAidEnabled ?? true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void Listview_paymenttype_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    // Suppose you're binding to a collection of objects with properties Param1 and Param2
                    dynamic dataItem = e.Item.DataItem;
                    //ShortCode
                    var lnkSample = (HtmlAnchor)e.Item.FindControl("link_change");
                    lnkSample.Attributes["onclick"] = $"myFunction('{dataItem.FixedFee}', '{dataItem.TransactionPercent}', '{dataItem.ShortCode}'); return false;";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //private void BindListviewPaytype()
        //{
        //    try
        //    {
        //        List<PortfolioMgt.Entity.tblPaymentMethod> plist = new List<PortfolioMgt.Entity.tblPaymentMethod>();
        //        IPortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod> ptRep = new PortfolioRepository<PortfolioMgt.Entity.tblPaymentMethod>();
        //        plist = ptRep.GetAll().Where(o => o.IsActive).OrderBy(p => p.RowID).ToList();
        //        var rlist = (from p in plist
        //                     select new
        //                     {
        //                         p.ID,
        //                         p.ShortCode,
        //                         Name = p.PaymentMethod,
        //                         link = "",
        //                         p.FixedFee,
        //                         p.TransactionPercent,

        //                     }).ToList();
        //        Listview_paymenttype.DataSource = rlist.ToList();
        //        Listview_paymenttype.DataBind();

        //        if (rlist.Count > 0)
        //        {
        //            var setDefaults = rlist.FirstOrDefault();
        //            if (setDefaults != null)
        //            {
        //                hcode.Value = setDefaults.ShortCode;
        //                hfeepercent.Value = (setDefaults.TransactionPercent).ToString();
        //                hfixedamount.Value = setDefaults.FixedFee.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}
        private void SetCardFeePercentage()
        {
            try
            {
                hfeepercent.Value = "0.00";
                var pSettings = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(p => p.PortfolioID == sessionKeys.FundPortfolioID).Where(o => o.IsActive.HasValue ? o.IsActive.Value : false == true).FirstOrDefault();
                if (pSettings != null)
                {
                    hfeepercent.Value = string.Format("{0:F2}", (pSettings.CardFee.HasValue ? pSettings.CardFee.Value : 0));
                    hplatformfee.Value = string.Format("{0:F2}", (pSettings.TransactionFee.HasValue ? pSettings.TransactionFee.Value : 0));
                    hplatformfeepercent.Value = string.Format("{0:F2}", (pSettings.TransactionFee.HasValue ? pSettings.TransactionFee.Value : 0));
                    hfixedamount.Value = string.Format("{0:F2}", (pSettings.FixedPrice.HasValue ? pSettings.FixedPrice.Value : 0));
                    LogExceptions.LogException("Platform fee percent:" + hplatformfeepercent.Value);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //private void BindOrg()
        //{
        //    try
        //    {
        //        var orgList = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_GetOranizationsAll().OrderBy(o => o.PortFolio);
        //        ddlOrg.DataSource = orgList;
        //        ddlOrg.DataTextField = "PortFolio";
        //        ddlOrg.DataValueField = "ID";
        //        ddlOrg.DataBind();

        //        if (orgList.Count() > 0)
        //        {
        //            hPortfolioid.Value = orgList.FirstOrDefault().ID.ToString();
        //            ddlOrg.SelectedValue = orgList.FirstOrDefault().ID.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}
        private string GetCardTypeImage(string cardtype)
        {
            string retval = "~/assets/media/svg/card-logos/visa.svg";
            if (cardtype.ToUpper() == "VISA")
            {
                retval = "~/assets/media/svg/card-logos/visa.svg";
            }
            else if (cardtype.ToUpper() == "MASTERCARD")
            {
                retval = "~/assets/media/svg/card-logos/mastercard.svg";
            }
            else if (cardtype.ToUpper() == "AMEX")
            {
                retval = "~/assets/media/svg/card-logos/american-express.svg";
            }
            return retval;
        }
        private void BindExistingCardDetails()
        {
            try
            {
                var plist = PortfolioMgt.BAL.PaymentCardDetailBAL.PPaymentCardDetailBAL_SelectAll().Where(o => o.UserID == sessionKeys.UID).ToList();

                if (plist.Count > 0)
                {
                    var rlist = (from r in plist
                                 select new
                                 {
                                     r.ID,
                                     name = r.Name,
                                     imageurl = GetCardTypeImage(r.CardType),
                                     cardtype = r.CardType,
                                     CardNumbder = r.CardNumber.Substring(r.CardNumber.Length - 4),
                                     month = r.ExpiryMonth.ToString(),
                                     year = r.ExpiryYear
                                 }).ToList();
                    listCards.DataSource = rlist;
                    listCards.DataBind();
                    hcount.Value = rlist.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindListviewData()
        {
            try
            {
                List<PortfolioMgt.Entity.TithingCategorySetting> plist = new List<PortfolioMgt.Entity.TithingCategorySetting>();

                if (Convert.ToInt32(hPortfolioid.Value) > 0)
                {
                    plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == Convert.ToInt32(hPortfolioid.Value)).ToList();
                    if (plist.Count == 0)
                    {
                        var nlist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == 0 && o.IsActive == true).ToList();
                        foreach (var p in nlist)
                        {
                            p.OrganizationID = Convert.ToInt32(hPortfolioid.Value);
                            PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_Add(p);
                        }
                        //copy new 
                    }
                }

                plist = PortfolioMgt.BAL.TithingCategorySettingBAL.TithingCategorySettingBAL_SelectAll().Where(o => o.OrganizationID == Convert.ToInt32(hPortfolioid.Value) && o.IsActive == true && o.IsHidden == false).ToList();
                //  listCategory.DataSource = plist;
                // listCategory.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public string GetMoreDetails()
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
        }
        protected void btnSaveRegion_Click(object sender, EventArgs e)
        {

            try
            {
                //find the control values
                string moredetails = GetMoreDetails();
                //listCategory

                // var chk = chkonetime1.Checked;
                var chk1 = chkRecurring.Checked;
                var chk_old = chkonetime2.Checked;

                var pfee = 0.00;
                var tfee = 0.00;
                if (chkfee.Checked)
                {
                    pfee = Convert.ToDouble(hplatformfee.Value);
                    tfee = Convert.ToDouble(hfee.Value);
                }
                var total = Convert.ToDouble(hamount.Value);
                if (txtCardNumber.Text.Trim().Length > 0)
                {
                    var d = PortfolioMgt.BAL.PortfolioPaymentSettingsBAL.PortfolioPaymentSettingsBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.FundPortfolioID).Where(o => o.IsActive == true).FirstOrDefault();
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
                            Name = txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(),
                            UserID = sessionKeys.UID,
                            IsActive = true,



                        });
                    }
                    DateTime? startdate = null;
                    DateTime? enddate = null;
                    int daystart = 0;
                    if (cardDetails != null)
                    {
                        var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                        var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                        if (hrec.Value == "2")
                        {
                            startdate = DateTime.Now;
                            enddate = DateTime.Now;
                            daystart = 1;

                            if (txtStartDate.Text.Length > 0)
                                startdate = Convert.ToDateTime(txtStartDate.Text.Trim());

                            //if (txtStartDate.Text.Length > 0)
                            //    enddate = Convert.ToDateTime(txtStartDate.Text.Trim());

                            if (txtStartDay.Text.Length > 0)
                                daystart = Convert.ToInt32(txtStartDay.SelectedValue.Trim());

                            var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                       ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                       sessionKeys.UID, Convert.ToDouble(total), txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), tfee, pfee, hrec.Value == "2" ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                       , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value);
                            //Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval, false);
                            Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval + "&type=" + d.PayType, false);
                            //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                            //    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                            //    sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()), txtNameOnCard.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                            //    , daystart, txtNotes.Text.Trim(), false, 0, moredetails, hunid.Value,QueryStringValues.UNID);
                            //sendThankyouMail(hunid.Value);
                            // sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                            // Response.Redirect(Request.RawUrl, false);
                        }
                        else
                        {
                            if (chkAnonymously.Checked)
                            {
                                var name = "Anonymous";
                                var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value),
                                    tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                     ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                     sessionKeys.UID, Convert.ToDouble(total), name, "", txtPhone.Text.Trim(), tfee, pfee, chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                     , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value);
                                //Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval, false);
                                Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval + "&type=" + d.PayType, false);
                            }

                            else
                            {
                                var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                      ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                      sessionKeys.UID, Convert.ToDouble(total), txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), tfee, pfee, chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                      , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value);
                                //Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval, false);
                                Response.Redirect("~/PayProcess.aspx?frm=fund&refid=" + retval + "&type=" + d.PayType, false);
                                //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                                //    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                                //    sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()), txtNameOnCard.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                                //    , daystart, txtNotes.Text.Trim(), false, 0, moredetails, hunid.Value, QueryStringValues.UNID);
                                //sendThankyouMail(hunid.Value);
                                // sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                                //  Response.Redirect(Request.RawUrl, false);
                            }


                        }
                    }
                }
                else
                {
                    sessionKeys.ErrorMessage = "Please enter valid details";// "Approved";
                    Response.Redirect(Request.RawUrl, false);

                }
            }
            catch (Exception ex)
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

                    //if (txtStartDate.Text.Length > 0)
                    //    startdate = Convert.ToDateTime(txtStartDate.Text.Trim());

                    //if (txtStartDate.Text.Length > 0)
                    //    enddate = Convert.ToDateTime(txtStartDate.Text.Trim());

                    //if (txtStartDay.Text.Length > 0)
                    //    daystart = Convert.ToInt32(txtStartDay.SelectedValue.Trim());

                    //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                    //    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                    //    sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()), txtNameOnCard.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                    //    , daystart, txtNotes.Text.Trim(), false, 0, moredetails, hunid.Value, QueryStringValues.UNID);
                    //sendThankyouMail(hunid.Value);
                    //  sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                    //Response.Redirect(Request.RawUrl, false);                                                       // Response.Redirect(Request.RawUrl, false);
                    // Response.Redirect("~/FundResult.aspx?tunid=" + hunid.Value + "&unid=" + huid.Value, false);

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
                    //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                    //    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue,
                    //    sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()), txtNameOnCard.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                    //    , daystart, txtNotes.Text.Trim(), false, 0, moredetails, hunid.Value, QueryStringValues.UNID);

                    //if (retval == "Approved")
                    //{
                    //    sendThankyouMail(hunid.Value);
                    //    Response.Redirect(Request.RawUrl, false);
                    //    //Response.Redirect("~/DonationResultNew.aspx?unid=" + huid.Value, false);
                    //     Response.Redirect("~/FundResult.aspx?tunid=" + hunid.Value + "&unid=" + huid.Value, false);
                    //    //Response.Redirect(Request.RawUrl, false);
                    //}
                    //else
                    //{
                    //    sessionKeys.ErrorMessage = retval;
                    //    Response.Redirect(Request.RawUrl, false);
                    //}

                }

                //var retval = QuickPayBAL.TithingCardConnectPay(txtCardNumber.Text.Trim(), Convert.ToInt32(hPortfolioid.Value), tithing.ID, txtCardConnectNumber.Text, ddlMonth.SelectedItem.Text.Trim(),
                //    ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2), txtCvv.Text.Trim(), ddlCardType.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()));

                //sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                //Response.Redirect(Request.RawUrl, false);
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
            Response.Redirect("~/App/Donations.aspx", false);
        }

        protected void listCards_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "paynow")
                {

                    string moredetails = GetMoreDetails();
                    var id = e.CommandArgument.ToString();
                    DateTime? startdate = null;
                    DateTime? enddate = null;
                    int daystart = 0;
                    var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.OrganizationID == 0).FirstOrDefault();
                    //get card details
                    var plist = PortfolioMgt.BAL.PaymentCardDetailBAL.PPaymentCardDetailBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(id)).FirstOrDefault();
                    //if (plist != null)
                    //{
                    //    var retval = QuickPayBAL.TithingCardConnectPay(plist.CardNumber, Convert.ToInt32(hPortfolioid.Value), tithing.ID, plist.Name, plist.ExpiryMonth.ToString(),
                    //   plist.ExpiryYear.ToString(), txtCvv.Text.Trim(), ddlCardType.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()));

                    //    sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                    //    Response.Redirect(Request.RawUrl, false);
                    //}

                    var pfee = 0.00;
                    var tfee = 0.00;
                    if (chkfee.Checked)
                    {
                        pfee = Convert.ToDouble(hplatformfee.Value);
                        tfee = Convert.ToDouble(hfee.Value);
                    }

                    if (hrec.Value == "2")
                    {

                        startdate = DateTime.Now;
                        enddate = DateTime.Now;
                        daystart = 1;



                        //if (txtStartDate.Text.Length > 0)
                        //    startdate = Convert.ToDateTime(txtStartDate.Text.Trim());

                        //if (txtStartDate.Text.Length > 0)
                        //    enddate = Convert.ToDateTime(txtStartDate.Text.Trim());

                        //if (txtStartDay.Text.Length > 0)
                        //    daystart = Convert.ToInt32(txtStartDay.Text.Trim());

                        var retval = QuickPayBAL.TithingCardConnectPay(plist.CardNumber, Convert.ToInt32(hPortfolioid.Value), tithing.ID, plist.Name, plist.ExpiryMonth.ToString(),
                       plist.ExpiryYear.ToString(), txtCvv.Text.Trim(), ddlCardType.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim()), txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(),
                       tfee, pfee,
                      hrec.Value == "2" ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                            , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value);
                        sendThankyouMail(hunid.Value);
                        sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                        Response.Redirect(Request.RawUrl, false);
                    }
                    else
                    {
                        var retval = QuickPayBAL.TithingCardConnectPay(plist.CardNumber, Convert.ToInt32(hPortfolioid.Value), tithing.ID, plist.Name, plist.ExpiryMonth.ToString(),
                       plist.ExpiryYear.ToString(), txtCvv.Text.Trim(), ddlCardType.SelectedValue, sessionKeys.UID, Convert.ToDouble(txtTotal.Text.Trim())
                       , txtNameOnCard.Text.Trim() + " " + txtLastname.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), tfee, pfee, chkRecurring.Checked ? chkMontly.Checked ? "Monthly" : "Weekly" : "", startdate, enddate
                            , daystart, txtNotes.Text.Trim(), chkAnonymously.Checked, 0, moredetails, hunid.Value);
                        sendThankyouMail(hunid.Value);
                        sessionKeys.Message = "Thank you for your kind donation";// "Approved";
                        Response.Redirect(Request.RawUrl, false);

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            // hPortfolioid.Value = ddlOrg.SelectedValue.ToString();
            // BindListviewData();
            pamentFieldsDefaults();
            BindExistingCardDetails();
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
                    body = body.Replace("{{category}}", GetDonationCategories(dItem.MoreDetails));
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
        private void changePanel(bool category, bool paymentoption, bool recurring, bool customedetails, bool carddetails, bool Result)
        {
            pnlCategory.Visible = category;
            pnlPaymentOptioin.Visible = paymentoption;
            pnlRecurringOption.Visible = recurring;
            pnlUserInfo.Visible = customedetails;
            pnlnewCard.Visible = carddetails;
            pnlResult.Visible = Result;
        }
        protected void btnNextCategory_Click(object sender, EventArgs e)
        {
            //enable payment option
            changePanel(false, true, false, false, false, false);
        }

        protected void btnNextToPayAdvanced_Click(object sender, EventArgs e)
        {
            //if its recurirng go to 
            if (chkRecurring.Checked)
                changePanel(false, false, true, false, false, false);
            else
                changePanel(false, false, false, true, false, false);
        }

        protected void btnBackToCategory_Click(object sender, EventArgs e)
        {
            changePanel(true, false, false, false, false, false);
        }

        protected void btnNextToCardDetails_Click(object sender, EventArgs e)
        {
            changePanel(false, false, false, false, true, false);
        }

        protected void btnBackToOptions_Click(object sender, EventArgs e)
        {
            changePanel(false, true, false, false, false, false);
        }

        protected void btnRecurringBack_Click(object sender, EventArgs e)
        {
            changePanel(false, true, false, false, false, false);
        }

        protected void btnNextUserInfo_Click(object sender, EventArgs e)
        {
            changePanel(false, false, false, true, false, false);
        }


        public string GetAmounts()
        {
            string retval = string.Empty;
            var tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == QueryStringValues.UNID).FirstOrDefault();

            if (tithing.MasterUNID != null)
            {
                if (tithing.MasterUNID.Length > 0)
                {
                    tithing = PortfolioMgt.BAL.TithingDefaultDetailsBAL.TithingDefaultDetailsBAL_Select().Where(o => o.unid == tithing.MasterUNID).FirstOrDefault();
                }
            }

            var amountlist = PortfolioMgt.BAL.TithingAmountBAL.TithingAmountBAL_SelectAll().Where(o => o.TithingDefaultUnid == tithing.unid).ToList();

            foreach (var p in amountlist)
            {
                selectamount.Visible = true;
                var val = string.Format("{1}{0:N0}", p.Amount, Deffinity.Utility.GetCurrencySymbol());
                //string.Format("{1}{0:N}", tList.Sum(o => o.PaidAmount.HasValue ? o.PaidAmount.Value : 0.00), Deffinity.Utility.GetCurrencySymbol());
                //  retval = retval + string.Format("<div class='col-lg-3 mb-3'><a href='#' onclick='return setmoney({0});'> <span class='badge badge-light-primary money_cls'>{1}</span> </a> <br/> <b>{2}</b><br/> <b>{3}</b>  </div>", p.Amount, val,p.Shortdescription,p.Description);


                retval = retval + string.Format("<div class='col-lg-3 mb-3' onclick='return setmoney({0});'><div class='notice d-flex bg-light-primary rounded border-primary border border-dashed mb-9 p-6'><div class='d-flex flex-stack flex-grow-1'><div class='fw-semibold'><div class='fs-6 text-gray-700 '><a class='fw-bold mb-5 fs-1' onclick='return setmoney({0});'><strong>{1}</strong></a><br><h4 class='text-gray-900 fw-bold'><strong>{2}</strong></h4><div class='fs-4 text-gray-700 pe-7'>{3}</div></div></div></div></div></div>", p.Amount, val, p.Shortdescription, p.Description);


            }
            //var plist = "";
            //if (tithing != null)
            //{
            //     plist = tithing.DefaultValues;
            //}
            //else
            //{
            //    plist = "10,20,50,100";
            //}

            //foreach (var p in plist.Split(','))
            //{
            //    if (p.Trim().Length > 0)
            //    {
            //        var val = string.Format("{0:C2}", Convert.ToDouble(p.Trim()));
            //        retval = retval + string.Format("<div class='col-lg-3 mb-3'><a href='#' onclick='return setmoney({0});'> <span class='badge badge-light-primary money_cls'>{1}</span> </a></div>", p.Trim(), val);
            //    }
            //}

            return retval;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (sessionKeys.UID > 0)
            {
                Response.Redirect("~/App/FundraiserListView.aspx", false);
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
    }
}
