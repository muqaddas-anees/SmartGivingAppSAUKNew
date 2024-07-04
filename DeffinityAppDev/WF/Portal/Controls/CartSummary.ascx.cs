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

public partial class controls_CartSummary : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (sessionKeys.CartID == Guid.Empty.ToString())
        {
            Guid _guid = Guid.NewGuid();
            sessionKeys.CartID = _guid.ToString();
        }
        Guid _userGuid = new Guid(sessionKeys.CartID);
        FillSummary(_userGuid);
    }
    public void FillSummary(Guid _userGuid)
    {
        DataRow _dr = CartManager.GetCartSummary(_userGuid);
        lblQty.Text = _dr["Qty"].ToString();
        double value = double.Parse(_dr["Total"].ToString());
        string result = value.ToString("f2");
        lblTotal.Text = result;
    }
}
