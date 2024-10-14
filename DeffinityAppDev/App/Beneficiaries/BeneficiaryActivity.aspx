<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeneficiaryActivity.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.BeneficiaryActivity" MasterPageFile="~/App/Beneficiaries/Beneficiaries.master" %>
<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Additional scripts or styles specific to this page -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/some-library/1.0.0/some-library.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/some-library/1.0.0/some-library.min.css" />



      <div class="d-flex justify-content-between align-items-center mb-4">
    <h1>Beneficiary Activity</h1>
    <button id="addCommunicationButton" class="btn btn-primary" type="button">Add Activty</button>
     
</div>
     <div class="card-header card-header-stretch">
    <!--begin::Title-->
    <div class="card-title d-flex align-items-center">            
        <i class="ki-duotone ki-calendar-8 fs-1 text-primary me-3 lh-0"><span class="path1"></span><span class="path2"></span><span class="path3"></span><span class="path4"></span><span class="path5"></span><span class="path6"></span></i> 	
        <h3 class="fw-bold m-0 text-gray-800">Jan 23, 2024</h3>
    </div>
    <!--end::Title-->

    <!--begin::Toolbar-->
    <div class="card-toolbar m-0">
        <!--begin::Tab nav-->
        <ul class="nav nav-tabs nav-line-tabs nav-stretch fs-6 border-0 fw-bold" role="tablist">
            <li class="nav-item" role="presentation">
                <a id="kt_activity_today_tab" class="nav-link justify-content-center text-active-gray-800 active" runat="server" onserverclick="LoadTodayActivities" href="#" aria-selected="true">
                    Today
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a id="kt_activity_week_tab" class="nav-link justify-content-center text-active-gray-800" runat="server" onserverclick="LoadWeeklyActivities" href="#" aria-selected="false" tabindex="-1">
                    Week
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a id="kt_activity_month_tab" class="nav-link justify-content-center text-active-gray-800" runat="server" onserverclick="LoadMonthlyActivities" href="#" aria-selected="false" tabindex="-1">
                    Month
                </a>
            </li>
            <li class="nav-item" role="presentation">
                <a id="kt_activity_year_tab" class="nav-link justify-content-center text-active-gray-800 text-hover-gray-800" runat="server" onserverclick="LoadYearlyActivities" href="#" aria-selected="false" tabindex="-1">
                    2024
                </a>
            </li>
        </ul>
        <!--end::Tab nav-->
    </div>
    <!--end::Toolbar-->
</div>

<div class="card-body">
        <!--begin::Tab Content-->
        <div class="tab-content">
            <!--begin::Tab panel-->
            <div id="kt_activity_today" class="card-body p-0 tab-pane fade show active" role="tabpanel" aria-labelledby="kt_activity_today_tab">
                <!--begin::Timeline-->
<div class="timeline timeline-border-dashed">
    <!--begin::Timeline item-->

<!--end::Timeline item-->
    <!--begin::Timeline item-->
   <asp:Repeater ID="rptActivities" runat="server">
    <ItemTemplate>
        <div class="timeline-item">
            <!--begin::Timeline line-->
            <div class="timeline-line"></div>
            <!--end::Timeline line-->

            <!--begin::Timeline icon-->
            <div class="timeline-icon me-4">
                <i class="ki-duotone ki-flag fs-2 text-gray-500">
                    <span class="path1"></span>
                    <span class="path2"></span>
                </i>
            </div>
            <!--end::Timeline icon-->

            <!--begin::Timeline content-->
            <div class="timeline-content mb-10 mt-n2">
                <!--begin::Timeline heading-->
                <div class="overflow-auto pe-3 activity">
                    <!--begin::Title-->
                    <div class="fs-5 fw-semibold mb-2">
                        <%# Eval("ProgressDetails") %>
                    </div>
                    <!--end::Title-->

                    <!--begin::Description-->
                    <div class="d-flex align-items-center mt-1 fs-6">
                        <!--begin::Info-->
                        <div class="text-muted me-2 fs-7">
                            <%# Eval("ActivityDate", "{0:MMMM dd, yyyy}") %> by <%# Eval("LoggedBy") %>
                        </div>
                        <!--end::Info-->

                        <!--begin::User-->
                        <div class="symbol symbol-circle symbol-25px" data-bs-toggle="tooltip" data-bs-boundary="window" data-bs-placement="top" aria-label="<%# Eval("LoggedBy") %>">
                            <img src='<%# (Eval("ImageData") == null) ? "/path/to/default-image.jpg" : "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("ImageData")) %>' alt="User">

                        </div>
                      
                    </div>
                    <!--end::Description-->
                </div>
                <!--end::Timeline heading-->
            </div>
            <!--end::Timeline content-->
        </div>
    </ItemTemplate>
