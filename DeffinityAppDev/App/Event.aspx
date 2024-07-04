<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="DeffinityAppDev.App.Event" %>



<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
								<!--begin::Card header-->
								<div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Event </h3>
									</div>
									 <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="" data-bs-original-title="Click to add a user">


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









                                             <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-4 col-form-label required fw-bold fs-6"> Detailed Description</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-12 fv-row fv-plugins-icon-container">

                                    <%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

                                     <CKEditor:CKEditorControl ID="CKEditorDetailedDescription" BasePath="~/Scripts/ckeditor/" runat="server" ></CKEditor:CKEditorControl>

                                    

                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>



											
														







											<!--begin::Input group-->
											<div class="row mb-6" style="display:none;visibility:hidden">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Organisation Name</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-6 fv-row fv-plugins-icon-container">
															 <asp:DropDownList ID="ddlOrganisation" runat="server" ><asp:ListItem Text="Organisation Name" Value="0" ></asp:ListItem> </asp:DropDownList>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->


											
											

											<br />

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
															 <asp:DropDownList AutoPostBack="true" ID="ddlActiviteCategory"  OnTextChanged="ddlActiviteCategory_TextChanged1"  runat="server" ><asp:ListItem Text="Event   Category" Value="0"  ></asp:ListItem> </asp:DropDownList>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->


											<br />

										

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
															 <asp:DropDownList ID="ddlSubCategory" runat="server" ><asp:ListItem Text="Event Sub  Category" Value="0" ></asp:ListItem> </asp:DropDownList>
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
													</div>
												</div>
												<!--end::Col-->
											</div>
											<!--end::Input group-->



										
											<div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														<label class="col-lg-4 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">

															<label class=" col-form-label required fw-bold fs-6">Event Start Date :</label>
															<asp:TextBox ID="txtStartDate" Text=""  SkinID="DateNew"  runat="server"></asp:TextBox>

															
														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
														
														<!--begin::Col-->
														<label class="col-lg-2 col-form-label  fw-bold fs-6"></label>
														<div class="col-lg-3 fv-row fv-plugins-icon-container">
															<label class=" col-form-label required fw-bold fs-6">Event End Date :</label>
															
															<asp:TextBox  ID="TextEndDate"    Text="" SkinID="DateNew" runat="server"></asp:TextBox>

														<div class="fv-plugins-message-container invalid-feedback"></div></div>
														<!--end::Col-->
													</div>
												</div>
												<!--end::Col-->
											</div>



											
											<!--begin::Input group-->


											<br />

											<br />
											
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-4 col-form-label required fw-bold fs-6">Is Active </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-4 fv-row fv-plugins-icon-container">
													<asp:CheckBox runat="server" ID="ckbIsActive" CssClass="BigCheckBox"></asp:CheckBox>
													
												<div class="fv-plugins-message-container invalid-feedback"></div>

												</div>
												<!--end::Col-->
											</div>



                                               <div class="card-footer d-flex justify-content-end py-6 px-9">


                                 <asp:Button ID="Button1" runat="server"  Text="Add Speaker" ToolTip="Add Speaker" OnClick="btnUpload_Click" Style="margin-right: 20px;" />   <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                           

                                   
                                    </div>




											 <div class="d-flex align-items-center rounded py-5 px-5  bg-light-primary ">

											
                                                  <div class="card-body">


                                                        <div class="form-group row mb-5">
                                                                               
                                                                                <div class="col-md-2">
                                                                                    <label class="form-label"> </label><br />
                                                                                 
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <label class="form-label">Speaker Name  </label><br />
                                                                                     
                                                                                   
                                                                                </div>

                                                                                 <div class="col-md-2">
                                                                                    <label class="form-label">Speaker Details  </label><br />
                                                                                   
                                                                                </div>


                                                                                 <div class="col-md-2">
                                                                                    <label class="form-label">LinkedIn</label><br />
                                                                                   
                                                                                     
                                                                                   
                                                                                </div>

                                                                                       
                                                                               <div class="col-md-2">
                                                                                    <label class="form-label">  </label><br />
                                                                                  
                                                                                </div>
                                                                                        
                                                                                



                                                                            </div>



                                    <asp:ListView runat="server" ID="BannerList" GroupPlaceholderID="groupplaceholder" ItemPlaceholderID="itemplaceholder">

                                        <LayoutTemplate>
                                            <table>



                                                <tr id="groupplaceholder" runat="server"></tr>

                                            </table>
                                        </LayoutTemplate>

                                        <GroupTemplate>
                                            <tr>
                                                <tr id="itemplaceholder" runat="server"></tr>
                                            </tr>
                                        </GroupTemplate>


                                        <ItemTemplate>



											  <div class="form-group row mb-5">
                                                                               
                                                                                <div class="col-md-2">
                                                                                    
                                                                                     <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("Speaker_ID").ToString()) %>' Width="100px" Height="100px" />
                                                                                  <%-- <%# Eval("LinkedIn") %>--%>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                 
                                                                                     <%# Eval("Speaker_Name") %> 
                                                                                   
                                                                                </div>

                                                                                 <div class="col-md-2">
                                                                                    
                                                                                    <%# Eval("Speaker_Bio") %>
                                                                                </div>


                                                                                 <div class="col-md-2">
                                                                                   
                                                                                   
                                                                                      <%# Eval("LinkedIn") %>
                                                                                   
                                                                                </div>


                                                                                      <div class="col-md-2">
                                                                                   
                                                                                    
                                                                                    <input id="Button1" type="button" value="Edit" onclick="EditSpeakerDetails('<%# Eval("Speaker_ID") %>');" class="bi bi-pencil-fill fs-7" style="width: 70px; height: 30px; color: blue">  </input >
                                                                                    
                                                                                    
                                                                                </div>


                                                                               <%-- <div class="col-md-1">
                                                                                    <a href="javascript:;" data-repeater-delete class="btn btn-sm btn-light-danger mt-3 mt-md-9">
                                                                                        <i class="la la-trash-o fs-3"></i>
                                                                                    </a>
                                                                                </div>--%>
                                                                            </div>





                                               
                                               
                                               
                                        </ItemTemplate>


                                    </asp:ListView>

                                     
                                </div>     


												 </div>










                                            
                                            <asp:HiddenField ID="HiddenSpeakerID" runat="server" />


                                            <asp:Button ID="btnEditSpeaker" runat="server" Text="Button"  OnClick="EditSpeakerinList"  style="display: none"    />

                                        









										
										</div>





										<!--end::Card body-->
										<!--begin::Actions-->
										<div class="card-footer d-flex justify-content-end py-6 px-9">
											<%--<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

											<asp:Button ID="btnSaveAndEdit" runat="server" SkinID="btnDefault"  Text="Save Changes" OnClick="btnSaveAndEdit_Click"  />  
											

										</div>
										<!--end::Actions-->
									<input type="hidden"><div></div></form>
									<!--end::Form-->
								</div>
								<!--end::Content-->
							</div>





	
















	 <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
    <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>



    <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display: none;"
        Width="1000px"  Height="800px" ScrollBars="Both">                                                                        
                                                                                                   
        <div class="card card-bordered">
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
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" />

                

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

