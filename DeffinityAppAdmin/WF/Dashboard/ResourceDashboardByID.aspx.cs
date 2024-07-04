using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectMgt.DAL;
using ProjectMgt.Entity;
using DC.BAL;
using DC.Entity;
using DC.BLL;
using System.Data;
using VT.DAL;
using Flan.FutureControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DC.DAL;
using PortfolioMgt.DAL;



public partial class ResourceDashboardByID : System.Web.UI.Page
{
    int resourceid = 0;
    Database db = DatabaseFactory.CreateDatabase("DBstring");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPage();
            
        }
    }
    private void BindPage()
    {
        try
        {
            if (Request.QueryString["uid"] != null)
            {
                BindServiceDeskTickets();
                resourceid = int.Parse(Request.QueryString["uid"].ToString());
                ltrHeader.Text = "Project Tasks for " + Request.QueryString["uname"].ToString();
                ltrPendingHeader.Text = "Tasks for " + Request.QueryString["uname"].ToString();
                ltrAnnualLeave.Text = "Annual Leave for " + Request.QueryString["uname"].ToString();
                LtrlServiceDesk.Text = "Service Desk Tickets for " + Request.QueryString["uname"].ToString();
                BindProjects(resourceid);
                BindPendingProjects(resourceid);
                BindResourceTaskGrid();
                BindResourceTaskGridPendingProjects(int.Parse(ddlPendingProjects.SelectedValue), resourceid);
            
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindServiceDeskTickets()
    {
        try
        {
            using (PortfolioDataContext pd = new PortfolioDataContext())
            {

                using (DCDataContext dc = new DCDataContext())
                {
                    var cList = dc.CallDetails.Where(o => o.RequestTypeID == 6 && o.LoggedBy==int.Parse(Request.QueryString["uid"] != null ? Request.QueryString["uid"] : "0")).Select(c => c).ToList();
                    var companyids = cList.Select(o => o.CompanyID.HasValue ? o.CompanyID.Value : 0).Distinct().ToList();
                    var pCompany = pd.ProjectPortfolios.Where(p => companyids.ToArray().Contains(p.ID)).Select(p => p).ToList();
                    var FlsList = dc.FLSDetails.ToList();
                    var Slist = dc.Status.ToList();

                    var x = (from a in cList
                             join b in FlsList on a.ID equals b.CallID
                             join c in Slist on a.StatusID equals c.ID
                             select new
                             {
                                 CallID = "TN:" + b.CallID.ToString(),
                                 Details = b.Details,
                                 CompanyID =a.CompanyID.HasValue ? pCompany.Where(d=>d.ID==a.CompanyID.Value).Select(d=>d.PortFolio).FirstOrDefault(): string.Empty,
                                 ScheduledDate = b.ScheduledDate,
                                 StatusID = c.Name
                             }).ToList();
                    GridServicedeskTickets.DataSource = x;
                    GridServicedeskTickets.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void BindProjects(int ResourceID)
    {
        using (projectTaskDataContext pdt = new projectTaskDataContext())
        {
           ddlProjects.DataSource = (from p in pdt.ProjectDetails
                               join q in pdt.ProjectItems on p.ProjectReference equals q.ProjectReference
                               where q.ContractorID == int.Parse(Request.QueryString["uid"] != null ?Request.QueryString["uid"] : "0")
                               select new { ProjectReference = p.ProjectReference, ProjectTitle = p.ProjectReferenceWithPrefix + "_" + p.ProjectTitle });
            //getProjects.Dump();
            ddlProjects.DataValueField = "ProjectReference";
            ddlProjects.DataTextField = "ProjectTitle";
            ddlProjects.DataBind();
        }
        ddlProjects.Items.Insert(0, new ListItem("All", "0"));
       // ddlProjects.DataSource = Deffinity.Projects.Resources.GetProjectListByResource(ResourceID);
       
    }
    private void BindPendingProjects(int ResourceID)
    {
        using (projectTaskDataContext pdt = new projectTaskDataContext())
        {
            ddlPendingProjects.DataSource = (from p in pdt.ProjectDetails
                                      join q in pdt.ProjectItems on p.ProjectReference equals q.ProjectReference
                                      where q.ContractorID == int.Parse(Request.QueryString["uid"] != null ? Request.QueryString["uid"] : "0") && p.ProjectStatusName.ToString().ToLower() == "pending"
                                      select new { ProjectReference = p.ProjectReference, ProjectTitle = p.ProjectReferenceWithPrefix + "_" + p.ProjectTitle });
            //getProjects.Dump();
            ddlPendingProjects.DataValueField = "ProjectReference";
            ddlPendingProjects.DataTextField = "ProjectTitle";
            ddlPendingProjects.DataBind();
        }
        ddlPendingProjects.Items.Insert(0, new ListItem("All", "0"));
        // ddlProjects.DataSource = Deffinity.Projects.Resources.GetProjectListByResource(ResourceID);

    }
    private void BindResourceTaskGrid()
    {
        resourceid = int.Parse(Request.QueryString["uid"].ToString());
        GridProjects.DataSource = Deffinity.Projects.Resources.GetProjectTaskListByResource(int.Parse(ddlProjects.SelectedValue), resourceid);
        GridProjects.DataBind();
        
    }
  

    private void BindResourceTaskGridPendingProjects(int project, int ResourceID)
    {
        string filterExp = "ProjectStatusName = 'Pending'";
        DataTable dt = Deffinity.Projects.Resources.GetProjectTaskListByResource(project, ResourceID);
        
        DataView dvData = new DataView(dt);
        dvData.RowFilter = filterExp;

        gvPendingProjects.DataSource = dvData;
        gvPendingProjects.DataBind();
    }
    protected void btnProjectView_Click(object sender, EventArgs e)
    {
        try
        {
            BindResourceTaskGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void GridProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        try
        {
            GridProjects.PageIndex = e.NewPageIndex;
            BindResourceTaskGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void gvPendingProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPendingProjects.PageIndex = e.NewPageIndex;
            BindResourceTaskGridPendingProjects(int.Parse(ddlPendingProjects.SelectedValue), int.Parse(Request.QueryString["uid"].ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void btnPendingProjectView_Click(object sender, EventArgs e)
    {
        try
        {
            BindResourceTaskGridPendingProjects(int.Parse(ddlPendingProjects.SelectedValue), int.Parse(Request.QueryString["uid"].ToString()));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void rowDragOE_RowDrop(object sender, RowDropEventArgs e)
    {
        try
        {
           // GetMaxListPosition(7);
            string stemp = "";
            int id;
            int newCnt;
            int i = 0;
            //UpdateRowCount();
            RowDragOverlayExtender rde = (RowDragOverlayExtender)sender;
            stemp = e.SourceGridViewID;
            stemp = e.SourceRowIndex.ToString();
            id = Convert.ToInt32(((GridView)Page.FindControl(e.SourceGridViewID)).DataKeys[e.SourceRowIndex].Value.ToString());
            stemp = rde.GridViewUniqueID;

            //grid to grid drag and drop        
            if (e.SourceGridViewID.Contains("GridProjects") == true)
            {
                GridItemPosition(e.SourceRowIndex + 1, rde.RowIndex + 1, id, Convert.ToInt32(Request.QueryString["uid"]));
            }
            BindResourceTaskGrid();
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message);
        }

    }
    private void GridItemPosition(int oldPos, int newPos, int id, int contractorID)
    {
        DbCommand cmd = db.GetStoredProcCommand("DN_UserTaskListPostion");
        db.AddInParameter(cmd, "@newPos", DbType.Int32, newPos);
        db.AddInParameter(cmd, "@oldPos", DbType.Int32, oldPos);
        db.AddInParameter(cmd, "@id", DbType.Int32, id);
        db.AddInParameter(cmd, "@ContractorID", DbType.Int32, contractorID);
        db.ExecuteNonQuery(cmd);
        cmd.Dispose();
    }


    protected void GridProjects_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridProjects.EditIndex = e.NewEditIndex;
        BindResourceTaskGrid();
    }
    protected void GridProjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridProjects.EditIndex = -1;
        BindResourceTaskGrid();
    }
    protected void GridProjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridProjects.EditIndex = -1;
        BindResourceTaskGrid();
    }
   
    protected void GridProjects_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Update")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                int i = GridProjects.EditIndex;
                GridViewRow row = GridProjects.Rows[i];
                int utilisation = Convert.ToInt32(((TextBox)row.FindControl("txtUtilisation")).Text);
                using (projectTaskDataContext db = new projectTaskDataContext())
                {
                    var projectItem = db.ProjectItems.Where(p => p.ID == id).FirstOrDefault();
                    if (projectItem != null)
                    {
                        projectItem.Utilisation = utilisation;
                        db.SubmitChanges();
                        BindResourceTaskGrid();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
       
    }
    protected void GridServicedeskTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridServicedeskTickets.PageIndex = e.NewPageIndex;
        BindServiceDeskTickets();
    }
}
