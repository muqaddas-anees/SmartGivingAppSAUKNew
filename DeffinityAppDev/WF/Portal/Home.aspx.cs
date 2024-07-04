using DC.BLL;
using DC.DAL;
using Microsoft.ApplicationBlocks.Data;
using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeffinityAppDev.WF.Portal
{
    public partial class Home : System.Web.UI.Page
    {
        string customer_sql = string.Empty;
        string User_sql = string.Empty;
        string Portfolio_sql = string.Empty;
        string Programme_sql = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int contactid = 0;
                if (Session["contactid"] == null)
                    contactid = CustomerDetailsBAL.GetCustomerUser_ContactID(sessionKeys.UID);
                else
                    contactid = Convert.ToInt32(Session["contactid"].ToString());
                hcid.Value = contactid.ToString();
                BindIssues();
                BindAssignedTask();
                ServicesGridBind();


                if (contactid >0)
                {
                    btnAddNewAddress.NavigateUrl = "~/WF/Portal/ContactAddressDetails.aspx?ContactID=" + contactid;
                }
            }
        }


        public void ServicesGridBind()
        {
            try
            {
                using (DCDataContext dd = new DCDataContext())
                {
                    using (PortfolioDataContext pd = new PortfolioDataContext())
                    {
                        var pAssociates = pd.PortfolioContactAssociates.Where(o => o.CustomerUserID == sessionKeys.UID).FirstOrDefault();
                        var contactid = pAssociates.ContactID;
                        //Get associate ids
                        var jqlist = FLSDetailsBAL.Jqgridlist().Where(o=>o.RequesterID == contactid).ToList();
                       // var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && o.CompanyID == sessionKeys.PortfolioID && o.RequesterID == pAssociates.ContactID).Select(c => c).ToList();
                       // var fDetails = dd.FLSDetails.Select(d => d).Where(d=> cList.Select(o=>o.ID).ToArray().Contains(d.CallID.HasValue?d.CallID.Value:0)).ToList();
                       // var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();
                       // var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                       // var requesterList = pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                       // var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                       // var siteList = dd.OurSites.Select(s => s).ToList();
                       //var pCompany = pd.ProjectPortfolios.Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                       // var requestTypeList = dd.TypeOfRequests.ToList();

                        var result = (from f in jqlist
                                      //join f in fDetails on c.ID equals f.CallID
                                      //where c.CompanyID == sessionKeys.PortfolioID
                                      where f.Status != "Cancelled" && f.Status != "Job Complete"
                                      // where f.UserID.HasValue ? f.UserID.Value == sessionKeys.UID : f.UserID == null
                                      select new
                                      {
                                          CCID = "" +f.CCID.ToString(),
                                          ID = f.CallID,
                                          CallID1 = f.CallID.ToString(),
                                          CallID = "" + f.CallID.ToString(),
                                          RequesterName = f.RequesterName,// requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.Name).FirstOrDefault(),
                                          Status =f.Status,// sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                                          Site = f.Site,// siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault(),
                                          Notes = f.Details,// f.Details,
                                          Company = f.Company,// pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault(),
                                          TypeofRequest = f.TypeofRequest,// requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault(),
                                          LoggedDateTime = f.LoggedDateTime,// string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), c.LoggedDate),
                                          CompanyID = f.Company,// c.CompanyID,
                                          ServiceProvider = f.AssignedTechnician,
                                          Category = f.Category
                                      }).ToList();
                        //if (sessionKeys.PortfolioID > 0)
                        //{
                        //    result = result.Where(a => a.CompanyID == sessionKeys.PortfolioID).ToList();
                        //}
                        
                        GridServices.DataSource = result.OrderByDescending(o=>o.ID).ToList();
                        GridServices.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        protected void GridServices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridServices.PageIndex = e.NewPageIndex;
            ServicesGridBind();
            //UpdatePanelServices.Update();
        }
        protected void GridServices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Url")
            {

                var pe = FLSDetailsBAL.GetCallIDByCustomer(Convert.ToInt32(e.CommandArgument.ToString()));
                Response.Redirect("~/WF/DC/DCCustomerNavigation.ashx?CCID=" + pe.ToString() + "&callid=" + e.CommandArgument.ToString()+"&type=FLS");
            }
            else if(e.CommandName == "CancelRequest")
            {
                //33 Cancelled
                //FLSDetailsBAL.UpdateTicketStatus(Convert.ToInt32(e.CommandArgument.ToString()), sessionKeys.UID, 33);
                FLSDetailsBAL.TicketCancelled(Convert.ToInt32(e.CommandArgument.ToString()));
                MailtoServiceProvider(e.CommandArgument.ToString());
                lblMsg.Text = "Request has been cancelled";
                ServicesGridBind();
            }
            else if (e.CommandName == "Reschedule")
            {
                var pe = FLSDetailsBAL.GetCallIDByCustomer(Convert.ToInt32(e.CommandArgument.ToString()));
                //22 New
                FLSDetailsBAL.TicketReschedule(Convert.ToInt32(e.CommandArgument.ToString()));
                //FLSDetailsBAL.UpdateTicketStatus(Convert.ToInt32(e.CommandArgument.ToString()), sessionKeys.UID, 22);
                //lblMsg.Text = "Request has been cancelld";
                Response.Redirect("~/WF/DC/DCCustomerNavigation.ashx?CCID="+ pe.ToString() + "&callid=" + e.CommandArgument.ToString() + "&type=FLS");
            }
        }

        private void BindAssignedTask()
        {
            try
            {
                string str = " SELECT V_ProjectDetails.ProjectReferenceWithPrefix,pti.ID,pti.ItemStatus as StatusID , V_ProjectDetails.ProjectReference, ItemDescription AS TaskTitle, ProjectStartDate, " +
                            " pti.ProjectEndDate, ItemStatus.Status as ItemStatus, pti.Notes FROM ProjectTaskItems pti INNER JOIN" +
                            " V_ProjectDetails ON pti.ProjectReference = V_ProjectDetails.ProjectReference INNER JOIN ItemStatus ON " +
                            " pti.ItemStatus = ItemStatus.ID  where isnull(ViewCustomer,0) = 1 ";
                              //and
                            //pti.ProjectReference in" +
                            //" (select ProjectReference from Customer_ProjectPermissions where UserId = " + sessionKeys.UID + " )";

                if (sessionKeys.SID == 7)
                    str = str + getCustomersql() + getLiveProjectsql();
                if (sessionKeys.UID > 0 && sessionKeys.SID != 7)
                    str = str + getCustomersql() + getUsersql() + getPortfoliosql() + getProgrammesql() + getLiveProjectsql();

                str = str + " order by TaskTitle";
                DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str).Tables[0];
               // grdAssignedTask.DataSource = dt;
                //grdAssignedTask.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }

        }
        private string getCustomersql()
        {
            customer_sql = string.Format(" AND V_ProjectDetails.ProjectReference in (select ProjectReference from Customer_ProjectPermissions where UserId = {0}) ", sessionKeys.UID.ToString());
            return customer_sql;
        }
        private string getUsersql()
        {
            User_sql = string.Format(" AND V_ProjectDetails.ProjectReference in (select ProjectReference from ProjectItems where ContractorID = {0}) ", sessionKeys.UID.ToString());

            return User_sql;
        }
        private string getPortfoliosql()
        {
            Portfolio_sql = sessionKeys.PortfolioID > 0 ? string.Format(" And V_ProjectDetails.Portfolio = {0} ", sessionKeys.PortfolioID) : " And V_ProjectDetails.Portfolio >0 ";
            return Portfolio_sql;
        }
        private string getProgrammesql()
        {
            //if (ddlProgramme.SelectedValue == "0")
            Programme_sql = "";
            // else
            //   Programme_sql = string.Format(" And V_ProjectDetails.OwnerGroupID = {0}", ddlProgramme.SelectedValue);

            return Programme_sql;
        }
        private string getLiveProjectsql()
        {
            return string.Format(" And lower(V_ProjectDetails.ProjectStatusName)='live'"); ;
            //return string.Empty;
        }
        private DataTable getProgrammeList()
        {
            string customer = sessionKeys.PortfolioID > 0 ? "= " + sessionKeys.PortfolioID.ToString() : ">0";
            string str = "select id,OperationsOwners as Programme from OperationsOwners where Level = 1  and PortfolioID" + customer + " order by OperationsOwners";
            return SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str).Tables[0];
        }
        private void BindIssues()
        {
            try
            {
                string str = " SELECT ProjectIssues.ID IssueID, V_ProjectDetails.ProjectReferenceWithPrefix ,V_ProjectDetails.ProjectReference, V_ProjectDetails.ProjectTitle," +
                              " V_ProjectDetails.ProjectStatusName,ProjectIssues.Issue FROM  ProjectIssues INNER JOIN " +
                             " V_ProjectDetails ON ProjectIssues.Projectreference = V_ProjectDetails.ProjectReference" +
                             " WHERE  isnull(ViewCustomer,0) = 1 and  (LOWER(ProjectIssues.IssueSection) = 'project')   ";

                if (sessionKeys.SID == 7)
                    str = str + getCustomersql() + getLiveProjectsql();
                if (sessionKeys.UID > 0 && sessionKeys.SID != 7)
                    str = str + getUsersql() + getPortfoliosql() + getProgrammeList() + getLiveProjectsql();

                DataTable dt = SqlHelper.ExecuteDataset(Constants.DBString, CommandType.Text, str.Replace("Table", "")).Tables[0];
                //grdIssues.DataSource = dt;
                //grdIssues.DataBind();
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        #region Property
        private string section = string.Empty;
        public string SetSection
        {
            get { return section; }
            set { section = value; }
        }
        #endregion
        protected void grdAssignedTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try {
                if (e.CommandName == "View")
                {
                    if (SetSection == "customer")
                        Response.Redirect("CustomerProjectOverview.aspx?Project=" + e.CommandArgument.ToString());
                    //else
                    //    Response.Redirect("ProjectOverviewV2.aspx?Project=" + e.CommandArgument.ToString());
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

       

        protected void GridDocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                //AC2P_DocumentsController _AC2P_DocumentsController = new AC2P_DocumentsController();
                //_AC2P_DocumentsController.DocumentJournalInsert(Convert.ToInt32(e.CommandArgument.ToString()), sessionKeys.PortfolioID, sessionKeys.UID);
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    string sqlCommand = string.Format("SELECT ContentType,Document,DocumentName FROM AC2P_Documents WHERE ID={0}", e.CommandArgument);
                    using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HttpContext.Current.Response.ContentType = reader.GetString(0);
                                byte[] getContent = (byte[])reader[1];
                                HttpContext.Current.Response.BinaryWrite(getContent);
                                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;FileName=" + reader.GetString(2).Replace(" ", string.Empty));
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                }
            }
        }

       

        protected void grdIssues_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdIssues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdIssues.PageIndex = e.NewPageIndex;
            BindIssues();
        }

        protected void grdAssignedTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdAssignedTask.PageIndex = e.NewPageIndex;
            BindAssignedTask();
        }

        public void MailtoServiceProvider(string ticketno)
        {
            try
            {
                var flsdata = FLSDetailsBAL.Jqgridlist(Convert.ToInt32(ticketno)).FirstOrDefault();
                if (!string.IsNullOrEmpty(flsdata.AssignedTechnicianEmail))
                {
                    string fromemailid = Deffinity.systemdefaults.GetFromEmail();

                    Emailer em = new Emailer();
                    string body = em.ReadFile("~/WF/DC/EmailTemplates/DefaultTemplate.htm");
                    string subject = string.Empty;
                    subject = "Ticket Ref:" + flsdata.CallID.ToString() + " has been cancelled";
                    body = body.Replace("[mail_head]", "Service Desk Request");
                    body = body.Replace("[mail_body]", "Ticket Ref:" + flsdata.CallID.ToString() + " has been cancelled by Requester.");
                    body = body.Replace("[logo]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["maillogo"]);
                    body = body.Replace("[border]", Deffinity.systemdefaults.GetWebUrl() + System.Configuration.ConfigurationManager.AppSettings["mailboarder"]);
                    body = body.Replace("[user]", flsdata.AssignedTechnician);
                    body = body.Replace("[instancename]", Deffinity.systemdefaults.GetInstanceTitle());

                    em.SendingMail(fromemailid, subject, body, flsdata.AssignedTechnicianEmail);
                }

            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        protected void GridServices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.FindControl("lblPname5") != null)
                    {
                        var stext = ((e.Row.FindControl("lblPname5")) as Label).Text;

                        if(stext == "New" || stext == "Scheduled" || stext == "Awaiting Schedule"|| stext == "Customer Not Responding")
                        {
                            (e.Row.FindControl("btnCancel") as Button).Visible = true;
                            (e.Row.FindControl("btnReschedule") as Button).Visible = true;
                        }
                        else
                        {
                            (e.Row.FindControl("btnCancel") as Button).Visible = false;
                            (e.Row.FindControl("btnReschedule") as Button).Visible = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
}