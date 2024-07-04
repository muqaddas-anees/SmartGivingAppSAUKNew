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
using Deffinity.ServiceCatalogManager;


public partial class ManageCatalogues : System.Web.UI.Page
{
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //Master.PageHead = "Customer Admin";
        ServiceCatalogue1.SetPageType = 1;

    }
    
}


  

