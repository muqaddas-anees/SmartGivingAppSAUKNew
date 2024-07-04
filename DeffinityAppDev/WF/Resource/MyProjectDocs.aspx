<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="MyProjectDocs" Codebehind="MyProjectDocs.aspx.cs" %>
<%@ Register Src="~/WF/Resource/Controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<%@ Register Src="controls/MyProjectDocumentCtrl.ascx" TagName="MyProjectDocumentCtrl" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Documents
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:ProjectStatus ID="ProjectStatus1" runat="server" />  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <uc2:MyProjectDocumentCtrl ID="MyProjectDocumentCtrl1" runat="server"></uc2:MyProjectDocumentCtrl>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>


