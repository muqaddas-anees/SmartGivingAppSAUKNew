using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.Entity;
using DC.BLL;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
using UserMgt.DAL;

namespace DeffinityAppDev.WF.DC
{
    public partial class SPInvoiceList : System.Web.UI.Page
    {
        //IDCRespository<Jqgrid> cRepository = null;
        IDCRespository<CallInvoice> iRepository = null;
        //IDCRespository<Incident_ServicePrice> sRepository = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
                BindGrid(Convert.ToInt32(ddlUsers.SelectedValue),txtSearch.Text.Trim());
                BindGridInCriditNote();
            }
           
        }

        private void BindGrid(int userid, string searchtext)
        {
            try
            {
                var jlist = FLSDetailsBAL.Jqgridlist().Where(o => o.Status == "Closed").ToList();
                var invoicelist = (from j in jlist.Where(o => o.Cost != "0.00" && !string.IsNullOrEmpty(o.InvoiceDate)).ToList()
                                   select new
                                   {
                                       DateRaised = j.InvoiceDate,
                                       InvoiceNo = j.InvoiceRef,
                                       TicketRef = "" + j.CallID,
                                       Details = j.Details,
                                       ServiceProvider = j.AssignedTechnician,
                                       ServiceProviderID = j.AssignedTechnicianID,
                                       Cost = j.Cost,
                                       VAT = j.VAT,
                                       TotalCost = j.TotalCost,
                                       CallID = j.CallID,
                                       Status = j.InvoiceStatus,
                                       TicketStatus = j.Status
                                   }).ToList();
                if (userid > 0)
                    invoicelist = invoicelist.Where(o => o.ServiceProviderID == userid).ToList();
                if(!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    invoicelist = invoicelist.Where(p => (p.InvoiceNo != null ? p.InvoiceNo.Contains(searchtext.ToLower()) : false)
                   || (p.TicketRef != null ? p.TicketRef.Contains(searchtext.ToLower()) : false)
                   || (p.Details != null ? p.Details.Contains(searchtext.ToLower()) : false)
                   || (p.DateRaised != null ? p.DateRaised.Contains(searchtext.ToLower()) : false)).ToList();
                }
                GridInvoice.DataSource = invoicelist;
                GridInvoice.DataBind();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void BindUsers()
{
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                int[] sids = {1,2,3,4,9};
                ddlUsers.DataSource = cb.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).OrderBy(o => o.ContractorName).ToList();
                ddlUsers.DataTextField = "ContractorName";
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0,new ListItem("Please select...","0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue), txtSearch.Text.Trim());
        }

        protected void GridInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "details")
            {
                Response.Redirect(string.Format( "~/WF/DC/DCNavigation.ashx?callid={0}&type=FLS",e.CommandArgument.ToString()),false);
            }
            else if(e.CommandName == "invoice")
            {
                Response.Redirect(string.Format("~/WF/DC/DCInvoice.aspx?callid={0}&SDID={0}&type=FLS", e.CommandArgument.ToString()), false);
            }
            else if (e.CommandName == "Update1")
            {
                int i = GridInvoice.EditIndex;
                GridViewRow Row = GridInvoice.Rows[i];
                string invoiceID = e.CommandArgument.ToString();

                int invid = Convert.ToInt32(e.CommandArgument.ToString().Replace("Invoice No:", "").Trim());
                if ((DropDownList)Row.FindControl("ddlStatus") != null)
                {
                    var ddlStatus = ((DropDownList)Row.FindControl("ddlStatus"));
                    var lblTicketStatus = ((Label)Row.FindControl("lblTicketStatus"));
                    
                    var sval = ddlStatus.SelectedValue;

                    iRepository = new DCRepository<CallInvoice>();
                    var entity = iRepository.GetAll().Where(o => o.ID == invid).FirstOrDefault();
                    if(entity != null)
                    {
                        if (sval == "Cancelled")
                        { 
                          if(lblTicketStatus.Text == " Cancelled")
                          {
                              entity.StatusValue = sval;
                              iRepository.Edit(entity);
                              lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                          }
                          else
                          {
                              //iRepository.Edit(entity);
                              lblerror.Text = "Ticket is active, you cann't cancel the invoice";
                          }
                        }
                        else
                        {
                            entity.StatusValue = sval;
                            iRepository.Edit(entity);
                            lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        }
                       
                        GridInvoice.EditIndex = -1;
                        BindGrid(Convert.ToInt32(ddlUsers.SelectedValue), txtSearch.Text.Trim());
                    }
                }
            }
        }



        //protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlVendor.SelectedValue != "0")
        //        {
        //            int projectRef = int.Parse(string.IsNullOrEmpty(txtProjectRef.Text) ? "0" : txtProjectRef.Text);
        //            //BindGrid(projectRef, int.Parse(ddlProjects.SelectedValue), int.Parse(ddlWorkSheet.SelectedValue));
        //            BindGrid(projectRef, int.Parse(string.IsNullOrEmpty(ddlProjects.SelectedValue) ? "0" : ddlProjects.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlWorkSheet.SelectedValue) ? "0" : ddlWorkSheet.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlVendor.SelectedValue) ? "0" : ddlVendor.SelectedValue));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}
        public void BindVendorInCreditNote()
        {
            try
            {
                UserMgt.BAL.ContractorsBAL cb = new UserMgt.BAL.ContractorsBAL();
                int[] sids = { 1, 2, 3, 4, 9 };

                ddlVendorsIncredit.DataSource = cb.Contractor_Select_Active().Where(o => sids.Contains(o.SID.Value)).OrderBy(o => o.ContractorName).ToList();
                ddlVendorsIncredit.DataValueField = "ID";
                ddlVendorsIncredit.DataTextField = "ContractorName";
                ddlVendorsIncredit.DataBind();
                ddlVendorsIncredit.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnCreditNote_Click(object sender, EventArgs e)
        {
            try
            {
                 lblmsg.Text = string.Empty;
            //int Projectreference = 0;
            int x = GridInvoice.Rows.Count;
            for (int i = 0; i < GridInvoice.Rows.Count; i++)
            {
                GridViewRow row = GridInvoice.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chk")).Checked;

                if (isChecked)
                {
                    Label lbl = ((Label)row.FindControl("lblCallid"));
                    if (lbl.Text.ToString().Contains(','))
                    {
                        var d = lbl.Text.Split(',');
                        hcallid.Value = d[0];
                    }
                    else
                    {
                        hcallid.Value = lbl.Text;
                    }

                }

            }
                BindVendorInCreditNote();
                BindGridInCriditNote();
                mdlpopUpCreditNote.Show();
                ddlVendorsIncredit.SelectedValue = "0";
                txtCreditValue.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void BindGridInCriditNote()
        {
            try
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    using (UserDataContext Udc = new UserDataContext())
                    {
                        //var projectref = 0;
                        //if (int.Parse(string.IsNullOrEmpty(hcallid.Value) ? "0" : hcallid.Value) != 0)
                        //{
                        //    projectref = int.Parse(string.IsNullOrEmpty(hcallid.Value) ? "0" : hcallid.Value);
                        //}
                       

                        //if (projectref > 0)
                        //{
                            //var CreditNotesList = Pdc.Project_CreditNotes.Where(a => a.ProjectRef == projectref).ToList();
                            var CreditNotesList = Pdc.Project_CreditNotes.ToList();
                            var clist = Udc.Contractors.Where(a => CreditNotesList.Select(b => b.Appliedby.HasValue ? b.Appliedby.Value : 0).ToArray().Contains(a.ID)).ToList();

                            var x = (from a in CreditNotesList
                                     join b in clist on a.Appliedby equals b.ID
                                     orderby a.Id descending
                                     select new
                                     {
                                         Id = a.Id,
                                         CreditValue = a.CreditValue,
                                         Description = a.Description,
                                         DateandTime = a.DateandTime,
                                         Appliedby = b.ContractorName
                                     }).ToList();
                            gridCreditRecord.DataSource = x;
                            gridCreditRecord.DataBind();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnCreditApply_Click(object sender, EventArgs e)
        {
            try
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    Project_CreditNote P_cn = new Project_CreditNote();
                    P_cn.CreditValue = Convert.ToDouble(txtCreditValue.Text);
                    P_cn.Description = "Credit Note from " + ddlVendorsIncredit.SelectedItem.Text.ToString();
                    P_cn.VId = int.Parse(ddlVendorsIncredit.SelectedValue);
                    if (int.Parse(string.IsNullOrEmpty(hcallid.Value) ? "0" : hcallid.Value) != 0)
                    {
                        P_cn.ProjectRef = int.Parse(string.IsNullOrEmpty(hcallid.Value) ? "0" : hcallid.Value);
                    }
                   
                    P_cn.DateandTime = DateTime.Now;
                    P_cn.Appliedby = sessionKeys.UID;
                    Pdc.Project_CreditNotes.InsertOnSubmit(P_cn);
                    if (P_cn.ProjectRef > 0)
                    {
                        Pdc.SubmitChanges();
                        lblCreditMsg.Text = "Credit added successfully";
                        mdlpopUpCreditNote.Show();

                        ddlVendorsIncredit.SelectedValue = "0";
                        txtCreditValue.Text = string.Empty;
                    }
                    else
                    {
                        lblCreditMsgError.Text = "Please select Ticket Ref";
                        mdlpopUpCreditNote.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void imgBtnCancel_Click(object sender, EventArgs e)
        {
            BindGridInCriditNote();
        }

        protected void gridCreditRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "del")
            {
                try
                {
                    using (projectTaskDataContext Pdc = new projectTaskDataContext())
                    {
                        Project_CreditNote P_cn = Pdc.Project_CreditNotes.Where(o => o.Id == Convert.ToInt32(e.CommandArgument.ToString())).FirstOrDefault();

                        if (P_cn != null)
                        {
                            Pdc.Project_CreditNotes.DeleteOnSubmit(P_cn);
                            Pdc.SubmitChanges();
                        }

                        lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindGridInCriditNote();
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }

        protected void GridInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                //Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlStatus");

                if(ddlstatus != null)
                {
                    ddlstatus.DataSource = FLSDetailsBAL.InvoiceStatus();
                    ddlstatus.DataBind();
                    if(!string.IsNullOrEmpty(lblStatus.Text.Trim()))
                    {
                        ddlstatus.SelectedValue = lblStatus.Text.Trim();
                    }
                }

            }
        }

        protected void GridInvoice_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridInvoice.EditIndex = e.NewEditIndex;
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue), txtSearch.Text.Trim());
        }

        protected void GridInvoice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridInvoice.EditIndex = -1;
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue), txtSearch.Text.Trim());
        }
    }
}