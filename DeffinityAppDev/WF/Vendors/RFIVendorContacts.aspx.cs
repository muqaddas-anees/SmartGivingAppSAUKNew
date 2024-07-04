using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RFI_VendorContacts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Vendor Account Information: Key Contacts";
    }
    protected void btnnext_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Vendors/RFIVendorCertifications.aspx?VendorID=" + QueryStringValues.Vendor.ToString(), true);
    }
    protected void imgbtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("RFIVendorSites.aspx?VendorID=" + QueryStringValues.Vendor.ToString(), false);
    }
}
