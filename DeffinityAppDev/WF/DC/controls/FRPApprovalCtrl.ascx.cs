using DC.BLL;
using DC.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class FRPApprovalCtrl : System.Web.UI.UserControl
    {
        IDCRespository<FixedPriceThresholdApprover> fRepostity = null;
        IDCRespository<FixedPriceApproval> faRepostity = null;
        //IDCRespository<Incident_ServicePrice> priceRepostity = null;

        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
            {
                BindStatus();
                BindGrid();
                UpdateDashboard();
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

                int callid = 0;
                if (Request.QueryString["cid"] != null)
                    callid = Convert.ToInt32(Request.QueryString["cid"].ToString());
                //22	New
                //33	Cancelled
                //34	Resolved
                //35	Closed
                //43	Scheduled
                //44	Awaiting Schedule
                //45	Arrived
                //46	Customer Not Responding
                //47	Feedback Submitted
                //48	Feedback Received
                //string[] strArray = { "Closed", "Cancelled","Resolved" };
                List<Jqgrid> list = new List<Jqgrid>();
                // if (callid > 0)
                //list = FLSDetailsBAL.Jqgridlist(callid).Where(o => !strArray.Contains(o.Status)).ToList();
                //else
                //    list = FLSDetailsBAL.Jqgridlist().Where(o => !strArray.Contains(o.Status)).ToList();
                list = FLSDetailsBAL.Jqgridlist().Where(o=>o.Status != "Cancelled").ToList();

                int rid = 0;
                //if (Request.QueryString["rid"] != null)
                //    rid = Convert.ToInt32(Request.QueryString["rid"].ToString());
                //else
                //    rid = sessionKeys.UID;
                //priceRepostity = new DCRepository<Incident_ServicePrice>();
                //var prcelist = priceRepostity.GetAll().ToList();
                //double totalPrice = 0.0;
                //if (!IsPostBack)
                //{
                //    SqlDataReader dr = IncidentService_Price.IncidentService_Price_Select(callid, "FLS");
                //    while (dr.Read())
                //    {
                //        totalPrice = Convert.ToDouble(dr["OriginalPrice"]);

                //    }
                //    dr.Close();
                //    dr.Dispose();
                //}
                var pmRep = new PortfolioRepository<PortfolioMgt.Entity.PortfolioContactPaymentDetail>();
                var invoiceList = pmRep.GetAll().Where(o => o.OrderRef.Contains("InvoiceID")).ToList();

                var priceRepostity = new DCRepository<Incident_ServicePrice>();
                var serviceRepostity = new DCRepository<Incident_Service>();

                var pricelist = priceRepostity.GetAll().Where(o=> list.Select(p=>p.CallID).Contains(o.IncidentID.HasValue?o.IncidentID.Value:0)).ToList();
                var servicelist = serviceRepostity.GetAll().Where(o => list.Select(p => p.CallID).Contains(o.IncidentID.HasValue ? o.IncidentID.Value : 0)).ToList();
                var dList = pricelist;// InvoiceBAL.Incident_ServicePrice_SelectByCallID(QueryStringValues.CallID);
                var slist = servicelist;// InvoiceBAL.Incident_Service_SelectByCallID(QueryStringValues.CallID);

                var rlist = (from d in dList
                             where d.OriginalPrice >0
                             select new
                             {
                                 d.ID,
                                 d.IncidentID,
                                 d.InvoiceRef,
                                 d.LoggedBy,
                                 d.LoggedDate,
                                 d.ModifiedDate,
                                 d.Notes,
                                 d.OriginalPrice,
                                 d.RevicedPrice,
                                 d.FinalPrice,
                                 d.FinalPriceIncludeTax,
                                 d.Type,
                                 d.UnitConsumption,
                                 Status = d.Status != null? d.Status:string.Empty,
                                 VAT = slist.Count > 0 ? slist.Where(o => o.Incident_ServicePriceID == d.ID).Sum(o => o.VAT.HasValue ? o.VAT.Value : 0) : 0,
                                 SubTotal = slist.Count > 0 ? slist.Where(o => o.Incident_ServicePriceID == d.ID).Sum(o => o.SellingPrice.HasValue ? o.SellingPrice.Value : 0) : 0
                             }
                             ).ToList();

                //fRepostity = new DCRepository<FixedPriceThresholdApprover>();
                //var rlist = fRepostity.GetAll().Where(o => o.UserID == rid).ToList();
                //if (rlist.Count > 0)
                //{
                //faRepostity = new DCRepository<FixedPriceApproval>();
                //var entity = faRepostity.GetAll().ToList();

                var retval = (from j in rlist
                              join f in list on j.IncidentID equals f.CallID 
                                  orderby j.ID  descending
                                  select new
                                  {
                                      CallID = f.CallID,
                                      ID = j.ID,
                                      InvoiceRef = j.InvoiceRef,
                                      CallRef = "" + f.CCID.ToString(),
                                      CCID = f.CCID,
                                      DateLogged = string.Format(Deffinity.systemdefaults.GetStringDateformat(), f.LoggedDateTime),
                                      ServiceTechnician = f.AssignedTechnician,
                                      Mobile = f.AssignedTechnicianContact,
                                      FixedRatePrice = j.SubTotal,
                                      DiscountPrice = (j.FinalPriceIncludeTax.HasValue?j.FinalPriceIncludeTax.Value:0) >0? j.FinalPriceIncludeTax.Value: j.SubTotal,
                                      VAT = j.VAT,
                                      Total = j.OriginalPrice,
                                      Notes = j.Notes,// pricelist.Where(o => o.IncidentID == f.CallID).FirstOrDefault() != null ? pricelist.Where(o => o.IncidentID == f.CallID).FirstOrDefault().Notes : string.Empty,
                                      Requester = f.RequesterName,
                                      Details = f.Details.Length >50? f.Details.Substring(0,50)+"...": f.Details,
                                      Status =  j.Status == ""? "Pending":  j.Status, //DisplayStatus(f.CallID, entity),
                                     payref = (invoiceList.Where(o=>o.OrderRef == "InvoiceID:"+ j.ID.ToString() + ",InvoiceRef:"+j.InvoiceRef).FirstOrDefault()!= null? invoiceList.Where(o => o.OrderRef == "InvoiceID:" + j.ID.ToString() + ",InvoiceRef:" + j.InvoiceRef).FirstOrDefault().PayPalRef:string.Empty)
                                  }).ToList();
                var searchtext = txtSearch.Text.Trim();
                if (searchtext.Length > 0)
                {
                    retval = retval.Where(p => (
                    (p.ServiceTechnician != null ? p.ServiceTechnician.ToLower().Contains(searchtext.ToLower()) : false)
                    || (p.CallRef != null ? p.CallRef.ToLower().Contains(searchtext.ToLower()) : false)
                    || (p.ServiceTechnician != null ? p.ServiceTechnician.ToLower().Contains(searchtext.ToLower()) : false)
                    || (p.Requester != null ? p.Requester.ToLower().Contains(searchtext.ToLower()) : false)
                    || (p.Details != null ? p.Details.ToLower().Contains(searchtext.ToLower()) : false)
                    || (p.Status != null ? p.Status.ToLower().Contains(searchtext.ToLower()) : false)
                    || (p.Mobile != null ? p.Mobile.ToLower().Contains(searchtext.ToLower()) : false)
                   
                    )).OrderByDescending(p => p.ID).Select(p => p).ToList();
                }
                if (ddlStatus.SelectedItem.Text == "ALL")
                        GridDisplay.DataSource = retval;
                    else
                        GridDisplay.DataSource = retval.Where(o=>o.Status == ddlStatus.SelectedItem.Text).ToList();
                    GridDisplay.DataBind();
                //}
            }
            catch(Exception ex)
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
                int rid =0;
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
                else if (e.CommandName == "send")
                {
                    var priceID = Convert.ToInt32(e.CommandArgument.ToString());
                    var priceRepostity = new DCRepository<Incident_ServicePrice>();
                    var serviceRepostity = new DCRepository<Incident_Service>();

                    var pricelist = priceRepostity.GetAll().Where(o => o.ID == priceID).FirstOrDefault();

                    List<ToEmailCalss> tlist = new List<ToEmailCalss>();
                    BindContacts(pricelist.IncidentID.Value);
                    if (gridContacts.Rows.Count > 1)
                    {
                        //hpriceid.Value = priceID.ToString();
                        mdlContacts.Show();
                    }
                    else
                    {


                        InvoiceBAL.SendMailToCustomer(priceID);
                        BindGrid();
                        lblMsg.Text = "Mail sent successfully";
                    }


                }

                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch(Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
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

                    InvoiceBAL.SendMailToCustomer(Convert.ToInt32(hpriceid.Value), tlist);
                    BindGrid();
                    lblMsg.Text = "Mail sent successfully";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void BindContacts(int callid)
        {
            try
            {
                var jEntity = FLSDetailsBAL.Jqgridlist(callid).FirstOrDefault();
                var gList = PortfolioMgt.BAL.CustomerKeyContactsBAL.CustomerKeyContact_SelectAll(jEntity.RequesterID);
                if (gList.Count > 0)
                {
                    gList.Add(new PortfolioMgt.Entity.CustomerKeyContact() { Name = jEntity.RequesterName, EmailAddress = jEntity.RequestersEmailAddress });
                    gridContacts.DataSource = gList.OrderBy(o => o.Name).ToList();
                    gridContacts.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btn_Click(object sender, EventArgs e)
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
                    entity.ApprovedBy = rid;
                    entity.ModifiedDate = DateTime.Now;
                    faRepostity.Edit(entity);
                }

                BindGrid();
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
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
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Button btnapprove = (Button)e.Row.FindControl("btnApprove");
                Button btnDeny = (Button)e.Row.FindControl("btnDeny");
                DropDownList ddlStatus1 = (DropDownList)e.Row.FindControl("ddlStatus");
                if(ddlStatus1 != null)
                {
                    var d = InvoiceBAL.GetInvoiveStatus();
                    ddlStatus1.DataSource = d;
                    ddlStatus1.DataTextField = "Name";
                    ddlStatus1.DataValueField = "ID";
                    ddlStatus1.DataBind();
                    if (!string.IsNullOrEmpty(lblStatus.Text))
                        ddlStatus1.SelectedValue = d.Where(o => o.Name == lblStatus.Text).FirstOrDefault().ID.ToString();
                    else
                        ddlStatus1.SelectedValue = "0";

                }
                if (lblStatus.Text == "Sent")
                {
                    btnapprove.Visible = true;
                    //btnDeny.Visible = true;
                }
                else if (lblStatus.Text == "Pending")
                {
                    btnapprove.Visible = true;
                   // btnDeny.Visible = false;
                }

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
                    if(pentiry != null)
                    {
                       
                        pentiry.Notes = notes;
                        if(status != "Please select...")
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
            ddlStatus.DataSource = InvoiceBAL.GetInvoiveStatus().Where(o=>o.ID >0).ToList();
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