<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master"
        AutoEventWireup="true" Inherits="CustomerChgPassword" Codebehind="CustomerChgPassword.aspx.cs" %>
<%@ Register Src="~/WF/Controls/ChangePassword.ascx" TagName="ChangePassword" TagPrefix="uc5" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Change Password
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" Runat="Server">
      Change Password
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Tabs" Runat="Server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Scripts_Section" Runat="Server">
    <script type="text/javascript">
        hidetabs();
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc5:ChangePassword  ID="ChangePassword" runat="server" />


</asp:Content>

