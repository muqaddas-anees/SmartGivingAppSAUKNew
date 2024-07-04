using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using UserMgt.Entity;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using ListItem = System.Web.UI.WebControls.ListItem;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;

namespace DeffinityAppDev.App
{
    public partial class DonorList : System.Web.UI.Page
    {
        public const string users = "users";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlShowUpload.Visible = false;
                //firt time
                Session[users] = null;
                //check users and update the ower id in Project portfoliio table
                BindCountry();
                BingGrid();

                if (QueryStringValues.Type == "2")
                {
                    lblModelHeading.Text = "Upload Donors";
                    btnUpload.Text = "Upload Donors";
                    lblPageTitle.Text = "Donors";
                    lblSubHeading.Text = "Donors";
                    btnAddOrganization.Text = "Add New Donor";
                }
                else
                {
                    lblModelHeading.Text = "Upload Members";
                    btnUpload.Text = "Upload Members";
                    lblPageTitle.Text = "Members";
                    lblSubHeading.Text = "Members";
                    btnAddOrganization.Text = "Add New Member";
                    vimeo.Src = "https://player.vimeo.com/video/773365561?h=47f350836f";


                }
            }
        }

        private void BindCountry()
        {
            LocationRepository<Location.Entity.CountryClass> lcRep = new LocationRepository<Location.Entity.CountryClass>();
            var lc = lcRep.GetAll().Where(o => o.Visible == 'Y').OrderBy(o => o.Country1).ToList();
            if (lc.Count > 0)
            {
                ddlCountry.DataSource = lc;
                ddlCountry.DataTextField = "Country1";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
            }
            ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        //public List<UserMgt.Entity.v_contractor> GetUsers(bool getNewdata = false)
        //{
        //    //if (getNewdata)
        //    //    Session[users] = null;
        //    //if (Session[users] == null)
        //    //{
        //    //    Session[users] = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
        //    //}

        //    //return (Session[users] as List<UserMgt.Entity.v_contractor>).ToList();
        //    return UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().ToList();
        //}
        public void BingGrid(bool getNewData = false)
        {
            try
            {
                //Active

                var iList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.Status == "Active").ToList();// //PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectAll().ToList();
                //var tList = PortfolioMgt.BAL.PortfolioTypeBAL.PortfolioTypeBAL_Select();
                //var rlist = PortfolioMgt.BAL.DenominationDetailsBAL.DenominationDetailsBAL_Select().ToList();

                //var dlist = PortfolioMgt.BAL.SubDenominationDetailsBAL.SubDenominationDetailsBAL_Select().ToList();
                //var pb = new PortfolioMgt.BAL.PortfolioContactBAL();
                //var pdlist = pb.PortfolioContact_SelectAll().ToList();
                //var uList = GetUsers(getNewData);

                var rData = (from p in iList
                                 //join u in uList on p.ID equals u.CompanyID
                             select new
                             {
                                 ID = p.ID,
                                 InstanceName = p.ContractorName,
                                 Name = p.ContractorName,
                                 Address = p.Address1,
                                 p.EmailAddress,
                                 // p.OrgarnizationStatus,
                                 //p.OrgarnizationGUID,
                                 //p.OrgarnizationApproval,
                                 p.Town,
                                 p.State,
                                 CountryID = p.Country,
                                 p.DenominationDetailsID,
                                 Religion = p.DenominationDetailsName,// p.DenominationDetailsID.HasValue ? (rlist.Where(o => o.ID == p.DenominationDetailsID.Value).FirstOrDefault().Name) : string.Empty,
                                 Denomination = p.SubDenominationDetailsName,// p.SubDenominationDetailsID.HasValue ? (dlist.Where(o => o.ID == p.SubDenominationDetailsID.Value).FirstOrDefault().Name) : string.Empty,
                                 Contact = p.ContractorName,// pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Name : string.Empty,
                                                            // ContactEmail = pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Email : string.Empty,
                                 ContactNumber = p.ContactNumber, //pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault() != null ? pdlist.Where(o => o.PortfolioID == p.ID).FirstOrDefault().Mobile : string.Empty,
                                 LogoPath = "",
                                 Status = p.Status,
                                 Permission = p.SID.Value == 1 ? "Admin" : "Member",

                             }).ToList();

                // var ulist = GetUsers();

                //if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                //{
                //    rData = rData.Where(p => (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.EmailAddress != null ? p.EmailAddress.Contains(txtSearch.Text.ToLower()) : false)
                //|| (p.Administrator != null ? p.Administrator.Contains(txtSearch.Text.ToLower()) : false)).ToList();
                //}


                if (QueryStringValues.Type == "all")
                {

                    var resultlist = rData.Where(o => o.InstanceName != "").OrderBy(o => o.InstanceName).ToList();

                    //lblNumberofInstances.Text = resultlist.Count().ToString();
                    // lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist;
                    GridInstances.DataBind();
                }
                else
                {
                    var type = QueryStringValues.Type;
                    var resultlist = rData.OrderBy(o => o.InstanceName).ToList();

                    if (Convert.ToInt32(ddlCountry.SelectedValue) > 0)
                        resultlist = resultlist.Where(o => o.CountryID == Convert.ToInt32(ddlCountry.SelectedValue)).ToList();

                    if (txtsearch.Value.Length > 0)
                    {
                        resultlist = resultlist.Where(p =>
                        (p.InstanceName != null ? p.InstanceName.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                || (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                 || (p.Name != null ? p.Name.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                  || (p.ContactNumber != null ? p.ContactNumber.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                  || (p.Address != null ? p.ContactNumber.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    }
                    else
                    {
                        if (type == "2")
                        {
                            resultlist = resultlist.Where(p => p.Permission == "Member").ToList();
                        }
                        else
                        {
                            resultlist = resultlist.Where(p => p.Permission == "Admin").ToList();
                        }
                    }
                    //    if (txtst.Value.Length > 0)
                    //    {
                    //        resultlist = resultlist.Where(p => (p.State != null ? p.State.ToLower().Contains(txtsearch.Value.ToLower()) : false)
                    //|| (p.EmailAddress != null ? p.EmailAddress.ToLower().Contains(txtsearch.Value.ToLower()) : false)).ToList();
                    //    }
                    // lblNumberofInstances.Text = resultlist.Count().ToString();
                    //lblNumberofUsers.Text = uList.Where(o => resultlist.Select(p => p.PortfolioID).Contains(o.CompanyID.HasValue ? o.CompanyID.Value : 0)).Count().ToString();
                    GridInstances.DataSource = resultlist.OrderBy(o => o.InstanceName).ToList();
                    GridInstances.DataBind();

                    if (QueryStringValues.Type == "2")
                        GridInstances.Columns[9].Visible = false;
                }



            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }





        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BingGrid();

        }
        protected void GridInstances_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gv = new GridView();
                GridViewRow row = e.Row;

                // Make sure we aren't in header/footer rows
                if (row.DataItem == null)
                {
                    return;
                }
                else
                {
                    //gv = (GridView)row.FindControl("gvInnerUsers");

                    //var dataRow = row.DataItem as dynamic;
                    //var r1 = dataRow.PortfolioID;
                    //var r = Convert.ToInt32(r1);
                    //if (r > 0)
                    //{
                    //    gv.DataSource = GetUsers().Where(o => o.CompanyID == r).OrderBy(o => o.ContractorName).ToList();
                    //    gv.DataBind();
                    //}
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            //if (gv.UniqueID == gvUniqueID)
            //{
            //    gv.PageIndex = gvNewPageIndex;
            //    gv.EditIndex = gvEditIndex;
            //    //Check if Sorting used


            //}


        }
        protected void GridInstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridInstances.PageIndex = e.NewPageIndex;
            BingGrid();
        }
        protected void GridInstances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                var userid = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "sendpasswordmail")
                {
                    huid.Value = userid.ToString();
                    mdlManagePassword.Show();
                }
                else
                if (e.CommandName == "password")
                {
                    huid.Value = userid.ToString();
                    mdlManagePassword.Show();

                }
                else if (e.CommandName == "status")
                {
                    var u = UserMgt.BAL.ContractorsBAL.Contractor_SelectByID(userid);

                    if (u.Status == UserMgt.BAL.ContractorStatus.Active)
                        u.Status = UserMgt.BAL.ContractorStatus.InActive;
                    else
                        u.Status = UserMgt.BAL.ContractorStatus.Active;
                    UserMgt.BAL.ContractorsBAL.Contractor_UpdateByStatus(userid, u.Status);
                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid(true);
                }
                else if (e.CommandName == "Instance")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.Visible.HasValue ? c.Visible.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateVisibility(userid, true);
                    }

                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
                else if (e.CommandName == "Modules")
                {
                    var c = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(userid);
                    if (c.AllowModules.HasValue ? c.AllowModules.Value : false == false)
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, false);
                    }
                    else
                    {
                        var u = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateAllowModules(userid, true);
                    }

                    // lblMsg.Text = Resources.DeffinityRes.UpdatedSuccessfully;
                    BingGrid();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnGenaratePassword_Click(object sender, EventArgs e)
        {
            txtPassword.Text = DeffinityManager.Utill.RandomPasswordGenerator.GeneratePassword(8);
        }
        protected void btnSubmitPop_Click(object sender, EventArgs e)
        {
            try
            {
                //var user
                if (!string.IsNullOrEmpty(huid.Value))
                {

                    if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        //update password
                        var u = UserMgt.BAL.ContractorsBAL.Contractor_UpdatePassword(Convert.ToInt32(huid.Value.Trim()), txtPassword.Text.Trim());
                        PasswordSendToMail(u, txtPassword.Text.Trim());
                        txtPassword.Text = string.Empty;
                        huid.Value = string.Empty;
                        lblMsgPop.Text = "Password sent to user successfully";
                        mdlManagePassword.Show();
                    }
                    else
                        lblErrorPop.Text = "Please enter password";

                }
                else
                {
                    lblErrorPop.Text = "Please select user";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void PasswordSendToMail(UserMgt.Entity.Contractor con, string password)
        {
            try
            {
                Email em = new Email();
                var v_contact = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).Where(o => o.ID == con.ID).FirstOrDefault();  //UserMgt.BAL.ContractorsBAL.v_Contractor_SelectByID(con.ID);

                var url = Deffinity.systemdefaults.GetWebUrl();
                var instancetitle = sessionKeys.PortfolioName;
                var fromemail = Deffinity.systemdefaults.GetFromEmail(sessionKeys.PortfolioID);


                string Body = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/WF/DC/EmailTemplates/PasswordSendingmailNew.html"));
                Body = Body.Replace("[border]", url + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                Body = Body.Replace("[Logo]", Deffinity.systemdefaults.GetWebUrl() + Deffinity.systemdefaults.GetMailLogo(sessionKeys.PortfolioID, Deffinity.systemdefaults.GetLocalPath()));
                if (Body.Contains("[name]"))
                {
                    Body = Body.Replace("[name]", con.ContractorName);
                }
                if (Body.Contains("[password]"))
                {
                    Body = Body.Replace("[password]", password);
                }
                if (Body.Contains("[Link]"))
                {
                    Body = Body.Replace("[Link]", url);
                }
                // Body = Body.Replace("[url]", Deffinity.systemdefaults.GetWebUrl());
                em.SendingMail(fromemail, instancetitle + " " + con.ContractorName + " Password", Body, con.EmailAddress, "");

            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
        }
        protected void btnAddOrganization_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/App/Member.aspx?type=" + QueryStringValues.Type, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                // pnlShowUpload.Visible = true;
                // mdlManageOptions.Show();
                mdlManageOptionsNew.Show();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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

        private void SaveMember(string firstname, string lastname, string emailaddress, string cellnumber, string permission, string tags)
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
                value.Password = Deffinity.Users.Login.GeneratePasswordString(pw);// FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1");
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

                var userDetails = cvRep.GetAll().Where(o => o.LoginName.ToLower() == value.LoginName.ToLower() && o.Status == UserStatus.Active).FirstOrDefault();
                if (userDetails == null)
                {
                    value.Company = "";
                    cRep.Add(value);
                }
                else
                {
                    cRep.Edit(value);
                }

                var cdRep = new UserRepository<UserMgt.Entity.UserDetail>();
                var cdEntity = new UserMgt.Entity.UserDetail();

                if (cdRep.GetAll().Where(o => o.UserId == value.ID).Count() == 0)
                {
                    cdEntity.Address1 = "";
                    cdEntity.Country = 190;
                    cdEntity.PostCode = "";
                    cdEntity.State = "";
                    //cdEntity.SubDenominationDetailsID = pDetails.SubDenominationDetailsID;
                    //cdEntity.DenominationDetailsID = pDetails.DenominationDetailsID;
                    cdEntity.Town = "";
                    cdEntity.UserId = value.ID;




                    cdRep.Add(cdEntity);
                }

                var urRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var urEntity = new UserMgt.Entity.UserToCompany();

                if (urRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID && o.UserID == value.ID).Count() == 0)
                {
                    urEntity.CompanyID = sessionKeys.PortfolioID;
                    urEntity.UserID = value.ID;
                    urRep.Add(urEntity);
                }


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
        //btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected static string GetImageUrl(string contactsId)
        {
            //return GetImageUrl(a_gId, a_oThumbSize, true);
            bool isOriginal = false;

            string img = string.Empty;

            string filepath = HttpContext.Current.Server.MapPath("~/WF/UploadData/Users/") + "user_" + contactsId.ToString() + ".png";

            if (System.IO.File.Exists(filepath))
            {
                if (isOriginal)
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                else
                    img = string.Format("~/WF/UploadData/Users/user_{0}.png", contactsId.ToString());
                // string navUrl = string.Format("DisplayUser.aspx?userid={0}", contactsId.ToString());
                //img = string.Format("<img src='{0}' />", imgurl);
            }
            else
            {
                img = "~/WF/UploadData/Users/ThumbNailsMedium/user_0.png";
                //img = string.Format("<img src='{0}'  width='50px' height='50px'  />", imgurl);
            }
            return img + "?r=" + DateTime.Now.TimeOfDay.Milliseconds.ToString();
            // +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadMembers();

                //string filename = Server.MapPath("~/WF/UploadData/Templates/Members_Template.xlsx");
                //System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
                //if (fileInfo.Exists)
                //{
                //    Response.Clear();
                //    var filenamenew = string.Format( "Members_Template_{0}.xlsx",DateTime.Now.Ticks);
                //    HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //    HttpContext.Current.Response.ContentType = "application/ms-excel";
                //    HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename="+ filenamenew);
                //    HttpContext.Current.Response.Flush();
                //    HttpContext.Current.Response.End();
                //}


            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        private string getFirstname(string name)
        {
            string retval = "";
            var tstr = name.Split(' ');
            if (tstr.Count() > 1)
            {
                retval = tstr[0];
            }
            return retval;
        }

        private string getLastname(string name)
        {
            string retval = "";
            var tstr = name.Split(' ');
            if (tstr.Count() > 1)
            {
                retval = tstr[1];
            }
            return retval;
        }

        private void DownloadMembers()
        {
            try
            {

                var tList = UserMgt.BAL.ContractorsBAL.Contractor_SelectAll_WithOutCompany().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();

                //   var ulist = UserMgt.BAL.ContractorsBAL.Contractor_SelectAllNew().ToList();
                Random generator = new Random();
                var rlist = (from t in tList
                                 //join c in ulist on t.LoggedByID equals c.ID
                                 //join tc in tclist on t.TithingID equals tc.ID
                             select new
                             {
                                 ID = t.ID,
                                 Firstname = getFirstname(t.ContractorName.Replace("  ", " ")),
                                 Lastname = getLastname(t.ContractorName.Replace("  ", " ")),
                                 Email = t.LoginName,
                                 CellNumber = t.ContactNumber,
                                 Tags = t.Tags,
                                 Permission = t.SID == 1 ? "Admin" : "Member"


                             }).ToList();

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Donors");



                // Title
                ws.Cell("A1").Value = "Donor List"; //+ string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
                ws.Cell("A2").Value = "First name";
                //ws.Cell("B2").Value = "Comapany";
                ws.Cell("B2").Value = "Last name";
                ws.Cell("C2").Value = "Email";
                ws.Cell("D2").Value = "Cell number";
                ws.Cell("E2").Value = "Permission (Admin/Donor)";
                //ws.Cell("G2").Value = "Site";
                //ws.Cell("H2").Value = "Department";
                ws.Cell("F2").Value = "Tags";
                int i = 3;
                foreach (var item in rlist)
                {
                    ws.Cell("A" + i.ToString()).Value = item.Firstname;
                    //ws.Cell("B" + i.ToString()).Value = item.ComapanyName;
                    ws.Cell("B" + i.ToString()).Value = item.Lastname;
                    ws.Cell("C" + i.ToString()).Value = item.Email;
                    ws.Cell("D" + i.ToString()).SetDataType(XLDataType.Text);
                    ws.Cell("D" + i.ToString()).Value = "(" + item.CellNumber.Trim() + ")";

                    ws.Cell("E" + i.ToString()).Value = item.Permission;

                    ws.Cell("F" + i.ToString()).Value = item.Tags;

                    i = i + 1;
                }

                // From worksheet
                var rngTable = ws.Range("A1:F2");

                var rngHeaders = rngTable.Range("A2:F2");
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;
                rngHeaders.Style.Fill.BackgroundColor = XLColor.LightGray;

                rngTable.Cell(1, 1).Style.Font.Bold = true;
                rngTable.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                rngTable.Cell(1, 1).Style.Font.FontSize = 15;
                rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkGray;
                rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rngTable.Row(1).Merge();

                ws.Columns(1, 9).AdjustToContents();

                string path = HttpContext.Current.Server.MapPath("~/WF/UploadData/DonorsReport");

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                var filename_download = string.Format("DonorsList_{0}.xlsx", DateTime.Now.Ticks);
                wb.SaveAs(path + "\\" + filename_download);

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path + "\\" + filename_download);
                if (fileInfo.Exists)
                {
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.WriteFile(fileInfo.FullName, true);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + filename_download);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();

                }
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
                    // fileUpload2.SaveAs(path + fileName);
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

                            string firstname = dataTable.Rows[i][0].ToString();
                            string lastname = dataTable.Rows[i][1].ToString();
                            string emailaddress = dataTable.Rows[i][2].ToString();
                            string cellnumber = dataTable.Rows[i][3].ToString();
                            string permission = dataTable.Rows[i][4].ToString();
                            string tags = dataTable.Rows[i][5].ToString();

                            // string state = dataTable.Rows[i][3].ToString();
                            //  string tags = dataTable.Rows[i][4].ToString().Replace('"', ' ').Trim();

                            SaveMember(firstname, lastname, emailaddress, cellnumber, permission, tags);

                            sessionKeys.Message = "Successfully uploaded";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            Response.Redirect("~/App/Members.aspx", false);
        }
    }
}