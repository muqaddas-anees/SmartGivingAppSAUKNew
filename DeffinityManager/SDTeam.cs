using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


/// <summary>
/// Summary description for SDTeam
/// </summary>
namespace Deffinity.SDTeam_Manager
{
    public class SDTeam
    {
        public static int Method_Insert(string TeamName, string Email, string ContactNumber, string Notes, int Site, int PortfolioID, int AreaID)
        {
            SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 4);
            OutVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "SDteam_Insert", new SqlParameter("@TeamName", TeamName), new SqlParameter("@Email", Email), new SqlParameter("@ContactNumber", ContactNumber), new SqlParameter("@Notes", Notes), new SqlParameter("@Site", Site), new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@AreaID", AreaID), OutVal);
            return int.Parse(OutVal.Value.ToString());
        }
        public static int Method_Update(int ID,string TeamName, string Email, string ContactNumber, string Notes, int Site, int PortfolioID)
        {
            SqlParameter OutVal = new SqlParameter("@OutVal", SqlDbType.Int, 4);
            OutVal.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "SDteam_Update", new SqlParameter("@ID", ID), new SqlParameter("@TeamName", TeamName), new SqlParameter("@Email", Email), new SqlParameter("@ContactNumber", ContactNumber), new SqlParameter("@Notes", Notes), new SqlParameter("@Site", Site), new SqlParameter("@PortfolioID", PortfolioID), OutVal);
            return int.Parse(OutVal.Value.ToString());
        }

        public static int Method_CopyTeam(int ID, int PortfolioID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "SDteam_CopyTeams", new SqlParameter("@ID", ID), new SqlParameter("@PortfolioID", PortfolioID));
            return 1;
        }

        public static int Method_AddUserToCustomer(int UserID, int PortfolioID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "SDUsertoTeams", new SqlParameter("@UserID", UserID), new SqlParameter("@PortfolioID", PortfolioID));
            return 1;
        }

        public static int Method_AddUserToCustomer_All(int UserID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "SDUsertoCustomers", new SqlParameter("@UserID", UserID));
            return 1;
        }

        public static DataTable Method_SelectAll(int PortfolioID, int AreaID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "SDteam_SelectByPortfolio", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@AreaID", AreaID)).Tables[0];
        }

        public static DataTable Method_SelectTeam(int PortfolioID, string AreaID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "Select ID,TeamName from SDteam where PortfolioID =@PortfolioID and ( CASE  WHEN @AreaID >0 AND AreaID = @AreaID THEN 1 WHEN isnull(@AreaID,0) =0 AND PortfolioID > 0 THEN 1 ELSE 0 END ) = 1 union select 0,' Please select...'  order by TeamName ", new SqlParameter("@PortfolioID", PortfolioID), new SqlParameter("@AreaID", int.Parse(string.IsNullOrEmpty(AreaID) ? "0" : AreaID))).Tables[0];
        }
        public static void Method_Delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "delete from SDteam where ID = @ID", new SqlParameter("@ID", ID));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SDteamID"></param>
        /// <param name="UserID"></param>
        /// <param name="SDUserType">1.Manager 2.Member</param>
        public static void Method_TeamUser_insert(int SDteamID, int UserID,int SDUserType)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "INSERT INTO [dbo].[SDteamToUsers] ([SDteamID],[UserID],SDUserType) VALUES(@SDteamID,@UserID,@SDUserType)", new SqlParameter("@SDteamID", SDteamID), new SqlParameter("@UserID", UserID),new SqlParameter("@SDUserType",SDUserType));
        }

        public static DataTable Method_TeamUser_Select(int SDTeamID)
        {
            DataTable dt = new DataTable();
            try {

                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "SELECT SDteamToUsers.ID,[SDteamID],[UserID],isnull(SDUserType,0) as SDUserType,Contractors.ContractorName,Contractors.EmailAddress,Contractors.ContactNumber, (case isnull(SDUserType,0)when 0 then '0' when 1 then 'Manager'when 2 then 'Member' end)  as ContractorType FROM SDteamToUsers INNER JOIN Contractors ON SDteamToUsers.UserID = Contractors.ID where SDteamID =@SDteamID  order by Contractors.ContractorName", new SqlParameter("@SDteamID", SDTeamID)).Tables[0];
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


            return dt;
        }

        public static void Method_TeamUser_update(int ID, int SDUserType)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "update SDteamToUsers set SDUserType = @SDUserType where ID = @ID", new SqlParameter("@ID", ID), new SqlParameter("@SDUserType", SDUserType));
        }
        //Method_TeamUser_delete
        public static void Method_TeamUser_delete(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.Text, "Delete From SDteamToUsers where ID = @ID", new SqlParameter("@ID", ID));
        }
        //select [SDteamID],[UserID],(Select ContractorName from Contractors where ID = SDteamToUsers.UserID) as ContractorName from [SDteamToUsers]  where SDteamID =@SDteamID
        //public static DataTable Method_SelectTeam(int SDteamID)
        //{
        //    return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "select [SDteamID],[UserID],(Select ContractorName from Contractors where ID = SDteamToUsers.UserID) as ContractorName from [SDteamToUsers]  where SDteamID =@SDteamID", new SqlParameter("@SDteamID", SDteamID)).Tables[0];
        //}
        
    }
}