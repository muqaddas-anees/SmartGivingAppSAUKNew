<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="InternationalSettings.aspx.cs" Inherits="DeffinityAppDev.App.InternationalSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Settings</h3>
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
											
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Country</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country of origination" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlCountry" runat="server"><asp:ListItem Text="Please select..."></asp:ListItem></asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
											



											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Default Language</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country of origination" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <asp:DropDownList ID="ddlLanguage" runat="server">
                                                        <asp:ListItem Text="Please select..."></asp:ListItem>
                                                        <asp:ListItem Value="English">English</asp:ListItem>
                                                        <asp:ListItem Value="Swedish">Swedish</asp:ListItem>
                                                        <asp:ListItem Value="German">German</asp:ListItem>
                                                        <asp:ListItem Value="Russian">Russian</asp:ListItem>
                                                        <asp:ListItem Value="Italian">Italian</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>



											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Currency </span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country of origination" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="DropDownList2" runat="server">
														<asp:ListItem Text="Please select..."></asp:ListItem>
														<asp:ListItem value="USD" >United States Dollars</asp:ListItem>
														<asp:ListItem value="EUR">Euro</asp:ListItem>
														<asp:ListItem value="GBP">United Kingdom Pounds</asp:ListItem>

														<asp:ListItem value="INR">India Rupees</asp:ListItem>
														


													</asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>




											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Currency Symbol</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country of origination" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="DropDownList3" runat="server">
														<asp:ListItem Text="Please select..."></asp:ListItem>
														<asp:ListItem Value="$">$</asp:ListItem>
														<asp:ListItem Value="£">£</asp:ListItem>
														<asp:ListItem Value="₹">₹</asp:ListItem>
                                                      




													</asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>



										
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
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

</asp:Content>