<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EmailSettings.aspx.cs" Inherits="DeffinityAppDev.App.EmailSetting" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		  #cke_txtEmailSinature .cke_top { display: none !important } 
         #cke_txtEmailSinature .cke_bottom {
    border-radius: 0 0 8px 8px;
    display:none;
}
	</style>
	
     <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Email Template Settings</h3>
									</div>
									 <div class="card-toolbar">
										 <asp:Button ID="btnSave" runat="server" OnClick="btnSaveChange_Click" SkinID="btnSave" />

									 </div>

										
									<!--end::Card title-->
								</div>
								<!--begin::Card header-->
								<!--begin::Content-->
								<div id="kt_account_profile_details" class="collapse show" style="">
									<!--begin::Form-->
									<div id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
										<!--begin::Card body-->
										<div class="card-body border-top p-9">
											<div class="row mb-6">
												<!--begin::Label-->
																							<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Subject </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtEmailSubject" runat="server"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
										
												<div class="row mb-6">
												<!--begin::Label-->
																					<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Email Banner </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													 <asp:FileUpload runat="server" id="imgFile" CssClass="form-control" Text="Upload" />
													<div class="row p-5">
													<asp:Image ID="imgbanner" runat="server" CssClass="img-fluid" /></div>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
																							<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Body of Text </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8 fv-row fv-plugins-icon-container">
													  <div class="row mb-6">
														  <div class="col-lg-12 d-flex justify-content-between">

															  <script>
                                                                  $(document).ready(function () {
                                                                      $("#ddlType").change(function () {
                                                                          
                                                                          var index = this.selectedIndex;
                                                                          var tag = $("#ddlType").val();
                                                                          //alert( $("#ddlType").val());
                                                                          if (tag.length > 0) {
                                                                              CKEDITOR.instances['txtEmailContent'].insertHtml("{{" + tag + "}} ");
                                                                          }

                                                                          return false;
                                                                      });

                                                                  });

                                                              </script>

                                                         <asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static" SkinID="ddl_250px">
              
            </asp:DropDownList>

														  <asp:Button ID="btnDefaultTemplate" runat="server" CssClass="btn btn-light" OnClick="btnDefaultTemplate_Click" Text="Load Default Template" /> </div>
                                                    </div>
													 <CKEditor:CKEditorControl ID="txtEmailContent"  BasePath="~/Scripts/ckeditor_4.20.1/" runat="server" Height="700px"  ClientIDMode="Static" ToolbarFull="true" BasicEntities="true" FullPage="true"></CKEditor:CKEditorControl>
												</div>
												<!--end::Col-->
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
																							<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Button 1 Text </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtButton1Text" runat="server"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
																						<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Button 1 URL </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtButton1URL" runat="server"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
																								<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Button 2 Text </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtButton2Text" runat="server"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>

											<div class="row mb-6">
												<!--begin::Label-->
																								<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Button 2 URL </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtButton2URL" runat="server"></asp:TextBox>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
																								<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Signature of Email </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													 <CKEditor:CKEditorControl ID="txtEmailSinature"  BasePath="~/Scripts/ckeditor_4.20.1/" runat="server" Height="100px"  ClientIDMode="Static" BasicEntities="true" FullPage="true"></CKEditor:CKEditorControl>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
																							<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span class="required">Attachments </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													 <asp:FileUpload runat="server" id="fileattachment" CssClass="form-control" Text="Upload" />
													
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
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:Button ID="btnUploa" runat="server" OnClick="btnUploa_Click" Text="Upload" CssClass="btn btn-secondary" />
													</div>
												</div>
											
												<div class="row mb-6">
												<!--begin::Label-->
																		<label class="col-lg-3 col-form-label fw-bold fs-6">
													<span> </span>
												
												</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">

													<asp:Button ID="btnSaveChange" runat="server" SkinID="btnSave" OnClick="btnSaveChange_Click" />
													</div>
												<!--end::Col-->
											</div>
											</div>
											</div>
									</div>
									</div>
		
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
