using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.BAL;
using System.Reflection;

namespace DeffinityAppDev.WF.DC
{
    public partial class DCInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (QueryStringValues.CCID > 0)
                    lblTitle.InnerText = "Job Reference " + QueryStringValues.CCID;
                else
                    lblTitle.InnerText = Resources.DeffinityRes.ServiceDesk;
                BindSuppliers();
                BindStorageLocation();
                BindGrid();
                BindAssignedGrid();
                ClearData();
            }
        }
        private void BindGrid()
        {
            try
            {
                List<InventoryMgt.Entity.V_InventoryItem> rlist = InventoryItemList();
                GridList.DataSource = rlist;
                GridList.DataBind();

                if(GridList.Rows.Count >0)
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
        private void BindAssignedGrid()
        {
            try
            {
                var clist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_SelectByCallID(QueryStringValues.CallID).ToList();
                var iList = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Select();
                // List<InventoryMgt.Entity.V_InventoryItem> rlist = InventoryItemUsedList();
                var retval = (from i in clist
                                  //join j in clist on i.ID equals j.InventoryID
                              select new
                              {
                                  ID= i.InventoryID,
                                  InventoryID = i.InventoryID,
                                  QtyUsed = i.Quantity, //(clist.Where(o => o.InventoryID == i.ID).Select(o => o.Quantity).Sum() > 0 ? clist.Where(o => o.InventoryID == i.ID).Select(o => o.Quantity).Sum() : 0),
                                  Equipment = iList.Where(o => o.ID == i.InventoryID).FirstOrDefault().Equipment,
                                  StorageLocationName = iList.Where(o => o.ID == i.InventoryID).FirstOrDefault().StorageLocationName,
                              }).ToList();
                
                //var clist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_SelectByCallID(QueryStringValues.CallID).ToList();
                //var iList = InventoryItemUsedList();
                // List<InventoryMgt.Entity.V_InventoryItem> rlist = InventoryItemUsedList();
                GridAS.DataSource = retval;
                GridAS.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private List<InventoryMgt.Entity.V_InventoryItem> InventoryItemList()
        {
            var rlist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Select();
            try
            {
                string searchtext = txtSearch.Text.Trim();

                if (Convert.ToInt32(ddlStorageLocation.SelectedValue) > 0)
                    rlist = rlist.Where(o => o.StorageLocationID == Convert.ToInt32(ddlStorageLocation.SelectedValue)).ToList();
                //if (!string.IsNullOrEmpty(txtTodate.Text.Trim()))
                //    rlist = rlist.Where(o => o.DateOfReminder <= Convert.ToDateTime(txtTodate.Text.Trim())).ToList();
                if (!string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
                {
                    rlist = rlist.Where(p =>
                (p.CategoryName != null ? p.CategoryName.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.Equipment != null ? p.Equipment.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.SupplierName != null ? p.SupplierName.ToLower().Contains(searchtext.ToLower()) : false)
                || (p.SubCategoryName != null ? p.SubCategoryName.ToLower().Contains(searchtext.ToLower()) : false)

                ).OrderBy(p => p.CategoryName).Select(p => p).ToList();
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return rlist;
        }
        //private List<InventoryMgt.Entity.V_InventoryItem> InventoryItemUsedList()
        //{
        //    var clist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_SelectByCallID(QueryStringValues.CallID).ToList();
        //    var iList = InventoryItemUsedList();
        //    // List<InventoryMgt.Entity.V_InventoryItem> rlist = InventoryItemUsedList();
        //    var retval = (from i in clist
        //                      //join j in clist on i.ID equals j.InventoryID
        //                  select new 
        //                         {
        //                            InventoryID = i.InventoryID,
        //                            Qty = (clist.Where(o => o.InventoryID == i.ID).Select(o => o.Quantity).Sum() >0 ? clist.Where(o => o.InventoryID == i.ID).Select(o => o.Quantity).Sum() : 0),
        //                      Equipment = iList.Where(o=>o.ID == i.InventoryID).FirstOrDefault().Equipment,
        //                      StorageLocationName = iList.Where(o => o.ID == i.InventoryID).FirstOrDefault().StorageLocationName,
        //                  }).ToList();
        //    return retval;
        //    // var clist = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_SelectByCallID(QueryStringValues.CallID).ToList();

        //    // var iList = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Select().Where(o=>o.QtyUsed >0).ToList();


        //    ////rlist =  (from i in iList
        //    ////  join j in clist on i.ID equals j.InventoryID
        //    ////  select i).ToList();
        //    // try
        //    // {
        //    //     //string searchtext = txtSearch.Text.Trim();

        //    //     //if (Convert.ToInt32(ddlStorageLocation.SelectedValue) > 0)
        //    //     //    rlist = rlist.Where(o => o.StorageLocationID == Convert.ToInt32(ddlStorageLocation.SelectedValue)).ToList();
        //    //     ////if (!string.IsNullOrEmpty(txtTodate.Text.Trim()))
        //    //     ////    rlist = rlist.Where(o => o.DateOfReminder <= Convert.ToDateTime(txtTodate.Text.Trim())).ToList();
        //    //     //if (!string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
        //    //     //{
        //    //     //    rlist = rlist.Where(p =>
        //    //     //(p.CategoryName != null ? p.CategoryName.ToLower().Contains(searchtext.ToLower()) : false)
        //    //     //|| (p.Equipment != null ? p.Equipment.ToLower().Contains(searchtext.ToLower()) : false)
        //    //     //|| (p.SupplierName != null ? p.SupplierName.ToLower().Contains(searchtext.ToLower()) : false)
        //    //     //|| (p.SubCategoryName != null ? p.SubCategoryName.ToLower().Contains(searchtext.ToLower()) : false)

        //    //     //).OrderBy(p => p.CategoryName).Select(p => p).ToList();
        //    //     //}


        //    // }
        //    // catch (Exception ex)
        //    // {
        //    //     LogExceptions.WriteExceptionLog(ex);
        //    // }
        //    // return rlist;
        //}
        
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
                    //lblPnltitle.Text = "Update Stock";
                    int ID = Convert.ToInt32(e.CommandArgument);
                    hid.Value = ID.ToString();
                    var mData = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Select().Where(o=>o.ID==Convert.ToInt32(hid.Value)).FirstOrDefault();
                    if (mData != null)
                    {
                        //ddlCategory.SelectedValue = mData.CategoryID.ToString();
                        //ddlSubCategory.SelectedValue = mData.SubCategoryID.ToString();
                        //ddlSupplier.SelectedValue = mData.SupplierID.ToString();
                        //ddlStorageLocation.SelectedValue = mData.StorageLocationID.ToString();
                        txtEquipment.Text = mData.Equipment.ToString();
                        txtQuantity.Text = "1";//(mData.Quantity.HasValue ? mData.Quantity.Value : 0).ToString();
                        //txtDateOfReminder.Text = mData.DateOfReminder.Value.ToShortDateString();
                        //txtEquipment.Text = mData.EquipmentName;
                        //txtReminderDescription.Text = mData.ReminderDescription;
                        //txtRenewalAmount.Text = string.Format("{0:F2}", mData.RenewalAmount);
                        //ddlMaintenanceType.SelectedValue = mData.MaintenanceTypeID.ToString();
                        mdlExnter.Show();
                    }
                    else
                    {
                        ClearData();
                    }
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
                    int ID = Convert.ToInt32(e.CommandArgument);
                    var retval = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_DeleteByID(ID);
                    if (retval)
                    {
                        lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindGrid();
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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
                //var rdl = new RFIRepository<RFI.Entity.VendorDetails>();
                //var wManagerlist = rdl.GetAll().OrderBy(o => o.ContractorName).ToList();
                //ddlSupplier.DataSource = wManagerlist;
                //ddlSupplier.DataValueField = "VendorID";
                //ddlSupplier.DataTextField = "ContractorName";
                //ddlSupplier.DataBind();
                ////To add Please select
                //ddlSupplier.Items.Insert(0, new ListItem("Please select...", "0"));

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            try
            {
               
                var mData = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_SelectByID(Convert.ToInt32(hid.Value));
                if (mData != null)
                {

                    InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_Add(Convert.ToInt32(hid.Value), Convert.ToInt32(txtQuantity.Text), QueryStringValues.CallID);
                    ////mData.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    ////mData.SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    //mData.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
                    //mData.StorageLocationID = Convert.ToInt32(ddlStorageLocation.SelectedValue);
                    //mData.Equipment = txtEquipment.Text.Trim();
                    //mData.Quantity = Convert.ToInt32(txtQuantity.Text);


                    //InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsBAL_Update(mData);

                    lblmsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    ClearData();
                    mdlExnter.Hide();
                    BindGrid();
                    BindAssignedGrid();
                }
               
                // Storage_AddUpdate(Convert.ToInt32(hbomid.Value), Convert.ToInt32(ddlsiteInSearch.SelectedValue), Convert.ToInt32(ddlWareshouse.SelectedValue), Convert.ToDouble(txtQtyReceived.Text));
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }

        private void ClearData()
        {
            //ddlCategory.DataBind();
            //ddlSubCategory.DataBind();
            //ddlSupplier.SelectedIndex = 0;
            ddlStorageLocation.SelectedIndex = 0;
            txtEquipment.Text = string.Empty;

            txtQuantity.Text = "1";
            hid.Value = "0";
            //lblPnltitle.Text = "Add Stock";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GridAS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridAS.PageIndex = e.NewPageIndex;
            BindAssignedGrid();
        }

        protected void GridAS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit1")
                {
                   
                }
                
                else if (e.CommandName == "Delete1")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    var retval = InventoryMgt.BAL.InventoryItemsBAL.InventoryItemsAlloatment_Delete(ID,QueryStringValues.CallID);
                    if (retval)
                    {
                        lblmsg.Text = Resources.DeffinityRes.Deletedsuccessfully;
                        BindAssignedGrid();
                        BindGrid();
                    }
                }


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        //protected void GridAS_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    string Sortdir = GetSortDirection(e.SortExpression);
        //    string SortExp = e.SortExpression;
        //    var list = InventoryItemUsedList();
        //    if (Sortdir == "ASC")
        //    {
        //        list = Sort<InventoryMgt.Entity.V_InventoryItem>(list, SortExp, SortDirection.Ascending);
        //    }
        //    else
        //    {
        //        list = Sort<InventoryMgt.Entity.V_InventoryItem>(list, SortExp, SortDirection.Descending);
        //    }
        //    this.GridAS.DataSource = list;
        //    this.GridAS.DataBind();
        //}
    }
}