using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for ProjectsByResource
/// </summary>
namespace Deffinity.Projects
{
    public class Resources
    {

        public static DataTable GetResourceList()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID, ContractorName from Contractors where Status ='Active' and SID not in (8,6) order by ContractorName").Tables[0];
        }
        public static DataTable GetProjectListByResource(int ContractorID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DN_TimeSheet_ProjectTile", new SqlParameter("@ContractorID", ContractorID),new SqlParameter("@UserID",sessionKeys.UID)).Tables[0];
        }
        public static DataTable GetProjectTaskListByResource(int project, int ContractorID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectTaskItemsByResource ", new SqlParameter("@ProjectReference", project), new SqlParameter("@ContractorID", ContractorID),new SqlParameter("@UserID",sessionKeys.UID)).Tables[0];
        }
        public static DataTable GetProjectList()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ProjectReference,ProjectReferenceWithPrefix + ' - '+ ProjectTitle as ProjectTitle  from V_ProjectDetails where ProjectStatusID in (1,2) order by ProjectReference").Tables[0];
        }
        public static DataTable GetProjectTask(int project)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,ItemDescription as task from V_ProjectTaskDetails where ItemStatus not in (3,5,6) and ProjectReference = @ProjectReference order by ItemDescription", new SqlParameter("@ProjectReference", project)).Tables[0];
        }
        public static void ResourceDashboardTeam_Insert(int LoggedUserID, int ResourceID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "ResourceDashboardTeam_Insert", new SqlParameter("@LoggedUserID", LoggedUserID), new SqlParameter("@ResourceID", ResourceID));
        }
        public static DataTable ResourceDashboardTeam_SelectByTeam(int LoggedUserID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ResourceDashboardTeam_SelectByTeam", new SqlParameter("@LoggedUserID", LoggedUserID)).Tables[0];
        }
    }
}
