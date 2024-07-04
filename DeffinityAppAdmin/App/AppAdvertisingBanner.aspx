<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AppAdvertisingBanner.aspx.cs" Inherits="DeffinityAppDev.App.AppAdvertisingBanner" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

    <style>
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
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Denomination</h3>
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

                    <!--end::Input group-->
                    <!--begin::Input group-->
                 





					  <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-3 col-form-label required fw-bold fs-6">Religion</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-4">
                           <%-- <asp:DropDownList ID="ddlReligion" runat="server" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged"></asp:DropDownList>--%>


                            <select id="continents"     ></select>


                            <asp:HiddenField ID="HiddenFieldReligion" runat="server" Value="0"  />



                        </div>


                        <div class="col-lg-4">
                            <%--<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="btn btn-light btn-active-light-primary me-2" Text="Add" />--%>
                        </div>
                        <!--end::Col-->
                    </div>
                    <!--end::Input group-->
                    <!--begin::Input group-->
                    <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-3 col-form-label required fw-bold fs-6">Group</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                           <%-- <asp:DropDownList ID="ddlDenimination" runat="server"></asp:DropDownList>--%>


                            <select id="countries"></select>

                            <asp:HiddenField ID="HiddenFieldGroup" runat="server" Value="0" />


                        </div>
                        
                        <!--end::Col-->
                    </div>






                      <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-3 col-form-label required fw-bold fs-6">Denomination</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                            <%--<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>--%>


                            <select id="cities"></select>


                            <asp:HiddenField ID="HiddenFieldDenomination" runat="server" Value="0" />


                        </div>
                        <%--<div class="col-lg-4">
                            <asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="BtnGetVideo_Click"  Text="Get Banners" />
                        </div>--%>
                        <!--end::Col-->
                    </div>


                   





                    <div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-7 fv-row fv-plugins-icon-container">

															<label class=" col-form-label required fw-bold fs-6">Page Title</label>
															<asp:TextBox ID="txtPageTitle"     placeholder="Faith Union Card" runat="server"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div>

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
												<label class="col-lg-3 col-form-label fw-bold fs-6"> Banner 1</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-250px h-250px" style="background-image: url(assets/media/avatars/150-26.jpg)">
															
															<asp:Image ID="ImageFileUploadBanner" runat="server" /></div>
														<!--end::Preview existing avatar-->
														<!--begin::Label-->
														<label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner"  style="display:none"  >
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="FileUploadBanner1" />
															
															<!--end::Inputs-->
														</label>
														<!--end::Label-->
														<!--begin::Cancel-->
														<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="cancel" data-bs-toggle="tooltip" title="" data-bs-original-title="Cancel Banner">
															<i class="bi bi-x fs-2"></i>
														</span>
														<!--end::Cancel-->
														<!--begin::Remove-->
														<span class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="remove" data-bs-toggle="tooltip" title="" data-bs-original-title="Remove Banner">
															<i class="bi bi-x fs-2"></i>
														</span>
														<!--end::Remove-->
													</div>
													<!--end::Image input-->
													<!--begin::Hint-->
													<div class="form-text">Allowed file types: png, jpg, jpeg.</div>
													<!--end::Hint-->
												</div>
												<!--end::Col-->




						 	<button class="btn btn-icon btn-circle btn-active-color-primary w-125px h-125px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner"  style="display:none"  >
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="FileUpload3" />
															
															<!--end::Inputs-->
														</button>


						                  

						 


											</div>

                    <br />
                    <br />


                    
                    <div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-7 fv-row fv-plugins-icon-container">

                                                            
															
															<asp:TextBox ID="txtLinkUrl"     placeholder="Link URL" runat="server"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>
														<!--end::Col-->
														
														
													</div>
												</div>
												<!--end::Col-->
											</div>









                     <div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-5 fv-row fv-plugins-icon-container">

															<button  class="btn btn-primary" type="button"  title="Upload Image" onclick="SelectFile()"  >  Upload Image </button>

                                                           

                                                            <asp:Button runat="server" ID="btnDelete" Text="Delete" />
															
															

														</div>
														<!--end::Col-->
														
														
													</div>
												</div>
												<!--end::Col-->
											</div>




                    <br />
                    <br />
                    <br />
                  



                    			<!--begin::Input group-->
											<div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-1 col-form-label  fw-bold fs-6"> </label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8">
													<div class="row">
														<!--begin::Col-->
														<div class="col-lg-12 fv-row fv-plugins-icon-container">
													
															<%--<textarea id="txtDescriptionArea" rows="4"  style="background-color:#F5F8FA;border:none;  " cols="100"></textarea>--%>

															

															  <CKEditor:CKEditorControl ID="CKEditorTextArea" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="500px" ClientIDMode="Static"></CKEditor:CKEditorControl>
															
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
														
														<div class="col-lg-5 fv-row fv-plugins-icon-container">

															<label class=" col-form-label  fw-bold fs-6">Button 1</label>
															<asp:TextBox ID="txtBtnText1"     placeholder="Button 1 Text: Read More" runat="server"></asp:TextBox>
															
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>


													


														<!--end::Col-->
														
														
													</div>

													<br />

													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-8 fv-row fv-plugins-icon-container">

															
															<asp:TextBox ID="txtBtnUrl1"     placeholder="Button 1 URL" runat="server"></asp:TextBox>
															
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>


													


														<!--end::Col-->
														
														
													</div>
												</div>
												<!--end::Col-->
											</div>









					 <div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-5 fv-row fv-plugins-icon-container">

															<label class=" col-form-label  fw-bold fs-6">Button 2</label>
															<asp:TextBox ID="txtBtnText2"     placeholder="Button 2 Text: Apply Now" runat="server"></asp:TextBox>
															
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>

												
														<!--end::Col-->
														
														
													</div>

													<br />

													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-8 fv-row fv-plugins-icon-container">

															
															<asp:TextBox ID="txtBtnUrl2"     placeholder="Button 2 URL" runat="server"></asp:TextBox>
															
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>


													


														<!--end::Col-->
														
														
													</div>
												</div>
												<!--end::Col-->
											</div>




					<br /><br />



					 <div class="row mb-6">
												<!--begin::Label-->
												<label class="col-lg-3 col-form-label fw-bold fs-6"> Page Background</label>
												<!--end::Label-->
												<!--begin::Col-->
												<div class="col-lg-8"     >
													<!--begin::Image input-->
													<div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
														<!--begin::Preview existing avatar-->
														<div class="image-input-wrapper w-250px h-250px" style="background-image: url(assets/media/avatars/150-26.jpg)">
															
															<asp:Image ID="ImagePageBackground" runat="server" /></div>
														<!--end::Preview existing avatar-->
														<!--begin::Label-->
														<label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner"  style="display:none"  >
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="FileUploadPageBackground" />
															
															<!--end::Inputs-->
														</label>
														<!--end::Label-->
														
													</div>
													<!--end::Image input-->
													<!--begin::Hint-->
													<div class="form-text">Allowed file types: png, jpg, jpeg.</div>
													<!--end::Hint-->
												</div>
												<!--end::Col-->




						 	<button class="btn btn-icon btn-circle btn-active-color-primary w-125px h-125px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner"  style="display:none"  >
															<i class="bi bi-pencil-fill fs-7"></i>
															<!--begin::Inputs-->
															<%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
															<asp:FileUpload runat="server" id="FileUpload4" />
															
															<!--end::Inputs-->
														</button>


						                  

						 


											</div>




                    


					 <div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-5 fv-row fv-plugins-icon-container">

															<button  class="btn btn-primary" type="button"  title="Upload Image" onclick="SelectFileForBackGround()"  >  Upload Image </button>

                                                           

                                                            <asp:Button runat="server" ID="btnDeletePageBackground" Text="Delete" />
															
															

														</div>
														<!--end::Col-->
														
														
													</div>
												</div>
												<!--end::Col-->
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


    <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
        TargetControlID="btnAddOptions" PopupControlID="pnlAddReligion" CancelControlID="btnClose">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
    <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
    <asp:Panel ID="pnlAddReligion" runat="server" BackColor="White" Style="display: none;"
        Width="450px" Height="190px" ScrollBars="None">

        <div class="card card-bordered">
            <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Add Religion"></asp:Label>
                </h3>
                <div class="card-toolbar">
                    <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
                </div>
            </div>
            <div class="card-body">
                <div class="row mb-6">
                    <!--begin::Label-->
                    <label class="col-lg-4 col-form-label required fw-bold fs-6">Religion</label>
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
            <div class="card-footer d-flex justify-content-end py-6 px-9"      >
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" />
               <%-- <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />--%>
            </div>
        </div>

    </asp:Panel>


    <%--	<asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
 
