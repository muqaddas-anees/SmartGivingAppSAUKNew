<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="DeffinityAppDev.App.Activity" %>

<%@ Register Src="~/App/controls/OrgTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Tabs" runat="server">
	 <Pref:OrgTabs runat="server" ID="OrgTabs" />
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
										<h3 class="fw-bolder m-0">Activity </h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">


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
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> Activity Category </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															 <asp:DropDownList AutoPostBack="true" ID="ddlActiviteCategory" OnSelectedIndexChanged="ddlActiviteCategory_SelectedIndexChanged"  runat="server"></asp:DropDownList>
															
														<div class="fv-plugins-message-container invalid-feedback"></div>


														</div>
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />
															<asp:LinkButton ID="btnDelCategory" runat="server"  OnClick="btnCategoryDelete_click" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete record?');"  />
															</div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->


											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Activity Sub Category </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															 <asp:DropDownList ID="ddlSubCategory" runat="server"> </asp:DropDownList>
															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:Button ID="btnAddDenimination" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add"  />
														<asp:LinkButton ID="btnSubCategoryDelete" runat="server"  OnClick="btnSubCategoryDelete_click" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete record?');"  />	
														</div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->

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
															<asp:TextBox ID="txtTitle" runat="server"  ></asp:TextBox>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">Description</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
													
															<%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

															<asp:TextBox ID="txtDescriptionArea" runat="server" TextMode="MultiLine" Height="200" Columns="100" ></asp:TextBox>

															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											
													


											
										
											<div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="row">
														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">

															<label class="col-form-label required fw-bold fs-6">Activity Start Date & Time :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox ID="txtStartDate" Text="" runat="server" SkinID="DateNew"></asp:TextBox>
																	</div>
																<div class="col-sm-6">
															<asp:TextBox ID="txtStartTime" runat="server" SkinID="TimeNew"></asp:TextBox>
																	</div>
															</div>
														<%--<div class="fv-plugins-message-container invalid-feedback"></div>--%>

														</div>
														<!--end::Col-->
														
														<!--begin::Col-->
														<label class="col-lg-2 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
															<label class=" col-form-label required fw-bold fs-6">Activity End Date :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox  ID="TextEndDate" Text="" SkinID="DateNew" runat="server"></asp:TextBox>
																	</div>
																<div class="col-sm-4">
															<asp:TextBox ID="txtEndTime" runat="server" SkinID="TimeNew"></asp:TextBox>
																	</div>
																</div>
														<%--<div class="fv-plugins-message-container invalid-feedback"></div>--%>

														</div>
														<!--end::Col-->
													</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Slots </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtSlot" runat="server" SkinID="Price_175px" Text="0"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Price </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtPrice" runat="server" SkinID="Price_175px" Text="0.00"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>
												</div>
												<!--end::Col-->
											</div>
												<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"> Notes</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
															<%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>
															<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="200" Columns="100" ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6" style="display:none;visibility:hidden">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Is Active </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:CheckBox runat="server" ID="ckbIsActive" CssClass="BigCheckBox" Checked="true"></asp:CheckBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>
												</div>
												<!--end::Col-->
											</div>
										</div>

										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>
											<asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Save Changes" OnClick="btnSaveAndEdit_Click"  />   <%--<div class ="col-lg-1"></div>--%>
										</div>
										<!--end::Actions-->
									<input type="hidden"> 


									</form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

	<style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#F5F8FA; padding:40px; padding-bottom:4px;  }

  .checkbox-round {
    width: 1.3em;
    height: 1.3em;
    background-color: white;
    border-radius: 50%;
    vertical-align: middle;
    border: 1px solid #ddd;
   
    -webkit-appearance: none;
    outline: none;
    cursor: pointer;
}

  .checkbox-round:checked {
    background-color: gray;
}

</style>


	
	  <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display:none;"
                       Width="450px" Height="190px"   ScrollBars="None">

		   <div class="card card-bordered">
    <div class="card-header">
        <h3 class="card-title"><asp:Label ID="lblModelHeading" runat="server" Text="Add Category"></asp:Label> </h3>
        <div class="card-toolbar">
           <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
        </div>
    </div>
    <div class="card-body">
         <div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> <asp:Literal ID="lblsubtitle" runat="server" Text="Category"></asp:Literal>  </label>
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
       	<asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" style="margin-right:10px" />
				<asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />
    </div>
</div>
     
           </asp:Panel>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
	
	<%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css"/>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
<script type="text/javascript">

   


    $(".input_date_new").flatpickr({
        //enableTime: true,
		dateFormat: "m/d/Y"
		
    });
</script>--%>
</asp:Content>