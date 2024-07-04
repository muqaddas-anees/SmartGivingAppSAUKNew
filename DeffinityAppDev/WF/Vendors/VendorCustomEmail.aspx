<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="VendorCustomEmail" Codebehind="VendorCustomEmail.aspx.cs" %>

<%@ Register src="~/WF/Projects/Controls/CustomerAlerts.ascx" tagname="CustomAlert" tagprefix="uc3" %>
<%@ Register src="controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorTabsNew" tagprefix="ucNew1" %>
<%@ Register src="controls/VendorRef.ascx" tagname="VendorRef" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <ucNew1:RFIVendorTabsNew ID="RFIVendorTabs1" runat="server" />   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="data_carrier">
        <h1 class="section5">
            <span>Vendor Email alert</span></h1>
            <div class="flds_11">
        <uc2:VendorRef ID="VendorRef2" runat="server" />
</div>
        <div class="p_section5 data_carrier_block">
        <uc3:customalert runat="server" ID="UC2Alert" />
            <div class="clr">
            </div>
            </div>
            </div>
</asp:Content>


