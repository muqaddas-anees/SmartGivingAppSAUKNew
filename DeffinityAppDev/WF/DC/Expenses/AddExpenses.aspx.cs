using DC.BLL;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.Entity;

namespace DeffinityAppDev.WF.DC.Expenses
{
    public partial class AddExpenses : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindJobs();
                    BindReimburseto();
                    BindAccountingcode();
                    ClearFields();
                    BindGrid();
                    
                  
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindJobs()
        {
            try
            {
                var jlist = FLSDetailsBAL.GetActiveJobDropdownLists(sessionKeys.PortfolioID).ToList();
                ddlJobs.DataSource = jlist;
                ddlJobs.DataTextField = "jobtitle";
                ddlJobs.DataValueField = "jobid";
                ddlJobs.DataBind();
                ddlJobs.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindReimburseto()
        {
            try
            {
                var jlist = (from p in UserMgt.BAL.ContractorsBAL.Contractor_SelectAdmins()
                             orderby p.ContractorName
                             select new { ID = p.ID, Text = p.ContractorName }).ToList();
                ddlReimburseto.DataSource = jlist;
                ddlReimburseto.DataTextField = "Text";
                ddlReimburseto.DataValueField = "ID";
                ddlReimburseto.DataBind();
                ddlReimburseto.Items.Insert(0, new ListItem("Not reimbursable", "0"));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void BindAccountingcode()
        {
            try
            {
                var jlist = (from p in TimesheetMgt.BAL.ExpensesAccountingCodesBAL.ExpensesAccountingCodeBAL_Select(sessionKeys.PortfolioID)
                             orderby p.AccountingCode
                             select new { ID = p.ID, Text = p.AccountingCode }).ToList();
                ddlAccountingcode.DataSource = jlist;
                ddlAccountingcode.DataTextField = "Text";
                ddlAccountingcode.DataValueField = "ID";
                ddlAccountingcode.DataBind();
                ddlAccountingcode.Items.Insert(0, new ListItem("Please select...", "0"));
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
                var mlist = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_Select(sessionKeys.UID).OrderByDescending(o=>o.ID).ToList();
                GridPartner.DataSource = mlist;
                GridPartner.DataBind();
                if (GridPartner.Rows.Count == 0)
                {
                    mdlManageOptions.Show();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridPartner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "editmodule")
                {
                    huid.Value = e.CommandArgument.ToString();
                    var m = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectByID(Convert.ToInt32(huid.Value));
                    if (m != null)
                    {
                        txtDate.Text = m.TimeExpensesDate.Value.ToShortDateString();
                        txtItemName.Text = m.Item;
                        txtDetails.Text = m.Details;
                        txtTotal.Text = string.Format("{0:F2}", m.amount);
                        ddlAccountingcode.SelectedValue = m.AccountingCodesID.HasValue ? m.AccountingCodesID.Value.ToString() : "0";
                        ddlJobs.SelectedValue = m.ProjectReference.HasValue?m.ProjectReference.Value.ToString():"0";
                        ddlReimburseto.SelectedValue = m.ReimburseToID.HasValue ? m.ReimburseToID.Value.ToString() : "0";
                        hImageID.Value = m.Image.HasValue ? m.Image.Value.ToString() : "00000000-0000-0000-0000-000000000000";
                        lblOptions.Text = "Edit Expenses";

                        mdlManageOptions.Show();
                    }

                }
                else if (e.CommandName == "del")
                {
                    var m = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSubmitSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var moduleid = Convert.ToInt32(huid.Value);

                if (huid.Value == "0")
                {
                    var t = new TimeExpense();
                    t.AccountingCodesID = Convert.ToInt32(ddlAccountingcode.SelectedValue);
                    t.amount = Convert.ToDouble(txtTotal.Text.Trim());
                    t.ContractorID = sessionKeys.UID;
                    t.Details = txtDetails.Text.Trim();
                    t.Item = txtItemName.Text.Trim();
                    t.LoggedDate = DateTime.Now;
                    t.Notes = string.Empty;
                    t.ProjectReference = Convert.ToInt32(ddlJobs.SelectedValue);
                    t.Qty = 1;
                    t.ReimburseToID = Convert.ToInt32(ddlReimburseto.SelectedValue);
                    t.Status = string.Empty;
                    t.TimeExpensesDate = Convert.ToDateTime(txtDate.Text.Trim());
                    string ItemImg = hImageID.Value != "00000000-0000-0000-0000-000000000000" ? hImageID.Value : "00000000-0000-0000-0000-000000000000";
                    Guid _guid = new Guid(ItemImg);
                    if ((FileUploadMaterial.HasFile))
                    {
                        _guid = Guid.NewGuid();
                       
                    }
                    t.Image = _guid;


                    TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_Add(t);
                    
                    lblMsg.Text = Resources.DeffinityRes.Addedsuccessfully;

                  

                    if (FileUploadMaterial.HasFile)
                    {
                        ImageManager.Save_FlsCustomerFiles(FileUploadMaterial.FileBytes, _guid.ToString(), Server.MapPath("~/WF/UploadData/"));
                        // ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                    }


                    ClearFields();
                    mdlManageOptions.Hide();
                    BindGrid();
                }
                else
                {
                    var t = TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_SelectByID(moduleid);

                    t.AccountingCodesID = Convert.ToInt32(ddlAccountingcode.SelectedValue);
                    t.amount = Convert.ToDouble(txtTotal.Text.Trim());
                    t.ContractorID = sessionKeys.UID;
                    t.Details = txtDetails.Text.Trim();
                    t.Item = txtItemName.Text.Trim();
                    t.LoggedDate = DateTime.Now;
                    t.Notes = string.Empty;
                    t.ProjectReference = Convert.ToInt32(ddlJobs.SelectedValue);
                    t.Qty = 1;
                    t.ReimburseToID = Convert.ToInt32(ddlReimburseto.SelectedValue);
                    t.Status = string.Empty;
                    t.TimeExpensesDate = Convert.ToDateTime(txtDate.Text.Trim());
                    string ItemImg = hImageID.Value != "00000000-0000-0000-0000-000000000000" ? hImageID.Value : "00000000-0000-0000-0000-000000000000";
                    Guid _guid = new Guid(ItemImg);
                    if ((FileUploadMaterial.HasFile))
                    {
                        _guid = Guid.NewGuid();

                    }
                    t.Image = _guid;
                    TimesheetMgt.BAL.TimeExpensesBAL.TimeExpensesBAL_update(t);

                    if (FileUploadMaterial.HasFile)
                    {
                       // ImageManager.Save_FlsCustomerFiles()
                        //ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);

                       

                        ImageManager.Save_FlsCustomerFiles(FileUploadMaterial.FileBytes, _guid.ToString(), Server.MapPath("~/WF/UploadData/"));
                    }

                    lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    ClearFields();
                    BindGrid();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewPopup();
        }

        private void AddNewPopup()
        {
            huid.Value = "0";
            ClearFields();
            lblOptions.Text = "Edit Expenses";
            mdlManageOptions.Show();
        }

        private void ClearFields()
        {
            huid.Value = "0";
            txtDate.Text = DateTime.Now.ToShortDateString();
            txtItemName.Text = string.Empty;
            txtTotal.Text = "0.00";
            txtDetails.Text = string.Empty;
            ddlAccountingcode.SelectedValue = "0";
            ddlJobs.SelectedValue = "0";
            ddlReimburseto.SelectedValue = "0";
            hImageID.Value = "00000000-0000-0000-0000-000000000000";

        }

        public static string GetImageUrl(Guid a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);

            ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
            if (a_oThumbSize.HasValue)
            {
                switch (a_oThumbSize.Value)
                {
                    case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
                }
            }
            else
            {
                eImageType = ImageManager.ImageType.OriginalData;
            }

            // return "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";
            return "~/WF/UploadData/" + a_gId.ToString() + "_thumb.png";
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        public bool CheckImageVisibility(Guid a_guid)
        {
            bool _visible = true;
            if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                _visible = true;
            }
            return _visible;
        }
    }
}