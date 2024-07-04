<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CreateEvent.aspx.cs" Inherits="DeffinityAppDev.App.CreateEvent" %>

<%--
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

 <link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
    

    <div class="container-xxl" id="kt_content_container">
        <!--begin::Row-->
        <div class="row gy-5 g-xl-8">
            <br />
            <br />
            <h1 class="d-flex align-items-center text-dark fw-bolder fs-3 my-1">Create An Event</h1>
            <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />

            <!--end::Col-->
            <!--begin::Col-->
            <div class="col-xl-12">
                <!--begin::Mixed Widget 12-->




                <div class="card card-xl-stretch mb-5 mb-xl-8">
                    <!--begin::Header-->



                    <div id="kt_content_container" class="container-xxl">



                        <div id="EventDetailsId" style="display: block">

                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                      <%--  <h3 class="fw-bolder    m-0">Create An Event</h3>--%>
                                        <i class="bi bi-calendar3 text-primary fs-3x me-6"></i>
									<h3 class="fw-bolder m-0">Name of your event</h3>

                                    </div>
                                    <!--end::Card title-->
                                </div>
                                <div class="card-body border-top p-9">
                                    



                                    <!--begin::Card title-->
								<div class="card-title m-0">
									
								</div>
								<!--end::Card title-->

                                               <input type="text" name="eventName" id="txtEventName" runat="server" class="form-control form-control-lg form-control-solid" placeholder="Enter name" />



                                </div>

                                <div class="card mb-5 mb-xl-10">
                                    <div class="card-header border-0 " aria-expanded="true">
                                        <!--begin::Card title-->
                                        <div class="card-title m-0">
                                            <i class="bi bi-calendar text-primary fs-3x me-6"></i>
                                            <h3 class="fw-bolder m-0">Date and time</h3>
                                        </div>
                                        <!--end::Card title-->
                                    </div>
                                    <div class="card-body border-top p-9">
                                        <div class="row mb-6">
                                            <!--begin::Label-->

                                            <!--end::Label-->
                                            <!--begin::Col-->

                                            <div class="row mb-6 ms-0">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Start Date </h5>
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                    <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew" Style="width:175px"  placeholder="mm/dd/yyyy"></asp:TextBox>
                                                </div>

                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Start  time</h5>
                                                    <%--<input type="time" id="Time13" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start time" />--%>
                                                    <asp:TextBox ID="txtStartTime" runat="server" SkinID="TimeNew"  Style="width:150px" placeholder="hh:mm"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row mb-6 ms-0">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">End Date </h5>
                                                    <%--<input type="date" id="Date15" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="End Date" />--%>
                                                     <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew" Style="width:175px"  placeholder="mm/dd/yyyy"></asp:TextBox>
                                                </div>


                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">End time </h5>
                                                   <%-- <input type="time" id="Time14" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="End time" />--%>
                                                     <asp:TextBox ID="txtEndTime" runat="server" SkinID="TimeNew" Style="width:150px" placeholder="hh:mm"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!--end::Col-->
                                        </div>

                                    </div>
                                </div>


                             <%--   <div class="card mb-5 mb-xl-10">
                                    <div class="card-header border-0 " aria-expanded="true">
                                        <!--begin::Card title-->
                                        <div class="card-title m-0">
                                            <i class="bi bi-building text-primary fs-3x me-6"></i>
                                            <h3 class="fw-bolder m-0">Location of your event</h3>
                                        </div>
                                        <!--end::Card title-->
                                    </div>
                                    <div class="card-body border-top p-9">
                                        <div class="col-lg-8 fv-row fv-plugins-icon-container">

                                            <input type="text" name="company" class="form-control form-control-lg form-control-solid" placeholder="Address 1" />

                                        </div>

                                    </div>
                                </div>--%>




                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                   
                                    

                                    <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue"    value="Back" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    <input type="button" onclick="NextLocationTab();" name="Next" title="Next"  style="width:100px; text-decoration-color:black" class="btn btn-primary"  value="Next"  />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;

                                </div>

                            </div>


                            


                        </div>



                        <div id="LocationEventId" style="display: none">


                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-building text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Location of your event</h3>
                                    </div>
                                    <!--end::Card title-->
                                </div>


                                <div class="card-body border-top p-9">



                                    <div class="col-lg-8 fv-row fv-plugins-icon-container">
                                        <h5 class="fw-bolder m-0">Venue Name  </h5>
                                        <input type="text" name="company" class="form-control form-control-lg form-control-solid" placeholder="Venue Name " id="txtvenuename" runat="server" />
                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                    </div>

                                    <div class="row mb-6 ms-0">
                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Address 1</h5>
                                            <input type="text"  name="company" class="form-control form-control-lg form-control-solid" placeholder="Address 1"  id="txtAddress1" runat="server" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>


                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Address 2 </h5>
                                            <input type="text" id="txtAddress2" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Address 2" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>
                                    </div>


                                    <div class="row mb-6 ms-0">
                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">City </h5>
                                            <input type="text" id="txtCity" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="City" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>


                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">State/Province  </h5>
                                            <input type="text" id="txtState" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="State/Province" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>

                                       
                                    </div>



                                    <div class="row mb-6 ms-0">
                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Country </h5>
                                            <input type="text" id="txtCountry" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Country" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>


                                         <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Zipcode  </h5>
                                            <input type="text" id="txtZipcode" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Zipcode" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>

                                    </div>


                                  <%--  <ul class="nav nav-tabs nav-pills  flex-row border-0  me-5 mb-3 mb-md-0 fs-6">
                                        <li class="nav-item me-0 mb-md-2">
                                            <a class="nav-link active btn btn-flex btn-active-light-success" data-bs-toggle="tab" href="#kt_tab_pane_4">
                                                <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                <span class="d-flex flex-column align-items-start">
                                                    <span class="fs-4 fw-bolder">Search Location</span>

                                                </span>
                                            </a>
                                        </li>

                                    </ul>--%>


                                </div>

                                <div class="card-footer d-flex justify-content-end py-6 px-9">              
                                   

                                   <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="BackEventDetailsIdTab()" value="Back" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    <input type="button" onclick="NextMainEventImageTab();" name="Next" title="Next" style="width: 100px; text-decoration-color: black" class="btn btn-primary" value="Next" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;

                                </div>


                            </div>

                        </div>








                        <div id="MainEventImageId" style="display: none">
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
                              

                                      <div class="row mb-6">
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
                                                    <asp:TextBox ID="txtDescription" runat="server" Height="400px"></asp:TextBox>
                                                    <ajaxToolkit:HtmlEditorExtender ID="txtDescriptionext" runat="server" TargetControlID="txtDescription"></ajaxToolkit:HtmlEditorExtender>
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


                                </div>

                               

                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                  



                                     <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="BackLocationEventIdTab()" value="Back" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    <input type="button" onclick="NextAddTicketsIdTab();" name="Next" title="Next" style="width: 100px; text-decoration-color: black" class="btn btn-primary" value="Next" />

                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;




                                </div>


                               

                            </div>

                        </div>








                        <div id="AddTicketsId" style="display: none">

                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-aspect-ratio text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Add Tickets</h3>
                                    </div>
                                    <!--end::Card title-->
                                </div>
                                <div class="card-body border-top p-9">



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
                                      <%--  </li>
                                        <li class="nav-item ">
                                            <a class="nav-link btn btn-flex btn-active-light-warning" data-bs-toggle="tab" href="#kt_tab_pane_6">
                                                <span class="svg-icon svg-icon-2"><i class="bi bi-textarea"></i></span>
                                                <span class="d-flex flex-column align-items-start">
                                                    <span class="fs-4 fw-bolder">Donation</span>

                                                </span>
                                            </a>
                                        </li>--%>
                                    </ul>






                                    <div class="tab-content" id="myTabContent">
                                        <div class="tab-pane fade show active" id="kt_tab_pane_4" role="tabpanel">

                                            <div class="row mb-6">
                                                <!--begin::Label-->
                                                <div class="col-lg-8 fv-row fv-plugins-icon-container mb-6 ms-0">
                                                    <input type="text"  name="company" class="form-control form-control-lg form-control-solid" placeholder="Name of the Tickets" id="txtAvailableName">
                                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                                </div>

                                                <div class="col-lg-8 fv-row fv-plugins-icon-container mb-6 ms-0">
                                                    <input type="text" name="company" class="form-control form-control-lg form-control-solid" placeholder="Available quantity" id="txtSlot" runat="server">
                                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                                </div>

                                                <div class="col-lg-8 fv-row fv-plugins-icon-container mb-6 ms-0">
                                                    <input type="text" name="company" class="form-control form-control-lg form-control-solid text-right" placeholder="Price" id="txtPrice" runat="server">
                                                    <div class="fv-plugins-message-container invalid-feedback"></div>
                                                </div>
                                                <!--end::Label-->
                                                <!--begin::Col-->
                                                <div class="row mb-6 ">
                                                   <%-- <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                        <div class="d-flex align-items-center justify-content-end flex-equal order-3 fw-row" data-bs-toggle="tooltip" data-bs-trigger="hover" title="Specify invoice due date">
                                                            <!--begin::Date-->
                                                            <div class="fs-6 fw-bolder text-gray-700 text-nowrap">Due Date:</div>
                                                            <!--end::Date-->
                                                            <!--begin::Input-->
                                                            <div class="position-relative d-flex align-items-center w-150px">
                                                                <!--begin::Datepicker-->
                                                                <input class="form-control form-control-solid ps-12 flatpickr-input active" placeholder="Select date" name="invoice_due_date" />

                                                            </div>


                                                            <!--end::Input-->
                                                        </div>
                                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                                    </div>--%>


                                                </div>





                                                <div class="row mb-6 ms-0">
                                                    <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                        <h5 class="fw-bolder m-0">Booking Start Date </h5>
                                                        <asp:TextBox ID="txtBookingStartDate" runat="server" SkinID="DateNew" Style="width:175px" placeholder="mm/dd/yyyy" ></asp:TextBox>
                                                       <%-- <input type="date" id="Date16" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start Date" />--%>
                                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                                    </div>

                                                    <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                        <h5 class="fw-bolder m-0">Booking Start  time</h5>
                                                        <asp:TextBox ID="txtBookingStartTime" runat="server" SkinID="TimeNew" Style="width:150px" placeholder="hh:mm" ></asp:TextBox>
                                                        <%--<input type="time" id="Time15" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="Start time" />--%>
                                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                                    </div>
                                                </div>

                                                <div class="row mb-6 ms-0">
                                                    <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                        <h5 class="fw-bolder m-0">End Date </h5>
                                                         <asp:TextBox ID="txtBookingEndDate" runat="server" SkinID="DateNew" Style="width:175px" placeholder="mm/dd/yyyy" ></asp:TextBox>
                                                       <%-- <input type="date" id="Date17" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="End Date" />--%>
                                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                                    </div>


                                                    <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                        <h5 class="fw-bolder m-0">End time </h5>
                                                         <asp:TextBox ID="txtBookingEndTime" runat="server" SkinID="TimeNew" Style="width:150px" placeholder="hh:mm" ></asp:TextBox>
                                                       <%-- <input type="time" id="Time16" runat="server" name="company" class="form-control form-control-lg form-control-solid" placeholder="End time" />--%>
                                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                                    </div>
                                                </div>





                                            </div>


                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_5" role="tabpanel">
                                            
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_6" role="tabpanel">
                                            
								
                                        </div>
                                    </div>



                                </div>             


                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                   


                                        <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="BackMainEventImageIdIdTab()" value="Back" />
                                      &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button ID="btnAddSpeakers" runat="server" SkinID="btnDefault" Text="Add Speakers" OnClick="btnAddSpeakers_Click" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" SkinID="btnDefault" Text="Save & Publish" OnClick="btnSave_Click" />
                                    <input type="button" onclick="NextTicketSettingsIdTab();" name="Next" title="Next" style="width: 100px; text-decoration-color: black;display:none;visibility:hidden;" class="btn btn-primary" value="Next"  />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>

                                
                            </div>

                        </div>





                        <div id="TicketSettingsId" style="display: none">


                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-intersect text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Tickets Settings</h3>
                                    </div>
                                    <!--end::Card title-->
                                </div>
                                <div class="card-body border-top p-9">
                                    <ul class="nav nav-tabs nav-line-tabs mb-5 fs-6">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-bs-toggle="tab" href="#kt_tab_pane_17">Admission</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-bs-toggle="tab" href="#kt_tab_pane_18">Addons</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-bs-toggle="tab" href="#kt_tab_pane_19">Promocodes</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-bs-toggle="tab" href="#kt_tab_pane_20">Hold</a>
                                        </li>
                                    </ul>

                                    <div class="tab-content" id="myTabContent">
                                        <div class="tab-pane fade show active" id="kt_tab_pane_17" role="tabpanel">
                                            xxEt et consectetur ipsum labore excepteur est proident excepteur ad velit occaecat qui minim occaecat veniam.
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_18" role="tabpanel">
                                            11Nulla est ullamco ut irure incididunt nulla Lorem Lorem minim irure officia enim reprehenderit.
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_19" role="tabpanel">
                                            Promocodes
                                            <br>
                                            22Sint sit mollit irure quis est nostrud cillum consequat Lorem esse do quis dolor esse fugiat sunt do.
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_20" role="tabpanel">
                                            Tickets on Hold
                                            <br>
                                            22Sint sit mollit irure quis est nostrud cillum consequat Lorem esse do quis dolor esse fugiat sunt do.
								
                                        </div>
                                    </div>


                                </div>

                                

                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                    

                                      <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="BackAddTicketsIdTab()" value="Back" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    <input type="button" onclick="NextPublishEventIdTab();" name="Next" title="Next" style="width: 100px; text-decoration-color: black" class="btn btn-primary" value="Next" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;

                                </div>
                            </div>

                        </div>







                       





                        <div id="PublishEventId" style="display: none">

                            <div class="card mb-5 mb-xl-10">
                                <div class="card-header border-0 " aria-expanded="true">
                                    <!--begin::Card title-->
                                    <div class="card-title m-0">
                                        <i class="bi bi-intersect text-primary fs-3x me-6"></i>
                                        <h3 class="fw-bolder m-0">Publish Your Event</h3>
                                    </div>
                                    <!--end::Card title-->
                                </div>
                                <div class="card-body border-top p-9">
                                 <%--   <ul class="nav nav-tabs nav-line-tabs mb-5 fs-6">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-bs-toggle="tab" href="#kt_tab_pane_17">Admission</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-bs-toggle="tab" href="#kt_tab_pane_18">Addons</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-bs-toggle="tab" href="#kt_tab_pane_19">Promocodes</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-bs-toggle="tab" href="#kt_tab_pane_20">Hold</a>
                                        </li>
                                    </ul>--%>

                                    <div class="tab-content" id="myTabContent">
                                        <div class="tab-pane fade show active" id="kt_tab_pane_17" role="tabpanel">
                                            xxEt et consectetur ipsum labore excepteur est proident excepteur ad velit occaecat qui minim occaecat veniam.
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_18" role="tabpanel">
                                            11Nulla est ullamco ut irure incididunt nulla Lorem Lorem minim irure officia enim reprehenderit.
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_19" role="tabpanel">
                                            Promocodes
                                            <br>
                                            22Sint sit mollit irure quis est nostrud cillum consequat Lorem esse do quis dolor esse fugiat sunt do.
								
                                        </div>
                                        <div class="tab-pane fade" id="kt_tab_pane_20" role="tabpanel">
                                            Tickets on Hold
                                            <br>
                                            22Sint sit mollit irure quis est nostrud cillum consequat Lorem esse do quis dolor esse fugiat sunt do.
								
                                        </div>
                                    </div>


                                </div>





                                <div class="card-footer d-flex justify-content-end py-6 px-9">
                                    
                                      <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="BackTicketSettingsIdTab()" value="Back" />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                     <button type="submit" class="btn btn-primary">Create Event</button>
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>



                            </div>

                        </div>











                    </div>
                    <!--end::Mixed Widget 12-->
                </div>
                <!--end::Col-->

            </div>
            <!--end::Row-->

        </div>
        <!--end::Container-->













       




















      


        <!--end::Root-->
        <!--begin::Drawers-->














       
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


        <script>

           // alert("its working         EventDetailsId         LocationEventId    MainEventImageId   AddTicketsId    TicketSettingsId      block            PublishEventId            ");



            //// document.getElementById("EventDetailsId").style.display = "none";
            ////  document.getElementById("LocationEventId").style.display = "none";
            //document.getElementById("MainEventImageId").style.display = "none";
            //document.getElementById("AddTicketsId").style.display = "none";
            //document.getElementById("TicketSettingsId").style.display = "none";
            //document.getElementById("PublishEventId").style.display = "none";


            function BackEventDetailsIdTab() {

                document.getElementById("EventDetailsId").style.display = "block";
                document.getElementById("LocationEventId").style.display = "none";

            }

            function NextLocationTab() {



                


                document.getElementById("EventDetailsId").style.display = "none";


                document.getElementById("LocationEventId").style.display = "block";


               

            }

            function BackLocationEventIdTab() {


                document.getElementById("LocationEventId").style.display = "block";
                document.getElementById("MainEventImageId").style.display = "none";


            }

            function NextMainEventImageTab() {

                document.getElementById("LocationEventId").style.display = "none";
                document.getElementById("MainEventImageId").style.display = "block";


            }

            function BackMainEventImageIdIdTab() {

                document.getElementById("MainEventImageId").style.display = "block";
                document.getElementById("AddTicketsId").style.display = "none";
            }

            function NextAddTicketsIdTab() {

                document.getElementById("MainEventImageId").style.display = "none";
                document.getElementById("AddTicketsId").style.display = "block";

            }

            function BackAddTicketsIdTab() {

                document.getElementById("AddTicketsId").style.display = "block";
                document.getElementById("TicketSettingsId").style.display = "none";

            }

            function NextTicketSettingsIdTab() {

                document.getElementById("AddTicketsId").style.display = "none";
                document.getElementById("TicketSettingsId").style.display = "block";


            }

            function BackTicketSettingsIdTab() {



                document.getElementById("TicketSettingsId").style.display = "block";
                document.getElementById("PublishEventId").style.display = "none";



            }


            function NextPublishEventIdTab() {


                document.getElementById("TicketSettingsId").style.display = "none";
                document.getElementById("PublishEventId").style.display = "block";



            }
           

        </script>
          

       
</asp:Content>
































