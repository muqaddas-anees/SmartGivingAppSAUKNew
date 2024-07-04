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

namespace DeffinityAppDev.WF.DC.controls
{
    public partial class DCQuotationCtrl : System.Web.UI.UserControl
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
            if(Request.QueryString["tab"] ==  null)
            {
                Response.Redirect(Request.RawUrl + "&Option=0&tab=quote", false);
            }
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

                    
                    //BindOptionMenu();
                    BindOptions();
                    //BindCustomFields();
                    //bindMaintenacePlan();
                    //DataTable dt = this.BindMenuData(0);
                    //DynamicMenuControlPopulation(dt, 0, null);
                    //check DLT
                    //DLT_check();
                    BindVendoritems();
                    BasicBind();
                    BindPopWindow();
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
                    BindTitle();
                    bindTemplatedata();
                    setAppliedTemplate(QueryStringValues.CallID);

                    OptionsAcceptMsg();

                    if (QueryStringValues.Type == "mail")
                    {
                        pnlAccept.Visible = true;
                        pnlAddOptonsButton.Visible = false;
                        pnlTemplateSection.Visible = false;
                        pnlTemplateDisplay.Visible = false;
                        pnlAdd.Visible = false;
                        btnUpdateTotals.Visible = false;
                        pnlOffer.Visible = false;
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
                        pnlOffer.Visible = true;
                    }


