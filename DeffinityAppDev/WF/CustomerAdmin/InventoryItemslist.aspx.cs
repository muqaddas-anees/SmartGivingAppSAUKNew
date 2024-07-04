using Deffinity.BE;
using InventoryMgt.BAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.BLL;

namespace DeffinityAppDev.WF.CustomerAdmin
{
    public partial class InventoryItemslist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["errormsg"] != null)
                {
                    lblError.Text = Session["errormsg"].ToString();
                    Session["errormsg"] = null;
                }
                if (Session["msg"] != null)
                {
                    lblmsg.Text = Session["msg"].ToString();
                    Session["msg"] = null;
                }
                //check supplier is exists
                Supplier_AddInternal();
                BindEquipemnt();
                //BindSuppliers();
                //SetInternalSupplierChange();
                //BindSuppplierMaterialData();
                BindStorageLocation();
                BindGrid();
                ClearData();
            }
        }
        private void BindSuppplierMaterialData()
        {
            try
            {
                IPortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material> mRep = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material>();
                //var mLIst = mRep.GetAll().Where(o => o.Supplier == Convert.ToInt32(ddlSupplier.SelectedValue)).OrderBy(o => o.ItemDescription).ToList();
                var mLIst = mRep.GetAll().Where(o => o.Supplier == Convert.ToInt32(ddlSupplier.SelectedValue) && o.SubCategory == Convert.ToInt32(ddlSubCategory.SelectedValue)).OrderBy(o => o.ItemDescription).ToList();
                ddlSupplierEquipment.DataSource = mLIst;
                ddlSupplierEquipment.DataTextField = "ItemDescription";
                ddlSupplierEquipment.DataValueField = "ID";
                ddlSupplierEquipment.DataBind();
                ddlSupplierEquipment.Items.Insert(0, new ListItem("Please select...", "0"));
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindGrid()
        {
            try
            {
                List<InventoryMgt.Entity.V_InventoryItem> rlist = InventoryItemList();
                GridList.DataSource = rlist;
                GridList.DataBind();
                if (GridList.Rows.Count > 0)
                {
                    GridList.HeaderRow.Cells[1].Text = Deffinity.systemdefaults.GetCategoryName();
                    GridList.HeaderRow.Cells[2].Text = Deffinity.systemdefaults.GetSubCategoryName();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private List<InventoryMgt.Entity.V_InventoryItem> InventoryItemList()
        {
            string searchtext = txtSearch.Text.Trim();
            var rlist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Select();
            //if (!string.IsNullOrEmpty(txtFromdate.Text.Trim()))
            //    rlist = rlist.Where(o => o.DateOfReminder >= Convert.ToDateTime(txtFromdate.Text.Trim())).ToList();
            //if (!string.IsNullOrEmpty(txtTodate.Text.Trim()))
            //    rlist = rlist.Where(o => o.DateOfReminder <= Convert.ToDateTime(txtTodate.Text.Trim())).ToList();
            if (!string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
            {
                rlist = rlist.Where(p =>
            (p.CategoryName != null ? p.CategoryName.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.Equipment != null ? p.Equipment.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.SubCategoryName != null ? p.SubCategoryName.ToLower().Contains(searchtext.ToLower()) : false)
            || (p.StorageLocationName != null ? p.StorageLocationName.ToLower().Contains(searchtext.ToLower()) : false)

            ).OrderByDescending(p => p.ID).Select(p => p).ToList();
            }

            return rlist;
        }

        protected void GridList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;
            var list = InventoryItemList();
            if (Sortdir == "ASC")
            {
                list = Sort<InventoryMgt.Entity.V_InventoryItem>(list, SortExp, SortDirection.Ascending);
            }
            else
            {
                list = Sort<InventoryMgt.Entity.V_InventoryItem>(list, SortExp, SortDirection.Descending);
            }
            this.GridList.DataSource = list;
            this.GridList.DataBind();
        }
        /// <summary>
        /// GEt Sorting direction
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<InventoryMgt.Entity.V_InventoryItem> Sort<TKey>(List<InventoryMgt.Entity.V_InventoryItem> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<InventoryMgt.Entity.V_InventoryItem>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<InventoryMgt.Entity.V_InventoryItem>();
            }
        }
        protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit1")
                {
                    hIsAddstock.Value = "0";
                    lblPnltitle.Text = "Update Stock";
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                    UpdateInventory();


                }
                if (e.CommandName == "AddStock")
                {
                    hIsAddstock.Value = "1";
                    lblPnltitle.Text = "Add Stock";
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                    UpdateInventory();

                }
                if (e.CommandName == "Transfer1")
                {
                    hIsAddstock.Value = "0";
                    lblTransferTitle.Text = "Transfer Stock";
                    int ID = Convert.ToInt32(e.CommandArgument);
                    htid.Value = ID.ToString();
                    TransferInventory();
                    MdlTransfer.Show();


                }
                else if (e.CommandName == "SendMail")
                {

                    //int ID = Convert.ToInt32(e.CommandArgument);

                    //var pBAL = new PortfolioMgt.BAL.PortfolioContactBAL();
                    //var flsdata = PortfolioMgt.BAL.MaintenanceBAL.MaintenanceSchedule_Select(sessionKeys.PortfolioID).Where(o => o.ID == ID).FirstOrDefault();
                    //// var userBAL = new UserMgt.BAL.ContractorsBAL();
                    ////userBAL.
                    //var pdata = pBAL.PortfolioContact_SelectByID(flsdata.RequesterID).FirstOrDefault();

                    //string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                    //Emailer em = new Emailer();
                    //string body = em.ReadFile("~/WF/CustomerAdmin/EmailTemplates/SendReminderEmail.htm");
                    //string subject = string.Empty;
                    //subject = "Maintenance alert";
                    //body = body.Replace("[mail_head]", "Maintenance Schedule");

                    //body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo());
                    //body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    //body = body.Replace("[user]", flsdata.RequesterName);
                    //body = body.Replace("[MaintenanceType]", flsdata.MaintenanceTypeName);
                    //body = body.Replace("[Equipment]", flsdata.EquipmentName);
                    //body = body.Replace("[Instance]", flsdata.Portfolio);
                    //body = body.Replace("[user]", flsdata.RequesterName);
                    //body = body.Replace("[number]", string.Empty);


                    //body = body.Replace("[img]", "<img src='" + Deffinity.systemdefaults.GetWebUrl() + string.Format("/WF/Admin/ImageHandler.ashx?type=customer&id={0}", flsdata.PortfolioID) + "'/>");


                    ////foreach (var s in pdata.Split(','))
                    ////{
                    //if (!string.IsNullOrEmpty(pdata.Email))
                    //{
                    //    em.SendingMail(fromemailid, subject, body, pdata.Email);
                    //    lblmsg.Text = "Mail has been sent successfully";
                    //}
                    //}
                }
                else if (e.CommandName == "Delete1")
                {
                    hIsAddstock.Value = "0";
                    int ID = Convert.ToInt32(e.CommandArgument);
                    var retval = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_DeleteByID(ID);
                    if (retval)
                    {
                        lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindGrid();
                    }
                }
                else if(e.CommandName == "Deployed")
                {
                    try
                    {

                        var inventoryID = Convert.ToInt32(e.CommandArgument.ToString());

                        var clist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_SelectByInventoryID(inventoryID).ToList();
                        var iList = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Select();
                        // List<InventoryMgt.Entity.V_InventoryItem> rlist = InventoryItemUsedList();
                        var jList = FLSDetailsBAL.Jqgridlist();
                        var retval = (from i in clist
                                          //join j in clist on i.ID equals j.InventoryID
                                      join j in jList on i.CallID equals j.CallID
                                      select new
                                      {
                                          DateDeployed = i.AllotmentDate.ToShortDateString(),
                                          ID = i.InventoryID,
                                          InventoryID = i.InventoryID,
                                          Qty = i.Quantity, //(clist.Where(o => o.InventoryID == i.ID).Select(o => o.Quantity).Sum() > 0 ? clist.Where(o => o.InventoryID == i.ID).Select(o => o.Quantity).Sum() : 0),
                                          Equipment = iList.Where(o => o.ID == i.InventoryID).FirstOrDefault().Equipment,
                                          StorageLocationName = iList.Where(o => o.ID == i.InventoryID).FirstOrDefault().StorageLocationName,
                                          JobRef = j.CallID,
                                          Customer = j.RequesterName,
                                          Address = j.RequestersAddress + ", " + j.RequestersTown + ", " + j.RequestersCity + ", " + j.RequestersPostCode

                                      }).ToList();

                        GridDeploy.DataSource = retval;
                        GridDeploy.DataBind();

                        mdlDeploy.Show();
                    }
                    catch(Exception ex)
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
        private void TransferInventory()
        {
            var mData = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_SelectByID(Convert.ToInt32(htid.Value));
            if (mData != null)
            {
                txtEquipmentTransfer.Text = mData.Equipment;
                txtTransferQty.Text = "1";
                
            }

        }
        private void UpdateInventory(bool IsAddStock=false)
        {
            var mData = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_SelectByID(Convert.ToInt32(hid.Value));
            if (mData != null)
            {
                ccdCategory.SelectedValue = mData.CategoryID.ToString();
                ddlSubCategory.DataBind();
                ccdSubCategory.SelectedValue = mData.SubCategoryID.ToString();
                //ddlCategory.SelectedValue = mData.CategoryID.ToString();
                //ddlSubCategory.SelectedValue = mData.SubCategoryID.ToString();
                ddlSupplier.SelectedValue = mData.SupplierID.ToString();
                //ddlStorageLocation.SelectedValue = mData.StorageLocationID.ToString();
                txtEquipment.Text = mData.Equipment != null?mData.Equipment:string.Empty;
                //1 is add stock value
                if(hIsAddstock.Value == "1")
                    txtQuantity.Text = "";
                else
                txtQuantity.Text = (mData.Quantity.HasValue ? mData.Quantity.Value : 0).ToString();
                txtAisle.Text = mData.Aisle;
                txtBin.Text = mData.Bin;
                txtReorderLevel.Text = (mData.ReorderLevel.HasValue ? mData.ReorderLevel.Value : 0).ToString();
                txtShelf.Text = mData.Shelf;
                txtFloor.Text = mData.Floor;
                //txtDateOfReminder.Text = mData.DateOfReminder.Value.ToShortDateString();
                //txtEquipment.Text = mData.EquipmentName;
                //txtReminderDescription.Text = mData.ReminderDescription;
                //txtRenewalAmount.Text = string.Format("{0:F2}", mData.RenewalAmount);
                //ddlMaintenanceType.SelectedValue = mData.MaintenanceTypeID.ToString();
                ddlStorageLocation.SelectedValue = mData.StorageLocationID.ToString();
                if ((mData.EquipmentID.HasValue ? mData.EquipmentID.Value : 0) > 0)
                    ShowEquipmentVisibile(string.Empty);
                else
                    ShowEquipmentVisibile(mData.Equipment != null ? mData.Equipment : string.Empty);
                //SetInternalSupplierChange();
                if (ddlSupplierEquipment.Visible)
                {
                    //casecadeMaterail.DataBind();
                    ddlSupplierEquipment.SelectedValue = mData.EquipmentID.HasValue ? mData.EquipmentID.Value.ToString() : "0";
                }
                ShowEquipmentVisibile(txtEquipment.Text.Trim());
                mdlExnter.Show();
            }
            else
            {
                ClearData();
            }
        }
        private void BindStorageLocation()
        {
            try
            {

                var wManagerlist = WarehouseDetailsBAL.WarehouseDetailsBAL_SelectAll().OrderBy(o => o.WarehouseName).ToList();
                ddlStorageLocation.DataSource = wManagerlist;
                ddlStorageLocation.DataValueField = "ID";
                ddlStorageLocation.DataTextField = "WarehouseName";
                ddlStorageLocation.DataBind();
                //To add Please select
                ddlStorageLocation.Items.Insert(0, new ListItem("Please select...", "0"));

                ddlTranStorageLocation.DataSource = wManagerlist;
                ddlTranStorageLocation.DataValueField = "ID";
                ddlTranStorageLocation.DataTextField = "WarehouseName";
                ddlTranStorageLocation.DataBind();
                //To add Please select
                ddlTranStorageLocation.Items.Insert(0, new ListItem("Please select...", "0"));

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindSuppliers()
        {
            try
            {
                var rdl = new RFIRepository<RFI.Entity.VendorDetails>();
                var wManagerlist = rdl.GetAll().Where(o=>o.CompanyID == sessionKeys.PortfolioID).OrderBy(o=>o.ContractorName).ToList();
                ddlSupplier.DataSource = wManagerlist;
                ddlSupplier.DataValueField = "VendorID";
                ddlSupplier.DataTextField = "ContractorName";
                ddlSupplier.DataBind();
                //To add Please select
                ddlSupplier.Items.Insert(0, new ListItem("Please select...", "0"));

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void ShowEquipmentVisibile(string Equimenttext)
        {
            if(Equimenttext.Length >0)
            {
                AddEquipmentView(true);
            }
            else
            {
                AddEquipmentView(false);
            }
        }
        private void AddEquipmentView(bool showtextbox)
        {
           
                txtEquipment.Visible = showtextbox;
                btnCancel.Visible = showtextbox;
                ddlSupplierEquipment.Visible = !showtextbox;
                btnShowText.Visible = !showtextbox;
        }
        protected void btnShowText_OnClick(object sender, EventArgs e)
        {
            AddEquipmentView(true);
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            AddEquipmentView(false);
        }
        protected void btnAddStock_OnClick(object sender, EventArgs e)
        {
            ClearData();
            mdlExnter.Show();
        }
        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            try
            {

                var mData = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_SelectByID(Convert.ToInt32(hid.Value));
                if (mData != null)
                {
                    mData.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    mData.SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    mData.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    mData.StorageLocationID = Convert.ToInt32(ddlStorageLocation.SelectedValue);
                    //mData.Equipment = txtEquipment.Text.Trim();
                    if (ddlSupplierEquipment.Visible)
                        mData.EquipmentID = Convert.ToInt32(ddlSupplierEquipment.SelectedValue);
                    else
                        mData.Equipment = txtEquipment.Text.Trim();
                    //1 is add stock value
                    if (hIsAddstock.Value == "1")
                        mData.Quantity = mData.Quantity + Convert.ToInt32(txtQuantity.Text);
                    else
                        mData.Quantity = Convert.ToInt32(txtQuantity.Text);

                    mData.Aisle = txtAisle.Text;
                    mData.Bin = txtBin.Text;
                    mData.Floor = txtFloor.Text;
                    mData.Shelf = txtShelf.Text;
                    mData.ReorderLevel = Convert.ToInt32(!string.IsNullOrEmpty(txtReorderLevel.Text) ? txtReorderLevel.Text : "0");

                    InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Update(mData);

                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    hIsAddstock.Value = "0";
                    ClearData();
                    mdlExnter.Hide();
                    BindGrid();
                }
                else
                {
                    mData = new InventoryMgt.Entity.InventoryItem();
                    mData.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    mData.SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    mData.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    mData.StorageLocationID = Convert.ToInt32(ddlStorageLocation.SelectedValue);
                    if (ddlSupplierEquipment.Visible)
                        mData.EquipmentID = Convert.ToInt32(ddlSupplierEquipment.SelectedValue);
                    else
                    mData.Equipment = txtEquipment.Text.Trim();
                    mData.Quantity = Convert.ToInt32(txtQuantity.Text);
                    mData.Aisle = txtAisle.Text;
                    mData.Bin = txtBin.Text;
                    mData.Floor = txtFloor.Text;
                    mData.Shelf = txtShelf.Text;
                    mData.ReorderLevel = Convert.ToInt32(!string.IsNullOrEmpty(txtReorderLevel.Text) ? txtReorderLevel.Text : "0");

                    InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Add(mData);

                    lblmsg.Text = Resources.DeffinityRes.Addedsuccessfully;
                    ClearData();
                    mdlExnter.Hide();
                    BindGrid();
                }
                // Storage_AddUpdate(Convert.ToInt32(hbomid.Value), Convert.ToInt32(ddlsiteInSearch.SelectedValue), Convert.ToInt32(ddlWareshouse.SelectedValue), Convert.ToDouble(txtQtyReceived.Text));
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        protected void btnTransfer_OnClick(object sender, EventArgs e)
        {
            try
            {
                var mData = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_SelectByID(Convert.ToInt32(htid.Value));
                if (mData != null)
                {
                    string outMsg = string.Empty;
                    var retval = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsTransfer_Add(Convert.ToInt32(htid.Value), Convert.ToInt32(txtTransferQty.Text), Convert.ToInt32(ddlTranStorageLocation.SelectedValue), out outMsg);
                    if (!retval)
                    {
                        if (!string.IsNullOrEmpty(outMsg))
                            Session["errormsg"] = outMsg;
                    }
                    else
                    {
                        Session["msg"] = "Transferred successfully";
                    }
                    htid.Value = "0";
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch(Exception ex )
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void ClearData()
        {
            //BindSuppliers();
            ccdVendors.DataBind();
            BindStorageLocation();
            ccdCategory.DataBind();
            ccdSubCategory.DataBind();
            // casecadeMaterail.DataBind();
           
            ddlSupplier.SelectedIndex = 0;
            BindEquipemnt();
            //ddlStorageLocation.SelectedIndex = 0;
            txtEquipment.Text = string.Empty;

            txtQuantity.Text = "1";
            txtFloor.Text = string.Empty;
            txtAisle.Text = string.Empty;
            txtBin.Text = string.Empty;
            txtReorderLevel.Text = "0";
            txtShelf.Text = string.Empty;
            hid.Value = "0";
            lblPnltitle.Text = "Add Stock";
            //SetInternalSupplierChange();
            ShowEquipmentVisibile(txtEquipment.Text.Trim());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEquipemnt();
            //SetInternalSupplierChange();
            ShowEquipmentVisibile(txtEquipment.Text.Trim());
        }

        private void SetInternalSupplierChange()
        {
            sessionKeys.VendorID = Convert.ToInt32(ddlSupplier.SelectedValue);
            if (ddlSupplier.SelectedItem.Text.ToString().Length >0)
            {
                txtEquipment.Visible = true;
                ddlSupplierEquipment.Visible = false;
                btnShowText.Visible = false;
            }
            else
            {
                txtEquipment.Visible = false;
                ddlSupplierEquipment.Visible = true;
                btnShowText.Visible = true;
                //BindSuppplierMaterialData();
            }
        }
        //ddlSupplier

        string name = "Internal";
        string password = "Internal@123";
        public bool Supplier_InternalExists()
        {
            bool retval = false;
            IRFIRepository<RFI.Entity.VendorDetails> vRep = new RFIRepository<RFI.Entity.VendorDetails>();
            var vEntity = vRep.GetAll().Where(o =>o.CompanyID == sessionKeys.PortfolioID && o.ContractorName.ToLower() == name.ToLower()).FirstOrDefault();
            if(vEntity != null)
            {
                retval = true;
            }

            return retval;
        }
        public void Supplier_AddInternal()
        {
            try
            {
                if (!Supplier_InternalExists())
                {
                    int vendorID = 0;
                    RFI_Vendor _rfiVendor = new RFI_Vendor();
                    _rfiVendor.CONTRACTORNAME = name;
                    _rfiVendor.LOGINAME = name;
                    _rfiVendor.PASSWORD = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                    _rfiVendor.ADDRESS = string.Empty;
                    _rfiVendor.COMPANY = "";//txtCompany.Text.Trim();
                    _rfiVendor.EMAILADDRESS = string.Empty;
                    _rfiVendor.HQADDRESS = "";// txtHQAddress.Text.Trim();
                    _rfiVendor.POSTCODE = string.Empty;
                    _rfiVendor.REGNO = string.Empty;
                    _rfiVendor.VATNO = string.Empty;
                    _rfiVendor.SKILLS = string.Empty;

                    if (vendorID == 0)
                    {
                        vendorID = Deffinity.BLL.RFI_Vendor_SVC.Insert(_rfiVendor);
                        AddUsertoCompany(vendorID);
                        //BindSuppliers();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private void AddUsertoCompany(int VendorID)
        {
            try
            {
                if (sessionKeys.PortfolioID > 0)
                {
                    IRFIRepository<RFI.Entity.RFI_Vendor> rRep = new RFIRepository<RFI.Entity.RFI_Vendor>();

                    var rData = rRep.GetAll().Where(o => o.VendorID == VendorID).FirstOrDefault();
                    if (rData != null)
                    {

                        IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        var uEntity = uRep.GetAll().Where(o => o.UserID == rData.ContractorID && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                        if (uEntity == null)
                        {
                            uEntity = new UserMgt.Entity.UserToCompany();
                            uEntity.CompanyID = sessionKeys.PortfolioID;
                            uEntity.UserID = rData.ContractorID;
                            uRep.Add(uEntity);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEquipemnt();
        }
      
        private void BindEquipemnt()
        {
            try
            {
                int categoryId = Convert.ToInt32(!string.IsNullOrEmpty(ddlSubCategory.SelectedValue)? ddlSubCategory.SelectedValue:"0" );
                int VendorID = Convert.ToInt32(!string.IsNullOrEmpty(ddlSupplier.SelectedValue)?ddlSupplier.SelectedValue:"0");

                IPortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material> mRep = new PortfolioRepository<PortfolioMgt.Entity.ServiceCatelog_Material>();

                var subCategoyList = mRep.GetAll().Where(o => o.SubCategory == categoryId && o.Supplier == VendorID).ToList();
                if (subCategoyList.Count > 0)
                {
                    var result = (from p in subCategoyList
                                  select new { value = p.ID.ToString(), name = p.ItemDescription }).ToList();
                    ddlSupplierEquipment.DataSource = result;
                    ddlSupplierEquipment.DataTextField = "name";
                    ddlSupplierEquipment.DataValueField = "value";
                    ddlSupplierEquipment.DataBind();
                }
                ddlSupplierEquipment.Items.Insert(0, new ListItem("Please select...", "0"));
                // return result;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}