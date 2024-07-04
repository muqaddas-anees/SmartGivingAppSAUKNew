<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="POJournal" Codebehind="POJournal.aspx.cs" %>

<%@ Register src="controls/POTab.ascx" TagName="POTab" TagPrefix="uc1" %>
<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<%@ Register Src="controls/CustomerPO.ascx" TagName="CustomerPO" TagPrefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPO%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btnProcurementReport" runat="server" Text="Procurement Report" OnClick="btnProcurementReport_Click"></asp:LinkButton>
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab ID="tab" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc1:POTab ID="poSubTAb" runat="server" />
<uc3:CustomerPO ID="CustomerPO1" runat="server" />
</asp:Content>


