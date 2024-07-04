using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;

/// <summary>
/// Summary description for AssetDataBinding
/// </summary>
public class AssetDataBinding
{
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    DisBindings getData = new DisBindings();
    DataSet ds = new DataSet();
	public AssetDataBinding()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet AdminAssetGrid()
    {
        if (ds != null)
        {
            ds.Clear();
        }
        cmd = db.GetStoredProcCommand("DN_AdminAssetsdisplay");
        ds = db.ExecuteDataSet(cmd);
        cmd.Dispose();
        return ds;
    }
}
