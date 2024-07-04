<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AppAdvertSettings.aspx.cs" Inherits="DeffinityAppDev.App.AppAdvertSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">App Advertising Banner Config</h3>
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
										
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" ></asp:DropDownList>
												</div>


												<div class="col-lg-4">
													<%--<asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />--%>
													</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Denomination</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlDenimination" runat="server" ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													<asp:Button ID="btnAddDenimination" runat="server" SkinID="btnDefault"  CssClass="btn btn-light btn-active-light-primary me-2" Text="Copy To"  />
													</div>
												<!--end::Col-->
											</div>
										
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnSave" OnClick="btnSaveChanges_Click" Text="Save Changes"   />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
</asp:Content>
