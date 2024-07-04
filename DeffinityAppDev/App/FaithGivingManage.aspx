<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="FaithGivingManage.aspx.cs" Inherits="DeffinityAppDev.App.FaithGivingManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Faith Giving</h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">
										 testet

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
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> Detailed Description</label>
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
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"></label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													
													<asp:CheckBoxList ID="ckbCurrencyList"  CssClass="BigCheckBox" runat="server" CellPadding="10" CellSpacing="10" 
														BorderColor="#F5F8FA" BorderStyle="None"  
														RepeatLayout="Table" RepeatDirection="Vertical" RepeatColumns="7" Font-Size="Medium">
														<asp:ListItem  Value="10"  > 10.00  </asp:ListItem>
														<asp:ListItem  Value="300" >300.00 </asp:ListItem>
														<asp:ListItem  Value="20" >20.00 </asp:ListItem>
														<asp:ListItem  Value="500" >500.00 </asp:ListItem>
														<asp:ListItem  Value="50" >50.00 </asp:ListItem>
														<asp:ListItem  Value="1000" >1000.00 </asp:ListItem>
														<asp:ListItem  Value="100" >100.00 </asp:ListItem>
														<asp:ListItem  Value="1500" >1500.00 </asp:ListItem>
														<asp:ListItem  Value="150" >150.00 </asp:ListItem>
														<asp:ListItem  Value="2000" >2000.00 </asp:ListItem>
														<asp:ListItem  Value="200" >200.00 </asp:ListItem>
														<asp:ListItem  Value="250" >250.00 </asp:ListItem>
													</asp:CheckBoxList>
													<br />
													<br />
												</div>
												<!--end::Col-->
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Target</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-4 fv-row fv-plugins-icon-container">
															<asp:DropDownList  ID="ddlCurrency"    runat="server">
                                                        <asp:ListItem Value="-1">Slect Currency Type</asp:ListItem>
                                                        <asp:ListItem  Value="US Doller">$</asp:ListItem>
                                                        <asp:ListItem  value="UK Pound" >£</asp:ListItem>
                                                    </asp:DropDownList>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtcurrenyValue" runat="server" Text="0.00" ToolTip="Enter the Amount" SkinID="Price" ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>

											<br />

											
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Upload Banner</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<asp:FileUpload runat="server" id="imgLogo" />
															
													<!--end::Image input-->
													<!--begin::Hint-->
													<div class="form-text">Allowed file types: png, jpg, jpeg.</div>

													<asp:Image ID="img" runat="server" CssClass="img-responsive" style="max-height:250px" />
													<!--end::Hint-->
												</div>
												<!--end::Col-->
											</div>

											<br />
										
											<div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="row">
														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">

															<label class="col-form-label required fw-bold fs-6">Campaign Start Date :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox ID="txtStartDate" Text="" runat="server" SkinID="DateNew"></asp:TextBox>
																	</div>
																
															</div>
														<%--<div class="fv-plugins-message-container invalid-feedback"></div>--%>

														</div>
														<!--end::Col-->
														
														<!--begin::Col-->
														<label class="col-lg-2 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
															<label class=" col-form-label required fw-bold fs-6">Campaign End Date :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox  ID="TextEndDate" Text="" SkinID="DateNew" runat="server"></asp:TextBox>
																	</div>
															
																</div>
														<%--<div class="fv-plugins-message-container invalid-feedback"></div>--%>

														</div>
														<!--end::Col-->
													</div>
												<!--end::Col-->
											</div>
											
											<!--begin::Input group-->

											<br />
											
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Campaign Owner</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:DropDownList ID="ddlOwner" runat="server"></asp:DropDownList>
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>


											<div class="row mb-6" >
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"></label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:CheckBox ID="chkSendEmail" runat="server" Text="Send Email to the donor after donation" CssClass="form-check-input" />
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

											<asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Save" OnClick="btnSaveAndEdit_Click"  />  <%-- <div class ="col-lg-1"></div>--%>
											<asp:Button ID="btnPublish" runat="server" SkinID="btnDefault"  Text="Publish" OnClick="btnPublish_Click"  Visible="false" />  <%--<div class ="col-lg-1"></div>--%>

											<%--<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save Changes"  />--%>


										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

	<style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#7239EA; padding:40px; padding-bottom:4px;  }
   .BigCheckBox label { font-size:17px;  }

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
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
