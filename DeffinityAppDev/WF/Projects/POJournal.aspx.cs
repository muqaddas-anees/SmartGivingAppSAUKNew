using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using POMgt.DAL;
using POMgt.Entity;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
public partial class POJournal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Master.PageHead = "Purchase Order Management";
           
        }

    }



    protected void btnProcurementReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProcurementReport.aspx");
    }
}
