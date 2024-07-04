<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Speakers.aspx.cs" Inherits="DeffinityAppDev.App.Events.ManageSpeakers" %>

<%@ Register Src="~/App/Events/controls/EventTabs.ascx" TagPrefix="Pref" TagName="EventTabs" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Event
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:EventTabs runat="server" id="EventTabs" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    

    <!--begin::Head-->
	<%--<head>
		<title>Event Name and Details</title>
		
		<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->
	</head>--%>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="header-fixed header-tablet-and-mobile-fixed toolbar-enabled toolbar-fixed aside-enabled aside-fixed" style="--kt-toolbar-height:55px;--kt-toolbar-height-tablet-and-mobile:55px">
		<!--begin::Main-->
		



			<!--begin::Content-->
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">


						<asp:HiddenField ID="hEventid" runat="server" />

                        <div id="LocationEventId">


                            <div class="card card-xl-stretch mb-5 mb-xl-8">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-person-video3 text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Manage Speaker(s)</h3>
                                    </div>
                                    <!--end::Card title-->
                                     <div class="card-toolbar">
                                          <asp:Button ID="Button1" runat="server"  Text="Add Speaker" ToolTip="Add Speaker" OnClick="btnUpload_Click" Style="margin-right: 20px;" /> 
                                </div>
                                </div>
                               

                                <div class="card-body border-top p-9">



                                    <div class="row mb-6 d-flex justify-content-end"" >
												   <div class="col-sm-3 d-flex justify-content-end">

                                  <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                           
</div>
                                   
                                    </div>


											<asp:GridView runat="server" ID="BannerList" OnRowCommand="BannerList_RowCommand">

												<Columns>
													<asp:TemplateField ItemStyle-Width="5%">
														<ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("Speaker_ID") %>' CommandName="edit1" SkinID="BtnLinkEdit"></asp:LinkButton>
                                                            
															 <%-- <input id="Button1" type="button" value="Edit" onclick="EditSpeakerDetails('<%# Eval("Speaker_ID") %>');" class="btn btn-gray" > </input >--%>
                                                                                    
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField ItemStyle-Width="10%">
														<ItemTemplate>
															 <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("Speaker_ID")) %>' Width="100px" Height="100px" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Speaker" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="lblSpeaker" runat="server" Text=' <%# Eval("Speaker_Name") %> '></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Details" ItemStyle-Width="60%">
														<ItemTemplate>
															<asp:Label ID="lblBio" runat="server" Text=' <%# Eval("Speaker_Bio") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Linked In" ItemStyle-Width="10%">
														<ItemTemplate>
															<asp:Label ID="Label3" runat="server" Text= <%# Eval("LinkedIn") %>></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:LinkButton ID="btnDel" runat="server" SkinID="BtnLinkDelete" CommandName="del" CommandArgument='<%# Eval("Speaker_ID") %>' OnClientClick="return confirm('Do you want to delete this record?');"></asp:LinkButton>
														</ItemTemplate>
													</asp:TemplateField>
													
												</Columns>
											</asp:GridView>

										
											     
                                            <asp:HiddenField ID="HiddenSpeakerID" runat="server" />


                                            <asp:Button ID="btnEditSpeaker" runat="server" Text="Button"  OnClick="EditSpeakerinList"  style="display: none"    />


						<ajaxToolkit:ModalPopupExtender ID="mdlAddSpeaker" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnSpeakerAddOption" PopupControlID="pnlAddSpeaker" CancelControlID="btnSpekerClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnSpeakerAddOption" runat="server"></asp:Label>
    <asp:Label ID="btnSpekerClose" runat="server"></asp:Label>

	<asp:Panel ID="pnlAddSpeaker" runat="server" BackColor="White" Style="display: none;"
        Width="1000px"  Height="900px" ScrollBars="Both">                                                                        
                                                                                                   
        <div class="card card-bordered p-5">
            <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Add Speaker"></asp:Label>
                </h3>
                <div class="card-toolbar">
                    <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
                </div>
            </div>
           
            <!--begin::Input group-->
											<div class="row mb-6 ">
												<!--begin::Label-->
                                                <label class="col-lg-3 col-form-label fw-bold fs-6"    > Speaker's Image</label>
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline mt-6" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-150px h-150px" style="background-image: url(assets/media/avatars/150-26.jpg)">
                                                            <asp:Image ID="ImagePageBackground" runat="server" Height="150px" Width="150px" /></div>
														<!--end::Preview existing avatar-->
														<!--begin::Label-->
														<label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Change avatar">
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="FileUploadPageBackground" />
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


                                                    <asp:Label runat="server" ID="lblFilenotSelected" ForeColor="Red"> </asp:Label>

													<!--end::Hint-->
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
           

            <!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												 <label class="col-lg-3 col-form-label required fw-bold fs-6">  Speaker's Name :              </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
															<asp:TextBox ID="txtSpeakerName"     placeholder="Speaker Name" runat="server"></asp:TextBox>
                                                             <asp:RequiredFieldValidator  style="font-size:small" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" 
							ForeColor="Red" ErrorMessage="Please enter Speaker Name" ControlToValidate="txtSpeakerName" ValidationGroup="group1" ></asp:RequiredFieldValidator>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->




               <div class="row mb-6">
                <!--begin::Label-->
             <label class="col-lg-3 col-form-label fw-bold fs-6"> LinkedIn :</label>
                <!--end::Label-->
                <!--begin::Col-->
                <div class="col-lg-8">
                    <div class="row">
                        <!--begin::Col-->
                        <div class="col-lg-9 fv-row fv-plugins-icon-container">
                           <asp:TextBox ID="TextBoxLinkedIn"     placeholder="LinkedIn  ID" runat="server"></asp:TextBox>
                            <div class="fv-plugins-message-container invalid-feedback"></div>
                        </div>
                        <!--end::Col-->

                    </div>
                </div>
                <!--end::Col-->
            </div>







            <div class="row mb-6">
                <!--begin::Label-->
              

                 <label class="col-lg-3 col-form-label fw-bold fs-6">  Speaker's Details( Bio ) :             </label>
                <!--end::Label-->
                <!--begin::Col-->
                <div class="col-lg-8">
                    <div class="row">
                        <!--begin::Col-->
                        <div class="col-lg-13 fv-row fv-plugins-icon-container">
                             <CKEditor:CKEditorControl ID="CKEditorSpeakerDetails" BasePath="~/Scripts/ckeditor_4.20.1/" runat="server"  Height="180px"  Width="100%"  ></CKEditor:CKEditorControl>
                            <div class="fv-plugins-message-container invalid-feedback"></div>
                        </div>
                        <!--end::Col-->

                    </div>
                </div>
                <!--end::Col-->
            </div>




          


            



           

            <div class="card-footer d-flex justify-content-end py-6 px-9">
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-light mx-5" Text="Close" />

                

                <asp:Button runat="server" ID="UploadBanner" Text="Submit"  OnClick="UploadBanner_Click"  ValidationGroup="group1"  />

               <%-- <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />--%>
            </div>
        </div>






    </asp:Panel>


	



                                </div>

                                <div class="card-footer d-flex justify-content-end py-6 px-9">              
                                   

                             
                                  <asp:Button ID="btnPublish" runat="server" SkinID="btnDefault" Text="Save & Publish" OnClick="btnPublish_Click" Visible="false" />

                                </div>


                            </div>

                        </div>


						

	  <style>
        .modalBackground {
            overflow : scroll;
           
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
           
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }



          select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }

    </style>


						</div>
						</body>
	
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
 