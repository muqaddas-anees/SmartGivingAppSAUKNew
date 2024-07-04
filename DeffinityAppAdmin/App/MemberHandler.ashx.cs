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
using System.Web.SessionState;

namespace DeffinityAppDev.App
{
    /// <summary>
    /// Summary description for MemberHandler
    /// </summary>
    public class MemberHandler : IHttpHandler, IRequiresSessionState
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
                string MembersName = string.Empty;
                string MembersSurname = string.Empty;
                string Address = string.Empty;
                string Town = string.Empty;
                string City = string.Empty;
                string State = string.Empty;

                string Zipcode = string.Empty;
                string MembersMobile = string.Empty;
                string MembersEmailAddress = string.Empty;
                string Religion = string.Empty;
                string Group = string.Empty;
                string Denomination = string.Empty;
                //string ContactTelephone = string.Empty;
                //string ContactEmailAddress = string.Empty;
                //string Religion = string.Empty;
                //string Denomination = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string Address2 = string.Empty;
                    int row = 0;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            MembersName = dt.Rows[i][row].ToString();
                    }

                    row = 1;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            MembersSurname = dt.Rows[i][row].ToString();
                    }

                    row = 2;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Address = dt.Rows[i][row].ToString();
                    }

                    row = 3;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Town = dt.Rows[i][row].ToString();
                    }
                    row = 4;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            City = dt.Rows[i][row].ToString();
                    }
                    row = 5;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            State = dt.Rows[i][row].ToString();
                    }
                    row = 6;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Zipcode = dt.Rows[i][row].ToString();
                    }
                    row = 7;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            MembersMobile = dt.Rows[i][row].ToString();
                    }
                    row = 8;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            MembersEmailAddress = dt.Rows[i][row].ToString();
                    }
                    row = 9;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Religion = dt.Rows[i][row].ToString();

                    }
                    row = 10;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Group = dt.Rows[i][row].ToString();
                    }
                    row = 11;
                    if (dt.Rows[i][row] != null)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                            Denomination = dt.Rows[i][row].ToString();
                    }
                    //row = 11;
                    //if (dt.Rows[i][row] != null)
                    //{
                    //    if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                    //        ContactTelephone = dt.Rows[i][row].ToString();
                    //}
                    //row = 12;
                    //if (dt.Rows[i][row] != null)
                    //{
                    //    if (!string.IsNullOrEmpty(dt.Rows[i][row].ToString()))
                    //        ContactEmailAddress = dt.Rows[i][row].ToString();
                    //}
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
                    if (!string.IsNullOrEmpty(MembersEmailAddress))
                    {
                        var cRep = new UserRepository<Contractor>();
                        var cvRep = new UserRepository<v_contractor>();
                        string pw = "Faith@2021";
                        var value = new UserMgt.Entity.Contractor();
                        value.ContractorName = MembersName + " " +MembersSurname;
                        value.EmailAddress = MembersEmailAddress;
                        value.LoginName = MembersEmailAddress; 
                        value.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
                        //1 - Admin
                        value.SID = UserType.SmartPros;
                        value.CreatedDate = DateTime.Now;
                        value.ModifiedDate = DateTime.Now;
                        value.Status = UserStatus.Active;
                        value.isFirstlogin = 0;
                        value.ResetPassword = false;
                        value.Company = "";
                        value.ContactNumber = MembersMobile;



                        //value. = Convert.ToInt32(HttpContext.Current.Session["InstId"]);
                        //check email is already exists 
                        //user should not be customer user
                        /// if (cRep.GetAll().Where(o => o.EmailAddress.ToLower() == value.EmailAddress.ToLower() && o.Status.ToLower() == "active").Count() == 0)
                        /// 


                        if (cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower()&& o.EmailAddress.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active).Count() == 0)
                        {
                            cRep.Add(value);
                        }

                        var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        var cdEntity = new UserMgt.Entity.UserDetail();
                        cdEntity.Address1 = Address;
                        cdEntity.Country = 190;
                        cdEntity.PostCode = Zipcode;
                        cdEntity.State = State;
                        //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        cdEntity.Town = Town;
                        cdEntity.UserId = value.ID;



                        int rid=0;
                        int gid = 0;
                        if (Religion.Length > 0)
                        {
                            var rData = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().Where(o => o.Name.ToLower().Trim() == Religion.ToLower().Trim()).FirstOrDefault();
                            if (rData == null)
                            {
                                rData = new PortfolioMgt.Entity.DenominationDetail();
                                rData.IsActive = true;
                                rData.Name = Religion;
                                PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Add(rData);
                            }

                            rid = rData.ID;
                            cdEntity.DenominationDetailsID = rData.ID;
                        }
                        if (Group.Length > 0)
                        {
                            var rData = PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Select().Where(o => o.Name.ToLower().Trim() == Group.ToLower().Trim()).FirstOrDefault();
                            if (rData == null)
                            {
                                rData = new PortfolioMgt.Entity.DenominationGroupDetail();
                               // rData.IsActive = true;
                                rData.Name = Denomination;
                                rData.DenominationDetailsID = rid;
                                PortfolioMgt.BAL.DenominationGroupDetailsBAL.DenominationGroupDetailsBAL_Add(rData);
                            }
                            gid = rData.ID;
                            cdEntity.GroupDetailsID = rData.ID;
                        }
                        if (Denomination.Length > 0)
                        {
                            var rData = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().Where(o => o.Name.ToLower().Trim() == Denomination.ToLower().Trim()).FirstOrDefault();
                            if (rData == null)
                            {
                                rData = new PortfolioMgt.Entity.SubDenominationDetail();
                                rData.IsActive = true;
                                rData.Name = Denomination;
                                rData.DenominationGroupDetailsID = rid;
                                PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Add(rData);
                            }
                            cdEntity.SubDenominationDetailsID = rData.ID;
                        }

                        cdRep.Add(cdEntity);

                        if (sessionKeys.PortfolioID > 0)
                        {
                            var cuRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                            var usertoCompany = new UserMgt.Entity.UserToCompany();
                            usertoCompany.CompanyID = sessionKeys.PortfolioID;
                            usertoCompany.UserID = value.ID;
                            cuRep.Add(usertoCompany);
                        }
                        else
                        {

                            var pd = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.PortFolio == "Faith church").FirstOrDefault();
                            if (pd != null)
                            {
                                var cuRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                                var usertoCompany = new UserMgt.Entity.UserToCompany();
                                usertoCompany.CompanyID = pd.ID;
                                usertoCompany.UserID = value.ID;
                                cuRep.Add(usertoCompany);
                            }

                        }

                        //insert into portfolio contacts
                        //  var pDetails = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().Where(o => o.PortFolio.ToLower().Trim() == OrganizationName.ToLower().Trim()).FirstOrDefault();

                        //if (pDetails == null)
                        //{
                        //    pDetails = new PortfolioMgt.Entity.ProjectPortfolio();

                        //    pDetails.OrgarnizationGUID = Guid.NewGuid().ToString();
                        //    pDetails.PortFolio = OrganizationName;
                        //    pDetails.Address = Address;
                        //    pDetails.Town = Town + ' ' + City;
                        //    pDetails.State = State;
                        //    pDetails.Postcode = Zipcode;
                        //    pDetails.TelephoneNumber = OrganizationTelephone;
                        //    pDetails.EmailAddress = OrganizationEmailAddress;
                        //    pDetails.RegistrationNumber = WebAddress;
                        //    pDetails.Owner = value.ID;
                        //    pDetails.OrgarnizationStatus = "Active";
                        //    pDetails.OrgarnizationApproval = "Pending Approval";
                        //    // add DenominationDetails
                        //    int rid = 0;



                        //    pDetails.CountryID = 190;
                        //    pDetails.StartDate = DateTime.Now;
                        //    pDetails.EndDate = DateTime.Now;

                        //    PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_Add(pDetails);

                        //    var cuRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                        //    var usertoCompany = new UserMgt.Entity.UserToCompany();
                        //    usertoCompany.CompanyID = pDetails.ID;
                        //    usertoCompany.UserID = value.ID;
                        //    cuRep.Add(usertoCompany);

                        //    var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                        //    var cdEntity = new UserMgt.Entity.UserDetail();
                        //    cdEntity.Address1 = pDetails.Address;
                        //    cdEntity.Country = pDetails.CountryID;
                        //    cdEntity.PostCode = pDetails.Postcode;
                        //    cdEntity.State = pDetails.State;
                        //    cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                        //    cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                        //    cdEntity.Town = pDetails.Town;
                        //    cdEntity.UserId = value.ID;


                        //    cdRep.Add(cdEntity);




                        //}




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