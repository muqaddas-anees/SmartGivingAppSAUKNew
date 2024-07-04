<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeFile="POPurchaseDetails.aspx.cs" Inherits="POGeneralInformation" %>

<%@ Register src="~/WF/Projects/controls/POTab.ascx" TagName="POTab" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Projects/controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Projects/controls/InternalPODetails.ascx" TagName="InternalPODetails" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
     <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    PO Purchase Details 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab ID="tab" runat="server" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:POTab ID="poSubTAb" runat="server"  />
    <uc3:InternalPODetails ID="InternalPODetails1" runat="server"  />

</asp:Content>


