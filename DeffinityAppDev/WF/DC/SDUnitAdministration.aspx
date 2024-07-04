<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="Servicedesk_SDUnitAdministration" Codebehind="SDUnitAdministration.aspx.cs" %>
<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<%@ Register Src="~/WF/DC/Controls/sd_UnitsSubTab.ascx" TagName="sd_UnitsSubTab" TagPrefix="uc4" %>
<%--<%@ Register Src="~/Servicedesk/sdcontrols/sd_unitstatus.ascx" TagName="sd_unitstatus" TagPrefix="uc5" %>--%>
<%@ Register Src="~/WF/DC/controls/sd_unitadministration.ascx" TagName="sd_unitadministration"
    TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="page_description" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_title" runat="Server">
      Unit Administration - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr2" runat="server" />
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i>Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc4:sd_UnitsSubTab ID="sd_UnitsSubtab" runat="server" />
            <br /><br />
    <uc5:sd_unitadministration ID="sd_unitadministration" runat="server" />
     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>

