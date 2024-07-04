using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Web.Script.Serialization;

namespace ProjectMgt.BAL
{
    
    public class ProjectBAL
    {
        IProjectRepository<ProjectMgt.Entity.ProjectDetails> pdRepository = null;
        IProjectRepository<ProjectMgt.Entity.ProjectTaskItem> ptRepository = null;
        IProjectRepository<ProjectMgt.Entity.ProjectItem> piRepository = null;
        IProjectRepository<ProjectMgt.Entity.V_ProjectTaskDetail> ptvRepository = null;
        IProjectRepository<ProjectMgt.Entity.ProjectIssue> psRepository = null;
        IProjectRepository<ProjectMgt.Entity.Projects_DashboardBasicSummaryResult> pdBasic = null;
        //IProjectRepository<ProjectMgt.Entity.Projects_DashboardFinanceChartResult> pdFinance = null;
        IProjectRepository<ProjectMgt.Entity.Projects_DashboardSummaryResult> pdGeneral = null;
        public ProjectBAL()
        {
            pdRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();
            ptRepository = new ProjectRepository<ProjectMgt.Entity.ProjectTaskItem>();
            piRepository = new ProjectRepository<ProjectMgt.Entity.ProjectItem>();
            ptvRepository = new ProjectRepository<ProjectMgt.Entity.V_ProjectTaskDetail>();
            psRepository = new ProjectRepository<ProjectMgt.Entity.ProjectIssue>();
            pdBasic = new ProjectRepository<ProjectMgt.Entity.Projects_DashboardBasicSummaryResult>();
            //pdFinance = new ProjectRepository<ProjectMgt.Entity.Projects_DashboardFinanceChartResult>();
            pdGeneral = new ProjectRepository<ProjectMgt.Entity.Projects_DashboardSummaryResult>();
        }

        #region Project details
        //Select all projects
        public IQueryable<ProjectMgt.Entity.ProjectDetails> Project_SelectAll()
        {
            return pdRepository.GetAll();
        }
        //Select Projects by Owner
        public IQueryable<ProjectMgt.Entity.ProjectDetails> Project_SelectByOwner(int OwnerID)
        {
            return pdRepository.GetAll().Where(o => o.OwnerID == OwnerID);
        }
        
        //Select project by status
        public IQueryable<ProjectMgt.Entity.ProjectDetails> Project_SelectByStatus(int StatusID)
        {
            return Project_SelectAll().Where(o => o.ProjectStatusID == StatusID);
        }

        //Select project by customer
        public IQueryable<ProjectMgt.Entity.ProjectDetails> Project_SelectByCustomer(int CustomerID)
        {
            return Project_SelectAll().Where(o => o.Portfolio == CustomerID);
        }
        //Select projects by assigned user / resource
        public IQueryable<ProjectMgt.Entity.ProjectDetails> Project_SelectByAssignedUser(int UserID)
        {
            //get list of projects
            var pArray = piRepository.GetAll().Where(o => o.ContractorID == UserID).Select(o => o.ProjectReference).ToArray();

            return Project_SelectAll().Where(o => pArray.Contains(o.ProjectReference));
        }
        public IQueryable<ProjectMgt.Entity.ProjectItem> ProjectItems_SelectAll()
        {
            return piRepository.GetAll();
        }
        public IQueryable<ProjectMgt.Entity.ProjectItem> ProjectItems_SelectByProjectreference(int projectReference)
        {
            return piRepository.GetAll().Where(o=>(o.ProjectReference.HasValue?o.ProjectReference.Value:0) == projectReference);
        }
        #endregion

        #region Project tasks
        //Select all tasks
        public IQueryable<ProjectMgt.Entity.ProjectTaskItem> ProjectTask_ByProjectRef()
        {
            return ptRepository.GetAll();
        }
        //Select tasks by Project
        public IQueryable<ProjectMgt.Entity.ProjectTaskItem> ProjectTask_ByProjectRef(int projectReference)
        {

            return ptRepository.GetAll().Where(o => (o.ProjectReference.HasValue ? o.ProjectReference.Value : 0) == projectReference);
        }
        //Select project tasks by assigned user / resource
        //public IQueryable<ProjectMgt.Entity.ProjectTaskItem> ProjectTask_ByProjectRef(int UserID)
        //{
        //    //get list of projects
        //    var pArray = piRepository.GetAll().Where(o => o.ContractorID == UserID).Select(o => o.ProjectReference).ToArray();
        //    //get list of tasks
        //    return ptRepository.GetAll().Where(o => pArray.Contains(o.ProjectReference));
        //}
        //Select project tasks by Project and assigned user / resource
        public IQueryable<ProjectMgt.Entity.ProjectTaskItem> ProjectTask_ByProjectRef(int ProjectReference,int UserID)
        {
            //get list of tasks by project
            return ptRepository.GetAll().Where(o => o.ProjectReference == ProjectReference);
        }

