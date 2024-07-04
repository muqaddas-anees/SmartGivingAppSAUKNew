<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="JobTargets.aspx.cs" EnableEventValidation="false" Inherits="DeffinityAppDev.WF.DC.JobTargets" %>

<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagPrefix="Pref" TagName="FLSTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	 <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:FLSTab runat="server" ID="FLSTab" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
	
	<asp:UpdatePanel ID="upTarget" runat="server" UpdateMode="Conditional">
		<ContentTemplate>

			  <link href="../../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../../assets/plugins/global/plugins.bundle.js"></script>

			<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
<script type="text/javascript">

    $("#MainContent_MainContent_txtStartDate").flatpickr({
        //enableTime: true,
        dateFormat: "m/d/Y"

	});

    $(".input_time_new").flatpickr({
        enableTime: true,
		dateFormat: "H:i",
        noCalendar: true,

    });
</script>
		 <ajaxToolkit:ModalPopupExtender ID="mdlTargets" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddTarget" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>

    <asp:Panel ID="pnlAddTarget" runat="server" BackColor="White" Style="display:none;"
                      Height="400px" Width="700px"   CssClass="card " ScrollBars="None">
		<%--<div class="modal fade show">
		<div class="modal-dialog modal-dialog-centered mw-650px">--%>
										<!--begin::Modal content-->
										<div class="modal-content rounded">
											<!--begin::Modal header-->
											<div class="modal-header pb-0 border-0 justify-content-end">
												 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
												
											</div>
											<!--begin::Modal header-->
											<!--begin::Modal body-->
											<div class="modal-body scroll-y px-10 px-lg-15 pt-0 pb-15">
												<!--begin:Form-->
												<form id="kt_modal_new_target_form2" class="form" action="#">
													<!--begin::Heading-->
													<div class="mb-13 text-center">
														<!--begin::Title-->
														<h1 class="mb-3"><asp:Label ID="lblOptions" runat="server" Text="Set Task"></asp:Label> <asp:HiddenField ID="hid" runat="server" Value="0" /><asp:HiddenField ID="hcallid" runat="server" Value="0" /> </h1>
														<!--end::Title-->
													<%--	<!--begin::Description-->
														<div class="text-muted fw-bold fs-5">If you need more info, please check
														<a href="#" class="fw-bolder link-primary">Project Guidelines</a>.</div>
														<!--end::Description-->--%>
													</div>
													<!--end::Heading-->
													<!--begin::Input group-->
													<div class="d-flex flex-column mb-8 fv-row">
														<!--begin::Label-->
														<label class="d-flex align-items-center fs-6 fw-bold mb-2">
															<span class="required">Task Title</span>
															<i class="fas fa-exclamation-circle ms-2 fs-7" data-bs-toggle="tooltip" title="Specify a target name for future usage and reference"></i>
														</label>
														<!--end::Label-->
														<input runat="server" id="txttitle" type="text" class="form-control form-control-solid" placeholder="Enter Task Title" name="target_title" />
													</div>
													<!--end::Input group-->
													<!--begin::Input group-->
													<div class="row g-9 mb-8">
														<!--begin::Col-->
														<div class="col-md-6 fv-row">
															<label class="required fs-6 fw-bold mb-2">Status</label>

															<asp:DropDownList ID="ddlStatus" runat="server">
																<asp:ListItem Text="Pending"></asp:ListItem>
																<asp:ListItem Text="In Progress"></asp:ListItem>
																<asp:ListItem Text="Completed"></asp:ListItem>
															</asp:DropDownList>
															<%--<select class="form-select form-select-solid" data-control="select2" data-hide-search="true" data-placeholder="Select a Team Member" name="target_assign">
																<option value="">Select user...</option>
																<option value="1">Karina Clark</option>
																<option value="2">Robert Doe</option>
																<option value="3">Niel Owen</option>
																<option value="4">Olivia Wild</option>
																<option value="5">Sean Bean</option>
															</select>--%>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-md-6 fv-row">
															<label class="required fs-6 fw-bold mb-2">Due Date</label>
															<!--begin::Input-->
															<div class="position-relative d-flex align-items-center">
																<asp:TextBox ID="txtStartDate" Text="" runat="server" SkinID="DateNew"></asp:TextBox>
																
															</div>
															<!--end::Input-->
														</div>
														<!--end::Col-->
													</div>
													<!--end::Input group-->
													<!--begin::Input group-->
													<div class="d-flex flex-column mb-8">
														<label class="fs-6 fw-bold mb-2">Task Details</label>
														<textarea id="txtDetails" runat="server" class="form-control form-control-solid" rows="3" name="target_details" placeholder="Type Task Details"></textarea>
													</div>

													<div class="d-flex flex-column mb-8">
														<label class="fs-6 fw-bold mb-2">Assigned To</label>

														<asp:Panel ID="pnlUsers" runat="server" Width="600px" Height="100px" ScrollBars="Vertical">
															<asp:CheckBoxList ID="ddlUsers" runat="server" CssClass="form-select form-select-solid form-select-lg fw-bold" ></asp:CheckBoxList>
														</asp:Panel>

														</div>
													<!--end::Input group-->
													
													
													<!--begin::Actions-->
													<div class="text-center">
														<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-light me-3" Text="Cancel"  />
														<asp:Button ID="btnSaveTarget" runat="server" SkinID="btnSubmit"  OnClick="btnSaveTarget_click"  />
														<%--<button type="reset" id="kt_modal_new_target_cancel" class="btn btn-light me-3">Cancel</button>--%>
														<%--<button type="submit" id="kt_modal_new_target_submit" class="btn btn-primary">
															<span class="indicator-label">Submit</span>
															<span class="indicator-progress">Please wait...
															<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
														</button>--%>
													</div>
													<!--end::Actions-->
												</form>
												<!--end:Form-->
											</div>
											<!--end::Modal body-->
										</div>
										<!--end::Modal content-->
									<%--</div>

			</div>--%>
          
       
    </asp:Panel>

			 <ajaxToolkit:ModalPopupExtender ID="mdlFile" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddFiles" PopupControlID="pnlFiles" CancelControlID="btnclosefilespopup" >
