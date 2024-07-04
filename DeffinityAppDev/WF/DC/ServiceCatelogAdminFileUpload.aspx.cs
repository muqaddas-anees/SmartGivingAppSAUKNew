using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PortfolioMgt.DAL;
using PortfolioMgt.BAL;
using PortfolioMgt.Entity;

public partial class ServiceCatelogAdminFileUpload_1 : System.Web.UI.Page
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);

    DataTable dt = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            int PortfolioID = 0;
            string type = string.Empty;
            if (Request.QueryString["type"] != null)
            {
                type = Request.QueryString["type"];
                if (type.ToLower().Contains("service"))
                {
                    sessionKeys.Cataloguetype = 3;
                }
                else if (type.ToLower().Contains("material"))
                {
                    sessionKeys.Cataloguetype = 2;
                }
                else if (type.ToLower().Contains("labour"))
                {
                    sessionKeys.Cataloguetype = 1;
                }

               
            }

            string page = string.Empty;
            if (Request.QueryString["page"] != null)
            {
                page = Request.QueryString["page"];
                PortfolioID = 0;
            }
            else
            {
                PortfolioID = sessionKeys.PortfolioID;
            }


            if (FileUploadControl.HasFile)
            {
                bool uplod = true;
                string fleUpload = Path.GetExtension(FileUploadControl.FileName.ToString());
                if (fleUpload.Trim().ToLower() == ".xls" | fleUpload.Trim().ToLower() == ".xlsx")
                {
                    // Save excel file into Server sub dir  
                    // to catch excel file downloading permission
                    FileUploadControl.SaveAs(Server.MapPath("~/upload" + FileUploadControl.FileName.ToString()));
                    string uploadedFile = (Server.MapPath("~/upload" + FileUploadControl.FileName.ToString()));
                    try
                    {
                        //dt = xlsInsert(uploadedFile);
                        ds = xlsInsertDS(uploadedFile);
                        if (type.ToLower().Contains("service"))
                        {
                            Fill_Service(ds, PortfolioID);
                        }
                        else if (type.ToLower().Contains("material"))
                        {
                            //Material
                            FillData(ds,PortfolioID);
                        }
                        else if (type.ToLower().Contains("labour"))
                        {
                            // labour
                            Fill_Labour(ds, PortfolioID);
                        }


                    }
                    catch (Exception ex)
                    {
                        uplod = false;
                        LogExceptions.WriteExceptionLog(ex);
                        //this.lblMessage.Text = "System uploading Error";
                    }
                    File.Delete(uploadedFile); // Delete upload exel  file in sub dir  'lsUploadFile' no need to keep...
                }
                if (uplod)
                {
                    string mess1 = "Successfully uploaded";
                    this.lblMessage.Text = mess1;
                }
            }
            else
            {
                //this.lblMessage.Text = "Please select file to upload.";
            }
            if (Request.QueryString["page"] == null)
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SCRIPT", "window.parent.location.href = 'ServiceCatalogue.aspx';", true);
            else
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SCRIPT", "window.parent.location.href = 'ServiceCatalogAdmin.aspx';", true);

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }

    public void FillData(DataSet ds, int PortfoliioID)
    {
        foreach (DataTable tbl in ds.Tables)
        {
            string contractorName = tbl.TableName.Trim().Replace("$", "");
            //check table row count 
            if (tbl.Rows.Count > 0)
            {
                // Insert Contractor and Get vendorID
                //8- Vendor
                int vendorID = UserInsert(contractorName, contractorName, 8, "Active", contractorName);



                DataTable tblData = new DataTable();
                tblData = tbl;
                string sellingPriceColumnNAme = string.Empty;
                string buyingPirceColumnNAme = string.Empty;
                string unitColumnNAme = string.Empty;
                string dateColumnNAme = string.Empty;

                string dateColumn_category = string.Empty;
                string dateColumn_subCategory = string.Empty;
                string dateColumn_markup = string.Empty;
                string dateColumn_desc = string.Empty;
                string dateColumn_part = string.Empty;
                string dateColumn_nsp = string.Empty;
                string dateColumn_date = string.Empty;

                foreach (DataColumn column in tblData.Columns)
                {
                    if (column.ColumnName.Trim().ToString().ToUpper().Equals("CATEGORY"))
                        dateColumn_category = column.ColumnName.ToString();
                    if (column.ColumnName.Trim().ToString().ToUpper().Equals("SUB-CATEGORY"))
                        dateColumn_subCategory = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("PART"))
                        dateColumn_part = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("DESCRIPTION"))
                        dateColumn_desc = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("NSP"))
                        dateColumn_nsp = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("% MRK UP") || column.ColumnName.ToString().ToUpper().Contains("MARK UP"))
                        dateColumn_markup = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("ACTUAL BUY"))
                        buyingPirceColumnNAme = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("SELL PRICE"))
                        sellingPriceColumnNAme = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("UNIT"))
                        unitColumnNAme = column.ColumnName.ToString();
                    if (column.ColumnName.ToString().ToUpper().Contains("DATE"))
                        dateColumn_date = column.ColumnName.ToString();
                }

                foreach (DataRow rwUniq in tblData.Rows)
                {
                    if (rwUniq[dateColumn_category].ToString() != "")
                    {
                        string categoryName = rwUniq[dateColumn_category].ToString();
                        string subCategoryName = rwUniq[dateColumn_subCategory].ToString();
                        string description = rwUniq[dateColumn_desc].ToString();
                        string partNumber = rwUniq[dateColumn_part].ToString();

                        string unit = "1";
                        if (rwUniq[unitColumnNAme] != null)
                        {
                            if (rwUniq[unitColumnNAme].ToString() != "")
                            {
                                unit = rwUniq[unitColumnNAme].ToString();
                            }
                            else
                                unit = "1";
                        }
                        else
                            unit = "1";

                        string NSPPrice = string.Empty;
                        if (rwUniq[dateColumn_nsp] != null)
                        {
                            if (rwUniq[dateColumn_nsp].ToString() != "")
                            {
                                NSPPrice = rwUniq[dateColumn_nsp].ToString();
                                NSPPrice = rwUniq[dateColumn_nsp].ToString().Replace('£', ' ').Trim();
                                //NSPPrice.Replace('£', ' ').Trim();
                            }
                            else
                            {
                                NSPPrice = "0.00";
                            }
                        }
                        else
                        {
                            NSPPrice = "0.00";
                        }

                        string buyingPirce = string.Empty;
                        if (rwUniq[buyingPirceColumnNAme] != null)
                        {
                            if (rwUniq[buyingPirceColumnNAme].ToString() != "")
                            {
                                buyingPirce = rwUniq[buyingPirceColumnNAme].ToString();
                            }
                            else
                            {
                                buyingPirce = "0.00";
                            }
                        }
                        else
                        {
                            buyingPirce = "0.00";
                        }

                        string MRKUP = string.Empty;
                        if (rwUniq[dateColumn_markup] != null)
                        {
                            if (rwUniq[dateColumn_markup].ToString() != "")
                            {

                                MRKUP = rwUniq[dateColumn_markup].ToString();
                                //MRKUP = MRKUP * 100;
                               // MRKUP = MRKUP;
                            }
                            else
                                MRKUP = string.Empty;

                        }
                        else
                            MRKUP = string.Empty;

                        string sellingPrice = string.Empty;
                        if (rwUniq[sellingPriceColumnNAme] != null)
                        {
                            if (rwUniq[sellingPriceColumnNAme].ToString() != "")
                            {
                                sellingPrice = rwUniq[sellingPriceColumnNAme].ToString();
                            }
                            else
                            {
                                sellingPrice = "0.00";
                            }
                        }
                        else
                        {
                            sellingPrice = "0.00";
                        }

                        string dateadded = string.Empty;
                        if (rwUniq[dateColumn_date] != null)
                        {
                            if (rwUniq[dateColumn_date].ToString() != "")
                            {
                                dateadded = rwUniq[dateColumn_date].ToString();
                            }
                           
                        }
                        else
                        {
                            dateadded = string.Empty; ;
                        }

                        CatalogueInsert(categoryName, subCategoryName, vendorID, description, partNumber, buyingPirce, sellingPrice, NSPPrice, unit, MRKUP.ToString(),dateadded, PortfoliioID);
                    }
                }
            }
        }
    }
    public void Fill_Service(DataSet ds,int portfolioid)
    {
        foreach (DataTable tbl in ds.Tables)
        {
            //string contractorName = tbl.TableName.Trim().Replace("$", "");
            //check table row count 
            if (tbl.Rows.Count > 0)
            {
               
                DataTable tblData = new DataTable();
                tblData = tbl;

                string dc_category = string.Empty;
                string dc_subCategory = string.Empty;
                string dc_desc = string.Empty;
                string dc_unit = string.Empty;
                string dc_setupbuy = string.Empty;
                string dc_setupsell = string.Empty;
                string dc_materialbuy = string.Empty;
                string dc_materialsell = string.Empty;
                string dc_labourbuy = string.Empty;
                string dc_laboursell = string.Empty;
                string dc_markUp = string.Empty;
                

                foreach (DataColumn column in tblData.Columns)
                {
                    if (column.ColumnName.Trim().ToString().ToUpper().Equals("CATEGORY"))
                        dc_category = column.ColumnName.ToString();
                    else if (column.ColumnName.Trim().ToString().ToUpper().Equals("SUB-CATEGORY"))
                        dc_subCategory = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("DESCRIPTION"))
                        dc_desc = column.ColumnName.ToString();
                    //else if (column.ColumnName.ToString().ToUpper().Contains("UNIT"))
                    //    dc_unit = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("SETUP BUY"))
                        dc_setupbuy = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("SETUP SELL"))
                        dc_setupsell = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("MATERIAL BUY"))
                        dc_materialbuy = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("MATERIAL SELL"))
                        dc_materialsell = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("LABOUR BUY"))
                        dc_labourbuy = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("LABOUR SELL"))
                        dc_laboursell = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("MARK UP"))
                        dc_markUp = column.ColumnName.ToString();
                  
                }

                foreach (DataRow rwUniq in tblData.Rows)
                {
                    if (!string.IsNullOrEmpty(rwUniq[dc_category].ToString()) && !string.IsNullOrEmpty(rwUniq[dc_subCategory].ToString()) && !string.IsNullOrEmpty(rwUniq[dc_desc].ToString()))
                    {
                        SerivceDataInsert(portfolioid, rwUniq[dc_category].ToString(), rwUniq[dc_subCategory].ToString(), rwUniq[dc_desc].ToString(), rwUniq[dc_setupbuy].ToString(), rwUniq[dc_setupsell].ToString(), rwUniq[dc_materialbuy].ToString(), rwUniq[dc_materialsell].ToString(), rwUniq[dc_labourbuy].ToString(), rwUniq[dc_laboursell].ToString(), rwUniq[dc_markUp].ToString());
                       
                    }
                }
            }
        }
    }

    public void Fill_Labour(DataSet ds, int portfolioid)
    {
        foreach (DataTable tbl in ds.Tables)
        {
            //string contractorName = tbl.TableName.Trim().Replace("$", "");
            //check table row count 
            if (tbl.Rows.Count > 0)
            {

                DataTable tblData = new DataTable();
                tblData = tbl;

                string dc_category = string.Empty;
                string dc_subCategory = string.Empty;
                string dc_desc = string.Empty;
                string dc_unit = string.Empty;
                string dc_buy = string.Empty;
                string dc_sell = string.Empty;
                string dc_markUp = string.Empty;
                //string dc_setupbuy = string.Empty;
                //string dc_setupsell = string.Empty;
                //string dc_materialbuy = string.Empty;
                //string dc_materialsell = string.Empty;
                //string dc_labourbuy = string.Empty;
                //string dc_laboursell = string.Empty;


                foreach (DataColumn column in tblData.Columns)
                {
                    if (column.ColumnName.Trim().ToString().ToUpper().Equals("CATEGORY"))
                        dc_category = column.ColumnName.ToString();
                    else if (column.ColumnName.Trim().ToString().ToUpper().Equals("SUB-CATEGORY"))
                        dc_subCategory = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("DESCRIPTION"))
                        dc_desc = column.ColumnName.ToString();
                    //else if (column.ColumnName.ToString().ToUpper().Contains("UNIT"))
                    //    dc_unit = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("BUY"))
                        dc_buy = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("SELL"))
                        dc_sell = column.ColumnName.ToString();
                    else if (column.ColumnName.ToString().ToUpper().Contains("MARK UP"))
                        dc_markUp = column.ColumnName.ToString();
                    //else if (column.ColumnName.ToString().ToUpper().Contains("MATERIAL BUY"))
                    //    dc_materialbuy = column.ColumnName.ToString();
                    //else if (column.ColumnName.ToString().ToUpper().Contains("MATERIAL SELL"))
                    //    dc_materialsell = column.ColumnName.ToString();
                    //else if (column.ColumnName.ToString().ToUpper().Contains("LABOUR BUY"))
                    //    dc_labourbuy = column.ColumnName.ToString();
                    //else if (column.ColumnName.ToString().ToUpper().Contains("LABOUR SELL"))
                    //    dc_laboursell = column.ColumnName.ToString();

                }

                foreach (DataRow rwUniq in tblData.Rows)
                {
                    if (!string.IsNullOrEmpty(rwUniq[dc_category].ToString()) && !string.IsNullOrEmpty(rwUniq[dc_subCategory].ToString()) && !string.IsNullOrEmpty(rwUniq[dc_desc].ToString()))
                    {
                        LabourDataInsert(portfolioid, rwUniq[dc_category].ToString(), rwUniq[dc_subCategory].ToString(), rwUniq[dc_desc].ToString(), rwUniq[dc_buy].ToString(), rwUniq[dc_sell].ToString(), rwUniq[dc_markUp].ToString());

                    }
                }
            }
        }
    }
    public System.Data.DataSet xlsInsertDS(string pth)
    {
        string strcon = string.Empty;
        if (Path.GetExtension(pth).ToLower().Equals(".xls"))
        {
            strcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                            + pth +
                            ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
        }
        else
        {
            strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                          + pth +
                          ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
        }
        OleDbCommand ocmd;
        OleDbDataAdapter oda;
       // DataTable dtDetails;
        DataSet dsDetails;

        DataSet output = new DataSet();
        string sheet = string.Empty;
        using (OleDbConnection conn = new OleDbConnection(strcon))
        {
            try
            {
                conn.Open();

                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                dsDetails = new DataSet();
                
                foreach (DataRow row in dt.Rows)
                {
                    sheet = row["TABLE_NAME"].ToString().Trim().Replace("'", "");
                    //if (!sheet.Contains("$"))
                    //    sheet = sheet + "$";
                    

                    try
                    {
                        ocmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                        ocmd.CommandType = CommandType.Text;
                        oda = new OleDbDataAdapter(ocmd);
                        oda.Fill(dsDetails, sheet);
                        
                    }
                    catch (Exception ex)
                    {
                        //suppress out of rang expression
                        LogExceptions.LogException(ex.Message, "Excel file worksheet - " + sheet);
                    }

                    //clear sheet name
                    sheet = string.Empty;
                }
            }
            catch (OleDbException oledb)
            {
                throw new Exception(oledb.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        return dsDetails;
    }

    private int UserInsert(string fullname, string loginname, int sid, string status, string company)
    {

        DbCommand cmd = db.GetStoredProcCommand("RFI_VENDOR_Insert");
        db.AddInParameter(cmd, "@ContractorName", DbType.String, fullname);
        db.AddInParameter(cmd, "@LoginName", DbType.String, loginname);
        db.AddInParameter(cmd, "@SID", DbType.Int32, sid);
        db.AddInParameter(cmd, "@Status", DbType.String, status);
        db.AddInParameter(cmd, "@Company", DbType.String, company);
        db.AddOutParameter(cmd, "@OutStatus", DbType.Int32, 4);
        db.AddOutParameter(cmd, "@OutID", DbType.Int32, 4);
        db.ExecuteNonQuery(cmd);
        //if getVal = 1 sucess 2 for already item exist
        int getVal = (int)db.GetParameterValue(cmd, "OutStatus");

        int OutID = (int)db.GetParameterValue(cmd, "OutID");
        cmd.Dispose();
        return OutID;
    }

    private int CatalogueInsert(string CategoryName, string SubCategoryName, int VendorID, string Description, string PartNumber, string BuyingPrice, string SellingPrice, string NSPPrice, string Unit, string MRKUP,string datemodified,int PortfolioID)
    {
        int OutID = 0;
        try
        {
            //DbCommand cmd = db.GetStoredProcCommand("ContractorMaterialCatalogue_InsertBYPortfolio");
            //db.AddInParameter(cmd, "@CategoryName", DbType.String, CategoryName);
            //db.AddInParameter(cmd, "@SubCategoryName", DbType.String, SubCategoryName);
            //db.AddInParameter(cmd, "@SupplierID", DbType.Int32, VendorID);
            //db.AddInParameter(cmd, "@Description", DbType.String, Description);
            //db.AddInParameter(cmd, "@PartNumber", DbType.String, PartNumber);
            //db.AddInParameter(cmd, "@BuyingPrice", DbType.Double, BuyingPrice);
            //db.AddInParameter(cmd, "@SellingPrice", DbType.Double, SellingPrice);
            //db.AddInParameter(cmd, "@Unit", DbType.String, Unit);
            //db.AddInParameter(cmd, "@NSPPrice", DbType.Double, NSPPrice);
            //db.AddInParameter(cmd, "@MrkUp", DbType.String, MRKUP);
            //db.AddInParameter(cmd, "@portfolioID", DbType.Int32, PortfolioID);
            //db.AddOutParameter(cmd, "@OutID", DbType.Int32, 4);

            //db.ExecuteNonQuery(cmd);
            ////if OutID = 1 sucess 2 for already item exist
            //OutID = (int)db.GetParameterValue(cmd, "OutID");
            //cmd.Dispose();


            int categoryID = CategoryInsert(CategoryName, PortfolioID);
            int subcategoryID = SubCategoryInsert(SubCategoryName, categoryID, PortfolioID);


            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                ServiceCatelog_Material ss = new ServiceCatelog_Material();
                ss = (from p in pd.ServiceCatelog_Materials
                      where p.Category == categoryID && p.SubCategory == subcategoryID && p.PortfolioID == PortfolioID && p.ItemDescription.ToLower() == Description.ToLower() && p.ItemDelete != '1' && p.VendorID == 0
                      select p).FirstOrDefault();
                if (MRKUP.Contains("%"))
                    MRKUP = MRKUP.Replace("%", string.Empty).Trim();

                if (ss == null)
                {
                    ss = new ServiceCatelog_Material();
                    ss.Category = categoryID;
                    ss.BuyingPrice = Convert.ToDouble(string.IsNullOrEmpty(BuyingPrice) ? "0.00" : BuyingPrice);
                    //ss.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(SellingPrice) ? "0.00" : SellingPrice);
                    if (MRKUP != string.Empty)
                    {
                        ss.SellingPrice = Convert.ToDouble(GetSellingPrice(MRKUP,BuyingPrice));
                        ss.MrkUp = MRKUP;
                    }
                    else
                    {
                        ss.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(SellingPrice) ? "0.00" : SellingPrice);
                        ss.MrkUp = GetMarkup(SellingPrice, BuyingPrice);
                    }
                    ss.PortfolioID = PortfolioID;
                    ss.ItemDescription = Description;
                    ss.SubCategory = subcategoryID;
                    ss.VendorID = 0;
                    ss.Type = 0;
                    ss.UnitConsumption = 0;
                    ss.QTY = 0;
                    ss.ProjectReference = 0;
                    ss.PageType = 1;
                    ss.DiscountPrice = 0;
                    ss.currency = string.Empty;
                    ss.ContractorID = 0;
                    ss.Image = Guid.Empty;
                    ss.ItemDelete = '0';
                    ss.NSPPrice = Convert.ToDouble(string.IsNullOrEmpty(NSPPrice) ? "0.00" : NSPPrice);
                    ss.Unit = Unit;
                    ss.PartNumber = PartNumber;
                    //ss.MrkUp = GetMarkup(SellingPrice, BuyingPrice); //MRKUP;
                    ss.Supplier = VendorID;
                    ss.DateModified = Convert.ToDateTime(string.IsNullOrEmpty(datemodified) ? DateTime.Now.ToShortDateString() : datemodified);
                    //ss.BuyingPrice = 0;
                    pd.ServiceCatelog_Materials.InsertOnSubmit(ss);
                }
                else
                {
                    ss.Category = categoryID;
                    ss.BuyingPrice = Convert.ToDouble(string.IsNullOrEmpty(BuyingPrice) ? "0.00" : BuyingPrice);
                    //ss.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(SellingPrice) ? "0.00" : SellingPrice);
                    if (MRKUP != string.Empty)
                    {
                        ss.SellingPrice = Convert.ToDouble(GetSellingPrice(MRKUP, BuyingPrice));
                        ss.MrkUp = MRKUP;
                    }
                    else
                    {
                        ss.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(SellingPrice) ? "0.00" : SellingPrice);
                        ss.MrkUp = GetMarkup(SellingPrice, BuyingPrice);
                    }
                    ss.PortfolioID = PortfolioID;
                    ss.ItemDescription = Description;
                    ss.SubCategory = subcategoryID;
                    ss.NSPPrice = Convert.ToDouble(string.IsNullOrEmpty(NSPPrice) ? "0.00" : NSPPrice);
                    ss.Unit = Unit;
                    ss.PartNumber = PartNumber;
                    ss.DateModified = Convert.ToDateTime(string.IsNullOrEmpty(datemodified) ? DateTime.Now.ToShortDateString() : datemodified);
                    //ss.MrkUp = GetMarkup(SellingPrice, BuyingPrice);
                    ss.Supplier = VendorID;
                }
                pd.SubmitChanges();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return OutID;
    }

    #region Category

    private int CategoryInsert(string categoryName,int portfolioID)
    {
        int categoryID = 0;
        ServiceCatalog_category sc = new ServiceCatalog_category();
        using (PortfolioDataContext pd = new PortfolioDataContext())
        { 
            
          sc = (from p in pd.ServiceCatalog_categories
                                       where (p.CategoryName.ToLower() == categoryName.ToLower() && p.PortfolioID == portfolioID) && p.VendorID == 0 
                                        select p).FirstOrDefault();
          if (sc == null)
          {
              sc = new ServiceCatalog_category();
              sc.MasterID = 0;
              sc.PageType = 1;
              sc.PortfolioID = portfolioID;
              sc.VendorID = 0;
              sc.Type = 0;
              sc.CategoryName = categoryName;
              pd.ServiceCatalog_categories.InsertOnSubmit(sc);
              pd.SubmitChanges();
              categoryID = sc.ID;
          }
          else
          {
              categoryID = sc.ID;
          }
        
        }

        return categoryID ;
    }

    private int SubCategoryInsert(string SubcategoryName,int CategoryID, int portfolioID)
    {
        int subcategoryID = 0;
        ServiceCatalog_category sc = new ServiceCatalog_category();
       
        using (PortfolioDataContext pd = new PortfolioDataContext())
        {

            sc = (from p in pd.ServiceCatalog_categories
                         where (p.CategoryName.ToLower() == SubcategoryName.ToLower() && p.MasterID == CategoryID) && p.PortfolioID == portfolioID && p.VendorID == 0
                  select p).FirstOrDefault();


            if (sc == null)
            {
                sc = new ServiceCatalog_category();
                sc.MasterID = CategoryID;
                sc.PageType = 1;
                sc.PortfolioID = portfolioID;
                sc.VendorID = 0;
                sc.Type = 1;
                sc.CategoryName = SubcategoryName;
                pd.ServiceCatalog_categories.InsertOnSubmit(sc);
                pd.SubmitChanges();
                subcategoryID = sc.ID;
            }
            else
            {
                subcategoryID = sc.ID;
            }
        }
        return subcategoryID;
    }

    private void SerivceDataInsert(int portfolioID, string CategoryName, string SubCategoryName, string description, string setupbuy, string setupsell, string materialbuy, string materialsell, string labourbuy, string laboursell,string markUp)
    { 
    
        int categoryID = CategoryInsert(CategoryName,portfolioID);
        int subcategoryID = SubCategoryInsert(SubCategoryName, categoryID, portfolioID);
        if (markUp.Contains("%"))
            markUp = markUp.Replace("%",string.Empty).Trim();
        
        using (PortfolioDataContext pd = new PortfolioDataContext())
        {
            ServiceCatelog_Service ss = new ServiceCatelog_Service();
            ss = (from p in pd.ServiceCatelog_Services
                         where p.Category == categoryID && p.SubCategory == subcategoryID && p.PortfolioID == portfolioID && p.ServiceDescription.ToLower() == description.ToLower() && p.ItemDelete == 0 && p.VendorID == 0
                         select p).FirstOrDefault();

            if (ss == null)
            {
                ss = new ServiceCatelog_Service();
                ss.Category = categoryID;
                ss.LabourBuy = Convert.ToDouble(string.IsNullOrEmpty(labourbuy) ? "0.00" : labourbuy);
                
                ss.MaterialsBuy = Convert.ToDouble(string.IsNullOrEmpty(materialbuy) ? "0.00" : materialbuy);
                
                ss.PortfolioID = portfolioID;
                ss.ServiceDescription = description;
                ss.SetupBuy = Convert.ToDouble(string.IsNullOrEmpty(setupbuy) ? "0.00" : setupbuy);
                
                ss.SubCategory = subcategoryID;
                ss.TotalServiceBuy = (ss.LabourBuy + ss.MaterialsBuy + ss.SetupBuy);

                if (markUp != string.Empty)
                {
                    ss.LabourSell = Convert.ToDouble(GetSellingPrice(markUp, string.IsNullOrEmpty(labourbuy) ? "0.00" : labourbuy));
                    ss.MaterialsSell = Convert.ToDouble(GetSellingPrice(markUp, string.IsNullOrEmpty(materialbuy) ? "0.00" : materialbuy));
                    ss.SetupSell = Convert.ToDouble(GetSellingPrice(markUp, string.IsNullOrEmpty(setupbuy) ? "0.00" : setupbuy));

                    //ss.TotalServiceSell = Convert.ToDouble(GetSellingPrice(markUp, ss.TotalServiceBuy.Value.ToString()));
                    ss.MrkUp = markUp;
                }
                else
                {
                    ss.LabourSell = Convert.ToDouble(string.IsNullOrEmpty(laboursell) ? "0.00" : laboursell);
                    ss.MaterialsSell = Convert.ToDouble(string.IsNullOrEmpty(materialsell) ? "0.00" : materialsell);
                    ss.SetupSell = Convert.ToDouble(string.IsNullOrEmpty(setupsell) ? "0.00" : setupsell);
                    //ss.TotalServiceSell = (ss.LabourSell + ss.MaterialsSell + ss.SetupSell);
                    ss.MrkUp = GetMarkup(ss.TotalServiceSell.Value.ToString(), ss.TotalServiceBuy.Value.ToString());
                }
                ss.TotalServiceSell = (ss.LabourSell + ss.MaterialsSell + ss.SetupSell);

                ss.VendorID = 0;
                ss.Type = 0;
                ss.UnitConsumption = 0;
                ss.ServiceType = 3;
                ss.SellingPrice = 0;
                ss.QTY = 0;
                ss.ProjectReference = 0;
                ss.PageType = 1;
                ss.GP = 0;
                ss.DiscountPrice = 0;
                ss.DetailedDesc = string.Empty;
                ss.currency = 0;
                ss.ContractorID = 0;
                ss.Image = Guid.Empty;
                ss.ItemDelete = 0;
                ss.BuyingPrice = 0;
                //ss.MrkUp = GetMarkup( ss.TotalServiceSell.Value.ToString(),ss.TotalServiceBuy.Value.ToString());
                pd.ServiceCatelog_Services.InsertOnSubmit(ss);
            }
            else
            {
                ss.Category = categoryID;
                ss.LabourBuy = Convert.ToDouble(string.IsNullOrEmpty(labourbuy) ? "0.00" : labourbuy);
                ss.MaterialsBuy = Convert.ToDouble(string.IsNullOrEmpty(materialbuy) ? "0.00" : materialbuy);
                ss.PortfolioID = portfolioID;
                ss.ServiceDescription = description;
                ss.SetupBuy = Convert.ToDouble(string.IsNullOrEmpty(setupbuy) ? "0.00" : setupbuy);
                
                ss.SubCategory = subcategoryID;
                ss.TotalServiceBuy = (ss.LabourBuy + ss.MaterialsBuy + ss.SetupBuy);
                if (markUp != string.Empty)
                {
                    ss.LabourSell = Convert.ToDouble(GetSellingPrice(markUp, string.IsNullOrEmpty(labourbuy) ? "0.00" : labourbuy));
                    ss.MaterialsSell = Convert.ToDouble(GetSellingPrice(markUp, string.IsNullOrEmpty(materialbuy) ? "0.00" : materialbuy));
                    ss.SetupSell = Convert.ToDouble(GetSellingPrice(markUp, string.IsNullOrEmpty(setupbuy) ? "0.00" : setupbuy));

                    //ss.TotalServiceSell = Convert.ToDouble(GetSellingPrice(markUp, ss.TotalServiceBuy.Value.ToString()));
                    ss.MrkUp = markUp;
                }
                else
                {
                    ss.LabourSell = Convert.ToDouble(string.IsNullOrEmpty(laboursell) ? "0.00" : laboursell);
                    ss.MaterialsSell = Convert.ToDouble(string.IsNullOrEmpty(materialsell) ? "0.00" : materialsell);
                    ss.SetupSell = Convert.ToDouble(string.IsNullOrEmpty(setupsell) ? "0.00" : setupsell);
                    //ss.TotalServiceSell = (ss.LabourSell + ss.MaterialsSell + ss.SetupSell);
                    ss.MrkUp = GetMarkup(ss.TotalServiceSell.Value.ToString(), ss.TotalServiceBuy.Value.ToString());
                }
                ss.TotalServiceSell = (ss.LabourSell + ss.MaterialsSell + ss.SetupSell);
                //ss.TotalServiceSell = (ss.LabourSell + ss.MaterialsSell + ss.SetupSell);
                //ss.MrkUp = GetMarkup(ss.TotalServiceSell.Value.ToString(), ss.TotalServiceBuy.Value.ToString());
            }
            pd.SubmitChanges();
        }

    }
    private void LabourDataInsert(int portfolioID, string CategoryName, string SubCategoryName, string description, string buy, string sell,string markUp)
    {

        int categoryID = CategoryInsert(CategoryName, portfolioID);
        int subcategoryID = SubCategoryInsert(SubCategoryName, categoryID, portfolioID);

        if (markUp.Contains("%"))
            markUp = markUp.Replace("%", string.Empty).Trim();

        using (PortfolioDataContext pd = new PortfolioDataContext())
        {
            ServiceCatelog_Labour ss = new ServiceCatelog_Labour();
            ss = (from p in pd.ServiceCatelog_Labours
                  where p.Category == categoryID && p.SubCategory == subcategoryID && p.PortfolioID == portfolioID && p.EngineerDescription.ToLower() == description.ToLower() && p.ItemDelete != '1' && p.VendorID == 0
                  select p).FirstOrDefault();

            if (ss == null)
            {
                ss = new ServiceCatelog_Labour();
                ss.Category = categoryID;
                ss.BuyingPrice = Convert.ToDouble(string.IsNullOrEmpty(buy) ? "0.00" : buy);
                if (markUp != string.Empty)
                {
                    ss.SellingPrice = Convert.ToDouble(GetSellingPrice(markUp, buy));
                    ss.MrkUp = markUp; 
                }
                else
                {
                    ss.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(sell) ? "0.00" : sell);
                    ss.MrkUp=GetMarkup(sell, buy);
                }
                ss.PortfolioID = portfolioID;
                ss.EngineerDescription = description;
                //default
                ss.RateType = 1;
                ss.SubCategory = subcategoryID;
                ss.VendorID = 0;
                ss.Type = 0;
                ss.UnitConsumption = 0;
                
                ss.QTY = 0;
                ss.ProjectReference = 0;
                ss.PageType = 1;
                ss.DiscountPrice = 0;
                ss.Notes = string.Empty;
                ss.currency = string.Empty;
                ss.ContractorID = 0;
                ss.Image = Guid.Empty;
                ss.ItemDelete = '0';
                
                pd.ServiceCatelog_Labours.InsertOnSubmit(ss);
            }
            else
            {
                ss.Category = categoryID;
                ss.BuyingPrice = Convert.ToDouble(string.IsNullOrEmpty(buy) ? "0.00" : buy);
                if (markUp != string.Empty)
                {
                    ss.SellingPrice = Convert.ToDouble(GetSellingPrice(markUp, buy));
                    ss.MrkUp = markUp;
                }
                else
                {
                    ss.SellingPrice = Convert.ToDouble(string.IsNullOrEmpty(sell) ? "0.00" : sell);
                    ss.MrkUp = GetMarkup(sell, buy);
                }
                ss.PortfolioID = portfolioID;
                ss.EngineerDescription = description;
                ss.SubCategory = subcategoryID;
                
                
            }
            pd.SubmitChanges();
        }

    }
#endregion

    #region Mark up data
    private string GetMarkup(string SellingPrice, string BuyingPrice)
    {
        string retval = string.Empty;
        double sellprice = 0;
        double buypirce = 0;

        if (!string.IsNullOrEmpty(SellingPrice))
        {
            sellprice = Convert.ToDouble(SellingPrice);
        }

        if (!string.IsNullOrEmpty(BuyingPrice))
        {
            buypirce = Convert.ToDouble(BuyingPrice);
        }

        //formula

        if (buypirce > 0 && sellprice >0)
        {
            retval = string.Format("{0:F2}", 100 * ((sellprice - buypirce) / buypirce));
        }
        else
            retval = string.Empty;
        

        return retval;
    }

    private string GetSellingPrice(string MarkUp, string BuyingPrice)
    {
        string retval = string.Empty;
        double markUp = 0;
        double buypirce = 0;
        try
        {
            if (!string.IsNullOrEmpty(MarkUp))
            {
                markUp = Convert.ToDouble(MarkUp);
            }

            if (!string.IsNullOrEmpty(BuyingPrice))
            {
                buypirce = Convert.ToDouble(BuyingPrice);
            }

            //formula

            if (buypirce != null && markUp != null)
            {
                retval = string.Format("{0:F2}", buypirce + ((buypirce * markUp) / 100));
            }
            else
                retval = string.Empty;

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            retval = string.Empty;
        }
        return retval;
    }
    #endregion
}
