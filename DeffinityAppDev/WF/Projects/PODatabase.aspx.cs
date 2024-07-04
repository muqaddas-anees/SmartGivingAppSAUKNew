using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using POMgt.DAL;
using POMgt.Entity;
using ProjectMgt.DAL;
using ProjectMgt.Entity;

public partial class PODatabase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Purchase Order Management";


            }

          
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
   

    
}
