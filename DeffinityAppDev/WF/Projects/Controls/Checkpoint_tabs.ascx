<%@ Control Language="C#" AutoEventWireup="true" Inherits="Checkpoint_controls_Checkpoint_tabs" Codebehind="Checkpoint_tabs.ascx.cs" %>
<ul class="tabs_list1">
    <li class="<%=GetCssClass(0)%>"><a href="<%=getUrl(0)%>" target="_self"><span>Overview</span></a></li>
    <li class="<%=GetCssClass(1)%>"><a href="<%=getUrl(1)%>" target="_self"><span>Tasks</span></a></li>
   <%-- <li class="<%=GetCssClass(2)%>"><a href="<%=getUrl(2)%>" target="_self"><span>Financials</span></a></li>
    <li class="<%=GetCssClass(3)%>"><label id="link_assets" runat="server"><a href="<%=getUrl(3)%>" target="_self"><span>Assets</span></a></label>
    	<ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" HoverCssClass="popupHover" PopupControlID="PopupMenu" TargetControlID="link_assets" PopupPosition="Bottom"> </ajaxToolkit:HoverMenuExtender>
    </li>--%>
    <%--<li class="<%=GetCssClass(4)%>"><a href="<%=getUrl(4)%>" target="_self"><span>Risks</span></a></li>--%>
    <li class="<%=GetCssClass(5)%>"><a href="<%=getUrl(5)%>" target="_self"><span>Issues</span></a></li>
    <li class="<%=GetCssClass(6)%>"><a href="<%=getUrl(6)%>" target="_self"><span>Feedback</span></a></li>
    <li class="<%=GetCssClass(7)%>"><a href="<%=getUrl(7)%>" target="_self"><span>CSI</span></a></li>
    
    <%--<li class="<%=GetCssClass(9)%>"><a href="<%=getUrl(9)%>" target="_self"><span>Timesheet</span></a></li>--%>
    <li class="<%=GetCssClass(13)%>"><a href="<%=getUrl(13)%>" target="_self"><span>Docs</span></a></li>
    <li class="<%=GetCssClass(8)%>"><a href="<%=getUrl(8)%>" target="_self"><span>Recommendations</span></a></li>
</ul>
 
 