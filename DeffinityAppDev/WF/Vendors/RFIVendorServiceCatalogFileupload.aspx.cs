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

public partial class RFIVendorServiceCatalogFileupload : System.Web.UI.Page
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
            if (FileUploadControl.HasFile)
            {
                bool uplod = true;
                string fleUpload = Path.GetExtension(FileUploadControl.FileName.ToString());
                if (fleUpload.Trim().ToLower() == ".xls" | fleUpload.Trim().ToLower() == ".xlsx")
                {
                    // Save excel file into Server sub dir  
                    // to catch excel file downloading permission
                    FileUploadControl.SaveAs(Server.MapPath("~/" + FileUploadControl.FileName.ToString()));
                    string uploadedFile = (Server.MapPath("~/" + FileUploadControl.FileName.ToString()));
                    try
                    {
                        //dt = xlsInsert(uploadedFile);
                        ds = xlsInsertDS(uploadedFile);
                        FillData(ds);
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
            if (QueryStringValues.Vendor > 0)
            { System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SCRIPT", string.Format("window.parent.location.href = 'RFIVendorServiceCatalog.aspx?VendorID={0}';", QueryStringValues.Vendor), true); }
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SCRIPT", "window.parent.location.href = 'RFIVendors.aspx';", true);
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    public void FillData(DataSet ds)
    {
        try
        {
            foreach (DataTable tbl in ds.Tables)
            {
                string contractorName = tbl.TableName.Trim().Replace("$", "");
                //check table row count 
                if (tbl.Rows.Count > 0)
                {
                    // Insert Contractor and Get vendorID
                    //8- Vendor
                    int vendorID = QueryStringValues.Vendor; //UserInsert(contractorName, contractorName, 8, "Active", contractorName);



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
                    string dateColumn_refid = string.Empty;
                    string dateColumn_itemtype = string.Empty;

                    foreach (DataColumn column in tblData.Columns)
                    {
                        if (column.ColumnName.Trim().ToString().ToUpper().Equals("CATEGORY"))
                            dateColumn_category = column.ColumnName.ToString();
                        if (column.ColumnName.Trim().ToString().ToUpper().Equals("SUB-CATEGORY"))
                            dateColumn_subCategory = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("ITEM NAME"))
                            dateColumn_part = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("DESCRIPTION"))
                            dateColumn_desc = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("NSP"))
                            dateColumn_nsp = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("% MRK UP"))
                            dateColumn_markup = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("ACTUAL BUY"))
                            buyingPirceColumnNAme = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("SELL PRICE"))
                            sellingPriceColumnNAme = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("UNIT"))
                            unitColumnNAme = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("ITEM TYPE"))
                            dateColumn_itemtype = column.ColumnName.ToString();
                        if (column.ColumnName.ToString().ToUpper().Contains("REF ID"))
                            dateColumn_refid = column.ColumnName.ToString();
                    }

                    foreach (DataRow rwUniq in tblData.Rows)
                    {
                        if (rwUniq[dateColumn_category].ToString() != "")
                        {
                            string categoryName = rwUniq[dateColumn_category].ToString();
                            string subCategoryName = rwUniq[dateColumn_subCategory].ToString();
                            string description = rwUniq[dateColumn_desc].ToString();
                            string partNumber = rwUniq[dateColumn_part].ToString();
                            string refid = rwUniq[dateColumn_refid].ToString();
                            string itemtype = rwUniq[dateColumn_itemtype].ToString();
                            string unit = "1";
                            if (unitColumnNAme != string.Empty && rwUniq[unitColumnNAme] != null)
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
                            if (dateColumn_nsp != string.Empty && rwUniq[dateColumn_nsp] != null)
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
                            if (buyingPirceColumnNAme != string.Empty && rwUniq[buyingPirceColumnNAme] != null)
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

                            float MRKUP = 0;
                            if (dateColumn_markup != string.Empty && rwUniq[dateColumn_markup] != null)
                            {
                                if (rwUniq[dateColumn_markup].ToString() != "")
                                {
                                    MRKUP = Convert.ToSingle(rwUniq[dateColumn_markup]);
                                    MRKUP = MRKUP * 100;
                                }
                                else
                                    MRKUP = 0;

                            }
                            else
                                MRKUP = 0;

                            string sellingPrice = string.Empty;
                            if (sellingPriceColumnNAme != string.Empty && rwUniq[sellingPriceColumnNAme] != null)
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

                            CatalogueInsert(categoryName, subCategoryName, vendorID, description, partNumber, buyingPirce, sellingPrice, NSPPrice, unit, MRKUP.ToString(),refid,itemtype);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
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
        DataSet output = new DataSet();
        string sheet = string.Empty;
        using (OleDbConnection conn = new OleDbConnection(strcon))
        {
            try
            {
                conn.Open();

                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    sheet = row["TABLE_NAME"].ToString().Trim().Replace("'", "");
                    try
                    {

                        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "a1:z]", conn);
                        cmd.CommandType = CommandType.Text;

                        DataTable outputTable = new DataTable(sheet);
                        output.Tables.Add(outputTable);
                        new OleDbDataAdapter(cmd).Fill(outputTable);
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
            finally {
                conn.Close();
            }
        }
        return output;
    }

    private int UserInsert(string fullname, string loginname, int sid, string status, string company)
    {
        int OutID = 0;
        try
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

            OutID = (int)db.GetParameterValue(cmd, "OutID");
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return OutID;
    }

    private int CatalogueInsert(string CategoryName, string SubCategoryName, int VendorID, 
        string Description, string PartNumber, string BuyingPrice, 
        string SellingPrice, string NSPPrice, string Unit, string MRKUP,string RefID,string ItemType)
    {
        int OutID = 0;
        try
        {
            DbCommand cmd = db.GetStoredProcCommand("ContractorMaterialCatalogue_InsertBYVendor");
            db.AddInParameter(cmd, "@CategoryName", DbType.String, CategoryName);
            db.AddInParameter(cmd, "@SubCategoryName", DbType.String, SubCategoryName);
            db.AddInParameter(cmd, "@VendorID", DbType.Int32, VendorID);
            db.AddInParameter(cmd, "@Description", DbType.String, Description);
            db.AddInParameter(cmd, "@PartNumber", DbType.String, PartNumber);
            db.AddInParameter(cmd, "@BuyingPrice", DbType.Double, BuyingPrice);
            db.AddInParameter(cmd, "@SellingPrice", DbType.Double, SellingPrice);
            db.AddInParameter(cmd, "@Unit", DbType.String, Unit);
            db.AddInParameter(cmd, "@NSPPrice", DbType.Double, NSPPrice);
            db.AddInParameter(cmd, "@MrkUp", DbType.String, MRKUP);
            db.AddInParameter(cmd, "@PortfolioID", DbType.Int32, sessionKeys.PortfolioID);
            db.AddInParameter(cmd, "@ItemType", DbType.String, ItemType);
            db.AddInParameter(cmd, "@RefID", DbType.String, RefID);
            
            db.AddOutParameter(cmd, "@OutID", DbType.Int32, 4);

            db.ExecuteNonQuery(cmd);
            //if OutID = 1 sucess 2 for already item exist
            OutID = (int)db.GetParameterValue(cmd, "OutID");
            cmd.Dispose();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return OutID;
    }
}