</ajaxToolkit:ModalPopupExtender>

			 <asp:Label ID="btnAddFiles" runat="server"></asp:Label>
        <asp:Label ID="btnclosefilespopup" runat="server"></asp:Label>

			<asp:Panel ID="pnlFiles" runat="server" BackColor="White" Style="display:none;"
                      Height="600px" Width="700px"   CssClass="card " ScrollBars="None">
		<%--<div class="modal fade show">
		<div class="modal-dialog modal-dialog-centered mw-650px">--%>
										<!--begin::Modal content-->
										<div class="modal-content rounded">
											<!--begin::Modal header-->
											<div class="modal-header pb-0 border-0 justify-content-end">
												 <asp:LinkButton ID="btnclosefilespopup1" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"  OnClick="btnclosedocs_click"/>
												
											</div>
											<!--begin::Modal header-->
											<!--begin::Modal body-->
											<div class="modal-body scroll-y px-10 px-lg-15 pt-0 pb-15" style="height:600px">
												<!--begin:Form-->
												<form id="kt_modal_new_target_form1" class="form" action="#">
													<!--begin::Heading-->
													<div class="mb-13 text-center">
														<!--begin::Title-->
														<h1 class="mb-3"><asp:Label ID="Label1" runat="server" Text="Upload Files"></asp:Label> <asp:HiddenField ID="hTaskID" runat="server" Value="0" /> <asp:HiddenField ID="hTaskCallID" runat="server" Value="0" /> </h1>
														<!--end::Title-->
													
													</div>
													<!--end::Heading-->
													<!--begin::Input group-->
												<%--	<div class="d-flex flex-column mb-8 fv-row">
														<!--begin::Label-->
														<label class="d-flex align-items-center fs-6 fw-bold mb-2">
															<span class="required">Task Title</span>
															<i class="fas fa-exclamation-circle ms-2 fs-7" data-bs-toggle="tooltip" title="Specify a target name for future usage and reference"></i>
														</label>
														<!--end::Label-->
														<input runat="server" id="Text1" type="text" class="form-control form-control-solid" placeholder="Enter Task Title" name="target_title" />
													</div>--%>

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

													<div class="d-flex flex-column mb-8 fv-row">

														 <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand" >
                       <Columns>
						   <asp:HyperLinkField HeaderText="File Name"  Text="Link"  DataNavigateUrlFormatString="~/WF/DC/TaskFileDownload.ashx?docid={0}" DataNavigateUrlFields="ID" DataTextField="FileName" />
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false"  />
                           <asp:TemplateField HeaderText="File Name" Visible="false">
                               <ItemTemplate>
								   <asp:HyperLink ID="lbldownload" runat="server"  NavigateUrl="~/WF/DC/TaskFileDownload.ashx?docid=" Text='<%# Eval("FileName") %>'  ></asp:HyperLink>
                                   <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("FileName") %>'></asp:LinkButton>
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

													</form>
													</div>
											</div>
				</asp:Panel>



			 <ajaxToolkit:ModalPopupExtender ID="mdlCommentes" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddComments" PopupControlID="pblComments" CancelControlID="btnclosedisplaycomment" >
