using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Web;
using System.Web.Security;
using DeffinityManager.DAL.UserManagementTableAdapters;
using DeffinityManager.DAL;
using System.Security.Cryptography;
using System.Text;
using Deffinity.PortfolioManager;
/// <summary>
/// Summary description for Login
/// </summary>
namespace Deffinity.Users
{
    public class Login
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>0 is login fails and 1 is success</returns>
        public static int LoginUser(string UserName, string Password)
        {
            //Select @sid as SID,@cid as CID,@cname as cname,@Prefix as Prefix,@cnt as Outval
            int retval = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Login", new SqlParameter("@LoginName", UserName), new SqlParameter("@Password", GeneratePasswordString(Password)), new SqlParameter("@PartnerID", sessionKeys.PartnerID.ToString()));
            while (dr.Read())
            {
                retval = int.Parse(dr["Outval"].ToString());
                if (retval > 0)
                {
                    HttpContext.Current.Session["SID"] = dr["SID"].ToString();
                    HttpContext.Current.Session["UID"] = dr["CID"].ToString();
                    HttpContext.Current.Session["Uname"] = dr["cname"].ToString();
                    HttpContext.Current.Session["UEmail"] = dr["useremail"].ToString();
                    HttpContext.Current.Session["Prefix"] = dr["Prefix"].ToString();
                    HttpContext.Current.Session["PortfolioID"] = dr["PortfolioID"].ToString();

                    sessionKeys.UID = int.Parse(dr["CID"].ToString());
                    sessionKeys.UName = dr["cname"].ToString();
                    sessionKeys.Prefix = dr["Prefix"].ToString();
                    sessionKeys.SID = int.Parse(dr["SID"].ToString());
                    sessionKeys.PortfolioID = int.Parse(dr["PortfolioID"].ToString());
                    sessionKeys.UEmail = dr["useremail"].ToString();

                }
            }
            dr.Close();
            dr.Dispose();

            //update org ID if not exists

            try
            {
               var portfolio= PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                if (portfolio != null)
                {
                    if(portfolio.OrgarnizationGUID != null)
                    PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(portfolio.OrgarnizationGUID);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;

        }


        public static int LoginUser_withDecript(string UserName, string Password)
        {
            //Select @sid as SID,@cid as CID,@cname as cname,@Prefix as Prefix,@cnt as Outval
            int retval = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_Login", new SqlParameter("@LoginName", UserName), new SqlParameter("@Password", Password));
            while (dr.Read())
            {
                retval = int.Parse(dr["Outval"].ToString());
                if (retval > 0)
                {
                    HttpContext.Current.Session["SID"] = dr["SID"].ToString();
                    HttpContext.Current.Session["UID"] = dr["CID"].ToString();
                    HttpContext.Current.Session["UEmail"] = dr["useremail"].ToString();
                    HttpContext.Current.Session["Uname"] = dr["cname"].ToString();
                    HttpContext.Current.Session["Prefix"] = dr["Prefix"].ToString();
                    HttpContext.Current.Session["PortfolioID"] = dr["PortfolioID"].ToString();

                    sessionKeys.UID = int.Parse(dr["CID"].ToString());
                    sessionKeys.UName = dr["cname"].ToString();
                    sessionKeys.Prefix = dr["Prefix"].ToString();
                    sessionKeys.SID = int.Parse(dr["SID"].ToString());
                    sessionKeys.PortfolioID = int.Parse(dr["PortfolioID"].ToString());
                    sessionKeys.UEmail = dr["useremail"].ToString();



                }
            }
            dr.Close();
            dr.Dispose();

            try
            {
                var portfolio = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(sessionKeys.PortfolioID);
                if (portfolio != null)
                {
                    if (portfolio.OrgarnizationGUID != null)
                        PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_UpdateOrgID(portfolio.OrgarnizationGUID);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

            return retval;

        }

        public static bool IsFirstLogged(int userid)
        {
            bool isFirstlogin = false;
            string a_firstlog = string.Empty;
            try
            {
                a_firstlog = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select isnull(isFirstlogin,0) from Contractors where ID = @userid", new SqlParameter("@userid", userid)).ToString();
                isFirstlogin =(a_firstlog =="0" ? false:true);
            }
            catch (Exception ex)
            { LogExceptions.WriteExceptionLog(ex); }
            return isFirstlogin;
        }
        public static void IsFirstLogged_update(int userid,int islogged)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update Contractors set isFirstlogin = @islogged where ID = @userid", new SqlParameter("@userid", userid), new SqlParameter("@islogged", islogged));
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        #region change password
        private static Contractors_SelectAllTableAdapter _Adacontractor;

        public static Contractors_SelectAllTableAdapter Adacontractor
        {
            get
            {
                if (_Adacontractor == null)
                    _Adacontractor = new Contractors_SelectAllTableAdapter();
                return _Adacontractor;

            }
        }
        public object LoginUser_ChangePassword(string Password, int UID)
        {
            object retVal = null;
            try
            {
                Adacontractor.ChangePassword(UID, Deffinity.Users.Login.GeneratePasswordString(Password.Trim())); //// FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1"));
                retVal = 1;
            }
            catch
            {

            }
            return retVal;
        }
        public object Old_Password(int UID, string Password)
        {
            object retVal = null;
            try
            {
                retVal = Adacontractor.OldPassword(UID, Deffinity.Users.Login.GeneratePasswordString(Password.Trim()));// //FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1"));

            }
            catch
            {

            }
            return retVal;
        }

        #endregion


        public static string GeneratePasswordString(string inputString)
        {
            var pval = FormsAuthentication.HashPasswordForStoringInConfigFile(inputString, "SHA1");
            var aStr = "salt";

            return GenerateSHA256String(aStr + pval);
        }
        public static string GenerateSHA1to256String(string SHA1String)
        {
            var pval = SHA1String;
            var aStr = "salt";

            return GenerateSHA256String(aStr + pval);
        }
        public static string GenerateSHA1String(string inputString)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(inputString, "SHA1");
        }
        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }

}
