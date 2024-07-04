<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DCCustomerService" EnableEventValidation="false" Codebehind="DCCustomerService.aspx.cs" %>

<%@ Register src="~/WF/DC/controls/FLSCustomerTab.ascx" tagname="ServiceTab" tagprefix="uc1" %>
<%@ Register src="~/WF/DC/Controls/sd_customerservice.ascx" tagname="sd_customerservice" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ServiceTab ID="Sevice1" runat ="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
<label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink runat="Server" Text="" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=FLS&customer=0"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <uc2:sd_customerservice ID="sd_customerservice" runat="server" Type="FLS" />
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


