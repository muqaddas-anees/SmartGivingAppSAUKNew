<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="DCServices" MaintainScrollPositionOnPostback="true"  EnableEventValidation="false" Codebehind="DCServices.aspx.cs" %>
<%@ Register src="~/WF/DC/controls/sd_services.ascx" tagname="sd_services" tagprefix="uc1" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
   <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self" title="Return to Invoice List"><i class="fa fa-arrow-left"></i> Return to Invoice List</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <uc1:sd_services ID="DcServices" runat="server" Type="FLS"/>

    <script type="text/javascript">
       // activeTab('invoice');
    </script>
</asp:Content>




