using ClosedXML.Excel;
using Location.DAL;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ProjectMgt.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetMgt.DAL;
using UserMgt.DAL;

public partial class FileuploadPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     //   if (!IsPostBack)
       // {
            lblsessionuid.Text = sessionKeys.UID.ToString();
       // }
            linkException.NavigateUrl = string.Format("~/WF/Projects/DownloadHandler.ashx?Project={0}&download=exceptions",QueryStringValues.Project);
    }
   
}