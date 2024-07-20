<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DeffinityAppDev.Default" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="manifest" href="/manifest.json" crossorigin="use-credentials" />

    <%--<base href="../../../"/>--%>
    <title><% = Deffinity.systemdefaults.GetInstanceTitle() %> </title>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta charset="utf-8" />
    <meta property="og:locale" content="en_US" />
    <meta property="og:type" content="article" />
    <%--<meta property="og:title" content="Metronic - Bootstrap 5 HTML, VueJS, React, Angular &amp; Laravel Admin Dashboard Theme" />--%>
    <%--<meta property="og:url" content="https://keenthemes.com/metronic" />--%>
    <%--<meta property="og:site_name" content="Keenthemes | Metronic" />--%>
    <%--<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />--%>
    <%--<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
    <!--begin::Fonts-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
    <!--end::Fonts-->
    <!--begin::Global Stylesheets Bundle(used by all pages)-->
    <%--<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />--%>
    <!--end::Global Stylesheets Bundle-->

    <style>
        #eye-icon{
            transition;0.2s;
        }
         #eye-slash-icon{
     transition;0.2s;
 }
    </style>
</head>
<body id="kt_body" class=" bg-body">
    <div class="page-loader">
        <span class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading...</span>
        </span>
    </div>

    <link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css">
    <!--end::Page Vendor Stylesheets-->
    <!--begin::Global Stylesheets Bundle(used by all pages)-->
    <link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css">
    <link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css">
    <!--begin::Main-->
    <div class="d-flex flex-column flex-root">
        <!--begin::Authentication - Sign-in -->
        <div class="d-flex flex-column flex-column-fluid bgi-position-y-bottom position-x-center bgi-size-contain bgi-attachment-fixed" style="background-image: url(assets/media/illustrations/dozzy-1/14.png?id=39);background-repeat: no-repeat;background-position: center;">
            <!--begin::Content-->
            <div class="d-flex flex-center flex-column flex-column-fluid p-10 pb-lg-20">
                <!--begin::Logo-->
                <a href="../../demo4/dist/index.html" class="mb-12">
                    <img alt="Logo" src="../assets/media/logos/logo-1.png?d=112" class="h-110px" style="width:200px" />
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
                            <h1 class="text-dark mb-3">Sign Into The Portal</h1>
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
                            <asp:TextBox ClientIDMode="Static" ID="txtName" CssClass="form-control" runat="server" AutoCompleteType="Disabled"> </asp:TextBox>
                            <asp:RequiredFieldValidator style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Email" ControlToValidate="txtName" ValidationGroup="group1"></asp:RequiredFieldValidator>
                            <!--end::Input-->
                        </div>
                        <!--end::Input group-->
                        <!--begin::Input group-->
                        <div class="fv-row mb-10 position-relative">
                            <div class="d-flex flex-stack mb-2">
                                <label class="form-label fw-bolder text-dark fs-6 mb-0">Password</label>
                            </div>
                            <div class="input-group d-flex flex-nowrap">
                                <asp:TextBox ID="txtPwd" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                                <div class="input-group-append">
                                    <span style="height:46px; border-radius:0 .75rem .75rem 0;cursor:pointer" class="input-group-text eye-icon" title="Toggle Password Visibility" onclick="togglePassword();">
                                      <svg id="eye-icon" class="svg-inline--fa fa-eye" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="eye" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" data-fa-i2svg="">
        <path fill="currentColor" d="M572.5 238.1C518.3 115.5 410.9 32 288 32S57.69 115.6 3.469 238.1C1.563 243.4 0 251 0 256c0 4.977 1.562 12.6 3.469 17.03C57.72 396.5 165.1 480 288 480s230.3-83.58 284.5-206.1C574.4 268.6 576 260.1 576 256C576 251 574.4 243.4 572.5 238.1zM432 256c0 79.45-64.47 144-143.9 144C208.6 400 144 335.5 144 256S208.5 112 288 112S432 176.5 432 256zM288 160C285.7 160 282.4 160.4 279.5 160.8C284.8 170 288 180.6 288 192c0 35.35-28.65 64-64 64C212.6 256 201.1 252.7 192.7 247.5C192.4 250.5 192 253.6 192 256c0 52.1 43 96 96 96s96-42.99 96-95.99S340.1 160 288 160z"></path>
    </svg>

     <svg id="eye-slash-icon" class="svg-inline--fa fa-eye-slash" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="eye-slash" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512" data-fa-i2svg="" style="display:none;">   
         <path fill="currentColor" d="M325.1 351.5L225.8 273.6c8.303 44.56 47.26 78.37 94.22 78.37C321.8 352 323.4 351.6 325.1 351.5zM320 400c-79.5 0-144-64.52-144-143.1c0-6.789 1.09-13.28 1.1-19.82L81.28 160.4c-17.77 23.75-33.27 50.04-45.81 78.59C33.56 243.4 31.1 251 31.1 256c0 4.977 1.563 12.6 3.469 17.03c54.25 123.4 161.6 206.1 284.5 206.1c45.46 0 88.77-11.49 128.1-32.14l-74.5-58.4C356.1 396.1 338.1 400 320 400zM630.8 469.1l-103.5-81.11c31.37-31.96 57.77-70.75 77.21-114.1c1.906-4.43 3.469-12.07 3.469-17.03c0-4.976-1.562-12.6-3.469-17.03c-54.25-123.4-161.6-206.1-284.5-206.1c-62.69 0-121.2 21.94-170.8 59.62L38.81 5.116C34.41 1.679 29.19 0 24.03 0C16.91 0 9.839 3.158 5.121 9.189c-8.187 10.44-6.37 25.53 4.068 33.7l591.1 463.1c10.5 8.203 25.57 6.333 33.69-4.073C643.1 492.4 641.2 477.3 630.8 469.1zM463.1 256c0 24.85-6.705 47.98-17.95 68.27l-38.55-30.23c5.24-11.68 8.495-24.42 8.495-38.08c0-52.1-43-96-95.1-96c-2.301 .0293-5.575 .4436-8.461 .7658C316.8 170 319.1 180.6 319.1 192c0 10.17-2.561 19.67-6.821 28.16L223.6 149.9c25.46-23.38 59.12-37.93 96.42-37.93C399.5 112 463.1 176.6 463.1 256z"></path></svg>
                                    </span>
                                </div>
                            </div>
                            <asp:RequiredFieldValidator style="font-size:small" ID="Rfv2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Password" ControlToValidate="txtPwd" ValidationGroup="group1"></asp:RequiredFieldValidator>
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
                            <asp:LinkButton ID="btnsubmit" runat="server" SkinID="BtnLinkLogin" OnClick="btnsubmit_Click" ValidationGroup="group1" Text="Login" TabIndex="0"></asp:LinkButton>

                            <div class="errors-container">
                                <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" Visible="false"></asp:Label>
                            </div>
                            <div class="login-footer">
                                <a style="font-size:14px;" href="ForgotPassword.aspx"> Forgot your password?</a>
                                <br /><br />
                                <a style="font-size:14px;" href="https://portal.plegit.ai/registration" >Register for a FREE account</a>
                                <br /><br />
                                <%--  <div class="info-links">

                                    <span id="Label2" runat="server" style="color:gray;font-size:18px;width:120px">Powered by </span>
                                    <img  height="25px;" style="margin-bottom:10px"  src="Content/images/fiserv.png"  />


                                </div>--%>
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
              
                <!--end::Links-->
            </div>
            <!--end::Footer-->
        </div>
        <!--end::Authentication - Sign-in-->
    </div>
    <!--end::Main-->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/js/all.min.js"></script>
    <script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
    <script src='<%:ResolveClientUrl("~/assets/js/scripts.bundle.js")%>'></script>
    <!--begin::Page Custom Javascript(used by this page)-->
    <script src="assets/js/custom/authentication/sign-in/general.js"></script>
    <script src='<%:ResolveClientUrl("~/assets/js/custom/modals/create-account.js")%>'></script>
    <script src="assets/plugins/global/plugins.bundle.js"></script>
    <script src="assets/js/scripts.bundle.js"></script>
    <script src="assets/js/custom/authentication/sign-in/general.js"></script>
    <script src="assets/js/custom/modals/create-account.js"></script>
    <!--end::Page Custom Javascript-->
    <!--end::Javascript-->
    <!--begin::Javascript-->
    <script>
        var hostUrl = "assets/";
        function togglePassword() {
            var passwordInput = document.getElementById('txtPwd');
            var eyeicon = document.getElementById('eye-icon');
            var eyeslashicon = document.getElementById('eye-slash-icon')
            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                eyeicon.style.display = 'none'
                eyeslashicon.style.display=''
            } else {
                passwordInput.type = 'password';
                eyeslashicon.style.display = 'none'
                eyeicon.style.display = ''
             
            }
        }
        document.getElementById('<%= txtPwd.ClientID %>').addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                document.getElementById('<%= btnsubmit.ClientID %>').click();
             }
         });
    </script>
</body>
</html>
