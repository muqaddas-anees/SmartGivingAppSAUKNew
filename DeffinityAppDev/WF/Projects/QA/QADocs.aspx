<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="QADocs1" Title="Documents" Codebehind="QADocs.aspx.cs" %>
<%@ Register Src="~/WF/Projects/Controls/Documents.ascx" TagName="Documents" TagPrefix="uc2" %>

<%@ Register Src="controls/QAtabs.ascx" TagName="QATab1" TagPrefix="QA1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_description" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
<QA1:QATab1 ID ="QATab1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
    <td><h1 class="section1"><span>Documents</span></h1>
    <div class="flds_11">
    <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</div>

    </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block">
    <uc2:Documents id="Documents1" runat="server"></uc2:Documents>
    </td>
  </tr>
</table>
<br /><br />




</asp:Content>

