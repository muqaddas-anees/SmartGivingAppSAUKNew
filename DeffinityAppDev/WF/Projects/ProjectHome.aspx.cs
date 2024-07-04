using ProjectMgt.DAL;
using ProjectMgt.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DC.DAL;
using DC.Entity;
using PortfolioMgt.DAL;
using UserMgt.DAL;
using Finance.DAL;
using Finance.Entity;
using System.Web.Script.Serialization;
using HealthCheckMgt.Entity;
using HealthCheckMgt.DAL;
//using DeffinityManager.Marketplace.BLL;

namespace DeffinityAppDev.WF.Projects
{
    public partial class ProjectHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindData();
                    ProjectsGridBind();
                    ServicesGridBind();
                    SectionOnlyForPms();
                    IssuesGridBind();
                    //VisibilityChecking();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        //public void VisibilityChecking()
        //{
        //    try
        //    {

        //        Market_BLL M_BLL = new Market_BLL();
        //        ServiceDesk_PerDayCharts.Visible = M_BLL.VisibilityChecking((int)DeffinityManager.Marketplace.BLL.Market_BLL.ModuleNames.servicedesk);
        //        ServiceDesk_Calls.Visible = ServiceDesk_PerDayCharts.Visible;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogExceptions.WriteExceptionLog(ex);
        //    }
        //}

