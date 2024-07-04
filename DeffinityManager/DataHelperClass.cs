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
using System.Text;

/// <summary>
/// Summary description for DataHelperClass
/// </summary>
public partial class DataHelperClass
{
    public static int AddPortfolioDepartment(string deptName)
    {
        return DataInsertHelper("Insert Into PortfolioDepartment Values('" + deptName + "')");
    }

    public static int AddSLACategory(string deptName)
    {
        return DataInsertHelper("Insert Into ProjectCategory Values('" + deptName + "')");
    }

    public static int Addemail(string sqlInsertStatement)
    {
        return DataInsertHelper(sqlInsertStatement);
    }

    public static int GenericInsertHelper(string sqlInsertStatement)
    {
        return DataInsertHelper(sqlInsertStatement);
    }

    private static int DataInsertHelper(string insertStatement)
    {
        int errorNumber = 0;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(insertStatement, conn))
                {
                    int i = cmd.ExecuteNonQuery();
                    errorNumber = (i > 0) ? 0 : 1;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            if (ex.Message.Contains("UNIQUE KEY constraint"))
                errorNumber = 1;
            else
                errorNumber = 2;
        }
        return errorNumber;
    }
}
