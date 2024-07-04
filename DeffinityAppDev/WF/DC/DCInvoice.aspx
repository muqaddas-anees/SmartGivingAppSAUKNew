<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCInvoice.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCInvoice" %>

<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagPrefix="Pref" TagName="FLSTab" %>
<%@ Register Src="~/WF/DC/controls/InvoiceCtrl.ascx" TagPrefix="Pref" TagName="InvoiceCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Service desk
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:FLSTab runat="server" ID="FLSTab" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">Invoice</label>  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <Pref:InvoiceCtrl runat="server" id="InvoiceCtrl" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
