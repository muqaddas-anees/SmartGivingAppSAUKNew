using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ProjectMgt.DAL;

public partial class MyTasksView : System.Web.UI.Page
{
    protected string RetUrl1()
    {
        string s = "";
        try
        {

            projectTaskDataContext _db = new projectTaskDataContext();
            var getDates = (from r in _db.Projects
                            where r.ProjectReference == int.Parse(Request.QueryString["Project"].ToString())
                            select r).ToList().FirstOrDefault();
            if (getDates != null)
            {
                string sDate = string.Format("{0:MM/dd/yyyy}", getDates.StartDate.Value);
                string eDate = string.Format("{0:MM/dd/yyyy}", getDates.ProjectEndDate.Value);
                string status = getDates.ProjectStatusID.ToString();
                // s = string.Format("ProjectNewGanttReadOnly.aspx?project={0}&start={1}&end={2}&status={3}", Request.QueryString["Project"], sDate, eDate, status);
                //s = string.Format("ProjectGanntNewReadOnly.aspx?ProjectReference={0}&start={1}&end={2}&status={3}", Request.QueryString["Project"], sDate, eDate, status);
                s = string.Format("/WF/Projects/Gantt/rindex.aspx?project={0}#en", Request.QueryString["Project"]);
            }


        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
        return s;
    }
}
