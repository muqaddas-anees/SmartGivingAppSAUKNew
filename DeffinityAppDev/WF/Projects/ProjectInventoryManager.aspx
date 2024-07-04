<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectInventoryManager"   EnableEventValidation="false" Codebehind="ProjectInventoryManager.aspx.cs" %>
<%@ Register Src="~/controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
 
<%@ Register Src="controls/Inventory.ascx" TagName="Inventory"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
    <td><h1 class="section1"><span>Inventory</span></h1>
   <%-- <div class="flds_11">
    <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</div>--%>
    </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block">
     <uc2:Inventory ID="Inventory1" runat="server" />
    </td>
    </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

