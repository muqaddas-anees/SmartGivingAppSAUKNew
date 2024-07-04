<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ExportDataTab" Codebehind="ExportDataTab.ascx.cs" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
<li class="<%=GetCssClass("ExportofProjectOverviewdata.aspx")%>"><a href="<%: ResolveClientUrl("~/WF/Projects/ExportofProjectOverviewdata.aspx") %>" target="_self"><%= Resources.DeffinityRes.ExportofProjectdata%></a></li>
    <li class="<%=GetCssClass("TimesheetandCostsExport.aspx")%>"><a href="<%: ResolveClientUrl("~/WF/Projects/TimesheetandCostsExport.aspx") %>" target="_self"><%= Resources.DeffinityRes.TimesheetandCosts%></a></li>
    <li class="<%=GetCssClass("IssuesRisksExport.aspx")%>"><a href="<%: ResolveClientUrl("~/WF/Projects/IssuesRisksExport.aspx") %>" target="_self"><%= Resources.DeffinityRes.IssuesAndRisks%></a></li>
    <li class="<%=GetCssClass("AnnualLeaveandAbsenceexport.aspx")%>"><a href="<%: ResolveClientUrl("~/WF/Projects/AnnualLeaveandAbsenceexport.aspx") %>" target="_self"><%= Resources.DeffinityRes.AnnualLeaveandAbsence%></a></li>
    <li class="<%=GetCssClass("BOMExportDataReport1.aspx")%>"><a href="<%: ResolveClientUrl("~/WF/Projects/BOMExportDataReport1.aspx") %>" target="_self"><%= Resources.DeffinityRes.BOMSummary%></a></li>
    <li class="<%=GetCssClass("Healthcheck_Export.aspx")%>"><a href="<%: ResolveClientUrl("~/WF/Health/Healthcheck_Export.aspx") %>" target="_self"><%= Resources.DeffinityRes.HealthChecks%></a></li>   
    <%--<li class="current6" style="float:right;" ><a href="POJournal.aspx" target="_self">Back to Finance</a></li>--%>
    

</ul>
     </div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 