                    BindUrlToTabs();
                    ChangeViewByType();
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
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
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void setAppliedTemplate(int callid)
        {
            try
            {
                var q = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                if (q != null)
                {
                    ddlTitle.SelectedValue = q.BaseTemplateID.ToString();
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindTitle()
        {
            var qlist = QuoteTemplateBAL.QuoteTemplateSelectAll();
            if (qlist.Count > 0)
            {
                ddlTitle.DataSource = qlist.ToList().OrderBy(o => o.Title);
                ddlTitle.DataTextField = "Title";
                ddlTitle.DataValueField = "ID";
                ddlTitle.DataBind();


            }
            ddlTitle.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        private void BindVendoritems()
        {
            try
            {
                WebService ds = new WebService();
                ddlItems.DataSource = ds.GetVendorItems().OrderBy(o => o.Description).ToList();
                ddlItems.DataTextField = "Description";
                ddlItems.DataValueField = "ID";
                ddlItems.DataBind();
                ddlItems.Items.Insert(0, new ListItem("Please select...", "0"));
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
                SqlDataReader dr = IncidentService_Price.Quotation_Price_Select(QueryStringValues.CallID, Type,QueryStringValues.OPTION);
                while (dr.Read())
                {
                    lblTotalPrice.InnerText = string.Format("{0:f2}", dr["OriginalPrice"]);
                    lblDiscountValue.InnerText = string.Format("{0:f2}", dr["DiscountPrice"]);
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

                        int ID, QTY;
                        double SellingPrice;
                        double VAT;
                        double units = 0;
                        string Notes;
                        int servicetype = 0;
                        string desc = string.Empty;
                        double SalesPrice = 0;
                        int i = Grid_services.EditIndex;
                        GridViewRow Row = Grid_services.Rows[i];
                        ID = int.Parse(e.CommandArgument.ToString());
                        QTY = int.Parse(((TextBox)Row.FindControl("txtQty")).Text);
                        SellingPrice = double.Parse(((TextBox)Row.FindControl("txtSellingPrice")).Text);
                         
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

                        //check qty or amount is updated
                        using (DCDataContext dc = new DCDataContext())
                        {
                            var applyVAT = true;
                            var qitem = dc.QuotationItems.Where(o => o.ID == ID).FirstOrDefault();
                            if (qitem != null)
                            {
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
                                    //else
                                    //    v = 0.00;
                                }
                            }
                        }
                        //update
                        ServiceManager.Quotation_Update(ID, QTY, SellingPrice, units, Notes, desc, servicetype, VAT,new Guid(),0,0,0);
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
                //list_Customfields.DataSource= ServiceManager.Quotation_SelectByIncidentID(QueryStringValues.CallID, Type, QueryStringValues.OPTION);
                //list_Customfields.DataBind();
                Grid_services.DataSource = ServiceManager.Quotation_SelectByIncidentID(QueryStringValues.CallID, Type,QueryStringValues.OPTION);
                Grid_services.DataBind();
                //update grid
                BindServiceCharge();
                if (Grid_services.Rows.Count > 0)
                {
                    pnlSummary.Visible = true;
                    try
                    {
                        //aRepository = new DCRepository<FixedPriceApproval>();
                        //if (aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault() == null)
                        //{
                        //    var ent = new FixedPriceApproval();
                        //    ent.CallID = QueryStringValues.CallID;
                        //    ent.ModifiedDate = DateTime.Now;
                        //    aRepository.Add(ent);
                        //}
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else
                    pnlSummary.Visible = false;
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
                            if ((s.Amount.HasValue ? s.Amount.Value : 0.00) > 0)
                            {
                                int retval = Insertservice(0, QueryStringValues.CallID, 1, 2, Type, s.Name, 1, s.Amount.HasValue ? s.Amount.Value : 0.00, 0, s.ApplyVAT.HasValue ? s.ApplyVAT.Value : false);
                                Service_Prices();

                                Grid_services.DataSource = ServiceManager.Quotation_SelectByIncidentID(QueryStringValues.CallID, Type,QueryStringValues.OPTION);
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
                IncidentService_Price.QuotationPrice_Update(QueryStringValues.CallID, double.Parse(txtDiscountPercent.Text.Trim()), txtNotes.Text.Trim(), Type,QueryStringValues.OPTION);
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
                        ToEmail.SendingMail(emailid.ToString(), "Quotation - Job Ref: TN" + QueryStringValues.CCID, htmlWrite.InnerWriter.ToString(), attachment1);
                    }
                    else
                    {
                        ToEmail.SendingMail(emailid.ToString(), "Quotation - Job Ref: TN" + QueryStringValues.CCID, htmlWrite.InnerWriter.ToString());
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
            var id = !string.IsNullOrEmpty(ddlItems.SelectedValue) ? ddlItems.SelectedValue : "0";  //hfCustomerId.Value;

            if (id == "0" && txtSearch.Text.Trim().Length > 0 && txtSearch.Visible == true)
            {
                var serviceID = Convert.ToInt32(!string.IsNullOrEmpty(hCatelogID.Value) ? hCatelogID.Value : "0");

                int retval = Insertservice(serviceID, QueryStringValues.CallID, int.Parse(string.IsNullOrEmpty(txtQty.Text.Trim()) ? "0" : txtQty.Text.Trim()), 2, Type, txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), Convert.ToDouble(!string.IsNullOrEmpty(txtCost.Text.Trim()) ? txtCost.Text.Trim() : "0.00"), Convert.ToDouble(!string.IsNullOrEmpty(txtVAT.Text.Trim()) ? txtVAT.Text.Trim() : "0.00"));
                if (retval == 1)
                {

                    lblMsg.Text = "Added successfully";
                    txtQty.Text = "1";
                    ddlstype.SelectedIndex = 0;
                    txtSearch.Text = string.Empty;
                    txtCost.Text = "0.00";
                    bindGrid();
                    //fill and get the values for total
                    Service_Prices();
                    hCatelogID.Value = "0";
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
                    int retval = Insertservice(serviceID, QueryStringValues.CallID, int.Parse(string.IsNullOrEmpty(txtQty.Text.Trim()) ? "0" : txtQty.Text.Trim()), 2, Type, txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), 0.00, Convert.ToDouble(!string.IsNullOrEmpty(txtVAT.Text.Trim()) ? txtVAT.Text.Trim() : "0.00"));
                    if (retval == 1)
                    {
                        ddlItems.SelectedIndex = 0;
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Added successfully";
                        txtQty.Text = string.Empty;
                        bindGrid();
                        //fill and get the values for total
                        Service_Prices();
                        hCatelogID.Value = "0";
                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                    }


                }
            }
        }
        private int Insertservice(int ServiceID, int IncidentID, int QTY, int ServiceTypeID, string type, string servicetext, int servicetypeid, double cost, double VAT, bool applyVAT = true)
        {
            var vt = 0.00;
            if (applyVAT)
            {
                vt = VATByCustomerBAL.VATByCustomer_select();
            }
            var v = (QTY * cost);
            if (vt > 0)
                v = (QTY * cost) * (vt / 100);
            else
                v = 0.00;
            SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 8);
            OutVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Quotation_Item_Insert",
                new SqlParameter("@ServiceID", ServiceID),
                new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@QTY", QTY), new SqlParameter("@Type", type),
                new SqlParameter("@ServiceTypeID", ServiceTypeID),
                new SqlParameter("@ServiceDescription", servicetext),
            new SqlParameter("@FixedRateTypeID", servicetypeid),
            new SqlParameter("@cost", cost),
            new SqlParameter("@VAT", v),
            new SqlParameter("@Option",QueryStringValues.OPTION)
                , OutVal);


            return int.Parse(OutVal.Value.ToString());


        }

        public void btnSubmitToCustomer_Click1(object sender, EventArgs e)
        {
            try
            {
                //foreach (ListViewDataItem item in list_Customfields.Items)

                //{

                //    CheckBox arName;

                //    arName = item.FindControl("chkRecommand") as CheckBox;


                //    var r1 = arName.Checked;
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
                    FLS_OptionalMailtoRequester();
                    pnlservice.Visible = false;
                    lblservice.Text = "Quotation has been sent to the client";
                    lblservice.Font.Bold = true;

                

                // }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
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

                //rebind the data
                bindGrid();
                Service_Prices();
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

                        string subject = "Ticket Reference: " + QueryStringValues.CCID + " Fixed Price Approval";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/fixedpriceapprovalmail.html");
                        body = body.Replace("[mail_head]", "Job Request");
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                TextBox txtDesc = (TextBox)e.Row.FindControl("txtDesc");

                Label lblstype2 = (Label)e.Row.FindControl("lblstype2");
                DropDownList ddlSType = (DropDownList)e.Row.FindControl("ddlSType");

                Label lblServiceID = (Label)e.Row.FindControl("lblServiceID");
                Label lblFixedRateTypeID = (Label)e.Row.FindControl("lblFixedRateTypeID");

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

                        ddlSType.SelectedValue = lblFixedRateTypeID.Text;
                    }
                }





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

                        string subject = "Job ref : " + fls.CCID.ToString();
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoice.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", "<a href='"+Deffinity.systemdefaults.GetWebUrl()+"'>"+ Deffinity.systemdefaults.GetWebUrl() + "</a>");

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
                    var FixedRateTypeName = string.Empty;

                    var d = stypelist.Where(o => o.FixedRateTypeID == n.ServiceTypeID).FirstOrDefault();
                    if (d != null)
                        FixedRateTypeName = d.FixedRateTypeName;
                    sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td>", n.ServiceDescription, FixedRateTypeName, n.QTY, string.Format("{0:F2}", n.SellingPrice)));

                    sbuild.Append("</tr>");
                }
                sbuild.Append("</table>");
            }

            return sbuild.ToString();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //add one entiry for approval
                aRepository = new DCRepository<FixedPriceApproval>();
                var inv = aRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();
                if (inv == null)
                {
                    var ent = new FixedPriceApproval();
                    ent.CallID = QueryStringValues.CallID;
                    ent.ModifiedDate = DateTime.Now;
                    inv.DeniedBy = sessionKeys.UID;
                    aRepository.Add(ent);
                }
                else
                {
                    inv.DeniedBy = sessionKeys.UID;
                    inv.ModifiedDate = DateTime.Now;
                    aRepository.Edit(inv);
                }
                IDCRespository<CallInvoice> inRepository = new DCRepository<CallInvoice>();
                var inDetails = inRepository.GetAll().Where(o => o.CallID == QueryStringValues.CallID).FirstOrDefault();

                if (inDetails != null)
                {
                    // lblInvoiceNo.Text = "#" + inDetails.ID.ToString();
                }
                else
                {
                    inDetails = new CallInvoice();
                    inDetails.CreatedDate = DateTime.Now;
                    inDetails.CallID = QueryStringValues.CallID;
                    inRepository.Add(inDetails);
                }
                FLS_SendMailtoRequester(inDetails);
                lblMsg.Text = "Mail has been sent successfully";
                BindCustomFields();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


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
                AddNewItems();
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
                            ddlstype.SelectedValue = "2";
                            hCatelogID.Value = lblID.Text;
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
                            mdlExnter.Hide();
                            chkRow.Checked = false;
                        }

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
            bool _visible = false;
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

        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(ddlTitle.SelectedValue);
                if (id > 0)
                {
                    txtTemplateName.Text = ddlTitle.SelectedItem.Text;
                    var str = QuoteTemplateBAL.QuoteTemplateSelectHTML(id);
                    QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_AddUpdate(QueryStringValues.CallID, Convert.ToInt32(ddlTitle.SelectedValue), txtTemplateName.Text.Trim());

                    QuoteTemplateBAL.GenerateJobQuoteHtml(QueryStringValues.CallID, str);
                    lblTemplateData.Text = str;
                    lblTemplateData.Text = txtTemplate.Text;
                    if (lblTemplateData.Text.Length > 0)
                    {
                        pnlTemplateDisplay.Visible = true;
                    }
                    else
                    {
                        pnlTemplateDisplay.Visible = false;
                    }
                    lblMsg.Text = "Template applied successfully";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        protected void btnEditTemplate_Click(object sender, EventArgs e)
        {
            //check ticket already assigned or not

            var tVal = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
            if (tVal == null)
            {
                var id = Convert.ToInt32(ddlTitle.SelectedValue);
                if (id > 0)
                {
                    var str = QuoteTemplateBAL.QuoteTemplateSelectHTML(id);
                    txtTemplate.Text = str;
                    lblTemplate.Text = "Template - " + ddlTitle.SelectedItem.Text;
                    txtTemplateName.Text = ddlTitle.SelectedItem.Text;

                    mdl_Template.Show();
                }
            }
            else
            {
                var t = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                if (t != null)
                {
                    txtTemplateName.Text = t.CurrentTemplateName;
                    txtTemplate.Text = QuoteTemplateBAL.JobQuoteTemplateSelectHTML(QueryStringValues.CallID);
                    mdl_Template.Show();
                }
            }
        }

        protected void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            try
            {



                QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_AddUpdate(QueryStringValues.CallID, Convert.ToInt32(ddlTitle.SelectedValue), txtTemplateName.Text.Trim());
                QuoteTemplateBAL.GenerateJobQuoteHtml(QueryStringValues.CallID, txtTemplate.Text);
                //QuoteTemplateBAL.GenerateJobQuoteHtml(QueryStringValues.CallID, txtTemplate.Text);
                lblTemplateData.Text = txtTemplate.Text;
                if (lblTemplateData.Text.Length > 0)
                {
                    pnlTemplateDisplay.Visible = true;
                }
                else
                {
                    pnlTemplateDisplay.Visible = false;
                }
                lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void bindTemplatedata()
        {
            try
            {
                var d = QuoteTemplateBAL.QuotationTemplatesAssignedToTicket_Select(QueryStringValues.CallID);
                if (d != null)
                {
                    lblTemplateName.Text = "Template: <b>" + d.CurrentTemplateName + "</b>";
                    lblTemplateData.Text = QuoteTemplateBAL.JobQuoteTemplateSelectHTML(QueryStringValues.CallID);
                }
                if (lblTemplateData.Text.Length > 0)
                {
                    pnlTemplateDisplay.Visible = true;
                }
                else
                {
                    pnlTemplateDisplay.Visible = false;
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
            ddlOptions.Items.Clear();
            var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID);
           
            if (oList.Count > 0)
            {
                ddlOptions.DataSource = oList.OrderBy(o => o.OptionName).ToList();
                ddlOptions.DataTextField = "OptionName";
                ddlOptions.DataValueField = "ID";
                ddlOptions.DataBind();
                ddlOptions.Items.Insert(0, new ListItem("Please select...", "0"));
                AddOptionButton_Visibility();
            }
            else
            {
                ddlOptions.Items.Insert(0, new ListItem("Please select...", "0"));
                AddOptionButton_Visibility(true);
                
            }
            
        }
        private void AddOptionButton_Visibility(bool v = false)
        {
            btnSubmitOptions.Visible = v;
            btnCancelOptions.Visible = v;
            txtOptions.Visible = v;
            ddlOptions.Visible = !v;
            btnAddOption.Visible = !v;
            btnEditOption.Visible = !v;
            btnDeleteOptions.Visible = !v;
            
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
            catch(Exception ex)
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
            catch(Exception ex)
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
                        BindOptions();
                        AddOptionButton_Visibility();
                        mdlManageOptions.Show();
                    }

                }
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
                if (ddlOptions.SelectedValue == "0")
                {
                    var q = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = QueryStringValues.CallID, OptionName = txtOptions.Text.Trim(), CustomerID = sessionKeys.PortfolioID, IsActive = false, Description = txtOptionDescription.Text.Trim() });
                    if (q != null)
                    {
                        if (q.ID == 0)
                        {
                            lblErrorOptions.Text = "Option name already exists";
                        }
                        else
                        {
                            lblMsgOptions.Text = Resources.DeffinityRes.Addedsuccessfully;
                            Response.Redirect(string.Format( "~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.CallID), false);
                            
                            //AddOptionButton_Visibility();
                            //txtOptions.Text = string.Empty;
                            //BindOptions();
                            //mdlManageOptions.Show();
                        }
                    }
                }
                else
                {
                    var item = QuotationOptionsBAL.QuotationOption_SelectByID(Convert.ToInt32(ddlOptions.SelectedValue));
                    item.OptionName = txtOptions.Text.Trim();
                    item.Description = txtOptionDescription.Text.Trim();
                    var q = QuotationOptionsBAL.QuotationOption_Update(item);
                    if (q != null)
                    {
                        if (q.ID == 0)
                        {
                            lblErrorOptions.Text = "Option name already exists";
                        }
                        else
                        {
                            lblMsgOptions.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                            Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.CallID), false);

                            //AddOptionButton_Visibility();
                            //txtOptions.Text = string.Empty;
                            //BindOptions();
                            //mdlManageOptions.Show();
                        }
                    }

                }
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
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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
                var r = QuotationOptionsBAL.QuotationOption_Add(new QuotationOption() { CallID = QueryStringValues.CallID, CustomerID = sessionKeys.PortfolioID, OptionName = "Option 1", IsActive=false});
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
        private void bindMaintenacePlan()
        {
            try
            {
                var c = FLSDetailsBAL.Jqgridlist(QueryStringValues.CallID).FirstOrDefault();
                IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer> prRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer>();
                IPortfolioRepository<PortfolioMgt.Entity.ProductPolicyType> ptRep = new PortfolioRepository<PortfolioMgt.Entity.ProductPolicyType>();

                var pList = prRep.GetAll().Where(o => o.OptionID == QueryStringValues.OPTION).ToList();
                var mList = ptRep.GetAll().Where(o => o.CustomerID == sessionKeys.PortfolioID).ToList();
                if (pList.Count == 0)
                {
                    foreach (var m in mList)
                    {
                        prRep.Add(new PortfolioMgt.Entity.ProductPolicyTypeAssociatedCustomer() { CustomerID = c.RequesterID, DiscountPrice = m.Yearly.HasValue ? m.Yearly.Value : 0, Price = m.Yearly.HasValue ? m.Yearly.Value : 0, OptionID = QueryStringValues.OPTION, PolicyTypeID = m.ID });
                    }

                    pList = prRep.GetAll().Where(o => o.OptionID == QueryStringValues.OPTION).ToList();
                }
                if (pList.Count > 0)
                {
                    var r = (from p in pList
                             join m in mList on p.PolicyTypeID equals m.ID
                             select new
                             {
                                 ID = p.ID,
                                 Title = m.Title,
                                 Price = p.Price,
                                 DiscountPrice = p.DiscountPrice

                             }).ToList();
                    GridOffers.DataSource = r;
                    GridOffers.DataBind();
                    pnlManageOptions.Visible = true;
                }
                else
                {
                    pnlManageOptions.Visible = false;
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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

                                string subject = "Job ref : " + fls.CCID.ToString();
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

                                body = body.Replace("[Address]",  fls.RequestersAddress + " ," + fls.RequestersCity + " ," + fls.RequestersTown + " , " + fls.RequestersPostCode + " , " + fls.RequestersTelephoneNo);
                               
                                //[date]



                                string Dis_body = body;
                                bool ismailsent = false;
                                // mail to requester
                                //if help desk or assign users are changed then mail should go to requester
                                //body = body.Replace("[user]", pcontact.Name);
                                em.SendingMail(fromemailid, subject, body,"indra@deffinity.com");
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
            catch(Exception ex)
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

        private string GetQuoteItemList(List<QuotationItem> rlist, List<QuotationOption> qlist,List<QuotationPrice> qtPrice,List<PortfolioMgt.Entity.ProductPolicyType> policylist, PortfolioMgt.Entity.PortfolioContactAddress addressDetails, string weburl)
        {
            StringBuilder sbuild = new StringBuilder();
            foreach (var q in qlist)
            {
                var noteslist = rlist.Where(o => o.QuotationOptionID == q.ID).ToList();
                var qPrice = qtPrice.Where(o => o.QuotationOptionID == q.ID).FirstOrDefault();
                if (noteslist.Count > 0)
                {
                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append(string.Format( "<tr><td><b> Option: {0}</b></td></tr>",q.OptionName));
                    sbuild.Append("</table>");
                    // UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();

                    sbuild.Append("<table style='width:100%'>");
                    sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                    sbuild.Append("<td style='width:40%'>Item</td><td>Unit Price</td><td>QTY</td><td> TAX</td><td>Total</td>");
                    sbuild.Append("</tr>");
                    foreach (var n in noteslist)
                    {
                        sbuild.Append("<tr>");
                        sbuild.Append(string.Format("<td>{0}</td><td style='text-align:right'>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td><td style='text-align:right'>{4}</td>", n.ServiceDescription, string.Format("{0:F2}", n.SellingPrice), n.QTY, string.Format("{0:F2}", n.VAT), string.Format("{0:F2}", (n.SellingPrice * n.QTY )+ n.VAT)));

                        sbuild.Append("</tr>");
                    }
                    sbuild.Append("</table>");

                    sbuild.Append("<table style='width:40%'>");
                    var price = string.Format("{0:F2}", qPrice.OriginalPrice);
                    sbuild.Append(string.Format("<tr><td><b> Total price: </b></td><td>{0}</td></tr>", price));
                    sbuild.Append("</table>");
                    if (!string.IsNullOrEmpty(qPrice.Notes))
                    {
                        sbuild.Append("<table style='width:100%'>");
                        var notes = qPrice.Notes;
                        sbuild.Append(string.Format("<tr><td><b> Notes: </b><br>{0}</td></tr>", notes));
                        sbuild.Append("</table>");
                    }

                    sbuild.Append("<table style='width:70%'>");
                    var b = weburl + string.Format("/WF/DC/DCQuoteMail.aspx?ccid={3}&callid={0}&statusid={1}&type={2}&Option={4}&cid={5}&tab=quote", sessionKeys.IncidentID, 1, "mail",QueryStringValues.CCID,q.ID,sessionKeys.PortfolioID);
                    sbuild.Append(string.Format("<tr><td><b> Accept: </b><br></td><td>{0}</td></tr>", getButton(b, q.OptionName)));
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

                            sbuild.Append(string.Format("<tr><td style='font-size:15px'> <img src='"+ Deffinity.systemdefaults.GetWebUrl() +"/Content/images/SpecialOffer.png' style='border:0px' /> <br /></td></tr>"));

                            sbuild.Append(string.Format("<tr><td> If you were to take out the following maintenance plan, this price would be as follows:</td></tr>"));
                            sbuild.Append("</table>");

                            sbuild.Append("<table style='width:85%'>");
                            //Header row
                            sbuild.Append(string.Format("<tr class='tab_header'><td><b> Plan </b></td><td><b> Discount Amount </b></td><td><b>New Total Price</b></td><td><b></b></td></tr>"));
                            foreach (var v in policylist)
                            {
                                var cUrl = weburl + string.Format("/WF/DC/DCQuoteContactMail.aspx?ccid={3}&callid={0}&statusid={1}&type={2}&Option={4}&cid={5}&planid={6}&tab=quote", sessionKeys.IncidentID, 1, "mail", QueryStringValues.CCID, q.ID, sessionKeys.PortfolioID, v.ID.ToString());

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

                        var noteslist = dc.QuotationItems.Where(c => c.CallidID == callid ).ToList();
                        var qtPrice = dc.QuotationPrices.Where(c => c.CallID == callid ).ToList();
                        var stypelist = dc.QuotationOptions.Where(c=>c.CallID == callid).ToList();
                        var policylist = pd.ProductPolicyTypes.Where(o => o.CustomerID == cdetails.CompanyID).ToList();
                        var addressDetails = pd.PortfolioContactAddresses.Where(c => c.ID == fdetails.ContactAddressID).FirstOrDefault();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Here's Your Quotation";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSQuotation.htm");

                        body = body.Replace("[mail_head]", "Job Quotation");
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


                        body = body.Replace("[refno]",  fls.CCID.ToString());
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
            var v= string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style = 'border-radius: 3px;' bgcolor = '#ED7D31'><a href = '{0}' target = '_blank' style = 'font-size: 16px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; text-decoration: none;border-radius: 3px; padding: 12px 18px; border: 1px solid #ED7D31; display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);
            return v;
        }

        private string getGreenButton(string url, string name)
        {
            //var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' style='border-radius:2px;' bgcolor='#63b026'><a href = '{0}' target = '_blank' style='padding: 8px 12px; border: 1px solid #63b026;border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px; color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;'>{1}</a></td ></tr></table></td></tr ></table>", url, name);

            var v = string.Format("<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td><table border='0' cellspacing='0' cellpadding='0'><tr><td align = 'center' ><a href = '{0}' target = '_blank' >{1}</a></td ></tr></table></td></tr ></table>", url, "<img src='"+ Deffinity.systemdefaults.GetWebUrl()+"/Content/images/ContactMe.png' style='border:0px' />");
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
                                lblservice.Text = "Quotation has been accepted";
                                lblservice.Font.Bold = true;
                                pnlAccept.Visible = false;

                            }

                        }
                        else
                        {
                            lblMsg.Text = "Quote already accepted. Please check again";
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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
        private void ChangeViewByType()
        {
            pnlFinance.Visible = false;
            pnlTemplateTab.Visible = false;
            pnlQuoteTab.Visible = false;
            if (Request.QueryString["tab"] != null)
            {

                if(Request.QueryString["tab"] == "finance")
                {
                    pnlFinance.Visible = true;
                    lbtnFinance.BackColor = System.Drawing.Color.White;
                }
                else if (Request.QueryString["tab"] == "attach")
                {
                    pnlTemplateTab.Visible = true;
                    lbtnAttach.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    pnlQuoteTab.Visible = true;
                    lbtnQuote.BackColor = System.Drawing.Color.White;
                }
            }
            else
            {
                pnlQuoteTab.Visible = true;
                lbtnQuote.BackColor = System.Drawing.Color.White;
            }
        }
        private string GetPolicyData(int AssignedPolicyID, List<PortfolioMgt.Entity.ProductPolicyType> pd, double total)
        {
            string retval = string.Empty;
            //2 is default
            if (AssignedPolicyID == 2)
            {
                retval = string.Format("<div class='form-group'><div class='col-md-2 price_lable'> {0}</div> <div class='col-md-2 price_lable'> {1}</div><div class='col-md-2 price_lable' style='color:#7c38bc;'> {2}</div></div>", "Plan", "Member Total", "Savings");
                foreach (var p in pd.ToList())
                {
                    var d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;
                    var dtotal = d > 0 ? total - (total * (d / 100)) : total;
                    retval = retval + string.Format("<div class='form-group'><div class='col-md-2 price_text' style='text-align:right;'> {0}</div> <div class='col-md-2 price_text' style='text-align:right;'> {1}</div><div class='col-md-2 price_text' style='color:#7c38bc;text-align:right;'> {2}</div></div>", p.Title, string.Format("{0:F2}", dtotal), string.Format("{0:F2}", total - dtotal));
                }
            }
            else
            {


                retval = string.Format("<div class='form-group'><div class='col-md-2 price_lable'> {0}</div><div class='col-md-2 price_lable' style='color:#7c38bc;'> {1}</div></div>", "Member Total", "Savings");
                foreach (var p in pd.Where(o => o.ID == AssignedPolicyID).ToList())
                {
                    var d = p.DiscountPercent.HasValue ? p.DiscountPercent.Value : 0;
                    var dtotal = d > 0 ? total - (total * (d / 100)) : total;
                    retval = retval + string.Format("<div class='form-group'> <div class='col-md-2 price_text' style='text-align:right;'> {0}</div><div class='col-md-2 price_text' style='color:#7c38bc;text-align:right;'> {1}</div></div>", string.Format("{0:F2}", dtotal), string.Format("{0:F2}", total - dtotal));
                }
               
            }
            return retval;
        }
        private void BindCustomFields()
        {
            try
            {
                var oList = QuotationOptionsBAL.QuotationOption_Select(QueryStringValues.CallID).OrderBy(o=>o.OptionName).ToList();
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
                                    o.Description,
                                    ItemsCount = qList.Where(p=>p.QuotationOptionID == o.ID).Count(),
                                    Price = (pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault() != null ? pList.Where(p => p.QuotationOptionID == o.ID).FirstOrDefault().OriginalPrice : 0.00),
                                    IsAplied = pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault() != null? pList.Where(p => p.QuotationOptionID == o.ID && p.IsOptionActive == true).FirstOrDefault().IsOptionActive:false
                                    
                                }).ToList();
                    var rLIst = (from o in dList
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
                                     o.Description,
                                     d_IsAplied = (o.IsAplied.HasValue ? o.IsAplied.Value : false) ? "Sold" : "",
                                     //MemberCost = string.Format("{0:F2}", (discountPercent > 0) ? o.Price - (o.Price * (discountPercent / 100)) : o.Price),
                                     //Savings = string.Format("{0:F2}", (discountPercent > 0) ?  (o.Price * (discountPercent / 100)) : 0.00),
                                     mdata = GetPolicyData(selectedpolicyID, policyList, o.Price)
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
                    Response.Redirect(string.Format("/WF/DC/DCQuotationItems.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, optionid, "quote"));
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
                        if(!r)
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

        protected void btnViewCompare_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option={2}&tab={3}", QueryStringValues.CCID, QueryStringValues.CallID, QueryStringValues.OPTION, "quote"));

        }

        protected void btnSave_Click1(object sender, EventArgs e)
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
                    double VAT = Convert.ToDouble(!string.IsNullOrEmpty(txtVAT.Text.Trim()) ? txtVAT.Text.Trim() : "0.00");
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
                                //else
                                //    v = 0.00;
                            }
                        }
                    }
                    //update
                    double units = 0.00;
                    ServiceManager.Quotation_Update(ID, QTY, SellingPrice, units, Notes, desc, servicetype, VAT, new Guid(), 0,0,0);
                    
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                

            }

            lblMsg.Text = "Updated successfully";
            Service_Prices();
        }
    }


}