</ajaxToolkit:ModalPopupExtender>

			 <asp:Label ID="btnAddComments" runat="server"></asp:Label>
        <asp:Label ID="btnclosedisplaycomment" runat="server"></asp:Label>

			<asp:Panel ID="pblComments" runat="server" BackColor="White" Style="display:none;"
                      Height="600px" Width="700px"   CssClass="card " ScrollBars="None">
				<div class="modal-content rounded">
											<!--begin::Modal header-->
											<div class="modal-header pb-0 border-0 justify-content-end">
												 <asp:LinkButton ID="btnCloseComments" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"  OnClick="btncloseCommnets_click" />
												
											</div>
											<!--begin::Modal header-->
											<!--begin::Modal body-->
											<div class="modal-body scroll-y px-10 px-lg-15 pt-0 pb-15" style="height:600px">
												<!--begin:Form-->
												<%--<form id="kt_modal_new_target_form13" class="form" action="#">--%>
													<!--begin::Heading-->
													<div class="mb-13 text-center">
														<!--begin::Title-->
														<h1 class="mb-3"><asp:Label ID="lblTItleCOmments" runat="server" Text="Comments"></asp:Label> <asp:HiddenField ID="hTaskIDComments" runat="server" Value="0" /> <asp:HiddenField ID="hCallIDComments" runat="server" Value="0" /> </h1>
														<!--end::Title-->
													
													</div>
												

												
													<div class="d-flex flex-column mb-8 fv-row">
														<asp:TextBox ID="txtComments" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" placeholder="Add Comments"></asp:TextBox>
														 
														
														</div>
													<div class="d-flex flex-column mb-8 fv-row">
														
														<div class="col-sm-3 d-flex">
														<asp:Button ID="btnAdd" runat="server" Text="Post" SkinID="btnDefault" OnClick="Post_click" />
															</div>
														</div>

													<div class="d-flex flex-column mb-8 fv-row">

														 <asp:ListView ID="list_comments" runat="server">
															  <LayoutTemplate>
             <div >
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
															 <AlternatingItemTemplate>
																 													
			  <div class="p-5 rounded bg-light-primary text-dark fw-bold text-start" data-kt-element="message-text">
			  <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Details") %>'></asp:Label>

				  </div>
			  <div class="row mb-6">
				  <div class="col-lg-6"> <label style="font-weight:bold">Posted by: </label>
			    <asp:Label ID="lblLoggedBy" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
					  </div>
				  <div class="col-lg-6 d-flex justify-content-end" > <label style="font-weight:bold">Posted On: </label>
			    <asp:Label ID="lblLoggedTime" runat="server" Text='<%# Eval("LoggedDateDisplay") %>'></asp:Label>
					  </div>
				  </div>
			

															 </AlternatingItemTemplate>
          <ItemTemplate>
			  <div class="p-5 rounded bg-light-info text-dark fw-bold text-start" data-kt-element="message-text">
			  <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Details") %>'></asp:Label>

				  </div>
			  <div class="row mb-6">
				  <div class="col-lg-6"> <label style="font-weight:bold">Posted by: </label>
			    <asp:Label ID="lblLoggedBy" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
					  </div>
				  <div class="col-lg-6 d-flex justify-content-end" > <label style="font-weight:bold">Posted On: </label>
			    <asp:Label ID="lblLoggedTime" runat="server" Text='<%# Eval("LoggedDateDisplay") %>'></asp:Label>
					  </div>
				  </div>
			 </ItemTemplate>
														 </asp:ListView>
														
														</div>

												<%--	</form>--%>
													</div>
											</div>

				</asp:Panel>

    <div class="d-flex flex-wrap flex-stack pt-10 pb-8" data-select2-id="select2-data-148-dkpk">
									<!--begin::Heading-->
									<h3 class="fw-bolder my-2">Tasks
									<span class="fs-6 text-gray-400 fw-bold ms-1">by Recent Updates ↓</span></h3>
									<!--end::Heading-->
									<!--begin::Controls-->
									<div class="d-flex flex-wrap my-1" data-select2-id="select2-data-147-3cc4">
										<!--begin::Tab nav-->
										<ul class="nav nav-pills me-5">
											<li class="nav-item m-0" style="display:none;">
												<a class="btn btn-sm btn-icon btn-light btn-color-muted btn-active-primary active me-3" data-bs-toggle="tab" href="#kt_project_targets_card_pane">
													<!--begin::Svg Icon | path: icons/duotune/general/gen024.svg-->
													<span class="svg-icon svg-icon-2">
														<svg xmlns="http://www.w3.org/2000/svg" width="24px" height="24px" viewBox="0 0 24 24">
															<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
																<rect x="5" y="5" width="5" height="5" rx="1" fill="#000000"></rect>
																<rect x="14" y="5" width="5" height="5" rx="1" fill="#000000" opacity="0.3"></rect>
																<rect x="5" y="14" width="5" height="5" rx="1" fill="#000000" opacity="0.3"></rect>
																<rect x="14" y="14" width="5" height="5" rx="1" fill="#000000" opacity="0.3"></rect>
															</g>
														</svg>
													</span>
													<!--end::Svg Icon-->
												</a>
											</li>
											<%--<li class="nav-item m-0">
												<a class="btn btn-sm btn-icon btn-light btn-color-muted btn-active-primary" data-bs-toggle="tab" href="#kt_project_targets_table_pane">
													<!--begin::Svg Icon | path: icons/duotune/abstract/abs015.svg-->
													<span class="svg-icon svg-icon-2">
														<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
															<path d="M21 7H3C2.4 7 2 6.6 2 6V4C2 3.4 2.4 3 3 3H21C21.6 3 22 3.4 22 4V6C22 6.6 21.6 7 21 7Z" fill="black"></path>
															<path opacity="0.3" d="M21 14H3C2.4 14 2 13.6 2 13V11C2 10.4 2.4 10 3 10H21C21.6 10 22 10.4 22 11V13C22 13.6 21.6 14 21 14ZM22 20V18C22 17.4 21.6 17 21 17H3C2.4 17 2 17.4 2 18V20C2 20.6 2.4 21 3 21H21C21.6 21 22 20.6 22 20Z" fill="black"></path>
														</svg>
													</span>
													<!--end::Svg Icon-->
												</a>
											</li>--%>

											<li class="nav-item m-0">

												
   <%-- <div class="form-group row mb-6">
              <div class="col-md-12 d-flex d-inline">
                  <asp:Button ID="btnAddNew" runat="server" SkinID="btnDefault" Text="Create New Target" style="text-align:right;float:right;" OnClick="btnAddTarget_click"   />
                  </div>
        </div>--%>
											</li>
										</ul>
										<!--end::Tab nav-->
										<!--begin::Wrapper-->
										<div class="my-0"  >

											<asp:Button ID="btnSaveTemplate" runat="server" SkinID="btnDefault" Text="Save Template" OnClick="btnSaveTemplate_Click" Visible="false" />
											<!--begin::Select-->
											<select style="display:none;" name="status" data-control="select2" data-hide-search="true" class="form-select form-select-white form-select-sm w-150px select2-hidden-accessible" data-select2-id="select2-data-64-h81u" tabindex="-1" aria-hidden="true">
												<option value="1" selected="selected" data-select2-id="select2-data-66-wkdw">Recently Updated</option>
												<option value="2" data-select2-id="select2-data-149-3wkt">Last Month</option>
												<option value="3" data-select2-id="select2-data-150-7fcq">Last Quarter</option>
												<option value="4" data-select2-id="select2-data-151-rg11">Last Year</option>
											</select>
											<%--<span class="select2 select2-container select2-container--bootstrap5 select2-container--below" dir="ltr" data-select2-id="select2-data-65-80a9" style="width: 100%;">
												<span class="selection">
													<span class="select2-selection select2-selection--single form-select form-select-white form-select-sm w-150px" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-disabled="false" aria-labelledby="select2-status-x8-container" aria-controls="select2-status-x8-container">
												<span class="select2-selection__rendered" id="select2-status-x8-container" role="textbox" aria-readonly="true" title="Recently Updated">Recently Updated</span>
												<span class="select2-selection__arrow" role="presentation"><b role="presentation"></b></span>

												                        </span>

												</span>
												<span class="dropdown-wrapper" aria-hidden="true"></span>

											</span>--%>
											<!--end::Select-->
										</div>
										<!--end::Wrapper-->
									</div>
									<!--end::Controls-->
								</div>


	<div id="kt_project_targets_card_pane" class="tab-pane fade show active" data-select2-id="select2-data-kt_project_targets_card_pane">
										<!--begin::Row-->
										<div class="row g-9" data-select2-id="select2-data-137-qw49">
											<!--begin::Col-->
											<div class="col-md-4 col-lg-12 col-xl-4" data-select2-id="select2-data-136-9n3x">
												<div class="mb-9">
													<div class="d-flex flex-stack">
														<div class="fw-bolder fs-4">Yet to start
														<span class="fs-6 text-gray-400 ms-2"><asp:Literal ID="lblPendingCount" runat="server" Text="0"></asp:Literal> </span></div>
														<!--begin::Menu-->
														<div>
															
															
														</div>
														<!--end::Menu-->
													</div>
													<div class="h-3px w-100 bg-warning"></div>
												</div>
												<asp:ListView ID="list_pending" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
             <div >
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
			  <div class="card mb-6 mb-xl-9">
													<!--begin::Card body-->
													<div class="card-body">
														<!--begin::Header-->
														<div class="d-flex flex-stack mb-3">
															<!--begin::Badge-->
															<%--<div class="badge badge-light">Phase 2.6 QA</div>--%>
															<!--end::Badge-->
															
														</div>
														<!--end::Header-->
														<!--begin::Title-->
														<div class="mb-2">

															<asp:LinkButton ID="btnTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary" CommandArgument='<%# Eval("ID") %>' CommandName="View"></asp:LinkButton>
															<%--<a href="#" class="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary"> <asp:Literal ID="lblTargetTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Literal> </a>--%>
														</div>
														<!--end::Title-->
														<!--begin::Content-->
														<div class="fs-6 fw-bold text-gray-600 mb-5"><asp:Literal ID="lblTargetDesc" runat="server" Text='<%# Eval("Details") %>'></asp:Literal></div>
														<!--end::Content-->
														<!--begin::Footer-->
														<div class="d-flex flex-stack flex-wrapr">
															<!--begin::Users-->
															<div class="symbol-group symbol-hover my-1">

																<asp:Literal ID="lblUsers" runat="server"></asp:Literal>
																<%--<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Alan Warden">
																	<span class="symbol-label bg-warning text-inverse-warning fw-bolder">A</span>
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Perry Matthew">
																	<span class="symbol-label bg-success text-inverse-success fw-bolder">R</span>
																</div>--%>
															</div>
															<!--end::Users-->
															<!--begin::Stats-->
															<div class="d-flex my-1">
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3">
																	<asp:LinkButton ID="btnDocSHow" runat="server" CssClass="fas fa-paperclip" CommandName="Docs" CommandArgument='<%# Eval("ID") %>' ForeColor="GrayText"></asp:LinkButton>
																	<!--begin::Svg Icon | path: icons/duotune/communication/com008.svg-->
																	<%--<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z" fill="black"></path>
																			<path d="M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z" fill="black"></path>
																		</svg>
																	</span>--%>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600"><asp:Literal ID="lblDocs" runat="server" Text='<%# Eval("DocsCount") %>' ></asp:Literal> </span>
																</div>
																<!--end::Stat-->
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3 ms-3">
																	<asp:LinkButton ID="btnComments" runat="server" CssClass="far fa-comment-alt" CommandName="Comments" CommandArgument='<%# Eval("ID") %>' ForeColor="GrayText"></asp:LinkButton>
																	<!--begin::Svg Icon | path: icons/duotune/communication/com012.svg-->
																	<%--<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="black"></path>
																			<rect x="6" y="12" width="7" height="2" rx="1" fill="black"></rect>
																			<rect x="6" y="7" width="12" height="2" rx="1" fill="black"></rect>
																		</svg>
																	</span>--%>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600"><asp:Literal ID="lblComments" runat="server" Text='<%# Eval("CommentsCount") %>'></asp:Literal> </span>
																</div>
																<!--end::Stat-->
															</div>
															<!--end::Stats-->
														</div>
														<!--end::Footer-->
													</div>
													<!--end::Card body-->
												</div>

			  	

			  </ItemTemplate>
		</asp:ListView>
												<div class="d-flex">
													 <asp:Button ID="Button1" runat="server" SkinID="btnDefault" Text="Create New Task" style="width:100%;" OnClick="btnAddTarget_click"   />
												</div>

												</div>
											<div class="col-md-4 col-lg-12 col-xl-4" data-select2-id="select2-data-136-9n3x">
												<div class="mb-9">
													<div class="d-flex flex-stack">
														<div class="fw-bolder fs-4">In Progress
														<span class="fs-6 text-gray-400 ms-2"><asp:Literal ID="lblInProgressCount" runat="server" Text="0"></asp:Literal> </span></div>
														<!--begin::Menu-->
														<div>
															
															
														</div>
														<!--end::Menu-->
													</div>
													<div class="h-3px w-100 bg-primary"></div>
												</div>
												<asp:ListView ID="list_inprogress" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
             <div >
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
			  <div class="card mb-6 mb-xl-9">
													<!--begin::Card body-->
													<div class="card-body">
														<!--begin::Header-->
														<div class="d-flex flex-stack mb-3">
															<!--begin::Badge-->
															<%--<div class="badge badge-light">Phase 2.6 QA</div>--%>
															<!--end::Badge-->
															
														</div>
														<!--end::Header-->
														<!--begin::Title-->
														<div class="mb-2">

															<asp:LinkButton ID="btnTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary" CommandArgument='<%# Eval("ID") %>' CommandName="View"></asp:LinkButton>
															<%--<a href="#" class="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary"> <asp:Literal ID="lblTargetTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Literal> </a>--%>
														</div>
														<!--end::Title-->
														<!--begin::Content-->
														<div class="fs-6 fw-bold text-gray-600 mb-5"><asp:Literal ID="lblTargetDesc" runat="server" Text='<%# Eval("Details") %>'></asp:Literal></div>
														<!--end::Content-->
														<!--begin::Footer-->
														<div class="d-flex flex-stack flex-wrapr">
															<!--begin::Users-->
															<div class="symbol-group symbol-hover my-1">

																<asp:Literal ID="lblUsers" runat="server"></asp:Literal>
																<%--<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Alan Warden">
																	<span class="symbol-label bg-warning text-inverse-warning fw-bolder">A</span>
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Perry Matthew">
																	<span class="symbol-label bg-success text-inverse-success fw-bolder">R</span>
																</div>--%>
															</div>
															<!--end::Users-->
															<!--begin::Stats-->
															<div class="d-flex my-1">
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3">
																	<asp:LinkButton ID="btnDocSHow" runat="server" CssClass="fas fa-paperclip" CommandName="Docs" CommandArgument='<%# Eval("ID") %>' ForeColor="GrayText"></asp:LinkButton>
																	<!--begin::Svg Icon | path: icons/duotune/communication/com008.svg-->
																	<%--<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z" fill="black"></path>
																			<path d="M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z" fill="black"></path>
																		</svg>
																	</span>--%>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600"><asp:Literal ID="lblDocs" runat="server" Text='<%# Eval("DocsCount") %>' ></asp:Literal> </span>
																</div>
																<!--end::Stat-->
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3 ms-3">
																	<asp:LinkButton ID="btnComments" runat="server" CssClass="far fa-comment-alt" CommandName="Comments" CommandArgument='<%# Eval("ID") %>' ForeColor="GrayText"></asp:LinkButton>
																	<!--begin::Svg Icon | path: icons/duotune/communication/com012.svg-->
																	<%--<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="black"></path>
																			<rect x="6" y="12" width="7" height="2" rx="1" fill="black"></rect>
																			<rect x="6" y="7" width="12" height="2" rx="1" fill="black"></rect>
																		</svg>
																	</span>--%>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600"><asp:Literal ID="lblComments" runat="server" Text='<%# Eval("CommentsCount") %>'></asp:Literal> </span>
																</div>
																<!--end::Stat-->
															</div>
															<!--end::Stats-->
														</div>
														<!--end::Footer-->
													</div>
													<!--end::Card body-->
												</div>

			  	

			  </ItemTemplate>
		</asp:ListView>
												</div>
											<div class="col-md-4 col-lg-12 col-xl-4" data-select2-id="select2-data-136-9n3x">
												<div class="mb-9">
													<div class="d-flex flex-stack">
														<div class="fw-bolder fs-4">Completed
														<span class="fs-6 text-gray-400 ms-2"><asp:Literal ID="lblCompletedCount" runat="server" Text="0"></asp:Literal> </span></div>
														<!--begin::Menu-->
														<div>
															
															
														</div>
														<!--end::Menu-->
													</div>
													<div class="h-3px w-100 bg-success"></div>
												</div>
												<asp:ListView ID="list_complete" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
             <div >
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  
              </LayoutTemplate>
          <ItemTemplate>
			  <div class="card mb-6 mb-xl-9">
													<!--begin::Card body-->
													<div class="card-body">
														<!--begin::Header-->
														<div class="d-flex flex-stack mb-3">
															<!--begin::Badge-->
															<%--<div class="badge badge-light">Phase 2.6 QA</div>--%>
															<!--end::Badge-->
															
														</div>
														<!--end::Header-->
														<!--begin::Title-->
														<div class="mb-2">

															<asp:LinkButton ID="btnTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary" CommandArgument='<%# Eval("ID") %>' CommandName="View"></asp:LinkButton>
															<%--<a href="#" class="fs-4 fw-bolder mb-1 text-gray-900 text-hover-primary"> <asp:Literal ID="lblTargetTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Literal> </a>--%>
														</div>
														<!--end::Title-->
														<!--begin::Content-->
														<div class="fs-6 fw-bold text-gray-600 mb-5"><asp:Literal ID="lblTargetDesc" runat="server" Text='<%# Eval("Details") %>'></asp:Literal></div>
														<!--end::Content-->
														<!--begin::Footer-->
														<div class="d-flex flex-stack flex-wrapr">
															<!--begin::Users-->
															<div class="symbol-group symbol-hover my-1">

																<asp:Literal ID="lblUsers" runat="server"></asp:Literal>
																<%--<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Alan Warden">
																	<span class="symbol-label bg-warning text-inverse-warning fw-bolder">A</span>
																</div>
																<div class="symbol symbol-35px symbol-circle" data-bs-toggle="tooltip" title="" data-bs-original-title="Perry Matthew">
																	<span class="symbol-label bg-success text-inverse-success fw-bolder">R</span>
																</div>--%>
															</div>
															<!--end::Users-->
															<!--begin::Stats-->
															<div class="d-flex my-1">
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3">
																	<asp:LinkButton ID="btnDocSHow" runat="server" CssClass="fas fa-paperclip" CommandName="Docs" CommandArgument='<%# Eval("ID") %>' ForeColor="GrayText"></asp:LinkButton>
																	<!--begin::Svg Icon | path: icons/duotune/communication/com008.svg-->
																	<%--<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z" fill="black"></path>
																			<path d="M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z" fill="black"></path>
																		</svg>
																	</span>--%>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600"><asp:Literal ID="lblDocs" runat="server" Text='<%# Eval("DocsCount") %>' ></asp:Literal> </span>
																</div>
																<!--end::Stat-->
																<!--begin::Stat-->
																<div class="border border-dashed border-gray-300 rounded py-2 px-3 ms-3">
																	<asp:LinkButton ID="btnComments" runat="server" CssClass="far fa-comment-alt" CommandName="Comments" CommandArgument='<%# Eval("ID") %>' ForeColor="GrayText"></asp:LinkButton>
																	<!--begin::Svg Icon | path: icons/duotune/communication/com012.svg-->
																	<%--<span class="svg-icon svg-icon-3">
																		<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																			<path opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="black"></path>
																			<rect x="6" y="12" width="7" height="2" rx="1" fill="black"></rect>
																			<rect x="6" y="7" width="12" height="2" rx="1" fill="black"></rect>
																		</svg>
																	</span>--%>
																	<!--end::Svg Icon-->
																	<span class="ms-1 fs-7 fw-bolder text-gray-600"><asp:Literal ID="lblComments" runat="server" Text='<%# Eval("CommentsCount") %>'></asp:Literal> </span>
																</div>
																<!--end::Stat-->
															</div>
															<!--end::Stats-->
														</div>
														<!--end::Footer-->
													</div>
													<!--end::Card body-->
												</div>

			  	

			  </ItemTemplate>
		</asp:ListView>
												</div>
											</div>
		</div>


			

			<script type="text/javascript">
                function pageLoad() {

                    $(document).ready(function () {

                        //jquery code and events, event binding.. etc..

                        var id = "#kt_dropzonejs_example_3";

                        // set the preview element template
                        var previewNode = $(id + " .dropzone-item");

                        previewNode.id = "";
                        var previewTemplate = previewNode.parent(".dropzone-items").html();
                        previewNode.remove();

                        var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
                            url: "TaskUploadHandler.ashx?taskid=" + $("#<%:hTaskID.ClientID%>").val(), // Set the url for your upload script location
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

                    });
                }
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

            </script>

	</ContentTemplate>

	</asp:UpdatePanel>

	


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
