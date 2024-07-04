using DC.Entity;
using DeffinityAppDev.WF.CustomerAdmin.PayPalPayflowPro;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class UpgradePlanV2 : System.Web.UI.Page
    {
        public const string paymentSucessStatus = "Approved";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    var InTril = PortfolioMgt.BAL.PortfolioBillingManagerBAL.ShowUpgradeOption();
                     
                    var days = 0;
                    if (InTril)
                    {
                        var pr = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Select(sessionKeys.PartnerID);
                        var p = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                        if (pr != null)
                        {
                            if (p.TrailEndDate.HasValue)
                            {
                                var totaldays = pr.TrailDays;
                                if(pr.TrailDays.HasValue)
                                    days = totaldays.Value - (DateTime.Now.Subtract(p.TrailStartDate.Value).Days);

                                if (days < 0)
                                    days = 0;
                            }
                        }
                        lblTitle.Text = string.Format( "You have <b> {0} </b> days left on your free trial.", days );
                    }

                    PanelActive();
                    BindNoofUsers();
                    BindCustomFields();
                   

                    pamentFieldsDefaults();

                    if (Session["amount"] != null && Session["term"] != null)
                    {
                        txtAmount.Text = string.Format("{0:F2}", Session["amount"]);
                        txtAmount.ReadOnly = true;
                        hterm.Value = Session["term"].ToString();
                        Session["amount"] = null;
                        Session["term"] = null;
                    }
                    else
                    {
                        //Response.Redirect("~/WF/CustomerAdmin/UpgradeModules.aspx");
                    }
                }
                HttpContext.Current.Response.AddHeader("Set-Cookie", "HttpOnly;Secure;SameSite=Secure");

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void BindNoofUsers()
        {
            try
            {
                var usrCount = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllActiveUsers(sessionKeys.PortfolioID).Where(o => o.ContractorName != "Frank Boreham").Count();
                for (int i = usrCount; i < 30; i++)
                {
                    ddlUsers.Items.Add(new ListItem(i.ToString(), i.ToString()));

                }

            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region Listview
        private void BindCustomFields()
        {
            var chkValue = chk.Checked;
            try
            {
               
               var pmEntity=  PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_SelectAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                var oList = PortfolioMgt.BAL.PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_SelectAll().Where(o => o.PartnerID == sessionKeys.PartnerID && o.IsActive == true).ToList();// QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o => o.OptionName).ToList();
                var qList = PortfolioMgt.BAL.PortfolioBillingModulesBAL.PortfolioBillingModulesBAL_SelectAll().ToList(); //QuotationBAL.QuotationItem_SelectByCallid(QueryStringValues.CallID).ToList();

                var pList = PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect().OrderBy(o=>o.ModuleName).ToList();// QuotationBAL.QuotationPrice_selectAll(QueryStringValues.CallID);

                if (oList.Count > 0)
                {
                    //if(pmEntity != null)
                    //lblTitle.Text = string.Format( "You have {0} days left on your free trial.", pmEntity.TrailDays);

                    var rLIst = (from o in oList
                                 orderby o.ID ascending
                                 select new
                                 {
                                     o.ID,
                                     o.MonthlyPrice,
                                     o.YearlyPrice,
                                     o.PlanName,
                                     ItemsCountName = " Items",
                                     ModuleSelected = qList.Where(p => p.PortfolioBillingTypeID == o.ID).ToList(),
                                     ModuleList = pList,
                                     Price = chkValue ? o.YearlyPrice : o.MonthlyPrice,
                                     Term = chkValue ? "Year":"Month"
                                     // ItemsList = GetItems(qList.Where(p => p.QuotationOptionID == o.ID).ToList())
                                 }).ToList();
                    list_Customfields.DataSource = rLIst;
                    list_Customfields.DataBind();
                }


                int chk = 0;
                foreach (ListViewDataItem item in list_Customfields.Items)
                {
                    RadioButton chkSelect = (RadioButton)item.FindControl("radiioPlan");
                    Label lblID = (Label)item.FindControl("lblID");
                    HiddenField hMonth = (HiddenField)item.FindControl("hMonth");
                    HiddenField hYear = (HiddenField)item.FindControl("hYear");
                    //
                    if (chkSelect != null)
                    {
                        if (chk == 0)
                        {
                            chkSelect.Checked = true;
                            hamount.Value = hMonth.Value;
                        }
                        chk++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public string GetDesign(string plan)
        {
            string retval = "Standard";
            if (plan == "Standard")
                retval = "blockquote blockquote-info";
            else if (plan == "Advanced")
                retval = "blockquote blockquote-success";
            else if (plan == "Premium")
                retval = "blockquote blockquote-warning";

            return retval;

        }
        protected void list_Customfields_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            list_Customfields.EditIndex = -1;
            //BindCustomFields();
        }
        protected void list_Customfields_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            list_Customfields.EditIndex = e.NewEditIndex;
            //BindCustomFields();
            BindCustomFields();
        }
        protected void list_Customfields_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName == "UpdateItem")
                //{
                //    cb = new CustomFieldsBAL();
                //    CustomField dc = cb.CustomFields_SelectByID(Convert.ToInt32(e.CommandArgument.ToString()));
                //    TextBox txteDescription = (TextBox)e.Item.FindControl("txtLable");
                //    TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");
                //    DropDownList ddltype = (DropDownList)e.Item.FindControl("ddlType");
                //    dc.FieldLable = txteDescription.Text.Trim();
                //    //dc.Cost = Convert.ToDouble(txteCost.Text.Trim());
                //    dc.FieldType = ddltype.SelectedValue;
                //    dc.FieldValue = txtValue.Text.Trim();
                //    cb.CustomFields_update(dc);
                //    lblMsg.Text = "Updated sucessfully";
                //    list_Customfields.EditIndex = -1;
                //    //BindCustomFields();
                //}
                if (e.CommandName == "upgrade")
                {
                    var planid = Convert.ToInt32(e.CommandArgument.ToString());
                    DropDownList ddlTerm = (DropDownList)e.Item.FindControl("ddlTerm");
                    DropDownList ddlUsers = (DropDownList)e.Item.FindControl("ddlUser");
                    HiddenField hMonth = (HiddenField)e.Item.FindControl("hMonth");
                    HiddenField hYear = (HiddenField)e.Item.FindControl("hYear");
                    PortfolioBillingManager pm;
                    PortfolioBilling pb;
                    AddTOBillingManager(planid, ddlTerm.SelectedValue, Convert.ToInt32(ddlUsers.SelectedValue), hMonth.Value, hYear.Value, out pm, out pb);

                    //insert data to billing plan



                    //sessionKeys.ProgrammeName = e.CommandArgument.ToString();
                    Response.Redirect(string.Format("~/WF/CustomerAdmin/UpgradeStatus.aspx?bid={0}&mid={1}", pb.ID, pm.ID));
                }
                if (e.CommandName == "Item")
                {
                    var optionid = Convert.ToInt32(e.CommandArgument.ToString());
                    // Response.Redirect(string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
                }
                //else if (e.CommandName == "Del")
                //{

                //    QuotationOptionsBAL.QuotationOption_DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));

                //    sessionKeys.Message = "Estimate deleted successfully";

                //    //Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID));

                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private static void AddTOBillingManager(int planid, string terms, int usercount, string MonthValue, string YearValue, out PortfolioBillingManager pm, out PortfolioBilling pb)
        {
            var cdate = DateTime.Now;
            //insert into plan id
            var amout = 0.00;
            var peruser = 0.00;
            var noofDays = 0;
            pm = new PortfolioBillingManager();
            if (terms == "Monthly")
            {
                amout = Convert.ToInt32(usercount) * Convert.ToDouble(MonthValue);
                peruser = Convert.ToDouble(MonthValue);
                noofDays = 30;
            }
            else
            {
                amout = Convert.ToInt32(usercount) * Convert.ToDouble(YearValue);
                peruser = Convert.ToDouble(YearValue);
                noofDays = 365;
            }

            pm.Amount = amout;
            pm.AmountPerUser = peruser;
            pm.BillingStatus = "Pending";
            pm.IsPaid = false;
            pm.LoggedDateTime = cdate;
            pm.NumberofUsers = usercount;
            pm.PaymentTerm = terms;
            pm.PlanID = planid;
            pm.PortfolioID = sessionKeys.PortfolioID;
            pm.SubscriptionStartDate = cdate;
            pm.SubscriptionCancelDate = cdate.AddDays(noofDays);


            PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Add(pm);

            pb = new PortfolioBilling();
            pb.Amount = pm.Amount;
            pb.InvoiceSetDate = cdate;
            pb.IsPaid = false;
            pb.MonthlyPaymentDate = cdate;
            pb.NumberofUsers = pm.NumberofUsers;
            pb.PaymentDate = cdate;
            pb.PaymentTerm = pm.PaymentTerm;
            pb.PlanID = pm.PlanID;
            pb.PortfolioID = pm.PortfolioID;
            pb.TransationStartDate = cdate;
            pb.TransationEndDate = cdate;

            PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Add(pb);
        }

        protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    // Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                    var d = e.Item.DataItem as dynamic;

                    try
                    {

                        DropDownList ddlTerm = (DropDownList)e.Item.FindControl("ddlTerm");
                        DropDownList ddlUsers = (DropDownList)e.Item.FindControl("ddlUser");
                        HiddenField hMonth = (HiddenField)e.Item.FindControl("hMonth");
                        HiddenField hYear = (HiddenField)e.Item.FindControl("hYear");

                        Label lbltotal = (Label)e.Item.FindControl("lbltotal");


                        if (ddlTerm.SelectedValue == "Monthly")
                            lbltotal.Text = string.Format("{0:C2}", Convert.ToInt32(ddlUsers.SelectedValue) * Convert.ToDouble(hMonth.Value));
                        else
                            lbltotal.Text = string.Format("{0:C2}", Convert.ToInt32(ddlUsers.SelectedValue) * Convert.ToDouble(hYear.Value));
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                    //if (lbl != null)
                    //{

                    //    //var r = d.IsAplied;
                    //    //if (!r)
                    //    //{
                    //    //    lbl.Visible = false;
                    //    //}
                    //    // BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl);
                    //}
                    HyperLink hlink = (HyperLink)e.Item.FindControl("hlinkItems");
                    if (hlink != null)
                    {
                        var optionID = d.ID;
                        //hlink.NavigateUrl = string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionID, "quote");

                    }
                    //   ModuleSelected=qList.Where(p=>p.PortfolioBillingTypeID == o.ID  ).ToList(),
                    // ModuleList = pList

                    GridView gv = (GridView)e.Item.FindControl("gv");
                    if (gv != null)
                    {
                        var msList = d.ModuleSelected as List<PortfolioBillingModule>;

                        gv.DataSource = (d.ModuleList as List<PortfolioModule>).Where(o => msList.Select(p => p.ModuleID).Contains(o.ModuleID)).ToList();
                        gv.DataBind();


                      
                    }

                    //DropDownList ddl = (DropDownList)e.Item.FindControl("ddlRatetype");
                    //if (ddl != null)
                    //{
                    //    BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl);
                    //}
                    //DropDownList ddl_e = (DropDownList)e.Item.FindControl("ddlRatetype_e");
                    //if (ddl_e != null)
                    //{
                    //    BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl_e);
                    //}
                    //CheckBoxList chkDays = (CheckBoxList)e.Item.FindControl("chkDays");
                    //if (chkDays != null)
                    //{
                    //    BindDays(chkDays, (e.Item.DataItem as v_TimesheetEntryCustom).Days.Split(',').ToList());
                    //}
                    //CheckBoxList chkDays_e = (CheckBoxList)e.Item.FindControl("chkDays_e");
                    //if (chkDays_e != null)
                    //{
                    //    BindDays(chkDays_e, (e.Item.DataItem as v_TimesheetEntryCustom).Days.Split(',').ToList());
                    //}
                    //Panel pnlTime = (Panel)e.Item.FindControl("pnlTime");
                    //Panel pnlHours = (Panel)e.Item.FindControl("pnlHours");
                    //if (pnlTime != null && pnlHours != null)
                    //{
                    //    SetPanleVisibility(pnlTime, pnlHours);
                    //}
                    //Panel pnlTime_e = (Panel)e.Item.FindControl("pnlTime_e");
                    //Panel pnlHours_e = (Panel)e.Item.FindControl("pnlHours_e");
                    //if (pnlTime_e != null && pnlHours_e != null)
                    //{
                    //    SetPanleVisibility(pnlTime_e, pnlHours_e);
                    //}

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        #endregion

        #region Card connect 

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
                var payDetials = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                if (payDetials != null)
                {
                    tokenframe.Src = string.Format("{0}/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D", payDetials.Payment_host);
                    pnlCardConnect.Visible = true;
                    pnlCreditCard.Visible = false;
                    rfCardnumber.Visible = true;
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Get Listview values
            int PlanID = 0;
            int usercount = 0;
            double amount = 0.00;
            double monthvalue = 0.00;
            double yearlyvalue = 0.00;
            string term = chk.Checked? "Yearly": "Monthly";
            usercount = Convert.ToInt32(ddlUsers.SelectedValue);
            foreach (ListViewDataItem item in list_Customfields.Items)
            {
                RadioButton chkSelect = (RadioButton)item.FindControl("radiioPlan");
                Label lblID = (Label)item.FindControl("lblID");
                HiddenField hMonth = (HiddenField)item.FindControl("hMonth");
                HiddenField hYear = (HiddenField)item.FindControl("hYear");
                //
                if (chkSelect != null)
                {
                    PlanID = Convert.ToInt32( lblID.Text);
                    monthvalue = Convert.ToDouble(hMonth.Value);
                    yearlyvalue = Convert.ToDouble(hYear.Value);
                    amount = Convert.ToDouble(monthvalue) * usercount;
                }
            }

            if (Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00") > 0)
            {

                try
                {
                    //insert 
                    PortfolioBillingManager pm;
                    PortfolioBilling pb;
                    int pbid =0;
                    int pmid = 0;
                    // AddTOBillingManager(PlanID, term, Convert.ToInt32(ddlUsers.SelectedValue), monthvalue.ToString(), yearlyvalue.ToString(), out pm, out pb);

                    //payment 
                    var userRep = new UserRepository<UserMgt.Entity.v_contractor>();
                    var userdetailsRep = new UserRepository<UserMgt.Entity.UserDetail>();

                    var user = userRep.GetAll().Where(o => o.ID == sessionKeys.UID).FirstOrDefault();
                    var userdetails = userdetailsRep.GetAll().Where(o => o.UserId == sessionKeys.UID).FirstOrDefault();
                    //var pcontact = pcRep.GetAll().Where(o => o.ID == pAddress.ContactID).FirstOrDefault();

                    var payDetials = UserMgt.BAL.CompanyBAL.CompanyBAL_Select();
                    //Payment details
                    var paymentResult = string.Empty;
                    if (payDetials != null)
                    {
                        paymentResult = CardConnectPay_ByAddress(payDetials, userdetails, user, Convert.ToInt32(ddlUsers.SelectedValue),term, Convert.ToInt32(ddlUsers.SelectedValue), PlanID,out pbid,out pmid );
                    }

                    if (paymentResult == paymentSucessStatus)
                    {
                        
                        var sdate = DateTime.Now;
                        var eDate = term == "Monthly" ? DateTime.Now.AddMonths(1) : DateTime.Now.AddYears(1);
                        var pmNew = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Select(pbid);

                        if(pmNew != null)
                        {
                            pmNew.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00");
                            pmNew.IsPaid = true;
                            pmNew.SubscriptionStartDate = sdate;
                            pmNew.SubscriptionCancelDate = eDate;
                            PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Update(pmNew);

                        }

                        var pbNew = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Select(pmid);
                        if(pbNew != null)
                        {
                            pbNew.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00");
                            pbNew.TransationStartDate = sdate;
                            pbNew.TransationEndDate = eDate;
                            pbNew.IsPaid = true;
                            
                            PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Update(pbNew);
                           // pbNew.PaymentProfile = 
                        }
                        //update the call status

                        // btnBack.Text = "Take another payment";
                        sessionKeys.CompanyAccess = null;
                        sessionKeys.CompanyModules = null;
                        pnlResult.Visible = true;
                        pnlPaymentDetails.Visible = false;
                        lblResultSucess.Visible = true;
                        lblResultSucess.Text = "Payment completed successfully";
                    }
                    else
                    {
                        //LogExceptions.LogException("Callid:" + CallID + ", Invoice ref:" + InvoiceRefID + ", Error:", paymentResult);
                        //update the call status
                        //FLSDetailsBAL.UpdateTicketStatus(CallID, sessionKeys.UID, JobStatus.Cancelled);
                        pnlResult.Visible = true;
                        pnlPaymentDetails.Visible = true;
                        lblResultFail.Visible = true;
                        lblResultFail.Text = "Payment process failed, Please try again";

                        btnBack.Text = "Try payment again";
                    }

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);

                }

            }
            else
            {

                if (Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00") == 0)
                    lblError.Text = "Please enter valid amount";
            }

        }
        public string CardConnectPay_ByAddress(UserMgt.Entity.Company payDetials, UserMgt.Entity.UserDetail userdetails, UserMgt.Entity.v_contractor user,int numberofusers,string term,double peruseramount,int planid,out int pbid,out int pmid)
        {
            string retval = string.Empty;
            pbid = 0;
            pmid = 0;
            try
            {
                
                var cdate = DateTime.Now;
                //var workingdata = CardConnectRestClientExt.authTransactionWithProfile();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var ccnumber = txtCardConnectNumber.Text;
                if (string.IsNullOrEmpty(txtCardConnectNumber.Text))
                {
                    ccnumber = mytoken.Value;
                    if (string.IsNullOrEmpty(ccnumber))
                    {
                        lblError.Text = "Please enter card number";
                        //return;
                    }
                    LogExceptions.LogException("mytoken:" + ccnumber);
                }
                //get invoice data
                var month_year_expiry = ddlMonth.SelectedItem.Text.Trim() + ddlYear.SelectedItem.Text.Substring(ddlYear.SelectedItem.Text.Length - 2);
                var cvv = txtCvv.Text.Trim();
                var MID = payDetials.Payment_vendor;
                var amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00");

                //var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioBilling>();
                var p = new PortfolioMgt.Entity.PortfolioBilling();
                p.Amount = amount;
                p.BillingFrom = payDetials.Name + ", " + payDetials.Address + ", " + payDetials.Town + ", " + payDetials.City + ", " + payDetials.Zipcode;
                if (userdetails != null)
                    p.BillingTo = user.ContractorName + " ," + userdetails.Address1 + userdetails.Address2 + " ," + userdetails.Town + " ," + userdetails.PostCode;
                else
                    p.BillingTo = user.ContractorName;
                p.Currency = "USD";
                p.InvoiceSetDate = DateTime.Now;
                p.IsPaid = false;
                p.MonthlyPaymentDate = DateTime.Now;
                p.NumberofUsers = numberofusers;
                p.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00");
                p.PaymentDate = DateTime.Now;
                p.PortfolioID = sessionKeys.PortfolioID;
                p.SendInvoice = false;
                p.TransationEndDate = term == PortfolioMgt.BAL.PaymentTerm.Monthly ? DateTime.Now.AddMonths(1) : DateTime.Now.AddYears(1);
                p.TransationStartDate = DateTime.Now;
                p.PaymentTerm = term;
                PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Add(p);

                pbid = p.ID;

                var pm = new PortfolioBillingManager();
              

                pm.Amount = Convert.ToDouble(!string.IsNullOrEmpty(txtAmount.Text.Trim()) ? txtAmount.Text.Trim() : "0.00"); ;
                pm.AmountPerUser = peruseramount;
                pm.BillingStatus = "Pending";
                pm.IsActive = false;
                pm.IsPaid = false;
                pm.LoggedDateTime = DateTime.Now;
                pm.NumberofUsers = numberofusers;
                pm.PaymentTerm = term;
                pm.PlanID = planid;
                pm.PortfolioID = sessionKeys.PortfolioID;
                pm.SubscriptionStartDate = cdate;
                pm.SubscriptionCancelDate = term== PortfolioMgt.BAL.PaymentTerm.Monthly ? DateTime.Now.AddMonths(1) : DateTime.Now.AddYears(1); ;


                PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Add(pm);
                pmid = pm.ID;

                List<ReponseValues> rval = CardConnectRestClientExt.authTransactionWithProfile_Recurring(
                    payDetials.Payment_host + "/cardconnect/rest", payDetials.Payment_username, payDetials.Payment_password,
                    //Here will pass MID value
                    MID,
                    //mytoken.value 
                    ccnumber,
                    ddlCardType.SelectedValue,
                    //month year expiry
                    month_year_expiry,
                    txtCvv.Text.Trim(),
                    amount.ToString(),
                    //Order and address details
                    "", user.ContractorName, string.Empty, string.Empty, string.Empty, "USA", string.Empty, String.Format("{0:MMddyyyy}", DateTime.Now));
                //Get Reponse values
                foreach (var r in rval)
                {
                    retval = retval + "key:" + r.key + "value:" + r.value + "/n";
                }
                var ret = rval.Where(o => o.key == "resptext").FirstOrDefault();
                if (ret != null)
                {
                    if (ret.value.ToString() == "Approval")
                    {

                        var addedData = PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Select(p.ID);
                        if (addedData != null)
                        {

                            addedData.PaymentReference = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                            addedData.PaymentProfile = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                            addedData.IsPaid = true;
                            PortfolioMgt.BAL.PortolioBillingBAL.PortolioBillingBAL_Update(addedData);

                        }

                        var pmData = PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Select(pm.ID);
                        if(pmData != null)
                        {
                            pmData.IsActive = true;
                            pmData.IsPaid = true;
                            pmData.PlanID = planid;
                            //pmData.
                            PortfolioMgt.BAL.PortfolioBillingManagerBAL.PortfolioBillingManagerBAL_Update(pmData);
                        }

                        //Process the return values 
                        // ProcessReturnValues(p, rval, InvoiceRefID);
                        //Display success process status
                        retval = "Approved";// ret.value.ToString();

                    }
                    else
                    {

                        //Display faild process status
                        retval = ret.value.ToString();
                    }
                    //ChangePanelVisibility();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;

        }

        private void ProcessReturnValues(PortfolioContactPaymentDetail p, List<ReponseValues> rval, int invoiceid)
        {
            try
            {
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var pE = pmRep.GetAll().Where(o => o.PayID == p.PayID).FirstOrDefault();
                pE.PayPalRef = rval.Where(o => o.key == "retref").FirstOrDefault().value.ToString();
                pE.Payref = rval.Where(o => o.key == "profileid").FirstOrDefault() != null ? rval.Where(o => o.key == "profileid").FirstOrDefault().value.ToString() : string.Empty;
                pE.IsPaid = true;

                pmRep.Edit(pE);

                var ipRep = new DCRepository<Incident_ServicePrice>();
                var pEntity = ipRep.GetAll().Where(o => o.ID == Convert.ToInt32(invoiceid)).FirstOrDefault();
                if (pEntity != null)
                {
                    pEntity.Status = "Paid";
                    pEntity.ModifiedDate = DateTime.Now;
                    ipRep.Edit(pEntity);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(Request.RawUrl, false);

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
        #endregion

        private void PanelActive()
        {
            //if (pnlplans.Visible)
            //{
            //    Next = 1;
            //    btnLastSubmit.Text = "Next";
            //    pnlusers.Visible = !pnlplans.Visible;
            //    pnlpayment.Visible = !pnlplans.Visible;
            //}
            //if (pnlusers.Visible)
            //{
            //    Next = 2;
            //    btnLastSubmit.Text = "Next";
            //    pnlplans.Visible = !pnlusers.Visible;
            //    pnlpayment.Visible = !pnlusers.Visible;
            //}
            //if (pnlpayment.Visible)
            //{
            //    Next = 3;
            //    btnLastSubmit.Text = "Submit";
            //    pnlplans.Visible = !pnlpayment.Visible;
            //    pnlusers.Visible = !pnlpayment.Visible;
            //}
        }
        int Next = 1;
        protected void btnLastSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if(Next != 3)
                Next = Next + 1;
                PanelActive();

               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            lblSelectedPlan.Text = "ANNUAL PLAN COST";
            
           // chk.Checked = !chk_month.Checked;
            chk_month.Checked = !chk.Checked;
           
            


            BindCustomFields();


        }

        protected void chk_month_CheckedChanged(object sender, EventArgs e)
        {
            lblSelectedPlan.Text = "MONTHLY PLAN COST";
            chk.Checked = !chk_month.Checked;
            //chk_month.Checked = !chk.Checked;




            BindCustomFields();


        }
    }
}