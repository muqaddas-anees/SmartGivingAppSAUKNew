<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiveSession.aspx.cs" Inherits="DeffinityAppDev.App.sessions.LiveSession" %>

<%@ Register Src="~/App/controls/taithingNewCtrl.ascx" TagPrefix="Pref" TagName="TithingNewCtrl" %>

<!DOCTYPE html>

<html lang="en">
	<!--begin::Head-->
	<head><base href="./">
		<title><asp:Literal ID="lblTitle" runat="server"></asp:Literal> </title>
		<meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
		<%--<meta name="description" content='<%:sitedesciption() %>' />
		<meta name="keywords" content='<%:sitekey() %> ' />--%>
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<%--<meta property="og:title" content='<%:sitetitle() %>' />
		<meta property="og:url" content='<%:siteurl() %>' />
		<meta property="og:site_name" content='<%:sitetitle() %>' />--%>
<meta property="og:type" content="website" />
<%--<meta property="og:image" content='<%:siteimage() %>' />
<meta property="og:description" content='<%:sitedesciption() %>' />
		<meta name="twitter:card" content='<%:siteimage() %>'>
		<meta name="twitter:image:alt" content='<%:sitetitle() %>'>--%>



		<link rel="canonical" href="" />
		<%--<link rel="shortcut icon" href="../../assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
		 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
		<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" />
	
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />
			<link href="../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
		 <%= System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
		<!--end::Global Stylesheets Bundle-->
		 <style>
				.ios-strech-fix
{
      height: intrinsic;
}
				.row:before, .row:after {display: none !important;}
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

