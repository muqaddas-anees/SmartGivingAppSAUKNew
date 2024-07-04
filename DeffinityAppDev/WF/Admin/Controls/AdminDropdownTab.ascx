<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_AdminDropdownTab" Codebehind="AdminDropdownTab.ascx.cs" %>
<div id="topmenu" class="form-wizard">
<ul class="tabs">
<li class="ms-hover" id="lbtnProjects_li" runat="server"><asp:HyperLink ID="lbtnProjects"  target="_self" runat="server"   Text="Projects"></asp:HyperLink></li>
<li class="ms-hover" id="lbtnTS_li" runat="server"><asp:HyperLink ID="lbtnTS" Target="_self" runat="server" Text="Timesheets"></asp:HyperLink> </li>
<li class="ms-hover" id="lbtnResource_li" runat="server"><asp:HyperLink ID="lbtnResource"  target="_self" runat="server"  Text="Resources"></asp:HyperLink></li>
<li class="ms-hover" id="lbtnSD_li" runat="server"><asp:HyperLink ID="lbtnSD" Target="_self" runat="server" Text="SD" ></asp:HyperLink> </li>
<li class="ms-hover" id="lbtnIssRisks_li" runat="server"><asp:HyperLink ID="lbtnIssRisks" Target="_self" runat="server" Text="Issues & Risks"></asp:HyperLink> </li>
<li class="ms-hover" id="lbtnAssets_li" runat="server"><asp:HyperLink ID="lbtnAssets" Target="_self" runat="server" Text="Product"></asp:HyperLink> </li>
<li class="ms-hover" id="lbtnVendors_li" runat="server"><asp:HyperLink ID="lbtnVendors" Target="_self" runat="server" Text="Vendors"></asp:HyperLink> </li>
<%--<li class="ms-hover" id="lbtnBBBEE_li" runat="server"><asp:HyperLink ID="lbtnBBBEE" Target="_self" runat="server" Text="Vendors"></asp:HyperLink> </li>--%>
<li class="ms-hover" id="lbtnInventory_li" runat="server"><asp:HyperLink ID="lbtnInventory" Target="_self" runat="server" Text="Inventory"></asp:HyperLink> </li>
<li class="ms-hover" id="lbtnContracts_li" runat="server" visible="false"><asp:HyperLink ID="lbtnContracts" Target="_self" runat="server" Text="Contracts"></asp:HyperLink> </li>
</ul>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>


