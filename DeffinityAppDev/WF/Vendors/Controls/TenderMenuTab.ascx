<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_TenderMenuTab" Codebehind="TenderMenuTab.ascx.cs" %>
<ul class="tabs_list5" >
   <li class="<%=GetCssClass("RFIProjects.aspx")%>"><a href="RFIProjects.aspx" target="_self"><span>Manage Tenders</span></a></li>
   <%-- <li class="<%=GetCssClass("RFIResponseProjects.aspx")%>"><a href="RFIResponseProjects.aspx" target="_self"><span>Review Responses and Queries</span></a></li>--%>
    <li class="<%=GetCssClass("RFIAssignedToPanelist.aspx")%>"><a href="RFIAssignedToPanelist.aspx" target="_self"><span>Scoring Process</span></a></li>
    <li class="<%=GetCssClass("RFIVendors.aspx")%>"><a href="RFIVendors.aspx" target="_self"><span>Vendor Management</span></a></li>    
    <li class="current5" style="float:right"><a href=" RFIMain.aspx" target="_self"><span>Return to Tender Home Page</span></a></li>
    
    
    
    
    
 

</ul>
