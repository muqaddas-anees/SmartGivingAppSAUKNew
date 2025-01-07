<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainTab.master" CodeBehind="EventReminders.aspx.cs" Inherits="DeffinityAppDev.App.Events.EventReminders" %>





<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
    Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">




    <div class="card">
        <!--begin::Body-->
        <div class="card-body p-lg-20 pb-lg-0">

            <!--begin::Layout-->
            <div class="d-flex flex-column flex-xl-row">
                <!--begin::Content-->
                <div class="flex-lg-row-fluid me-xl-10">
                    <!--begin::Post content-->
                    <div class="mb-10">
                        <!--begin::Wrapper-->
                        <div class="mb-8">
                            <!--begin::Info-->

                            <!--end::Info-->
                            <!--begin::Wrapper-->
                            <div class="d-flex flex-column h-100">
                                <!--begin::Header-->
                                <div class="mb-7">
                                    <!--begin::Headin-->
                                    <div class="d-flex flex-stack mb-12">
                                        <!--begin::Title-->
                                        <div class="col-md-12 col-xxl-12">
                                            <a href="#" class="fs-4 text-gray-800 text-hover-primary fw-bolder mb-0">
                                                <asp:Literal ID="title" runat="server"></asp:Literal>
                                            </a>
                                        </div>
                                        <!--end::Title-->

                                    </div>
                                    <!--end::Heading-->
                                    <!--begin::Items-->
                                    <div class="d-flex align-items-center flex-wrap d-grid gap-2">
                                        <!--begin::Item-->
                                        <div class="d-flex align-items-center me-5 me-xl-13">
                                            <!--begin::Symbol-->
                                            <div class="symbol symbol-30px symbol-circle me-3">
                                                <asp:Image ID="img_user" runat="server" />
                                            </div>
                                            <!--end::Symbol-->
                                            <!--begin::Info-->
                                            <div class="m-0">
                                                <span class="fw-bold text-gray-400 d-block fs-8">Organiser</span>
                                                <span class="fw-bolder text-gray-800 fs-7">
                                                    <asp:Literal ID="loggedbyname" runat="server" />
                                                </span>
                                            </div>
                                            <!--end::Info-->
                                        </div>
                                        <!--end::Item-->
                                        <!--begin::Item-->
                                        <div class="d-flex align-items-center">
                                            <!--begin::Symbol-->
                                            <div class="symbol symbol-30px symbol-circle me-3">
                                                <span class="symbol-label bg-success">
                                                    <!--begin::Svg Icon | path: icons/duotune/abstract/abs042.svg-->
                                                    <span class="svg-icon svg-icon-5 svg-icon-white">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
                                                            <path d="M18 21.6C16.6 20.4 9.1 20.3 6.3 21.2C5.7 21.4 5.1 21.2 4.7 20.8L2 18C4.2 15.8 10.8 15.1 15.8 15.8C16.2 18.3 17 20.5 18 21.6ZM18.8 2.8C18.4 2.4 17.8 2.20001 17.2 2.40001C14.4 3.30001 6.9 3.2 5.5 2C6.8 3.3 7.4 5.5 7.7 7.7C9 7.9 10.3 8 11.7 8C15.8 8 19.8 7.2 21.5 5.5L18.8 2.8Z" fill="currentColor"></path>
                                                            <path opacity="0.3" d="M21.2 17.3C21.4 17.9 21.2 18.5 20.8 18.9L18 21.6C15.8 19.4 15.1 12.8 15.8 7.8C18.3 7.4 20.4 6.70001 21.5 5.60001C20.4 7.00001 20.2 14.5 21.2 17.3ZM8 11.7C8 9 7.7 4.2 5.5 2L2.8 4.8C2.4 5.2 2.2 5.80001 2.4 6.40001C2.7 7.40001 3.00001 9.2 3.10001 11.7C3.10001 15.5 2.40001 17.6 2.10001 18C3.20001 16.9 5.3 16.2 7.8 15.8C8 14.2 8 12.7 8 11.7Z" fill="currentColor"></path>
                                                        </svg>
                                                    </span>
                                                    <!--end::Svg Icon-->
                                                </span>
                                            </div>
                                            <!--end::Symbol-->
                                            <!--begin::Info-->
                                            <div class="m-0">
                                                <span class="fw-bold text-gray-400 d-block fs-8">Price</span>
                                                <span class="fw-bolder text-gray-800 fs-7">
                                                    <asp:Literal ID="price" runat="server"></asp:Literal>
                                                </span>
                                            </div>
                                            <!--end::Info-->
                                        </div>



                                             <div class="d-flex align-items-center">
         <!--begin::Symbol-->
         <div class="symbol symbol-30px symbol-circle me-3">
             <span class="symbol-label bg-success">
                 <!--begin::Svg Icon | path: icons/duotune/abstract/abs042.svg-->
                 <span class="svg-icon svg-icon-5 svg-icon-white">
                    <i class="bi bi-calendar" style="width:24px;color:white;height:24px;font-size:14px"></i>
                 </span>
                 <!--end::Svg Icon-->
             </span>
         </div>
         <!--end::Symbol-->
         <!--begin::Info-->
         <div class="m-0">
             <span class="fw-bold text-gray-400 d-block fs-8">Date</span>
             <span class="fw-bolder text-gray-800 fs-7">
                 <asp:Literal ID="Date" runat="server"></asp:Literal>
             </span>
         </div>
         <!--end::Info-->
     </div>
                                        <!--end::Item-->
                                    </div>
                                    <!--end::Items-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Body-->
                                <div class="mb-6" style="min-height: 180px">
                                    <!--begin::Text-->
                                    <span class="fw-bold text-gray-600 fs-6 mb-8 d-block" style="max-height: 200px">
                                        <asp:Literal ID="addressdesc" runat="server"></asp:Literal>
                                    </span>
                                    <!--end::Text-->
                                    <!--begin::Stats-->
                                    <div class=" ">
                                        <div class="fs-7 fw-bolder text-gray-700"><i class="fas fa-location-arrow" style="font-size: 22px"></i>
                                            <asp:Literal ID="addressadr" runat="server"></asp:Literal>
                                        </div>
                                        <div class="fw-bold text-gray-400 mb-6 mt-5 ">
                                            <%--    <a href="">
                                                                        <i class="bi bi-pin-map-fill"></i>
                                                                        Google map location</a>--%>
                                        </div>
                                    </div>
                                    <!--end::Stats-->
                                </div>
                                <!--end::Body-->
                                <!--begin::Footer-->

                                <!--end::Footer-->
                            </div>
                            <!--end::Wrapper-->
                            <!--end::Container-->
                        </div>
                        <!--end::Wrapper-->


                    </div>
                    <!--end::Post content-->
                </div>
                <!--end::Content-->
                <!--begin::Sidebar-->
                <div class="flex-column flex-lg-row-auto w-100 w-xl-600px mb-5">
                    <div class="overlay mt-8">
                        <asp:Image ID="img_event" runat="server" CssClass="img-fluid" />
                        <%-- <asp:Image ID="imgView" runat="server" ImageUrl='<%# GetImmage(Eval("ID").ToString()) %>' Height="300" />--%>
                        <%--   <asp:Label ID="lblImg" runat="server" Text='<%# GetImmageString(Eval("ID").ToString()) %>'></asp:Label>--%>
                        <%--<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px" style=background-image:url('<%# GetImmage(Eval("unid")) %>')>
                                                                   
																</div>  --%>
                    </div>
                </div>
                <!--end::Sidebar-->
            </div>
            <div class="card shadow mt-4">
                <!-- Card Header with Title and Button -->
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h4 class="card-title mb-0">Configure Reminders</h4>
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                </div>

                <!-- Card Body -->
                <div class="card-body">
                    <!-- Reminder Configuration Row -->
                    <div class="row align-items-center mb-3">
                        <div class="col-md-7 d-flex align-items-center">
                            <asp:Label runat="server" ID="lblReminderDays" Style="font-size: 20px; margin-bottom: 0 !important" Text="Send reminder before" CssClass="form-label"></asp:Label>
                            <asp:DropDownList runat="server" Width="100px" OnSelectedIndexChanged="ddlReminderDays_SelectedIndexChanged" AutoPostBack="true" Style="width: 100px !important; margin: 0px 10px" ID="ddlReminderDays" CssClass="form-select">
                                <asp:ListItem Text="Select a value" Value=""></asp:ListItem>
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>

                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            </asp:DropDownList>
                            <p class="" style="font-size: 20px; margin-bottom: 0 !important">Days before the event begins</p>
                        </div>
                    </div>

                    <!-- Subject Line Row -->
                    <div class="row mb-3">
                        <div class="col-md-5">
                            <asp:Label runat="server" ID="lblSubjectLine" Text="Subject Line" CssClass="form-label"></asp:Label>
                            <asp:TextBox runat="server" ID="txtSubjectLine" ClientIDMode="Static" CssClass="form-control" Placeholder="Enter subject line"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblSubjectTags" Text="Subject Personalisation Tags" CssClass="form-label"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlSubjectTags" ClientIDMode="Static" CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Body Personalisation Tags Row -->
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <asp:Label runat="server" ID="lblBodyTags" Text="Body Personalisation Tags" CssClass="form-label"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlBodyTags" ClientIDMode="Static" CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- CKEditor Row -->
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <asp:Label runat="server" ID="lblEditor" Text="Body Editor" CssClass="form-label"></asp:Label>
                            <CKEditor:CKEditorControl ID="txtDescription" BasePath="~/Scripts/ckeditor_4.20.1/" runat="server"
                                Height="400px" ClientIDMode="Static" BasicEntities="true" FullPage="true" />
                        </div>
                    </div>
                </div>
            </div>

            <!--end::Body-->
        </div>
        <!--end::Post card-->
    </div>
    <script type="text/javascript">
        // This function updates the subject line when a tag is selected
        function updateSubjectLine() {
            var selectedTag = document.getElementById('<%= ddlSubjectTags.ClientID %>').value;
           var subjectLine = document.getElementById('<%= txtSubjectLine.ClientID %>').value;

           if (subjectLine.trim() !== "") {
               subjectLine += " " + selectedTag;
           } else {
               subjectLine = selectedTag;
           }

           document.getElementById('<%= txtSubjectLine.ClientID %>').value = subjectLine;
        }

        // This function updates the body content when a tag is selected
        function updateBodyContent() {
            var selectedTag = document.getElementById('<%= ddlBodyTags.ClientID %>').value;
           var bodyContent = document.getElementById('<%= txtDescription.ClientID %>').value;

           if (bodyContent.trim() !== "") {
               bodyContent += " " + selectedTag;
           } else {
               bodyContent = selectedTag;
           }

           CKEDITOR.instances['<%= txtDescription.ClientID %>'].insertHtml(bodyContent);
        }

        // Attach the updateSubjectLine function to the onchange event of the subject tags dropdown
        document.getElementById('<%= ddlSubjectTags.ClientID %>').addEventListener('change', updateSubjectLine);

        // Attach the updateBodyContent function to the onchange event of the body tags dropdown
        document.getElementById('<%= ddlBodyTags.ClientID %>').addEventListener('change', updateBodyContent);
    </script>
    <script type="text/javascript">
        window.onload = function () {
            // Get the user's time zone name (e.g., "America/New_York")
            var timeZoneName = Intl.DateTimeFormat().resolvedOptions().timeZone;

            // Get the user's time zone offset in minutes from UTC
            var timeZoneOffset = new Date().getTimezoneOffset();  // in minutes

            // Set the HiddenField values with the time zone info
            document.getElementById('<%= hfTimeZoneName.ClientID %>').value = timeZoneName;
         document.getElementById('<%= hfTimeZoneOffset.ClientID %>').value = timeZoneOffset;
        };
    </script>

    <asp:HiddenField runat="server" ID="hfTimeZoneName" />
    <asp:HiddenField runat="server" ID="hfTimeZoneOffset" />

</asp:Content>
