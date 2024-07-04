using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Can set or get the values for the session variable forgotting about casting..
/// </summary>
public class sessionKeys : Page
{

    #region SessionFields
    private const string jobDisplayName = "jobDisplayName";
    private const string isOrganization = "IsOrganization";
    private const string isGroup = "IsGroup";
    private const string isService = "IsService";
    private const string contractorId = "contractorId";
    private const string project = "project";
    private const string ac2pId = "ac2pid";
    private const string sId = "SID";
    private const string uId = "UID";
    private const string uName = "Uname";
    private const string uEmail = "Uemail";
    
    private const string prefix = "Prefix";
    private const string message = "msg";
    private const string errormessage = "errormsg";
    private const string portfolioID = "PortfolioID";
    private const string portfolioName = "PortfolioName";
    private const string fundportfolioName = "PortfolioName";
    
    private const string fundportfolioID = "FundPortfolioID";
    private const string programmeID = "ProgrammeID";
    private const string programmeName = "ProgrammeName";
    private const string changeControlID = "ChangeControlID";
    private const string incidentID = "IncidentID";
    private const string cartID = "CartID";
    private const string portfolioHealthCheckID = "PortfolioHealthCheckID";
    private const string isCustomer="IsCustomer";
    private const string ExistCustomer = "customer";
    private const string Vendor = "Vendor";
    private const string Portaluser = "Portaluser";
    private const string CatalogueType = "CatalogueType";
    private const string page = "page";
    private const string RequesterInsertedId = "RInsertedId";
    private const string CTid = "CTID";
    private const string modules = "Module";
    private const string companymodules = "CompanyModules";
    private const string companyaccess = "CompanyAccess";
    private const string partnerID = "PartnerID";
    private const string partnerName = "PartnerName";
    private const string partnerTheme = "PartnerTheme";
    private const string payStatus = "PayStatus";
    private const string culture = "culture";
    private const string uiculture = "uiculture";

    private const string dateformat = "dateformat";
    private const string timeformat = "timeformat";

    private const string currency = "currency";

