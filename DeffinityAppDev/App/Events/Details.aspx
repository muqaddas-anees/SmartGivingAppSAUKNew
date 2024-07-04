<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="DeffinityAppDev.App.Events.Details" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/App/Events/controls/EventTabs.ascx" TagPrefix="Pref" TagName="EventTabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:EventTabs runat="server" id="EventTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    

 <link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
    

    <div class="container-xxl" id="kt_content_container">
        <!--begin::Row-->
        <div class="row gy-5 g-xl-8">
            <br />
            <br />
            <h1 class="d-flex align-items-center text-dark fw-bolder fs-3 my-1">Event Details</h1>
            <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />

            <!--end::Col-->
            <!--begin::Col-->
            <div class="col-xl-12">
                <!--begin::Mixed Widget 12-->




                <div class="card card-xl-stretch mb-5 mb-xl-8">
                    <!--begin::Header-->



                    <div id="kt_content_container" class="container-xxl">


                        <div id="MainEventImageId" >
                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-images text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Main event image</h3>
                                    </div>
                                    <!--end::Card title-->
                                </div>
                                <div class="card-body border-top p-9">
                                <div class="row mb-3">
                                          <div class="col-lg-10 d-flex d-inline gap-3 ">
                                            <asp:FileUpload runat="server" id="imgBanner" CssClass="form-control" Text="Upload" />


                                          <asp:Button ID="btnUpload" runat="server" SkinID="btnUpload" OnClick="btnUpload_Click" />
                                              </div>

												<%--<div class="fv-row">
        <!--begin::Dropzone-->
        <div class="dropzone" id="kt_dropzonejs_example_1">
            <!--begin::Message-->
            <div class="dz-message needsclick">
                <!--begin::Icon-->
                <i class="bi bi-file-earmark-arrow-up text-primary fs-3x"></i>
                <!--end::Icon-->

                <!--begin::Info-->
                <div class="ms-4">
                    <h3 class="fs-5 fw-bolder text-gray-900 mb-1">Upload Event Image</h3>
                   <%-- <span class="fs-7 fw-bold text-gray-400">Upload up to 10 files</span>
                </div>
                
            </div>
        </div>
       
    </div>--%>
											</div>
                    

                                     <%-- <div class="row mb-6">
												<div class="fv-row">
        <!--begin::Dropzone-->
        <div class="dropzone" id="kt_dropzonejs_example_1">
            <!--begin::Message-->
            <div class="dz-message needsclick">
                <!--begin::Icon-->
                <i class="bi bi-file-earmark-arrow-up text-primary fs-3x"></i>
                <!--end::Icon-->

                <!--begin::Info-->
                <div class="ms-4">
                    <h3 class="fs-5 fw-bolder text-gray-900 mb-1">Drop files here or click to upload.</h3>
                   <%-- <span class="fs-7 fw-bold text-gray-400">Upload up to 10 files</span>
                </div>
                <!--end::Info-->
            </div>
        </div>
        <!--end::Dropzone-->
    </div>
											</div>--%>


                                    <div class="row">
                                        	<div class="overlay mt-8">
																<!--begin::Image-->
																<%--<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px"  style=background-image:url('<%= GetImmage(huid.Value) %>')></div>  --%>
                                                  <img style="width:100%" id="img_banner" class="img-fluid" src='<%= GetImmage(huid.Value) %>' alt="" />
																<!--end::Image-->
																<!--begin::Links-->
																
																<!--end::Links-->
															
															</div>

                                    </div>




                             




                                </div>



                                <div class="card-header border-0 " aria-expanded="true">
                                </div>
                                <div class="card-body border-top p-9">
                                    <div class="card mb-5 mb-xl-10">
                                        <div class="card-header border-0 " aria-expanded="true">
                                            <!--begin::Card title-->
                                            <div class="card-title m-0">
                                                <i class="bi bi-filter-square text-primary fs-3x me-6"></i>
                                                <h3 class="fw-bolder m-0">Description</h3>
                                            </div>
                                            <!--end::Card title-->
                                        </div>
                                        <div class="card-body border-top p-9">



                                            <div class="row mb-6">
                                                <!--begin::Label-->

                                                <!--end::Label-->
                                                <!--begin::Col-->
                                            <%--    <label class="col-lg-2 col-form-label required fw-bold fs-6">Short Title</label>
                                                <div class="col-lg-10 fv-row fv-plugins-icon-container">
                                                    <input type="text" name="Property" class="form-control form-control-lg form-control-solid" placeholder="Short title">
                                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                                </div>--%>

                                              <%--  <label class="col-lg-2 col-form-label required fw-bold fs-6">Description</label>--%>
                                                <div class="col-lg-12 fv-row fv-plugins-icon-container">
                                                   <%-- <asp:TextBox ID="txtDescription" runat="server" Height="400px"></asp:TextBox>--%>
                                                      <CKEditor:CKEditorControl ID="txtDescription"  BasePath="~/Scripts/ckeditor_4.20.1/" runat="server" Height="400px"  ClientIDMode="Static" BasicEntities="true" FullPage="true"></CKEditor:CKEditorControl>
                                                   <%-- <ajaxToolkit:HtmlEditorExtender ID="txtDescriptionext" runat="server" TargetControlID="txtDescription"></ajaxToolkit:HtmlEditorExtender>--%>
                                                 <%-- <CKEditor:CKEditorControl ID="" BasePath="~/Scripts/ckeditor/" runat="server"  Height="200px"  Width="100%"  ></CKEditor:CKEditorControl>--%>

                                                  <%--  <textarea class="form-control form-control-solid rounded-3" rows="4"></textarea>--%>



                                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                                </div>
                                                <!--end::Col-->







                                            </div>







                                        </div>


