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
using Deffinity.HelpCenterEntitys;

/// <summary>
/// Summary description for HelpCenterActionManager
/// </summary>
/// 
namespace Deffinity.HelpCenterManagers
{
    public class HelpCenterManager
    {
        public static bool HelpcenterDetailsInsert(HelpCenterEntity Helpcenterentity)
        {
            int restult;
            restult = DetailsInsert(Helpcenterentity, true, "Deffinity_HelpCenterDetailsInsert");
            return ((restult > 0) ? true : false);
        }
        private static int DetailsInsert(HelpCenterEntity Helpcenterentity, bool SqlType, string spName)
        {//"@ID", riskentity.ID),
            SqlParameter[] sqlParams;

            sqlParams = new SqlParameter[]{    
                                               new SqlParameter("@PortfolioID", Helpcenterentity.PortfolioID),
                                               new SqlParameter("@Name", Helpcenterentity.Name),
                                               new SqlParameter("@ContactNumber", Helpcenterentity.ContactNumber),
                                               new SqlParameter("@SenderEmailID", Helpcenterentity.SenderEmail),
                                               new SqlParameter("@OwnerEmailID", Helpcenterentity.OwnerEmail),
                                               new SqlParameter("@Details", Helpcenterentity.Details)
                                                };

            return SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, spName, sqlParams);

        }

        public static HelpCenterEntity GetMailDetails()
        {
            HelpCenterEntity helpcenterentity = new HelpCenterEntity();
            using (SqlConnection cn = new SqlConnection(Constants.DBString))
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(cn, CommandType.StoredProcedure, "Deffinity_GetHelpCenterDetails"))
                {
                    while (dr.Read())
                    {
                        helpcenterentity.ID = Convert.ToInt32(dr["ID"]);
                        helpcenterentity.Name=dr["Name"].ToString();
                        helpcenterentity.ContactNumber = dr["ContactNumber"].ToString();
                        helpcenterentity.SenderEmail = dr["SenderEmailID"].ToString();
                        helpcenterentity.OwnerEmail = dr["OwnerEmailID"].ToString();
                        helpcenterentity.Details = dr["Details"].ToString();
                        helpcenterentity.DateRaised = Convert.ToDateTime(dr["DateTime"]);
                    }
                }
            }
            return helpcenterentity;

        }
        public string GetOwnerEmail(int PortfolioID)
        {

            SqlConnection con = new SqlConnection(Constants.DBString);
            string strcmd;
            string OwnerEmailID;
            strcmd = "select EmailAddress from contractors inner join projectportfolio on contractors.ID=projectportfolio.Owner where projectportfolio.ID="+PortfolioID;
            SqlCommand cmd = new SqlCommand(strcmd);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            OwnerEmailID = cmd.ExecuteScalar().ToString();
            return OwnerEmailID;
        }

    }
}
 
           