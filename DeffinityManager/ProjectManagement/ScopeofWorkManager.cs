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
using System.Data.SqlTypes;
using Microsoft.ApplicationBlocks.Data;
using Deffinity.ScopeofWorkEntitys;

/// <summary>
/// Summary description for ScopeofWorkManager
/// </summary>

namespace Deffinity.ScopeofWorkManagers
{
    public class ScopeofWorkManager
    {
        
        public static int ProjectScopeofWorkCheckRecord(int ProjectReference, int PUID)
        {
            string restult;
            restult = SqlHelper.ExecuteScalar(Constants.DBString,CommandType.Text,"Select count(*) from [ProjectScopeofWork] where ProjectReference=@ProjectReference and PUID = @PUID",new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PUID", PUID)).ToString();
            return int.Parse(restult);
        }
        public static bool ProjectScopeofWorkSave(ScopeofWorkEntity ScopeofWork)
        {
            int restult;
            restult = Insert_Update(ScopeofWork, "Deffinity_ScopeofworkInsert");           
            return ((restult > 0) ? true : false);
        }
        private static int Insert_Update(ScopeofWorkEntity ScopeofWork, string spName)
        {
            SqlParameter[] sqlParams = new SqlParameter[]{new SqlParameter("@ProjectReference", ScopeofWork.ProjectReference),
                                                   new SqlParameter("@PUID", ScopeofWork.PUID),
                                                   new SqlParameter("@DetailsofWork", ScopeofWork.DetailsofWork),
                                                   new SqlParameter("@DetailsofServices", ScopeofWork.DetailsofServices),
                                                   new SqlParameter("@DetailsofSecurity", ScopeofWork.DetailsofSecurity),
                                                   new SqlParameter("@Skills", ScopeofWork.Skills),
                                                   new SqlParameter("@StartDate", Convert.ToDateTime(ScopeofWork.StartDate == string.Empty?"01/01/1900":ScopeofWork.StartDate)),
                                                   new SqlParameter("@StartTime", ScopeofWork.StartTime),
                                                   new SqlParameter("@EndDate", Convert.ToDateTime( ScopeofWork.EndDate==string.Empty?"01/01/1900":ScopeofWork.EndDate)),
                                                   new SqlParameter("@EndTime", ScopeofWork.EndTime),
                                                   new SqlParameter("@Site", ScopeofWork.Site),
                                                   new SqlParameter("@Address1", ScopeofWork.Address1),
                                                   new SqlParameter("@Address2", ScopeofWork.Address2),
                                                   new SqlParameter("@Address3", ScopeofWork.Address3),
                                                   new SqlParameter("@PostCode", ScopeofWork.PostCode),
                                                   new SqlParameter("@Name", ScopeofWork.Name),
                                                   new SqlParameter("@Number", ScopeofWork.Number),
                                                   new SqlParameter("@Email", ScopeofWork.Email),
                                                   new SqlParameter("@RaisedBy", ScopeofWork.RaisedBy)
                                                   };

            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);

        }

        public static ScopeofWorkEntity ProjectScopeofWorkSelect(int ProjectReference,int PUID)
        {
            ScopeofWorkEntity ScopeofWork = new ScopeofWorkEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_ScopeofworkSelect", new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@PUID", PUID)))
                {
                    while (dr.Read())
                    {
                        ScopeofWork.ID = int.Parse(dr["ID"].ToString());
                        ScopeofWork.Name = dr["Name"].ToString();
                        ScopeofWork.Number = dr["Number"].ToString();
                        ScopeofWork.PostCode = dr["PostCode"].ToString();
                        ScopeofWork.ProjectReference = int.Parse(dr["ProjectReference"].ToString());
                        ScopeofWork.PUID = int.Parse(dr["PUID"].ToString());
                        ScopeofWork.RaisedBy = int.Parse(dr["RaisedBy"].ToString());
                        ScopeofWork.Site = dr["Site"].ToString();
                        ScopeofWork.Skills = dr["Skills"].ToString();                       
                        ScopeofWork.StartTime = dr["StartTime"].ToString();
                        ScopeofWork.Address1 = dr["Address1"].ToString();
                        ScopeofWork.Address2 = dr["Address2"].ToString();
                        ScopeofWork.Address3 = dr["Address3"].ToString();
                        ScopeofWork.DetailsofSecurity = dr["DetailsofSecurity"].ToString();
                        ScopeofWork.DetailsofServices = dr["DetailsofServices"].ToString();
                        ScopeofWork.DetailsofWork = dr["DetailsofWork"].ToString();
                        ScopeofWork.Email = dr["Email"].ToString();
                        ScopeofWork.EndDate = string.IsNullOrEmpty(dr["EndDate"].ToString())? string.Empty:DateTime.Parse(dr["EndDate"].ToString()).ToShortDateString();
                        ScopeofWork.StartDate = string.IsNullOrEmpty(dr["StartDate"].ToString()) ? string.Empty : DateTime.Parse(dr["StartDate"].ToString()).ToShortDateString();
                        ScopeofWork.EndTime = dr["EndTime"].ToString();
                    }
                }
            }
            return ScopeofWork;
        }
    }
    
     
}
