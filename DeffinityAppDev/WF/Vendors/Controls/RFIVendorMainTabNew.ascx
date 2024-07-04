<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_RFIVendorMainTabNew" Codebehind="RFIVendorMainTabNew.ascx.cs" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="<%=getUrl(0) %>" target="_self">Supplier</a></li>
    <%--<li id="liMyTender" runat="server" visible="true" class="current1"> <a href=" <%=getUrl(9) %>" target="_self"><span>My Tenders</span></a></li>--%>
    <li runat="server" visible="false"><a href="<%=getUrl(6) %>" target="_self">My Tasks</a></li>
    <li runat="server" style="display:none;visibility:hidden;"><a href="<%=getUrl(1) %>" target="_self">Sites</a></li>
    <li runat="server" visible="false"><a href=" <%=getUrl(4) %>" target="_self">Vendor Attributes</a></li>
    <li runat="server"><a href=" <%=getUrl(5) %>" target="_self">Service Catalogue</a></li>
    <li runat="server" ><a href=" <%=getUrl(2) %>" target="_self">Key Contacts</a></li>
   <%-- <li class="<%=GetCssClass(10)%>"><a href=" <%=getUrl(10) %>" target="_self"><span>Contracts Management</span></a></li>--%>
    <%--<li id="liVendorPerformance" runat="server" class="current1"><a href=" <%=getUrl(11) %>" target="_self"><span>Vendor Performance</span></a></li>--%>
    <li runat="server" visible="false"><a href=" <%=getUrl(12) %>&Type=Vendor" target="_self">Custom Alerts</a></li>
    
</ul>
     </div>

<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.VendorManagement%>');
</script>