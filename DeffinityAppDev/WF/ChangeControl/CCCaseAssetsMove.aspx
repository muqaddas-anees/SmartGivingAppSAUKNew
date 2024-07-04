<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CCAssetsMove" Codebehind="CCCaseAssetsMove.aspx.cs" %>

<%@ Register Src="controls/ChangeControlTab.ascx" TagName="Tab" TagPrefix="Deffinity" %>
<%@ Register Src="~/WF/DC/controls/UserAssetsMove.ascx" TagName="AssetsId" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<Deffinity:Tab ID="tabMenu" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <script src="Scripts/jquery-1.3.2.js" type="text/javascript"></script>
        <script src="Scripts/jquery.MultiFile.js" type="text/javascript"></script>
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
    <td><h1 class="section2"><span><label id="lblPageTitle" runat="server"></label> </span></h1>
   
    </td>
  </tr>
  <tr>    
    <td class="p_section2 data_carrier_block">
<uc3:AssetsId ID="AssetsId" runat="server"  />
</td>
</tr>
</table>
</asp:Content>


