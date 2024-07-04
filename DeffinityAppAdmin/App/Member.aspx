<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="DeffinityAppDev.App.MemberForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Member Details
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
	<style>




 select {
            width: 100%;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;

			font-display:block;

			font-size:15px;
        }





		.tags-look .tagify__dropdown__item{
  display: inline-block;
  border-radius: 3px;
  padding: .3em .5em;
  border: 1px solid #CCC;
  background: #F3F3F3;
  margin: .2em;
  font-size: .85em;
  color: black;
  transition: 0s;
}

.tags-look .tagify__dropdown__item--active{
  color: black;
}

.tags-look .tagify__dropdown__item:hover{
  background: lightyellow;
  border-color: gold;
}
	</style>
	<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/documentation/forms/tagify.js")%>'></script>--%>
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Member Details</h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">
										 <asp:HyperLink ID="linkBack" runat="server" Text="Back to Members"></asp:HyperLink>

										 </div>
									<!--end::Card title-->
								</div>
								<!--begin::Card header-->
								<!--begin::Content-->
								<div id="kt_account_profile_details" class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Member Image</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-125px h-125px" style="background-image: url(assets/media/avatars/150-26.jpg)"><asp:Image ID="img" runat="server" Height="100px" /></div>
														<!--end::Preview existing avatar-->
														<!--begin::Label-->
														<label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Change avatar">
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="imgLogo" />
															<input type="hidden" name="avatar_remove">
															<!--end::Inputs-->
														</label>
														<!--end::Label-->
														<!--begin::Cancel-->
														<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="cancel" data-bs-toggle="tooltip" title="" data-bs-original-title="Cancel avatar">
															<i class="bi bi-x fs-2"></i>
														</span>
														<!--end::Cancel-->
														<!--begin::Remove-->
														<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="remove" data-bs-toggle="tooltip" title="" data-bs-original-title="Remove avatar">
															<i class="bi bi-x fs-2"></i>
														</span>
														<!--end::Remove-->
													</div>
													<!--end::Image input-->
													<!--begin::Hint-->
													<div class="form-text">Allowed file types: png, jpg, jpeg.</div>
													<!--end::Hint-->
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Full Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
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
													<asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="250"></asp:TextBox>
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
													<asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Contact</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtContactNumber" runat="server" MaxLength="250"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<!--begin::Input group-->
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
													<span class="required">Town</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Phone number must be active" aria-label="Phone number must be active"></i>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">Zipcode</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtZipcode" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Country</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
												<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<%--<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>--%>
												
												
												
												 <select id="continents"  class="form-select form-select-solid form-select-lg fw-bold"    ></select>


                                                  <asp:HiddenField ID="HiddenFieldReligion" runat="server" Value="0"  />
												
												
												</div>

												<!--end::Col-->
											</div>



											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Group</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<%--<asp:DropDownList ID="DropDownList1" runat="server" ></asp:DropDownList>--%>
											
													<select id="countries" class="form-select form-select-solid form-select-lg fw-bold"></select>

                                                    <asp:HiddenField ID="HiddenFieldGroup" runat="server" Value="0" />

												</div>
												
											</div>








												<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Denomination</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">

													<%--<asp:DropDownList ID="ddlDenimination" runat="server" ></asp:DropDownList>--%>

													<select id="cities" class="form-select form-select-solid form-select-lg fw-bold"></select>


                                                    <asp:HiddenField ID="HiddenFieldDenomination" runat="server" Value="0" />

												</div>
												
											</div>
											<!--end::Input group-->
											
											
											<!--begin::Input group-->
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save Changes"  />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>


	<div class="card mb-5 mb-xl-10" id="pnlPassword" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_signin_method">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Sign-in Method</h3>
									</div>
								</div>
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="kt_account_signin_method" class="collapse show">
									<!--begin::Card body-->
									<div class="card-body border-top p-9">
										<!--begin::Email Address-->
										<div class="d-flex flex-wrap align-items-center">
											<!--begin::Label-->
											<div id="kt_signin_email">
												<div class="fs-6 fw-bolder mb-1">Email Address</div>
												<div class="fw-bold text-gray-600"><%--support@keenthemes.com--%>

													<asp:Label ID="lblEmail" runat="server"></asp:Label>
												</div>
											</div>
											<!--end::Label-->
											<!--begin::Edit-->
											<div id="kt_signin_email_edit" class="flex-row-fluid d-none">
												<!--begin::Form-->
												<form id="kt_signin_change_email" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
													<div class="row mb-6">
														<div class="col-lg-6 mb-4 mb-lg-0">
															<div class="fv-row mb-0 fv-plugins-icon-container">
																<label for="emailaddress" class="form-label fs-6 fw-bolder mb-3">Enter New Email Address</label>
																<input type="email" class="form-control form-control-lg form-control-solid" id="txtemailaddress_update" placeholder="Email Address" name="emailaddress" value="support@keenthemes.com" runat="server">
															<div class="fv-plugins-message-container invalid-feedback"></div></div>
														</div>
														<div class="col-lg-6">
															<div class="fv-row mb-0 fv-plugins-icon-container">
																<label for="confirmemailpassword" class="form-label fs-6 fw-bolder mb-3">Confirm Password</label>
																<input type="password" class="form-control form-control-lg form-control-solid" name="confirmemailpassword" id="txtconfirmemailpassword" runat="server">
															<div class="fv-plugins-message-container invalid-feedback"></div></div>
														</div>
													</div>
													<div class="d-flex">
														<asp:Button ID="btnUpdateEmail" runat="server" Text="Update Email" SkinID="btnDefault" CssClass="btn btn-primary me-2 px-6" OnClick="btnUpdateEmail_Click" />
														<%--<button id="kt_signin_submit" type="button" class="btn btn-primary me-2 px-6">Update Email</button>--%>
														<button id="kt_signin_cancel" type="button" class="btn btn-color-gray-400 btn-active-light-primary px-6">Cancel</button>
													</div>
												<div></div></form>
												<!--end::Form-->
											</div>
											<!--end::Edit-->
											<!--begin::Action-->
											<div id="kt_signin_email_button" class="ms-auto">
												<button class="btn btn-light btn-active-light-primary" style="display:none;visibility:hidden;">Change Email</button>
											</div>
											<!--end::Action-->
										</div>
										<!--end::Email Address-->
										<!--begin::Separator-->
										<div class="separator separator-dashed my-6"></div>
										<!--end::Separator-->
										<!--begin::Password-->
										<div class="d-flex flex-wrap align-items-center mb-10">
											<!--begin::Label-->
											<div id="kt_signin_password">
												<div class="fs-6 fw-bolder mb-1">Password</div>
												<div class="fw-bold text-gray-600">************</div>
											</div>
											<!--end::Label-->
											<!--begin::Edit-->
											<div id="kt_signin_password_edit" class="flex-row-fluid d-none">
												<!--begin::Form-->
												<form id="kt_signin_change_password" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
													<div class="row mb-1">
														<div class="col-lg-4">
															<div class="fv-row mb-0 fv-plugins-icon-container">
																<label for="currentpassword" class="form-label fs-6 fw-bolder mb-3">Current Password</label>
																<input type="password" class="form-control form-control-lg form-control-solid" name="currentpassword" id="txtcurrentpassword" runat="server">
															<div class="fv-plugins-message-container invalid-feedback"></div></div>
														</div>
														<div class="col-lg-4">
															<div class="fv-row mb-0 fv-plugins-icon-container">
																<label for="newpassword" class="form-label fs-6 fw-bolder mb-3">New Password</label>
																<input type="password" class="form-control form-control-lg form-control-solid" name="newpassword" id="txtnewpassword" runat="server">
															<div class="fv-plugins-message-container invalid-feedback"></div></div>
														</div>
														<div class="col-lg-4">
															<div class="fv-row mb-0 fv-plugins-icon-container">
																<label for="confirmpassword" class="form-label fs-6 fw-bolder mb-3">Confirm New Password</label>
																<input type="password" class="form-control form-control-lg form-control-solid" name="confirmpassword" id="txtconfirmpassword" runat="server">
															<div class="fv-plugins-message-container invalid-feedback"></div></div>
														</div>
													</div>
													<div class="form-text mb-5">Password must be at least 8 character and contain symbols</div>
													<div class="d-flex">
														<asp:Button ID="btnUpdatePassword" runat="server" SkinID="btnDefault" Text="Update Password" CssClass="btn btn-primary me-2 px-6" />
														<%--<button id="kt_password_submit" type="button" class="btn btn-primary me-2 px-6">Update Password</button>--%>
														<button id="kt_password_cancel" type="button" class="btn btn-color-gray-400 btn-active-light-primary px-6">Cancel</button>
													</div>
												<div></div></form>
												<!--end::Form-->
											</div>
											<!--end::Edit-->
											<!--begin::Action-->
											<div id="kt_signin_password_button" class="ms-auto">
												<button class="btn btn-light btn-active-light-primary" style="display:none;visibility:hidden;">Reset Password</button>
											</div>
											<!--end::Action-->
										</div>
										<!--end::Password-->
										<!--begin::Notice-->
										<div class="notice d-flex bg-light-primary rounded border-primary border border-dashed p-6">
											<!--begin::Icon-->
											<!--begin::Svg Icon | path: icons/duotune/general/gen048.svg-->
											<span class="svg-icon svg-icon-2tx svg-icon-primary me-4">
												<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
													<path opacity="0.3" d="M20.5543 4.37824L12.1798 2.02473C12.0626 1.99176 11.9376 1.99176 11.8203 2.02473L3.44572 4.37824C3.18118 4.45258 3 4.6807 3 4.93945V13.569C3 14.6914 3.48509 15.8404 4.4417 16.984C5.17231 17.8575 6.18314 18.7345 7.446 19.5909C9.56752 21.0295 11.6566 21.912 11.7445 21.9488C11.8258 21.9829 11.9129 22 12.0001 22C12.0872 22 12.1744 21.983 12.2557 21.9488C12.3435 21.912 14.4326 21.0295 16.5541 19.5909C17.8169 18.7345 18.8277 17.8575 19.5584 16.984C20.515 15.8404 21 14.6914 21 13.569V4.93945C21 4.6807 20.8189 4.45258 20.5543 4.37824Z" fill="black"></path>
													<path d="M10.5606 11.3042L9.57283 10.3018C9.28174 10.0065 8.80522 10.0065 8.51412 10.3018C8.22897 10.5912 8.22897 11.0559 8.51412 11.3452L10.4182 13.2773C10.8099 13.6747 11.451 13.6747 11.8427 13.2773L15.4859 9.58051C15.771 9.29117 15.771 8.82648 15.4859 8.53714C15.1948 8.24176 14.7183 8.24176 14.4272 8.53714L11.7002 11.3042C11.3869 11.6221 10.874 11.6221 10.5606 11.3042Z" fill="black"></path>
												</svg>
											</span>
											<!--end::Svg Icon-->
											<!--end::Icon-->
											<!--begin::Wrapper-->
											<div class="d-flex flex-stack flex-grow-1 flex-wrap flex-md-nowrap">
												<!--begin::Content-->
												<div class="mb-3 mb-md-0 fw-bold">
													<h4 class="text-gray-900 fw-bolder">Secure Your Account</h4>
													<div class="fs-6 text-gray-700 pe-7">Two-factor authentication adds an extra layer of security to your account. To log in, in addition you'll need to provide a 6 digit code</div>
												</div>
												<!--end::Content-->
												<!--begin::Action-->
												<a href="#" class="btn btn-primary px-6 align-self-center text-nowrap" data-bs-toggle="modal" data-bs-target="#kt_modal_two_factor_authentication">Enable</a>
												<%--<asp:Button ID="btnEnable" runat="server" />--%>
												<!--end::Action-->
											</div>
											<!--end::Wrapper-->
										</div>
										<!--end::Notice-->
									</div>
									<!--end::Card body-->
								</div>
								<!--end::Content-->
							</div>
		<!--begin::Deactivate Account-->

	<div class="card mb-5 mb-xl-10" id="pnlskills" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Skills</h3>
									</div>
								</div>
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="kt_account_tags" class="collapse show">
									<!--begin::Form-->
								<%--	<form id="kt_account_deactivate_form" class="form">--%>
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<div class="row">
												<div class="col-lg-8">
												<%--<input class="form-control" value="tag1, tag2, tag3" id="kt_tagify_1"/>--%>
												<input id="txtSkills" runat="server" name="input-custom-dropdown" class="form-control" placeholder="write some tags" value="" style="width:50%" />
												<%--<​input name="input-custom-dropdown" class="form-control" placeholder="write some tags" value="css, html, javascript"/>--%>
													</div>
											</div>
											<!--begin::Notice-->
											<%--<div class="notice d-flex bg-light-warning rounded border-warning border border-dashed mb-9 p-6">
												<!--begin::Icon-->
												<!--begin::Svg Icon | path: icons/duotune/general/gen044.svg-->
												<span class="svg-icon svg-icon-2tx svg-icon-warning me-4">
													<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
														<rect opacity="0.3" x="2" y="2" width="20" height="20" rx="10" fill="black" />
														<rect x="11" y="14" width="7" height="2" rx="1" transform="rotate(-90 11 14)" fill="black" />
														<rect x="11" y="17" width="2" height="2" rx="1" transform="rotate(-90 11 17)" fill="black" />
													</svg>
												</span>
												<!--end::Svg Icon-->
												<!--end::Icon-->
												<!--begin::Wrapper-->
												<div class="d-flex flex-stack flex-grow-1">
													<div class="mb-10">
    <label class="form-label">Default input style</label>
  
													
