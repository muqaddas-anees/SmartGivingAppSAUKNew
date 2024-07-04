<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ChangeControlTab" Codebehind="ChangeControlTab.ascx.cs" %>
 <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="ChangeControl.aspx" target="_self"><%= Resources.DeffinityRes.ChangeControl%></a></li>
    <li><a href="CCScheduledTasks.aspx" target="_self"><%= Resources.DeffinityRes.Tasks%></a></li>
    <li><a href="CCRiskAssessment.aspx" target="_self"><%= Resources.DeffinityRes.Risks%></a></li>
    <li><a href="CCAddApproval.aspx" target="_self"><%= Resources.DeffinityRes.Approval%></a></li>
</ul>
     </div>


 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.ChangeControl%>');
</script>