<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Checkpoint_Docs" Codebehind="Checkpoint_Docs.aspx.cs" %>
<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc1" %>

<%@ Register Src="~/WF/Projects/Controls/Documents.ascx" TagName="Documents" TagPrefix="uc2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Checkpoint%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Documents%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:OpsViewTabs ID="OpsViewTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <uc2:Documents id="Documents1" runat="server"></uc2:Documents>
    <script  type="text/javascript">
//activeTab('Docs');
</script>
</asp:Content>


