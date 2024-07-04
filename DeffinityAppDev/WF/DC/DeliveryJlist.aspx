<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DeliveryJlist.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DeliveryJlist" %>
<%@ Register Src="~/WF/DC/controls/DeliveryListCtrl.ascx" TagName="FLSListCtrl" TagPrefix="uc1" %>
<%@ Register Src="~/WF/DC/controls/FLSListTabCtrl.ascx" TagPrefix="uc1" TagName="FLSListTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    
    <asp:Literal ID="lit_pagetitle" runat="server"></asp:Literal> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    
    <script type="text/javascript">
        setInterval(function () {
            window.location.reload();
        }, 4 * 60000);
    </script>
    <uc1:FLSListTabCtrl runat="server" id="FLSListTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Scheduler
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <%-- <script src="../../Content/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
    <script src="../../Content/assets/js/jquery-1.11.1.min.js"></script>
    <uc1:FLSListCtrl ID="FLSListCtrl1"  runat="server"/>
    
    <script type="text/javascript">
        function getQuerystring(key, default_) {

            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href.toLowerCase());
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

    </script>
   
    <script type="text/javascript">

        $(document).ready(function () {
            var qval = getQuerystring('type')
            if (qval.toLowerCase() == 'fls') {
                $("#a1_link").attr("href", "FLSDefault.aspx?tab=fls");
                $("#a1_link").text("Service Desk - Admin Dropdown");
                /* Set the number below to the amount of delay, in milliseconds,
                you want between page reloads: 1 minute = 60000 milliseconds. */
                // window.setInterval("reFresh()", 60000);

            }
            else if (qval.toLowerCase() == 'delivery') {
                $("#a1_link").attr("href", "DeliveryDefaults.aspx?tab=delivery");
                $("#a1_link").text("Delivery - Admin Dropdown");
            }
            else if (qval.toLowerCase() == 'accesscontrol') {
                $("#a1_link").attr("href", "AccessControlDefaults.aspx?tab=accesscontrol");
                $("#a1_link").text("Access Control - Admin Dropdown");
            }
            else {
                $("#a1_link").attr("href", "PermitToWorkDefaults.aspx?tab=permittowork");
                $("#a1_link").text("Permit to Work - Admin Dropdown");
            }

            $("#a2_link").attr("href", "/WF/CustomerAdmin/PortfolioContacts.aspx?type=" + qval);
        });


    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
