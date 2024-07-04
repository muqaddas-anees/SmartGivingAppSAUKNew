using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using PortfolioMgt.DAL;
using PortfolioMgt.Entity;
using UserMgt.DAL;
using UserMgt.Entity;
using HealthCheckMgt.BAL;
using ProjectMgt.Entity;



public partial class Default11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTasks();
            BindChecklist();
            BindCustomerUser();
            BindAssignedPM();
            Bindhealthcheck();
            BindGrid();
        }
    }

    protected void imgApplyChecklist_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlAssigntoPM.SelectedValue) > 0 || int.Parse(ddlCustomerUser.SelectedValue) >0)
        {
            if (int.Parse(ddlChecklist.SelectedValue) > 0 || int.Parse(ddlForms.SelectedValue) > 0)
            {
                //using (projectTaskDataContext db = new projectTaskDataContext())
                //{
                // Add checklistItems to the tasks
                AddCheckpointChecklistItems(QueryStringValues.Project, int.Parse(ddlTasks.SelectedValue), int.Parse(ddlChecklist.SelectedValue), int.Parse(ddlCustomerUser.SelectedValue), int.Parse(ddlAssigntoPM.SelectedValue));
                // }
                BindGrid();
            }
            else
            {

                lblMsg.Text = "Please select Checkpoint (or) Form";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblMsg.Text = "Please select Assigned to customer user (or) Assing to PM";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }
    public void AddCheckpointChecklistItems(int projectReference, int taskId, int templateId,int customerUserId,int assigntopm)
    {
        try
        {
            IHCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask> hpRepository = null;
            using (projectTaskDataContext pt = new projectTaskDataContext())
            {
                //AssignCheckPointCheckList assignCheckPointCheckList = pt.AssignCheckPointCheckLists.Select(c => c).FirstOrDefault();
                List<string> strlist = pt.MasterTemplateItems.Where(m => m.MasterTemplateID == templateId).Select(m => m.ItemDescription).ToList();
                List<CheckPointCheckListItem> cl = pt.CheckPointCheckListItems.Where(c => c.ProjectReference == projectReference && c.TaskID == taskId).Select(c => c).ToList();
                hpRepository = new HCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask>();
                if (Convert.ToInt32(ddlForms.SelectedValue) > 0)
                {
                    var hc_entity = hpRepository.GetAll().Where(o => o.TaskID == taskId).FirstOrDefault();
                    if (hc_entity == null)
                    {
                        var hc_pdetails = new HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask();
                        hc_pdetails.FormID = Convert.ToInt32(ddlForms.SelectedValue);
                        hc_pdetails.TaskID = taskId;
                        hpRepository.Add(hc_pdetails);
                    }
                    else
                    {
                        hc_entity.FormID = Convert.ToInt32(ddlForms.SelectedValue);
                        hpRepository.Edit(hc_entity);
                    }
                    lblMsg.Text = "Updated successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

                if (cl.Count == 0)
                {
                    // Update CheckPointID and AssignedToCustomerUser in ProjectTaskItems table
                    ProjectTaskItem projectTaskItem = pt.ProjectTaskItems.Where(p => p.ID == taskId).FirstOrDefault();
                    projectTaskItem.CheckPointID = templateId;
                    projectTaskItem.AssignedToCustomerUser = customerUserId;
                    projectTaskItem.AssignedTo = assigntopm.ToString();
                    pt.SubmitChanges();
                    //insert formid
                    if(Convert.ToInt32(ddlForms.SelectedValue)>0)
                    {
                        var hc_pdetails = new HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask();
                        hc_pdetails.FormID = Convert.ToInt32(ddlForms.SelectedValue);
                        hc_pdetails.TaskID = taskId;
                        hpRepository.Add(hc_pdetails);
                    }

                    if (strlist.Count > 0)
                    {

                        foreach (string s in strlist)
                        {
                            CheckPointCheckListItem checkPointCheckListItem = new CheckPointCheckListItem();
                            checkPointCheckListItem.ProjectReference = projectReference;
                            checkPointCheckListItem.Status = "In Progress";
                            checkPointCheckListItem.ItemDescription = s;
                            checkPointCheckListItem.TaskID = taskId;
                            pt.CheckPointCheckListItems.InsertOnSubmit(checkPointCheckListItem);
                            pt.SubmitChanges();
                        }
                    }
                    lblMsg.Text = "Applied successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    ProjectTaskItem projectTaskItem = pt.ProjectTaskItems.Where(p => p.ID == taskId).FirstOrDefault();
                   // projectTaskItem.CheckPointID = templateId;
                    projectTaskItem.AssignedToCustomerUser = customerUserId;
                    projectTaskItem.AssignedTo = assigntopm.ToString();
                    pt.SubmitChanges();


                    lblMsg.Text = "Applied successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
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
    private void BindTasks()
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var listTasks = (from r in db.ProjectTaskItems
                                 where r.ProjectReference == QueryStringValues.Project && r.isMilestone == true && r.QA.ToLower() == "y"
                                 orderby r.ListPosition
                                 select new { ID = r.ID, Name = r.ItemDescription }).ToList();
                ddlTasks.DataSource = listTasks;
                ddlTasks.DataTextField = "Name";
                ddlTasks.DataValueField = "ID";
                ddlTasks.DataBind();
                ddlTasks.Items.Insert(0, new ListItem("Please select...", "0"));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    private void BindChecklist()
    {
        try
        {
            using (projectTaskDataContext db = new projectTaskDataContext())
            {
                var checklist = (from p in db.MasterTemplates
                                 where (p.ChecklistType == 8)
                                 select new { ID = p.ID, Name = p.Description }).ToList();

                ddlChecklist.DataSource = checklist;
                ddlChecklist.DataTextField = "Name";
                ddlChecklist.DataValueField = "ID";
                ddlChecklist.DataBind();
                ddlChecklist.Items.Insert(0, new ListItem("Please select...", "0"));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindCustomerUser()
    {
       
        using (PortfolioDataContext db = new PortfolioDataContext())
        {
            try
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    var portfolioId = db.ProjectDetails.Where(p => p.ProjectReference == QueryStringValues.Project).Select(p => p.Portfolio).FirstOrDefault();
                    var x = db.AssignedCustomerToPortfolios.Select(a => a).ToList();
                    var y = ud.Contractors.Where(c => c.Status.ToLower() == "active").Select(c => c).ToList();

                    var customerUser = (from p in x
                                        join c in y on
                                        p.CustomerID equals c.ID
                                        where p.Portfolio == portfolioId
                                        select new { ID = c.ID, Name = c.ContractorName }).ToList();

                    ddlCustomerUser.DataSource = customerUser;
                    ddlCustomerUser.DataValueField = "ID";
                    ddlCustomerUser.DataTextField = "Name";
                    ddlCustomerUser.DataBind();
                    ddlCustomerUser.Items.Insert(0, new ListItem("Please select...", "0"));
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    private void BindAssignedPM()
    {

        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            try
            {
                using (UserDataContext ud = new UserDataContext())
                {
                    int[] sids = new int[] { 1, 2, 3 };
                    var portfolioId = db.ProjectDetails.Where(p => p.ProjectReference == QueryStringValues.Project).Select(p => p.Portfolio).FirstOrDefault();
                    //var x = db.ProjectItems.Where(o => o.ProjectReference == QueryStringValues.Project && o.ContractorID.HasValue).Select(p => p.ContractorID.Value).ToList();
                    //var y = ud.Contractors.Where(c => c.Status.ToLower() == "active" && sids.Contains(c.SID.Value) && x.Select(p => p).ToArray().Contains(c.ID)).Select(c => c).ToList();
                    var y = ud.Contractors.Where(c => c.Status.ToLower() == "active" && sids.Contains(c.SID.Value) ).Select(c => c).ToList();

                    var AssignUsers = (from c in y
                                        orderby c.ContractorName
                                        select new { ID = c.ID, Name = c.ContractorName }).ToList();

                    ddlAssigntoPM.DataSource = AssignUsers;
                    ddlAssigntoPM.DataValueField = "ID";
                    ddlAssigntoPM.DataTextField = "Name";
                    ddlAssigntoPM.DataBind();
                    ddlAssigntoPM.Items.Insert(0, new ListItem("Please select...", "0"));
                }
            }
            catch (Exception ex)
            {
                LogExceptions.WriteExceptionLog(ex);
            }
        }
    }
    HealthCheckBAL hb;
    IProjectRepository<ProjectMgt.Entity.ProjectDetails> pRepository = null;
    public void Bindhealthcheck()
    {
        try
        {
            int customerID = 0;
            if (QueryStringValues.Project > 0)
            {
                pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();
                var pdetails = pRepository.GetAll().Where(o=>o.ProjectReference == QueryStringValues.Project).FirstOrDefault();
                customerID = pdetails.Portfolio.Value;
            }
            else
            {
                customerID = sessionKeys.PortfolioID;


            }
            hb = new HealthCheckBAL();
            var formlist = hb.HealthCheck_Form_SelectByCustomerID(customerID);
            ddlForms.DataSource = (from h in formlist
                                   select new { h.FormID, h.FormName }).ToList();
            ddlForms.DataTextField = "FormName";
            ddlForms.DataValueField = "FormID";
            ddlForms.DataBind();
            ddlForms.Items.Insert(0, new ListItem("Please select...", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }

    public void BindGrid()
    {
        string formname = string.Empty;
        IProjectRepository<ProjectMgt.Entity.ProjectTaskItem> ptRepository = new ProjectRepository<ProjectMgt.Entity.ProjectTaskItem>();
        IProjectRepository<ProjectMgt.Entity.MasterTemplate> mtRepository = new ProjectRepository<ProjectMgt.Entity.MasterTemplate>();
        IUserRepository<UserMgt.Entity.Contractor> usRepository = new UserRepository<UserMgt.Entity.Contractor>();
        IHCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask> hpRepository = new HCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask>();
        int formid = 0;
        List<ProjectMgt.Entity.ProjectTaskItem> taskDetails = ptRepository.GetAll().Where(o => o.QA.ToLower() == "y" && o.ProjectReference == QueryStringValues.Project).ToList();
        if(taskDetails != null)
        {
            var assignuser = usRepository.GetAll().Where(o => taskDetails.Select(t=>t.AssignedTo).ToArray().Contains(o.ID.ToString())).ToList();
            var formDetails = hpRepository.GetAll().Where(o => taskDetails.Select(t=>t.ID).ToArray().Contains(o.TaskID.HasValue?o.TaskID.Value:0)).ToList();
            var templateDetails = mtRepository.GetAll().Where(o => taskDetails.Select(t => t.CheckPointID).ToArray().Contains(o.ID)).ToList();

            var result = (from p in taskDetails
                          select new
                          {
                              TaskName = p.ItemDescription,
                              TaskDate = p.ProjectEndDate.HasValue? p.ProjectEndDate.Value.ToShortDateString():string.Empty,
                              Checkpoint = GetTemplateName( templateDetails, p.CheckPointID ),
                              AssignTo = GetAssigntoName(assignuser, Convert.ToInt32(string.IsNullOrEmpty(p.AssignedTo)?"0":p.AssignedTo)),
                              Form = GetFormName(formDetails,p.ID)
                          }).ToList();
            Grid_assignto.DataSource = result;
            Grid_assignto.DataBind();
          
        }

    }

    public void BindSetDropdown(int TaskID)
    {
        IProjectRepository<ProjectMgt.Entity.ProjectTaskItem> ptRepository = new ProjectRepository<ProjectMgt.Entity.ProjectTaskItem>();
        IHCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask> hpRepository = new HCRepository<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask>();
       
        var taskDetails = ptRepository.GetAll().Where(o => o.QA.ToLower() == "y" && o.ID == TaskID).FirstOrDefault();
        if (taskDetails != null)
        {
            ddlAssigntoPM.SelectedValue = !string.IsNullOrEmpty(taskDetails.AssignedTo) ? taskDetails.AssignedTo.ToString() : "0";
            ddlCustomerUser.SelectedValue = taskDetails.AssignedToCustomerUser != null ? taskDetails.AssignedToCustomerUser.Value.ToString() : "0";
            ddlChecklist.SelectedValue = taskDetails.CheckPointID != null ? taskDetails.CheckPointID.Value.ToString() : "0";
            var h = hpRepository.GetAll().Where(o => o.TaskID == taskDetails.ID).FirstOrDefault();
            if (h != null)
                ddlForms.SelectedValue = h.FormID.HasValue ? h.FormID.Value.ToString() : "0";
        }
    }
    public string GetTemplateName(List<ProjectMgt.Entity.MasterTemplate> tlist, int? checkpointid)
    {
        string retval = string.Empty;
        if (tlist != null)
        {
            var u = tlist.Where(o => o.ID == checkpointid).FirstOrDefault();
            if (u != null)
            {
                retval = u.Description;
            }
        }
        return retval;
    }
    public string GetAssigntoName(List<UserMgt.Entity.Contractor> ulist, int? assignto)
    {
        string retval = string.Empty;

        if (ulist != null)
        {
            var u = ulist.Where(o => o.ID == assignto).FirstOrDefault();
            if(u != null)
            {
                retval = u.ContractorName;
            }
        }

        return retval;
    }
    public string GetFormName(List<HealthCheckMgt.Entity.HealthCheck_FormAssignToProjectTask>formDetails, int taskid)
    {
        string retval = string.Empty;

        if (formDetails != null)
        {
            var f = formDetails.Where(o => o.TaskID == taskid).Select(o => o).FirstOrDefault();
            if (f != null)
            {
                retval = f.HealthCheck_Form.FormName;
            }
            else
                retval = string.Empty;
        }


        return retval;
    }
    protected void ddlTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlTasks.SelectedValue) > 0)
            {
                BindSetDropdown(Convert.ToInt32(ddlTasks.SelectedValue));
                //BindGrid();
            }
            else
            {
                ddlAssigntoPM.SelectedIndex = 0;
                ddlChecklist.SelectedIndex = 0;
                ddlCustomerUser.SelectedIndex = 0;
                ddlForms.SelectedIndex = 0;

            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
    protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdlist.SelectedValue == "Checkpoint")
        {
            pnlCheckpoints.Visible = true;
            pnlForms.Visible = false;
        }
        else
        {
            pnlCheckpoints.Visible = false;
            pnlForms.Visible = true;
        }
    }
}