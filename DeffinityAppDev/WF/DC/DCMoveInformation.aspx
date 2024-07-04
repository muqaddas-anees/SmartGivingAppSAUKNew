<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DCMoveInformation" Codebehind="DCMoveInformation.aspx.cs" %>

<%@ Register src="~/controls/Assetsmove1.ascx" tagname="AssetsMove" tagprefix="uc1" %>
<%@ Register Src="~/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<%@ Register Src="~/DC/controls/DCMoveTab.ascx" TagName="MoveTab" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
  <script language="javascript" type="text/javascript" src="../js/Dynamic_styles.js"></script>
<uc2:FlsTab ID="flsTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section1">
              <span>
                 <label id="lblTitle" runat="server"></label>
              </span>
          </h1>
          
          
      </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block" >  
     
    <uc3:MoveTab ID="AssetsMoveTab1" runat="server" />  
     <div id="countrydivcontainer" style="border: 1px solid #d8dee5; margin-bottom: 1em;
                    padding: 10px"> 
<uc1:AssetsMove ID="AssetsId" runat="server" Type="FLS" />
</div>
 </td>
  </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

