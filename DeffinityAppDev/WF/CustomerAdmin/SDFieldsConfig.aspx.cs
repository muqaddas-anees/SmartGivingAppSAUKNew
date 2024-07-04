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
public partial class DC_SDFieldsConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Master.PageHead = "Customer Admin";
                if (sessionKeys.PortfolioID > 0)
                {
                     FLSFieldsConfigBAL.InsertConfigData(sessionKeys.PortfolioID);
                }
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindGrid()
    {
        using (DCDataContext db = new DCDataContext())
        {
            gvConfig.DataSource = db.FLSFieldsConfigs.Where(f => f.CustomerID == sessionKeys.PortfolioID).OrderBy(o=>o.DefaultName).ToList();
            gvConfig.DataBind();
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

                using (DCDataContext db = new DCDataContext())
                {
                    var configData = db.FLSFieldsConfigs.Where(f => f.ID == id).FirstOrDefault();
                    if (configData != null)
                    {
                        configData.InstanceName = instanceName;
                        configData.IsVisible = isVisible;
                        configData.IsMandatory = isMandatory;
                        configData.DefaultValue = defaultValue;
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
                DropDownList ddlIsVisible = (DropDownList)e.Row.FindControl("ddlIsVisible");
                DropDownList ddlIsMandatory = (DropDownList)e.Row.FindControl("ddlIsMandatory");
                if (ddlIsVisible != null)
                {

                    HiddenField hfIsVisible = (HiddenField)e.Row.FindControl("hfIsVisible");
                    ddlIsVisible.SelectedValue = hfIsVisible.Value == "True" ? "Yes" : "No";

                    HiddenField hfIsMandatory = (HiddenField)e.Row.FindControl("hfIsMandatory");
                    ddlIsMandatory.SelectedValue = hfIsMandatory.Value == "True" ? "Yes" : "No";
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    #endregion
   
}