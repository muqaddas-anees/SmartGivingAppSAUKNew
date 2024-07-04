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

public partial class controls_CART : System.Web.UI.UserControl
{
    public double TotalSP = 0;
    public double TotalUnits = 0.00;
    public double TotalQuantity = 0.00;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.ConnectionString = Constants.DBString;
        if (sessionKeys.CartID == Guid.Empty.ToString())
        {             
            Guid _guid = Guid.NewGuid();
            sessionKeys.CartID = _guid.ToString();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Guid _g = new Guid(sessionKeys.CartID);
        CartManager.ClearCart(_g);
        FillCart();
    }
    public void FillCart()
    {
        rptCart.DataBind();
    }
    protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if ((e.CommandName.ToString() == "Delete") && (e.CommandArgument.ToString() != ""))
        {
            //DeleteActivity(Convert.ToInt32(e.CommandArgument));
            CartManager.DeleteCartItem(Convert.ToInt32(e.CommandArgument));
            FillCart();
        }
    }
    protected void rptCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
     {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
            {
                TotalQuantity += Convert.ToDouble(((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[2]);
                TotalSP += Convert.ToDouble(((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[3]);
                TotalUnits += Convert.ToDouble(((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[4]);
            }
        }
    protected void btnupdate_Click(object sender, EventArgs e)
    { 
        for (int i = 0; i < rptCart.Items.Count ; i++)
        {
            int id, quantity;
            RepeaterItem rptitem=rptCart.Items[i];
            Label lblid = (Label)rptitem.FindControl("lblID");
            TextBox txtqty = (TextBox)rptitem.FindControl("txtqty");
            id = Convert.ToInt32(lblid.Text);
            quantity = Convert.ToInt32(txtqty.Text);
            CartManager.UpdateCart(id, quantity);
        }
        rptCart.DataBind();
    }
    protected void btnRequestQuote_Click(object sender, EventArgs e)
    {
        //Response.Redirect("NewOrder.aspx?type=rq", false);
    }
    protected void btnProcessOrder_Click(object sender, EventArgs e)
    {
        //Response.Redirect("NewOrder.aspx?type=po", false);
    }
}
