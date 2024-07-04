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
/// Summary description for CustomerHome
/// </summary>
namespace Deffinity.CustomerManager
{
    public class CustomerHome
    {

        public static SqlDataReader DisplayRag(int ProjectStatus,int PortfolioID)
        {
            return SqlHelper.ExecuteReader(Constants.DBString, CommandType.StoredProcedure, "DN_DisplayCustomerPage", new SqlParameter("@status", ProjectStatus), new SqlParameter("@Portfolio", PortfolioID));
        }

        
    }
    
}
