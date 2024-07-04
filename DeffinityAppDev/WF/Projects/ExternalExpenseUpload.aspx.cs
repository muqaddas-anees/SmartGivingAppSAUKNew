using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Finance.DAL;
using Finance.Entity;
using UserMgt.DAL;
using UserMgt.Entity;

namespace DeffinityAppDev.WF.Projects
{
    public partial class ExternalExpenseUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = fileUpload.PostedFile.FileName;
                string Extension = Path.GetExtension(filePath);
                //Check the Extention of file
                if (string.IsNullOrEmpty(Extension))
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = Resources.DeffinityRes.Pleaseselectafile; //"Please select a file";
                    return;
                }
                if (IsValidExcel(fileUpload.PostedFile.FileName))
                {
                    lblMsg.Visible = false;

                    string path = Server.MapPath("~\\WF\\UploadData\\Expense");
                    string fileName = "\\" + fileUpload.FileName;

                    if (System.IO.Directory.Exists(path) == false)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    fileUpload.SaveAs(path + fileName);
                    string conStr = string.Empty;
                    //string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + "Extended Properties=Excel 8.0;"; ;
                    switch (Extension)
                    {
                        case ".xls": //Excel 97-03
                            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                                     .ConnectionString;
                            break;
                        case ".xlsx": //Excel 07
                            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                                      .ConnectionString;
                            break;
                    }
                    
                    conStr = String.Format(conStr, path + fileName, "Yes");
                    
                    InsertExcelData(conStr);

                    if (File.Exists(path + fileName))
                    {
                        File.Delete(path + fileName);
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = Resources.DeffinityRes.Pleaseselectvalidfile; //"Please select valid file";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private bool IsValidExcel(string fileName)
        {

            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {

                case ".xls":
                    return true;
                case ".xlsx":
                    return true;

                default:
                    return false;
            }

        }
        private void InsertExcelData(string conStr)
        {
            try
            {
                
                DataTable dt = Import_To_Grid(conStr);
                
                using (UserDataContext ud = new UserDataContext())
                {
                    using (FinanceModuleDataContext fd = new FinanceModuleDataContext())
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string projectReference = string.IsNullOrEmpty(dt.Rows[i][0].ToString()) ? "0" : dt.Rows[i][0].ToString();
                            if (Convert.ToInt32(projectReference) > 0)
                            {
                                string d = dt.Rows[i][1].ToString().Trim();
                                DateTime? excelDate = null;

                                //try
                                //{
                                //    string[] sp = d.Split('/');
                                //    if (sp.Count() > 1)
                                //    {
                                //        d = sp[1] + "/" + sp[0] + "/" + sp[2];
                                //        excelDate = new DateTime(int.Parse(sp[2]), int.Parse(sp[1]), int.Parse(sp[0]));
                                //    }
                                //    else
                                //    {
                                //        string[] sp1 = d.Split('-');
                                //        d = sp1[1] + "/" + sp1[0] + "/" + sp1[2];
                                //        excelDate = new DateTime(int.Parse(sp1[2]), int.Parse(sp1[1]), int.Parse(sp1[0]));
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    excelDate = null;
                                //    LogExceptions.WriteExceptionLog(ex);
                                //}
            

                                //DateTime date = Convert.ToDateTime(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "01/01/1900" : dt.Rows[i][1].ToString());
                                DateTime? date = Convert.ToDateTime(d);// Convert.ToDateTime(string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "01/01/1900" : dt.Rows[i][1].ToString());
                                string description = string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "0" : dt.Rows[i][2].ToString();
                                string entryTypeName = string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "0" : dt.Rows[i][3].ToString();
                                double qty = Convert.ToDouble(string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "0" : dt.Rows[i][4].ToString());
                                double unitCost = Convert.ToDouble(string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "0" : dt.Rows[i][5].ToString());
                                string assignedToName = string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? "0" : dt.Rows[i][7].ToString();
                                string expensedName = string.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? "0" : dt.Rows[i][8].ToString();
                                double amount = qty * unitCost;

                                int assignedTo = 0;
                                int entryTypeId = 0;
                                bool expensed = expensedName.ToLower() == "yes" ? true : false;
                                var contractors = ud.Contractors.Where(c => c.ContractorName.ToLower() == assignedToName.ToLower()).FirstOrDefault();
                                var entryType = fd.Expensesentrytypes.Where(e => e.ExpensesentryType1.ToLower() == entryTypeName.ToLower()).FirstOrDefault();

                                if (contractors != null)
                                    assignedTo = contractors.ID;
                                if (entryType != null)
                                    entryTypeId = entryType.ID;
                                //check date is having value
                                if (date.HasValue)
                                {
                                    InsertExternalExpenses(Convert.ToInt32(projectReference), sessionKeys.UID,
                                        entryTypeId, date.Value, amount, "", description, qty, unitCost, assignedTo, expensed);
                                }
                                else
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    lblMsg.Text = "Please check the date format";
                                }
                            }

                        }
                    }
                }
                lblMsg.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "Expenses uploaded successfully";

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public int InsertExternalExpenses(int projectReference, int contractorId, int entryTypeId,
            DateTime date, double amount, string notes, string description, double qty,
            double unitCost, int assignedTo, bool expensed)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("DBstring");
                DbCommand cmd = db.GetStoredProcCommand("DN_ExternalExpensesInsert");
                db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, projectReference);
                db.AddInParameter(cmd, "@ContractorID", DbType.Int32, contractorId);
                db.AddInParameter(cmd, "@ExpensesentrytypeID", DbType.Int32, entryTypeId);
                db.AddInParameter(cmd, "@ExternalExpensesDate", DbType.DateTime, date);
                db.AddInParameter(cmd, "@amount", DbType.Double, amount);
                db.AddInParameter(cmd, "@Notes", DbType.String, notes);
                db.AddInParameter(cmd, "@Description", DbType.String, description);
                db.AddInParameter(cmd, "@Qty", DbType.Double, qty);
                db.AddInParameter(cmd, "@UnitCost", DbType.Double, unitCost);
                db.AddInParameter(cmd, "@AssignedTo", DbType.Int32, assignedTo);
                db.AddInParameter(cmd, "@Expensed", DbType.Boolean, expensed);

                int getVal = Convert.ToInt32(db.ExecuteNonQuery(cmd));
                cmd.Dispose();
                return getVal;
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return 0;
            }
            finally
            {

            }
        }
        private DataTable Import_To_Grid(string conStr)
        {
            string[] sheetnames = GetExcelSheetNames(conStr);


            string sheetname = string.Empty;
            if (sheetnames.Length > 0)
            {
                sheetname = sheetnames[0];
            }

          

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", sheetname);
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
                if (!row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                {
                    excelSheetNames[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
            }
            con.Close();
            //LogExceptions.LogException(excelSheetNames.ToString());
            return excelSheetNames;
        }
    }
}