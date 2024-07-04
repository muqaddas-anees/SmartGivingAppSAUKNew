using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;


public partial class OpsApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Check Points";
        if(!IsPostBack)
        {
            BindCheckPoint(sessionKeys.UID);

        }
         
    }
   IProjectRepository<ProjectMgt.Entity.ProjectDetails> pRepository= null;
    IProjectRepository<ProjectMgt.Entity.CheckPointCheckListItem> cRepository= null;
    IProjectRepository<ProjectMgt.Entity.ProjectTaskItem> tRepository = null;
    public void BindCheckPoint(int userID)
    {
        try
        {
            pRepository = new ProjectRepository<ProjectMgt.Entity.ProjectDetails>();
            cRepository = new ProjectRepository<ProjectMgt.Entity.CheckPointCheckListItem>();
            tRepository = new ProjectRepository<ProjectMgt.Entity.ProjectTaskItem>();
            //project check point assign
            var cdate = DateTime.Now;
            var tlist = tRepository.GetAll().Where(p => p.AssignedTo == userID.ToString() && p.QA.ToLower() == "y" && p.ItemStatus != 3 && (p.CompletionDate.HasValue && DateTime.Compare(p.CompletionDate.Value, cdate) < 0)).ToList();
            if (tlist != null)
            {
                var pArray = tlist.Select(o => o.ProjectReference).Distinct().ToArray();
                var ctArray = tlist.Select(o => o.CheckPointID).Distinct().ToArray();

                var plist = pRepository.GetAll().Where(o => pArray.Contains(o.ProjectReference) && o.ProjectStatusID == 2).ToList();
                var clist = cRepository.GetAll().Where(o => pArray.Contains(o.ProjectReference) && o.Status == "Closed").ToList();
                if (plist != null)
                {
                    var restultset = (from p in plist
                                      select new
                                      {
                                          p.ProjectReference,
                                          p.ProjectReferenceWithPrefix,
                                          p.ProjectTitle,
                                          p.OwnerName,
                                          p.SiteName,
                                          checkitemcount = (clist != null ? (clist.Where(o => o.ProjectReference == p.ProjectReference).Count()) : 0),
                                          checkData = tlist.Where(o => o.ProjectReference == p.ProjectReference).OrderByDescending(o => o.CompletionDate).Select(o=>o.CompletionDate).FirstOrDefault()
                                      }).ToList();
                    GridView2.DataSource = restultset.OrderByDescending(o => o.checkData).ToList();
                    GridView2.DataBind();
                }
            }
        }
        catch(Exception ex)
        { LogExceptions.WriteExceptionLog(ex); }
    }
}
