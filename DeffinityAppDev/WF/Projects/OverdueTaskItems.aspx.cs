using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
public partial class OverdueTaskItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            lblProject.Text = sessionKeys.Prefix + sessionKeys.Project.ToString();        
        }
    }

    private void BindGrid()
    {
        using (projectTaskDataContext db = new projectTaskDataContext())
        {
            int projectReference = 0;
            if(Request.QueryString["project"]!=null)
                projectReference = Convert.ToInt32(Request.QueryString["project"].ToString());
            var project = db.Projects.Where(p => p.ProjectReference == projectReference).FirstOrDefault();
            if (project != null)
            {
                var projectItemList = db.ProjectTaskItems.Where(p => p.ProjectReference == projectReference).ToList();
                if (project.ProjectStatusID == 1) // status - pending

                    projectItemList = projectItemList.Where(r => r.ProjectEndDate < DateTime.Now).ToList();
                else
                    projectItemList = projectItemList.Where(r => r.CompletionDate < DateTime.Now).ToList();

                if (project.ProjectStatusID == 1)
                {

                    var result = (from p in projectItemList select new OverdueTasks { Task = p.ItemDescription, StartDate = p.ProjectStartDate, EndDate = p.ProjectEndDate }).ToList();
                    gvOverdueTasks.DataSource = result;
                    gvOverdueTasks.DataBind();
                }
                else
                {
                    var result = (from p in projectItemList select new OverdueTasks { Task = p.ItemDescription, StartDate = p.StartDate, EndDate = p.CompletionDate }).ToList();
                    gvOverdueTasks.DataSource = result;
                    gvOverdueTasks.DataBind();
                }
                
            }
           
           

        }
    }
}

public class OverdueTasks
{
    public string Task { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}