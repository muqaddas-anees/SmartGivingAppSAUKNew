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


namespace Deffinity.PortfolioContactManager
{
    /// <summary>
    /// Summary description for PorfolioContacts
    /// </summary>
    public partial class PorfolioContacts
    {
        /// <summary>
        /// returns 1.inserted 2.failed 
        /// </summary>
        /// <param name="PortfolioID"></param>
        /// <param name="Name"></param>
        /// <param name="Title"></param>
        /// <param name="Email"></param>
        /// <param name="Telephone"></param>
        /// <param name="Location"></param>
        /// <param name="Key_Contact"></param>
        /// <param name="Notes"></param>
        /// <returns></returns>
        public static int InsertPortfolioContacts(int PortfolioID, string Name, string Title, string Email, string Telephone, string Location, Boolean Key_Contact, string Notes)
        {
            SqlParameter Outval = new SqlParameter("@OutVal", SqlDbType.Int);
            Outval.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString,CommandType.StoredProcedure,"Deffinity_PortfolioContact_Insert",
                new SqlParameter("@PortfolioID",PortfolioID),
                new SqlParameter("@Name",Name),new SqlParameter("@Title",Title),new SqlParameter("@Email",Email),new SqlParameter("@Telephone",Telephone)
                , new SqlParameter("@Location", Location), new SqlParameter("@Key_Contact", Key_Contact), new SqlParameter("@Notes", Notes),Outval);
            int retval = int.Parse(Outval.Value.ToString());


            return retval;
        }
        /// <summary>
        /// returns 1.updated 2.failed 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PortfolioID"></param>
        /// <param name="Name"></param>
        /// <param name="Title"></param>
        /// <param name="Email"></param>
        /// <param name="Telephone"></param>
        /// <param name="Location"></param>
        /// <param name="Key_Contact"></param>
        /// <param name="Notes"></param>
        /// <returns></returns>
        public static int UpdatePortfolioContacts(int ID,int PortfolioID, string Name, string Title, string Email, string Telephone, string Location, Boolean Key_Contact, string Notes)
        {
            SqlParameter Outval = new SqlParameter("@OutVal", SqlDbType.Int);
            Outval.Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_PortfolioContact_Update",
                new SqlParameter("@ID", ID), new SqlParameter("@PortfolioID", PortfolioID),
                new SqlParameter("@Name", Name), new SqlParameter("@Title", Title), new SqlParameter("@Email", Email), new SqlParameter("@Telephone", Telephone)
                , new SqlParameter("@Location", Location), new SqlParameter("@Key_Contact", Key_Contact), new SqlParameter("@Notes", Notes), Outval);
            int retval = int.Parse(Outval.Value.ToString());


            return retval;
        }


        public static SqlDataReader SelectPortfolioContacts(int ID)
        { 
            return SqlHelper.ExecuteReader(Constants.DBString,CommandType.StoredProcedure,"Deffinity_PortfolioContact_select",new SqlParameter("@ID",ID));
        }
        public static DataTable SelectAllPortfolioContacts(int PortfolioID)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Deffinity_PortfolioContact_selectAll", new SqlParameter("@PortfolioID", PortfolioID)).Tables[0];
        }
        public static void DeletePortfolioContacts(int ID)
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_PortfolioContact_Delete", new SqlParameter("@ID", ID));
        }

    }
}
