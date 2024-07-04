using DC.BAL;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using Deffinity.IncidentService;
using Deffinity.IncidentService_Price_Manager;
using Infragistics.WebUI.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DeffinityAppDev.WF.DC
{
    public partial class DCQuotationCompare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bindServiceType();
                    //if option are empty
                    QuotationBAL.AddDefault_Options(QueryStringValues.CallID);
                    if (!string.IsNullOrEmpty(sessionKeys.Message))
                    {
                        lblMsg.Text = sessionKeys.Message;
                        sessionKeys.Message = string.Empty;
                    }
                    //if(QueryStringValues.Type == string.Empty && Request.RawUrl.Contains(""))
                    //{
                    //    Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID),false);
                    //    return;
                    //}
                    if (Request.QueryString["callid"] != null)
                    {
                        sessionKeys.IncidentID = Convert.ToInt32(Request.QueryString["callid"]);
                    }
                    if (QueryStringValues.CCID > 0)
                        lblTitle.InnerText = "Quote Options for " + sessionKeys.JobDisplayName +"  Reference " + QueryStringValues.CCID + ": " + FLSDetailsBAL.GetJobDetails(QueryStringValues.CallID);
                    else
                        lblTitle.InnerText = "Quote Options for " + " " + Resources.DeffinityRes.ServiceDesk;

                    link_return.HRef = string.Format("~/WF/DC/FLSJlist.aspx?type=FLS");

                    UpdateDetailInOption();
                    BindOptions();
                    BindCustomFields();
                    BindUrlToTabs();
                    //bind contacts
                    // BindContacts();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }

        private void UpdateDetailInOption()
        {
            try
            {
                var p = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                if(p != null)
                txtOptionDescription.Text = p.Details;
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string GetPolicyData(int AssignedPolicyID, List<PortfolioMgt.Entity.ProductPolicyType> pd, double total,List<QuotationItem> qItems, QuotationPrice pTotal)
        {
            string retval = string.Empty;
            //2 is default
            //if (AssignedPolicyID == 2)
            //{
            //    retval = string.Format("<div class='form-group'><div class='col-md-6 price_lable'> {0}</div> <div class='col-md-3 price_lable'> {1}</div><div class='col-md-3 price_lable' style='color:#7c38bc;'> {2}</div></div>", "Plan", "Member", "Savings");
            //    foreach (var p in pd.ToList())
            //    {
            //        var d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;
            //        var dtotal = d > 0 ? total - (total * (d / 100)) : total;
            //        retval = retval + string.Format("<div class='form-group'><div class='col-md-6 price_text' style='text-align:right;'> {0}</div> <div class='col-md-3 price_text' style='text-align:right;'> {1}</div><div class='col-md-3 price_text' style='color:#7c38bc;text-align:right;'> {2}</div></div>", p.Title, string.Format("{0:F2}", dtotal), string.Format("{0:F2}", total - dtotal));
            //    }
            //}
            //else
            //{


            //    retval = string.Format("<div class='form-group'><div class='col-md-6 price_lable'> {0}</div><div class='col-md-6 price_lable' style='color:#7c38bc;'> {1}</div></div>", "Member", "Savings");
            //    foreach (var p in pd.Where(o => o.ID == AssignedPolicyID).ToList())
            //    {
            //        var d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;
            //        var dtotal = d > 0 ? total - (total * (d / 100)) : total;
            //        retval = retval + string.Format("<div class='form-group'> <div class='col-md-6 price_text' style='text-align:right;'> {0}</div><div class='col-md-6 price_text' style='color:#7c38bc;text-align:right;'> {1}</div></div>", string.Format("{0:F2}", dtotal), string.Format("{0:F2}", total - dtotal));
            //    }

            //}

            var costPrice = 0.00;
            var salePrice = 0.00;
            var percent = 0.00;

            if (qItems.Count > 0)
                costPrice = qItems.Sum(o =>  o.SellingPrice * (o.QTY.HasValue?o.QTY.Value:0));
            if (pTotal != null)
                salePrice = qItems.Sum(o => o.SalesPrice.HasValue?o.SalesPrice.Value:0 );// pTotal.RevicedPrice.HasValue?pTotal.RevicedPrice.Value:0;
            if (costPrice > 0 && salePrice > 0)
                percent = ((salePrice - costPrice)/ salePrice)*100;
           // retval = retval + string.Format("<div class='form-group'><div class='col-md-7 price_text' style='text-align:right;'> {0}</div> <div class='col-md-5 price_text' style='text-align:right;'> {1}</div></div>", "Cost Price", string.Format("{0:N2}", costPrice));
            retval = retval + string.Format("<div class='form-group row mb-10'><div class='col-md-6' style='text-align:right;font-weight: bold;font-size: 20px;'> {0}</div> <div class='col-md-3 price_text' style='text-align:right;font-weight: bold;font-size: 20px;'> {1}</div></div>", "Sales Price:", string.Format("{0:N2}", salePrice));
           // retval = retval + string.Format("<div class='form-group'><div class='col-md-7 price_text' style='text-align:right;'> {0}</div> <div class='col-md-5 price_text' style='text-align:right;'> {1}</div></div>", "Profit Margin %", string.Format("{0:F2}", percent));


            return retval;
        }
        private void BindUrlToTabs()
        {
            if (Request.QueryString["tab"] == null)
            {
                lbtnQuote.NavigateUrl = Request.RawUrl + "&tab=quote";
                lbtnAttach.NavigateUrl = Request.RawUrl + "&tab=attach";
                lbtnFinance.NavigateUrl = Request.RawUrl + "&tab=finance";
            }
            else
            {
                lbtnQuote.NavigateUrl = string.Format("/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.OPTION, "quote");
                lbtnAttach.NavigateUrl = string.Format("/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.OPTION, "attach");
                lbtnFinance.NavigateUrl = string.Format("/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.OPTION, "finance");
            }


        }
        private string GetItems(List<QuotationItem> qItems)
        {
            string retval = string.Empty;

            foreach(var p in qItems)
            {
                retval = retval + string.Format("<li class='col-md-12 price_item'> {0}</li>", p.ServiceDescription);
            }

            return retval;
        }
            private void BindCustomFields()
        {
            try
            {
                var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o => o.OptionName).ToList();
                var qList = QuotationBAL.QuotationItem_SelectByCallid(QueryStringValues.CallID).ToList();
                var pList = QuotationBAL.QuotationPrice_selectAll(QueryStringValues.CallID);
                var policyList = PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select(sessionKeys.PortfolioID);
                //var policyDetails = policyList.FirstOrDefault();
                //var discountPercent = policyDetails.DiscountPercent.HasValue?policyDetails.DiscountPercent.Value:0.00;

                var paBal = new PortfolioMgt.BAL.PortfolioContactBAL();
                var CallDetails = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                var addressID = CallDetails.ContactAddressID;
                var addressDetails = paBal.v_PortfolioContactAddress_SelectByID(addressID).FirstOrDefault();
                var selectedpolicyID = addressDetails.PolicyTypeID.HasValue ? addressDetails.PolicyTypeID.Value : 0;
                if (oList.Count > 0)
                {
                    var dList = (from o in oList
                                 select new
                                 {
                                     o.CallID,
                                     o.ID,
                                     o.IsActive,
                                     o.LoggedDate,
                                     o.ModifiedDate,
                                     o.OptionName,
                                     ItemsCount = qList.Where(p => p.QuotationOptionID == o.ID).Count(),
                                     Price = (pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault().OriginalPrice : 0.00),
                                     IsAplied = pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault().IsOptionActive : false

                                 }).ToList();
                    var rLIst = (from o in dList
                                 orderby o.ID ascending
                                 select new
                                 {
                                     o.CallID,
                                     o.ID,
                                     o.IsActive,
                                     o.LoggedDate,
                                     o.ModifiedDate,
                                     o.OptionName,
                                     o.Price,
                                     o.IsAplied,
                                     o.ItemsCount,
                                     ItemsCountName = o.ItemsCount.ToString() + " Items",
                                     //MemberCost = string.Format("{0:F2}", (discountPercent > 0) ? o.Price - (o.Price * (discountPercent / 100)) : o.Price),
                                     //Savings = string.Format("{0:F2}", (discountPercent > 0) ?  (o.Price * (discountPercent / 100)) : 0.00),
                                     mdata = GetPolicyData(selectedpolicyID, policyList, o.Price, qList.Where(p => p.QuotationOptionID == o.ID).ToList(), pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault()),
                                     ItemsList = GetItems(qList.Where(p => p.QuotationOptionID == o.ID).ToList()),
                                     ImageUrl = GetImageUrl()
                                 }).ToList();
                    list_Customfields.DataSource = rLIst;
                    list_Customfields.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public int imgIndex = -1;

        public string GetImageUrl()
        {
            string[] strImage = { "~/Content/images/quote/quote1.png", "~/Content/images/quote/quote2.png", "~/Content/images/quote/quote3.png" };

            imgIndex++;

            if (imgIndex >= 2)
                imgIndex = -1;
          

            return strImage[imgIndex+1];
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
                if (e.CommandName == "Item")
                {
                    var optionid = Convert.ToInt32(e.CommandArgument.ToString());
                    Response.Redirect(string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
                }
                else if(e.CommandName == "Del")
                {
                    
                    QuotationOptionsBAL.QuotationOption_DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));

                    sessionKeys.Message = "Estimate deleted successfully";

                    Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID));

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void list_Customfields_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                    var d = e.Item.DataItem as dynamic;
                    if (lbl != null)
                    {

                        var r = d.IsAplied;
                        if (!r)
                        {
                            lbl.Visible = false;
                        }
                        // BindRateType_SetVal((e.Item.DataItem as v_TimesheetEntryCustom).TimesheetEntryTypeID.Value.ToString(), ddl);
                    }
                    HyperLink hlink = (HyperLink)e.Item.FindControl("hlinkItems");
                    if (hlink != null)
                    {
                        var optionID = d.ID;
                        hlink.NavigateUrl = string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionID, "quote");

                    }
                    HyperLink hLinkPlan = (HyperLink)e.Item.FindControl("hLinkPlan");
                    if (hLinkPlan != null)
                    {
                        //WF/CustomerAdmin/ContactAddressDetails.aspx?ContactID=10227&addid=3866
                        //var optionID = d.ID;
                        var j =  FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID);
                        hLinkPlan.NavigateUrl = string.Format("~/WF/CustomerAdmin/ContactAddressDetails.aspx?CCID={0}&callid={1}&SDID={1}&ContactID={3}&addid={2}", QueryStringValues.CCID, QueryStringValues.CallID, j.FirstOrDefault().ContactAddressID,j.FirstOrDefault().RequesterID);

                    }

                    HyperLink hLinkFinnace = (HyperLink)e.Item.FindControl("hLinkFinnace");
                    if (hLinkFinnace != null)
                    {
                        var optionID = d.ID;
                        hLinkFinnace.NavigateUrl = "#"; //string.Format("~/WF/DC/DCFinancing.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionID, "quote");

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

        #region Option changes
        public void BindOptions()
        {
            //ddlOptions.Items.Clear();
            var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID);
            if (oList.Count == 0)
            {
                //set the  
                UpdateDetailInOption();
                mdlManageOptions.Show();
            }
            //if (oList.Count > 0)
            //{
            //    ddlOptions.DataSource = oList.OrderBy(o => o.OptionName).ToList();
            //    ddlOptions.DataTextField = "OptionName";
            //    ddlOptions.DataValueField = "ID";
            //    ddlOptions.DataBind();
            //    ddlOptions.Items.Insert(0, new ListItem("Please select...", "0"));
            //    AddOptionButton_Visibility();
            //}
            //else
            //{
            //ddlOptions.Items.Insert(0, new ListItem("Please select...", "0"));
            //AddOptionButton_Visibility(true);

                // }

        }
        private void AddOptionButton_Visibility(bool v = false)
        {
            //btnSubmitOptions.Visible = v;
            //btnCancelOptions.Visible = v;
            //txtOptions.Visible = v;
            //ddlOptions.Visible = !v;
            //btnAddOption.Visible = !v;
            //btnEditOption.Visible = !v;
            //btnDeleteOptions.Visible = !v;

        }
        protected void btnAddOption_Click(object sender, EventArgs e)
        {
            try
            {
                ddlOptions.SelectedValue = "0";
                hOptionID.Value = ddlOptions.SelectedValue;
                txtOptions.Text = string.Empty;

                txtOptionDescription.Text = string.Empty;
                AddOptionButton_Visibility(true);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void btnEditOption_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlOptions.SelectedValue != "0")
                {
                    txtOptions.Text = ddlOptions.SelectedItem.Text;
                    hOptionID.Value = ddlOptions.SelectedValue;
                    var d = QuotationOptionsBAL.QuotationOption_SelectByID(Convert.ToInt32(hOptionID.Value));
                    txtOptionDescription.Text = d.Description;
                    AddOptionButton_Visibility(true);
                }
                else
                {
                    txtOptions.Text = string.Empty;
                    hOptionID.Value = "0";
                    txtOptionDescription.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnDeleteOptions_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlOptions.SelectedValue != "0")
                {
                    var v = QuotationOptionsBAL.QuotationOption_DeleteByID(Convert.ToInt32(ddlOptions.SelectedValue));
                    if (v != null)
                    {
                        lblMsgOptions.Text = Resources.DeffinityRes.Deletedsuccessfully;

                        txtOptions.Text = string.Empty;
                        txtOptionDescription.Text = string.Empty;
                        //BindOptions();
                        AddOptionButton_Visibility();
                        UpdateDetailInOption();
                        //mdlManageOptions.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            pnlOptionalDetails.Visible = false;
            pnlAddItem.Visible = true;
           

        }
        public List<FixedRateType> GetTypeData()
        {
            var ftypeRepository = new DCRepository<FixedRateType>();
            return ftypeRepository.GetAll().OrderBy(o => o.FixedRateTypeName).ToList();

        }
        public void bindServiceType()
        {

            ddlstype.DataSource = GetTypeData();
            ddlstype.DataTextField = "FixedRateTypeName";
            ddlstype.DataValueField = "FixedRateTypeID";
            ddlstype.DataBind();
            ddlstype.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        private void AddNewItems(int optionID)
        {
            var id = !string.IsNullOrEmpty(hItemID.Value) ? hItemID.Value : "0";// !string.IsNullOrEmpty(ddlItems.SelectedValue) ? ddlItems.SelectedValue : "0";  //hfCustomerId.Value;
            if (Convert.ToInt32(!string.IsNullOrEmpty(hItemID.Value) ? hItemID.Value : "0") > 0)
            {
                var ID = Convert.ToInt32(!string.IsNullOrEmpty(hItemID.Value) ? hItemID.Value : "0");
                var SellingPrice = Convert.ToDouble(txtCost.Text.Trim());
                var QTY = Convert.ToDouble(txtQty.Text.Trim());
                var VAT = 0.00;
                var SalesPrice = 0.00;
                var markup = 0.00;
                var vtrate = VATByCustomerBAL.VATByCustomer_select();
                using (DCDataContext dc = new DCDataContext())
                {
                    var applyVAT = true;
                    var qitem = dc.QuotationItems.Where(o => o.ID == ID).FirstOrDefault();
                    if (qitem != null)
                    {
                        SalesPrice = qitem.SalesPrice.HasValue ? qitem.SalesPrice.Value : 0.00;
                        markup = qitem.Markup.HasValue ? qitem.Markup.Value : 0.00;
                        hImageID.Value = qitem.Image.HasValue ? qitem.Image.Value.ToString() : "00000000-0000-0000-0000-000000000000";
                        if ((qitem.QTY != QTY) || (qitem.SellingPrice != SellingPrice))
                        {

                            var v = (QTY * SellingPrice);
                            if (vtrate > 0)
                                VAT = (QTY * SellingPrice) * (vtrate / 100);
                            else
                                VAT = 0.00;
                        }
                    }
                }
                string ItemImg = hImageID.Value != "00000000-0000-0000-0000-000000000000" ? hImageID.Value : "00000000-0000-0000-0000-000000000000";
                Guid _guid = new Guid(ItemImg);
                if ((FileUploadMaterial.HasFile))
                {
                    _guid = Guid.NewGuid();
                }
                //if (hImageID.Value != "00000000-0000-0000-0000-000000000000")
                //    _guid = new Guid(hImageID.Value);
                // var vat_val = Convert.ToDouble(!string.IsNullOrEmpty(txtAddItemVat.Text.Trim()) ? txtAddItemVat.Text.Trim() : "0.00");
                //update
                ServiceManager.Quotation_Update(ID, QTY, SellingPrice, 0, txtItemNotes.Text.Trim(), txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), VAT, _guid, SalesPrice, markup, vtrate);

                if (FileUploadMaterial.HasFile)
                {
                    ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                }

                lblMsg.Text = "Updated successfully";
                Service_Prices(optionID);
              //  BindCustomFields();
               // mdlAddItem.Hide();
            }
            else
            if (id == "0" && txtSearch.Text.Trim().Length > 0 && txtSearch.Visible == true)
            {
                var serviceID = Convert.ToInt32(!string.IsNullOrEmpty(hCatelogID.Value) ? hCatelogID.Value : "0");
                string ItemImg = "00000000-0000-0000-0000-000000000000";
                Guid _guid = new Guid(ItemImg);
                if ((FileUploadMaterial.HasFile))
                {
                    _guid = Guid.NewGuid();
                }
                if (hImageID.Value != string.Empty)
                {
                    if (hImageID.Value != "00000000-0000-0000-0000-000000000000")
                        _guid = new Guid(hImageID.Value);
                }
                var qty = Convert.ToDouble(string.IsNullOrEmpty(txtQty.Text.Trim()) ? "0" : txtQty.Text.Trim());
                var cost = Convert.ToDouble(!string.IsNullOrEmpty(txtCost.Text.Trim()) ? txtCost.Text.Trim() : "0.00");
                var markup = 0;// PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_SelectMargin();
                var vtRate = VATByCustomerBAL.VATByCustomer_select();

                var sales = 0.00;
                if (markup > 0)
                    sales = ((cost * qty) + ((cost * qty) * (markup / 100)));
                else
                    sales = (cost * qty);

                var vat = 0.00;
                if (vtRate > 0)
                    vat = (qty * sales) * (vtRate / 100);
                else
                    vat = 0.00;

                int retval = Insertservice(serviceID, QueryStringValues.CallID, qty, 2,"FLS", txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), cost, vat, txtItemNotes.Text.Trim(), _guid.ToString(), sales, markup, vtRate, optionID);
                if (retval == 1)
                {

                    if (FileUploadMaterial.HasFile)
                    {
                        ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                    }
                    Service_Prices(optionID);
                   // Response.Redirect(Request.RawUrl);
                    lblMsg.Text = "Added successfully";
                    txtQty.Text = "1";
                    ddlstype.SelectedIndex = 0;
                    txtSearch.Text = string.Empty;
                    txtCost.Text = "0.00";
                    txtAddItemVat.Text = "0.00";
                    txtItemNotes.Text = string.Empty;
                    //mdlAddItem.Hide();
                    //BindCustomFields();
                    //bindGrid();
                    //fill and get the values for total
                    Service_Prices(optionID);
                    hCatelogID.Value = "0";
                    hImageID.Value = "00000000-0000-0000-0000-000000000000";
                   
                }
                //else
                //{
                //    lblError.Text = "Item already exists";
                //}


            }
            else
            {
                int serviceID = Convert.ToInt32(id);//int.Parse(ddlService.SelectedValue);

                if (serviceID > 0)
                {
                    // string ItemImg = "00000000-0000-0000-0000-000000000000";
                    Guid _guid = new Guid(hImageID.Value);
                    if ((FileUploadMaterial.HasFile))
                    {
                        _guid = Guid.NewGuid();
                    }
                    var qty = Convert.ToDouble(string.IsNullOrEmpty(txtQty.Text.Trim()) ? "0" : txtQty.Text.Trim());
                    var cost = Convert.ToDouble(!string.IsNullOrEmpty(txtCost.Text.Trim()) ? txtCost.Text.Trim() : "0.00");
                    var margin = 0;// PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_SelectMargin();
                    var sales = 0.00;
                    if (margin > 0)
                        sales = ((cost * qty) + ((cost * qty) * (margin / 100)));
                    else
                        sales = (cost * qty);
                    int retval = 0;
                    // int retval = Insertservice(serviceID, QueryStringValues.CallID, qty, 2, Type, txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), 0.00, Convert.ToDouble(!string.IsNullOrEmpty(txtVAT.Text.Trim()) ? txtVAT.Text.Trim() : "0.00"),txtItemNotes.Text.Trim(), _guid.ToString());
                    if (retval == 1)
                    {

                        if (FileUploadMaterial.HasFile)
                        {
                            ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                        }
                        ddlItems.SelectedIndex = 0;
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Added successfully";
                        txtQty.Text = string.Empty;
                        //bindGrid();
                        //fill and get the values for total
                       // mdlAddItem.Hide();
                        Service_Prices(optionID);
                       // BindCustomFields();
                        hCatelogID.Value = "0";
                        hImageID.Value = "00000000-0000-0000-0000-000000000000";
                       // Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                       // lblError.Text = "Item already exists";
                    }


                }
            }
        }
        private int Insertservice(int ServiceID, int IncidentID, double QTY, int ServiceTypeID, string type, string servicetext, int servicetypeid, double cost, double VAT, string notes, string imgGuid, double SalesPrice, double markup, double vatrate, int optionID, bool applyVAT = true, int PolicyID = 0, string PolicyNotes = "")
        {
            //var vt = 0.00;
            //if (applyVAT)
            //{
            //    vt = VATByCustomerBAL.VATByCustomer_select();
            //}
            //var v = (QTY * cost);
            //if (vt > 0)
            //    v = (QTY * cost) * (vt / 100);
            //else
            //    v = 0.00;
            //SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 8);
            //OutVal.Direction = ParameterDirection.Output;
            var nGuid = new Guid(imgGuid);
            //SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Quotation_Item_Insert",
            //    new SqlParameter("@ServiceID", ServiceID),
            //    new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@QTY", QTY), new SqlParameter("@Type", type),
            //    new SqlParameter("@ServiceTypeID", ServiceTypeID),
            //    new SqlParameter("@ServiceDescription", servicetext),
            //new SqlParameter("@FixedRateTypeID", servicetypeid),
            //new SqlParameter("@cost", cost),
            //new SqlParameter("@VAT", v),
            //new SqlParameter("@Option",QueryStringValues.OPTION)
            //, new SqlParameter("@Notes", notes)
            //, new SqlParameter("@Image", nGuid)
            //, new SqlParameter("@PolicyID", PolicyID)
            //, new SqlParameter("@PolicyNotes", PolicyNotes)
            //, new SqlParameter("@SalesPrice", )
            //    , OutVal);
            if (FileUploadMaterial.HasFile)
            {
                ImageManager.SaveImage(nGuid, FileUploadMaterial.FileBytes);
            }

            return QuotationBAL.InsertQuoteItem(ServiceID, IncidentID, QTY, ServiceTypeID, type, servicetext, servicetypeid, cost, VAT, notes, imgGuid, SalesPrice, markup, vatrate, applyVAT = true, PolicyID = 0, PolicyNotes = "",optionID);


        }
        private void Service_Prices(int optionid)
        {
            try
            {
                SqlDataReader dr = IncidentService_Price.Quotation_Price_Select(QueryStringValues.CallID, "FLS", optionid);
                while (dr.Read())
                {
                    //lblTotalPrice.InnerText = string.Format("{0:f2}", dr["OriginalPrice"]);
                    ////lblTotalPrice.InnerText = string.Format("{0:f2}", dr["RevicedPrice"]);
                    //lblDiscountValue.InnerText = string.Format("{0:f2}", dr["DiscountPrice"]);
                    //txtDiscountPercent.Text = dr["DiscountPercent"].ToString();
                    //lblRevisedPrice.InnerText = string.Format("{0:f2}", dr["RevicedPrice"]);
                    //lbluc.InnerText = string.Format("{0:f2}", dr["UnitConsumption"]);
                    ////txtNotes.Text = dr["Notes"].ToString();
                    //txtNotes.Text = dr["Notes"].ToString();
                    //
                    //if (GetFixedPriceApprovalData() == null)
                    //{
                    //    if (Convert.ToDouble(dr["OriginalPrice"]) > GetThresholdPrice())
                    //    {
                    //        pnlOverprice.Visible = true;
                    //        Session["invoicedisable"] = true;
                    //    }
                    //    else
                    //    {
                    //        Session["invoicedisable"] = false;
                    //    }
                    //}
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnSubmitOptions_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOptions.Text.Trim().Length >0)
                {
                    var q = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = QueryStringValues.CallID, OptionName = txtOptions.Text.Trim(), CustomerID = sessionKeys.PortfolioID, IsActive = false, Description = txtOptionDescription.Text.Trim() });
                    if (q != null)
                    {
                        if (q.ID == 0)
                        {
                            lblErrorOptions.Text = "Quote name already exists";
                        }
                        else
                        {
                            sessionKeys.Message = Resources.DeffinityRes.Addedsuccessfully;
                            AddNewItems(q.ID);
                            //  Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.CallID), false);

                            Response.Redirect(string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID, q.ID), false);

                            //AddOptionButton_Visibility();
                            //txtOptions.Text = string.Empty;
                            //BindOptions();
                            //mdlManageOptions.Show();
                        }
                    }
                }
                //else
                //{
                //    var item = QuotationOptionsBAL.QuotationOption_SelectByID(Convert.ToInt32(ddlOptions.SelectedValue));
                //    item.OptionName = txtOptions.Text.Trim();
                //    item.Description = txtOptionDescription.Text.Trim();
                //    var q = QuotationOptionsBAL.QuotationOption_Update(item);
                //    if (q != null)
                //    {
                //        if (q.ID == 0)
                //        {
                //            lblErrorOptions.Text = "Option name already exists";
                //        }
                //        else
                //        {
                //            sessionKeys.Message = Resources.DeffinityRes.UpdatedSuccessfully;
                //            Response.Redirect(Request.RawUrl);
                //            //Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.CallID), false);

                //            //AddOptionButton_Visibility();
                //            //txtOptions.Text = string.Empty;
                //            //BindOptions();
                //            //mdlManageOptions.Show();
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnCancelOptions_Click(object sender, EventArgs e)
        {
            txtOptions.Text = string.Empty;
            AddOptionButton_Visibility();
        }
        protected void lbtnCloseOptions_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
            //int OptionID = 0;
            //var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).ToList();
            //if (oList.Count > 0)
            //    OptionID = oList.FirstOrDefault().ID;
            //Response.Redirect(string.Format("/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}",QueryStringValues.CCID,QueryStringValues.CallID,OptionID ),false);
        }
        protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlOptions.SelectedValue != "0")
                {
                    hOptionID.Value = ddlOptions.SelectedValue;
                    txtOptions.Text = ddlOptions.SelectedItem.Text;
                    var d = QuotationOptionsBAL.QuotationOption_SelectByID(Convert.ToInt32(hOptionID.Value));
                    txtOptionDescription.Text = d.Description;
                }
                else
                {
                    txtOptionDescription.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #endregion

        protected void btnViewCompare_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.OPTION, "quote"));

        }
        public void btnSubmitToCustomer_Click1(object sender, EventArgs e)
        {
            try
            {
                List<int> SelectedID =new List<int>();
                foreach (ListViewDataItem item in list_Customfields.Items)
                {
                    CheckBox chk = item.FindControl("chkRecommand") as CheckBox;
                    var lblID = item.FindControl("lblID") as Label;
                    if (chk != null && lblID != null)
                    {
                        if (chk.Checked)
                        {
                            SelectedID.Add( Convert.ToInt32(lblID.Text));
                        }
                    }
                }

                //var qList = QuotationBAL.QuotationPrice_selectAll(QueryStringValues.CallID);


                ////if (txtDiscountPercent.Text != string.Empty)
                ////{
                //foreach (var v in qList)
                //{
                //    IncidentService_Price.QuotationPrice_Update(sessionKeys.IncidentID, double.Parse(txtDiscountPercent.Text.Trim()), txtNotes.Text.Trim(), Type, v.QuotationOptionID.HasValue ? v.QuotationOptionID.Value : 0);
                //}
                //if (Type != "FLS")
                //{
                //    using (IncidentDataContext su = new IncidentDataContext())
                //    {
                //        int id = QueryStringValues.SDID;
                //        IncidentMgt.Entity.Incident inc = su.Incidents.Where(p => p.ID == id).FirstOrDefault();
                //        inc.Status = "Pending Approval";
                //        su.SubmitChanges();
                //    }
                //}
                //else
                //{
                //    CallDetail cd = CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
                //    //40	Quotation Submitted
                //    cd.StatusID = 40;
                //    CallDetailsBAL.CallDetailsUpdate(cd);
                //    DateTime modified_date = DateTime.Now;
                //    AddCallDetailsJournal(cd, modified_date);
                //    AddServicePriceJouranl(txtNotes.Text.Trim(), Convert.ToDouble(string.IsNullOrEmpty(lblTotalPrice.InnerText.Trim()) ? "0" : lblTotalPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblRevisedPrice.InnerText.Trim()) ? "0" : lblRevisedPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lbluc.InnerText.Trim()) ? "0" : lbluc.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPercent.Text.Trim()) ? "0" : txtDiscountPercent.Text.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblDiscountValue.InnerText.Trim()) ? "0" : lblDiscountValue.InnerText.Trim()), modified_date);
                //}

                //Incident incident = new Incident();
                //incident.Status = "Pending Approval";
                //IncidentHelper.Update(incident);
                //BuildMail();
                FLS_OptionalMailtoRequester(SelectedID);
                //pnlservice.Visible = false;
                //sessionKeys.Message = "Estimate has been sent to the client";
                //lblservice.Font.Bold = true;

                //Response.Redirect(Request.RawUrl,false);

                // }
                BindContacts();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

       

        public void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                List<ToEmailCalss> tlist = new List<ToEmailCalss>();

                if (gridContacts.Rows.Count > 1)
                {
                    mdlContacts.Show();
                    //for (int i = 0; i < gridContacts.Rows.Count; i++)
                    //{
                    //    GridViewRow grow = gridContacts.Rows[i];

                    //    Label lblContact = (Label)grow.FindControl("lblContact");
                    //    Label lblContactEmail = (Label)grow.FindControl("lblContactEmail");

                    //    CheckBox GridCheckBox = (CheckBox)grow.FindControl("chkContact");
                    //    if (GridCheckBox.Checked)
                    //    {
                    //        tlist.Add(new ToEmailCalss() { name = lblContact.Text, email = lblContactEmail.Text });
                    //    }
                    //}
                    //SendQuoteMail(tlist);
                }
                else
                {
                   

                    SendQuoteMail(tlist);
                }

               

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void btnSendMailContacts_Click(object sender, EventArgs e)
        {
            try
            {
                List<ToEmailCalss> tlist = new List<ToEmailCalss>();

                if (gridContacts.Rows.Count > 1)
                {
                    for (int i = 0; i < gridContacts.Rows.Count; i++)
                    {
                        GridViewRow grow = gridContacts.Rows[i];

                        Label lblContact = (Label)grow.FindControl("lblContact");
                        Label lblContactEmail = (Label)grow.FindControl("lblContactEmail");

                        CheckBox GridCheckBox = (CheckBox)grow.FindControl("chkContact");
                        if (GridCheckBox.Checked)
                        {
                            tlist.Add(new ToEmailCalss() { name = lblContact.Text, email = lblContactEmail.Text });
                        }
                    }
                    
                    SendQuoteMail(tlist);
                }
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void SendQuoteMail(List<ToEmailCalss>  tlist)
        {
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                using (DCDataContext dc = new DCDataContext())
                {

                    var fls = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                    var cdetails = dc.CallDetails.Where(c => c.ID == QueryStringValues.CallID).FirstOrDefault();
                    var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                    string body = CKEditor1.Text;

                    if(!body.Contains("!DOCTYPE HTML PUBLIC"))
                    {
                        Emailer em = new Emailer();
                        string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                        html_body = html_body.Replace("[table]", body);
                        body = html_body;
                    }
                    string fromid = Deffinity.systemdefaults.GetFromEmail();
                    string toid = "";
                    string subject = "Here's Your Quotation";
                    var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                    string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                    if (File.Exists(pname))
                    {
                        var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                        Email ToEmail = new Email();
                        Attachment attachment1 = new Attachment(pname);
                        attachment1.Name = q.CurrentTemplateName + ".pdf";

                        if (tlist.Count > 0)
                        {
                            foreach(var t in tlist)
                            {
                                ToEmail.SendingMail(t.email, subject, body, fromid, attachment1);
                            }

                        }
                            else

                        ToEmail.SendingMail(pcontact.Email, subject, body, fromid, attachment1);


                    }
                    else
                    {
                        Emailer em = new Emailer();
                        if (tlist.Count > 0)
                        {
                            foreach (var t in tlist)
                            {
                                em.SendingMail(fromid, subject, body, t.email);
                            }

                        }
                        else
                            em.SendingMail(fromid, subject, body, pcontact.Email);
                    }
                    //update quote status
                    FLSDetailsBAL.UpdateTicketStatus(QueryStringValues.CallID, sessionKeys.UID, JobStatus.Quote_Sent);
                    sessionKeys.Message = "Quotation has been sent to the client";
                    Response.Redirect(Request.RawUrl, false);
                }
            }
        }

        private void FLS_OptionalMailtoRequester(List<int> optionID)
        {
            try
            {
                int callid = QueryStringValues.CallID;
                string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                WebService ws = new WebService();
                EmailFooter ef = new EmailFooter();
                AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select();

                ef = FooterEmail.EmailFooter_selectByID(6, sessionKeys.PortfolioID);
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var fls = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                        //var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();
                        var fdetails = dc.FLSDetails.Where(c => c.CallID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        //var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        List<QuotationItem> noteslist = new List<QuotationItem>();
                        if(optionID.Count >0)
                            noteslist = dc.QuotationItems.Where(c => c.CallidID == callid && optionID.Contains(c.QuotationOptionID.Value)).ToList();
                        else
                         noteslist = dc.QuotationItems.Where(c => c.CallidID == callid).ToList();

                        List<QuotationPrice> qtPrice = new List<QuotationPrice>();
                        if (optionID.Count > 0)
                            qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid && optionID.Contains(c.QuotationOptionID.Value)).ToList();
                        else
                            qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid).ToList();
                        var stypelist = dc.QuotationOptions.Where(c => c.CallID == callid).ToList();
                        var policylist = pd.ProductPolicyTypes.Where(o => o.CustomerID == cdetails.CompanyID).ToList();
                        var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Quotation";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSQuotation.htm");

                        body = body.Replace("[mail_head]", sessionKeys.JobDisplayName+" Quotation");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                        body = body.Replace("[InstnaceTitle]", Deffinity.systemdefaults.GetInstanceTitle());
                        body = body.Replace("[noteslist]", GetQuoteItemList(noteslist, stypelist, qtPrice, policylist, addressDetails, Deffinity.systemdefaults.GetWebUrl()));

                        // body = body.Replace("[notes]", GetQuoteItemList(noteslist, stypelist, qtPrice));

                        // body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        //var sp = noteslist.Select(o => o.SellingPrice).Sum();
                        //var vat = 0.00;
                        //var s1 = Convert.ToDouble((vat * sp) / Convert.ToDouble(100));
                        body = body.Replace("[refno]", fls.CCID.ToString());
                        body = body.Replace("[details]", fls.Details.ToString());

                        //[date]
                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester
                        body = body.Replace("[user]", pcontact.Name);

                        CKEditor1.Text = body;
                        mdlShowMail.Show();

                        //var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                        //string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                        //if (File.Exists(pname))
                        //{
                        //    var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                        //    Email ToEmail = new Email();
                        //    Attachment attachment1 = new Attachment(pname);
                        //    attachment1.Name = q.CurrentTemplateName + ".pdf";

                        //    ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                        //}
                        //else
                        //{
                        //    em.SendingMail(fromemailid, subject, body, pcontact.Email);
                        //}
                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetQuoteItemList(List<QuotationItem> rlist, List<QuotationOption> qlist, List<QuotationPrice> qtPrice, List<PortfolioMgt.Entity.ProductPolicyType> policylist, PortfolioMgt.Entity.PortfolioContactAddress addressDetails, string weburl)
        {
            StringBuilder sbuild = new StringBuilder();

            foreach (var q in qlist)
            {
                var noteslist = rlist.Where(o => o.QuotationOptionID == q.ID).ToList();
                var qPrice = qtPrice.Where(o => o.QuotationOptionID == q.ID).FirstOrDefault();
                if (noteslist.Count > 0)
                {
                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append(string.Format("<tr><td><b> Option: {0}</b></td></tr>", q.OptionName));
                    sbuild.Append("</table>");
                    // UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();

                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                    sbuild.Append("<td style='width:40%'>Item</td><td>Unit Price</td><td>QTY</td><td> TAX</td><td>Total</td>");
                    sbuild.Append("</tr>");
                    foreach (var n in noteslist)
                    {
                        sbuild.Append("<tr>");
                        sbuild.Append(string.Format("<td>{0}</td><td style='text-align:right'>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td><td style='text-align:right'>{4}</td>", n.ServiceDescription, string.Format("{0:F2}", n.SalesPrice / n.QTY), n.QTY, string.Format("{0:F2}", n.VAT), string.Format("{0:F2}", n.SalesPrice + n.VAT)));

                        sbuild.Append("</tr>");
                    }
                    sbuild.Append("</table>");

                    sbuild.Append("<table style='width:40%'>");
                    var price = string.Format("{0:F2}", qPrice.OriginalPrice);
                    sbuild.Append(string.Format("<tr><td><b> Total price: </b></td><td>{0}</td></tr>", price));
                    if ((qPrice.FinalPrice.HasValue ? qPrice.FinalPrice.Value : 0) > 0)
                    {
                        var discountpriceIncludetax = string.Format("{0:F2}", (qPrice.FinalPriceIncludeTax.HasValue ? qPrice.FinalPriceIncludeTax.Value : 0));
                        var taxtprice = string.Format("{0:F2}", (qPrice.FinalPriceIncludeTax.HasValue ? qPrice.FinalPriceIncludeTax.Value : 0) - (qPrice.FinalPrice.HasValue ? qPrice.FinalPrice.Value : 0));
                        sbuild.Append(string.Format("<tr><td><b> Your price: </b></td><td>{0} Price includes Tax ({1})</td></tr>", discountpriceIncludetax, taxtprice));
                    }


                    sbuild.Append("</table>");
                    if (!string.IsNullOrEmpty(qPrice.Notes))
                    {
                        sbuild.Append("<table style='width:100%'>");
                        var notes = qPrice.Notes;
                        sbuild.Append(string.Format("<tr><td><b> Notes: </b><br>{0}</td></tr>", notes));
                        sbuild.Append("</table>");
                    }

                    sbuild.Append("<table style='width:70%'>");
                    var b = weburl + string.Format("/WF/DC/DCQuoteMail.aspx?ccid={3}&callid={0}&statusid={1}&type={2}&Option={4}&cid={5}", QueryStringValues.CallID, 1, "mail", QueryStringValues.CCID, q.ID, sessionKeys.PortfolioID);
                    sbuild.Append(string.Format("<tr><td><b> {1}: </b><br></td><td>{0}</td></tr>", getButton(b, "Click here to accept this quote"), q.OptionName));
                    sbuild.Append("</table>");
                    if (addressDetails != null)
                    {
                        if ((addressDetails.PolicyTypeID.HasValue ? addressDetails.PolicyTypeID.Value : 0) > 0)
                        {
                            sbuild.Append("<table style='width:100%'>");

                            sbuild.Append(string.Format("<tr><td><br></td></tr>"));
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:100%'>");

                            //sbuild.Append(string.Format("<tr><td style='font-size:15px'> Special offer: <br /></td></tr>"));

                            sbuild.Append(string.Format("<tr><td style='font-size:15px'> <img src='" + Deffinity.systemdefaults.GetWebUrl() + "/Content/images/SpecialOffer.png' style='border:0px' /> <br /></td></tr>"));

                            sbuild.Append(string.Format("<tr><td> If you were to take out the following maintenance plan, this price would be as follows:</td></tr>"));
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:85%'>");
                            //Header row
                            sbuild.Append(string.Format("<tr class='tab_header'><td><b> Plan </b></td><td><b> Discount Amount </b></td><td><b>New Total Price</b></td><td><b></b></td></tr>"));
                            foreach (var v in policylist)
                            {
                                var cUrl = weburl + string.Format("/WF/DC/DCQuoteContactMail.aspx?ccid={3}&callid={0}&statusid={1}&type={2}&Option={4}&cid={5}&planid={6}", QueryStringValues.CallID, 1, "mail", QueryStringValues.CCID, q.ID, sessionKeys.PortfolioID, v.ID.ToString());

                                sbuild.Append(string.Format("<tr><td><b> {0} </b></td><td style='text-align:right'> {1} </td><td style='text-align:right'>{2}</td><td>{3} </td></tr>", v.Title, string.Format("{0:F2}", GetDiscountAmount(qPrice.OriginalPrice, v.DiscountPercent.HasValue ? v.DiscountPercent.Value : 0)), string.Format("{0:F2}", GetDiscountAmountTotal(qPrice.OriginalPrice, v.DiscountPercent.HasValue ? v.DiscountPercent.Value : 0)), getGreenButton(cUrl, "Please contact me")));
                            }
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:100%'>");

                            sbuild.Append(string.Format("<tr><td><br><br></td></tr>"));
                            sbuild.Append("</table>");
                        }
                    }
                }
            }




            return sbuild.ToString();
        }
        private string getGreenButton(string url, string name)
        {
            //var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style='border-radius:2px;' bgcolor='#63b026'><a href = '{0}' target = '_blank' style='padding: 8px 12px; border: 1px solid #63b026;border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px; color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);

            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' ><a href = '{0}' target = '_blank' >{1}</a></td ></tr></table></td></tr ></table>", url, "<img src='"+ Deffinity.systemdefaults.GetWebUrl() + "/Content/images/ContactMe.png' style='border:0px' />");
            return v;
        }
        private double GetDiscountAmount(double amount, double discount)
        {
            double retval = 0;
            if (discount > 0)
            {
                retval = (amount * (discount / 100));
            }
            else retval = 0;

            return retval;
        }
        private double GetDiscountAmountTotal(double amount, double discount)
        {
            double retval = 0;
            if (discount > 0)
            {
                retval = amount - (amount * (discount / 100));
            }
            else retval = amount;

            return retval;
        }
        private string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#FF6600'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #FF6600; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
        }


        protected void btnRaiseInvoice_Click(object sender, EventArgs e)
        {
            //copy quote items 
            //rise invoice 

            try
            {
                var ID = 0;
                ID = GetOptionID();
                if (ID == 0)
                {
                    var lq = QuotationOptionsBAL.QuotationOption_SelectAll().Where(o => o.CallID == QueryStringValues.CallID).ToList();
                    if (lq.Count == 1)
                    {
                        txtInoicedescription.Text = lq.FirstOrDefault().Description;
                        mdlRaiseInvoice.Show();
                    }
                    else
                    {
                        lblErrorMsg.Text = "Please select a quote";
                    }
                }
                else
                {
                    var lq = QuotationOptionsBAL.QuotationOption_SelectByID(ID);
                    if (lq != null)
                    {
                        txtInoicedescription.Text = lq.Description;
                        mdlRaiseInvoice.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
           

        }

        //get the checked id

       private int GetOptionID()
        {
            int retval = 0;
            
            foreach (ListViewDataItem item in list_Customfields.Items)
            {
                var chk_b = item.FindControl("chkRecommand") as CheckBox;
                //var chk = item.FindControl("chkRecommand") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                var lblID = item.FindControl("lblID") as Label;
                if (chk_b != null && chk_b.Checked)
                {
                    if(lblID != null)
                        retval =  Convert.ToInt32(lblID.Text);
                }
            }

            return retval;
        }

        protected void btnSubmitRaiseinvoice_Click(object sender, EventArgs e)
        {
            try
            {   
                int OptionID = GetOptionID();
                if (OptionID > 0)
                {
                    DeffinityAppDev.WF.DC.controls.DCQuotationItemsCtrl.RiseInvoice(OptionID,txtInoicedescription.Text.Trim());
                }
                else
                {
                    lblErrorMsg.Text = "Please select one estimate";
                    mdlRaiseInvoice.Hide();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void btnCancelRaiseinvoice_Click(object sender, EventArgs e)
        {
            txtInoicedescription.Text = string.Empty;
            mdlRaiseInvoice.Hide();
        }

        protected bool planvisibile()
        {
            return PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_PolicyExists(QueryStringValues.CallID);
        }

        public void BindContacts()
        {
            try
            {
                var jEntity = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                var gList = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_SelectAll(jEntity.RequesterID);
                if(gList.Count >0)
                {
                    gList.Add(new PortfolioMgt.Entity.CustomerKeyContact() { Name = jEntity.RequesterName, EmailAddress = jEntity.RequestersEmailAddress });
                    gridContacts.DataSource = gList.OrderBy(o=>o.Name).ToList();
                    gridContacts.DataBind();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddOptions_Click(object sender, EventArgs e)
        {
           
           
        }

    }
}