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
using Deffinity.RagSectionProjectEntity;

/// <summary>
/// Summary description for RagSectiontoProjectManager
/// </summary>
namespace Deffinity.RagSectionProjectManager
{
    public class RagSectiontoProjectManager
    {
        public static bool RagSectionProjectInsert(RagSectiontoProjectEntity rsp)
        {
            int restult;
            rsp.ID = 0;
            restult = RagSectionProjectInsert_Update(rsp, "Deffinity_RAGSectiontoProject_InsertUpdate");
            return ((restult > 0) ? true : false);
        }
        public static bool RagSectionProjectUpdate(RagSectiontoProjectEntity rsp)
        {
            int restult;
            restult = RagSectionProjectInsert_Update(rsp, "Deffinity_RAGSectiontoProject_InsertUpdate");
            return ((restult > 0) ? true : false);
        }
        public static bool RagSectionProjectBulkInsert(RagSectiontoProjectEntity rsp)
        {
            int restult;            
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]{   new SqlParameter("@ProjectReference", rsp.ProjectReference),
                                                   new SqlParameter("@PortfolioID", rsp.PortfolioID)     ,
                                                   new SqlParameter("@ProgrammeID", rsp.ProgrammeID)                                                   
                                                  };
            restult= SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RagSectionsToProjectBulkInsert", sqlParams);
            
            return ((restult > 0) ? true : false);
        }
        private static int RagSectionProjectInsert_Update(RagSectiontoProjectEntity rsp, string spName)
        {
            SqlParameter[] sqlParams;
            sqlParams = new SqlParameter[]{    new SqlParameter("@ID", rsp.ID),
                                                   new SqlParameter("@ProjectReference", rsp.ProjectReference),
                                                   new SqlParameter("@PortfolioID", rsp.PortfolioID),
                                                   new SqlParameter("@RAGSectionID", rsp.RagSectionid),                                                   
                                                   new SqlParameter("@KeyIssue", rsp.KeyIssue),
                                                   new SqlParameter("@RAGStatus", rsp.RAGStatus),
                                                   new SqlParameter("@PlannedDate", rsp.PlannedDate),
                                                   new SqlParameter("@LatestDate", rsp.LatestDate),
                                                   new SqlParameter("@ActualDate", rsp.ActualDate),
                                                   new SqlParameter("@ProgrammeID", rsp.ProgrammeID),
                                                   new SqlParameter("@TaskID", rsp.Taskid)
                                                   
                                                  };

            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);

        }
        public static RagSectiontoProjectEntity RagSectionProjectSelect(int ID)
        {
            RagSectiontoProjectEntity rsp = new RagSectiontoProjectEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_RAGSectiontoProject_Select", new SqlParameter("@ID", ID)))
                {
                    while (dr.Read())
                    {
                        rsp.ID = int.Parse(dr["ID"].ToString());
                        rsp.ProjectReference = int.Parse(dr["ProjectReference"].ToString());
                        rsp.PortfolioID = int.Parse(dr["PortfolioID"].ToString());                        
                        rsp.RagSectionid = int.Parse(dr["RAGSectionID"].ToString());
                        rsp.RAGSectionName = dr["RAGSectionName"].ToString();
                        rsp.KeyIssue = dr["KeyIssue"].ToString();
                        rsp.RAGStatus = dr["RAGStatus"].ToString();
                        rsp.ActualDate = DateTime.Parse(string.IsNullOrEmpty(dr["ActualDate"].ToString())?"01/01/1900":dr["ActualDate"].ToString());
                        rsp.PlannedDate = DateTime.Parse(string.IsNullOrEmpty(dr["PlannedDate"].ToString())?"01/01/1900":dr["PlannedDate"].ToString());
                        rsp.LatestDate = DateTime.Parse(string.IsNullOrEmpty(dr["LatestDate"].ToString()) ? "01/01/1900" : dr["LatestDate"].ToString());
                        rsp.ProgrammeID = int.Parse(dr["ProgrammeID"].ToString());
                        rsp.Taskid = int.Parse(string.IsNullOrEmpty(dr["TaskID"].ToString()) ? "0" : dr["TaskID"].ToString()); ;
                    }
                    dr.Close();
                }
            }
            return rsp;
        }
        public static DataTable RagSectionProjectSelectAll(int ProjectReference, int portfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectiontoProject_Select", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PortfolioID", portfolioID)).Tables[0];
        }
            public static DataTable RagSectionProjectSelectAll(int ProjectReference, int portfolioID,int programmeid)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectiontoProject_Select", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@ProgrammeID", programmeid) ).Tables[0];
        }
        public static bool RagSectionProjectDelete(int ID)
        {
            int restult;
            restult = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectiontoProject_delete", new SqlParameter("@ID", ID));
            return ((restult > 0) ? true : false);
        }

        public static void InsertMilestoneToTask(int ProjectReference, int ProgrammeID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RagSectionsToTaskBulkInsert", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@ProgrammeID", ProgrammeID));
        }
    }
}
