using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Incidents.Entity;
using Deffinity.EmailService;
using System.Collections;
using Incidents.DAL;
using Incidents.StateManager;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using DeffinityManager;
public partial class ChangeControl : System.Web.UI.Page
{
  
    ClsChangeControl objChangeControl = new ClsChangeControl();
    DisBindings getdata = new DisBindings();
    #region Page Load
  
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                //Clear other data cache
                ClearOtherCache();
               
                DataDictionary();
                if (sessionKeys.ChangeControlID != 0)
                {
                    BindDataByChangeControlID();
                    
                }
                else
                {
                    BindControls();
                    BindServiceRequest();
                    //bind projects by customers
                    BindProjectsByCustomer();
                }

              
                ddlRaisedBy.SelectedValue = Convert.ToString(sessionKeys.UID);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void DataDictionary()
    {
        lblSubcategory.Text = DataDictionaryBAL.DataDictionary_GetName("Sub Category");
        lblArea.Text = DataDictionaryBAL.DataDictionary_GetName("Area");
        lblCategory.Text = DataDictionaryBAL.DataDictionary_GetName("Category");
        lblSite.Text = DataDictionaryBAL.DataDictionary_GetName("Site");
        lblSubCategory1.Text = lblSubcategory.Text;
        lblArea1.Text = lblArea.Text;
    }
    private void BindControls()
    {
        BindCutomers();
        // set default value
        if (sessionKeys.PortfolioName != "ALL")
        {
            ListItem itemToRemove = ddlCustomerID.Items.FindByValue("0");
            if (itemToRemove != null)
            {
                ddlCustomerID.Items.Remove(itemToRemove);
            }
        }

        BindArea();
    }

    private void BindDataByChangeControlID()
    {
        Change change = new Change();
        DataTable dtFillValues = objChangeControl.GetDataById(sessionKeys.ChangeControlID);
        change = Getdata(dtFillValues);
        FillPageControls(change);
    }

    private static void ClearOtherCache()
    {
        TaskState.ClearTaskCache();
        ApprovalState.ClearApprovalCache();
        RiskState.ClearRiskCache();
    }

