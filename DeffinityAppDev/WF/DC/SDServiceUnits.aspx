<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="Servicedesk_SDServiceUnits"  EnableEventValidation="false" Codebehind="SDServiceUnits.aspx.cs" %>
<%@ Register src="~/WF/DC/Controls/sd_ServiceUnit.ascx" tagname="sd_new" tagprefix="uc2" %>
<%@ Register src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>
<%--<%@ Register src="../controls/sd_tabs.ascx" tagname="sd_tabs" tagprefix="uc3"  %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

<%--<ul class="tabs_list6" style="float:right;" runat="server" id="pnl_backtoCustomerHome" visible="false">
    <li class="current6"><a href="../SDsummary.aspx?customer=0" target="_self"><span>Back to Service Desk</span></a></li>        
</ul>
<uc3:sd_tabs ID="sd_tabs1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
<label id="lblTitle" runat="server">
                  </label> - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i>Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <uc2:sd_new ID="Sd_new1" runat="server" SectionName="Service Desk" />
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


