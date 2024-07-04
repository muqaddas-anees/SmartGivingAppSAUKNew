<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="DeffinityAppDev.App.Activity" %>


<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	Event
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
	 <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }
        </style>
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Event </h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
										 <asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Save Changes" OnClick="btnSaveAndEdit_Click"  /> 

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
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> Event Category </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															 <asp:DropDownList AutoPostBack="true" ID="ddlActiviteCategory" OnSelectedIndexChanged="ddlActiviteCategory_SelectedIndexChanged"  runat="server"></asp:DropDownList>
															
														<div class="fv-plugins-message-container invalid-feedback"></div>


														</div>
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:Button ID="btnAdd" runat="server"  OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />
															<asp:LinkButton ID="btnDelCategory" runat="server"  OnClick="btnCategoryDelete_click" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete record?');"  />
															</div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->


											<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Event Sub Category </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															 <asp:DropDownList ID="ddlSubCategory" runat="server"> </asp:DropDownList>
															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															<asp:Button ID="btnAddDenimination" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add"  />
															<asp:LinkButton ID="btnSubCategoryDelete" runat="server"  OnClick="btnSubCategoryDelete_click" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete record?');"  />	
															</div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->

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
															<asp:TextBox ID="txtTitle" runat="server"  ></asp:TextBox>
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
												<label class="col-lg-4 col-form-label fw-bold fs-6">Description</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
													
															<%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

															<%--<asp:TextBox ID="" runat="server" TextMode="MultiLine" Height="200" Columns="100" ></asp:TextBox>--%>
															  <CKEditor:CKEditorControl ID="txtDescriptionArea" BasePath="~/Scripts/ckeditor/" runat="server"  Height="200px"  Width="100%"  ></CKEditor:CKEditorControl>
															
															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											
													
											  <div class="row mb-6">
                                 
                                       <label class="col-lg-4 col-form-label fw-bold fs-6"">Image</label>
                                      <div class="col-lg-8 ">
                                         
                                           <asp:FileUpload ID="fileupload" runat="server" /><br /><br />

										  <asp:Image ID="img" runat="server" ImageUrl="" />
                                          </div>
                                    
    </div>

											
										
											<div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="row">
														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">

															<label class="col-form-label required fw-bold fs-6">Event Start Date & Time :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox ID="txtStartDate" Text="" runat="server" SkinID="DateNew"></asp:TextBox>
																	</div>
																<div class="col-sm-6">
															<asp:TextBox ID="txtStartTime" runat="server" SkinID="TimeNew"></asp:TextBox>
																	</div>
															</div>
														<%--<div class="fv-plugins-message-container invalid-feedback"></div>--%>

														</div>
														<!--end::Col-->
														
														<!--begin::Col-->
														<label class="col-lg-2 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
															<label class=" col-form-label required fw-bold fs-6">Event End Date & Time :</label>
															<div class="row">
																<div class="col-sm-4">
															<asp:TextBox  ID="TextEndDate" Text="" SkinID="DateNew" runat="server"></asp:TextBox>
																	</div>
																<div class="col-sm-4">
															<asp:TextBox ID="txtEndTime" runat="server" SkinID="TimeNew"></asp:TextBox>
																	</div>
																</div>
														<%--<div class="fv-plugins-message-container invalid-feedback"></div>--%>

														</div>
														<!--end::Col-->
													</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6">Slots </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtSlot" runat="server" SkinID="Price_175px" Text="0"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>
												</div>
												<!--end::Col-->
											</div>
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Price </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtPrice" runat="server" SkinID="Price_175px" Text="0.00"></asp:TextBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>
												</div>
												<!--end::Col-->
											</div>
												<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label fw-bold fs-6"> Notes</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-9 fv-row fv-plugins-icon-container">
															<%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>
															<asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="200" Columns="100" ></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->
											<!--begin::Input group-->
											<div class="row mb-6" style="display:none;visibility:hidden">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Is Active </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:CheckBox runat="server" ID="ckbIsActive" CssClass="BigCheckBox" Checked="true"></asp:CheckBox>
												<div class="fv-plugins-message-container invalid-feedback"></div>
												</div>
												<!--end::Col-->
											</div>

											   <div class="row mb-6 d-flex justify-content-end"" >
												   <div class="col-sm-3 d-flex justify-content-end">

                                 <asp:Button ID="Button1" runat="server"  Text="Add Speaker" ToolTip="Add Speaker" OnClick="btnUpload_Click" Style="margin-right: 20px;" />   <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                           
