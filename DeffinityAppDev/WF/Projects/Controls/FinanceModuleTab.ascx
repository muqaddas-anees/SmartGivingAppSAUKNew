<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_FinanceModuleTab" Codebehind="FinanceModuleTab.ascx.cs" %>

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
    <li class="<%=GetCssClass(0)%>"><a href="FMResources.aspx" target="_self"><%= Resources.DeffinityRes.TimesheetView%></a></li>
    <li class="<%=GetCssClass(1)%>"><a  href="FMProjects.aspx" target="_self"><%= Resources.DeffinityRes.Projects%></a></li>
  <%-- <li class="<%=GetCssClass(2)%>"><a href="CustomerContracts.aspx" target="_self">Customer Contracts</span></a></li>--%>
    <li class="<%=GetCssClass(3)%>"><a  href="FMInvoicing.aspx" target="_self"><%= Resources.DeffinityRes.Invoicing%></a></li>
        <li class="<%=GetCssClass(4)%>"><a  href="POJournal.aspx" target="_self"><%= Resources.DeffinityRes.PODatabase%></a></li>
        <li class="<%=GetCssClass(6)%>"><a  href="BoMSupplierPayments.aspx" target="_self"><%= Resources.DeffinityRes.SupplierInvoices%></a></li>
        <li class="<%=GetCssClass(7)%>"><a  href="FMWorkInProgress.aspx" target="_self"><%= Resources.DeffinityRes.WorkInProgress%></a></li>
        <li class="<%=GetCssClass(8)%>"><a  href="FMSalesForeCast.aspx" target="_self"><%= Resources.DeffinityRes.RevenueForecast%></a></li>
        <li class="<%=GetCssClass(9)%>"><a  href="KPIFinancial.aspx" target="_self"><%= Resources.DeffinityRes.KPI%></a></li>
        <li class="<%=GetCssClass(5)%>"><a  href="ExportofProjectOverviewdata.aspx" target="_self"><%= Resources.DeffinityRes.ExportData%></a></li>
</ul>
     </div>

