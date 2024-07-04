using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.ApplicationBlocks.Data;
using ProjectMgt.DAL;
using DC.BAL;

/// <summary>
/// Summary description for systemdefaults
/// </summary>
namespace Deffinity
    {
    public class systemdefaults
    {
        public static string GetCallPrefix()
        {
            return "";
        }
        public static string GetHomepage(string page)
        {
            string FromEmail = string.Empty;
            string retval = page;
            if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("localhost") || HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Contains("dev"))
            {
                if (page.ToLower().Contains("resourceschedular.aspx"))
                    retval = "~/WF/DC/Home.aspx";
            }
            return retval;
        }
        public static string GetFromEmail()
        {
            string FromEmail = string.Empty;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Application["FromEmail"] == null)
                {
                    using (projectTaskDataContext pd = new projectTaskDataContext())
                    {
                        HttpContext.Current.Application["FromEmail"] = (from p in pd.ProjectDefaults
                                                                        select p.FromEmail).FirstOrDefault();
                    }
                }

                FromEmail = HttpContext.Current.Application["FromEmail"].ToString();
            }
            else
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {

                    FromEmail = (from p in pd.ProjectDefaults
                                 select p.FromEmail).FirstOrDefault();
                }
            }
            return FromEmail;

        }

        public static string GetFromEmail(int portfolioID)
        {
            string FromEmail = string.Empty;

            if (HttpContext.Current.Application["FromEmail"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
                    {
                        var f = dc.AccessControlEmails.Where(o => o.CustomerID == portfolioID).FirstOrDefault();
                        if (f != null)
                        {
                            HttpContext.Current.Application["FromEmail"] = f.EmailAddress;
                        }
                        else
                            HttpContext.Current.Application["FromEmail"] = (from p in pd.ProjectDefaults
                                                                            select p.FromEmail).FirstOrDefault();
                    }
                }
            }
            FromEmail = HttpContext.Current.Application["FromEmail"].ToString();
            return FromEmail;

        }

        public static string GetLocalPath()
        {
            string localpath = string.Empty;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Application["localpath"] == null)
                {
                    using (projectTaskDataContext pd = new projectTaskDataContext())
                    {
                        HttpContext.Current.Application["localpath"] = (from p in pd.ProjectDefaults
                                                                        select p.LocalDocumentPath).FirstOrDefault();
                    }
                }
                localpath = HttpContext.Current.Application["localpath"].ToString();
            }
            return localpath;

        }
        public static string GetChatkey()
        {
            string chatkey = string.Empty;

            if (HttpContext.Current.Application["ChatKey"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {

                    var f = pd.ProjectDefaults.FirstOrDefault();
                    if (f != null)
                    {
                        HttpContext.Current.Application["ChatKey"] = f.Chat_API_key;

                    }

                }
            }

            chatkey = HttpContext.Current.Application["ChatKey"].ToString();
            return chatkey;

        }


        public static string GetChatSecret()
        {
            string ChatSecret = string.Empty;

            if (HttpContext.Current.Application["ChatSecret"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {

                    var f = pd.ProjectDefaults.FirstOrDefault();
                    if (f != null)
                    {
                        HttpContext.Current.Application["ChatSecret"] = f.Chat_API_Secret;

                    }

                }
            }

            ChatSecret = HttpContext.Current.Application["ChatSecret"].ToString();
            return ChatSecret;

        }



        public static string GetWebUrl()
        {
            string url = string.Empty;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Application["url"] == null)
                {
                    using (projectTaskDataContext pd = new projectTaskDataContext())
                    {
                        HttpContext.Current.Application["url"] = (from p in pd.ProjectDefaults
                                                                  select p.WebURL).FirstOrDefault();
                    }
                }
                url = HttpContext.Current.Application["url"].ToString();
            }
            else
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {

                    url = (from p in pd.ProjectDefaults
                           select p.WebURL).FirstOrDefault();
                }
            }
           
            return url;

        }
        public static string GetWebSiteName()
        {
            return GetWebUrl().Replace("https://", string.Empty).Replace("http://", string.Empty);
        }
        public static string GetFinanceDistributionEmail()
        {
            string FinanceDistributionEmail = string.Empty;

            FinanceDistributionEmail = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select FinanceDistributionEmail from projectdefaults").ToString();
            return FinanceDistributionEmail;
        }
        public static string GetInstanceTitle()
        {
            string InstanceTitle = string.Empty;

            if (HttpContext.Current.Application["InstanceTitle"] == null)
            {
                HttpContext.Current.Application["InstanceTitle"] = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select ApplicationName from projectdefaults").ToString();
            }
            //if (HttpContext.Current.Application["InstanceTitle"] == null)
            //{
            //    HttpContext.Current.Application["InstanceTitle"] = sessionKeys.PartnerName;
            //}
            InstanceTitle = HttpContext.Current.Application["InstanceTitle"].ToString();
            return InstanceTitle;
        }

        public static string GetInstanceTitle(int portfolioID)
        {
            string InstanceTitle = string.Empty;

            var pEntity = PortfolioMgt.BAL.ProjectPortfolioBAL.ProjectPortfolioBAL_SelectByID(portfolioID);
            if (pEntity != null)
                InstanceTitle = pEntity.PortFolio;
            else
                InstanceTitle = string.Empty;
            return InstanceTitle;
        }
        public static void GetCulture()
        {
            string culture = string.Empty;
            if (HttpContext.Current.Application["MyUICulture"] == null)
            {
                culture = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select Culture from projectdefaults").ToString();
                HttpContext.Current.Application["MyUICulture"] = culture;
                HttpContext.Current.Application["MyCulture"] = culture;
            }
        }
        //clear copy right text
        public static void UpdateCopyrightText()
        {
            HttpContext.Current.Application["copyrighttext"] = null;
        }
        public static string GetCopyrightText()
        {
            string copyrighttext = string.Empty;

            if (HttpContext.Current.Application["copyrighttext"] == null)
            {
                HttpContext.Current.Application["copyrighttext"] = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select copyrighttext from projectdefaults").ToString();
            }
            copyrighttext = HttpContext.Current.Application["copyrighttext"].ToString();
            return copyrighttext;
        }
        public static string GetDateformat()
        {

            return System.Configuration.ConfigurationManager.AppSettings["dateformat"];
        }
        public static string GetTimeformat()
        {

            return System.Configuration.ConfigurationManager.AppSettings["timeformat"];
        }
        public static string GetTimeformat12()
        {
            return System.Configuration.ConfigurationManager.AppSettings["timeformat12"];
        }
        public static string GetDateTimeformat()
        {

            return System.Configuration.ConfigurationManager.AppSettings["datetimeformat"];
        }
        public static string GetFullDateTimeformat()
        {
            return System.Configuration.ConfigurationManager.AppSettings["fulldatetimeformat"];
        }
        public static string GetStringDateformat()
        {

            return System.Configuration.ConfigurationManager.AppSettings["stringdateformat"];
        }
        public static string GetStringTimeformat()
        {

            return System.Configuration.ConfigurationManager.AppSettings["stringtimeformat"];
        }
        public static string GetStringTimeformat12()
        {

            return System.Configuration.ConfigurationManager.AppSettings["stringtimeformat12"];
        }
        public static string GetStringDateTimeformat()
        {

            return System.Configuration.ConfigurationManager.AppSettings["stringdatetimeformat"];
        }
        public static string GetCCEmailAddress()
        {
            return System.Configuration.ConfigurationManager.AppSettings["CCEmailAddress"];
        }
        public static string GetLocalHostUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings["LocalUrl"];
        }

        public static string GetLogoFolderPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Logofolderpath"];
        }

        public static string GetUsersFolderPath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Usersfolderpath"];
        }
        public static string GetCategoryfolderpath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Categoryfolderpath"];
        }

        public static string GetCopyLocation()
        {
            return System.Configuration.ConfigurationManager.AppSettings["copylocation"];
        }
        public static string GetMailLogo()
        {
            return Deffinity.PortfolioManager.Portfilio.setMailLogo();
        }
        public static string GetMailLogo(int portfolioid)
        {
            return Deffinity.PortfolioManager.Portfilio.setMailLogo(portfolioid);
        }
        public static string GetMailLogo(int portfolioid,string folderpath)
        {
            return Deffinity.PortfolioManager.Portfilio.setMailLogo(portfolioid, folderpath);
        }
        public static string GetCategoryName()
        {
            //Category

            if (HttpContext.Current.Session["FLSCategory"] == null)
            {
                var c = FLSFieldsConfigBAL.GetFLSFieldsConfigByDefaultName("Category", sessionKeys.PortfolioID);
                if (c != null)
                    HttpContext.Current.Session["FLSCategory"] = c.InstanceName;
                else
                    HttpContext.Current.Session["FLSCategory"] = "Category";
            }
            return HttpContext.Current.Session["FLSCategory"].ToString();
        }
        public static void ClearCategoryName()
        {
            //Category
            HttpContext.Current.Session["FLSCategory"] = null;
        }
        public static string GetSubCategoryName()
        {
            //Category
            if (HttpContext.Current.Session["FLSSubCategory"] == null)
            {
                var c = FLSFieldsConfigBAL.GetFLSFieldsConfigByDefaultName("Sub Category", sessionKeys.PortfolioID);
                if (c != null)
                    HttpContext.Current.Session["FLSSubCategory"] = c.InstanceName;
                else
                    HttpContext.Current.Session["FLSSubCategory"] = "Sub Category";
            }
            return HttpContext.Current.Session["FLSSubCategory"].ToString();
        }
        public static void ClearSubCategoryName()
        {
            //Sub Category
            HttpContext.Current.Session["FLSSubCategory"] = null;
        }

        public static string GetRequesterName()
        {
            //Category
            if (HttpContext.Current.Session["FLSRequesterName"] == null)
            {
                var c = FLSFieldsConfigBAL.GetFLSFieldsConfigByDefaultName("Requester Name", sessionKeys.PortfolioID);
                HttpContext.Current.Session["FLSRequesterName"] = c.InstanceName;
            }
            return HttpContext.Current.Session["FLSRequesterName"].ToString();
        }
        public static void ClearRequesterName()
        {
            //Sub Category
            HttpContext.Current.Session["FLSRequesterName"] = null;
        }

        public static string GetTypeofRequestName()
        {
            //Category
            if (HttpContext.Current.Session["FLSTypeofRequestName"] == null)
            {
                var c = FLSFieldsConfigBAL.GetFLSFieldsConfigByDefaultName("Type of Request", sessionKeys.PortfolioID);
                HttpContext.Current.Session["FLSTypeofRequestName"] = c.InstanceName;
            }
            return HttpContext.Current.Session["FLSTypeofRequestName"].ToString();
        }
        public static void ClearTypeofRequestName()
        {
            //Sub Category
            HttpContext.Current.Session["FLSTypeofRequestName"] = null;
        }


        public static string GetCoutryID()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["countryid"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["countryid"] = (from p in pd.ProjectDefaults
                                                                    select p.CountryID).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["countryid"].ToString();
            return url;

        }
        public static void ClearCoutryID()
        {
            HttpContext.Current.Session["countryid"] = null;
        }
        public static string GetCoutryName()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["countryname"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["countryname"] = (from p in pd.ProjectDefaults
                                                                      select p.CountryName).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["countryname"].ToString();
            return url;

        }
        public static void ClearCoutryName()
        {
            HttpContext.Current.Session["countryname"] = null;
        }
        public static string GetCityName()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["CityName"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["CityName"] = (from p in pd.ProjectDefaults
                                                                   select p.City_Display).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["CityName"].ToString();
            return url;

        }
        public static void ClearCityName()
        {
            HttpContext.Current.Session["CityName"] = null;
        }
        public static string GetStateName()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["StateName"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["StateName"] = (from p in pd.ProjectDefaults
                                                                    select p.State_Display).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["StateName"].ToString();
            return url;

        }
        public static void ClearStateName()
        {
            HttpContext.Current.Session["StateName"] = null;
        }
        public static string GetPostcode()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["Postcode"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["Postcode"] = (from p in pd.ProjectDefaults
                                                                   select p.Postcode).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["Postcode"].ToString();
            return url;

        }
        public static void ClearPostcode()
        {
            HttpContext.Current.Session["Postcode"] = null;
        }
        public static string GetCultureName()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["CultureName"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["CultureName"] = (from p in pd.ProjectDefaults
                                                                      select p.Culture).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["CultureName"].ToString();
            return url;

        }
        public static void ClearCultureName()
        {
            HttpContext.Current.Session["CultureName"] = null;
        }
        public static string GetCountryCode()
        {
            string url = string.Empty;

            if (HttpContext.Current.Application["CountryCode"] == null)
            {
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    HttpContext.Current.Application["CountryCode"] = (from p in pd.ProjectDefaults
                                                                      select p.Phonecode).FirstOrDefault();
                }
            }
            url = HttpContext.Current.Application["CountryCode"].ToString();
            return url;

        }
        public static void ClearCountryCode()
        {
            HttpContext.Current.Session["CountryCode"] = null;
        }


        public static string GetStripeSecreatKey()
        {
            string url = string.Empty;

            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                url = (from p in pd.ProjectDefaults
                       select p.Payment_Merchant_key).FirstOrDefault();
            }
           
            return url;

        }
        public static string GetPublicKey()
        {
            string url = string.Empty;

            using (projectTaskDataContext pd = new projectTaskDataContext())
            {
                url = (from p in pd.ProjectDefaults
                       select p.Payment_Merchant_ID).FirstOrDefault();
            }

            return url;

        }
    }
}

   
