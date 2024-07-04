<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="ajtest1.aspx.cs" Inherits="DeffinityAppDev.App.ajtest1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="server">
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

    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
 <a href="#" class="btn btn-color-muted px-0 text-start rounded-0 py-1" id="kt_modal_select_location_target" data-bs-toggle="modal" data-bs-target="#kt_modal_select_location">Location</a>
												
<!-- ModalPopupExtender -->
<div class="modal fade" id="kt_modal_select_location" tabindex="-1" aria-hidden="true">
			<!--begin::Modal dialog-->
			<div class="modal-dialog mw-1000px">
				<!--begin::Modal content-->
				<div class="modal-content">
					<!--begin::Modal header-->
					<div class="modal-header">
						<!--begin::Title-->
						<h2>Select Location</h2>
						<!--end::Title-->
						<!--begin::Close-->
						<div class="btn btn-sm btn-icon btn-active-color-primary" data-bs-dismiss="modal">
							<!--begin::Svg Icon | path: icons/duotune/arrows/arr061.svg-->
							<span class="svg-icon svg-icon-1">
								<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
									<rect opacity="0.5" x="6" y="17.3137" width="16" height="2" rx="1" transform="rotate(-45 6 17.3137)" fill="black" />
									<rect x="7.41422" y="6" width="16" height="2" rx="1" transform="rotate(45 7.41422 6)" fill="black" />
								</svg>
							</span>
							<!--end::Svg Icon-->
						</div>
						<!--end::Close-->
					</div>
					<!--end::Modal header-->
					<!--begin::Modal body-->
					<div class="modal-body">
						<div id="kt_modal_select_location_map" class="w-100 rounded" style="height:450px"></div>
					</div>
					<!--end::Modal body-->
					<!--begin::Modal footer-->
					<div class="modal-footer d-flex justify-content-end">
						<a href="#" class="btn btn-active-light me-5" data-bs-dismiss="modal">Cancel</a>
						<button type="button" id="kt_modal_select_location_button" class="btn btn-primary" data-bs-dismiss="modal">Apply</button>
					</div>
					<!--end::Modal footer-->
				</div>
</asp:Content>
