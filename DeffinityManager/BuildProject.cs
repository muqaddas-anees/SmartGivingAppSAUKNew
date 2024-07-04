using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for BuildProject
/// </summary>
public class BuildProject
{
    Database db;
    DbCommand cmd;
	public BuildProject()
	{
        
	}

    public void VarianceApprove(int pref,int id, bool approve)
    {
        try
        {
            db = DatabaseFactory.CreateDatabase("DBstring");            
            cmd = db.GetStoredProcCommand("DN_VariationApprove");
            db.AddInParameter(cmd, "@Approved", DbType.Boolean, approve);
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Build project class -- Approve variance");
        }
    
    }
}
