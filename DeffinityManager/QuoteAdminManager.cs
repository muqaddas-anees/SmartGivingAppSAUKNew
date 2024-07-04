using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Quote.DAL;
using Microsoft.ApplicationBlocks.Data;


/// <summary>
/// Summary description for QuoteAdminManager
/// </summary>
namespace Quote.BLL
{
    public class QuoteAdminManager
    {

        public QuoteAdminManager()
        {
        }
        public int QuoteAdminInsertUpdate(QuoteAdminEntity Qa)
        {
            using (SqlConnection conn = new SqlConnection(Constants.DBString))
            {
               
                using(SqlCommand cmd=new SqlCommand(QuoteAdminProcs.QuoteAdminInsertUpdate,conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Qa.ID);
                    cmd.Parameters.AddWithValue("@Portfolio", Qa.Portfolio);
                    cmd.Parameters.AddWithValue("@Prefix", Qa.Prefix);
                    cmd.Parameters.AddWithValue("@VAT", Qa.VAT);
                    cmd.Parameters.AddWithValue("@FolderName", Qa.FolderName);
                    cmd.Parameters.AddWithValue("@ContactName", Qa.ContactName);
                    cmd.Parameters.AddWithValue("@Email", Qa.Email);
                    cmd.Parameters.AddWithValue("@QuoteStartNumber", Qa.QuoteStart);
                    cmd.Parameters.AddWithValue("@Header", Qa.Header);
                    cmd.Parameters.AddWithValue("@Footer", Qa.Footer);
                    cmd.Parameters.AddWithValue("@ContactNumber", Qa.Contactnumber);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                    
                }
               
            }
          
        }

        public  QuoteAdminEntity SelectQuoteAdmin(int ProjectReference,int Portfolio)
        {
            List<QuoteAdminEntity> QuoteAdmin = new List<QuoteAdminEntity>();
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, QuoteAdminProcs.QuoteAdminSelectByPortfolio, new SqlParameter[] { new SqlParameter("@ProjectReference", ProjectReference), new SqlParameter("@Portfolio", Portfolio) });
            while (dr.Read())
            {
                QuoteAdminEntity Qa=new QuoteAdminEntity
                {
                    ID=int.Parse(dr["ID"].ToString()),
                    Portfolio = int.Parse(dr["Portfolio"].ToString()),
                    Prefix=dr["Prefix"].ToString(),
                    QuoteStart = int.Parse(dr["QuoteStartPoint"].ToString()),
                    VAT=float.Parse(dr["VAT"].ToString()),
                    FolderName=dr["FolderName"].ToString(),
                    ContactName = dr["ContactName"].ToString(),
                    Email=dr["Email"].ToString(),
                    Contactnumber=dr["ContactNo"].ToString(),
                    Header=dr["QuoteHeader"].ToString(),
                    Footer=dr["QuoteFooter"].ToString()
                };
                QuoteAdmin.Add(Qa);
            }
            dr.Close();
            dr.Dispose();
            return QuoteAdmin[0];
            //return Qa;
        }

        public DataTable SelectQuoteMainDetails(int QuoteID, int ProjectReference)
        {
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "QuoteMain_SelectByQuoteID", new SqlParameter[] { new SqlParameter("@QuoteID", QuoteID), new SqlParameter("@ProjectReference", ProjectReference) }).Tables[0];
          
        }
        public int SelectID(int Potfolio)
        {
            object obj;
            obj = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.StoredProcedure,QuoteAdminProcs.QuoteAdminSelectID, new SqlParameter("@Portfolio", Potfolio));
            return Convert.ToInt32(obj);
        }
    }
}
