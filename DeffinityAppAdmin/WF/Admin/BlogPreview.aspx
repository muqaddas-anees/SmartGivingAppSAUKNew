<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="BlogPreview.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.BlogPreview" %>
<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
      <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblName" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
      <asp:HyperLink ID="btnBack" runat="server" Text="Back to List" NavigateUrl="~/WF/Admin/BlogList.aspx"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">    <asp:Literal ID="lblContent" runat="server"></asp:Literal>
        </div>
     <div class="row">


         </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
