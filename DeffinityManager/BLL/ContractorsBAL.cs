using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Security;
using UserMgt.DAL;
using UserMgt.Entity;

namespace UserMgt.BAL
{
    /// <summary>
    /// Summary description for ContractorsBAL
    /// </summary>rr
    /// 

    public class UserType
    {
        public const int SmartPros = 1;
        public const int SmartTechs = 4;
        public const int Vendor = 8;
        public const int Fundraiser = 3;
        public const int Donor = 2;
    }
    public class UserStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
    }
    public class ContractorStatus
    {
        public const string Active = "Active";
        public const string InActive = "InActive";
    }
    public class ContractorsBAL :IDisposable
    {



        IUserRepository<UserMgt.Entity.Contractor> ContractorRepository = null;
        public ContractorsBAL()
        {
            ContractorRepository = new UserRepository<UserMgt.Entity.Contractor>();
        }

        //Select All
        public static IQueryable<UserMgt.Entity.Contractor> Contractor_SelectAllNew()
        {
            IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
            return uRep.GetAll();
        }
        public static List<UserMgt.Entity.v_contractor> Contractor_SelectAll_WithOutCompany()
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().ToList();
        }
        public static List<UserMgt.Entity.v_contractor> Contractor_SelectAllActiveUsers(int PortfolioID)
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o =>  o.CompanyID == PortfolioID && o.Status == UserStatus.Active).ToList();
        }
        public static List<UserMgt.Entity.v_contractor> Contractor_SelectVendros()
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.SID == UserType.Vendor && o.CompanyID == sessionKeys.VendorID && o.Status == UserStatus.Active).ToList();
        }

        public static List<UserMgt.Entity.v_contractor> Contractor_SelectVendros(int PortfolioID)
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.SID == UserType.Vendor && o.CompanyID == PortfolioID && o.Status == UserStatus.Active).ToList();
        }
        public static List<UserMgt.Entity.Contractor> Contractor_SelectBySID(int sid)
        {
            IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
            return uRep.GetAll().Where(o => o.SID == sid).ToList();
        }

        public static List<UserMgt.Entity.v_contractor> Contractor_SelectServiceProviers()
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.SID == UserType.SmartTechs && o.CompanyID == sessionKeys.PortfolioID && o.Status == UserStatus.Active).ToList();
        }
        public static List<UserMgt.Entity.v_contractor> Contractor_SelectServiceProviers(int PortfolioID)
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.SID == UserType.SmartTechs && o.CompanyID == PortfolioID && o.Status == UserStatus.Active).ToList();
        }
        public static List<UserMgt.Entity.v_contractor> Contractor_SelectAdmins()
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.SID == UserType.SmartPros && o.CompanyID == sessionKeys.PortfolioID && o.Status == UserStatus.Active).ToList();
        }
        public static List<UserMgt.Entity.v_contractor> Contractor_SelectAdmins(int PortfolioID)
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.SID == UserType.SmartPros && o.CompanyID == PortfolioID && o.Status == UserStatus.Active).ToList();
        }
        public static UserMgt.Entity.Contractor Contractor_SelectByID(int UserID)
        {
            IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
            return uRep.GetAll().Where(o => o.ID == UserID).FirstOrDefault();
        }
        public static UserMgt.Entity.v_contractor v_Contractor_SelectByID(int UserID)
        {
            IUserRepository<UserMgt.Entity.v_contractor> uRep = new UserRepository<UserMgt.Entity.v_contractor>();
            return uRep.GetAll().Where(o => o.ID == UserID).FirstOrDefault();
        }
        public static UserMgt.Entity.Contractor Contractor_UpdateByStatus(int userID,string status)
        {
            IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
            var cc= uRep.GetAll().Where(o => o.ID == userID).FirstOrDefault();
            if(cc != null)
            {
                cc.ModifiedDate = DateTime.Now;
                cc.Status = status;
                uRep.Edit(cc);
            }
            return cc;
        }
        public static UserMgt.Entity.Contractor Contractor_UpdateByRestpassword(int userID, bool restpassword)
        {
            IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
            var cc = uRep.GetAll().Where(o => o.ID == userID).FirstOrDefault();
            if (cc != null)
            {
                cc.ModifiedDate = DateTime.Now;
                cc.ResetPassword = restpassword;
                uRep.Edit(cc);
            }
            return cc;
        }
        public static UserMgt.Entity.Contractor Contractor_UpdatePassword(int userID, string password)
        {
            IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
            var cc = uRep.GetAll().Where(o => o.ID == userID).FirstOrDefault();
            if (cc != null)
            {
                cc.ModifiedDate = DateTime.Now;
                cc.Password = Deffinity.Users.Login.GeneratePasswordString(password);// FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1") ;
                uRep.Edit(cc);
            }
            return cc;
        }
        public IEnumerable<UserMgt.Entity.Contractor> Contractor_SelectAll()
        {
            var ulist = GetUserListByCompany();
            return ContractorRepository.GetAll().Where(o=>ulist.Contains(o.ID));
        }
       
        //Select by userid
        public UserMgt.Entity.Contractor Contractor_Select_ByID(int userid)
        {
            return Contractor_SelectAll().Where(p => p.ID == userid).FirstOrDefault();
        }
        //Select Active users
        public IEnumerable<UserMgt.Entity.Contractor> Contractor_Select_Active()
        {
            return Contractor_SelectAll().Where(p => p.Status.ToLower() == "active");
        }
        //Select InActive users
        public IEnumerable<UserMgt.Entity.Contractor> Contractor_Select_InActive()
        {
            return Contractor_SelectAll().Where(p => p.Status.ToLower() == "inactive");
        }
        //select Active admin,PM , pm & QA (sid = 1,2,3) users 
        //Select InActive users
        public IEnumerable<UserMgt.Entity.Contractor> Contractor_Select_Admins()
        {
            var sid_array = new int?[] {1,2,3 };
            return Contractor_SelectAll().Where(p => p.Status.ToLower() == "active" && sid_array.Contains(p.SID));
        }
        //select Active resources (sid = 4) users 
        public IEnumerable<UserMgt.Entity.Contractor> Contractor_Select_Resources()
        {
            var sid_array = new int?[] { 4 };
            return Contractor_SelectAll().Where(p => p.Status.ToLower() == "active" && sid_array.Contains(p.SID));
        }
        //select Active customers (sid = 7) users 
        public IEnumerable<UserMgt.Entity.Contractor> Contractor_Select_Customers()
        {
            var sid_array = new int?[] { 7 };
            return Contractor_SelectAll().Where(p => p.Status.ToLower() == "active" && sid_array.Contains(p.SID));
        }
        public void Dispose()
        {
            if (ContractorRepository != null)
                ContractorRepository.Dispose();
        }

        public static List<int> GetUserListByCompany()
        {
            List<int> retval = new List<int>();
            try
            {
                //if (Session["CompanyID"] != null)
                //{
                IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var uEntity = uRep.GetAll().Where(o => o.CompanyID == sessionKeys.PortfolioID).ToList();
                if (uEntity.Count > 0)
                    retval = uEntity.Select(o => o.UserID).ToList();
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

        public static int GetUserCompany(int userid)
        {
            int retval = 0;
            try
            {
                //if (Session["CompanyID"] != null)
                //{
                IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var uEntity = uRep.GetAll().Where(o => o.UserID == userid).FirstOrDefault();
                if (uEntity !=null)
                    retval = uEntity.CompanyID;
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

        public static List<int> GetUserListByCompany(int PortfolioID)
        {
            List<int> retval = new List<int>();
            try
            {
                //if (Session["CompanyID"] != null)
                //{
                IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                var uEntity = uRep.GetAll().Where(o => o.CompanyID == PortfolioID).ToList();
                if (uEntity.Count > 0)
                    retval = uEntity.Select(o => o.UserID).ToList();
                //}
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retval;
        }

        public bool UserExits(string username)
        {
            var retval = false;
            var ExistingUserID = Contractor_SelectAll().Where(o => o.LoginName.ToLower() == username.ToLower()).FirstOrDefault();
            if (ExistingUserID != null)
            {
                retval = true;
            }
            return retval;
        }
        public int GetUserByName(string username="Internal")
        {
            var retval = 0;
            var ExistingUserID = Contractor_SelectAll().Where(o => o.LoginName.ToLower() == username.ToLower()).FirstOrDefault();
            if (ExistingUserID != null)
            {
                retval = ExistingUserID.ID;
            }
            return retval;
        }

        Database db = DatabaseFactory.CreateDatabase("DBstring");
        public void AddUsertoCompany(int userID)
        {
            try
            {
                if (sessionKeys.PortfolioID > 0)
                {
                    IUserRepository<UserMgt.Entity.UserToCompany> uRep = new UserRepository<UserMgt.Entity.UserToCompany>();
                    var uEntity = uRep.GetAll().Where(o => o.UserID == userID && o.CompanyID == sessionKeys.PortfolioID).FirstOrDefault();
                    if (uEntity == null)
                    {
                        uEntity = new UserMgt.Entity.UserToCompany();
                        uEntity.CompanyID = sessionKeys.PortfolioID;
                        uEntity.UserID = userID;
                        uRep.Add(uEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public static void UpdateUserDetails(int userID,string contractorname,string loginname,string email,string contactnumber)
        {
            try
            {

                IUserRepository<UserMgt.Entity.Contractor> uRep = new UserRepository<UserMgt.Entity.Contractor>();
                var uEntity = uRep.GetAll().Where(o => o.ID == userID).FirstOrDefault();
                if (uEntity != null)
                {
                    uEntity.ContractorName = contractorname;
                    uEntity.LoginName = loginname;
                    uEntity.EmailAddress = email;
                    uEntity.ContactNumber = contactnumber;
                    uRep.Add(uEntity);
                }
            }
            catch(Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        public static void UpdateUserChatDetails(int userID,string chat_id,string chat_key)
        {
            try
            {

                IUserRepository<UserMgt.Entity.UserDetail> uRep = new UserRepository<UserMgt.Entity.UserDetail>();
                var uEntity = uRep.GetAll().Where(o => o.UserId == userID).FirstOrDefault();
                if (uEntity == null)
                {
                    uEntity = new UserDetail();
                    uEntity.UserId = userID;
                    uEntity.Chat_ID = chat_id;
                    uEntity.Chat_Key = chat_key;

                    uRep.Add(uEntity);
                }
                else
                {
                    
                    uEntity.Chat_ID = chat_id;
                    uEntity.Chat_Key = chat_key;

                    uRep.Edit(uEntity);

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }

        public int UserInsert(string fullname, string loginname, string password, int permissionlevel=4, string status="Active", string details="",
       string email="", int timesheet_primary_approver=0, int timesheet_secondary_approver=0, string company="", string contactnumber="",
       string ReleaseDate = "", string EmploymentStartDate = "", int ExpClassification=0, int DepartmentID=0, int AreaID=0, int CasualLabourType =0, bool resetpassword=false)
        {
            DbCommand cmd = db.GetStoredProcCommand("DN_InsertUser");
            db.AddInParameter(cmd, "@ContractorName", DbType.String, fullname);
            db.AddInParameter(cmd, "@LoginName", DbType.String, loginname);
            db.AddInParameter(cmd, "@Password", DbType.String, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1"));
            db.AddInParameter(cmd, "@SID", DbType.Int32, permissionlevel);
            db.AddInParameter(cmd, "@Status", DbType.String, status);
            db.AddInParameter(cmd, "@Details", DbType.String, details);
            db.AddInParameter(cmd, "@EmailAddress", DbType.String, email);
            db.AddInParameter(cmd, "@TimeApproveID", DbType.Int32, timesheet_primary_approver);
            db.AddInParameter(cmd, "@SecondTSApprover", DbType.Double, timesheet_secondary_approver);
            db.AddInParameter(cmd, "@Company", DbType.String, company);
            db.AddInParameter(cmd, "@ContactNumber", DbType.String, contactnumber);
            db.AddInParameter(cmd, "@ReleaseDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(ReleaseDate) ? "01/01/1990" : ReleaseDate));
            db.AddInParameter(cmd, "@EmploymentStartDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(EmploymentStartDate) ? "01/01/1990" : EmploymentStartDate));
            db.AddInParameter(cmd, "@ExpClassification", DbType.Int32, ExpClassification);
            db.AddInParameter(cmd, "@DepartmentID", DbType.Int32, DepartmentID);
            db.AddInParameter(cmd, "@AreaID", DbType.Int32, AreaID);
            db.AddInParameter(cmd, "@CasualLabourType", DbType.Int32, CasualLabourType);
            db.AddInParameter(cmd, "@ResetPassword", DbType.Boolean, resetpassword);

            db.AddOutParameter(cmd, "@OutStatus", DbType.Int32, 4);
            db.AddOutParameter(cmd, "@OutID", DbType.Int32, 4);
            //@CompanyID
            db.AddInParameter(cmd, "@CompanyID", DbType.Int32, sessionKeys.PortfolioID);
            db.ExecuteNonQuery(cmd);
            //if getVal = 1 sucess 2 for already item exist
            int getVal = (int)db.GetParameterValue(cmd, "OutStatus");
            int OutID = (int)db.GetParameterValue(cmd, "OutID");
            cmd.Dispose();
            if (getVal == 1)
            {


                if (OutID > 0)
                {
                    // update the userdetails
                   // UserDetails_update(OutID, chk_disable_customerportal.Checked);
                    //set values
                    //SelectUserData(OutID);
                    //set userdetails
                    //Set_UserDetails(OutID);

                    AddUsertoCompany(OutID);
                }
                //change visibility
                //clearNamefromName();
                //defaultBindings();
               
            }
            else if (getVal == 2)
            {
                //lblError1.Text = "Sorry but the username is already in use by another user.";
            }

           // RequiredFieldValidator2.ErrorMessage = "";
            return OutID;
        }
        //private void UserUpdate(int ID, string fullname, string loginname, string password, int permissionlevel, string status, string details,
        //    string email, int timesheet_primary_approver, int timesheet_secondary_approver, string company, string contactnumber,
        //    string ReleaseDate, string EmploymentStartDate, int ExpClassification, int DepartmentID, int AreaID, int CasualLabourType, bool resetpassword)
        //{

        //    int s_val = Convert.ToInt32(drContractors.SelectedValue);
        //    //update data
        //    DbCommand cmd = db.GetStoredProcCommand("DN_UpdateUser");
        //    db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
        //    db.AddInParameter(cmd, "@ContractorName", DbType.String, fullname);
        //    db.AddInParameter(cmd, "@LoginName", DbType.String, loginname);
        //    if (txtPassword.Text.Trim() == "")
        //    {
        //        db.AddInParameter(cmd, "@Password", DbType.String, password);
        //    }
        //    else
        //    {
        //        db.AddInParameter(cmd, "@Password", DbType.String, FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1"));
        //    }
        //    db.AddInParameter(cmd, "@SID", DbType.Int32, permissionlevel);
        //    db.AddInParameter(cmd, "@Status", DbType.String, status);
        //    db.AddInParameter(cmd, "@Details", DbType.String, details);
        //    db.AddInParameter(cmd, "@EmailAddress", DbType.String, email);
        //    db.AddInParameter(cmd, "@TimeApproveID", DbType.Int32, timesheet_primary_approver);
        //    db.AddInParameter(cmd, "@SecondTSApprover", DbType.Double, timesheet_secondary_approver);
        //    db.AddInParameter(cmd, "@Company", DbType.String, company);
        //    db.AddInParameter(cmd, "@ContactNumber", DbType.String, contactnumber);
        //    db.AddInParameter(cmd, "@ReleaseDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(ReleaseDate) ? "01/01/1990" : ReleaseDate));
        //    db.AddInParameter(cmd, "@EmploymentStartDate", DbType.DateTime, Convert.ToDateTime(string.IsNullOrEmpty(EmploymentStartDate) ? "01/01/1990" : EmploymentStartDate));
        //    db.AddInParameter(cmd, "@ExpClassification", DbType.Int32, ExpClassification);
        //    db.AddInParameter(cmd, "@CasualLabourType", DbType.Int32, CasualLabourType);
        //    db.AddInParameter(cmd, "@DepartmentID", DbType.Int32, DepartmentID);
        //    db.AddInParameter(cmd, "@AreaID", DbType.Int32, AreaID);
        //    db.AddInParameter(cmd, "@ResetPassword", DbType.Boolean, resetpassword);
        //    db.AddOutParameter(cmd, "@OutStatus", DbType.Int32, 4);
        //    db.ExecuteNonQuery(cmd);
        //    //if getVal = 1 sucess 2 for already item exist
        //    int getVal = (int)db.GetParameterValue(cmd, "OutStatus");
        //    cmd.Dispose();
        //    if (getVal == 1)
        //    {
        //        // update the userdetails
        //        UserDetails_update(ID, chk_disable_customerportal.Checked);

        //        lblMsg1.Text = "User details updated successfully";
        //        //lblError1.ForeColor = System.Drawing.Color.Green;
        //        ChangeUpdateVisible();
        //        emailblank.ErrorMessage = string.Empty;
        //    }
        //    else if (getVal == 2)
        //    {
        //        lblError1.Text = "Sorry but the username is already in use by another user.";
        //    }

        //    RequiredFieldValidator1.ErrorMessage = "";

        //    //reload dropdown and select existing data
        //    //getData.DdlBindSelect(drContractors, "DN_ResourcesList", "ID", "ContractorName", true, true);
        //    //drContractors.SelectedItem.Text = txtEditName.Text;
        //    //BindUsers(int.Parse(drpermission.SelectedValue));
        //    if (Request.QueryString["sid"] != null)
        //    {
        //        if (Request.QueryString["sid"] == "10")
        //        {
        //            drpermission.SelectedValue = "10";
        //            drpermission.Enabled = false;
        //            GetID.Visible = true;
        //            txtEmail.Visible = false;
        //            txtCompany.Visible = false;
        //            GetUser.Visible = false;
        //            //ddlCasual_Labour.SelectedValue = "3";
        //            BindUsers(int.Parse(drpermission.SelectedValue));
        //        }
        //        else if (Request.QueryString["sid"] == "7")
        //        {
        //            drpermission.SelectedValue = "7";
        //            drpermission.Enabled = false;
        //            BindUsers(int.Parse(drpermission.SelectedValue));
        //        }
        //        else
        //        {
        //            getData.DdlBindSelect(drContractors, "select * from Contractors where Status='ACTIVE' and sid not in (-99,7,10,8) and ID in (select UserID from UserToCompany where CompanyID = " + sessionKeys.PortfolioID + ") order by ContractorName", "ID", "ContractorName", false, false, true);
        //        }
        //    }
        //    //SelectUserData(s_val);

        //    //set userdetails
        //    //Set_UserDetails(ID);

        //    //edit panel
        //    //PanleEditName.Visible = false;


        //}

    }
}