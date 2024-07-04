<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="OtherDonations.aspx.cs" Inherits="DeffinityAppDev.App.OtherDonations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	<asp:Label ID="lblTitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
   <link href="../assets/plugins/global/plugins.bundle.css?V1" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
	<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/documentation/forms/tagify.js")%>'></script>--%>
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" data-bs-target="#kt_account_profile_details" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Donor Details</h3>
									</div>
									 <div class="card-toolbar gap-3" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
										 
										 <asp:Button ID="btnSaveDonation" runat="server" Text="Save" SkinID="btnDefault" OnClick="btnSaveDonation_Click" ValidationGroup="d" />
										 <asp:Button ID="btnBack" runat="server" CssClass="btn btn-light" Text="Back" OnClick="btnBack_Click" />
										 <asp:HiddenField ID="hunid" runat="server" ClientIDMode="Static" />
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
												<asp:ValidationSummary ID="ValSUm1" runat="server" ValidationGroup="d" />

												</div>
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Donor Image</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-125px h-125px" id="imageDiv" runat="server" style="background-image: url(assets/media/avatars/150-26.jpg)"><asp:Image ID="img" runat="server" Height="100px" Visible="false" /></div>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">Full Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
															<asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server"
																ForeColor="Red" ErrorMessage="Please enter First name" ControlToValidate="txtFirstName" ValidationGroup="d" ></asp:RequiredFieldValidator>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
															<asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator1" Display="Dynamic" runat="server"
																ForeColor="Red" ErrorMessage="Please enter Last name" ControlToValidate="txtSurname" ValidationGroup="d" ></asp:RequiredFieldValidator>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Email Address</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="250"></asp:TextBox>
													<asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator2" Display="Dynamic" runat="server"
																ForeColor="Red" ErrorMessage="Please enter Email Address" ControlToValidate="txtEmailAddress" ValidationGroup="d" ></asp:RequiredFieldValidator>
													<asp:RegularExpressionValidator 
    ID="regexEmailValidator" 
    runat="server" 
    ControlToValidate="txtEmailAddress"
    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
    ErrorMessage="Please enter valid Email Address"
    Display="Dynamic"
    ForeColor="Red" ValidationGroup="d" >
</asp:RegularExpressionValidator>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6" style="display:none;visibility:hidden;">
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
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Phone number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtContactNumber" runat="server" MaxLength="50"></asp:TextBox>
													 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactNumber"
                                ValidChars="0123456789+()- " />
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
													<span class="">Town</span>
													<%--<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Phone number must be active" aria-label="Phone number must be active"></i>--%>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">County</label>
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
											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Country</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span class="required">Permission</span>
													<i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="" data-bs-original-title="Country" aria-label="Country of origination"></i>
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container" >
												<asp:RadioButtonList ID="ddlPermission" runat="server"  >
													<asp:ListItem  Value="1" Text="Admin"></asp:ListItem>
													<asp:ListItem  Value="2" Text="Charity Member" Enabled="true"></asp:ListItem>
													</asp:RadioButtonList>
												<div class="fv-plugins-message-container invalid-feedback"></div></div>
												<!--end::Col-->
											</div>
											
											
											<!--end::Input group-->
											
											
											<!--begin::Input group-->
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save Changes" Visible="false"  />
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>


	
		<!--begin::Deactivate Account-->

	

	<div class="card mb-5 mb-xl-10" id="pnlTags" runat="server">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_deactivate" aria-expanded="true" aria-controls="kt_account_deactivate">
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Details of the Donation</h3>
									</div>
								</div>
								
								<div id="kt_account_tags" class="collapse show">
									
										<div class="card-body border-top p-9">
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"><asp:Label ID="Label1" runat="server" Text="Fundraisers"></asp:Label> </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-5 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlFund" runat="server"></asp:DropDownList>
													  
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Details Of the Donation</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtDetailsofDonation" runat="server" MaxLength="5000" TextMode="MultiLine" SkinID="txtMulti_150"></asp:TextBox>
													<asp:RequiredFieldValidator ID="rfDonation" runat="server" ErrorMessage="Please enter details of the donation" ControlToValidate="txtDetailsofDonation" ValidationGroup="d" Display="None"></asp:RequiredFieldValidator>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
											
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6"><asp:Label ID="lblValueofgoods" runat="server" Text="Value of goods"></asp:Label> </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-3 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtValueOfGoods" runat="server" MaxLength="25" SkinID="Price"></asp:TextBox>
													  <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtValueOfGoods"
                                ValidChars="0123456789." />
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6" id="pnlChecknumber" runat="server" visible="false">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Cheque number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtChecknumber" runat="server" MaxLength="250"></asp:TextBox>
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
			ControlToValidate="txtAddCompany" ValidationGroup="group2" ></asp:RequiredFieldValidator>

	</div>
	
