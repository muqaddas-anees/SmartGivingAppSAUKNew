<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectIssues" Title="Untitled Page" Codebehind="ProjectIssues.aspx.cs" %>
<%@ Register Src= "controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/ProjectPmNewIssues.ascx" TagName="ProjectPmIssues" TagPrefix="uc2" %>
<%@ Register src= "MailControls/ProjectIssue.ascx" tagname="ProjectIssue" tagprefix="PD1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ProjectIssues%> - <Pref:ProjectRef ID="ProjectRef2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
   
<script src="js/jquery.MultiFile.js" type="text/javascript"></script>

     <asp:Panel ID="PanelControl" runat="server">
        <uc2:ProjectPmIssues id="ProjectPmIssues1" runat="server" >
        </uc2:ProjectPmIssues>
        </asp:Panel>
<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 

    
</asp:Content>

