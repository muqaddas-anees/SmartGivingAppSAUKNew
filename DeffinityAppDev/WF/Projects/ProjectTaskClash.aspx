<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectTaskClash" Codebehind="ProjectTaskClash.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/ProjectResourceUtill.ascx" TagName="ResourceUtil" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Project Task Clash - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <%--<asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>--%>
    <asp:HyperLink ID="hlinkGantt" runat="server" Font-Bold="true"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
          <uc3:ResourceUtil ID="Resourceutil1" runat="server" />
</asp:Content>

