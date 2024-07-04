<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
     Inherits="FLSJlist1" EnableEventValidation="false" Codebehind="FLSJlist.aspx.cs" %>
<%@ Register Src="controls/FLSListCtrl.ascx" TagName="FLSListCtrl" TagPrefix="uc1" %>
<%--<%@ Register Src="~/WF/DC/controls/FLSListTabCtrl.ascx" TagPrefix="uc1" TagName="FLSListTabCtrl" %>--%>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
   
    <asp:Literal ID="lit_pagetitle" runat="server"></asp:Literal> 
   </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <script src="../../Content/assets/js/jquery-1.11.1.min.js"></script>
     <%-- <div class="row">
<div class="col-md-12">--%>
   <%-- <nav class="navbar navbar-default" role="navigation" id="navTab">
      <uc1:FLSListTabCtrl runat="server" id="FLSListTabCtrl" />
        </nav>--%>
    <style>
        .icon_callback {
    background-color: #FF6264;
    color: #fff;
}
        
    </style>
  
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
                $("#a1_link").text("Jobs - Admin");
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


    <%: System.Web.Optimization.Styles.Render("~/bundles/jtablecss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>
   

</asp:Content>

