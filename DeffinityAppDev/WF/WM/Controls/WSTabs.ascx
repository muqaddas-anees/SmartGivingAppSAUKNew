<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WSTabs.ascx.cs" Inherits="DeffinityAppDev.WF.WM.Controls.WSTabs" %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
				</div>
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
    <ul class="nav navbar-nav">
        <li><a href="~/WF/WM/WMdetails.aspx" target="_self" title="Warehouse Manager" runat="server"><span>Warehouse Manager</span></a></li>
        <li style="display:none;visibility:hidden;"><a href="../WMItems.aspx" target="_self" title="Search"><span>Inventory</span></a></li>
       <%-- <li class="dropdown"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><span><%= Resources.DeffinityRes.Setup%> &nbsp;</span><b class="caret"></b></a>
             <ul class="dropdown-menu dropdown-menu-right">
                    <li id="AdminAccessOnly" runat="server" visible="false">
                      <a href="InventoryCustomFormDesigner.aspx">Custom Field Configurator</a></li>
                    <li>
                     <a href="InventoryAdminDropdown.aspx">Admin Dropdown List</a>
                   </li>
             </ul>
        </li>--%>
        <%--<li><a href="InventoryManagerPage.aspx?status=0&project"
                        target="_self"  title="Return to Inventory"><span> Return to Inventory</span></a></li>--%>
    </ul>
</div>
<script type="text/javascript">
   // sideMenuActive('<%= Resources.DeffinityRes.Inventory%>');
</script>
