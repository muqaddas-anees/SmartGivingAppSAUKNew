<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="DashboardReport.aspx.cs" Inherits="DeffinityAppDev.App.TaithingDashBoard" %>

<%@ Register Src="~/App/controls/DonationReportTabs.ascx" TagPrefix="Pref" TagName="DonationReportTabs" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Donations Report
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:DonationReportTabs runat="server" id="DonationReportTabs" />
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    
</asp:Content>--%>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
    <style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px}
       </style>
    <style>
        .grid_header_right{
			text-align:right;
		}
    </style>
	<div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-5">
                <h3 class="fw-bolder m-0">Start date</h3> <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew" style="margin-left:5px;margin-right:5px"></asp:TextBox>
				<h3 class="fw-bolder m-0">End date</h3> <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew" style="margin-left:5px;margin-right:5px"></asp:TextBox>
				<asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" style="margin-left:5px;margin-right:5px"></asp:Button>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                  <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Text="Export Report" OnClick="btnReport_Click"     />

               
                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Add New Category" OnClick="btnAddOrganization_Click"       />--%>
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->

    </div>
	<div class="row gy-5 g-xl-8 mb-6">
		 <div class="col-xxl-9">
			<div class="card mb-5 mb-xl-10">
     
    <div class="row mb-6">
        <asp:GridView ID="GridDashboard" runat="server" Width="100%" OnRowCommand="GridDashboard_RowCommand">
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
				 <asp:TemplateField  HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' Visible="false"></asp:Label>
                         <asp:LinkButton ID="btnNavigate" runat="server"  Text='<%# Bind("Name") %>' CommandArgument='<%# Bind("ID") %>' CommandName="member"  />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Donation">
                    <ItemTemplate>
                        <asp:Label ID="lblTithigName" runat="server" Text='<%# Bind("CategoryList") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Logged By">
                    <ItemTemplate>
                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# Bind("PaidBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="grid_header_right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px" >
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N2}") %>'></asp:Label>
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
                 <asp:TemplateField HeaderText="Gift Aid">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkg" runat="server" Checked='<%# Bind("GiftAid") %>' Font-Size="20px" CssClass="mycheckBig pt-2" ></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField >
                    <ItemTemplate>
                       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="view"  />
                    </ItemTemplate>
                </asp:TemplateField>
				 <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                       <asp:Button ID="btnSendRecipt" runat="server" Text="Send Thank You" CssClass="btn btn-light" CommandArgument='<%# Bind("ID") %>' CommandName="SendReceipt"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        </div>

			 </div>
			 </div>

		<div class="col-xxl-3">
			 <div class="card card-xxl-stretch" >
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
															<asp:Label ID="Label8" runat="server" Text="Gift Aid"></asp:Label>
														</div>

														<div class="col">
															<asp:CheckBox ID="chkgiftaid" runat="server" Font-Size="20px" CssClass="mycheckBig pt-2" />
														</div>

													</div>
                                                    	<hr />
                                                    <div class="row mb-6" id="pnlTransaction" runat="server">
														<div class="col">
															<asp:Label ID="Label7" runat="server" Text="Transaction Fee Covered"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="lbltr" runat="server" Text=""></asp:Label>
														</div>

													</div>
                                                    	<hr />
                                                    <div class="row mb-6" id="pnlPlatform" runat="server">
														<div class="col">
															<asp:Label ID="Label9" runat="server" Text="Platform Fee Covered"></asp:Label>
														</div>

														<div class="col">
															<asp:Label ID="lblpf" runat="server" Text=""></asp:Label>
														</div>

													</div>
                                                      <div class="row mb-6">

                                                            <div class="row mb-6">
                                                                <label style="font-size:16px"> Documents </label>

                                                                </div>


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
												  <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand" >
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false"  />
                           <asp:TemplateField HeaderText="File Name">
                               <ItemTemplate>
                                   <asp:LinkButton ID="btnDownload" runat="server"  CommandArgument='<%# Eval("Value") %>' Text='<%# Eval("Text") %>' OnClick="DownloadFile"></asp:LinkButton>
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
                                                    <div class="row mb-6">
														<div class="col-lg-12">
															<asp:Label ID="Label6" runat="server" Text="Notes"></asp:Label>
														</div>

														<div class="col-lg-12">
															<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" SkinID="txtMulti_100"></asp:TextBox>
														</div>

													</div>

                                                    <div class="row mb-6">
                                                        <div class="col-lg-12">
                                                        <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" />

                                                             <asp:Button ID="btnCancelSubscription" runat="server" Text="Cancel Subscription" SkinID="btnDefault" OnClick="btnCancelSubscription_Click" Visible="false" />
                                                            </div>
                                                        </div>

													</div>
												
				 
				 </div>

			 </div>
	</div>


		

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
												
													<h3 class="card-label"><asp:Label ID="Label2" runat="server" Text="Send Receipt"></asp:Label> </h3>
												</div>
							
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
          <div class="row mb-6">
        <div class="col-lg-6 d-flex d-inline">
            <asp:Label ID="Label4" runat="server" Text="Template" style="margin-top:10px;padding-right:10px" ></asp:Label>
       
            <asp:DropDownList ID="ddlTemplate" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" AutoPostBack="true" SkinID="ddl_60" style="padding-right:10px">
               
            </asp:DropDownList>
           
            </div>
       
        </div>
 
        <div class="form-group row mb-6" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row mb-6">
               <div class="col-md-12 form-inline">
                  
									 <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor_4.20.1/" runat="server"
                         Height="370px" ClientIDMode="Static" BasicEntities="true" FullPage="true"  ></CKEditor:CKEditorControl>
                   </div>
								</div>
    </div>
       
           <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       
                      <asp:HiddenField ID="HiddenField1" runat="server" />
                       
                       
                          <asp:HiddenField ID="htomail" runat="server" />  <asp:HiddenField ID="hsubject" runat="server" />
                                        <asp:Button ID="Button1" runat="server" SkinID="btnDefault" Text="Send" OnClick="btnSend_Click" />
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
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


        var tid = $("[id$=hunid]").val();
        
        var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
            url: "UploadHandler.ashx?donate_unid=" + tid, // Set the url for your upload script location
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
