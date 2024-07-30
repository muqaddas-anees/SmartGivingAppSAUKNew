using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using DC.BLL;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Reflection;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;
using UserMgt.BAL;
using UserMgt.Entity;
using Deffinity.PortfolioManager;
using DocumentFormat.OpenXml.Vml;
using DataTables;
using DeffinityManager.BLL;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for OrganizationHandler
    /// </summary>
    public class OrganizationHandler : IHttpHandler
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

        private void InsertExcelData(string conStr)
        {
            try
            {
                DataTable dt = Import_To_Grid(conStr);
                string DateUploaded = string.Empty;

                string CharityName = string.Empty;

                string RegistrationNumber = string.Empty;
                string PhoneNumber = string.Empty;

                string EmailAddress = string.Empty;
                string WebAddress = string.Empty;
                string Address = string.Empty;

                string Town = string.Empty;
                string City = string.Empty;
                string State = string.Empty;
                string Postcode = string.Empty;
                string CharityType = string.Empty;
                string Activity = string.Empty;
                
                for (int i = 1; i < dt.Rows.Count; i++)
                {

                    string Address2 = string.Empty;
                    int row = 0;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            DateUploaded = dt.Rows[i][row].ToString();
                    }

                    row = 1;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            CharityName = dt.Rows[i][row].ToString();
                    }

                    row = 2;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            RegistrationNumber = dt.Rows[i][row].ToString();
                    }

                    row = 3;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            PhoneNumber = dt.Rows[i][row].ToString();
                    }
                    row = 4;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            EmailAddress = dt.Rows[i][row].ToString();
                    }
                    row = 5;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            WebAddress = dt.Rows[i][row].ToString();
                    }
                    row = 6;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Address = dt.Rows[i][row].ToString();
                    }
                    row = 7;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Town = dt.Rows[i][row].ToString();
                    }
                    row = 8;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            City = dt.Rows[i][row].ToString();
                    }
                    row = 9;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            State = dt.Rows[i][row].ToString();

                    }
                    row = 10;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Postcode = dt.Rows[i][row].ToString();
                    }
                    row = 11;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            CharityType = dt.Rows[i][row].ToString();
                    }
                    row = 12;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Activity = dt.Rows[i][row].ToString();
                    }
                    //row = 13;
                    //if (dt.Rows[i][row] != null)
                    //{
                    //    if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                    //        Religion = dt.Rows[i][row].ToString();
                    //}

                    //row = 14;
                    //if (dt.Rows[i][row] != null)
                    //{
                    //    if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                    //        Denomination = dt.Rows[i][row].ToString();
                    //}

                    int contactID = 0;
                    //name = name + " " + surname;
                    if (!string.IsNullOrEmpty(CharityName))
                    {
                        var cdate = DateTime.Now;
                        try
                        {
                            if (DateUploaded.Length > 0)
                            {
                                cdate = Convert.ToDateTime(DateUploaded.Trim());
                            }
                        }
                        catch (Exception ex)
                        {
                            LogExceptions.WriteExceptionLog(ex);
                        }
                        var cRep = new UserRepository<Contractor>();
                        var cvRep = new UserRepository<v_contractor>();
                        string pw = "Plegit@2024!#";
                        var value = new UserMgt.Entity.Contractor();

                        var pDetails = new PortfolioMgt.Entity.ProjectPortfolio();

                        value.ContractorName = CharityName;
                        value.EmailAddress = EmailAddress.Trim();
                        value.LoginName = EmailAddress.Trim(); //value.ContractorName.Replace(" ", "");
                                                               //if (HttpContext.Current.Session["password"] == null)
                                                               //{
                                                               //    var pw = DeffinityManager.RandomPassword.GetPassword();
                                                               //    pwd.Add(new plist() { Email = value.EmailAddress, Password = pw });
                                                               //    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                                                               //}
                                                               //else
                                                               //{
                                                               //    var pw1 = HttpContext.Current.Session["password"].ToString();
                                                               //    pwd.Add(new plist() { Email = value.EmailAddress, Password = pw1 });
                                                               //    value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw1, "SHA1");
                                                               //}
                        value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        value.TypeofLogin = pw;
                        //1 - Admin
                        value.SID = UserType.SmartPros;
                        value.CreatedDate = cdate;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 1;
                        value.ResetPassword = false;
                        value.Company = CharityName;
                        value.ContactNumber = PhoneNumber;

                        //value. = Convert.ToInt32(HttpContext.Current.Session["InstId"]);
                        //check email is already exists 
                        //user should not be customer user
                        /// if (cRep.GetAll().Where(o => o.EmailAddress.ToLower() == value.EmailAddress.ToLower() && o.Status.ToLower() == "active").Count() == 0)
                        /// 
                        

                        if (cvRep.GetAll().Where(o =>  o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active && o.SID != UserType.Donor).Count() == 0)
                        {
                            cRep.Add(value);


                           
                        }

                        if (value.ID >0)
                        {
                            //insert into portfolio contacts
                           // PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.PortFolio.ToLower().Trim() == OrganizationName.ToLower().Trim()).FirstOrDefault();

                            //if (pDetails == null)
                            //{
                               
                               // pDetails = new PortfolioMgt.Entity.ProjectPortfolio();
                              
                                pDetails.OrgarnizationGUID = Guid.NewGuid().ToString();
                                pDetails.PortFolio = CharityName.Trim();
                                pDetails.Address = Address.Trim();
                                pDetails.Town = Town.Trim() + ' ' + City.Trim();
                                pDetails.State = State.Trim();
                                pDetails.Postcode = Postcode;
                                pDetails.TelephoneNumber = PhoneNumber;
                                pDetails.EmailAddress = EmailAddress.Trim();
                                pDetails.BaseUrl = WebAddress.Trim();
                                pDetails.RegistrationNumber = RegistrationNumber.Trim();
                                pDetails.Owner = value.ID;
                                pDetails.OrgarnizationStatus = "Uploaded";
                                pDetails.OrgarnizationApproval = "Uploaded";
                                pDetails.Description = Activity;
                                pDetails.Justification = CharityType;
                           
                                // add DenominationDetails
                                int rid = 0;
                               
                                pDetails.CountryID = 190;
                                
                                pDetails.StartDate = cdate;
                                pDetails.EndDate = DateTime.Now;

                                PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Add(pDetails);

                                var cuRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                                var usertoCompany = new UserMgt.Entity.UserToCompany();
                                usertoCompany.CompanyID = pDetails.ID;
                                usertoCompany.UserID = value.ID;
                                cuRep.Add(usertoCompany);

                                var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                                var cdEntity = new UserMgt.Entity.UserDetail();
                                cdEntity.Address1 = pDetails.Address;
                                cdEntity.Country = pDetails.CountryID;
                                cdEntity.PostCode = pDetails.Postcode;
                                cdEntity.State = pDetails.State;
                                //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                                //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                                cdEntity.Town = pDetails.Town;
                                cdEntity.UserId = value.ID;


                                cdRep.Add(cdEntity);


                                //PortfolioMgt.Entity.PortfolioContact pc_check = CustomerContactsBAL.CheckContact(ContactEmailAddress, pDetails.ID);

                                //if (pc_check == null)
                                //{
                                //    PortfolioMgt.Entity.PortfolioContact pc = new PortfolioMgt.Entity.PortfolioContact();
                                //    pc.PortfolioID = pDetails.ID;
                                //    pc.Name = ContactName;
                                //    pc.Title = "";
                                //    pc.Email = ContactEmailAddress;
                                //    pc.Telephone = ContactEmailAddress;
                                //    pc.Mobile = ContactTelephone;
                                //    pc.LogintoPortal = false;
                                //    pc.Key_Contact = false;
                                //    pc.Fax = "";
                                //    CustomerContactsBAL.AddCustomerContacts(pc);
                                //    contactID = pc.ID;


                                //    var pA = new PortfolioMgt.Entity.PortfolioContactAddress();
                                //    pA.ContactID = contactID;
                                //    pA.Address = Address;
                                //    pA.City = Town;
                                //    pA.PostCode = Zipcode;
                                //    pA.State = State;
                                //    pA.City = City;
                                //    pA.LoggedDatetime = DateTime.Now;

                                //    PortfolioMgt.BAL.PortfolioContactAddressBAL.PortfolioContactAddressBAL_add(pA);

                                //}


                         //   }


                        }

                        if (value != null)
                        {
                            LogExceptions.LogException("got user details");
                            if (pDetails != null)
                            {
                                LogExceptions.LogException("got org details");
                                UpdateActiveCamp(value, "UPLOADED", pDetails.PortFolio);

                                LogExceptions.LogException("Completed");
                            }
                        }

                        //if (logintoPortal && contactID > 0)
                        //{
                        //    ContractorsAndAssociateInsert(contactID);
                        //}



                    }


                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }


        private void UpdateActiveCamp(UserMgt.Entity.Contractor uDetails, string tag,string orgname)
        {
            var helper = new ActiveCampaignHelper();

            // Create contact and get contactId
            // var contactId = helper.CreateContact(txtemail.Text, uDetails.ContractorName, uDetails.LastName);
            var contactId = helper.EnsureContact(uDetails.EmailAddress.ToLower(), uDetails.ContractorName, uDetails.LastName,orgname);
            if (contactId.HasValue)
            {

                // Create a tag and get tagId
                //var tagId = helper.CreateTag("PREMID");
                var tagId = helper.EnsureTag(tag);
                if (tagId.HasValue)
                {
                    // Add tag to contact
                    if (helper.AddTagToContact(contactId.Value, tagId.Value))
                    {
                        LogExceptions.LogException("Contact created and tagged successfully!");
                    }
                    else
                    {
                        LogExceptions.LogException("Failed to add tag to contact.");
                    }
                }
                else
                {
                    LogExceptions.LogException("Failed to create tag.");
                }


            }
            else
            {
                LogExceptions.LogException("Failed to create contact.");
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