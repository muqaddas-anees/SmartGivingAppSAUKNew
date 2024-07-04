using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for CustomerFeedback
/// </summary>
/// 
namespace CustomerfeedbackNms
{
    public class CustomerFeedbackClass
    {
        Database db = DatabaseFactory.CreateDatabase("DBstring");

        public bool InsertOrSendCustomerFeedback(int PortfilioId, int Pref, int CaseId, bool satisfy, string waystoimprove, string nonperformind, bool discus,int loggedby)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_INSERT_CUSTSATISFACTION");
                db.AddInParameter(cmd, "@PORTFOLIOID", DbType.Int32, PortfilioId);
                db.AddInParameter(cmd, "@PROJECTID", DbType.Int32, Pref);
                db.AddInParameter(cmd, "@CASEID", DbType.Int32, 0);
                db.AddInParameter(cmd, "@SATISFIED", DbType.Boolean, satisfy);
                db.AddInParameter(cmd, "@WAYSTOIMPROVE", DbType.String, waystoimprove);
                db.AddInParameter(cmd, "@NONPERFORMINGINDIVIDUALS", DbType.String, nonperformind);
                db.AddInParameter(cmd, "@DISCUSS", DbType.Boolean, discus);
                db.AddInParameter(cmd, "@LoggedBy", DbType.Int32, loggedby);
                db.ExecuteNonQuery(cmd);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return true;
        }
        public DataSet Getfeedbackdetails()
        {           
            //int ID,PortfolioID,Satisfied,Discuss;
            //string  WaysToImprove,NonPerformingIndividuals;
            //DateTime DateLogged;
            SqlConnection cn = new SqlConnection(Constants.DBString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("Deffinity_GetFeedBackDetails",cn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);            
            cmd.ExecuteNonQuery();
            da.Fill(ds);
            return ds;

        }
    }
}