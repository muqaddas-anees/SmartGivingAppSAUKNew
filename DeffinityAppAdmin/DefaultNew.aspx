<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultNew.aspx.cs" Inherits="DeffinityAppDev.WF.DefaultNew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->
	
</head>
<body id="kt_body" class="bg-body">
    
     
		<!--begin::Main-->
		<div class="d-flex flex-column flex-root">
			<!--begin::Authentication - Sign-in -->
			<div class="d-flex flex-column flex-column-fluid bgi-position-y-bottom position-x-center bgi-no-repeat bgi-size-contain bgi-attachment-fixed" style="background-image: url(assets/media/illustrations/dozzy-1/14.png">
				<!--begin::Content-->
				<div class="d-flex flex-center flex-column flex-column-fluid p-10 pb-lg-20">
					<!--begin::Logo-->
					<a href="../../demo4/dist/index.html" class="mb-12">
						<img alt="Logo" src="assets/media/logos/logo-1.svg" class="h-40px" />
					</a>
					<!--end::Logo-->
					<!--begin::Wrapper-->
					<div class="w-lg-500px bg-body rounded shadow-sm p-10 p-lg-15 mx-auto">
						<!--begin::Form-->
						<form class="form w-100" novalidate="novalidate" id="kt_sign_in_form" action="#" runat="server">

							   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
           <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
							<!--begin::Heading-->
							<div class="text-center mb-10">
								<!--begin::Title-->
								<h1 class="text-dark mb-3">Sign In to Admin</h1>
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
								<asp:TextBox ClientIDMode="Static" ID="txtName" CssClass="form-control" runat="server" AutoCompleteType="Disabled" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Username" ControlToValidate="txtName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>
							<!--end::Input group-->
							<!--begin::Input group-->
							<div class="fv-row mb-10">
								<!--begin::Wrapper-->
								<div class="d-flex flex-stack mb-2">
									<!--begin::Label-->
									<label class="form-label fw-bolder text-dark fs-6 mb-0">Password</label>
									<!--end::Label-->
									<!--begin::Link-->
									<%--<a href="../../demo4/dist/authentication/flows/basic/password-reset.html" class="link-primary fs-6 fw-bolder">Forgot Password ?</a>--%>
									<!--end::Link-->
								</div>
								<!--end::Wrapper-->
								<!--begin::Input-->
								<%--<input class="form-control form-control-lg form-control-solid" type="password" name="password" autocomplete="off" />--%>
								<asp:TextBox ID="txtPwd" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
		 <asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Password" ControlToValidate="txtPwd" ValidationGroup="group1"></asp:RequiredFieldValidator>
				
								<!--end::Input-->
							</div>
							<!--end::Input group-->
							<!--begin::Actions-->
							<div class="text-center">
								<!--begin::Submit button-->
								<button type="submit" id="kt_sign_in_submit" class="btn btn-lg btn-primary w-100 mb-5" style="display:none;visibility:hidden;">
									<span class="indicator-label">Continue</span>
									<span class="indicator-progress">Please wait...
									<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
								</button>
								 <asp:LinkButton ID="btnsubmit" runat="server" SkinID="BtnLinkLogin" OnClick="btnsubmit_Click" ValidationGroup="group1" Text="Login" TabIndex="0" ></asp:LinkButton>
								
									<div class="errors-container">
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
				</div>
					<div class="login-footer" style="display:none;visibility:hidden;">
						<a href="ForgotPassword.aspx" style="display:none;visibility:hidden;"> Forgot your password?</a>
                        <div class="info-links" style="display:none;visibility:hidden;">
						 <label id="Label2" runat="server" visible="false">Powered by First Data Field Services</label>
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
		<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src="assets/js/custom/authentication/sign-in/general.js"></script>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
	
    
</body>
</html>
