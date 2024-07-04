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
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;

public partial class Tasks : System.Web.UI.Page
{
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    DisBindings getdata = new DisBindings();
    DataTable dt = new DataTable();
    static bool check;
    bool retVal;
    bool btnTemp = false;
    
    //protected string strvalurl;
    //protected string strvalurltasks;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Dashboard";
        //to allow dash board users
            
        if (!Page.IsPostBack)
        {
            
            try
            {
                
                //if dashboard view update status button visiblility is false
                if (sessionKeys.SID == 6)
                {                    
                    btnUpdateStatus1.Visible = false;
                    btnTemp = true;
                }
                //ddlProjectOwner
                //ddlContractor.DataBind();Project_AssignedTo
                ddlPortfolio.DataBind();
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedResource",
                    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
                ddlContractor.DataSource = dt;
                ddlContractor.DataTextField = "ContractorName";
                ddlContractor.DataValueField = "ID";
                ddlContractor.DataBind();
                ddlContractor.Items.Insert(0, new ListItem("Please select...", "0"));
                //getdata.DdlBindSelect(ddlContractor, "DN_SelectContractors", "ID", "ContractorName", true, true);
                getdata.DdlBindSelect(ddlSite, "DN_ProjectSites", "ID", "Site", true, true);
                //getdata.DdlBindSelect(ddlProjectOwner, "DN_SelectOwners", "ID", "OperationsOwners", true, true);
                getdata.DdlBindSelect(ddlStatus, "Select ID,Status from ItemStatus", "ID", "Status", false, true);
               // getdata.DdlBindSelect(ddlProjects, "AMPS_Projects", "ProjectReference", "ProjectTitle", true, true);
                ddlProjectOwner.DataBind();
                ddlProjectOwnerSub.DataBind();
                //getdata.DdlBindSelect(ddlProjectOwnerSub, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlProjectOwner.SelectedValue), "ID", "OperationsOwners", false, false, true);
               
                BindCountries();
                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();
               
                if (GridView1.Rows.Count > 0)
                {
                    //if dashboard viewer has been loged in
                    if (!btnTemp)
                    {
                        btnUpdateStatus1.Visible = false;
                    }
                }
                else
                {
                    btnUpdateStatus1.Visible = false;
                }

                dt.Clear();

                
                check = false;

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        

        
    }
    protected void ddlProjectOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlProjectOwner.SelectedValue == "0")
        //{
        //    //Session["SubProgramme"] = ddlsubprogram.SelectedValue;
        //    //sessionKeys.ProgrammeID = Convert.ToInt32(ddlProjGroups.SelectedValue);
        //    //sessionKeys.ProgrammeName = ddlProjGroups.SelectedItem.Text;
        //}
        //else
        //{
        //    //Session["SubProgramme"] = ddlsubprogram.SelectedValue;
        //    //sessionKeys.ProgrammeID = Convert.ToInt32(ddlsubprogram.SelectedValue);
        //    //sessionKeys.ProgrammeName = ddlsubprogram.SelectedItem.Text;
        //}
        //bindCountry();

