<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectAsset" Codebehind="ProjectAsset.aspx.cs" %>
<%@ Register Src="controls/ProjectRef.ascx" TagName="ProjectRef" TagPrefix="uc2" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/UserAssetsMove.ascx" TagName="AssetsId" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ProjectAssets%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline" ></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <script src="Scripts/jquery-1.3.2.js" type="text/javascript"></script>
        <script src="Scripts/jquery.MultiFile.js" type="text/javascript"></script>

    <uc3:AssetsId ID="AssetsId" runat="server"  />
</asp:Content>


