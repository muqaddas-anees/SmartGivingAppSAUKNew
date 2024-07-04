<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddEducationalVideo.aspx.cs" Inherits="DeffinityAppDev.App.AddEducationalVideo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



	 <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }
        </style>

    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Add Video</h3>
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
											<div class="row mb-6" style="display:none;visibility:hidden">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
												</div>
												
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6"  style="display:none;visibility:hidden">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Denomination</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlDenimination" runat="server" ></asp:DropDownList>
												</div>
												<!--end::Col-->
											</div>

												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Category</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<asp:DropDownList ID="ddlCategoryID" runat="server" ></asp:DropDownList>
												</div>


												<div class="col-lg-4">
													<%--<asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />--%>
													</div>
												<!--end::Col-->
											</div>
												<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Title</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtTitle" runat="server"   ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->


												<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Video Url </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="TextBoxVideourl" runat="server"   ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											</div>
										
											<!--end::Input group-->


										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnSave" OnClick="btnSaveChanges_Click" Text="Save Changes"   />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							


	  <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display:none;"
                       Width="450px" Height="190px"   ScrollBars="None">

		   <div class="card card-bordered">
    <div class="card-header">
        <h3 class="card-title"><asp:Label ID="lblModelHeading" runat="server" Text="Add Religion"></asp:Label> </h3>
        <div class="card-toolbar">
           <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
        </div>
    </div>
    <div class="card-body">
         <div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-6 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtAddReligion" runat="server"></asp:TextBox>
											

												</div>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
											</div>
    </div>
    <div class="card-footer d-flex justify-content-end py-6 px-9">
       	<asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" />
				<asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />
    </div>
</div>
     
           </asp:Panel>

</asp:Content>
