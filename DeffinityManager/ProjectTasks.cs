using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using Deffinity.ProjectTasksManagers;
using System.Linq;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using AjaxControlToolkit;
using System.IO;

    /// <summary>
    /// Summary description for ProjectTasks
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class ProjectTasks : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void ProjectTaskXMLData(string ProjectReference)
        {
            System.IO.StringWriter htmlStringWriter = new System.IO.StringWriter();
            HttpContext.Current.Server.Execute("ProjectOverviewV2_XML.aspx?project=" + int.Parse(ProjectReference), htmlStringWriter);
            string path = HttpContext.Current.Server.MapPath("WF\\UploadData\\projects");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string path1 = path+ "\\" + ProjectReference + ".xml";
            if (File.Exists(path1))
            {
                File.Delete(path1);
            }
            File.WriteAllText(path + "\\" + ProjectReference + ".xml", (htmlStringWriter.GetStringBuilder().ToString()));

        }

        [WebMethod]
        public DataSet ProjectTask_GetGanttchart(int ProjectReference)
        {
            return ProjectTasksManager.GetChartData(ProjectReference);
        }

        [WebMethod(EnableSession = true)]
        public void ProjectTask_InsertUpdate(string TaskID, string ProjectReference, string ItemDescription, string IndentLevel, string
    ProjectStartDate, string ProjectEndDate, string CompletionDate, string StartDate, string PercentComplete, string Notes,
    string AmberDays, string RedDays, string AmberPercent, string RedPercent, string RAGRequired, string QA, string IncludeInValuation,
    string ResourceIDs, string ItemStatus, string RAGStatus, string isMilestone, string categoryid, string TeamID, string ChechPointID,string customerId )
        {
            if (!string.IsNullOrEmpty(TaskID))
            {
                if (int.Parse(TaskID) > 0)
                {

                    ProjectTasksManager.TaskGantt_InsertUpdate(int.Parse(TaskID.Trim()), int.Parse(ProjectReference.Trim()), ItemDescription,
                        int.Parse(IndentLevel.Trim()), ProjectStartDate, ProjectEndDate, CompletionDate, StartDate, PercentComplete, Notes,
                        int.Parse(string.IsNullOrEmpty(AmberDays) ? "3" : AmberDays), int.Parse(string.IsNullOrEmpty(RedDays) ? "1" : RedDays), int.Parse(AmberPercent.Trim()), int.Parse(RedPercent.Trim()), RAGRequired,
                        QA, IncludeInValuation, ResourceIDs, int.Parse(ItemStatus.Trim()), RAGStatus, bool.Parse(string.IsNullOrEmpty(isMilestone) ? "false" : isMilestone),
                        int.Parse(string.IsNullOrEmpty(categoryid) ? "0" : categoryid), sessionKeys.UID, 0, int.Parse(TeamID), int.Parse(ChechPointID),int.Parse(customerId));
                }
            }

        }
        [WebMethod]
        public string ProjectTask_ResourceByTeam_srv(string TaskID)
        {
            string retval = string.Empty;
            if (!string.IsNullOrEmpty(TaskID))
            {
                if (int.Parse(TaskID) > 0)
                {
                    retval = ProjectTasksManager.ResourceIDByTask(int.Parse(TaskID));
                }
            }

            return retval;
        }

        [WebMethod]
        public IEnumerable<CheckPointCheckListItem> GetChecklistItemsByTaskId(int taskId)
        {
                List<CheckPointCheckListItem> checkPointCheckListItem = new List<CheckPointCheckListItem>();
                using (projectTaskDataContext pt = new projectTaskDataContext())
                {
                   checkPointCheckListItem =  pt.CheckPointCheckListItems.Where(c => c.TaskID == taskId).Select(c => c).ToList();
                }
                return checkPointCheckListItem;
           
        }
        [WebMethod]
        public  void AddCheckpointChecklistItems(int projectReference, int taskId, int templateId)
        {
            try
            {
                using (projectTaskDataContext pt = new projectTaskDataContext())
                {
                    //AssignCheckPointCheckList assignCheckPointCheckList = pt.AssignCheckPointCheckLists.Select(c => c).FirstOrDefault();

                    List<string> strlist = pt.MasterTemplateItems.Where(m => m.MasterTemplateID == templateId).Select(m => m.ItemDescription).ToList();
                    List<CheckPointCheckListItem> cl = pt.CheckPointCheckListItems.Where(c => c.ProjectReference == projectReference && c.TaskID == taskId).Select(c => c).ToList();

                    if (cl.Count == 0)
                    {
                        if (strlist.Count > 0)
                        {

                            foreach (string s in strlist)
                            {
                                CheckPointCheckListItem checkPointCheckListItem = new CheckPointCheckListItem();
                                checkPointCheckListItem.ProjectReference = projectReference;
                                checkPointCheckListItem.Status = "Pending";
                                checkPointCheckListItem.ItemDescription = s;
                                checkPointCheckListItem.TaskID = taskId;
                                pt.CheckPointCheckListItems.InsertOnSubmit(checkPointCheckListItem);
                                pt.SubmitChanges();
                            }
                        }
                    }
                    //else
                    //{
                    //    foreach (var item in cl)
                    //    {
                    //        CheckPointCheckListItem cc = pt.CheckPointCheckListItems.Where(p => p.ID == item.ID).FirstOrDefault();
                    //        if (cc != null)
                    //        {
                    //            cc.Status = "In Progress";
                    //            cc.ClosedDate = null;
                    //            pt.SubmitChanges();
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
        [WebMethod]
        public string ProjectTask_ResourceByTeam_srv(string ProjectReference, string TaskID)
        {
            string retval = string.Empty;
            if (!string.IsNullOrEmpty(TaskID))
            {
                if (int.Parse(TaskID) > 0)
                {
                    retval = ProjectTasksManager.ResourceIDByTask(int.Parse(TaskID));
                }
            }

            return retval;
        }
        [WebMethod]
        public string ProjectTask_ResourceIDByListposition_srv(string ListPosition, string ProjectReference)
        {
            string retval = string.Empty;
            if (!string.IsNullOrEmpty(ListPosition))
            {
                if (int.Parse(ListPosition) > 0)
                {
                    retval = ProjectTasksManager.ResourceIDByListposition(int.Parse(ListPosition), int.Parse(ProjectReference));
                }
            }
            return retval;
        }
        [WebMethod]
        public void ProjectTask_DeleteResourceByTask_srv(string ProjectReference, string TaskID)
        {
            if (!string.IsNullOrEmpty(TaskID))
            {
                if (int.Parse(TaskID) > 0)
                {
                    ProjectTasksManager.ProjectTask_DeleteResourceByTask(int.Parse(ProjectReference), int.Parse(TaskID));
                }
            }
        }
        [WebMethod]
        public void ProjectTask_CopyResourceByTask_srv(string ProjectReference, string ListPosition_selected, string ListPosition_apply)
        {
            if (!string.IsNullOrEmpty(ListPosition_apply))
            {
                if (int.Parse(ListPosition_apply) > 0)
                {
                    ProjectTasksManager.ProjectTask_CopyResourceByTask(int.Parse(ProjectReference), int.Parse(ListPosition_selected), int.Parse(ListPosition_apply));
                }
            }
        }
        [WebMethod]
        public void ProjectTask_MoveResourceByTask_srv(string ProjectReference, string ListPosition_selected, string ListPosition_apply)
        {
            if (!string.IsNullOrEmpty(ListPosition_apply))
            {
                if (int.Parse(ListPosition_apply) > 0)
                {
                    ProjectTasksManager.ProjectTask_MoveResourceByTask(int.Parse(ProjectReference), int.Parse(ListPosition_selected), int.Parse(ListPosition_apply));
                }
            }
        }

        [WebMethod]
        public void ResourcePlanInsert(string TaskID, string ResourceTypeID, string QTY)
        {
            if ((int.Parse(TaskID) > 0) && (int.Parse(ResourceTypeID) > 0))
            {
                ProjectTasksManager.ProjectTaskToResourceType_Insert(int.Parse(TaskID), int.Parse(ResourceTypeID), int.Parse(string.IsNullOrEmpty(QTY) ? "1" : QTY));
            }
        }

        [WebMethod]
        public string InsertResource(string txtVal)
        {
            int myret = 0;
            int k = ProjectTasksManager.insertResource("Deffinity_ResourceTypeInsertUpdate", txtVal, 0, myret);
            return myret.ToString();
        }

        [WebMethod(EnableSession = true)]
        public void projectTaskItems_Update(string taskID, string sDate, string eDate,
            string projectRef, string resourceID, string Notes)
        {
            projectTaskDataContext taskPlanner = new projectTaskDataContext();
            taskPlanner.ProjectTaskItems_DateUpdate(int.Parse(taskID), Convert.ToDateTime(string.IsNullOrEmpty(sDate) ? DateTime.Now.ToShortDateString() : sDate),
                Convert.ToDateTime(string.IsNullOrEmpty(eDate) ? DateTime.Now.ToShortDateString() : eDate),
                int.Parse(resourceID), int.Parse(projectRef), Notes);
        }
        //Update_ProjectTaskDates
        [WebMethod]
        public void ProjectTaskDates(string startdate, string enddate, string taskid, string projectstatus)
        {
            if (int.Parse(taskid) > 0)
            {
                ProjectTasksManager.Update_ProjectTaskDates(startdate, enddate, int.Parse(taskid), int.Parse(projectstatus));
            }
        }
        [WebMethod(EnableSession = true)]
        public void ProjectTaskDeleteAll(string projectreference)
        {
            if (ProjectTasksManager.CommandField(sessionKeys.SID, sessionKeys.UID) != false)
            {
                if (int.Parse(projectreference) > 0)
                {
                    ProjectTasksManager.Delete_ProjectTasks(int.Parse(projectreference));
                }
            }
        }
        //Bind ResourceTypes
        [WebMethod]
        public List<ResourceTypes> GetResourcesTypeData()
        {
            List<ResourceTypes> resources = new List<ResourceTypes>();
            //resourcetypes 
            DataSet ds = new DataSet();
            ds = ProjectTasksManager.GetAllResourceTypes();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ResourceTypes resourcetypes = new ResourceTypes();
                    resourcetypes.value = ds.Tables[0].Rows[i][0].ToString();
                    resourcetypes.Name = ds.Tables[0].Rows[i][1].ToString();
                    resources.Add(resourcetypes);
                }

            }
            ////resources = ds;
            //return ds;
            return resources;
        }
        //28/07/2011-------------

        [WebMethod]
        public List<ResourcesByTeam> GetResourceByTeam(int TeamID)
        {
            List<ResourcesByTeam> resource = new List<ResourcesByTeam>();
            try
            {

                DataSet ds = new DataSet();
                ds = ProjectTasksManager.ResourceByTeam(TeamID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ResourcesByTeam members = new ResourcesByTeam();
                        members.value = ds.Tables[0].Rows[i]["ID"].ToString();
                        members.Name = ds.Tables[0].Rows[i]["ContractorName"].ToString();
                        resource.Add(members);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
            return resource;

        }
        public DataSet ProjectTask_ResourceByteam(int team)
        {
            return ProjectTasksManager.ResourceByTeam(team);
        }

        //

    }

    public class ResourceTypes
    {
        private string _value;

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
    public class ResourcesByTeam
    {
        private string _value;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
