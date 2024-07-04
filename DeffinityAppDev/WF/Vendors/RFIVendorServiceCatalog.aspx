<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/Main.master" Inherits="RFIVendorServiceCatalog" Codebehind="RFIVendorServiceCatalog.aspx.cs" EnableEventValidation="false" %>
<%@ Register src="controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorMainTab" tagprefix="uc1" %>
<%@ Register src="~/WF/CustomerAdmin/Controls/ServiceCatalogue.ascx" tagname="ServiceCatalogueControl" tagprefix="SCC" %>
<%@ Register src="controls/VendorRef.ascx" tagname="VendorRef" tagprefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    Supplier  <uc2:VendorRef ID="VendorRef2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <nav class="navbar navbar-default" role="navigation" id="navTab">
     <uc1:RFIVendorMainTab ID="RFIVendorMainTab1" runat="server" />
        </nav>
    
<SCC:ServiceCatalogueControl ID="ServiceCatalogue1" runat='server' />

 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>

