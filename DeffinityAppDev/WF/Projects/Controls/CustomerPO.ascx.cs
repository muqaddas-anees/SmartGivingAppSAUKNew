using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using POMgt.DAL;

public partial class controls_CustomerPO : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            
            BindData();
        }

    }

    private void BindData()
    {
        try
        {
            PurchaseOrderMgtDataContext PODatabases = new PurchaseOrderMgtDataContext();

            var assets = from r in PODatabases.v_CustomerPO_Details
                         where r.ID != -99
                         select r;
            if (Request.QueryString["project"] != null)
                assets = assets.Where(a => a.ProjectRef == Convert.ToInt32(Request.QueryString["project"].ToString())).Select(a => a);
            grdPODetails.DataSource = assets;
            grdPODetails.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
            PurchaseOrderMgtDataContext PODatabases = new PurchaseOrderMgtDataContext();

            var assets = from r in PODatabases.v_CustomerPO_Details
                         where r.PONumber.Contains(txtPONumber.Text.Trim())
                         select r;
            if (Request.QueryString["project"] != null)
                assets = assets.Where(a => a.ProjectRef == Convert.ToInt32(Request.QueryString["project"].ToString())).Select(a => a);
            grdPODetails.DataSource = assets;
            grdPODetails.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void grdPODetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
           
            if (e.CommandName == "Show")
            {
                if (Request.QueryString["project"] != null)
                {
                    Response.Redirect("Project_CustomerPODetails.aspx?POID=" + e.CommandArgument.ToString() + "&project=" + Request.QueryString["project"].ToString());
                }
                else
                {
                    Response.Redirect("PODatabase.aspx?POID=" + e.CommandArgument.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void imgAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["project"] != null)
            {
                Response.Redirect("Project_CustomerPODetails.aspx?New=Yes" + "&project=" + Request.QueryString["project"].ToString());
            }
            else
            {
                Response.Redirect("PODatabase.aspx?New=Yes");
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected string ChangeHoues(string GetHours)
    {
        string GetActivity = "";
        char[] comm1 = { '.' };
        string[] displayTime = GetHours.Split(comm1);


        GetActivity = displayTime[0] + ":" + displayTime[1];



        return GetActivity;
    }
}
