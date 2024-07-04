using DC.BLL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;

namespace DeffinityAppDev.WF.DC_page
{
    public partial class QuoteList : System.Web.UI.Page
    {
        IDCRespository<FixedPriceThresholdApprover> fRepostity = null;
        IDCRespository<FixedPriceApproval> faRepostity = null;
        //IDCRespository<Incident_ServicePrice> priceRepostity = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindStatus();
                BindGrid();
               // UpdateDashboard();
            }

            txtSearch.Attributes.Add("placeholder", "Search...");
        }

        private void UpdateDashboard()
        {
            try
            {
                var d = InvoiceBAL.GetDashboradValues();
                lblPending.InnerText = string.Format("{0:N2}", d.TotalPending);
                lblUnpaid.InnerText = string.Format("{0:N2}", d.TotalUnpaid);
                lblPaidthismonth.InnerText = string.Format("{0:N2}", d.TotalPaidThisMonth);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindGrid()
        {
            try
            {
                using(DCDataContext dc = new DCDataContext())

                {
                    var dList = dc.QuotationListReport(sessionKeys.PortfolioID);

                    var rlist = (from d in dList
                                 where d.QuoteCount > 0
                                 select new
                                 {
                                     d.CallID,
                                     d.CCID,
                                     d.RequesterName,
                                     d.ContactNumber,
                                     d.Address,
                                     d.City,
                                     d.State,
                                     d.PostCode,
                                     d.QuoteCount,
                                     d.QuoteAmount,
                                 }
                             ).ToList();

                   

                    var retval = (from d in rlist
                                  orderby d.CallID descending
                                  select new
                                  {
                                      ID = d.CallID,
                                      d.CallID,
                                      CCID= d.CCID.ToString(),
                                      CallRef = d.CCID.ToString(),
                                      d.RequesterName,
                                      d.ContactNumber,
                                      d.Address,
                                      d.City,
                                      d.State,
                                      d.PostCode,
                                      d.QuoteCount,
                                      d.QuoteAmount, }).ToList();
                    var searchtext = txtSearch.Text.Trim();
                    if (searchtext.Length > 0)
                    {
                        retval = retval.Where(p => (
                        (p.RequesterName != null ? p.RequesterName.ToLower().Contains(searchtext.ToLower()) : false)
                        || (p.CCID != null ? p.CCID.ToLower().Contains(searchtext.ToLower()) : false)
                        || (p.Address != null ? p.Address.ToLower().Contains(searchtext.ToLower()) : false)
                        || (p.City != null ? p.City.ToLower().Contains(searchtext.ToLower()) : false)
                        || (p.State != null ? p.State.ToLower().Contains(searchtext.ToLower()) : false)
                        || (p.PostCode != null ? p.PostCode.ToLower().Contains(searchtext.ToLower()) : false)
                        || (p.ContactNumber != null ? p.ContactNumber.ToLower().Contains(searchtext.ToLower()) : false)

                        )).OrderByDescending(p => p.ID).Select(p => p).ToList();
                    }
                    //if (ddlStatus.SelectedItem.Text == "ALL")
                        GridDisplay.DataSource = retval.ToList();
                   // else
                    //    GridDisplay.DataSource = retval.Where(o => o.Status == ddlStatus.SelectedItem.Text).ToList();
                    GridDisplay.DataBind();

                }
                
               
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public string DisplayStatus(int CallID, List<FixedPriceApproval> entity)
        {
            var ent = entity.Where(o => o.CallID == CallID).FirstOrDefault();
            if (ent == null)
            {
                return "Pending";
            }
            else
            {
                if (ent.ApprovedBy.HasValue)
                    return "Paid";
                else if (ent.DeniedBy.HasValue)
                    return "Sent";
                else
                    return "Pending";
            }

        }
        protected void GridDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rid = 0;
                if (Request.QueryString["rid"] != null)
                    rid = Convert.ToInt32(Request.QueryString["rid"].ToString());
                else
                    rid = sessionKeys.UID;


                faRepostity = new DCRepository<FixedPriceApproval>();
                var entity = faRepostity.GetAll().Where(p => p.CallID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                if (e.CommandName == "Approve")
                {

                    if (entity != null)
                    {
                        entity.ApprovedBy = rid;
                        entity.ModifiedDate = DateTime.Now;
                        faRepostity.Edit(entity);
                    }

                }
                else if (e.CommandName == "Deny")
                {
                    entity.DeniedBy = rid;
                    entity.ModifiedDate = DateTime.Now;
                    faRepostity.Edit(entity);
                }

                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            //var c = ((Control)sour).NamingContainer;
            try
            {
                int rid = 0;
                int callid = 0;
                int ccid = 0;
                GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;
                var lblcallid = grdrow.FindControl("lblCallID") as Label;
                var lblCCID = grdrow.FindControl("lblCCID") as Label;
                if (lblcallid != null)
                {
                    callid = Convert.ToInt32(lblcallid.Text);
                    ccid = Convert.ToInt32(lblCCID.Text);
                }
                var qList = QuotationOptionsBAL.QuotationOption_SelectAll().Where(o => o.CallID == callid).ToList().OrderBy(o => o.OptionName).ToList();
                var qlList = QuotationBAL.QuotationItem_SelectByCallid(callid).ToList();

                var rList = (from q in qList
                                 //join p in qlList on q.ID equals p.QuotationOptionID
                             select new
                             {
                                 q.OptionName,
                                 q.ID,
                                 q.CallID,
                                 CCID = ccid,
                                 QuoteAmount = string.Format("{0:N2}", qlList.Where(o=>o.QuotationOptionID == q.ID).Sum(o=>o.SalesPrice + o.VAT) != null? qlList.Where(o => o.QuotationOptionID == q.ID).Sum(o => o.SalesPrice + o.VAT).Value:0)
                             }).ToList();
                gridQuotations.DataSource = rList;
                gridQuotations.DataBind();
                mdlInvoice.Show();
               
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        protected void btndeny_Click(object sender, EventArgs e)
        {
            //var c = ((Control)sour).NamingContainer;
            try
            {
                int rid = 0;
                if (Request.QueryString["rid"] != null)
                    rid = Convert.ToInt32(Request.QueryString["rid"].ToString());
                else
                    rid = sessionKeys.UID;

                int callid = 0;
                if (Request.QueryString["cid"] != null)
                    callid = Convert.ToInt32(Request.QueryString["cid"].ToString());

                GridViewRow grdrow = (GridViewRow)((Button)sender).NamingContainer;

                var lblcallid = grdrow.FindControl("lblCallID") as Label;
                if (lblcallid != null)
                {
                    callid = Convert.ToInt32(lblcallid.Text);
                }
                faRepostity = new DCRepository<FixedPriceApproval>();
                var entity = faRepostity.GetAll().Where(p => p.CallID == callid).FirstOrDefault();
                if (entity != null)
                {
                    entity.DeniedBy = rid;
                    entity.ModifiedDate = DateTime.Now;
                    faRepostity.Edit(entity);
                }

                BindGrid();
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        protected void GridDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if(e.Row.ite)
            //lblStatus
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                //Button btnapprove = (Button)e.Row.FindControl("btnApprove");
                //Button btnDeny = (Button)e.Row.FindControl("btnDeny");
                //DropDownList ddlStatus1 = (DropDownList)e.Row.FindControl("ddlStatus");
                //if (ddlStatus1 != null)
                //{
                //    var d = InvoiceBAL.GetInvoiveStatus();
                //    ddlStatus1.DataSource = d;
                //    ddlStatus1.DataTextField = "Name";
                //    ddlStatus1.DataValueField = "ID";
                //    ddlStatus1.DataBind();
                //    if (!string.IsNullOrEmpty(lblStatus.Text))
                //        ddlStatus1.SelectedValue = d.Where(o => o.Name == lblStatus.Text).FirstOrDefault().ID.ToString();
                //    else
                //        ddlStatus1.SelectedValue = "0";

                //}
                //if (lblStatus.Text == "Sent")
                //{
                //    btnapprove.Visible = true;
                //    //btnDeny.Visible = true;
                //}
                //else if (lblStatus.Text == "Pending")
                //{
                //    btnapprove.Visible = true;
                //    // btnDeny.Visible = false;
                //}

            }
        }
        //gridQuotations_RowCommandNew
        protected void gridQuotations_RowCommandNew(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var entity = QuotationOptionsBAL.QuotationOption_SelectByID(Convert.ToInt32( e.CommandArgument.ToString()));
                if (e.CommandName == "Invoice")
                {
                    var cDetails = FLSDetailsBAL.Jqgridlist(entity.CallID).FirstOrDefault();
                    DeffinityAppDev.WF.DC.controls.DCQuotationItemsCtrl.RiseInvoice(cDetails.CCID, entity.CallID,entity.ID,"Invoice");

                }
                }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
            protected void GridDisplay_RowCommandNew(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rid = 0;
                if (Request.QueryString["rid"] != null)
                    rid = Convert.ToInt32(Request.QueryString["rid"].ToString());
                else
                    rid = sessionKeys.UID;
                faRepostity = new DCRepository<FixedPriceApproval>();
                var entity = faRepostity.GetAll().Where(p => p.CallID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                if (e.CommandName == "update1")
                {
                    int i = GridDisplay.EditIndex;
                    GridViewRow Row = GridDisplay.Rows[i];
                    var notes = ((TextBox)Row.FindControl("txtNotes")).Text;
                    var status = ((DropDownList)Row.FindControl("ddlStatus")).SelectedItem.Text;

                    var paRepostity = new DCRepository<Incident_ServicePrice>();
                    var pentiry = paRepostity.GetAll().Where(o => o.ID == Convert.ToInt32(e.CommandArgument)).FirstOrDefault();
                    if (pentiry != null)
                    {

                        pentiry.Notes = notes;
                        if (status != "Please select...")
                            pentiry.Status = status;
                        paRepostity.Edit(pentiry);
                        //update
                        InvoiceBAL.UpdateInvoiceStatusJournal(pentiry.IncidentID.Value, pentiry.ID, status);
                        //update status
                        //if (status == "Paid")
                        //{
                        //    entity.ApprovedBy = sessionKeys.UID;
                        //    entity.DeniedBy = null;
                        //}
                        //else if (status == "Sent")
                        //{
                        //    entity.ApprovedBy = null;
                        //    entity.DeniedBy = sessionKeys.UID;
                        //}
                        //else
                        //{
                        //    entity.DeniedBy = null;
                        //    entity.ApprovedBy = null;
                        //}

                        faRepostity.Edit(entity);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;

                        UpdateDashboard();
                    }
                    GridDisplay.EditIndex = -1;
                    BindGrid();
                }
                else
                if (e.CommandName == "Approve")
                {

                    if (entity != null)
                    {
                        entity.ApprovedBy = rid;
                        entity.ModifiedDate = DateTime.Now;
                        faRepostity.Edit(entity);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    }

                }
                else if (e.CommandName == "Deny")
                {
                    entity.DeniedBy = rid;
                    entity.ModifiedDate = DateTime.Now;
                    faRepostity.Edit(entity);
                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                }


            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        protected void GridDisplay_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridDisplay.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void GridDisplay_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridDisplay.EditIndex = -1;
            BindGrid();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        public void BindStatus()
        {
            ddlStatus.DataSource = InvoiceBAL.GetInvoiveStatus().Where(o => o.ID > 0).ToList();
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("ALL", "0"));

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GridDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridDisplay.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}