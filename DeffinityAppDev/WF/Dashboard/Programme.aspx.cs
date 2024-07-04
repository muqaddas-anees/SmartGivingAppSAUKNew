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
using System.Web.Configuration;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Resources.Editor;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Shared.Events;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using Deffinity.ProgrammeManagers;
using Deffinity.ProgrammeEntitys;
using ProgrammeMgt.DAL;
using ProgrammeMgt.Entity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Resources.Editor;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Shared.Events;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.Script.Serialization;


public partial class ProgrammeDashboard : BasePage
{
    private string connectionString = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    private string error = "";
    DisBindings getData = new DisBindings(); protected string Program;
    DataSet dsP = new DataSet();
    Admin ad = new Admin();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //check dash boared user
            if (!IsPostBack)
            {
                Session["SubProgramme"] = null;
                Restore();
                //bind benefit types
                BindTypes();
              
                //set sessions if query string values exists
                ReadQuarystringValues();

                fillDropdownlist(DdlRiskType);
                //Master.PageHead = Resources.DeffinityRes.Dashboard;//"Dashboard";
                ViewState["sortOrder"] = "";

                BindFirstGrid("", "");
                //BindPortfolios();
                fillviewallSelectView();
               BindCountry();
               
               
                //PnlSummaryGraph.Visible = false;
                //PnlBenefitsTracking.Visible = false;
                //PnlProgrammeSummary.Visible = false;
                
                BindRAG();
                BindWorkstream();
               
            }
            
            BindData();



            int Panel = 1;
            if (Request.QueryString["Panel"] != null)
            {
                Panel = Convert.ToInt32(Request.QueryString["Panel"]);
            }

