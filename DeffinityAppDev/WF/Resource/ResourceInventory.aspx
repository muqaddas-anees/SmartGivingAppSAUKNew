<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
     CodeBehind="ResourceInventory.aspx.cs" Inherits="DeffinityAppDev.WF.Resource.ResourceInventory" %>
<%@ Register Src="controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Inventory/Controls/InventoryNewCntl.ascx" TagPrefix="uc1" TagName="Inventory" %>


<asp:Content ID="Content9" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content10" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content11" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Inventory%> 
</asp:Content>
<asp:Content ID="Content12" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:ProjectStatus id="ProjectStatus1" runat="server"> </uc1:ProjectStatus>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Inventory runat="server" ID="Inventory" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
      <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    $(window).load(function () {
        //GridResponsiveCss();
        NestedGridResponsiveCss();
    });
 </script> 
</asp:Content>
