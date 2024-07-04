using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DC_InventoryManagerCustomer : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (sessionKeys.PortalUser || sessionKeys.SID == 7)
        {
            this.Page.MasterPageFile = "~/DeffinityCustomerTab.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }
}