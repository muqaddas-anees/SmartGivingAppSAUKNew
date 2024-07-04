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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.DC1.controls
{
    public partial class DCBOMCtrl : System.Web.UI.UserControl
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
            var list = ftypeRepository.GetAll().OrderBy(o => o.FixedRateTypeName).ToList();
            //check default entries
            //update labour to labor
            if (list.Where(o => o.FixedRateTypeName.ToLower() == "labour").Count() == 1)
            {
                var v = list.Where(o => o.FixedRateTypeName.ToLower() == "labour").FirstOrDefault();
                v.FixedRateTypeName = "Labor";
                ftypeRepository.Edit(v);

                list = ftypeRepository.GetAll().OrderBy(o => o.FixedRateTypeName).ToList();
            }
            //Materials
            if (list.Where(o => o.FixedRateTypeName.ToLower() == "materials").Count() == 0)
            {
                ftypeRepository.Add(new FixedRateType() { FixedRateTypeName = "Materials" });
            }

            if (list.Where(o => o.FixedRateTypeName.ToLower() == "labor").Count() == 0)
            {
                //update to Labor
                ftypeRepository.Add(new FixedRateType() { FixedRateTypeName = "Labor" });
            }
                //Labour
              

            //Expenses
            if (list.Where(o => o.FixedRateTypeName.ToLower() == "expenses").Count() == 0)
            {
                ftypeRepository.Add(new FixedRateType() { FixedRateTypeName = "Expenses" });
            }

            if (list.Where(o => o.FixedRateTypeName.ToLower() == "other").Count() == 0)
            {
                ftypeRepository.Add(new FixedRateType() { FixedRateTypeName = "Other" });
            }

            //•	Expenses
            //•	Labor
            //•	Maintenance
            //•	Materials
            //•	Other


            return list;

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
                    bindServiceType();
                    //add one entiry for approval

                    //check DLT
                    //DLT_check();
                    BindVendoritems();

                    BasicBind();
                    BindPopWindow();
                    bindGrid();

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
                    Gridfilesbind();

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

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
            BindCategory();
            BindSubCategory();
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
            SqlDataReader dr = IncidentService_Price.BOM_Price_Select(QueryStringValues.CallID, Type);
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
                        double QTY,SellingPrice;
                        double VAT;
                        double units = 0;
                        string Notes;
                        int servicetype = 0;
                        string desc = string.Empty;
                        int i = Grid_services.EditIndex;
                        GridViewRow Row = Grid_services.Rows[i];
                        ID = int.Parse(e.CommandArgument.ToString());
                        QTY = Convert.ToDouble(((TextBox)Row.FindControl("txtQty")).Text);
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
                            var qitem = dc.BOMItems.Where(o => o.ID == ID).FirstOrDefault();
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
                                    else
                                        VAT = 0.00;
                                }
                            }
                        }
                        //update
                        ServiceManager.BOM_Update(ID, QTY, SellingPrice, units, Notes, desc, servicetype, VAT,0);
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
                Grid_services.DataSource = ServiceManager.BOM_SelectByCallID(QueryStringValues.CallID, Type);
                Grid_services.DataBind();

                //check default data is exits 
                var retval = JobDefaultSettingsBAL.JobDefaultSettingsBAL_SelectCopiedBOMDefaults(QueryStringValues.CallID);
                //Add default values
                if (Grid_services.Rows.Count == 0 && !retval)
                {
                  

                    try
                    {
                        //add default enties 
                        Insertservice(0, QueryStringValues.CallID, 1, 2, Type, "Materials", GetTypeData().Where(o => o.FixedRateTypeName == "Materials").FirstOrDefault().FixedRateTypeID, 0, "00000000-0000-0000-0000-000000000000");
                        Insertservice(0, QueryStringValues.CallID, 1, 2, Type, "Time/Labor", GetTypeData().Where(o => o.FixedRateTypeName == "Labor").FirstOrDefault().FixedRateTypeID, 0, "00000000-0000-0000-0000-000000000000");
                        Insertservice(0, QueryStringValues.CallID, 1, 2, Type, "Expenses", GetTypeData().Where(o => o.FixedRateTypeName == "Expenses").FirstOrDefault().FixedRateTypeID, 0, "00000000-0000-0000-0000-000000000000");

                        Grid_services.DataSource = ServiceManager.BOM_SelectByCallID(QueryStringValues.CallID, Type);
                        Grid_services.DataBind();

                        if(Grid_services.Rows.Count >0)
                        {
                            //default values are added
                            JobDefaultSettingsBAL.JobDefaultSettingsBAL_UpdateBOMDefaults(QueryStringValues.CallID, true);
                        }

                        btnSave.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
               

                if (Grid_services.Rows.Count > 0)
                {
                    btnSave.Visible = true;
                    pnlSummary.Visible = true;
                  
                }
                else
                {
                    btnSave.Visible = false;
                    pnlSummary.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //private void BindServiceCharge()
        //{
        //    if (Grid_services.Rows.Count > 0)
        //    {
        //        //Bind service charge
        //        using (DCDataContext dc = new DCDataContext())
        //        {
        //            var s = dc.ServiceChargeDefaults.Where(o => o.PortfolioID == sessionKeys.PortfolioID).FirstOrDefault();
        //            if (s != null)
        //            {
        //                //check service charge already exits or not

        //                var isExists = dc.BOMItems.Where(o => o.CallID == QueryStringValues.CallID).Where(o => o.ServiceDescription.ToLower() == s.Name.ToLower()).FirstOrDefault();
        //                if (isExists == null)
        //                {
        //                    if ((s.Amount.HasValue ? s.Amount.Value : 0.00) > 0)
        //                    {
        //                        int retval = Insertservice(0, QueryStringValues.CallID, 1, 2, Type, s.Name, 1, s.Amount.HasValue ? s.Amount.Value : 0.00, s.ApplyVAT.HasValue ? s.ApplyVAT.Value : false);
        //                        Service_Prices();

        //                        Grid_services.DataSource = ServiceManager.BOM_SelectByCallID(QueryStringValues.CallID, Type);
        //                        Grid_services.DataBind();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected void btnUpdateTotals_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGrid();
                IncidentService_Price.BOM_Price_Update(QueryStringValues.CallID, double.Parse(txtDiscountPercent.Text.Trim()), txtNotes.Text.Trim(), Type);
                bindGrid();
                Service_Prices();
                AddServicePriceJouranl(txtNotes.Text.Trim(), Convert.ToDouble(string.IsNullOrEmpty(lblTotalPrice.InnerText.Trim()) ? "0" : lblTotalPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblRevisedPrice.InnerText.Trim()) ? "0" : lblRevisedPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lbluc.InnerText.Trim()) ? "0" : lbluc.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPercent.Text.Trim()) ? "0" : txtDiscountPercent.Text.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblDiscountValue.InnerText.Trim()) ? "0" : lblDiscountValue.InnerText.Trim()), DateTime.Now);
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
            BindItems();
            BindService();
        }
        protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlSelect1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindItems();
            BindService();
        }
        private void BindService()
        {
            try
            {
                //ddlService.DataSourceID = "SqlDataSource2";
                ddlService.DataSource = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCatlogue",
                new SqlParameter("@Type", int.Parse(ddlSelect.SelectedValue)), new SqlParameter("@PortfolioID", Convert.ToInt32(ddlVendors.SelectedValue))
                , new SqlParameter("@Category", int.Parse(ddlCategory.SelectedValue)), new SqlParameter("@SubCategory", int.Parse(ddlSubCategory.SelectedValue)),
                new SqlParameter("@Add_select", 1), new SqlParameter("@Supplier", 0)).Tables[0];
                ddlService.DataBind();
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

                    string emailid = SDTeamToCustomer1.BindControls();
                    StringWriter stringWrite = new StringWriter();
                    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    SDTeamToCustomer1.RenderControl(htmlWrite);
                    Email ToEmail = new Email();
                    ToEmail.SendingMail(emailid.ToString(), "Order", htmlWrite.InnerWriter.ToString());
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
                //BindServiceCharge();
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
                var serviceID = 0;
                int retval = Insertservice(serviceID, QueryStringValues.CallID, int.Parse(string.IsNullOrEmpty(txtQty.Text.Trim()) ? "0" : txtQty.Text.Trim()), 2, Type, txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), Convert.ToDouble(!string.IsNullOrEmpty(txtCost.Text.Trim()) ? txtCost.Text.Trim() : "0.00"), hImage.Value);
                if (retval == 1)
                {

                    lblMsg.Text = "Added successfully";
                    txtQty.Text = "1";
                    ddlstype.SelectedIndex = 0;
                    txtSearch.Text = string.Empty;
                    txtCost.Text = "0.00";
                    hImage.Value = "00000000-0000-0000-0000-000000000000";
                    bindGrid();

                    //fill and get the values for total
                    Service_Prices();
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
                    int retval = Insertservice(serviceID, QueryStringValues.CallID, int.Parse(string.IsNullOrEmpty(txtQty.Text.Trim()) ? "0" : txtQty.Text.Trim()), 2, Type, txtSearch.Text.Trim(), Convert.ToInt32(ddlstype.SelectedValue), 0.00, hImage.Value);
                    if (retval == 1)
                    {
                        ddlItems.SelectedIndex = 0;
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Added successfully";
                        txtQty.Text = string.Empty;
                        hImage.Value = "00000000-0000-0000-0000-000000000000";
                        bindGrid();
                        //fill and get the values for total
                        Service_Prices();
                    }
                    else
                    {
                        lblError.Text = "Item already exists";
                    }


                }
            }
        }
        private int Insertservice(int ServiceID, int IncidentID, int QTY, int ServiceTypeID, string type, string servicetext, int servicetypeid, double cost, string imgGuid = "00000000-0000-0000-0000-000000000000", bool applyVAT = true)
        {
            var vtrate = 0.00;
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

            var nGuid = new Guid(imgGuid);
            SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 8);
            OutVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DCBOMItem_Insert",
                new SqlParameter("@ServiceID", ServiceID),
                new SqlParameter("@IncidentID", IncidentID), new SqlParameter("@QTY", QTY), new SqlParameter("@Type", type),
                new SqlParameter("@ServiceTypeID", ServiceTypeID),
                new SqlParameter("@ServiceDescription", servicetext),
            new SqlParameter("@FixedRateTypeID", servicetypeid),
            new SqlParameter("@cost", cost), new SqlParameter("@VAT", v)
            , new SqlParameter("@VATRate", vt)
            , new SqlParameter("@Image", nGuid)
                , OutVal);


            return int.Parse(OutVal.Value.ToString());


        }

        public void btnSubmitToCustomer_Click1(object sender, EventArgs e)
        {
            if (txtDiscountPercent.Text != string.Empty)
            {
                IncidentService_Price.BOM_Price_Update(sessionKeys.IncidentID, double.Parse(txtDiscountPercent.Text.Trim()), txtNotes.Text.Trim(), Type);

                if (Type != "FLS")
                {
                    //using (IncidentDataContext su = new IncidentDataContext())
                    //{
                    //    int id = QueryStringValues.SDID;
                    //    IncidentMgt.Entity.Incident inc = su.Incidents.Where(p => p.ID == id).FirstOrDefault();
                    //    inc.Status = "Pending Approval";
                    //    su.SubmitChanges();
                    //}
                }
                else
                {
                    //CallDetail cd = CallDetailsBAL.SelectbyId(sessionKeys.IncidentID);
                    ////40	Quotation Submitted
                    //cd.StatusID = 40;
                    //CallDetailsBAL.CallDetailsUpdate(cd);
                    //DateTime modified_date = DateTime.Now;
                    //AddCallDetailsJournal(cd, modified_date);
                    //AddServicePriceJouranl(txtNotes.Text.Trim(), Convert.ToDouble(string.IsNullOrEmpty(lblTotalPrice.InnerText.Trim()) ? "0" : lblTotalPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblRevisedPrice.InnerText.Trim()) ? "0" : lblRevisedPrice.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lbluc.InnerText.Trim()) ? "0" : lbluc.InnerText.Trim()), Convert.ToDouble(string.IsNullOrEmpty(txtDiscountPercent.Text.Trim()) ? "0" : txtDiscountPercent.Text.Trim()), Convert.ToDouble(string.IsNullOrEmpty(lblDiscountValue.InnerText.Trim()) ? "0" : lblDiscountValue.InnerText.Trim()), modified_date);
                }

                //Incident incident = new Incident();
                //incident.Status = "Pending Approval";
                //IncidentHelper.Update(incident);
                //BuildMail();
                pnlservice.Visible = false;
                lblservice.Text = "Revised Price has been sent to the client";
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
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "BOMItems_Delete",
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
            IPortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category> cRepository = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category>();
            var vlist = cRepository.GetAll().Where(o => (o.MasterID.HasValue ? o.MasterID.Value : 0) == 0 && o.VendorID == Convert.ToInt32(ddlVendors.SelectedValue)).ToList();
            if (vlist.Count > 0)
            {
                ddlCategory.DataSource = (from v in vlist
                                          orderby v.CategoryName
                                          select new { Name = v.CategoryName, ID = v.ID }).ToList();
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        public void BindSubCategory()
        {
            IPortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category> cRepository = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatalog_category>();
            var vlist = cRepository.GetAll().Where(o => (o.MasterID.HasValue ? o.MasterID.Value : 0) == Convert.ToInt32(ddlCategory.SelectedValue) && o.VendorID == Convert.ToInt32(ddlVendors.SelectedValue)).ToList();
            if (vlist.Count > 0)
            {
                ddlSubCategory.DataSource = (from v in vlist
                                             orderby v.CategoryName
                                             select new { Name = v.CategoryName, ID = v.ID }).ToList();
                ddlSubCategory.DataTextField = "Name";
                ddlSubCategory.DataValueField = "ID";
                ddlSubCategory.DataBind();
            }
            ddlSubCategory.Items.Insert(0, new ListItem("Please select...", "0"));
        }

        public void BindItems()
        {
            IPortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor> cRepository = new PortfolioRepository<PortfolioMgt.Entity.v_ShopItems_vendor>();
            var vlist = cRepository.GetAll().Where(o => (o.Category.HasValue ? o.Category.Value : 0) == Convert.ToInt32(ddlCategory.SelectedValue) && (o.SubCategory.HasValue ? o.SubCategory.Value : 0) == Convert.ToInt32(ddlSubCategory.SelectedValue) && o.Type == Convert.ToInt32(ddlSelect.SelectedValue) && o.VID == Convert.ToInt32(ddlVendors.SelectedValue)).ToList();
            if (vlist.Count > 0)
            {
                ddlService.DataSource = (from v in vlist
                                         orderby v.Description
                                         select new { Name = v.Description, ID = v.ID }).ToList();
                ddlService.DataTextField = "Name";
                ddlService.DataValueField = "ID";
                ddlService.DataBind();
            }
            ddlService.Items.Insert(0, new ListItem("Please select...", "0"));
        }
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
                        string subject = "Ticket Reference: " + QueryStringValues.CallID + " Fixed Price Approval";
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/fixedpriceapprovalmail.html");
                        body = body.Replace("[mail_head]", "Service Desk Request");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[user]", uEntity.ContractorName);
                        //body = body.Replace("[adminemail]", ae == null ? string.Empty : ae.EmailAddress);
                        //body = body.Replace("[Status]", ddlStatus.SelectedItem.Text);
                        //body = body.Replace("[footer]", Server.HtmlDecode(ef == null ? string.Empty : ef.EmailFooter1));
                        body = body.Replace("[ref]", "" + QueryStringValues.CallID);
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

            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                var list = BOMBAL.BOMBAL_BOMItems_Select(QueryStringValues.CallID).ToList();

                Label lblfooter_total = (Label)e.Row.FindControl("lblfooter_total");
                Label lblfooter_vat = (Label)e.Row.FindControl("lblfooter_vat");
                Label lblfooter_unitprice = (Label)e.Row.FindControl("lblfooter_unitprice");
                Label lblfooter_qty = (Label)e.Row.FindControl("lblfooter_qty");
                if (list.Count > 0)
                {
                    lblfooter_total.Text = string.Format("{0:N2}", list.Sum(o => (o.SellingPrice * o.QTY) + o.VAT));
                    lblfooter_vat.Text = string.Format("{0:N2}", list.Sum(o => o.VAT));
                    lblfooter_unitprice.Text = string.Format("{0:N2}", list.Sum(o => o.SellingPrice));
                    lblfooter_qty.Text = string.Format("{0:N2}", list.Sum(o => o.QTY));
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
                        var fieldList = FLSFieldsConfigBAL.GetListOfFields().Where(g => g.CustomerID == sessionKeys.PortfolioID).ToList();
                        var cdetails = dc.CallDetails.Where(c => c.ID == callid).FirstOrDefault();

                        var pcontact = pd.PortfolioContacts.Where(c => c.ID == (cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0)).FirstOrDefault();
                        var portfolio = pd.ProjectPortfolios.Where(c => c.ID == cdetails.CompanyID).FirstOrDefault();
                        var status = dc.Status.Where(c => c.ID == cdetails.StatusID).FirstOrDefault();
                        var flsdetail = dc.FLSDetails.Where(p => p.CallID == cdetails.ID).FirstOrDefault();

                        var noteslist = dc.Incident_Services.Where(c => c.IncidentID == callid).ToList();
                        var pricelist = dc.Incident_ServicePrices.Where(c => c.IncidentID == callid).OrderByDescending(o => o.ID).FirstOrDefault();
                        var stypelist = dc.FixedRateTypes.ToList();
                        //var subject = dc
                        //PortfolioContact pc = ws.GetContactDetails(cdetails.RequesterID.HasValue ? cdetails.RequesterID.Value : 0);

                        string subject = "Ticket Reference: " + fls.CCID.ToString();
                        Emailer em = new Emailer();
                        string body = em.ReadFile("~/WF/DC/EmailTemplates/FLSInvoice.htm");

                        body = body.Replace("[mail_head]", "Invoice");
                        body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                        body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                        body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                        body = body.Replace("[noteslist]", GetNotesList(noteslist, stypelist));



                        body = body.Replace("[address]", fls.RequesterName + "<br>" + fls.RequestersAddress + "<br>" + fls.RequestersCity + "," + fls.RequestersTown + "<br>" + fls.RequestersPostCode + "<br>" + fls.RequestersTelephoneNo);
                        var sp = 0.00;
                        var vat = 0.00;
                        if (pricelist != null)
                            sp = pricelist.OriginalPrice.HasValue ? pricelist.OriginalPrice.Value : 0.00;

                        //var s1 = Convert.ToDouble( (vat * sp) / Convert.ToDouble(100));

                        body = body.Replace("[gtotal]", string.Format("{0:F2}", sp));
                        body = body.Replace("[discount]", "--------");
                        body = body.Replace("[vat]", string.Format("{0:F2}", vat));
                        body = body.Replace("[sub]", string.Format("{0:F2}", sp));
                        body = body.Replace("[invno]", invoice.ID.ToString());
                        body = body.Replace("[refno]", "" + fls.CCID.ToString());
                        body = body.Replace("[date]", invoice.CreatedDate.Value.ToShortDateString());
                        body = body.Replace("[company]", portfolio.PortFolio);
                        body = body.Replace("[bank]", portfolio.BankName);
                        body = body.Replace("[account]", portfolio.AccountNumber);
                        body = body.Replace("[taxreg]", portfolio.TaxReg);
                        body = body.Replace("[sortcode]", portfolio.SortCode);
                        body = body.Replace("[iban]", portfolio.IBAN);
                        body = body.Replace("[swiftcode]", portfolio.SwiftCode);

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
                sbuild.Append("<td style='width:50%'>Service</td><td>Type</td><td>Quantity</td><td> Unit Price</td><td>TAX</td><td>Total</td>");
                sbuild.Append("</tr>");
                foreach (var n in noteslist)
                {
                    double vTotal = 0;
                    if (n.VAT.HasValue)
                    {
                        if (n.VAT.Value > 0)
                        {
                            vTotal = (n.QTY.Value * n.SellingPrice.Value) + n.VAT.Value;
                        }
                        else
                        {
                            vTotal = (n.QTY.Value * n.SellingPrice.Value);
                        }
                    }
                    string FixedRateTypeName = string.Empty;
                    var f = stypelist.Where(o => o.FixedRateTypeID == (n.FixedRateTypeID.HasValue ? n.FixedRateTypeID.Value : 0)).FirstOrDefault();
                    if (f != null)
                        FixedRateTypeName = f.FixedRateTypeName;

                    sbuild.Append("<tr>");
                    sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td style='text-align:right'>{2}</td><td style='text-align:right'>{3}</td><td style='text-align:right'>{4}</td><td style='text-align:right'>{5}</td>", n.ServiceDescription, FixedRateTypeName, n.QTY, string.Format("{0:F2}", n.SellingPrice), string.Format("{0:F2}", n.VAT), string.Format("{0:F2}", vTotal)));

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

                //BindServiceCharge();
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
                            hImage.Value = vitems.Image.ToString();
                            hCatelogID.Value = vitems.ID.ToString();// lblID.Text;
                            ddlstype.SelectedValue = "0";

                            //add item
                            AddNewItems();

                            //un check selected item
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


        protected void btnSendToQuotations_Click(object sender, EventArgs e)
        {
            try
            {
                QuotationBAL.AddDefault_Options(QueryStringValues.CallID, true);
                Response.Redirect(string.Format("~/WF/DC/DCQuotationCompare.aspx?CCID={0}&callid={1}&SDID={1}&Option=0&tab=quote", QueryStringValues.CCID, QueryStringValues.CallID), false);

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            UpdateGrid();
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
                    //TextBox txtSalesPrice = (TextBox)row.FindControl("txtSalesPrice");
                    //double SalesPrice = Convert.ToDouble(!string.IsNullOrEmpty(txtSalesPrice.Text.Trim()) ? txtSalesPrice.Text.Trim() : "0.00");

                    double VAT = Convert.ToDouble(!string.IsNullOrEmpty(txtVAT.Text.Trim()) ? txtVAT.Text.Trim() : "0.00");
                    double VATRate = Convert.ToDouble(!string.IsNullOrEmpty(txtVATRate.Text.Trim()) ? txtVATRate.Text.Trim() : "0.00");
                    TextBox txtEnotes = (TextBox)row.FindControl("txtEnotes");
                    string Notes = txtEnotes.Text.Trim();
                    //Label lblDescription = (Label)row.FindControl("lblDescription");
                    int servicetype = 0;
                    string desc = string.Empty;
                    //check qty or amount is updated
                    using (DCDataContext dc = new DCDataContext())
                    {
                        var applyVAT = true;
                        var qitem = dc.BOMItems.Where(o => o.ID == ID).FirstOrDefault();
                        if (qitem != null)
                        {
                            desc = qitem.ServiceDescription;
                            servicetype = qitem.ServiceTypeID.HasValue ? qitem.ServiceTypeID.Value : 0;
                            if ((qitem.QTY != QTY) || (qitem.SellingPrice != SellingPrice) || (qitem.VATRate != VATRate))
                            {
                                VATByCustomerBAL.VATByCustomer_select();
                                var vt = 0.00;
                                if (applyVAT)
                                {
                                    vt = VATByCustomerBAL.VATByCustomer_select();
                                }
                                var v = (QTY * SellingPrice);
                                if (VATRate > 0)
                                    VAT = (QTY * SellingPrice) * (VATRate / 100);
                                else
                                    VAT = 0.00;
                            }
                        }
                    }
                    //update
                    double units = 0.00;
                    ServiceManager.BOM_Update(ID, Convert.ToInt32(QTY), SellingPrice, units, Notes, desc, servicetype, VAT,VATRate);

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }


            }

            lblMsg.Text = "Updated successfully";
            Service_Prices();
            bindGrid();
        }

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
                    string filepath = string.Format("~/WF/UploadData/DC/{0}/", QueryStringValues.CallID.ToString(), e.CommandArgument.ToString());
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
                var folderpath = Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString());
                if (System.IO.Directory.Exists(folderpath))
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/WF/UploadData/DC/" + QueryStringValues.CallID.ToString()));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
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