            if (Panel == 2)
            {
                if (!IsPostBack)
                {
                    PanelMakeVisible(Panel);
                    //Toggle();
                }
                else
                {
                    //DivPnlSummaryGraph.Visible = false;
                    //DivProgrammeSummary.Visible = false;
                    //DivBenefitsTracking.Visible = true;
                    //PnlBenefitsTracking.Visible = true;
                }

            }
            else
            {
                PanelMakeVisible(Panel);
            }
        }
      catch (Exception ex)
      {
          LogExceptions.WriteExceptionLog(ex);
      }

        if (sessionKeys.SID == 1)
        {

            btnSubmit.Visible = true;
            GridView5.Columns[10].Visible = true;
            GridView5.Columns[0].Visible = false;
            GridView5.Columns[1].Visible = true;

        }
        else
        {
            btnSubmit.Visible = false;
            GridView5.Columns[10].Visible = false;
            GridView5.Columns[0].Visible = true;
            GridView5.Columns[1].Visible = false;
        }

        

    }
    private void Restore()
    {

        //if (ddlPortfolios.SelectedIndex >= 0 &&  Session["Programme"]!= null)
        //{
        //    //BindPortfolios();

        //    // BindProgramme();
        //   // BindSubProgramme();
        //    BindProgramme();
        //    ddlprojectProgramme.SelectedValue = Programmeid().ToString();
            
        //    sessionKeys.ProgrammeID = Programmeid();
        //    ddlprojectProgramme.SelectedValue = sessionKeys.ProgrammeID.ToString();
        //    sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
        //}
        BindProgramme();
        if (sessionKeys.ProgrammeID > 0)
            ddlprojectProgramme.SelectedValue = sessionKeys.ProgrammeID.ToString();
        
        if (Session["SubProgramme"] != null)
        {
            //BindProgramme();
            //if (sessionKeys.ProgrammeID >0)
            //ddlprojectProgramme.SelectedValue = sessionKeys.ProgrammeID.ToString();
            BindSubProgramme();

            ddlSubProgramme.SelectedValue = SubProgrammeid().ToString();
        }
       
    }

    //private  void BindPortfolios()
    //{
    //    DataTable dt = new DataTable();
    //    dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "deffinity_getportfolios").Tables[0];
    //    ddlPortfolios.DataSource = dt;
        
    //    ddlPortfolios.DataTextField = "Portfolio";
    //    ddlPortfolios.DataValueField = "ID";
    //    ddlPortfolios.DataBind();
    //  //  DataSourceID="" DataTextField="" 
    //}

    private void BindProgramme()
    {
        ddlprojectProgramme.DataBind();
        //getData.DdlBindSelect(ddlprojectProgramme, string.Format("select id,operationsowners from operationsowners where PortfolioID ={0} and Level=1 order by operationsowners ", ddlPortfolios.SelectedValue), "ID", "OperationsOwners", false, false, true);
    }

    private void BindSubProgramme()
    {
        getData.DdlBindSelect(ddlSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlprojectProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
    }

    
    public void fillviewallSelectView()
    {
        try
        {
            ddlprojectProgramme.DataBind();
            //getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners where level=1 order by OperationsOwners ", "ID", "OperationsOwners", false, false,true);
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
            sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;

            Program = sessionKeys.ProgrammeID.ToString();
           
            if (ddlprojectProgramme.SelectedValue == "0")
            {
                //getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners order by OperationsOwners asc", "ID", "OperationsOwners", false, false);
                ddlSubProgramme.Items.Clear();
                ddlSubProgramme.Items.Insert(0, Constants.ddlDefaultBind(true));
            }
            else
            {
                //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
                getData.DdlBindSelect(ddlSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlprojectProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
            }

            displaydetails();
            displaydata();
            displaydataPage();
           
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    protected string LoadRagSTatus(string pin)
    {


        string p = pin.ToUpper();
        string retVal = @"~\images\indcate_green.png";
        if (!string.IsNullOrEmpty(p))
        {


            if (p == "RED")
            {
                retVal = @"~/images/indcate_red.png";
            }
            else if (p == "GREEN")
            {

                retVal = @"~/images/indcate_green.png";
            }
            else if (p == "AMBER")
            {
                retVal = @"~/images/indcate_yellow.png";
            }
        }
        return retVal;

    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in GridView6.Rows)
        {
            //CheckBox chkStatus = (CheckBox)row.Cells[11].Controls[1];
            //if (chkStatus.Checked)
            //{
            //    ((CheckBox)row.FindControl("chkStatus")).Enabled = false;
            //}
            //Label lblImage = (Label)row.Cells[2].Controls[1];
            int k = 6;
            HiddenField hid = (HiddenField)row.Cells[k].Controls[1];

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
    private void PanelVisibility(bool p_Projects, bool p_reports, bool p_CategoryView, bool p_RiskMatrix, bool p_Assessment, bool p_Dependency, bool p_RAG, bool p_Workstream)
    {

        //if (ddlPortfolios.SelectedIndex != -1)
        //{
        //    fillviewallSelectView();

        //    ddlPortfolios.SelectedValue = Session["Portfolio"].ToString();
        //    ddlprojectProgramme.SelectedValue = Session["Programme"].ToString();
        //    ddlSubProgramme.SelectedValue = Session["SubProgramme"].ToString();
        //}

        PnlLiveProjects.Visible = p_Projects;
        PnlPendingProjects.Visible = p_Projects;
        PnlCompletedprojects.Visible = p_Projects;
        PnlReports.Visible = p_reports;
        ddlHidePanel.Visible = p_reports;
        lblSelectReport.Visible = p_reports;
        //DivPnlSummaryGraph.Visible = p_reports;        
        PnlSummaryGraph.Visible = p_reports;
        PnlCategoryView.Visible = p_CategoryView;
        PnlRisk.Visible = p_CategoryView;
        PnlIssue.Visible = p_CategoryView;
        PnlRiskMatrix.Visible = p_RiskMatrix;
        PnlAssessment.Visible = p_Assessment;
        PnlDependencyMap.Visible = p_Dependency;
        PnlRAGSummary.Visible = p_RAG;
        pnlWorkstream.Visible = p_Workstream;
        PnlProgrammeSummary.Visible = false;
        PnlBenefitsTracking.Visible = false;

    }
    private void PanelMakeVisible(int _type)
    {
        //if (ddlPortfolios.SelectedIndex != -1)
        //{
        //    fillviewallSelectView();

        //    ddlPortfolios.SelectedValue = Session["Portfolio"].ToString();
        //    ddlprojectProgramme.SelectedValue = Session["Programme"].ToString();
        //    ddlSubProgramme.SelectedValue = Session["SubProgramme"].ToString();
        //}
        switch (_type)
        {
            case 1:
                PanelVisibility(true, false, false, false, false, false, false, false);
                break;
            case 2:
                PanelVisibility(false, true, false, false, false, false, false, false);
                setBindFunctions();
                break;
            case 3:
                PanelVisibility(false, false, true, false, false, false, false, false);

                break;
            case 4:
                PanelVisibility(false, false, false, true, false, false, false, false);

                break;
            case 5:
                PanelVisibility(false, false, false, false, true, false, false, false);

                break;
            case 6:
                PanelVisibility(false, false, false, false, false, true, false, false);
                break;
            case 7:
                PanelVisibility(false, false, false, false, false, false,true,false);
                break;
            case 8:
                PanelVisibility(false, false, false, false, false, false, false,true);
                break;
            default:
                PanelVisibility(true, false, false, false, false, false, false, false);
                break;

        }



    }
    public void displaydetails()
    {
        SqlCommand cmd = new SqlCommand("DN_CheckProjectProgrammeStatus", myConnection);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter Programme = new SqlParameter("@Programme", SqlDbType.Int, 32);
        Programme.Value = Programmeid();//int.Parse(ddlprojectProgramme.SelectedValue);// sessionKeys.ProgrammeID;
        cmd.Parameters.Add(Programme);
        SqlParameter uid = new SqlParameter("@UserID", SqlDbType.Int, 32);
        uid.Value = sessionKeys.UID;
        cmd.Parameters.Add(uid);
        SqlDataReader reader;
        try
        {
            myConnection.Open();
            reader = cmd.ExecuteReader();
            reader.Read();
            lblportfolioowner.Text = reader["OwnerName"].ToString();            
            lblliveProject.Text = reader["Live"].ToString();
            lblpendingProject.Text = reader["Pending"].ToString();
            lbltotpvalue.Text = string.Format("{0:c}", reader["totalcost"]);
            lblTotalLiveVal.Text = string.Format("{0:c}", reader["LiveProjectcost"]);
            lblTotalPendingVal.Text = string.Format("{0:c}", reader["PendingProjectcost"]);
            reader.Close();
            myConnection.Close();
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "dashboard - programme selection");
        }
        finally
        {
            myConnection.Close();
        }

    }

    protected void DdlRiskType_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlSubProgramme.SelectedValue == "0")
        {
            Session["SubProgramme"] = ddlSubProgramme.SelectedValue;
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
            sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
        }
        else
        {
            Session["SubProgramme"] = ddlSubProgramme.SelectedValue;
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlSubProgramme.SelectedValue);
            sessionKeys.ProgrammeName = ddlSubProgramme.SelectedItem.Text;
        }

        
        //sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
        //sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
        displaydataPageCategory();
    }


    protected void ddlselectview_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlselectview.SelectedValue == "0")
        {
            getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners where  Level=1 order by OperationsOwners ", "ID", "OperationsOwners", false, false);
           

        }
        else
        {
            //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
            getData.DdlBindSelect(ddlprojectProgramme, string.Format("select id,operationsowners from operationsowners where PortfolioID ={0} and Level=1 order by operationsowners asc", ddlselectview.SelectedValue), "ID", "OperationsOwners", false, true);
        }
        sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
        sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
       
        displaydata();
        displaydetails();
    }

    public void BindFirstGrid(string sortExp,string sortOrd)
    {
        DataTable dt = new DataTable();

        SqlCommand myCommand = new SqlCommand("Deffinity_DisplayProgramme", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@Programme", GetProgrammeID());
        myCommand.Parameters.AddWithValue("@UID", sessionKeys.UID);
        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
        DataSet ds = new DataSet();
        int strt =1*GridView1.PageSize;

        myadapter.Fill(ds);
        int i = ds.Tables.Count;
        DataView myDataView = new DataView();
        myDataView = ds.Tables[1].DefaultView;
       
        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortOrd);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();
    }

    private int GetProgrammeID()
    {
        int pid = 0;
        if (!string.IsNullOrEmpty(ddlSubProgramme.SelectedValue) && ddlSubProgramme.SelectedValue != "0")
        {
            pid = Convert.ToInt32(ddlSubProgramme.SelectedValue);
        }
        else if (!string.IsNullOrEmpty(ddlprojectProgramme.SelectedValue) && ddlprojectProgramme.SelectedValue != "0")
        {
            pid = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
        }
        return pid;
    }
    private void BindData()
    {
        SqlCommand myCommand = new SqlCommand("Deffinity_DisplayProgramme", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.AddWithValue("@Programme", GetProgrammeID());
        myCommand.Parameters.AddWithValue("@UID", sessionKeys.UID);
        SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
        DataSet ds = new DataSet();
        myadapter.Fill(dsP);
    }
    [System.Web.Services.WebMethod]
    public static object BindProgrammeSummaryGraph(string PID)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            List<ProgramSummaryCls1> P_List = new List<ProgramSummaryCls1>();
            ProgramSummaryCls1 P_Cls = null;
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectBenefitBudget_Chart2",
                new SqlParameter("@ProgrammeID ", int.Parse(PID))).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                P_Cls = new ProgramSummaryCls1();
                P_Cls.state = dr["OperationsOwners"].ToString();
                P_Cls.Late = Convert.ToDouble(dr["Late"].ToString());
                P_Cls.OnTime = Convert.ToDouble(dr["OnTime"].ToString());
                P_Cls.Complete = Convert.ToDouble(dr["Complete"].ToString());
                P_List.Add(P_Cls);
            }
            return jsonSerializer.Serialize(P_List).ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
    [System.Web.Services.WebMethod]
    public static object BindFinancialGrpah(string PID)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        try
        {
            List<ProgramSummaryCls> P_List = new List<ProgramSummaryCls>();
            ProgramSummaryCls P_Cls = null;
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "DEFFINITY_PROJECTS_PROGRAMME_SUMMURY",
                        new SqlParameter("@PROGRAMME", int.Parse(PID)), new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                P_Cls = new ProgramSummaryCls();
                P_Cls.state = dr["ProjectReference"].ToString();
                P_Cls.ProjectValue =Convert.ToDouble( dr["Project value"].ToString());
                P_Cls.ActualcosttoDate =Convert.ToDouble( dr["Actual Cost to Date"].ToString());
                P_Cls.Variances =Convert.ToDouble( dr["Variances"].ToString());
                P_Cls.Invoiced = Convert.ToDouble(dr["Invoiced"].ToString());
                P_List.Add(P_Cls);
            }
            return jsonSerializer.Serialize(P_List).ToString();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            return jsonSerializer.Serialize(string.Empty).ToString();
        }
    }
    [System.Web.Services.WebMethod]
    public static object BindBenFitGrpah(string PID, string Type,string SPID,string Cid)
    {
         JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
         try
         {
             List<BenfitReportCls1> P_List = new List<BenfitReportCls1>();
             BenfitReportCls1 P_Cls = null;
             DataTable dt = new DataTable();

             DataSet dtset = new DataSet();

             dtset = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectBenefitChart_Modify1",

                 new SqlParameter("@type", int.Parse(Type)),
                 new SqlParameter("@Programme", int.Parse(PID)),
                 new SqlParameter("@SubProgramme", int.Parse(SPID)),
                 new SqlParameter("@country", int.Parse(Cid)),
                 new SqlParameter("@UserID", sessionKeys.UID));
             if (dtset.Tables.Count > 0)
             {
                 dt = dtset.Tables[0];
             }
             foreach (DataRow dr in dt.Rows)
             {
                 P_Cls = new BenfitReportCls1();
                 P_Cls.state = dr["ProjectName"].ToString();
                 P_Cls.ActualtoDate = Convert.ToDouble(dr["Actual to Date"].ToString());
                 P_Cls.PlannedtoDate = Convert.ToDouble(dr["Planned to Date"].ToString());
                 P_Cls.TargettoEnd =Convert.ToDouble( dr["Target to End"].ToString());
                 P_List.Add(P_Cls);
             }
             return jsonSerializer.Serialize(P_List).ToString();
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
             return jsonSerializer.Serialize(string.Empty).ToString();
         }
    }


    public void displaydata()
    {
        try
        {
            DataTable dt = new DataTable();
            
            SqlCommand myCommand = new SqlCommand("Deffinity_DisplayProgramme", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddWithValue("@Programme", GetProgrammeID());
            myCommand.Parameters.AddWithValue("@UID", sessionKeys.UID);
            //SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            //myadapter.Fill(dsP);
            myadapter.Fill(ds);
            dsP = ds;
            int i = ds.Tables.Count;
            //GridView1.DataSource = ds.Tables[1];
            //GridView1.DataBind();

            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();

            GridView3.DataSource = ds.Tables[2];
            GridView3.DataBind();

            //ListLiveIssues.DataSource = ds.Tables[3];
            //ListLiveIssues.DataBind();

            //ListResources.DataSource = ds.Tables[4];
            //ListResources.DataBind();

            //ListMitigation.DataSource = ds.Tables[5];
            //ListMitigation.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "dashboard - programme grid binding");
        }
       

    }

    public void displaydataPage()
    {
        try
        {
            DataTable dt = new DataTable();

            //SqlCommand myCommand = new SqlCommand("Deffinity_DisplayProgramme", myConnection);
            //myCommand.CommandType = CommandType.StoredProcedure;
            //myCommand.Parameters.AddWithValue("@Programme", sessionKeys.ProgrammeID);
            //SqlDataAdapter myadapter = new SqlDataAdapter(myCommand);
            //DataSet ds = new DataSet();
            //myadapter.Fill(ds);
            int i = dsP.Tables.Count;
            //GridView1.DataSource = ds.Tables[1];
            //GridView1.DataBind();

            GridView2.DataSource = dsP.Tables[0];
            GridView2.DataBind();

            GridView3.DataSource = dsP.Tables[2];
            GridView3.DataBind();

            //ListLiveIssues.DataSource = dsP.Tables[3];
            //ListLiveIssues.DataBind();

            //ListResources.DataSource = dsP.Tables[4];
            //ListResources.DataBind();

            //ListMitigation.DataSource = dsP.Tables[5];
            //ListMitigation.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "dashboard - programme grid binding");
        }


    }
    protected void ddlprojectPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlprojectProgramme.SelectedValue == "0")
        {
            Session["Programme"] = ddlprojectProgramme.SelectedValue;
            //getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners order by OperationsOwners asc", "ID", "OperationsOwners", false, false);
            ddlSubProgramme.Items.Clear();
            ddlSubProgramme.Items.Insert(0, Constants.ddlDefaultBind(true));
        }
        else
        {
            Session["Programme"] = ddlprojectProgramme.SelectedValue;
            //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
            getData.DdlBindSelect(ddlSubProgramme, string.Format("select id,operationsowners from operationsowners where MasterProgramme={0} and Level=2 order by operationsowners ", ddlprojectProgramme.SelectedValue), "ID", "OperationsOwners", false, false, true);
        }
        sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
        sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
        BindFirstGrid("", "");
        BindCountry();
        displaydetails();
        displaydata();
        BindRAG();
        BindWorkstream();
        BindGridPB();
        Toggle();
    }
    //protected void ddlPortfolios_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlPortfolios.SelectedValue == "0")
    //    {
    //        Session["Portfolio"] = ddlPortfolios.SelectedValue.ToString();
    //        getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners where and Level=1 order by OperationsOwners ", "ID", "OperationsOwners", false, false, true);
    //        ddlSubProgramme.Items.Clear();
    //        ddlSubProgramme.Items.Insert(0, Constants.ddlDefaultBind(true));
    //        getData.DdlBindSelect(ddlprojectProgramme, "select ID,OperationsOwners from OperationsOwners where level=1 order by OperationsOwners ", "ID", "OperationsOwners", false, false, true);
    //        sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
    //        sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
            
    //    }
    //    else
    //    {
    //        Session["Portfolio"] = ddlPortfolios.SelectedValue.ToString();
    //        //getData.DdlBindSelect(ddlprojectProgramme, string.Format("SELECT OperationsOwners.ID, OperationsOwners.OperationsOwners FROM OperationsOwners INNER JOIN ContractorsToOwners ON OperationsOwners.ID = ContractorsToOwners.OpsOwner where ContractorsToOwners.ContractorID = {0} order by OperationsOwners asc",sessionKeys.UID), "ID", "OperationsOwners", false, false);
    //        getData.DdlBindSelect(ddlprojectProgramme, string.Format("select id,operationsowners from operationsowners where PortfolioID ={0} and Level=1 order by operationsowners ", ddlPortfolios.SelectedValue), "ID", "OperationsOwners", false, false, true);
    //    }
    //    sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
    //    sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
    //    Program = sessionKeys.ProgrammeID.ToString();
    //    BindFirstGrid("", "");
    //    displaydata();
    //    displaydetails();
    //    BindChart();
    //    BindGridPB();
    //    BindChart1();
    //    Toggle();
    //}

    protected void ddlSubProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubProgramme.SelectedValue == "0")
        {
            Session["SubProgramme"] = ddlSubProgramme.SelectedValue;
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
            sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
        }
        else
        {
            Session["SubProgramme"] = ddlSubProgramme.SelectedValue;
            sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
            sessionKeys.ProgrammeName = ddlprojectProgramme.SelectedItem.Text;
        }
        BindFirstGrid("", "");
        displaydata();
        displaydetails();
        BindGridPB();
    }
    protected void fillDropdownlist(DropDownList dl)
    {
        try
        {

            DataTable dt = new DataTable();

            
            if (dl.ID == "DdlRiskType")
            {
               // getData.DdlBindSelect(dl, "select ID,IssueTypeName from issuetype", "ID", "IssueTypeName", false, false, true);
                getData.DdlBindSelect(dl, "SELECT ID,Name FROM ProjectTaskCategory ORDER BY Name", "ID", "Name", false, false, true);
            }
            //else if (dl.ID == "DdlPGroups")
            //{
            //    getData.DdlBindSelect(DdlPGroups, "select ID,OperationsOwners from OperationsOwners where Visible='Y'", "ID", "OperationsOwners", false, true);

            //}
            //else if (dl.ID == "DdlRptStatus")
            //{
            //    getData.DdlBindSelect(DdlRptStatus, "select ID,Status from AC2PStatus", "ID", "Status", false, false, true);

            //}
            //else if (dl.ID == "DdlStatus")
            //{
            //    getData.DdlBindSelect(DdlStatus, "select ID,Status from ItemStatus", "ID", "Status", false, false, true);

            //}
            //else if (dl.ID == "DdlRagStatus")
            //{
            //    DdlRagStatus.Items.Insert(0, new ListItem("Please select...", "0"));
            //    DdlRagStatus.Items.Insert(1, "AMBER");
            //    DdlRagStatus.Items.Insert(2, "RED");
            //    DdlRagStatus.Items.Insert(3, "GREEN");
            //}
            //else if (dl.ID == "ddlAssignedTo")
            //{

            //    //ddlAssignedTo

            //    ddlAssignedTo.DataSource = getAssign();
            //    ddlAssignedTo.DataTextField = "cname";
            //    ddlAssignedTo.DataValueField = "cname";
            //    ddlAssignedTo.DataBind();

            //    ddlAssignedTo.Items.Insert(0, new ListItem("Please select...", "0"));
            //    if (HidAssignTo.Value != "")
            //    {
            //        ddlAssignedTo.Items.Add(HidAssignTo.Value);
            //        HidAssignTo.Value = "";

            //    }


            //    dt.Clear();

            //}
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            projectTaskDataContext projectUp = new projectTaskDataContext();
            int i = GridView1.EditIndex;
            GridViewRow row = GridView1.Rows[i];
            DropDownList ddlPriority = (DropDownList)row.FindControl("ddlPriority");
            Project ProjectUpdate =
                      projectUp.Projects.Single(P => P.ProjectReference == int.Parse(e.CommandArgument.ToString()));
            ProjectUpdate.Priority = ddlPriority.SelectedValue;
            projectUp.SubmitChanges();
            //BindFirstGrid("", "");
            displaydata();
            BindFirstGrid("", sortOrder);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        //BindFirstGrid("", "");
        displaydata();
        BindFirstGrid("", "");
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView1.EditIndex =-1;
        displaydata();
       //BindFirstGrid("", sortOrder);
        BindFirstGrid("", "");
        
    }
    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            if (e.Row.FindControl("ddlPriority") != null)
            {
                //Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                //DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                Label lblPriority1 = (Label)e.Row.FindControl("lblStatusID");
                //DropDownList ddlProjectTitle = (DropDownList)e.Row.FindControl("ddlProjectTitle");
                DropDownList ddlPriority = (DropDownList)e.Row.FindControl("ddlPriority");
                ddlPriority.SelectedValue = lblPriority1.Text;
                
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        displaydata();
        //BindFirstGrid("", sortOrder);
        BindFirstGrid("", "");
       
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindFirstGrid(e.SortExpression,sortOrder);
        displaydata();
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    private void BindGrid()
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //BindFirstGrid("", sortOrder);
        BindFirstGrid("", "");
        displaydata();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        displaydataPage();
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.PageIndex = e.NewPageIndex;
        displaydataPage();
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;
        displaydataPage();
    }
    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;
        displaydataPageCategory();
    }

     protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
        GridView7.DataBind();
    }
     protected void GridView8_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView8.PageIndex = e.NewPageIndex;
        GridView8.DataBind();
    }
     protected void GridView9_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         GridView9.PageIndex = e.NewPageIndex;
         GridView9.DataBind();
     }
     //protected void btnView_Click(object sender, ImageClickEventArgs e)
     //{

     //    if (ddlprojectProgramme.SelectedValue != "0")
     //    {
     //        if (ddlSubProgramme.SelectedValue != "0")
     //        {
     //            sessionKeys.ProgrammeID = Convert.ToInt32(ddlSubProgramme.SelectedValue);
     //        }
     //        else
     //        {
     //            sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
     //        }
     //    }
     //    else
     //    {
     //        sessionKeys.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
     //    }

     //    SqlDataSource7.SelectParameters["Program"].DefaultValue = sessionKeys.ProgrammeID.ToString();        
     //   // SqlDataSource7.SelectParameters["Exposure"].DefaultValue = txtExposure.Text.Trim();
     //    //SqlDataSource7.SelectParameters["ExposureCondition"].DefaultValue = ddlListExposure.SelectedValue;
     //   GridView9.DataBind();
     //    //select ID,OperationsOwners from OperationsOwners where level=1 order by OperationsOwners
     //}
    
    #region Programme Assessment
    void displaydataPageCategory()
    {
        GridView6.DataBind();
    }
