using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.DAL;
using InventoryMgt.Entity;

public partial class controls_InventoryConfigureFields : System.Web.UI.UserControl
{
    PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> IC = null;
    InventoryRepository<InventoryFieldsConfig> INF = null;
    InventoryRepository<InventoryFieldsConfigData> IND = null;
    InventoryFieldsConfig in_f = null;
    List<InventoryFieldsConfig> in_fList = null;
    InventoryFieldsConfigData in_d = null;
    List<InventoryFieldsConfigData> in_dList = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCustomer();
        }
    }
    private void BindCustomer()
    {
        IC = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
        var iclist = IC.GetAll().OrderBy(a => a.PortFolio).ToList();
        ddlCustomer.DataSource = iclist;
        ddlCustomer.DataTextField = "PortFolio";
        ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select...", "0"));
        if (sessionKeys.PortfolioID > 0)
        {
            ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            
        }
    }
    public void bindgrid(int cid)
    {
        try
        {
            INF = new InventoryRepository<InventoryFieldsConfig>();
            IND = new InventoryRepository<InventoryFieldsConfigData>();
            var xlist = INF.GetAll().Where(a => a.CustomerId == cid).ToList();
            var ylist = IND.GetAll().ToList();
            var xy = (from a in ylist
                      select new
                      {
                          ID = xlist.Where(p => p.DefaultName == a.DefaultName && p.CustomerId == cid).Select(p => p.id).FirstOrDefault() == 0 ? 0 : xlist.Where(p => p.DefaultName == a.DefaultName && p.CustomerId == cid).Select(p => p.id).FirstOrDefault(),
                          DefaultName = a.DefaultName,
                          IsVisible = xlist.Where(p => p.DefaultName == a.DefaultName && p.CustomerId == cid).Select(p => p.IsVisible).FirstOrDefault() == null ? true : xlist.Where(p => p.DefaultName == a.DefaultName && p.CustomerId == cid).Select(p => p.IsVisible).FirstOrDefault(),
                      }).ToList();
            gvInventoryConfig.DataSource = xy;
            gvInventoryConfig.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCustomer.SelectedValue != "0")
        {
            bindgrid(int.Parse(ddlCustomer.SelectedValue));
        }
    }
    protected void gvInventoryConfig_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvInventoryConfig.EditIndex = e.NewEditIndex;
        bindgrid(int.Parse(ddlCustomer.SelectedValue));
    }
    protected void gvInventoryConfig_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvInventoryConfig.EditIndex = -1;
        bindgrid(int.Parse(ddlCustomer.SelectedValue));
    }
    protected void gvInventoryConfig_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvInventoryConfig.EditIndex = -1;
        bindgrid(int.Parse(ddlCustomer.SelectedValue));
    }
    protected void gvInventoryConfig_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                INF = new InventoryRepository<InventoryFieldsConfig>();
                in_f = new InventoryFieldsConfig();
                int CustomerId = int.Parse(ddlCustomer.SelectedValue);
                int id = Convert.ToInt32(e.CommandArgument);
                int i = gvInventoryConfig.EditIndex;
                GridViewRow row = gvInventoryConfig.Rows[i];
                string Name = ((Label)row.FindControl("lblDefaultFieldEdit")).Text;
                bool isVisible = ((DropDownList)row.FindControl("ddlIsVisible")).SelectedValue == "Yes" ? true : false;
                if (id == 0)
                {
                    //insert
                    in_f.CustomerId = CustomerId;
                    in_f.DefaultName = Name;
                    in_f.IsVisible = isVisible;
                    INF.Add(in_f);
                    lblmsg.Text = "Inserted Successfully.";
                    bindgrid(int.Parse(ddlCustomer.SelectedValue));
                }
                else
                {
                    //update
                    in_f = INF.GetAll().Where(o => o.id == id).FirstOrDefault();
                    //in_f.CustomerId = CustomerId;
                    //in_f.DefaultName = Name;
                    in_f.IsVisible = isVisible;
                    INF.Edit(in_f);
                    lblmsg.Text = "Updated Successfully.";
                    bindgrid(int.Parse(ddlCustomer.SelectedValue));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvInventoryConfig_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlIsVisible = (DropDownList)e.Row.FindControl("ddlIsVisible");
                if (ddlIsVisible != null)
                {
                    HiddenField hfIsVisible = (HiddenField)e.Row.FindControl("hfIsVisible");
                    ddlIsVisible.SelectedValue = hfIsVisible.Value == "True" ? "Yes" : "No";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}