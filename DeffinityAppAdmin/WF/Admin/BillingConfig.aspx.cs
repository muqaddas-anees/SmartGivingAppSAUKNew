using DC.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.Entity;
using PortfolioMgt.BAL;
using DeffinityManager.Portfolio.BAL;

namespace DeffinityAppDev.WF.Admin
{
    public partial class BillingConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindPartnerDropdown();
                    BindCustomFields();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindPartnerDropdown()
        {
            try
            {
                var mlist = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().OrderBy(o=>o.PartnerName).ToList();
                ddlPartner.DataSource = mlist;
                ddlPartner.DataValueField = "ID";
                ddlPartner.DataTextField = "PartnerName";
                ddlPartner.DataBind();
                ddlPartner.Items.Insert(0, new ListItem("Please select...", "0"));
                    
                if(mlist.Count >0)
                {
                    var m = mlist.FirstOrDefault();
                    ddlPartner.SelectedValue = m.ID.ToString();
                    txtDays.Text = (m.TrailDays.HasValue ? m.TrailDays.Value : 0).ToString();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void btnUpdatePartner_Click1(object sender, EventArgs e)
        {
            updateDays();
            UpdateModules();
        }

        private void updateDays()
        {
            try
            {
                var p = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o => o.ID == Convert.ToInt32(ddlPartner.SelectedValue)).FirstOrDefault();
                if (p != null)
                {
                    p.TrailDays = Convert.ToInt32(txtDays.Text.Trim());
                    PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_Update(p);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void btnSubmitToCustomer_Click1(object sender, EventArgs e)
        {
            try
            {
                List<int> SelectedID = new List<int>();
                foreach (ListViewDataItem item in list_Customfields.Items)
                {
                  
                    TextBox txtMonth = item.FindControl("txtMonth") as TextBox;
                    TextBox txtYear = item.FindControl("txtYear") as TextBox;
                    CheckBox chkActive = item.FindControl("chkActive") as CheckBox;
                    var lblID = item.FindControl("lblID") as Label;

                    var pbItem = PortfolioMgt.BAL.PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_Select(Convert.ToInt32(lblID.Text));
                    if(pbItem != null)
                    {
                        pbItem.MonthlyPrice = Convert.ToDouble(txtMonth.Text.Trim());
                        pbItem.YearlyPrice = Convert.ToDouble(txtYear.Text.Trim());
                        pbItem.IsActive = chkActive.Checked;
                        PortfolioMgt.BAL.PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_Update(pbItem);
                    }

                    GridView gv = item.FindControl("gv") as GridView;

                   
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
                //FLS_OptionalMailtoRequester(SelectedID);
                //pnlservice.Visible = false;
                //sessionKeys.Message = "Estimate has been sent to the client";
                //lblservice.Font.Bold = true;

                //Response.Redirect(Request.RawUrl,false);

                // }
               // BindContacts();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            updateDays();
            UpdateModules();
        }

        private void UpdateModules()
        {
            try
            {
                foreach (ListViewDataItem item in list_Customfields.Items)
                {

                    var lblID = item.FindControl("lblID") as Label;
                    var gv = item.FindControl("gv") as GridView;
                    var txtMonth = item.FindControl("txtMonth") as TextBox;
                    var txtYear = item.FindControl("txtYear") as TextBox;
                    CheckBox chkActive = item.FindControl("chkActive") as CheckBox;

                    var BEntity = PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_Select(Convert.ToInt32(lblID.Text));
                    if (BEntity != null)
                    {
                        BEntity.MonthlyPrice = Convert.ToDouble(txtMonth.Text.Trim());
                        BEntity.YearlyPrice = Convert.ToDouble(txtYear.Text.Trim());
                        BEntity.IsActive = chkActive.Checked;

                        PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_Update(BEntity);
                        //update modeuls
                        foreach (GridViewRow row in gv.Rows)
                        {
                            var chk = row.FindControl("chk") as CheckBox;
                            var lblModuleID = row.FindControl("lblModuleID") as Label;
                            var pEntity = PortfolioBillingModulesBAL.PortfolioBillingModulesBAL_SelectAll().Where(o => o.PortfolioBillingTypeID == Convert.ToInt32(lblID.Text) && o.ModuleID == Convert.ToInt32(lblModuleID.Text)).FirstOrDefault();
                            if (pEntity != null)
                            {
                                if (!chk.Checked)
                                {
                                    PortfolioBillingModulesBAL.PortfolioBillingModulesBAL_delete(pEntity.ID);
                                }
                            }
                            else
                            {
                                if (chk.Checked)
                                {
                                    PortfolioBillingModulesBAL.PortfolioBillingModulesBAL_Add(new PortfolioBillingModule() { ModuleID = Convert.ToInt32(lblModuleID.Text), PortfolioBillingTypeID = Convert.ToInt32(lblID.Text) });
                                }
                            }
                        }
                    }
                }

                //PortfolioDefaultsBAL.PortfolioDefaultsBAL_AddUpdate(Convert.ToDouble(txtPrice.Text.Trim()), ddlCurrency.SelectedItem.Text);
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //private string GetItems(List<QuotationItem> qItems)
        //{
        //    string retval = string.Empty;

        //    foreach (var p in qItems)
        //    {
        //        retval = retval + string.Format("<li class='col-md-12 price_item'> {0}</li>", p.ServiceDescription);
        //    }

        //    return retval;
        //}
        private void BindCustomFields()
        {
            try
            {
                var oList = PortfolioMgt.BAL.PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_SelectByPartner(Convert.ToInt32(ddlPartner.SelectedValue)).ToList();// QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o => o.OptionName).ToList();
                var qList = PortfolioMgt.BAL.PortfolioBillingModulesBAL.PortfolioBillingModulesBAL_SelectAll().ToList(); //QuotationBAL.QuotationItem_SelectByCallid(QueryStringValues.CallID).ToList();

                var pList = PortfolioMgt.BAL.PortfolioModulesBAL.PortfolioModulesBAL_ModuleSelect().ToList();// QuotationBAL.QuotationPrice_selectAll(QueryStringValues.CallID);
              
                if (oList.Count > 0)
                {
                   
                    var rLIst = (from o in oList
                                 orderby o.ID ascending
                                 select new
                                 {
                                     o.ID,
                                     o.MonthlyPrice,
                                     o.YearlyPrice,
                                     IsActive = o.IsActive.HasValue?o.IsActive.Value:false,
                                     o.PlanName,
                                     ItemsCountName =" Items",
                                     ModuleSelected=qList.Where(p=>p.PortfolioBillingTypeID == o.ID  ).ToList(),
                                     ModuleList= pList
                                     // ItemsList = GetItems(qList.Where(p => p.QuotationOptionID == o.ID).ToList())
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
                else if (e.CommandName == "Del")
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
                   // Label lbl = (Label)e.Item.FindControl("lblIsApplied");
                    var d = e.Item.DataItem as dynamic;
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
                        hlink.NavigateUrl = string.Format("~/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionID, "quote");

                    }
                    //   ModuleSelected=qList.Where(p=>p.PortfolioBillingTypeID == o.ID  ).ToList(),
                   // ModuleList = pList

                    GridView gv = (GridView)e.Item.FindControl("gv");
                    if(gv != null)
                    {
                        gv.DataSource = d.ModuleList as List<PortfolioModule>;
                        gv.DataBind();
                        var msList = d.ModuleSelected as List<PortfolioBillingModule>;

                        foreach (GridViewRow row in gv.Rows)
                        {
                            var chk = row.FindControl("chk") as CheckBox;
                            var lblModuleID = row.FindControl("lblModuleID") as Label;

                            var m = msList.Where(o => o.ModuleID == Convert.ToInt32(lblModuleID.Text)).FirstOrDefault();
                            if (m != null)
                                chk.Checked = true;
                        }
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

        protected void ddlPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlPartner.SelectedValue) > 0)
                {
                    var m = PortfolioMgt.BAL.PartnerDetailBAL.PartnerDetailBAL_SelectAll().Where(o=>o.ID == Convert.ToInt32(ddlPartner.SelectedValue)).FirstOrDefault();
                    if(m != null)
                    {
                        txtDays.Text = (m.TrailDays.HasValue ? m.TrailDays.Value : 0).ToString();
                    }
                    BindCustomFields();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}