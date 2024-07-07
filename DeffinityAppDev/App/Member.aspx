<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="DeffinityAppDev.App.Member" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	<asp:Label ID="lblPageTitle" runat="server" Text="Member Details"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../assets/plugins/global/plugins.bundle.css?v=33" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/tagify/4.12.0/tagify.min.js" integrity="sha512-uDMk0LmYVhMq6mKY7QfiJAXBchLmLiCZjh5hmZ6UUEJ/iNDk2s8maQDx4lOPCqLJqvhktN/g7oZTesQ6SOIjhw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
	<style>

		.tagify {
    --tags-disabled-bg: #F1F1F1;
    --tags-border-color: #DDD;
    --tags-hover-border-color: #CCC;
    --tags-focus-border-color: #3595f6;
    --tag-bg: #E5E5E5;
    --tag-hover: #D3E2E2;
    --tag-text-color: black;
    --tag-text-color--edit: black;
    --tag-pad: 0.3em 0.5em;
    --tag-inset-shadow-size: 1.1em;
    --tag-invalid-color: #D39494;
    --tag-invalid-bg: rgba(211, 148, 148, 0.5);
    --tag-remove-bg: rgba(211, 148, 148, 0.3);
    --tag-remove-btn-color: black;
    --tag-remove-btn-bg: none;
    --tag-remove-btn-bg--hover: #c77777;
    --input-color: inherit;
    --tag--min-width: 1ch;
    --tag--max-width: auto;
    --tag-hide-transition: 0.3s;
    --placeholder-color: rgba(0, 0, 0, 0.4);
    --placeholder-color-focus: rgba(0, 0, 0, 0.25);
    --loader-size: .8em;
    --readonly-striped: 1;
     display: inherit; 
    align-items: flex-start;
    flex-wrap: wrap;
    border: 1px solid #ddd;
    border: 1px solid var(--tags-border-color);
    padding: 0;
    line-height: 0;
    cursor: text;
    outline: 0;
    position: relative;
    box-sizing: border-box;
    transition: .1s;
}
		.tagify__input {
    flex-grow: 1;
     display: inline; 
    min-width: 110px;
    margin: 5px;
    padding: 0.3em 0.5em;
    padding: var(--tag-pad,.3em .5em);
    line-height: normal;
    position: relative;
    white-space: pre-wrap;
    color: inherit;
    color: var(--input-color,inherit);
    box-sizing: inherit;
}

		.grid_header_right{
			text-align:right;
		}

		.col-lg-8> tags{
			  padding:20px;
			  height:70px;
		}
		/*.tagify__input {
    flex-grow: 1;*/
  /*  display: inline-block;*/
    /*min-width: 110px;
    margin: 5px;
    padding: 0.3em 0.5em;
    padding: var(--tag-pad,.3em .5em);
    line-height: inherit;
    position: relative;
    white-space: pre-wrap;
    color: inherit;
    color: var(--input-color,inherit);
    box-sizing: inherit;
  
}*/
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
  height:35px;
}

.tags-look .tagify__dropdown__item--active{
  color: black;
}

.tags-look .tagify__dropdown__item:hover{
  background: lightyellow;
  border-color: gold;
}
/*.tagify__input {
    flex-grow: 1;*/
    /*display: inline-block;*/
    /*min-width: 110px;
    margin: 5px;
    padding: 0.3em 0.5em;
    padding: var(--tag-pad,.3em .5em);
    line-height: inherit;
    position: relative;
    white-space: pre-wrap;
    color: inherit;
    color: var(--input-color,inherit);
    box-sizing: inherit;
}*/

	</style>
	 <style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px}
       </style>
	<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/documentation/forms/tagify.js")%>'></script>--%>
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"><asp:Label ID="lblsubtitle" runat="server"></asp:Label> </h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" >

										 <asp:HyperLink ID="btnBack" runat="server" NavigateUrl="~/App/Members.aspx" CssClass="btn btn-light" Text="Back to Members"></asp:HyperLink>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6"><asp:Label ID="lblSection" runat="server" Text="Member"></asp:Label> Image</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div id="imageDiv" runat="server" class="image-input-wrapper w-125px h-125px" style="background-image: url(assets/media/avatars/150-26.jpg)"><asp:Image ID="img" runat="server" Height="100px" Visible="false" /></div>
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
												<label class="col-lg-4 col-form-label required fw-bold fs-6">First Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-12 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
															 <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter First name" ControlToValidate="txtFirstName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														<!--begin::Col-->
														
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Last name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
													 <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Last name" ControlToValidate="txtSurname" ValidationGroup="group1" ></asp:RequiredFieldValidator>
														
												<div class="fv-plugins-message-container invalid-feedback"></div>

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
													  <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Email" ControlToValidate="txtEmailAddress" ValidationGroup="group1" ></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator 
    ID="regexEmailValidator" 
    runat="server" 
    ControlToValidate="txtEmailAddress"
    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
    ErrorMessage="Please enter valid Email "
    Display="Dynamic"
    ForeColor="Red" ValidationGroup="group1" >
