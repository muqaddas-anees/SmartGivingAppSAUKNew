<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="AddActivity.aspx.cs" Inherits="DeffinityAppDev.App.Activity.AddActivity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	Activities
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
	<asp:Label ID="lbltitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

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

															<%--<asp:TextBox ID="txtDescriptionArea" runat="server" TextMode="MultiLine" Height="200" Columns="100" ></asp:TextBox>--%>

															<CKEditor:CKEditorControl ID="txtDescriptionArea" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="500px" ClientIDMode="Static"></CKEditor:CKEditorControl>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->

												
											

											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Slot(s)</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4">
													<div class="row">
														
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtcurrenyValue" runat="server" Text="0" ToolTip="Please enter the slot" SkinID="Price" ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>

		<div class="row mb-6" id="pnlDates" runat="server" visible="false">
												
												<div class="row">
														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">

															<label class="col-form-label required fw-bold fs-6"> Start Date :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox ID="txtStartDate" Text="" runat="server" SkinID="DateNew"></asp:TextBox>
																	</div>
																
															</div>
														

														</div>
														
														<label class="col-lg-2 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
															<label class=" col-form-label required fw-bold fs-6"> End Date :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox  ID="txtEndDate" Text="" SkinID="DateNew" runat="server"></asp:TextBox>
																	</div>
															
																</div>
														

														</div>
														
													</div>
												
											</div>

		<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Save" OnClick="btnSaveAndEdit_Click"  />  <%-- <div class ="col-lg-1"></div>--%>
											<asp:Button ID="btnPublish" runat="server" SkinID="btnDefault"  Text="Publish"  Visible="false" />  <%--<div class ="col-lg-1"></div>--%>

											<%--<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnDefault" OnClick="btnSaveChanges_Click" Text="Save Changes"  />--%>


										</div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
