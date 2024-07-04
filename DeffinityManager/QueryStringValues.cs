using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for QueryStringValues
/// </summary>

public class QueryStringValues
{
    private const string _Project = "project";
    private const string _AC2PID = "ac2pid";
    private const string _ID = "id";
    private const string _type = "type";
    private const string _ForumID = "forumid";
    private const string _Status = "status";
    private const string _Meeting = "meeting";
    private const string _ProjectPlanID = "projectplanid";
    private const string _TaskID = "taskid";
    private const string _RiskID = "riskid";
    private const string _IssueID = "issueid";
    private const string _ContractorID = "contractorid";
    private const string _VendorID = "vendorid";
    private const string _SDID = "sdid";
    private const string _HealthCheckId = "healthcheckid";
    private const string _Callid = "callid";
    private const string _CCid = "ccid";
    private const string _ivref= "ivref";
    private const string _Option = "option";
    private const string _CTid = "CTID";
    private const string _CSid = "CSID";
    
    private const string _planid = "planid";
    private const string _contactid = "contactid";
    private const string _addressid = "addressid";
    private const string _eqid = "eqid";
    private const string _orgid = "orgid";
    private const string _mid = "mid";
    private const string _eid = "eid";
    private const string _unid = "unid";
    private const string _munid = "munid";
    private const string _eventunid = "eventunid";

    public static int OrgID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._orgid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._orgid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._orgid] = value.ToString();
        }
    }
    public static int MID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._mid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._mid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._mid] = value.ToString();
        }
    }
    public static int EID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._eid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._eid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._eid] = value.ToString();
        }
    }
    public static int PlanID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._planid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._planid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._planid] = value.ToString();
        }
    }
    public static int ContactID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._contactid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._contactid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._contactid] = value.ToString();
        }
    }
    public static int AddressID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._addressid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._addressid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._addressid] = value.ToString();
        }
    }
    public static int EQID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._eqid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._eqid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._eqid] = value.ToString();
        }
    }
    public static int Project
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._Project] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._Project]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._Project] = value.ToString();
        }
    }
    public static int  ForumID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._ForumID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._ForumID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._ForumID] = value.ToString();
        }
    }
    public static int ProjectStatusID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._Status] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._Status]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._Status] = value.ToString();
        }
    }
    public static int AC2PID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._AC2PID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._AC2PID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._AC2PID] = value.ToString();
        }
    }
    public static int Meeting
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._Meeting] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._Meeting]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._Meeting] = value.ToString();
        }
    }
    public static int ProjectPlanID
     {
         get
         {
             if (HttpContext.Current.Request.QueryString[QueryStringValues._ProjectPlanID] != null)
                 return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._ProjectPlanID]);
             else
                 return 0;
         }
         set
         {
             HttpContext.Current.Request.QueryString[QueryStringValues._ProjectPlanID] = value.ToString();
         }
     }
    public static string Type
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._type] != null)
                return HttpContext.Current.Request.QueryString[QueryStringValues._type];
            else
                return string.Empty;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._type] = value.ToString();
        }
    }

    public static string MUNID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._munid] != null)
                return HttpContext.Current.Request.QueryString[QueryStringValues._munid];
            else
                return string.Empty;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._munid] = value.ToString();
        }
    }
    public static string UNID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._unid] != null)
                return HttpContext.Current.Request.QueryString[QueryStringValues._unid];
            else
                return string.Empty;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._unid] = value.ToString();
        }
    }

    public static string EVENTUNID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._eventunid] != null)
                return HttpContext.Current.Request.QueryString[QueryStringValues._eventunid];
            else
                return string.Empty;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._eventunid] = value.ToString();
        }
    }
    public static int TaskID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._TaskID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._TaskID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._TaskID] = value.ToString();
        }
    }
    public static int RiskID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._RiskID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._RiskID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._RiskID] = value.ToString();
        }
    }
    public static int IssueID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._IssueID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._IssueID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._IssueID] = value.ToString();
        }
    }

    public static int ContractorID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._ContractorID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._ContractorID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._ContractorID] = value.ToString();
        }
    }

    public static int Vendor
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._VendorID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._VendorID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._VendorID] = value.ToString();
        }
    }

    public static int SDID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._SDID] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._SDID]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._SDID] = value.ToString();
        }
    }
    public static int CallID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._Callid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._Callid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._Callid] = value.ToString();
        }
    }
    public static int CCID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._CCid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._CCid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._CCid] = value.ToString();
        }
    }

    public static int IVREF
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._ivref] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._ivref]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._ivref] = value.ToString();
        }
    }
    public static int OPTION
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._Option] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._Option]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._Option] = value.ToString();
        }
    }
    public static int HealthCheckId
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._HealthCheckId] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._HealthCheckId]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._HealthCheckId] = value.ToString();
        }
    }

    public static int CTID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._CTid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._CTid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._CTid] = value.ToString();
        }
    }

    public static int CSID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString[QueryStringValues._CSid] != null)
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryStringValues._CSid]);
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Request.QueryString[QueryStringValues._CSid] = value.ToString();
        }
    }
}
