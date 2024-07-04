using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using Deffinity.LessonsLearntEntitys;

/// <summary>
/// Summary description for LessonsLearntManager
/// </summary>
namespace Deffinity.LessonsLearntManagers
{
    public class LessonsLearntManager
    {
        public LessonsLearntManager()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static bool LearnInsert(LessonsLearntEntity LessonsLearn)
        {
            int restult;            
            restult = Insert_Update(LessonsLearn, true, "Deffinity_LessonsLearntInsert");
            return ((restult > 0) ? true : false);
        }       
        private static int Insert_Update(LessonsLearntEntity LL, bool SqlType, string spName)
        {

            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ProjectReference", LL.ProjectReference),
                                                   new SqlParameter("@Description", LL.Description),
                                                   new SqlParameter("@RemediationActions", LL.RemediationActions),
                                                   new SqlParameter("@BusinessImpact", LL.BusinessImpact),
                                                   new SqlParameter("@IdentifiedBy", LL.IdentifiedBy),
                                                   new SqlParameter("@AssignedTo", LL.AssignedTo),
                                                   new SqlParameter("@Status", LL.Status)
                                                    };
                      
            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);
            
        }
        public static LessonsLearntEntity LessonsLearntSelect(int ID)
        {
            LessonsLearntEntity ll = new LessonsLearntEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_LessonsLearntSelect",new SqlParameter("@ID", ID)))
                {
                    while (dr.Read())
                    {
                      ll.ID = int.Parse(dr["ID"].ToString());
                      ll.ProjectReference = int.Parse(dr["ProjectReference"].ToString());
                      ll.Description = dr["Description"].ToString();
                      ll.RemediationActions = dr["RemediationActions"].ToString();
                      ll.BusinessImpact = dr["BusinessImpact"].ToString();
                      ll.IdentifiedBy = int.Parse(dr["IdentifiedBy"].ToString());
                      ll.AssignedTo = int.Parse(dr["AssignedTo"].ToString());
                      ll.Status = int.Parse(dr["Status"].ToString());
                      ll.DateRaised = DateTime.Parse(dr["DateRaised"].ToString());
                      ll.DateInProgress =DateTime.Parse( dr["DateInProgress"].ToString());
                      ll.DateCompleted =DateTime.Parse(dr["DateCompleted"].ToString());
                    }
                }
            }
            return ll;

        }
        public static DataTable LessonsLearntSelectAll()
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_LessonsLearntSelectAll").Tables[0];
        }
       
    }
}
