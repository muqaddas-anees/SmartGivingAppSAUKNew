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
using System.Data.SqlClient;
using System.IO;
public partial class CustomerProjectDocuments : BasePage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

       // Master.PageHead = Resources.DeffinityRes.CustomerPortal;//"Customer Portal ";
    }
 
}
