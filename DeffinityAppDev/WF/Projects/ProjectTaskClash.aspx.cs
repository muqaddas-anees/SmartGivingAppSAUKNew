using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectTaskClash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hlinkGantt.NavigateUrl = "~/WF/Projects/ProjectOverviewV4.aspx?project=" + QueryStringValues.Project.ToString();
        hlinkGantt.Text = "Show Gantt";
    }
}