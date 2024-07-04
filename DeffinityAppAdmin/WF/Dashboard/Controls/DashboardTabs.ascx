<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_DashboardTabs" Codebehind="DashboardTabs.ascx.cs" %>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="~/WF/Dashboard/DashboardMain.aspx" target="_self" runat="server" id="div_projects"><%= Resources.DeffinityRes.Projects%></a></li>
    <li><a href="~/WF/Dashboard/Tasks.aspx" target="_self"  runat="server" id="div_tasks"><%= Resources.DeffinityRes.Tasks%></a></li>
    <li><a href="~/WF/Dashboard/DashboardIssues.aspx" target="_self"  runat="server" id="div_issues"><%= Resources.DeffinityRes.Issues%></a></li>
    <li><a href="~/WF/Dashboard/Risks.aspx" target="_self"  runat="server" id="div_risks"><%= Resources.DeffinityRes.Risks%></a></li>
    <li><a href="~/WF/Dashboard/DashboardHealthCheck.aspx" target="_self" runat="server" id="div_helath"><%= Resources.DeffinityRes.HealthCheck%></a></li>
    <li><a href="~/WF/Dashboard/ResourceDashboard.aspx" target="_self" runat="server" id="div_resource"><%= Resources.DeffinityRes.MyTeam%></a></li>
    <li><a href="~/WF/Dashboard/PortfolioMain.aspx" target="_self" runat="server" id="div_portfolio"><%= Resources.DeffinityRes.Customer%></a></li>
    <li><a href="~/WF/Dashboard/Programme.aspx?Panel=7" target="_self" runat="server" id="div_programe"><%= Resources.DeffinityRes.Programme%></a></li>
    <li><a href="~/WF/Dashboard/CSI.aspx" target="_self" runat="server" id="div_csi"><span><%= Resources.DeffinityRes.CSI%></span></a></li>
   
</ul>
     </div>

<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Dashboard%>');
</script>