</div>
												
												</div>
												<!--end::Wrapper-->
											</div>--%>
											
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="Button1" runat="server" CausesValidation="false" OnClick="btnUpdateSkill_Click" Text="Update" />
											<%--<button id="kt_account_deactivate_account_submit" type="submit" class="btn btn-danger fw-bold" runat="server" onclick="">Deactivate Account</button>--%>
										</div>
										<!--end::Card footer-->
									<%--</form>--%>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>

	<div class="card mb-5 mb-xl-10" id="pnlDocuments" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Documents</h3>
									</div>
								</div>
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="11kt_account_deactivate" class="collapse show">
									<!--begin::Form-->
								<%--	<form id="kt_account_deactivate_form" class="form">--%>
										<!--begin::Card body-->
										<div class="card-body border-top p-9">

											<div class="row mb-6">
												<!--begin::Label-->
												<%--<label class="col-lg-4 col-form-label required fw-bold fs-6"></label>--%>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-10 fv-row fv-plugins-icon-container">
													 <div class="dropzone dropzone-queue mb-2" id="kt_dropzonejs_example_3">
                <!--begin::Controls-->
                <div class="dropzone-panel mb-lg-0 mb-2">
                    <a class="dropzone-select btn btn-sm btn-bg-light me-2">Drop files here or click to upload</a>
                    <a class="dropzone-remove-all btn btn-sm btn-light-primary">Remove All</a>
                </div>
                <!--end::Controls-->

                <!--begin::Items-->
                <div class="dropzone-items wm-200px">
                    <div class="dropzone-item" style="display:none">
                        <!--begin::File-->
                        <div class="dropzone-file">
                            <div class="dropzone-filename" title="some_image_file_name.jpg">
                                <span data-dz-name>some_image_file_name.jpg</span>
                                <strong>(<span data-dz-size>340kb</span>)</strong>
                            </div>

                            <div class="dropzone-error" data-dz-errormessage></div>
                        </div>
                        <!--end::File-->

                        <!--begin::Progress-->
                        <div class="dropzone-progress">
                            <div class="progress">
                                <div
                                    class="progress-bar bg-primary"
                                    role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress>
                                </div>
                            </div>
                        </div>
                        <!--end::Progress-->

                        <!--begin::Toolbar-->
                        <div class="dropzone-toolbar">
                            <span class="dropzone-delete" data-dz-remove><i class="bi bi-x fs-1"></i></span>
                        </div>
                        <!--end::Toolbar-->
                    </div>
                </div>
                <!--end::Items-->
           <%-- </div>
            <!--end::Dropzone-->

            <!--begin::Hint-->
            <span class="form-text text-muted">Max file size is 1MB and max number of files is 5.</span>
            <!--end::Hint-->
        </div>       --%>
                                                         
                                                         </div>
											

												</div>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
											</div>


											<div class="row mb-6">
												  <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand" >
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false"  />
                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("Text") %>' Text='<%# Eval("Text") %>'></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="30px">
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
												</div>

										
													
