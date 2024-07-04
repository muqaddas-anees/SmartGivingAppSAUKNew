<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="BasicInfo.aspx.cs" Inherits="DeffinityAppDev.App.Events.BasicInfo" %>

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




                <div class="card card-xl-stretch mb-5">
                    <!--begin::Header-->



                    <div id="kt_content_container" class="container-xxl">



                        <div id="EventDetailsId" style="display: block">

                            <div class="card mb-5 mb-xl-1">
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

                                               <asp:TextBox ID="txtEventName" runat="server" placeholder="Enter name" />

                                     <asp:RequiredFieldValidator  style="font-size:small" ID="Rfv1" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Please enter Event name" ControlToValidate="txtEventName" ValidationGroup="group1" ></asp:RequiredFieldValidator>

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
                                                   <%-- <input type="date" id="Date14" runat="server" name="company" class="form-control form-control-lg" placeholder="Start Date" />--%>
                                                    <asp:TextBox ID="txtStartDate" runat="server" SkinID="DateNew" Style="width:175px"  placeholder="dd/mm/yyyy"></asp:TextBox>
                                                    <asp:CompareValidator 
    ID="cpStartDate" 
    runat="server" 
    ControlToValidate="txtStartDate" 
    Operator="GreaterThanEqual" 
    Type="Date" 
    ErrorMessage="Please enter the current date or a future date" Display="Dynamic" style="font-size:small" ValidationGroup="group1" />
                                                </div>

                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">Start  time</h5>
                                                    <%--<input type="time" id="Time13" runat="server" name="company" class="form-control form-control-lg" placeholder="Start time" />--%>
                                                    <asp:TextBox ID="txtStartTime" runat="server" SkinID="TimeNew"  Style="width:150px" placeholder="hh:mm"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row mb-6 ms-0">
                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">End Date </h5>
                                                    <%--<input type="date" id="Date15" runat="server" name="company" class="form-control form-control-lg" placeholder="End Date" />--%>
                                                     <asp:TextBox ID="txtEndDate" runat="server" SkinID="DateNew" Style="width:175px"  placeholder="dd/mm/yyyy"></asp:TextBox>
                                                      <asp:CompareValidator 
    ID="cpEndDate" 
    runat="server" 
    ControlToValidate="txtEndDate" 
    Operator="GreaterThanEqual" 
    Type="Date" 
    ValueToCompare="<%= DateTime.Today.ToShortDateString() %>" 
    ErrorMessage="Please enter the current date or a future date" Display="Dynamic" style="font-size:small" ValidationGroup="group1" />

                                                    <asp:CompareValidator 
    ID="CompareDatesValidator" 
    runat="server" 
    ControlToValidate="txtEndDate" 
    ControlToCompare="txtStartDate" 
    Operator="GreaterThanEqual" 
    Type="Date" 
    ErrorMessage="Please check end date should be greater than or equal to the start date" style="font-size:small" ValidationGroup="group1" />

                                                </div>


                                                <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                                    <h5 class="fw-bolder m-0">End time </h5>
                                                   <%-- <input type="time" id="Time14" runat="server" name="company" class="form-control form-control-lg" placeholder="End time" />--%>
                                                     <asp:TextBox ID="txtEndTime" runat="server" SkinID="TimeNew" Style="width:150px" placeholder="hh:mm"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!--end::Col-->
                                        </div>

                                    </div>
                                </div>


                             






                            </div>


                            


                        </div>



                      


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

                                      <div class="form-group row mb-6">
                       <%-- <div id="map_canvas" style="width: 99%; height: 350px"></div>--%>
                       <%-- <script type="text/javascript"
                            src="https://maps.googleapis.com/maps/api/js?key=<%=hkey.Value %>"></script>--%>
                        <asp:HiddenField ID="hkey" runat="server" />
                       <%-- <script type="text/javascript">
                            $(document).ready(function () {
                                initialize();
                                //WeatherInitialize();
                            });
                            
                            
                            function getParameterByName(name, url) {
                                if (!url) {
                                    url = window.location.href;
                                }
                                name = name.replace(/[\[\]]/g, "\\$&");
                                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                                    results = regex.exec(url);
                                if (!results) return null;
                                if (!results[2]) return '';
                                return decodeURIComponent(results[2].replace(/\+/g, " "));
                            }
                            function initialize() {

                                var foo = getParameterByName('callid');

                                try {
                                    var markers = JSON.parse('<%=GetAllPincodesOfRequester() %>');

                                    if (markers[0] != null) {
                                        var mapOptions = {
                                            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                                            zoom: 16,
                                            mapTypeId: google.maps.MapTypeId.ROADMAP
                                            //  marker:true
                                        };

                                        var infoWindow = new google.maps.InfoWindow();
                                        var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

                                        for (i = 0; i < markers.length; i++) {
                                            var data = markers[i]
                                           
                                            if (data.lat != "") {
                                                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                                                var marker = new google.maps.Marker({
                                                    position: myLatlng,
                                                    map: map,
                                                    //icon: 'http://localhost:55580/WF/Admin/ImageHandler.ashx?type=user&id=' + data.Id,
                                                    title: data.title
                                                });
                                                (function (marker, data) {

                                                    // Attaching a click event to the current marker
                                                    //google.maps.event.addListener(marker, "click", function (e) {
                                                    //    infoWindow.setContent('<img src="../../WF/Admin/ImageHandler.ashx?v=1&type=user&id='
                                                    //        + data.Id + '" style="height:30px;" /> ' + data.description);
                                                    //    infoWindow.open(map, marker);
                                                    //});
                                                    google.maps.event.addListener(marker, "click", function (e) {
                                                        infoWindow.setContent( data.description);
                                                        infoWindow.open(map, marker);
                                                    });
                                                })(marker, data);
                                            }


                                        }
                                    }
                                    else {

                                        defaultMap();
                                    }
                                }
                                catch (err) {

                                    var map = new google.maps.Map(document.getElementById("map_canvas"));
                                }







                            }


                            function defaultMap() {

                                var mapOptions = {
                                    center: new google.maps.LatLng(40.7128, -74.0060),
                                    zoom: 6,
                                    mapTypeId: google.maps.MapTypeId.ROADMAP
                                }
                                var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);


                            }
                        </script>--%>
                    </div>

                                    <div class="col-lg-8 fv-row fv-plugins-icon-container">
                                        <h5 class="fw-bolder m-0">Venue Name  </h5>
                                        <input type="text" name="company" class="form-control form-control-lg" placeholder="Venue Name " id="txtvenuename" runat="server" />
                                        <div class="fv-plugins-message-container invalid-feedback"></div>
                                    </div>

                                    <div class="row mb-6 ms-0">
                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Address 1</h5>
                                            <input type="text"  name="company" class="form-control form-control-lg" placeholder="Address 1"  id="txtAddress1" runat="server" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>


                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Address 2 </h5>
                                            <input type="text" id="txtAddress2" runat="server" name="company" class="form-control form-control-lg" placeholder="Address 2" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>
                                    </div>


                                    <div class="row mb-6 ms-0">
                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">City </h5>
                                            <input type="text" id="txtCity" runat="server" name="company" class="form-control form-control-lg" placeholder="City" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>


                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Town </h5>
                                            <input type="text" id="txtState" runat="server" name="company" class="form-control form-control-lg" placeholder="Town" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>

                                       
                                    </div>



                                    <div class="row mb-6 ms-0">
                                           <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Postcode  </h5>
                                            <input type="text" id="txtZipcode" runat="server" name="company" class="form-control form-control-lg" placeholder="Postcode" />
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>
                                        <div class="col-lg-4 fv-row fv-plugins-icon-container">
                                            <h5 class="fw-bolder m-0">Country </h5>
                                           <asp:DropDownList ID="ddlCountry" runat="server" SkinID="ddl_90"></asp:DropDownList>
                                            <div class="fv-plugins-message-container invalid-feedback"></div>
                                        </div>


                                      

                                    </div>
                                </div>

                                <div class="card-footer d-flex justify-content-end py-6 px-9">              
                                   

                                 <%--  <input type="button" class="btn btn-primary" style="color: black; background-color: aliceblue" onclick="BackEventDetailsIdTab()" value="Back" vis />
                                     &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    <asp:Button ID="btnSaveBasic" runat="server" SkinID="btnSave" OnClick="btnSaveBasic_Click" ValidationGroup="group1"></asp:Button>
                                  

                                </div>


                            </div>

</div>
                    </div>
                </div>
            </div>
     
    </div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
