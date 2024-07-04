<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="DeffinityAppDev.App.Organization" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/App/controls/OrgTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>


<asp:Content ID="ContentTabs" runat="server" ContentPlaceHolderID="Tabs">

    <Pref:OrgTabs runat="server" ID="OrgTabs" />
</asp:Content>

<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Organization Details</h3>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">Organization Logo</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														
														<div class="image-input-wrapper w-125px h-125px" style="background-image: url(assets/media/avatars/150-26.jpg)"><asp:Image ID="img" runat="server" Height="100px" /></div>
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
											
											<!--end::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">First Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtContactname" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Last Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtLastName" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> Email</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Phone Number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtPhone" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Organization Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtOrganizationName" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Org Registration Number</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtOrgRegistrationNumber" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Activity</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtDescription" runat="server"  TextMode="MultiLine" SkinID="80`"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Web URL</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtWebURL" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Average Donation Per Month</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtCostCenter" runat="server" MaxLength="500"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>

											<!--end::Input group-->
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
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span>Town</span>
													
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">State</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtState" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Zip</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtZip" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Strip Account ID</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row">
													<asp:TextBox ID="txtAccount" runat="server" MaxLength="100"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">
													<span >Country</span>
													
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlCountry" runat="server"><asp:ListItem Text="Please select..."></asp:ListItem></asp:DropDownList>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Card Transaction Fee (%)</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtCardTransactionFee" SkinID="Price_150px" runat="server" MaxLength="6" Text="0.00"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6" >
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Fixed Price</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtFixedPrice" SkinID="Price_150px" runat="server" MaxLength="6" Text="0.00"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
												<div class="row mb-6"  >
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Platform Support Cost (%)</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<asp:TextBox ID="txtTransactionFee" SkinID="Price_150px" runat="server" MaxLength="6" Text="0.00"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6"  >
								<label class="col-lg-4 col-form-label required fw-bold fs-6">Upload Registration Certificate</label>
													<div class="col-lg-8">
								 <asp:FileUpload runat="server" id="imgFile" CssClass="form-control" Text="Upload" />

														<br />

														 <div class="form-group row mb-6 mt-6">
          <div class="col-md-12">
              <label class="col-sm-2 control-label"></label>
               <div class="col-sm-6">
                   <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false">
                       <Columns>
                      <%-- <asp:BoundField DataField="Text" HeaderText="File Name" />--%>

                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID = "lnkDownload" OnClick="DownloadFile" CausesValidation="false" 
                                 Text = '<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' runat = "server"></asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                      <asp:TemplateField>
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');"  Visible="false"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
               </div>
          </div>
    </div>
														</div>
								<!--end::Input-->
							</div>
											<!--end::Input group-->
											
											
											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged"  AutoPostBack="true" ></asp:DropDownList>
												</div>


												<div class="col-lg-4">
													</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->










											<div class="row mb-6"  style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Group</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlGroup" runat="server"   OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													</div>
												<!--end::Col-->
											</div>












											<!--begin::Input group-->
											<div class="row mb-6"  style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Denomination</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlDenimination" runat="server" ></asp:DropDownList>
											

												</div>
												<div class="col-lg-4">
													</div>
												<!--end::Col-->
											</div>

											
											<!--begin::Input group-->
										
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
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>


	<script type="text/javascript">
        //const button = document.getElementById('kt_docs_sweetalert_basic1');

        //button.addEventListener('click', e => {
        //    e.preventDefault();

        //    Swal.fire({
        //        text: "Here's a basic example of SweetAlert!",
        //        icon: "success",
        //        buttonsStyling: false,
        //        confirmButtonText: "Ok, got it!",
        //        customClass: {
        //            confirmButton: "btn btn-primary"
        //        }
        //    });
        //});

      

		
    </script>
</asp:Content>