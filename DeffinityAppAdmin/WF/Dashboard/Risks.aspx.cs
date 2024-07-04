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

public partial class Risks : BasePage
{
    DisBindings getData = new DisBindings();
    protected string Program;
    DataSet dsP = new DataSet();
    Admin ad = new Admin();
    LocationDataContext LocDataCntxt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = Resources.DeffinityRes.Dashboard;// "Dashboard";
        
        if (!IsPostBack)
        {
            //GridBind();

            GridView9.DataBind();
            //bindCountry();
            ddlPortfolio.DataBind();
            ddlProjGroups.DataBind();
            //getData.DdlBindSelect(ddlsubprogram, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlProjGroups.SelectedValue), "ID", "OperationsOwners", false, false, true);
            ddlsubprogram.DataBind();
            BindCountries();
        }


    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GridView9.DataBind();
        //select ID,OperationsOwners from OperationsOwners where level=1 order by OperationsOwners
    }
    protected void GridView9_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView9.PageIndex = e.NewPageIndex;
        GridView9.DataBind();
    }
    protected void BindCountries()
    {
        
        SqlDataSourceCountry.SelectParameters["Program"].DefaultValue = getVal(ddlProjGroups.SelectedValue);
        SqlDataSourceCountry.SelectParameters["SubProgram"].DefaultValue = getVal(ddlsubprogram.SelectedValue);
        ddlCountry.DataSourceID = "SqlDataSourceCountry";
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
    }
    private string getVal(string val)
    {
        if (val == "Please select...")
        {
            val = "0";
        }
        return val;
    }
    protected string GetStatus(string status)
    {
        string strRet=string.Empty;
        
        return strRet;
    }
    protected string LoadRagSTatus(string NextDate)
    {
        string retVal = @"~\images\indcate_red.png";

        if (NextDate != null && NextDate != "")
        {
            DateTime dtt1 = Convert.ToDateTime(NextDate.Trim());
            DateTime dtt2 = Convert.ToDateTime(System.DateTime.Now.ToString());

            DateTime dt1 = new DateTime(dtt1.Year, dtt1.Month, dtt1.Day);
            DateTime dt2 = new DateTime(dtt2.Year, dtt2.Month, dtt2.Day);
            if (DateTime.Compare(dt1, dt2) > 0)
            {
                retVal = @"~/images/indcate_green.png";
            }
        }
        
        return retVal;

    }
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
        //SqlDataSourcesubprogram.SelectParameters[""].DefaultValue = ddlPortfolio.SelectedValue;
        BindCountries();
        //ddlPortfolio.DataBind();
        GridView9.DataBind();

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
        //if (ddlsubprogram.SelectedValue == "0")
        //{
        //    Session["SubProgramme"] = ddlsubprogram.SelectedValue;
        //    sessionKeys.ProgrammeID = Convert.ToInt32(ddlProjGroups.SelectedValue);
        //    sessionKeys.ProgrammeName = ddlProjGroups.SelectedItem.Text;
            
        //}
        //else
        //{
        //    Session["SubProgramme"] = ddlsubprogram.SelectedValue;

        //    sessionKeys.ProgrammeID = Convert.ToInt32(ddlsubprogram.SelectedValue);
        //    sessionKeys.ProgrammeName = ddlsubprogram.SelectedItem.Text;
        //}
        //bindCountry();
        BindCountries();
        //ddlPortfolio.DataBind();
        GridView9.DataBind();

    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

        //ddlProjGroups.DataBind();
        //ddlsubprogram.Items.Clear();
        //ddlsubprogram.Items.Insert(0, Constants.ddlDefaultBind(true));
        //ddlProjects.DataBind();
        //ddlPortfolio.DataBind();
        GridView9.DataBind();


    }
    protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindCountry();
        //ddlPortfolio.DataBind();
        BindCountries();
        GridView9.DataBind();
        //ddlProjGroups.SelectedIndex = 0;
        //BindGrid();
        //BindCharts();
    }

    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView9.DataBind();
    }
}