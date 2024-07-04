<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="testdrop.aspx.cs" Inherits="DeffinityAppDev.WF.DC.testdrop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      <link href="../../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../../assets/plugins/global/plugins.bundle.js"></script>
   
    <!--begin::Form-->

    <!--begin::Input group-->
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
                    <span class="fs-7 fw-bold text-gray-400">Upload up to 10 files</span>
                </div>
                <!--end::Info-->
            </div>
        </div>
        <!--end::Dropzone-->
    </div>
    <!--end::Input group-->

<!--end::Form-->
     <script>
         var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
             url: "https://keenthemes.com/scripts/void.php", // Set the url for your upload script location
             paramName: "file", // The name that will be used to transfer the file
             maxFiles: 10,
             maxFilesize: 10, // MB
             addRemoveLinks: true,
             accept: function (file, done) {
                 if (file.name == "wow.jpg") {
                     done("Naha, you don't.");
                 } else {
                     done();
                 }
             }
         });
     </script>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
