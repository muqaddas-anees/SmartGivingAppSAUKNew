<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="DC_HistoryDisplay" Codebehind="HistoryDisplay.aspx.cs" %>

<%@ Register src="controls/FlsHistory.ascx" tagname="FlsHistory" tagprefix="uc1" %>
<%@ Register src="controls/DeliveryHistory.ascx" tagname="DeliveryHistory" tagprefix="uc2" %>
<%@ Register src="controls/AccessControlHistory.ascx" tagname="AccessControlHistory" tagprefix="uc3" %>
<%@ Register src="controls/PermitToWorkHistory.ascx" tagname="PermitToWorkHistory" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc2:DeliveryHistory ID="DeliveryHistory1" runat="server" Visible="false" />
    <uc4:PermitToWorkHistory ID="PermitToWorkHistory1" runat="server" Visible="false" />
    <uc3:AccessControlHistory ID="AccessControlHistory1" runat="server" Visible="false" />
    <uc1:FlsHistory ID="FlsHistory1" runat="server" Visible="false" />
</asp:Content>

