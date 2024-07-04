<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ResourcePlannerTabs" Codebehind="ResourcePlannerTabs.ascx.cs" %>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li> <a href="Timesheet.aspx" target="_self">Timesheet Manager</a></li>
    <li runat="server" id="LinkVacationTracker"><a href="Vt.RequestVacation.aspx" target="_self">Vacation Tracker</a></li>
    <li runat="server" id="LinkResourcePlanner"><a href="ResourcePlanner.aspx" target="_self">Resource Planner</a></li>
    <li><a href="<%= this.ResolveClientUrl("~/WF/Training/trManageSkills.aspx") %>" target="_self">Skills Manager</a></li>
      <li class="dropdown"><a href="#" data-toggle="dropdown" class="dropdown-toggle">&nbsp;Setup&nbsp;&nbsp;<b class="caret"></b></a>
		<ul class="dropdown-menu">
			<li><a href="TimesheetApproverMailAlert.aspx">Timesheet Approver Alert</a></li>
            <li><a href="TimesheetReminders.aspx">Timesheet Reminders</a></li>
		</ul> 
	</li>
</ul>
    </div>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 