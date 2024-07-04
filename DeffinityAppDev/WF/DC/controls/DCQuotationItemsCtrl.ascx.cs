using DC.BAL;
using DC.BLL;
using DC.DAL;
using DC.Entity;
using DC.SRV;
using Deffinity.IncidentService;
using Deffinity.IncidentService_Price_Manager;
using IncidentMgt.DAL;
using Microsoft.ApplicationBlocks.Data;
using POMgt.DAL;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DocumentFormat.OpenXml.Drawing;

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class DCQuotationItemsCtrl : System.Web.UI.UserControl
    {
        Incidents.Entity.Incident incid = new Incidents.Entity.Incident();
        IncidentDataContext obj = new IncidentDataContext();
        IDCRespository<FixedPriceThreshold> tRepository = null;
        IDCRespository<FixedPriceThresholdApprover> arRepository = null;
        IDCRespository<FixedPriceApproval> aRepository = null;
        IUserRepository<UserMgt.Entity.Contractor> uRepository = null;
        IDCRespository<FixedRateType> ftypeRepository = null;

        public List<FixedRateType> GetTypeData()
        {
            ftypeRepository = new DCRepository<FixedRateType>();
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
        public string Type
        {
            set;
            get;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["cid"] != null)
            {
                sessionKeys.PortfolioID = Convert.ToInt32(Request.QueryString["cid"].ToString());
            }
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //if (url.ToLower().Contains("dev.123servicepro.com") || url.ToLower().Contains("localhost"))
            //{
            //    pnlTemplateSection.Visible = true;
            //    pnlTemplateDisplay.Visible = true;
            //}
            //else
            //{
            //    pnlTemplateSection.Visible = false;
            //    pnlTemplateDisplay.Visible = false;
            //}
            //GetThresholdPrice();
            //Master.PageHead = "Service Request";
            //if (sessionKeys.IncidentID > 0)
            //    lblTitle.InnerText = "Services - SR Reference " + sessionKeys.IncidentID;
            //else
            //    lblTitle.InnerText = "Services - New Service Request";
            //ServiceState.ClearServiceCache();
            if (!IsPostBack)
            {
                try
                {
                    SetJobDescription();

                    BindOptionMenu();
                    //BindOptions();
                    //bindMaintenacePlan();
                    //DataTable dt = this.BindMenuData(0);
                    //DynamicMenuControlPopulation(dt, 0, null);
                    //check DLT
                    //DLT_check();

                    BasicBind();
                    BindPopWindow();
                    BindCustomFields();
                    bindGrid();
                    bindServiceType();
                    if (Type != "FLS")
                    {
                        pnlOrder.Visible = false;
                    }
                    else
                    {
                        pnlOrder.Visible = true;
                    }

                    //fill and get the values for total
                    Service_Prices();
                    Getstatus(QueryStringValues.CallID);
                    // hide the units values
                    Hide_unitsection();


                    OptionsAcceptMsg();
                    Gridfilesbind();
                    if (QueryStringValues.Type == "mail")
                    {

                        pnlAccept.Visible = true;
                        pnlAdd.Visible = false;
                        btnUpdateTotals.Visible = false;
                        pnlTopAddPanel.Visible = false;
                        if (Grid_services.Rows.Count > 0)

                        {
                            Grid_services.Columns[0].Visible = false;
                            Grid_services.Columns[8].Visible = false;

                        }

                        //if (GridOffers.Rows.Count > 0)
                        //{

                        //    GridOffers.Columns[0].Visible = false;

                        //}
                    }
                    else
                    {

                    }


                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }

        private void SetJobDescription()
        {
            try
            {
                var j = QuotationOptionsBAL.QuotationOption_SelectByID(QueryStringValues.OPTION);
                if (j != null)
                {
                    lblJobdes.Text = j.OptionName;
                    txtJobDescription.Text = j.Description;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void OptionsAcceptMsg()
        {
            try
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    var cEntity = dc.CallDetails.Where(o => o.ID == QueryStringValues.CallID).FirstOrDefault();
                    var dcLIst = dc.QuotationPrices.Where(o => o.CallID == QueryStringValues.CallID && o.QuotationOptionID == QueryStringValues.OPTION && o.IsOptionActive == true).FirstOrDefault();
                    if (dcLIst != null)
                    {
                        lblQuotemsg.Visible = true;
                    }
                    else
                    {
                        lblQuotemsg.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private FixedPriceApproval GetFixedPriceApprovalData()
        {
            aRepository = new DCRepository<FixedPriceApproval>();
            return aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
        }
        private double GetThresholdPrice()
        {
            double retval = 0.0;
            tRepository = new DCRepository<FixedPriceThreshold>();
            var fentiry = tRepository.GetAll().FirstOrDefault();
            if (fentiry != null)
                retval = fentiry.FPTPrice;

            return retval;
        }
        private void Hide_unitsection()
        {
            lblUnit_title.Visible = false;
            lbluc.Visible = false;
        }
        private void BasicBind()
        {
            BindVendor();
            //BindCategory();
            //BindSubCategory();
            //ddlCategory.DataBind();
            //BindCategory();
            //bindSubcategory();
            //BindService();
        }


        private void Getstatus(int IncidentID)
        {

            if (Type == "FLS")
            {
                CallDetail callDetail = CallDetailsBAL.SelectbyId(QueryStringValues.CallID);
                if (callDetail != null)
                {
                    string statusName = StatusBAL.StatusNamebyId(Convert.ToInt32(callDetail.StatusID));
                    if ((statusName.ToLower() == "quote requested") || (statusName.ToLower() == "order received") || (statusName.ToLower() == "cancelled") || (statusName.ToLower() == "quotation submitted"))
                    {
                        pnlStatus.Visible = true;
                        lblStatus.InnerText = statusName;
                    }
                    else
                    {
                        pnlStatus.Visible = false;
                        lblStatus.Visible = false;

                    }
                }
            }
            else
            {
                Incidents.Entity.Incident incid = new Incidents.Entity.Incident();
                incid = Incidents.DAL.IncidentHelper.SelectById(IncidentID);
                if ((incid.Status == "Approved") || (incid.Status == "Pending Approval") || (incid.Status == "Quote Requested") || (incid.Status == "Declined") || (incid.Status == "In Hand"))
                {
                    if (incid.IncidentType == "Service Request")
                    {
                        pnlStatus.Visible = true;
                        lblStatus.InnerText = incid.Status;
                    }
                }
                else
                {
                    pnlStatus.Visible = false;
                    lblStatus.Visible = false;

                }
            }

        }
        private void Service_Prices()
        {
            try
            {
               


                SqlDataReader dr = IncidentService_Price.Quotation_Price_Select(QueryStringValues.CallID, Type, QueryStringValues.OPTION);
                while (dr.Read())
                {
                   // lblTotalPrice.InnerText = string.Format("{0:f2}", dr["OriginalPrice"]);
                    //lblTotalPrice.InnerText = string.Format("{0:f2}", dr["RevicedPrice"]);
                    lblDiscountValue.InnerText = string.Format("{0:f2}", dr["DiscountPrice"]);
                   // txtDiscount.Text = string.Format("{0:f2}", dr["DiscountPrice"]);
                    txtDiscountPercent.Text = dr["DiscountPercent"].ToString();
                    lblRevisedPrice.InnerText = string.Format("{0:f2}", dr["RevicedPrice"]);
                    lbluc.InnerText = string.Format("{0:f2}", dr["UnitConsumption"]);
                    //txtNotes.Text = dr["Notes"].ToString();
                    txtNotes.Text = dr["Notes"].ToString();
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


                var q = QuotationBAL.QuotationPrice_selectByOptionID(QueryStringValues.CallID, QueryStringValues.OPTION).FirstOrDefault();
                if (q != null)
                {
                    lblTotalPrice.InnerText = string.Format("{0:F2}", q.RevicedPrice);
                    txtDiscount.Text = string.Format("{0:F2}", q.FinalPriceIncludeTax);
                    
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Response.Redirect("CaseAssets.aspx", false);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("NewCase.aspx", false);
        }


        protected void Grid_services_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    try
                    {
                        int ID;
                        double QTY;
                        double SellingPrice;
                        double VAT;
                        double units = 0;
                        double SalesPrice = 0;
                        string Notes;
                        int servicetype = 0;
                        string desc = string.Empty;
                        int i = Grid_services.EditIndex;
                        GridViewRow Row = Grid_services.Rows[i];
                        ID = int.Parse(e.CommandArgument.ToString());
                        QTY = Convert.ToDouble(((TextBox)Row.FindControl("txtQty")).Text);
                        SellingPrice = double.Parse(((TextBox)Row.FindControl("txtSellingPrice")).Text);
                        SalesPrice = double.Parse(((TextBox)Row.FindControl("txtSalesPrice")).Text);
                        VAT = double.Parse(((TextBox)Row.FindControl("txtVAT")).Text);
                        if ((TextBox)Row.FindControl("txtuc") != null)
                            units = double.Parse(string.IsNullOrEmpty(((TextBox)Row.FindControl("txtuc")).Text.Trim()) ? "0" : ((TextBox)Row.FindControl("txtuc")).Text.Trim());
                        Notes = ((TextBox)Row.FindControl("txtEnotes")).Text;
                        if ((TextBox)Row.FindControl("txtDesc") != null)
                        {
                            desc = ((TextBox)Row.FindControl("txtDesc")).Text;
                            var ddltype = (Row.FindControl("ddlSType") as DropDownList);
                            if (!string.IsNullOrEmpty(ddltype.SelectedValue))
                                servicetype = Convert.ToInt32(ddltype.SelectedValue);
                        }
                        Guid g = Guid.Parse("00000000-0000-0000-0000-000000000000");
                        //check qty or amount is updated
                        using (DCDataContext dc = new DCDataContext())
                        {
                            var applyVAT = true;
                            var qitem = dc.QuotationItems.Where(o => o.ID == ID).FirstOrDefault();
                            if (qitem != null)
                            {
                                g = Guid.Parse(qitem.Image.HasValue ? qitem.Image.Value.ToString() : "00000000-0000-0000-0000-000000000000");
                                if ((qitem.QTY != QTY) || (qitem.SellingPrice != SellingPrice))
                                {
                                    var vt = 0.00;
                                    if (applyVAT)
                                    {
                                        vt = VATByCustomerBAL.VATByCustomer_select();
                                    }
                                    var v = (QTY * SellingPrice);
                                    if (vt > 0)
                                        VAT = (QTY * SellingPrice) * (vt / 100);
                                    else
                                        VAT = 0.00;




                                }

                                //
                                //if(SellingPrice != qitem.SellingPrice || QTY != qitem.QTY)
                                //{

                                //}
                            }
                        }
                        //update

                        ServiceManager.Quotation_Update(ID, QTY, SellingPrice, units, Notes, desc, servicetype, VAT, g, SalesPrice, 0, 0);
                        lblMsg.Text = "Updated successfully";
                        Service_Prices();
                        //Gridupdate
                        bindGrid();

                    }
                    catch (Exception ex)
                    { LogExceptions.WriteExceptionLog(ex); }
                    //fill and get the values for total

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void bindGrid()
        {
            try
            {
                Grid_services.DataSource = ServiceManager.Quotation_SelectByIncidentID(QueryStringValues.CallID, Type, QueryStringValues.OPTION);
                Grid_services.DataBind();
                //update grid
                BindServiceCharge();
                if (Grid_services.Rows.Count > 0)
                {
                   // btnSendQuotetoCustomer.Visible = true;
                    btnSave.Visible = true;
                    //check maintenance plan already exists
                    var retval = PortfolioMgt.BAL.PortfolioContactAddressBAL.PorfolioContact_PolicyExistsByAddressID(QueryStringValues.CallID);
                    if (retval)
                    {
                        pnlOffer.Visible = true;
                        pnlSummary.Visible = true;
                        bindMaintenacePlan();
                    }

                }
                else
                {
                    //btnSendQuotetoCustomer.Visible = false;
                    btnSave.Visible = false;
                    pnlSummary.Visible = false;
                    ShowAddPopup();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindServiceCharge()
        {
            if (Grid_services.Rows.Count > 0)
            {
                //Bind service charge
                using (DCDataContext dc = new DCDataContext())
                {
                    var s = dc.ServiceChargeDefaults.Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (s != null)
                    {
                        //check service charge already exits or not

                        var isExists = dc.QuotationItems.Where(o => o.CallidID == QueryStringValues.CallID && o.QuotationOptionID == QueryStringValues.OPTION).Where(o => o.ServiceDescription.ToLower() == s.Name.ToLower()).FirstOrDefault();
                        if (isExists == null)
                        {
                            string ItemImg = string.Empty;

                            Guid _guid = new Guid();
                            if ((_guid == Guid.Empty) && (FileUploadMaterial.HasFile))
                            {
                                _guid = Guid.NewGuid();
                            }

                            if ((s.Amount.HasValue ? s.Amount.Value : 0.00) > 0)
                            {
                                int retval = Insertservice(0, QueryStringValues.CallID, 1, 2, Type, s.Name, 1, s.Amount.HasValue ? s.Amount.Value : 0.00, 0, txtItemNotes.Text, _guid.ToString(), s.Amount.HasValue ? s.Amount.Value : 0.00, 0, 0, s.ApplyVAT.HasValue ? s.ApplyVAT.Value : false);
                                Service_Prices();

                                Grid_services.DataSource = ServiceManager.Quotation_SelectByIncidentID(QueryStringValues.CallID, Type, QueryStringValues.OPTION);
                                Grid_services.DataBind();
                            }
                        }
                    }
                }
            }
        }
        protected void btnUpdateTotals_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGrid();
                IncidentService_Price.QuotationPrice_Update(QueryStringValues.CallID, double.Parse(txtDiscountPercent.Text.Trim()), txtNotes.Text.Trim(), Type, QueryStringValues.OPTION);
                bindGrid();
                Service_Prices();
                //AddServicePriceJouranl(txtNotes.Text.Trim(), Convert.ToDouble(string.IsNullOrEmpty(lblTotalPrice.InnerText.Trim()) ? "0" : lblTotalPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblRevisedPrice.InnerText.Trim()) ? "0" : lblRevisedPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lbluc.InnerText.Trim()) ? "0" : lbluc.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPercent.Text.Trim()) ? "0" : txtDiscountPercent.Text.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblDiscountValue.InnerText.Trim()) ? "0" : lblDiscountValue.InnerText.Trim()), DateTime.Now);
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        protected void Grid_services_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception == null)
            {
                e.ExceptionHandled = true;
            }
        }
        protected void obj_services_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {

            if (e.Exception == null)
            {
                e.ExceptionHandled = true;
            }
        }
        protected void Grid_services_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Grid_services.EditIndex = -1;
            bindGrid();
        }
        protected void Grid_services_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            Grid_services.EditIndex = -1;
            bindGrid();
        }
        protected void Grid_services_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Grid_services.EditIndex = e.NewEditIndex;
            bindGrid();
        }
        //protected void btnSubmitToCustomer_Click(object sender, EventArgs e)
        //{
        //    BuildMail();
        //}
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bindSubcategory();
            //fillGrid(ddlSelect.SelectedItem.Text);
            BindSubCategory();
            //BindItems();
            BindService();
        }
        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlSelect1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindItems();
            BindService();
        }
        private void BindService()
        {
            try
            {
                ////ddlService.DataSourceID = "SqlDataSource2";
                //ddlService.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue",
                //new SqlParameter("@Type", int.Parse(ddlSelect.SelectedValue)), new SqlParameter("@PortfolioID", Convert.ToInt32(ddlVendors.SelectedValue))
                //, new SqlParameter("@Category", int.Parse(ddlCategory.SelectedValue)), new SqlParameter("@SubCategory", int.Parse(ddlSubCategory.SelectedValue)),
                //new SqlParameter("@Add_select", 1), new SqlParameter("@Supplier", 0)).Tables[0];
                //ddlService.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //private void bindSubcategory()
        //{
        //    DS_Category.DataBind();
        //    ddlSubCategory.DataSource = DS_SubCategory;
        //    ddlSubCategory.DataValueField = "ID";
        //    ddlSubCategory.DataTextField = "CategoryName";
        //    ddlSubCategory.DataBind();

        //}
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindService();

        }
        //private void BindCategory()
        //{
        //    //ddlCategory.DataSourceID = "DS_Category";
        //   // ddlCategory.DataBind();

        //}
        #region Mail
        private void BuildMail()
        {
            SDTeamToCustomer1.Visible = true;
            try
            {
                if (sessionKeys.IncidentID > 0)
                {
                    var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                    string emailid = SDTeamToCustomer1.BindControls();
                    StringWriter stringWrite = new StringWriter();
                    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    SDTeamToCustomer1.RenderControl(htmlWrite);
                    Email ToEmail = new Email();
                    var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                    string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                    if (File.Exists(pname))
                    {
                        Attachment attachment1 = new Attachment(pname);
                        attachment1.Name = q.CurrentTemplateName + ".pdf";
                        ToEmail.SendingMail(emailid.ToString(), "Quote - "+sessionKeys.JobDisplayName+" Ref: " + QueryStringValues.CCID, htmlWrite.InnerWriter.ToString(), attachment1);
                    }
                    else
                    {
                        ToEmail.SendingMail(emailid.ToString(), "Quote - "+sessionKeys.JobDisplayName+" Ref: " + QueryStringValues.CCID, htmlWrite.InnerWriter.ToString());
                    }
                    SDTeamToCustomer1.Dispose();
                }

            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
            finally { SDTeamToCustomer1.Visible = false; }

        }
        //private Incident FillControlFields()
        //{
        //    Incident incident = new Incident();
        //    incident.Status = "Pending Approval";
        //    return incident;
        //}

        //Deffinity_CustomerOrderToSDteam
        #endregion
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                //hItemID.Value = "0";
                AddNewItems();
                BindServiceCharge();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void AddNewItems()
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
                Service_Prices();
                BindCustomFields();
                mdlAddItem.Hide();
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

                int retval = Insertservice(serviceID, QueryStringValues.CallID, qty, 2, Type, txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), cost, vat, txtItemNotes.Text.Trim(), _guid.ToString(), sales, markup, vtRate);
                if (retval == 1)
                {

                    if (FileUploadMaterial.HasFile)
                    {
                        ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                    }
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
                    Service_Prices();
                    hCatelogID.Value = "0";
                    hImageID.Value = "00000000-0000-0000-0000-000000000000";
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    lblError.Text = "Item already exists";
                }


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
                        mdlAddItem.Hide();
                        Service_Prices();
                        BindCustomFields();
                        hCatelogID.Value = "0";
                        hImageID.Value = "00000000-0000-0000-0000-000000000000";
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                    }


                }
            }
        }
        private int Insertservice(int ServiceID, int IncidentID, double QTY, int ServiceTypeID, string type, string servicetext, int servicetypeid, double cost, double VAT, string notes, string imgGuid, double SalesPrice, double markup, double vatrate, bool applyVAT = true, int PolicyID = 0, string PolicyNotes = "")
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

            return QuotationBAL.InsertQuoteItem(ServiceID, IncidentID, QTY, ServiceTypeID, type, servicetext, servicetypeid, cost, VAT, notes, imgGuid, SalesPrice, markup, vatrate, applyVAT = true, PolicyID = 0, PolicyNotes = "");


        }

        //public void btnSubmitToCustomer_Click1(object sender, EventArgs e)
        //{
        //    //if (txtDiscountPercent.Text != string.Empty)
        //    //{
        //    IncidentService_Price.QuotationPrice_Update(sessionKeys.IncidentID, double.Parse(txtDiscountPercent.Text.Trim()), txtNotes.Text.Trim(), Type, QueryStringValues.OPTION);

        //    //if (Type != "FLS")
        //    //{
        //    //    using (IncidentDataContext su = new IncidentDataContext())
        //    //    {
        //    //        int id = QueryStringValues.SDID;
        //    //        IncidentMgt.Entity.Incident inc = su.Incidents.Where(p => p.ID == id).FirstOrDefault();
        //    //        inc.Status = "Pending Approval";
        //    //        su.SubmitChanges();
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    CallDetail cd = CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
        //    //    //40	Quotation Submitted
        //    //    cd.StatusID = 40;
        //    //    CallDetailsBAL.CallDetailsUpdate(cd);
        //    //    DateTime modified_date = DateTime.Now;
        //    //    AddCallDetailsJournal(cd, modified_date);
        //    //    AddServicePriceJouranl(txtNotes.Text.Trim(), Convert.ToDouble(string.IsNullOrEmpty(lblTotalPrice.InnerText.Trim()) ? "0" : lblTotalPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblRevisedPrice.InnerText.Trim()) ? "0" : lblRevisedPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lbluc.InnerText.Trim()) ? "0" : lbluc.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPercent.Text.Trim()) ? "0" : txtDiscountPercent.Text.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblDiscountValue.InnerText.Trim()) ? "0" : lblDiscountValue.InnerText.Trim()), modified_date);
        //    //}

        //    //Incident incident = new Incident();
        //    //incident.Status = "Pending Approval";
        //    //IncidentHelper.Update(incident);
        //    //BuildMail();
        //    FLS_OptionalMailtoRequester();
        //    pnlservice.Visible = false;
        //    FLSDetailsBAL.UpdateTicketStatus(QueryStringValues.CallID, sessionKeys.UID, JobStatus.Quote_Sent);
        //    lblservice.Text = "Quotation has been sent to the client";
        //    lblservice.Font.Bold = true;



        //    //  }

        //}
        private void AddCallDetailsJournal(CallDetail cd, DateTime modified_date)
        {
            try
            {
                WebService ws = new WebService();
                CallDetailsJournal cdj = new CallDetailsJournal();
                cdj.CallID = cd.ID;
                cdj.CompanyID = cd.CompanyID;
                cdj.LoggedBy = cd.LoggedBy;
                cdj.LoggedDate = cd.LoggedDate;
                cdj.ModifiedBy = sessionKeys.UID;
                cdj.ModifiedDate = modified_date;
                cdj.RequesterID = cd.RequesterID;
                cdj.RequestTypeID = cd.RequestTypeID;
                cdj.SiteID = cd.SiteID;
                cdj.StatusID = cd.StatusID;
                cdj.VisibleToCustomer = true;

                ws.AddCallDetailsJournal(cdj);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void AddServicePriceJouranl(string notes, double total_price, double reviced_price, double unitconsuption, double discount_percent, double discount_price, DateTime modified_date)
        {
            try
            {
                WebService ws = new WebService();
                ServicePriceJournal sj = new ServicePriceJournal();
                sj.IncidentID = sessionKeys.IncidentID;
                sj.Type = "FLS";
                //if(!string.IsNullOrEmpty(notes))
                sj.Notes = notes;
                sj.OriginalPrice = total_price;
                sj.RevicedPrice = reviced_price;
                sj.UnitConsumption = unitconsuption;
                sj.VisibleToCustomer = true;
                sj.DiscountPercent = discount_percent;
                sj.DiscountPrice = discount_price;
                sj.ModifiedBy = sessionKeys.UID;
                sj.ModifiedDate = modified_date;

                ws.AddServicePriceJournal(sj);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        //grid_delete
        protected void grid_delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnDelete = sender as LinkButton;
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "QuotationItem_Delete",
                     new SqlParameter("@ID", int.Parse(btnDelete.CommandArgument.ToString())));
                try
                {
                    //delete if policy type
                    QuotationBAL.QuotationItem_DeletePlanDiscount(QueryStringValues.CallID, QueryStringValues.OPTION);
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                //rebind the data
                bindGrid();
                Service_Prices();
                bindMaintenacePlan();
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        #region DLT
        private void DLT_check()
        {
            string[] str = PermissionManager.GetFeatures();
            //135,'Service Desk - Service - Add Service Catalogue option'
            //pnl_servicecatalog_add.Visible = Convert.ToBoolean(str[135]);
        }
        #endregion

        #region Vendor

        public void BindVendor()
        {
            IRFIRepository<RFI.Entity.VendorDetails> vRepository = new RFIRepository<RFI.Entity.VendorDetails>();
            var vlist = vRepository.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.VendorID > 0).ToList();
            if (vlist.Count > 0)
            {
                ddlVendors.DataSource = (from v in vlist

                                         orderby v.ContractorName
                                         select new { Name = v.ContractorName, ID = v.VendorID }).ToList();
                ddlVendors.DataTextField = "Name";
                ddlVendors.DataValueField = "ID";
                ddlVendors.DataBind();
            }
            ddlVendors.Items.Insert(0, new ListItem("Please select...", "0"));
        }

        public void BindCategory()
        {
            var categoyList = CategoryBAL.GetCategoryList().ToList();
            ddlCategory.DataSource = categoyList;
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));

            //IPortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category> cRepository = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category>();
            //var vlist = cRepository.GetAll().Where(o => (o.MasterID.HasValue ? o.MasterID.Value : 0) == 0 && o.VendorID == Convert.ToInt32(ddlVendors.SelectedValue)).ToList();
            //if (vlist.Count > 0)
            //{
            //    ddlCategory.DataSource = (from v in vlist
            //                              orderby v.CategoryName
            //                              select new { Name = v.CategoryName, ID = v.ID }).ToList();
            //    ddlCategory.DataTextField = "Name";
            //    ddlCategory.DataValueField = "ID";
            //    ddlCategory.DataBind();
            //}
            //ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        public void BindSubCategory()
        {
            int CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            //DataTable DT_SubCategory;
            //DT_SubCategory = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceSubCategory",
            //            new SqlParameter[] { new SqlParameter("@CategoryId", CategoryId), new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@VendorID", (QueryStringValues.Vendor == null ? 0 : QueryStringValues.Vendor)) }).Tables[0];

            var subCategoyList = SubCategoryBAL.GetSubCategoryList().Where(c => c.CategoryID == CategoryId).ToList();
            ddlSubCategory.DataSource = subCategoyList;
            ddlSubCategory.DataValueField = "ID";
            ddlSubCategory.DataTextField = "Name";
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, new ListItem("Please select...", "0"));
            //IPortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category> cRepository = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category>();
            //var vlist = cRepository.GetAll().Where(o => (o.MasterID.HasValue ? o.MasterID.Value : 0) == Convert.ToInt32(ddlCategory.SelectedValue) && o.VendorID == Convert.ToInt32(ddlVendors.SelectedValue)).ToList();
            //if (vlist.Count > 0)
            //{
            //    ddlSubCategory.DataSource = (from v in vlist
            //                                 orderby v.CategoryName
            //                                 select new { Name = v.CategoryName, ID = v.ID }).ToList();
            //    ddlSubCategory.DataTextField = "Name";
            //    ddlSubCategory.DataValueField = "ID";
            //    ddlSubCategory.DataBind();
            //}
            //ddlSubCategory.Items.Insert(0, new ListItem("Please select...", "0"));
        }

        //public void BindItems()
        //{
        //    IPortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor> cRepository = new PortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor>();
        //    var vlist = cRepository.GetAll().Where(o => (o.Category.HasValue ? o.Category.Value : 0) == Convert.ToInt32(ddlCategory.SelectedValue) && (o.SubCategory.HasValue ? o.SubCategory.Value : 0) == Convert.ToInt32(ddlSubCategory.SelectedValue) && o.Type == Convert.ToInt32(ddlSelect.SelectedValue) && o.VID == Convert.ToInt32(ddlVendors.SelectedValue)).ToList();
        //    if (vlist.Count > 0)
        //    {
        //        ddlService.DataSource = (from v in vlist
        //                                 orderby v.Description
        //                                 select new { Name = v.Description, ID = v.ID }).ToList();
        //        ddlService.DataTextField = "Name";
        //        ddlService.DataValueField = "ID";
        //        ddlService.DataBind();
        //    }
        //    ddlService.Items.Insert(0, new ListItem("Please select...", "0"));
        //}
        #endregion

        //protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindCategory();
        //    BindSubCategory();
        //    BindItems();
        //}

        protected void btnSendforapproval_Click(object sender, EventArgs e)
        {
            try
            {

                //add one entiry for approval
                aRepository = new DCRepository<FixedPriceApproval>();
                if (aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault() == null)
                {
                    var ent = new FixedPriceApproval();
                    ent.CallID = QueryStringValues.CallID;
                    ent.ModifiedDate = DateTime.Now;
                    aRepository.Add(ent);
                }

                arRepository = new DCRepository<FixedPriceThresholdApprover>();
                var alist = arRepository.GetAll().ToList();
                uRepository = new UserRepository<UserMgt.Entity.Contractor>();
                int[] sid = { 1, 2, 3 };
                var ulist = uRepository.GetAll().Where(o => o.Status == "Active" && sid.ToArray().Contains(o.SID.Value)).ToList();

                foreach (var a in alist)
                {
                    try
                    {
                        var uEntity = ulist.Where(p => p.ID == a.UserID).FirstOrDefault();
                        int customerId = sessionKeys.PortfolioID;
                        string fromemailid = Deffinity.systemdefaults.GetFromEmail();
                        WebService ws = new WebService();

                        EmailFooter ef = new EmailFooter();
                        AccessControlEmail ae = DefaultsOfAccessControl.AccessEmail_select(customerId);
                        ef = FooterEmail.EmailFooter_selectByID(6, customerId);
                        // var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        // PortfolioContact pc = ws.GetContactDetails(int.Parse(ddlName.SelectedValue));

                        string subject = sessionKeys.JobDisplayName + " Reference: " + QueryStringValues.CCID + " Fixed Price Approval";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/fixedpriceapprovalmail.html");
                        body = body.Replace("[mail_head]", sessionKeys.JobDisplayName+" Request");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[user]", uEntity.ContractorName);
                        //body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
                        //body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
                        //body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
                        body = body.Replace("[ref]", "" + QueryStringValues.CCID);
                        //body = body.Replace("[linkapproval]", string.Format("<a href='{0}'>click here</a> ", Deffinity.systemdefaults.GetWebUrl() + "/DC/FPApproval.aspx?cid=" + QueryStringValues.CallID + "&rid=" + a.UserID.Value.ToString()));
                        body = body.Replace("[linkapproval]", string.Format("{0}", Deffinity.systemdefaults.GetWebUrl() + "/WF/DC/FPApproval.aspx?cid=" + QueryStringValues.CallID + "&rid=" + a.UserID.Value.ToString()));

                        em.SendingMail(fromemailid, subject, body, uEntity.EmailAddress);
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                lblMsg.Text = "Mail(s) has been sent successfully";
                //pnlOverprice.Visible = false;
                //Service_Prices();
            }
            catch (Exception ex)
            {

                LogExceptions.WriteExceptionLog(ex);
            }

        }

        protected void Grid_services_DataBound1(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Grid_services_DataBound(object sender, EventArgs e)
        {

        }

        protected void Grid_services_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                    TextBox txtDesc = (TextBox)e.Row.FindControl("txtDesc");

                    Label lblstype2 = (Label)e.Row.FindControl("lblstype2");
                    DropDownList ddlSType = (DropDownList)e.Row.FindControl("ddlSType");

                    Label lblServiceID = (Label)e.Row.FindControl("lblServiceID");
                    Label lblFixedRateTypeID = (Label)e.Row.FindControl("lblFixedRateTypeID");
                    try
                    {

                        if (ddlSType != null)
                        {
                            if (Convert.ToInt32(lblServiceID.Text) == 0)
                            {
                                lblDesc.Visible = false;
                                lblstype2.Visible = false;
                                txtDesc.Visible = true;

                                ddlSType.Visible = true;
                                ddlSType.DataSource = GetTypeData();
                                ddlSType.DataTextField = "FixedRateTypeName";
                                ddlSType.DataValueField = "FixedRateTypeID";
                                ddlSType.DataBind();
                                ddlSType.Items.Insert(0, new ListItem("Please select...", "0"));
                                try
                                {
                                    ddlSType.SelectedValue = !string.IsNullOrEmpty(lblFixedRateTypeID.Text) ? lblFixedRateTypeID.Text : "0";
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    try
                    {
                        var list = QuotationBAL.QuotationItem_SelectByOptionid(QueryStringValues.CallID, QueryStringValues.OPTION).ToList();

                        Label lblfooter_unitprice = (Label)e.Row.FindControl("lblfooter_unitprice");
                        Label lblfooter_total = (Label)e.Row.FindControl("lblfooter_total");
                        Label lblfooter_salestotal = (Label)e.Row.FindControl("lblfooter_salestotal");
                        Label lblfooter_vat = (Label)e.Row.FindControl("lblfooter_vat");
                        if (list.Count > 0)
                        {
                            lblfooter_unitprice.Text = string.Format("{0:N2}", list.Sum(o => (o.SellingPrice)));
                            lblfooter_total.Text = string.Format("{0:N2}", list.Sum(o => (o.SellingPrice * o.QTY)));
                            lblfooter_salestotal.Text = string.Format("{0:N2}", list.Sum(o => o.SalesPrice));
                            lblfooter_vat.Text = string.Format("{0:N2}", list.Sum(o => o.VAT));
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }

                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            btnAddItem.Visible = false;
            btnAdditemCancel.Visible = true;
            txtSearch.Visible = true;
            txtSearch.Text = string.Empty;
            ddlItems.Visible = false;
        }

        protected void btnAdditemCancel_Click(object sender, EventArgs e)
        {
            btnAddItem.Visible = true;
            btnAdditemCancel.Visible = false;
            txtSearch.Visible = false;
            txtSearch.Text = string.Empty;
            ddlItems.Visible = true;
            ddlItems.SelectedIndex = 0;

        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }


        private void FLS_SendMailtoRequester(CallInvoice invoice)
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

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid).ToList();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = sessionKeys.JobDisplayName + " ref : " + fls.CCID.ToString();
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoice.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", "<a href='" + Deffinity.systemdefaults.GetWebUrl() + "'>" + Deffinity.systemdefaults.GetWebUrl() + "</a>");

                        body = body.Replace("[noteslist]", GetNotesList(noteslist, stypelist));



                        body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        var sp = noteslist.Select(o => o.SellingPrice).Sum();
                        var vat = 0.00;
                        var s1 = Convert.ToDouble((vat * sp) / Convert.ToDouble(100));

                        body = body.Replace("[gtotal]", string.Format("{0:F2}", sp + s1));
                        body = body.Replace("[discount]", "--------");
                        body = body.Replace("[vat]", string.Format("{0:F2}", vat));
                        body = body.Replace("[sub]", string.Format("{0:F2}", sp));
                        body = body.Replace("[invno]", invoice.ID.ToString());
                        body = body.Replace("[refno]", "" + fls.CCID.ToString());
                        body = body.Replace("[date]", invoice.CreatedDate.Value.ToShortDateString());
                        //[date]



                        string Dis_body = body;
                        bool ismailsent = false;
                        // mail to requester
                        //if help desk or assign users are changed then mail should go to requester
                        body = body.Replace("[user]", pcontact.Name);

                        em.SendingMail(fromemailid, subject, body, pcontact.Email);
                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private string GetNotesList(List<Incident_Service> noteslist, List<FixedRateType> stypelist)
        {
            StringBuilder sbuild = new StringBuilder();
            if (noteslist.Count > 0)
            {
                UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();

                sbuild.Append("<table style='width:100%'>");
                sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                sbuild.Append("<td style='width:50%'>Service</td><td>Type</td><td>Quantity</td><td> Price</td>");
                sbuild.Append("</tr>");
                foreach (var n in noteslist)
                {
                    sbuild.Append("<tr>");
                    sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td>", n.ServiceDescription, stypelist.Where(o => o.FixedRateTypeID == n.ServiceTypeID).FirstOrDefault().FixedRateTypeName, n.QTY, string.Format("{0:F2}", n.SellingPrice)));

                    sbuild.Append("</tr>");
                }
                sbuild.Append("</table>");
            }

            return sbuild.ToString();
        }

        //protected void btnSend_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //add one entiry for approval
        //        aRepository = new DCRepository<FixedPriceApproval>();
        //        var inv = aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
        //        if (inv == null)
        //        {
        //            var ent = new FixedPriceApproval();
        //            ent.CallID = QueryStringValues.CallID;
        //            ent.ModifiedDate = DateTime.Now;
        //            inv.DeniedBy = sessionKeys.UID;
        //            aRepository.Add(ent);
        //        }
        //        else
        //        {
        //            inv.DeniedBy = sessionKeys.UID;
        //            inv.ModifiedDate = DateTime.Now;
        //            aRepository.Edit(inv);
        //        }
        //        IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
        //        var inDetails = inRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();

        //        if (inDetails != null)
        //        {
        //            // lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
        //        }
        //        else
        //        {
        //            inDetails = new CallInvoice();
        //            inDetails.CreatedDate = DateTime.Now;
        //            inDetails.CallID = QueryStringValues.CallID;
        //            inRepository.Add(inDetails);
        //        }
        //        FLS_SendMailtoRequester(inDetails);
        //        lblMsg.Text = "Mail has been sent successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}


        #region popup
        #region "Add Service catalog"

        protected bool CommandField()
        {
            bool vis = true;
            try
            {
                if ((Request.QueryString["Project"] != null))
                {
                    if (sessionKeys.SID != 1)
                    {
                        int role = 0;
                        role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                        if (role == 3)
                        {

                            vis = false;
                            //  Disable();

                        }
                        role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                        if (role == 3)
                        {
                            vis = false;

                            // Disable();

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return vis;

        }
        private void BindPopWindow()
        {
            try
            {
                BaseCache.Cache_Remove("vendorcatalogue");
                //Query_SerchItems
                using (projectTaskDataContext projectDB = new projectTaskDataContext())
                {
                    //var shopItems = (from r in projectDB.ShopItems_vendorDetails
                    //                 orderby r.Type
                    //                 select r).ToList();


                    //if (BaseCache.Cache_Select("vendorcatalogue") == null)
                    //{
                    var shopItems_temp = NewMethod(projectDB);
                    BaseCache.Cache_Insert("vendorcatalogue", shopItems_temp);
                    //}

                    var retvals = (BaseCache.Cache_Select("vendorcatalogue")) as List<ShopItems_vendorDetails>;
                    var shopItems = (from p in retvals
                                     select new { p.BP, p.Category, p.Description, p.Details, p.ID, p.Image, p.PartNumber, p.PortfolioID, p.ServiceType, p.SP, p.SubCategory, p.Type, UnitPrice = Convert.ToDouble(!string.IsNullOrEmpty(p.UnitPrice.Trim()) ? p.UnitPrice.Trim() : "0"), p.VendorName, p.VID }).ToList();

                    if (int.Parse(ddlVendors.SelectedValue) > 0)
                    {


                        shopItems = shopItems.Where(p => p.VID == int.Parse(ddlVendors.SelectedValue)).ToList();

                        if (!string.IsNullOrEmpty(ddlCategory.SelectedValue))
                        {
                            if (int.Parse(ddlCategory.SelectedValue) > 0)
                            {
                                shopItems = shopItems.Where(p => p.Category == int.Parse(ddlCategory.SelectedValue)).ToList();
                                if (!string.IsNullOrEmpty(ddlSubCategory.SelectedValue))
                                {
                                    if (int.Parse(ddlSubCategory.SelectedValue) > 0)
                                    {
                                        shopItems = shopItems.Where(p => p.SubCategory == int.Parse(ddlSubCategory.SelectedValue)).ToList();
                                    }
                                }
                            }
                        }

                    }

                    if (int.Parse(ddlSelect.SelectedValue) > 0)
                    {
                        shopItems = shopItems.Where(p => p.Type == int.Parse(ddlSelect.SelectedValue)).ToList();
                    }

                    if (!string.IsNullOrEmpty(txtItemDescription.Text.Trim()))
                    {
                        shopItems = shopItems.Where(p => p.Description.ToLower().Contains(txtItemDescription.Text.ToLower().Trim())).ToList();
                    }

                    //var shopItems = projectDB.ExecuteQuery<ShopItems_vendorDetails>
                    //    (Query_SerchItems(int.Parse(ddlVendors.SelectedValue), int.Parse(ddlSelect.SelectedValue), txtItemDescription.Text.Trim(), ddlCategory.SelectedValue, ddlSubCategory.SelectedValue)).ToList();

                    if (shopItems != null)
                    {
                        GridView2.DataSource = shopItems.Take(100);
                        GridView2.DataBind();
                    }
                }
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        private static IEnumerable<ProjectMgt.Entity.ShopItems_vendorDetails> NewMethod(projectTaskDataContext projectDB)
        {
            IRFIRepository<RFI.Entity.VendorDetails> vRepository = new RFIRepository<RFI.Entity.VendorDetails>();
            var vlist = vRepository.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.VendorID > 0).ToList();

            //PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

            //var vendorsList = from r in Vendors.v_Vendors
            //                  where vlist.Select(o=>o.VendorID).ToArray().Contains(  r.VendorID)
            //                  orderby r.ContractorName
            //                  select r;
            var shopItems_temp = (from p in projectDB.ShopItems_vendorDetails
                                  where vlist.Select(o => o.VendorID).ToArray().Contains(p.VID.HasValue ? p.VID.Value : 0)
                                  select p).ToList();
            return shopItems_temp;
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        private void BindVendors(DropDownList ddlVendor, string setvalue)
        {


            try
            {
                PurchaseOrderMgtDataContext Vendors = new PurchaseOrderMgtDataContext();

                var vendorsList = from r in Vendors.v_Vendors

                                  orderby r.ContractorName
                                  select r;
                ddlVendor.DataSource = vendorsList;
                ddlVendor.DataValueField = "VendorID";
                ddlVendor.DataTextField = "ContractorName";
                ddlVendor.DataBind();
                ddlVendor.Items.Insert(0, new ListItem("Please select...", "0"));

                //if (ddlVendor.Items.FindByText(setvalue) != null)
                //{
                //    ddlVendor.SelectedValue = ddlVendor.Items.FindByText(setvalue).Value;
                //}
                //else
                //{
                //    ddlVendor.SelectedValue = "0";
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }


        public string GetItemsType(string value)
        {
            string val = "";
            if (value == "1")
            {
                val = "Labour";
            }
            if (value == "2")
            {
                val = "Product";
            }
            if (value == "3")
            {
                val = "Service";
            }
            return val;
        }

        protected void imgUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int worksheetid = 0;
                AddItemsToBOM(worksheetid);

                BindServiceCharge();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        private void AddItemsToBOM(int worksheetid)
        {
            using (projectTaskDataContext InsertBOM = new projectTaskDataContext())
            {
                try
                {

                    //var currencey = (from r in InsertBOM.ProjectDefaults
                    //                 select r).ToList().FirstOrDefault();
                    //int Defaultcurrence = 0;
                    //if (currencey != null)
                    //{
                    //    Defaultcurrence = currencey.DefaultCurrency.Value;
                    //}
                    int items = 0;
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        CheckBox chkRow = (CheckBox)row.FindControl("chkbox");
                        if (chkRow.Checked)
                        {
                            items++;
                            Label lblID = (Label)row.FindControl("lblID");
                            ShopItems_vendorDetails vitems = InsertBOM.ShopItems_vendorDetails.Where(p => p.ID == int.Parse(lblID.Text)).FirstOrDefault();

                            Label lblType = (Label)row.FindControl("lblType");
                            Label lblVendorID = (Label)row.FindControl("lblVendorID");
                            Label lblDescription = (Label)row.FindControl("lblDescription");
                            Label lblSP = (Label)row.FindControl("lblSP");
                            Label lblUnitPrice = (Label)row.FindControl("lblUnitPrice");

                            txtCost.Text = lblSP.Text;
                            txtSearch.Text = lblDescription.Text;

                            hCatelogID.Value = lblID.Text;
                            hImageID.Value = vitems.Image.HasValue ? vitems.Image.Value.ToString() : "00000000-0000-0000-0000-000000000000";
                            ddlstype.SelectedValue = "0";
                            //ProjectMgt.Entity.ProjectBOM add = new ProjectMgt.Entity.ProjectBOM();
                            //add.Description = lblDescription.Text;
                            //add.PartNumber = vitems.PartNumber;
                            //add.ProjectReference = QueryStringValues.Project;
                            //add.Supplier = int.Parse(string.IsNullOrEmpty(lblVendorID.Text) ? "0" : lblVendorID.Text);
                            ////add.Unit = Convert.ToDouble(txtUnitf.Text);
                            //add.Unit = vitems.UnitPrice;
                            //add.WorkSheetID = worksheetid;
                            //add.Qty = 1;
                            //add.Material = vitems.BP;
                            ////add.Material = vitems.SP;
                            //add.Labour = 0;
                            //add.Mics = 0;//Convert.ToDouble(string.IsNullOrEmpty(txtMiscf.Text) ? "0" : txtMiscf.Text);
                            //add.CurrencyID = Defaultcurrence;
                            //add.SellingTotal = vitems.SP;
                            //add.GP = vitems.BP > 0 ? ((vitems.SP - vitems.BP) / vitems.BP) * 100 : 0;
                            //add.Unit = "1";
                            //InsertBOM.ProjectBOMs.InsertOnSubmit(add);
                            //InsertBOM.SubmitChanges();
                            AddNewItems();

                            chkRow.Checked = false;
                        }
                        mdlExnter.Hide();
                    }
                    if (items == 0)
                    {
                        lblError.Text = "Please select item to apply";

                    }
                    else
                    {
                        //lblErr.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Item selectd successfully";
                    }

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }
        protected void imgVendorSearch_Click(object sender, EventArgs e)
        {
            BindPopWindow();

        }
        protected void ddlVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPopWindow();

        }
        private string Query_SerchItems(int VendorID, int Type, string Description, string category, string subcategory)
        {

            string sql = string.Empty;
            sql = "select ID,VID,Type,Description,SP,VendorName,BP,Category,SubCategory from v_ShopItems_vendor where vid!=0";
            if (VendorID != 0)
            {
                sql += " and vid=" + VendorID.ToString();
            }
            if (Type != 0)
            {
                sql += " and Type=" + Type.ToString();
            }
            if (!string.IsNullOrEmpty(Description))
            {
                sql += " and Lower(Description) like '%" + Description.ToLower() + "%'";
            }
            if (!string.IsNullOrEmpty(category))
            {
                sql += " and Category = " + category;
            }
            if (!string.IsNullOrEmpty(subcategory))
            {
                sql += " and SubCategory = " + subcategory;
            }

            sql += " order by Type ";
            return sql;
        }
        #endregion

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

            return "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";
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
        #endregion

        protected void btnPopShow_Click(object sender, EventArgs e)
        {
            hCatelogID.Value = "0";
            BindPopWindow();
            mdlExnter.Show();
        }
        protected void btnRaiseInvoice_Click(object sender, EventArgs e)
        {
            //copy quote items 
            //rise invoice 
            try
            {
                UpdateGrid();
                txtInoicedescription.Text = txtJobDescription.Text;
                mdlRaiseInvoice.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public static void RiseInvoice(int CCID, int CallID, int OPTIONID, string details)
        {
            IDCRespository<QuotationItem> qrep = new DCRepository<QuotationItem>();
            IDCRespository<QuotationPrice> qprep = new DCRepository<QuotationPrice>();
            //get quote items
            var qlist = qrep.GetAll().Where(o => o.CallidID == CallID && o.QuotationOptionID == OPTIONID).ToList();
            //get quote total price
            var qrEntity = qprep.GetAll().Where(o => o.CallID == CallID && o.QuotationOptionID == OPTIONID).FirstOrDefault();

            if (qlist.Count > 0)
            {

                var d = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = CallID, InvoiceDescription = details }, sessionKeys.PortfolioID);
                if (d != null)
                {
                    int invRef = d.ID;


                    IDCRespository<Incident_Service> irep = new DCRepository<Incident_Service>();
                    IDCRespository<Incident_ServicePrice> iprep = new DCRepository<Incident_ServicePrice>();

                    //insert quote items to invoice
                    foreach (var q in qlist)
                    {
                        var i = new Incident_Service();
                        i.BuyingPrice = q.SellingPrice;
                        i.FixedRateTypeID = q.FixedRateTypeID;
                        i.IncidentID = q.CallidID;
                        i.Incident_ServicePriceID = invRef;
                        i.Notes = q.Notes;
                        i.QTY = q.QTY;
                        i.SellingPrice = Convert.ToDouble(q.QTY > 0 ? q.SalesPrice / q.QTY : 0.00);
                        i.ServiceDescription = q.ServiceDescription;
                        i.ServiceID = q.ServiceID;
                        i.ServiceTypeID = q.ServiceTypeID;
                        i.Type = q.Type;
                        i.VAT = q.VAT;

                        irep.Add(i);
                    }

                    if (d != null)
                    {
                        var dEntity = iprep.GetAll().Where(o => o.ID == d.ID).FirstOrDefault();


                        dEntity.DiscountPercent = qrEntity.DiscountPercent;
                        dEntity.DiscountPrice = qrEntity.DiscountPrice;
                        //d.IncidentID = qrEntity.CallID;
                        //d.InvoiceRef = d.InvoiceRef;
                        dEntity.LoggedBy = sessionKeys.UID;
                        dEntity.LoggedDate = DateTime.Now;
                        dEntity.ModifiedDate = DateTime.Now;
                        dEntity.Notes = qrEntity.Notes;
                        dEntity.OriginalPrice = qrEntity.OriginalPrice;
                        dEntity.RevicedPrice = qrEntity.RevicedPrice;
                        dEntity.Type = qrEntity.Type;
                        dEntity.Status = "Pending";
                        iprep.Edit(dEntity);
                    }

                    HttpContext.Current.Response.Redirect(string.Format("~/WF/DC/DCInvoiceList.aspx?CCID={0}&callid={1}&SDID={1}", CCID, CallID), false);

                }
            }
        }
        public static void RiseInvoice(int OPTIONID, string details)
        {
            IDCRespository<QuotationItem> qrep = new DCRepository<QuotationItem>();
            IDCRespository<QuotationPrice> qprep = new DCRepository<QuotationPrice>();
            //get quote items
            var qlist = qrep.GetAll().Where(o => o.CallidID == QueryStringValues.CallID && o.QuotationOptionID == OPTIONID).ToList();
            //get quote total price
            var qrEntity = qprep.GetAll().Where(o => o.CallID == QueryStringValues.CallID && o.QuotationOptionID == OPTIONID).FirstOrDefault();

            if (qlist.Count > 0)
            {

                var d = InvoiceBAL.Incident_ServicePrice_AddInvoiceReference(new Incident_ServicePrice() { IncidentID = QueryStringValues.CallID, InvoiceDescription = details }, sessionKeys.PortfolioID);
                if (d != null)
                {
                    int invRef = d.ID;


                    IDCRespository<Incident_Service> irep = new DCRepository<Incident_Service>();
                    IDCRespository<Incident_ServicePrice> iprep = new DCRepository<Incident_ServicePrice>();

                    //insert quote items to invoice
                    foreach (var q in qlist)
                    {
                        var i = new Incident_Service();
                        i.BuyingPrice = q.SellingPrice;
                        i.FixedRateTypeID = q.FixedRateTypeID;
                        i.IncidentID = q.CallidID;
                        i.Incident_ServicePriceID = invRef;
                        i.Notes = q.Notes;
                        i.QTY = q.QTY;
                        i.SellingPrice = Convert.ToDouble(q.QTY > 0 ? q.SalesPrice / q.QTY : 0.00);
                        i.ServiceDescription = q.ServiceDescription;
                        i.ServiceID = q.ServiceID;
                        i.ServiceTypeID = q.ServiceTypeID;
                        i.Type = q.Type;
                        i.VAT = q.VAT;

                        irep.Add(i);
                    }

                    if (d != null)
                    {
                        var dEntity = iprep.GetAll().Where(o => o.ID == d.ID).FirstOrDefault();


                        dEntity.DiscountPercent = qrEntity.DiscountPercent;
                        dEntity.DiscountPrice = qrEntity.DiscountPrice;
                        //d.IncidentID = qrEntity.CallID;
                        //d.InvoiceRef = d.InvoiceRef;
                        dEntity.LoggedBy = sessionKeys.UID;
                        dEntity.LoggedDate = DateTime.Now;
                        dEntity.ModifiedDate = DateTime.Now;
                        dEntity.Notes = qrEntity.Notes;
                        dEntity.OriginalPrice = qrEntity.OriginalPrice;
                        dEntity.RevicedPrice = qrEntity.RevicedPrice;
                        dEntity.FinalPrice = qrEntity.FinalPrice - (qrEntity.FinalPriceIncludeTax - qrEntity.FinalPrice);
                        dEntity.FinalPriceIncludeTax = qrEntity.FinalPriceIncludeTax;
                        dEntity.Type = qrEntity.Type;
                        dEntity.Status = "Pending";
                        iprep.Edit(dEntity);
                        if ((qrEntity.FinalPriceIncludeTax.HasValue ? qrEntity.FinalPriceIncludeTax.Value : 0) > 0)
                            InvoiceBAL.Incident_ServicePrice_UpdateFinalPrice(qrEntity.CallID, invRef, qrEntity.FinalPriceIncludeTax.Value);
                    }

                    HttpContext.Current.Response.Redirect(string.Format("~/WF/DC/DCInvoiceList.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID), false);

                }
            }
        }

        protected void btnAddMemberShip_Click(object sender, EventArgs e)
        {
            var d = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
            Response.Redirect(string.Format("~/WF/CustomerAdmin/CustomerAddressList.aspx?ContactID={0}", d.RequesterID), false);
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ShowAddPopup();
        }

        private void ShowAddPopup()
        {
            hItemID.Value = "0";
            pnlAddItemVat.Visible = false;
            txtAddItemVat.Text = "0.00";
            hCatelogID.Value = "0";
            hImageID.Value = string.Empty;
            //hCatelogID.Value = "0";
            ClearData();
            mdlAddItem.Show();
        }

        private void ClearData()
        {
            txtSearch.Text = string.Empty;
            txtQty.Text = "1";
            txtCost.Text = "0.00";
            ddlstype.SelectedValue = "0";
            txtItemNotes.Text = string.Empty;
        }

        #region Option changes

        protected void lbtnCloseOptions_Click(object sender, EventArgs e)
        {
            int OptionID = 0;
            var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).ToList();
            if (oList.Count > 0)
                OptionID = oList.FirstOrDefault().ID;
            Response.Redirect(string.Format("~/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}", QueryStringValues.CCID, QueryStringValues.CallID, OptionID), false);
        }

        #endregion

        //#region Options
        private void BindOptionMenu()
        {
            StringBuilder sb = new StringBuilder();

            int OptionID = 0;
            var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).ToList();
            if (oList.Count > 0)
                OptionID = oList.FirstOrDefault().ID;
            if (oList.Count == 0)
            {
                var r = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = QueryStringValues.CallID, CustomerID = sessionKeys.PortfolioID, OptionName = "Option 1", IsActive = false });
                if (r != null)
                    Response.Redirect(string.Format("~/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}", QueryStringValues.CCID, QueryStringValues.CallID, r.ID), false);
            }
            if (QueryStringValues.OPTION == 0)
                Response.Redirect(string.Format("~/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}", QueryStringValues.CCID, QueryStringValues.CallID, OptionID), false);

            else
            {
                sb.Append("<ul class='tabs'>");
                foreach (var o in oList)
                {

                    string url = string.Format("DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}", QueryStringValues.CCID, QueryStringValues.CallID, o.ID);
                    sb.Append(string.Format("<li class='{0}'><a href='{2}'>{1} {3}</a></li>", o.ID == QueryStringValues.OPTION ? "active" : string.Empty, o.ID == QueryStringValues.OPTION ? "<i class='fa fa-circle' style='color: green;'></i>" : string.Empty, url, o.OptionName));

                }
                sb.Append("</ul>");
                lbltext.Text = sb.ToString();
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
                    sbuild.Append("<td style='width:40%'>Item</td><td>Unit Price</td><td>QTY</td><td> VAT</td><td>Total</td>");
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

                    if ((qPrice.FinalPrice.HasValue? qPrice.FinalPrice.Value:0)>0)
                    {
                        var discountpriceIncludetax = string.Format("{0:F2}", (qPrice.FinalPriceIncludeTax.HasValue ? qPrice.FinalPriceIncludeTax.Value : 0));
                        var taxtprice = string.Format("{0:F2}", (qPrice.FinalPriceIncludeTax.HasValue ? qPrice.FinalPriceIncludeTax.Value : 0) - (qPrice.FinalPrice.HasValue ? qPrice.FinalPrice.Value : 0));
                        sbuild.Append(string.Format("<tr><td><b> Your price: </b></td><td>{0} Price includes VAT ({1})</td></tr>", discountpriceIncludetax, taxtprice));
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
        private void FLS_OptionalMailtoRequester()
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

                        var noteslist = dc.QuotationItems.Where(c => c.CallidID == callid).ToList();
                        var qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid).ToList();
                        var stypelist = dc.QuotationOptions.Where(c => c.CallID == callid).ToList();
                        var policylist = pd.ProductPolicyTypes.Where(o => o.CustomerID == cdetails.CompanyID).ToList();
                        var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Quotation";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSQuotation.htm");

                        body = body.Replace("[mail_head]", sessionKeys.JobDisplayName +" Quotation");
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
                        var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID);
                        string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", sessionKeys.IncidentID));
                        if (File.Exists(pname))
                        {
                            var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                            Email ToEmail = new Email();
                            Attachment attachment1 = new Attachment(pname);
                            attachment1.Name = q.CurrentTemplateName + ".pdf";

                            ToEmail.SendingMail(pcontact.Email, subject, body, fromemailid, attachment1);
                        }
                        else
                        {
                            em.SendingMail(fromemailid, subject, body, pcontact.Email);
                        }
                        ismailsent = true;


                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string getButton(string url, string name)
        {
            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#ED7D31'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #ED7D31; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
        }

        private string getGreenButton(string url, string name)
        {
            //var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style='border-radius:2px;' bgcolor='#63b026'><a href = '{0}' target = '_blank' style='padding: 8px 12px; border: 1px solid #63b026;border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px; color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);

            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' ><a href = '{0}' target = '_blank' >{1}</a></td ></tr></table></td></tr ></table>", url, "<img src='" + Deffinity.systemdefaults.GetWebUrl() + "/Content/images/ContactMe.png' style='border:0px' />");
            return v;
        }
       
        protected void btnAcceptOption_Click(object sender, EventArgs e)
        {
            try
            {
                if (QueryStringValues.OPTION > 0)
                {
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var cEntity = dc.CallDetails.Where(o => o.ID == QueryStringValues.CallID).FirstOrDefault();
                        var dcLIst = dc.QuotationPrices.Where(o => o.CallID == QueryStringValues.CallID && o.IsOptionActive == true).ToList();
                        if (dcLIst.Count() == 0)
                        {
                            var op = dc.QuotationPrices.Where(o => o.QuotationOptionID == QueryStringValues.OPTION).FirstOrDefault();
                            if (op != null)
                            {
                                //quote accepted
                                cEntity.StatusID = 50;

                                op.IsOptionActive = true;

                                dc.SubmitChanges();
                                pnlservice.Visible = false;
                                lblservice.Text = "Estimate has been accepted";
                                lblservice.Font.Bold = true;
                                //pnlAccept.Visible = false;
                                pnlAccept.Visible = false;
                            }

                        }
                        else
                        {
                            lblMsg.Text = "Estimate already accepted. Please check again";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string GetPolicyData(int AssignedPolicyID, List<PortfolioMgt.Entity.ProductPolicyType> pd, double total, double QTY)
        {
            string retval = string.Empty;
            retval = string.Format("<div class='form-group'><div class='col-md-2 price_lable'> {0}</div><div class='col-md-2 price_lable'> {1}</div> <div class='col-md-2 price_lable'> {2}</div><div class='col-md-2 price_lable' style='color:#7c38bc;'> {3}</div></div>", "QTY", "Standard price", "Your price", "Member savings"); ;
            //2 is default
            if (AssignedPolicyID == 2)
            {
                foreach (var p in pd)
                {
                    var d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;
                    var dtotal = d > 0 ? total - (total * (d / 100)) : total;
                    retval = retval + string.Format("<div class='form-group'><div class='col-md-2 price_text' style='text-align:right;'> {0}</div><div class='col-md-2 price_text' style='text-align:right;'> {1}</div> <div class='col-md-2 price_text' style='text-align:right;'> {2}</div><div class='col-md-2 price_text' style='color:#7c38bc;text-align:right;'> {3}</div></div>", QTY, total, string.Format("{0:F2}", dtotal), string.Format("{0:F2}", total - dtotal));
                }
            }
            else
            {
                foreach (var p in pd.Where(o => o.ID == AssignedPolicyID).ToList())
                {
                    var d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;
                    var dtotal = d > 0 ? total - (total * (d / 100)) : total;
                    retval = retval + string.Format("<div class='form-group'><div class='col-md-2 price_text' style='text-align:right;'> {0}</div><div class='col-md-2 price_text' style='text-align:right;'> {1}</div> <div class='col-md-2 price_text' style='text-align:right;'> {2}</div><div class='col-md-2 price_text' style='color:#7c38bc;text-align:right;'> {3}</div></div>", QTY, total, string.Format("{0:F2}", dtotal), string.Format("{0:F2}", total - dtotal));
                }
            }
            return retval;
        }
        private void BindCustomFields()
        {
            try
            {
                var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o => o.OptionName).ToList();

                var qList = QuotationBAL.QuotationItem_SelectByCallid(QueryStringValues.CallID).Where(o => o.QuotationOptionID == QueryStringValues.OPTION).ToList();
                var pList = QuotationBAL.QuotationPrice_selectAll(QueryStringValues.CallID).Where(o => o.QuotationOptionID == QueryStringValues.OPTION);
                var policyList = PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select(sessionKeys.PortfolioID);
                var policyDetails = policyList.ToList();
                //var discountPercent = policyDetails.DiscountPercent.HasValue ? policyDetails.DiscountPercent.Value : 0.00;
                var paBal = new PortfolioMgt.BAL.PortfolioContactBAL();
                var CallDetails = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                var addressID = 0;// CallDetails.ContactAddressID;
                //var addressDetails = paBal.v_PortfolioContactAddress_SelectByID(addressID).FirstOrDefault();
                //var selectedpolicyID = addressDetails.PolicyTypeID.HasValue ? addressDetails.PolicyTypeID.Value : 0;
                IPortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material> mRep = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material>();
                var mList = mRep.GetAll().Where(o => o.PortfolioID == sessionKeys.PortfolioID).ToList();
                if (qList.Count > 0)
                {

                    var rslit = (from o in qList
                                 select new
                                 {
                                     Description = o.ServiceDescription,
                                     o.CallidID,
                                     o.FixedRateTypeID,
                                     o.ID,
                                     o.Notes,
                                     o.QTY,
                                     o.QuotationOptionID,
                                     o.SellingPrice,
                                     o.ServiceID,
                                     o.ServiceTypeID,
                                     o.Type,
                                     o.VAT,
                                     Image = o.Image.HasValue ? o.Image.Value : new Guid("00000000-0000-0000-0000-000000000000"),
                                     Total = (o.QTY * o.SellingPrice) + (o.VAT.HasValue ? o.VAT.Value : 0),
                                     mdata = GetPolicyData(0, policyList, (o.QTY.Value * o.SellingPrice) + (o.VAT.HasValue ? o.VAT.Value : 0), o.QTY.Value)
                                 }).ToList();
                    list_Customfields.DataSource = rslit;
                    list_Customfields.DataBind();

                    if (rslit.Count > 0)
                    {
                        double memberprice = 0;
                        double price = rslit.Sum(o => o.SellingPrice * o.QTY.Value);
                        double d = 0;
                        //var p = policyList.Where(o => o.ID == selectedpolicyID).FirstOrDefault();
                        //if (p != null)
                        //    d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;

                        memberprice = d > 0 ? price - (price * (d / 100)) : price;

                        double vat = rslit.Sum(o => o.VAT.HasValue ? o.VAT.Value : 0);

                        blblMemberPrice.InnerText = string.Format("{0:F2}", memberprice);
                        blblSubTotal.InnerText = string.Format("{0:F2}", memberprice);
                        blblTax.InnerText = string.Format("{0:F2}", vat);
                        blblTotal.InnerText = string.Format("{0:F2}", memberprice);
                        blblYourSavings.InnerText = string.Format("{0:F2}", price - memberprice);
                        blbl_StandardPrice.InnerText = string.Format("{0:F2}", price);
                    }

                }

                //if (oList.Count > 0)
                //{
                //    var dList = (from o in oList
                //                 select new
                //                 {
                //                     o.CallID,
                //                     o.ID,
                //                     o.IsActive,
                //                     o.LoggedDate,
                //                     o.ModifiedDate,
                //                     o.OptionName,
                //                     ItemsCount = qList.Where(p => p.QuotationOptionID == o.ID).Count(),
                //                     Price = (pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault().OriginalPrice : 0.00),
                //                     IsAplied = pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault().IsOptionActive : false
                //                 }).ToList();
                //    var rLIst = (from o in dList
                //                 select new
                //                 {
                //                     o.CallID,
                //                     o.ID,
                //                     o.IsActive,
                //                     o.LoggedDate,
                //                     o.ModifiedDate,
                //                     o.OptionName,
                //                     o.Price,
                //                     o.IsAplied,
                //                     o.ItemsCount,
                //                     ItemsCountName = o.ItemsCount.ToString() + " Items",
                //                     MemberCost = string.Format("{0:F2}", (discountPercent > 0) ? o.Price - (o.Price * (discountPercent / 100)) : o.Price),
                //                     Savings = string.Format("{0:F2}", (discountPercent > 0) ? (o.Price * (discountPercent / 100)) : 0.00),
                //                 }).ToList();
                //    list_Customfields.DataSource = rLIst;
                //    list_Customfields.DataBind();
                //}
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
                    Response.Redirect(string.Format("/WF/DC/DCQuotation.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
                }
                else if (e.CommandName == "cEdit")
                {
                    hItemID.Value = e.CommandArgument.ToString();
                    try
                    {
                        var qEntity = QuotationBAL.QuotationItem_SelectByCallid(QueryStringValues.CallID).Where(o => o.ID == Convert.ToInt32(hItemID.Value)).FirstOrDefault();
                        if (qEntity != null)
                        {
                            txtSearch.Text = qEntity.ServiceDescription;
                            ddlstype.SelectedValue = qEntity.FixedRateTypeID.HasValue ? qEntity.FixedRateTypeID.Value.ToString() : "0";
                            txtCost.Text = string.Format("{0:F2}", qEntity.SellingPrice.ToString());
                            txtQty.Text = qEntity.QTY.ToString();
                            txtItemNotes.Text = qEntity.Notes;
                            pnlAddItemVat.Visible = true;
                            txtAddItemVat.Text = string.Format("{0:F2}", qEntity.VAT.ToString());
                        }


                        mdlAddItem.Show();
                        lblItemAddHeader.Text = "Edit Item";
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }


                }
                else if (e.CommandName == "cDelete")
                {
                    try
                    {
                        var ID = e.CommandArgument.ToString();
                        QuotationBAL.QuotationItem_Delete(Convert.ToInt32(ID));
                        lblMsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindCustomFields();
                        Service_Prices();
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
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


        private void bindMaintenacePlan()
        {
            try
            {
                IDCRespository<QuotationItem> qiRep = new DCRepository<QuotationItem>();
                var discountExists = qiRep.GetAll().Where(o => o.CallidID == QueryStringValues.CallID && o.QuotationOptionID.Value == QueryStringValues.OPTION && o.PolicyNotes.ToLower().Contains(QuotationBAL.policy)).ToList();

                if (discountExists.Count == 0)
                {
                    var c = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                    IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer>();
                    // quote price
                    IDCRespository<QuotationPrice> qRep = new DCRepository<QuotationPrice>();
                    var priceItem = qRep.GetAll().Where(o => o.CallID == QueryStringValues.CallID && o.QuotationOptionID == QueryStringValues.OPTION).FirstOrDefault();
                    double quotePrice = priceItem.OriginalPrice;
                    var pList = prRep.GetAll().Where(o => o.OptionID == QueryStringValues.OPTION).ToList();
                    var mList = PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select(sessionKeys.PortfolioID).ToList();
                    // PortfolioMgt.BAL.ProductPolicyTypeBAL.ProductPolicyType_Select
                    if (pList.Count == 0)
                    {
                        foreach (var m in mList)
                        {
                            prRep.Add(new PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer()
                            {
                                CustomerID = c.RequesterID,

                                DiscountPrice = m.DiscountPercent.HasValue ? m.DiscountPercent.Value : 0,
                                Price = quotePrice,// m.Yearly.HasValue ? m.Yearly.Value : 0,
                                OptionID = QueryStringValues.OPTION,
                                PolicyTypeID = m.ID
                            });
                        }

                        pList = prRep.GetAll().Where(o => o.OptionID == QueryStringValues.OPTION).ToList();
                    }
                    if (pList.Count > 0)
                    {

                        var r = (from m in mList
                                 select new
                                 {
                                     m.ID,
                                     m.Title,
                                     Price = quotePrice - GetDiscount(quotePrice, m.DiscountPercent),
                                     DiscountPrice = GetDiscount(quotePrice, m.DiscountPercent),
                                     AnnualPrice = m.Yearly.HasValue ? m.Yearly.Value : 0.00
                                 }).ToList();
                        //var r = (from p in pList
                        //         join m in mList on p.PolicyTypeID equals m.ID
                        //         select new
                        //         {
                        //             ID = p.ID,
                        //             Title = m.Title,
                        //             Price = p.Price - (p.DiscountPrice >0? Math.Round(((p.DiscountPrice / p.Price) * 100),2) :0),
                        //             DiscountPrice = (p.DiscountPrice > 0 ? Math.Round(((p.DiscountPrice / p.Price) * 100), 2) : 0)

                        //         }).ToList();
                        GridOffers.DataSource = r;
                        GridOffers.DataBind();
                        //pnlManageOptions.Visible = true;
                    }
                    else
                    {
                        //pnlManageOptions.Visible = false;

                        // pnlOffer
                    }
                }
                else
                {
                    pnlOffer.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private double GetDiscount(double? price, double? discountpercent)
        {
            double retval = 0;

            var dcnt = discountpercent.HasValue ? discountpercent.Value : 0;


            retval = Math.Round(Convert.ToDouble(dcnt > 0 ? price * (dcnt / 100) : 0), 2);

            return retval;
        }
        protected void GridOffers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    int i = GridOffers.EditIndex;
                    GridViewRow Row = GridOffers.Rows[i];
                    var ID = int.Parse(e.CommandArgument.ToString());
                    var DiscountPrice = double.Parse(((TextBox)Row.FindControl("txtDiscountPrice")).Text);
                    IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer>();
                    IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ptRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();

                    var pEntity = prRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                    if (pEntity != null)
                    {
                        pEntity.DiscountPrice = DiscountPrice;
                        prRep.Edit(pEntity);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                        //bindMaintenacePlan();
                    }


                }
                else if (e.CommandName == "apply")
                {
                    try
                    {
                        GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                        var ID = int.Parse(e.CommandArgument.ToString());
                        GridViewRow Row = GridOffers.Rows[gvr.RowIndex];
                        //GridViewRow Row = GridOffers.Rows[i];
                        GridView innerGrid = (GridView)(Row.FindControl("gridInner"));
                        List<int> ids = new List<int>();
                        try
                        {
                            foreach (GridViewRow row in innerGrid.Rows)
                            {
                                bool isMailable = Convert.ToBoolean(((CheckBox)row.FindControl("chkMailable")).Checked);

                                if (isMailable)
                                {
                                    //litID
                                    string litIDval = ((Label)row.FindControl("litID")).Text;
                                    //get the addes its
                                    ids.Add(Convert.ToInt32(litIDval));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);

                        }


                        UpdatePriceByDiscount(ID, QueryStringValues.CallID, QueryStringValues.OPTION, ids);
                        lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else if (e.CommandName == "contact")
                {
                    try
                    {
                        var ID = int.Parse(e.CommandArgument.ToString());
                        IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer>();
                        IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ptRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();

                        var pEntity = prRep.GetAll().Where(o => o.ID == ID).FirstOrDefault();
                        var plEntity = ptRep.GetAll().Where(o => o.ID == pEntity.PolicyTypeID).FirstOrDefault();
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
                                var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                                //var subject = dc
                                //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                                string subject = sessionKeys.JobDisplayName +" ref : " + fls.CCID.ToString();
                                Emailer em = new Emailer();
                                string body = em.ReadFile("~/WF/DC/EmailTemplates/PlanOffer.htm");

                                body = body.Replace("[mail_head]", "Invoice");
                                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                                body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                                body = body.Replace("[CustomerName]", fls.RequesterName);

                                body = body.Replace("[MaintenancePlanName]", plEntity.Title);
                                body = body.Replace("[Customermobilenumber]", fls.RequestersTelephoneNo);
                                body = body.Replace("[Customeremailaddress]", fls.RequestersEmailAddress);

                                body = body.Replace("[Address]", fls.RequestersAddress + " ," + fls.RequestersCity + " ," + fls.RequestersTown + " , " + fls.RequestersPostCode + " , " + fls.RequestersTelephoneNo);

                                //[date]



                                string Dis_body = body;
                                bool ismailsent = false;
                                // mail to requester
                                //if help desk or assign users are changed then mail should go to requester
                                //body = body.Replace("[user]", pcontact.Name);
                                em.SendingMail(fromemailid, subject, body, "indra@deffinity.com");
                                em.SendingMail(fromemailid, subject, body, "");
                                ismailsent = true;


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void UpdatePriceByDiscount(int PolicyID, int CallID, int OptionID, List<int> addonids)
        {
            try
            {
                PolicyTypeBAL pb = new PolicyTypeBAL();
                var p = pb.PolicyType_SelectBYID(PolicyID);


                var pricelist = QuotationBAL.QuotationPrice_selectByOptionID(CallID, OptionID);
                var addonlist = PortfolioMgt.BAL.ProductAddonPriceBAL.ProductAddonPrice_selectByPolicyType(PolicyID);
                var pentity = pricelist.FirstOrDefault();
                var Discount = GetDiscount(pentity.OriginalPrice, p.DiscountPercent);
                string ItemImg = "00000000-0000-0000-0000-000000000000";
                Guid _guid = new Guid(ItemImg);
                // add policy price
                Insertservice(0, QueryStringValues.CallID, 1, 0, "FLS", p.Title + " - Plan " + "(MP-0000-" + PolicyID.ToString() + ")", 0, p.Yearly.HasValue ? p.Yearly.Value : 0.00, 0, "", _guid.ToString(), p.Yearly.HasValue ? p.Yearly.Value : 0.00, 0, 0, false, p.ID, QuotationBAL.policy);

                //add addon ids
                foreach (var addonid in addonids)
                {
                    var addontitle = addonlist.Where(o => o.PAPID == addonid).FirstOrDefault();
                    Insertservice(0, QueryStringValues.CallID, 1, 0, "FLS", addontitle.AddOnDetails + " - Plan add-on" + "(AD-0000-" + addonid.ToString() + ")", 0, addontitle.YearlyCost, 0, "", _guid.ToString(), addontitle.YearlyCost, 0, 0, false, p.ID, QuotationBAL.policy);
                }
                //add discount item
                Insertservice(0, QueryStringValues.CallID, 1, 0, "FLS", p.Title + " - Discount", 0, Discount * -1, 0, "", _guid.ToString(), Discount * -1, 0, 0, false, p.ID, QuotationBAL.policydisount);

                //QuotationBAL.QuotationPrice_AddUpdate(QueryStringValues.CallID, pentity.OriginalPrice, pentity.OriginalPrice - Discount,
                //    p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0, Discount, OptionID);
                //update price
                Service_Prices();

                bindGrid();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        protected void GridOffers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridOffers.EditIndex = -1;
            bindMaintenacePlan();
        }
        protected void GridOffers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridOffers.EditIndex = -1;
            bindMaintenacePlan();
        }
        protected void GridOffers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridOffers.EditIndex = e.NewEditIndex;
            bindMaintenacePlan();
        }

        protected void GridOffers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception == null)
            {
                e.ExceptionHandled = true;
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            try
            {
                UpdateGrid();
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void UpdateGrid()
        {
            int countRow = Grid_services.Rows.Count;
            bool dateError = false;
            bool qtyError = false;
            for (int i = 0; i < countRow; i++)
            {
                try
                {
                    GridViewRow row = Grid_services.Rows[i];
                    Label lblID = (Label)row.FindControl("lblID");
                    int ID = Convert.ToInt32(lblID.Text);

                    TextBox txtSellingPrice = (TextBox)row.FindControl("txtSellingPrice");
                    double SellingPrice = Convert.ToDouble(!string.IsNullOrEmpty(txtSellingPrice.Text.Trim()) ? txtSellingPrice.Text.Trim() : "0.00");
                    TextBox txtQty = (TextBox)row.FindControl("txtQty");
                    double QTY = Convert.ToDouble(!string.IsNullOrEmpty(txtQty.Text.Trim()) ? txtQty.Text.Trim() : "0.00");
                    TextBox txtVAT = (TextBox)row.FindControl("txtVAT");
                    TextBox txtVATRate = (TextBox)row.FindControl("txtVATRate");
                    //txtSalesPrice 
                    TextBox txtSalesPrice = (TextBox)row.FindControl("txtSalesPrice");
                    double SalesPrice = Convert.ToDouble(!string.IsNullOrEmpty(txtSalesPrice.Text.Trim()) ? txtSalesPrice.Text.Trim() : "0.00");
                    TextBox txtMarkup = (TextBox)row.FindControl("txtMarkup");
                    double Markup = Convert.ToDouble(!string.IsNullOrEmpty(txtMarkup.Text.Trim()) ? txtMarkup.Text.Trim() : "0.00");

                    double VAT = Convert.ToDouble(!string.IsNullOrEmpty(txtVAT.Text.Trim()) ? txtVAT.Text.Trim() : "0.00");
                    double VATRate = Convert.ToDouble(!string.IsNullOrEmpty(txtVATRate.Text.Trim()) ? txtVATRate.Text.Trim() : "0.00");
                    TextBox txtEnotes = (TextBox)row.FindControl("txtEnotes");
                    string Notes = txtEnotes.Text.Trim();
                    Label lblDescription = (Label)row.FindControl("lblDescription");
                    int servicetype = 0;
                    string desc = string.Empty;
                    //check qty or amount is updated
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var applyVAT = true;
                        var qitem = dc.QuotationItems.Where(o => o.ID == ID).FirstOrDefault();
                        if (qitem != null)
                        {
                            desc = qitem.ServiceDescription;
                            servicetype = qitem.ServiceTypeID.HasValue ? qitem.ServiceTypeID.Value : 0;
                            //if selling price


                            //if ((qitem.QTY != QTY) )
                            //{

                            //    var vt = 0.00;
                            //    if (applyVAT)
                            //    {
                            //        vt = VATByCustomerBAL.VATByCustomer_select();
                            //    }
                            //    //var v = (SalesPrice);
                            //    if (vt > 0)
                            //        VAT = (SalesPrice) * (vt / 100);
                            //    //else
                            //    //    v = 0.00;


                            //}

                            if ((qitem.QTY != QTY) || (qitem.SalesPrice != SalesPrice) || (qitem.SellingPrice != SellingPrice) || (qitem.Markup) != Markup || (qitem.VATRate) != VATRate)
                            {
                                try
                                {
                                    //var margin = PortfolioMgt.BAL.PortolioMarginBAL.PortolioMarginBAL_SelectMargin();
                                    //var sales = 0.00;
                                    if (Markup > 0)
                                        SalesPrice = ((SellingPrice * QTY) + ((SellingPrice * QTY) * (Markup / 100)));
                                    else
                                        SalesPrice = (SellingPrice * QTY);

                                    if (VATRate > 0)
                                        VAT = (SalesPrice) * (VATRate / 100);
                                    else
                                        VAT = 0.00;
                                }
                                catch (Exception ex)
                                {
                                    LogExceptions.WriteExceptionLog(ex);
                                }
                            }




                        }
                    }
                    //update
                    double units = 0.00;
                    ServiceManager.Quotation_Update(ID, QTY, SellingPrice, units, Notes, desc, servicetype, VAT, new Guid(), SalesPrice, Markup, VATRate);

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }


            }
            
          IncidentService_Price.QuotationPrice_Update(QueryStringValues.CallID, Convert.ToDouble(!string.IsNullOrEmpty(txtDiscountPercent.Text.Trim()) ? txtDiscountPercent.Text.Trim() : "0"), txtNotes.Text.Trim(), Type, QueryStringValues.OPTION);

            QuotationBAL.QuotationPrice_UpdateFinalPrice(QueryStringValues.CallID, QueryStringValues.OPTION, Convert.ToDouble(!string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? txtDiscount.Text.Trim() : "0"));

            lblMsg.Text = "Updated successfully";
            Service_Prices();
            bindGrid();
        }

        protected void btnSubmitRaiseinvoice_Click(object sender, EventArgs e)
        {
            try
            {
                RiseInvoice(QueryStringValues.OPTION, txtInoicedescription.Text.Trim());
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
        public void btnSubmitToCustomer_Click1(object sender, EventArgs e)
        {
            try
            {
                UpdateGrid();
                List<int> SelectedID = new List<int>();
                SelectedID.Add(QueryStringValues.OPTION);
                //foreach (ListViewDataItem item in list_Customfields.Items)
                //{
                //    CheckBox chk = item.FindControl("chkRecommand") as CheckBox;
                //    var lblID = item.FindControl("lblID") as Label;
                //    if (chk != null && lblID != null)
                //    {
                //        if (chk.Checked)
                //        {
                //            SelectedID.Add(Convert.ToInt32(lblID.Text));
                //        }
                //    }
                //}

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
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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
                        if (optionID.Count > 0)
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

                        body = body.Replace("[mail_head]", sessionKeys.JobDisplayName +" Quotation");
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
                        //txthtml1.Text = body;
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

        public void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                BindContacts();
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

                    //InvoiceBAL.SendMailToCustomer(Convert.ToInt32(hpriceid.Value), tlist);
                    //BindGrid();
                    //  lblMsg.Text = "Mail sent successfully";
                    mdlContacts.Show();
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

        private void SendQuoteMail(List<ToEmailCalss> tlist)
        {
            using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioMgt.DAL.PortfolioDataContext())
            {
                using (DCDataContext dc = new DCDataContext())
                {
                    var fls = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                    var cdetails = dc.CallDetails.Where(c => c.ID == QueryStringValues.CallID).FirstOrDefault();
                    var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                    string body = CKEditor1.Text;
                   // CKEditor1.CustomConfig.
                    //if (!body.Contains("!DOCTYPE HTML PUBLIC"))
                    //{
                    //    Emailer em = new Emailer();
                    //    string html_body = em.ReadFile("~/WF/DC/EmailTemplates/mastertemplate.html");

                    //    html_body = html_body.Replace("[table]", body);
                    //    body = html_body;
                    //}
                    string fromid = Deffinity.systemdefaults.GetFromEmail();
                    string toid = "";
                    string subject = "Here's Your Quotation";
                    var templatePath = string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", QueryStringValues.CallID);
                    string pname = HttpContext.Current.Server.MapPath(string.Format("~/WF/UploadData/JobQuoteTemplate/{0}/Template.pdf", QueryStringValues.CallID));

                    Email er = new Email();
                    List<System.Net.Mail.Attachment> a = new List<System.Net.Mail.Attachment>();
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString() + "-OPTION" + QueryStringValues.OPTION.ToString())))
                    {
                        string[] S1 = Directory.GetFiles(Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString() + "-OPTION" + QueryStringValues.OPTION.ToString()));
                        foreach (string fileName in S1)
                        {
                            a.Add(new System.Net.Mail.Attachment(fileName));
                        }
                    }
                    if (File.Exists(pname))
                    {
                        var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                        Email ToEmail = new Email();
                        Attachment attachment1 = new Attachment(pname);
                        attachment1.Name = q.CurrentTemplateName + ".pdf";
                        if (tlist.Count > 0)
                        {
                            foreach (var t in tlist)
                            {
                                // ToEmail.SendingMail(t.email, subject, body, fromid, attachment1);
                                a.Add(attachment1);
                                er.SendingMail(t.email, subject, body, fromid, a);
                            }
                        }
                        else
                        {
                            a.Add(attachment1);
                            er.SendingMail(pcontact.Email, subject, body, fromid, a);
                        }
                       // ToEmail.SendingMail(pcontact.Email, subject, body, fromid, attachment1);
                    }
                    else
                    {
                        Emailer em = new Emailer();

                        if (tlist.Count > 0)
                        {
                            foreach (var t in tlist)
                            {
                               // em.SendingMail(fromid, subject, body,t.email);
                                er.SendingMail(t.email, subject, body, fromid, a);
                            }
                        }
                        else
                            er.SendingMail(pcontact.Email, subject, body, fromid, a);
                        //em.SendingMail(fromid, subject, body, pcontact.Email);


                    }
                    //update quote status
                    FLSDetailsBAL.UpdateTicketStatus(QueryStringValues.CallID, sessionKeys.UID, JobStatus.Quote_Sent);
                    //sessionKeys.Message = "Quotation has been sent to the client";
                    //Response.Redirect(Request.RawUrl, false);
                    pnlservice.Visible = false;
                    lblservice.Text = "Quotation has been sent to the client";
                    lblservice.Font.Bold = true;
                }
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
                    //InvoiceBAL.SendMailToCustomer(Convert.ToInt32(hpriceid.Value), tlist);
                    //BindGrid();
                  //  lblMsg.Text = "Mail sent successfully";
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void BindContacts()
        {
            try
            {
                var jEntity = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
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

        //btnSubmitOptions_Click
        protected void gridfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "Download")
            {
                try
                {
                    GridViewRow gvrow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    // string contenttype = gridfiles.DataKeys[gvrow.RowIndex].Values[1].ToString();
                    //string filename = gridfiles.DataKeys[gvrow.RowIndex].Values[2].ToString();
                    //string[] ex = filename.Split('.');
                    //string ext = ex[ex.Length - 1];
                    //"~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString(
                    string filepath = string.Format("~/WF/UploadData/DC/{0}/", QueryStringValues.CallID.ToString() + "-" + QueryStringValues.IVREF.ToString(), e.CommandArgument.ToString());
                    //Response.ContentType = contenttype;
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + e.CommandArgument.ToString() + "\"");
                    Context.Response.ContentType = "octet/stream";
                    Response.TransmitFile(filepath);
                    Response.End();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

        }
        public void Gridfilesbind()
        {
            try
            {
                var folderpath = Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString() + "-OPTION" + QueryStringValues.OPTION.ToString());
                if (System.IO.Directory.Exists(folderpath))
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString() + "-OPTION" + QueryStringValues.OPTION.ToString()));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new ListItem(System.IO.Path.GetFileName(filePath), filePath));
                    }

                    gridfiles.DataSource = files;
                    gridfiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                File.Delete(filePath);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void ClosePopup(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
    }



}