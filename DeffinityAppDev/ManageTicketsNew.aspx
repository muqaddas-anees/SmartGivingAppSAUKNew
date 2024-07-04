<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageTicketsNew.aspx.cs" Inherits="DeffinityAppDev.App.Events.ManageTicketsNew" %>


<%@ Register Src="~/App/Events/controls/ManageTicketsCtrl.ascx" TagPrefix="Pref" TagName="EventDetailsCtrl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><base href="../../">
		<title>Organization</title>
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<meta property="og:title" content="" />
		<meta property="og:url" content="" />
		<meta property="og:site_name" content="" />
		<link rel="canonical" href="" />
		<%--<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
	<script src='<%=ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
		<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" />
	
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />
			<link href="Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
		 <%= System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
		<!--end::Global Stylesheets Bundle-->
		 <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
   /* .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }*/
        </style>
	</head>
<body id="kt_body" class="page-loading-enabled page-loading">
	<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
          <%-- <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
		    <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
			 <Pref:EventDetailsCtrl runat="server" ID="EventDetailsCtrl" />

         
			</div>
    </form>


	<script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/scripts.bundle.js")%>'></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.js")%>'></script>
	<%--<script src='<%:ResolveClientUrl("~/assets/plugins/custom/fullcalendar/fullcalendar.bundle.js")%>'></script>--%>
	
	
<script src='<%:ResolveClientUrl("~/assets/plugins/custom/datatables/datatables.bundle.js")%>'></script>

	<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/documentation/forms/tagify.js")%>'></script>--%>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/select-location.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/widgets.js")%>'></script>
		<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/apps/chat/chat.js")%>'></script>--%>
		<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/create-app.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/upgrade-plan.js")%>'></script>--%>
	  <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
    <script src='<%:ResolveClientUrl("~/Scripts/Utility.js?id=10")%>'></script>

	<div style="display:none;visibility:hidden">
<iframe id="frm_setpage" name="frm_setpage" runat="server" width="100%" frameborder="0" src="~/WF/SessionKeepAlive.aspx" scrolling="no" style="display:none;visibility:hidden" ></iframe>
</div>
	<%--<script src='assets/plugins/global/plugins.bundle.js'></script>
		<script src='assets/js/scripts.bundle.js'></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src='assets/plugins/custom/leaflet/leaflet.bundle.js'></script>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src='assets/js/custom/modals/select-location.js'></script>
		<script src='assets/js/custom/widgets.js'></script>
		<script src='assets/js/custom/apps/chat/chat.js'></script>
		<script src='assets/js/custom/modals/create-app.js'></script>
		<script src='assets/js/custom/modals/upgrade-plan.js'></script>--%>

	<script type="text/javascript">
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        //toastr.success("etetetetet", "testet");
        function showswal(msg, buttontitle) {
            Swal.fire({
                text: msg,
                icon: 'success',
                buttonsStyling: false,
                confirmButtonText: buttontitle,
                customClass: {
                    confirmButton: 'btn btn-primary'
                }
            });
		}
        function showswalerror(msg, buttontitle) {
            //Swal.fire({
            //    text: msg,
            //    icon: 'error',
            //    buttonsStyling: false,
            //    confirmButtonText: buttontitle,
            //    customClass: {
            //        confirmButton: 'btn btn-primary'
            //    }
            //});
			Swal.fire({
				text: msg,
				icon: 'error',
				buttonsStyling: false,
				confirmButtonText: buttontitle,
				customClass: {
					confirmButton: 'btn btn-primary'
				}
			});

				//.then((result) => {
    //            if (result.isConfirmed) {
				//	location.reload();
    //            }
    //        })
        }
    </script>
	
	
<%--	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css"/>--%>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
<script type="text/javascript">




    $(".input_date_new").flatpickr({
        //enableTime: true,
        dateFormat: "d/m/Y"

	});

    $(".input_time_new").flatpickr({
        enableTime: true,
		dateFormat: "H:i",
        noCalendar: true,

    });
</script>
</body>
</html>
