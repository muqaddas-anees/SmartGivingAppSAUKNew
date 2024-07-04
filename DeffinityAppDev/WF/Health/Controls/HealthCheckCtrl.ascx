<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_HealthCheckCtrl" Codebehind="HealthCheckCtrl.ascx.cs" %>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<%--<ul class="tabs_list4">
<li><div runat="server" id="img29"><a href="PortfolioHealthCheck.aspx?type=health" target="_self"><span>Manage Health checks</span></a></div></li>
</ul>--%>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a  href = "<%=this.ResolveUrl("~/WF/Health/HealthCheckSchedule.aspx")%>"><%= Resources.DeffinityRes.HealthChecks%></a></li>
    <li><a href="<%=this.ResolveUrl("~/WF/Health/HC/HealthCheckSchedule.aspx") %>"><%= Resources.DeffinityRes.Forms%></a></li>
   <li class="dropdown"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><span><%= Resources.DeffinityRes.Setup%> &nbsp;</span><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
		<li runat="server" id="img12"><a href="<%=this.ResolveUrl("~/WF/CustomerAdmin/PortfolioHealthCheck.aspx?type=health")%>" target="_self" ><%= Resources.DeffinityRes.ManageHealthchecks%></a></li>
		<!-- 2 is to set dropdown in admin check list page -->
		     <li runat="server" id="img7"><a href="<%=this.ResolveUrl("~/WF/Admin/adminmasterlists.aspx?setval=2&type=health")%>" target="_self"><%= Resources.DeffinityRes.ManageChecklist%></a></li>
             <li runat="server" id="Li1"><a href="<%=this.ResolveUrl("~/WF/Health/HC/FormList.aspx")%>" target="_self"> <%= Resources.DeffinityRes.ConfigureForms%></a></li>
             <li runat="server" id="img8"><a href="<%=this.ResolveUrl("~/WF/Health/HealthCustomer_logo.aspx?type=health")%>" target="_self">Select Logo on Email Notifications</a></li>
            <li runat="server" id="Li2"><a href="<%=this.ResolveUrl("~/WF/Health/HealthCheckConfigurator.aspx?type=health")%>" target="_self">Health Check Configurator</a></li>
		</ul>
	</li>
  
	</ul>
     </div>