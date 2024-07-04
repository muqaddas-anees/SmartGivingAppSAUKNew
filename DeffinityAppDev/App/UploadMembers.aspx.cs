using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;
using POMgt.DAL;
using ProjectMgt.DAL;
using System.Data;
using System.IO;
using DeffinityManager;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net.Mail;
using System.Data.OleDb;

namespace DeffinityAppDev.App
{
    public partial class UploadMembers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



      

        protected void btnUpload_Click(object sender, EventArgs e)
        {

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

        private void SaveMember(string firstname, string lastname, string emailaddress, string state, string tags)
        {
            try
            {
                var cRep = new UserRepository<UserMgt.Entity.Contractor>();
                var cvRep = new UserRepository<v_contractor>();
                string pw = "SmartGiving@2022";
                var value = new UserMgt.Entity.Contractor();
                value.ContractorName = firstname + " " + lastname;
                value.EmailAddress = emailaddress;
                value.LoginName = emailaddress;
                //if (txtPassword.Text.Trim().Length > 0)
                //{
                //    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");
                //}
                //else
                //{
                value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                //}
                //1 - Admin
                //value.SID = UserType.SmartPros;
                value.SID = 2;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.Status = UserStatus.Active;
                value.isFirstlogin = 0;
                value.ResetPassword = false;
                value.Company = "";
                value.ContactNumber = "";


                if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active && o.SID == UserType.SmartPros).Count() == 0)
                {
                    cRep.Add(value);
                }

                var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                var cdEntity = new UserMgt.Entity.UserDetail();
                cdEntity.Address1 = "";
                cdEntity.Country = 190;
                cdEntity.PostCode = "";
                cdEntity.State = state;
                //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                cdEntity.Town = "";
                cdEntity.UserId = value.ID;
                cdEntity.DenominationDetailsID = 0;
                cdEntity.SubDenominationDetailsID = 0;



                cdRep.Add(cdEntity);

                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var urEntity = new UserMgt.Entity.UserToCompany();
                urEntity.CompanyID = sessionKeys.PortfolioID;
                urEntity.UserID = value.ID;
                urRep.Add(urEntity);


                var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == value.ID).FirstOrDefault();
                if (ud == null)
                {
                    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = tags, UserId = value.ID });
                }
                else
                {
                    ud.Notes = tags;
                    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Update(ud);
                }

                //uploadLogo(value.ID);
                //img.ImageUrl = GetImageUrl(value.ID.ToString());
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUploadData_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = fileUpload2.PostedFile.FileName;
                string Extension = Path.GetExtension(filePath);
                //Check the Extention of file
              
                if (IsValid(fileUpload2.PostedFile.FileName))
                {

                    string path = Server.MapPath("~/WF/UploadData");
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
                    //for (int i = 0; i < sheetNames.Length; i++)
                    //{
                    //    sheetNames[i] = sheetNames[i].Replace("$", string.Empty);
                    //    sheetNames[i] = sheetNames[i].Replace("'", string.Empty);
                    //}
                  //  sheetNames = sheetNames.Where(s => s.ToLower() != "x-dropdowns").ToArray();

                  
                               
                              
                                    DataTable dataTable = Import_To_Grid(conStr, sheetNames[0].Replace("$", string.Empty));

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow[] foundRows = dataTable.Select();


                        for (int i = 0; i < foundRows.Count(); i++)
                        {
                            string emailaddress = dataTable.Rows[i][0].ToString();
                            string firstname = dataTable.Rows[i][1].ToString();
                            string lastname = dataTable.Rows[i][2].ToString();
                            string state = dataTable.Rows[i][3].ToString();
                            string tags = dataTable.Rows[i][4].ToString().Replace('"', ' ').Trim();

                            SaveMember(firstname, lastname, emailaddress, state, tags);

                        }
                    }



                    

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
      

    }
}