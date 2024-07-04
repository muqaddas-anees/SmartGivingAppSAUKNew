<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HealthCheckFormIssues" Codebehind="HealthCheckFormIssues.aspx.cs" %>

<%@ Register src="~/WF/Projects/Controls/ProjectPmNewIssues.ascx" tagname="ProjectPmNewIssues" tagprefix="uc1" %>
<%@ Register src="controls/HealthcheckSubtabs.ascx" tagname="HealthcheckSubtabs" tagprefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Issues%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <script  type="text/javascript">
       $(document).ready(function () {
           $('#navTab').hide();
           sideMenuActive('<%= Resources.DeffinityRes.HealthChecks%>');
       });
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 
    <uc2:HealthcheckSubtabs ID="HealthcheckSubtabs1" runat="server" />
    <br />
     <uc1:ProjectPmNewIssues ID="ProjectPmNewIssues1" runat="server"  />
    <script  type="text/javascript">
        $(document).ready(function () {

            sideMenuActive('<%= Resources.DeffinityRes.HealthChecks%>');
        });
       </script>
</asp:Content>

