<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="PortfolioDocs" Title="Untitled Page" Codebehind="PortfolioDocs.aspx.cs" %>
<%@ Register Src="~/WF/Projects/Controls/Documents.ascx" TagName="PortfolioDoc" TagPrefix="uc1" %>

<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.DocumentLibrary%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.DocumentLibrary%>  <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
<uc1:PortfolioDoc ID="PortfolioDoc2" runat="server"></uc1:PortfolioDoc>
</asp:Content>
