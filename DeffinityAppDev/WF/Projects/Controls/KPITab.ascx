<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_KPITab" Codebehind="KPITab.ascx.cs" %>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li class="<%=GetCssClass(0)%>"><a href="KPIFinancial.aspx" target="_self"><%= Resources.DeffinityRes.Financial%></a></li>
    <li class="<%=GetCssClass(1)%>"><a  href="KPIResource.aspx" target="_self"><%= Resources.DeffinityRes.Resources%></a></li>
   <li class="<%=GetCssClass(2)%>"><a href="KPICustomers.aspx" target="_self"><%= Resources.DeffinityRes.Customer%></a></li>
   <li class="<%=GetCssClass(3)%>"><a href="KPIServiceDesk.aspx" target="_self"><%= Resources.DeffinityRes.ServiceDesk%></a></li>
   <li class="<%=GetCssClass(4)%>"><a href="KPIInternalPerspective.aspx" target="_self"><%= Resources.DeffinityRes.KPIInternalPerspective%></a></li>
    <li class="<%=GetCssClass(5)%>"><a href="KPITarget.aspx" target="_self"><%= Resources.DeffinityRes.Target%></a></li>

</ul>
     </div>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 