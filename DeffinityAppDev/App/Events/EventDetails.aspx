<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="DeffinityAppDev.App.EventDetails" EnableEventValidation="false" %>

<%@ Register Src="~/App/Events/controls/EventDetailsCtrl.ascx" TagPrefix="Pref" TagName="EventDetailsCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>







<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <!--begin::Head-->
    <head>
        <title>Event Name and Details</title>

        <link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
        <link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
        <!--begin::Fonts-->
        <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
        <!--end::Fonts-->
        <!--begin::Global Stylesheets Bundle(used by all pages)-->
        <link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
        <link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
        <!--end::Global Stylesheets Bundle-->
    </head>
    <!--end::Head-->
    <!--begin::Body-->
    <body id="kt_body" class="header-fixed header-tablet-and-mobile-fixed toolbar-enabled toolbar-fixed aside-enabled aside-fixed" style="--kt-toolbar-height: 55px; --kt-toolbar-height-tablet-and-mobile: 55px">
        <!--begin::Main-->




        <!--begin::Content-->
        <div class="content d-flex flex-column flex-column-fluid" id="kt_content">



            <div class="toolbar" id="kt_toolbar">
                <!--begin::Container-->
                <!--end::Container-->
            </div>



            			 <Pref:EventDetailsCtrl runat="server" ID="EventDetailsCtrl" />



        </div>
        <!--end::Content-->





        <!--end::Main-->
        <!--begin::Javascript-->
        <!--begin::Global Javascript Bundle(used by all pages)-->
        <%--<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>--%>
        <!--end::Global Javascript Bundle-->
        <!--begin::Page Vendors Javascript(used by this page)-->
        <%--<script src="assets/plugins/custom/fslightbox/fslightbox.bundle.js"></script>--%>
        <!--end::Page Vendors Javascript-->
        <!--begin::Page Custom Javascript(used by this page)-->
        <%--<script src="assets/js/custom/widgets.js"></script>
		<script src="assets/js/custom/apps/chat/chat.js"></script>
		<script src="assets/js/custom/modals/create-app.js"></script>
		<script src="assets/js/custom/modals/upgrade-plan.js"></script>--%>
        <!--end::Page Custom Javascript-->
        <!--end::Javascript-->
    </body>
    <!--end::Body-->













    <%--<asp:ListView runat="server" ID="BannerList" GroupPlaceholderID="groupplaceholder" ItemPlaceholderID="itemplaceholder">

                                        <LayoutTemplate>
                                           
                                        </LayoutTemplate>

                                        <GroupTemplate>
                                            <tr>
                                                <tr id="itemplaceholder" runat="server"></tr>
                                            </tr>
                                        </GroupTemplate>


                                        <ItemTemplate>




                                        </ItemTemplate>


                                    </asp:ListView>--%>
</asp:Content>
