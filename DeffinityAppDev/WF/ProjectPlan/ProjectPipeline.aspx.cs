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
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using ProgrammeMgt.Entity;
using ProgrammeMgt.DAL;


using Deffinity.Bindings;



public partial class ProjectPlan_ProjectPipleline : BasePage
{
    public string Status;
    public string ProjectApproval;
    
    protected void Page_Load(object sender, EventArgs e)
    {
       // gviewclientprojectstatus.Columns[5].HeaderText = "Hello";//8-status
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
        {
            try
            {
              
                //check list query string parmaeter
                if (Request.QueryString["list"] != null)
                {
                    if (Request.QueryString["list"] == "all")
                    {
                        sessionKeys.PortfolioID = 0;
                    }
                }
               
                if (Request.QueryString["Status"] != null)
                {
                    int status1 = Convert.ToInt32(Request.QueryString["Status"].ToString());
                    //bind the portfolio dropdown
                     if (status1 == 8)
                    {
                        PanelPln.Visible = true;
                       
                        ddlbind_Contribution();
                        lblProjectPlanMsg.Visible = false;
                        GetProjectPlanData();
                    }
                    else if (status1 == 3)
                    {
                        PanelPln.Visible = false;

                    }
                    else
                    {
                        PanelPln.Visible = false;
                    }
                    
                }
                else
                {
                    Response.Redirect("~/WF/default.aspx", false);
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        string JavascriptBlock = @"
            <script type='text/javascript'>
            var checkedObject = null;
            function isChecked(what){
                if(checkedObject == null)
                {
                    checkedObject = what;
                    return true;
                }
                else{
                    if(what==checkedObject)
                    {
                        if(!what.checked)
                        {
                            checkedObject = null;
                            return true;
                        }
                    }
                    else{
                        alert('Only one project proposal can select!!!');
                        return false;
                    }
                }       
            }
            </script>
        ";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "JavascriptBlock", JavascriptBlock);


    }
   
   
    public void ddlbind_Contribution()
    {
        SqlConnection cn = new SqlConnection(Constants.DBString);
        SqlDataAdapter adp_Contribution = new SqlDataAdapter("SELECT ContributionID,Contribution FROM Contribution", cn);
        DataSet ds_Contribution = new DataSet();
        adp_Contribution.Fill(ds_Contribution);
        ddlContribution.DataSource = ds_Contribution;
        ddlContribution.DataTextField = "Contribution";
        ddlContribution.DataValueField = "ContributionID";
        ddlContribution.DataBind();
        ListItem _selectTemplates = new ListItem("Select...", "0");
        ddlContribution.Items.Insert(0, _selectTemplates);
        ds_Contribution.Dispose();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetProjectPlanData();
    }
    private void GetProjectPlanData()
    {
        try
        {
            SqlConnection cn = new SqlConnection(Constants.DBString);
            SqlDataAdapter adp_search = new SqlDataAdapter("DN_ProjectPipelineSearch", cn);
            adp_search.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp_search.SelectCommand.Parameters.Add("@ContributionID", SqlDbType.Int).Value = Convert.ToInt32(ddlContribution.SelectedItem.Value);
            adp_search.SelectCommand.Parameters.Add("@RiskScore", SqlDbType.Int).Value = Convert.ToInt32(ddlRisk.SelectedItem.Value);
            adp_search.SelectCommand.Parameters.Add("@FinancialScore", SqlDbType.Int).Value = Convert.ToInt32(ddlFinance.SelectedItem.Value);
            adp_search.SelectCommand.Parameters.Add("@BusinessScore", SqlDbType.Int).Value = Convert.ToInt32(ddlBiz.SelectedItem.Value);
            DataSet ds = new DataSet();
            adp_search.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            ds.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected bool getVisible(string i)
    {
        bool val = false;
        try
        {
            //staus 3 is approved
            if (Convert.ToInt32(i) == 3)
            {
                val = true;
            }

        }
        catch (Exception ex) { }
        return val;
    }
   
    private void Gridview_colums()
    {
       
        //if (PermissionManager.IsEnabled(PermissionManager.Feature.ProjectRisks))
        //{
        //    gviewclientprojectstatus.Columns[16].Visible = true; 
        //}
        //else
        //{
        //    gviewclientprojectstatus.Columns[16].Visible = false;
        //}
        //if (PermissionManager.IsEnabled(PermissionManager.Feature.ProjectIssues))
        //{
        //    gviewclientprojectstatus.Columns[15].Visible = true;
        //}
        //else
        //{
        //    gviewclientprojectstatus.Columns[15].Visible = false;
        //}
        //if (PermissionManager.IsEnabled(PermissionManager.Feature.ProjectBOM))
        //{
        //    gviewclientprojectstatus.Columns[14].Visible = true;
        //}
        //else
        //{
        //    gviewclientprojectstatus.Columns[14].Visible = false;
        //}

    }
    
  
    protected void lbtnPlan_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/ProjectPlan/projectplan.aspx?ProjectPlanID=0");
    }
    protected void gviewclientprojectstatus_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //int i = e.NewSelectedIndex;
        //HiddenField HID = (HiddenField)gviewclientprojectstatus.Rows[i].FindControl("HID");        
        //getVariations(Convert.ToInt32(HID.Value));
    }
    #region grid function
    protected bool getImage(string status)
    {
        bool s = false;

        if (Convert.ToInt32(status) > 0)
        {
            s = true;
        }


        return s;
    }
    #endregion

   

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //((CheckBox)e.Row.FindControl("rdbGVRow")).Attributes.Add("onclick", "javascript:selectone('" + ((CheckBox)e.Row.FindControl("rdbGVRow")).ClientID + "','" + GridView1.ClientID + "');");
        //    CheckBox chkh = new CheckBox();
        //    chkh = (CheckBox)e.Row.FindControl("rdbGVRow");
        //    chkh.Attributes.Add("onclick", "javascript:return Selectone(this,'" + GridView1.ClientID + "');");

        //}
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox c1 = (CheckBox)e.Row.Cells[0].FindControl("rdbGVRow");
            c1.Attributes.Add("onclick", "return isChecked(this)");
        }

    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        int id = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            //string s = row.Cells[0].Controls[1].ToString();
            //rdbGVRow
            CheckBox chkNew = (CheckBox)row.FindControl("rdbGVRow");//; (CheckBox)row.Cells[1].Controls[1];
            id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
            //get indent level
            int indentLevel = Convert.ToInt32(((HiddenField)row.FindControl("HidIndent")).Value);

        }
    }
    protected void btnCopy_Click1(object sender, EventArgs e)
    {

        lblProjectPlanMsg.Visible = false;
        try
        {
            int id = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {

                CheckBox chkNew = (CheckBox)row.FindControl("rdbGVRow");
                if (chkNew.Checked)
                {
                    id = Convert.ToInt32(((HiddenField)row.FindControl("HID")).Value);
                }
            }
            if (id > 0)
            {
                SqlParameter outval = new SqlParameter("@outval", SqlDbType.Int);
                outval.Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_ProjectPlan_copy", new SqlParameter("@ProjectPlanID", id), new SqlParameter("@ProjectTitle", txtNewProjectProposal.Text.Trim()), outval);
                int retval = int.Parse(outval.Value.ToString());

                if (retval == 0)
                {
                    txtNewProjectProposal.Text = string.Empty;
                    GetProjectPlanData();
                }
                else
                {
                    lblProjectPlanMsg.Visible = true;
                    lblProjectPlanMsg.Text = Resources.DeffinityRes.Prjproposaltitlealreadyexists;// "Project proposal title already exists.";
                }
            }
            else
            {
                lblProjectPlanMsg.Visible = true;
                lblProjectPlanMsg.Text = Resources.DeffinityRes.PlsselectProjectProposal;// "Please select Project Proposal.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
   
    //protected void GridQuoted_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    //{
    //    GridQuoted.PageIndex = e.NewPageIndex;
    //    gviewclientprojectstatus1();
    //}
    protected void gviewclientprojectstatus_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox c1 = (CheckBox)e.Row.Cells[0].FindControl("rdbGVRow1");
            c1.Attributes.Add("onclick", "return isChecked(this)");
        }

    }
   
   
   
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    /// Gets or Sets the gridview sortdirection property
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    /// Gets or Sets the gridview sortexpression property
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    
    protected void ddlowner_DataBound(object sender, EventArgs e)
    {
        //if (ddlowner.Items.Count > 0)
        //{
        //    ddlowner.Items.RemoveAt(0);
        //    ddlowner.Items.Insert(0, new ListItem("All", "0"));
        //}
    }
    #region "Added on 15/02/2011"
    protected string ReturnDays(string days, string CR)
    {
        //, string DDays
        string stemp = string.Empty;
        if (CR != "")
        {
           
                double val = Convert.ToDouble(days);
                if (val > 0.0)
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else if (val < 0)
                {
                    stemp = " ";
                }
                else
                {
                    stemp = "-";
                }
            
        }
        return stemp;
        
    }
    protected string ReturnDays(string days, string CR,string DDays,string totalHours)
    {
        string stemp = string.Empty;
        try
        {
            //, string DDays

            if (CR != "")
            {

                double val = Convert.ToDouble(days);
                if (val > 0.0)
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else if (Convert.ToDouble(DDays) == 0)
                {
                    stemp = " ";
                }
                else if (Convert.ToDouble(DDays) < Convert.ToDouble(totalHours))
                {
                    stemp = Math.Round(val, 2).ToString();
                }
                else
                {
                    stemp = "-";
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return stemp;
    }

    protected System.Drawing.Color foreColor(string days)
    {
        System.Drawing.Color stemp = System.Drawing.Color.Gray;
        try
        {

            double val = Convert.ToDouble(days);
            if (val < 0)
            {
                stemp = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
        return stemp;
    }

    public void SetProgress(Label lblProgress, decimal percent, double days, double ddays, double totldays,string CR)
    {
        if (CR != "")
        {
            if (ddays == 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>Setup&nbsp;Required</TD</TR></TABLE>";
            }
            if (ddays < totldays && days < 0 && ddays != 0)
            {
                lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD style=color:Red bgcolor=#CCCCCC width=100% forecolor=red  align=center>0%</TD</TR></TABLE>";
            }

            if (days != 0 && days > 0)
            {
                if (percent == 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD bgcolor=#FFF7CE width=100% align=center>100%</TD</TR></TABLE>";
                }
                if (percent >= 50)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100% height=7><TR><TD  bgcolor=#99FF66 width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 49 && percent > 10)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD  bgcolor=#FFCC00  width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
                if (percent <= 10 && percent > 0)
                {
                    lblProgress.Text = "<TABLE cellspacing=0 cellpadding=0 border=0 width=100%  height=7><TR><TD bgcolor=red width=" + percent.ToString() + "%>&nbsp;</TD><TD bgcolor=#FFF7CE>" + percent.ToString() + "%</TD></TR></TABLE>";
                }
            }
        }

       
    }
    


    protected bool POVisible(string val)
    {
       
        bool visible = false;
        if (int.Parse(val) != 0)
        {
            visible = true;
        }

        return visible;
    }

    #endregion
   
}
