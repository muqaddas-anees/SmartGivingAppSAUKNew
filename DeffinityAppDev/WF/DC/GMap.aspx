<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GMap.aspx.cs" Inherits="DeffinityAppDev.WF.DC1.GMap1" %>


<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<title> Call Status </title>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    
<link href="../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
	<script src='<%:ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
    <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }


    </style>
    <style>
   .ralert-success {
    background-color: #8dc63f;
    border-color: #8dc63f;
    color: #ffffff;
}

.ralert {
    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
    border-radius: 0px;
}
</style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
</head>
<body class="page-body">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
<form id="form2" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
           <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
    <div class="card shadow-sm">
						<div class="card-header">
							Map
						</div>
        <div class="panel-body">
              <div class="form-group row" >
    <div id="map_canvas" style="width: 99%; height: 400px" ></div>
</div>
<script type="text/javascript"
    src ="https://maps.googleapis.com/maps/api/js?key=AIzaSyDTG2gmhucQQb4b76th6dCz8H07_jBJap8"></script>
 
<script type="text/javascript">
    $(document).ready(function () {
        initialize();
    });

    function getbuttonid(btn) {

        var v = $(btn).attr("value");
        //onwaymail
        //alert($(btn).attr("value"));
        url = '/WF/DC/FLSJlist.aspx/onwaymail';
        data = "{ticketno:'" + v + "'}";
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: OnCheckUserNameAvailabilitySuccess,
            error: OnCheckUserNameAvailabilityError
        });
        function OnCheckUserNameAvailabilitySuccess(response) {

            //$("#lblMessage").text("Configuration saved successfully");
        }
        function OnCheckUserNameAvailabilityError(xhr, ajaxOptions, thrownError) {
            //alert(xhr.statusText);
        }
        return false;
    }
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
            var markers = JSON.parse('<%=GetAllPincodesOfRequester(QueryStringValues.CallID) %>');
            debugger;
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
            debugger;
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

            </div>
        </div>

    

<div class="data_carrier" style="color:gray;">


<uc1:Footer ID="ctrl_footer" runat="server" />
</div>

</form>
        </div>
      
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
    <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
  
	 <script type="text/javascript">
	     $(window).load(function () {
	         
	        // disableAutofill();
	     });
	     //if (navigator.userAgent.toLowerCase().indexOf("chrome") >= 0 || navigator.userAgent.toLowerCase().indexOf("safari") >= 0) {
	     //}
	 </script>
</body>
</html>

