<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PermitCustomerTab.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.PermitCustomerTab" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation
						<i class="fa-bars"></i></span>
					</button>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a id="lbtnPermit" href="~/WF/DC/PermitToWork.aspx" target="_self" runat="server"><%= Resources.DeffinityRes.PermittoWork%></a></li>
    <li><a id="lbtnChecklists" href="~/WF/DC/ChecklistCustomer.aspx" target="_self" runat="server"><%= Resources.DeffinityRes.CheckList%></a></li>
   
</ul>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.PermittoWork%>');
</script>
