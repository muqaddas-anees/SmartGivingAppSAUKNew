<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FaithEducationConfig.aspx.cs" Inherits="DeffinityAppDev.App.FaithEducationConfig" %>
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
										<h3 class="fw-bolder m-0">Faith Education Config</h3>
									</div>
									  <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">
                <%--  <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-bg-light" Text="Upload Members" OnClick="btnUpload_Click" style="margin-right:20px;"   /> --%>
                <asp:Button ID="btnAddVideo" runat="server" CssClass="btn btn-primary" Text="Add New" OnClick="btnAddVideo_Click" />

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
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
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
													<asp:DropDownList ID="ddlDenimination" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlDenimination_SelectedIndexChanged" ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													<asp:Button ID="btnAddDenimination" runat="server" SkinID="btnDefault"  CssClass="btn btn-light btn-active-light-primary me-2" Text="Copy To" Visible="false"  />
													</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Category</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<asp:DropDownList ID="ddlCategoryID" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
												</div>


												<div class="col-lg-4">
													<%--<asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />--%>
													</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">

													</div>
											<div class="row mt-10">
												<asp:ListView ID="ListFaithGiving" runat="server" OnItemCommand="ListFaithGiving_ItemCommand">
														<LayoutTemplate>
        <div class="row mb-6">
			<div runat="server"  id="itemPlaceholder"></div>
			
			</div>
															</LayoutTemplate>

														<ItemTemplate>
															<div class="col-lg-3 p-5 ">
															<a
    class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-175px"
    style= "background-image:url('https://img.youtube.com/vi/<%# Eval("VideoID") %>/0.jpg') "
    data-fslightbox="lightbox-youtube"
    href="https://www.youtube.com/embed/<%# Eval("VideoID") %>"
    >
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>
<div class="row mb-6"> <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("Title") %>' Font-Size="Medium"></asp:Label> </div>
<div class="row">
	<div class="col-lg-12" style="text-align:center;">
<asp:LinkButton SkinID="BtnLinkEdit" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="edit1" />	
	<asp:LinkButton SkinID="BtnLinkDelete" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="return confirm('Do you want to delete record?');" /> </div></div>
																</div>
														</ItemTemplate>
													</asp:ListView>
											</div>
										
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											
										</div>
										<!--end::Actions-->
									<input type="hidden"/><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
    <script src="../assets/plugins/custom/fslightbox/fslightbox.bundle.js"></script>
</asp:Content>
