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
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Deffinity.ProgrammeManagers;
using Microsoft.ApplicationBlocks.Data;
public partial class ProjectRiskItems : BasePage
{
    DisBindings getData = new DisBindings();
    SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    
    string ac2pid;
    string project;
    string contractorID;
    public int i;
    public string error;
    int RiskRef;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblerror1.Text = "";
        //Master.ErrorMsg = "";

        //Master.PageHead = Resources.DeffinityRes.ProjectManagement;// "Project Management";
        linkToRisk.NavigateUrl = string.Format("~/WF/Projects/ProjectRisks.aspx?Project={0}",QueryStringValues.Project);
        try
        {
            if (!IsPostBack)
            {

                //txtExposure.Text = "0";
                fillDropdownlist(DdlRiskType);
                // fillDropdownlist(DdlPGroups);
                fillDropdownlist(DdlRptStatus);
                fillDropdownlist(DdlStatus);
                fillDropdownlist(DdlRagStatus);
                fillDropdownlist(ddlAssignedTo);
                fillDropdownlist(ddlRiskCoordinator);
                
                //
                if (Request.QueryString["RiskRef"] != null)
                {
                    RiskRef = Convert.ToInt32(Request.QueryString["RiskRef"].ToString());
                    if (RiskRef == 0)
                    {
                        //to perform insert add
                        int Riskref1 = GetRiskRef();
                        txtRiskReference.Text = Convert.ToString(Riskref1 + 1);
                        DateTime CurrentDate=System.DateTime.Now;                        
                        txtDateRaised.Text = Convert.ToDateTime(CurrentDate.ToString()).ToShortDateString();
                    }
                    else
                    {
                        //btnAdd.Visible = false;
                        txtRiskReference.Text = RiskRef.ToString();
                        selectRisk(Convert.ToInt32(RiskRef.ToString()));
                        pnlrisk.Visible = true;

                    }
                }
                CheckUserRole();
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    #region functions
    private void BindGrid()
    {
        try
        {
            GridView1.DataSourceID = "Mysource";
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    
    }
    private void SelectRiskItems(string Ritem)
    {
        filldropdownlistaddrisk();
        int ID = Convert.ToInt32(Ritem);
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM AC2P_RiskItems WHERE ID =" + ID, conn);
        SqlDataReader dr1 = cmd.ExecuteReader();
        try
        {
            if (dr1.Read())
            {

                dl_AddRisk.SelectedValue = dr1["MitigationAction"].ToString();
                string s = dr1["MitigationAction"].ToString();
                ddlAssignedTo.SelectedValue = dr1["AssignedTo"].ToString();
                txtActionDeadline.Text = getData.getDate(dr1["ActionDeadline"].ToString());
                DdlRagStatus.SelectedValue = dr1["RagStaus"].ToString();
                DdlStatus.SelectedIndex = Convert.ToInt32(dr1["Status"].ToString());
                HdRiskItem.Value = dr1["ID"].ToString();
                txtResolution.Text = dr1["Resolution"].ToString();

            }
            dr1.Close();
            conn.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        finally
        { conn.Close(); }


    }
    protected string getEditImage(string ID)
    {
        string str1;
        str1 = "<a href=ProjectRiskItems.aspx?project=" + project + "&RiskRef=" + RiskRef + "&EditItem=" + ID + "><img src=images/btn_save.gif border=0 alt=edit></a>";

        return str1;

    }
    
    protected string getImage(string RagStaus)
    {
        string str1;
        if (RagStaus == "AMBER")
        {
            str1 = "<img src=images/indcate_yellow.png  style='border:0px'>";
        }
        else if (RagStaus == "RED")
        {
            str1 = "<img src=images/indcate_red.png  style='border:0px'>";
        }
        else
        {
            str1 = "<img src=images/indcate_green.png  style='border:0px'>";
        }

        return str1;

    }
    

    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    private bool validRiskItem()
    {
        string s;
        bool Myval = true;
        string strTemp = "";
        int i = 1;
        lblError.Text = "";
       
        if (i == 1)
        {
            if (dl_AddRisk.SelectedItem.Text == "")
            {
                strTemp += "Please enter data in title fields <br />";
                Myval = false;
               
            }
           
            else if (txtActionDeadline.Text == "")
            {
                strTemp += "Please select date in date field <br />";
                Myval = false;

            }
            else if ((DdlRagStatus.SelectedIndex == 0) || (DdlStatus.SelectedIndex == 0) || (ddlAssignedTo.SelectedIndex == 0))
            {
                strTemp += "Please select data in dropdown fields <br />";
                Myval = false;

            }
            i = 2;
        }
        lblError.Text = strTemp;
        return Myval;

    }
   
    protected DataTable getRateType()
    {
        DataTable dt;
        dt = new DataTable();
        //Add columns to the data table.
        dt.Columns.Add("ID");
        dt.Columns["ID"].DataType = System.Type.GetType("System.Int32");
        dt.Columns.Add("Status");

        //Add rows to the data table.
        DataRow row1 = dt.NewRow();
        row1["ID"] = "1";
        row1["Status"] = "Live";
        dt.Rows.Add(row1);
        DataRow row2 = dt.NewRow();
        row2["ID"] = "2";
        row2["Status"] = "Pending";
        dt.Rows.Add(row2);
        DataRow row3 = dt.NewRow();
        row3["ID"] = "3";
        row3["Status"] = "Cancelled";
        dt.Rows.Add(row3);
        return dt;
    }


    protected void insertRiskItems()
    {
        try
        {
            if (dl_AddRisk.Visible == true)
            {
                ac2pid = "0";
                SqlCommand myCommand = new SqlCommand("DN_InsertRiskItems", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                string Risktmp = txtRiskReference.Text;
                // myCommand.Parameters.Add(new SqlParameter("@AC2PID", Convert.ToInt32(ac2pid)));
                myCommand.Parameters.Add(new SqlParameter("@Project", QueryStringValues.Project));
                myCommand.Parameters.Add(new SqlParameter("@RiskReference", Convert.ToInt32(txtRiskReference.Text)));
                myCommand.Parameters.Add(new SqlParameter("@MitigationAction", getData.getDdlval(dl_AddRisk.SelectedValue)));
                myCommand.Parameters.Add(new SqlParameter("@AssignedTo", ddlAssignedTo.SelectedValue));
                myCommand.Parameters.Add(new SqlParameter("@ActionDeadline", Convert.ToDateTime((txtActionDeadline.Text).ToString())));
                myCommand.Parameters.Add(new SqlParameter("@RagStaus", DdlRagStatus.SelectedItem.Text));
                myCommand.Parameters.Add(new SqlParameter("@Status", getData.getDdlval(DdlStatus.SelectedValue)));
                myCommand.Parameters.Add(new SqlParameter("@Resolution", txtResolution.Text.Trim()));
                

                myCommand.Connection.Open();
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
            }
            else 
            {
                
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }



    public DataSet GetCordinatorsRisk()
    {

        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBString"]);
        SqlCommand cmd = new SqlCommand("DN_CordinatorsRisk", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ProjectReference", QueryStringValues.Project));

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds1 = new DataSet();
        da.Fill(ds1);
        return ds1;

    }

    public DataSet getAssign()
    {//getAssign

        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBString"]);
        SqlCommand cmd = new SqlCommand("AMPS_Cname", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds1 = new DataSet();
        da.Fill(ds1);
        return ds1;

    }
    public DataSet StatusType()
    {
        //SQLConnection
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring1"]);
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM AC2PStatus", conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "AC2PStatus");
        return ds;
    }
    protected void UpdateRisk()
    {
        int Delay = (string.IsNullOrEmpty(txtdelay.Text) ? 0 : Convert.ToInt32(txtdelay.Text));
        try
        {

            int ExportVal = 0;

            int intddlProbability = Convert.ToInt32(ddlProbability.SelectedValue);
            int intddlDegreeofImpact = Convert.ToInt32(ddlDegreeofImpact.SelectedValue);
            ExportVal = intddlDegreeofImpact * intddlProbability;

            SqlCommand myCommand = new SqlCommand("DN_UpdateRisk", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
     
            //myCommand.Parameters.Add(new SqlParameter("@ProjectGroup", getData.getDdlval(DdlPGroups.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@RiskReference", Convert.ToInt32(txtRiskReference.Text)));
            myCommand.Parameters.Add(new SqlParameter("@Title", txtRiskTitle.Text));
            myCommand.Parameters.Add(new SqlParameter("@DateRaised", Convert.ToDateTime((txtDateRaised.Text).ToString())));
            myCommand.Parameters.Add(new SqlParameter("@Co_ordinator", ddlRiskCoordinator.SelectedValue));
            myCommand.Parameters.Add(new SqlParameter("@MinimumCost", getData.getDouble(txtMinimumCost.Text)));
            myCommand.Parameters.Add(new SqlParameter("@MaximumCost", getData.getDouble(txtMaximumCost.Text)));
            myCommand.Parameters.Add(new SqlParameter("@RiskType", getData.getDdlval(DdlRiskType.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@ReportStatus", getData.getDdlval(DdlRptStatus.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@ClosureCriteria", txtClosureCriteria.Text));
            myCommand.Parameters.Add(new SqlParameter("@Delay", Delay));
            myCommand.Parameters.Add(new SqlParameter("@DegreeofImpact", Convert.ToInt32(ddlDegreeofImpact.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@NextReviewDate", Convert.ToDateTime(string.IsNullOrEmpty(txtNextReviewDate.Text.Trim()) ? "01/01/1900" : txtNextReviewDate.Text.Trim())));
            myCommand.Parameters.Add(new SqlParameter("@Probability", Convert.ToInt32(ddlProbability.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@Exposure", ExportVal));         


            myCommand.Connection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();

           // SqlCommand cmd = new SqlCommand("UPDATE AC2P_Risks SET Title ='" + txtRiskTitle.Text + "', DateRaised ='" +Convert.ToDateTime(txtDateRaised.Text).ToShortDateString()+ "', Co_ordinator ='" + txtRiskCo_ordinator.Text + "' , MinimumCost =" + txtMinimumCost.Text + ", MaximumCost =" + txtMaximumCost.Text + ", RiskType =" + DdlRiskType.SelectedIndex + ", ReportStatus =" + DdlRptStatus.SelectedIndex + ", ClosureCriteria ='" + txtClosureCriteria.Text + "', ProjectGroup =" + DdlPGroups.SelectedIndex + "WHERE RiskReference='" + RiskId + "'", myConnection);
            //cmd.Connection.Open();
           // cmd.ExecuteNonQuery();
            //cmd.Connection.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        
    }
    protected void selectRisk(int RiskId)
    {
        try
        {

            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select DegreeofImpact,NextReviewDate,Probability,Exposure,AC2PID,ProjectReference,ContractorID,Title,DateRaised,Co_ordinator,MinimumCost,MaximumCost,RiskType,ReportStatus,ClosureCriteria,Delay from AC2P_Risks where RiskReference=" + RiskId.ToString(), myConnection);
            cmd.Connection.Open();
            dr = cmd.ExecuteReader();


            //myCommand.Parameters.Add(new SqlParameter("@DegreeofImpact", Convert.ToInt32(ddlDegreeofImpact.SelectedValue)));
            //myCommand.Parameters.Add(new SqlParameter("@NextReviewDate", Convert.ToDateTime((txtNextReviewDate.Text.Trim()).ToString())));
            //myCommand.Parameters.Add(new SqlParameter("@Probability", Convert.ToInt32(ddlProbability.SelectedValue)));
            //myCommand.Parameters.Add(new SqlParameter("@Exposure", Convert.ToInt32(txtExposure.Text.Trim())));         

            while (dr.Read())
            {
                txtRiskTitle.Text = dr["Title"].ToString();
                txtDateRaised.Text = string.IsNullOrEmpty(dr["DateRaised"].ToString())? string.Empty:Convert.ToDateTime(dr["DateRaised"].ToString()).ToShortDateString() ;
                txtMaximumCost.Text = dr["MaximumCost"].ToString();
                txtMinimumCost.Text = dr["MinimumCost"].ToString();
                ddlRiskCoordinator.SelectedValue = dr["Co_ordinator"].ToString();
                txtClosureCriteria.Text = dr["ClosureCriteria"].ToString();
               // DdlPGroups.SelectedValue = dr["ProjectGroup"].ToString();
                DdlRiskType.SelectedValue = dr["RiskType"].ToString();
                DdlRptStatus.SelectedValue = dr["ReportStatus"].ToString();
                txtdelay.Text = dr["Delay"].ToString();// (string.IsNullOrEmpty(dr["Delay"].ToString()) ? 0 : dr["Delay"].ToString()); 

                ddlDegreeofImpact.SelectedValue=dr["DegreeofImpact"].ToString();
                txtNextReviewDate.Text = string.IsNullOrEmpty(dr["NextReviewDate"].ToString()) || dr["NextReviewDate"].ToString().Contains("01/01/1900")? string.Empty : Convert.ToDateTime(dr["NextReviewDate"].ToString()).ToShortDateString(); 
                ddlProbability.SelectedValue = dr["Probability"].ToString(); 
                txtExposure.Text = dr["Exposure"].ToString(); 

            }
            dr.Close();
            cmd.Connection.Close();
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        filldropdownlistaddrisk();


    }
    protected void insertRisk()
    {
        int Delay = (string.IsNullOrEmpty(txtdelay.Text) ? 0 : Convert.ToInt32(txtdelay.Text));

        try
        {

            int ExportVal = 0;
            
            int intddlProbability=Convert.ToInt32(ddlProbability.SelectedValue);
            int intddlDegreeofImpact = Convert.ToInt32(ddlDegreeofImpact.SelectedValue);
            ExportVal = intddlDegreeofImpact * intddlProbability;

            SqlCommand myCommand = new SqlCommand("DN_InsertRisk", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add(new SqlParameter("@AC2PID", Convert.ToInt32(ac2pid)));
            myCommand.Parameters.Add(new SqlParameter("@ProjectReference", Convert.ToInt32(Request.QueryString["Project"].ToString())));
            contractorID = "0";
            myCommand.Parameters.Add(new SqlParameter("@ContractorID", Convert.ToInt32(contractorID)));
           // myCommand.Parameters.Add(new SqlParameter("@ProjectGroup",  getData.getDdlval(DdlPGroups.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@RiskReference", Convert.ToInt32(txtRiskReference.Text.Trim())));
            myCommand.Parameters.Add(new SqlParameter("@Title", txtRiskTitle.Text.Trim()));
            myCommand.Parameters.Add(new SqlParameter("@DateRaised",Convert.ToDateTime((txtDateRaised.Text).ToString())));
            myCommand.Parameters.Add(new SqlParameter("@Co_ordinator", ddlRiskCoordinator.SelectedValue));
            myCommand.Parameters.Add(new SqlParameter("@MinimumCost", txtMinimumCost.Text.Trim()));
            myCommand.Parameters.Add(new SqlParameter("@MaximumCost", txtMaximumCost.Text.Trim()));
            myCommand.Parameters.Add(new SqlParameter("@RiskType", getData.getDdlval(DdlRiskType.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@ReportStatus", getData.getDdlval(DdlRptStatus.SelectedValue)));
            myCommand.Parameters.Add(new SqlParameter("@ClosureCriteria", txtClosureCriteria.Text.Trim()));
            myCommand.Parameters.Add(new SqlParameter("@Delay",Delay));
            SqlParameter RiskIDOutParam = new SqlParameter("@RiskID", SqlDbType.Int);
            RiskIDOutParam.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(new SqlParameter("@DegreeofImpact", Convert.ToInt32(ddlDegreeofImpact.SelectedValue)));

            myCommand.Parameters.Add(new SqlParameter("@NextReviewDate", Convert.ToDateTime(string.IsNullOrEmpty(txtNextReviewDate.Text.Trim()) ? "01/01/1900" : txtNextReviewDate.Text.Trim())));            
            myCommand.Parameters.Add(new SqlParameter("@Probability", Convert.ToInt32(ddlProbability.SelectedValue)));

            myCommand.Parameters.Add(new SqlParameter("@Exposure",ExportVal));         
            myCommand.Parameters.Add(RiskIDOutParam);
            myCommand.Connection.Open();
            myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();
            string RiskRef=RiskIDOutParam.Value.ToString();
            pnlrisk.Visible = true;
            lblerror1.Text = Resources.DeffinityRes.Risk;// "Risk ";
            txtRiskReference.Text = RiskRef;
            //Response.Redirect("~/ProjectRiskItems.aspx?project=" + QueryStringValues.Project.ToString() + "&RiskRef=" + txtRiskReference.Text);
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);

        }
    }
    protected void fillDropdownlist(DropDownList dl)
    {
        try
        {

            DataTable dt = new DataTable();


            if (dl.ID == "DdlRiskType")
            {


                getData.DdlBindSelect(DdlRiskType, "select ID,IssueTypeName from issuetype", "ID", "IssueTypeName", false, false, true);
                //getData.DdlBindSelect(DdlRiskType, "select ID,RiskType from RiskTypes", "ID", "RiskType", false,false,true);
                
            }
            //else if (dl.ID == "DdlPGroups")
            //{
            //    getData.DdlBindSelect(DdlPGroups, "select ID,OperationsOwners from OperationsOwners where Visible='Y'", "ID", "OperationsOwners", false, true);
               
            //}
            else if (dl.ID == "DdlRptStatus")
            {
                getData.DdlBindSelect(DdlRptStatus, "select ID,Status from AC2PStatus", "ID", "Status", false, false, true);
                               
            }
            else if (dl.ID == "DdlStatus")
            {
                getData.DdlBindSelect(DdlStatus, "select ID,Status from ItemStatus", "ID", "Status", false, false, true);              
               
            }
            else if (dl.ID == "DdlRagStatus")
            {
                DdlRagStatus.Items.Insert(0, new ListItem("Please select...","0"));
                DdlRagStatus.Items.Insert(1, "AMBER");
                DdlRagStatus.Items.Insert(2, "RED");
                DdlRagStatus.Items.Insert(3, "GREEN");
            }
            else if (dl.ID == "ddlRiskCoordinator")
            {
                ddlRiskCoordinator.DataSource = GetCordinatorsRisk();
                ddlRiskCoordinator.DataTextField = "ContractorName";
                ddlRiskCoordinator.DataValueField = "ContractorID";
                ddlRiskCoordinator.DataBind();

                ddlRiskCoordinator.Items.Insert(0, new ListItem("Please select...", "0"));
                    
            }
                
            else if (dl.ID == "ddlAssignedTo")
            {

                //ddlAssignedTo
                DataTable dt1 = new DataTable();
                dt1= SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo", new SqlParameter("@ProjectRefrence", QueryStringValues.Project)
      , new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
                ddlAssignedTo.DataSource = dt1;// getAssign();
                //ddlAssignedTo.DataTextField = "cname";
                //ddlAssignedTo.DataValueField = "cname";
                ddlAssignedTo.DataTextField = "ContractorName";
                ddlAssignedTo.DataValueField = "ID";
                ddlAssignedTo.DataBind();

                //ddlAssignedTo.Items.Insert(0, new ListItem("Please select...", "0"));
                if (HidAssignTo.Value != "")
                {
                    ddlAssignedTo.Items.Add(HidAssignTo.Value);
                    HidAssignTo.Value = "";
                }


                dt.Clear();

            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    protected int GetRiskRef()
    {
        int i=0;
        try
        {
            SqlCommand cmd = new SqlCommand("select max(RiskReference) from AC2P_Risks", myConnection);
            cmd.Connection.Open();
            string st = cmd.ExecuteScalar().ToString();

            if ((st == null) || (st == "0") || (st == ""))
            {
                i = 0;
            }
            else
            {
                i = Convert.ToInt32(st);

            }
            cmd.Connection.Close();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
        return i;


    }
   
    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.ManageRisk))
        //{
        //    Master.ErrorMsg = Resources.DeffinityRes.UserdsnthavrghtstoManageRisks;//"User doesn't have rights to Manage Risks";
        //    return;
        //}
        //Please enter Next Review Date
        //to insert into AC2P_Risks table

        DateTime dtt1 = Convert.ToDateTime(txtDateRaised.Text.Trim());

        string nextreviewdate = txtNextReviewDate.Text.Trim();

        if (DdlRptStatus.SelectedValue != "3")
        {



            DateTime dtt2 = Convert.ToDateTime(string.IsNullOrEmpty(nextreviewdate) ? "01/01/1900" : nextreviewdate);


            DateTime dt1 = new DateTime(dtt1.Year, dtt1.Month, dtt1.Day);
            DateTime dt2 = new DateTime(dtt2.Year, dtt2.Month, dtt2.Day);
            if (nextreviewdate != "01/01/1900")
            {
                if (DateTime.Compare(dt1, dt2) < 0)
                {
                    //Console.WriteLine("second date is larger than the first date");
                }
                else if (DateTime.Compare(dt1, dt2) == 0)
                {
                    //Console.WriteLine("second date is same as first date");
                }
                else
                {
                    //Console.WriteLine("second date is smaller than the first date");

                    lblerror1.Text = Resources.DeffinityRes.NextReviewDatecannotbesmaller;
                    return;

                }
            }
            else
            {
                lblerror1.Text = "Please enter Next Review Date";
                return;
            }
        }
       

       

        if (DdlRiskType.Visible == true)
        {
            if (Convert.ToInt32(Request.QueryString["RiskRef"].ToString()) == 0)
            {
                insertRisk();
                //Response.Redirect("~/ProjectRiskItems.aspx?project=" + QueryStringValues.Project.ToString() + "&RiskRef=" + txtRiskReference.Text);
                
                Response.Redirect("~/WF/Projects/ProjectRisks.aspx?project=" + QueryStringValues.Project.ToString());
            }
            else
            {
                UpdateRisk();
                Response.Redirect("~/WF/Projects/ProjectRiskItems.aspx?project=" + QueryStringValues.Project.ToString() + "&RiskRef=" + txtRiskReference.Text);
            }

        }
        else
        {
            lblerror1.Text = Resources.DeffinityRes.ClickonAddRisktoDisInDropdown;// "Note: Please click on add risk type to display in dropdown list";
        }


        //}
        //else
        //{
        //    // Response.Write("check");
        //}



    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

       
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        //if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.ManageRisk))
        //{
        //    Master.ErrorMsg =Resources.DeffinityRes.UserdsnthavrghtstoManageRisks;// "User doesn't have rights to Manage Risks";
        //    return;
        //}
        if (dl_AddRisk.Visible != false)
        {
            if (ddlAssignedTo.Visible != false)
            {



                    lblerror1.Text = "";                
                    insertRiskItems();
                    BindGrid();
                    txtActionDeadline.Text = "";
                    ddlAssignedTo.SelectedIndex = 0;
                    DdlRagStatus.SelectedIndex = 0;
                    DdlStatus.SelectedIndex = 0;
                    dl_AddRisk.SelectedIndex = 0;
                    
            }

            else
            {
                lblerror1.Text = Resources.DeffinityRes.ClickonAddAssigntoDisInDropdown;//"Note: Please click on add assign to to display in dropdown list";
            }
        }
        else
        {

            lblerror1.Text = Resources.DeffinityRes.ClckonAddMitigationtoDisInDropdown;// "Note: Please click on add  Mitigation Action to display in dropdown list";
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string Proj = QueryStringValues.Project.ToString();      
        Response.Redirect("~/WF/Projects/ProjectRisks.aspx?project=" + Proj);
    }
    protected void btnCancelItems_Click(object sender, EventArgs e)
    {
        //clear text boxes
        dl_AddRisk.SelectedIndex = 0;
        txtAssignedTo.Text = "";
        txtActionDeadline.Text = "";
       // txtMitigationAction.Text = "";
        ddlAssignedTo.SelectedIndex = 0;
        DdlRagStatus.SelectedIndex = 0;
        DdlStatus.SelectedIndex = 0;
       

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WF/Projects/ProjectRisks.aspx?Project=" + QueryStringValues.Project.ToString());
    }
    protected void btnAddIcon_Click(object sender, EventArgs e)
    {
        txtRiskType.Visible = true;
        btnCancelRiskType.Visible = true;
        btnAddRiskType.Visible = true;
        btnAddIcon.Visible = false;
        DdlRiskType.Visible = false;

    }
    protected void btnAddRiskType_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (txtRiskType.Text.Trim() != "")
            {
                SqlCommand cmd = new SqlCommand("DN_InsertRiskType1", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RiskType", txtRiskType.Text.Trim()));
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                
                txtRiskType.Visible = false;
                btnCancelRiskType.Visible = false;
                btnAddRiskType.Visible = false;
                btnAddIcon.Visible = true;
                DdlRiskType.Visible = true;
                txtRiskType.Text = "";
                //fill dropdown list
                fillDropdownlist(DdlRiskType);
                //to set new entry in selected position
                //getData.selectDdlNewEntry(txtRiskType.Text.Trim(), DdlRiskType);
            }
            else
            {
                lblError.Text = Resources.DeffinityRes.Plsenterdatainrisktypefield;// "Please enter data in risk type field ";
            }

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }


    }
    protected void btnCancelRiskType_Click(object sender, EventArgs e)
    {
        txtRiskType.Visible = false;
        btnCancelRiskType.Visible = false;
        btnAddRiskType.Visible = false;
        btnAddIcon.Visible = true;
        DdlRiskType.Visible = true;
        txtRiskType.Text = "";

    }
    protected void btnAddAssignto_Click(object sender, EventArgs e)
    {
        ddlAssignedTo.Visible = false;
        txtAssignedTo.Visible = true;
        btnAddAssignto.Visible = false;
        btnAssignto.Visible = true;
        btnCancelAssignto.Visible = true;

    }
    protected void btnAssignto_Click(object sender, EventArgs e)
    {
        ddlAssignedTo.Visible = true;
        txtAssignedTo.Visible = false;
        btnAddAssignto.Visible = true;
        btnAssignto.Visible = false;
        btnCancelAssignto.Visible = false;

        if (txtAssignedTo.Text.Trim() != "")
        {
            HidAssignTo.Value = txtAssignedTo.Text;
        }
        else
        {
            lblError.Text = Resources.DeffinityRes.Plsenterdatainassignedfield;// "Please enter data in assigned to field";
        }
        fillDropdownlist(ddlAssignedTo);
        //to set new entry in selected position
        //getData.selectDdlNewEntry(txtAssignedTo.Text.Trim(), ddlAssignedTo);
        txtAssignedTo.Text = "";


    }
    protected void btnCancelAssignto_Click(object sender, EventArgs e)
    {
        ddlAssignedTo.Visible = true;
        txtAssignedTo.Visible = false;
        btnAddAssignto.Visible = true;
        btnAssignto.Visible = false;
        btnCancelAssignto.Visible = false;
        txtAssignedTo.Text = "";
        HidAssignTo.Value = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        //gridbind();
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
       
    }

    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!PermissionManager.IsPermitted(Convert.ToInt32(Request.QueryString["Project"].ToString()), Convert.ToInt32(Session["UID"]), PermissionManager.PermissionsTo.ManageRisk))
            //{
            //    Master.ErrorMsg = Resources.DeffinityRes.UserdsnthavrghtstoManageRisks;//"User doesn't have rights to Manage Risks";
            //    return;
            //}
            if (dl_AddRisk.Visible != false)
            {
                if (ddlAssignedTo.Visible != false)
                {
                    SqlCommand myCommand = new SqlCommand("DN_UpdateRiskItems", myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.Add(new SqlParameter("@ID", Convert.ToInt32(HdRiskItem.Value)));
                    myCommand.Parameters.Add(new SqlParameter("@MitigationAction", getData.getDdlval(dl_AddRisk.SelectedValue)));
                    myCommand.Parameters.Add(new SqlParameter("@AssignedTo", ddlAssignedTo.SelectedValue));
                    myCommand.Parameters.Add(new SqlParameter("@ActionDeadline", txtActionDeadline.Text));
                    myCommand.Parameters.Add(new SqlParameter("@RagStaus", DdlRagStatus.SelectedValue));
                    myCommand.Parameters.Add(new SqlParameter("@Status", DdlStatus.SelectedValue));
                    myCommand.Parameters.Add(new SqlParameter("@Resolution", txtResolution.Text.Trim()));
                    myCommand.Connection.Open();
                    myCommand.ExecuteNonQuery();
                    myCommand.Connection.Close();

                    //clear values in controls  
                    dl_AddRisk.SelectedIndex = 0;
                    ddlAssignedTo.SelectedIndex = 0;
                    txtActionDeadline.Text = "";
                    DdlRagStatus.SelectedIndex = 0;
                    DdlStatus.SelectedIndex = 0;
                    ImageButton2.Visible = false;
                    btnAddItems.Visible = true;
                    GridView1.DataSourceID = "Mysource";
                    GridView1.DataBind();
                }

                else
                {
                    lblerror1.Text =Resources.DeffinityRes.ClickonAddAssigntoDisInDropdown;// "Note: Please click on add assign to to display in dropdown list";
                }
            }
            else
            {

                lblerror1.Text = Resources.DeffinityRes.ClckonAddMitigationtoDisInDropdown;// "Note: Please click on add  Mitigation Action to display in dropdown list";
            }
           


        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);  
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblid = (Label)GridView1.Rows[e.RowIndex].Cells[0].FindControl("lblID");
            string id = lblid.Text;
            DropDownList txtmigrationaction = (DropDownList)GridView1.Rows[e.RowIndex].Cells[1].FindControl("drMitigationAction");
            int migrationaction = int.Parse(txtmigrationaction.SelectedValue);
            DropDownList ddlassignedto = (DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("DropDownList3");
            string assignedto = ddlassignedto.SelectedItem.Text;
            TextBox txtActionDeadline = (TextBox)GridView1.Rows[e.RowIndex].Cells[3].FindControl("txtActionDeadline");
            string ActionDeadline = txtActionDeadline.Text;
            DropDownList ddlragstatus = (DropDownList)GridView1.Rows[e.RowIndex].Cells[4].FindControl("DropDownList1");
            string ragstatus = ddlragstatus.SelectedValue;
            string ragstatustext = ddlragstatus.SelectedItem.Text;
            DropDownList ddlstatus = (DropDownList)GridView1.Rows[e.RowIndex].Cells[5].FindControl("DropDownList2");
            int status = int.Parse(ddlstatus.SelectedValue);
            e.NewValues["ID"] = id;
            e.NewValues["MitigationAction"] = migrationaction;
            e.NewValues["AssignedTo"] = assignedto;
            e.NewValues["ActionDeadline"] = ActionDeadline;
            e.NewValues["RagStaus"] = ragstatustext;
            e.NewValues["Status"] = status;
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            i = GridView1.EditIndex;
            GridViewRow Row = GridView1.Rows[i];
            if (e.CommandName == "PanelNew")
            {

                Panel paneltxt = (Panel)GridView1.Rows[i].Cells[3].FindControl("panelddl");
                paneltxt.Visible = false;
                Panel panelddl = (Panel)GridView1.Rows[i].Cells[3].FindControl("paneltxt");
                panelddl.Visible = true;
            }
            else if (e.CommandName == "PanelCancel")
            {
                //int i = GridView1.EditIndex;
                //GridViewRow Row = GridView1.Rows[i];
                Panel paneltxt = (Panel)GridView1.Rows[i].Cells[3].FindControl("panelddl");
                paneltxt.Visible = true;
                Panel panelddl = (Panel)GridView1.Rows[i].Cells[3].FindControl("paneltxt");
                panelddl.Visible = false;
            }
            else if (e.CommandName == "Save")
            {
                //int i = GridView1.EditIndex;
                //GridViewRow Row = GridView1.Rows[i];
                TextBox txtAssignedTo = (TextBox)GridView1.Rows[i].Cells[2].FindControl("txtAssignedTo");
                DropDownList ddlAssignedTo = (DropDownList)GridView1.Rows[i].Cells[2].FindControl("DropDownList3");

                if (txtAssignedTo.Text != "")
                {
                    HidAssignTo.Value = txtAssignedTo.Text;
                }
                else
                {
                    lblError.Text = Resources.DeffinityRes.Plsenterdatainassignedfield;// "Please enter data in assigned to field";
                }


                ddlAssignedTo.Items.Insert(0, new ListItem("Please select...", "0"));
                if (HidAssignTo.Value != "")
                {
                    ddlAssignedTo.Items.Add(HidAssignTo.Value);
                    ddlAssignedTo.SelectedValue = HidAssignTo.Value;
                    HidAssignTo.Value = "";
                    Panel paneltxt = (Panel)GridView1.Rows[i].Cells[3].FindControl("panelddl");
                    paneltxt.Visible = true;
                    Panel panelddl = (Panel)GridView1.Rows[i].Cells[3].FindControl("paneltxt");
                    panelddl.Visible = false;

                }
               
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
 
    public void filldropdownlistaddrisk()
    {
        getData.DdlBindSelect(dl_AddRisk, "SELECT ID,MigatingActions FROM MasterMigatingActions where RiskTypeID = " + DdlRiskType.SelectedValue, "ID", "MigatingActions", false,false,true);
        
    }
    protected void btnAddMigration_Click(object sender, EventArgs e)
    {
        btnAddMigration.Visible = false;
        btn_MigrationPlus.Visible = true;
        btn_MigrationCancel.Visible = true;
        dl_AddRisk.Visible = false;
        txtMigration.Visible = true;
        
    }
    protected void btn_MigrationPlus_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtMigration.Text.Trim() != "")
            {
                if (DdlRiskType.SelectedValue != "0")
                {
                    Database db = DatabaseFactory.CreateDatabase("DBstring");
                    DbCommand cmd = db.GetStoredProcCommand("DN_MasterMigatingActions_add");                    
                    db.AddInParameter(cmd, "@RiskTypeID", DbType.Int32, DdlRiskType.SelectedValue);
                    db.AddInParameter(cmd, "@MigatingAction", DbType.String, txtMigration.Text.Trim());

                    db.ExecuteNonQuery(cmd);
                    cmd.Dispose();
                    //For successful insertion only 
                    btnAddMigration.Visible = true;
                    btn_MigrationPlus.Visible = false;
                    btn_MigrationCancel.Visible = false;
                    txtMigration.Visible = false;
                    dl_AddRisk.Visible = true;
                    filldropdownlistaddrisk();
                    
                }
                else
                {
                    lblError.Text = Resources.DeffinityRes.PleaseselectRiskTypeForCat;//"Please select risk type";
                }
            }
            else
            {
                lblError.Text = Resources.DeffinityRes.Plsenterdatainmigration;// "Please enter data in migration";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
       
        //to set new entry in selected position
       // getData.selectDdlNewEntry(txtMigration.Text.Trim(), dl_AddRisk);
        
    }
   
    protected void btn_MigrationCancel_Click(object sender, EventArgs e)
    {
        btnAddMigration.Visible = true;
        btn_MigrationPlus.Visible = false;
        btn_MigrationCancel.Visible = false;
        dl_AddRisk.Visible = true;
        txtMigration.Visible = false;
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int i = e.NewSelectedIndex;
        HiddenField HID = (HiddenField)GridView1.Rows[i].FindControl("HID");
        filldropdownlistaddrisk();
        SelectRiskItems(HID.Value);
        btnAddItems.Visible = false;
        ImageButton2.Visible = true;
        btnAddItems.Focus();
    }


    protected void ddlDegreeofImpact_SelectedIndexChanged(object sender, EventArgs e)
    {        
        int Exposure = Convert.ToInt32(ddlDegreeofImpact.SelectedValue) * Convert.ToInt32(ddlProbability.SelectedValue);
        txtExposure.Text = Exposure.ToString();

    }
    protected void ddlProbability_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Exposure = Convert.ToInt32(ddlDegreeofImpact.SelectedValue) * Convert.ToInt32(ddlProbability.SelectedValue);
        txtExposure.Text = Exposure.ToString();
    }
    #region Check Permission
    //03/06/2011 by sani

    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
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
    }
    private void Disable()
    {
        btnAdd.Enabled = false;
        btnAddAssignto.Enabled = false;
        btnAddIcon.Enabled = false;
        btnAddItems.Enabled = false;
        btnAddMigration.Enabled = false;
        btnAddRiskType.Enabled = false;
        btnAssignto.Enabled = false;
        ImageButton2.Enabled = false;
        btn_MigrationPlus.Enabled = false;
        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";


    }
    protected bool CommandField()
    {
        bool vis = true;
        try
        {
            if ((Request.QueryString["Project"] != null))
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
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return vis;

    }
    #endregion
}
