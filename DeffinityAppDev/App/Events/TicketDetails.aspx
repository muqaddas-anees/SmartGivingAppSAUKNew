<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketDetails.aspx.cs" Inherits="DeffinityAppDev.App.Events.TicketDetails" %>

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
		<div class=" d-flex flex-column flex-root ">
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
										<div class="mb-10" id="pnlTicketDetails" runat="server">
											<!--begin::Top-->
											<div class="text-center mb-15">
												<!--begin::Title-->
												<h3 class="fs-2hx text-dark mb-5"><asp:Literal ID="lblEventTickets" runat="server" Text="Event"></asp:Literal> </h3>

												<h4 class="fs-1hx text-dark mb-5"><asp:Label ID="lblDateTime" runat="server"></asp:Label></h4>

												<h4 class="fs-1hx text-dark mb-5"><asp:Label ID="lblVenue" runat="server"></asp:Label>  </h4>
												
													<div class="overlay mt-8">
														<asp:HiddenField ID="hevent_unid" runat="server" />
														<img id="img_event" class="img-fluid"  style="width:100%" runat="server"  />
																<!--begin::Image-->
															<%--	<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded img-fluid"  style=background-image:url('<%= GetImmage() %>')></div>  --%>
																<!--end::Image-->
																<!--begin::Links-->
																
																<!--end::Links-->
															
															</div>
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
														<asp:Image ID="imgqrcode" runat="server" Height="250" />
													</div>
													</div>
												<div class="row mb-6" style="font-size:20px">
													<div class="col-lg-12 text-left">
														<p>Hi <asp:Label ID="lblContactName" runat="server" Font-Bold="true"></asp:Label> </p>
														
														<p> Thank you for purchasing tickets for <asp:Label ID="lblEvent" runat="server"></asp:Label> </p>
														
														<p> Your booking details are as follows: </p>

														<p><strong>Booking Reference: </strong>  B<asp:Label ID="lblbookingid" runat="server"></asp:Label> </p>
													<%--	<p> <strong>Date & Time: </strong>  </p>
														<p><strong> Venue: </strong> </p>--%>
														</div>
													</div>
												<div class="row mb-6">
													<div class="col-lg-12 text-left table-responsive">
														<asp:GridView ID="grid" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="20px" >
															<Columns>
																<asp:TemplateField HeaderText="#" ItemStyle-Width="30px" HeaderStyle-Width="30px" >
																	<ItemTemplate>
																		<asp:Label ID="lblIndex" runat="server" Text="1"></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																	<asp:TemplateField  ItemStyle-Width="20%" HeaderText="Ticket type" >
																	<ItemTemplate>
																		<asp:Label ID="lbltype" runat="server" Text='<%# Eval("TicketType") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField  ItemStyle-Width="20%" HeaderText="Name" >
																	<ItemTemplate>
																		<asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField  ItemStyle-Width="20%" HeaderText="Email" >
																	<ItemTemplate>
																		<asp:Label ID="lblUserEmail" runat="server" Text='<%# Eval("UserEmail") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																<asp:TemplateField  ItemStyle-Width="20%" HeaderText="Cell" >
																	<ItemTemplate>
																		<asp:Label ID="lblUserContact" runat="server" Text='<%# Eval("UserContact") %>'></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																	<asp:TemplateField ItemStyle-Width="100px" HeaderText="No.of slot(s)">
																	<ItemTemplate>
																		<asp:Label ID="lblqty" runat="server" Text="1"></asp:Label>
																	</ItemTemplate>
																</asp:TemplateField>
																	
															</Columns>
														</asp:GridView>
														</div>
													</div>
												
											</div>
											<!--end::Container-->
										</div>

											<div class="mb-10" id="pnlVlidateDetails" runat="server" visible="false">
													<div class='row mb-15 text-center'>

														</div>
												<div class="text-center mb-15">
												<!--begin::Title-->
												<div class='row mb-6 text-center'>
<i class='far fa-check-circle' style='font-size:150px;color:green'></i>
</div>
												
												
											</div>

													<div class="row mb-15 text-center" style="font-size:20px">

														<asp:Label ID="lblUserName1" runat="server" style="font-size:35px" Font-Bold="true"></asp:Label>
														</div>

													<div class="row mb-15  text-center" style="font-size:20px">

															<asp:Label ID="lblCompany" runat="server" style="font-size:45px" Font-Bold="true"></asp:Label>
														</div>

													<div class="row mb-5  text-center" style="font-size:20px">

															<asp:Label ID="lblEventName" runat="server" style="font-size:35px" Font-Bold="true"></asp:Label>
														</div>

													<div class="row mb-15  text-center" style="font-size:20px">

															<asp:Label ID="lblDateTime1" runat="server" style="font-size:35px" ></asp:Label>
														</div>

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
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
			 </form>
	</body>
	<!--end::Body-->


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


</html>
    </form>
</body>
</html>
