using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using UserMgt.BAL;
using UserMgt.Entity;
using System.IO;
using System.Web.SessionState;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for MemberHandler
    /// </summary>
    public class MemberHandler : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)

        {
            int callid = 0;
            int ivref = 0;
            int option = 0;
            string qid = string.Empty;


            if (context.Request.Files.Count > 0)

            {



                //var folder = context.Server.MapPath("~/WF/UploadData/" + foldername);
                //if (!System.IO.Directory.Exists(folder))
                //{
                //    System.IO.Directory.CreateDirectory(folder);

                //}


                HttpFileCollection SelectedFiles = context.Request.Files;

                for (int i = 0; i < SelectedFiles.Count; i++)

                {

                    HttpPostedFile PostedFile = SelectedFiles[i];

                    //string FileName = context.Server.MapPath("~/WF/UploadData/" + foldername + "/" + PostedFile.FileName);

                    //PostedFile.SaveAs(FileName);



                    string filePath = PostedFile.FileName;
                    string Extension = System.IO.Path.GetExtension(filePath);
                    //Check the Extention of file
                    if (string.IsNullOrEmpty(Extension))
                    {
                        //lblmsg.Visible = true;
                        //lblError.ForeColor = System.Drawing.Color.Red;
                        //lblError.Text = Resources.DeffinityRes.Pleaseselectafile; //"Please select a file";
                        return;
                    }
                    if (isValid(PostedFile.FileName))
                    {

                        string path = context.Server.MapPath("~/WF/UploadData/Contacts");
                        string fileName = "\\" + PostedFile.FileName;

                        if (System.IO.Directory.Exists(path) == false)
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        PostedFile.SaveAs(path + fileName);
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

                        //lblmsg.Visible = true;
                        //lblmsg.ForeColor = System.Drawing.Color.Green;
                        // lblmsg.Text = "Uploaded Successfully.";
                    }
                    else
                    {
                        //lblmsg.Visible = true;
                        //lblmsg.ForeColor = System.Drawing.Color.Red;
                        //lblError.Text = Resources.DeffinityRes.Pleaseselectvalidfile; //"Please select valid file";

                    }

                }

            }



            else

            {

                context.Response.ContentType = "text/plain";

                context.Response.Write("Please Select Files");

            }



            context.Response.ContentType = "text/plain";

            context.Response.Write("Files Uploaded Successfully!!");

        }

        private bool isValid(string fileName)
        {

            string ext = System.IO.Path.GetExtension(fileName);
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

        public bool IsReusable

        {

            get

            {

                return false;

            }

        }
        private void SaveMember(string firstname, string lastname, string emailaddress, string cellnumber, string permission, string tags)
        {
            try
            {
                int userid = 0;
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
               
                //}
                //1 - Admin
                //value.SID = UserType.SmartPros;
                value.SID = permission.ToLower().Contains("admin") ? 1 : 2;
                value.CreatedDate = DateTime.Now;
                value.ModifiedDate = DateTime.Now;
                value.Status = UserStatus.Active;
                value.isFirstlogin = 0;
                value.ResetPassword = false;
                // value.Company = "";
                value.ContactNumber = cellnumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

                var userDetails = cRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active).FirstOrDefault();
                if (userDetails == null)
                {
                    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                    value.Company = "";
                    cRep.Add(value);
                    userid = value.ID;
                }
                else
                {
                    userDetails.ContactNumber = value.ContactNumber;
                    userDetails.SID = value.SID;
                    userDetails.ContractorName = value.ContractorName;
                    userDetails.EmailAddress = value.EmailAddress;
                    userDetails.ModifiedDate = DateTime.Now;
                    cRep.Edit(userDetails);
                    userid = userDetails.ID;
                }

                var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                var cdEntity = new UserMgt.Entity.UserDetail();

                if (cdRep.GetAll().Where(o => o.UserId == userid).Count() == 0)
                {
                    cdEntity.Address1 = "";
                    cdEntity.Country = 190;
                    cdEntity.PostCode = "";
                    cdEntity.State = "";
                    //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                    //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                    cdEntity.Town = "";
                    cdEntity.UserId = userid;

                    cdRep.Add(cdEntity);
                }

                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var urEntity = new UserMgt.Entity.UserToCompany();

                if (urRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID && o.UserID == userid).Count() == 0)
                {
                    urEntity.CompanyID = sessionKeys.PortfolioID;
                    urEntity.UserID = userid;
                    urRep.Add(urEntity);
                }


                var ud = UserMgt.BAL.UserSkillBAL.UserSkillBAL_SelectAll().Where(o => o.UserId == userid).FirstOrDefault();
                if (ud == null)
                {
                    UserMgt.BAL.UserSkillBAL.UserSkillBAL_Add(new UserMgt.Entity.UserSkill() { Notes = tags, UserId = userid });
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
        private void InsertExcelData(string conStr)
        {
            try
            {
                DataTable dt = Import_To_Grid(conStr);
                string firstname = string.Empty;
                string lastname = string.Empty;
                string email = string.Empty;
                string cellnumber = string.Empty;
                string permission = string.Empty;
                string tags = string.Empty;


                //string ContactEmailAddress = string.Empty;
                //string Religion = string.Empty;
                //string Denomination = string.Empty;
                if (dt.Rows.Count > 1)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {

                        string Address2 = string.Empty;
                        int row = 0;
                        if (dt.Rows[i][row] != null)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                                firstname = dt.Rows[i][row].ToString();
                        }

                        row = 1;
                        if (dt.Rows[i][row] != null)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                                lastname = dt.Rows[i][row].ToString();
                        }

                        row = 2;
                        if (dt.Rows[i][row] != null)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                                email = dt.Rows[i][row].ToString();
                        }

                        row = 3;
                        if (dt.Rows[i][row] != null)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                                cellnumber = dt.Rows[i][row].ToString();
                        }
                        row = 4;
                        if (dt.Rows[i][row] != null)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                                permission = dt.Rows[i][row].ToString();
                        }
                        row = 5;
                        if (dt.Rows[i][row] != null)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                                tags = dt.Rows[i][row].ToString();
                        }



                        SaveMember(firstname, lastname, email, cellnumber, permission, tags);




                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
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
        private DataTable Import_To_Grid(string conStr)
        {
            string[] sheetnames = GetExcelSheetNames(conStr);
            string sheetname = string.Empty;
            if (sheetnames.Length > 0)
                sheetname = sheetnames[0];

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            //To fetch sheet names
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = sheetname;
            cmdExcel.CommandText = string.Format("SELECT  * From  [{0}]", SheetName);
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            return dt;

        }
    }
}