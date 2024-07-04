<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_MyProjectsTab" Codebehind="MyProjectsTab.ascx.cs" %>

 <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li runat="server" id="link_SD"> <a href='<%= ResolveClientUrl("~/WF/DC/FLSResourceList.aspx?type=FLS")%>' target="_self"><%= Resources.DeffinityRes.ServiceDesk%></a> </li>
    <li runat="server" id="img29" style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyTasks.aspx")%>' target="_self"><%= Resources.DeffinityRes.MyTasks%></a> </li>
     <li runat="server" id="link_Inventory" style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/ResourceInventory.aspx")%>' target="_self"><%= Resources.DeffinityRes.Inventory%></a> </li>
    
    <li runat="server" id="link_health"  style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Health/HealthCheckSchedule.aspx?R=Y&type=resource")%>' target="_self"><%= Resources.DeffinityRes.HealthChecks%></a> </li>
      <li runat="server" id="img43"  style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyProjectDocs.aspx")%>' target="_self"><%= Resources.DeffinityRes.MyProjectDocs%></a> </li>
    <li runat="server" id="img39"  style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyTasksDocuments.aspx?mode=central")%>' target="_self"><%= Resources.DeffinityRes.CentralDocs%></a> </li>
    <li runat="server" id="img37"  style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/TimeSheetResourcesDaily.aspx")%>' target="_self"><%= Resources.DeffinityRes.Timesheet%></a> </li>
    <li runat="server" id="img38" style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/VT.ResourceVacationRequest.aspx")%>' target="_self"><%= Resources.DeffinityRes.AnnualLeave%></a> </li>
    <li runat="server" id="img40" style="display:none;visibility:hidden"> <a href='<%= ResolveClientUrl("~/WF/Resource/ResourceNewChitChat.aspx")%>' target="_self"><%= Resources.DeffinityRes.ChitChat%></a> </li>
     <li class="dropdown"  style="display:none;visibility:hidden"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><span><%= Resources.DeffinityRes.More%> &nbsp;</span><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            <li runat="server" id="img34"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyProjects.aspx?Status=2")%>' target="_self"><%= Resources.DeffinityRes.LiveProjects%></a> </li>
            <li runat="server" id="img35"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyProjects.aspx?Status=3")%>' target="_self"><%= Resources.DeffinityRes.Completed%> </a> </li>
            <li runat="server" id="img33"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyRisks.aspx")%>' target="_self"><%= Resources.DeffinityRes.MyRisks%></a> </li>
             <li runat="server" id="link_changecontrol" visible="false"> <a href='<%= ResolveClientUrl("~/WF/Resource/CCApproval.aspx")%>' target="_self"><%= Resources.DeffinityRes.ChangeControl%></a> </li>
            <li runat="server" id="img32"> <a href='<%= ResolveClientUrl("~/WF/Resource/MyQAIssues.aspx")%>' target="_self"><%= Resources.DeffinityRes.MyIssues%></a> </li>
		</ul> 
	</li>
</ul>
    </div>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.ServiceDesk %>');
</script>