</asp:Repeater>


<!--end::Timeline item--> </div>
<!--end::Timeline-->            </div>
           

            <!--begin::Tab panel-->
            <div id="kt_activity_week" class="card-body p-0 tab-pane fade show" role="tabpanel" aria-labelledby="kt_activity_week_tab">
                <!--begin::Timeline-->

<!--end::Timeline-->            </div>
            <!--end::Tab panel-->

            <!--begin::Tab panel-->
            <div id="kt_activity_month" class="card-body p-0 tab-pane fade show" role="tabpanel" aria-labelledby="kt_activity_month_tab">
                <!--begin::Timeline-->

<!--end::Timeline-->            </div>
            <!--end::Tab panel-->

            <!--begin::Tab panel-->
            <div id="kt_activity_year" class="card-body p-0 tab-pane fade show" role="tabpanel" aria-labelledby="kt_activity_year_tab">
                <!--begin::Timeline-->
<div class="timeline timeline-border-dashed">
    <!--begin::Timeline item-->
<div class="timeline-item">
    <!--begin::Timeline line-->
    <div class="timeline-line"></div>
    <!--end::Timeline line-->

    <!--begin::Timeline icon-->
    <div class="timeline-icon">
        <i class="ki-duotone ki-disconnect fs-2 text-gray-500"><span class="path1"></span><span class="path2"></span><span class="path3"></span><span class="path4"></span><span class="path5"></span></i>    </div>
    <!--end::Timeline icon-->

    <!--begin::Timeline content-->
    <div class="timeline-content mb-10 mt-n1">
        <!--begin::Timeline heading-->
        <div class="mb-5 pe-3">
            <!--begin::Title-->
            <a href="#" class="fs-5 fw-semibold text-gray-800 text-hover-primary mb-2">3 New Incoming Project Files:</a>
            <!--end::Title-->

            <!--begin::Description-->
            <div class="d-flex align-items-center mt-1 fs-6">
                <!--begin::Info-->
                <div class="text-muted me-2 fs-7">Sent at 10:30 PM by</div>
                <!--end::Info-->

                <!--begin::User-->
                <div class="symbol symbol-circle symbol-25px" data-bs-toggle="tooltip" data-bs-boundary="window" data-bs-placement="top" aria-label="Jan Hummer" data-bs-original-title="Jan Hummer" data-kt-initialized="1">
                    <img src="/metronic8/demo1/assets/media/avatars/300-23.jpg" alt="img">
                </div>  
                <!--end::User--> 
            </div>
            <!--end::Description-->
        </div>
        <!--end::Timeline heading-->

        <!--begin::Timeline details-->
        <div class="overflow-auto pb-5">
            <div class="d-flex align-items-center border border-dashed border-gray-300 rounded min-w-700px p-5">
                <!--begin::Item-->
                <div class="d-flex flex-aligns-center pe-10 pe-lg-20">  
                    <!--begin::Icon-->                                  
                    <img alt="" class="w-30px me-3" src="/metronic8/demo1/assets/media/svg/files/pdf.svg">
                    <!--end::Icon--> 

                    <!--begin::Info--> 
                    <div class="ms-1 fw-semibold">
                        <!--begin::Desc--> 
                        <a href="/metronic8/demo1/apps/projects/project.html" class="fs-6 text-hover-primary fw-bold">Finance KPI App Guidelines</a>
                        <!--end::Desc--> 

                        <!--begin::Number--> 
                        <div class="text-gray-500">1.9mb</div>
                        <!--end::Number--> 
                    </div>
                    <!--begin::Info--> 
                </div>
                <!--end::Item-->

                <!--begin::Item-->
                <div class="d-flex flex-aligns-center pe-10 pe-lg-20">   
                    <!--begin::Icon-->                                  
                    <img alt="/metronic8/demo1/apps/projects/project.html" class="w-30px me-3" src="/metronic8/demo1/assets/media/svg/files/doc.svg">
                    <!--end::Icon--> 

                    <!--begin::Info--> 
                    <div class="ms-1 fw-semibold">
                        <!--begin::Desc--> 
                        <a href="#" class="fs-6 text-hover-primary fw-bold">Client UAT Testing Results</a>
                        <!--end::Desc--> 

                        <!--begin::Number--> 
                        <div class="text-gray-500">18kb</div>
                        <!--end::Number--> 
                    </div>
                    <!--end::Info--> 
                </div>
                <!--end::Item-->

                <!--begin::Item-->
                <div class="d-flex flex-aligns-center">   
                    <!--begin::Icon-->                                  
                    <img alt="/metronic8/demo1/apps/projects/project.html" class="w-30px me-3" src="/metronic8/demo1/assets/media/svg/files/css.svg">
                    <!--end::Icon--> 

                    <!--begin::Info--> 
                    <div class="ms-1 fw-semibold">
                        <!--begin::Desc--> 
                        <a href="#" class="fs-6 text-hover-primary fw-bold">Finance Reports</a>
                        <!--end::Desc--> 

                        <!--begin::Number--> 
                        <div class="text-gray-500">20mb</div>
                        <!--end::Number--> 
                    </div>
                    <!--end::Icon--> 
                </div>
                <!--end::Item-->
            </div>
        </div>
        <!--end::Timeline details-->
    </div>
    <!--end::Timeline content-->    
