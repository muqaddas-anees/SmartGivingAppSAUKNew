<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_InventoryManagerTab" Codebehind="InventoryManagerTab.ascx.cs" %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
				</div>
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
    <ul class="nav navbar-nav">
        <li><a href="InventoryManagerPage.aspx?Status=0" target="_self" title="Search" runat="server" visible="false"><span><%= Resources.DeffinityRes.Search%></span></a></li>
        <li><a href="NewInventoryManagerPage.aspx" target="_self" title="Search"><span>Search</span></a></li>
        <li class="dropdown"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><span><%= Resources.DeffinityRes.Setup%> &nbsp;</span><b class="caret"></b></a>
             <ul class="dropdown-menu dropdown-menu-right">
                    <li id="AdminAccessOnly" runat="server" visible="false">
                      <a href="InventoryCustomFormDesigner.aspx">Custom Field Configurator</a></li>
                    <li>
                     <a href="InventoryAdminDropdown.aspx">Admin Dropdown List</a>
                   </li>
             </ul>
        </li>
        <%--<li><a href="InventoryManagerPage.aspx?status=0&project"
                        target="_self"  title="Return to Inventory"><span> Return to Inventory</span></a></li>--%>
    </ul>
</div>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Inventory%>');
</script>