#endregion
    #region Programme Assessment
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProgrammeAssessmentCL Assessment = new ProgrammeAssessmentCL();
        Assessment.Benefits = txtBenefits.Text.Trim();
        Assessment.EmergentOpportunities = txtOpportunities.Text.Trim();
        Assessment.PaceOfProgress = txtPaceOfProgress.Text.Trim();
        Assessment.ProgressToDate = txtProgress.Text.Trim();
        Assessment.ProgrammeID = sessionKeys.ProgrammeID;
        Assessment.PortfolioID = 0;
        Assessment.DateLogged=Convert.ToDateTime((txtDateLogged.Text).ToString());
        if (!string.IsNullOrEmpty(id.Value))
        {
            Assessment.ID = Convert.ToInt32(id.Value);
            Admin.InsertUpdateProgrammeAssessment(false, Assessment);
        }
        else
        {
            Admin.InsertUpdateProgrammeAssessment(true, Assessment);
        }

        
        ClearFields();
        GridView5.DataBind();
        //clear hideen field
        id.Value = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
    }
    private void ClearFields()
    {
        txtBenefits.Text = "";
        txtOpportunities.Text = "";
        txtPaceOfProgress.Text = "";
        txtProgress.Text = "";
        txtDateLogged.Text = "";
        
    }
    protected void GridView5_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int i = e.NewSelectedIndex;
        HiddenField HID = (HiddenField)GridView5.Rows[i].FindControl("HID");
        getAssement(ad.SelectProgrammeAssessment(Convert.ToInt32(HID.Value)));
    }
    private void getAssement(ProgrammeAssessmentCL assessment)
    {
        txtBenefits.Text = assessment.Benefits;
        txtOpportunities.Text = assessment.EmergentOpportunities;
        txtPaceOfProgress.Text = assessment.PaceOfProgress;
        txtProgress.Text = assessment.ProgressToDate;
        id.Value = assessment.ID.ToString();
        txtDateLogged.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), assessment.DateLogged);
    }
    #endregion Programme Assessment

    #region Added 16/02/2011
  private void SetProperties()
    {

        //BenefitsTracking.ProgrammeID = sessionKeys.ProgrammeID;
        //PnlProgrammeSummary1.ProgrammeID = sessionKeys.ProgrammeID;

    }
  private void BindTypes()
  {
      projectTaskDataContext projects = new projectTaskDataContext();

      var types = (from r in projects.ProjectBenefitTypes
                   select r).ToList();


      ddlType.DataSource = types;
      ddlType.DataTextField = "Description";
      ddlType.DataValueField = "ID";
      ddlType.DataBind();

      ddlType.Items.Insert(0, new ListItem("Please select...", "0"));




  }
  protected string LoadRagSTatusDate(string NextDate)
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
  private void SetColors(Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1)
  {

      UltraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
      System.Drawing.Color[] chartColor;
      chartColor = new System.Drawing.Color[]{
            
            //53 different color's

          System.Drawing.Color.Red,System.Drawing.Color.Green,System.Drawing.Color.Gray
            

        };
      UltraChart1.ColorModel.CustomPalette = chartColor;
  }
  protected void ingSearch_Click(object sender, EventArgs e)
  {
      try
      {
          BindChart_Grid();
      }
      catch (Exception ex)
      {
          LogExceptions.WriteExceptionLog(ex);
      }

     
  }
  private void BindChart_Grid()
  {
      Toggle();
      BindGridPB();
      displaydetails();
      displaydata();
      displaydataPage();
  
  }
 

  
  private void BindGridPB()
  {
      try
      {
          DataTable dt = new DataTable();

          DataSet dtset = new DataSet();

          dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "ProjectBenefitGrid_Modify1 ",

              new SqlParameter("@type", int.Parse(ddlType.SelectedValue)),
              new SqlParameter("@Programme", int.Parse(ddlprojectProgramme.SelectedValue)),
           new SqlParameter("@SubProgramme", int.Parse(ddlSubProgramme.SelectedValue)),
           new SqlParameter("@country", int.Parse(ddlCountry.SelectedValue)),
            new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
          grdProjectBenefit.DataSource = dt;
          grdProjectBenefit.DataBind();
      }
      catch (Exception ex)
      {
          LogExceptions.WriteExceptionLog(ex);
      }
  }
  private void Toggle()
  {
      if (ddlHidePanel.Visible == true)
      {

          if (ddlHidePanel.SelectedValue == "1")
          {
              PnlSummaryGraph.Visible = true;
              PnlBenefitsTracking.Visible = false;
              PnlProgrammeSummary.Visible = false;
              displaydetails();
              displaydata();
              displaydataPage();
          }
          if (ddlHidePanel.SelectedValue == "2")
          {
              PnlSummaryGraph.Visible = false;
              PnlBenefitsTracking.Visible = false;
              PnlProgrammeSummary.Visible = true;
          }
          if (ddlHidePanel.SelectedValue == "3")
          {
              PnlSummaryGraph.Visible = false;
              PnlBenefitsTracking.Visible = true;
              PnlProgrammeSummary.Visible = false;
              BindGridPB();
          }
      }
      //ddlHidePanel.Visible

  }
  protected void ddlHidePanel_SelectedIndexChanged(object sender, EventArgs e)
  {
      try
      {
          Toggle();
      }
      
      catch (Exception ex)
      {
          LogExceptions.WriteExceptionLog(ex);
      }
  }


  private void BindRAG()
  {
      try
      {
          rag.programmeid = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
          rag.LoadControl();
      }
      catch (Exception ex)
      {
          LogExceptions.WriteExceptionLog(ex);
      }
  }

    #endregion 
  
  #region Bind workstream
  private void BindWorkstream()
  {
      //ProgrammeWorkStream1.ProgrammeID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
      ProgrammeWorkStream1.LoadControl();
  }
  #endregion 

  private int SubProgrammeid()
  {
      int retval = 0;
      if (Session["SubProgramme"] != null)
      {
          retval = int.Parse(Session["SubProgramme"].ToString());
      }

     return retval;
  }
     private int Programmeid()
  {
      int retval = 0;
      if (Session["Programme"] != null)
      {
          retval = int.Parse(Session["Programme"].ToString());
      }

     return retval;
  }
     private int Portfolioid()
  {
      int retval = 0;
      if (Session["Portfolio"] != null)
      {
          retval = int.Parse(Session["Portfolio"].ToString());
      }

     return retval;
  }
     protected void grdProjectBenefit_RowCommand(object sender, GridViewCommandEventArgs e)
     {
         try
         {
             if (e.CommandName == "View")
             {
                 Response.Redirect("~/WF/Projects/ProjectOverviewV4.aspx?project=" + e.CommandArgument.ToString());
             }

             if (e.CommandName == "Link")
             {
                 Response.Redirect("~/WF/Projects/ProjectBenfitBudget.aspx?project=" + e.CommandArgument.ToString());
             }
             //ProjectBillofMaterials.aspx?project=103228
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }

     }

     protected string TrimSmallDescStr(string desc)
     {
         try
         {
             string newString = "";
             if (desc.Length <= 80)
             {
                 newString = desc;
             }
             else
             {

                 newString = desc.Substring(0, 80) + "...";

             }
             return newString;
         }
         catch
         {
             throw;
         }
     }


     protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
     {

         ////if ((e.Row.RowType == DataControlRowType.Header))
         ////{
         ////   e.Row.Cells[0].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[1].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[2].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[3].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[4].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[5].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[6].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[7].Attributes.Add("onmouseover", string.Format("HideNotes()"));
         ////   e.Row.Cells[8].Attributes.Add("onmouseover", string.Format("HideNotes()"));
            

         ////}
         //if ((e.Row.RowType == DataControlRowType.DataRow))
         //{
         

         //    Label lblProgressToDate = new Label();
         //    lblProgressToDate = ((Label)(e.Row.FindControl("lblProgressToDate")));
         //    if (!(lblProgressToDate == null))
         //    {

         //        Label lblProgressToDate1 = ((Label)(e.Row.FindControl("lblProgressToDate1")));

         //        if (!(lblProgressToDate1 == null))
         //        {

         //            lblProgressToDate.Attributes.Add("onmouseover", "javascript:return overlib('" + DisplayGridPopUp(lblProgressToDate1.Text.Trim()) + "');");
         //            lblProgressToDate.Attributes.Add("onmouseout", "javascript:return nd();");

         //            //lblProgressToDate.Attributes.Add("onmouseover", string.Format("showNotes(\"{0}\", event)", lblProgressToDate1.Text.Trim()));
         //            //e.Row.Cells[e.Row.RowIndex].ForeColor = System.Drawing.Color.Red;
         //           //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFFFE1';");
         //            //lblNotes.Text = "Notes";
         //        }
         //        else
         //        {
         //            //lblProgressToDate.Text = "";
         //        }
         //    }


         //    Label lblBenefits = new Label();
         //    lblBenefits = ((Label)(e.Row.FindControl("lblBenefits")));
         //    if (!(lblBenefits == null))
         //    {

         //        Label lblBenefits1 = ((Label)(e.Row.FindControl("lblBenefits1")));

         //        if (!(lblBenefits1 == null))
         //        {
         //            //lblBenefits.Attributes.Add("onmouseover", string.Format("showNotes(\"{0}\", event)", lblBenefits1.Text.Trim()));
         //            lblBenefits.Attributes.Add("onmouseover", "javascript:return overlib('" + DisplayGridPopUp(lblBenefits1.Text.Trim()) + "');");
         //            lblBenefits.Attributes.Add("onmouseout", "javascript:return nd();");
         //        }
         //        else
         //        {
         //           // lblNotes.Text = "";
         //        }
         //    }
         //    //CommandField

         //    Label lblEmergentOpportunities = new Label();
         //    lblEmergentOpportunities = ((Label)(e.Row.FindControl("lblEmergentOpportunities")));
         //    if (!(lblEmergentOpportunities == null))
         //    {

         //        Label lblEmergentOpportunities1 = ((Label)(e.Row.FindControl("lblEmergentOpportunities1")));

         //        if (!(lblEmergentOpportunities1 == null))
         //        {
         //            //lblEmergentOpportunities.Attributes.Add("onmouseover", string.Format("showNotes(\"{0}\", event)", lblEmergentOpportunities1.Text.Trim()));
         //            lblEmergentOpportunities.Attributes.Add("onmouseover", "javascript:return overlib('" + DisplayGridPopUp(lblEmergentOpportunities1.Text.Trim()) + "');");
         //            lblEmergentOpportunities.Attributes.Add("onmouseout", "javascript:return nd();");
         //        }
         //        else
         //        {
         //            //lblNotes.Text = "";
         //        }
         //    }

         //    Label lblPaceOfProgress = new Label();
         //    lblPaceOfProgress = ((Label)(e.Row.FindControl("lblPaceOfProgress")));
         //    if (!(lblPaceOfProgress == null))
         //    {

         //        Label lblPaceOfProgress1 = ((Label)(e.Row.FindControl("lblPaceOfProgress1")));

         //        if (!(lblPaceOfProgress1 == null))
         //        {
         //            //lblPaceOfProgress.Attributes.Add("onmouseover", string.Format("showNotes(\"{0}\", event)", lblPaceOfProgress1.Text.Trim()));
         //            lblPaceOfProgress.Attributes.Add("onmouseover", "javascript:return overlib('" + DisplayGridPopUp(lblPaceOfProgress1.Text.Trim()) + "');");
         //            lblPaceOfProgress.Attributes.Add("onmouseout", "javascript:return nd();");                     
         //        }
         //        else
         //        {
         //            //lblNotes.Text = "";
         //        }
         //    }


         //    //Label lblRaisedDate = new Label();
         //    //lblPaceOfProgress = ((Label)(e.Row.FindControl("lblRaisedDate")));
         //    //if (!(lblRaisedDate == null))
         //    //{
         //    //    lblRaisedDate.Attributes.Add("onmouseover", string.Format("HideNotes()"));              
         //    //}

         //    //Label lblDateLogged = new Label();
         //    //lblDateLogged = ((Label)(e.Row.FindControl("lblDateLogged")));
         //    //if (!(lblDateLogged == null))
         //    //{
         //    //    lblDateLogged.Attributes.Add("onmouseover", string.Format("HideNotes()"));              
         //    //}
         //    //ImageButton deletebut = new ImageButton();
         //    //deletebut = ((ImageButton)(e.Row.FindControl("deletebut")));
         //    //if (!(deletebut == null))
         //    //{
         //    //    deletebut.Attributes.Add("onmouseover", string.Format("HideNotes()"));              
         //    //} 
             

         //    //Label lblID = new Label();
         //    //lblID = ((Label)(e.Row.FindControl("lblID")));
         //    //lblID.Attributes.Add("onmouseover", string.Format("HideNotes()"));



         //}
     }

     public static string DisplayGridPopUp(string str)
     {
         string retVal = "";
         
         try
         {


             retVal = "<div style=background-color:#f2f9ec; border: solid 2px black;><font size=2>" + str + "</font></div>";
             

         }
         catch (Exception ex)
         {

         }

         return retVal;

     }

     private void ReadQuarystringValues()
     {
         if (Request.QueryString["programmeid"] != null)
         {
             Session["Programme"] = Request.QueryString["programmeid"].ToString();  
         }
         if (Request.QueryString["subprogrammeid"] != null)
         { Session["SubProgramme"] = Request.QueryString["subprogrammeid"].ToString(); }
       

         if (Request.QueryString["benefittypeid"] != null)
         { ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(Request.QueryString["benefittypeid"].ToString())); }
         if (Request.QueryString["country"] != null)
         { ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(Request.QueryString["country"].ToString())); }
        

     }
     private void setBindFunctions()
     {
         if (Request.QueryString["programmeid"] != null)
         {
             Session["Programme"] = Request.QueryString["programmeid"].ToString();
             //set report tabe benifit report
             ddlHidePanel.Visible = true;
             ddlHidePanel.SelectedValue = "3";
             try
             {
                 BindChart_Grid();
             }

             catch (Exception ex)
             {
                 LogExceptions.WriteExceptionLog(ex);
             }
         }
     }
     private void BindCountry()
     {
         //Location.DAL.LocationDataContext location = new  Location.DAL.LocationDataContext();

         DataTable dt = new DataTable();
         dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, "select ID,Country from Country where ID in (select CountryID from projects where OwnerGroupID =@Programme)",
             new SqlParameter("@Programme", int.Parse(ddlprojectProgramme.SelectedValue))).Tables[0];
         ddlCountry.DataSource = dt;
         // ddlCountry.DataSource = from r in location.CountryClasses orderby r.Country1 select new { r.ID, r.Country1 } ;
         ddlCountry.DataValueField = "ID";
         ddlCountry.DataTextField = "Country";
         ddlCountry.DataBind();

         ddlCountry.Items.Insert(0, new ListItem("Please select...", "0"));
     }
     protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlReportType.SelectedValue == "1")
         {
             Toggle();
             BindCountry();
             btn_ViewReport.Visible = false;
             lblCoutry.Visible = true;
             ddlCountry.Visible = true;
             ingSearch.Visible = true;             
         }
         else if (ddlReportType.SelectedValue == "2")
         {
             Toggle();
             lblCoutry.Visible = false;
             ddlCountry.Visible = false;
             ingSearch.Visible = false;
             btn_ViewReport.Visible = true;
         }
        
     }
     protected void btn_ViewReport_Click(object sender, EventArgs e)
     {
         Toggle();
         //int portfolioID = Convert.ToInt32(ddlPortfolios.SelectedValue);
         int progID = Convert.ToInt32(ddlprojectProgramme.SelectedValue);
         int subprogID = Convert.ToInt32(ddlSubProgramme.SelectedValue);
         int benefitID = int.Parse(ddlType.SelectedValue);
         Page.ClientScript.RegisterStartupScript(typeof(string), "onLoad", "<script type='text/javascript'>window.open('WF/Reports/BenefitTrackingSummaryReport.aspx?PortfolioID=" + sessionKeys.PortfolioID + "&ProgID=" + progID + "&SubprogID=" + subprogID + "&BenefitID=" + benefitID + "','_blank');</script>");
     }
     protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
     {
         try
         {
             BindChart_Grid();
             Toggle();
             ddlReportType.SelectedValue = "0";
             lblCoutry.Visible = false;
             ddlCountry.Visible = false;
             ingSearch.Visible = false;
             btn_ViewReport.Visible = false;
         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
     }

     [WebMethod(EnableSession = true)]
     public static List<TimelineChart> GetChartData1(string projectGroup, string projectId, string subProgrammeId)
     {
         var programmeid = sessionKeys.ProgrammeID;
         var subprogrammeid = HttpContext.Current.Session["SubProgramme"];
         List<TimelineChart> dataList = new List<TimelineChart>();
         try
         {
            
             IProjectRepository<ProjectMgt.Entity.ProjectDetails> pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();
             IProjectRepository<ProjectMgt.Entity.ProjectTaskItem> ptRepository = new ProjectRepository<ProjectMgt.Entity.ProjectTaskItem>();
             List<ProjectMgt.Entity.ProjectDetails> pList = new List<ProjectDetails>();
             List<ProjectMgt.Entity.ProjectTaskItem> ptList = new List<ProjectMgt.Entity.ProjectTaskItem>();
             if (sessionKeys.ProgrammeID > 0 )
             {
                 pList = pRepository.GetAll().Where(o => o.ProjectStatusID == 2 && o.OwnerGroupID == programmeid).OrderByDescending(o => o.ProjectReference).ToList();

                 if (subprogrammeid != null)
                 {
                     if (Convert.ToInt32(subprogrammeid) > 0)
                         pList = pList.Where(o => o.SubProgramme == Convert.ToInt32(subprogrammeid)).OrderByDescending(o => o.ProjectReference).ToList();
                 }
             }
             else
                 pList = pRepository.GetAll().Where(o => o.ProjectStatusID == 2).OrderByDescending(o => o.ProjectReference).ToList();
             if (pList.Count > 0)
             {
                 var parray = pList.Select(o => o.ProjectReference).ToArray();
                 ptList = ptRepository.GetAll().Where(o => parray.Contains(o.ProjectReference.Value)).ToList();
             }
             foreach (var p in pList)
             {
                 TimelineChart details = new TimelineChart();
                 details.Project = p.ProjectReferenceWithPrefix;
                 details.Name = p.ProjectTitle;
                 var sdate = ptList.Where(o => o.ProjectReference == p.ProjectReference).Select(o => o.StartDate).Min();
                 if (sdate == null)
                     sdate = p.StartDate.Value;
                 var edate = ptList.Where(o => o.ProjectReference == p.ProjectReference).Select(o => o.CompletionDate).Max();
                 if (edate == null)
                     edate = p.ProjectEndDate.Value;
                 details.sday = sdate.Value.Day;
                 details.smonth = sdate.Value.Month;
                 details.syear = sdate.Value.Year;
                 details.eday = edate.Value.Day;
                 details.emonth = edate.Value.Month;
                 details.eyear = edate.Value.Year;

                 dataList.Add(details);
             }

         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
         return dataList;
     }
     [WebMethod(EnableSession = true)]
     public static List<TimelineChart> GetChartData2(string projectGroup, string projectId, string subProgrammeId)
     {
         var programmeid = sessionKeys.ProgrammeID;
         var subprogrammeid = HttpContext.Current.Session["SubProgramme"];
         List<TimelineChart> dataList = new List<TimelineChart>();
         try
         {

             IProjectRepository<ProjectMgt.Entity.ProjectDetails> pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();
             IProjectRepository<ProjectMgt.Entity.ProjectTaskItem> ptRepository = new ProjectRepository<ProjectMgt.Entity.ProjectTaskItem>();
             List<ProjectMgt.Entity.ProjectDetails> pList = new List<ProjectDetails>();
             List<ProjectMgt.Entity.ProjectTaskItem> ptList = new List<ProjectMgt.Entity.ProjectTaskItem>();
             if (sessionKeys.ProgrammeID > 0 )
             {
                 pList = pRepository.GetAll().Where(o => o.ProjectStatusID == 1 && o.OwnerGroupID == programmeid).OrderByDescending(o => o.ProjectReference).ToList();
                 if (subprogrammeid != null)
                 {
                     if (Convert.ToInt32(subprogrammeid) > 0)
                         pList = pList.Where(o => o.SubProgramme == Convert.ToInt32(subprogrammeid)).OrderByDescending(o => o.ProjectReference).ToList();
                 }
             }
             else
                 pList = pRepository.GetAll().Where(o => o.ProjectStatusID == 1).OrderByDescending(o => o.ProjectReference).ToList();

             if(pList.Count >0)
             {
                 var parray = pList.Select(o => o.ProjectReference).ToArray();
                 ptList = ptRepository.GetAll().Where(o => parray.Contains(o.ProjectReference.Value)).ToList();
             }

             foreach (var p in pList)
             {
                 TimelineChart details = new TimelineChart();
                 details.Project = p.ProjectReferenceWithPrefix;
                 details.Name = p.ProjectTitle;
                 var sdate = ptList.Where(o => o.ProjectReference == p.ProjectReference).Select(o => o.ProjectStartDate).Min();
                 if (sdate == null)
                     sdate = p.StartDate.Value;
                 var edate = ptList.Where(o => o.ProjectReference == p.ProjectReference).Select(o => o.ProjectEndDate).Max();
                 if (edate == null)
                     edate = p.ProjectEndDate.Value;
                 details.sday = sdate.Value.Day;
                 details.smonth = sdate.Value.Month;
                 details.syear = sdate.Value.Year;
                 details.eday = edate.Value.Day;
                 details.emonth = edate.Value.Month;
                 details.eyear = edate.Value.Year;

                 dataList.Add(details);
             }

         }
         catch (Exception ex)
         {
             LogExceptions.WriteExceptionLog(ex);
         }
         return dataList;
     }
     public class TimelineChart
     {
         public string Project { get; set; }
         public string Name { get; set; }
         public int syear { get; set; }
         public int smonth { get; set; }
         public int sday { get; set; }
         public int eyear { get; set; }
         public int emonth { get; set; }
         public int eday { get; set; }
     }
     public class ProgramSummaryCls
     {
         public string state { get; set; }
         public double ProjectValue { get; set; }
         public double ActualcosttoDate { get; set; }
         public double Variances { get; set; }
         public double Invoiced { get; set; }
     }
     public class ProgramSummaryCls1
     {
         public string state { get; set; }
         public double Late { get; set; }
         public double OnTime { get; set; }
         public double Complete { get; set; }
     }
     public class BenfitReportCls1
     {
         public string state { get; set; }
         public double ActualtoDate { get; set; }
         public double PlannedtoDate { get; set; }
         public double TargettoEnd { get; set; }
     }
}