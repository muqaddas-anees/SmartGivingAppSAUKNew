<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgQRCode.aspx.cs" Inherits="DeffinityAppDev.OrgQRCode" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <html lang="en">
	<!--begin::Head-->
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
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
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
	 @media print {
tr:nth-child(even) td {
background-color: red !important;
-webkit-print-color-adjust: exact;
}
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
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="page-loading-enabled page-loading">
		<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
		 
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
          <%-- <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
		    <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
		<!--begin::Main-->
		<!--begin::Root-->
		<div id="content_1" class=" d-flex flex-column flex-root ">
			<!--begin::Page-->
			<div class="page d-flex flex-row flex-column-fluid">
				<!--begin::Aside-->
				
				<!--end::Aside-->
				<!--begin::Wrapper-->
				<div class="wrapper d-flex flex-column flex-row-fluid" id="kt_wrapper">
					<!--begin::Header-->
					
					<!--end::Header-->
					<!--begin::Content-->
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">
						<!--begin::Container-->
						<div class="container-xxl" id="kt_content_container">
							<div class="row mb-6">
							<!--begin::About card-->
							<div class="card">
								<!--begin::Body-->
								<div class="card-body p-lg-17">
									<!--begin::About-->
									<div class="mb-8">
										<!--begin::Wrapper-->
										<div class="mb-10">
											<!--begin::Top-->
											<div class="text-center mb-15">
												<!--begin::Title-->
												<h3 class="fs-2hx text-dark mb-5"><asp:Literal ID="lblOrgname" runat="server" Text="Org"></asp:Literal> </h3>
												<!--end::Title-->
												<!--begin::Text-->
												<%--<div class="fs-5 text-muted fw-bold">Within the last 10 years, we have sold over 100,000 admin theme copies that have been
												<br />successfully deployed by small businesses to global enterprises</div>--%>
												<!--end::Text-->
											</div>
											<!--end::Top-->
											<!--begin::Overlay-->
											<div class="overlay">
												<!--begin::Image-->
												<div class="row mb-25">
													<div class="col-lg-12 text-center">
														<asp:Image ID="imgqrcode" runat="server"  />
													</div>
													</div>
											
												<div class="row mb-6">
													<div class="col-lg-12 text-center">
														<h3 class="fs-5hx text-dark mb-5"><asp:Literal ID="Literal1" runat="server" Text="SCAN TO DONATE"></asp:Literal> </h3>
														</div>
													</div>
												
											</div>
											<!--end::Container-->
										</div>
										
									</div>
									
								</div>
								<!--end::Body-->
							</div>
								</div>
							

							

							<!--end::About card-->
						</div>
						<!--end::Container-->
					</div>
					
					<!--end::Content-->
					<!--begin::Footer-->
					<div class="footer py-4 d-flex flex-lg-column" id="kt_footer">
						<!--begin::Container-->
						<div class="container-xxl d-flex flex-column flex-md-row flex-stack">
							<!--begin::Copyright-->
							<div class="text-dark order-2 order-md-1">
								<%--<span class="text-gray-400 fw-bold me-1">Created by</span>
								<a href="https://keenthemes.com" target="_blank" class="text-muted text-hover-primary fw-bold me-2 fs-6">Keenthemes</a>--%>
							</div>
							<!--end::Copyright-->
							<!--begin::Menu-->
							<ul class="menu menu-gray-600 menu-hover-primary fw-bold order-1">
								<%--<li class="menu-item">
									<a href="https://keenthemes.com" target="_blank" class="menu-link px-2">About</a>
								</li>
								<li class="menu-item">
									<a href="https://keenthemes.com/support" target="_blank" class="menu-link px-2">Support</a>
								</li>
								<li class="menu-item">
									<a href="https://1.envato.market/EA4JP" target="_blank" class="menu-link px-2">Purchase</a>
								</li>--%>
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

		<div class="row mb-6">
			<div id="editor"></div>
    <button id="cmd">Generate PDF</button>
		</div>
		<!--end::Scrolltop-->
		<!--end::Main-->
		<!--begin::Javascript-->
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


		<script>
			
            $(window).on('load', function () {
                // Handler for .load() called.
				window.print();
            });
        </script>


		<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/0.9.0rc1/jspdf.min.js"></script>


		<script >
            var doc = new jsPDF();
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };

            $('#cmd').click(function () {
                doc.fromHTML($('#content_1').html(), 15, 15, {
                    'width': 170,
                    'elementHandlers': specialElementHandlers
                });
                doc.save('sample-file.pdf');
            });


        </script>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
			 </form>
	</body>
	<!--end::Body-->





 

</html>

