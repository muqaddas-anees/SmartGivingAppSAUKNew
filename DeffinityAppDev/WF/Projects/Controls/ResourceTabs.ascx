<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ResourceTabs" Codebehind="ResourceTabs.ascx.cs" %>
<ul class="tabs_list1">
    <li class="<%=GetCssClass(0)%>"><a href="<%=getUrl(0) %>" target="_self"><span>Overview</span></a></li>
    <li class="<%=GetCssClass(1)%>"><a href="<%=getUrl(1) %>" target="_self"><span>Tasks</span></a></li>
    <li class="<%=GetCssClass(2)%>"><a href="<%=getUrl(2) %>" target="_self"><span>Scope</span></a></li>
    <li class="<%=GetCssClass(3)%>"><a href="<%=getUrl(3) %>" target="_self"><span>Variations</span></a></li>
    <li class="<%=GetCssClass(5)%>"><a href="<%=getUrl(4) %>" target="_self"><span>CSI</span></a></li>
   <%-- <li class="<%=GetCssClass(4)%>"><a href="<%=getUrl(5) %>" target="_self"><span>Assets</span></a></li>--%>
    <li class="<%=GetCssClass(6)%>"><a href="<%=getUrl(6) %>" target="_self"><span>Docs</span></a></li>
    <li class="<%=GetCssClass(7)%>"><a href="<%=getUrl(7) %>" target="_self"><span>Forum</span></a></li>
    
</ul>
