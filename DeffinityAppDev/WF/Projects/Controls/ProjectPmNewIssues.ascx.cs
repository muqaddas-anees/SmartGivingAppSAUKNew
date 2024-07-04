using System;
using System.Collections;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Deffinity.ProgrammeManagers;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using UserMgt.DAL;
using UserMgt.Entity;
using Deffinity.ServiceCatalogManager;
using System.Text;
using ProjectMgt.Entity;
using ProjectMgt.DAL;
using Deffinity.LessonsLearntEntitys;
using Deffinity.LessonsLearntManagers;
using DeffinityManager.DAL;


namespace Deffinity.GlobalIssues
{
    public partial class controls_ProjectPmNewIssues : System.Web.UI.UserControl
    {


        #region properties

        string _IssueSection = string.Empty;
        public string IssueSection
        {
            get { return _IssueSection; }
            set { _IssueSection = value; }
        }
        string _healthcheckname = string.Empty;
        public string Healthcheckname
        {
            get { return _healthcheckname; }
            set { _healthcheckname = value; }
        }
        #endregion
        Project objGlobal = new Project();
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["FristTime"] = "0";
            try
            {


                if ((Request.QueryString["Project"] != null) || (Request.QueryString["HealthCheckId"] != null))
                {

                    pnlAssociated.Visible = false;
                }
                //IssueSection = GetSection();
                //int GetIDSection = GetIDbySection();

                //loginBoxContent.Visible = false;
                lblError.Text = "";
                if (!IsPostBack)
                {
                    lblErrorMsg.Visible = false;
                    Session["FristTime"] = "1";
                    bindAssignedResource();
                    ddlStatus.DataBind();
                    ddlStatus.Items.RemoveAt(0);
                    hidIssueID.Value = "-99";
                    //ddlIssueraisedBy.DataBind();
                    BindResources(ddlIssueraisedBy);
                    BindResources(ddlcheckedby);
                    BindResources(ddlAssign);
                    BindResources(ddlAssigntoInNewIssue);
                    if (hidIssueID.Value != "-99")
                    {

                        btnAddnew.CommandName = "Update";
                        //btnAddnew.SkinID = "ImgUpdate";

                        //Project or Healthcheck dropdown and lable
                        lblAssociatedTo.Visible = true;
                        txtAssociatedTo.Visible = true;
                        ddlAssociatedTo.Visible = false;
                        ddlIssueraisedBy.Visible = true;
                    }
                    else
                    {

                        //ddlIssueraisedBy.DataBind();
                        //ddlIssueraisedBy.Items.Insert(0, new ListItem("Please select...", "0"));
                        //ddlAssign.DataBind();
                        //ddlAssign.Items.Insert(0, new ListItem("Please select...", "0"));
                        //ddlcheckedby.DataBind();
                        //ddlcheckedby.Items.Insert(0, new ListItem("Please select...", "0"));
                        btnAddnew.CommandName = "Insert";
                        //btnAddnew.SkinID = "ImgSubmit";
                        ddlIssueraisedBy.SelectedIndex = ddlIssueraisedBy.Items.IndexOf(ddlIssueraisedBy.Items.FindByValue(sessionKeys.UID.ToString()));
                        //Project or Healthcheck dropdown and lable
                        lblAssociatedTo.Visible = false;
                        txtAssociatedTo.Visible = false;
                        ddlAssociatedTo.Visible = true;

                        if (pnlAssociated.Visible == true)
                        {
                            if (ddlAssociatedTo.SelectedItem.Text.Trim().ToLower() == "project")
                            {
                                ddlProjectList.Visible = true;
                                ddlHealthCheck.Visible = false;
                            }
                            else
                            {
                                ddlProjectList.Visible = true;
                                ddlHealthCheck.Visible = false;
                            }
                        }
                        SetdefaultsValues();
                    }

                    CheckUserRole();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }

        }

        private void bindAssignedResource()
        {
            DataTable dt = new DataTable();
            if (Request.QueryString["project"] != null)
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo",
                    new SqlParameter("@ProjectRefrence", int.Parse(Request.QueryString["project"].ToString())),
                    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
                DropDownListAssignedTo.DataSource = dt;
                DropDownListAssignedTo.DataTextField = "ContractorName";
                DropDownListAssignedTo.DataValueField = "ID";
                DropDownListAssignedTo.DataBind();
                DropDownListAssignedTo.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedResource",
                    new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
                DropDownListAssignedTo.DataSource = dt;
                DropDownListAssignedTo.DataTextField = "ContractorName";
                DropDownListAssignedTo.DataValueField = "ID";
                DropDownListAssignedTo.DataBind();
                DropDownListAssignedTo.Items.Insert(0, new ListItem("All", "0"));
            }


        }

