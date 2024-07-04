<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="InventoryManagerPage" Title="Untitled Page"
    EnableEventValidation="false" Codebehind="InventoryManagerPage.aspx.cs" %>
<%@ Register Src="~/WF/Inventory/Controls/InventoryManagerTab.ascx" TagName="InventoryManagerTab" TagPrefix="uc1" %>
    <%@ Register Src="controls/Inventory.ascx" TagName="Inventory" TagPrefix="uc2" %>
   <%@ Register src="~/WF/CustomerAdmin/Controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   <uc1:InventoryManagerTab ID="InventoryManagerTab1" runat="server" />
   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.InventoryManager%>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.InventoryManager%>  <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" />
    </asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" NavigateUrl="InventoryManagerPage.aspx?status=0&project">
<i class="fa fa-arrow-left"></i> Return to Inventory</asp:HyperLink>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
  <div>
        <uc2:Inventory ID="Inventory1" runat="server" />
  </div> 
    
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
     

    <script type="text/javascript">
        
    </script>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    $(window).load(function () {
        //GridResponsiveCss();
        NestedGridResponsiveCss();
    });

 </script> 
</asp:Content>
<asp:Content ID="c5" runat="server" ContentPlaceHolderID="Scripts_Section">
  <style>
      
 .mq.js .table-responsive[data-pattern="priority-columns"] th[data-priority],
 .mq.js.lt-ie10 .sticky-table-header th[data-priority], .mq.js 
.table-responsive[data-pattern="priority-columns"] 
td[data-priority], .mq.js.lt-ie10 .sticky-table-header td[data-priority] {
    display: table-cell;
}
  </style>
</asp:Content>
