<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="OpsViewProject" Title="OpsView Project" Codebehind="Checkpoint_Overview.aspx.cs" %>

<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/CheckPointOverview.ascx" TagName="CheckPointOverview" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Checkpoint%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ProjectOverview%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:OpsViewTabs ID="OpsViewTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <uc2:CheckPointOverview ID="CheckPointOverview1" runat="server" />


</asp:Content>

