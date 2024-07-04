using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using Deffinity.ProgrammeManagers;
using Deffinity.ProgrammeEntitys;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using Location.DAL;
using Location.Entity;



public partial class DashboardIssues : BasePage
{
    ProgrammeDataContext operationOwners = new ProgrammeDataContext();
    DisBindings getData = new DisBindings();
    protected string Program;
    DataSet dsP = new DataSet();
    Admin ad = new Admin();
    LocationDataContext LocDataCntxt;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.Dashboard;//"Dashboard";
        if (!IsPostBack)
        {
            try
            {
                //fillviewallSelectView();

                sessionKeys.ProgrammeID = 0;
                // bindCountry();
                ddlPortfolio.DataBind();
                BindProgramme();
                getData.DdlBindSelect(ddlsubprogram, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlProjGroups.SelectedValue), "ID", "OperationsOwners", false, false, true);
                //ddlCountry.DataBind();
                //ddlCountry.Items.Insert(0, "Please select..."); 
                BindCountries();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            
       
        }
    }
    private void BindProgramme()
    {
        //var programmeLevel = (from r in operationOwners.OperationsOwners
        //                      where r.Level == 1
        //                      select new { r.ID, r.OperationsOwners }).ToList();
        //ddlProjGroups.DataSource = programmeLevel;
        //ddlProjGroups.DataTextField = "OperationsOwners";
        //ddlProjGroups.DataValueField = "ID";
        //ddlProjGroups.DataBind();
        //ddlProjGroups.Items.Insert(0, new ListItem("Please select...", "0"));
    }
    //protected void ddlprojectPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlprojectProgramme.SelectedValue == "0")
    //    {
    //        Session["Programme"] = ddlprojectProgramme.SelectedValue;
    //        //getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners order by OperationsOwners asc", "ID", "OperationsOwners", false, false);
    //        ddlSubProgramme.Items.Clear();
    //        ddlSubProgramme.Items.Insert(0, Constants.ddlDefaultBind(true));
    //    }
    //    else
    //    {
    //        Session["Programme"] = ddlprojectProgramme.SelectedValue;
    //        //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
    //        getData.DdlBindSelect(ddlSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlprojectProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
    //    }
    //    sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
    //    sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
    //    //BindFirstGrid("", "");
    //    //displaydetails();
    //    //displaydata();
    //    //BindRAG();
    //    //BindChart();
    //    //BindChart1();
    //}
    //protected void ddlSubProgramme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSubProgramme.SelectedValue == "0")
    //    {
    //        Session["SubProgramme"] = ddlSubProgramme.SelectedValue;
    //        sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
    //        sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
    //    }
    //    else
    //    {
    //        Session["SubProgramme"] = ddlSubProgramme.SelectedValue;
    //        sessionKeys.ProgrammeID = Convert.ToInt32(ddlSubProgramme.SelectedValue);
    //        sessionKeys.ProgrammeName = ddlSubProgramme.SelectedItem.Text;
    //    }
    //    Grid_Issues.DataBind();
    //    //BindFirstGrid("", "");
    //    //displaydata();
    //    //displaydetails();
    //    //BindChart();
    //    //BindChart1();
    //}
    //public void fillviewallSelectView()
    //{
    //    try
    //    {
    //        getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners where level=1 order by OperationsOwners ", "ID", "OperationsOwners", false, false, true);
    //        sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
    //        sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;

    //        Program = sessionKeys.ProgrammeID.ToString();

    //        if (ddlprojectProgramme.SelectedValue == "0")
    //        {
    //            //getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners order by OperationsOwners asc", "ID", "OperationsOwners", false, false);
    //            ddlSubProgramme.Items.Clear();
    //            ddlSubProgramme.Items.Insert(0, Constants.ddlDefaultBind(true));
    //        }
    //        else
    //        {
    //            //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
    //            getData.DdlBindSelect(ddlSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlprojectProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
    //        }
    //        Grid_Issues.DataBind();
    //        //displaydetails();
    //        //displaydata();
    //        //displaydataPage();

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}



    protected void ddlProjGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProjects.SelectedIndex = 0;
        SqlDataSourcesubprogram.SelectParameters[0].DefaultValue = ddlsubprogram.SelectedValue;
        if (ddlProjGroups.SelectedValue == "0")
        {
            Session["Programme"] = ddlProjGroups.SelectedValue;
            //getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners order by OperationsOwners asc", "ID", "OperationsOwners", false, false);
            ddlsubprogram.Items.Clear();
            ddlsubprogram.Items.Insert(0, Constants.ddlDefaultBind(true));
        }
        else
        {
            Session["Programme"] = ddlProjGroups.SelectedValue;
            //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
           // getData.DdlBindSelect(ddlsubprogram, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlProjGroups.SelectedValue), "ID", "OperationsOwners", false, false, true);
            ddlsubprogram.DataBind();
            SqlDataSourcesubprogram.SelectParameters[0].DefaultValue = ddlProjGroups.SelectedValue;
        }
        sessionKeys.ProgrammeID = Convert.ToInt32(ddlProjGroups.SelectedValue);
        sessionKeys.ProgrammeName = ddlProjGroups.SelectedItem.Text;
        //bindCountry();
        BindCountries();
        Grid_Issues.DataBind();
     
    }
    //private void bindCountry()
    //{
    //    try
    //    {
    //        LocDataCntxt = new LocationDataContext();

    //        var data = (from f in LocDataCntxt.CountryClasses
    //                    where f.Visible == 'Y'
    //                    orderby f.Country1
    //                    select new
    //                    {
    //                        f.ID,
    //                        f.Country1

    //                    }).Distinct();

    //        ddlCountry.DataSource = data.OrderBy(o => o.Country1);
    //        ddlCountry.DataTextField = "Country1";
    //        ddlCountry.DataValueField = "ID";
    //        ddlCountry.DataBind();

    //        ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    protected void ddlsubprogram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubprogram.SelectedValue == "0")
        {
            Session["SubProgramme"] = ddlsubprogram.SelectedValue;
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlProjGroups.SelectedValue);
            sessionKeys.ProgrammeName = ddlProjGroups.SelectedItem.Text;
        }
        else
        {
            Session["SubProgramme"] = ddlsubprogram.SelectedValue;
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlsubprogram.SelectedValue);
            sessionKeys.ProgrammeName = ddlsubprogram.SelectedItem.Text;
        }
        //bindCountry();
        BindCountries();
        Grid_Issues.DataBind();
        
    }

    protected void BindCountries()
    {
        ddlCountry.DataSourceID = "SqlDataSourceCountry";
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlProjGroups.DataBind();
        ddlsubprogram.Items.Clear();
        ddlsubprogram.Items.Insert(0, Constants.ddlDefaultBind(true));
        ddlProjects.DataBind();
        Grid_Issues.DataBind();          
    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindCountry();
        Grid_Issues.DataBind();
        //ddlProjGroups.SelectedIndex = 0;
        //BindGrid();
        //BindCharts();
    }
    protected void Grid_Issues_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = Grid_Issues.EditIndex;
        GridViewRow Grow = Grid_Issues.Rows[index];

        TextBox txtNotes = (TextBox)Grow.FindControl("txtNotes");
        DropDownList ddlGdStatus = (DropDownList)Grow.FindControl("ddlGdStatus");

        objDS_SelectIssues.UpdateParameters["ID"].DefaultValue = e.Keys["ID"].ToString();
        objDS_SelectIssues.UpdateParameters["Status"].DefaultValue = ddlGdStatus.SelectedValue;
        objDS_SelectIssues.UpdateParameters["Notes"].DefaultValue = txtNotes.Text.Trim();
        objDS_SelectIssues.Update();
    }
    protected void Grid_Issues_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
    }
    #region Check Permission
    //03/06/2011 by sani

    private void CheckUserRole()
    {
        if (sessionKeys.SID != 1)
        {
            int role = 0;
            role = Admin.CheckLoginUserPermission(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }
            role = Admin.GetTeamID(sessionKeys.UID);
            if (role == 3)
            {

                Disable();

            }

        }

    }
    private void Disable()
    {
        //btnUpdateIssue.Enabled = false;

    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {

            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    vis = false;
                    //  Disable();

                }
                role = Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    vis = false;

                    // Disable();

                }

            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    #endregion
    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Grid_Issues.DataBind();
    }
}
