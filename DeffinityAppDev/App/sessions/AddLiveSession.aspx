<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddLiveSession.aspx.cs" Inherits="DeffinityAppDev.App.AddLiveSession" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

    <style >
        .modalBackground {
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

 .BigCheckBox input {width:30px; height:30px;}




    select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }



    </style>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Add Live Session</h3>
            </div>
            <div class="card-toolbar">
                <asp:HyperLink ID="backButton" runat="server" Text="View All Sessions" CssClass="btn btn-light" NavigateUrl="~/App/sessions/LiveSessions.aspx"></asp:HyperLink>
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
                        <label class="col-lg-2 col-form-label required fw-bold fs-6">Live Session Title </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtLiveSessionTitle" runat="server"   placeholder="Live Session Title"  ></asp:TextBox>
                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                    <!--begin::Input group-->
                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label  fw-bold fs-6">  Description  </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-12 fv-row fv-plugins-icon-container">

                                    <%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

                                    

                                    
                                     <asp:TextBox ID="TextAreaDescription" runat="server"    TextMode="MultiLine" Rows="10"  ></asp:TextBox>



                                    <%--<CKEditor:CKEditorControl ID="CKEditorTextArea" BasePath="~/Scripts/ckeditor/" runat="server"
                                        Height="200px" ClientIDMode="Static"></CKEditor:CKEditorControl>--%>

                                    
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>
                    <!--end::Input group-->

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6">Speaker(s) </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtSpeakers" runat="server"   placeholder="Speaker(s)"   ></asp:TextBox>
                                    
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6">Date Scheduled </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-4">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtDateScheduled" runat="server" SkinID="DateNew"></asp:TextBox>
                                  <%--<ajaxToolkit:CalendarExtender ID="txtOrderDate_CalendarExtender" runat="server" TargetControlID="TextBoxOnairDate"
                BehaviorID="calendar" Format="MM/dd/yyyy" OnClientDateSelectionChanged="DataSelect" />--%>
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>


                          <div class="col-lg-6">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-10 fv-row fv-plugins-icon-container">
                                    <label class="col-lg-3 col-form-label required fw-bold fs-6">  Enable Donation  </label>   
                                   <asp:CheckBox ID="CheckBoxEnableTithing" runat="server" CssClass="BigCheckBox" />
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>

                        <!--end::Col-->
                    </div>

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6"        >Zoom Link </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtZoomLink" runat="server"     placeholder="Zoom Link"     ></asp:TextBox>
                                    
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6"> Recorded Link </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtRecordedLink" runat="server"   placeholder="Recorded Link"></asp:TextBox>
                                   
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6"> Zoom User ID </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtZoomUserID" runat="server"   placeholder="Zoom User ID"      ></asp:TextBox>
                                   
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6"> Zoom API Key  </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtZoomAPIKey" runat="server"   placeholder="Zoom API Key"      ></asp:TextBox>
                                   
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>

                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label required fw-bold fs-6"> Zoom Secret Code  </label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <div class="row">
                                <!--begin::Col-->
                                <div class="col-lg-6 fv-row fv-plugins-icon-container">
                                    <asp:TextBox ID="txtZoomSecretCode" runat="server"   placeholder="Zoom Secret Code "      ></asp:TextBox>
                                   
                                </div>
                                <!--end::Col-->

                            </div>
                        </div>
                        <!--end::Col-->
                    </div>
                    <br />
                    <br />

                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-2 col-form-label fw-bold fs-6">Image</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                              <div class="row mb-6">
                        <%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
                        <!--begin::Col-->
                        <div class="col-lg-12">
                            <div class="row mb-6">
                                <!--begin::Col-->

                                <div class="col-lg-10 fv-row fv-plugins-icon-container">

                                    <button class="btn btn-light" type="button" title="Select Image" onclick="SelectFileForBackGround()">Select Image </button>



                                    <asp:Button runat="server" ID="btnDeletePageBackground" Text="Delete" Visible="false" />



                                </div>
                                <!--end::Col-->


                            </div>
                             <div class="row">
                                   <div class="col-lg-8">
                                  <asp:Image ID="ImagePageBackground" runat="server" CssClass="img-fluid" />
                                       </div>
                                 </div>
                        </div>
                        <!--end::Col-->
                    </div>
                            <!--begin::Image input-->
                            <div class="image-input image-input-outline" data-kt-image-input="true" style="display: none">
                                <!--begin::Preview existing avatar-->
                              
                                <!--end::Preview existing avatar-->
                                <!--begin::Label-->
                                <label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner" style="display: none">
                                    <i class="bi bi-pencil-fill fs-7"></i>
                                    <!--begin::Inputs-->
                                    <%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
                                    <asp:FileUpload runat="server" ID="FileUploadPageBackground"  />

                                    <!--end::Inputs-->
                                </label>
                                <!--end::Label-->

                            </div>
                            <!--end::Image input-->
                            <!--begin::Hint-->
                           <%-- <div class="form-text">Allowed file types: png, jpg, jpeg.</div>--%>
                            <!--end::Hint-->
                        </div>
                        <!--end::Col-->

                        <button class="btn btn-icon btn-circle btn-active-color-primary w-125px h-125px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner" style="display: none">
                            <i class="bi bi-pencil-fill fs-7"></i>
                            <!--begin::Inputs-->
                            <%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
                            <asp:FileUpload runat="server" ID="FileUpload4" />

                            <!--end::Inputs-->
                        </button>

                    </div>

                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class="card-footer d-flex justify-content-end py-6 px-9">
                    <%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

                    <asp:Button ID="btnSaveChanges" runat="server" SkinID="btnSave" OnClick="btnSaveChanges_Click" Text="Save Changes" />
                </div>
                <!--end::Actions-->
                <input type="hidden"><div></div>
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>


    <script type="text/javascript">

        function SelectFile() {
           <%-- document.getElementById('<%=FileUploadBanner1.ClientID%>').click();--%>
        }
        function SelectFileForBackGround() {
            document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();
        }

        function previewFile() {
            <%--var preview = document.querySelector('#<%=Avatar.ClientID %>');
             var file = document.querySelector('#<%=avatarUpload.ClientID %>').files[0];--%>
             var reader = new FileReader();

             reader.onloadend = function () {
                 preview.src = reader.result;
             }

             if (file) {
                 reader.readAsDataURL(file);
             } else {
                 preview.src = "";
             }
         }


    </script>






</asp:Content>
