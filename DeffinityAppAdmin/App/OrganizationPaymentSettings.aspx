<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="OrganizationPaymentSettings.aspx.cs" Inherits="DeffinityAppDev.App.OrganizationPaymentSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>

<%@ Register Src="~/App/controls/OrgMainTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>


<asp:Content ID="ContentTabs" runat="server" ContentPlaceHolderID="Tabs">

    <Pref:OrgTabs runat="server" ID="OrgTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Payment Settings</h3>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">Publishable key</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtMarchantID" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Secret key</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtMarchantKey" runat="server" MaxLength="500"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<!--begin::Input group-->
											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="">Salt Passphrase</span>
													
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtSaltPass" runat="server" MaxLength="100"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Mode</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<%--<asp:TextBox ID="txtHost" runat="server" MaxLength="500"></asp:TextBox>--%>
													<asp:DropDownList ID="ddlHost" runat="server" SkinID="ddl_50">
														<asp:ListItem Text="Please select..." Value=""></asp:ListItem>
														<asp:ListItem Text="Test" Value="Test"></asp:ListItem>
														<asp:ListItem Text="Production" Value="Production"></asp:ListItem>
													</asp:DropDownList>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Percentage</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtPercent" runat="server" MaxLength="4" SkinID="Price_150px"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											
										
											
											
											
											
											
											
											
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>
											<%--<button type="submit" class="btn btn-primary" id="kt_docs_sweetalert_basic1">Save Changes</button>
											<asp:Button type="button" id="kt_docs_sweetalert_basic" class="btn btn-primary" OnClick="btnShowAlert_Click" Text="Toggle SweetAlert" runat="server"></asp:Button>
											--%>
											<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save Changes"  />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
