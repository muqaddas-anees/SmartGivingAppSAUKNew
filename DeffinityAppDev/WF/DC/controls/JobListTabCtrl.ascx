<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobListTabCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.JobListTabCtrl" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_FLSTab" Codebehind="FLSTab.ascx.cs" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation
						<i class="fa-bars"></i></span>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="<%=getUrl(0)%>"  title="Jobs">Jobs </a></li>
    <li id="link_diagnostics" runat="server" visible="false"><a href="<%=getUrl(1)%>"  title="HVAC Diagnostics"> Maintenance Jobs </a> </li>
</ul>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Customer%>');
</script>

