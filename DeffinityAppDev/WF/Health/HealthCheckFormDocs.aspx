<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HealthCheckFormDocs" Codebehind="HealthCheckFormDocs.aspx.cs" %>

<%@ Register src="~/WF/Projects/Controls/Documents.ascx" tagname="Documents" tagprefix="uc1" %>
<%@ Register src="controls/HealthcheckSubtabs.ascx" tagname="HealthcheckSubtabs" tagprefix="uc2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Documents%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc2:HealthcheckSubtabs ID="HealthcheckSubtabs1" runat="server" />
       <uc1:Documents ID="Documents1" runat="server" />
   
    
    <script  type="text/javascript">
        $(document).ready(function () {
           
            sideMenuActive('<%= Resources.DeffinityRes.HealthChecks%>');
       });
       </script>
</asp:Content>


