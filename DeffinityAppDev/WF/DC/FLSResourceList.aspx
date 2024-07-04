<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" 
Inherits="FLSResourceList" EnableEventValidation="false" Codebehind="FLSResourceList.aspx.cs" %>

<%@ Register Src="~/WF/DC/controls/FLSListCtrl.ascx" TagName="FLSListCtrl" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Resource/Controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
      <script src="../../Content/assets/js/jquery-1.11.1.min.js"></script>
   <uc1:FLSListCtrl ID="FLSListCtrl1" runat="server" />

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
</asp:Content>