    private static Change Getdata(DataTable dtRow)
    {
        Change change = new Change();
        try
        {


            change.Id = Convert.ToInt32(dtRow.Rows[0]["Id"]);
            change.IncidentID = Convert.ToInt32(dtRow.Rows[0]["IncidentID"]);
            //change.PortfolioID = Convert.ToInt32(reader.IsDBNull(reader.GetOrdinal("PortfolioID")) ? 0 : reader["PortfolioID"]);
            change.PortfolioID = Convert.ToInt32(dtRow.Rows[0]["PortfolioID"]);
            change.ChangeDescription = dtRow.Rows[0]["ChangeDescription"].ToString();
            change.DateRaised = Convert.ToDateTime(string.IsNullOrEmpty(dtRow.Rows[0]["DateRaised"].ToString()) ? "01/01/1900" : dtRow.Rows[0]["DateRaised"].ToString());
            change.Justification = dtRow.Rows[0]["Justification"].ToString();
            change.ResourceImpact = dtRow.Rows[0]["ResourceImpact"].ToString();
            change.TargetReleaseDate = Convert.ToDateTime(string.IsNullOrEmpty(dtRow.Rows[0]["TargetReleaseDate"].ToString()) ? "01/01/1900" : dtRow.Rows[0]["TargetReleaseDate"].ToString());
            change.Title = dtRow.Rows[0]["Title"].ToString();
            change.RequesterName = dtRow.Rows[0]["RequesterName"].ToString();
            change.RequesterEmailID = dtRow.Rows[0]["RequesterEmailID"].ToString();
            change.Status = dtRow.Rows[0]["Status"].ToString();
            change.CategoryID = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["CategoryID"].ToString()) ? "0" : dtRow.Rows[0]["CategoryID"].ToString());
            change.Justification = dtRow.Rows[0]["Justification"].ToString();
            change.RaisedBy =  Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["RaisedBy"].ToString()) ? "0" : dtRow.Rows[0]["RaisedBy"].ToString());
            change.TargetStartDate = Convert.ToDateTime(string.IsNullOrEmpty(dtRow.Rows[0]["TargetStartDate"].ToString()) ? "01/01/1900" : dtRow.Rows[0]["TargetStartDate"].ToString());
            change.RelatesToProjectRef = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["RelatesToProjectRef"].ToString()) ? "0" : dtRow.Rows[0]["RelatesToProjectRef"].ToString());
            change.RelatesToservicerequest = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["RelatesToServiceRequest"].ToString()) ? "0" : dtRow.Rows[0]["RelatesToServiceRequest"].ToString());
            change.EstimatedCost = dtRow.Rows[0]["EstimatedCost"].ToString();
            change.EstimatedDaysRequired = dtRow.Rows[0]["EstimatedDaysRequired"].ToString();
            change.RequesterID = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["RequesterID"].ToString()) ? "0" : dtRow.Rows[0]["RequesterID"].ToString());
            change.CoOrdinator = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["CoOrdinator"].ToString()) ? "0" : dtRow.Rows[0]["CoOrdinator"].ToString());
            change.SubCategoryID = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["SubCategoryID"].ToString()) ? "0" : dtRow.Rows[0]["SubCategoryID"].ToString());
            change.DateLogged = Convert.ToDateTime(dtRow.Rows[0]["DateLogged"].ToString());
            change.InhandDate = Convert.ToDateTime(dtRow.Rows[0]["InHandTime"].ToString());
            change.ClosedDate = Convert.ToDateTime(dtRow.Rows[0]["ClosedTime"].ToString());
            change.AreaID = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["AreaID"].ToString()) ? "0" : dtRow.Rows[0]["AreaID"].ToString());
            change.PriorityID = Convert.ToInt32(string.IsNullOrEmpty(dtRow.Rows[0]["PriorityID"].ToString()) ? "0" : dtRow.Rows[0]["PriorityID"].ToString());
            change.AreaName = dtRow.Rows[0]["AreaName"].ToString();
            change.PriorityName = dtRow.Rows[0]["PriorityName"].ToString();
            change.SiteID = int.Parse( dtRow.Rows[0]["SiteID"].ToString());
            change.SiteName = dtRow.Rows[0]["SiteName"].ToString();

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return change;
    }

    #endregion

    #region Helper Methods

    private void FillPageControls(Change change)
    {
        ChangeState.ClearChangeCache();
        try
        {
            if (sessionKeys.ChangeControlID != 0)
            {
                //txtRequester.Text = change.RequesterName;
                txtRequesterEmail.Text = change.RequesterEmailID;
                txtChangeControlTitle.Text = change.Title;
                txtDateRaised.Text = change.DateRaised.ToShortDateString();
                txtTargetReleaseDate.Text = change.TargetReleaseDate.ToShortDateString();
                txtDescriptionOfChange.Text = change.ChangeDescription;
                txtJustification.Text = change.Justification;
                txtResourceImpact.Text = change.ResourceImpact;
                txtestimatedCost.Text = change.EstimatedCost;
                txtEstimatedDays.Text = change.EstimatedDaysRequired;
                txtTargetStartDate.Text = change.TargetStartDate.ToShortDateString();
                //ddlIssueraisedBy.SelectedItem.Text = string.IsNullOrEmpty(dtIssues.Rows[0]["IssueRaisedby"].ToString()) ? "0" : dtIssues.Rows[0]["IssueRaisedby"].ToString().Trim();
                //ddlAssign.SelectedValue = string.IsNullOrEmpty(dtIssues.Rows[0]["AssignTo"].ToString()) ? "0" : dtIssues.Rows[0]["AssignTo"].ToString();
                BindCutomers();
                //ListItem itm = new ListItem("Please select...","0");

                //ddlCustomerID.Items.Add(itm);
                //ddlCustomerID.SelectedValue = change.PortfolioID.ToString();
                //if (change.PortfolioID > 0)
                //{
                //    ddlCustomerID.SelectedValue = change.PortfolioID.ToString();
                    
                //}
                ddlCustomerID.SelectedIndex = ddlCustomerID.Items.IndexOf(ddlCustomerID.Items.FindByValue(change.PortfolioID.ToString()));
                //ddlRaisedBy.SelectedValue = change.RaisedBy.ToString();
                ddlRaisedBy.SelectedIndex = ddlRaisedBy.Items.IndexOf(ddlRaisedBy.Items.FindByValue(change.RaisedBy.ToString()));
                //ddlstatus.Text = change.Status;
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText(change.Status));

                BindServiceRequest();

                ddlRelatesToServiceRequest.SelectedValue = change.RelatesToservicerequest.ToString();
               // ddlRelatesToServiceRequest.SelectedIndex = ddlRelatesToServiceRequest.Items.IndexOf(ddlRelatesToServiceRequest.Items.FindByValue(change.RelatesToservicerequest.ToString()));
                //ddlRelatesToServiceRequest.SelectedValue = string.IsNullOrEmpty(change.RelatesToservicerequest.ToString()) ? "0" : change.RelatesToservicerequest.ToString();
                //bind projects by customers
                BindProjectsByCustomer();
                ddlProjectRef.SelectedIndex = ddlProjectRef.Items.IndexOf(ddlProjectRef.Items.FindByValue(change.RelatesToProjectRef.ToString()));
                ddlCategory1.DataBind();
                ddlCategory1.SelectedIndex = ddlCategory1.Items.IndexOf(ddlCategory1.Items.FindByValue(change.CategoryID.ToString()));
               
                //ddlCategory1.SelectedValue = string.IsNullOrEmpty(change.CategoryID.ToString()) ? "0" : change.CategoryID.ToString();
                
                //ddlProjectRef.SelectedValue = string.IsNullOrEmpty(change.RelatesToProjectRef.ToString()) ? "0" : change.RelatesToProjectRef.ToString();
                //////ddlCategory1.SelectedValue = change.CategoryID.ToString();
                //////ddlRelatesToServiceRequest.SelectedValue = change.RelatesToservicerequest.ToString();
                //////ddlProjectRef.SelectedValue = change.RelatesToProjectRef.ToString();
                ddlRequester.DataBind();
                ddlRequester.SelectedIndex = ddlRequester.Items.IndexOf(ddlRequester.Items.FindByValue(change.RequesterID.ToString()));
                ddlcoordinator.DataBind();
                ddlcoordinator.SelectedIndex = ddlcoordinator.Items.IndexOf(ddlcoordinator.Items.FindByValue(change.CoOrdinator.ToString()));
                casCadSubCategory.DataBind();
                casCadSubCategory.SelectedValue = string.IsNullOrEmpty(change.SubCategoryID.ToString()) ? "0" : change.SubCategoryID.ToString();
                BindArea();
                ddlarea.SelectedIndex = ddlarea.Items.IndexOf(ddlarea.Items.FindByValue(change.AreaID.ToString()));
                ddlsite_CascadingDropDown.DataBind();
                ddlsite_CascadingDropDown.SelectedValue = string.IsNullOrEmpty(change.SiteID.ToString()) ? "0" : change.SiteID.ToString();
                ddlPriority.DataBind();
                ddlPriority.SelectedIndex = ddlPriority.Items.IndexOf(ddlPriority.Items.FindByValue(change.PriorityID.ToString()));

                txtDatelogged.Text = change.DateLogged.ToString("d").Contains("1900") ? string.Empty : change.DateLogged.ToString("d");
                txtTimelogged.Text = change.DateLogged.ToString("d").Contains("1900") ? string.Empty : change.DateLogged.ToString("HH:mm");
                txtDateInhand.Text = change.InhandDate.ToString("d").Contains("1900") ? string.Empty : change.InhandDate.ToString("d");
                txtTimeInhand.Text = change.InhandDate.ToString("d").Contains("1900") ? string.Empty : change.InhandDate.ToString("HH:mm");
                txtDateclosed.Text = change.ClosedDate.ToString("d").Contains("1900") ? string.Empty : change.ClosedDate.ToString("d");
                txtTimeclosed.Text = change.ClosedDate.ToString("d").Contains("1900") ? string.Empty : change.ClosedDate.ToString("HH:mm");
               

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void BindServiceRequest()
    {
        using (DC.DAL.DCDataContext db = new DC.DAL.DCDataContext())
        {
            var result = (from c in db.CallDetails
                          join f in db.FLSDetails
                              on c.ID equals f.CallID
                          where c.CompanyID == Convert.ToInt32(ddlCustomerID.SelectedValue)
                          select new { ID = c.ID, Details = "TN:" + f.CallID + " - " + f.Details }).ToList();
            ddlRelatesToServiceRequest.DataSource = result;
            ddlRelatesToServiceRequest.DataValueField = "ID";
            ddlRelatesToServiceRequest.DataTextField = "Details";
            ddlRelatesToServiceRequest.DataBind();
            ddlRelatesToServiceRequest.Items.Insert(0, new ListItem("Please select...", "0"));
           
        }
    }
    private void SaveHelper()
    {
        //Insert or update change based on the session value changeControlID.
        if (sessionKeys.ChangeControlID == 0) InsertChange(); else UpdateChange();
    }
    private void BindCutomers()
    {
        ddlCustomerID.DataSource = Deffinity.PortfolioManager.Portfilio.Portfolio_display();
        ddlCustomerID.DataTextField = "PortFolio";
        ddlCustomerID.DataValueField = "ID";

        ddlCustomerID.DataBind();
    }
    private void InsertChange()//later change with xsd
    {
        try
        {
            Incident incident = IncidentState.IncidentSaver;
            Change change = new Change();
            change.IncidentID = 0;
            change.PortfolioID = int.Parse(ddlCustomerID.SelectedValue);
            //change.RequesterName = (string.IsNullOrEmpty(txtRequester.Text) ? string.Empty : txtRequester.Text);
            change.RequesterEmailID = (string.IsNullOrEmpty(txtRequesterEmail.Text) ? string.Empty : txtRequesterEmail.Text);
            change.Title = txtChangeControlTitle.Text.Trim();
            change.DateRaised = Convert.ToDateTime(txtDateRaised.Text.Trim());
            change.TargetReleaseDate = Convert.ToDateTime(txtTargetReleaseDate.Text.Trim());
            change.ChangeDescription = txtDescriptionOfChange.Text.Trim();
            change.Justification = txtJustification.Text.Trim();
            change.ResourceImpact = txtResourceImpact.Text.Trim();
            change.EstimatedCost = txtestimatedCost.Text.Trim();
            change.EstimatedDaysRequired = txtEstimatedDays.Text.Trim();
            change.TargetStartDate = Convert.ToDateTime(txtTargetStartDate.Text.Trim());
            change.RelatesToservicerequest = Convert.ToInt32(ddlRelatesToServiceRequest.SelectedValue);
            change.RelatesToProjectRef = Convert.ToInt32(ddlProjectRef.SelectedValue);
            change.RaisedBy = Convert.ToInt32(ddlRaisedBy.SelectedValue);
            change.Status = ddlstatus.SelectedItem.Text;
            change.CategoryID = Convert.ToInt32(ddlCategory1.SelectedValue);
            change.RequesterID = Convert.ToInt32(ddlRequester.SelectedValue);
            change.CoOrdinator = Convert.ToInt32(ddlcoordinator.SelectedValue);
            change.SubCategoryID = Convert.ToInt32(string.IsNullOrEmpty(ddlSubcategory.SelectedValue) ? "0" : ddlSubcategory.SelectedValue);
            change.AreaID = Convert.ToInt32(string.IsNullOrEmpty(ddlarea.SelectedValue) ? "0" : ddlarea.SelectedValue);
            change.PriorityID = Convert.ToInt32(string.IsNullOrEmpty(ddlPriority.SelectedValue) ? "0" : ddlPriority.SelectedValue);
            change.SiteID = Convert.ToInt32(string.IsNullOrEmpty(ddlSite.SelectedValue) ? "0" : ddlSite.SelectedValue);
            // change.Id = ChangeHelper.Insert(change);
            object objReturn = objChangeControl.InsertchangeControl(change);

            if (objReturn != null)
            {
                change.Id = Convert.ToInt32(objReturn);
                sessionKeys.ChangeControlID = change.Id;
                // FillPageControls(change);
                lblMessage.Text = "Saved Successfully!!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }
        catch (Exception exp)
        {
            LogExceptions.WriteExceptionLog(exp);
        }
    }

    private void UpdateChange()
    {
        try
        {
            Incident incident = IncidentState.IncidentSaver;
            Change change = new Change();
            change.PortfolioID = int.Parse(ddlCustomerID.SelectedValue);
            change.IncidentID = 0;
            change.Id = sessionKeys.ChangeControlID;
            change.Title = txtChangeControlTitle.Text.Trim();
            change.DateRaised = Convert.ToDateTime(txtDateRaised.Text.Trim());
            change.TargetReleaseDate = Convert.ToDateTime(txtTargetReleaseDate.Text.Trim());
            change.ChangeDescription = txtDescriptionOfChange.Text.Trim();
            change.Justification = txtJustification.Text.Trim();
            change.ResourceImpact = txtResourceImpact.Text.Trim();
            //change.RequesterName = (string.IsNullOrEmpty(txtRequester.Text) ? string.Empty : txtRequester.Text);
            change.RequesterEmailID = (string.IsNullOrEmpty(txtRequesterEmail.Text) ? string.Empty : txtRequesterEmail.Text);
            change.Title = txtChangeControlTitle.Text.Trim();
            change.DateRaised = Convert.ToDateTime(txtDateRaised.Text.Trim());
            change.TargetReleaseDate = Convert.ToDateTime(txtTargetReleaseDate.Text.Trim());
            change.ChangeDescription = txtDescriptionOfChange.Text.Trim();
            change.Justification = txtJustification.Text.Trim();
            change.ResourceImpact = txtResourceImpact.Text.Trim();
            change.EstimatedCost = txtestimatedCost.Text.Trim();
            change.EstimatedDaysRequired = txtEstimatedDays.Text.Trim();
            change.TargetStartDate = Convert.ToDateTime(txtTargetStartDate.Text.Trim());
            change.RelatesToservicerequest = Convert.ToInt32(ddlRelatesToServiceRequest.SelectedValue);
            change.RelatesToProjectRef = Convert.ToInt32(ddlProjectRef.SelectedValue);
            change.RaisedBy = Convert.ToInt32(ddlRaisedBy.SelectedValue);
            change.Status = ddlstatus.SelectedItem.Text;
            change.CategoryID = Convert.ToInt32(ddlCategory1.SelectedValue);
            change.RequesterID = Convert.ToInt32(ddlRequester.SelectedValue);
            change.CoOrdinator = Convert.ToInt32(ddlcoordinator.SelectedValue);
            change.SubCategoryID = Convert.ToInt32(string.IsNullOrEmpty(ddlSubcategory.SelectedValue) ? "0" : ddlSubcategory.SelectedValue);
            change.AreaID = Convert.ToInt32(string.IsNullOrEmpty(ddlarea.SelectedValue) ? "0" : ddlarea.SelectedValue);
            change.PriorityID = Convert.ToInt32(string.IsNullOrEmpty(ddlPriority.SelectedValue) ? "0" : ddlPriority.SelectedValue);
            change.SiteID = Convert.ToInt32(string.IsNullOrEmpty(ddlSite.SelectedValue) ? "0" : ddlSite.SelectedValue);

            //ChangeHelper.Update(change);
            object objRet = objChangeControl.UpdateControl(change);
            //FillPageControls(change);

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }

    private void Mailer()
    {
        ApprovalCollection approvers = new ApprovalCollection();
        approvers = ApprovalHelper.LoadApprovalsById(sessionKeys.ChangeControlID);
        ArrayList mailIds = GetMailIds(approvers);
        string errorString = string.Empty;
        string htmlText = string.Empty;
        HtmlHijacker HTML = new HtmlHijacker();
        htmlText = HTML.RetrieveBodyFromAnotherPage(@System.Configuration.ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/MailTemplates/ChangeControl.aspx?ChangeControlID=" + sessionKeys.ChangeControlID, ref errorString);
        Email eMail = new Email();
        eMail.SendingMail(string.Empty, "Change Control Reference - " + sessionKeys.ChangeControlID, htmlText, string.Empty, string.Empty, mailIds);
    }

    private ArrayList GetMailIds(ApprovalCollection approvers)
    {
        ArrayList mails = new ArrayList();
        AssigneeMailID(mails, approvers);
        return mails;
    }

    private void AssigneeMailID(ArrayList mails, ApprovalCollection approvers)
    {
        DataTable table = DataHelperClass.LoadContractorMailID();
        foreach (DataRow row in table.Rows)
        {
            foreach (Approval approver in approvers)
            {
                if (Convert.ToInt32(row["Id"]) == approver.ApprovalID)
                {
                    mails.Add(row["EmailAddress"]);
                    break;
                }
            }
        }
    }

    #endregion

    #region Page and Control Events

    protected void Page_PreRender(object sender, EventArgs e)
    {
       // Master.PageHead = "Change Control";
        if (string.IsNullOrEmpty(txtDateRaised.Text))
            txtDateRaised.Text = DateTime.Now.Date.ToShortDateString();
        if (sessionKeys.ChangeControlID == 0)
        {
            lblPageTitle.InnerText = "Change Control -  Log New Change Control Request";
            btnAddChangeControl.Visible = true;
            btnUpdateChangeControl.Visible = false;
        }
        else
        {
            lblPageTitle.InnerText = "Change Control - Reference " + sessionKeys.ChangeControlID.ToString();
            btnAddChangeControl.Visible = false;
            btnUpdateChangeControl.Visible = true;
        }
    }

    protected void btnAddChangeControl_Click(object sender, EventArgs e)
    {
        InsertChange();
        // rebind data
        BindDataByChangeControlID();
    }
    protected void btnSubmitChangeControl_Click(object sender, EventArgs e)
    {
        UpdateChange();
        // rebind data
        BindDataByChangeControlID();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ChangeState.ChangeSaver = null;
        ChangeState.ClearChangeCache();
        sessionKeys.ChangeControlID = 0;
        sessionKeys.PortfolioID = 0;
        Response.Redirect("ChangeControlManagement.aspx", false);
    }

    #endregion

    protected void ddlCustomerID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindArea();
            BindProjectsByCustomer();
            BindServiceRequest();
            //ddlRelatesToServiceRequest.DataBind();
            
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }
    }
   
    protected void lnkOk_Click1(object sender, EventArgs e)
    {

        try
        {
            //Convert.ToInt32(ddlCustomerID.SelectedValue);
            sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomerID.SelectedValue);
            int Portfolio = sessionKeys.PortfolioID;
            string Category = txtmastercategory.Text.Trim();
            Deffinity.PortfolioSLAmanager.PortfolioSLA.InsertMasterCategory(Category, Portfolio);
            // BindCatagory();
            txtmastercategory.Text = string.Empty;
            ddlCategory1.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    protected void imgAddSubCategory_Click(object sender, EventArgs e)
    {

        try
        {
            //Convert.ToInt32(ddlCustomerID.SelectedValue);
            sessionKeys.PortfolioID = Convert.ToInt32(ddlCustomerID.SelectedValue);
            int Portfolio = sessionKeys.PortfolioID;
            string Category = txtmastercategory.Text.Trim();
            
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {
                int cnt = pd.ProjectCategories.Where(p => p.MasterID == int.Parse(ddlCategory1.SelectedValue) && p.CategoryName.ToLower() == txtSubCategory.Text.ToLower().Trim()).Count();
                if (cnt == 0)
                {
                    ProjectCategory pc = new ProjectCategory();
                    pc.MasterID = int.Parse(ddlCategory1.SelectedValue);
                    pc.CategoryName = txtSubCategory.Text.Trim();
                    pd.ProjectCategories.InsertOnSubmit(pc);
                    pd.SubmitChanges();
                }
            }
            // BindCatagory();
            txtSubCategory.Text = string.Empty;
            casCadSubCategory.DataBind();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    
    protected void ddlRequester_SelectedIndexChanged(object sender, EventArgs e)
    {
        Change change = new Change();
        DataTable dt=  ClsChangeControl.objadaUserEmailAddress.GetData(int.Parse(ddlRequester.SelectedValue));
        txtRequesterEmail.Text = dt.Rows[0][0].ToString();
       // txtRequesterEmail.Text = change.
    }

    #region Project reference
    private void BindProjectsByCustomer()
    {
        try
        {
            ddlProjectRef.Items.Clear();
            projectTaskDataContext pd = new projectTaskDataContext();
            ddlProjectRef.DataSource = from p in pd.ProjectDetails
                                       where p.Portfolio == int.Parse(string.IsNullOrEmpty(ddlCustomerID.SelectedValue) ? "0" : ddlCustomerID.SelectedValue) 
                                       orderby p.ProjectReference
                                       select new { ProjectReference = p.ProjectReference, ProjectTitle = p.ProjectReferenceWithPrefix +"- " + p.ProjectTitle };

            ddlProjectRef.DataTextField = "ProjectTitle";
            ddlProjectRef.DataValueField = "ProjectReference";
            ddlProjectRef.DataBind();
            ddlProjectRef.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        
    }
    #endregion

    #region Area

    protected void btn_insert_area_Click(object sender, EventArgs e)
    {

        try
        {
            if (!string.IsNullOrEmpty(txtArea.Text.Trim()))
                SqlHelper.ExecuteNonQuery(Constants.DBString, CommandType.StoredProcedure, "incident_Area_Insert", new SqlParameter("@name", txtArea.Text.Trim()), new SqlParameter("@Portfolio", int.Parse(ddlCustomerID.SelectedValue)));

            txtArea.Text = string.Empty;
            BindArea();

        }
        catch (Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }

    }
    private void BindArea()
    {
        using (PortfolioDataContext pdt = new PortfolioDataContext())
        {
            ddlarea.DataSource = from area in pdt.incident_Areas
                                 where area.Portfolio == int.Parse(string.IsNullOrEmpty(ddlCustomerID.SelectedValue) ? "0" : ddlCustomerID.SelectedValue)
                                 orderby area.Name ascending
                                 select new { id = area.ID, name = area.Name };
            ddlarea.DataTextField = "name";
            ddlarea.DataValueField = "id";
            ddlarea.DataBind();

            if (ddlarea.Items.Count > 0)
                ddlarea.SelectedIndex = 0;


        }
        ddlarea.Items.Insert(0, new ListItem("Please select...", "0"));
        
    }
    #endregion
}
