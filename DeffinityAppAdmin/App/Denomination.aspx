<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Denomination.aspx.cs" Inherits="DeffinityAppDev.App.Denomination" %>

<%@ Register Src="~/App/controls/OrgTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="page_title">
	Denomination
    
</asp:Content>


<asp:Content ID="ContentTabs" runat="server" ContentPlaceHolderID="Tabs">

    <Pref:OrgTabs runat="server" ID="OrgTabs" />
</asp:Content>


<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

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
								<div class="card-header border-0 cursor-pointer" aria-controls="kt_account_profile_details">

									   <h3 class="card-title">Denomination</h3>
        <div class="card-toolbar">
             <asp:Button ID="btnSettings" runat="server" Text ="Contribution settings" SkinID="btnDefault" OnClick="btnSettings_Click" />
        </div>
									<!--begin::Card title-->
									
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
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged"  AutoPostBack="true" ></asp:DropDownList>
												</div>


												<div class="col-lg-4">
													<asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />
													<asp:Button ID="btn" runat="server" SkinID="btnDefault" OnClick="btnEditReligion_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Edit"  />
													<asp:LinkButton SkinID="BtnLinkDelete" runat="server" OnClientClick ="Do you want to delete this record?" OnClick="btnDeleteReligion_Click"></asp:LinkButton>
													</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->










											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Group</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlGroup" runat="server"   OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													<asp:Button ID="btnAddGroup" runat="server" SkinID="btnDefault" OnClick="btnAddGroup_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add"  />
													<asp:Button ID="btnEditGroup" runat="server" SkinID="btnDefault" OnClick="btnEditGroup_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Edit"  />
													<asp:LinkButton SkinID="BtnLinkDelete" runat="server" OnClientClick ="Do you want to delete this record?" OnClick="btnDeleteGroup_Click"></asp:LinkButton>
														<asp:HiddenField ID="hdid" runat="server" Value="0" />
													</div>
												<!--end::Col-->
											</div>












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
													<asp:Button ID="btnAddDenimination" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add"  />
													<asp:Button ID="Button3" runat="server" SkinID="btnDefault" OnClick="btnEditDenimination_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Edit"  />
													<asp:LinkButton SkinID="BtnLinkDelete" runat="server" OnClientClick ="Do you want to delete this record?" OnClick="btnDeleteDenomination_Click"></asp:LinkButton>
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


	   <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
	 <asp:Label ID="btnCancel" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White"
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
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> <asp:Label ID="lblpoptitlte" runat="server" Text="Religion"></asp:Label> </label>
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


<%--	<asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
 
<!-- ModalPopupExtender -->
<ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    This is an ASP.Net AJAX ModalPopupExtender Example<br />
    <asp:Button ID="Button1" runat="server" Text="Close" />
</asp:Panel>--%>

</asp:Content>