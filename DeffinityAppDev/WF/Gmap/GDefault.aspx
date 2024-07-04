<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GDefault.aspx.cs" Inherits="_GDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Route Direction</title>

    <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=geometry"></script>--%>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCDN_UGu8DEEZgb2Y15Wf3BGxSj4EDM7qU&libraries=geometry,places&sensor=false">
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>

    <%--Getting User Current Location--%>

    <script type="text/javascript">
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            alert("There is Some Problem on your current browser to get Geo Location!");
        }

        function success(position) {
            var geocoder = new google.maps.Geocoder();
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
            var city = position.coords.locality;
            var LatLng = new google.maps.LatLng(lat, long);
            var mapOptions = {
                center: LatLng,
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            geocoder.geocode({ 'location': LatLng }, function (results, status) {
                if (status === 'OK') {
                    if (results[1]) {
                        document.getElementById("dist1").innerHTML = results[1].formatted_address;

                    }
                }
            });

           // var map = new google.maps.Map(document.getElementById("MyMapLOC"), mapOptions);
            var marker = new google.maps.Marker({
                position: LatLng,
                title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: "
                            + lat + +"<br />Longitude: " + long
            });

            //marker.setMap(map);
            //var getInfoWindow = new google.maps.InfoWindow({
            //    content: "<b>Your Current Location</b><br/> Latitude:" +
            //                            lat + "<br /> Longitude:" + long + ""
            //});
            //getInfoWindow.open(map, marker);
        }
    </script>


    <%--Getting Route Direction From User Current Location to Destination--%>

    <script type="text/javascript">
        function SearchRoute() {
           // document.getElementById("MyMapLOC").style.display = 'none';

            var markers = new Array();
            var myLatLng;

            //var geocoder = new google.maps.Geocoder();
          
            //geocoder.geocode({ 'address': src }, function (results, status) {
            //    if (status === 'OK') {
            //        var myLatLng = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
            //    }
            //});
          //  Find the current location of the user.
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (p) {
                    var myLatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);

                    var m = {};
                    m.title = "Your Current Location";
                    m.lat = p.coords.latitude;
                    m.lng = p.coords.longitude;
                    markers.push(m);


            //Find Destination address location.
           
                    var address = document.getElementById("txtDestination").value;
                    var splitstr = address.split(/,/);
                    for (var index = 0; index < splitstr.length; ++index) {


                        var geocoder = new google.maps.Geocoder();
                        geocoder.geocode({ 'address': splitstr[index] }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                for (var d = 0; d < index; ++d) {
                                    if (results[0].address_components[0].long_name == splitstr[d]) {

                                        m = {};
                                        m.title = splitstr[d];
                                        m.lat = results[0].geometry.location.lat();
                                        m.lng = results[0].geometry.location.lng();
                                        var des = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());

                                        var dist = (google.maps.geometry.spherical.computeDistanceBetween(myLatLng, des) / 1000).toFixed(2);
                                        if (dist < 10) {
                                            myCreateFunction(splitstr[d],'true')
                                        }
                                        else {
                                            //alert(splitstr[d] + " " + false)
                                            myCreateFunction(splitstr[d], 'false')
                                            //$("#GridView1").append("<tr><td>" + splitstr[d] + "</td><td>'false'</td></tr>");
                                        }

                                    }
                                }
                            } else {
                                alert("Request failed.")
                            }
                        });
                    }
                });
            }
            else {
                alert('some problem in getting geo location.');
                return;
            }
        }
        function myCreateFunction(a, b) {
            var table = document.getElementById("insert");
            var row = table.insertRow(1);
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            cell1.innerHTML = a;
            cell2.innerHTML = b;
        }

    </script>

</head>
<body>

    <form id="form1" runat="server">

        <table>

            <tr>
                <td>
                    <b>Enter Destination:</b>
                     <%--  <input type="text" id="source" value="" style="width: 200px" />--%>
                    <input type="text" id="txtDestination" value="" style="width: 200px" />
                    <input type="button" value="Search Route" onclick="SearchRoute()" />
                      <br />  <br />
                    Current Location :
                    <label id="dist1"></label>
                </td>

            </tr>
            <%--<tr>
                <td>
                    <div id="MyMapLOC" style="width: 1550px; height: 770px">
                    </div>
                    <div id="MapRoute" style="width: 1500px; height: 800px">
                    </div>
                </td>
            </tr>--%>
        </table>

        <br />
        <br />
        
        <table id="insert">
            <tr>
                <th>pin no</th><th> value</th>
            </tr>
        </table>
       <%-- <asp:GridView ID="GridView1" runat="server"></asp:GridView>--%>
        <div id="location" style="float: right; width: 30%; height:100%"></div>
    </form>
</body>
</html>