</div>
<!--end::Timeline item-->
    <!--begin::Timeline item-->

<!--end::Timeline item--> </div>
<!--end::Timeline-->            </div>
            <!--end::Tab panel-->
        </div>
        <!--end::Tab Content-->
    </div>

     <!--ADD Activity Modal-->
         <!-- ADD Activity Modal -->
<div class="modal fade" id="addCommunicationModal" tabindex="-1" aria-labelledby="addCommunicationModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="addCommunicationModalLabel">Add Communication</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <!-- Date Picker -->
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                        <div class="input-group">
                            <asp:TextBox ID="txtActivityDate" runat="server" CssClass="form-control bg-transparent" TextMode="Date" />
                            <asp:RequiredFieldValidator ID="txt" ControlToValidate="txtActivityDate" runat="server" ErrorMessage="Date is Required" CssClass="text-danger"/>
                        </div>
                    </div>
                </div>


                                <div class="row mb-3">
    <div class="col-12">
        <asp:Label for="ddlLoggedBy" runat="server" Text="Logged By"></asp:Label>
        <asp:DropDownList ID="ddlLoggedBy" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="false">
            <asp:ListItem Text="Select a person" Value=""></asp:ListItem>
          
        </asp:DropDownList>
    </div>
</div>


                <!-- Rich Text Editor -->
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Label ID="lblFeedback" runat="server" Text="Progress"></asp:Label>
                        <asp:TextBox ID="txtProgressDetails" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5" placeholder="Details of the Progress made"></asp:TextBox>
                    </div>  
                </div>

                <!-- File Upload with Add/Remove functionality -->
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Label ID="lblUpload" runat="server" Text="Upload file(s)"></asp:Label>
                        <asp:FileUpload ID="fileUploadDocuments" runat="server" CssClass="form-control" onchange="showImagePreview(this);" />
                        <div id="imagePreviewContainer" class="mt-3">
                            <!-- Image preview will be displayed here -->
                        </div>
                    </div>
                </div>

                <!-- Logged By -->


                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnSaveCommunication" CssClass="btn btn-primary" Text="Save" OnClick="btnSaveActivity_Click" />
                </div>
            </div>
        </div>
    </div>
</div>


     <!--Success Modal-->
           <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="successModalLabel">Success</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
<asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
</div>
                        <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
                 <script>
                var hostUrl = "/metronic8/demo1/assets/";
                 </script>
<script src="/metronic8/demo1/assets/plugins/global/plugins.bundle.js"></script>
<script src="/metronic8/demo1/assets/js/scripts.bundle.js"></script>
     <script type="text/javascript">
         document.getElementById("addCommunicationButton").addEventListener("click", function () {
             // Use Bootstrap's modal method to show the modal
             var myModal = new bootstrap.Modal(document.getElementById("addCommunicationModal"));
             myModal.show();
         });
         function showImagePreview(input) {
             var previewContainer = document.getElementById('imagePreviewContainer');
             previewContainer.innerHTML = ''; // Clear previous content

             if (input.files && input.files[0]) {
                 var file = input.files[0];
                 var fileExtension = file.name.split('.').pop().toLowerCase();
                 var allowedExtensions = ['jpg', 'jpeg', 'png', 'gif'];

                 if (allowedExtensions.includes(fileExtension)) {
                     var reader = new FileReader();

                     reader.onload = function (e) {
                         var img = document.createElement('img');
                         img.src = e.target.result;
                         img.alt = 'Image Preview';
                         img.style.maxWidth = '200px';
                         img.style.maxHeight = '200px';
                         previewContainer.appendChild(img);
                     };

                     reader.readAsDataURL(file);
                 } else {
                     var message = document.createElement('p');
                     message.textContent = 'Selected file: ' + file.name;
                     previewContainer.appendChild(message);
                 }
             }
         }
         function showSuccessModal() {
             var myModal = new bootstrap.Modal(document.getElementById("successModal"));
             myModal.show();
         }
         if (window.history.replaceState) {
             window.history.replaceState(null, null, window.location.href);
         }
     </script>
    </asp:Content>