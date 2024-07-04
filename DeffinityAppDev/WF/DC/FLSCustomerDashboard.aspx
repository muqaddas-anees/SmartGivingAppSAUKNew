<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" CodeFile="FLSCustomerDashboard.aspx.cs" Inherits="FLSCustomerDashboard" %>
<%@ Register Src="~/WF/DC/controls/FLSDashboardCtrl.ascx" TagName="FLSDashboard" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=FLS&customer=0"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<script src="Scripts/jquery-1.9.0.js" type="text/javascript"></script>--%>
      <uc2:FLSDashboard ID="FLSDashboard1" runat="server"/>
</asp:Content>