<%--

                                        <ul class="nav nav-tabs nav-pills  flex-row border-0  me-5 mb-3 mb-md-0 fs-6">
                                            <li class="nav-item me-0 mb-md-2">
                                                <a class="nav-link active btn btn-flex btn-active-light-success" data-bs-toggle="tab" href="#kt_tab_pane_4">
                                                    <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                    <span class="d-flex flex-column align-items-start">
                                                        <span class="fs-4 fw-bolder">Paid</span>

                                                    </span>
                                                </a>
                                            </li>


                                            <li class="nav-item me-0 mb-md-2">
                                                <a class="nav-link btn btn-flex btn-active-light-info" data-bs-toggle="tab" href="#kt_tab_pane_5">
                                                    <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                    <span class="d-flex flex-column align-items-start">
                                                        <span class="fs-4 fw-bolder">Free</span>

                                                    </span>
                                                </a>
                                            </li>

                                            <li class="nav-item me-0 mb-md-2">
                                                <a class="nav-link btn btn-flex btn-active-light-info" data-bs-toggle="tab" href="#kt_tab_pane_5">
                                                    <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                    <span class="d-flex flex-column align-items-start">
                                                        <span class="fs-4 fw-bolder">Free</span>

                                                    </span>
                                                </a>
                                            </li>

                                            <li class="nav-item me-0 mb-md-2">
                                                <a class="nav-link btn btn-flex btn-active-light-info" data-bs-toggle="tab" href="#kt_tab_pane_5">
                                                    <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                    <span class="d-flex flex-column align-items-start">
                                                        <span class="fs-4 fw-bolder">Free</span>

                                                    </span>
                                                </a>
                                            </li>

                                            <li class="nav-item ">
                                                <a class="nav-link btn btn-flex btn-active-light-warning" data-bs-toggle="tab" href="#kt_tab_pane_6">
                                                    <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                    <span class="d-flex flex-column align-items-start">
                                                        <span class="fs-4 fw-bolder">Donation</span>

                                                    </span>
                                                </a>
                                            </li>
                                        </ul>--%>


                                    </div>

                                     <div class="card mb-5 mb-xl-10">
                                        <div class="card-header border-0 " aria-expanded="true">
                                            <!--begin::Card title-->
                                            <div class="card-title m-0">
                                                <i class="bi bi-filter-square text-primary fs-3x me-6"></i>
                                                <h3 class="fw-bolder m-0">Refund Policy</h3>
                                            </div>
                                            <!--end::Card title-->
                                        </div>
                                        <div class="card-body border-top p-9">



                                            <div class="row mb-6">
                                               

                                                <div class="col-lg-12 fv-row fv-plugins-icon-container">
                                                    <asp:TextBox ID="txtRefund" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" Text="There are no refunds, all sales are final."></asp:TextBox>
                                                    
                                                
                                                </div>

                                            </div>

                                        </div>



                                    </div>

                                </div>

                               

                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                  


                                    <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
                                    <%-- <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="" value="Back" />--%>
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                   <%-- <input type="button" onclick="NextAddTicketsIdTab();" name="Next" title="Next" style="width: 100px; text-decoration-color: black" class="btn btn-primary" value="Next" />--%>

                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;




                                </div>


                               

                            </div>

                        </div>



                        </div>
                    </div>
                </div>
            </div>
        </div>
    



       
        <script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
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


    pageLoad();

    function pageLoad() {

        //  jQuery(document).ready(function () {

        var unid = $('#huid').val();
        var myDropzone1 = new Dropzone("#kt_dropzonejs_example_1", {
            url: '../UploadHandler.ashx?unid=' + unid,
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 1,
            maxFilesize: 10, // MB
            addRemoveLinks: true,
            accept: function (file, done) {
                if (file.name == "wow.jpg") {
                    done("Naha, you don't.");
                } else {
                    done();

                    window.location = "Details.aspx?unid=" + unid + "&v=" + Math.random();
                  //  window.location.reload();
                }
            }
        });
        // });



    }

    // set the dropzone container id
    //var id = "#kt_dropzonejs_example_1";
    //var unid = $('#huid').val();
    //// set the preview element template
    //var previewNode = $(id + " .dropzone-item");

    //previewNode.id = "";
    //var previewTemplate = previewNode.parent(".dropzone-items").html();
    //previewNode.remove();

    //var myDropzone = new Dropzone(id, { // Make the whole body a dropzone
    //    url: "../UploadHandler.ashx?unid=" + unid, // Set the url for your upload script location
    //    parallelUploads: 20,
    //    maxFilesize: 1, // Max filesize in MB
    //    previewTemplate: previewTemplate,
    //    previewsContainer: id + " .dropzone-items", // Define the container to display the previews
    //    clickable: id + " .dropzone-select" // Define the element that should be used as click trigger to select files.
    //});


    //myDropzone.on("addedfile", function (file) {
    //    // Hookup the start button
    //    $(document).find(id + " .dropzone-item").css("display", "");
    //});

    //// Update the total progress bar
    //myDropzone.on("totaluploadprogress", function (progress) {
    //    $(id + " .progress-bar").css("width", progress + "%");
    //});

    //myDropzone.on("sending", function (file) {
    //    // Show the total progress bar when upload starts
    //    $(id + " .progress-bar").css("opacity", "1");
    //});

    //// Hide the total progress bar when nothing"s uploading anymore
    //myDropzone.on("complete", function (progress) {
    //    var thisProgressBar = id + " .dz-complete";

    //    setTimeout(function () {
    //        $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css("opacity", "0");
    //    }, 300)
    //});
</script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
