<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DCAssignedTeam" Codebehind="DCAssignedTeam.aspx.cs" %>

<%@ Register src="~/Servicedesk/sdcontrols/sd_assignteam.ascx" tagname="sd_assignteam" tagprefix="uc1" %>
<%@ Register Src="~/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<script language="javascript" type="text/javascript" src="../js/Dynamic_styles.js"></script>
<uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section1">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
           <div class="flds_11">
              <span class="space_r50 float_l">Customer: <b>
                  <%=sessionKeys.PortfolioName %></b></span>
          </div>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block" style="width:100%">
    
     <uc1:sd_assignteam ID="SdAssignedteam" runat="server" Type="FLS"/>
    
       </td>
  </tr>
</table>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" Runat="Server">
</asp:Content>

