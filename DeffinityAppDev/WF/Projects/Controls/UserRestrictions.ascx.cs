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
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class controls_UserRestrictions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRestrict_Click(object sender, EventArgs e)
    {
        //insert into restriction table
        //DEFFINITY_INSERT_RESTRICTEDUSERS sp
        Database db = DatabaseFactory.CreateDatabase("DBstring");
        try
        {

            DbCommand cmd = db.GetStoredProcCommand("DEFFINITY_INSERT_RESTRICTEDUSERS ");
            db.AddInParameter(cmd, "@UserId", DbType.Int32, Convert.ToInt32(ddlResource.SelectedValue));
            db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, Convert.ToInt32(Request.QueryString["Project"]));
            db.ExecuteNonQuery(cmd);
            cmd.Dispose();
            GridView2.DataBind();
        }
        catch (Exception eX)
        {
            LogExceptions.LogException(eX.Message);
        }
    }
}
