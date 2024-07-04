<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Recommendations" Title="Project Recommendation" Codebehind="Checkpoint_Recommendations.aspx.cs" %>
<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc1" %>

<%@ Register Src="controls/CheckpointRecommendation.ascx" TagName="CheckpointRecommendation" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Recommendation -  <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:OpsViewTabs ID="OpsViewTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<uc2:CheckpointRecommendation ID="CheckpointRecommendation1" runat="server" />
</asp:Content>

