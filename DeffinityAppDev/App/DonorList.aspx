<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DonorList.aspx.cs" Inherits="DeffinityAppDev.App.DonorList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <asp:Label ID="lblPageTitle" runat="server" Text="Donors"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
     <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
   /* .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }*/
        </style>

    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>

      <ajaxToolkit:ModalPopupExtender ID="mdlManageOptionsNew" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptionsNew" PopupControlID="pnlAddReligionNew" CancelControlID="lbl_lbtnClosePassword" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptionsNew" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlAddReligionNew" runat="server" BackColor="White"
                       Width="450px" Height="190px"   ScrollBars="None">

		   <div class="card card-bordered">
    <div class="card-header">
        <h3 class="card-title"><asp:Label ID="lblModelHeading" runat="server" Text="Upload Members"></asp:Label> </h3>
        <div class="card-toolbar">
           <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
        </div>
    </div>
    <div class="card-body">
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
    </div>
    <div class="card-footer d-flex justify-content-end py-6 px-9">
       	<asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" OnClick="btnClose_Click" />
				<%--<asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" />--%>
    </div>
</div>
     
           </asp:Panel>

    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"><asp:Label ID="lblSubHeading" runat="server"></asp:Label> </h3>
									</div>
                                     <div class="card-toolbar gap-3" >

                                          <a class="btn btn-video" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#MainContent_MainContent_vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" runat="server" style="display:none" src="https://player.vimeo.com/video/773765312?h=329b78d0e8" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

                                         <asp:Button ID="btnDownload" runat="server" SkinID="btnDefault" Text="Download Template" OnClick="btnDownload_Click"   />  <asp:Button ID="btnUpload" runat="server" SkinID="btnDefault" Text="Upload Members" OnClick="btnUpload_Click" style="margin-right:20px;" CausesValidation="false"  />  <asp:Button ID="btnAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add New Member" OnClick="btnAddOrganization_Click"  />

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


                                             <div class="row mb-6" id="pnlShowUpload" runat="server">
                                                 <div class="col-lg-1">Select file:</div>
                                                   <div class="col-lg-2"><asp:FileUpload ID="fileUpload2" runat="server" /></div>
                                                   <div class="col-lg-2"> <asp:Button ID="btnUploadData" runat="server" SkinID="btnDefault" Text="Submit" OnClick="btnUploadData_Click"  /></div>
                                                     
                                                    </div>
											<!--begin::Input group-->
										
											<!--end::Input group-->
											<!--begin::Input group-->
											  <div class="row mb-6">

                                         <div class="row" style="display:none;visibility:hidden;">
                                             <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" >
                                                            <label class="col-lg-4 col-form-label fw-bold fs-6">Country</label>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" ><asp:ListItem Text="Please select..." ></asp:ListItem> </asp:DropDownList>
                                                            </div>
                                             <div class="col-lg-5 fv-row fv-plugins-icon-container" >
                                                   <label class="col-lg-4 col-form-label fw-bold fs-6">State</label>
															<input type="text" name="fname" class="form-control form-control-lg form-control-solid mb-3 mb-lg-0" placeholder="" value="">
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
                                             	<div class="col-lg-2 fv-row fv-plugins-icon-container" style="padding-top:40px">
															  </div>
                                             </div>

                                                    <div class="row">
                                                            <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                               <input type="text" name="fname" class="form-control form-control-lg form-control-solid mb-3 mb-lg-0" placeholder="Search" runat="server" id="txtsearch" value=""/>
                                                                </div>
                                                         <div class="col-lg-2 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                              <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" Text="Search" OnClick="btnSearch_Click" style="width:100%"   />
                                                            <asp:HyperLink ID="btnLinkAddOrganization" runat="server" CssClass="btn btn-primary" Text="Add New Organization" NavigateUrl="~/App/Organization.aspx" Visible="false" ></asp:HyperLink>
														
                                                                </div>
                                                         <div class="col-lg-2 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                            
                                                         

                                                        </div>
												
											
												<!--end::Col-->
											</div>

                                                   <div class="row">
                                                            <div class="col-lg-5 fv-row fv-plugins-icon-container form-inline" style="display:inline;">
                                                                <br />
                                                                </div>
                                                       </div>

											
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6">
												<asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand" OnRowDataBound="GridInstances_RowDataBound" OnPageIndexChanging="GridInstances_PageIndexChanging">
        <Columns>
              <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
                                                       <a href='Member.aspx?mid=<%# Eval("ID") %>&type=<%=QueryStringValues.Type %> ' class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
																		<!--begin::Svg Icon | path: icons/duotune/art/art005.svg-->
																		<span class="svg-icon svg-icon-3">
																			<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																				<path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
																				<path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
																			</svg>
																		</span>
																		<!--end::Svg Icon-->
																	</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="9px" >
                                                    <ItemTemplate>
                                                        
                                                         <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                       <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)" Visible="false"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <%-- <asp:TemplateField ItemStyle-Width="50px" >
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID").ToString()) %>' Width="50px" Height="50px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
              <asp:TemplateField HeaderText="Name" SortExpression="Organization">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstance" runat="server" Text='<%# Bind("InstanceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCNum" runat="server" Text='<%# Bind("ContactNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <%--<asp:TemplateField HeaderText="Religion" SortExpression="Religion" Visible="false">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReligion" runat="server" Text='<%# Bind("Religion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Denomination" SortExpression="Denomination" >
                                                    <HeaderStyle />
                                                    <ItemStyle Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDenomination" runat="server" Text='<%# Bind("Denomination") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
            
          
           <%--  <asp:TemplateField HeaderText="Email Address" SortExpression="EmailAddress">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Permission" SortExpression="" Visible="false">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPermission" runat="server" Text='<%# Bind("Permission") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="" SortExpression="">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSendPassword" runat="server" Text="Send Password" SkinID="btnDefault" CommandName="sendpasswordmail" CommandArgument='<%# Bind("ID") %>' />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
    
        </Columns>
    </asp:GridView>
											</div>
										
										
										</div>
										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
										<%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											
										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></div>
									<!--end::Form-->
								</div>
								<!--end::Content-->
        </div>
							</div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
             <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>


    <ajaxToolkit:ModalPopupExtender ID="mdlManagePassword" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lbladdPassword" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePopPassword" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="lbladdPassword" runat="server"></asp:Label>
        <asp:Label ID="lbtnClosePopPassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="700px" Height="320px" CssClass="card card-xxl-stretch p-5" ScrollBars="None">
           <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="card-header border-0">
							<h3 class="card-title fw-bolder"><asp:Label ID="lblOptions" runat="server" Text="Reset Password"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       <asp:Label ID="lblMsgPop" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                       <asp:Label ID="lblErrorPop" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="valSubmit" />
                       </div>
            </div>

         
        
        
    <div class="form-group row mb-6">
                   <div class="col-md-12">
                         <label class="col-sm-2 control-label">Password</label>
                  <div class="col-sm-10 d-flex d-inline">
                      <asp:TextBox ID="txtPassword" runat="server"  SkinID="txt_60" MaxLength="20"></asp:TextBox>
                      <asp:Button ID="btnGenaratePassword" runat="server" OnClick="btnGenaratePassword_Click" SkinID="btnDefault" Text="Generate Password"  />
                     <%--  <asp:RegularExpressionValidator ID="Regex3" runat="server" Display="None" ValidationGroup="valSubmit" ControlToValidate="txtPassword"
ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
ErrorMessage="Please enter password minimum 8 characters at least 1 upperCase alphabet, 1 lowerCase alphabet, 1 number and 1 special character" />--%>
                      
                      </div>
                       </div>
                        </div>
 

           <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       <div class="col-sm-12 form-inline">
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Send to User" OnClick="btnSubmitPop_Click" ValidationGroup="valSubmit" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
                           </div>
                       </div>
               </div>
                     </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>
           </asp:Panel>

   
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
        //set the dropzone container id
        var id = "#kt_dropzonejs_example_3";

        // set the preview element template
        var previewNode = $(id + " .dropzone-item");

        previewNode.id = "";
        var previewTemplate = previewNode.parent(".dropzone-items").html();
        previewNode.remove();

        var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
            url: "MemberHandler.ashx", // Set the url for your upload script location
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
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
