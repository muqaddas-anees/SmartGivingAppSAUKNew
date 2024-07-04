<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ChangeControlAdmin" EnableEventValidation="false" Codebehind="ChangeControlAdmin.aspx.cs" %>

<%@ Register Src="~/WF/Admin/controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc1" %>
<%@ Register Src="controls/CCCategoryCtrl.ascx" TagName="CategoryCtrl"
    TagPrefix="ucc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.AdminDropdownLists%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<uc1:AdminDropdownTab ID="AdminDropdownTab1" runat="server" />--%>

     <ucc:CategoryCtrl ID="CategoryCtrl1" runat="server" />
</asp:Content>

