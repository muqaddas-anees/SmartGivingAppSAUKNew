using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using Deffinity.ProgrammeManagers;
using ProjectMgt.Entity;
using UserMgt.DAL;
using InventoryMgt.DAL;
using InventoryMgt.Entity;
public partial class controls_PTMaterialTracker : System.Web.UI.UserControl
{

    double total = 0;
    double spentToDate = 0;
    double totalCostRemaining = 0;
    public DateTime dateRec;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindWorksheet();
            BindGrid();
            CheckUserRole();
            CommandField();
        }
    }
    private void BindGrid()
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var materialList = (from r in db.ProjectBOMDetils
                                    join b in db.BOM_Types on r.WorkSheetID equals b.ID
                                    where r.ProjectReference == QueryStringValues.Project && r.Material != 0 && r.ID != -99
                                    select new
                                    {
                                        ID = r.ID,
                                        r.Worksheet,
                                        r.WorkSheetID,
                                        r.Material,
                                        r.Qty, //Bom Quantities
                                        r.Description,
                                        r.NextShipmentDate,
                                        r.ExpectedShipmentDate,
                                        r.Notes,
                                        QtyReceived = r.QtyReceived.HasValue ? r.QtyReceived : 0,
                                        Total = r.Qty * r.Material, // BoM Quantities * Unit cost
                                        OrderToDate = (r.Material * (r.QtyReceived.HasValue ? r.QtyReceived : 0)),
                                        OrderLeft = (r.Qty - (r.QtyReceived.HasValue ? r.QtyReceived : 0)),


                                    }).ToList();


                if (ddlWorksheet.SelectedValue != "0")
                {
                    materialList = materialList.Where(l => l.WorkSheetID == Convert.ToInt32(ddlWorksheet.SelectedValue)).ToList();
                }
                if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                {
                    materialList = materialList.Where(l => l.Description.ToLower().Contains(txtDescription.Text.ToLower())).ToList();
                }
                total = materialList.Select(m => m.Total).Sum();
                spentToDate = Convert.ToDouble(materialList.Select(m => m.OrderToDate).Sum());
                totalCostRemaining = total - spentToDate;


                gvMaterial.DataSource = materialList;
                gvMaterial.DataBind();

                //Top section 
                lblMaterialsCost.Text = total.ToString("C");
                lblSpentToDate.Text = spentToDate.ToString("C");
                lblCostRemaining.Text = totalCostRemaining.ToString("C");


            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region "Permission Code Here"
    private void CommandField()
    {

        try
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    imgUpdate.Visible = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    imgUpdate.Visible = false;

                    // Disable();

                }

            }
            if (Request.Url.ToString().ToLower().Contains("projecttracker_actuals.aspx"))
            {
                imgUpdate.Visible = false;
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }
    private void CheckUserRole()
    {

        if (sessionKeys.SID != 1)
        {
            int role = 0;
            role = Admin.CheckLoginUserPermission(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }
            role = Admin.GetTeamID(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }

        }

    }

    private void Disable()
    {
        //.Enabled = false;
       // Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";

    }
    #endregion

    private void BindWorksheet()
    {
        ddlWorksheet.DataSource = Deffinity.Worksheet.Worksheet_SelectAll(QueryStringValues.Project);
        ddlWorksheet.DataTextField = "TypeName";
        ddlWorksheet.DataValueField = "ID";
        ddlWorksheet.DataBind();
        ddlWorksheet.Items.Insert(0, new ListItem("Select All...", "0"));
    }

    protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //total += double.Parse(DataBinder.Eval(e.Row.DataItem, "Total").ToString());
                //spentToDate += double.Parse(DataBinder.Eval(e.Row.DataItem, "OrderToDate").ToString());
                LinkButton imgApplyDate = (LinkButton)e.Row.FindControl("imgApplyDate");
                LinkButton imgBarcode = (LinkButton)e.Row.FindControl("imgStorageDetails");
                LinkButton imgHistory = (LinkButton)e.Row.FindControl("imgHis");
                if (e.Row.RowIndex == 0)
                {
                    imgApplyDate.Visible = true;
                }
                else
                {
                    imgApplyDate.Visible = false;
                }
                if(Request.Url.ToString().ToLower().Contains("projecttracker_actuals.aspx"))
                {
                    imgBarcode.Visible=false;
                    imgHistory.Visible = false;
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                //Footer row binding
                Label lblfTotal = (Label)e.Row.FindControl("lblfTotal");
                lblfTotal.Text = total.ToString("C");
                Label lblfOrderToDate = (Label)e.Row.FindControl("lblfOrderToDate");
                lblfOrderToDate.Text = spentToDate.ToString("C");

               

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        bool dateError = false;
        bool qtyError = false;
        using (projectTaskDataContext project = new projectTaskDataContext())
        {
            int countRow = gvMaterial.Rows.Count;
            for (int i = 0; i < countRow; i++)
            {

                GridViewRow row = gvMaterial.Rows[i];
                TextBox txtDateReceived = (TextBox)row.FindControl("txtDateReceived");
                Label txtQtyOrdered = (Label)row.FindControl("lblBOMQuantities");
                Label lblWorksheetId = (Label)row.FindControl("lblWorksheetID");
                Label lblDescription = (Label)row.FindControl("lblDescription");
                TextBox txtQtyReceived = (TextBox)row.FindControl("txtQtyReceived");
                TextBox txtNextExpectedDate = (TextBox)row.FindControl("txtNextExpectedDate");
                TextBox Comments = (TextBox)row.FindControl("txtComments");

                double qtyReceived = double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text);
                Label lblID = (Label)row.FindControl("lblID");
                ProjectMgt.Entity.GoodsReceipt gr = new ProjectMgt.Entity.GoodsReceipt();
                var IsExist = (from r in project.GoodsReceipts
                               where r.BOMID == int.Parse(lblID.Text)
                               select r).ToList();
                if (IsExist != null)
                {
                    if (IsExist.Count == 0)
                    {

                        if (qtyReceived > 0 && txtDateReceived.Text == "")
                        {
                            dateError = true;
                        }
                        else
                        {
                            if (txtDateReceived.Text != "" && qtyReceived > 0)
                            {
                                if (double.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text) < qtyReceived)
                                {
                                    qtyError = true;
                                   
                                }
                                else
                                {
                                    ProjectMgt.Entity.ProjectBOM Update =
                           project.ProjectBOMs.Single(P => P.ID == int.Parse(lblID.Text));
                                    Update.Qty = double.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                    project.SubmitChanges();

                                    gr.BOMID = int.Parse(lblID.Text);
                                    gr.ExpectedShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                        DateTime.Now.ToShortDateString() : txtDateReceived.Text);
                                    gr.NextShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextExpectedDate.Text) ?
                                        DateTime.Now.ToShortDateString() : txtNextExpectedDate.Text);
                                    gr.QtyOrdered = double.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                    gr.QtyReceived = double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text);
                                    gr.Notes = Comments.Text;
                                    gr.OutStandingQty = gr.QtyOrdered - gr.QtyReceived;
                                    project.GoodsReceipts.InsertOnSubmit(gr);
                                    project.SubmitChanges();


                                   

                                    if (CheckDetailsChanged(Convert.ToInt32(lblID.Text), double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text)))
                                    {
                                        //Journal Insert
                                        JournalInsert(Convert.ToInt32(lblID.Text), int.Parse(lblWorksheetId.Text), lblDescription.Text, double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text));

                                    }
                                }
                            }
                        }



                    }
                    else
                    {

                        if (qtyReceived > 0 && txtDateReceived.Text == "")
                        {
                            dateError = true;
                        }
                        else
                        {
                            if (txtDateReceived.Text != "" && qtyReceived > 0)
                            {
                                if (double.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text) < qtyReceived)
                                {
                                    qtyError = true;
                                   
                                }
                                else
                                {
                                    ProjectMgt.Entity.ProjectBOM UpdatePO =
                           project.ProjectBOMs.Single(P => P.ID == int.Parse(lblID.Text));
                                    UpdatePO.Qty = double.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                    project.SubmitChanges();



                                    ProjectMgt.Entity.GoodsReceipt Update =
                                     project.GoodsReceipts.Single(P => P.BOMID == int.Parse(lblID.Text));

                                    Update.ExpectedShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateReceived.Text) ?
                                         DateTime.Now.ToShortDateString() : txtDateReceived.Text);
                                    Update.NextShipmentDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextExpectedDate.Text) ?
                                        DateTime.Now.ToShortDateString() : txtNextExpectedDate.Text);
                                    Update.QtyOrdered = double.Parse(string.IsNullOrEmpty(txtQtyOrdered.Text) ? "0" : txtQtyOrdered.Text);
                                    Update.QtyReceived = double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text);
                                    Update.Notes = Comments.Text;
                                    Update.OutStandingQty = Update.QtyOrdered - Update.QtyReceived;
                                    project.SubmitChanges();

                                   
                                    if (CheckDetailsChanged(Convert.ToInt32(lblID.Text), double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text)))
                                    {
                                        //journal
                                        JournalInsert(Convert.ToInt32(lblID.Text), int.Parse(lblWorksheetId.Text), lblDescription.Text, double.Parse(string.IsNullOrEmpty(txtQtyReceived.Text) ? "0" : txtQtyReceived.Text));
                                    }
                                }
                            }
                        }


                    }
                }


            }
            if (qtyError || dateError)
            {
                if (qtyError)
                {
                    lblRes.Text = "You have entered a received value greater than the original order";
                    lblRes.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblRes.Text = "Please enter date received";
                    lblRes.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
               
                lblRes.Text = "Updated successfully.";
                lblRes.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
        }
      
    }

    private bool CheckDetailsChanged(int id, double value)
    {
        bool changed = false;
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {

                ProjectTrackerJournal journal = db.ProjectTrackerJournals.Where(j => j.BOMID == id && j.SectionType == "Material").OrderByDescending(j => j.ModifiedDate).FirstOrDefault();
                if (journal != null)
                {
                    if (journal.ValueNow != value)
                        changed = true;

                }
                else
                {
                    //first time
                    changed = true;
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return changed;
    }
    private void JournalInsert(int id, int worksheetId, string description, double valueNow)
    {

        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            double priviousValue = 0;
            ProjectTrackerJournal journal = db.ProjectTrackerJournals.Where(j => j.BOMID == id).OrderByDescending(j => j.ModifiedDate).FirstOrDefault();
            if (journal != null)
            {
                priviousValue = double.Parse(journal.ValueNow.ToString());
            }

            //journal 
            ProjectTrackerJournal projectTrackerJournal = new ProjectTrackerJournal();
            projectTrackerJournal.BOMID = id;
            projectTrackerJournal.WorksheetID = worksheetId;
            projectTrackerJournal.Description = description;
            projectTrackerJournal.PreviousValue = priviousValue;
            projectTrackerJournal.ValueNow = valueNow;
            projectTrackerJournal.SectionType = "Material";
            projectTrackerJournal.ModifiedBy = sessionKeys.UID;
            projectTrackerJournal.ModifiedDate = DateTime.Now;
            db.ProjectTrackerJournals.InsertOnSubmit(projectTrackerJournal);
            db.SubmitChanges();
        }
    }
    protected void btn_ApplyDate_OnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnDetails = sender as LinkButton;
            GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
            //HIndent
            dateRec = Convert.ToDateTime(((TextBox)row.FindControl("txtDateReceived")).Text);

            int CountRow = gvMaterial.Rows.Count;
            for (int i = 0; i < CountRow; i++)
            {

                GridViewRow Row = gvMaterial.Rows[i];
                TextBox txtDateReceived = (TextBox)Row.FindControl("txtDateReceived");

                TextBox txtNextExpectedDate = (TextBox)Row.FindControl("txtNextExpectedDate");

                txtDateReceived.Text = string.Format("{0:d}", dateRec);
                txtNextExpectedDate.Text = string.Format("{0:d}", dateRec);
            }


        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }



    }
    protected void gvMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "History")
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            mdlpopupHisstory.Show();
            BindHistory(id);
        }
        if (e.CommandName == "Storage")
        {

            GridViewRow rw = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblId = (Label)rw.FindControl("lblID");
            TextBox txtQtyReceived = (TextBox)rw.FindControl("txtQtyReceived");

            int bomId = int.Parse(lblId.Text);
            hfBOMID.Value = bomId.ToString();
            sessionKeys.Project = Convert.ToInt32(Request.QueryString["project"].ToString());
            //hfCategoryID.Value = category;
            //hfSubcategoryID.Value = string.IsNullOrEmpty(subCategory) ? "0" : subCategory;
            //hfPartNumber.Value = partNumber;
            //hfDescription.Value = description;
            txtQty.Text = txtQtyReceived.Text;
            //hfBarcode.Value = barcode;
            //GoodsStorageDetailsInsert(qtyReceived,bomId); 
            BindStorageDetails(bomId);
            mdlPopupStorage.Show();
            lblMsg.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            lblError.Text = string.Empty;
            lblStockError.Text = string.Empty;
            ccdCategory1.SelectedValue = "0";
            ccdSite.SelectedValue = "0";



        }
    }
    private void BindHistory(int id)
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var contractorList = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();
                    var jList = db.ProjectTrackerJournals.Where(j => j.BOMID == id && j.SectionType.ToLower() == "material").Select(j => j).ToList();
                    var bomList = db.ProjectBOMDetils.Where(b => b.ID == id).ToList();
                    var journalList = (from p in jList
                                       join c in contractorList on p.ModifiedBy equals c.ID
                                       join b in bomList on p.BOMID equals b.ID
                                       orderby p.ModifiedDate descending
                                       select new { p.ID, p.WorksheetID, b.Worksheet, p.PreviousValue, p.ValueNow, UserName = c.ContractorName, p.Description, p.ModifiedDate }).ToList();

                    GvHistory.DataSource = journalList;
                    GvHistory.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindStorageDetails(int bomId)
    {

        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                //var storageList = (from s in db.Sites
                //                   join g in db.GoodsStorageDetails
                //                       on s.ID equals g.SiteID join u in db.Inventory_Uses on g.UseID equals u.ID 
                //                   where g.BOMID == bomId
                //                   select new {ID=g.ID, SiteId=g.SiteID,InventoryID= g.InventoryID,UseId=g.UseID, Site = s.Site1, Qty = g.Qty ,UseCode= u.Name}).ToList();
                var storageList = (from s in db.Sites
                                   join g in db.GoodsStorageDetails
                                       on s.ID equals g.SiteID
                                   where g.BOMID == bomId
                                   select new { ID = g.ID, SiteId = g.SiteID, InventoryID = g.InventoryID, Site = s.Site1, Qty = g.Qty, CategoryId = g.CategoryId, SubCategoryId = g.SubCategoryId, Product = g.Product }).ToList();
                gvStorage.DataSource = storageList;
                gvStorage.DataBind();
                if (storageList.Count() > 0)
                    btnAddToInventory.Visible = true;
                else
                    btnAddToInventory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        try
        {
            int bomId = int.Parse(hfBOMID.Value);
            if (bomId != 0)
            {
                using (InventoryDataContext db = new InventoryDataContext())
                {
                    InventoryMgt.Entity.GoodsReceipt goodsReceipt = db.GoodsReceipts.Where(g => g.BOMID == bomId).Select(g => g).FirstOrDefault();
                    if (goodsReceipt != null)
                    {
                        int sumOfQty = 0;
                        var gs = db.GoodsStorageDetails.Where(g => g.BOMID == bomId).Select(g => g).ToList();
                        if (gs.Count > 0)
                        {
                            sumOfQty = gs.Sum(g => Convert.ToInt32(g.Qty));
                        }
                        int totalQty = sumOfQty + int.Parse(txtQty.Text);
                        if (totalQty <= goodsReceipt.QtyReceived)
                        {
                            lblMsg.Text = string.Empty;
                            GoodsStorageDetail goodsStorageDetail = new GoodsStorageDetail();
                            goodsStorageDetail.BOMID = bomId;
                            goodsStorageDetail.SiteID = int.Parse(ddlSite.SelectedValue);
                            goodsStorageDetail.Qty = int.Parse(txtQty.Text);
                            goodsStorageDetail.CategoryId = int.Parse(ddlCategory.SelectedValue);
                            goodsStorageDetail.SubCategoryId = int.Parse(ddlSubCategory.SelectedValue);
                            goodsStorageDetail.Product = ddlProduct.SelectedItem.Text;
                            goodsStorageDetail.UseID = 0;
                            goodsStorageDetail.InventoryID = 0;
                            db.GoodsStorageDetails.InsertOnSubmit(goodsStorageDetail);
                            db.SubmitChanges();

                            using (projectTaskDataContext pd = new projectTaskDataContext())
                            {
                                ProjectMgt.Entity.ProjectBOM projectBOM = pd.ProjectBOMs.Where(p => p.ID == bomId).Select(p => p).FirstOrDefault();
                                if (projectBOM != null)
                                {
                                    projectBOM.Description = ddlProduct.SelectedItem.Text;
                                    pd.SubmitChanges();
                                }


                            }


                        }
                        else
                        {
                            lblMsg.Text = "You have entered a qty value greater than the quantity received";

                        }
                    }
                    else
                    {
                        lblMsg.Text = "There is no quantity received available. Please check and try again.";
                    }
                }
            }
            //ccdSite.SelectedValue = "0";

            BindStorageDetails(bomId);

            mdlPopupStorage.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {

            lblSuccess.Text = string.Empty;
            lblError.Text = string.Empty;
            lblStockError.Text = string.Empty;
            string successMsg = string.Empty;
            string errorMsg = string.Empty;
            bool added = false;
            int portfolioID = InventoryBAL.GetPortfolioId();
            //if (hfCategoryID.Value == "0" && hfSubcategoryID.Value == "0")
            //{
            //    InventoryBAL.CategoryInsert();
            //}
            //int categoryId = hfCategoryID.Value == "0" ? InventoryBAL.GetCategoryID() : int.Parse(hfCategoryID.Value);
            //int subCategoryId = hfSubcategoryID.Value == "0" ? InventoryBAL.GetSubCategoryID() : int.Parse(hfSubcategoryID.Value);
            using (InventoryDataContext db = new InventoryDataContext())
            {
                List<Inventory_SiteStorageDetail> sList = new List<Inventory_SiteStorageDetail>();
                for (int i = 0; i < gvStorage.Rows.Count; i++)
                {
                    GridViewRow row = gvStorage.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkInventory")).Checked;

                    if (isChecked)
                    {
                        string siteId = ((Label)row.FindControl("lblSiteId")).Text.Trim();
                        string Id = ((Label)row.FindControl("lblSiteId")).Text.Trim();
                        //string useId = ((Label)row.FindControl("lblUseId")).Text.Trim();
                        string site = ((Label)row.FindControl("lblSite")).Text.Trim();
                        int categoryId = int.Parse(((Label)row.FindControl("lblCategoryId")).Text.Trim());
                        int subCategoryId = int.Parse(((Label)row.FindControl("lblSubCategoryId")).Text.Trim());
                        string product = ((Label)row.FindControl("lblProduct")).Text.Trim();
                        string qty = ((Label)row.FindControl("lblQty")).Text.Trim();
                        string inventoryId = ((Label)row.FindControl("lblInventoryId")).Text.Trim();

                        string id = ((Label)row.FindControl("lblId")).Text.Trim();
                        string projectPrefix = string.Empty;
                        if (inventoryId == "0")
                        {
                            using (projectTaskDataContext pt = new projectTaskDataContext())
                            {
                                ProjectDefault projectDefault = pt.ProjectDefaults.Select(p => p).FirstOrDefault();
                                if (projectDefault != null)
                                    projectPrefix = projectDefault.ProjectReferencePrefix;
                            }
                            // InventoryManager im = db.InventoryManagers.Where(m => m.PortfolioID == portfolioID && m.PartNumber == hfPartNumber.Value && m.Categoery == categoryId && m.SubCategory == subCategoryId && m.SiteId == int.Parse(siteId)).Select(m => m).FirstOrDefault();
                            var imList = db.InventoryManagers.Select(l => l).ToList();
                            InventoryMgt.Entity.InventoryManager imDetails = imList.Where(m => m.PortfolioID == portfolioID && m.ItemDescription == product && m.Categoery == categoryId && m.SubCategory == subCategoryId).Select(m => m).FirstOrDefault();
                            InventoryMgt.Entity.InventoryManager im = imList.Where(m => m.PortfolioID == portfolioID && m.ItemDescription == product && m.Categoery == categoryId && m.SubCategory == subCategoryId && m.SiteId == int.Parse(siteId)).Select(m => m).FirstOrDefault();

                            GoodsStorageDetail gs = db.GoodsStorageDetails.Where(g => g.ID == int.Parse(id)).Select(g => g).FirstOrDefault();
                            // Inventory_AssociatedBarcode ia = db.Inventory_AssociatedBarcodes.Where(a => a.SubCategoryID == subCategoryId).Select(a => a).FirstOrDefault();

                            if (im == null && imDetails != null)
                            {
                                //Inventory Manager insert

                                InventoryMgt.Entity.InventoryManager inventoryManager = new InventoryMgt.Entity.InventoryManager();
                                inventoryManager.PartNumber = imDetails.PartNumber;
                                inventoryManager.PortfolioID = portfolioID;
                                inventoryManager.Categoery = categoryId;
                                inventoryManager.SubCategory = subCategoryId;
                                inventoryManager.SiteId = int.Parse(siteId);
                                inventoryManager.Image = Guid.Empty;
                                inventoryManager.Supplier = 0;
                                inventoryManager.Manufacturer = 0;
                                inventoryManager.ItemDescription = product;
                                inventoryManager.Barcode = imDetails.Barcode;
                                inventoryManager.SectionType = "Global";
                                inventoryManager.Qty = int.Parse(qty);
                                db.InventoryManagers.InsertOnSubmit(inventoryManager);
                                db.SubmitChanges();
                                inventoryManager.Id = inventoryManager.Id;

                                //Inventory Journal Insert 
                                InventoryMgt.Entity.InventoryManager getCurrentInventory = InventoryBAL.GetInventoryByID(inventoryManager.Id);
                                if (getCurrentInventory != null)
                                {
                                    getCurrentInventory.Notes = "From project reference: " + projectPrefix + Request.QueryString["project"].ToString();
                                    InventoryBAL.InsertInventoryJournal(getCurrentInventory, 0, "BOM", int.Parse(qty), Convert.ToInt32(getCurrentInventory.Qty), 0);
                                }

                                //Inventory Stock level Jornal insert
                                JInventoryStockItem journal = new JInventoryStockItem();
                                journal.InventoryID = inventoryManager.Id;
                                journal.UserID = sessionKeys.UID;
                                journal.Qty = int.Parse(qty);
                                journal.ReasonCode = "BOM";
                                journal.Notes = "From Project Reference: " + projectPrefix + Request.QueryString["project"].ToString();
                                journal.EntryDate = DateTime.Now;

                                InventoryBAL.InsertInventoryStockitemJournal(journal);


                                //update GoodsStorageDetail inventoryID
                                gs.InventoryID = inventoryManager.Id;
                                db.SubmitChanges();

                                successMsg = successMsg + site + ",";
                                for (int x = 0; x < int.Parse(qty); x++)
                                {
                                    Inventory_SiteStorageDetail inventory_SiteStorageDetail = new Inventory_SiteStorageDetail();
                                    inventory_SiteStorageDetail.Barcode = string.Empty;
                                    inventory_SiteStorageDetail.InventoryId = inventoryManager.Id;
                                    inventory_SiteStorageDetail.SiteId = int.Parse(siteId);
                                    inventory_SiteStorageDetail.CategoryID = categoryId;
                                    inventory_SiteStorageDetail.SubCategoryID = subCategoryId;
                                    inventory_SiteStorageDetail.UseID = 0;
                                    inventory_SiteStorageDetail.UseStatus = false;// "NOT IN USE"
                                    //inventory_SiteStorageDetail.AssociatedBarcode = ia == null ? string.Empty : ia.Barcode;
                                    sList.Add(inventory_SiteStorageDetail);
                                }
                                db.Inventory_SiteStorageDetails.InsertAllOnSubmit(sList);
                                db.SubmitChanges();

                            }
                            else if (im != null)
                            {
                                //Inventory Manager update
                                //im.Qty = im.Qty + int.Parse(qty);
                                //db.SubmitChanges();

                                int openingStock = InventoryBAL.GetQtyAvailable(im.Id.ToString());
                                if (openingStock < int.Parse(qty))
                                {
                                    lblStockError.Text = "The site has insufficent stock to allocate this item. Please check the Inventory Module";
                                }
                                else
                                {
                                    //Inventory Stock level Jornal insert
                                    JInventoryStockItem journal = new JInventoryStockItem();
                                    journal.InventoryID = im.Id;
                                    journal.UserID = sessionKeys.UID;
                                    journal.Qty = int.Parse(qty);
                                    journal.ReasonCode = "BOM";
                                    journal.Notes = "From Project Reference: " + projectPrefix + Request.QueryString["project"].ToString();
                                    journal.EntryDate = DateTime.Now;

                                    InventoryBAL.InsertInventoryStockitemJournal(journal);

                                    //update GoodsStorageDetail inventoryID
                                    gs.InventoryID = im.Id;
                                    db.SubmitChanges();

                                    successMsg = successMsg + site + ",";


                                    im.Qty = im.Qty - int.Parse(qty);
                                    db.SubmitChanges();

                                    List<Inventory_SiteStorageDetail> stockList = db.Inventory_SiteStorageDetails.ToList();
                                  //  db.Inventory_SiteStorageDetails.DeleteAllOnSubmit(stockList);
                                  //  db.SubmitChanges();
                                    try
                                    {
                                        var AllBatchs = db.Inventory_Batches.Where(a => a.InventoryID == im.Id).OrderBy(a => a.ID).ToList();
                                        int NewQty = 0;
                                        bool IsFirstTime = true;
                                        List<InventoryManager_PSDProduct> PSDList = new List<InventoryManager_PSDProduct>();
                                        PSDList = db.InventoryManager_PSDProducts.Where(p => p.ProductId == im.Id).ToList();

                                        int FirstBatchId = AllBatchs.FirstOrDefault().ID;
                                        int BAQty1 = AllBatchs.FirstOrDefault().QTY.Value - PSDList.Where(o => o.BatchID == FirstBatchId).Select(o => o.QtyUsed.Value).Sum();

                                        if (BAQty1 >= int.Parse(qty))
                                        {
                                            UpdatingUsageTables( im.Id, int.Parse(qty), AllBatchs.FirstOrDefault().ID);
                                        }
                                        else
                                        {
                                            foreach (var x in AllBatchs)
                                            {
                                                int BAQty = x.QTY.Value - PSDList.Where(o => o.BatchID == x.ID).Select(o => o.QtyUsed.Value).Sum();
                                                if (IsFirstTime == true)
                                                {
                                                    if (BAQty > 0)
                                                    {
                                                        UpdatingUsageTables( im.Id, BAQty, x.ID);
                                                        NewQty = int.Parse(qty) - BAQty;
                                                    }
                                                    IsFirstTime = false;
                                                }
                                                else
                                                {
                                                    if (NewQty > 0)
                                                    {
                                                        if (BAQty > 0)
                                                        {
                                                            UpdatingUsageTables(im.Id, int.Parse(NewQty.ToString()), x.ID);
                                                            NewQty = NewQty - x.QTY.Value;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }

                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        LogExceptions.WriteExceptionLog(ex);
                                    }
                                    


                                    //for (int x = 0; x < int.Parse(qty); x++)
                                    //{
                                    //    Inventory_SiteStorageDetail inventory_SiteStorageDetail = new Inventory_SiteStorageDetail();
                                    //    inventory_SiteStorageDetail.Barcode = string.Empty;
                                    //    inventory_SiteStorageDetail.InventoryId = im.Id;
                                    //    inventory_SiteStorageDetail.SiteId = im.SiteId;
                                    //    inventory_SiteStorageDetail.CategoryID = im.Categoery;
                                    //    inventory_SiteStorageDetail.SubCategoryID = im.SubCategory;
                                    //    inventory_SiteStorageDetail.UseID =0;
                                    //    inventory_SiteStorageDetail.UseStatus = false;// "NOT IN USE"
                                    //    //inventory_SiteStorageDetail.AssociatedBarcode = ia == null ? string.Empty : ia.Barcode;
                                    //    sList.Add(inventory_SiteStorageDetail);
                                    //}
                                    //db.Inventory_SiteStorageDetails.InsertAllOnSubmit(sList);
                                    //db.SubmitChanges();

                                    //Inventory Journal Insert
                                    im.Notes = "From project reference: " + projectPrefix + Request.QueryString["project"].ToString();
                                    im.Qty = InventoryBAL.GetQtyAvailable(im.Id.ToString());
                                    InventoryBAL.InsertInventoryJournal(im, 0, "BOM", int.Parse(qty), openingStock, 0);
                                }
                            }

                        }
                        else
                        {
                            errorMsg = errorMsg + site + ",";
                        }

                    }


                }


            }

            if (successMsg != string.Empty)
                lblSuccess.Text = successMsg.TrimEnd(',') + " site(s) items added to inventory successfully.";
            if (errorMsg != string.Empty)
                lblError.Text = errorMsg.TrimEnd(',') + " site(s) item already added to inventory. Please check and try again.";

            BindStorageDetails(int.Parse(hfBOMID.Value));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void UpdatingUsageTables(int InventoryId,int Qty,int BatchId)
    {
        try
        {
            using (InventoryDataContext db = new InventoryDataContext())
            {
                List<Inventory_SiteStorageDetail> storageListA = new List<Inventory_SiteStorageDetail>();
                List<Inventory_SiteStorageDetail> stockList = db.Inventory_SiteStorageDetails.Where(s => s.InventoryId == InventoryId && s.UseStatus == false).Take(Qty).ToList();
                foreach (Inventory_SiteStorageDetail item in stockList)
                {
                    item.UseStatus = true;
                    db.SubmitChanges();
                }

                InventoryManager_PSDProduct Im_Usage = new InventoryManager_PSDProduct();
                Im_Usage.ProductId = InventoryId;
                Im_Usage.SectionType = "PROJECT";
                Im_Usage.QtyUsed = Qty;
                Im_Usage.Status = 1;
                Im_Usage.ModifiedBy = sessionKeys.UID;
                Im_Usage.ModifiedDate = DateTime.Now;
                Im_Usage.projectid = sessionKeys.Project;
                Im_Usage.BatchID = BatchId;
                db.InventoryManager_PSDProducts.InsertOnSubmit(Im_Usage);
                db.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void imghistoryCancel_Click(object sender, EventArgs e)
    {

        mdlPopupStorage.Hide();
        BindGrid();
    }
    protected void gvMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaterial.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void ddlWorksheet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void imgViewAll_Click(object sender, EventArgs e)
    {
        ddlWorksheet.SelectedValue = "0";
        txtDescription.Text = string.Empty;
        gvMaterial.AllowPaging = false;
        BindGrid();

    }
}