        private void BindResources(DropDownList ddlResources)
        {
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.StoredProcedure, "Project_AssignedTo",
                new SqlParameter("@ProjectRefrence", 0),
                   new SqlParameter("@UserID", sessionKeys.UID)).Tables[0];
            ddlResources.DataSource = dt;
            ddlResources.DataTextField = "ContractorName";
            ddlResources.DataValueField = "ID";
            ddlResources.DataBind();
            ddlResources.Items.Insert(0, new ListItem("Please select...", "0"));
            //int.Parse(string.IsNullOrEmpty(Request.QueryString["project"].ToString()) ? "0" : Request.QueryString["project"].ToString())
        }

        protected void ddlAssociatedTo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlAssociatedTo.SelectedValue == "1")
            {
                ddlProjectList.Visible = true;
                ddlHealthCheck.Visible = false;
                chklesson.Visible = true;
            }
            else if (ddlAssociatedTo.SelectedValue == "2")
            {
                ddlProjectList.Visible = false;
                ddlHealthCheck.Visible = true;
                chklesson.Visible = false;
            }
            else
            {
                ddlProjectList.Visible = false;
                ddlHealthCheck.Visible = false;
                chklesson.Visible = false;
            }
            popIssues.Show();
        }
        private void SetdefaultsValues()
        {
            ddlProjectList.DataBind();
            //ddlAssign.DataBind();
            txtDate.Text = DateTime.Now.ToShortDateString();
            //bind site to add project 
            BindSite();
            //set logged in user
            //ddlIssueraisedBy.DataBind();
            //ddlIssueraisedBy.SelectedIndex = ddlIssueraisedBy.Items.IndexOf(ddlIssueraisedBy.Items.FindByValue(sessionKeys.UID.ToString()));
            //Health check related changes
            if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {
                ddtype.DataBind();
                ddtype.SelectedIndex = ddtype.Items.IndexOf(ddtype.Items.FindByText(Deffinity.GlobalIssues.Project.IssueTypeDefault));
                ddlQAtype.DataBind();
                ddlQAtype.SelectedIndex = ddlQAtype.Items.IndexOf(ddlQAtype.Items.FindByText(Deffinity.GlobalIssues.Project.IssueTypeDefault));
                if (ddlQAtype.SelectedIndex > 0)
                    ddlQAtype.Enabled = false;
                //Bind grid view
                gvBindIssues.DataBind();
                //Set the issue text
                txtIssue.Text = Healthcheckname.ToString();
                //Hide the issue type if it is health check section
                //pnlIssuetype.Visible = false;
                //ddlCategory.Visible = false;
                //Category.Visible = false;
                //pnlUploadfile.Visible = false;

            }
            else
            {
                pnlIssuetype.Visible = true;
                pnlUploadfile.Visible = true;
            }
        }

        protected string GetUrl(string projectReference, string IssueID)
        {
            string retVal = string.Empty;
            try
            {
                if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("projectissues"))
                {
                    retVal = string.Format("~/ProjectIssues.aspx?Project={0}&issueid={1}", projectReference, IssueID);
                }
                else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("checkpoint"))
                {
                    retVal = string.Format("~/Checkpoint_PMIssueList.aspx?Project={0}&issueid={1}", projectReference, IssueID);
                }
                else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("qa"))
                {
                    retVal = string.Format("~/QAIssues.aspx?Project={0}&issueid={1}", projectReference, IssueID);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return retVal;
        }



        protected void InsertIssueClick()
        {
            int GetIDSection = 0;
            if (!PermissionManager.IsPermitted(QueryStringValues.Project, sessionKeys.UID, PermissionManager.PermissionsTo.ManagageIssues))
            {
                lblError.Text = Resources.DeffinityRes.Userdoesnthaverights; //"User doesn't have rights to Manage Issues";

                return;
            }
            try
            {
                DateTime asdate = Convert.ToDateTime(string.IsNullOrEmpty(txtdatechecked.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtdatechecked.Text.Trim());
                DateTime DateCompleted = Convert.ToDateTime(string.IsNullOrEmpty(txtDateCompleted.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtDateCompleted.Text.Trim());
                DateTime DateNextReviewDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextReviewDate.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtNextReviewDate.Text.Trim());

                //if (QueryStringValues.Project == 0)//Global
                //{

                if (pnlAssociated.Visible == true)
                {
                    GetIDSection = GetIDbySection();

                    if (ddlAssociatedTo.Visible == true)
                    {
                        IssueSection = ddlAssociatedTo.SelectedItem.Text.Trim();
                    }
                    if (ddlProjectList.Visible == true)
                    {
                        GetIDSection = Convert.ToInt32(ddlProjectList.SelectedValue);
                    }

                    if (ddlHealthCheck.Visible == true)
                    {
                        GetIDSection = Convert.ToInt32(ddlHealthCheck.SelectedValue);
                    }

                }
                else
                {
                    GetIDSection = GetIDbySection();
                }
                int EnableCust = 0;
                if (chkEnbleCust.Checked == true)
                {
                    EnableCust = 1;
                }

                object objResult = objGlobal.IssueInsert(GetIDSection, Convert.ToInt32(ddlQAtype.SelectedValue), txtIssue.Text.Trim(), Convert.ToInt32(ddlAssign.SelectedValue),
                    Convert.ToDateTime(txtDate.Text.Trim()), txtNotes.Text.Trim(), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlQAtype.SelectedValue),
                    txtExpectedOutcome.Text.Trim(), sessionKeys.UID, Convert.ToInt32(ddlcheckedby.SelectedValue), asdate, Convert.ToInt16(ddlIssueraisedBy.SelectedValue), Convert.ToInt32(ddllocation.SelectedValue),
                    DateCompleted, IssueSection, DateNextReviewDate, EnableCust, int.Parse(ddlRagstatus.SelectedValue), txtActionPlan.Text.Trim());

                if (objResult != null)
                {
                    hidIssueID.Value = objResult.ToString();
                    gvBindIssues.DataBind();
                    if ((hidIssueID.Value != "") && (hidIssueID.Value != "-99"))
                    {
                        UploadDocuments();
                        gvBindDocuments.DataBind();
                    }

                    lblError.Text = Resources.DeffinityRes.Issuesavedsuccessfully; // "Issue saved successfully!!";
                    int assignedUser = Convert.ToInt32(ddlAssign.SelectedValue);
                    if (assignedUser > 0)
                    {
                        MailToAssignedUser(int.Parse(hidIssueID.Value), assignedUser);
                    }
                    //if (EnableCust == 1)
                    //{
                    //    if (chkEnbleCust.Checked == true)
                    //    {
                    //        MailSection(int.Parse(hidIssueID.Value));
                    //    }
                    //}
                    ClearFields();
                }
                //}

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public string Prefix()
        {
            return sessionKeys.Prefix;
        }
        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                int projectRef = 0;
                if (QueryStringValues.Project != 0 || ddlProjectList.Visible == true)
                {
                    projectRef = QueryStringValues.Project == 0 ? int.Parse(ddlProjectList.SelectedValue) : QueryStringValues.Project;
                }
                if (projectRef > 0)
                {
                    if (chklesson.Checked)
                    {
                        using (projectTaskDataContext pt = new projectTaskDataContext())
                        {

                            LessonsLearnt lL = pt.LessonsLearnts.Where(l => l.ProjectReference == projectRef && l.AssignedTo == int.Parse(ddlAssign.SelectedValue) && l.IdentifiedBy == int.Parse(ddlIssueraisedBy.SelectedValue) && l.Status == 1 && l.Description == txtIssue.Text).FirstOrDefault();

                            if (lL == null)
                            {
                                lblErrorMsg.Visible = false;

                                LessonsLearnt lessonsLearnt = new LessonsLearnt();
                                lessonsLearnt.AssignedTo = int.Parse(ddlAssign.SelectedValue);
                                lessonsLearnt.IdentifiedBy = int.Parse(ddlIssueraisedBy.SelectedValue);
                                lessonsLearnt.Status = 1;
                                lessonsLearnt.Description = txtIssue.Text;
                                lessonsLearnt.BusinessImpact = string.Empty;
                                lessonsLearnt.ProjectReference = projectRef;
                                lessonsLearnt.RemediationActions = txtNotes.Text;
                                pt.LessonsLearnts.InsertOnSubmit(lessonsLearnt);
                                pt.SubmitChanges();

                                IssueLessonLearnt issueLessonLearnt = new IssueLessonLearnt();
                                issueLessonLearnt.IssueRef = int.Parse(hidIssueID.Value);
                                issueLessonLearnt.LessonLearntID = lessonsLearnt.ID;
                                pt.IssueLessonLearnts.InsertOnSubmit(issueLessonLearnt);
                                pt.SubmitChanges();
                            }
                            else
                            {
                                lblErrorMsg.Visible = true;
                                lblErrorMsg.Text = "Already added to lessons learnt.";
                            }
                        }

                    }
                }
                Button objsender = (Button)sender;

                if (objsender.CommandName == "Update")
                {
                    int i = int.Parse(ddlIssueraisedBy.SelectedValue);
                    UpdateIssueClick();

                }
                else
                {
                    InsertIssueClick();

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }


        }

        protected bool CreateFolder(string _Path)
        {
            bool blnReturn = false;
            if (!Directory.Exists(_Path))
            {
                try
                {
                    Directory.CreateDirectory(_Path);
                    blnReturn = true;
                }
                catch (UnauthorizedAccessException UnAuthorized)
                {
                    throw UnAuthorized;
                }
                finally
                {
                }
            }
            return blnReturn;
        }

        protected bool DeleteFolder(string _Path)
        {
            bool blnReturn = false;
            if (Directory.Exists(_Path))
            {
                try
                {
                    Directory.Delete(_Path);
                    blnReturn = true;
                }
                catch (UnauthorizedAccessException UnAuthorized)
                {
                    throw UnAuthorized;
                }
                finally
                {
                }
            }
            return blnReturn;
        }

        protected string GetDocuments(string ID)
        {
            return "return PopUp('" + ID + "');";
            //hidIssueID.Value = ID;
        }

        private void UploadDocuments()
        {

            HttpFileCollection hfc = Request.Files;
            if (hfc.Count > 0)
            {


                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {

                        string file = System.IO.Path.GetFileName(hpf.FileName);
                        string[] FileExtension = file.Split('.');
                        string Documentename = FileExtension[0].ToString();
                        string Extension = FileExtension[1].ToString();
                        string DeviationIDPath = Server.MapPath(("~//UploadData//Issues//" + hidIssueID.Value));
                        CreateFolder(DeviationIDPath);
                        DateTime createDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        //int DocumentID = objGlobalDiviation.DeviationDocument_Insert(Documentename, Convert.ToInt32(hidIssueID.Value), createDate, Extension);
                        object objDocResult = objGlobal.InsertDocument(Documentename, createDate, Convert.ToInt32(hidIssueID.Value), Extension);
                        string Deletedocument = Server.MapPath(("~//UploadData//Issues//" + hidIssueID.Value) + "/" + objDocResult.ToString() + "_" + file);
                        //fUploadUserImage.PostedFile.SaveAs(Server.MapPath("~//UploadData//Deviations//" + hidIssueID.Value) + "/" + DocumentID + "_" + file);
                        hpf.SaveAs(Server.MapPath("~//UploadData//Issues//" + hidIssueID.Value) + "/" + objDocResult.ToString() + "_" + file);
                    }
                }
            }
        }

        protected void UpdateIssueClick()
        {
            try
            {
                DateTime asdate = Convert.ToDateTime(string.IsNullOrEmpty(txtdatechecked.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtdatechecked.Text.Trim());
                DateTime DateCompleted = Convert.ToDateTime(string.IsNullOrEmpty(txtDateCompleted.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtDateCompleted.Text.Trim());
                DateTime DateNextReviewDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextReviewDate.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtNextReviewDate.Text.Trim());
                int IssueId = Convert.ToInt32(hidIssueID.Value);

                string IssueSectionHidden = HiddenIssueSection.Value;
                int EnableCust = 0;
                if (chkEnbleCust.Checked == true)
                {
                    EnableCust = 1;
                }
                using (projectTaskDataContext pd = new projectTaskDataContext())
                {
                    var projectIssue = pd.ProjectIssues.Where(p => p.ID == IssueId).Select(p => p).FirstOrDefault();
                    if (projectIssue != null)
                    {
                        int assignedUser = Convert.ToInt32(ddlAssign.SelectedValue);
                        if (assignedUser > 0)
                        {
                            if (assignedUser != projectIssue.AssignTo)
                            {
                                MailToAssignedUser(int.Parse(hidIssueID.Value), assignedUser);

                            }
                        }
                    }
                    ProjectIssue P_Issue = new ProjectIssue();
                    P_Issue = pd.ProjectIssues.Where(a => a.ID == IssueId).FirstOrDefault();
                    if (ddlAssociatedTo.SelectedItem.Text == "Project")
                    {
                        P_Issue.Projectreference = int.Parse(ddlProjectList.SelectedValue);
                    }
                    if (ddlAssociatedTo.SelectedItem.Text == "Health Check")
                    {
                        P_Issue.Projectreference = int.Parse(ddlHealthCheck.SelectedValue);
                    }
                    P_Issue.IssueSection = ddlAssociatedTo.SelectedItem.Text;
                    pd.SubmitChanges();

                }
                object objResult = objGlobal.IssueUpdate(IssueId, txtIssue.Text.Trim(), Convert.ToInt32(ddlAssign.SelectedValue), Convert.ToDateTime(txtDate.Text.Trim()), txtNotes.Text.Trim(),
                    Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlQAtype.SelectedValue),
                    txtExpectedOutcome.Text.Trim(), DateCompleted, Convert.ToInt32(ddlIssueraisedBy.SelectedValue),
                    DateNextReviewDate, EnableCust, int.Parse(ddlRagstatus.SelectedValue), txtActionPlan.Text.Trim());


                //if (EnableCust == 1)
                //{
                //    if (chkEnbleCust.Checked == true)
                //    {
                //        MailSection(IssueId);
                //    }
                //}
                gvBindIssues.DataBind();




                //string file = System.IO.Path.GetFileName(fUploadUserImage.FileName);

                UploadDocuments();
                gvBindDocuments.DataBind();



                //clear the fieds
                ClearFields();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
        }


        protected void btnIssueUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime asdate = Convert.ToDateTime(string.IsNullOrEmpty(txtdatechecked.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtdatechecked.Text.Trim());
                DateTime DateCompleted = Convert.ToDateTime(string.IsNullOrEmpty(txtDateCompleted.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtDateCompleted.Text.Trim());
                DateTime DateNextReviewDate = Convert.ToDateTime(string.IsNullOrEmpty(txtNextReviewDate.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtNextReviewDate.Text.Trim());
                int IssueId = Convert.ToInt32(hidIssueID.Value);

                string IssueSectionHidden = HiddenIssueSection.Value;
                int EnableCust = 0;
                if (chkEnbleCust.Checked == true)
                {
                    EnableCust = 1;
                }
                object objResult = objGlobal.IssueUpdate(IssueId, txtIssue.Text.Trim(), Convert.ToInt32(ddlAssign.SelectedValue), Convert.ToDateTime(txtDate.Text.Trim()),
                    txtNotes.Text.Trim(), Convert.ToInt32(ddlStatus.SelectedValue),
                    Convert.ToInt32(ddlQAtype.SelectedValue), txtExpectedOutcome.Text.Trim(),
                    DateCompleted, Convert.ToInt32(ddlIssueraisedBy.SelectedValue),
                    DateNextReviewDate, EnableCust, int.Parse(ddlRagstatus.SelectedValue), txtActionPlan.Text.Trim());
                gvBindIssues.DataBind();




                //string file = System.IO.Path.GetFileName(fUploadUserImage.FileName);

                UploadDocuments();
                gvBindDocuments.DataBind();



                //clear the fieds
                ClearFields();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }

        }
        protected void btnIssueCancel_Click(object sender, EventArgs e)
        {

            ClearFields();
            popIssues.Hide();

        }
        private void ClearFields()
        {
            lblError.Text = "";
            //lblErrorDocument.Text = "";
            hidIssueID.Value = "-99";
            gvBindDocuments.DataBind();
            gvBindIssues.DataBind();
            txtDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), DateTime.Now);
            txtdatechecked.Text = "";
            txtExpectedOutcome.Text = "";
            txtIssue.Text = "";
            txtNotes.Text = "";
            txtActionPlan.Text = "";
            ddlAssign.SelectedValue = "0";
            ddlcheckedby.SelectedValue = "0";
            ddlIssueraisedBy.SelectedIndex = ddlIssueraisedBy.Items.IndexOf(ddlIssueraisedBy.Items.FindByValue(sessionKeys.UID.ToString()));
            txtNextReviewDate.Text = string.Empty;
            txtDateCompleted.Text = string.Empty;
            ddlRagstatus.SelectedValue = "0";
            chklesson.Checked = false;
            if (IssueSection != Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {
                ddlQAtype.DataBind();
                ddtype.DataBind();
            }


            // ddlIssueraisedBy.DataBind();
            // ddlIssueraisedBy.Items.Insert(0, new ListItem("Please select...", "0"));
            // ddlAssign.DataBind();
            // ddlAssign.Items.Insert(0, new ListItem("Please select...", "0"));
            // ddlcheckedby.DataBind();
            // ddlcheckedby.Items.Insert(0, new ListItem("Please select...", "0"));
            BindSite();
            //ddllocation.DataBind();
            //ddlQAtype.DataBind();
            ddlStatus.DataBind();
            ddlStatus.Items.RemoveAt(0);
            //ddtype.DataBind();
            if (hidIssueID.Value != "-99")
            {


                btnAddnew.CommandName = "Update";
                //btnAddnew.SkinID = "ImgUpdate";
            }
            else
            {

                btnAddnew.CommandName = "Insert";
                //btnAddnew.SkinID = "ImgSubmit";

            }
            HiddenIssueSection.Value = "";
            lblAssociatedTo.Text = Resources.DeffinityRes.Associatedto; // "Associated to";
            txtAssociatedTo.Visible = false;
            ddlAssociatedTo.Visible = true;
            //ddlHealthCheck.Visible = true;
            ddlProjectList.Visible = true;
        }
        protected void gvBindIssues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "cmdViewExt")
                {

                    int IssueID = Convert.ToInt16(e.CommandArgument);
                    hidIssueID.Value = IssueID.ToString();
                    lblError.Text = "";

                    gvBindDocuments.DataSourceID = "";
                    gvBindDocuments.DataSource = objGlobal.DocumentsByID(IssueID);
                    gvBindDocuments.DataBind();

                    mplDocuments.Show();


                }
                else if (e.CommandName == "Approve")
                {
                    try
                    {

                        int ID = Convert.ToInt32(e.CommandArgument.ToString());
                        objGlobal.UpdateStatus(ID);
                        gvBindIssues.DataBind();

                    }
                    catch (Exception ex)
                    {
                        LogExceptions.WriteExceptionLog(ex);
                    }
                }
                else if (e.CommandName == "DeleteThis")
                {
                    //Delete code here
                    int ID = int.Parse(e.CommandArgument.ToString());

                    /// To get Deviation assocated Documents
                    //DataTable dtDocuments = objGlobal.GetDataIssueDeleteDocuments(ID);
                    /// To delete Deviation and assocated Documents
                    objGlobal.IssueAndDocumentsDelete(ID);



                    //if (dtDocuments.Rows.Count != 0)
                    //{

                    //    /// To delete Deviation assocated Documents by physical delete
                    //    for (int i = 0; i < dtDocuments.Rows.Count; i++)
                    //    {
                    //        string DocId = dtDocuments.Rows[i]["DocId"].ToString();
                    //        string DocumentName = dtDocuments.Rows[i]["DocumentName"].ToString();
                    //        string Extension = dtDocuments.Rows[i]["Extension"].ToString();
                    //        string filepath = DocId + "_" + DocumentName + "." + Extension;
                    //        string Deletedocument = Server.MapPath(("~//UploadData//Issues//" + ID.ToString()) + "/" + filepath);

                    //        if (File.Exists(Deletedocument))
                    //        {
                    //            File.Delete(Deletedocument);
                    //        }
                    //    }
                    //}
                    string DeleteMyFolder = Server.MapPath("~//UploadData//Issues//" + ID.ToString());

                    DeleteFolder(DeleteMyFolder);
                    gvBindIssues.DataBind();

                }
                else if (e.CommandName == "cmdEdit")
                {

                    DisplayCmdEdit(Convert.ToInt16(e.CommandArgument));

                    popIssues.Show();
                    //btnAddnew.Visible = false;
                    //lnkCancel.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public bool CustomerSignOffVisible(string status)
        {
            bool visible = false;
            if (status == "True")
                visible = true;
            return visible;
        }
        private void BindCommentsGrid(string projectRef)
        {
            using (projectTaskDataContext pi = new projectTaskDataContext())
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var x = ud.Contractors.Select(c => c).ToList();
                    var y = pi.ProjectIssueComments.Where(p => p.ProjectRef == int.Parse(projectRef) && p.IssueRef == int.Parse(hidIssueID.Value)).Select(p => p).ToList();

                    var result = (from p in y
                                  join c in x on p.SubmitteBy equals c.ID
                                  where p.ProjectRef == int.Parse(projectRef) && p.IssueRef == int.Parse(hidIssueID.Value)
                                  select new { p.ID, p.ProjectRef, p.IssueRef, p.Comments, p.CommentDate, c.ContractorName }).ToList();
                    gvCustomerComments.DataSource = result;
                    gvCustomerComments.DataBind();

                }
            }

        }
        protected void DisplayCmdEdit(int IssueIDArg)
        {



            //popIssues.Show();

            //string projectRef = Request.QueryString["project"].ToString();

            int IssueID = IssueIDArg;
            hidIssueID.Value = IssueID.ToString();
            lblError.Text = "";
            DataTable dtIssues = objGlobal.SelectionWithID(IssueID);
            if (dtIssues.Rows.Count != 0)
            {
                lblRefID.Text = sessionKeys.Prefix + dtIssues.Rows[0]["ProjectReference"].ToString() + "-" + IssueID;

               
               
                txtDate.Text = string.Format(Deffinity.systemdefaults.GetStringDateformat(), dtIssues.Rows[0]["ScheduledDate"]);
                txtExpectedOutcome.Text = dtIssues.Rows[0]["ExpectedOutcome"].ToString();
                txtIssue.Text = dtIssues.Rows[0]["Issue"].ToString();
                txtNotes.Text = dtIssues.Rows[0]["Notes"].ToString();
                txtActionPlan.Text = dtIssues.Rows[0]["ActionPlan"].ToString();

                if (!string.IsNullOrEmpty(dtIssues.Rows[0]["IssueRaisedby"].ToString()))
                {
                    ddlIssueraisedBy.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["IssueRaisedbyID"].ToString()) ? "0" : dtIssues.Rows[0]["IssueRaisedbyID"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(dtIssues.Rows[0]["AssignTo"].ToString()))
                {
                    ddlAssign.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["AssignTo"].ToString()) ? "0" : dtIssues.Rows[0]["AssignTo"].ToString();
                }
                if (!string.IsNullOrEmpty(dtIssues.Rows[0]["CheckedBy"].ToString()))
                {
                    ddlcheckedby.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["CheckedByID"].ToString()) ? "0" : dtIssues.Rows[0]["CheckedByID"].ToString().Trim();
                }

                ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(dtIssues.Rows[0]["Location"].ToString()));

                //ddllocation.SelectedItem.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["Location"].ToString()) ? "0" : dtIssues.Rows[0]["Location"].ToString();
                ddlStatus.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["Status"].ToString()) ? "0" : dtIssues.Rows[0]["Status"].ToString();
                ddlQAtype.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["IssuseType"].ToString()) ? "0" : dtIssues.Rows[0]["IssuseType"].ToString();
                txtdatechecked.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["DateChecked"].ToString()) ? string.Empty : Convert.ToDateTime(dtIssues.Rows[0]["DateChecked"].ToString()).ToShortDateString();
                txtDateCompleted.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["DateCompleted"].ToString()) ? string.Empty : Convert.ToDateTime(dtIssues.Rows[0]["DateCompleted"].ToString()).ToShortDateString();
                txtNextReviewDate.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["NextReviewDate"].ToString()) ? string.Empty : Convert.ToDateTime(dtIssues.Rows[0]["NextReviewDate"].ToString()).ToShortDateString();
                // txtAssociatedTo.Text = dtIssues.Rows[0]["ProjectTitleWithRef"].ToString();
                //Projectreference
                txtAssociatedTo.Text = sessionKeys.Prefix + dtIssues.Rows[0]["ProjectReference"].ToString();
                ddlRagstatus.SelectedValue = "";
                ddlRagstatus.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["RAGStatus"].ToString()) ? "0" : dtIssues.Rows[0]["RAGStatus"].ToString();
                string val = string.IsNullOrEmpty(dtIssues.Rows[0]["EnableCust"].ToString()) ? "0" : dtIssues.Rows[0]["EnableCust"].ToString();
                if (val == "0")
                { chkEnbleCust.Checked = false; }
                else
                { chkEnbleCust.Checked = true; ; }
                gvBindDocuments.DataBind();
                if (hidIssueID.Value != "-99")
                {



                    Panel_Insert.Visible = true;
                    btnAddnew.Visible = true;
                    lnkCancel.Visible = true;

                    btnAddnew.CommandName = "Update";
                    //btnAddnew.SkinID = "ImgUpdate";

                    ddlAssociatedTo.Visible = false;
                    ddlProjectList.Visible = false;
                    ddlHealthCheck.Visible = false;
                }
                else
                {

                    Panel_Insert.Visible = true;
                    btnAddnew.Visible = true;
                    lnkCancel.Visible = true;
                    btnAddnew.CommandName = "Insert";
                    //btnAddnew.SkinID = "ImgSubmit";

                    txtAssociatedTo.Visible = false;
                    lblAssociatedTo.Visible = false;
                    ddlAssociatedTo.Visible = true;
                    ddlProjectList.Visible = true;
                }
                //  if (HiddenIssueSection.Value.ToLower() == "project")
                if (dtIssues.Rows[0]["IssueSection"].ToString().ToLower() == "project")
                {
                    lblAssociatedTo.Text = Resources.DeffinityRes.AssociatedtoProject;//"Associated to Project:";
                }
                else if (dtIssues.Rows[0]["IssueSection"].ToString().ToLower() == "healthcheck")
                {
                    lblAssociatedTo.Text = Resources.DeffinityRes.AssociatedtoAudit; //"Associated to Audit :";
                }
                else if (dtIssues.Rows[0]["IssueSection"].ToString().ToLower() == "global")
                {
                    //lblAssociatedTo.Text = string.Empty;
                    lblAssociatedTo.Text = string.Empty;
                    //lblAssociatedTo0.Visible = false;
                    txtAssociatedTo.Text = string.Empty;
                }
                BindCommentsGrid(dtIssues.Rows[0]["ProjectReference"].ToString());

                if (dtIssues.Rows[0]["ProjectReference"].ToString() != "0")
                {
                    lblAssociatedTo.Visible = true;
                    txtAssociatedTo.Visible = true;
                    lblAssociatedTo0.Visible = false;
                    ddlAssociatedTo.Visible = false;
                    ddlProjectList.Visible = false;
                    ddlHealthCheck.Visible = false;
                    if (ddlAssociatedTo.Visible == true)
                    {
                        ddlAssociatedTo.SelectedItem.Text = HiddenIssueSection.Value;
                    }
                }
                else
                {
                    lblAssociatedTo.Visible = false;
                    txtAssociatedTo.Visible = false;
                    lblAssociatedTo0.Visible = true;
                    ddlAssociatedTo.Visible = true;
                    ddlProjectList.Visible = true;
                    ddlHealthCheck.Visible = false;
                    if (ddlAssociatedTo.Visible == true)
                    {
                        if (!string.IsNullOrEmpty(HiddenIssueSection.Value))
                        {
                            ddlAssociatedTo.SelectedItem.Text = HiddenIssueSection.Value;
                        }
                        else {
                            ddlAssociatedTo.SelectedValue = "3";
                            ddlProjectList.Visible = false;
                        }
                    }
                }

                //if (dtIssues.Rows[0]["Projectreference"].ToString() != " ")
                //{


                //}
                //else
                //{

                //}

            }
        }
        protected void gvBindDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                object[] objList = (e.Row.DataItem as DataRowView).Row.ItemArray as object[];
                if (objList != null)
                {
                    if (objList[0].ToString() == "-99")
                    {
                        e.Row.Visible = false;
                    }
                }


            }
        }
        protected void gvBindDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (hidIssueID.Value != "-99")
                {

                    if (e.CommandName == "cmdDownload")
                    {
                        int DocID = Convert.ToInt16(e.CommandArgument);
                        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        string fileName = ((Label)gvrow.FindControl("lblDocumentName")).Text;

                        string extension = ((Label)gvrow.FindControl("lblExtension")).Text;

                        string path1 = Server.MapPath(("~//UploadData//Issues//" + hidIssueID.Value) + "/" + DocID + "_" + fileName);
                        //"D:\shaifal's Documents\My Projects\default.aspx";
                        if (File.Exists(path1))
                        {
                            string name = Path.GetFileName(path1);
                            Response.ContentType = "application/ms-excel";
                            Response.WriteFile(path1);
                            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; FileName=" + fileName);
                            //threadEnd = true;
                            Response.End();
                        }
                    }
                    if (e.CommandName == "cmdDelete")
                    {
                        int DocID = Convert.ToInt16(e.CommandArgument);
                        objGlobal.DocumentDelete(DocID);

                        GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        string file = ((Label)gvrow.FindControl("lblDocumentName")).Text;
                        string fileExtension = ((Label)gvrow.FindControl("lblExtension")).Text;
                        string FileName = Server.MapPath(("~//UploadData//Issues//" + hidIssueID.Value) + "/" + DocID + "_" + file);

                        //string Deletedocument = Server.MapPath(("UploadDocuments") + "/" + hidIssueID.Value + "_" + file);
                        if (File.Exists(FileName))
                        {
                            // File.Delete(Deletedocument);
                            File.Delete(FileName);
                            //lblError.Text = "Document Deleted";
                            lblError.Text = Resources.DeffinityRes.DoucmentDeleted;

                        }

                    }
                }

                else
                {
                    lblError.Text = Resources.DeffinityRes.PlsselectIssueorcreatenew; //"Please select one Issue or create new Issue";
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);

            }
        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            //loginBoxContent.Visible = false;


        }
        protected void gvBindDocuments_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBindDocuments.EditIndex = e.NewEditIndex;
            //loginBoxContent.Visible = true;
            int IssueID = Convert.ToInt32(hidIssueID.Value);
            gvBindDocuments.DataSourceID = "";
            gvBindDocuments.DataSource = objGlobal.DocumentsByID(IssueID);
            gvBindDocuments.DataBind();


        }
        protected void gvBindDocuments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBindDocuments.EditIndex = -1;
            //loginBoxContent.Visible = true;
            int IssueID = Convert.ToInt32(hidIssueID.Value);
            gvBindDocuments.DataSourceID = "";
            gvBindDocuments.DataSource = objGlobal.DocumentsByID(IssueID);
            gvBindDocuments.DataBind();
        }
        protected void gvBindDocuments_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //loginBoxContent.Visible = true;
            int IssueID = Convert.ToInt32(hidIssueID.Value);
            gvBindDocuments.DataSourceID = "";
            gvBindDocuments.DataSource = objGlobal.DocumentsByID(IssueID);
            gvBindDocuments.DataBind();
        }
        protected void gvBindDocuments_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //loginBoxContent.Visible = true;
            int IssueID = Convert.ToInt32(hidIssueID.Value);
            gvBindDocuments.DataSourceID = "";
            gvBindDocuments.DataSource = objGlobal.DocumentsByID(IssueID);
            gvBindDocuments.DataBind();
        }
        protected void gvBindDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int IssueID = Convert.ToInt32(hidIssueID.Value);
            gvBindDocuments.DataSourceID = "";
            gvBindDocuments.DataSource = objGlobal.DocumentsByID(IssueID);
            gvBindDocuments.DataBind();
        }


        protected bool DeleteVisible(string RaisedBy)
        {
            bool blnRetVal = false;

            if ((sessionKeys.SID <= 3))
            {
                blnRetVal = true;
            }
            else if (sessionKeys.UID == Convert.ToInt32(RaisedBy))
            {
                blnRetVal = true;

            }

            return blnRetVal;
        }
        protected bool GetDocsEnable(string strStatus)
        {

            bool blnEnable = true;
            if (strStatus == "0")
            {
                blnEnable = false;
            }
            return blnEnable;

        }
        protected bool CompletedVisibilityNot(string strStatus)
        {


            bool blnRetVal = false;

            if (strStatus != "3")
            {
                blnRetVal = true;
            }

            return blnRetVal;
        }

        protected bool CompletedVisibility(string strStatus)
        {
            bool blnRetVal = false;

            if (strStatus == "3")
            {
                blnRetVal = true;
            }

            return blnRetVal;
        }
        protected void objDsBindGrid_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            if (QueryStringValues.Project > 0)
            {
                e.InputParameters["ID"] = QueryStringValues.Project;
                e.InputParameters["IssueSection"] = Deffinity.GlobalIssues.IssueSection.Project.ToString();
            }
            else if (QueryStringValues.HealthCheckId > 0)
            {
                ddtype.Visible = false;
                tdCategory.Visible = false;
                tdddlCategory.Visible = false;
                DivCategory.Visible = false;

                e.InputParameters["ID"] = QueryStringValues.HealthCheckId;
                e.InputParameters["IssueSection"] = Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString();
            }
            else
            {
                e.InputParameters["ID"] = 0;
                e.InputParameters["IssueSection"] = Deffinity.GlobalIssues.IssueSection.Global.ToString();
            }

        }
        private int GetIDbySection()
        {
            int ID = 0;
            if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {
                ID = QueryStringValues.HealthCheckId;
            }
            else if (IssueSection == Deffinity.GlobalIssues.IssueSection.Project.ToString())
            {
                ID = QueryStringValues.Project;
            }
            return ID;
        }

        private string GetSection()
        {
            string ID = "";
            if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {
                ID = "HealthCheck";
            }
            else if (IssueSection == Deffinity.GlobalIssues.IssueSection.Project.ToString())
            {
                ID = "Project";
            }
            return ID;
        }

        #region change Issue type
        private void CheckIssueType()
        {
            if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {
                //pnlIssuetype.Visible = false;
                //ddlCategory.Visible = false;
                //Category.Visible = false;
                if (ddtype.Items.FindByText(Deffinity.GlobalIssues.Project.IssueTypeDefault) != null)
                {
                    ddtype.SelectedIndex = ddtype.Items.IndexOf(ddtype.Items.FindByText(Deffinity.GlobalIssues.Project.IssueTypeDefault));
                }
            }
            else
            {
                pnlIssuetype.Visible = true;
            }

        }
        #endregion

        protected void gvBindIssues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {

                gvBindIssues.Columns[10].Visible = false;
            }
            else
            {
                gvBindIssues.Columns[10].Visible = true;
            }

        }

        private void BindSite()
        {
            if (IssueSection == Deffinity.GlobalIssues.IssueSection.Healthcheck.ToString())
            {
                ddllocation.DataSourceID = "";
                ddllocation.DataSource = Deffinity.Bindings.DefaultDatabind.b_SiteSelect_Portfilio(sessionKeys.PortfolioID);
                ddllocation.DataValueField = "ID";
                ddllocation.DataTextField = "Site";
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem("Please select...", "0"));
                //ddllocation.Items.Insert(0, new ListItem(Resources.DeffinityRes.Pleaseselect, "0"));

            }
            else
            {


                //int p=71;
                if (QueryStringValues.Project.ToString() != "0")
                {
                    objSiteByProjectRef.SelectParameters[0].DefaultValue = QueryStringValues.Project.ToString();
                }
                else
                {
                    //ddlProjectList.DataBind();

                    objSiteByProjectRef.SelectParameters[0].DefaultValue = ddlProjectList.SelectedValue;
                }

                ddllocation.DataSourceID = "objSiteByProjectRef";
                ddllocation.DataBind();


                ddllocation.Items.Insert(0, new ListItem("Please select...", "0"));
            }
        }
        protected void gvBindIssues_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
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
            else
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

            btnAddnew.Enabled = false;

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
                else
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

        protected string GetImage(string status)
        {
            string url = string.Empty;
            if (status == "1")
                url = "<span style='color:green;'><i class='fa fa-circle'></i></span>";
            else if (status == "3")
                url = "<span style='color:red;'><i class='fa fa-circle'></i></span>";
            else if (status == "2")
                url = "<span style='color:yellow;'><i class='fa fa-circle'></i></span>";
            else
                url = string.Empty;

            return url;
        }

        protected bool SetVisible(string status)
        {
            bool vis = false;
            //if ( != 0)
            if (int.Parse(string.IsNullOrEmpty(status) ? "0" : status) != 0)
            {
                vis = true;
            }
            return vis;
        }


        #region "Mail to Assigned User"
        private void MailToAssignedUser(int issueID, int userID)
        {
            try
            {
                int projectRef = 0;
                if (QueryStringValues.Project != 0 || ddlProjectList.Visible == true)
                {
                    projectRef = QueryStringValues.Project == 0 ? int.Parse(ddlProjectList.SelectedValue) : QueryStringValues.Project;
                }
                if (projectRef > 0)
                {
                    using (UserDataContext ud = new UserDataContext())
                    {
                        using (projectTaskDataContext pd = new projectTaskDataContext())
                        {
                            var projectDetail = pd.ProjectDetails.Where(p => p.ProjectReference == projectRef).Select(p => p).FirstOrDefault();
                            if (projectDetail != null)
                            {
                                var contractor = ud.Contractors.Where(c => c.Status.ToLower() == "active" && c.ID == userID).Select(c => c).FirstOrDefault();
                                if (contractor != null)
                                {
                                    MailCnt.Visible = true;
                                    MailCnt.setdata(issueID, sessionKeys.Project, contractor.ContractorName);


                                    string htmlText = string.Empty;
                                    StringWriter sw = new StringWriter();
                                    Html32TextWriter htmlTW = new Html32TextWriter(sw);
                                    MailCnt.RenderControl(htmlTW);
                                    htmlText = htmlTW.InnerWriter.ToString();
                                    string errorString = string.Empty;
                                    Email eMail = new Email();
                                    if (!string.IsNullOrEmpty(contractor.EmailAddress))
                                    {
                                        eMail.SendingMail(contractor.EmailAddress.ToString(), "Project-" + projectDetail.ProjectReferenceWithPrefix + ":" + projectDetail.ProjectTitle + "-Issue raised", htmlText);
                                        //Save to Inbox
                                        InboxBAL.SaveInboxMessage("Project-" + projectDetail.ProjectReferenceWithPrefix + ":" + projectDetail.ProjectTitle + "-Issue raised", userID, contractor.EmailAddress.ToString(), htmlText);

                                    }
                                    MailCnt.Visible = false;
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
        #endregion
        #region "Mail Section"
        private void MailSection(int issuID)
        {
            try
            {
                UserDataContext CustomersDB = new UserDataContext();
                projectTaskDataContext projectDB = new projectTaskDataContext();

                var projectTitle = (from r in projectDB.ProjectDetails
                                    where r.ProjectReference == sessionKeys.Project
                                    select r).ToList().FirstOrDefault();

                var listCustomers = (from r in projectDB.CustomerRagAlerts
                                     where r.ProjectReference == sessionKeys.Project
                                     && r.Issue == 1
                                     select r).ToList();
                var contractors = (from c in CustomersDB.Contractors
                                   where
                                       c.Status == "Active"
                                   select c).ToList();

                var emailIDs = (from c in contractors
                                join cp in listCustomers on c.ID equals cp.CustomerID
                                select c).ToList();
                if (emailIDs != null)
                {
                    foreach (UserMgt.Entity.Contractor id in emailIDs)
                    {
                        MailCnt.Visible = true;
                        MailCnt.setdata(issuID, sessionKeys.Project, id.ContractorName);

                        ArrayList ToEmailIds = new ArrayList(0);
                        ToEmailIds.Add(id.EmailAddress);
                        //ToEmailIds.Add(txtEmail.Text.Trim());
                        //string[] EmailIds = txtemail.Text.Trim().Split(';');
                        //foreach (string EmailId in EmailIds)
                        //{
                        //    ToEmailIds.Add(EmailId);
                        //}

                        //ArrayList al = new ArrayList(0); ;

                        string htmlText = string.Empty;
                        StringWriter sw = new StringWriter();
                        Html32TextWriter htmlTW = new Html32TextWriter(sw);
                        MailCnt.RenderControl(htmlTW);
                        htmlText = htmlTW.InnerWriter.ToString();
                        string errorString = string.Empty;
                        Email eMail = new Email();
                        if (!string.IsNullOrEmpty(id.EmailAddress))
                        {
                            //eMail.SendingMail("info@deffinity.com", "Project-" + projectTitle.ProjectReferenceWithPrefix + ":" + projectTitle.ProjectTitle+"-Issue raised", htmlText, id.EmailAddress.ToString());
                            eMail.SendingMail(id.EmailAddress.ToString(), "Project-" + projectTitle.ProjectReferenceWithPrefix + ":" + projectTitle.ProjectTitle + "-Issue raised", htmlText);
                            //lblMsg.Text = "Supplier requisition form has been sent successfully.";
                            //imgSaveEmail.Enabled = false;
                            //eMail.SendingMail(ToEmailIds, "Quote For " + ProjectReference, htmlText, FromEmail, attachment);
                            //eMail.SendingMail(txtemail.Text.Trim(), "Quote For " + ProjectReference, htmlText, FromEmail, attachment);
                        }
                        MailCnt.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        #endregion

        protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSite();
            popIssues.Show();
        }

        protected void lnkCancelInNewIssue_Click(object sender, EventArgs e)
        {
            ClaerFieldsINNewIssue();
            ModalPopupExtender1.Hide();
        }

        public void ClaerFieldsINNewIssue()
        {
            TxtNewIssue.Text = string.Empty;
            ddlAssigntoInNewIssue.SelectedValue = "0";
            ddlCategoryInNewIssue.SelectedValue = "0";
            TxtNextReviewDateinNewIssue.Text = string.Empty;
            txtDateLoggedInNewIssue.Text = string.Empty;
        }
        protected void btnAddIssue_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime asdate = Convert.ToDateTime(string.IsNullOrEmpty(txtDateLoggedInNewIssue.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtDateLoggedInNewIssue.Text.Trim());
                DateTime DateCompleted = Convert.ToDateTime(string.IsNullOrEmpty(string.Empty) ? DateTime.Now.ToShortDateString() : txtDateCompleted.Text.Trim());
                DateTime DateNextReviewDate = Convert.ToDateTime(string.IsNullOrEmpty(TxtNextReviewDateinNewIssue.Text.Trim()) ? DateTime.Now.ToShortDateString() : TxtNextReviewDateinNewIssue.Text.Trim());

                string Section = "";
                if (QueryStringValues.Project > 0)
                {
                    Section = "Project";
                }
                object objResult = objGlobal.IssueInsert(QueryStringValues.Project, Convert.ToInt32(0), TxtNewIssue.Text.Trim(),
                    Convert.ToInt32(ddlAssigntoInNewIssue.SelectedValue),
                       Convert.ToDateTime(txtDate.Text.Trim()), "", Convert.ToInt32(1),
                       Convert.ToInt32(ddlCategoryInNewIssue.SelectedValue),
                       "", sessionKeys.UID, Convert.ToInt32(0),
                       asdate, Convert.ToInt16(sessionKeys.UID), Convert.ToInt32(0),
                       DateCompleted, IssueSection, DateNextReviewDate, 0, int.Parse(ddlRagstatus.SelectedValue),
                       txtActionPlan.Text.Trim());
                ModalPopupExtender1.Hide();
                ClaerFieldsINNewIssue();
                gvBindIssues.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}