<%@ Page Language="C#" MasterPageFile="~/MainMenu2.master" AutoEventWireup="true" Inherits="RiskSummaryReport" Title="Risk Summary" Codebehind="RiskSummaryReport.aspx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" DisplayGroupTree="False"/>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
</asp:Content>