/*
		 	.embed-responsive {
		 		position: relative;
		 		display: block;
		 		height: 0;
		 		padding: 0;
		 		overflow: hidden;
		 	}


.embed-responsive-16by9 {
  padding-bottom: 56.25%;
}


.embed-responsive-4by3 {
  padding-bottom: 75%;
}


.embed-responsive .embed-responsive-item, .embed-responsive embed, .embed-responsive iframe, .embed-responsive object, .embed-responsive video {
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    width: 100%;
    height: 100%;
    border: 0;
}*/
        </style>
	</head>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="page-loading-enabled page-loading">
		<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
		 <form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
          <%-- <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
		    <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
			 <asp:HiddenField ID="hpath" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="hurl" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="himage" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="hdescription" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="hkey" runat="server" ClientIDMode="Static" />
		<!--begin::Main-->
		<!--begin::Root-->
		<div class=" d-flex flex-column flex-root ">
			<!--begin::Page-->
			<div class="page d-flex flex-row flex-column-fluid">
			
				<div class="wrapper d-flex flex-column flex-row-fluid" id="kt_wrapper">
					
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">
						<!--begin::Container-->
						<div class="" id="kt_content_container">
							
						
					
							<div class="row">
								<div class="mb-6">
											<div class="row g-10 text-center">
											<!--begin::Col-->
											<asp:Literal ID="lit_html" runat="server" Text=""></asp:Literal>
												

												</div>

										</div>
					</div>

							<%--<div class="row mb-6">
								
							</div>--%>
								<div class="row">
							<div class="card card-xxl-stretch" id="divquicklinks">
											<!--begin::Header-->
											<div class="card-header border-0 py-5">
												<h3 class="card-title fw-bolder text-white"> Live session </h3>
												<div class="card-toolbar">

													</div>
												</div>
								<div class="card-body">
									<div class="row">
										<div class="iframe-container" style="overflow: hidden; padding-top: 56.25%; position: relative;">
    	<iframe allow="microphone; camera" style="border: 0; height: 100%; left: 0; position: absolute; top: 0; width: 100%;" src="https://success.zoom.us/wc/join/98255406111" frameborder="0"></iframe>
	</div>
									</div>
									<div class="row">
										<div class="col-lg-8">
											<iframe id="frm" runat="server" width="100%" height="700px" sandbox="allow-forms allow-scripts" allow="microphone; camera; fullscreen" src="https://zoom.us/"></iframe>
											</div>

										<div class="col-lg-4" style="overflow-y:scroll;height:700px">
											 <Pref:TithingNewCtrl runat="server" id="TithingNewCtrl" />
											</div>
										</div>

									<div class="row">
										<div class="col-lg-12">
											<asp:Label ID="lblSessonTitle" runat="server" Font-Size="40px"></asp:Label>
											<br />
												<asp:Label ID="lblDescription" runat="server" Font-Size="20px"></asp:Label>
											</div>
										</div>
										<div class="row">
										 <label class="col-lg-1 col-form-label fw-bold fs-6">Speaker(s) </label>

										<div class="col-lg-10 mt-3">
											<asp:Label ID="lblSpeakers" runat="server"></asp:Label>
											</div>
										</div>
									<div class="row">
										 <label class="col-lg-1 col-form-label fw-bold fs-6">Date Scheduled </label>

										<div class="col-lg-10  mt-3">
											<asp:Label ID="lblDateScheduled" runat="server"></asp:Label>
											</div>
										</div>
										<div class="row">
											 <label class="col-lg-1 col-form-label fw-bold fs-6">Zoom Link </label>
											<div class="col-lg-10  mt-3">
												<asp:Label ID="lblZoomLink" runat="server"></asp:Label>
												</div>

											</div>
									<div class="row">
											 <label class="col-lg-1 col-form-label fw-bold fs-6">Recorded Link </label>
											<div class="col-lg-10  mt-3">
												<asp:Label ID="lblRecordeLink" runat="server"></asp:Label>
												</div>
										</div>
									</div>
								</div>

									</div>

						
							
							
							<!--end::About card-->
						</div>
						<!--end::Container-->
					</div>
					
				
					<div class="footer py-4 d-flex flex-lg-column" id="kt_footer">
						<!--begin::Container-->
						<div class="container-xxl d-flex flex-column flex-md-row flex-stack">
							<!--begin::Copyright-->
							<div class="text-dark order-2 order-md-1">
							</div>
							
							<ul class="menu menu-gray-600 menu-hover-primary fw-bold order-1">
								
							</ul>
							<!--end::Menu-->
						</div>
						<!--end::Container-->
					</div>
					<!--end::Footer-->
				</div>
				<!--end::Wrapper-->
			</div>
			<!--end::Page-->
		</div>
		<!--end::Root-->
		<!--begin::Drawers-->
		
		<!--end::Modals-->
		<!--begin::Scrolltop-->
		<div id="kt_scrolltop" class="scrolltop" data-kt-scrolltop="true">
			<!--begin::Svg Icon | path: icons/duotune/arrows/arr066.svg-->
			<span class="svg-icon">
				<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
					<rect opacity="0.5" x="13" y="6" width="13" height="2" rx="1" transform="rotate(90 13 6)" fill="black" />
					<path d="M12.5657 8.56569L16.75 12.75C17.1642 13.1642 17.8358 13.1642 18.25 12.75C18.6642 12.3358 18.6642 11.6642 18.25 11.25L12.7071 5.70711C12.3166 5.31658 11.6834 5.31658 11.2929 5.70711L5.75 11.25C5.33579 11.6642 5.33579 12.3358 5.75 12.75C6.16421 13.1642 6.83579 13.1642 7.25 12.75L11.4343 8.56569C11.7467 8.25327 12.2533 8.25327 12.5657 8.56569Z" fill="black" />
				</svg>
			</span>
			<!--end::Svg Icon-->
		</div>
		<!--end::Scrolltop-->
		<!--end::Main-->
		<!--begin::Javascript-->
		<!--begin::Global Javascript Bundle(used by all pages)-->
	<%--	<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src="assets/plugins/custom/fslightbox/fslightbox.bundle.js"></script>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src="assets/js/custom/widgets.js"></script>--%>
		<%--<script src="assets/js/custom/apps/chat/chat.js"></script>
		<script src="assets/js/custom/modals/create-app.js"></script>
		<script src="assets/js/custom/modals/upgrade-plan.js"></script>--%>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
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
		
	<script type="text/javascript">
        $(document).ready(function () {
            console.log('path:' + $("[id$='hpath']").val);
          //  $("#div_load").load($("[id$='hpath']").val());

           

			
		});


		//function page_load() {
		//	var path = $("[id$='hpath']").val();

  //          $("#div_load").load(path, function (responseTxt, statusTxt, xhr) {
  //              if (statusTxt == "success") {
		//			console.log("External content loaded successfully!");


  //                  $("#divfaitheducation").insertBefore("#faitheducation");
  //                  $("#divtithing").insertBefore("#tithing");
  //                  $("#divactivites").insertBefore("#activites");
  //                  $("#divfaithgiving").insertBefore("#faithgiving");
  //                  $("#divquicklinks").insertBefore("#quicklinks");
  //                  $("#divsignup").insertBefore("#signup");
  //              }
  //              if (statusTxt == "error")
  //                  console.log("Error: " + xhr.status + ": " + xhr.statusText);
  //          });
  //      }
		

  //      $(window).on('load', function () {
  //          //   
		//	page_load();
  //      });


    </script>
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
        //    window.onload = function () {
        //        document.querySelector("iframe").addEventListener("load", function () {
        //console.log("iframe content loaded");
        //alert('1');
        //        });
        //    }
        //$(window).on('load', function () {
        //    $("iframe").each(function () {



        //        //            //alert($(this).text())
        //        ////resizeIframeID($(this));
        //        var _height = '250px';
        //        //var iframe = $(this);//.attr("id"); //document.getElementById("myIframe");
        //        //            //alert($(this).attr("src"));
        //        //            // Adjusting the iframe height onload event
        //        //            iframe.onload = function () {
        //        //	iframe.style.height = (iframe.contentWindow.document.body.scrollHeight) + 'px';
        //        //	_height = (iframe.contentWindow.document.body.scrollHeight) + 'px';
        //        //	console.log(_height);
        //        //                //alert(iframe.style.height);
        //        //                //$(this).parent().css({
        //        //                //    "height": iframe.style.height,
        //        //                //    // "border": "2px solid green"
        //        //                //});
        //        //}

        //        //            alert(_height);
        //        $(this).parent().css({
        //            "height": _height,
        //            // "border": "2px solid green"
        //        });

        //    });
        //});

        //function resizeIframeID(obj) {
        //    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
        //}

    </script>
	
	
<%--	<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css"/>--%>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
<script type="text/javascript">




    $(".input_date_new").flatpickr({
        //enableTime: true,
        dateFormat: "m/d/Y"

    });

    $(".input_time_new").flatpickr({
        enableTime: true,
        dateFormat: "H:i",
        noCalendar: true,

    });
</script>
	</body>
	<!--end::Body-->



</html>