        private void VisibilityByRole(bool isVisible)
        {
            //suppliers 
            link_suppliers.Visible = isVisible;
            //users
            link_user.Visible = isVisible;

        }
        public void SectionOnlyForPms()
        {
            if (sessionKeys.SID == 2 || sessionKeys.SID == 3)
            {
                VisibilityByRole(false);
            }
            else if(sessionKeys.SID == 1)
            {
                VisibilityByRole(true);
            }
        }
        public void IssuesGridBind()
        {
            try
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    using (UserDataContext Udc = new UserDataContext())
                    {
                        var ContractorsList = Udc.Contractors.Where(a => a.Status.ToLower() == "active").ToList();
                        var PIssuesList = Pdc.ProjectIssues.Where(a => a.AssignTo == sessionKeys.UID).ToList();
                        var IssueType = Pdc.IssueTypes.ToList();
                        var StatusList = Pdc.ItemStatus.ToList();
                        var ProjectIds = Pdc.Projects.Where(a => a.Portfolio == sessionKeys.PortfolioID).Select(a => a.ProjectReference).ToList();
                        if (sessionKeys.PortfolioID > 0)
                        {
                            PIssuesList = PIssuesList.Where(a => ProjectIds.Contains(a.Projectreference.Value)).ToList();
                        }

                        var result = (from a in PIssuesList
                                      where a.AssignTo == sessionKeys.UID
                                      orderby a.ID descending
                                      select new
                                      {
                                          IssueReference = sessionKeys.Prefix + a.Projectreference.Value.ToString() + "-" + a.ID.ToString(),
                                          Issue = a.Issue,
                                          DateRaised = string.Format("{0:dd/MM/yyyy HH:mm}", a.Datechecked),
                                          Status = StatusList.Where(c => c.ID == a.Status).Select(c => c.Status).FirstOrDefault(),
                                          Type = IssueType.Where(c => c.ID == a.IssuseType).Select(c => c.IssueTypeName).FirstOrDefault(),
                                          AssignTo = ContractorsList.Where(b => b.ID == a.AssignTo).Select(b => b.ContractorName).FirstOrDefault()
                                      }).ToList();
                       
                        GridIssues.DataSource = result;
                        GridIssues.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        public void ProjectsGridBind()
        {
            try
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    var ProjectItemsList = Pdc.ProjectItems.Where(a => a.ContractorID == sessionKeys.UID).ToList();
                    var V_ProjectsList = Pdc.ProjectDetails.Where(a => (a.ProjectStatusID == 1 || a.ProjectStatusID == 2) && ProjectItemsList.Select(o=>o.ProjectReference).ToArray().Contains(a.ProjectReference)).ToList();
                    var result = (from a in V_ProjectsList
                                  join b in ProjectItemsList on a.ProjectReference equals b.ProjectReference
                                  orderby a.ProjectReference descending
                                  where b.ContractorID == sessionKeys.UID
                                  select new
                                  {
                                      ProjectReference1 = a.ProjectReference.ToString(),
                                      ProjectReference = a.ProjectReferenceWithPrefix,
                                      ProjectTitle = a.ProjectTitle,
                                      SiteName = a.SiteName,
                                      PortfolioName = a.PortfolioName,
                                      Portfolio = a.Portfolio,
                                      StatusName = a.ProjectStatusName
                                  }).Distinct().ToList();
                    if (sessionKeys.PortfolioID > 0)
                    {
                        result = result.Where(a => a.Portfolio == sessionKeys.PortfolioID).ToList();
                    }
                    gviewclientprojectstatus.DataSource = result.ToList() ;
                    gviewclientprojectstatus.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
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
                        var fDetails = dd.FLSDetails.Select(d => d).Where(f=>f.UserID.HasValue ? f.UserID.Value == sessionKeys.UID : f.UserID == null).ToList();

                        var cList = dd.CallDetails.Where(o => o.RequestTypeID == 6 && fDetails.Select(f=>f.CallID).ToArray().Contains(o.ID)).Select(c => c).ToList();
                       
                        var requesterids = cList.Select(o => o.RequesterID.HasValue ? o.RequesterID.Value : 0).Distinct().ToArray();
                        var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToArray();
                        var requesterList = pd.PortfolioContacts.Where(o => requesterids.Contains(o.ID)).Select(s => s).ToList();
                        var sList = dd.Status.Where(p => p.RequestTypeID == 6).Select(s => s).ToList();
                        var siteList = dd.OurSites.Select(s => s).ToList();
                        var pCompany = pd.ProjectPortfolios.Where(p => companyids.Contains(p.ID)).Select(p => p).ToList();
                        var requestTypeList = dd.TypeOfRequests.ToList();

                        var result = (from c in cList
                                      join f in fDetails on c.ID equals f.CallID
                                      where f.UserID.HasValue ? f.UserID.Value == sessionKeys.UID : f.UserID == null
                                      orderby c.ID descending
                                      select new
                                      {
                                          CallID1 = f.CallID.Value.ToString(),
                                          CallID = "TN:" + f.CallID.Value.ToString(),
                                          RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.Name).FirstOrDefault(),
                                          Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                                          Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault(),
                                          Notes = f.Details,
                                          Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault(),
                                          TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault(),
                                          LoggedDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", c.LoggedDate),
                                          CompanyID = c.CompanyID
                                      }).ToList();
                        if (sessionKeys.PortfolioID > 0)
                        {
                            result = result.Where(a => a.CompanyID == sessionKeys.PortfolioID).ToList();
                            //result = (from c in cList
                            //          join f in fDetails on c.ID equals f.CallID
                            //          where f.UserID.HasValue ? f.UserID.Value == sessionKeys.UID : f.UserID == null
                            //          && c.CompanyID.Value == sessionKeys.PortfolioID
                            //          select new
                            //          {
                            //              CallID1 = f.CallID.Value.ToString(),
                            //              CallID = "TN:" + f.CallID.Value.ToString(),
                            //              RequesterName = requesterList.Where(p => p.ID == c.RequesterID).Select(p => p.Name).FirstOrDefault(),
                            //              Status = sList.Where(p => p.ID == c.StatusID).Select(p => p.Name).FirstOrDefault(),
                            //              Site = siteList.Where(p => p.ID == c.SiteID).Select(p => p.Name).FirstOrDefault(),
                            //              Notes = f.Details,
                            //              Company = pCompany.Where(a => a.ID == c.CompanyID).Select(a => a.PortFolio).FirstOrDefault(),
                            //              TypeofRequest = requestTypeList.Where(r => r.ID == f.RequestType).Select(r => r.Name).FirstOrDefault(),
                            //              LoggedDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", c.LoggedDate)
                            //          }).ToList();
                        }
                        GridServices.DataSource = result;
                        GridServices.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        private void BindData()
        {
            try
            {
                ddlPortfolio.DataBind();
                if (ddlPortfolio.Items.Count > 0)
                {
                    if (sessionKeys.PortfolioID != 0)
                    {
                        ddlPortfolio.SelectedValue = sessionKeys.PortfolioID.ToString();
                    }
                    else
                    {
                        sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
                        sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        [System.Web.Services.WebMethod]
        public static object CreatedSmartApps()
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            AppManager app_m = new AppManager();
            List<AppManager> app_mList = new List<AppManager>();
            try
            {
                using (HealthCheckDataContext Ddc = new HealthCheckDataContext())
                {
                    var x = (from a in Ddc.AppManagers
                             select new
                             {
                                 ID = a.ID,
                                 Name = a.Name,
                                 Css = a.Icon
                             }).ToList();
                    return jsonSerializer.Serialize(x).ToString();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
                return jsonSerializer.Serialize(string.Empty).ToString();
            }
        }


        [WebMethod]
        public static bool GetProject(string projectRef)
        {
            bool exists = false;
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                Project project = db.Projects.Where(p => p.ProjectReference.ToString() == projectRef).Select(p => p).FirstOrDefault();
                if (project != null)
                    exists = true;
            }
            return exists;
        }
        protected void btnSearchDocs_Click(object sender, EventArgs e)
        {

        }
        protected void btnSearchProject_Click(object sender, EventArgs e)
        {

        }
        protected void ddlPortfolio_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                sessionKeys.PortfolioID = Convert.ToInt32(ddlPortfolio.SelectedValue);
                sessionKeys.PortfolioName = ddlPortfolio.SelectedItem.Text;
                ProjectsGridBind();
                ServicesGridBind();
                IssuesGridBind();

                GridDocuments.DataBind();
                sqlFileList.DataBind();
                UpdatePanel2.Update();

                Update2.Update();
                UpdatePanelServices.Update();
                UpdatePanelIssues.Update();
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex.Message);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ModalControlExtender1.Show();
        }

        protected void gviewclientprojectstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gviewclientprojectstatus.PageIndex = e.NewPageIndex;
            ProjectsGridBind();
            Update2.Update();
        }

        protected void GridServices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridServices.PageIndex = e.NewPageIndex;
            ServicesGridBind();
            UpdatePanelServices.Update();
        }

        protected void GridIssues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridIssues.PageIndex = e.NewPageIndex;
            IssuesGridBind();
            UpdatePanelIssues.Update();
        }

        protected void gviewclientprojectstatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Url")
            {
                Response.Redirect("~/WF/Projects/ProjectOverviewV4.aspx?project=" + e.CommandArgument.ToString(),false);
            }
        }

        protected void GridServices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Url")
            {
                Response.Redirect("~/WF/DC/FLSForm.aspx?callid=" + e.CommandArgument.ToString(),false);
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

        protected void GridDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (sessionKeys.SID == 1 || sessionKeys.SID == 2 || sessionKeys.SID == 3)
            {
                GridDocuments.Columns[0].Visible = true;
            }
            else
            {
                GridDocuments.Columns[1].Visible = true;
            }
        }
    }
}