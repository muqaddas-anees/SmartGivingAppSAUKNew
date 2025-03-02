﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DonationResultNew.aspx.cs" Inherits="DeffinityAppDev.App.DonationResultNew" %>

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
	<%--	<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
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
									<div class="text-center mb-5">
												<!--begin::Title-->
												<h3 class="fs-2hx text-dark mb-15" style="padding-bottom:50px;"><asp:Literal ID="lblOrgname" runat="server" Text="Org"></asp:Literal> <asp:HiddenField ID="hurl" runat="server" /> </h3>
												<!--end::Title-->
												<!--begin::Text-->
												<%--<div class="fs-5 text-muted fw-bold">Within the last 10 years, we have sold over 100,000 admin theme copies that have been
												<br />successfully deployed by small businesses to global enterprises</div>--%>
												<!--end::Text-->
											</div>
											<!--end::Top-->
											<!--begin::Overlay-->
											
												
												<div class="row mb-6" id="pnlFist" runat="server">

													<div class="row mb-10 text-center" style="padding-bottom:20px;">
														<p>
														<asp:Label ID="lblResult" runat="server" Text="We're Doing More With Your Support. Thank you For Your Donation" Font-Size="22px" ></asp:Label></p>

														
													</div>
													<div class="row text-center">
														
														<asp:Label ID="Label1" runat="server" Text="Would you like to set up an account?" Font-Size="22px"></asp:Label>
														<br /><br />

													</div>
													<div class="row mb-6 text-center">
														<div class ="col-lg-12">
															<asp:Button ID="btnNO" runat="server" OnClick="btnNO_Click" SkinID="btnDefault" Text="Skip" />

															<asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text ="Yes Please" SkinID="btnDefault" />
														</div>
														</div>
													</div>
												

												<div class="row mb-6" id="pnlUserBasic" runat="server" >

													<div class="row mb-10 text-center" >
														<p>
														<asp:Label ID="Label3" runat="server" Text="We just need a few basic details from you..." Font-Size="18px" Font-Bold="true" ></asp:Label></p>

														
													</div>
													<div class="col-lg-8" >
													<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Full Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtFirstName" runat="server" placeholder="First name"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtSurname" runat="server" placeholder="Last name"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Email Address</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="250" placeholder="Email"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Password</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password" placeholder="Password"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Cell Number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtContactNumber" runat="server" MaxLength="250" placeholder="Contact"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>

													<div class="row mb-6">
														<div class ="col-lg-12 d-flex justify-content-around" style="margin-left:20px">
															
															<asp:Button ID="btnBackContinue" runat="server" OnClick="btnBackContinue_Click" Text ="Back" SkinID="btnDefault" />
															<asp:Button ID="btnContinue" runat="server" OnClick="btnContinue_Click" Text ="Next" SkinID="btnDefault" />
														</div>
														</div>
														</div>
													</div>

												

													<div class="row mb-6" id="pnlUserAddress" runat="server" >
														<div class="row mb-10 text-center" >
														<p>
														<asp:Label ID="Label4" runat="server" Text="Awesome! On occasions we like to send our donors a little thank you in the post." Font-Size="18px" Font-Bold="true" ></asp:Label></p>

														
													</div>
														<div class="col-lg-8">
															

														<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Address</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtAddress" runat="server" MaxLength="4000"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">City</span>
													<i class="fas  ms-1 fs-7" data-bs-toggle="tooltip" title="" ></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtTown" runat="server" MaxLength="100"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">State</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtState" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
															<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6"><%=Resources.DeffinityRes.Postcode %> </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row d-flex d-inline">
													<asp:TextBox ID="txtZipcode" runat="server" MaxLength="100" SkinID="txt_150px" style="margin-right:30px"></asp:TextBox> <%--<asp:Button style="margin-left:30px" ID="btnLookup" runat="server" Text="Look Up" CssClass="btn btn-light" OnClick="btnLookup_Click" />--%>
												</div>
												<!--end::Col-->
											</div>
										

															<div class="row mb-6">
																<label class="col-lg-4 col-form-label fw-bold fs-6"></label>
														<div class ="col-lg-12 d-flex justify-content-between" style="margin-left:20px">
															
															<asp:Button ID="btnBackSave" runat="server" OnClick="btnBackSave_Click" SkinID="btnDefault" Text="Skip" />
															<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text ="Finish" SkinID="btnDefault" />
															
														</div>
														</div>

															</div>

														</div>
												<div class="row mb-6" id="pnlResult" runat="server">
													<div class="row mb-10 text-center">
														<p>
														<asp:Label ID="Label2" runat="server" Text="Thank you" Font-Size="22px" Font-Bold="true"></asp:Label></p>

														<p>
														<%--<asp:Label ID="Label3" runat="server" Text="Do want to be a member?" Font-Size="22px"></asp:Label></p>--%>

													</div>
													<div class="row mb-6  text-center">
														<div class ="col-lg-12  text-center">
															

															<asp:Button ID="btnBacktologin" runat="server" OnClick="btnBacktologin_Click" Text ="Back to Home" SkinID="btnDefault" />
														</div>
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
</body>
</html>
