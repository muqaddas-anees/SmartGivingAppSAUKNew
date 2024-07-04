using DC.BLL;
using PortfolioMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class UpgradePlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindCustomFields();
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void BindCustomFields()
        {
            try
            {
                var oList = PortfolioMgt.BAL.PortfolioBillingTypeBAL.PortfolioBillingTypeBAL_SelectAll().Where(o=>o.PartnerID == sessionKeys.PartnerID && o.IsActive == true).ToList();// QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o => o.OptionName).ToList();
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
                                     o.PlanName,
                                     ItemsCountName = " Items",
                                     ModuleSelected = qList.Where(p => p.PortfolioBillingTypeID == o.ID).ToList(),
                                     ModuleList = pList
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
                if(e.CommandName == "upgrade")
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
                else if (e.CommandName == "Del")
                {

                    QuotationOptionsBAL.QuotationOption_DeleteByID(Convert.ToInt32(e.CommandArgument.ToString()));

                    sessionKeys.Message = "Estimate deleted successfully";

                    //Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID));

                }
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
            pb.PlanID = pb.PlanID;
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

                        gv.DataSource = (d.ModuleList as List<PortfolioModule>).Where(o=>msList.Select(p=>p.ModuleID).Contains(o.ModuleID)).ToList();
                        gv.DataBind();
                       

                        //foreach (GridViewRow row in gv.Rows)
                        //{
                        //    var chk = row.FindControl("chk") as CheckBox;
                        //    //var chk = row.FindControl("chk") as Label;
                        //    var lblModuleID = row.FindControl("lblModuleID") as Label;

                        //    var m = msList.Where(o => o.ModuleID == Convert.ToInt32(lblModuleID.Text)).FirstOrDefault();
                        //    if (m != null)
                        //        chk.Checked = true;


                        //}
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

        protected void ddlterm_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DropDownList ddl = (DropDownList)sender;
                ListViewItem dst = (ListViewItem)ddl.NamingContainer;
                DropDownList ddlTerm = (DropDownList)dst.FindControl("ddlTerm");
                DropDownList ddlUsers = (DropDownList)dst.FindControl("ddlUser");
                HiddenField hMonth = (HiddenField)dst.FindControl("hMonth");
                HiddenField hYear = (HiddenField)dst.FindControl("hYear");

                Label lbltotal = (Label)dst.FindControl("lbltotal");

                if (ddlTerm.SelectedValue == "Monthly")
                    lbltotal.Text = string.Format("{0:C2}", Convert.ToInt32(ddlUsers.SelectedValue) * Convert.ToDouble(hMonth.Value));
                else
                    lbltotal.Text = string.Format("{0:C2}", Convert.ToInt32(ddlUsers.SelectedValue) * Convert.ToDouble(hYear.Value));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
    }
}