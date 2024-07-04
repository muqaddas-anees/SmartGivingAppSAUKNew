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
using CustomerLogicPermissions; 

public partial class deviationreport : System.Web.UI.Page
{
    int Report;
    Email mail = new Email();
    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBstring"]);
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    DbCommand cmd;
    DisBindings getData = new DisBindings();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Management";
        if (!Page.IsPostBack)
        {
            try
            {
                
                //change tabs visibility
                changeTabVisibility();
                //check the user type
                CheckUser();
                //get Approver Name
                GetApprover(QueryStringValues.Project);
                if (Request.QueryString["ID"] != null)
                {
                    binddata(int.Parse(Request.QueryString["ID"].ToString()));
                }
                else
                {
                    newrpt(QueryStringValues.Project);
                }
                CheckUserRole();
                BindManagerList();
                BindCustomer();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
          
        }
        
    }
    private void CheckUser()
    {
        if (sessionKeys.SID == 4)
        {
            Panel_PM.Visible = false;
        }
    
    }
    private void BindCustomer()
    {
        try
        {
            CLPermissionCS CLPClass = new CLPermissionCS();
            DataTable CTable = CLPClass.SelectPRCustomers(Convert.ToInt32(Request.QueryString["Project"]));
            ddlCustomer.DataSource = CTable;
            ddlCustomer.DataValueField = "ContractorID";
            ddlCustomer.DataTextField = "ContractorName";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void GetApprover(int p)
    {
        cmd = db.GetSqlStringCommand(string.Format("SELECT ContractorName,EmailAddress FROM Projects INNER JOIN Contractors ON Projects.OwnerID = Contractors.ID where ProjectReference ={0}",p));        
        using(IDataReader dr = db.ExecuteReader(cmd))
        {        
            while(dr.Read())
            {
                txtApproversName.Text = dr["ContractorName"].ToString();
                txtApproveemail.Text = dr["EmailAddress"].ToString();
                //lblCustomerName.Text = dr["ContractorName"].ToString();
            }
        }
    }
    private void changeTabVisibility()
    {
        if (Request.QueryString["Type"] != null)
        {
            //change tabs visibility 
            if (Request.QueryString["Type"].ToString() == "PM")
            {
                TabVisibility(true, false, false);
            }
            else if (Request.QueryString["Type"].ToString() == "resource")
            {
                TabVisibility(false, true, false);
            }
            else if (Request.QueryString["Type"].ToString() == "checkpoint")
            {
                TabVisibility(false, false, true);
            }

        }
        else { TabVisibility(true, false, false); }
    }
    private void TabVisibility(bool project,bool resource,bool checkpoints)
    {
        project_tabs.Visible = project;
        //resource_tabs.Visible = resource;
        //checkpoint_tabs.Visible = checkpoints;
    }

    private void BindManagerList()
    {
        int userId = sessionKeys.UID;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBString"].ToString());
        conn.Open();
        string cmdText = "SELECT v.ID,c.ContractorName as ManagerName from VariationPermission v join Contractors c on v.ManagerID = c.ID WHERE v.UserID=" + userId + " ORDER BY c.ContractorName";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        DataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        cmd.Dispose();
        conn.Close();
        lstManager.DataSource = ds;
        lstManager.DataValueField = "ID";
        lstManager.DataTextField = "ManagerName";
        lstManager.DataBind();
    }
    public void binddata(int ID)
    {
        try
        {
            
            cmd = db.GetStoredProcCommand("DN_DeviationReportSelect");
            db.AddInParameter(cmd, "@ID", DbType.Int32,ID);
            ds = db.ExecuteDataSet(cmd);        

            txtRequesterName.Text = ds.Tables[0].Rows[0]["RequesterName"].ToString();
            txtTelephone.Text = ds.Tables[0].Rows[0]["Telephone"].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
            txtCRSProjectManager.Text = ds.Tables[0].Rows[0]["CRSProjectManager"].ToString();
            txtBusinessHead.Text = ds.Tables[0].Rows[0]["BusinessHead"].ToString();
            txtBusinessGroup.Text = ds.Tables[0].Rows[0]["BusinessGroup"].ToString();
            txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
            txtProjectLocation.Text = ds.Tables[0].Rows[0]["ProjectLocation"].ToString();
            txtScope.Text = ds.Tables[0].Rows[0]["ScopeOfProject"].ToString();
            txtDetailedExplanation.Text = ds.Tables[0].Rows[0]["DetailedExplanation"].ToString();
            txtJustification.Text = ds.Tables[0].Rows[0]["Justification"].ToString();
            txtProposedCompensation.Text = ds.Tables[0].Rows[0]["ProposedCompensation"].ToString();
            txtRemediationDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpectedRemediationDate"].ToString()).ToShortDateString() == "01/01/1900" ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpectedRemediationDate"].ToString()).ToShortDateString();
            txtvariation.Text = ds.Tables[0].Rows[0]["DeviationValue"].ToString();
            txtIndirectCost.Text = ds.Tables[0].Rows[0]["IndirectCost"].ToString();
            //txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"].ToString()).ToShortDateString();
            //txtenddate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"].ToString()).ToShortDateString();
            txtvariationFC.Text = ds.Tables[0].Rows[0]["VariationForcast"].ToString();
            txtApproveemail.Text = ds.Tables[0].Rows[0]["ApproversEmail"].ToString();
            txtApproversName.Text = ds.Tables[0].Rows[0]["Approversname"].ToString();
            txtDesctiption.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            txtForecastCost.Text = ds.Tables[0].Rows[0]["VariationCostForcast"].ToString();
            txtPercentageComplete.Text = ds.Tables[0].Rows[0]["PercentageComplete"].ToString();
           // txtAdditionalPMHours.Text = ChangeHours(ds.Tables[0].Rows[0]["AdditionalPMHours"].ToString());
            txtCustomerInstructionNumber.Text = ds.Tables[0].Rows[0]["CustomerInstructionNumber"].ToString();
            ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
            bool approved = Convert.ToBoolean(ds.Tables[0].Rows[0]["Approved"]);
            if (approved)
            {
                btnSubmitAndApprove.Visible = false;
                btnSubmitAndEmail.Visible = false;
                ImageButton1.Visible = false;
            }

            cmd.Dispose();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    public void newrpt(int ID)
    {
        try
        {
            ds.Clear();
            
            cmd = db.GetStoredProcCommand("DN_ProjectSelect");
            db.AddInParameter(cmd, "@ID", DbType.Int32, ID);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    txtProjectName.Text = dr["ProjectTitle"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    //get ac2pid
   
    private int getAc2pid()
    {
        int ac2pid = 0;
        if (Request.QueryString["AC2PID"] != null)
        {
            ac2pid = QueryStringValues.AC2PID; 
        }
        return ac2pid;
    }
    private int EmailIDCheck(string ApproverMailid, string UserMailID)
    {
       // string[] s={"yahoo","gmail","hotmail"};
        int retval = 0;
        if (ApproverMailid == UserMailID)
        {
            retval = 1;
        }
        //else
        //{
        //    for (int i = 0; i <= 2; i++)
        //    {
        //        if ((ApproverMailid.ToLower().Contains(s[i])) || (UserMailID.ToLower().Contains(s[i])))
        //        {
        //            retval = 2;
        //        }
        //    }
           
        //}
        return retval;         

    }

    private void VariationInsertUpdate(string type)
    {
        int pref = QueryStringValues.Project;
        int retval;
        try
        {
            //check mail id's            
            retval = EmailIDCheck(txtApproveemail.Text.Trim(), txtEmail.Text.Trim());
            if (retval == 0)
            {
                //double additionalPMHours = 0;
                //if (!string.IsNullOrEmpty(txtAdditionalPMHours.Text.Trim()))
                //{
                //    additionalPMHours = Convert.ToDouble(ChangeToDouble(txtAdditionalPMHours.Text.Trim()));
                //}
                if (Request.QueryString["ID"] == null)
                {
                   

                    cmd = db.GetStoredProcCommand("DN_Deviationreportadd");
                    db.AddInParameter(cmd, "@AC2PID", DbType.Int32, getAc2pid());
                    db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
                    db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
                    db.AddInParameter(cmd, "@RequesterName", DbType.String, txtRequesterName.Text.Trim());
                    db.AddInParameter(cmd, "@Telephone", DbType.String, txtTelephone.Text.Trim());
                    db.AddInParameter(cmd, "@MobileNumber", DbType.String, txtMobile.Text.Trim());
                    db.AddInParameter(cmd, "@Email", DbType.String, txtEmail.Text.Trim());
                    db.AddInParameter(cmd, "@StartDate", DbType.DateTime, DateTime.Now);// start date and end date not required
                    db.AddInParameter(cmd, "@EndDate", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@CRSProjectManager", DbType.String, txtCRSProjectManager.Text.Trim());
                    db.AddInParameter(cmd, "@BusinessHead", DbType.String, txtBusinessHead.Text.Trim());
                    db.AddInParameter(cmd, "@BusinessGroup", DbType.String, txtBusinessGroup.Text.Trim());
                    db.AddInParameter(cmd, "@ProjectName", DbType.String, txtProjectName.Text.Trim());
                    db.AddInParameter(cmd, "@ProjectLocation", DbType.String, txtProjectLocation.Text.Trim());
                    db.AddInParameter(cmd, "@ScopeofProject", DbType.String, txtScope.Text.Trim());
                    db.AddInParameter(cmd, "@DetailedExplanation", DbType.String, txtDetailedExplanation.Text.Trim());
                    db.AddInParameter(cmd, "@Justification", DbType.String, txtJustification.Text.Trim());
                    db.AddInParameter(cmd, "@ProposedCompensation", DbType.String, txtProposedCompensation.Text.Trim());
                    db.AddInParameter(cmd, "@ExpectedRemediationDate", DbType.String, txtRemediationDate.Text.Trim());
                    db.AddInParameter(cmd, "@DeviationValue", DbType.Double, getData.getDouble(txtvariation.Text.Trim()));
                    db.AddInParameter(cmd, "@IndirectCost", DbType.Double, getData.getDouble(txtIndirectCost.Text.Trim()));
                    db.AddInParameter(cmd, "@Approversname", DbType.String, txtApproversName.Text.Trim());
                    db.AddInParameter(cmd, "@ApproversEmail", DbType.String, txtApproveemail.Text.Trim());
                    db.AddInParameter(cmd, "@VariationForcast", DbType.Double, getData.getDouble(string.IsNullOrEmpty(txtvariationFC.Text.Trim()) ? txtvariation.Text.Trim() : txtvariationFC.Text.Trim()));
                    db.AddInParameter(cmd, "@VariationCostForcast", DbType.Double, getData.getDouble(string.IsNullOrEmpty(txtForecastCost.Text.Trim()) ? txtIndirectCost.Text.Trim() : txtForecastCost.Text.Trim())); ;
                    db.AddInParameter(cmd, "@Description", DbType.String, txtDesctiption.Text.Trim());
                    db.AddInParameter(cmd, "@AdditionalPMHours", DbType.Double, 0);
                    db.AddInParameter(cmd, "@CustomerInstructionNumber", DbType.String, txtCustomerInstructionNumber.Text.Trim());
                    db.AddInParameter(cmd, "@Status", DbType.String, ddlStatus.SelectedValue);
                    db.AddInParameter(cmd, "@PercentageComplete", DbType.Double, string.IsNullOrEmpty(txtPercentageComplete.Text.Trim()) ? "0" : txtPercentageComplete.Text.Trim());
                   
                    if (type == "b")
                        db.AddInParameter(cmd, "@CustomerID", DbType.Int32, int.Parse(ddlCustomer.SelectedValue));
                    else
                        db.AddInParameter(cmd, "@CustomerID", DbType.Int32, 0);
                    db.AddOutParameter(cmd, "@Outval", DbType.Int32, 4);

                    db.ExecuteNonQuery(cmd);
                    int getVariationID = (int)db.GetParameterValue(cmd, "Outval");
                    cmd.Dispose();
                    if (type == "a")
                    {
                        mail.sendMail(pref, getVariationID, 11);
                    }
                    else
                    {
                        mail.sendMail(pref, getVariationID, 3);
                    }

                    //send mail for approval                
                    //mail.sendMail(pref, getVariationID, 3);
                    //navigate after insert or update
                    lblerror1.ForeColor = System.Drawing.Color.Green;
                    lblerror1.Text = "Variation added successfully.";
                    Navigation();

                }
                else
                {

                    cmd = db.GetStoredProcCommand("DN_Deviationreportupdated");
                    db.AddInParameter(cmd, "@ID", DbType.Int32, int.Parse(Request.QueryString["ID"].ToString()));
                    db.AddInParameter(cmd, "@AC2PID", DbType.Int32, getAc2pid());
                    db.AddInParameter(cmd, "@ProjectReference", DbType.Int32, pref);
                    db.AddInParameter(cmd, "@ContractorID", DbType.Int32, sessionKeys.UID);
                    db.AddInParameter(cmd, "@RequesterName", DbType.String, txtRequesterName.Text.Trim());
                    db.AddInParameter(cmd, "@Telephone", DbType.String, txtTelephone.Text.Trim());
                    db.AddInParameter(cmd, "@MobileNumber", DbType.String, txtMobile.Text.Trim());
                    db.AddInParameter(cmd, "@Email", DbType.String, txtEmail.Text.Trim());
                    db.AddInParameter(cmd, "@StartDate", DbType.DateTime, DateTime.Now); // start date and end date not required
                    db.AddInParameter(cmd, "@EndDate", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@CRSProjectManager", DbType.String, txtCRSProjectManager.Text.Trim());
                    db.AddInParameter(cmd, "@BusinessHead", DbType.String, txtBusinessHead.Text.Trim());
                    db.AddInParameter(cmd, "@BusinessGroup", DbType.String, txtBusinessGroup.Text.Trim());
                    db.AddInParameter(cmd, "@ProjectName", DbType.String, txtProjectName.Text.Trim());
                    db.AddInParameter(cmd, "@ProjectLocation", DbType.String, txtProjectLocation.Text.Trim());
                    db.AddInParameter(cmd, "@ScopeofProject", DbType.String, txtScope.Text.Trim());
                    db.AddInParameter(cmd, "@DetailedExplanation", DbType.String, txtDetailedExplanation.Text.Trim());
                    db.AddInParameter(cmd, "@Justification", DbType.String, txtJustification.Text.Trim());
                    db.AddInParameter(cmd, "@ProposedCompensation", DbType.String, txtProposedCompensation.Text.Trim());
                    db.AddInParameter(cmd, "@ExpectedRemediationDate", DbType.String, txtRemediationDate.Text.Trim());
                    db.AddInParameter(cmd, "@DeviationValue", DbType.Double, getData.getDouble(txtvariation.Text.Trim()));
                    db.AddInParameter(cmd, "@IndirectCost", DbType.Double, getData.getDouble(txtIndirectCost.Text.Trim()));
                    db.AddInParameter(cmd, "@Approversname", DbType.String, txtApproversName.Text.Trim());
                    db.AddInParameter(cmd, "@ApproversEmail", DbType.String, txtApproveemail.Text.Trim());
                    db.AddInParameter(cmd, "@VariationForcast", DbType.Double, getData.getDouble(string.IsNullOrEmpty(txtvariationFC.Text.Trim()) ? txtvariation.Text.Trim() : txtvariationFC.Text.Trim()));
                    db.AddInParameter(cmd, "@VariationCostForcast", DbType.Double, getData.getDouble(string.IsNullOrEmpty(txtForecastCost.Text.Trim()) ? txtIndirectCost.Text.Trim() : txtForecastCost.Text.Trim()));
                    db.AddInParameter(cmd, "@Description", DbType.String, txtDesctiption.Text.Trim());
                    db.AddInParameter(cmd, "@AdditionalPMHours", DbType.Double, 0);
                    db.AddInParameter(cmd, "@CustomerInstructionNumber", DbType.String, txtCustomerInstructionNumber.Text.Trim());
                    db.AddInParameter(cmd, "@Status", DbType.String, ddlStatus.SelectedValue);
                    db.AddInParameter(cmd, "@PercentageComplete", DbType.Double, string.IsNullOrEmpty(txtPercentageComplete.Text.Trim()) ? "0" : txtPercentageComplete.Text.Trim());

                    db.ExecuteNonQuery(cmd);
                    cmd.Dispose();
                    lblerror1.ForeColor = System.Drawing.Color.Green;
                    lblerror1.Text = "Variation updated successfully.";
                    int variationId = Convert.ToInt32(Request.QueryString["ID"]);
                    if (type == "a")
                    {
                        mail.sendMail(pref, variationId, 11);
                    }
                    else
                    {
                        mail.sendMail(pref, variationId, 3);
                    }
                    Navigation();
                }

            }
            else
            {
                if (retval == 1)
                {
                    lblerror1.ForeColor = System.Drawing.Color.Red;
                    lblerror1.Text = "Both approvers email and requesters email should not be the same."; }
                else if (retval == 2)
                {
                    lblerror1.ForeColor = System.Drawing.Color.Red;
                    lblerror1.Text = "Personal email addresses are not allowed. Please use a corporate email address"; }

            }



        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    
    }

    public string ChangeHours(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { '.', ',' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + ":" + displayTime[1];
            return GetActivity;
        }
        else
        {
            return "0:00";
        }



    }
    public string ChangeToDouble(string GetHours)
    {
        string GetActivity = "";
        if (GetHours != "")
        {
            char[] comm1 = { ':' };
            string[] displayTime = GetHours.Split(comm1);
            GetActivity = displayTime[0] + "." + displayTime[1];
            return GetActivity;
        }
        else
        {
            return GetActivity;
        }
    }
    //protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    //{
    //   // VariationInsertUpdate("a");
       
    //}
    private void Navigation()
    {        
        if (Request.QueryString["Type"] != null)
        {
            //if (Request.QueryString["Type"] == "resource")
            //{
            //    Response.Redirect(string.Format("~/updateProjectVariations.aspx?Project={0}&AC2PID={1}&ContractorID={2}", QueryStringValues.Project, QueryStringValues.AC2PID,sessionKeys.UID));
            //    //Response.Redirect("~/updateProjectVariations.aspx?Project=" + QueryStringValues.Project.ToString()+"&AC2PID="+QueryStringValues.AC2PID.ToString());
            //}
            //else if(Request.QueryString["Type"] == "checkpoint")
            //{
            //    Response.Redirect(string.Format("~/Checkpoint_Financials.aspx?Project={0}", QueryStringValues.Project.ToString()));
            //}
        }
        else 
        {
            Response.Redirect("~/WF/Projects/ProjectTracker_Variations.aspx?Project=" + QueryStringValues.Project.ToString());
        }        
        
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        Navigation();        
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
        btnSubmitAndApprove.Enabled = false;
        btnSubmitAndEmail.Enabled = false;
       
       // btnApprove.Enabled = false;


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
    //protected void btnSubmitEmail_Click(object sender, ImageClickEventArgs e)
    //{
    //    VariationInsertUpdate("b");
    //}
   
    protected void btnSubmitAndApprove_Click(object sender, EventArgs e)
    {
        VariationInsertUpdate("a"); // send mail to manager's list
    }
    protected void btnSubmitAndEmail_Click(object sender, EventArgs e)
    {
        VariationInsertUpdate("b"); //send mail to customer
    }
}
