<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="DeffinityAppDev.Registration" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    	<%--<base href="../../../"/>--%>
		<title><% = Deffinity.systemdefaults.GetInstanceTitle() %> </title>
		
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
					<a href="https://plegit.ai" class="mb-12">
						<img alt="Logo" src="../Content/assets/images/logo-white-bg@2x.png?id=12" class="h-110px" style="width:200px" />
					</a>
					<!--end::Logo-->
					<!--begin::Wrapper-->
					<div class="w-lg-600px bg-body rounded shadow-sm p-10 p-lg-15 mx-auto">
						<!--begin::Form-->
						<form class="form w-100" novalidate="novalidate" id="kt_sign_in_form" action="#" runat="server">

							   <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
     
							</asp:ScriptManager>
							<!--begin::Heading-->
							<div class="text-center mb-5">
								<!--begin::Title-->
								<h1 class="text-dark mb-3"> <asp:Label ID="lblInstance" runat="server"></asp:Label> Registration
								</h1>
								
							</div>
							<div class="text-center mb-10">
								<h5 class="text-dark mb-3"> 
									<p> Please complete the following registration form. Once you’ve completed it, you’ll receive an email with a link to confirm your email. Please note: you can only log into Plegit once you confirm the link in the email. </p>
									</h5>
								</div>
							<!--begin::Heading-->
							<!--begin::Input group-->
							<div class="fv-row mb-6">
								<!--begin::Label-->
								<label class="form-label fs-6 fw-bolder required text-dark">First Name</label>
								
								<asp:TextBox ClientIDMode="Static" ID="txtFirstName" runat="server"> </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter First name" ControlToValidate="txtFirstName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>
							<div class="fv-row mb-6">
								<!--begin::Label-->
								<label class="form-label fs-6 fw-bolder required text-dark">Last Name</label>
								
								<asp:TextBox ClientIDMode="Static" ID="txtLastName" runat="server"> </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" 
							ForeColor="Red" ErrorMessage="Please enter Last name" ControlToValidate="txtLastName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>
								<div class="fv-row mb-6">
								<!--begin::Label-->
								<label class="form-label fs-6 fw-bolder required text-dark">Email</label>
								
								<asp:TextBox ClientIDMode="Static" ID="txtEmail" runat="server"> </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Email" ControlToValidate="txtEmail" ValidationGroup="group1" ></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator 
    ID="regexEmailValidator" 
    runat="server" 
    ControlToValidate="txtEmail"
    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
    ErrorMessage="Please enter valid Email "
    Display="Dynamic"
    ForeColor="Red" ValidationGroup="group1" >
</asp:RegularExpressionValidator>
								<!--end::Input-->
							</div>
							<div class="fv-row mb-6">
								<!--begin::Label-->
								<label class="form-label fs-6 fw-bolder required text-dark">Password</label>
								
								 <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="newPasswordReq" runat="server" ControlToValidate="txtNewPwd"
                  ErrorMessage="Please enter new password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator 
    ID="regexPasswordLength" 
    runat="server" 
    ControlToValidate="txtNewPwd"
    ValidationExpression="^.{8,20}$" 
    ErrorMessage="Password must be between 8 to 20 characters long." SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert">
