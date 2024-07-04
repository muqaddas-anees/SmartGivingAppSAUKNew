<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="BusinessServices.aspx.cs" Inherits="DeffinityAppDev.WF.DC.BusinessServices" %>

<%@ Register Src="~/WF/DC/controls/BlogListCtrl.ascx" TagPrefix="Pref" TagName="BlogListCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Business Services
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row"  >
        <div class="col-lg-12">
           
            <Pref:BlogListCtrl runat="server" id="BlogListCtrl" />

        </div>

    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
