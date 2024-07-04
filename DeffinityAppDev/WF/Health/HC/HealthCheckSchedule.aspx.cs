using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Health.Entity;
using Health.DAL;
using Health.StateManager;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Deffinity.EmailService;
using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Mail;

public partial class HC_HealthCheckSchedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //new page (forms) is 0
            sessionKeys.pageid = 0;
            //Master.PageHead = "Health Check Schedule";
            HealthCheckListState.ClearHealthCheckItemsCache();
            ResetPageControls();
        }
        Cache.Remove("MailList");

        HideCustomerColumn();
    }

    void ResetPageControls()
    {
        ddlHealthCheckTitle.SelectedIndex = 0;
        txtDate.Text = DateTime.Now.Date.ToString("d");
        txtTime.Text = string.Format("{0:t hh:mm}", DateTime.Now.ToString("t")); ;
        ddlLocation.SelectedIndex = 0;
        ddlPortfolio.DataBind();
        ddlPortfolio.SelectedIndex = ddlPortfolio.Items.IndexOf(ddlPortfolio.Items.FindByValue(sessionKeys.PortfolioID.ToString()));
       
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if(sessionKeys.SID ==1)
        {
            HealthCheckCtrl1.Visible = true;

        }
        //if (Request.QueryString["R"] != null)
        //{
        //    //lblPortfolio.Visible = true;
        //    //ddlPortfolio.Visible = true;
        //    tabControl.Visible = false;
        //    HealthCheckCtrl1.Visible = true;
        //    //ProjectStatus.v
        //}
        //else
        //{
        //    //lblPortfolio.Visible = false;
        //    //ddlPortfolio.Visible = false;
        //    tabControl.Visible = false;
        //    HealthCheckCtrl1.Visible = true;
        //}

        bool setVal = true;
        if (Request.QueryString["type"] != null)
        {
            if (Request.QueryString["type"] == "resource")
            {
                tabControl.Visible = setVal;
                //HealthCheckCtrl1.Visible = !setVal;
                pnlSearch.Visible = setVal;
                RFIVendorTabs1.Visible = !setVal;
            }
            else  if (Request.QueryString["type"] == "vendor")
            {
               
                RFIVendorTabs1.Visible = setVal;
                tabControl.Visible = !setVal;
                pnlSearch.Visible = !setVal;
                ImageButton1.Visible = !setVal;
            }
            else
            {
                tabControl.Visible = !setVal;
                //HealthCheckCtrl1.Visible = setVal;
                pnlSearch.Visible = setVal;
                RFIVendorTabs1.Visible = !setVal;
            }
        }
        else
        {
            RFIVendorTabs1.Visible = !setVal;
            tabControl.Visible = !setVal;
            //HealthCheckCtrl1.Visible = setVal;
            pnlSearch.Visible = setVal;
        }

        //HealthCheckCtrl1.Visible = false;
            
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            HealthCheckList healthCheckList = new HealthCheckList();
            healthCheckList.AssignedTeam = 0;
            healthCheckList.DateRaised = Convert.ToDateTime(txtDate.Text + " " + txtTime.Text);
            healthCheckList.HealthCheckListID = Convert.ToInt32(ddlHealthCheckTitle.SelectedValue);
            healthCheckList.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            healthCheckList.Id = HealthCheckListHelper.Insert(healthCheckList);
            healthCheckList.Status = "Pending";
            UpdateNotCompletedHCLItems(healthCheckList);
            lblMessage.Text = "Inserted Successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            gridHealthChecks.DataBind();
            ResetPageControls();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
            lblMessage.Text = "Failed to add. Please try again.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void UpdateNotCompletedHCLItems(HealthCheckList healthCheckList)
    {
        string sqlUnCompletedHealthCheckID = "SELECT TOP 1 * FROM HealthCheckList WHERE Location=" + healthCheckList.LocationID + " AND STATUS IN('In Progress','Critical') AND PortfolioHealthCheckID=" + healthCheckList.HealthCheckListID + " ORDER BY DateRaised DESC;";
        DataTable table = new DataTable();
        DataHelperClass.DDLHelper(table, sqlUnCompletedHealthCheckID);
        if (table.Rows.Count > 0)
        {
            HealthCheckListItemsCollection newHealthCheckListItems = HealthCheckListItemsHelper.LoadAllHealthCheckListItems(healthCheckList.Id);
            HealthCheckListItemsCollection oldHealthCheckListItems = HealthCheckListItemsHelper.LoadAllHealthCheckListItems(Convert.ToInt32(table.Rows[0]["ID"]));
            updateNewHealthCheckListItems(newHealthCheckListItems, oldHealthCheckListItems);
        }
    }
    private void updateNewHealthCheckListItems(HealthCheckListItemsCollection newHealthCheckListItems, HealthCheckListItemsCollection oldHealthCheckListItems)
    {
        HealthCheckListItems updatableHealthCheckListItem = new HealthCheckListItems();

        foreach (HealthCheckListItems oldHealthCheckListItem in oldHealthCheckListItems)
        {
            foreach (HealthCheckListItems newHealthCheckListItem in newHealthCheckListItems)
            {
                if (oldHealthCheckListItem.HealthCheck == newHealthCheckListItem.HealthCheck &&(oldHealthCheckListItem.Status.Equals("Pending") || oldHealthCheckListItem.Status.Equals("In Progress") ||oldHealthCheckListItem.Status.Equals("Critical")))
                {
                    updatableHealthCheckListItem = oldHealthCheckListItem;
                    updatableHealthCheckListItem.IsChecked = SqlBoolean.Null;
                    updatableHealthCheckListItem.Id = newHealthCheckListItem.Id;
                    updatableHealthCheckListItem.HealthCheckListID = newHealthCheckListItem.HealthCheckListID;
                    HealthCheckListItemsHelper.Update(updatableHealthCheckListItem);
                }
            }
        }
    }
   
    protected void gridHealthChecks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(((Label)gridHealthChecks.Rows[e.RowIndex].FindControl("lblID")).Text);
        foreach (HealthCheckList healthCheckList in HealthCheckListHelper.LoadAllHealthCheckLists())
        {
            if (id == healthCheckList.Id)
            {
                HealthCheckListHelper.Delete(healthCheckList);
                ResetPageControls();
                break;
            }
        }
    }

    protected void gridHealthChecks_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            AddGlyph(gridHealthChecks, e.Row);
    }

    protected void AddGlyph(GridView grid, GridViewRow item)
    {
        Image glyph = new Image();
        glyph.ImageUrl = (grid.SortDirection == SortDirection.Ascending ? @"~\images\ArrowUp.gif" : @"~\images\ArrowDown.gif");
        glyph.ImageAlign = ImageAlign.AbsMiddle;
        glyph.Height = 4;
        // Find the column that is sorted by
        for (int i = 0; i < grid.Columns.Count; i++)
        {
            string colExpr = grid.Columns[i].SortExpression;
            if (colExpr != "" && colExpr == grid.SortExpression)
                item.Cells[i].Controls.Add(glyph);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //updateEmailAddress(-1);
    }

    private void updateEmailAddress(int rowID,GridView innerGrid)
    {
        //SaveEmailIDsToDB(rowID,innerGrid);
      //  gridHealthChecks.DataBind();
    }

    //private void SaveEmailIDsToDB(int i,GridView innerGrid)
    //{
    //    GridViewRow row = gridHealthChecks.Rows[i];
    //    string checkListID = ((Label)row.FindControl("lblID")).Text;
        
    //    for (int j = 0; j < innerGrid.Rows.Count; j++)
    //    {
    //        GridViewRow innerRow = innerGrid.Rows[j];
    //        bool isChecked = ((CheckBox)innerRow.FindControl("chkMailable")).Checked;
    //        string mailID = ((Literal)innerRow.FindControl("litMailID")).Text;

    //        using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("HealthCheck_DistributionList", conn))
    //            {
    //                cmd.Parameters.AddWithValue("@MailID", mailID);
    //                cmd.Parameters.AddWithValue("@checkListID", checkListID);
    //                cmd.Parameters.AddWithValue("@Associate", isChecked);
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                conn.Open();
    //                cmd.ExecuteNonQuery();
    //                cmd.Dispose();
    //                conn.Dispose();
    //            }
    //        }
    //    }
    //}

    protected void ddlPortfolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
        sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
        HealthCheckListState.ClearHealthCheckItemsCache();
        ddlHealthCheckTitle.Items.Clear();
        ListItem item = new ListItem("Please Select..", "0");
        ddlHealthCheckTitle.Items.Add(item);
        ddlHealthCheckTitle.DataBind();
        //clear before combine
        //if (ddlLocation.Items.Count > 1)
        //{
        //    ddlLocation.Items.Clear();
        //}
        
        //ddlLocation.DataBind();

        gridHealthChecks.DataBind();
        
    }

    //protected void Page_Error(object sender, EventArgs e)
    //{
    //    Exception ex = Server.GetLastError();
    //    LogExceptions.WriteExceptionLog(ex);
    //    Email eMail = new Email();
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<br/>Message: " + ex.Message);
    //    sb.Append("<br/>Source: " + ex.Source);
    //    sb.Append("<br/>TargetSite: " + ex.TargetSite);
    //    sb.Append("<br/>Data: " + ex.Data);
    //    sb.Append("<br/>Inner Exception: " + ex.InnerException);
    //    sb.Append("<br/>Stack Trace:<br/><hr/>" + ex.StackTrace);
    //    ArrayList mailIds = new ArrayList();
    //    mailIds.Add("chandra.sekhar@emsysindia.com");
    //    mailIds.Add("goverdhan@emsysindia.com");
    //    mailIds.Add("indra@emsysindia.com");
    //    eMail.SendingMail(string.Empty, "Deffinity Error", sb.ToString(), string.Empty, string.Empty, mailIds);
    //    Response.Redirect("~/Message.aspx?aspxerrorpath=/HealthCheckSchedule.aspx");
    //}

    void SendMail(int portfolioHealthCheckID,int healthCheckID,int selectedRow,ArrayList mailIDs)
    {
        Email eMail = new Email();
        //string sqlStatement = "SELECT Distinct HCN.* FROM HealthCheckNameMailID" +
        //                " HCN INNER JOIN HealthCheckList_HealthCheckNameMailID HCL_HCN" +
        //                " ON HCN.ID=HCL_HCN.MailID WHERE HCN.PortfolioHealthCheckID=" + portfolioHealthCheckID;
        //DataTable teamMembers = DataHelperClass.GenericSelectMethodHelp(sqlStatement);
        GridView innerGrid = (GridView)(gridHealthChecks.Rows[selectedRow].FindControl("gridInner"));
        
        Literal litHealthCheckTitle = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litHealthCheckTitle"));
        Literal litHealthCheckDate = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litDateRaised"));
        Literal litLocation = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litLocation"));
        //foreach (DataRow row in teamMembers.Rows)
        //    mailIDs.Add(row["EmailID"].ToString());
        if (mailIDs.Count > 0)
        {
            HtmlHijacker htmlHiJacker = new HtmlHijacker();
            string errorString = string.Empty;
            string mailTitle = litHealthCheckTitle.Text + " - " + litLocation.Text + " - " + litHealthCheckDate.Text;
            string htmlTest = htmlHiJacker.RetrieveBodyFromAnotherPage(@ConfigurationManager.AppSettings["LocalUrl"].ToString() + "/MailTemplates/HealthCheckDetails.aspx?healthcheckid=" + healthCheckID + "&PortfolioID=" + sessionKeys.PortfolioID, ref errorString);
            eMail.SendingMail(string.Empty, mailTitle, htmlTest, string.Empty, string.Empty, mailIDs);
            lblErrorMessage.Text = string.Empty;
        }
        else
        {
            lblErrorMessage.Text = "Please select atleast one email address to mail.";
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridHealthChecks_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int selectedRow = e.NewSelectedIndex;
        int portfolioHealthCheckID = Convert.ToInt32(((Label)gridHealthChecks.Rows[selectedRow].FindControl("lblHealthCheckID")).Text);
        int healthCheckID = Convert.ToInt32(((Label)gridHealthChecks.Rows[selectedRow].FindControl("lblID")).Text);
        string assignedTeam = ((Label)gridHealthChecks.Rows[selectedRow].FindControl("lblAssignedTeam")).Text;
        GridView innerGrid = (GridView)(gridHealthChecks.Rows[selectedRow].FindControl("gridInner"));

        string status=((Label)gridHealthChecks.Rows[selectedRow].FindControl("lblStatus")).Text;
        updateEmailAddress(selectedRow,innerGrid);
        if (string.IsNullOrEmpty(assignedTeam) || string.IsNullOrEmpty(status))
        {
            lblErrorMessage.Text = "Email cannot be sent. Either team is not assigned or no status for the health check.";
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            ArrayList mailIDs = new ArrayList();
            foreach (GridViewRow row in innerGrid.Rows)
            {
                bool isMailable = Convert.ToBoolean(((CheckBox)row.FindControl("chkMailable")).Checked);
                string testMailID = ((Label)row.FindControl("lblEmailID")).Text;
                if (isMailable)
                {
                    string emailID = ((Label)row.FindControl("lblEmailID")).Text;
                    string name = ((Label)row.FindControl("lblName")).Text;
                    
                    mailIDs.Add(new ListItem(name , emailID));
                }
            }
            SendMailtoUsers(portfolioHealthCheckID, healthCheckID, selectedRow, mailIDs);
            //SendMail(portfolioHealthCheckID, healthCheckID, selectedRow, mailIDs);
            lblErrorMessage.Text = "Mail sent successfully";
            lblErrorMessage.ForeColor = System.Drawing.Color.Green;
        }
    }
    public string GetRAGStatus(object value)
    {
        string RAGStatus = "";
        try
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "select...")
                {
                    RAGStatus = string.Empty;
                }
                else
                {
                    RAGStatus = value.ToString();
                }
            }
            else
            {
                return RAGStatus;
            }

        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return RAGStatus;
    }
    protected void ddlLocation_DataBound(object sender, EventArgs e)
    {
        if (ddlLocation.Items.FindByValue("0") == null)
        {
            ddlLocation.Items.Insert(0, new ListItem("Please Select...", "0"));
        }
    }
    protected void gridHealthChecks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink HyperLink_resource = e.Row.FindControl("HyperLink_resource") as HyperLink;
            HyperLink HyperLink_main = e.Row.FindControl("HyperLink_main") as HyperLink;
            HyperLink HyperLink_health = e.Row.FindControl("HyperLink_health") as HyperLink;
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "resource")
                {
                    HyperLink_main.Visible = false;
                    HyperLink_health.Visible = false;
                }
                else if (Request.QueryString["type"] == "vendor")
                {
                    HyperLink_main.Visible = false;
                    HyperLink_resource.Visible = false;
                }
                else
                {
                    HyperLink_resource.Visible = false;
                    HyperLink_health.Visible = false;
                }
            }
            else
            {
                HyperLink_resource.Visible = false;
                HyperLink_health.Visible = false;
            }
        }
    }

    private void HideCustomerColumn()
    {
        if (Request.QueryString["type"] != null)
        {
            if (Request.QueryString["type"] == "main")
            {
                if (gridHealthChecks.Columns.Count > 0)
                {
                    gridHealthChecks.Columns[8].Visible = false;
                }
            }
        }
        else 
        
        {
            if (gridHealthChecks.Columns.Count > 0)
            {
                gridHealthChecks.Columns[8].Visible = false;
            }
        }
    }

    public void SendMailtoUsers(int portfolioHealthCheckID, int healthCheckID, int selectedRow, ArrayList mailIDs)
    {

        try
        {
            GridView innerGrid = (GridView)(gridHealthChecks.Rows[selectedRow].FindControl("gridInner"));

           

            Literal litHealthCheckTitle = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litHealthCheckTitle"));
            Literal litHealthCheckDate = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litDateRaised"));
            Literal litLocation = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litLocation"));

            Label lblPortfolioName = (Label)(gridHealthChecks.Rows[selectedRow].FindControl("lblPortfolioName"));
            Label lblAssignedTeam = (Label)(gridHealthChecks.Rows[selectedRow].FindControl("lblAssignedTeam"));
            Label lblStatus = (Label)(gridHealthChecks.Rows[selectedRow].FindControl("lblStatus"));
            Literal litID = (Literal)(gridHealthChecks.Rows[selectedRow].FindControl("litMainID"));
            //litID
            string mailTitle = litHealthCheckTitle.Text + " - " + litLocation.Text + " - " + litHealthCheckDate.Text;

            string fromemailid = Deffinity.systemdefaults.GetFromEmail();
            DC.SRV.WebService ws = new DC.SRV.WebService();

            Emailer em = new Emailer();
            Email new_em = new Email();
            string body = em.ReadFile("~/WF/Health/HC/EmailTemplates/healthcheckdetails.html");
            var url = Deffinity.systemdefaults.GetWebUrl();
            body = body.Replace("[logo]", url + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
            body = body.Replace("[border]", url + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
            body = body.Replace("[healthcheck]", litHealthCheckTitle.Text);
            body = body.Replace("[dateraised]", litHealthCheckDate.Text);
            body = body.Replace("[location]", litLocation.Text);
            body = body.Replace("[team]", lblAssignedTeam.Text);
            body = body.Replace("[status]", lblStatus.Text);
            //string link = string.Format("<a href='{0}'>Click Here</a>", url + "/HC/FormDataPreview.aspx?hcid=" + litID.Text);
            //body = body.Replace("[view]", link);



            using (System.Net.Mail.Attachment attachFile = new System.Net.Mail.Attachment(Server.MapPath(string.Format("~/WF/UploadData/HC/HC{0}", healthCheckID.ToString() + ".pdf"))))
            {
                foreach (ListItem t in mailIDs)
                {
                    string bodywithUser = body;
                    bodywithUser = bodywithUser.Replace("[user]", t.Text);
                    //em.SendingMail(fromemailid, mailTitle, bodywithUser, t.Value);
                    attachFile.Name = DateTime.Now.ToShortDateString() + " " + litHealthCheckTitle.Text + " " + attachFile.Name;
                    new_em.SendingMail(t.Value, mailTitle, bodywithUser, fromemailid, attachFile);
                }
            }

            //body = body.Replace("[issuestatus]", ddlStatus.SelectedItem.Text);
            //body = body.Replace("[notes]", ddlStatus.SelectedItem.Text);
        }
        catch(Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}
