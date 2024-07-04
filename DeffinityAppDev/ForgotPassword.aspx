r<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DeffinityAppDev.ForgotPassword" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    	<%--<base href="../../../"/>--%>
		<title>Smart Giving Admin</title>
		
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
<body id="kt_body" class=" bg-body">
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
		<div class="d-flex flex-column flex-root">
			<!--begin::Authentication - Sign-in -->
			<div class="d-flex flex-column flex-column-fluid bgi-position-y-bottom position-x-center bgi-size-contain bgi-attachment-fixed" style="background-image: url(assets/media/illustrations/dozzy-1/14.png?id=39);background-repeat: no-repeat;background-position: center;">
				<!--begin::Content-->
				<div class="d-flex flex-center flex-column flex-column-fluid p-10 pb-lg-20">
					<!--begin::Logo-->
					<a href="#" class="mb-12">
						<img alt="Logo" src="../Content/assets/images/logo-white-bg@2x.png?id=11" class="h-110px" style="width:200px" />
					</a>
					<!--end::Logo-->
					<!--begin::Wrapper-->
					<div class="w-lg-500px bg-body rounded shadow-sm p-10 p-lg-15 mx-auto">
						<!--begin::Form-->
						<form class="form w-100" novalidate="novalidate" id="kt_sign_in_form" action="#" runat="server">

							   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
     
   </asp:ScriptManager>
							<!--begin::Heading-->
							<div class="text-center mb-10">
								<!--begin::Title-->
								<h1 class="text-dark mb-3">Forgot Password</h1>
								<!--end::Title-->
								<!--begin::Link-->
								<%--<div class="text-gray-400 fw-bold fs-4">New Here?
								<a href="../../demo4/dist/authentication/flows/basic/sign-up.html" class="link-primary fw-bolder">Create an Account</a></div>--%>
								<!--end::Link-->
							</div>
							<!--begin::Heading-->
							<!--begin::Input group-->
							<div class="fv-row mb-10">
								<!--begin::Label-->
								<label class="form-label fs-6 fw-bolder text-dark">Email</label>
								<!--end::Label-->
								<!--begin::Input-->
								<%--<input class="form-control form-control-lg form-control-solid" type="text" name="email" autocomplete="off" />--%>
								<asp:TextBox ClientIDMode="Static" ID="txtEmail" CssClass="form-control" runat="server" AutoCompleteType="Disabled" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter email" ControlToValidate="txtEmail" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>
							
							<!--end::Input group-->
							<!--begin::Actions-->
							<div class="row mb-6">
								 <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" OnClick="btnsendagain_Click" ValidationGroup="group1" ></asp:Button>
							</div>
							<div class="text-center">
								<!--begin::Submit button-->
								<%--<button type="submit" id="kt_sign_in_submit" class="btn btn-lg btn-primary w-100 mb-5" style="display:none;visibility:hidden;">
									<span class="indicator-label">Continue</span>
									<span class="indicator-progress">Please wait...
									<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
								</button>--%>
								
							
								
									<div class="errors-container">
										<asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor"></asp:Label>
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
				</div>
					<div class="login-footer" >
						<a href="Default.aspx" > Back to login</a>
                        <div class="info-links" style="display:none;visibility:hidden;">
						 <label id="Label2" runat="server" visible="false">Powered by </label>
						</div>
						<div class="info-links" style="display:none;visibility:hidden;">
						 &copy; <label id="lblyear" runat="server"></label> <label id="lblcopyrighttext" runat="server"></label>
						</div>
					</div>
								<!--end::Submit button-->
								<!--begin::Separator-->
								<%--<div class="text-center text-muted text-uppercase fw-bolder mb-5">or</div>
								<!--end::Separator-->
								<!--begin::Google link-->
								<a href="#" class="btn btn-flex flex-center btn-light btn-lg w-100 mb-5">
								<img alt="Logo" src="assets/media/svg/brand-logos/google-icon.svg" class="h-20px me-3" />Continue with Google</a>
								<!--end::Google link-->
								<!--begin::Google link-->
								<a href="#" class="btn btn-flex flex-center btn-light btn-lg w-100 mb-5">
								<img alt="Logo" src="assets/media/svg/brand-logos/facebook-4.svg" class="h-20px me-3" />Continue with Facebook</a>
								<!--end::Google link-->
								<!--begin::Google link-->
								<a href="#" class="btn btn-flex flex-center btn-light btn-lg w-100">
								<img alt="Logo" src="assets/media/svg/brand-logos/apple-black.svg" class="h-20px me-3" />Continue with Apple</a>--%>
								<!--end::Google link-->
							</div>
							<!--end::Actions-->
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
	<%--	<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/select-location.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/widgets.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/apps/chat/chat.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/create-app.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/upgrade-plan.js")%>'></script>--%>
    
</body>
</html>
