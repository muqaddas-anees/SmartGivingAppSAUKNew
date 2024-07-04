using ProjectMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.DAL;
using UserMgt.BAL;
using System.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using ProjectMgt.BAL;

namespace DeffinityAppDev.WF.Projects
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTasksData();
            }
        }
        public void GetTasksData()
        {
            string Counts = string.Empty;

            int WithInonTime = 0;
            int DelayedTasks = 0;
            int TaskDueWithIn3Days = 0;

            ProjectTaskResultValues P_Task = null;
            List<ProjectTaskResultValues> P_TaskList_WithInonTime = new List<ProjectTaskResultValues>();
            List<ProjectTaskResultValues> P_TaskList_DelayedTasks = new List<ProjectTaskResultValues>();
            List<ProjectTaskResultValues> P_TaskList_TaskDueWithIn3Days = new List<ProjectTaskResultValues>();

            List<UserMgt.Entity.Contractor> C_List = new List<UserMgt.Entity.Contractor>();
            
            try
            {
                using (UserDataContext Udc = new UserDataContext())
                {
                    C_List = Udc.Contractors.ToList();
                }

                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    
                    var ProjectsList = Pdc.Projects.Where(a => a.ProjectStatusID == 2).ToList();
                    var ProjectItemsLsit = Pdc.ProjectItems.Where(a => a.ContractorID.Value == sessionKeys.UID).ToList();

                    var V_ProjectsList = Pdc.ProjectDetails.ToList();


                    if (sessionKeys.PortfolioID > 0)
                    {
                        ProjectsList = ProjectsList.Where(a => a.Portfolio == sessionKeys.PortfolioID).ToList();
                    }
                    var ProjectsList1 = ProjectsList.Select(a => a.ProjectReference).ToArray();

                    foreach (var x in ProjectItemsLsit)
                    {
                        var ProjectTaskItemsRecord = Pdc.ProjectTaskItems.Where(a => a.ID == x.ItemReference).FirstOrDefault();
                        if (ProjectTaskItemsRecord != null)
                        {
                            if (ProjectsList1.Contains(ProjectTaskItemsRecord.ProjectReference.Value))
                            {
                                if (ProjectTaskItemsRecord.CompletionDate.Value.Date >= DateTime.Now.Date)
                                {
                                    WithInonTime = WithInonTime + 1;

                                    P_Task = new ProjectTaskResultValues();
                                    P_Task.ProjectReference = x.ProjectReference.ToString();
                                    P_Task.AssignedProjects = sessionKeys.Prefix.ToString() + x.ProjectReference.Value.ToString();
                                    P_Task.ProjectTitle = ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.ProjectTitle).FirstOrDefault();
                                    if (sessionKeys.PortfolioID > 0)
                                    {
                                        P_Task.Customer = sessionKeys.PortfolioName.ToString();
                                    }
                                    else
                                    {
                                        P_Task.Customer = V_ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.PortfolioName).FirstOrDefault();
                                    }
                                    P_Task.Site = V_ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.SiteName).FirstOrDefault(); 
                                    P_Task.TaskName = ProjectTaskItemsRecord.ItemDescription;
                                    P_TaskList_WithInonTime.Add(P_Task);

                                  
                                }
                                else if (ProjectTaskItemsRecord.CompletionDate.Value.Date.AddDays(3) >= DateTime.Now.Date)
                                {
                                    TaskDueWithIn3Days = TaskDueWithIn3Days + 1;

                                    P_Task = new ProjectTaskResultValues();
                                    P_Task.ProjectReference = x.ProjectReference.ToString();
                                    P_Task.AssignedProjects = sessionKeys.Prefix.ToString() + x.ProjectReference.Value.ToString();
                                    P_Task.ProjectTitle = ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.ProjectTitle).FirstOrDefault();
                                    if (sessionKeys.PortfolioID > 0)
                                    {
                                        P_Task.Customer = sessionKeys.PortfolioName.ToString();
                                    }
                                    else
                                    {
                                        P_Task.Customer = V_ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.PortfolioName).FirstOrDefault();
                                    }
                                    P_Task.Site = V_ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.SiteName).FirstOrDefault();
                                    P_Task.TaskName = ProjectTaskItemsRecord.ItemDescription;
                                    P_TaskList_TaskDueWithIn3Days.Add(P_Task);

                                   
                                }
                                else
                                {
                                    DelayedTasks = DelayedTasks + 1;

                                    P_Task = new ProjectTaskResultValues();
                                    P_Task.ProjectReference = x.ProjectReference.ToString();
                                    P_Task.AssignedProjects = sessionKeys.Prefix.ToString() + x.ProjectReference.Value.ToString();
                                    P_Task.ProjectTitle = ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.ProjectTitle).FirstOrDefault();
                                    if (sessionKeys.PortfolioID > 0)
                                    {
                                        P_Task.Customer = sessionKeys.PortfolioName.ToString();
                                    }
                                    else
                                    {
                                        P_Task.Customer = V_ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.PortfolioName).FirstOrDefault();
                                    }
                                    P_Task.Site = V_ProjectsList.Where(a => a.ProjectReference == x.ProjectReference.Value).Select(a => a.SiteName).FirstOrDefault();
                                    P_Task.TaskName = ProjectTaskItemsRecord.ItemDescription;
                                    P_TaskList_DelayedTasks.Add(P_Task);
                                }
                            }
                        }
                    }
                    GridP_TaskList_WithInonTime.DataSource = P_TaskList_WithInonTime;
                    GridP_TaskList_WithInonTime.DataBind();
                    GridP_TaskList_TaskDueWithIn3Days.DataSource = P_TaskList_TaskDueWithIn3Days;
                    GridP_TaskList_TaskDueWithIn3Days.DataBind();
                    GridP_TaskList_DelayedTasks.DataSource = P_TaskList_DelayedTasks;
                    GridP_TaskList_DelayedTasks.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }

        public void GetTasksDataForSingleGrid()
        {
            string Counts = string.Empty;

            int WithInonTime = 0;
            int DelayedTasks = 0;
            int TaskDueWithIn3Days = 0;

            try
            {
                using (projectTaskDataContext Pdc = new projectTaskDataContext())
                {
                    var ProjectsList = Pdc.Projects.Where(a => a.ProjectStatusID == 2).ToList();
                    var ProjectItemsLsit = Pdc.ProjectItems.Where(a => a.ContractorID.Value == sessionKeys.UID).ToList();

                    if (sessionKeys.PortfolioID > 0)
                    {
                        ProjectsList = ProjectsList.Where(a => a.Portfolio == sessionKeys.PortfolioID).ToList();
                    }
                    var ProjectsList1 = ProjectsList.Select(a => a.ProjectReference).ToArray();

                    foreach (var x in ProjectItemsLsit)
                    {
                        var ProjectTaskItemsRecord = Pdc.ProjectTaskItems.Where(a => a.ID == x.ItemReference).FirstOrDefault();
                        if (ProjectTaskItemsRecord != null)
                        {
                            if (ProjectsList1.Contains(ProjectTaskItemsRecord.ProjectReference.Value))
                            {
                                //if (ProjectTaskItemsRecord.ProjectEndDate.Value.Date >= DateTime.Now.Date)
                                if (ProjectTaskItemsRecord.CompletionDate.Value.Date >= DateTime.Now.Date)
                                {
                                    WithInonTime = WithInonTime + 1;
                                }
                                //else if (ProjectTaskItemsRecord.ProjectEndDate.Value.Date.AddDays(3) >= DateTime.Now.Date)
                                else if (ProjectTaskItemsRecord.CompletionDate.Value.Date.AddDays(3) >= DateTime.Now.Date)
                                {
                                    TaskDueWithIn3Days = TaskDueWithIn3Days + 1;
                                }
                                else
                                {
                                    DelayedTasks = DelayedTasks + 1;
                                }
                            }
                        }
                    }
                }
                if (DelayedTasks != 0 && TaskDueWithIn3Days != 0 && WithInonTime != 0)
                {
                    Counts = "-" + DelayedTasks.ToString() + ",-" + TaskDueWithIn3Days.ToString() + "," + WithInonTime.ToString();
                }
                else
                {
                    if (DelayedTasks != 0)
                    {
                        Counts = "-" + DelayedTasks.ToString();
                    }
                    else
                    {
                        Counts = DelayedTasks.ToString();
                    }
                    if (TaskDueWithIn3Days != 0)
                    {
                        Counts = Counts + ",-" + TaskDueWithIn3Days.ToString();
                    }
                    else
                    {
                        Counts = Counts + "," + TaskDueWithIn3Days.ToString();
                    }
                    if (WithInonTime != 0)
                    {
                        Counts = Counts + "," + WithInonTime.ToString();
                    }
                    else
                    {
                        Counts = Counts + "," + WithInonTime.ToString();
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
public class ProjectTaskResultValues
{
    public string ProjectReference { get; set; }
    public string AssignedProjects { get; set; }
    public string ProjectTitle { get; set; }
    public string Customer { get; set; }
    public string TaskName { get; set; }
    public string Site { get; set; }
}