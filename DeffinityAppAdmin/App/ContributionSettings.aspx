<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ContributionSettings.aspx.cs" Inherits="DeffinityAppDev.App.ContributionSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Contribution Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
	
Contribution Settings
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<!--begin::Input group-->
										
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged"  AutoPostBack="true" ></asp:DropDownList>
												</div>


												<div class="col-lg-4">
													
													</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Group</label>
													<asp:DropDownList ID="ddlGroup" runat="server"   OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Contribution</label>
													<asp:TextBox ID="txtGroupContribution" runat="server" SkinID="Price_150px"></asp:TextBox>
													</div>
												<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Updated on</label>
													<asp:TextBox ID="txtGroupDate" runat="server" SkinID="DateNew"></asp:TextBox>
													</div>
												<!--end::Col-->
											</div>
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Denomination</label>
													<asp:DropDownList ID="ddlDenimination" runat="server" OnSelectedIndexChanged="ddlDenimination_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Contribution</label>
													<asp:TextBox ID="txtDenominationContribution" runat="server" SkinID="Price_150px"></asp:TextBox>
													</div>
													<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Updated on</label>
													<asp:TextBox ID="txtDenominationDate" runat="server" SkinID="DateNew"></asp:TextBox>
													</div>
												<!--end::Col-->
											</div>

												<div class="row mb-6">
												<!--begin::Label-->
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Organization</label>
													<asp:DropDownList ID="ddlProtfolio" runat="server" OnSelectedIndexChanged="ddlProtfolio_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
											

												</div>
													<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Contribution</label>
													<asp:TextBox ID="txtPortfolioContribution" runat="server" SkinID="Price_150px"></asp:TextBox>
													</div>
														<div class="col-lg-4">
													<label class="col-lg-4 col-form-label required fw-bold fs-6">Updated on</label>
													<asp:TextBox ID="txtPortfolioDate" runat="server" SkinID="DateNew"></asp:TextBox>
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
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
