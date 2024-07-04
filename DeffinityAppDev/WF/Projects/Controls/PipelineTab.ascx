<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PipelineTab" Codebehind="PipelineTab.ascx.cs" %>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<div>
<asp:Label ID="lblmsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
</div>
 <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
<li ><a href="~/WF/Projects/ProjectPipeline.aspx?Status=0" target="_self" runat="server"><%= Resources.DeffinityRes.All%></a></li>
    <li ><a href="~/WF/Projects/ProjectPipeline.aspx?Status=2" target="_self" runat="server"><%= Resources.DeffinityRes.Live%></a></li>
    <li ><a href="~/WF/Projects/ProjectPipeline.aspx?Status=1" target="_self" runat="server"><%= Resources.DeffinityRes.PENDING%></a></li>
    <li ><a href="~/WF/Projects/ProjectPipeline.aspx?Status=3" target="_self" runat="server"><%= Resources.DeffinityRes.Completed%></a></li>
    <li id="linkpm" runat="server" class=""><a href="~/WF/Projects/ProjectPipeline.aspx?Status=5" target="_self" runat="server"><%= Resources.DeffinityRes.Archived%></a></li>
    <li ><a href="~/WF/Projects/ProjectPipeline.aspx?Status=4" target="_self" runat="server"><%= Resources.DeffinityRes.Cancelled%></a></li>
    <li class=""><a href="~/WF/Projects/ProjectPipeline.aspx?Status=7" target="_self" runat="server"><%= Resources.DeffinityRes.OnHold%></a></li>
    <li id="linkpm_retention" runat="server" class=""><a href="~/WF/Projects/ProjectPipeline.aspx?Status=11" target="_self" runat="server"><%= Resources.DeffinityRes.Retention%></a></li>
     <li class="dropdown"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><span><%= Resources.DeffinityRes.Setup%> &nbsp;</span><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            <li><a href="~/WF/Admin/AdminDropdown.aspx?Panel=0" target="_self" runat="server">Project Admin</a></li>
			<li><a href="~/WF/Admin/adminmasterlists.aspx?type=prj" target="_self" runat="server">Manage Checklist</a></li>
			<%--<li><a href="PortfolioStructure.aspx?type=prj">Checklist Structure</a></li>--%>
			<li><a href="~/WF/CustomerAdmin/PortfolioRag.aspx?type=prj" target="_self" runat="server">Key Milestones</a></li>
            <li><a href="~/WF/Admin/ManageTeamMembersNew.aspx?type=prj"  target="_self" runat="server">Team Structure</a></li>
            <li><a href="~/WF/Projects/Checkpoint/Checkpoint_Summary.aspx" target="_self" runat="server"><%= Resources.DeffinityRes.Checkpoint%></a></li>
            <li><a href="~/WF/Projects/QA/QASummary.aspx" target="_self" runat="server"><%= Resources.DeffinityRes.QA%></a></li>
            <li><a href="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8" target="_self" runat="server"><%= Resources.DeffinityRes.ProjectProposals%></a></li>
		</ul> 
	</li>
</ul>
     </div>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Projects%>');
</script>


