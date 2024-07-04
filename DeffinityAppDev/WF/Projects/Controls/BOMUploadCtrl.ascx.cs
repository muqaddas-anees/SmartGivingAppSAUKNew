using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using POMgt.DAL;
using System.IO;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using ProjectMgt.BLL;

public partial class controls_BOMUploadCtrl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Download BoM Template
    protected void btnDownloadBoMTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("worksheet1");
           
            int x = 2;
            // Title
            ws.Cell("A1").Value = "Description";
            
            ws.Cell("B1").Value = "Part Number";
            ws.Cell("C1").Value = "Type";
            ws.Cell("D1").Value = "Supplier";
            ws.Cell("E1").Value = "Manufacturer";
            ws.Cell("F1").Value = "Unit";
            ws.Cell("G1").Value = "Qty";
            ws.Cell("H1").Value = "Buying Price";
            ws.Cell("I1").Value = "Total";
            ws.Cell("J1").Value = "GP (%)";
            ws.Cell("K1").Value = "Selling Price";
           
            ws.Column(1).Width = 500;
            ws.Column(8).Style.NumberFormat.Format = "0.00";
            ws.Column(9).Style.NumberFormat.Format = "0.00";
            ws.Column(10).Style.NumberFormat.Format = "0.00%";
            ws.Column(11).Style.NumberFormat.Format = "0.00";

            ws.Columns(1, 11).AdjustToContents();
            var rngTable = ws.Range("A1:K1");
            // var rngHeaders = rngTable.Range("A2:I2");
            rngTable.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            rngTable.Style.Font.Bold = true;
            rngTable.Style.Fill.BackgroundColor = XLColor.LightGray;

            var ws2 = wb.Worksheets.Add("X-Dropdowns").Hide();

            //Supplier List
            using (PurchaseOrderMgtDataContext po = new PurchaseOrderMgtDataContext())
            {
                var supplierList = from r in po.v_Vendors
                                   orderby r.ContractorName
                                   select r;
                
                string[] type = new string[3] { "Material", "Labour", "Misc" };

                var manufacturerList = ManufacturerBAL.GetManufacturerList();
                int sCount = 1;
                int tCount = 1;
                int mCount = 1;
                foreach (var item in supplierList)
                {
                    ws2.Cell("A" + sCount.ToString()).Value = item.ContractorName;
                    sCount++;
                }
                foreach (var item in type)
                {
                    ws2.Cell("B" + tCount.ToString()).Value = item;
                    tCount++;
                }
                foreach (var item in manufacturerList)
                {
                    ws2.Cell("E" + mCount.ToString()).Value = item.Name;
                    mCount++;
                }
                // Set drop down list for 500 rows and validation
                for (int a = 2; a < 502; a++)
                {
                    var cellWithtotal = ws.Cell(a, 9);
                    var cellWithGP = ws.Cell(a, 10);
                   
                    // =IF((G2*H2)=0,"",(G2*H2))
                    string totalFormula = "=IF((G" + a + "*H" + a + ")=0,\"\",(G" + a + "*H" + a + "))";

                    //=IF((K2-IF(I2="",0,I2))<=0,"",((K2-I2))/I2)
                    string gpFormula = "=IF((K" + a + "-IF(I" + a + "=\"\",0,I" + a + "))<=0,\"\",((K" + a + "-I" + a + "))/I" + a + ")"; 
                 
                    cellWithtotal.FormulaA1 = totalFormula;
                    cellWithGP.FormulaA1 = gpFormula;
                  
                    // Supplier drop down
                    ws.Cell("D" + a.ToString()).DataValidation.List(ws2.Range("A1:A" + (sCount - 1).ToString()));
                    // type drop down
                    ws.Cell("C" + a.ToString()).DataValidation.List(ws2.Range("B1:B" + (tCount - 1).ToString()));

                    // manufacturer drop down
                    ws.Cell("E" + a.ToString()).DataValidation.List(ws2.Range("E1:E" + (mCount - 1).ToString()));

                    //// Qty validation
                    //ws.Cell(a, 6).DataValidation.Decimal.Between(0, 100000000);
                    //// Buying Price validation
                    //ws.Cell(a, 7).DataValidation.Decimal.Between(0, 100000000);
                    //// GP % validation
                    //ws.Cell(a, 9).DataValidation.Decimal.Between(0, 100);
                    //// Selling Price validation

                 

                   

                }
            }
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"BOM Template.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    #endregion
   
    #region BoM Upload
    protected void btnUploadData_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = fileUpload2.PostedFile.FileName;
            string Extension = Path.GetExtension(filePath);
            //Check the Extention of file
            if (string.IsNullOrEmpty(Extension))
            {
                lblUploadErrorMsg.Visible = true;
                lblUploadErrorMsg.ForeColor = System.Drawing.Color.Red;
                lblUploadErrorMsg.Text = Resources.DeffinityRes.Pleaseselectafile; //"Please select a file";
                return;
            }
            if (IsValid(fileUpload2.PostedFile.FileName))
            {

                string path = Server.MapPath("UploadData\\BoM");
                string fileName = "\\" + fileUpload2.FileName;

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload2.SaveAs(path + fileName);
                string conStr = string.Empty;
                //string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;"; ;
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider= Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                        break;
                    case ".xlsx": //Excel 07
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR={1}'";
                        break;

                }
                conStr = String.Format(conStr, path + fileName, "Yes");
                string[] sheetNames = GetExcelSheetNames(conStr);
                for (int i = 0; i < sheetNames.Length; i++)
                {
                    sheetNames[i] = sheetNames[i].Replace("$", string.Empty);
                    sheetNames[i] = sheetNames[i].Replace("'", string.Empty);
                }
                sheetNames = sheetNames.Where(s => s.ToLower() != "x-dropdowns").ToArray();

                List<ProjectMgt.Entity.ProjectBOM> projectBOMList = new List<ProjectMgt.Entity.ProjectBOM>();

                using (projectTaskDataContext db = new projectTaskDataContext())
                {
                    var bomTypeList = db.BOM_Types.Where(p => p.ProjectReference == QueryStringValues.Project).ToList();
                    var checkWorksheetNames = bomTypeList.Where(p => sheetNames.Contains(p.TypeName)).FirstOrDefault();
                    if (checkWorksheetNames == null)
                    {
                        lblUploadErrorMsg.Text = string.Empty;
                        using (PurchaseOrderMgtDataContext po = new PurchaseOrderMgtDataContext())
                        {
                            var supplierList = (from r in po.v_Vendors
                                               orderby r.ContractorName
                                               select r).ToList();
                            supplierList.Add(new POMgt.Entity.v_Vendor { VendorID = 0, ContractorName = "" });

                            var manufacturerList = ManufacturerBAL.GetManufacturerList().ToList();
                            manufacturerList.Add(new Manufacturer { Id = 0, Name = "" });
                            foreach (var item in sheetNames)
                            {
                                DataTable dataTable = Import_To_Grid(conStr, item);
   
                                if (dataTable.Rows.Count > 0)
                                {
                                    DataRow[] foundRows =dataTable.Select("Description <>''");
                                    int worksheetID = Deffinity.Worksheet.Worksheet_InsertUpdate(0, QueryStringValues.Project, item);

                                    for (int i = 0; i < foundRows.Count(); i++)
                                    {
                                        string description = dataTable.Rows[i][0].ToString();
                                        string partNumber = dataTable.Rows[i][1].ToString();
                                        string type = string.IsNullOrEmpty(dataTable.Rows[i][2].ToString()) ? "misc" : dataTable.Rows[i][2].ToString();
                                        string supplier = string.IsNullOrEmpty(dataTable.Rows[i][3].ToString()) ? "0" : dataTable.Rows[i][3].ToString();
                                        string manufacturer = string.IsNullOrEmpty(dataTable.Rows[i][4].ToString()) ? "0" : dataTable.Rows[i][4].ToString();
                                        string unit = dataTable.Rows[i][5].ToString();
                                        string qty = string.IsNullOrEmpty(dataTable.Rows[i][6].ToString()) ? "0" : dataTable.Rows[i][6].ToString();
                                        string buyingPrice = string.IsNullOrEmpty(dataTable.Rows[i][7].ToString()) ? "0" : dataTable.Rows[i][7].ToString();
                                        string gp = dataTable.Rows[i][9].ToString();
                                        string sellingPrice = string.IsNullOrEmpty(dataTable.Rows[i][10].ToString()) ? "0" : dataTable.Rows[i][10].ToString();
                                        double material, labour, misc;
                                        int supplierId = supplierList.Where(s => s.ContractorName.ToLower() == supplier.ToLower()).Select(s => s.VendorID).FirstOrDefault();
                                        int manufacturerId = manufacturerList.Where(m => m.Name.ToLower() == manufacturer.ToLower()).Select(m => m.Id).FirstOrDefault();
                                        TypeOfBoM(type, Convert.ToDouble(buyingPrice), out material, out labour, out misc);


                                        ProjectMgt.Entity.ProjectBOM projectBOM = new ProjectMgt.Entity.ProjectBOM();
                                        projectBOM.Description = description;
                                        projectBOM.PartNumber = partNumber;
                                        projectBOM.ProjectReference = QueryStringValues.Project;
                                        projectBOM.Supplier = supplierId;
                                        projectBOM.ManufactureID = manufacturerId;
                                        projectBOM.Unit = unit;
                                        projectBOM.WorkSheetID = worksheetID;
                                        projectBOM.Qty = Convert.ToDouble(qty);
                                        projectBOM.Material = material;
                                        projectBOM.Labour = labour;
                                        projectBOM.Mics = misc;
                                        projectBOM.CurrencyID = 54; // British Pound
                                        projectBOM.SellingTotal = Convert.ToDouble(sellingPrice);

                                        projectBOMList.Add(projectBOM);

                                    }
                                }

                            }
                        }
                        // bulk insert
                        db.ProjectBOMs.InsertAllOnSubmit(projectBOMList);
                        db.SubmitChanges();
                        Response.Redirect("~/ProjectBOM.aspx?project=" + QueryStringValues.Project);
                    }
                    else
                    {
                        lblUploadErrorMsg.Text = "File not loaded - one or more tab names have previously been used. Please check and try again.";

                    }
                   

                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void TypeOfBoM(string type, double buyingPrice, out double material, out double labour, out double misc)
    {
        material = 0; labour = 0; misc = 0;
        switch (type.ToLower())
        {
            case "material":
                {
                    material = buyingPrice;
                    break;
                }
            case "labour":
                {
                    labour = buyingPrice;
                    break;
                }
            case "misc":
                {
                    misc = buyingPrice;
                    break;
                }

        }
    }
    private DataTable Import_To_Grid(string conStr, string sheetName)
    {
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //string SheetName = "DealerVoice$";
        cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", sheetName + "$");
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        return dt;

    }
    public string[] GetExcelSheetNames(string connectionString)
    {
        OleDbConnection con = null; DataTable dt = null;
        String conStr = connectionString;
        con = new OleDbConnection(conStr);
        con.Close();
        con.Open();
        dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        if (dt == null)
        {
            return null;
        }
        String[] excelSheetNames = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
           
            excelSheetNames[i] = row["TABLE_NAME"].ToString();
            i++;
        }

        con.Close();
        return excelSheetNames;
    }
    private bool IsValid(string fileName)
    {

        string ext = Path.GetExtension(fileName);
        switch (ext.ToLower())
        {
            case ".xlsx":
                return true;
            case ".xls":
                return true;
            default:
                return false;
        }

    }
    #endregion
}