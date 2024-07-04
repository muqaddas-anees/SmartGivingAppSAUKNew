using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web.Caching;
using Microsoft.ApplicationBlocks.Data;



/// <summary>
/// Summary description for PermissionManager
/// </summary>
public class PermissionManager
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    private const string CACHE_PROJECTFEATURES = "CACHE_PROJECTFEATURES";
    private const string CACHE_DASHBOARDFEATURES = "CACHE_DASHBOARDFEATURES";
    private const string CACHE_LOCKDOWNFEATURES = "CACHE_LOCKDOWNFEATURES";
    private static string[] ProjectNavigateURLS =
    {"ProjectOverview.aspx", 
        "ProjectTasks.aspx", 
        "ProjectResources.aspx", 
        "ProjectBillofMaterials.aspx",
        "ProjectFinancials.aspx",
        "ProjectRisks.aspx",
        "ProjectPmApproval.aspx",
        "ProjectAsset.aspx",
        "ProjectDocuments.aspx",
        "ProjectForumMaster.aspx",
        "ProjectIssues.aspx",
        "ProjectMeetings.aspx",
        "IPD.aspx",
        "UserPermissions.aspx"
   };
    private static string[] Purl = { "MyTasks.aspx?Status=9",
                                    "Cases.aspx",
                                    "HealthCheckSchedule.aspx?R=Y", 
                                    "MyQAIssues.aspx?Status=10", 
                                    "MyRisks.aspx?Status=11",
                                    "MyProjects.aspx?Status=2",
                                    "MyProjects.aspx?Status=1",
                                    "MyProjects.aspx?Status=3", 
                                    "CCApproval.aspx",
                                    "TimeSheetResources.aspx?Status=8"  };
   public PermissionManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void UpdateFeaturesCache()
    {
        HttpContext.Current.Cache.Remove(CACHE_PROJECTFEATURES);
        HttpContext.Current.Cache.Remove(CACHE_DASHBOARDFEATURES);
        HttpContext.Current.Cache.Remove(CACHE_LOCKDOWNFEATURES);
    }
    public static bool IsPermitted(int ProjectReference,int contractorID,PermissionsTo permissionTo)
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
        DataSet ds;
        try
        {
            cmd = db.GetStoredProcCommand("DEFFINITY_GETPROJ_PERMISSIONS");
            db.AddInParameter(cmd, "@PROJECTREFERENCE", DbType.Int32, ProjectReference);
            db.AddInParameter(cmd, "@CONTRACTORID", DbType.Int32, contractorID);
            ds=db.ExecuteDataSet(cmd);
            cmd.Dispose();
            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
                return true;
            else if (Convert.ToInt32(ds.Tables[0].Rows[0][permissionTo.GetHashCode()]) == 1)
                return true;
            else
                return false;        
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Get Permissions for contractor to a Project");
            return false;
        }
    }
    public static bool IsEnabled(Feature feature)
    {
        try
        {
            int count = GetCount();
            string[] strFeatures = new string[count];
            strFeatures = GetFeatures();
            return Convert.ToBoolean(strFeatures[feature.GetHashCode()]);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Get Status of a Feature in the Instance");
            return false;
        }
    }
   
    
    //public static bool IsEnabled(Feature feature)
    //{
    //    Database db = DatabaseFactory.CreateDatabase("DBstring");
    //    DbCommand cmd;
    //    try
    //    {
    //        cmd = db.GetStoredProcCommand("Deffinity_GetLockDownStatus");
    //        db.AddInParameter(cmd, "@Feature", DbType.Int32, feature.GetHashCode());
    //        Boolean i= Convert.ToBoolean( db.ExecuteScalar(cmd));
    //        cmd.Dispose();
    //        return i;
    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.LogException(ex.Message, "Get Status of a Feature in the Instance");
    //        return false;
    //    }
    //}

    public static string[] GetProjectFeatures()
    {
        string[] strProjectFeatures = null;
        if (HttpContext.Current.Cache[CACHE_PROJECTFEATURES] == null)
        {
            strProjectFeatures = GetBuildProjectFeatureStatus();
            HttpContext.Current.Cache.Add(CACHE_PROJECTFEATURES, strProjectFeatures, null, DateTime.Now.AddMinutes(10), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
        else
        {
            strProjectFeatures = (string[])HttpContext.Current.Cache[CACHE_PROJECTFEATURES];
        }
        return strProjectFeatures;
    }

    public static string[] GetDashboardFeatures()
    {
        string[] strDashboarFeatures = null;
        if (HttpContext.Current.Cache[CACHE_PROJECTFEATURES] == null)
        {
            strDashboarFeatures = GetDashboardFeatureStatus();
            HttpContext.Current.Cache.Add(CACHE_DASHBOARDFEATURES, strDashboarFeatures, null, DateTime.Now.AddMinutes(10), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
        else
        {
            strDashboarFeatures = (string[])HttpContext.Current.Cache[CACHE_DASHBOARDFEATURES];
        }
        return strDashboarFeatures;
    }

    //Deffinity_GetDashboardTab

    public static string[] GetDashboardFeatureStatus()
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;

        cmd = db.GetStoredProcCommand("Deffinity_GetDashboardTab");
        string[] strFeatures = new string[11];
        int recordCount = 0;
        using (IDataReader datareader = db.ExecuteReader(cmd))
        {
            while (datareader.Read())
            {
                strFeatures[recordCount] = datareader[1].ToString();
                recordCount++;
            }
            //dataValues = new string[recordCount, datareader.FieldCount];
        }
        cmd.Dispose();
        return strFeatures;
      }
    public static string GetNextURL(Feature feature)
    {
        string[] strFeatures =GetProjectFeatures();
        int n = feature.GetHashCode();
        for (int i = n; i < 14; i++)
        {
            if (Convert.ToBoolean(strFeatures[i]) == true)
                return ProjectNavigateURLS[i].ToString();
        }
        return "~/ProjectPipeline.aspx?Status=2&list=all";
    }
    public static string GetBackURL(Feature feature)
    {
        string[] strFeatures = GetProjectFeatures();
        int n = feature.GetHashCode();
        for (int i = n-2; i >=0; i--)
        {
            if (Convert.ToBoolean(strFeatures[i]) == true)
                return ProjectNavigateURLS[i].ToString();
        }
        return "noaccess.aspx";
    }
    public static string[] GetBuildProjectFeatureStatus()
    {
       
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        DbCommand cmd;
                
            cmd = db.GetStoredProcCommand("Deffinity_GetProjectTab");
            string[] strFeatures = new string[14];
            int recordCount = 0;
            using (IDataReader datareader = db.ExecuteReader(cmd))
            {
                while (datareader.Read())
                {
                    strFeatures[recordCount] = datareader[1].ToString();
                    recordCount++;
                }
            }
 
            return strFeatures;
  
    }
    public static string[] GetFeatures()
    {
        string[] strFeatures = null;
        if (HttpContext.Current.Cache[CACHE_LOCKDOWNFEATURES] == null)
        {
            strFeatures = GetFeatureStatus();
            HttpContext.Current.Cache.Add(CACHE_LOCKDOWNFEATURES, strFeatures, null, DateTime.Now.AddMinutes(10), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
        else
        {
            strFeatures = (string[])HttpContext.Current.Cache[CACHE_LOCKDOWNFEATURES];
        }
        return strFeatures;
    }
    public static string[] GetFeatureStatus()
    {
        //get the count 
        int count =  GetCount();

        string[] strFeatures = new string[count];
        int recordCount = 1;
        using (SqlDataReader datareader = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetFeaturesStatus"))
        {
            while (datareader.Read())
            {
                strFeatures[recordCount] = datareader[1].ToString();
                recordCount++;
            }
            //dataValues = new string[recordCount, datareader.FieldCount];
        }
        return strFeatures;
    }

    private static int GetCount()
    {
        return Convert.ToInt32(SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, "select max(ID)+1 as cnt from LockDownFeatures").ToString());
    }
    public static bool UpdateFeatureStatus(string IDS, bool _Status)
    {
        //deffinity_UpdateFeatures
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]{
            new SqlParameter( "@Status",  _Status),
            new SqlParameter( "@IDS",  IDS)};
            int i = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "deffinity_UpdateFeatures", sqlParams);
            return (i > 0 ? true : false);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Update Status of a Feature in the Instance");
            return false;
        }
    }
    public static string GetNextMyTasksURL(Feature feature)
    {
        string[] strFeatures = GetFeatures();
        int n = feature.GetHashCode();
        for (int i = n; i < 38; i++)
        {
            if (Convert.ToBoolean(strFeatures[i]) == true)
                return Purl[i-29].ToString();
        }
        return "~/ProjectPipeline.aspx?Status=2&list=all";
    }
    public static bool CheckMaxUsersAllowed(int UserType)
    {
        try
        {
            SqlParameter sqlParamStatus = new SqlParameter("@Status", SqlDbType.Bit);
            sqlParamStatus.Direction = ParameterDirection.Output;
            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter( "@UserType",  UserType),sqlParamStatus};
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "deffinity_MaxAllowedUsers", sqlParams);
            return Convert.ToBoolean(sqlParamStatus.Value);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Checking max users allowed");
            return false;
        }
        //"deffinity_MaxAllowedUsers"
        //return false;
    }
    public static bool UpdateUserLimit(int PMs,int Resources)
    {
        //deffinity_UpdateFeatures
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]{
            new SqlParameter( "@PMs",  PMs),
            new SqlParameter( "@Resources",  Resources)};
            int i = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "deffinity_UpdateUserLimit", sqlParams);
            return (i > 0 ? true : false);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Update user Limit in the Instance");
            return false;
        }
    }
    public static DataRow GetUserLimit()
    {
        return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_GetUserLimits").Tables[0].Rows[0];
    }
    public enum Feature
    {
        ProjectOverview = 1,
        ProjectTasks = 2,
        ProjectResources = 3,
        ProjectBOM = 4,
        ProjectFinancials = 5,
        ProjectRisks = 6,
        ProjectCheckPoint = 7,
        ProjectAssets = 8,
        ProjectDocs = 9,
        ProjectForum = 10,
        ProjectIssues = 11,
        ProjectMeetings	=12,
        ProjectIPD = 13,
        ProjectProfiler = 14,
        CaseManagement = 15,
        DashboardProject=16,
	    DashboardTasks	=17,
	    DashboardIssues	=18,
	    DashboardResources=	19,
	    DashboardRisks	=20,
	    DashboardCSI	=21,
        DashboardPortfolio=	22,
	    DashboardProgramme=	23,
	    DashboardCase	=24,
	    DashboardSearch	=25,
	    DashboardReports=	26,
        DashboardHealthChecks=27,
        DashboardServiceRequests=28,
        MyTasks=29,
        MyTasksServiceReqs=30,
        MyTasksHealthChecks=31,
        MyTasksMyIssues=32,
        MyTasksMyRisks=33,
        MyTasksLiveProjects=34,
        MyTasksCompleted=35,
        MyTasksChangeControl=36,
        MyTasksTimeSheet = 37,
	    ResourceProjectOverView=38,
        ResourceTasks=39,
        ResourceScope=40,
        ResourceTandM=41,
        ResourceVariations=42,
        ResourceCSI=43,
        ResourceAssets=44,
        ResourceDocs=45,
        ResourceForum=46,
        ResourceClose=47
    }
    public enum PermissionsTo 
    {
        AdminRights = 0,
        ModifyProject = 1,
        AllocateTask = 2,
        ManagageIssues = 3,
        ManageRisk = 4,
        AddAssets = 5,
        AddDocuments = 6,
        ManageProjectFinancials = 7,
        ApproveVariations = 8,
        DeleteDocument = 9
    };
      
}
