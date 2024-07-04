<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="PortfolioDocsCommon" Title="Customer Admin" Codebehind="PortfolioDocsCommon.aspx.cs" %>
<%@ Register Src="controls/PortfolioDocsCommonctr.ascx" TagName="PortfolioDoc" TagPrefix="uc1" %>

<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
  <%--  <uc3:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
 Common Documents 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
 <uc1:PortfolioDoc ID="PortfolioDoc2" runat="server"></uc1:PortfolioDoc>
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();
        $(window).load(function () {
                     $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
                 });
    </script> 
</asp:Content>
