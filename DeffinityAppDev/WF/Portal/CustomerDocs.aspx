<%@ Page Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true"
    CodeFile="CustomerDocs.aspx.cs" Inherits="Portal_CustomerDocs" Title="Customer Documents" %>
<%@ Register Src="~/WF/Projects/Controls/Documents.ascx" TagName="Documents" TagPrefix="uc2" %>
<%--<%@ Register Src="~/controls/CustomerHomeTabs.ascx" TagName="CustomerTabs" TagPrefix="uc3" %>--%>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.2" Namespace="Infragistics.WebUI.UltraWebNavigator"
    TagPrefix="ignav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Documents%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
   <%-- <uc3:CustomerTabs ID="CustomerTabs1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
     <uc2:Documents ID="PortfolioDoc1" runat="server">
            </uc2:Documents>
</asp:Content>