</div>
                                   
                                    </div>

											<asp:GridView runat="server" ID="BannerList">

												<Columns>
													<asp:TemplateField>
														<ItemTemplate>
															  <input id="Button1" type="button" value="Edit" onclick="EditSpeakerDetails('<%# Eval("Speaker_ID") %>');" class="btn btn-gray" > </input >
                                                                                    
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															 <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("Speaker_Photo").ToString()) %>' Width="100px" Height="100px" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Speaker" ItemStyle-Width="25%">
														<ItemTemplate>
															<asp:Label ID="lblSpeaker" runat="server" Text=' <%# Eval("Speaker_Name") %> '></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Details" ItemStyle-Width="25%">
														<ItemTemplate>
															<asp:Label ID="lblBio" runat="server" Text=' <%# Eval("Speaker_Bio") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Linked In">
														<ItemTemplate>
															<asp:Label ID="Label3" runat="server" Text= <%# Eval("LinkedIn") %>></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:Label ID="Label4" runat="server" Text=""></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													
												</Columns>
											</asp:GridView>

										
											     
                                            <asp:HiddenField ID="HiddenSpeakerID" runat="server" />


                                            <asp:Button ID="btnEditSpeaker" runat="server" Text="Button"  OnClick="EditSpeakerinList"  style="display: none"    />
										</div>

										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>
											  <%--<div class ="col-lg-1"></div>--%>
										</div>
										<!--end::Actions-->
									<input type="hidden"> 


									</form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

	<style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#F5F8FA; padding:40px; padding-bottom:4px;  }

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


	
	  <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display:none;"
                       Width="450px" Height="190px"   ScrollBars="None">

		   <div class="card card-bordered">
    <div class="card-header">
        <h3 class="card-title"><asp:Label ID="lblModelHeading" runat="server" Text="Add Category"></asp:Label> </h3>
        <div class="card-toolbar">
           <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
        </div>
    </div>
    <div class="card-body">
         <div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6"> <asp:Literal ID="lblsubtitle" runat="server" Text="Category"></asp:Literal>  </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-6 fv-row fv-plugins-icon-container">
													<asp:TextBox ID="txtAddReligion" runat="server"></asp:TextBox>
												</div>
												<div class="col-lg-4">
													<%--<asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="btnAddDenimination_Click" />--%>
													</div>
												<!--end::Col-->
											</div>
    </div>
    <div class="card-footer d-flex justify-content-end py-6 px-9">
       	<asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" style="margin-right:10px" />
				<asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />
    </div>
</div>
     
           </asp:Panel>
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
	
	<%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css"/>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
<script type="text/javascript">

   


    $(".input_date_new").flatpickr({
        //enableTime: true,
		dateFormat: "m/d/Y"
		
    });
</script>--%>
	<ajaxToolkit:ModalPopupExtender ID="mdlAddSpeaker" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnSpeakerAddOption" PopupControlID="pnlAddSpeaker" CancelControlID="btnSpekerClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnSpeakerAddOption" runat="server"></asp:Label>
    <asp:Label ID="btnSpekerClose" runat="server"></asp:Label>

	<asp:Panel ID="pnlAddSpeaker" runat="server" BackColor="White" Style="display: none;"
        Width="1000px"  Height="800px" ScrollBars="Both">                                                                        
                                                                                                   
        <div class="card card-bordered">
            <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="Label1" runat="server" Text="Add Speaker"></asp:Label>
                </h3>
                <div class="card-toolbar">
                    <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
                </div>
            </div>
           


               





            <!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
                                                <label class="col-lg-3 col-form-label fw-bold fs-6"    > Speaker's Image</label>
												
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-200px h-200px" style="background-image: url(assets/media/avatars/150-26.jpg)">
                                                            <asp:Image ID="ImagePageBackground" runat="server" Height="200px" Width="200px" /></div>
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
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->




               <div class="row mb-6">
                <!--begin::Label-->
             <label class="col-lg-3 col-form-label required fw-bold fs-6"> LinkedIn :</label>
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
              

                 <label class="col-lg-3 col-form-label required fw-bold fs-6">  Speaker's Details( Bio ) :             </label>
                <!--end::Label-->
                <!--begin::Col-->
                <div class="col-lg-8">
                    <div class="row">
                        <!--begin::Col-->
                        <div class="col-lg-13 fv-row fv-plugins-icon-container">
                             <CKEditor:CKEditorControl ID="CKEditorSpeakerDetails" BasePath="~/Scripts/ckeditor/" runat="server"  Height="80px"  Width="100%"  ></CKEditor:CKEditorControl>
                            <div class="fv-plugins-message-container invalid-feedback"></div>
                        </div>
                        <!--end::Col-->

                    </div>
                </div>
                <!--end::Col-->
            </div>




          


            



           

            <div class="card-footer d-flex justify-content-end py-6 px-9">
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-light" Text="Close" />

                

                <asp:Button runat="server" ID="UploadBanner" Text="Submit"  OnClick="UploadBanner_Click"  />

               <%-- <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />--%>
            </div>
        </div>






    </asp:Panel>


	


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








    <script src='https://kit.fontawesome.com/a076d05399.js' crossorigin='anonymous'></script>



	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>

	<style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#F5F8FA; padding:40px; padding-bottom:4px;  }

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




	<script>




        function EditSpeakerDetails(value) {



            document.getElementById('<%=HiddenSpeakerID.ClientID%>').value = value;

            var data = document.getElementById('<%=HiddenSpeakerID.ClientID%>').value;



            document.getElementById('<%=btnEditSpeaker.ClientID%>').click();



           


        }








        function SelectFileForBackGround() {

            //divPageBackground






            var labelDetails = document.getElementById('<%=lblModelHeading.ClientID%>').textContent;


            if (labelDetails == "Add Speaker") {
                  document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();

            }
            else {




                var imgSrc = document.getElementById("<%=ImagePageBackground.ClientID %>").src;

               

                $("#<%= ImagePageBackground.ClientID %>").attr("src","Empty" );
                
               <%-- document.getElementById('<%=RemoveImageEdit.ClientID%>').click();--%>


                document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();

                setTimeout(function () {
                   
                    
                   // RmoveImage();

                }, 2000);

                //alert("/your code to be executed after 10 second");

            }



           

            


              // <%--document.getElementById('<%=ImagePageBackground.ClientID%>').--%>

            //document.getElementById("divPageBackground").style.display = block;         ImagePageBackground   


        }
    </script>


</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
