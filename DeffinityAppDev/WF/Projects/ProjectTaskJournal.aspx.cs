using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using UserMgt.DAL;

public partial class ProjectTaskJournal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Master.PageHead = "Project Management";
        hlinkGantt.NavigateUrl = "~/WF/Projects/ProjectOverviewV4.aspx?project=" + QueryStringValues.Project.ToString();
        hlinkGantt.Text = "Show Gantt";
        if (!IsPostBack)
        {
            BindTask();
            BindGridView();
        }
    }
    private void BindTask()
    {
        projectTaskDataContext pt = new projectTaskDataContext();
        var BindTasks = from p in pt.ProjectTaskItems
                           where p.ProjectReference == QueryStringValues.Project
                           select new
                           {
                               ID= p.ID,
                               Name = p.ItemDescription                               
                           };
        if (BindTasks != null)
        {
            ddlTask.DataSource = BindTasks;
            ddlTask.DataTextField = "Name";
            ddlTask.DataValueField = "ID";
            ddlTask.DataBind();
        }
        ddlTask.Items.Insert(0, new ListItem("All", "0"));
    }
    private void BindGridView()
    { 
        projectTaskDataContext pt = new projectTaskDataContext();
        UserDataContext uc = new UserDataContext();

        var Result1 = (from p in pt.ProjectTaskJournals
                       join t in pt.ProjectTaskItems on p.TaskID equals t.ID
                       join s in pt.ItemStatus on p.TaskStatus equals s.ID
                       join st in pt.ItemStatus on t.ItemStatus equals st.ID
                       where p.Projectreference == QueryStringValues.Project && p.ChangedbyUserID > 0
                       select new
                       {
                           TaskID1 = p.TaskID,
                           Task1 = t.ItemDescription,
                           Osdate1= t.ProjectStartDate,
                           sdate1 = p.StartDate,
                           Oedate1=t.ProjectEndDate,
                           edate1 = p.EndDate,
                           Oasdate=t.StartDate,
                           asdate1 = p.ActualStartDate,
                           Oaedate=t.CompletionDate,
                           aedate1 = p.ActualEndDate,
                           Ostatus = st.Status,
                           status1 = s.Status,
                           Opercentcomplete1=t.PercentComplete,
                           percentcomplete1 = p.PercentComplete,
                           ModifiedBy1 = p.ChangedbyUserID,
                           ModifiedDate1 = p.ModifiedDate
                       }).ToList();
        var Result2 = (from con in uc.Contractors
                       select new { ID1 = con.ID, Name1 = con.ContractorName }).ToList();

        var BindGridView = (from r1 in Result1
                          join r2 in Result2 on r1.ModifiedBy1 equals r2.ID1
                          select new
                          {
                              TaskID = r1.TaskID1,
                              Task = r1.Task1,
                              sdate = r1.sdate1,
                              edate = r1.edate1,
                              asdate = r1.asdate1,
                              aedate = r1.aedate1,
                              status = r1.status1,
                              percentcomplete = r1.percentcomplete1,
                              Osdate = r1.Osdate1,
                              Oedate = r1.Oedate1,
                              Oasdate = r1.Oasdate,
                              Oaedate = r1.Oaedate,
                              Ostatus = r1.Ostatus,
                              Opercentcomplete = r1.Opercentcomplete1,
                              ModifiedBy = r2.Name1,
                              ModifiedDate = r1.ModifiedDate1
                          }).ToList();


        //var BindGridView = from p in pt.ProjectTaskJournals
        //                   join t in pt.ProjectTaskItems on p.TaskID equals t.ID
        //                   join s in pt.ItemStatus on t.ItemStatus equals s.ID
        //                   join c in uc.Contractors on p.ChangedbyUserID equals c.ID
        //                   where p.Projectreference == QueryStringValues.Project && p.ChangedbyUserID >0
        //                   select new
        //                   {
        //                       TaskID=p.TaskID,
        //                       Task = t.ItemDescription,
        //                       sdate = p.StartDate,
        //                       edate = p.EndDate,
        //                       asdate = p.ActualStartDate,
        //                       aedate = p.ActualEndDate,
        //                       status = s.Status,
        //                       percentcomplete = t.PercentComplete,
        //                       ModifiedBy = c.ContractorName,
        //                       ModifiedDate = p.ModifiedDate
        //                   };


        //if task is selected
        if (int.Parse(ddlTask.SelectedValue) > 0)
        {
            BindGridView = BindGridView.Where(t => t.TaskID == int.Parse(ddlTask.SelectedValue)).ToList();
        }
        
        if (BindGridView != null)
        {
            GridTaskJournal.DataSource = BindGridView;
            GridTaskJournal.DataBind();
        }
        
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
}