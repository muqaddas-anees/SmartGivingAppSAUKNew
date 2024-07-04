using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Deffinity.BLL;
using Deffinity.BE;
public partial class controls_VendorRef : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryStringValues.Vendor != 0)
        {
            RFI_Vendor _rfiVendor = RFI_Vendor_SVC.Select(QueryStringValues.Vendor);
            lblVendor.InnerText = _rfiVendor.CONTRACTORNAME;
        }
        else
            lblVendor.InnerText = " New ";
        
    }


    public string VendorName
    {
        get
        {
            return lblVendor.InnerText;
        }
        set
        {
            lblVendor.InnerText = value;
        }
    }
}
