<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="ContactMaintenanceSchedule.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.ContactMaintenanceSchedule" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/ContactTabCtrl.ascx" TagPrefix="Pref" TagName="ContactTabCtrl" %>
<%@ Register Src="~/WF/CustomerAdmin/Controls/MaintenanceScheduleFormCtrl.ascx" TagPrefix="Pref" TagName="MaintenanceScheduleFormCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Contact Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:ContactTabCtrl runat="server" ID="ContactTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
      <asp:Literal ID="lblContact" runat="server"></asp:Literal> - <asp:Literal ID="lblAddress" runat="server" Text="Maintenance Schedule"></asp:Literal>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <Pref:MaintenanceScheduleFormCtrl runat="server" ID="MaintenanceScheduleFormCtrl" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
