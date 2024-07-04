<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ProjectResourceSearch" Codebehind="ProjectResourceSearch.aspx.cs" %>

<%@ Register src="controls/ResourceSearch.ascx" tagname="ResourceSearch" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:ResourceSearch ID="ResourceSearch1" runat="server" />
</asp:Content>

