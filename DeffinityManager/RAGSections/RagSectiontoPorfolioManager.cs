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
using Deffinity.RagSectionPortfolioEntity;


/// <summary>
/// Summary description for RagSectiontoPorfolioManager
/// </summary>
namespace Deffinity.RagSectionPorfolioManager
{
    public class RagSectiontoPorfolioManager
    {
        public static bool RagSectionPorfolioInsert(RagSectiontoPortfolioEntity rsp, out int retval)
        {
            int result;
            rsp.ID = 0;
            result = RagSectionPorfolioInsert_Update(rsp, "Deffinity_RAGSectionstoPortfolio_InsertUpdate",out retval);
            return ((result > 0) ? true : false);
        }
        public static bool RagSectionPorfolioUpdate(RagSectiontoPortfolioEntity rsp, out int retval)
        {
            int result;
            result = RagSectionPorfolioInsert_Update(rsp, "Deffinity_RAGSectionstoPortfolio_InsertUpdate",out retval);
            return ((result > 0) ? true : false);
        }
        private static int RagSectionPorfolioInsert_Update(RagSectiontoPortfolioEntity rsp, string spName, out int retval)
        {
            int t;
            SqlParameter ParamOut = new SqlParameter("@OutVal", SqlDbType.Int);
            ParamOut.Direction = ParameterDirection.Output;
            SqlParameter[] sqlParams;
                sqlParams = new SqlParameter[]{    new SqlParameter("@ID", rsp.ID),
                                                   new SqlParameter("@PortfolioID", rsp.PortfolioID),
                                                   new SqlParameter("@RAGSectionName", rsp.RAGSectionName),
                                                   new SqlParameter("@RAGDescription", rsp.RAGDescription),
                                                   new SqlParameter("@ProgrammeID",rsp.Programmeid),
                                                   new SqlParameter("@SubProgrammeID",rsp.Subprogrammeid),
                                                   new SqlParameter("@TaskID",rsp.Taskid)
                                                  ,ParamOut};

                t = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);
                retval = int.Parse(ParamOut.Value.ToString());
                return t;

        }
        public static bool RagSectionPorfolioDelete(int ID)
        {
            int result;
            result = SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectionstoPortfolio_delete", new SqlParameter("@ID", ID));
            return ((result > 0) ? true : false);
        }
        public static RagSectiontoPortfolioEntity RagSectionPorfolioSelect(int ID)
        {
            RagSectiontoPortfolioEntity rsp = new RagSectiontoPortfolioEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_RAGSectionstoPortfolio_Select", new SqlParameter("@ID", ID)))
                {
                    while (dr.Read())
                    {
                        rsp.ID = int.Parse(dr["ID"].ToString());
                        rsp.PortfolioID = int.Parse(dr["PortfolioID"].ToString());
                        rsp.RAGDescription = dr["RAGDescription"].ToString();                        
                        rsp.RAGSectionName = dr["RAGSectionName"].ToString();
                        rsp.Programmeid = int.Parse(string.IsNullOrEmpty(dr["ProgrammeID"].ToString())?"0":dr["ProgrammeID"].ToString());
                        rsp.Subprogrammeid = int.Parse(string.IsNullOrEmpty(dr["SubProgrammeID"].ToString())?"0":dr["SubProgrammeID"].ToString());
                        rsp.Taskid = int.Parse(string.IsNullOrEmpty(dr["TaskID"].ToString()) ? "0" : dr["TaskID"].ToString());
                        rsp.Programmename = dr["ProgrammeName"].ToString();
                        rsp.Subprogrammename = dr["SubProgrammeName"].ToString();
                    }
                }
            }
            return rsp;
        }
        public static DataTable RagSectionPorfolioSelectAll(int portfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectionstoPortfolio_Select", new SqlParameter("@PortfolioID", portfolioID)).Tables[0];
        }
        public static DataTable RagSectionProgrammeSelectAll(int programmeID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_RAGSectionstoProgramme_Select", new SqlParameter("@ProgrammeID", programmeID)).Tables[0];
        }
    }
}
