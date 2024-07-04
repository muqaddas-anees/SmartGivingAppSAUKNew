<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" EnableEventValidation="false"
     AutoEventWireup="true" CodeBehind="NewInventoryManagerPage.aspx.cs"
         Inherits="DeffinityAppDev.WF.Inventory.NewInventoryManagerPage" %>

<%@ Register Src="~/WF/Inventory/Controls/InventoryManagerTab.ascx" TagName="InventoryManagerTab" TagPrefix="uc1" %>
    <%@ Register Src="~/WF/Inventory/Controls/InventoryNewCntl.ascx" TagName="InventoryNew" TagPrefix="uc2" %>
   <%@ Register src="~/WF/CustomerAdmin/Controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.InventoryManager%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
      <uc1:InventoryManagerTab ID="InventoryManagerTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
       <%= Resources.DeffinityRes.InventoryManager%>  <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
      <asp:HyperLink runat="Server" NavigateUrl="NewInventoryManagerPage.aspx">
          <i class="fa fa-arrow-left"></i> Return to Inventory</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <uc2:InventoryNew ID="InventoryNew1" runat="server"></uc2:InventoryNew>
    </div> 
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    $(window).load(function () {
        //GridResponsiveCss();
        NestedGridResponsiveCss();
    });
 </script> 
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <style>
        
    </style>
</asp:Content>
