<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectTabs" Codebehind="ProjectTabs.ascx.cs" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li>
        <a href="<%=getUrl(15) %>" target="_self"  title="Project Plan" ><%= Resources.DeffinityRes.ProjectPlan%> </a></li>
    <li> <a href="<%=getUrl(0) %>" target="_self"  title="Project Overview" ><%= Resources.DeffinityRes.Details%></a></li>
    <%--<li><a href=" <%=getUrl(1) %>" target="_self"  title="Project Tasks" ><span ><%= Resources.DeffinityRes.Tasks%></span></a></li>--%>
    <li style="display:none;" > <a href=" <%=getUrl(2) %>" target="_self"  title="Project Resources" ><%= Resources.DeffinityRes.Resources%></a></li>
    <li><a href="<%=getUrl(3) %>" target="_self" title="Budget" ><%= Resources.DeffinityRes.Budget%></a></li>
    <li><a href="<%=getUrl(4) %>" target="_self"  title="Project Tracker" > <%= Resources.DeffinityRes.ProjectTracker%></a></li>
   <li runat="server" id="LinkRisks"> <a href="<%=getUrl(5) %>" target="_self" title="Project Risks" ><%= Resources.DeffinityRes.Risks%></a></li>
    <%--<li class="<%=GetCssClass(6)%>"><div runat="server" id="img6"><a href=" <%=getUrl(6) %>" target="_self" title="Check Points"><span>Check Points</span></a></div></li>--%>    
    <li><a href=" <%=getUrl(10) %>" target="_self" title="Project Issues"><%= Resources.DeffinityRes.Issues%></a></li>       
    <%--<li class="<%=GetCssClass(17)%>"><label  runat="server" id="img17"> <a href=" <%=getUrl(17) %>" target="_self" title="Project Deviations"><span>Deviations</span></a></label></li>--%>
	<li class="<%=getUrl(8)%>"><a href="<%=getUrl(8) %>" target="_self" title="Project Documents"><%= Resources.DeffinityRes.Docs%></a></li>
    	<%--<li class="<%=GetCssClass(20)%>"><label  runat="server" id="img20"> <a href=" <%=getUrl(20) %>" target="_self" title="Project Inventory"><span>Inventory</span></a></label></li>--%>
	 <li class="dropdown" style="display:none;"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><%= Resources.DeffinityRes.Advanced%><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
		<li runat="server" visible="false"><a href=" <%=getUrl(16) %>"><%= Resources.DeffinityRes.ManageChecklist%> </a></li>
		<%--<li runat="server" id="img12"><a href=" <%=getUrl(11) %>" target="_self" title="Inter-Project Dependency"><%= Resources.DeffinityRes.InterProjectDependencies%></a></li>--%>
		    <%-- <li runat="server" id="img7"><a href=" <%=getUrl(7) %>" target="_self" title="Check Points"><%= Resources.DeffinityRes.AssetTracking%></a></li>
			 <li runat="server" id="img9"><a href=" <%=getUrl(9) %>" target="_self" title="Project Forum"><%= Resources.DeffinityRes.Forum%></a></li>--%>
			 <li runat="server" id="img6"><a href=" <%=getUrl(6) %>" target="_self" title="Check Points"><%= Resources.DeffinityRes.CheckPoints%></a></li>
             <%--<%--<li><div runat="server" id="img10"><a href=" <%=getUrl(10) %>" target="_self" title="Project Issues">Issues</a></div></li>--%>
             <li runat="server" id="img11"><a href=" <%=getUrl(13) %>" target="_self" title="Project Updates"><%= Resources.DeffinityRes.ProjectUpdates%></a></li>
             <li  runat="server" id="img13"><a href=" <%=getUrl(12) %>" target="_self" title="Select this to apply permissions to your project" ><%= Resources.DeffinityRes.ProjectPermissions%></a></li>
           <li  runat="server" id="img15"><a href=" <%=getUrl(18) %>" target="_self" title="Select this to apply reocrrence to your project" ><%= Resources.DeffinityRes.ProjectEmailUpdateSchedule%></a></li>
             <li  runat="server" id="img14"><a href=" <%=getUrl(19) %>" target="_self" title="" ><%= Resources.DeffinityRes.CustomAlerts%></a></li>
		</ul>
	</li>
    </ul>
     </div>

<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Projects%>');
</script>