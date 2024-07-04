<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ManageCatalogues"  EnableEventValidation="false" Codebehind="ServiceCatalogue.aspx.cs" %>

<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>
<%@ Register src="controls/ServiceCatalogue.ascx" tagname="ServiceCatalogueControl" tagprefix="SCC" %>
<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">

<%--    <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ServiceCatalogue%> - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<SCC:ServiceCatalogueControl ID="ServiceCatalogue1" runat='server' />
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>