</asp:RegularExpressionValidator>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6"  id="pnlPasswordTextbox" runat="server">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Password</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtPassword" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6" id="pnlStatus" runat="server" visible="false">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Status</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlStatus" runat="server">
														<asp:ListItem Text="Active" Value="Active"></asp:ListItem>
														<asp:ListItem Text="Inactive" Value="InActive"></asp:ListItem>
													</asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Phone Number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtContactNumber" runat="server" MaxLength="250" Text="+44"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Company</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container d=flex d-inline">
													<div class="row">
														<div class="col-lg-6">
															<asp:DropDownList ID="ddlCompany" runat="server" ></asp:DropDownList> 
														</div>
														<div class="col-lg-6">
															<asp:Button ID="btnAddCompnay" runat="server" CssClass="btn btn-light" Text="Add" OnClick="btnAddCompnay_Click" ToolTip="Add Company" />
															<asp:TextBox ID="txtcompany" runat="server" MaxLength="250" Visible="false"></asp:TextBox>
														</div>
													</div>
													
													

													

<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
    TargetControlID="btnPop_open" PopupControlID="Panel_portfolio" 
    CancelControlID="lbtnCloseOptions" 
    BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
<asp:Label ID="btnPop_open" runat="server"></asp:Label>
<asp:Panel ID="Panel_portfolio" ClientIDMode="Static" runat="server" Width="50%" CssClass="card shadow-sm">
   <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Add Company"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">


  <%-- <asp:UpdatePanel ID="UpdatePanle_PortfolioDDL" runat="server">
<ContentTemplate>--%>
    <div class="modal-body">
<div class="row mb-6">
	<label class="col-lg-3 col-form-label required fw-bold fs-6">Company </label>
	<div class="col-lg-9"><asp:TextBox ID="txtAddCompany" runat="server" SkinID="txt_90"></asp:TextBox>                      
		<asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Company" 
			ControlToValidate="txtAddCompany" ValidationGroup="group2" ></asp:RequiredFieldValidator></div>
	
</div>
		
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Email </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyEmail" runat="server" SkinID="txt_90"></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Phone number </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyPhone" runat="server" SkinID="txt_90" ></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Address </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyAddress" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Notes </label>
	<div class="col-lg-9"><asp:TextBox ID="txtCompanyNotes" runat="server" SkinID="txtMulti_80" TextMode="MultiLine"></asp:TextBox></div>
	
</div>
		<div class="row mb-6">
		<label class="col-lg-3 col-form-label fw-bold fs-6"> </label>
	<div class="col-lg-9">
			<asp:Button ID="btnSubmitCompany" runat="server" SkinID="btnSubmit" OnClick="btnSubmitCompany_Click" ValidationGroup="group2" />
		</div>
			</div>
        </div>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
       </div>
</asp:Panel>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Address</label>
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
													Town
													
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">City</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtState" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Postcode</label>
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
													
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
											<div class="row mb-6" id="pnlUserType" runat="server" visible="false">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Permission</span>
													
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
												<asp:RadioButtonList ID="ddlPermission" runat="server" CssClass="mycheckBig"  >
													<asp:ListItem Value="1" Text="Admin" Enabled="true"></asp:ListItem>
													<asp:ListItem  Value="2" Text="Donor" ></asp:ListItem>
													<asp:ListItem  Value="3" Text="Participant" ></asp:ListItem>
													</asp:RadioButtonList>


													<asp:CheckBoxList ID="CheckBoxList1" runat="server" CssClass="mycheckBig">
    <asp:ListItem Value="Donor" Text="Donor"></asp:ListItem>
    <asp:ListItem Value="Volunteer" Text="Volunteer"></asp:ListItem>
    <asp:ListItem Value="Lead" Text="Lead"></asp:ListItem>
    <asp:ListItem Value="Sponsor" Text="Sponsor"></asp:ListItem>
