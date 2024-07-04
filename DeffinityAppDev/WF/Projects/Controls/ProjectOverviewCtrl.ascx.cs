using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using Deffinity.ProgrammeManagers;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using Deffinity.RagSectionProjectManager;
using Deffinity.RagSectionProjectEntity;
using ProjectMgt.BAL;
using System.Text;
using System.IO;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Deffinity.ProjectMangers;
using System.Configuration;
//using DeffinityManager.Marketplace.BLL;

public partial class controls_ProjectOverviewCtrl : System.Web.UI.UserControl

{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindData();
            if (QueryStringValues.Project > 0)
            {
                GetProjectDetails();
                CheckProjectClass();
            }
            if (sessionKeys.Project != 0)
            {
                CheckUserRole();
            }

            ProjectClass_Visibility();
            //VisibilityChecking();
        }
        
    }
    //public void VisibilityChecking()
    //{
    //    try
    //    {

    //        Market_BLL M_BLL = new Market_BLL();
    //        DivProgrammeSubProgramme.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.ProgrammeManagement);

    //    }
    //    catch (Exception ex)
    //    {
    //        LogExceptions.WriteExceptionLog(ex);
    //    }
    //}
    private void ProjectClass_Visibility()
    {
        try
        {
            //Check Project class feature is enabled
            string[] str = PermissionManager.GetFeatures();
            if (!Page.IsPostBack)
            {
                btnClassApply.Visible = Convert.ToBoolean(str[94]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }


    #region Bind dropdowns
    private void BindData()
    {
        //Bind project staus
        projectTaskDataContext db = new projectTaskDataContext();
        if(sessionKeys.SID==2 || sessionKeys.SID==3)
        {
            int[] ids={4,5};
            var projects = (from r in db.ProjectStatus
                            where !ids.Contains(r.ID)
                            select r);
            if (projects != null)
            {
                ddlStatus.DataSource = projects;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "ID";
                ddlStatus.DataBind();
            }

        }
        else
        {

        ddlStatus.DataSource = Deffinity.ProjectMangers.ProjectManager.ProjectStatus();
        ddlStatus.DataTextField = "Status";
        ddlStatus.DataValueField = "ID";
        ddlStatus.DataBind();
        }
        if(QueryStringValues.Project ==0 )
        {
            if(ddlStatus.Items.Count >0)
                //1 is pending
            ddlStatus.SelectedValue = "1";
        }
        //dbind.DdlBindSelect(ddlCustomer, "SELECT ID,PortFolio FROM ProjectPortfolio where visible=1 order by PortFolio", "ID", "PortFolio",false,true,false);
        //ddlCustomer.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display(1);
        //ddlCustomer.DataTextField = "PortFolio";
        //ddlCustomer.DataValueField = "ID";
        ddlCustomer.DataBind();

        if (ddlCustomer.Items.Count > 0)
        {
            if (sessionKeys.PortfolioID != 0)
            {
                ddlCustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }
            else
            {
                sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomer.SelectedValue);
                sessionKeys.PortfolioName = ddlCustomer.SelectedItem.Text;
            }
        }
        ddlProgramme.DataBind();
        BindSubProgramme();
        //ddlSubprogramme.DataBind();
    }
    private void BindSubProgramme()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure,
            "Project_AssignedSubProgramme", new SqlParameter("@UserID", sessionKeys.UID), new SqlParameter("@PROGRAMMEID",ddlProgramme.SelectedValue)).Tables[0];
        ddlSubprogramme.DataSource = dt;
        ddlSubprogramme.DataTextField = "OPERATIONSOWNERS";
        ddlSubprogramme.DataValueField = "ID";
        ddlSubprogramme.DataBind();
    }


    #endregion

    private void GetProjectDetails()
    {

        try
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(Constants.DBString, CommandType.Text, string.Format("Select isnull(ProjectTitle,'') as ProjectTitle, isnull(Portfolio,0) as Portfolio,isnull(ProjectStatusID,1) as ProjectStatusID,StartDate,ProjectEndDate, isnull(OwnerGroupID,0)OwnerGroupID,isnull(SubProgramme,0)SubProgramme  from Projects where ProjectReference ={0}", QueryStringValues.Project));
            while (dr.Read())
            {
                txtProjectTitle.Text = dr["ProjectTitle"].ToString();
                ddlCustomer.SelectedValue = dr["Portfolio"].ToString();
                ddlStatus.SelectedValue = dr["ProjectStatusID"].ToString();
                ddlProgramme.SelectedValue=dr["OwnerGroupID"].ToString();
                BindSubProgramme();
                ddlSubprogramme.SelectedValue=dr["SubProgramme"].ToString();
                //Sdate = string.IsNullOrEmpty(dr["StartDate"].ToString()) ? "" : Convert.ToDateTime(dr["StartDate"]).ToShortDateString();
                //Edate = string.IsNullOrEmpty(dr["ProjectEndDate"].ToString()) ? "" : Convert.ToDateTime(dr["ProjectEndDate"]).ToShortDateString();
                //txt_ins_Startdate.Text = Sdate;
                //txt_ins_enddate.Text = Edate;
                ViewState["status"] = dr["ProjectStatusID"].ToString(); 
            }
            dr.Dispose();
            dr.Close();

        }
        catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }

    }
    //check the both status
    private bool ProjectClassChecking(int projectreference,int currentStatus)
    {
        bool retval =  true;
        int Projectstatus =0;
        projectTaskDataContext ptd = new projectTaskDataContext();
        if (projectreference == 0)
            retval = true;
            //if Project class is not show in check that condition
        else if (!btnClassApply.Visible)
        { retval = true; }
        else
        {

            ProjectMgt.Entity.ProjectDetails pd = (from p in ptd.ProjectDetails
                                                   where (p.ProjectReference == projectreference)
                                                   select p).FirstOrDefault();
            Projectstatus = pd.ProjectStatusID.Value;


            if (Projectstatus != currentStatus)
            {
                var pcp = (from pc in ptd.ProjectClassByProjectReferences
                          where (pc.ProjectReference == projectreference && pc.DateAudited == null)
                               select pc).Count();
                if (int.Parse(pcp.ToString()) > 0)
                {
                    retval = false;
                }
            }
        }

       return retval;
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            

            int pref = 0;
            if (QueryStringValues.Project > 0)
            {
                pref = QueryStringValues.Project;
            }
            if (ProjectClassChecking(pref, int.Parse(string.IsNullOrEmpty(ddlStatus.SelectedValue) ? "0" : ddlStatus.SelectedValue)))
            {
                bool detailsChanged = false;
                string projectTitle = txtProjectTitle.Text.Trim();
                int portfolio = int.Parse(string.IsNullOrEmpty(ddlCustomer.SelectedValue) ? "0" : ddlCustomer.SelectedValue);
                int status = int.Parse(string.IsNullOrEmpty(ddlStatus.SelectedValue) ? "0" : ddlStatus.SelectedValue);
                int programmeID = int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue);
                int subProgrammeID = int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue);

                //Insert Project journal
               
                SqlParameter outRef = new SqlParameter("@outRef", SqlDbType.Int, 4);
                outRef.Direction = ParameterDirection.Output;
                SqlParameter outMsg = new SqlParameter("@outMsg", SqlDbType.Int, 4);
                outMsg.Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "Deffinity_ProjectTitle_insert"
                    , new SqlParameter("@ProjectReference", pref), new SqlParameter("@ProjectTitle", projectTitle)
                    , new SqlParameter("@Portfolio", portfolio)
                    , new SqlParameter("@status", status)
                    , new SqlParameter("@OwnerID", sessionKeys.UID)
                    , new SqlParameter("@programmeID", programmeID)
                    , new SqlParameter("@SubProgrammeID", subProgrammeID)
                    , outRef, outMsg);
                //insert default worksheet
                DefaultWorkSheet(int.Parse(outRef.Value.ToString()));

                //if (int.Parse(outMsg.Value.ToString()) == 0)
                //{
                //    if (int.Parse(outRef.Value.ToString()) > 0)
                //    {
                //        Response.Redirect(string.Format("ProjectOverviewV2.aspx?project={0}", outRef.Value.ToString()), false);
                //    }
                //    else
                //    {
                //        Response.Redirect(string.Format("ProjectOverviewV2.aspx?project={0}", QueryStringValues.Project), false);
                //    }

                //}
                if (ddlStatus.SelectedItem.Text.ToLower() == "live")
                {
                    SendTaskAlertMail();
                }
                if (ViewState["status"] != null)
                {
                    string cval = ViewState["status"].ToString();
                    if (cval == "2" && ddlStatus.SelectedValue == "1")
                    {
                        SendTaskAlertMail_LivetoPending();
                    }
                }
                if (int.Parse(outMsg.Value.ToString()) == 0)
                {
                    //first time project 
                    if (QueryStringValues.Project == 0)
                        pref = int.Parse(outRef.Value.ToString());

                    //project  journal insert
                    if (pref > 0)
                    {
                        Project project = ProjectJournalBAL.GetProjectsByReference(pref);
                        ProjectJournalBAL.InsertProjectJournal(project);
                       // ProjectManager.ProjectJournalInsert(pref, sessionKeys.UID);

                    }

                    //insert workstream
                    InsertProjectRags();
                    if (int.Parse(outRef.Value.ToString()) > 0)
                    {
                        Response.Redirect(string.Format("ProjectOverviewV4.aspx?project={0}", outRef.Value.ToString()), false);
                    }
                    else
                    {
                        Response.Redirect(string.Format("ProjectOverviewV4.aspx?project={0}", QueryStringValues.Project), false);
                    }

                }
                else
                {
                    lblMsg.Text = "Project title already exists.";
                }
            }
            else
            {
                lblMsg.Text = "Please complete the audit item in checklist.";
            }
        

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        //lblMsg.Text = "";
    }

    private static void DefaultWorkSheet(int outRef)
    {
        try
        {
            if (outRef > 0)
            {
                int returnID = Deffinity.Worksheet.Worksheet_InsertUpdate(0, outRef, "Worksheet1");
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    private void SendTaskAlertMail()
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                int projectReference = Convert.ToInt32(Request.QueryString["project"]);
                var contractorList = db.ProjectItems.Where(p => p.ProjectReference == projectReference).GroupBy(p => p.ContractorID).ToList();
                foreach (var item in contractorList)
                {
                    var assignedTaskList = (from p in db.ProjectItems
                                            join t in db.ProjectTaskItems on p.ItemReference equals t.ID
                                            where p.ProjectReference == projectReference && p.ContractorID == item.Key
                                            select new { p, t }).ToList();

                    StringBuilder sb = new StringBuilder();

                    foreach (var a in assignedTaskList)
                    {
                        sb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", a.t.ItemDescription,
                            string.Format(Deffinity.systemdefaults.GetStringDateformat(), a.t.StartDate), string.Format(Deffinity.systemdefaults.GetStringDateformat(), a.t.CompletionDate), a.t.Notes));
                    }

                    string fromemailId = Deffinity.systemdefaults.GetFromEmail();

                    string projectNumber = sessionKeys.Prefix + projectReference;
                    string subject = "You have been assigned to Project: " + projectNumber;
                    DC.SRV.WebService ws = new DC.SRV.WebService();

                    UserMgt.Entity.Contractor contractor = ws.GetContractorDetails(Convert.ToInt32(item.Key));
                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/EmailTemplates/TaskAlert.htm");

                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + ConfigurationManager.AppSettings["maillogo"]);
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + ConfigurationManager.AppSettings["mailboarder"]);

                    body = body.Replace("[user]", contractor.ContractorName);
                    body = body.Replace("[ProjectRef]", projectNumber);
                    body = body.Replace("[datarow]", sb.ToString());

                    body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());
                    
                    em.SendingMail(fromemailId, subject, body, contractor.EmailAddress);


                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void SendTaskAlertMail_LivetoPending()
    {
        try
        {
            // project Group members
            ProjectAdminGroupBAL pb = new ProjectAdminGroupBAL();
            UserMgt.BAL.ContractorsBAL clist = new UserMgt.BAL.ContractorsBAL();
            var plist = pb.ProjectAdminGroup_SelectAll();
            var ulist = clist.Contractor_Select_Admins();
            var pclist = (from p in plist
                          join c in ulist
                          on p.UserID equals c.ID
                          select new { p.ID, p.UserID, c.ContractorName,c.EmailAddress }).ToList();
            Emailer em = null;
            Email email = null;
            string body = null;
            string fromemailId = Deffinity.systemdefaults.GetFromEmail();
            string subject = "Project: " + sessionKeys.Prefix + QueryStringValues.Project.ToString() + " alert Live to Pending.";
            //bind a report
            BindReport();
            var at = new Attachment(Server.MapPath("~/WF/UploadData/projects/" + sessionKeys.Prefix + QueryStringValues.Project.ToString() + ".pdf"));
            foreach (var s in pclist)
            {
                em = new Emailer();
                email = new Email();
                body = em.ReadFile("~/WF/EmailTemplates/LivetoPendingAlert.htm");
                body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);

                body = body.Replace("[user]", s.ContractorName);
                body = body.Replace("[ProjectRef]", sessionKeys.Prefix + QueryStringValues.Project.ToString());
                body = body.Replace("[ref]", Deffinity.systemdefaults.GetWebUrl());


                try
                {
                    email.SendingMail(s.EmailAddress, subject, body, fromemailId, at);
                }
                catch (Exception ex)
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
    private bool CheckProjectDetailsChanged(dynamic project, string projectTitle, int portfolio, int status, int programmeID, int subProgrammeID)
    {
        bool changed = true;
        if (projectTitle != project.ProjectTitle)
            changed = true;
        else if (portfolio != project.Portfolio)
            changed = true;
        else if (status != project.ProjectStatusID)
            changed = true;
        else if (programmeID != project.OwnerGroupID)
            changed = true;
        else if (subProgrammeID != project.SubProgramme)
            changed = true;
        return changed;
    }

    private void InsertProjectRags()
    {
        try
        {
            if (int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue) > 0)
            {
                RagSectiontoProjectEntity rsp = new RagSectiontoProjectEntity();
                rsp.ProjectReference = QueryStringValues.Project;
                rsp.PortfolioID = int.Parse(string.IsNullOrEmpty(ddlProgramme.SelectedValue) ? "0" : ddlProgramme.SelectedValue);
                rsp.ProgrammeID = int.Parse(string.IsNullOrEmpty(ddlSubprogramme.SelectedValue) ? "0" : ddlSubprogramme.SelectedValue);
                RagSectiontoProjectManager.RagSectionProjectBulkInsert(rsp);
            }
            
        }

        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
    #region AssignItem
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (QueryStringValues.Project > 0)
        {
            if (ddlMasterTemplate.SelectedValue != "0")
            {
                AssignItems();
                //bind the the position
                //Bind_TaskPosition();
            }
        }
        else
        {
            lblMsg.Text = "Please create a project";
        }
    }
    private void AssignItems()
    {
        try
        {
            int mid = Convert.ToInt32(ddlMasterTemplate.SelectedValue);
            string mtext = ddlMasterTemplate.SelectedItem.Text;
            int Pref = QueryStringValues.Project;
            try
            {
                //first check for items are exist or not
                if (checkItems(mid) > 0)
                {
                    InsertTaskList(mid, mtext, Pref);
                }
                else
                {
                    lblMsg.Text = "No items exist in the master template.";
                }
            }
            catch (Exception ex1)
            {
                LogExceptions.LogException(ex1.Message);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    private int checkItems(int MasterID)
    {
        string stemp;
        stemp = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select count(*) from MasterTemplateItems where MasterTemplateID={0}", MasterID.ToString())).ToString();
        return Convert.ToInt32(stemp);
    }
    private int CheckMasterTemplate(int pref, int mid)
    {
        string stemp1;
        stemp1 = SqlHelper.ExecuteScalar(Constants.DBString, CommandType.Text, string.Format("select count(*) from ProjectTaskItems Where MasterID ={0}AND ProjectReference={1}", mid, pref)).ToString();
        return Convert.ToInt32(stemp1);
    }
    private void InsertTaskList(int val, string valText, int Pref)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "DN_TaskItemBulkInsert", new SqlParameter("@Pref", Pref), new SqlParameter("@MasterID", val));

        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Insert from assign");
        }
    }

    #endregion
    #region "Permission Code Here"
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
                    role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                    if (role == 3)
                    {

                        vis = false;
                        //  Disable();

                    }
                    role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
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
    private void CheckUserRole()
    {
        if ((Request.QueryString["Project"] != null))
        {
            if (sessionKeys.SID != 1)
            {
                int role = 0;
                role = Deffinity.ProgrammeManagers.Admin.CheckLoginUserPermission(sessionKeys.UID);
                if (role == 3)
                {

                    Disable();

                }
                role = Deffinity.ProgrammeManagers.Admin.GetTeamID(sessionKeys.UID);
                if (role == 3)
                {
                    //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";
                    Disable();

                }

            }
        }
    }
    private void Disable()
    {
        btnAssign.Enabled = false;
        btnCreate.Enabled = false;
        

        //Master.ErrorMsg = "Sorry but you do not have sufficient rights to modify this project.";

    }
    #endregion 
    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlProgramme.SelectedIndex = 0;
        //SqlDataSourcesubprogram.SelectParameters[0].DefaultValue = ddlProgramme.SelectedValue;
        BindSubProgramme();
    }
    #region Project Class
    private void BindProjectClassGrid()
    {
        projectTaskDataContext pdt = new projectTaskDataContext();
        
        //var User_list = (from p in ud.Contractors select p).ToList();
        var pcp = (from p in pdt.ProjectClassByProjectReferences select p).ToList();
        var mt = (from p in pdt.MasterTemplateItems select p).ToList();
        var r = (from p in pcp
                 join cp in mt on p.ClassItemID equals cp.ID
                 where p.ProjectReference == QueryStringValues.Project
                 select new { ID = p.ID, ItemDesc = cp.ItemDescription, dateAudited = p.DateAudited, Auditedby = ReturnName(p.AuditedBy) , Notes = p.notes });
        //gridProjectClass.DataSource = r;
        //gridProjectClass.DataBind();

    }

    private object ReturnName(int? userid)
    {
        object retVal = string.Empty;

        using (UserDataContext ud = new UserDataContext())
        {
            if(userid.HasValue)
            retVal = (from p in ud.Contractors where p.ID == userid.Value select p).FirstOrDefault().ContractorName.ToString();
        }

        return retVal;
    }
    private void CheckProjectClass()
    { 
         projectTaskDataContext dt = new projectTaskDataContext();
        var pct = (from p1 in dt.ProjectDefaults
                  select p1).FirstOrDefault();
        //if (pct != null)
        //{
        if (!pct.ProjectClass.Value || pct.ProjectClass.Value==null)
            btnClassApply.Visible = pct.ProjectClass.Value;
    }
    #endregion
    protected void gridProjectClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //projectTaskDataContext pdt = new projectTaskDataContext();
        //ProjectClassByProjectReference pcentity=null;
        //if (e.CommandName == "Audited")
        //{ 
             
        //     pcentity = pdt.ProjectClassByProjectReferences.Where(p => p.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();
        //     pcentity.DateAudited = DateTime.Now;
        //     pcentity.AuditedBy = sessionKeys.UID;
        //     pdt.SubmitChanges();
        //     BindProjectClassGrid();
        //}
        //else if (e.CommandName == "Update")
        //{
        //    int ID = Convert.ToInt32(e.CommandArgument.ToString());
        //    int i = gridProjectClass.EditIndex;
        //    GridViewRow Row = gridProjectClass.Rows[i];
        //    string notes = ((TextBox)Row.FindControl("txtNotes")).Text;
        //    pcentity = pdt.ProjectClassByProjectReferences.Where(p => p.ID == int.Parse(e.CommandArgument.ToString())).FirstOrDefault();
        //    pcentity.notes = notes;
        //    pdt.SubmitChanges();
        //    //gridProjectClass.EditIndex = -1;
        //    BindProjectClassGrid(); 
        
        //}
        
    }
    protected void btnClassApply_Click(object sender, EventArgs e)
    {
        BindProjectClassGrid();
        //modalClassPnl.Show();
    }
    protected void gridProjectClass_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gridProjectClass.EditIndex = e.NewEditIndex;
        //BindProjectClassGrid();
    }
    protected void gridProjectClass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //gridProjectClass.EditIndex = -1;
        //BindProjectClassGrid();
    }
    protected void gridProjectClass_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gridProjectClass_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            // Can perform custom error handling here, set ExceptionHandled = true when done
            e.ExceptionHandled = true;
        }
    }

    #region report
    ReportDocument rpt;
    string ProjectReference = string.Empty;
    private void BindReport()
    {
        rpt = new ReportDocument();
        
        //Load the selected report file.
        int ProjectReference = QueryStringValues.Project;
        string str = "~/WF/Reports/ProjectStatusReport.rpt";
        rpt.Load(Server.MapPath(str));

        //Set the Database Login Information
        string strUser = System.Configuration.ConfigurationManager.AppSettings["user"];
        string strPassword = System.Configuration.ConfigurationManager.AppSettings["password"];
        string strServer = System.Configuration.ConfigurationManager.AppSettings["server"];
        string strDatabase = System.Configuration.ConfigurationManager.AppSettings["database"];



        #region For Main Report

        DataTable dt12 = new DataTable();
        string strConn = System.Configuration.ConfigurationManager.AppSettings["DBstring"];
        SqlConnection MyCon = new SqlConnection(strConn);
        SqlCommand myCommand = new SqlCommand("DEFFINITY_ProjetReport_GETDeliverables", MyCon);
        myCommand.CommandType = CommandType.StoredProcedure;

        myCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
        myAdapter.Fill(dt12);

        //Set the Crytal Report Viewer control's source to the report document.
        rpt.SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.SetDataSource(dt12);


        #endregion
        #region For Deliverables

        DataTable dt = new DataTable();
        SqlCommand myCommanddel = new SqlCommand("DEFFINITY_ProjetReport_GETDeliverables", MyCon);
        myCommanddel.CommandType = CommandType.StoredProcedure;

        myCommanddel.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        SqlDataAdapter myAdapterdel = new SqlDataAdapter(myCommanddel);
        myAdapterdel.Fill(dt);

        ////Set the Crytal Report Viewer control's source to the report document.
        rpt.Subreports["KeyDeliverablesRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["KeyDeliverablesRpt"].SetDataSource(dt);


        #endregion

        #region For RAGSummary Sub Report

        DataTable dt1 = new DataTable();
        SqlDataAdapter adp_sub = new SqlDataAdapter("DEFFINITY_ProjetReport_GETRAGDetails", MyCon);
        adp_sub.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub.Fill(dt1);
        rpt.Subreports["ProjectRAGStatusRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectRAGStatusRpt"].SetDataSource(dt1);

        #endregion

        #region For KeyTasks.rpt

        DataTable dt3 = new DataTable();
        SqlDataAdapter adp_sub2 = new SqlDataAdapter("DEFFINITY_ProjetReport_AchivementsLastweek", MyCon);
        adp_sub2.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub2.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub2.Fill(dt3);
        rpt.Subreports["AchivementsLastWeekRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["AchivementsLastWeekRpt"].SetDataSource(dt3);

        #endregion

        #region For Achievements.rpt

        DataTable dt4 = new DataTable();
        SqlDataAdapter adp_sub3 = new SqlDataAdapter("DEFFINITY_ProjetReport_AchivementsNextweek", MyCon);
        adp_sub3.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub3.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub3.Fill(dt4);
        rpt.Subreports["AchivementsNextWeekRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["AchivementsNextWeekRpt"].SetDataSource(dt4);

        #endregion

        #region For KEY Risks.rpt

        DataTable dt10 = new DataTable();
        SqlDataAdapter adp_sub10 = new SqlDataAdapter("DN_RPT_KeyRisks", MyCon);
        adp_sub10.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub10.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub10.Fill(dt10);
        //rpt.Subreports["KeyRisksRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["KeyRisksRpt"].SetDataSource(dt10);

        #endregion


        #region ProjectProposals.rpt

        //DataTable dt6 = new DataTable();
        //SqlDataAdapter adp_sub5 = new SqlDataAdapter("DN_RPT_ProjectProposals", MyCon);
        //adp_sub5.SelectCommand.CommandType = CommandType.StoredProcedure;
        //adp_sub5.SelectCommand.Parameters.Add("@ProgrammeID", SqlDbType.Int).Value = sessionKeys.ProgrammeID;
        //adp_sub5.Fill(dt6);
        //rpt.Subreports["PojectProposalsRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        //rpt.Subreports["PojectProposalsRpt"].SetDataSource(dt6);

        #endregion

        #region CostAndSummary.rpt

        DataTable dt7 = new DataTable();
        SqlDataAdapter adp_sub6 = new SqlDataAdapter("DN_RPT_CostAndResourceSummary", MyCon);
        adp_sub6.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_sub6.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp_sub6.Fill(dt7);
        rpt.Subreports["CostingAndResourcesRpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["CostingAndResourcesRpt"].SetDataSource(dt7);

        #endregion
        #region IssueComments.rpt
        DataTable dtIssueComments = new DataTable();
        SqlDataAdapter adp_subIssueComments = new SqlDataAdapter("Project_IssueCommentsRpt", MyCon);
        adp_subIssueComments.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp_subIssueComments.SelectCommand.Parameters.Add("@ProjectRef", SqlDbType.VarChar).Value = ProjectReference;
        adp_subIssueComments.Fill(dtIssueComments);
        rpt.Subreports["IssueComments.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["IssueComments.rpt"].SetDataSource(dtIssueComments);

        #endregion

        #region newlyAddedCode

        //DataTable dtnew = new DataTable();
        int programmeid = 0;
        int countryid = 0;
        int subprogrammeid = 0;
        DataTable dt2 = new DataTable();
        SqlDataAdapter adp1 = new SqlDataAdapter("Programme_rptRisk_new", MyCon);
        adp1.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp1.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp1.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp1.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        //@subprogramme
        adp1.Fill(dt2);
        rpt.Subreports["ProgrammeRisk.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeRisk.rpt"].SetDataSource(dt2);

        DataTable dt5 = new DataTable();
        SqlDataAdapter adp2 = new SqlDataAdapter("Programme_rptMilestone_new", MyCon);
        adp2.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp2.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp2.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp2.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp2.Fill(dt5);
        rpt.Subreports["ProgrammeMilestones.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeMilestones.rpt"].SetDataSource(dt5);

        DataTable dt8 = new DataTable();
        SqlDataAdapter adp3 = new SqlDataAdapter("Programme_rptBenefit_new", MyCon);
        adp3.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp3.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp3.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp3.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp3.Fill(dt8);
        rpt.Subreports["ProjectBenefit.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectBenefit.rpt"].SetDataSource(dt8);

        DataTable dt9 = new DataTable();
        SqlDataAdapter adp4 = new SqlDataAdapter("Programme_rptIssues_new", MyCon);
        adp4.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp4.SelectCommand.Parameters.Add("@programmeid", SqlDbType.Int).Value = programmeid;
        adp4.SelectCommand.Parameters.Add("@countryid", SqlDbType.Int).Value = countryid;
        adp4.SelectCommand.Parameters.Add("@subprogramme", SqlDbType.Int).Value = subprogrammeid;
        adp4.Fill(dt9);
        rpt.Subreports["ProgrammeIssues.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProgrammeIssues.rpt"].SetDataSource(dt9);


        DataTable dt11 = new DataTable();
        SqlDataAdapter adp10 = new SqlDataAdapter("DEFFINITY_ProjetReport_Tasklist", MyCon);
        adp10.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp10.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp10.Fill(dt11);
        rpt.Subreports["ProjectTask.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectTask.rpt"].SetDataSource(dt11);


        DataTable dt14 = new DataTable();
        SqlDataAdapter adp12 = new SqlDataAdapter("deffinity_projectmeetingselect", MyCon);
        adp12.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp12.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp12.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = null;
        adp12.Fill(dt14);
        rpt.Subreports["ProjectUpdates.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectUpdates.rpt"].SetDataSource(dt14);

        DataTable dt16 = new DataTable();
        SqlDataAdapter adp16 = new SqlDataAdapter("Project_TrackerRpt", MyCon);
        adp16.SelectCommand.CommandType = CommandType.StoredProcedure;
        adp16.SelectCommand.Parameters.Add("@ProjectReference", SqlDbType.VarChar).Value = ProjectReference;
        adp16.Fill(dt16);
        rpt.Subreports["ProjectTracker.rpt"].SetDatabaseLogon(strUser, strPassword, strServer, strDatabase);
        rpt.Subreports["ProjectTracker.rpt"].SetDataSource(dt16);
        #endregion

        string filename = string.Format(sessionKeys.Prefix + QueryStringValues.Project.ToString() + ".pdf");
        string path = Server.MapPath("~/WF/UploadData/projects/" + filename);
        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
        rpt.Close();
        rpt = null;

        MyCon.Close();
        MyCon.Dispose();
    }

#endregion
}