using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for IssuesManager
/// </summary>
namespace Deffinity.IssuesManager
{
    public class IssuesManager
    {
        //get max for a project
        public static int GetmaxID(int ProjectReference)
        { 
           return int.Parse(SqlHelper.ExecuteScalar(Constants.DBString,CommandType.Text,"select max(ID) from ProjectIssues where ProjectReference = @ProjectReference",new SqlParameter("@ProjectReference",ProjectReference)).ToString());
        }
        public static void InsertIssue(int ProjectReference,string Issue,DateTime OutComeDate)
        {
            //SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ProjectReference", ProjectReference),
            //                                       new SqlParameter("@Issue", Issue),
            //                                       new SqlParameter("@ScheduledDate", OutComeDate),
            //                                       new SqlParameter("@Status", 1),
            //                                       new SqlParameter("@IssuseType", 1),
            //                                       new SqlParameter("@RaisedBy", sessionKeys.UID)                                                   
            //                                        };

            //SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_IssueInsert", sqlParams);
            InsertIssueCore(ProjectReference, Issue, OutComeDate,1,1,sessionKeys.UID);
        
        }
        private static void InsertIssueCore(int projectreference, string issue, DateTime ScheduledDate, int Status, int IssueType, int RaisedBy)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ProjectReference", projectreference),
                                                   new SqlParameter("@Issue", issue),
                                                   new SqlParameter("@ScheduledDate", ScheduledDate),
                                                   new SqlParameter("@Status", Status),
                                                   new SqlParameter("@IssuseType", IssueType),
                                                   new SqlParameter("@RaisedBy", RaisedBy)                                                   
                                                    };

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_IssueInsert", sqlParams);            
        
        }
        public static void InsertFromCustomer(int projectreference, string Issue, int RaisedBy)
        {
            InsertIssueCore(projectreference, Issue, DateTime.Now, 1, 0, RaisedBy);
        }
        public static void InsertFromCustomer(int projectreference, string Issue, int RaisedBy, int statusid, int siteid, DateTime loggedDate, int RAGStatus)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ProjectReference", projectreference),
                                                   new SqlParameter("@Issue", Issue),
                                                   new SqlParameter("@ScheduledDate", loggedDate),
                                                   new SqlParameter("@Status", statusid),
                                                   new SqlParameter("@IssuseType", 0),
                                                   new SqlParameter("@RaisedBy", RaisedBy) ,
                                                   new SqlParameter("@Location", siteid),
                                                   new SqlParameter("@RAGStatus", RAGStatus) };

            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_IssueInsert", sqlParams);  

        }
        public static void UpdateIssue_AllFields(int ID,string Issue,int assignTo,string ScheduledDate,string Notes,int Status,int IssueseType,
            string ExpectedOutcome,int CheckedBy,string DateChecked,string IssueRaisedBy,int Location)
        {

            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ID", ID),
                                                   new SqlParameter("@Issue", Issue),
                                                   new SqlParameter("@AssignTo", assignTo),
                                                   new SqlParameter("@ScheduledDate", Convert.ToDateTime(ScheduledDate)),
                                                    new SqlParameter("@Notes", Notes),
                                                   new SqlParameter("@Status", Status),
                                                   new SqlParameter("@IssuseType", IssueseType),
                                                   new SqlParameter("@ExpectedOutcome", ExpectedOutcome),                                                    
                                                    new SqlParameter("@Checkedby", CheckedBy),
                                                    new SqlParameter("@DateChecked", Convert.ToDateTime(string.IsNullOrEmpty(DateChecked)? "01/01/1900":DateChecked)),
                                                    new SqlParameter("@IssueRaisedby", IssueRaisedBy),
                                                    new SqlParameter("@Location", Location)                                                    
                                                    };
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_UpdateIssues", sqlParams);
        }
        public static SqlDataReader GetIssueDetails(int IssueID)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectissueSelect", new SqlParameter("@IssueID", IssueID));
        }

        #region Dashboard
        public static DataTable Dashboard_DisplayIssues(int IssueType, int Program, int ProjectRef, int CountryID, int SubProgram, int portfolio, int UserID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectIssue_Dashboard_Select", new SqlParameter("@IssuseType", IssueType), new SqlParameter("@Program", Program), new SqlParameter("@ProjectRef", ProjectRef), new SqlParameter("@CountryID", CountryID), new SqlParameter("@SubProgram", SubProgram), new SqlParameter("@UserID", UserID),
                new SqlParameter("@portfolioid", portfolio)).Tables[0];
        }
        public static void Dashboard_UpdateIssues(int ID, int Status, string Notes)
        { 
            SqlHelper.ExecuteNonQuery(Constants.DBString,CommandType.StoredProcedure,"Deffinity_ProjectIssue_Dashbaord_Update",new SqlParameter("@IssueID",ID),new SqlParameter("@Status",Status),new SqlParameter("@Notes",Notes));
        }
        #endregion

        #region My Issues
        public static DataTable Dashboard_Search_DisplayIssues(int status, int ContractorID, string ScheduleDate, int IssuseType)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_IssuesSelect_Search", new SqlParameter("@status", status), new SqlParameter("@ContractorID", ContractorID), new SqlParameter("@ScheduleDate", ScheduleDate), new SqlParameter("@IssuseType", IssuseType)).Tables[0];
        }
        #endregion
    }

 
}
