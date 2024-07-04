<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AppHomeScreen.aspx.cs" Inherits="DeffinityAppDev.App.AppHomeScreen" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">

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

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Banner Details</h3>
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
                        <div class="col-lg-4">
                            <asp:Button ID="Button1" runat="server" SkinID="btnDefault" OnClick="BtnGetVideo_Click"  Text="Get Banners" />
                        </div>
                        <!--end::Col-->
                    </div>


					
                    <br />

                   
                   
					<br />
                    <br />




              







					
                    






                



                 




















                    <div class="row g-6">


                        <div class="card-footer d-flex justify-content-end py-6 px-9">


                                 <asp:Button ID="Button2" runat="server"  Text="Add New Banner" ToolTip="Add new Banner" OnClick="btnUpload_Click" Style="margin-right: 20px;" />   <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                           

                                   
                                    </div>


                        <div class="col-lg-2">
                            

                            </div>

                         

                        <div class="col-lg-6">

                             <%--<div class="card-footer d-flex justify-content-end py-6 px-9">


                                 <asp:Button ID="Button5" runat="server"  Text="Add New Banner" ToolTip="Add new Banner" OnClick="btnUpload_Click" Style="margin-right: 20px;" />   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           

                                   
                                    </div>--%>
                           
                            <div class="card card-stretch card-bordered mb-5">

                                <div class="card-header" >







                                   
    
    <div class="d-flex flex-column flex-row-fluid">
        <div class="d-flex flex-column-auto h-70px  flex-center">
            <span class="text-white"><h1 >   Banner Details  </h1></span>
        </div>

        
    </div>









                                    
                                    


                                </div>
                                <div class="card-body">
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

                                            <tr>
                                                <td colspan="2">  <h2>  <center>  Banner  <%# Eval("ID") %> Details    </center>  </h2>      </td>
                                            </tr>

                                            <tr>
                                                <td>&nbsp;    </td>
                                            </tr>

                                            <tr>
                                                <th>Banner  <%# Eval("ID") %></th>
                                                <td><%--<%# Eval("BannerID") %> --%>
                                                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("BannerID").ToString()) %>' Width="150px" Height="150px" />
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>&nbsp;    </td>
                                            </tr>

                                            <tr>
                                                <th>Link Url :</th>
                                                <td><%# Eval("LinkURL") %>   </td>
                                            </tr>

                                            <tr>
                                                <th>Transition Time :</th>
                                                <td><%# Eval("TransitionTime") %>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>



                                                <tr>
                                                    <td>&nbsp;    </td>
                                                </tr>


                                                <tr>

                                                    <td>



                                                        <asp:Button runat="server" ID="btnDeleteBannerInList" Text="Delete Banner" CommandName='<%# DeleteBanner(Eval("ID").ToString()) %>' OnClick="deleteInListView" />
                                                    </td>
                                                    <td>
                                                    &nbsp; &nbsp; &nbsp;     <asp:Button runat="server" ID="EditBannerInList" Text="Edit Banner" OnClick="EditBannerInList"  CommandName='<%# DeleteBanner(Eval("ID").ToString()) %>'   />
                                                         

                                                    </td>


                                                </tr>


                                                <tr>
                                                    <td    colspan="2"    ><div class="card-footer">
                                        
                                                   </div>   </td>

                                                    
                                                </tr>
                                               
                                               
                                               
                                        </ItemTemplate>


                                    </asp:ListView>

                                    <%-- <div class="card-footer d-flex justify-content-end py-6 px-9">

                                        <asp:Button ID="ButtonAddBanner2" runat="server"  Text="Add New Banner" ToolTip="Add new Banner" OnClick="btnUpload_Click" Style="margin-right: 20px;" />
                                    </div>--%>
                                     
                                </div>
                               
                            </div>



                          


                        </div>


                         <div class="card-footer d-flex justify-content-end py-6 px-9">

                                       <%-- CssClass="btn btn-bg-light"--%>

                                        
                                        <asp:Button ID="Button4" runat="server"  Text="Add New Banner" ToolTip="Add new Banner" OnClick="btnUpload_Click" Style="margin-right: 20px;" />
                                    </div>

                    </div>










                </div>
                <!--end::Card body-->
                <!--begin::Actions-->
                <div class=" d-flex  py-6 px-9">
                    <%--	<button type="reset" class="btn btn-light btn-active-light-primary me-2">Discard</button>
											<button type="submit" class="btn btn-primary" id="kt_account_profile_details_submit">Save Changes</button>--%>

                    <%--<asp:Button ID="btnSaveChanges" runat="server" SkinID="btnSave" OnClick="btnSaveChanges_Click" Text="Save Changes" />--%>
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
        Width="800px"  Height="750px" ScrollBars="Both">                                                                        
                                                                                                   
        <div class="card card-bordered">
            <div class="card-header">
                <h3 class="card-title">
                    <asp:Label ID="lblModelHeading" runat="server" Text="Add New Banner"></asp:Label>
                </h3>
                <div class="card-toolbar">
                    <%-- <button type="button" class="btn btn-sm btn-light">
                Close
            </button>--%>
                </div>
            </div>
           


                <div class="row mb-6">
                        <!--begin::Label-->
                        <label class="col-lg-3 col-form-label fw-bold fs-6"    > Banner  Image</label>
                        <!--end::Label-->
                        <!--begin::Col-->
                        <div class="col-lg-8">
                            <!--begin::Image input-->
                            <div class="image-input image-input-outline" data-kt-image-input="true" style="background-image: url(assets/media/avatars/blank.png)">
                                <!--begin::Preview existing avatar-->
                                <div class="image-input-wrapper w-200px h-200px" style="background-image: url(assets/media/avatars/150-26.jpg)">

                                    <asp:Image ID="ImagePageBackground" runat="server"       Width="200" Height="200" />          
                                </div>
                                <!--end::Preview existing avatar-->
                                <!--begin::Label-->
                                <label class="btn btn-icon btn-circle btn-active-color-primary w-25px h-25px bg-body shadow" data-kt-image-input-action="change" data-bs-toggle="tooltip" title="" data-bs-original-title="Upload  Banner" style="display: none">
                                    <i class="bi bi-pencil-fill fs-7"></i>
                                    <!--begin::Inputs-->
                                    <%--<input type="file" name="avatar" accept=".png, .jpg, .jpeg" runat="server" id="imgLogo">--%>
                                    <asp:FileUpload runat="server" ID="FileUploadPageBackground"   />

                                    <asp:Button runat="server" Text="Button" id="RemoveImageEdit"  OnClick="RemoveImageEdit_Click"   ></asp:Button>

                                    <!--end::Inputs-->
                                </label>
                                <!--end::Label-->

                            </div>
                            <!--end::Image input-->
                            <!--begin::Hint-->
                            <div class="form-text">Allowed file types: png, jpg, jpeg.

                                <asp:Label runat="server" ID="lblFilenotSelected" ForeColor="Red"> </asp:Label>
                            </div>
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


                    <div        >

               <label class="col-lg-2 col-form-label fw-bold fs-6"></label>
                        <button class="btn btn-primary" type="button" title="Upload Image" onclick="SelectFileForBackGround()"    >Upload Image </button>

                       
                        <asp:Button runat="server" ID="btnRemoveImage" Text="Remove Image"   ToolTip="Remove Image" OnClick="RemoveImage_Click" />

                    </div>    

                    

                    </div>



           




             <div class="card-body">
               <div class="row mb-6">
												<%--<label class="col-lg-2 col-form-label required fw-bold fs-6">Campaign Owner</label>--%>
												<!--begin::Col-->
												<div class="col-lg-14">
													<div class="row">
														<!--begin::Col-->
														
														<div class="col-lg-9 fv-row fv-plugins-icon-container">

                                                            <label class="col-lg-4 col-form-label required fw-bold fs-6"> Link Url :</label>
															
															<asp:TextBox ID="txtLinkUrl"     placeholder="Link URL" runat="server"></asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>
														<!--end::Col-->
														

														
														<div class="col-lg-10   fv-row fv-plugins-icon-container">                  

                                                            <label class=" col-form-label required fw-bold fs-6">Transition Time in Seconds :</label>
															
															<asp:TextBox ID="txtTransitionTime"  TextMode="Number"    placeholder="Transition Time in Seconds" runat="server">        </asp:TextBox>
														<div class="fv-plugins-message-container invalid-feedback"></div>

														</div>
														
														
													</div>
												</div>
												<!--end::Col-->
											</div>
            </div>



           

            <div class="card-footer d-flex justify-content-end py-6 px-9">
                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-light" Text="Close" />

                

                <asp:Button runat="server" ID="UploadBanner" Text="Upload Banner" OnClick="UploadBanner_Click" />

               <%-- <asp:Button ID="btnSaveRegion" runat="server" SkinID="btnDefault" Text="Save Changes" OnClick="btnSaveChangesPop_Click" />--%>
            </div>
        </div>






    </asp:Panel>


    









     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

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

                document.getElementById("<%=HiddenFieldReligion.ClientID %>").value = -1;
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

                document.getElementById("<%=HiddenFieldGroup.ClientID %>").value = -1;
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









    <script type="text/javascript">

       <%-- function SelectFile() {


            document.getElementById('<%=FileUploadBanner1.ClientID%>').click();
        }--%>




        function SelectFileForBackGround() {

            //divPageBackground


           



            var labelDetails = document.getElementById('<%=lblModelHeading.ClientID%>').textContent;


            if (labelDetails == "Add New Banner") {
                document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();

            }
            else
            {


              

                var imgSrc = document.getElementById("<%=ImagePageBackground.ClientID %>").src;

                alert("      imgSrc        " + imgSrc);

               // document.getElementById('<%=ImagePageBackground.ClientID%>').attr("src", "");

                $("#<%= ImagePageBackground.ClientID %>").attr("src","Empty" );
                
             //   document.getElementById('<%=RemoveImageEdit.ClientID%>').click();


                document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();

                setTimeout(function () {
                   
                    
                   // RmoveImage();

                }, 2000);

                //alert("/your code to be executed after 10 second");

            }





            


           // <%--document.getElementById('<%=ImagePageBackground.ClientID%>').--%>

            //document.getElementById("divPageBackground").style.display = block;         ImagePageBackground   


        }




        


        function RmoveImage() {

            //divPageBackground


            document.getElementById('<%=FileUploadPageBackground.ClientID%>').click();

            //document.getElementById("divPageBackground").style.display = block;         ImagePageBackground


        }





    </script>














</asp:Content>