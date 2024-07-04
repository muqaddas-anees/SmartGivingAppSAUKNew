using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SC_ContractorMaterialCS;
using System.Data.SqlClient;
using IMClass;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ProjectTasksManagers;
using IMClass;

public partial class DC_Inventory : System.Web.UI.Page
{
    SC_ContractorMaterial SC_cntMaterialCS;
    InventoryManagerCs IMCS;
    public int Portfolio;
    public int PageType = 1;
    public int VendorID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "FLS";
        try
        {

            if (!IsPostBack)
            {
               // Master.PageHead = "FLS";
                if (QueryStringValues.CallID > 0)
                    lblTitle.InnerText = "Inventory Manager - Ticket Reference " + QueryStringValues.CallID;
                else
                    lblTitle.InnerText = "Inventory Manager - FLS";
                BindDDL();
                //Master.WebservicePath = "~/ProjectTasks.asmx";
                //Master.ServicePath = "~/ProjectTasks.asmx";
                // ~/ProjectTasks.asmx

                //GetProjectDetails();

                //Bind checklist box
                //  BindChecklistbox();

                //                    Bind_TaskPosition();

            }
        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    private void BindDDL()
    {
        BindCategory();
        BindSubCategory();
        BindProduct();
        BindSite();
        txtQty.Text = "";
        txtQtyRel.Text = "";
    }

    private void BindSite()
    {

        SC_cntMaterialCS = new SC_ContractorMaterial();
        //ddlSite.DataSource =  SC_cntMaterialCS.GetSites();
        ddlSite.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio(sessionKeys.PortfolioID);
        ddlSite.DataTextField = "Site";
        ddlSite.DataValueField = "ID";
        ddlSite.DataBind();
        ddlSite.Items.Insert(0, new ListItem("Please Select...", "0"));

    }
    private void GetProjectDetails()
    {

        try
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("Select isnull(ProjectTitle,'') as ProjectTitle, isnull(Portfolio,0) as Portfolio,isnull(ProjectStatusID,1) as ProjectStatusID,StartDate,ProjectEndDate  from Projects where ProjectReference ={0}", QueryStringValues.Project));
            while (dr.Read())
            {
                string ProjectTitle = dr["ProjectTitle"].ToString();
                //txtProjectTitle.Text = dr["ProjectTitle"].ToString();
                Portfolio = Convert.ToInt32(dr["Portfolio"].ToString());
                //ddlCustomer.SelectedValue = dr["Portfolio"].ToString();
                //ddlStatus.SelectedValue = dr["ProjectStatusID"].ToString();
                //txt_ins_Startdate.Text = string.IsNullOrEmpty(dr["StartDate"].ToString()) ? "" : Convert.ToDateTime(dr["StartDate"]).ToShortDateString();
                //txt_ins_enddate.Text = string.IsNullOrEmpty(dr["ProjectEndDate"].ToString()) ? "" : Convert.ToDateTime(dr["ProjectEndDate"]).ToShortDateString();
            }
            dr.Dispose();
            dr.Close();

        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }

    }

    //Bind Category

    private void BindCategory()
    {
        //Portfolio = Convert.ToInt32(ddlCustomer.SelectedItem.Value.ToString());
        int port = sessionKeys.PortfolioID;
        SC_cntMaterialCS = new SC_ContractorMaterial();
        ddlCategory.DataSource = SC_cntMaterialCS.SelectAllCategory(sessionKeys.PortfolioID, PageType, VendorID);
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataBind();
        ddlCategory.SelectedValue = Convert.ToString(Portfolio);
    }

    private void BindSubCategory()
    {
        SC_cntMaterialCS = new SC_ContractorMaterial();
        if (Convert.ToInt32(ddlCategory.SelectedValue.ToString()) != 0)
        {

            ddlSubCategory.DataSource = SC_cntMaterialCS.GetSubCategory(Convert.ToInt32(ddlCategory.SelectedValue.ToString()), sessionKeys.PortfolioID, 0);
            ddlSubCategory.DataValueField = "Id";
            ddlSubCategory.DataTextField = "CategoryName";
            ddlSubCategory.DataBind();
        }
        else
        {
            ddlSubCategory.Items.Insert(0, "Please Select...");
        }
    }

    private void BindProduct()
    {
        IMCS = new InventoryManagerCs();
        int categoryId = 0;
        if (Convert.ToInt32(ddlCategory.SelectedValue.ToString()) != 0)
        {
            categoryId = Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString());


            int subcategoryId = 0;
            if (Convert.ToInt32(ddlSubCategory.SelectedValue.ToString()) != 0)
            {
                subcategoryId = Convert.ToInt32(ddlSubCategory.SelectedItem.Value.ToString());
                ddlProduct.DataSource = IMCS.SelectProducts(sessionKeys.PortfolioID, categoryId,
                    subcategoryId, int.Parse(ddlSite.SelectedItem.Value.ToString()), "Global");
                ddlProduct.DataValueField = "Id";
                ddlProduct.DataTextField = "ItemDescription";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("Please Select...", "0"));
            }
        }
        else
        {
            ddlProduct.Items.Insert(0, new ListItem("Please Select...", "0"));
        }
        //SC_cntMaterialCS = new SC_ContractorMaterial();
        //int categoryId = 0;
        //int subcategoryId = 0;
        //if (Convert.ToInt32(ddlCategory.SelectedValue.ToString()) != 0)
        //{
        //    categoryId = Convert.ToInt32(ddlCategory.SelectedItem.Value.ToString());


        //    if ((ddlSubCategory.SelectedItem.Value != null))
        //    {
        //        subcategoryId = Convert.ToInt32(ddlSubCategory.SelectedItem.Value.ToString());
        //    }
        //    ddlProduct.DataSource = SC_cntMaterialCS.SelectProducts(sessionKeys.PortfolioID, categoryId, subcategoryId);
        //    ddlProduct.DataValueField = "Id";
        //    ddlProduct.DataTextField = "ItemDescription";
        //    ddlProduct.DataBind();
        //}
        //else
        //{
        //    ddlProduct.Items.Insert(0, "Please select...");
        //}
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblError.Visible = false;
        BindSubCategory();
        // BindProduct();
        //GridInventoryBinding();
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProduct();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindProduct();
        GridInventoryBinding();
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
    protected void grdInventory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdInventory.EditIndex = e.NewEditIndex;
        GridInventoryBinding();
        txtQty.Text = "";
        txtQtyRel.Text = "";
        //GridView2.EditIndex = e.NewEditIndex;
        //fillGrid(2);
    }
    protected void grdInventory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInventory.PageIndex = e.NewPageIndex;
        GridInventoryBinding();
    }
    //grdInventory_RowCancelingEdit
    protected void grdInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdInventory.EditIndex = -1;
        GridInventoryBinding();
        txtQty.Text = "";
        txtQtyRel.Text = "";

    }
    //'grdInventory_RowCommand' 
    //protected void grdInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //}
    ////grdInventory_RowUpdating
    protected void grdInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdInventory.EditIndex = -1;
        GridInventoryBinding();
    }

    //grdInventory_RowEditing
    protected void grdInventory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = grdInventory.EditIndex;
                GridViewRow Row = grdInventory.Rows[i];
                int Qty = Convert.ToInt32(((TextBox)Row.FindControl("QTYtxt")).Text);
                int ReOrderLevel = Convert.ToInt32(((TextBox)Row.FindControl("txtReOrderLevel")).Text);
                int ProductID = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                // need to find whether the product is already exists in Inventory Table or not
                IMCS = new InventoryManagerCs();

                //SC_cntMaterialCS = new SC_ContractorMaterial();
                //int SiteId = int.Parse(ddlSite.SelectedItem.Value.ToString());
                DataTable _dt = IMCS.SelectIMPSDByID(ID);
                int retVal = -1;
                if (_dt.Rows.Count > 0)
                {
                    DataRow _dr = _dt.Rows[0];
                    ID = Convert.ToInt32(_dr["Id"].ToString());
                    Qty = Qty + Convert.ToInt32(_dr["QtyUsed"].ToString());
                    int SiteId = Convert.ToInt32(_dr["SiteId"].ToString());
                    int chkVal = -1;
                    object Obj = IMCS.UpdateIMPSDProduct(ID, ProductID, SiteId, "FLS", Qty, ReOrderLevel);
                    if (chkVal == 1)
                    {
                        lblErroMsg.Visible = true;
                        lblErroMsg.Text = "Please enter inventory for this product";
                    }
                    else if (chkVal == 2)
                    {
                        Qty = Qty + Convert.ToInt32(_dr["Qty"].ToString());

                        //retVal = SC_cntMaterialCS.UpdateInventory(ID, 0, ProductID, Qty, ReOrderLevel, "FLS");
                        try
                        {
                            // int c = comm_update.ExecuteNonQuery();
                            if (retVal > 0)
                            {
                                //lblError.Visible = false;
                                GridInventoryBinding();
                                txtQty.Text = "";
                                txtQtyRel.Text = "";
                            }
                            else
                            {

                                //lblError1.Text = "The material description already exists";
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        finally
                        {
                            //conn.Close();
                        }
                    }


                    else if (chkVal == 3)
                    {
                        lblErroMsg.Visible = true;
                        lblErroMsg.Text = "Please check the quantity entered as it exceeds the amount specified in the Inventory module";
                    }
                    //retVal = SC_cntMaterialCS.UpdateInventory(ID, 0, ProductID, Qty, ReOrderLevel, "FLS");

                    //try
                    //{
                    //    // int c = comm_update.ExecuteNonQuery();
                    //    if (retVal > 0)
                    //    {
                    //        //lblError.Visible = false;
                    //        GridInventoryBinding();
                    //    }
                    //    else
                    //    {

                    //        //lblError1.Text = "The material description already exists";
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogExceptions.WriteExceptionLog(ex);
                    //}
                    //finally
                    //{
                    //    //conn.Close();
                    //}


                }
                else
                {
                    //retVal = SC_cntMaterialCS.InsertInventory(SiteId, ProductID, Qty, ReOrderLevel, "Global");
                }
            }
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                IMCS = new InventoryManagerCs();
                int retVal = -1;
                retVal = IMCS.DeleteIMPSDProduct(ID);
                if (retVal > 0)
                {
                    GridInventoryBinding();
                }


            }

        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgAddInventory_Click(object sender, EventArgs e)
    {
        try
        {
            IMCS = new InventoryManagerCs();
            int retVal = -1;
            SC_cntMaterialCS = new SC_ContractorMaterial();
            if (Convert.ToInt32(ddlProduct.SelectedValue.ToString()) != 0)
            {
                if (Convert.ToInt32(ddlSite.SelectedValue.ToString()) != 0)
                {
                    int ProductId = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                    int SiteID = Convert.ToInt32(ddlSite.SelectedValue.ToString());
                    int QtyUsed = int.Parse(string.IsNullOrEmpty(txtQty.Text.ToString()) ? "0" : txtQty.Text.ToString());
                    int QtyReplenish = int.Parse(string.IsNullOrEmpty(txtQtyRel.Text.ToString()) ? "0" : txtQtyRel.Text.ToString());
                    string SectionType = "FLS";

                    object Obj = IMCS.InsertIMPSDProduct(ProductId, SiteID, SectionType, QtyUsed, QtyReplenish);
                    if (Obj != null)
                    {
                        retVal = int.Parse(Obj.ToString());
                    }
                    if (retVal == 1)
                    {
                        lblErroMsg.Visible = true;
                        lblErroMsg.Text = "Enter Inventory for the Product";
                    }
                    else if (retVal == 2)
                    {
                        //bind the grd inventory
                        GridInventoryBinding();
                    }
                    else if (retVal == 3)
                    {
                        lblErroMsg.Visible = true;
                        lblErroMsg.Text = "Please check the quantity entered as it exceeds the amount specified in the Inventory module";

                    }

                }
                else
                {
                    lblErroMsg.Visible = true;
                    lblErroMsg.Text = "Please Select Site";
                }


            }
            else
            {
                lblErroMsg.Visible = true;
                lblErroMsg.Text = "Please Select Product";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    //for Grid view binding
    //

    private void GridInventoryBinding()
    {
        IMCS = new InventoryManagerCs();
        pnlInventory.Visible = true;
        int cust = sessionKeys.PortfolioID;
        int cat = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
        int sub = Convert.ToInt32(ddlSubCategory.SelectedValue.ToString());
        int prod = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
        int siteid = Convert.ToInt32(ddlSite.SelectedValue.ToString());
        grdInventory.DataSource = IMCS.SelectIMPSDProducts(prod, "FLS");
        grdInventory.DataBind();
    }
    protected void imgbtnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            IMCS = new InventoryManagerCs();
            int retVal = -1;
            SC_cntMaterialCS = new SC_ContractorMaterial();
            if (Convert.ToInt32(ddlProduct.SelectedValue.ToString()) != 0)
            {
                if (Convert.ToInt32(ddlSite.SelectedValue.ToString()) != 0)
                {
                    int ProductId = Convert.ToInt32(ddlProduct.SelectedValue.ToString());
                    int SiteID = Convert.ToInt32(ddlSite.SelectedValue.ToString());
                    int QtyUsed = int.Parse(string.IsNullOrEmpty(txtQty.Text.ToString()) ? "0" : txtQty.Text.ToString());
                    int QtyReplenish = int.Parse(string.IsNullOrEmpty(txtQtyRel.Text.ToString()) ? "0" : txtQtyRel.Text.ToString());
                    string SectionType = "FLS";

                    object Obj = IMCS.InsertIMPSDProduct(ProductId, SiteID, SectionType, QtyUsed, QtyReplenish);
                    if (Obj != null)
                    {
                        retVal = int.Parse(Obj.ToString());
                    }
                    if (retVal == 1)
                    {
                        lblErroMsg.Visible = true;
                        lblErroMsg.Text = "Enter Inventory for the Product";
                    }
                    else if (retVal == 2)
                    {
                        //bind the grd inventory
                        GridInventoryBinding();
                        txtQty.Text = string.Empty;
                        txtQtyRel.Text = string.Empty;
                    }
                    else if (retVal == 3)
                    {
                        lblErroMsg.Visible = true;
                        lblErroMsg.Text = "Please check the quantity entered as it exceeds the amount specified in the Inventory module";

                    }

                }
                else
                {
                    lblErroMsg.Visible = true;
                    lblErroMsg.Text = "Please Select Site";
                }


            }
            else
            {
                lblErroMsg.Visible = true;
                lblErroMsg.Text = "Please Select Product";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }


    }

    protected void rtrnSD_Click(object sender, EventArgs e)
    {
        //Response.Redirect("../SDsummary.aspx?sdid=" + QueryStringValues.SDID);
    }

    protected void grdInventory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridInventoryBinding();
    }
}