        //Select tasks by Project
        public object ProjectTaskDetails_ByProjectRef(int projectReference)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var x = ptvRepository.GetAll().Where(o => o.ProjectReference == projectReference && (o.ProjectEndDate.Value.Date >= DateTime.Now.Date && o.ProjectEndDate.Value.Date <= DateTime.Now.Date.AddDays(7)) && o.ItemStatus == 1).ToList();
            TimesheetMgt.DAL.TimeSheetDataContext tdc = new TimesheetMgt.DAL.TimeSheetDataContext();
            var pricelist = tdc.TimesheetEntries.Where(a => a.ProjectReference == projectReference).ToList();

            var plist = (from a in x
                         select new
                         {
                             ItemDescription = a.ItemDescription,
                             ProjectEndDate = Convert.ToDateTime(a.ProjectEndDate.Value),
                             Resources = a.Resources,
                             ItemStatus = a.ItemStatus.Value,
                             Price = pricelist != null ? (pricelist.Where(o => o.ProjectTaskID == a.ID) != null ? pricelist.Where(o => o.ProjectTaskID == a.ID).Select(o => o.TotalCost.Value).Sum() : 0) : 0
                         }).ToList();

            return jsonSerializer.Serialize(plist).ToString();
        }
        public object ProjectActiveTaskDetails_ByProjectRef(int projectReference)
        {
            IProjectRepository<ProjectMgt.Entity.V_ProjectTaskDetail> ptvRepository = new ProjectRepository<ProjectMgt.Entity.V_ProjectTaskDetail>();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var x = ptvRepository.GetAll().Where(o => o.ProjectReference == projectReference && o.ProjectEndDate <= DateTime.Now.Date && o.ItemStatus != 3).ToList();
            TimesheetMgt.DAL.TimeSheetDataContext tdc = new TimesheetMgt.DAL.TimeSheetDataContext();
            var pricelist = tdc.TimesheetEntries.Where(a => a.ProjectReference == projectReference).ToList();

            var plist = (from a in x
                         select new
                         {
                             ItemDescription = a.ItemDescription,
                             ProjectEndDate = a.ProjectEndDate.Value.Date,
                             Resources = a.Resources,
                             ItemStatus = a.ItemStatus.Value,
                             Price = pricelist != null ? (pricelist.Where(o => o.ProjectTaskID.Value == a.ID) != null ? pricelist.Where(o => o.ProjectTaskID.Value == a.ID).Select(o => o.TotalCost).Sum() : 0) : 0
                         }).ToList();

            return jsonSerializer.Serialize(plist).ToString();
        }
        #endregion

        #region Project Issues
        public IQueryable<ProjectMgt.Entity.ProjectIssue> ProjectIssues_SelectAll()
        {
            return psRepository.GetAll();
        }
        public IQueryable<ProjectMgt.Entity.ProjectIssue> ProjectIssues_SelectByProjectReference(int projectReference)
        {
            return psRepository.GetAll().Where(o=>o.Projectreference == projectReference);
        }
        #endregion

        #region Dashboard
        public ProjectMgt.Entity.Projects_DashboardBasicSummaryResult Projects_DashboardBasicSummary_Selectall()
        {
            using (ProjectMgt.DAL.projectTaskDataContext pd = new DAL.projectTaskDataContext())
            {
                return pd.Projects_DashboardBasicSummary().FirstOrDefault();
            }

        }
        public List<ProjectMgt.Entity.Projects_DashboardSummaryResult> Projects_DashboardSummary_Selectall()
        {
            using (ProjectMgt.DAL.projectTaskDataContext pd = new DAL.projectTaskDataContext())
            {
                return pd.Projects_DashboardSummary().OrderByDescending(o => o.ProjectReference).Take(20).ToList();
            }

        }
        public ProjectMgt.Entity.Projects_DashboardFinanceChartResult Projects_DashboardFinance_Select(int projectReference)
        {
            using(ProjectMgt.DAL.projectTaskDataContext pd = new DAL.projectTaskDataContext())
            {
                return pd.Projects_DashboardFinanceChart(projectReference).FirstOrDefault();
            }
        }
        #endregion
    }
}