</div>
		<div class="row mb-6">
	<label class="col-lg-3 col-form-label fw-bold fs-6">Contact Name </label>
	<div class="col-lg-9"><asp:TextBox ID="txtContactName" runat="server" SkinID="txt_90"></asp:TextBox></div>
	
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
			<asp:Button ID="btnSubmitCompany" runat="server" SkinID="btnSubmit" OnClick="btnSubmitCompany_Click" ValidationGroup="group2"  />
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
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Notes</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtNotes" runat="server" MaxLength="250" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>
										</div>
										<!--end::Card body-->
										<!--begin::Card footer-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<asp:Button ID="btnUpdateTags" runat="server" CausesValidation="false" Text="Update" Visible="false" />
											
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
								<!--end::Card header-->
								<!--begin::Content-->
								<div id="11kt_account_deactivate" class="collapse show">
									<!--begin::Form-->
								<%--	<form id="kt_account_deactivate_form" class="form">--%>
										<!--begin::Card body-->
										<div class="card-body border-top p-9">

											<div class="row mb-6">
												<!--begin::Label-->
												<%--<label class="col-lg-4 col-form-label required fw-bold fs-6"></label>--%>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-10 fv-row fv-plugins-icon-container">
													 <div class="dropzone dropzone-queue mb-2" id="kt_dropzonejs_example_3">
                <!--begin::Controls-->
                <div class="dropzone-panel mb-lg-0 mb-2">
                    <a class="dropzone-select btn btn-sm btn-bg-light me-2">Drop files here or click to upload</a>
                    <a class="dropzone-remove-all btn btn-sm btn-light-primary">Remove All</a>
                </div>
                <!--end::Controls-->

                <!--begin::Items-->
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
                        <!--end::File-->

                        <!--begin::Progress-->
                        <div class="dropzone-progress">
                            <div class="progress">
                                <div
                                    class="progress-bar bg-primary"
                                    role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress>
                                </div>
                            </div>
                        </div>
                        <!--end::Progress-->

                        <!--begin::Toolbar-->
                        <div class="dropzone-toolbar">
                            <span class="dropzone-delete" data-dz-remove><i class="bi bi-x fs-1"></i></span>
                        </div>
                        <!--end::Toolbar-->
                    </div>
                </div>
                <!--end::Items-->
           <%-- </div>
            <!--end::Dropzone-->

            <!--begin::Hint-->
            <span class="form-text text-muted">Max file size is 1MB and max number of files is 5.</span>
            <!--end::Hint-->
        </div>       --%>
                                                         
                                                         </div>
											

												</div>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
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
											<asp:Button ID="Button2" runat="server" CausesValidation="false" Text="Update" Visible="false" />
											<%--<button id="kt_account_deactivate_account_submit" type="submit" class="btn btn-danger fw-bold" runat="server" onclick="">Deactivate Account</button>--%>
										</div>
										<!--end::Card footer-->
									<%--</form>--%>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							

							
							<!--end::Deactivate Account-->
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%--<script src="../assets/js/custom/documentation/forms/tagify.js"></script>--%>
	

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
            url: "UploadHandler.ashx?donate_unid=" +$("#hunid").val(), // Set the url for your upload script location
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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
