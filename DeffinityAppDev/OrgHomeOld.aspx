<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgHomeOld.aspx.cs" Inherits="DeffinityAppDev.OrgHome" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/App/controls/taithingctrl.ascx" TagPrefix="Pref" TagName="taithingctrl" %>
<%@ Register Src="~/App/controls/ActivitiesCtrl.ascx" TagPrefix="Pref" TagName="ActivitiesCtrl" %>





<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    	<%--<base href="../../../"/>--%>
		<title>Faith union Hub Admin</title>
		
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<%--<meta property="og:title" content="Metronic - Bootstrap 5 HTML, VueJS, React, Angular &amp; Laravel Admin Dashboard Theme" />--%>
		<%--<meta property="og:url" content="https://keenthemes.com/metronic" />--%>
		<%--<meta property="og:site_name" content="Keenthemes | Metronic" />--%>
		<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
		<%--<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<%--<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />--%>
		<!--end::Global Stylesheets Bundle-->

	
</head>
<body id="kt_body" class="page-loading-enabled page-loading bg-body">
    <div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
     
	<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" >
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" >
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" >
		<!--begin::Main-->
	<%--<div class="row">
  <div class="col-sm-6"><p class="float-start">left</p></div>  
  <div class="col-sm-6"><p class="float-end">right</p></div>  
</div>--%>
	<div class="d-flex justify-content-end p-5 pb-lg-0">
						<a href="Default.aspx" class="btn btn-secondary">
						Login
					</a>
					</div>
		<div class="d-flex flex-column flex-root">
			<!--begin::Authentication - Sign-in -->
			<div class="d-flex flex-column flex-column-fluid bgi-position-y-bottom position-x-center bgi-no-repeat bgi-size-contain bgi-attachment-fixed" style="background-image: url(assets/media/illustrations/dozzy-1/14.png?id=24)">
				<!--begin::Content-->
				<div class="d-flex flex-center flex-column flex-column-fluid p-10 pb-lg-20">
					
					
					<!--begin::Logo-->
					<a href="#" class="mb-12">
						<img alt="Logo" runat="server" src="assets/media/logos/logo-1.png" class="h-110px" style="width:200px" id="imglogo" />
					</a>

					
					<!--end::Logo-->
					<!--begin::Wrapper-->
					<div class="Card">
						<!--begin::Form-->
						<form class="form w-100" novalidate="novalidate" id="kt_sign_in_form" action="#" runat="server">

							   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
     
   </asp:ScriptManager>
							<!--begin::Heading-->
							<div class="text-center mb-10">
								<!--begin::Title-->
								<%--<h1 class="text-dark mb-3">Sign In to Portal</h1>--%>
								<!--end::Title-->
								<!--begin::Link-->
								<%--<div class="text-gray-400 fw-bold fs-4">New Here?
								<a href="../../demo4/dist/authentication/flows/basic/sign-up.html" class="link-primary fw-bolder">Create an Account</a></div>--%>
								<!--end::Link-->
							</div>
							 <div class="row gy-5 g-xl-8">
								 <asp:Image ID="imgtop" runat="server" ImageUrl="~/assets/banners/banner1.jpg?v=11" Height="500px" />
								 </div>

							<Pref:taithingctrl runat="server" id="taithingctrl" />

							 <div class="row gy-5 g-xl-8">

								 <div class="col-xxl-4">
										<!--begin::List Widget 5-->
       <Pref:ActivitiesCtrl runat="server" id="ActivitiesCtrl" />
										<!--end: List Widget 5-->
									</div>
								 </div>
							<div class="text-center mb-10">
								<!--begin::Title-->
								<%--<h1 class="text-dark mb-3">Sign In to Portal</h1>--%>
								<!--end::Title-->
								<!--begin::Link-->
								<%--<div class="text-gray-400 fw-bold fs-4">New Here?
								<a href="../../demo4/dist/authentication/flows/basic/sign-up.html" class="link-primary fw-bolder">Create an Account</a></div>--%>
								<!--end::Link-->
							</div>
						</form>
						<!--end::Form-->
					</div>
					<!--end::Wrapper-->
				</div>
				<!--end::Content-->
				<!--begin::Footer-->
				<div class="d-flex flex-center flex-column-auto p-10">
					<!--begin::Links-->
					<%--<div class="d-flex align-items-center fw-bold fs-6">
						<a href="https://keenthemes.com" class="text-muted text-hover-primary px-2">About</a>
						<a href="mailto:support@keenthemes.com" class="text-muted text-hover-primary px-2">Contact</a>
						<a href="https://1.envato.market/EA4JP" class="text-muted text-hover-primary px-2">Contact Us</a>
					</div>--%>
					<!--end::Links-->
				</div>
				<!--end::Footer-->
			</div>
			<!--end::Authentication - Sign-in-->
		</div>
		<!--end::Main-->
		<!--begin::Javascript-->
		<!--begin::Global Javascript Bundle(used by all pages)-->
		<%--<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src="assets/js/custom/authentication/sign-in/general.js"></script>--%>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
	<script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/scripts.bundle.js")%>'></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.js")%>'></script>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/select-location.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/widgets.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/apps/chat/chat.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/create-app.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/upgrade-plan.js")%>'></script>
    
</body>
</html>
