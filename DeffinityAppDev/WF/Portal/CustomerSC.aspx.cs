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
public partial class CustomerSC : System.Web.UI.Page
{
   // int @CustomerID = 34;
    string  Portfolio = "";
    //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[sessionKeys.InstanceName].ToString());
    CartEntity _CartEntity = new CartEntity();
    protected void Page_Load(object sender, EventArgs e)
    {
      
        SqlDataSource1.ConnectionString = Constants.DBString;
        lblError.Text = "";
        if (!IsPostBack)
        {
            sessionKeys.IncidentID = 0;
            BindCategory();
            AutoCompleteExtender1.ContextKey = sessionKeys.PortfolioID.ToString();
        }
       
    }
  
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //if customer log's in
        if (sessionKeys.SID == 7)
        {
            if (ddlCategory.Items.Count <= 1)
            {
                lblError.Text = "No catalogue set up. Please discuss this with the system owner";
            }
        }
    }
    private void BindCategory()
    {
        ddlCategory.DataSourceID = "DS_Category";
        ddlCategory.DataBind();
    }
    public static string GetImageUrl(Guid a_gId, ImageManager.ThumbnailSize? a_oThumbSize)
    {
        //return GetImageUrl(a_gId, a_oThumbSize, true);

        ImageManager.ImageType eImageType = ImageManager.ImageType.OriginalData;
        if (a_oThumbSize.HasValue)
        {
            switch (a_oThumbSize.Value)
            {
                case ImageManager.ThumbnailSize.MediumSmaller: eImageType = ImageManager.ImageType.ThumbNails; break;
            }
        }
        else
        {
            eImageType = ImageManager.ImageType.OriginalData;
        }

        return "~/WF/UploadData/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png";// +"/" + eImageType.ToString() + "/" + a_gId.ToString() + ".png"; 
        
    }
    public bool CheckImageVisibility(Guid a_guid)
    {
        bool _visible = false;
        if (a_guid.ToString() != "00000000-0000-0000-0000-000000000000")
        {
            _visible = true;
        }
        return _visible;
    }
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int selectedRow = e.NewSelectedIndex;
        //int ID = Convert.ToInt32(e.CommandArgument.ToString());
        GridViewRow Row = GridView1.Rows[selectedRow];
        string Qty = ((TextBox)Row.FindControl("txtQty")).Text;
        int ID = Convert.ToInt32(((HiddenField)Row.FindControl("hdnID")).Value);
        _CartEntity.ProductID = ID;
        Guid _g = new Guid(sessionKeys.CartID);
        _CartEntity.UserID = _g;
        _CartEntity.TypeID = Convert.ToInt32(((HiddenField)Row.FindControl("HD_ServiceType")).Value);
        //_CartEntity.TypeID = Convert.ToInt32(ddlSelect.SelectedValue);
        if (Qty == "")
            _CartEntity.Quantity = 1;
        else
            _CartEntity.Quantity = Convert.ToInt32(Qty);
        CartManager.AddToCart(_CartEntity);
        ((TextBox)Row.FindControl("txtQty")).Text ="1";
        CART1.FillSummary(_g);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //if (QueryStringValues.Project > 0)
        //{
        //    Response.Redirect(string.Format("~/newOrder.aspx?Project={0}", QueryStringValues.Project), false);
        //}
        //else 
        //{
        //    Response.Redirect("~/newOrder.aspx", false);
        //}
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubcategory();
        GridView1.DataBind();
        
    }
    private void bindSubcategory()
    {
        DS_Category.DataBind();
        ddlSubCategory.DataSource = DS_SubCategory;
        ddlSubCategory.DataValueField = "ID";
        ddlSubCategory.DataTextField = "CategoryName";
        ddlSubCategory.DataBind();
        
    }
       private int GetCatagoryID()
    {
        int retval = 0;
        try
        {
            if (ddlCategory.Visible == true)
            {
                retval = int.Parse(ddlCategory.SelectedValue);
            }
        }
        catch (Exception ex)
        {

            retval = 0;
        }
        return retval;
    }
    private int GetSubCatagoryID()
    {
        int retval = 0;
        try
        {
            if (ddlSubCategory.Visible == true)
            {
                retval = int.Parse(ddlSubCategory.SelectedValue);
            }
        }
        catch (Exception ex)
        {

            retval = 0;
        }
        return retval;
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.DataBind();
       // GridView1.DataSourceID = "Obj_Description";
        //GridView1.DataBind();
    }
    protected void ddlSelect_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridView1.DataBind();

    }
}