    #endregion
    public static string JobDisplayName
    {
        get
        {

          if(sessionKeys.IsService)
                return "Project";
          else
                return "Project";
            //if (sessionKeys.IsOrganization)
            //    if (sessionKeys.SID == 3)
            //        return "Job";
            //    else
            //        return "Event";
            //else
            //    if (sessionKeys.SID == 3)
            //    return "Job";
            //else
            //    return "Job";
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.jobDisplayName] = value;
        }
    }

    public static string JobsDisplayName
    {
        get
        {

            if (sessionKeys.IsService)
                return "Projects";
            else
                return "Projects";
            //if (sessionKeys.IsOrganization)
            //    if (sessionKeys.SID == 3)
            //        return "Jobs";
            //    else
            //        return "Events";
            //else
            //    if (sessionKeys.SID == 3)
            //    return "Job";
            //else
            //    return "Jobs";
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.jobDisplayName] = value;
        }
    }
    public static string Currency
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.currency] != null)
            {
                return HttpContext.Current.Session[sessionKeys.currency].ToString();
            }
            else
            {
                return "£";
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.currency] = value;
        }
    }
    public static string DateFormat
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.dateformat] != null)
            {
                return HttpContext.Current.Session[sessionKeys.dateformat].ToString();
            }
            else
            {
                return "dd/MM/yyyy";
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.dateformat] = value;
        }
    }
    public static string TimeFormat
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.timeformat] != null)
            {
                return HttpContext.Current.Session[sessionKeys.timeformat].ToString();
            }
            else
            {
                return "HH:mm";
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.timeformat] = value;
        }
    }
    public static string LanCulture
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.culture] != null)
            {
                return HttpContext.Current.Session[sessionKeys.culture].ToString();
            }
            else
            {
                return "en-GB";
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.culture] = value;
        }
    }
    public static string LanUIculture
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.uiculture] != null)
            {
                return HttpContext.Current.Session[sessionKeys.uiculture].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.uiculture] = value;
        }
    }

    public static bool PayStatus
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.payStatus] != null)
            {
                if (Convert.ToBoolean(HttpContext.Current.Session[sessionKeys.payStatus]) == false)
                    return false;
                else
                    return true;
            }
            return true;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.payStatus] = value;
        }
    }

    public static bool IsOrganization
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.isOrganization] != null)
            {
                if (Convert.ToBoolean(HttpContext.Current.Session[sessionKeys.isOrganization]) == false)
                    return false;
                else
                    return true;
            }
            return true;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.isOrganization] = value;
        }
    }

    public static bool IsGroup
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.isGroup] != null)
            {
                if (Convert.ToBoolean(HttpContext.Current.Session[sessionKeys.isGroup]) == false)
                    return false;
                else
                    return true;
            }
            return true;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.isGroup] = value;
        }
    }

    public static bool IsService
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.isService] != null)
            {
                if (Convert.ToBoolean(HttpContext.Current.Session[sessionKeys.isService]) == false)
                    return false;
                else
                    return true;
            }
            return true;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.isService] = value;
        }
    }

    public static List<PortfolioMgt.Entity.PortfolioModule> Modules
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.modules] != null)
            {
                return HttpContext.Current.Session[sessionKeys.modules] as List<PortfolioMgt.Entity.PortfolioModule>;
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.modules] = value;
        }
    }

    public static List<PortfolioMgt.Entity.PortfolioBillingModule> CompanyModules
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.companymodules] != null)
            {
                return HttpContext.Current.Session[sessionKeys.companymodules] as List<PortfolioMgt.Entity.PortfolioBillingModule>;
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.companymodules] = value;
        }
    }

    public static PortfolioMgt.Entity.PortfolioBillingManager CompanyAccess
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.companyaccess] != null)
            {
                return HttpContext.Current.Session[sessionKeys.companyaccess] as PortfolioMgt.Entity.PortfolioBillingManager;
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.companyaccess] = value;
        }
    }

    public static string RInsertedId
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.RequesterInsertedId] != null)
            {
                return HttpContext.Current.Session[sessionKeys.RequesterInsertedId].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.RequesterInsertedId] = value;
        }
    }
    public static string Customer
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.ExistCustomer] != null)
                return HttpContext.Current.Session[sessionKeys.ExistCustomer].ToString();
            else
                return string.Empty;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.ExistCustomer] = value;
        }
    }
    public static bool IsCustomer
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.isCustomer] != null)
            {
                if (Convert.ToBoolean(HttpContext.Current.Session[sessionKeys.isCustomer]) == false)
                    return false;
                else
                    return true;
            }
            return true;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.isCustomer] = value;
        }
    }

    public static int CTID
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.CTID] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.CTID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.CTID] = value;
        }
    }
    public static int IncidentID
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.incidentID] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.incidentID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.incidentID] = value;
        }
    }
    public static int VendorID
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.Vendor] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.Vendor]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.Vendor] = value;
        }
    }
    public static int pageid
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.page] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.page]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.page] = value;
        }
    }


    public static int PortfolioHealthCheckID
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.portfolioHealthCheckID] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.PortfolioHealthCheckID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.PortfolioHealthCheckID] = value;
        }
    }

    /// <summary>
    /// Gets or Sets the programme Name from the session.
    /// </summary>

    public static string ProgrammeName
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.programmeName] != null)
                return HttpContext.Current.Session[sessionKeys.programmeName].ToString();
            else
                return string.Empty;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.programmeName] = value;
        }
    }
    /// <summary>
    /// Gets or Sets the programme ID from the session.
    /// </summary>

    public static int ProgrammeID
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.programmeID] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.programmeID]);
            else
                return 0;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.programmeID] = value;
        }
    }
    
    /// <summary>
    /// Gets or Sets the portfolio ID from the session.
    /// </summary>
    public static int PortfolioID
    {
        get
        {
            if 
                (
                HttpContext.Current.Session[sessionKeys.portfolioID] 
                != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.portfolioID]);
            else
                return 0;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.portfolioID] = value;
        }
    }

    public static int FundPortfolioID
    {
        get
        {
            if
                (
                HttpContext.Current.Session[sessionKeys.fundportfolioID]
                != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.fundportfolioID]);
            else
                return 0;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.fundportfolioID] = value;
        }
    }

    /// <summary>
    /// Gets or Sets the Portfolio Name from the session.
    /// </summary>

    public static string PortfolioName
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.portfolioName] != null)
                return HttpContext.Current.Session[sessionKeys.portfolioName].ToString();
            else
                return string.Empty;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.portfolioName] = value;
        }
    }

    public static string FundPortfolioName
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.fundportfolioName] != null)
                return HttpContext.Current.Session[sessionKeys.fundportfolioName].ToString();
            else
                return string.Empty;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.fundportfolioName] = value;
        }
    }


    /// <summary>
    /// Gets or Sets the portfolio ID from the session.
    /// </summary>
    public static int PartnerID
    {
        get
        {
            if
                (
                HttpContext.Current.Session[sessionKeys.partnerID]
                != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.partnerID]);
            else
                return 0;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.partnerID] = value;
        }
    }

    /// <summary>
    /// Gets or Sets the Portfolio Name from the session.
    /// </summary>

    public static string PartnerName
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.partnerName] != null)
                return HttpContext.Current.Session[sessionKeys.partnerName].ToString();
            else
                return string.Empty;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.partnerName] = value;
        }
    }

    public static string PartnerTheme
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.partnerTheme] != null)
                return HttpContext.Current.Session[sessionKeys.partnerTheme].ToString();
            else
                return string.Empty;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.partnerTheme] = value;
        }
    }



    ///<summary>
    ///Gets the query string from the message.
    /// </summary>
    /// 
    public static string Message
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.message] != null)
                return HttpContext.Current.Session[sessionKeys.message].ToString();
            else
                return string.Empty;
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.message] = value;
        }
    }

    public static string ErrorMessage
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.errormessage] != null)
                return HttpContext.Current.Session[sessionKeys.errormessage].ToString();
            else
                return string.Empty;

        }
        set
        {
            HttpContext.Current.Session[sessionKeys.errormessage] = value;
        }
    }

    ///<summary>
    ///Gets or sets the SID to the session variable..
    ///Use with integer value or else compiler will raise an error.
    ///<summary>

    public static int SID
    {
        get
        {
            try
            {
                int returnSIDValue;
                if (HttpContext.Current.Session[sessionKeys.sId] == null)
                    returnSIDValue = 0;
                else
                    returnSIDValue = Convert.ToInt32(HttpContext.Current.Session[sessionKeys.sId]);
                return returnSIDValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.sId] = value;
        }
    }

    /// <summary>
    /// Gets or sets the UID to the session variable..
    /// Use with integer variables or else compiler will raise an error.
    /// </summary>

    public static int UID
    {
        get
        {
            try
            {
                int returnUIDValue;
                if (HttpContext.Current.Session[sessionKeys.uId] == null)
                    returnUIDValue = 0;
                else
                    returnUIDValue = Convert.ToInt32(HttpContext.Current.Session[sessionKeys.uId]);
                return returnUIDValue;
            }
            catch (FormatException)
            { 
                throw  new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.uId] = value;
        }
    }

    /// <summary>
    /// Gets or sets the User Name to the session variable..
    /// Use with string variables or else compiler will raise an error.
    /// </summary>

    public static string UName
    {
        get
        {
           string returnUNameValue;
            try
            {
                if (HttpContext.Current.Session[sessionKeys.uName] == null)
                    returnUNameValue = "";
                else
                    returnUNameValue = HttpContext.Current.Session[sessionKeys.uName].ToString();
                return returnUNameValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.uName] = value;
        }
    }
    
    public static string UEmail
    {
        get
        {
            string returnUNameValue;
            try
            {
                if (HttpContext.Current.Session[sessionKeys.uEmail] == null)
                    returnUNameValue = "";
                else
                    returnUNameValue = HttpContext.Current.Session[sessionKeys.uEmail].ToString();
                return returnUNameValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.uEmail] = value;
        }
    }

    /// <summary>
    /// Gets or sets the Prefix value to the session variable..
    /// Use with string variables or else compiler will raise an error. 
    /// </summary>

    public static string Prefix
    {
        get
        {
            string returnPrefixValue;
            try
            {
                if (HttpContext.Current.Session[sessionKeys.prefix] == null)
                    returnPrefixValue = "";
                else
                    returnPrefixValue = HttpContext.Current.Session[sessionKeys.prefix].ToString();
                return returnPrefixValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.prefix] = value;
        }
    }


    /// <summary>
    /// Gets or sets the contractor id to the session variable..
    /// Use with integer variables or else compiler will raise an error.
    /// </summary>

    public static int ContractorId
    {
        get
        {
            int returnContractorIdValue;
            try
            {
                if (HttpContext.Current.Request.QueryString[sessionKeys.contractorId] == null)
                {
                    if (HttpContext.Current.Session[sessionKeys.contractorId] == null)
                        returnContractorIdValue = 0;
                    else
                        returnContractorIdValue = Convert.ToInt32(HttpContext.Current.Session[sessionKeys.contractorId].ToString());
                }
                else
                {
                    returnContractorIdValue = Convert.ToInt32(HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.QueryString[sessionKeys.contractorId].ToString()));
                    HttpContext.Current.Session[sessionKeys.contractorId] = (object)returnContractorIdValue;
                }
                return returnContractorIdValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.contractorId] = value;
        }
    }



    /// <summary>
    ///  Gets or sets the Project Reference to the session variable..
    /// Use with integer variables or else compiler will raise an error.
    /// </summary>

    public static int Project
    {
        get
        {
            int returnProjectReference;
            try
            {
                if (HttpContext.Current.Request.QueryString[sessionKeys.project] == null)
                {
                    if (HttpContext.Current.Session[sessionKeys.project] == null)
                        returnProjectReference = 0;
                    else
                        returnProjectReference = Convert.ToInt32(HttpContext.Current.Session[sessionKeys.project]);
                }
                else
                {
                    returnProjectReference = Convert.ToInt32(HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.QueryString[sessionKeys.project].ToString()));
                    HttpContext.Current.Session[sessionKeys.project] = (object)returnProjectReference;
                }
                return returnProjectReference;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.project] = value;
        }
    }


    /// <summary>
    /// Gets or sets the AC2PID (Assigned Contractors To Project ID) to the session variable..
    /// Use with integer variables or else compiler will raise an error.
    /// </summary>
    public static int Ac2pid
    {
        get
        {
            int returnAc2pidValue;
            try
            {
                if (HttpContext.Current.Request.QueryString[sessionKeys.ac2pId] == null)
                {
                    if (HttpContext.Current.Session[sessionKeys.ac2pId] == null)
                        returnAc2pidValue = 0;
                    else
                        returnAc2pidValue = Convert.ToInt32(HttpContext.Current.Session[sessionKeys.ac2pId]);
                }
                else
                {
                    returnAc2pidValue = Convert.ToInt32(HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.QueryString[sessionKeys.ac2pId].ToString()));
                    HttpContext.Current.Session[sessionKeys.ac2pId] = (object)returnAc2pidValue;
                }

                return returnAc2pidValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }
        set
        {
            HttpContext.Current.Session[sessionKeys.ac2pId] = value;
        }
    }

    public static int ChangeControlID
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.changeControlID] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.changeControlID]);
            else
                return 0;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.changeControlID] = value;
        }
    }
    public static string CartID
    {
        get
        {
            if
                (HttpContext.Current.Session[sessionKeys.cartID] != null)
                return (string)HttpContext.Current.Session[sessionKeys.cartID];
            else
                return Guid.Empty.ToString();
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.cartID] = value;
        }
    }
    public static bool PortalUser
    {
        get
        {
            bool returnPortalUserValue;
            try
            {
                if (HttpContext.Current.Session[sessionKeys.Portaluser] == null)
                    returnPortalUserValue = false;
                else
                    returnPortalUserValue = bool.Parse(HttpContext.Current.Session[sessionKeys.Portaluser].ToString());
                return returnPortalUserValue;
            }
            catch (FormatException)
            {
                throw new queryStringException("Please check the data in the query string values.");
            }
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.Portaluser] = value;
        }
    }

    public static int Cataloguetype
    {
        get
        {
            if (HttpContext.Current.Session[sessionKeys.CatalogueType] != null)
                return Convert.ToInt32(HttpContext.Current.Session[sessionKeys.CatalogueType]);
            else
                return 0;
        }

        set
        {
            HttpContext.Current.Session[sessionKeys.CatalogueType] = value;
        }
    }
}
