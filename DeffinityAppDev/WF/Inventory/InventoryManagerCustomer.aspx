<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true"
     Inherits="DC_InventoryManagerCustomer" EnableEventValidation="false" Codebehind="InventoryManagerCustomer.aspx.cs" %>
 <%@ Register Src="controls/InventoryCustomer.ascx" TagName="Inventory" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc2:Inventory ID="Inventory" runat="server" />
</asp:Content>


