<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" CodeBehind="ShowMap.aspx.cs" Inherits="DeffinityAppDev.WF.DC.ShowMap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">
                        </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a id ="link_return" href="~/WF/Portal/Home.aspx" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
                     <div class="row ">
                <div class="col-md-12">
                    <div class="form-group row">
                        <div id="map_canvas" style="width: 98%; height: 450px"></div>
                        <script type="text/javascript"
                            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDTG2gmhucQQb4b76th6dCz8H07_jBJap8"></script>
                         <asp:HiddenField ID="hkey" runat="server" Value="AIzaSyDTG2gmhucQQb4b76th6dCz8H07_jBJap8" />
                        </div>
                    </div>
                         </div>
        
    <script>
        $(document).ready(function () {
            initialize();
           
        });
        function initialize() {

            //var foo = getParameterByName('callid');

            try {
                var markers = JSON.parse('<%=GetAllPincodesOfRequester() %>');

                if (markers[0] != null) {
                    var mapOptions = {
                        center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                        zoom: 6,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                        //  marker:true
                    };

                    var infoWindow = new google.maps.InfoWindow();
                    var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

                    for (i = 0; i < markers.length; i++) {
                        var data = markers[i]
                        debugger;
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
                                google.maps.event.addListener(marker, "click", function (e) {
                                    infoWindow.setContent('<img src="../../WF/Admin/ImageHandler.ashx?v=1&type=user&id='
                                        + data.Id + '" style="height:30px;" /> ' + data.description);
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
                center: new google.maps.LatLng(51.5, -0.12),
                zoom: 6,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);


        }
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
