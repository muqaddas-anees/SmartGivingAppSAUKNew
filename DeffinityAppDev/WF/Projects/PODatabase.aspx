<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" EnableEventValidation="false"  AutoEventWireup="true" Inherits="PODatabase" Codebehind="PODatabase.aspx.cs" %>

<%@ Register src="~/WF/Projects/Controls/POTab.ascx" TagName="POTab" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Projects/Controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Projects/Controls/CustomerPODetails.ascx" TagName="CustomerPODetails" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
     <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Customer PO 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab ID="tab" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <uc1:POTab ID="poSubTAb" runat="server"  />
    <uc3:CustomerPODetails ID="CustomerPODetails1" runat="server"  />

</asp:Content>


