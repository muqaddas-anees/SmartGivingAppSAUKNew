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
using Deffinity.ProjectMeetingEntitys;
using Deffinity.ProjectMeetingManager;

public partial class ProjectMeetings : System.Web.UI.Page
{
    Qstring Qval = new Qstring();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.PageHead = "Project Management";
        if (!IsPostBack)
        {
            BindMeetingData();
        }
    }
    #region Functions
    private void BindMeetingData()
    {
        GridMeetings.DataSource = ProjectMeetingManager.ProjectMeetingSelectAll(QueryStringValues.Project);
        GridMeetings.DataBind();
    }
    #endregion
    protected void btnAddMeeting_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("AddMeeting.aspx?project={0}",QueryStringValues.Project));
    }
   

    protected string GetShortDescription(string Text,string ProjectRef,string meetingID)
    {
        string description = "";
        if (Text.Length > 200)
        {
            description = Text.ToString().Substring(0, 200) + "....<a href=AddMeeting.aspx?Project=" + ProjectRef + "&meeting=" + meetingID + ">Read More</a>..";
        }
        else
        {
            description = Text;
        }
        return description;
    }

    protected string GetImage(string status)
    {
        string url="";
        if (status=="1")
            url = "~/media/indcate_green.png";
        if (status == "2")
            url = "~/media/indcate_red.png";
        if (status == "3")
            url = "~/media/indcate_amber.gif";
        return url;
    }

    protected bool SetVisible(string status)
    {
        bool vis = false;
        if (int.Parse(string.IsNullOrEmpty(status)?"0":status)!= 0)
        {
            vis = true;
        }
        return vis;
    }


    protected void GridMeetings_Sorting(object sender, GridViewSortEventArgs e)
    {
        //ProjectMeetingSelectAll(QueryStringValues.Project)
        GridViewSortExpression = e.SortExpression;
        int pageIndex = GridMeetings.PageIndex;
        GridMeetings.DataSource = SortDataTable(ProjectMeetingManager.ProjectMeetingSelectAll(QueryStringValues.Project), false);
        GridMeetings.DataBind();
        GridMeetings.PageIndex = pageIndex;
    }
    private string GridViewSortExpression
    {
        get
        {
            return ViewState["SortExpression"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }
    private string GridViewSortDirection
    {
        get
        {
            return ViewState["SortDirection"] as string ?? "ASC";
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}",
                    GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}",
                   GridViewSortExpression, GetSortDirection());
                }
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }

}

