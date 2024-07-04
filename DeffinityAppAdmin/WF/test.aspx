<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="DeffinityAppDev.WF.test" %>

<%@ Register Src="~/App/controls/OrgMainTabs.ascx" TagPrefix="Pref" TagName="OrgMainTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:OrgMainTabs runat="server" ID="OrgMainTabs" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    test
</asp:Content>