<!-- ModalPopupExtender -->
<ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    This is an ASP.Net AJAX ModalPopupExtender Example<br />
    <asp:Button ID="Button1" runat="server" Text="Close" />
</asp:Panel>--%>



	<script type="text/javascript">

		function SelectFile() {

			
			document.getElementById('<%=FileUploadBanner1.ClientID%>').click();
		}


        

		function SelectFileForBackGround() {

            //divPageBackground

           
			document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();

            //document.getElementById("divPageBackground").style.display = block;


        }


    </script>







	<script type="text/javascript">


        //  FirstFunction();


        function MyFunction(religionId, groupId, Denominationid) {




            $(document).ready(function () {



                var continentsDDL = $('#continents');
                var countriesDDL = $('#countries');
                var citiesDDL = $('#cities');





                $.ajax({
                    url: 'WebService/DataServices.asmx/GetContinents',
                    method: 'post',
                    dataType: 'json',
                    success: function (data) {
                        continentsDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        citiesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        countriesDDL.prop('disabled', true);
                        citiesDDL.prop('disabled', true);




                        $(data).each(function (index, item) {
                            continentsDDL.append($('<option/>', { value: item.Id, text: item.Name }));



                        });




                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = religionId;


                        continentsDDL.val(religionId);
                        continentsDDL.prop('disabled', false);

                    },
                    error: function (err) {
                        alert(err);
                    }
                });



                $.ajax({
                    url: 'WebService/DataServices.asmx/GetCountriesByContinentId',
                    method: 'post',
                    dataType: 'json',
                    data: { ContinentId: religionId },
                    success: function (data) {
                        countriesDDL.empty();
                        countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        $(data).each(function (index, item) {
                            countriesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                        });

                       <%-- document.getElementById("countries").value = groupId;--%>


                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = religionId;

                        document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = groupId;

                        countriesDDL.val(groupId);
                        countriesDDL.prop('disabled', false);





                    },
                    error: function (err) {
                        alert(err);
                    }
                });



                $.ajax({
                    url: 'WebService/DataServices.asmx/GetCitiesByCountryId',
                    method: 'post',
                    dataType: 'json',
                    data: { GroupId: groupId },
                    success: function (data) {


                        citiesDDL.empty();
                        citiesDDL.append($('<option/>', { value: -1, text: 'Please select... ' }));
                        $(data).each(function (index, item) {
                            debugger;


                            citiesDDL.append($('<option/>', { value: item.Id, text: item.Name }));




                        });

                        //document.getElementById("cities").value = Denominationid;



                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = religionId;

                        document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = groupId;

                        document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = Denominationid;




                        citiesDDL.val(Denominationid);
                        citiesDDL.prop('disabled', false);
                    },
                    error: function (err) {
                        alert(err);
                    }
                });







            });



        }












        $(document).ready(function () {


            var continentsDDL = $('#continents');
            var countriesDDL = $('#countries');
            var citiesDDL = $('#cities');






            continentsDDL.change(function () {


                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = -1;

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = -1;


                if ($(this).val() == "-1") {
                    countriesDDL.empty();
                    citiesDDL.empty();
                    countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                    citiesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                    countriesDDL.val('-1');
                    citiesDDL.val('-1');
                    countriesDDL.prop('disabled', true);
                    citiesDDL.prop('disabled', true);
                }
                else {
                    citiesDDL.val('-1');
                    citiesDDL.prop('disabled', true);

                    var value1 = $('#continents').val();

                    var element = document.getElementById("continents").value;

                    document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = value1;





                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCountriesByContinentId',
                        method: 'post',
                        dataType: 'json',
                        data: { ContinentId: value1 },
                        success: function (data) {
                            countriesDDL.empty();
                            countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                            $(data).each(function (index, item) {
                                countriesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            countriesDDL.val('-1');
                            countriesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

            countriesDDL.change(function () {

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = -1;


                if ($(this).val() == "-1") {
                    citiesDDL.empty();
                    citiesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                    citiesDDL.val('-1');
                    citiesDDL.prop('disabled', true);
                }
                else {


                    var value = $('#countries').val();

                    document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = value;




                         document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                   

                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCitiesByCountryId',
                        method: 'post',
                        dataType: 'json',
                        data: { GroupId: value },
                        success: function (data) {
                            citiesDDL.empty();
                            citiesDDL.append($('<option/>', { value: -1, text: 'Please select... ' }));
                            $(data).each(function (index, item) {
                                debugger;
                                citiesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            citiesDDL.val('-1');
                            citiesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });



            citiesDDL.change(function () {

               

                document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = $('#countries').val();

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = $('#cities').val();


            });


       });
    







     








        function FirstFunction() {

           

            $(document).ready(function () {
               

                var continentsDDL = $('#continents');
                var countriesDDL = $('#countries');
                var citiesDDL = $('#cities');




                $.ajax({
                    url: 'WebService/DataServices.asmx/GetContinents',
                    method: 'post',
                    dataType: 'json',
                    success: function (data) {
                        continentsDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        citiesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        countriesDDL.prop('disabled', true);
                        citiesDDL.prop('disabled', true);

                        $(data).each(function (index, item) {
                            continentsDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                        });
                    },
                    error: function (err) {
                        alert(err);
                    }
                });

                continentsDDL.change(function () {



                    if ($(this).val() == "-1") {
                        countriesDDL.empty();
                        citiesDDL.empty();
                        countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        citiesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                        countriesDDL.val('-1');
                        citiesDDL.val('-1');
                        countriesDDL.prop('disabled', true);
                        citiesDDL.prop('disabled', true);
                    }
                    else {
                        citiesDDL.val('-1');
                        citiesDDL.prop('disabled', true);

                        var value1 = $('#continents').val();

                        var element = document.getElementById("continents").value;

                        document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = value1;


                   


                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCountriesByContinentId',
                        method: 'post',
                        dataType: 'json',
                        data: { ContinentId: value1 },
                        success: function (data) {
                            countriesDDL.empty();
                            countriesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                            $(data).each(function (index, item) {
                                countriesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            countriesDDL.val('-1');
                            countriesDDL.prop('disabled', false);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });

                 countriesDDL.change(function () {
                     if ($(this).val() == "-1") {
                         citiesDDL.empty();
                         citiesDDL.append($('<option/>', { value: -1, text: 'Please select...' }));
                         citiesDDL.val('-1');
                         citiesDDL.prop('disabled', true);
                     }
                     else {


                         var value = $('#countries').val();

                         document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = value;




                    document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                   

                    $.ajax({
                        url: 'WebService/DataServices.asmx/GetCitiesByCountryId',
                        method: 'post',
                        dataType: 'json',
                        data: { GroupId: value },
                        success: function (data) {
                            citiesDDL.empty();
                            citiesDDL.append($('<option/>', { value: -1, text: 'Please select... ' }));
                            $(data).each(function (index, item) {
                                debugger;
                                citiesDDL.append($('<option/>', { value: item.Id, text: item.Name }));
                            });
                            citiesDDL.val('-1');
                            citiesDDL.prop('disabled', false); 
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                }
            });



            citiesDDL.change(function () {

               

                document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = $('#continents').val();

                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = $('#countries').val();

                document.getElementById("<%=HiddenFieldDenomination.ClientID %>").value = $('#cities').val();


            });


            });

        }




    </script>







</asp:Content>