</div>
													
												</div>
												<!--end::Wrapper-->
											
											<!--end::Notice-->
											<!--begin::Form input row-->
										<%--	<div class="form-check form-check-solid fv-row">
												<input name="deactivate" class="form-check-input" type="checkbox" value="" id="deactivate" />
												<label class="form-check-label fw-bold ps-2 fs-6" for="deactivate">I confirm my account deactivation</label>
											</div>--%>
											<!--end::Form input row-->
										
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="Button2" runat="server" CausesValidation="false" OnClick="btnUpdateSkill_Click" Text="Update" />
											<%--<button id="kt_account_deactivate_account_submit" type="submit" class="btn btn-danger fw-bold" runat="server" onclick="">Deactivate Account</button>--%>
										</div>
										<!--end::Card footer-->
									<%--</form>--%>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							

							<div class="card" id="pnlDeactive" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Deactivate Account</h3>
									</div>
								</div>
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="1kt_account_deactivate" class="collapse show">
									<!--begin::Form-->
								<%--	<form id="kt_account_deactivate_form" class="form">--%>
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<!--begin::Notice-->
											<div class="notice d-flex bg-light-warning rounded border-warning border border-dashed mb-9 p-6">
												<!--begin::Icon-->
												<!--begin::Svg Icon | path: icons/duotune/general/gen044.svg-->
												<span class="svg-icon svg-icon-2tx svg-icon-warning me-4">
													<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
														<rect opacity="0.3" x="2" y="2" width="20" height="20" rx="10" fill="black" />
														<rect x="11" y="14" width="7" height="2" rx="1" transform="rotate(-90 11 14)" fill="black" />
														<rect x="11" y="17" width="2" height="2" rx="1" transform="rotate(-90 11 17)" fill="black" />
													</svg>
												</span>
												<!--end::Svg Icon-->
												<!--end::Icon-->
												<!--begin::Wrapper-->
												<div class="d-flex flex-stack flex-grow-1">
													<!--begin::Content-->
													<div class="fw-bold">
														<h4 class="text-gray-900 fw-bolder">You Are Deactivating Your Account</h4>
														<div class="fs-6 text-gray-700">For extra security, this requires you to confirm your email or phone number when you reset yousignr password.
														<br />
														<a class="fw-bolder" href="#">Learn more</a></div>
													</div>
													<!--end::Content-->
												</div>
												<!--end::Wrapper-->
											</div>
											<!--end::Notice-->
											<!--begin::Form input row-->
											<div class="form-check form-check-solid fv-row">
												<input name="deactivate" class="form-check-input" type="checkbox" value="" id="deactivate" />
												<label class="form-check-label fw-bold ps-2 fs-6" for="deactivate">I confirm my account deactivation</label>
											</div>
											<!--end::Form input row-->
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="btnDeativate" runat="server" CssClass="btn btn-danger fw-bold" CausesValidation="false" OnClick="btnDeactivate_Click" Text="Deactivate Account" />
											<%--<button id="kt_account_deactivate_account_submit" type="submit" class="btn btn-danger fw-bold" runat="server" onclick="">Deactivate Account</button>--%>
										</div>
										<!--end::Card footer-->
									<%--</form>--%>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
							<!--end::Deactivate Account-->







	<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script type="text/javascript">





		function MyFunction(religionId, groupId, Denominationid) {


			
			

            $(document).ready(function () {



                var continentsDDL = $('#continents');
                var countriesDDL = $('#countries');
                var citiesDDL = $('#cities');





                $.ajax({
                    url: 'WebService/DataServices.asmx/GetContinents',
                    method: 'post',
                    dataType: 'json',
                    success: function (data) {
                        continentsDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        citiesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        countriesDDL.prop('disabled', true);
                        citiesDDL.prop('disabled', true);




                        $(data).each(function (index, item) {
                            continentsDDL.append($('<option/>', { value: item.Id, text: item.Name }));



                        });




                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = religionId;


                        continentsDDL.val(religionId);
                        continentsDDL.prop('disabled', false);

                    },
                    error: function (err) {
                        alert(err);
                    }
                });



                $.ajax({
                    url: 'WebService/DataServices.asmx/GetCountriesByContinentId',
                    method: 'post',
                    dataType: 'json',
                    data: { ContinentId: religionId },
                    success: function (data) {
                        countriesDDL.empty();
                        countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        $(data).each(function (index, item) {
                            countriesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                        });

                       <%-- document.getElementById("countries").value = groupId;--%>


                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = religionId;

                        document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = groupId;

                        countriesDDL.val(groupId);
                        countriesDDL.prop('disabled', false);





                    },
                    error: function (err) {
                        alert(err);
                    }
                });



                $.ajax({
                    url: 'WebService/DataServices.asmx/GetCitiesByCountryId',
                    method: 'post',
                    dataType: 'json',
                    data: { GroupId: groupId },
                    success: function (data) {


                        citiesDDL.empty();
                        citiesDDL.append($('<option/>', { value: 0, text: 'Please select... ' }));
                        $(data).each(function (index, item) {
                            debugger;


                            citiesDDL.append($('<option/>', { value: item.Id, text: item.Name }));




                        });

                        //document.getElementById("cities").value = Denominationid;



                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = religionId;

                        document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = groupId;

                        document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = Denominationid;




                        citiesDDL.val(Denominationid);
                        citiesDDL.prop('disabled', false);
                    },
                    error: function (err) {
                        alert(err);
                    }
                });







            });



        }












        $(document).ready(function () {


            var continentsDDL = $('#continents');
            var countriesDDL = $('#countries');
            var citiesDDL = $('#cities');






            continentsDDL.change(function () {

                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = -1;

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = -1;

                if ($(this).val() == "0") {
                    countriesDDL.empty();
                    citiesDDL.empty();
                    countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                    citiesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                    countriesDDL.val('0');
                    citiesDDL.val('0');
                    countriesDDL.prop('disabled', true);
                    citiesDDL.prop('disabled', true);
                }
                else {
                    citiesDDL.val('0');
                    citiesDDL.prop('disabled', true);

                    var value1 = $('#continents').val();

                    var element = document.getElementById("continents").value;

                    document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = value1;





                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCountriesByContinentId',
                        method: 'post',
                        dataType: 'json',
                        data: { ContinentId: value1 },
                        success: function (data) {
                            countriesDDL.empty();
                            countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                            $(data).each(function (index, item) {
                                countriesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            countriesDDL.val('0');
                            countriesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

			countriesDDL.change(function () {


                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = -1;


                if ($(this).val() == "0") {
                    citiesDDL.empty();
                    citiesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                    citiesDDL.val('0');
                    citiesDDL.prop('disabled', true);
                }
                else {


                    var value = $('#countries').val();

                    document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = value;




                   document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                   

                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCitiesByCountryId',
                        method: 'post',
                        dataType: 'json',
                        data: { GroupId: value },
                        success: function (data) {
                            citiesDDL.empty();
                            citiesDDL.append($('<option/>', { value: 0, text: 'Please select... ' }));
                            $(data).each(function (index, item) {
                                debugger;
                                citiesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            citiesDDL.val('0');
                            citiesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });



            citiesDDL.change(function () {

               

                document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = $('#countries').val();

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = $('#cities').val();


            });


       });
    
















        function FirstFunction() {

           

            $(document).ready(function () {
               

                var continentsDDL = $('#continents');
                var countriesDDL = $('#countries');
                var citiesDDL = $('#cities');




                $.ajax({
                    url: 'WebService/DataServices.asmx/GetContinents',
                    method: 'post',
                    dataType: 'json',
                    success: function (data) {
                        continentsDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        citiesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        countriesDDL.prop('disabled', true);
                        citiesDDL.prop('disabled', true);

                        $(data).each(function (index, item) {
                            continentsDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                        });
                    },
                    error: function (err) {
                        alert(err);
                    }
                });

                continentsDDL.change(function () {



                    if ($(this).val() == "0") {
                        countriesDDL.empty();
                        citiesDDL.empty();
                        countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        citiesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                        countriesDDL.val('0');
                        citiesDDL.val('0');
                        countriesDDL.prop('disabled', true);
                        citiesDDL.prop('disabled', true);
                    }
                    else {
                        citiesDDL.val('0');
                        citiesDDL.prop('disabled', true);

                        var value1 = $('#continents').val();

                        var element = document.getElementById("continents").value;

                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = value1;


                   


                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCountriesByContinentId',
                        method: 'post',
                        dataType: 'json',
                        data: { ContinentId: value1 },
                        success: function (data) {
                            countriesDDL.empty();
                            countriesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                            $(data).each(function (index, item) {
                                countriesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            countriesDDL.val('0');
                            countriesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

                 countriesDDL.change(function () {
                     if ($(this).val() == "0") {
                         citiesDDL.empty();
                         citiesDDL.append($('<option/>', { value: 0, text: 'Please select...' }));
                         citiesDDL.val('0');
                         citiesDDL.prop('disabled', true);
                     }
                     else {


                         var value = $('#countries').val();

                         document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = value;

                    document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                   

                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCitiesByCountryId',
                        method: 'post',
                        dataType: 'json',
                        data: { GroupId: value },
                        success: function (data) {
                            citiesDDL.empty();
                            citiesDDL.append($('<option/>', { value: 0, text: 'Please select... ' }));
                            $(data).each(function (index, item) {
                                debugger;
                                citiesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            citiesDDL.val('0');
                            citiesDDL.prop('disabled', false); 
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });



            citiesDDL.change(function () {

               

                document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = $('#countries').val();

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = $('#cities').val();


            });


            });

        }




    </script>













	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%--<script src="../assets/js/custom/documentation/forms/tagify.js"></script>--%>
	<script>
  //      var input1 = document.querySelector("#kt_tagify_1");
		//new Tagify(input1);

        var input = document.querySelector('#MainContent_MainContent_txtSkills'),
            // init Tagify script on the above inputs
            tagify = new Tagify(input, {
                whitelist: ["Cloud computing", "Artificial intelligence", "Sales leadership", "Analysis", "Translation", "Mobile app development", "People management", "Video production", "Audio production", "UX design", "SEO / SEM marketing","Blockchain","Industrial design","Creativity","Collaboration","Adaptability","Time management","Persuasion","Digital journalism","Animation"],
                maxTags: 50,
                dropdown: {
                    maxItems: 50,           // <- mixumum allowed rendered suggestions
                    classname: "tags-look", // <- custom classname for this dropdown, so it could be targeted
                    enabled: 0,             // <- show suggestions on focus
                    closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
                }
            })
        //var input = document.querySelector('#kt_tagify_1'),
        //    tagify = new Tagify(input, {
        //        whitelist: ['aaa', 'aaab', 'aaabb', 'aaabc', 'aaabd', 'aaabe', 'aaac', 'aaacc'],
        //        dropdown: {
        //            classname: "color-blue",
        //            enabled: 0,              // show the dropdown immediately on focus
        //            maxItems: 5,
        //            position: "text",         // place the dropdown near the typed text
        //            closeOnSelect: false,          // keep the dropdown open after selecting a suggestion
        //            highlightFirst: true
        //        }
        //    });
        //var tagify = new Tagify(input)

        //// bind events
        //tagify.on('add', onAddTag)
        //tagify.DOM.input.addEventListener('focus', onSelectFocus)

        //function onAddTag(e) {
        //    console.log(e.detail)
        //}

        //function onSelectFocus(e) {
        //    console.log(e)
        //}
    </script>

	<script>
        function getQuerystring(key, default_) {

            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href.toLowerCase());
            if (qs == null)
                return default_;
            else
                return qs[1];
        }


        // set the dropzone container id
        var id = "#kt_dropzonejs_example_3";

        // set the preview element template
        var previewNode = $(id + " .dropzone-item");

        previewNode.id = "";
        var previewTemplate = previewNode.parent(".dropzone-items").html();
        previewNode.remove();

        var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
            url: "UploadHandler.ashx?mid=" + getQuerystring('mid'), // Set the url for your upload script location
            parallelUploads: 20,
            maxFilesize: 1, // Max filesize in MB
            previewTemplate: previewTemplate,
            previewsContainer: id + " .dropzone-items", // Define the container to display the previews
            clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
        });


        myDropzone.on("addedfile", function (file) {
            // Hookup the start button
            $(document).find(id + " .dropzone-item").css("display", "");
        });

        // Update the total progress bar
        myDropzone.on("totaluploadprogress", function (progress) {
            $(id + " .progress-bar").css("width", progress + "%");
        });

        myDropzone.on("sending", function (file) {
            // Show the total progress bar when upload starts
            $(id + " .progress-bar").css("opacity", "1");
        });

        // Hide the total progress bar when nothing"s uploading anymore
        myDropzone.on("complete", function (progress) {
            var thisProgressBar = id + " .dz-complete";

            setTimeout(function () {
                $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css("opacity", "0");
            }, 300)
        });
    </script>
</asp:Content>
