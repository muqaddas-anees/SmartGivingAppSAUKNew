<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="AdminServiceCharge.aspx.cs" Inherits="DeffinityAppDev.WF.DC.AdminServiceCharge" %>
<%@ Register Src="~/WF/DC/controls/AdminPolicyTypeCtrl.ascx" TagPrefix="DCUC1" TagName="AdminPolicyTypeCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <span>
    <%= Resources.DeffinityRes.AdminDropdownLists%> - 
    
    <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static" SkinID="ddl_50">
       
    </asp:DropDownList>
   </span>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i>Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
     <script>
        hidetabs();
    </script>
</asp:Content>
