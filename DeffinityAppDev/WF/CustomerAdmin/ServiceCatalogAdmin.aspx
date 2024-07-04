<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ServiceCatalogAdimin" EnableEventValidation="false" Codebehind="ServiceCatalogAdmin.aspx.cs" %>

<%@ Register src="controls/ServiceCatalogAdmin.ascx" tagname="SCAdmin" tagprefix="SCA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <ul class="tabs_list5" style="float: right;">
        <li class="current5"><a id="A1" href="ServiceCatalogue.aspx" runat="server" target="_self"><span>
            Back to Service Catalogue</span></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManagerProxy ID="proxy1" runat="server">
<Services>
    <asp:ServiceReference Path="~/webservices/ServiceCatalogSrv.asmx" />
</Services>
</asp:ScriptManagerProxy>
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <h1 class="section1">
                    <span>
                        <label id="lblTitle" runat="server"> Central Catalogue
                        </label>
                    </span>
                </h1>
            </td>
        </tr>
        <tr>
            <td class="p_section1 data_carrier_block" style="width: 100%">
<SCA:SCAdmin ID="SCAdmin1" runat="server"/>
</td>
</tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" Runat="Server">
</asp:Content>

