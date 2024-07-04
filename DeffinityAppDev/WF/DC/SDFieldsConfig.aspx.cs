using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using DC.BAL;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using UserMgt.BAL;


public partial class SDFieldsConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Deffinity.systemdefaults.ClearCategoryName();
                Deffinity.systemdefaults.ClearSubCategoryName();
                Deffinity.systemdefaults.ClearRequesterName();
                Deffinity.systemdefaults.ClearTypeofRequestName();
                //Master.PageHead = "Customer Admin";
                if (sessionKeys.PortfolioID > 0)
                {
                     FLSFieldsConfigBAL.InsertConfigData(sessionKeys.PortfolioID);
                }
                BindGrid();
                // BindlistView();
                // BindRightlistView();
                try
                {
                    if (Request.QueryString["change"] != null)
                    {
                        PortfolioDdlCtr1.Visible = true;

                        gvConfig.Columns[8].Visible = true;
                    }
                }
                catch(Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        popIssues.Hide();
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    public void BindlistView()
    {
        try 
        {
            using (DCDataContext db = new DCDataContext())
            {
                var x = db.FLSFieldsConfigs.Where(f => f.CustomerID == sessionKeys.PortfolioID && (f.Alignment == "Left" || f.Alignment == null)).OrderBy(f => f.Position).ToList();
                gridlist.DataSource = x;
                gridlist.DataValueField = "ID";
                gridlist.DataTextField = "DefaultName";
                gridlist.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindRightlistView()
    {
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                var x = db.FLSFieldsConfigs.Where(f => f.CustomerID == sessionKeys.PortfolioID && f.Alignment == "Right").OrderBy(f => f.Position).ToList();
                RightFieldslistBox.DataSource = x;
                RightFieldslistBox.DataValueField = "ID";
                RightFieldslistBox.DataTextField = "DefaultName";
                RightFieldslistBox.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindGrid()
    {
        try
        {
            using (DCDataContext db = new DCDataContext())
            {
                gvConfig.DataSource = db.FLSFieldsConfigs.Where(f => f.CustomerID == sessionKeys.PortfolioID).OrderBy(f => f.DefaultName).ToList();
                gvConfig.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #region "Grid events"

    protected void gvConfig_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvConfig.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvConfig_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvConfig.EditIndex = -1;
        BindGrid();
    }

    protected void gvConfig_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvConfig.EditIndex = -1;
        BindGrid();
    }

    protected void gvConfig_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                int i = gvConfig.EditIndex;
                GridViewRow row = gvConfig.Rows[i];

                string instanceName = ((TextBox)row.FindControl("txtInstanceName")).Text;
                bool isVisible = ((DropDownList)row.FindControl("ddlIsVisible")).SelectedValue == "Yes" ? true : false;
                bool isMandatory = ((DropDownList)row.FindControl("ddlIsMandatory")).SelectedValue == "Yes" ? true : false;
                string defaultValue = ((TextBox)row.FindControl("txtDefaultValue")).Text;

                string Alignment = ((DropDownList)row.FindControl("ddlAlignment")).SelectedItem.ToString();

                using (DCDataContext db = new DCDataContext())
                {
                    var configData = db.FLSFieldsConfigs.Where(f => f.ID == id).FirstOrDefault();
                    if (configData != null)
                    {
                        configData.InstanceName = instanceName;
                        configData.IsVisible = isVisible;
                        configData.IsMandatory = isMandatory;
                        configData.DefaultValue = defaultValue;
                        configData.Alignment = Alignment;
                        db.SubmitChanges();

                    }
                }
            }
            if (e.CommandName == "Copy")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                FLSFieldsConfig flsFieldsConfigById = FLSFieldsConfigBAL.GetFLSFieldsConfigByID(id);

                using (PortfolioDataContext db = new PortfolioDataContext())
                {
                    var customerList = db.ProjectPortfolios.ToList();
                    foreach (var item in customerList)
                    {
                        FLSFieldsConfigBAL.InsertConfigData(item.ID);
                    }
                    if (flsFieldsConfigById != null)
                    {
                        using (DCDataContext dc = new DCDataContext())
                        {
                            foreach (var item in customerList)
                            {
                                if (flsFieldsConfigById.CustomerID != item.ID)
                                {
                                    FLSFieldsConfig flsFieldsConfigByName = dc.FLSFieldsConfigs.Where(f => f.DefaultName == flsFieldsConfigById.DefaultName && f.CustomerID == item.ID).FirstOrDefault();//FLSFieldsConfigBAL.GetFLSFieldsConfigByDefaultName(flsFieldsConfigById.DefaultName, item.ID);
                                    if (flsFieldsConfigByName != null)
                                    {
                                        flsFieldsConfigByName.InstanceName = flsFieldsConfigById.InstanceName;
                                        flsFieldsConfigByName.IsVisible = flsFieldsConfigById.IsVisible;
                                        flsFieldsConfigByName.IsMandatory = flsFieldsConfigById.IsMandatory;
                                        flsFieldsConfigByName.DefaultValue = flsFieldsConfigById.DefaultValue;
                                        flsFieldsConfigByName.Alignment = flsFieldsConfigById.Alignment;
                                        dc.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected void gvConfig_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblalignment = (Label)e.Row.FindControl("lblalignment");
                if (lblalignment != null)
                {
                    if (lblalignment.Text == "")
                    {
                        lblalignment.Text = "Left";
                    }
                }

                DropDownList ddlIsVisible = (DropDownList)e.Row.FindControl("ddlIsVisible");
                DropDownList ddlIsMandatory = (DropDownList)e.Row.FindControl("ddlIsMandatory");
                if (ddlIsVisible != null)
                {

                    HiddenField hfIsVisible = (HiddenField)e.Row.FindControl("hfIsVisible");
                    ddlIsVisible.SelectedValue = hfIsVisible.Value == "True" ? "Yes" : "No";

                    HiddenField hfIsMandatory = (HiddenField)e.Row.FindControl("hfIsMandatory");
                    ddlIsMandatory.SelectedValue = hfIsMandatory.Value == "True" ? "Yes" : "No";
                }

                DropDownList ddlAlignment = (DropDownList)e.Row.FindControl("ddlAlignment");
                if (ddlAlignment != null)
                {
                    HiddenField hfalignment = (HiddenField)e.Row.FindControl("hfIsalignment");
                    if (hfalignment.Value == null || hfalignment.Value == "")
                    {
                        ddlAlignment.SelectedValue = "left";
                    }
                    else
                    {
                        if (hfalignment.Value == "Left")
                        {
                            ddlAlignment.SelectedValue = "left";
                        }
                        else 
                        {
                            ddlAlignment.SelectedValue = "right";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion

    protected void Btnappply_Click(object sender, EventArgs e)
    {
        string Ids = string.Empty;
        try
        {
            FLSFieldsConfig FlsConfig = null;
            if (gvConfig.Rows.Count > 0)
            {
                for (int i = 0; i < gvConfig.Rows.Count; i++)
                {
                    GridViewRow row = gvConfig.Rows[i];
                    CheckBox chkbox1 = (CheckBox)row.FindControl("CheckboxAlignment");
                    if (chkbox1.Checked)
                    {
                        if (Ids == string.Empty)
                        {
                            Ids = ((Label)row.FindControl("lblCheckbox")).Text;
                        }
                        else
                        {
                            Ids = Ids + "," + ((Label)row.FindControl("lblCheckbox")).Text;
                        }
                    }
                }
                if (!Ids.Contains(','))
                {
                    Ids = Ids + ",";
                }
                if (Ids != string.Empty)
                {
                    string[] ArrayIds = Ids.Split(',');
                    for (int i = 0; i <= ArrayIds.Length - 1; i++)
                    {
                        if (ArrayIds[i] != "")
                        {
                            using (DCDataContext Dc = new DCDataContext())
                            {
                                FlsConfig = new FLSFieldsConfig();
                                FlsConfig = Dc.FLSFieldsConfigs.Where(a => a.ID == int.Parse(ArrayIds[i])).FirstOrDefault();
                                FlsConfig.Alignment = ddlAlignment.SelectedItem.ToString();
                                Dc.SubmitChanges();
                            }
                        }
                    }
                }
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ImgConfig_Click(object sender, EventArgs e)
    {
        try 
        {
            BindlistView();
            BindRightlistView();
            popIssues.Show();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}