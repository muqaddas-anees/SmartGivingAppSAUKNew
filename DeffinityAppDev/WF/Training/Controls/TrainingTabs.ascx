<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_TrainingTabs" Codebehind="TrainingTabs.ascx.cs" %>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="~/WF/Training/trDueSoon.aspx" target="_self" runat="server">Training Dashboard</a></li>
    <li><a href="~/WF/Training/trBookingRecord.aspx" target="_self" runat="server">Bookings</a></li>
    <li><a href="~/WF/Training/trCategory.aspx" target="_self" runat="server">Admin</a></li>
    
    <%-- <li class="menu_tab"><a href="#"><span>&nbsp;Setup&nbsp;&nbsp;</span></a>
     
		<ul>
			<li><a href="adminmasterlists.aspx?type=prj">Manage Checklist</a></li>
		</ul> 
	</li>--%>
</ul>
</div>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Training%>');
</script>
