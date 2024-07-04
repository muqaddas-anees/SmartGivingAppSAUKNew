<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="AdminPolicyType.aspx.cs" Inherits="DeffinityAppDev.WF.DC.AdminPolicyType" %>

<%@ Register Src="~/WF/DC/controls/AdminPolicyTypeCtrl.ascx" TagPrefix="DCUC1" TagName="AdminPolicyTypeCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="panel_title" runat="Server">
     Policy Types 
</asp:Content>

<asp:Content ID="Content10" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i>Return to Ticket Journal</asp:HyperLink>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <DCUC1:AdminPolicyTypeCtrl runat="server" id="AdminPolicyTypeCtrl" />

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