</asp:RegularExpressionValidator>

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
							<div class="fv-row mb-6">
								<label class="form-label fs-6 fw-bolder required text-dark">Confirm Password:</label>
								 <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password"></asp:TextBox><br />
               <asp:RequiredFieldValidator ID="confirmPasswordReq" runat="server" ControlToValidate="txtConfirmPwd"
                   ErrorMessage="Please enter confirmation password" SetFocusOnError="True" Display="Dynamic"
                   ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPwd"
                    ControlToValidate="txtConfirmPwd" ErrorMessage="Your passwords do not match up"
                    Display="Dynamic" ValidationGroup="ValInsert"></asp:CompareValidator>

								</div>
							<div class="fv-row mb-6">
								<label class="form-label fs-6 fw-bolder required text-dark">Organization Name</label>
								<asp:TextBox ClientIDMode="Static" ID="txtOrganisationname" CssClass="form-control" runat="server" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Organization Name" ControlToValidate="txtOrganisationname" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>
							<%--<div class="fv-row mb-6">
								<label class="form-label fs-6 fw-bolder required text-dark">Org Registration Number</label>
								<asp:TextBox ClientIDMode="Static" ID="txtOrgRegistrationNumber" CssClass="form-control" runat="server" > </asp:TextBox>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Org Registration Number" ControlToValidate="txtOrgRegistrationNumber" ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>--%>
								<%--<div class="fv-row mb-6">
								<label class="form-label fs-6 fw-bolder required text-dark">Upload Registration Certificate</label>
								 <asp:FileUpload runat="server" id="imgFile" CssClass="form-control" Text="Upload" />
								<!--end::Input-->
							</div>--%>
							<div class="fv-row mb-6">
								<label class="form-label fs-6 fw-bolder required text-dark">Country</label>
								<asp:DropDownList ID="ddlCountry" runat="server" SkinID=""><asp:ListItem Text="Please select..."></asp:ListItem></asp:DropDownList>
								<!--end::Input-->
							</div>
							<div class="fv-row mb-6" style="display:none;visibility:hidden;">
								<label class="form-label fs-6 fw-bolder required text-dark">Referral code</label>
								<asp:TextBox ClientIDMode="Static" ID="txtRefCode" CssClass="form-control" runat="server" ReadOnly="true"> </asp:TextBox>
                       
								<!--end::Input-->
							</div>
							<%--<div class="fv-row mb-6">
								<label class="form-label fs-6 fw-bolder required text-dark">Average Donation Per Month</label>
								<asp:DropDownList ID="ddlAmount" runat="server">
									<asp:ListItem Text="Please select..." Value="Please select..."></asp:ListItem>
									<asp:ListItem Text="0 – 249,999" Value="0 – 249,999"></asp:ListItem>
<asp:ListItem Text="250,000 - 499,999" Value="250,000 - 499,999"></asp:ListItem>
<asp:ListItem Text="500,000 - 749,999" Value="500,000 - 749,999"></asp:ListItem>
<asp:ListItem Text="750,000 - 999,999" Value="750,000 - 999,999"></asp:ListItem>
<asp:ListItem Text="1,000,000 +" Value="1,000,000 +"></asp:ListItem>
								</asp:DropDownList>
                        <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Average Donation Per Month" ControlToValidate="ddlAmount" InitialValue="Please select..." ValidationGroup="group1" ></asp:RequiredFieldValidator>
								<!--end::Input-->
							</div>--%>
							
							<!--end::Input group-->
							<!--begin::Input group-->
						
								
							<!--end::Input group-->
							<!--begin::Actions-->
							<div class="text-center">
								<!--begin::Submit button-->
								<button type="submit" id="kt_sign_in_submit" class="btn btn-lg btn-primary w-100 mb-5" style="display:none;visibility:hidden;">
									<span class="indicator-label">Continue</span>
									<span class="indicator-progress">Please wait...
									<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
								</button>
								 <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" OnClick="btnsubmit_Click" ValidationGroup="group1" Text="Signup" TabIndex="0" Width="95%" ></asp:Button>
								
									<div class="errors-container">
				<asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
										 <asp:Label ID="lblMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
				</div>
					<div class="login-footer" >
						<%--<a href="Default.aspx" > Back to login?</a>--%>
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
            $('#<%=txtNewPwd.ClientID%>').on('input', function(){
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

		function resend() {
			//alert('resend');
        }

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
            }).then((result) => {
                if (result.isConfirmed) {
                    // Replace 'your-new-page.html' with the URL of the page you want to navigate to
                    window.location.href = 'https://plegit.ai';
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

