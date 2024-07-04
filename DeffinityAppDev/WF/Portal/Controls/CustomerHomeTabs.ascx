<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CustomerHomeTabs" Codebehind="CustomerHomeTabs.ascx.cs" %>

<ul class="tabs_list6" style="float:right;">
    <li class="current6"><a href="CustomerNewHome.aspx" target="_self"><span>Back to Customer Home</span></a></li>
    
</ul>
<ul class="tabs_list6" style="float:left;">
    <li><a href = "<%=this.ResolveUrl("~/HealthCheckSchedule_CPortal.aspx?customer=0")%>" target="_self"><span>Health Checks</span></a></li>
</ul>
<ul class="tabs_list6" style="float:left;">
    <li><a href = "<%=this.ResolveUrl("~/CustomerHealthCheckMgmt.aspx?customer=0")%>" target="_self"><span>Forms</span></a></li>
</ul>
<%--<ul class="tabs_list6">
    <li class="<%=GetCssClass(0)%>"><a href="CustomerHome.aspx" target="_self"><span>My Orders/Projects</span></a></li>
    <li class="<%=GetCssClass(1)%>"><a href="CustomerCases.aspx" target="_self"><span>Service Reqs</span></a></li>
    <li class="<%=GetCssClass(2)%>"><a href="CustomerHealthCheckMgmt.aspx" target="_self"><span>Health Checks</span></a></li>
    <li class="<%=GetCssClass(3)%>"><a href="FlowChartDownLoad.aspx" target="_self"><span>Flow Charts</span></a></li>
    <li class="<%=GetCssClass(4)%>"><a href="OrgnChartsDownLoad.aspx" target="_self"><span>Org Charts</span></a></li>
    <li class="<%=GetCssClass(5)%>"><a href="KPI.aspx" target="_self"><span>KPIs</span></a></li>
    <li class="<%=GetCssClass(6)%>"><a href="CustomerDocs.aspx" target="_self"><span>Docs</span></a></li>
</ul>--%>