</asp:CheckBoxList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
												<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6"  style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Denomination</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlDenimination" runat="server" ></asp:DropDownList>
											

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

											<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save Changes" ValidationGroup="group1"  />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>


	
		<!--begin::Deactivate Account-->

	<div class="card mb-5 mb-xl-10" id="pnlCustomfields" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Additional information <asp:Label ID="lblCustomFiledCustomer" runat="server" Visible="false"></asp:Label></h3>
									</div>
								</div>
								
								<div id="kt_account_tags3" class="collapse show">
									
										<div class="card-body border-top p-9">

		

                <%-- <asp:ValidationSummary ID="VS2" runat="server" ValidationGroup="Custom" DisplayMode="BulletList" />--%>
                <div>
                    <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                       <%-- <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>

                <div class="form-group row">
                    <div class="col-md-6">
                    </div>
                    <div class="col-md-6 pull-right">
                        <div id="div1">
                          
                        </div>
                    </div>

                </div>
	</div>
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											  <asp:Button ID="imgCustomFieldUpdate" runat="server" SkinID="btnUpdate" ValidationGroup="group1"
                              OnClick="imgCustomFieldUpdate_Click"  />
											</div>
									</div>
		</div>
	


	<div class="card mb-5 mb-xl-10" id="pnlskills" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Interests</h3>
									</div>
								</div>
								
								<div id="kt_account_tags" class="collapse show">
									
										<div class="card-body border-top p-9">
											<div class="row">
												<div class="col-lg-8">
												<%--<input class="form-control" value="tag1, tag2, tag3" id="kt_tagify_1"/>--%>
												<input id="txtSkills" runat="server" name="input-custom-dropdown" class="form-control" placeholder="write some tags" value="" style="width:50%;height:75px" />
												<%--<​input name="input-custom-dropdown" class="form-control" placeholder="write some tags" value="css, html, javascript"/>--%>
													</div>
											</div>
											
											
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="Button1" runat="server" CausesValidation="false" OnClick="btnUpdateSkill_Click" Text="Update" />
											
										</div>
										
								</div>
								<!--end::Content-->
							</div>

	<div class="card mb-5 mb-xl-10" id="pnlTags" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Tags</h3>
									</div>
								</div>
								
								<div id="kt_account_tags_1" class="collapse show">
									
										<div class="card-body border-top p-9">
											<div class="row">
												<div class="col-lg-8">
												<%--<input class="form-control" value="tag1, tag2, tag3" id="kt_tagify_1"/>--%>
												<input id="txtTags" runat="server" name="input-custom-dropdown" class="form-control" placeholder="write some tags" value="" style="width:50%;height:55px" />
												<%--<​input name="input-custom-dropdown" class="form-control" placeholder="write some tags" value="css, html, javascript"/>--%>
													</div>
											</div>
											
											
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="btnUpdateTags" runat="server" CausesValidation="false" OnClick="btnUpdateTags_Click" Text="Update" />
											
										</div>
										
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
								
								<div id="11kt_account_deactivate" class="collapse show">
								
										<div class="card-body border-top p-9">

											<div class="row mb-6">
												
												<div class="col-lg-10 fv-row fv-plugins-icon-container">
													 <div class="dropzone dropzone-queue mb-2" id="kt_dropzonejs_example_3">
                <!--begin::Controls-->
                <div class="dropzone-panel mb-lg-0 mb-2">
                    <a class="dropzone-select btn btn-sm btn-bg-light me-2">Drop files here or click to upload</a>
                    <a class="dropzone-remove-all btn btn-sm btn-light-primary">Remove All</a>
                </div>
               
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
                      
                        <div class="dropzone-progress">
                            <div class="progress">
                                <div
                                    class="progress-bar bg-primary"
                                    role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress>
                                </div>
                            </div>
                        </div>
                      
                        <div class="dropzone-toolbar">
                            <span class="dropzone-delete" data-dz-remove><i class="bi bi-x fs-1"></i></span>
                        </div>
                       
                    </div>
                </div>
               
                                                         
                                                         </div>
											

												</div>
												<div class="col-lg-4">
													
													</div>
												
											</div>


											<div class="row mb-6">
												  <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" >
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false"  />
                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID="btnDownload" runat="server" OnClick="DownloadFile" CommandArgument='<%# Eval("Value") %>' Text='<%# Eval("Text") %>'></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="30px">
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("ID") %>'
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

	<div class="card mb-5 mb-xl-10" id="pnlDonations" runat="server" >
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Donation History</h3>
									</div>
								</div>
								
								<div id="kt_account_tags1" class="collapse show">
									
										<div class="card-body border-top p-9">
											<div class="row">
												<div class="col-lg-9">
												<asp:GridView ID="GridDashboard" runat="server" Width="100%" OnRowCommand="GridDashboard_RowCommand" >
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidDate" runat="server" Text='<%# Bind("PaidDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<%-- <asp:TemplateField  HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField  HeaderText="Donation Type">
                    <ItemTemplate>
                        <asp:Label ID="lblTithigName" runat="server" Text='<%# Bind("CategoryList") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              <%--   <asp:TemplateField HeaderText="Logged By">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("PaidBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="grid_header_right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Pay Ref">
                    <ItemTemplate>
                        <asp:Label ID="lblPayRef" runat="server" Text='<%# Bind("PayRef") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="Payment Type">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

				 <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lbStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="view"  />
                    </ItemTemplate>
                </asp:TemplateField>
				<%-- 
				 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnSendRecipt" runat="server" Text="Send Receipt" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="SendReceipt"  />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
													</div>
												<div class="col-lg-3">
													<div class="card card-xxl-stretch" id="pnl_TranstactionDetails" runat="server" visible="false" >
											<!--begin::Header-->
											<div class="card-header">
												<h3 class="card-title "> Transaction details </h3>
                                                <asp:HiddenField ID="hid" runat="server" />
                                                 <asp:HiddenField ID="hunid" runat="server" />
												</div>
                                            
                                                <div class="card-body p-5" >

													<div class="row mb-6">
														<asp:Label ID="lblamount" runat="server" Font-Bold="true" Font-Size="25px" Text="0.00"></asp:Label>

													</div>

													<div class="row mb-6">
														<asp:Label ID="lblStatus" runat="server" ></asp:Label>

													</div>
													<br />
														<div class="row mb-6">
														<asp:Label ID="lblCategories" runat="server" ></asp:Label>

													</div>
													<br />
													<div class="row mb-6">
														<asp:Label ID="lblPaymentDetails" runat="server" ></asp:Label>

													</div>
													
													
													<div class="row mb-6">
														<div class="col">
															<asp:Label ID="lblName1" runat="server" Text="Name"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="txtname" runat="server" Text="Name"></asp:Label>
														</div>

													</div>
													<hr />
													<div class="row mb-6">
														<div class="col">
															<asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="txtemail" runat="server" Text="EMail"></asp:Label>
														</div>

													</div>
													<hr />
													<div class="row mb-6">
														<div class="col">
															<asp:Label ID="Label3" runat="server" Text="Type"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="txttype" runat="server" Text="Name"></asp:Label>
														</div>

													</div>
													<hr />
													<div class="row mb-6">
														<div class="col">
															<asp:Label ID="Label5" runat="server" Text="Method"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="txtMethod" runat="server" Text="Card"></asp:Label>
														</div>

													</div>
                                                   <hr />
                                                    <div class="row mb-6">
														<div class="col">
															<asp:Label ID="Label7" runat="server" Text="Transaction Fee Covered"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="lbltr" runat="server" Text=""></asp:Label>
														</div>

													</div>
                                                    	<hr />
                                                    <div class="row mb-6">
														<div class="col">
															<asp:Label ID="Label9" runat="server" Text="Platform Fee Covered"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="lblpf" runat="server" Text=""></asp:Label>
														</div>

													</div>

                                                 

													</div>
												
				 
				 </div>
													</div>
											</div>
											
											
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<asp:Button ID="Button3" runat="server" CausesValidation="false" OnClick="btnUpdateTags_Click" Text="Update" />--%>
											
										</div>
										
								</div>
								<!--end::Content-->
							</div>
								<!--end::Content-->
							
	
	<div class="card mb-5 mb-xl-10" id="pnlCommunication" runat="server" >
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Communication</h3>
									</div>
								</div>
								
								<div id="kt_account_tags2" class="collapse show">
									
										<div class="card-body border-top p-9">
											<div class="row">
												<div class="col-lg-12">
												<asp:GridView ID="GridCommunication" runat="server" Width="100%" OnRowCommand="GridCommunication_RowCommand" >
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Date & Time">
                    <ItemTemplate>
                        <asp:Label ID="lblDateTime" runat="server" Text='<%# Bind("SentDateTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField  HeaderText="">
                    <ItemTemplate>
                       
						<asp:Button ID="btnView" runat="server" CommandName="ViewCommunication" Text="View Communication" CommandArgument='<%# Bind("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
													</div>
											</div>
											
											
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<asp:Button ID="Button3" runat="server" CausesValidation="false" OnClick="btnUpdateTags_Click" Text="Update" />--%>
											
										</div>
										
								</div>
								<!--end::Content-->
							</div>
	<div class="card mb-5 mb-xl-10" id="pnlPassword" runat="server" visible="false">
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
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%--<script src="../assets/js/custom/documentation/forms/tagify.js"></script>--%>
	<script>
  //      var input1 = document.querySelector("#kt_tagify_1");
		//new Tagify(input1);

        var input = document.querySelector('#MainContent_MainContent_txtSkills'),
            // init Tagify script on the above inputs
            tagify = new Tagify(input, {
                //whitelist: ["Cloud computing", "Artificial intelligence", "Sales leadership", "Analysis", "Translation", "Mobile app development", "People management", "Video production", "Audio production", "UX design", "SEO / SEM marketing","Blockchain","Industrial design","Creativity","Collaboration","Adaptability","Time management","Persuasion","Digital journalism","Animation"],
                maxTags: 50,
                dropdown: {
                    maxItems: 50,           // <- mixumum allowed rendered suggestions
                    classname: "tags-look", // <- custom classname for this dropdown, so it could be targeted
                    enabled: 0,             // <- show suggestions on focus
                    closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
                }
			})

		//txtTags

        var input = document.querySelector('#MainContent_MainContent_txtTags'),
            // init Tagify script on the above inputs
            tagify = new Tagify(input, {
				//whitelist: ["Cloud computing", "Artificial intelligence", "Sales leadership", "Analysis", "Translation"],
                editTags: true,
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


	<%--<div class="row">
		<input class="form-control" name='basic' value='tag1, tag2'>

	</div>
	<script type="text/javascript">
        // The DOM element you wish to replace with Tagify
        var input1 = document.querySelector('input[name=basic]');

        // initialize Tagify on the above input node reference
         new Tagify(input1, {
           // whitelist: ["Cloud computing", "Artificial intelligence", "Sales leadership", "Analysis", "Translation"],
			editTags: true,
            enforceWhitelist: false,
            maxTags: 50,
            //dropdown: {
            //    maxItems: 50,           // <- mixumum allowed rendered suggestions
            //    classname: "tags-look", // <- custom classname for this dropdown, so it could be targeted
            //    enabled: 0,             // <- show suggestions on focus
            //    closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
            //}
        })
    </script>--%>
	<ajaxToolkit:ModalPopupExtender ID="mdlShowMail" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblSendMail" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lblSendMail" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="750px" CssClass="card card-custom" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
                 <div class="card-title">
												
													<h3 class="card-label"><asp:Label ID="Label2" runat="server" Text="Mail Details"></asp:Label> </h3>
												</div>
							
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
          <div class="row mb-6">
        <div class="col-lg-6 d-flex d-inline">
            <asp:Label ID="Label4" runat="server" Text="Subject" style="margin-top:10px;padding-right:10px" ></asp:Label>
       
           <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
           
            </div>
       
        </div>
 
        <div class="form-group row mb-6" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row mb-6">
               <div class="col-md-12 form-inline">
                  
									 <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="350px" ClientIDMode="Static" BasicEntities="true" FullPage="true"  ></CKEditor:CKEditorControl>
                   </div>
								</div>
    </div>
       
           <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       
                          <asp:HiddenField ID="hcid" runat="server" />  <asp:HiddenField ID="htomail" runat="server" /> 
                                        <asp:Button ID="btnSend" runat="server" SkinID="btnDefault" Text="Send" OnClick="btnSend_Click" />
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save"  Visible="false" />
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
