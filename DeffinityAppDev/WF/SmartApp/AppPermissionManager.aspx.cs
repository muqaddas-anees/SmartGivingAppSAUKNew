using InventoryMgt.DAL;
using InventoryMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using HealthCheckMgt.BAL;
using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;

public partial class App_AppPermissionManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                setBackUrl();
                bindgrid();
                BindUsers();
                
                ddlPermissions.DataSource = InventoryStatus();
                ddlPermissions.DataTextField = "text";
                ddlPermissions.DataValueField = "value";
                ddlPermissions.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void setBackUrl()
    {
        //string retval = string.Empty;
        if (Request.QueryString["appid"] != null)
        {
            string appID = Request.QueryString["appid"].ToString();
            link_back.NavigateUrl = "AppFormList.aspx" + "?appid=" + appID;
        }
        else
        {
            link_back.NavigateUrl = "AppManager.aspx";
        }
        //return retval;
    }
    public void BindUsers()
    {
        try
        {
            using (UserDataContext Udc = new UserDataContext())
            {
                using (InventoryDataContext Idc = new InventoryDataContext())
                {
                    var FilterCont = new List<int> { 1, 2, 3, 4, 9 };

                    var x = Udc.Contractors.Where(a => a.Status == "Active" && FilterCont.Contains(a.SID.Value)).OrderBy(a => a.ContractorName).ToList();
                    ddlusers.DataSource = x;
                    ddlusers.DataTextField = "ContractorName";
                    ddlusers.DataValueField = "ID";
                    ddlusers.DataBind();
                    ddlusers.Items.Insert(0, new ListItem("Please select..", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void bindgrid()
    {
        try
        {
            using (UserDataContext Udc = new UserDataContext())
            {
                using (HealthCheckDataContext Idc = new HealthCheckDataContext())
                {
                    var Clist = Udc.Contractors.ToList();
                    var Slist = InventoryStatus();
                   
                    var In_PList = Idc.App_Permissions.ToList();

                    int appid = 0;
                    if (Request.QueryString["appid"] != null)
                    {
                        appid = Convert.ToInt32(Request.QueryString["appid"]);
                        In_PList = In_PList.Where(a => a.AppId == appid).ToList();
                    }

                    var x = (from a in In_PList
                             select new
                             {
                                 Cname = Clist.Where(o => o.ID == a.UserId).Select(o => o.ContractorName).FirstOrDefault(),
                                 Permission = Slist.Where(o => o.Value == a.PermissionId.ToString()).Select(o => o.Text).FirstOrDefault(),
                                 Id = a.id
                             }).ToList();
                    GridUserPermission.DataSource = x;
                    GridUserPermission.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public List<System.Web.UI.WebControls.ListItem> InventoryStatus()
    {
        List<System.Web.UI.WebControls.ListItem> li = new List<System.Web.UI.WebControls.ListItem>();
        li.Add(new System.Web.UI.WebControls.ListItem("Please select...", "0"));
        li.Add(new System.Web.UI.WebControls.ListItem("Administrator", "1"));
        li.Add(new System.Web.UI.WebControls.ListItem("Read Only", "2"));
        li.Add(new System.Web.UI.WebControls.ListItem("Hide", "3"));
        li.Add(new System.Web.UI.WebControls.ListItem("Manage", "4"));
        return li;
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            using (HealthCheckDataContext Idc = new HealthCheckDataContext())
            {

                int appid = 0;
                if (Request.QueryString["appid"] != null)
                {
                    appid = Convert.ToInt32(Request.QueryString["appid"]);
                }

                App_Permission In_P = Idc.App_Permissions.Where(a => a.UserId == int.Parse(ddlusers.SelectedValue)).FirstOrDefault();

                if (appid > 0)
                {
                    In_P = Idc.App_Permissions.Where(a => a.UserId == int.Parse(ddlusers.SelectedValue) && a.AppId == appid).FirstOrDefault();
                }

                if (In_P == null)
                {
                    App_Permission In_PNew = new App_Permission();
                    In_PNew.UserId = int.Parse(ddlusers.SelectedValue);
                    In_PNew.PermissionId = int.Parse(ddlPermissions.SelectedValue);

                   
                    if (Request.QueryString["appid"] != null)
                    {
                        appid = Convert.ToInt32(Request.QueryString["appid"]);
                        In_PNew.AppId = appid;
                    }

                    Idc.App_Permissions.InsertOnSubmit(In_PNew);
                    Idc.SubmitChanges();
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Permission added successfully.";
                }
                else
                {
                    In_P.PermissionId = int.Parse(ddlPermissions.SelectedValue);
                    Idc.SubmitChanges();
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Permission updated successfully.";
                }
            }
            bindgrid();
            ddlPermissions.SelectedValue = "0";
            ddlusers.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void ddlusers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlusers.SelectedValue != "0")
            {
                using (HealthCheckDataContext Idc = new HealthCheckDataContext())
                {
                    int appid = 0;
                    var x = Idc.App_Permissions.FirstOrDefault();
                    if (Request.QueryString["appid"] == null)
                    {
                        x = Idc.App_Permissions.Where(a => a.UserId == int.Parse(ddlusers.SelectedValue)).FirstOrDefault();
                    }
                    else
                    {
                        appid = Convert.ToInt32(Request.QueryString["appid"]);
                        x = Idc.App_Permissions.Where(a => a.UserId == int.Parse(ddlusers.SelectedValue) && a.AppId == appid).FirstOrDefault();
                    }
                    if (x != null)
                    {
                        ddlPermissions.SelectedValue = x.PermissionId.ToString();
                    }
                    else
                    {
                        ddlPermissions.SelectedValue = "0";
                    }
                }
            }
            else
            {
                ddlPermissions.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
  
    protected void GridUserPermission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                int Id = int.Parse(e.CommandArgument.ToString());
                using (HealthCheckDataContext Idc = new HealthCheckDataContext())
                {
                    App_Permission In_p = Idc.App_Permissions.Where(a => a.id == Id).FirstOrDefault();
                    Idc.App_Permissions.DeleteOnSubmit(In_p);
                    Idc.SubmitChanges();
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Text = "Permission deleted successfully.";
                    bindgrid();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string GetCssClass(int t)
    {
        string CssClass = string.Empty;

        if (t == 0)
        {
            CssClass = "current1";
        }
        return CssClass;
    }
    protected void GridUserPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}