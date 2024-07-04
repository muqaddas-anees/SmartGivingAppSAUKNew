using HealthCheckMgt.DAL;
using HealthCheckMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GridColumns1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setBackUrl();
            if (Request.QueryString["appid"] != null)
            {
                IntialInsert(int.Parse(Request.QueryString["appid"].ToString()));
                BindGrid(int.Parse(Request.QueryString["appid"].ToString()));
            }
        }
    }
    public void BindGrid(int AppId)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                var controlsList = Hdc.HealthCheck_FormControls.ToList();
                var GList = Hdc.gridColumnsVisibilities.ToList();
                var x = (from a in GList
                         where a.Appid == AppId
                         select new
                         {
                             ID = a.ColumnId,
                             Name = controlsList.Where(o => o.ControlID == a.ColumnId.Value).Select(o => o.ControlLabelName).FirstOrDefault(),
                             visibility = a.Visibility
                         }).ToList();
                GridColumns.DataSource = x;
                GridColumns.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void IntialInsert(int AppId)
    {
        try
        {
            using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
            {
                if (Hdc.gridColumnsVisibilities.Where(a => a.Appid == AppId).ToList().Count == 0)
                {
                    List<gridColumnsVisibility> Tlist = new List<gridColumnsVisibility>();
                    gridColumnsVisibility gcv = null;
                    int ParentFormID = Hdc.AppManagers.Where(a => a.ID == AppId).Select(a => a.FormID.Value).First();
                    var PanelsList = Hdc.HealthCheck_FormPanels.Where(a => a.FormID == ParentFormID && a.PanelName != "Header" && a.PanelName != "Signature Panel").ToList();
                    foreach (var Pnl in PanelsList)
                    {
                        var FormControlsList = Hdc.HealthCheck_FormControls.Where(a => a.PanelID == Pnl.PanelID).ToList();
                        foreach (var Formcntl in FormControlsList)
                        {
                            if (Formcntl.TypeOfField != "Image")
                            {
                                gcv = new gridColumnsVisibility();
                                gcv.Appid = AppId;
                                gcv.ColumnId = Formcntl.ControlID;
                                gcv.Visibility = true;
                                Tlist.Add(gcv);
                            }
                        }
                    }
                    Hdc.gridColumnsVisibilities.InsertAllOnSubmit(Tlist);
                    Hdc.SubmitChanges();
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
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int AppId=0;
             if (Request.QueryString["appid"] != null)
            {
               AppId=int.Parse(Request.QueryString["appid"].ToString());
             }
            foreach (GridViewRow item in GridColumns.Rows)
            {
                string ID = ((Label)item.FindControl("LblId")).Text.Trim();
                CheckBox checkvisibility = (CheckBox)item.FindControl("checkvisibility");
                using (HealthCheckDataContext Hdc = new HealthCheckDataContext())
                {
                    gridColumnsVisibility gcv = Hdc.gridColumnsVisibilities.Where(a => a.Appid == AppId && a.ColumnId == int.Parse(ID)).FirstOrDefault();
                    gcv.Visibility = checkvisibility.Checked;
                    Hdc.SubmitChanges();
                }
            }
            LblMsg.ForeColor = System.Drawing.Color.Green;
            LblMsg.Text = "Updated successfully.";
            BindGrid(AppId);
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridColumns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Label Lblvisibility = (Label)e.Row.FindControl("Lblvisibility");
            CheckBox checkvisibility = (CheckBox)e.Row.FindControl("checkvisibility");
            if (Lblvisibility != null && checkvisibility != null)
            {
                if (Lblvisibility.Text == "True")
                {
                    checkvisibility.Checked = true;
                }
                else
                {
                    checkvisibility.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}