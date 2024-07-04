using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Deffinity.ServiceCatalogManager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;
using PortfolioMgt.DAL;


using System.Data.OleDb;
using DC.BLL;

public partial class controls_ServiceCatalogue_ctrl_1 : System.Web.UI.UserControl
{
    public int PageType;
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    public int SetPageType
    {
        set { PageType = value; }
        get { return PageType; }

    }

    protected string sid = "0";
    SqlConnection conn = new SqlConnection(Constants.DBString);
    public const int Labour = 1;
    public const int Products = 2;
    public const int Service = 3;
    public const int ServiceMinusLabour = 4;
    public int VendorID = 0;
    int PortfolioID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        VendorID = (QueryStringValues.Vendor == null ? 0 : QueryStringValues.Vendor);
        if (VendorID != 0)
        {
            PortfolioID = 0;
            Title(false, true);
        }
        else
        {
            PortfolioID = sessionKeys.PortfolioID;
            Title(true, false);
        }     
        if (!IsPostBack)
        {
            
            if (sessionKeys.Cataloguetype > 0)
                ddlSelect.SelectedValue = sessionKeys.Cataloguetype.ToString();
            //Bind the rate types to dropdown list 
            BaseBindings();
            fillGrid(int.Parse(ddlSelect.SelectedValue));
            AddNewItem(int.Parse(ddlSelect.SelectedValue));
            RateType();
            TexboxtScripts();
            if (sessionKeys.SID == 8)
            {
                tblAddData.Visible = false;
            }
            else
            {
                tblAddData.Visible = true;
            }
            
        }
        lblError.Text = "";
        lblError1.Text = "";
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //ShowFileUploadPnl();
        if (QueryStringValues.Vendor > 0)
            iframeMpp.Src = string.Format("~/WF/Vendors/RFIVendorServiceCatalogFileupload.aspx?VendorID={0}", QueryStringValues.Vendor);
           // iframeMpp.Attributes.Add("src", string.Format("../../WF/Vendors/RFIVendorServiceCatalogFileupload.aspx?VendorID={0}", QueryStringValues.Vendor));
        //else
        //    iframeMpp.Attributes.Add("src", string.Format("ServiceCatelogAdminFileUpload.aspx?type={0}", ddlSelect.SelectedItem.Text));

    }

    private void ShowFileUploadPnl()
    {
        //if (Request.Url.AbsoluteUri.ToString().ToLower().Contains("my") || Request.Url.AbsoluteUri.ToString().ToLower().Contains("kinetic"))
            Panel_fileupdload.Visible = true;
    }
    #region Default databindings
    //Adding Scripts to Textboxes
    public void TexboxtScripts()
    {
        try
        {
            txtLDPrice.Attributes.Add("onKeyUp", "Javascript:LDiscnt()");
            txtPDPrice.Attributes.Add("onKeyUp", "Javascript:PDiscnt()");
            txtSDprice.Attributes.Add("onKeyUp", "Javascript:SDiscnt()");
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void RateType()
    {
        ddlRateType.DataSource = RateTypes();
        ddlRateType.DataValueField = "ID";
        ddlRateType.DataTextField = "RateType";
        ddlRateType.DataBind();
    }

    //To Bind the Ratatypes to drop down list
    public DataSet RateTypes()
    {
        //SQLConnection
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RateType", conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "RateType");
        return ds;
    }

    //To Bind the PortFolio to drop down list

    public DataSet Supplier()
    {
        SqlDataAdapter da = new SqlDataAdapter("select VendorID as ID,ContractorName as SupplierName from v_Vendors where v_Vendors.CompanyID="+ sessionKeys.PortfolioID.ToString()+"  union select 0 as ID, ' Please select...' as SupplierName order by ContractorName", conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Supplier");
        return ds;
    }

    public DataSet Currency()
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CurrencyList where display='Y'", conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Supplier");
        return ds;
    }

    #endregion

    #region Gridview binding and change visibility of panels
    //Executes when view button clicked
    protected void btnView_Click(object sender, EventArgs e)
    {
        fillGrid(int.Parse(ddlSelect.SelectedValue));
    }
    private void Grid_visiblity(bool g_labour, bool g_meterial, bool g_services, bool g_servicesminuslabour)
    {
        GridView1.Visible = g_labour;
        GridView2.Visible = g_meterial;
        GridView3.Visible = g_services;
        //GridView4.Visible = g_servicesminuslabour;
    }
    /// <summary>
    /// type: 1 - Labour,2 - Metetial/Products,3 - Services,4 - ServicesMinusLabours
    /// </summary>
    /// <param name="type">
    /// </param>
    private void fillGrid(int type)
    {
        try
        {
            switch (type)
            {
                case 1:

                   // GridView1.DataSource = ServiceCatalogManager.LabourSelectAll(0, PortfolioID, int.Parse(string.IsNullOrEmpty(ddlCategory.SelectedValue) ? "0" : ddlCategory.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlSubCategory.SelectedValue) ? "0" : ddlSubCategory.SelectedValue), PageType, VendorID);
                   // GridView1.DataBind();
                    //change the visiblity of grid view
                    Grid_visiblity(true, false, false, false);

                    break;
                case 2:
                    GridView2.DataSource = ServiceCatalogManager.MaterialSelectAll(0, PortfolioID, int.Parse(string.IsNullOrEmpty(ddlCategory.SelectedValue) ? "0" : ddlCategory.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlSubCategory.SelectedValue) ? "0" : ddlSubCategory.SelectedValue), PageType, VendorID);
                    GridView2.DataBind();

                    if(GridView2.Rows.Count  ==0)
                    {
                        mdl_Products.Show();
                    }
                        //mdl_Products
                        //change the visiblity of grid view
                        Grid_visiblity(false, true, false, false);                    
                    break;
                case 3:
                 //   GridView3.DataSource = ServiceCatalogManager.ServicesSelectAll_ByServiceType(0, PortfolioID, int.Parse(string.IsNullOrEmpty(ddlCategory.SelectedValue) ? "0" : ddlCategory.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlSubCategory.SelectedValue) ? "0" : ddlSubCategory.SelectedValue), Service, PageType, VendorID);
                  // GridView3.DataBind();
                    //change the visiblity of grid view
                    Grid_visiblity(false, false, true, false);
                    break;
                //case 4:

                //    GridView4.DataSource = ServiceCatalogManager.ServicesSelectAll_ByServiceType(0, PortfolioID, int.Parse(string.IsNullOrEmpty(ddlCategory.SelectedValue) ? "0" : ddlCategory.SelectedValue), int.Parse(string.IsNullOrEmpty(ddlSubCategory.SelectedValue) ? "0" : ddlSubCategory.SelectedValue), ServiceMinusLabour, PageType, VendorID);
                //    GridView4.DataBind();
                //    //change the visiblity of grid view
                //    Grid_visiblity(false, false, false, true);
                //    break;

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    //New record
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        //btnAddnew.Visible = false;
        //display the text boxes to enter data
        AddNewItem(int.Parse(ddlSelect.SelectedValue));
        if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                AddNewItem(int.Parse(ddlSelect.SelectedValue));
                mdl_Labour.Show();
            }
            else
            {
                //lblError.Visible = true;
                //lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please Select "+ Deffinity.systemdefaults.GetSubCategoryName();
            }
        }
        else
        {
            //lblError.Visible = true;
            //lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please select "+ Deffinity.systemdefaults.GetCategoryName();
        }

        
    }
    protected void btnAddnewProduct_Click(object sender, EventArgs e)
    {
        //btnAddnew.Visible = false;
        //display the text boxes to enter data
        if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                AddNewItem(int.Parse(ddlSelect.SelectedValue));
                mdl_Products.Show();
            }
            else
            {
                //lblError.Visible = true;
                //lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please select " + Deffinity.systemdefaults.GetSubCategoryName(); ;
            }
        }
        else
        {
            //lblError.Visible = true;
            //lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please select " + Deffinity.systemdefaults.GetCategoryName(); ;
        }
        
        
    }
    protected void btnAddnewService_Click(object sender, EventArgs e)
    {
        //btnAddnew.Visible = false;
        //display the text boxes to enter data
        if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                AddNewItem(int.Parse(ddlSelect.SelectedValue));
                mdl_Service.Show();
            }
            else
            {
                //lblError.Visible = true;
                //lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please Select "+ Deffinity.systemdefaults.GetSubCategoryName();
            }
        }
        else
        {
            //lblError.Visible = true;
            //lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please Select "+ Deffinity.systemdefaults.GetCategoryName();
        }
    }
    //change the panel visibility
    private void PanelVisibility(bool p_labour, bool p_meterials, bool p_services, bool p_serviceMinusLabour)
    {
        pnl_Labour.Visible = p_labour;
        pnl_Product.Visible = p_meterials;
        pnl_Service.Visible = p_services;
       // MyPanel4.Visible = p_serviceMinusLabour;
    }
    private void AddNewItem(int _type)
    {
        switch (_type)
        {
            case 1:
                PanelVisibility(true, false, false, false);

                txtDescription.Text = string.Empty;
                txtBuyingPrice.Text = string.Empty;
                txtSellingPrice.Text = string.Empty;
                txtNotes.Text = string.Empty;
                txtunitconsumption.Text = string.Empty;
                ddlRateType.SelectedIndex = 0;
                btnSaveRecord.Visible = true;
                txtLPercent.Text = string.Empty;
                txtLDPrice.Text = string.Empty;
                imgbtnUpdateLabour.Visible = false;
                btnAddnewProduct.Visible = false;
                btnAddnewService.Visible = false;
                btnAddnew.Visible = true;
                txtLSPercent.Text = string.Empty;
                break;
            case 2:
                PanelVisibility(false, true, false, false);
                Bind_Supplier();
                btnSaveMaterial.Visible = true;
                imgbtnUpdateMaterial.Visible = false;
                txtItemDesc.Text = string.Empty;
                txtPartNumber.Text = string.Empty;
                txtucproduct.Text = string.Empty;
                txtUnit.Text = string.Empty;
                txtStockLevel.Text = string.Empty;
                txtMBuyingPrice.Text = string.Empty;
                txtMSellingPrice.Text = string.Empty;
                txtMNotes.Text = string.Empty;
                txtMItemType.Text = string.Empty;
                txtMQBRefID.Text = string.Empty;
                txtPPercent.Text = string.Empty;
                txtPDPrice.Text = string.Empty;
                btnAddnew.Visible = false;
                btnAddnewProduct.Visible = true;
                btnAddnewService.Visible = false;
                txtPSPercent.Text = string.Empty;
                break;
            case 3:
                PanelVisibility(false, false, true, false);
                txtSDescription.Text = string.Empty;
                txtSetupBuy.Text = string.Empty;
                txtSetupSell.Text = string.Empty;
                txtMaterialsBuy.Text = string.Empty;
                txtucservice.Text = string.Empty;
                txtMaterialsSell.Text = string.Empty;
                txtLabourBuy.Text = string.Empty;
                txtLabourSell.Text = string.Empty;
                txtSPercent.Text = string.Empty;
                txtSDprice.Text = string.Empty;
                btnSaveServices.Visible = true;
                imgbtnUpdateService.Visible = false;
                btnAddnewService.Visible = true;
                btnAddnew.Visible = false;
                btnAddnewProduct.Visible = false;
                txtSDPercent.Text = string.Empty;
                break;
            case 4:
                PanelVisibility(false, false, false, true);
                //txt_SMLDescription.Text = string.Empty;
                //txt_SMLMaterialBuy.Text = string.Empty;
                //txt_SMLMaterialsSell.Text = string.Empty;
                //txt_SMLSetupBuy.Text = string.Empty;
                //txt_SMLSetupSell.Text = string.Empty;
                //btnSaveServiceMinusLabour.Visible = true;
                //btnUpdateServiceMinusLabour.Visible = false;
                txtTotBuy.Text = string.Empty;
                txtTotSell.Text = string.Empty;
                break;
        }


        //RequiredFieldValidator21.Enabled = true;
        //RequiredFieldValidator22.Enabled = true;
    }

    private void Bind_Supplier()
    {
        try
        {
            ddlSupplier.DataSource = Supplier();
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataValueField = "ID";
            ddlSupplier.DataBind();

            if (ddlSupplier.Items.Count > 1)
            {
                ddlSupplier.SelectedValue = VendorID.ToString();
            }
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion

    //Executes this when Edit button is clicked
    #region Gridview1/ Grid labour events
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        fillGrid(1);
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGrid(1);
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillGrid(1);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView1.EditIndex;
                GridViewRow Row = GridView1.Rows[i];

                string LDesc = ((TextBox)Row.FindControl("txtLDesc1")).Text;
                string LRateType = ((DropDownList)Row.FindControl("DropDownList1")).SelectedItem.Value;
                string LBuyingPrice = ((TextBox)Row.FindControl("txtLbuyingPrice1")).Text;
                string LSellingPrice = ((TextBox)Row.FindControl("txtLSellingPrice1")).Text;
                string LNotes = ((TextBox)Row.FindControl("txtLNotes1")).Text;
                double LDscntPrice = Convert.ToDouble((Convert.ToDouble(LBuyingPrice.ToString())) - ((Convert.ToDouble(LSellingPrice.ToString()))));
                string uc = ((TextBox)Row.FindControl("txtuc")).Text;

                if (LDscntPrice < 0)
                {
                    LDscntPrice = 0;
                }

                SqlCommand comm_update = new SqlCommand("DN_ServiceCatalog_LabourUpdate", conn);
                comm_update.CommandType = CommandType.StoredProcedure;
                comm_update.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = LDesc;
                comm_update.Parameters.Add("@RateType", SqlDbType.Int).Value = LRateType;
                comm_update.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value = LBuyingPrice;
                comm_update.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = LSellingPrice;
                comm_update.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = LNotes;
                comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                comm_update.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
                comm_update.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = LDscntPrice;
                comm_update.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = uc;
                //SqlCommand comm_update=new SqlCommand("UPDATE ContractorLabourTypes SET EngineerDescription = '" +LDesc + "', [RateType] = '" + LRateType + "', [BuyingPrice] = '" + LBuyingPrice + "',[SellingPrice]= '" + LSellingPrice  + "',[WorstCase] = '" + LWorstCase  + "',[BestCase] = '" +LBestCase  + "',[MostLikelyCase] = '" +LMostLikelyCase  + "',[Notes]= '" +LNotes  + "' WHERE [ID] = "+ID,conn );
                conn.Open();
                try
                {
                    int c = comm_update.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                finally
                {
                    conn.Close();
                }

                //if (verification == 0)
                //{
                //    lblError1.Text = "The labour description alreay exists";
                //}
            }
            if (e.CommandName == "Delete")
            {
                if (sessionKeys.SID == 8)
                {
                    lblError1.Text = "You do not have permission to delete!!!";
                }
                else
                {
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    SqlCommand comm_del = new SqlCommand("UPDATE ContractorLabourTypes SET ItemDelete=1 WHERE ID=" + ID, conn);
                    conn.Open();
                    int c = comm_del.ExecuteNonQuery();
                    conn.Close();
                    if (c > 0)
                    {
                        fillGrid(1);
                    }
                }

            }

            if (e.CommandName == "MoreOptions")
            {
                if (sessionKeys.SID == 8)
                {
                    lblError1.Text = "You do not have permission to edit!!!";
                }
                else
                {
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    DataRow _dr = ServiceCatalogManager.GetLabourItem(ID);
                    txtDescription.Text = _dr["EngineerDescription"].ToString();
                    ddlRateType.SelectedValue = _dr["Ratetype"].ToString();
                    txtBuyingPrice.Text = _dr["BuyingPrice"].ToString();
                    txtSellingPrice.Text = _dr["SellingPrice"].ToString();
                    txtNotes.Text = _dr["Notes"].ToString();
                    txtLDPrice.Text = _dr["DiscountPrice"].ToString();
                    txtunitconsumption.Text = _dr["UnitConsumption"].ToString();
                    //hdnImageID.Value = _dr["Image"].ToString();
                    ItemImg = _dr["Image"].ToString();
                    ItemID = ID;
                    //hdnItemID.Value = ID.ToString();
                    btnSaveRecord.Visible = false;
                    imgbtnUpdateLabour.Visible = true;
                    //RequiredFieldValidator21.Enabled = false;
                    //RequiredFieldValidator22.Enabled = false;
                    mdl_Labour.Show();
                }
            }
        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillGrid(1);
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion

    #region Gridview / Grid Products/meterials
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        fillGrid(2);
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fillGrid(2);
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        fillGrid(2);
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView2.EditIndex;
                GridViewRow Row = GridView2.Rows[i];

                string Desc = ((TextBox)Row.FindControl("txtDesc1")).Text;
                string supp = ((DropDownList)Row.FindControl("ddlSupp")).SelectedItem.Value;
                string part = ((TextBox)Row.FindControl("txtPart1")).Text;
                string unitprice = ((TextBox)Row.FindControl("txtUnitPrice1")).Text;
                string BuyingPrice = ((TextBox)Row.FindControl("txtBuyingPrice1")).Text;
                string SellingPrice = ((TextBox)Row.FindControl("txtSellingPrice1")).Text;
                string ucp = ((TextBox)Row.FindControl("txtucp")).Text;
                string GridStockLevel = ((TextBox)Row.FindControl("txtGridStockLevel")).Text;
                string WorstCase = "0D";
                string bestcase = "0D"; ;
                string mcase = "0D";
                string notes = ((TextBox)Row.FindControl("txtNotes1")).Text;
                //Guid _guid = new Guid(((HiddenField)Row.FindControl("Hid_Image")).Value);
                //FileUpload fileUplodGird = ((FileUpload)Row.FindControl("GridFileUploadLabour"));
                double PDscntPrice = Convert.ToDouble((Convert.ToDouble(BuyingPrice.ToString())) - ((Convert.ToDouble(SellingPrice.ToString()))));
                if (PDscntPrice < 0)
                {
                    PDscntPrice = 0;
                }
                SqlCommand comm_update = new SqlCommand("DN_ServiceCatalog_MaterialUpdate", conn);
                comm_update.CommandType = CommandType.StoredProcedure;
                comm_update.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = Desc;
                comm_update.Parameters.Add("@Supplier", SqlDbType.Int).Value = supp;
                comm_update.Parameters.Add("@PartNumber", SqlDbType.NVarChar, 50).Value = part;
                comm_update.Parameters.Add("@Unit", SqlDbType.NVarChar, 50).Value = unitprice;
                comm_update.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value = BuyingPrice;
                comm_update.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = SellingPrice;
                comm_update.Parameters.Add("@UnitsinStock", SqlDbType.Int).Value = Convert.ToInt32((string.Empty == GridStockLevel ? "0" : GridStockLevel));//0;
                comm_update.Parameters.Add("@ReorderLevel", SqlDbType.Int).Value = 0;
                comm_update.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = notes;
                comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                comm_update.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;

                comm_update.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = PDscntPrice;
                comm_update.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = ucp;

                conn.Open();
                try
                {
                    int c = comm_update.ExecuteNonQuery();
                    if (c > 0)
                    {

                        fillGrid(Products);
                    }
                    else
                    {
                        lblError1.Text = "The material description already exists";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
                finally
                {
                    conn.Close();
                }


            }
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                SqlCommand comm_del = new SqlCommand("UPDATE ContractorMaterialCatalogue SET ItemDelete=1 WHERE ID=" + ID, conn);
                conn.Open();
                int c = comm_del.ExecuteNonQuery();
                conn.Close();
                if (c > 0)
                {
                    fillGrid(Products);
                }

            }
            if (e.CommandName == "MoreOptions")
            {
                lblMCatelogue.Text = "Edit Catalog Item";
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                DataRow _dr = ServiceCatalogManager.GetMaterialItem(ID);
                txtItemDesc.Text = _dr["ItemDescription"].ToString();
                ddlSupplier.SelectedValue = _dr["Supplier"].ToString();
                txtPartNumber.Text = _dr["PartNumber"].ToString();
                txtUnit.Text = _dr["UnitPrice"].ToString();
                txtMBuyingPrice.Text = _dr["BuyingPrice"].ToString();
                txtMSellingPrice.Text = _dr["SellingPrice"].ToString();
                txtStockLevel.Text = _dr["UnitsinStock"].ToString();
                txtucproduct.Text = _dr["UnitConsumption"].ToString();
                txtMNotes.Text = _dr["Notes"].ToString();
                txtMQBRefID.Text = _dr["RefID"].ToString();
                txtMItemType.Text = _dr["ItemType"].ToString();
                //Category
               

                //SubCategory

                //hdnImageID.Value = _dr["Image"].ToString();
                //hdnItemID.Value = ID.ToString();
                ItemImg = _dr["Image"].ToString();
                ItemID = ID;
                mdl_Products.Show();
                btnSaveMaterial.Visible = false;
                imgbtnUpdateMaterial.Visible = true;
                var cid = _dr["Category"].ToString();
                if (cid != null)
                    ddlCategory.SelectedValue = cid;

                bindSubcategory();

                var sid = _dr["SubCategory"].ToString();
                if (sid != null)
                    ddlSubCategory.SelectedValue = sid;

               
                //RequiredFieldValidator21.Enabled = false;
                //RequiredFieldValidator22.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView2.EditIndex = -1;
        fillGrid(Products);
    }
    #endregion

    #region GridView3 / Grid services
    protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView3.EditIndex = -1;
        fillGrid(3);
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridView3.EditIndex;
                GridViewRow Row = GridView3.Rows[i];
                Guid _guid = Guid.NewGuid();
                string SDesc = ((TextBox)Row.FindControl("txtSDesc1")).Text.Trim();
                string g3_setupbuy = ((TextBox)Row.FindControl("txtGrid3_SetupBuy")).Text.Trim();
                string g3_setupsell = ((TextBox)Row.FindControl("txtGrid3_SetupSell")).Text.Trim();
                string g3_materialsbuy = ((TextBox)Row.FindControl("txtGrid3_MaterialsBuy")).Text.Trim();
                string g3_materialssell = ((TextBox)Row.FindControl("txtGrid3_MaterialsSell")).Text.Trim();
                string g3_labourbuy = ((TextBox)Row.FindControl("txtGrid3_LabourBuy")).Text.Trim();
                string g3_laboursell = ((TextBox)Row.FindControl("txtGrid3_LabourSell")).Text.Trim();
                string ucs = ((TextBox)Row.FindControl("txtucs")).Text.Trim();

                SqlCommand comm_update = new SqlCommand("DN_ServiceCatalog_ServiceUpdate", conn);
                comm_update.CommandType = CommandType.StoredProcedure;
                comm_update.Parameters.Add("@Description", SqlDbType.NVarChar).Value = SDesc;
                comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                comm_update.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
                comm_update.Parameters.Add("@SetupBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == g3_setupbuy ? "0" : g3_setupbuy));
                comm_update.Parameters.Add("@SetupSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == g3_setupsell ? "0" : g3_setupsell));
                comm_update.Parameters.Add("@MaterialsBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == g3_materialsbuy ? "0" : g3_materialsbuy));
                comm_update.Parameters.Add("@MaterialsSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == g3_materialssell ? "0" : g3_materialssell));
                comm_update.Parameters.Add("@LabourBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == g3_labourbuy ? "0" : g3_labourbuy));
                comm_update.Parameters.Add("@LabourSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == g3_laboursell ? "0" : g3_laboursell));

                comm_update.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = 0.00;
                comm_update.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == ucs ? "0" : ucs));


                conn.Open();
                try
                {
                    int c = comm_update.ExecuteNonQuery();
                    if (c > 0)
                    {

                    }
                    else
                    {

                        lblError1.Text = "The service description already exists";
                    }
                    comm_update.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            if (e.CommandName == "Delete")
            {
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                SqlCommand comm_del = new SqlCommand("UPDATE Service SET ItemDelete=1 WHERE ID=" + ID, conn);
                conn.Open();
                int c = comm_del.ExecuteNonQuery();
                conn.Close();
                if (c > 0)
                {
                    fillGrid(3);
                }
            }
            if (e.CommandName == "MoreOptions")
            {
                hdnItemID.Value = string.Empty;
                int ID = Convert.ToInt32(e.CommandArgument.ToString());
                DataRow _dr = ServiceCatalogManager.GetServiceItem(ID);
                txtSDescription.Text = _dr["ServiceDescription"].ToString();
                txtSetupBuy.Text = _dr["SetupBuy"].ToString();
                txtSetupSell.Text = _dr["SetupSell"].ToString();
                txtMaterialsBuy.Text = _dr["MaterialsBuy"].ToString();
                txtMaterialsSell.Text = _dr["MaterialsSell"].ToString();
                txtLabourBuy.Text = _dr["LabourBuy"].ToString();
                txtLabourSell.Text = _dr["LabourSell"].ToString();
                txtSDprice.Text = _dr["DiscountPrice"].ToString();
                txtucservice.Text = _dr["UnitConsumption"].ToString();
                hdnImageID.Value = _dr["Image"].ToString();
                ItemImg = _dr["Image"].ToString();

                hdnItemID.Value = ID.ToString();
                ItemID = ID;
                
                mdl_Service.Show();
                btnSaveServices.Visible = false;
                imgbtnUpdateService.Visible = true;
                //RequiredFieldValidator21.Enabled = false;
                //RequiredFieldValidator22.Enabled = false;
                //imgbtnUpdateService.Focus();
            }
        }
        catch (Exception ex)
        {

            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView3.EditIndex = e.NewEditIndex;
        fillGrid(3);
    }
    protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView3.EditIndex = -1;
        fillGrid(3);
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        fillGrid(3);
    }
    #endregion



    //To cancel the action Adding a new record
    protected void btnCancelrow_Click(object sender, EventArgs e)
    {
        AddNewItem(int.Parse(ddlSelect.SelectedValue));
        //pnl_Labour.Visible = false;
        //pnl_Product.Visible = false;
        //pnl_Service.Visible = false;
    }
    #region check for servicce/material/labour exists or not
    public bool checkLabour(string Labour)
    {
        bool re = false;
        SqlCommand comm_chk = new SqlCommand(string.Format("select count(*) from ContractorLabourTypes where PortfolioID = {0} and EngineerDescription='{1}' and ProjectReference = 0 and ItemDelete = 0 and Category ={2} and SubCategory={3} ", PortfolioID, Labour, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue), conn);
        conn.Open();
        int c = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
        if (c == 0)
        {
            re = true;
        }
        conn.Close();
        return re;
    }
    public bool checkMaterial(string Material)
    {
        bool re = false;
        
        SqlCommand comm_chk = new SqlCommand(string.Format("select count(*) from ContractorMaterialCatalogue where PortfolioID = {0} and ItemDescription='{1}' and ProjectReference = 0 and ItemDelete = 0 and Category ={2} and SubCategory={3} and VendorID ={4} ", PortfolioID, Material, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue,QueryStringValues.Vendor),conn);
        conn.Open();
        int c = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
        if (c == 0)
        {
            re = true;
        }
        conn.Close();
        return re;
    }
    public bool checkService(string Service, int ServiceType)
    {
        bool re = false;

        SqlCommand comm_chk = new SqlCommand(string.Format("select count(*) from Service where PortfolioID = {0} and  ServiceDescription='{1}' and ProjectReference = 0 and ItemDelete = 0  and Category ={2} and SubCategory={3} and ServiceType = {4} and VendorID ={4} ", PortfolioID, Service.ToString(), ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ServiceType,QueryStringValues.Vendor), conn);
        conn.Open();
        int c = Convert.ToInt32(comm_chk.ExecuteScalar().ToString());
        if (c == 0)
        {
            re = true;
        }
        conn.Close();
        return re;
    }
    #endregion
    //To Save the new record
    protected void btnSaveRecord_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlCategory.SelectedValue) > 0)
        {
            if (int.Parse(ddlSubCategory.SelectedValue) > 0)
            {
                bool saved = false;
                //Sql Connection
                if (checkLabour(txtDescription.Text) == true)
                {
                    try
                    {
                        SqlCommand cmd;
                        int i = 0;
                        //To save Labour
                        if ((pnl_Labour.Visible == true) && (!saved))
                        {
                            if (ddlSelect.SelectedItem.Text == "Labour")
                            {
                                if (txtDescription.Text != "")
                                {

                                    Guid _guid = Guid.NewGuid();
                                    cmd = new SqlCommand("DN_LabourServiceCatalogInsert", conn);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@ContractorID", SqlDbType.Int).Value = sessionKeys.UID;
                                    cmd.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = PortfolioID;
                                    cmd.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = 0;
                                    cmd.Parameters.Add("@ItemDescription", SqlDbType.NVarChar, 500).Value = txtDescription.Text;
                                    cmd.Parameters.Add("@RateType", SqlDbType.Int).Value = ddlRateType.SelectedItem.Value;
                                    cmd.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtBuyingPrice.Text.Trim() ? "0" : txtBuyingPrice.Text.Trim()));
                                    cmd.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtSellingPrice.Text.Trim() ? "0" : txtSellingPrice.Text.Trim()));
                                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = txtNotes.Text;
                                    cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
                                    if (FileUploadLabour.HasFile)
                                    {
                                        cmd.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = _guid;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = Guid.Empty;
                                    }
                                    cmd.Parameters.Add("@Category", SqlDbType.Int).Value = GetCatagoryID();
                                    cmd.Parameters.Add("@SubCategory", SqlDbType.Int).Value = GetSubCatagoryID();
                                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
                                    cmd.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                                    cmd.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                                    cmd.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = ((string.Empty == txtLDPrice.Text.Trim() ? "0" : txtLDPrice.Text.Trim()));
                                    cmd.Parameters.Add("@RteToServcTeam", SqlDbType.Int).Value = Convert.ToInt32((ddlTeam.SelectedItem == null) ? "0" : ddlTeam.SelectedItem.Value);
                                    cmd.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = ((string.Empty == txtunitconsumption.Text.Trim() ? "0" : txtunitconsumption.Text.Trim()));
                                    conn.Open();
                                    try
                                    {
                                        i = cmd.ExecuteNonQuery();
                                        saved = true;

                                        if (FileUploadLabour.HasFile)
                                        {
                                            ImageManager.SaveImage(_guid, FileUploadLabour.FileBytes);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogExceptions.WriteExceptionLog(ex);
                                    }
                                    finally
                                    {
                                        conn.Close();
                                    }
                                }
                                else if ((txtDescription.Text == ""))
                                {
                                    RequiredFieldDescription.IsValid = false;
                                }

                            }


                            //Check the Bool value
                            if (saved)
                            {
                                fillGrid(int.Parse(ddlSelect.SelectedValue));
                                AddNewItem(int.Parse(ddlSelect.SelectedValue));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //call the error page if any error occurs
                        LogExceptions.LogException(ex.Message);

                    }
                    finally
                    {
                        //Check the connection state to close
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }

                }
                else
                {
                    lblError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Labour description already exists";
                }
            }
            else
            {
                //lblError.Visible = true;
                //lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please select "+ Deffinity.systemdefaults.GetSubCategoryName(); ;
            }
        }
        else
        {
            //lblError.Visible = true;
            //lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please select "+ Deffinity.systemdefaults.GetCategoryName(); ;
        }


    }

    protected void btnSaveMaterial_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        //{
            //if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            //{

                bool saved = false;
                if (checkMaterial(txtItemDesc.Text) == true)
                {
                    try
                    {
                        int i = 0;
                        //To save Material details
                        if ((pnl_Product.Visible == true) && (!saved))
                        {
                            //Check validations
                            string supplier = "";
                            if (ddlSupplier.Visible == true)
                            {
                                supplier = ddlSupplier.SelectedItem.Text.ToString();
                            }
                            else if (txtSupplier.Visible == true)
                            {
                                supplier = txtSupplier.Text;
                            }
                            if (!string.IsNullOrEmpty(supplier))
                            {
                                Guid _guid = Guid.NewGuid();
                                //string s1, s2, s3;
                                //s1 = txtMWorstCase.Text.ToString();
                                //s2 = txtMBestCase.Text.ToString();
                                //s3 = txtMMCase.Text.ToString();
                                SqlCommand cmd = new SqlCommand("DN_MaterialServiceCatalogInsert", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@ContractorID", SqlDbType.Int).Value = sessionKeys.UID;
                                cmd.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = PortfolioID;
                                cmd.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add("@ItemDescription", SqlDbType.NVarChar).Value = txtItemDesc.Text;
                                //cmd.Parameters.Add("@Supplier", SqlDbType.NVarChar, 100).Value = supplier;
                                cmd.Parameters.Add("@Supplier", SqlDbType.Int,32).Value = int.Parse(ddlSupplier.SelectedValue);
                                cmd.Parameters.Add("@PartNumber", SqlDbType.NVarChar, 50).Value = txtPartNumber.Text;
                                //cmd.Parameters.Add("@UnitPrice", SqlDbType.NVarChar, 50).Value = txtUnit.Text;
                                cmd.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtMBuyingPrice.Text.Trim() ? "0" : txtMBuyingPrice.Text.Trim()));
                                cmd.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtMSellingPrice.Text.Trim() ? "0" : txtMSellingPrice.Text.Trim()));
                                cmd.Parameters.Add("@UnitsinStock", SqlDbType.Int).Value = Convert.ToInt32((string.Empty == txtStockLevel.Text.Trim() ? "0" : txtStockLevel.Text.Trim()));//"0";
                                cmd.Parameters.Add("@ReorderLevel", SqlDbType.Int).Value = "0";//Convert.ToInt32((string.Empty == txtRecorderLevel.Text.Trim() ? "0" : txtRecorderLevel.Text.Trim()));
                                cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = txtMNotes.Text;
                                cmd.Parameters.Add("@RefID", SqlDbType.NVarChar).Value = txtMQBRefID.Text;
                                cmd.Parameters.Add("@ItemType", SqlDbType.NVarChar).Value = txtMItemType.Text;
                                cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = GetCatagoryID();
                                cmd.Parameters.Add("@SubCategory", SqlDbType.Int).Value = GetSubCatagoryID();
                                if (FileUploadMaterial.HasFile)
                                {
                                    cmd.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = _guid;
                                }
                                else
                                {

                                    cmd.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = Guid.Empty;
                                }
                                cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
                                cmd.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                                cmd.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                                cmd.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtPDPrice.Text.Trim() ? "0" : txtPDPrice.Text.Trim()));
                                cmd.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtucproduct.Text.Trim() ? "0" : txtucproduct.Text.Trim()));
                                conn.Open();
                                try
                                {
                                    i = cmd.ExecuteNonQuery();
                                    conn.Close();
                                    saved = true;
                                    if (FileUploadMaterial.HasFile)
                                    {
                                        ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                                    }
                                    //after addding 
                            Response.Redirect(Request.RawUrl,false);
                           // AddNewItem(2);
                           // mdl_Products.Hide();
                                }
                                catch (Exception ex)
                                {
                                    //cmpRate.IsValid = false;
                                    LogExceptions.WriteExceptionLog(ex);
                                }
                            }
                            else
                            {
                                lblErrMaterial.Visible = true;
                                lblErrMaterial.Text = "Please select/enter supplier";
                            }

                        }
                        if (saved)
                        {
                            fillGrid(int.Parse(ddlSelect.SelectedValue));
                            AddNewItem(int.Parse(ddlSelect.SelectedValue));
                        }
                    }
                    catch (Exception ex)
                    {
                        //call the error page if any error occurs
                        LogExceptions.LogException(ex.Message);
                        //
                    }
                    finally
                    {
                        //Check the connection state to close
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }

                else
                {
                    lblErrMaterial.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblErrMaterial.Text = "Description already exists";
                }

            //}

            //else
            //{
            //    //lblErrMaterial.Visible = true;
            //    //lblError.ForeColor = System.Drawing.Color.Red;
            //    lblErrMaterial.Text = "Please select " + Deffinity.systemdefaults.GetSubCategoryName(); ;
            //    mdl_Products.Show();
            //}
       // }
        //else
        //{
        //    //lblErrMaterial.Visible = true;
        //    //lblError.ForeColor = System.Drawing.Color.Red;
        //    lblErrMaterial.Text = "Please select " + Deffinity.systemdefaults.GetCategoryName(); ;
        //    mdl_Products.Show();
        //}


    }

    protected void btnAddSupplier_Click(object sender, EventArgs e)
    {
        txtSupplier.Visible = true;
        ddlSupplier.Visible = false;
        btnAddSupplier.Visible = false;
        btnCancelSupplier.Visible = true;

    }
    protected void btnCancelSupplier_Click(object sender, EventArgs e)
    {
        txtSupplier.Visible = false;
        ddlSupplier.Visible = true;
        btnCancelSupplier.Visible = false;
    }

    protected void btnSaveServices_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCategory.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlSubCategory.SelectedValue) > 0)
            {
                try
                {
                    if (checkService(txtSDescription.Text, Service) == true)
                    {
                        //DN_CatalogServicesInsertNew
                        //DN_CatalogServicesInsert
                        bool saved = false;
                        Guid _guid = Guid.NewGuid();
                        SqlCommand comm_add = new SqlCommand("DN_CatalogServicesInsertNew", conn);
                        comm_add.CommandType = CommandType.StoredProcedure;
                        comm_add.Parameters.Add("@ContractorID", SqlDbType.Int).Value = sessionKeys.UID;
                        comm_add.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = PortfolioID;
                        comm_add.Parameters.Add("@ProjectReference", SqlDbType.Int).Value = 0;
                        comm_add.Parameters.Add("@Descrption", SqlDbType.NVarChar).Value = txtSDescription.Text;

                        comm_add.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
                        comm_add.Parameters.Add("@Category", SqlDbType.Int).Value = GetCatagoryID();
                        comm_add.Parameters.Add("@SubCategory", SqlDbType.Int).Value = GetSubCatagoryID();
                        if (FileUploadService.HasFile)
                        {
                            comm_add.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = _guid;
                        }
                        else
                        {
                            comm_add.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = Guid.Empty;
                        }
                        comm_add.Parameters.Add("@Type", SqlDbType.Int).Value = 0;
                        comm_add.Parameters.Add("@SetupBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtSetupBuy.Text.Trim() ? "0" : txtSetupBuy.Text.Trim()));
                        comm_add.Parameters.Add("@SetupSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtSetupSell.Text.Trim() ? "0" : txtSetupSell.Text.Trim()));
                        comm_add.Parameters.Add("@MaterialsBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtMaterialsBuy.Text.Trim() ? "0" : txtMaterialsBuy.Text.Trim()));
                        comm_add.Parameters.Add("@MaterialsSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtMaterialsSell.Text.Trim() ? "0" : txtMaterialsSell.Text.Trim()));
                        comm_add.Parameters.Add("@LabourBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtLabourBuy.Text.Trim() ? "0" : txtLabourBuy.Text.Trim()));
                        comm_add.Parameters.Add("@LabourSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtLabourSell.Text.Trim() ? "0" : txtLabourSell.Text.Trim()));
                        comm_add.Parameters.Add("@ServiceType", SqlDbType.Float).Value = Service;
                        comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                        comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                        comm_add.Parameters.Add("@DiscountPrice", SqlDbType.Int).Value = Convert.ToDouble((string.Empty == txtSDprice.Text.Trim() ? "0" : txtSDprice.Text.Trim()));
                        comm_add.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtucservice.Text.Trim() ? "0" : txtucservice.Text.Trim()));
                        double setupBuy = Convert.ToDouble((string.Empty == txtSetupBuy.Text.Trim() ? "0" : txtSetupBuy.Text.Trim()));
                        double materialbuy =Convert.ToDouble((string.Empty == txtMaterialsBuy.Text.Trim() ? "0" : txtMaterialsBuy.Text.Trim()));
                        double labourBuy = Convert.ToDouble((string.Empty == txtLabourBuy.Text.Trim() ? "0" : txtLabourBuy.Text.Trim()));
                        double setupSell = Convert.ToDouble((string.Empty == txtSetupSell.Text.Trim() ? "0" : txtSetupSell.Text.Trim()));
                            double materialSell = Convert.ToDouble((string.Empty == txtMaterialsSell.Text.Trim() ? "0" : txtMaterialsSell.Text.Trim()));
                            double labourSell = Convert.ToDouble((string.Empty == txtLabourSell.Text.Trim() ? "0" : txtLabourSell.Text.Trim()));

                            double TotSerBuy = setupBuy + materialbuy + labourBuy;
                        double TotSerSell = setupSell + materialSell + labourSell;
                        //double GP =0.00;
                        //double temp= 0.00;
                        //if(TotSerSell >0)
                        //{
                        //    temp = (((TotSerSell- TotSerBuy)/TotSerSell))*100);
                        //}
                        comm_add.Parameters.Add("@TotalServiceBuy", SqlDbType.Float).Value = TotSerBuy;
                        comm_add.Parameters.Add("@TotalServiceSell", SqlDbType.Float).Value = TotSerSell;
                        //comm_add.Parameters.Add("@GP", SqlDbType.Float).Value =GP ;
                        conn.Open();
                        try
                        {
                            int i = comm_add.ExecuteNonQuery();
                            saved = true;
                            if (FileUploadService.HasFile)
                            {
                                ImageManager.SaveImage(_guid, FileUploadService.FileBytes);
                            }

                        }
                        catch (Exception ex)
                        {
                            LogExceptions.LogException(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                        if (saved == true)
                        {
                            fillGrid(int.Parse(ddlSelect.SelectedValue));
                            AddNewItem(int.Parse(ddlSelect.SelectedValue));
                        }

                    }
                    else
                    {
                        lblErrService.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblErrService.Text = "Service description already exists";
                        mdl_Service.Show();
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }

            else
            {
               
                lblErrService.Text = "Please select " + Deffinity.systemdefaults.GetSubCategoryName(); ;
                mdl_Service.Show();
            }
        }
        else
        {
            
            lblErrService.Text = "Please select " + Deffinity.systemdefaults.GetCategoryName(); ;
            mdl_Service.Show();
        }

    }
    protected void btnCancelServces_Click(object sender, EventArgs e)
    {
        AddNewItem(int.Parse(ddlSelect.SelectedValue));

    }
    protected string getCase(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) || (s1 == "0"))
        {
            s1 = "0";
            s2 = "M";
        }
        return s1 + s2;
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        sessionKeys.Cataloguetype = 0;
        fillGrid(int.Parse(ddlSelect.SelectedValue));
        AddNewItem(int.Parse(ddlSelect.SelectedValue));

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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubcategory();
       // fillGrid(int.Parse(ddlSelect.SelectedValue));
    }

    private void bindSubcategory()
    {
        //BaseBindings();
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
        panelVisibleSubCategory(true, false);
    }
    private void panelVisibleCategory(bool ddl, bool txt)
    {
        pnladdcategory.Visible = txt;
        pnlcategory.Visible = ddl;
        if (pnlcategory.Visible == true)
        {
           // btnAddnew.Visible = true;
        }
        else
        {
          //  btnAddnew.Visible = false;
        }

    }
    private void panelVisibleSubCategory(bool ddl, bool txt)
    {
        pnladdsubcategory.Visible = txt;
        pnlsubcategory.Visible = ddl;

        if (pnlsubcategory.Visible == true)
        {
           // btnAddnew.Visible = true;
            // need to write code here
            //if(ddlSelect.va
        }
        else
        {
            //btnAddnew.Visible = false;
        }

    }
    protected void btnaddcategory_Click(object sender, EventArgs e)
    {
        txtAddCategory.Text = string.Empty;
        panelVisibleCategory(false, true);
        bindSubcategory();
    }
    protected void btnaddsubcategory_Click(object sender, EventArgs e)
    {
        txtAddSubCategory.Text = string.Empty;
        panelVisibleSubCategory(false, true);
    }
    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(HID_Category.Value))
        {
            HID_Category.Value = string.Empty;
            txtAddCategory.Text = string.Empty;
        }
        panelVisibleCategory(true, false);
        bindSubcategory();
    }
    protected void btnCancelSubCategory_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(HID_SubCategory.Value))
        {
            HID_SubCategory.Value = string.Empty;
            txtAddSubCategory.Text = string.Empty;
        }
        panelVisibleSubCategory(true, false);

    }
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(HID_Category.Value))
            {
                int msg = UpdateCategory(int.Parse(HID_Category.Value), txtAddCategory.Text.Trim(), 0);
                if (msg == 1)
                {
                    lblError1.ForeColor = System.Drawing.Color.Green;
                    lblError1.Text = "Category updated successfully";
                    BaseBindings();
                    panelVisibleCategory(true, false);
                    ddlCategory.SelectedValue = HID_Category.Value;
                    HID_Category.Value = string.Empty;
                    txtAddCategory.Text = string.Empty;
                }
                else
                {
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    lblError1.Text = "Please check Category name already exists.";
                }


            }
            else
            {
                SqlCommand comm_add = new SqlCommand("Deffinity_InsertServiceCategory", conn);
                comm_add.CommandType = CommandType.StoredProcedure;
                comm_add.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = txtAddCategory.Text.Trim();
                comm_add.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = PortfolioID;
                comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                SqlParameter outCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int);
                outCategoryID.Direction = ParameterDirection.Output;
                comm_add.Parameters.Add(outCategoryID);
                conn.Open();
                try
                {
                    int i = comm_add.ExecuteNonQuery();
                    if (i > 0)
                    {
                        lblError1.ForeColor = System.Drawing.Color.Green;
                        lblError1.Text = "Category added successfully";
                        BaseBindings();
                        panelVisibleCategory(true, false);
                        BaseBindings();
                        ddlCategory.SelectedValue = comm_add.Parameters["@CategoryID"].Value.ToString();
                        txtAddCategory.Text = string.Empty;


                    }
                    else if (i == -1)
                    {
                        lblError1.ForeColor = System.Drawing.Color.Red;
                        lblError1.Text = "Category Already Exists";
                    }
                   
                   
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        //Bind sub category
        bindSubcategory();

    }
    private int UpdateCategory(int ID, string Name, int type)
    {
        int retval = 0;
        SqlCommand comm_add = new SqlCommand("Deffinity_UpdateServiceCategory", conn);
        comm_add.CommandType = CommandType.StoredProcedure;
        comm_add.Parameters.Add("@CategoryID", SqlDbType.Int).Value = ID;
        comm_add.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = Name;
        comm_add.Parameters.Add("@Type", SqlDbType.Int).Value = type;
        //comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
        //comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value =(QueryStringValues.Vendor == null ? 0 : QueryStringValues.Vendor);
        SqlParameter outCategoryID = new SqlParameter("@Outval", SqlDbType.Int);
        outCategoryID.Direction = ParameterDirection.Output;
        comm_add.Parameters.Add(outCategoryID);
        conn.Open();
        try
        {
            int i = comm_add.ExecuteNonQuery();
            BaseBindings();
            retval = int.Parse(comm_add.Parameters["@Outval"].Value.ToString());

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        finally
        {
            conn.Close();
        }
        return retval;
    }
    protected void btnSaveSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(HID_SubCategory.Value))
            {
                int msg = UpdateCategory(int.Parse(HID_SubCategory.Value), txtAddSubCategory.Text.Trim(), 1);
                if (msg == 1)
                {
                    lblError1.ForeColor = System.Drawing.Color.Green;
                    lblError1.Text = "Sub Category updated successfully";
                    bindSubcategory();
                    panelVisibleSubCategory(true, false);
                    ddlSubCategory.SelectedValue = HID_SubCategory.Value;
                    HID_SubCategory.Value = string.Empty;
                    txtAddSubCategory.Text = string.Empty;
                }
                else
                {
                    lblError1.ForeColor = System.Drawing.Color.Red;
                    lblError1.Text = "Please check Sub Category name already exists.";
                }
            }
            else
            {
                SqlCommand comm_add = new SqlCommand("Deffinity_InsertServiceSubCategory", conn);
                comm_add.CommandType = CommandType.StoredProcedure;
                comm_add.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = txtAddSubCategory.Text.Trim();
                comm_add.Parameters.Add("@PortfolioID", SqlDbType.Int).Value = PortfolioID;
                comm_add.Parameters.Add("@MasterID", SqlDbType.Int).Value = Convert.ToInt32(ddlCategory.SelectedValue);
                comm_add.Parameters.Add("@PageType", SqlDbType.Int).Value = PageType;
                comm_add.Parameters.Add("@VendorID", SqlDbType.Int).Value = VendorID;
                SqlParameter outCategoryID = new SqlParameter("@SubCategoryID", SqlDbType.Int);
                outCategoryID.Direction = ParameterDirection.Output;
                comm_add.Parameters.Add(outCategoryID);
                conn.Open();
                try
                {
                    comm_add.ExecuteNonQuery();
                    int i = Convert.ToInt32(comm_add.Parameters["@SubCategoryID"].Value);
                    if (i > 0)
                    {
                        lblError1.ForeColor = System.Drawing.Color.Green;
                        lblError1.Text = "Sub Category added successfully";
                        bindSubcategory();
                        ddlSubCategory.SelectedValue = comm_add.Parameters["@SubCategoryID"].Value.ToString();
                        txtAddSubCategory.Text = string.Empty;
                    }
                    else if (i == -1)
                    {
                        lblError1.ForeColor = System.Drawing.Color.Red;
                        lblError1.Text = "Sub Category Already Exists";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.LogException(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    private int GetCatagoryID()
    {
        int retval = 0;
        try
        {
            retval = int.Parse(ddlCategory.SelectedValue);
            //if (ddlCategory.Visible == true)
            //{
               
            //}
        }
        catch (Exception ex)
        {

            retval = 0;
        }
        return retval;
    }
    private int GetSubCatagoryID()
    {
        int retval = 0;
        try
        {
            retval = int.Parse(ddlSubCategory.SelectedValue);
            //if (ddlSubCategory.Visible == true)
            //{

            //}
        }
        catch (Exception ex)
        {

            retval = 0;
        }
        return retval;
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid(int.Parse(ddlSelect.SelectedValue));
    }
    protected void btn_CategoryEdit_Click(object sender, EventArgs e)
    {

        if (ddlCategory.Visible == true)
        {
            if (ddlCategory.SelectedValue != "0")
            {
                HID_Category.Value = ddlCategory.SelectedValue;
                txtAddCategory.Text = ddlCategory.SelectedItem.Text;
                panelVisibleCategory(false, true);
            }
        }
    }
    protected void btn_editSubCategory_Click(object sender, EventArgs e)
    {
        if (ddlSubCategory.Visible == true)
        {
            if (ddlSubCategory.SelectedValue != "0")
            {
                HID_SubCategory.Value = ddlSubCategory.SelectedValue;
                txtAddSubCategory.Text = ddlSubCategory.SelectedItem.Text;
                panelVisibleSubCategory(false, true);
            }
        }
    }
    #region Delete Category
    protected void btnDeleteCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "0")
                ServiceCatalogManager.DeleteCategory(int.Parse(ddlCategory.SelectedValue));

            BaseBindings();
            bindSubcategory();
            fillGrid(int.Parse(ddlSelect.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void btnSubCategory_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubCategory.SelectedValue != "0")
                ServiceCatalogManager.DeleteSubCategory(int.Parse(ddlSubCategory.SelectedValue));

            bindSubcategory();
            fillGrid(int.Parse(ddlSelect.SelectedValue));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
    protected void imgbtnUpdateService_Click(object sender, EventArgs e)
    {
        int ID = ItemID;
        
        //Guid _guid = new Guid(hdnImageID.Value);
        Guid _guid = new Guid(ItemImg);
        if ((_guid == Guid.Empty) && (FileUploadService.HasFile))
        {
            _guid = Guid.NewGuid();
        }
        SqlCommand comm_update = new SqlCommand("DN_ServiceCatalog_ServiceUpdate", conn);
        comm_update.CommandType = CommandType.StoredProcedure;
        comm_update.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = txtSDescription.Text;
        comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        comm_update.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
        comm_update.Parameters.Add("@SetupBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtSetupBuy.Text ? "0" : txtSetupBuy.Text));
        comm_update.Parameters.Add("@SetupSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtSetupSell.Text ? "0" : txtSetupSell.Text));
        comm_update.Parameters.Add("@MaterialsBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtMaterialsBuy.Text ? "0" : txtMaterialsBuy.Text));
        comm_update.Parameters.Add("@MaterialsSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtMaterialsSell.Text ? "0" : txtMaterialsSell.Text));
        comm_update.Parameters.Add("@LabourBuy", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtLabourBuy.Text ? "0" : txtLabourBuy.Text));
        comm_update.Parameters.Add("@LabourSell", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtLabourSell.Text ? "0" : txtLabourSell.Text));
        comm_update.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = _guid;
        comm_update.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtSDprice.Text ? "0" : txtSDprice.Text));
        comm_update.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtucservice.Text ? "0" : txtucservice.Text));
        conn.Open();
        try
        {
            int c = comm_update.ExecuteNonQuery();
            if (c > 0)
            {
                // verification = Convert.ToInt32(comm_update.Parameters["@flag"].Value);
                if (FileUploadService.HasFile)
                {
                    ImageManager.SaveImage(_guid, FileUploadService.FileBytes);
                }
                fillGrid(int.Parse(ddlSelect.SelectedValue));
                AddNewItem(int.Parse(ddlSelect.SelectedValue));
                ItemID = 0;
                ItemImg = string.Empty;
            }
            else
            {
                //verification = 0;

                lblError1.ForeColor = System.Drawing.Color.Red;
                lblError1.Text = "Service description already exists";
            }
            comm_update.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
    }
    protected void imgbtnUpdateMaterial_Click(object sender, EventArgs e)
    {
        int ID = ItemID;

        
        Guid _guid = new Guid(ItemImg);
        if ((_guid == Guid.Empty) && (FileUploadMaterial.HasFile))
        {
            _guid = Guid.NewGuid();
        }


        SqlCommand comm_update = new SqlCommand("DN_ServiceCatalog_MaterialUpdate", conn);
        comm_update.CommandType = CommandType.StoredProcedure;
        comm_update.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = txtItemDesc.Text;
        comm_update.Parameters.Add("@Supplier", SqlDbType.Int).Value = Convert.ToInt32(ddlSupplier.SelectedValue);
        comm_update.Parameters.Add("@PartNumber", SqlDbType.NVarChar, 50).Value = txtPartNumber.Text;
        comm_update.Parameters.Add("@Unit", SqlDbType.NVarChar, 50).Value = txtUnit.Text;
        comm_update.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value = txtMBuyingPrice.Text;
        comm_update.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = txtMSellingPrice.Text;
        comm_update.Parameters.Add("@UnitsinStock", SqlDbType.Int).Value = txtStockLevel.Text;//0;
        comm_update.Parameters.Add("@ReorderLevel", SqlDbType.Int).Value = 0;
        comm_update.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = txtMNotes.Text;
        comm_update.Parameters.Add("@RefID", SqlDbType.NVarChar).Value = txtMQBRefID.Text;
        comm_update.Parameters.Add("@ItemType", SqlDbType.NVarChar).Value = txtMItemType.Text;
        comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        comm_update.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
        comm_update.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = _guid;
        comm_update.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtPDPrice.Text.Trim() ? "0" : txtPDPrice.Text.Trim()));
        comm_update.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = Convert.ToDouble((string.Empty == txtucproduct.Text.Trim() ? "0" : txtucproduct.Text.Trim()));
        comm_update.Parameters.Add("@Category", SqlDbType.Int).Value = GetCatagoryID();
        comm_update.Parameters.Add("@SubCategory", SqlDbType.Int).Value = GetSubCatagoryID();
        conn.Open();
        try
        {
            int c = comm_update.ExecuteNonQuery();
            if (c > 0)
            {
                if (FileUploadMaterial.HasFile)
                {
                    ImageManager.SaveImage(_guid, FileUploadMaterial.FileBytes);
                }

                fillGrid(int.Parse(ddlSelect.SelectedValue));
                AddNewItem(int.Parse(ddlSelect.SelectedValue));
                ItemID = 0;
                ItemImg = string.Empty;
            }
            else
            {
                lblError1.ForeColor = System.Drawing.Color.Red;
                lblError1.Text = "The material description already exists";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            conn.Close();
        }


    }
    protected void imgbtnUpdateLabour_Click(object sender, EventArgs e)
    {
        int ID = ItemID;

       
        Guid _guid = new Guid(ItemImg);
        if ((_guid == Guid.Empty) && (FileUploadLabour.HasFile))
        {
            _guid = Guid.NewGuid();
        }
        SqlCommand comm_update = new SqlCommand("DN_ServiceCatalog_LabourUpdate", conn);
        comm_update.CommandType = CommandType.StoredProcedure;
        comm_update.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = txtDescription.Text;
        comm_update.Parameters.Add("@RateType", SqlDbType.Int).Value = Convert.ToInt32(ddlRateType.SelectedValue);
        comm_update.Parameters.Add("@BuyingPrice", SqlDbType.Float).Value = Convert.ToDouble(txtBuyingPrice.Text);
        comm_update.Parameters.Add("@SellingPrice", SqlDbType.Float).Value = Convert.ToDouble(txtSellingPrice.Text);
        comm_update.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = txtNotes.Text;
        comm_update.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        comm_update.Parameters.Add("@QTY", SqlDbType.Int).Value = 0;
        comm_update.Parameters.Add("@Image", SqlDbType.UniqueIdentifier).Value = _guid;
        comm_update.Parameters.Add("@DiscountPrice", SqlDbType.Float).Value = Convert.ToDouble(txtLDPrice.Text);
        comm_update.Parameters.Add("@RteToServcTeam", SqlDbType.Int).Value = Convert.ToInt32((ddlTeam.SelectedItem == null) ? "0" : ddlTeam.SelectedItem.Value);
        comm_update.Parameters.Add("@UnitConsumption", SqlDbType.Float).Value = Convert.ToDouble(txtunitconsumption.Text);
        conn.Open();
        try
        {
            int c = comm_update.ExecuteNonQuery();
            if (c > 0)
            {

                if (FileUploadLabour.HasFile)
                {
                    ImageManager.SaveImage(_guid, FileUploadLabour.FileBytes);
                }
                fillGrid(int.Parse(ddlSelect.SelectedValue));
                AddNewItem(int.Parse(ddlSelect.SelectedValue));
                ItemID = 0;
                ItemImg = string.Empty;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        finally
        {
            conn.Close();
        }
    }
    //protected void imgbtnUpdateService_Clic(object sender, EventArgs e)
    //{

    //}
   
    protected void Title(bool _iscustomer, bool _isvendor)
    {
       // divcustomer.Visible = _iscustomer;
       // VendorRef1.Visible = _isvendor;
    }

    #region Default Dropdowns
    private void BaseBindings()
    {
        //DataTable DT_Category;
        //DT_Category = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetServiceCategory",
        //            new SqlParameter[] { new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@PageType", PageType), new SqlParameter("@VendorID", (QueryStringValues.Vendor == null ? 0 : QueryStringValues.Vendor)) }).Tables[0];
        var categoyList = CategoryBAL.GetCategoryList().ToList();
        ddlCategory.DataSource = categoyList;
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("Please select...","0"));
    }
    #endregion 
   

    protected void lnkOk_Click(object sender, EventArgs e)
    {
        int items = 0;
        try
        {
           
            for (int i = 0; i < grd_Customers.Rows.Count; i++)
            {
                GridViewRow row = grd_Customers.Rows[i];
                CheckBox chkbox1 = (CheckBox)row.FindControl("chkItem");

                if (chkbox1.Checked)
                {
                    items++;
                    Label lblID = (Label)row.FindControl("lblID");
                    int FromPortfolio = sessionKeys.PortfolioID;
                    int ToPortfolio = int.Parse(lblID.Text);
                    SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure, "DN_CopyCatalogue", new SqlParameter[] { new SqlParameter("@FromPortfolio", FromPortfolio), new SqlParameter("@ToPortfolio", ToPortfolio), new SqlParameter("@ContractorID", sessionKeys.UID) });
                    chkbox1.Checked = false;
                    //lblGridMsg.Text = "Insertion failed. Please try again.";
                }
            }

            if (items == 0)
            {
                //lblError1.ForeColor = System.Drawing.Color.Red;
                lblError1.Text = "Please select atleast one item";
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void grd_Customers_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //check the empty row containg '-99' or not 
            //if yes then hide that row
            object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
            if (objList != null)
            {
                if (objList[0].ToString() == "0")
                {
                    e.Row.Visible = false;
                }
            }

            
        }

    }

    #region view state 
        
    public int ItemID
    {
        set { ViewState["itemid"] = value; }
        get { return int.Parse(string.IsNullOrEmpty(ViewState["itemid"].ToString()) ? "0" : ViewState["itemid"].ToString()); }
    }
    public string ItemImg
    {
        set { ViewState["itemimg"] = value; }
        get { return string.IsNullOrEmpty(ViewState["itemimg"].ToString()) ? string.Empty : ViewState["itemimg"].ToString(); }
    }

    #endregion

    //protected void txtSellingPrice_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        txtLDPrice.Text = txtSellingPrice.Text;
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}


    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {

        BindToExcel(ddlSelect.SelectedItem.Text,sessionKeys.PortfolioID);
    }

    protected void btnCopyToCatalogue_Click(object sender, EventArgs e)
    {
        try
        {
            string s = ddlPortfolio_copyto.SelectedValue;
            if (!string.IsNullOrEmpty(ddlPortfolio_copyto.SelectedValue))
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ServiceCatelog_Copy", new SqlParameter("@copy_from", sessionKeys.PortfolioID), new SqlParameter("@copy_to", Convert.ToInt32(ddlPortfolio_copyto.SelectedValue)));
                lblCopymessage.Text = "copied successfully";
                lblCopymessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblCopymessage.Text = "Please select customer";
                lblCopymessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region Download Export
    private void BindToExcel(string filetype, int PortfolioID)
    {

        string filename_template = @Server.MapPath(string.Format("~/WF/uploaddata/templates/servicecatelog_{0}.xlsx", filetype));
        string filename =  @Server.MapPath("~/WF/uploaddata/Temp/") + string.Format("servicecatelog_{0}_{1}.xlsx", filetype, string.Format("{0:ddMMyyyyHHmmss}", DateTime.Now));
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
        if (fileInfo.Exists)
        {
            File.Delete(filename);
            File.Copy(filename_template, filename);
        }
        else
        {
            File.Copy(filename_template, filename);
        }

        if (ddlSelect.SelectedValue == "1")
            ExportToExcel_Labour(filename, PortfolioID);
        else if (ddlSelect.SelectedValue == "2")
            ExportToExcel_Material(filename, PortfolioID);
        else if (ddlSelect.SelectedValue == "3")
            ExportToExcel_Service(filename, PortfolioID);
        System.IO.FileInfo fileInfo1 = new System.IO.FileInfo(filename);
        if (fileInfo1.Exists)
        {


            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileInfo1.Name);
            Response.AddHeader("Content-Type", "application/Excel");
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("Content-Length", fileInfo1.Length.ToString());
            Response.WriteFile(fileInfo1.FullName);
            Response.Flush();
            Response.End();


        }



    }

    public static int ExportToExcel(DataTable dt, string excelFile, string sheetName)
    {
        // Create the connection string
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            excelFile + ";Extended Properties=Excel 12.0 Xml;";

        int rNumb = 0;
        try
        {
            using (OleDbConnection con = new OleDbConnection(connString))
            {
                con.Open();

                // Build the field names string
                StringBuilder strField = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count - 1; i++)
                {
                    strField.Append("[" + dt.Columns[i].ColumnName + "],");
                }
                strField = strField.Remove(strField.Length - 1, 1);

                // Create Excel sheet
                var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                OleDbCommand cmd = new OleDbCommand(sqlCmd, con);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    // Insert data into Excel sheet
                    StringBuilder strValue = new StringBuilder();
                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        if (dt.Columns[j].ColumnName == "Date Added")
                        {
                            strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString().Remove(11)) + "',");
                        }
                        else
                        {
                            strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString()) + "',");
                        }
                        ////if (j < 4)
                        ////{
                        //strValue.Append("'" + AddSingleQuotes(dt.Rows[i][j].ToString()) + "',");
                        ////}
                        ////else
                        ////{
                        ////    strValue.Append(AddSingleQuotes(dt.Rows[i][j].ToString()) + ",");
                        ////}

                    }
                    strValue = strValue.Remove(strValue.Length - 1, 1);

                    cmd.CommandText = "INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" +
                            strValue.ToString() + ")";
                    cmd.ExecuteNonQuery();
                    rNumb = i + 1;
                }


                con.Close();
            }
            return rNumb;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
    public static int ExportToExcel_Material(string excelFile, int PortfolioID)
    {
        // Create the connection string
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            excelFile + ";Extended Properties=Excel 12.0 Xml;";
        string sheetName = string.Empty;
        string[] MaterialHeader = new string[] { "CATEGORY", "SUB-CATEGORY", "PART NUMBER", "DESCRIPTION", "NSP PRICE", "ACTUAL BUY", "% MARK UP", "SELL PRICE", "DISCOUNT PRICE" };

        int rNumb = 0;
        try
        {
            using (OleDbConnection con = new OleDbConnection(connString))
            {


                // Build the field names string
                StringBuilder strField = new StringBuilder();
                foreach (string m in MaterialHeader)
                {
                    strField.Append("[" + m + "],");

                }

                strField = strField.Remove(strField.Length - 1, 1);

                List<RFI.Entity.VendorDetails> v = new List<RFI.Entity.VendorDetails>();
                using (RFI.DAL.RFIDataContext rd = new RFI.DAL.RFIDataContext())
                {
                    v = rd.VendorDetails.ToList();
                }

                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    List<PortfolioMgt.Entity.ServiceCatalog_category> sc_category_subcategory = (from p in pd.ServiceCatalog_categories
                                                                                                 where p.PortfolioID == PortfolioID
                                                                                                 select p).ToList();

                    List<ServiceCatelog_Material> p_material = (from pm in pd.ServiceCatelog_Materials
                                                                where pm.PortfolioID == PortfolioID && pm.ItemDelete != '1' && pm.VendorID == 0
                                                                select pm).ToList();


                    int table_val = 0;
                    try
                    {
                        con.Open();
                        //foreach (ServiceCatelog_Material s in p_material)
                        //{

                        //    // Insert data into Excel sheet
                        //    StringBuilder strValue = new StringBuilder();
                        //    strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.Category && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                        //    strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.SubCategory && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                        //    strValue.Append("'" + AddSingleQuotes(s.PartNumber) + "',");
                        //    strValue.Append("'" + AddSingleQuotes(s.ItemDescription) + "',");
                        //    //strValue.Append("'" + AddSingleQuotes(string.IsNullOrEmpty(s.Unit) ? string.Empty : s.Unit) + "',");
                        //    strValue.Append("'" + AddSingleQuotes(s.NSPPrice.HasValue ? s.NSPPrice.Value.ToString() : "0.00") + "',");
                        //    strValue.Append("'" + AddSingleQuotes(s.BuyingPrice.HasValue ? s.BuyingPrice.Value.ToString() : "0.00") + "',");
                        //    strValue.Append("'" + AddSingleQuotes(string.IsNullOrEmpty(s.MrkUp) ? string.Empty : s.MrkUp) + "',");
                        //    strValue.Append("'" + AddSingleQuotes(s.SellingPrice.HasValue ? s.SellingPrice.Value.ToString() : "0.00") + "',");
                        //    strValue.Append("'" + AddSingleQuotes(s.DiscountPrice.HasValue ? s.DiscountPrice.Value.ToString() : "") + "'");


                        //    OleDbCommand cmd = null;

                        //    var distinctCategories = p_material.Select(m => m.Supplier).Distinct();
                        //    if (table_val == 0)
                        //    {
                        //        try
                        //        {
                        //            foreach (int sup in distinctCategories)
                        //            {
                        //                try
                        //                {
                        //                    var sname = v.Where(p => p.VendorID == sup).FirstOrDefault().ContractorName.Trim() ;
                        //                    var sqlCmd = "CREATE TABLE [" + sname + "] (" + strField.ToString().Replace("]", "] text") + ")";
                        //                    cmd = new OleDbCommand(sqlCmd, con);
                        //                    cmd.ExecuteNonQuery();
                        //                }
                        //                catch (Exception ex)
                        //                { LogExceptions.WriteExceptionLog(ex); }
                        //            }
                        //            table_val = 1;
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            LogExceptions.WriteExceptionLog(ex);
                        //        }

                        //    }
                        //    sheetName = v.Where(p => p.VendorID == s.Supplier).FirstOrDefault().ContractorName.Trim() + "$";
                        //    //string s1 = "INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")";


                        //    cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                        //    cmd.ExecuteNonQuery();

                        //    //rNumb = i + 1;
                        //}
                        //OleDbCommand cmd = null;
                        //cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                        //cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    { string er = ex.Message; }
                    finally { con.Close(); }

                }




                //con.Close();
            }
            return rNumb;
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return -1;
        }
    }

    public static int ExportToExcel_Service(string excelFile, int PortfolioID)
    {
        // Create the connection string
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            excelFile + ";Extended Properties=\"Excel 12.0;ReadOnly=False;HDR=Yes;\"";
        string sheetName = string.Empty;
        string[] MaterialHeader = new string[] { "CATEGORY", "SUB-CATEGORY", "DESCRIPTION", "SETUP BUY", "SETUP SELL", "MATERIAL BUY", "MATERIAL SELL", "LABOUR BUY", "LABOUR SELL", "MARK UP" };


        int rNumb = 0;
        try
        {
            using (OleDbConnection con = new OleDbConnection(connString))
            {


                // Build the field names string
                StringBuilder strField = new StringBuilder();
                foreach (string m in MaterialHeader)
                {
                    strField.Append("[" + m + "],");

                }

                strField = strField.Remove(strField.Length - 1, 1);



                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    List<PortfolioMgt.Entity.ServiceCatalog_category> sc_category_subcategory = (from p in pd.ServiceCatalog_categories
                                                                                                 where p.PortfolioID == PortfolioID
                                                                                                 select p).ToList();

                    List<ServiceCatelog_Service> p_service = (from pm in pd.ServiceCatelog_Services
                                                              where pm.PortfolioID == PortfolioID && pm.ItemDelete != 1 && pm.VendorID == 0
                                                              select pm).ToList();
                    int table_cnt = 0;
                    try
                    {
                        con.Open();
                        foreach (ServiceCatelog_Service s in p_service)
                        {

                            // Insert data into Excel sheet
                            StringBuilder strValue = new StringBuilder();
                            strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.Category && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                            strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.SubCategory && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                            strValue.Append("'" + AddSingleQuotes(s.ServiceDescription) + "',");
                            strValue.Append("'" + AddSingleQuotes(s.SetupBuy.HasValue ? s.SetupBuy.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes(s.SetupSell.HasValue ? s.SetupSell.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes(s.MaterialsBuy.HasValue ? s.MaterialsBuy.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes(s.MaterialsSell.HasValue ? s.MaterialsSell.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes(s.LabourBuy.HasValue ? s.LabourBuy.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes(s.LabourSell.HasValue ? s.LabourSell.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes((string.IsNullOrEmpty(s.MrkUp) ? string.Empty : s.MrkUp)) + "'");


                            // Create Excel sheet

                            OleDbCommand cmd = null;
                            sheetName = "Sheet1$";
                            //if (table_cnt == 0 && sheetName != "Sheet1$")
                            //{

                            //    var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                            //    cmd = new OleDbCommand(sqlCmd, con);
                            //    cmd.ExecuteNonQuery();
                            //    table_cnt = 1;
                            //}
                            //else { table_cnt = 1; }

                            cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                            cmd.ExecuteNonQuery();

                        }
                    }
                    catch (Exception ex)
                    { string er = ex.Message;
                    LogExceptions.WriteExceptionLog(ex);
                    }
                    finally { con.Close(); }

                }

                //con.Close();
            }
            return rNumb;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
    public static int ExportToExcel_Labour(string excelFile, int PortfolioID)
    {



        // Create the connection string
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            excelFile + ";Extended Properties=Excel 12.0 Xml;";
        string sheetName = string.Empty;
        string[] MaterialHeader = new string[] { "CATEGORY", "SUB-CATEGORY", "DESCRIPTION", "BUYING PRICE", "SELLING PRICE", "MARK UP" };


        int rNumb = 0;
        try
        {
            using (OleDbConnection con = new OleDbConnection(connString))
            {


                // Build the field names string
                StringBuilder strField = new StringBuilder();
                foreach (string m in MaterialHeader)
                {
                    strField.Append("[" + m + "],");

                }

                strField = strField.Remove(strField.Length - 1, 1);



                using (PortfolioDataContext pd = new PortfolioDataContext())
                {
                    List<PortfolioMgt.Entity.ServiceCatalog_category> sc_category_subcategory = (from p in pd.ServiceCatalog_categories
                                                                                                 where p.PortfolioID == PortfolioID
                                                                                                 select p).ToList();

                    List<ServiceCatelog_Labour> p_service = (from pm in pd.ServiceCatelog_Labours
                                                             where pm.PortfolioID == PortfolioID && pm.ItemDelete != '1' && pm.VendorID == 0
                                                             select pm).ToList();
                    int table_cnt = 0;
                    try
                    {
                        con.Open();
                        foreach (ServiceCatelog_Labour s in p_service)
                        {

                            // Insert data into Excel sheet
                            StringBuilder strValue = new StringBuilder();
                            strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.Category && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                            strValue.Append("'" + AddSingleQuotes(sc_category_subcategory.Where(p => p.ID == s.SubCategory && p.PortfolioID == PortfolioID).FirstOrDefault().CategoryName) + "',");
                            strValue.Append("'" + AddSingleQuotes(s.EngineerDescription) + "',");
                            strValue.Append("'" + AddSingleQuotes(s.BuyingPrice.HasValue ? s.BuyingPrice.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes(s.SellingPrice.HasValue ? s.SellingPrice.Value.ToString() : "0.00") + "',");
                            strValue.Append("'" + AddSingleQuotes((string.IsNullOrEmpty(s.MrkUp) ? string.Empty : s.MrkUp)) + "'");



                            // Create Excel sheet

                            OleDbCommand cmd = null;
                            sheetName = "Sheet1$";
                            //if (table_cnt == 0 && sheetName != "Sheet1$")
                            //{
                            //    //cmd = new OleDbCommand("Drop Table "+ sheetName +"$" , con);
                            //    //cmd.ExecuteNonQuery();

                            //    var sqlCmd = "CREATE TABLE [" + sheetName + "] (" + strField.ToString().Replace("]", "] text") + ")";
                            //    cmd = new OleDbCommand(sqlCmd, con);
                            //    cmd.ExecuteNonQuery();
                            //    table_cnt = 1;
                            //}
                            //else { table_cnt = 1; }

                            cmd = new OleDbCommand("INSERT INTO [" + sheetName + "] (" + strField.ToString() + ") VALUES (" + strValue.ToString() + ")", con);
                            cmd.ExecuteNonQuery();



                        }
                    }
                    catch (Exception ex)
                    { string er = ex.Message; }
                    finally { con.Close(); }

                }

                //con.Close();
            }
            return rNumb;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
    public static string AddSingleQuotes(string origText)
    {
        string s = origText;
        int i = 0;

        while ((i = s.IndexOf("'", i)) != -1)
        {
            // Add single quote after existing
            s = s.Substring(0, i) + "'" + s.Substring(i);

            // Increment the index.
            i += 2;
        }
        return s;
    }

    #endregion


    protected void lnkbtncatalog_Click(object sender, EventArgs e)
    {
        ifrmCatelog.Attributes.Add("src", "ServiceCatelogToCustomer.aspx");
        ifrmCatelog.DataBind();
        //lblErr.Text = "";
        //BindVendors(ddlVendors, 0);
        //if (ddlVendors.Items.Count > 0)
        //{
        //    ddlVendors.SelectedIndex = 1;
        //}
        //BindPopWindow();
        mpopBOM.Show();
    }

    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ServiceCatalogue.aspx");
    }
}

