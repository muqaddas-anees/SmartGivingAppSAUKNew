<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPasswordNew.aspx.cs" Inherits="DeffinityAppDev.ResetPasswordNew" %>


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

	 <style>
        
        /* Custom styles for the checkmarks */
        .condition {
            display: flex;
            align-items: center;
            margin-top: 5px;
        }

        .condition .icon {
            color: #dc3545; /* red by default */
            margin-right: 5px;
        }

        .condition.valid .icon {
            color: #28a745; /* green when valid */
        }
          #password-conditions {
            display: none; /* Initially hidden */
        }
    </style>
		<!--begin::Main-->
		<div class="d-flex flex-column flex-root">
			<!--begin::Authentication - Sign-in -->
			<div class="d-flex flex-column flex-column-fluid bgi-position-y-bottom position-x-center bgi-size-contain bgi-attachment-fixed" style="background-image: url(assets/media/illustrations/dozzy-1/14.png?id=39);background-repeat: no-repeat;background-position: center;">
				<!--begin::Content-->
				<div class="d-flex flex-center flex-column flex-column-fluid p-10 pb-lg-20">
					<!--begin::Logo-->
					<a href="#" class="mb-12">
						<img alt="Logo" src="../Content/assets/images/logo-white-bg@2x.png" class="h-110px" style="width:200px" />
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
								<h1 class="text-dark mb-3">Reset Password</h1>
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
								<asp:TextBox ClientIDMode="Static" ID="txtusername" CssClass="form-control" runat="server" AutoCompleteType="Disabled" ReadOnly="true" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter email address" ControlToValidate="txtusername" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>
							<!--end::Input group-->
							<!--begin::Input group-->
							<div class="fv-row mb-10">
								<!--begin::Wrapper-->
								<div class="d-flex flex-stack mb-2">
									<!--begin::Label-->
									<label class="form-label fw-bolder text-dark fs-6 mb-0">Password</label>
									
								</div>
								
								<asp:TextBox ID="txtpwd" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
		 <asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter password" ControlToValidate="txtPwd" ValidationGroup="group1"></asp:RequiredFieldValidator>
				   <div id="password-conditions">
                    <div class="condition" id="length-condition">
                        <span class="icon">&#10060;</span> 8-20 characters
                    </div>
                    <div class="condition" id="uppercase-condition">
                        <span class="icon">&#10060;</span> One uppercase letter
                    </div>
                    <div class="condition" id="special-char-condition">
                        <span class="icon">&#10060;</span> One special character
                    </div>
                </div>
								<!--end::Input-->
							</div>
								<div class="fv-row mb-10">
								<!--begin::Wrapper-->
								<div class="d-flex flex-stack mb-2">
									<!--begin::Label-->
									<label class="form-label fw-bolder text-dark fs-6 mb-0">Confirm Password</label>
									
								</div>
								
								<asp:TextBox ID="TxtCpwd" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
		
				<asp:RequiredFieldValidator ID="confirmPasswordReq" runat="server" ControlToValidate="TxtCpwd" style="font-size:small"
                                ErrorMessage="Please enter confirmation password" SetFocusOnError="True" Display="Dynamic"
                                ValidationGroup="ValInsert" Font-Size="X-Small" ForeColor="Red" />
				 <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtpwd" style="font-size:small"
                            ControlToValidate="TxtCpwd" ErrorMessage="Your passwords do not match up!"
                            Display="Dynamic" ValidationGroup="ValInsert" Font-Size="X-Small" ForeColor="Red"/>
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
								 <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" OnClick="btnforgot_Click" ValidationGroup="group1" Text="Login" TabIndex="0" Width="95%" disabled ></asp:Button>
								
									<div class="errors-container">
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
										 <asp:Label ID="lblMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
				</div>
					<div class="login-footer" >
						<a href="Default.aspx" > Back to login?</a>
                        <div class="info-links" style="display:none;visibility:hidden;">
						 <label id="Label2" runat="server" visible="false">Powered by Fixxit</label>
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
     <script>
         $(document).ready(function () {
             $('#<%=txtpwd.ClientID%>').on('input', function(){
                var password = $(this).val();
                $('#password-conditions').show();

                var hasUpperCase = /[A-Z]/.test(password);
                var hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);
                var isValidLength = password.length >= 8 && password.length <= 20;
                var isPasswordValid = hasUpperCase && hasSpecialChar && isValidLength;

                // Update condition indicators
                updateConditionIndicator('#length-condition', isValidLength);
                updateConditionIndicator('#uppercase-condition', hasUpperCase);
                updateConditionIndicator('#special-char-condition', hasSpecialChar);

                // Update the validity of the password field and submit button
                $(this).toggleClass('is-invalid', !isPasswordValid);
                $(this).toggleClass('is-valid', isPasswordValid);
                $('#<%=btnsubmit.ClientID%>').prop('disabled', !isPasswordValid);
            });

            function updateConditionIndicator(elementId, isValid) {
                if (isValid) {
                    $(elementId).addClass('valid');
                    $(elementId + ' .icon').html('&#10004;'); // Checkmark
                } else {
                    $(elementId).removeClass('valid');
                    $(elementId + ' .icon').html('&#10060;'); // Cross
                }
            }
        });
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
    </script>
</body>
</html>