        BindCountries();
        ddlProjectOwnerSub.DataBind();
        //getdata.DdlBindSelect(ddlProjectOwnerSub, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlProjectOwner.SelectedValue), "ID", "OperationsOwners", false, false, true);
        //Grid_Issues.DataBind();

    }
    protected void ddlProjectOwnerSub_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindCountries();
    }
    protected void BindCountries()
    {

        SqlDataSourceCountry.SelectParameters["Program"].DefaultValue = getVal(ddlProjectOwner.SelectedValue);
        SqlDataSourceCountry.SelectParameters["SubProgram"].DefaultValue = getVal(ddlProjectOwnerSub.SelectedValue);
        ddlCountry.DataSourceID = "SqlDataSourceCountry";
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
    }
    string Date;
    protected void btnReport_Click(object sender, ImageClickEventArgs e)
    {

        //if (txtDate.Text == "")
        //{
        //    Date = "mydate";
        //}
        //else
        //{
        //    Date = txtDate.Text;
        //}

        //string url = string.Format("<script>javascript:window.open('Tasks_report.aspx?Date={0}&Site={1}&Contractor={2}&ProjectOwner={3}&Status={4}&check={5}');</script>", Date, ddlSite.SelectedValue, ddlContractor.SelectedValue, ddlProjectOwner.SelectedValue, ddlStatus.SelectedValue, check);
        //Page.RegisterStartupScript("onclick", url);



    }
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected void gridload()
    {
        try
        {

            Task.SelectParameters["SiteID"].DefaultValue = getVal(string.IsNullOrEmpty(ddlSite.SelectedValue) ? "0" : ddlSite.SelectedValue);
            Task.SelectParameters["ContractorID"].DefaultValue = getVal(ddlContractor.SelectedValue);
            Task.SelectParameters["ProjectOwnerID"].DefaultValue = getVal(ddlProjectOwner.SelectedValue);
            Task.SelectParameters["StatusID"].DefaultValue = getVal(ddlStatus.SelectedValue);
            Task.SelectParameters["RAGStatus"].DefaultValue = getVal(ddlRAG.SelectedValue);
            Task.SelectParameters["ProjectReference"].DefaultValue = getVal(ddlProjects.SelectedValue);
            Task.SelectParameters["Priority"].DefaultValue = getVal(ddlPriority.SelectedValue);
            Task.SelectParameters["Portfolio"].DefaultValue = getVal(ddlPortfolio.SelectedValue);
            //Task.SelectParameters["ProjectOwnerIDSub"].DefaultValue = getVal(ddlProjectOwnerSub.SelectedValue);// getVal(ddlPriority.SelectedValue);
            //Task.SelectParameters["CountryID"].DefaultValue = getVal(ddlCountry.SelectedValue);//getVal(ddlPriority.SelectedValue);

            int i = 0;
            if (chkUpto.Checked)
            {
                i = 1;
            }
            Task.SelectParameters["Upto"].DefaultValue = i.ToString();
            string dt = "mydate";
            if (txtDate.Text.Trim() != "")
            {
                dt = txtDate.Text.Trim();
            }
            Task.SelectParameters["Date"].DefaultValue = dt.ToString();

            if (rdbtnMilestone.SelectedItem.Value == "0")
            {
                Task.SelectParameters["isMileStone"].DefaultValue = "0";
            }
            else if (rdbtnMilestone.SelectedItem.Value == "1")
            {
                Task.SelectParameters["isMileStone"].DefaultValue = "1";
            }
            GridView1.DataSourceID ="Task";
            GridView1.DataBind();
            check = true;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "add parameters to sql datasource"); 
        }

    }
    private string getVal( string val)
    { 
        if(val == "Please select...")
        {
            val = "0";
        }        
        return val;
    }
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        



    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {         
            //CheckBox chkStatus = (CheckBox)row.Cells[11].Controls[1];
            //if (chkStatus.Checked)
            //{
            //    ((CheckBox)row.FindControl("chkStatus")).Enabled = false;
            //}
            //Label lblImage = (Label)row.Cells[2].Controls[1];
            HiddenField hid = (HiddenField)row.Cells[3].Controls[1];

            if (hid.Value == "RED")
            {
                ((Image)row.FindControl("Image1")).ImageUrl = "~/images/indcate_red.png";
            }
            else if (hid.Value == "GREEN")
            {
                ((Image)row.FindControl("Image1")).ImageUrl = "~/images/indcate_green.png";
            }
            else if (hid.Value == "AMBER")
            {
                ((Image)row.FindControl("Image1")).ImageUrl = "~/images/indcate_yellow.png";
            }
            else { ((Image)row.FindControl("Image1")).ImageUrl = "~/images/indcate_green.png"; }
        }
    }
    protected void MyRowUpdate(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }




    protected void Task_Load(object sender, EventArgs e)
    {



    }

    protected void btnView_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        //if (txtDate.Text == "")
        //{
        //    Date = "mydate";
        //}
        //else
        //{
        //    Date = txtDate.Text;
        //}

        //string url = string.Format("<script>javascript:window.open('Tasks_report.aspx?Date={0}&Site={1}&Contractor={2}&ProjectOwner={3}&Status={4}&check={5}&upto={6}');</script>", Date, ddlSite.SelectedValue, ddlContractor.SelectedValue, ddlProjectOwner.SelectedValue, ddlStatus.SelectedValue, check, Convert.ToInt16(chkUpto.Checked));
        //Page.RegisterStartupScript("onclick", url);


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        gridload();
        if (GridView1.Rows.Count > 0)
        {
            btnUpdateStatus1.Visible = false;
        }
        else
            btnUpdateStatus1.Visible = false;

    }
    protected void btnViewreport_Click(object sender, ImageClickEventArgs e)
    {
        if (txtDate.Text == "")
        {
            Date = "mydate";
        }
        else
        {
            Date = txtDate.Text;
        }

        string url = string.Format("<script>javascript:window.open('Tasks_report.aspx?Date={0}&Site={1}&Contractor={2}&ProjectOwner={3}&Status={4}&check={5}&upto={6}');</script>", Date, ddlSite.SelectedValue, ddlContractor.SelectedValue, ddlProjectOwner.SelectedValue, ddlStatus.SelectedValue, check, Convert.ToInt16(chkUpto.Checked));
        Page.RegisterStartupScript("onclick", url);

    }
    protected void btnUpdateStatus1_Click(object sender, ImageClickEventArgs e)
    {
        int id;
        ArrayList ar = new ArrayList();
        string IDS = "";
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chkStatus = (CheckBox)row.Cells[12].Controls[1];
            if (chkStatus.Checked)
            {
                if (chkStatus.Enabled == true)
                {
                    id = Convert.ToInt32(((Label)row.FindControl("ID")).Text);
                    IDS = IDS + "," + id;
                    ar.Add(id);
                }
            }
        }

        SqlCommand comm = new SqlCommand("DN_Update_ItemStatus", myConnection);
        comm.CommandType = CommandType.StoredProcedure;
        comm.Parameters.Add(new SqlParameter("@ID", IDS));
        //SqlCommand comm = new SqlCommand("update ProjectItems set ItemStatus='Completed' where ID=" + id, myConnection);
        myConnection.Open();
        comm.ExecuteNonQuery();
        myConnection.Close();
        GridView1.DataBind();
    }
    protected string LoadPriority(string p)
    {
        string retVal = @"~\images\icon_priority_low.gif"; ;
        if (!string.IsNullOrEmpty(p))
        {
            retVal = @"~\images\icon_priority_" + p.ToLower().Trim() + ".gif";
        }
        return retVal;

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        
    }
}
