<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TurbProWeb.WebForm1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
     <div id="map_canvas" style="width: 99%; height: 415px" ></div>
  
<script type="text/javascript"
    src ="https://maps.googleapis.com/maps/api/js?key=AIzaSyC5fVno1A77Hcx9XMr5k070Nm9TwFPEYuM"></script>
 <asp:HiddenField ID="hkey" runat="server" Value="AIzaSyC5fVno1A77Hcx9XMr5k070Nm9TwFPEYuM" />
<script type="text/javascript">
    $(document).ready(function () {
        initialize();
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
    var gmarkers = [];
    var markers = [];
    function initialize() {

        var foo = getParameterByName('callid');

        try {
            markers = JSON.parse('<%=GetAllPincodesOfRequester() %>');
            // debugger;
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
                    //debugger;
                    if (data.lat != "") {
                        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                        //debugger;


                        let url = "https://maps.google.com/mapfiles/ms/icons/";
                        let n = "";
                        if (data.color == 'green') {
                            url += data.color + "-dot.png";
                            n = data.name + " " + data.title;
                        }
                        else {
                            url += "red-dot.png";
                            n = data.jobref + " " + data.name + " " + data.title;
                        }
                        //debugger;
                        var marker = new google.maps.Marker({
                            position: myLatlng,
                            map: map,
                            //icon: 'https://localhost:55580/WF/Admin/ImageHandler.ashx?type=user&id=' + data.Id,
                            icon: {
                                url: url
                            },
                            title: n
                        });

                        (function (marker, data) {

                            // Attaching a click event to the current marker
                            google.maps.event.addListener(marker, "click", function (e) {
                                infoWindow.setContent('<img src="../../WF/Admin/ImageHandler.ashx?v=1&type=' + data.imgtype + '&id='
                                    + data.cid + '" style="height:30px;" /> ' + data.description);
                                infoWindow.open(map, marker);
                            });
                        })(marker, data);

                        gmarkers.push(marker);
                    }


                }
            }
            else {

                defaultMap();
            }
        }
        catch (err) {
            //debugger;
            var map = new google.maps.Map(document.getElementById("map_canvas"));
        }

    }

    function myclick(title) {
        //(function (marker, data) {

        //    // Attaching a click event to the current marker
        //    google.maps.event.addListener(marker, "click", function (e) {
        //        infoWindow.setContent('<img src="../../WF/Admin/ImageHandler.ashx?v=1&type=user&id='
        //                            + data.Id + '" style="height:30px;" /> ' + data.description);
        //        infoWindow.open(map, marker);
        //    });
        //})(marker, data);
        for (i in gmarkers) {
            var dt = gmarkers[i]
            if (dt.title == title) {
                google.maps.event.trigger(dt, "click");
            }
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

    function gmapclick(title) {
        //debugger;
        var ourMarker = getMarker(title)
        google.maps.event.trigger(ourMarker, 'click');
        //debugger;
    }


</script>
    </form>
</body>
       
</html>
