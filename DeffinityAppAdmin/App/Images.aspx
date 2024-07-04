<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Images.aspx.cs" Inherits="DeffinityAppDev.App.Images" %>

<%@ Register Src="~/App/controls/OrgTabs.ascx" TagPrefix="Pref" TagName="OrgTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="server">
	 <Pref:OrgTabs runat="server" ID="OrgTabs" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Organization
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


      <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details"  >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Gallery for : </h3> <asp:Label runat="server" ID="lblGallery"> </asp:Label>
									</div>
									
									<!--end::Card title-->
								</div>
								<!--begin::Card header-->
								<!--begin::Content-->
								<div id="kt_account_profile_detail" class="collapse show" style="">
									<!--begin::Form-->
									<form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											



											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-2 col-form-label  fw-bold fs-6">Upload Image</label>
												<!--end::Label-->
												<!--begin::Col-->

												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
													
															<asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                                            <asp:Button runat="server" Text="Upload Image" ToolTip="Upload imgages of Organizaton" OnClick="UploadFile"></asp:Button>
															<div class="form-text">Allowed file types: png, jpg, jpeg.</div>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
													<asp:Label runat="server" ID="lblFilenotSelected" ForeColor="Red"> </asp:Label>
												</div>
												 
      
												</div>
												<!--end::Col-->  
											</div>
											<!--end::Input group-->


											<div class="row mb-6" style="display:none;visibility:hidden;">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label fw-bold fs-6"></label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														
														<div class="image-input-wrapper w-200px h-200px" style="background-image: url(assets/media/avatars/150-26.jpg)">
															<asp:Image ID="img" runat="server" Width="200px" Height="200px" />
															<br />
															<div >
                                                                
                                                                </div>
														</div>
														
														
														
													</div>
													
												</div>
												<!--end::Col-->
											</div>

									

										
														<!--begin::Input group-->
											<div class="row mb-10">
												
												<div class="card-title m-0">
													
												       <h3 class="fw-bolder m-0"> &emsp;&emsp;  Gallery for: <asp:Label runat="server"  ID="lblGalleryName"> </asp:Label>
                                                    </h3>
													
										
									</div>
												
											</div>
											<!--end::Input group-->

										<div>
											<label class="col-lg-1 col-form-label  fw-bold fs-6">   </label>
											<asp:ListView ID="ImageList" runat="server"     >  </asp:ListView>
										</div>


											<br />
										</div>
								</div>
								<!--end::Content-->
</asp:Content>
