<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ResourceSchedular.aspx.cs" Inherits="DeffinityAppDev.WF.DC.ResourceSchedular" EnableEventValidation="false" %>

<%--<%@ Register Src="~/WF/DC/controls/FLSListTabCtrl.ascx" TagPrefix="Pref" TagName="FLSListTabCtrl" %>--%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Dispatch Board
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <%--<Pref:FLSListTabCtrl runat="server" id="FLSListTabCtrl" />--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <span>
    Dispatch Board </span>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a id ="link_return" href="~/WF/DC/DCDashboardListview.aspx?type=FLS" runat="server" target="_self" style="display:none;visibility:hidden;"><%--<i class="fa fa-arrow-left"></i>--%> List View</a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

     <link href="../../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../../assets/plugins/global/plugins.bundle.js"></script>
 <%--   <link href='../../Content/assets/js/fullcalendar/lib/fullcalendar.min.css' rel='stylesheet' />
      <link href='../../Content/assets/js/fullcalendar/lib/fullcalendar.print.css' rel='stylesheet' media='print' />
      <link href='../../Content/assets/js/fullcalendar/lib/scheduler.min.css' rel='stylesheet' />
    <script src='../../Content/assets/js/fullcalendar/lib/moment.min.js'></script>
      <script src='../../Content/assets/js/fullcalendar/lib/fullcalendar.min.js'></script>
      <script src='../../Content/assets/js/fullcalendar/lib/scheduler.min.js'></script>--%>
   <%-- <script src="../../Content/assets/js/daterangepicker/daterangepicker.js"></script>
	<script src="../../Content/assets/js/datepicker/bootstrap-datepicker.js"></script>--%>

 
    <link href="../../assets/plugins/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" />
       <script src="../../assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>
    <link href="../../assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" />
    <script src="../../assets/plugins/custom/datatables/datatables.bundle.js"></script>

     <style type="text/css">
    .lab {
        width: 250px;
        float: left;
        margin-right: 5px;
    }
    .FieldCls {
         width: 250px;
         display:block;
    }
    .TxtCount {
        display:none;
    }
    .auto-style1 {
        width: 10px;
    }
    .btn {
        margin-bottom:0px;
    }

    /*a.fc-timeline-event {
    height: 52px;
}*/

    /*table.dataTable thead tr {
  background-color: #4160a0; /*#40bbea;
  color:#ffffff;
}*/

    table.dataTable tbody th, table.dataTable tbody td {
    padding: 8px 10px;
    max-width: 0;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  text-align:left;
}

    /*td.fc-widget-content div{
        height:70px;
    }*/
</style>

  
<script type="text/javascript"
    src ="https://maps.googleapis.com/maps/api/js?key=AIzaSyC5fVno1A77Hcx9XMr5k070Nm9TwFPEYuM"></script>
 <asp:HiddenField ID="hkey" runat="server" Value="AIzaSyC5fVno1A77Hcx9XMr5k070Nm9TwFPEYuM" />
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
                                url :url
                            },
                            title:  n
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
        for (i in gmarkers)
        {
            var dt = gmarkers[i]
            if (dt.title == title)
            {
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

    function gmapclick(title)
    {
        //debugger;
        var ourMarker = getMarker(title)
        google.maps.event.trigger(ourMarker, 'click');
        //debugger;
    }


</script>
    <%--<a onclick="javasript:myclick('10001')">Click</a>--%>
     <%--<div class="form-group row">
        <div class="col-md-12">
           <strong>Search </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <div class="form-group row" style="visibility:hidden;display:none;">
      <div class="col-md-3">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-12">
                <asp:DropDownList ID="ddlName" runat="server"
                 ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlName"
                BehaviorID="ccdNa" Category="Name" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetNameByCompanySession"
                 LoadingText="[Loading name...]" ClientIDMode="Static" />   
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-12">
               
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                BehaviorID="ccdS" Category="Name" PromptText="All" PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetStatusByFLS"
                 LoadingText="[Loading status...]" ClientIDMode="Static" />             
            </div>
	</div>
	<%--<div class="col-md-3">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
              
            </div>
	</div>--%>
        <div class="col-md-5 form-inline" style="padding-top:25px">
             <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" ClientIDMode="Static" />
                <asp:Button ID="btnClear" runat="server" SkinID="btnClear" ClientIDMode="Static" />
             <asp:HyperLink SkinID="btnLogNewTicket" runat="server" Text="Log New Job" id="Img1" NavigateUrl="~/WF/DC/DCNavigation.ashx?type=FLS" Style="vertical-align:bottom"/>
            </div>
</div>
     <div class="form-group row" style="visibility:hidden;display:none;">
         <div class="col-md-3" style="visibility:hidden;display:none;">
           <label class="col-sm-12 control-label">Town</label>
           <div class="col-sm-12">
               <asp:Panel ID="pnlTowns" runat="server" Width="250px" BorderColor="Silver" BorderWidth="1px"
                            Height="100px" ScrollBars="Auto">
                            <asp:CheckBoxList ID="chkTowns" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>

<%--                <asp:DropDownList ID="ddlTown" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdTown" runat="server" TargetControlID="ddlTown"
                BehaviorID="ccdT" Category="Name" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetTownByCompanySession"
                LoadingText="[Loading town...]" ClientIDMode="Static" />--%>
            </div>
	</div>
         <div class="col-md-3"  style="visibility:hidden;display:none;">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Postcode%></label>
           <div class="col-sm-12">
                <asp:Panel ID="pnlPostcode" runat="server" Width="250px" BorderColor="Silver" BorderWidth="1px"
                            Height="100px" ScrollBars="Auto">
                            <asp:CheckBoxList ID="chkPostcode" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
               <%-- <asp:DropDownList ID="ddlPostcode" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdPostcode" runat="server" TargetControlID="ddlPostcode"
                BehaviorID="ccdP" Category="Name" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetPostcodeByCompanySession"
                 LoadingText="[Loading postcode...]" ClientIDMode="Static" />--%>
            </div>
	</div>
         </div>
   <%-- <div class="form-group row">
     
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
              
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>--%>
   
    
      <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblStorageNew" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="300px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"> Update Scheduled Dates</h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
     <div class="form-group row">
          <div class="col-md-12" >
     
               <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vd" />
              </div>
                   </div>
               <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><asp:Label ID="Label1" runat="server" Text="Job Ref"></asp:Label></label>
           <div class="col-sm-9 form-inline">

               <asp:Label ID="lblJobRef" runat="server" style="vertical-align:super;"></asp:Label>
               </div>
              </div>


</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><asp:Label ID="lblScheduledDateTime" runat="server" Text="Preferred Date / Time"></asp:Label></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtSeheduledDateTime1" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" ClientIDMode="Static"  />
            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgSeheduledDate" TargetControlID="txtSeheduledDateTime1">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="rfvScheduledDate" runat="server" ControlToValidate="txtSeheduledDateTime1"
                Display="None" ErrorMessage="Please enter Preferred date" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid date"
                ControlToValidate="txtSeheduledDateTime1" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                 Display="None" SetFocusOnError="True"></asp:CompareValidator>
          
            <asp:TextBox ID="txtScheduledTime" runat="server" SkinID="Time" ClientIDMode="Static" MaxLength="5"></asp:TextBox>(HH:MM)
                   
                   
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtScheduledTime"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                    </div>
	</div>
</div>
    
    
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">  <asp:Label ID="lblScheduledEndDateTime" Text="Scheduled End Date/Time" runat="server"></asp:Label></label>
           <div class="col-sm-9 form-inline">
              <asp:TextBox ID="txtScheduledEndDateTime1" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>

            <asp:Label ID="ImgScheduledEndDateTime" runat="server" SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="calScheduledEndDateTime" runat="server" CssClass="MyCalendar" 
                                 PopupButtonID="ImgScheduledEndDateTime" TargetControlID="txtScheduledEndDateTime1"></ajaxToolkit:CalendarExtender>
               <asp:TextBox ID="txtScheduledEndTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox>(HH:MM)
                   
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtScheduledEndTime"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
 </div>
	</div>
</div>
   
   
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:HiddenField ID="hbomid" runat="server" Value="0" />
               <asp:Button ID="btnUpdate" runat="server" SkinID="btnSubmit" OnClick="btnUpdateDates_Click" ValidationGroup="fls" />
              
               </div>
              </div>
         </div>

              </div>
         </div>
        </div>
</asp:Panel>
    <div class="form-group row mb-6" >





           <div class="col-md-7">
             
 <div class="row clshistory">
                  <div class="col-md-12">

                   <div style="width:135px;padding-bottom:5px"><asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static" SkinID="ddl_125px">
            </asp:DropDownList></div>  
                      <div class="table-responsive">
        <table id="students" class="table table-striped table-bordered gy-7 gs-7"></table>
                          </div>
    </div>
     </div>

               
               </div>
           <div class="col-md-5">
                 <div class="form-group row" >
                      <div style="width:135px;padding-bottom:5px;visibility:hidden" ><asp:TextBox runat="server" ClientIDMode="Static"  >
            </asp:TextBox></div>  
                     
    <div id="map_canvas" style="width: 99%; height: 415px" ></div>
</div>
               </div>
         </div>
    <div class="form-group row mb-6">
        <div class="col-md-12">
            <div class="form-group row" id="pnldetails" style="padding-right:15px">
        <div class="col-md-12 well">
            <div class="form-group row">
                <div class="col-md-12">
                    <strong>Schedule Details </strong>
                    <hr class="no-top-margin" />
                </div>
            </div>
            <div class="form-group row">
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">Ref:</span></label>
                    <div class="col-sm-9">
                        <asp:Label ID="tref" Text="" ClientIDMode="Static" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">Customer:</span></label>
                    <div class="col-sm-9">
                        <asp:Label ID="title" Text="" ClientIDMode="Static" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">Start Date:</span></label>
                    <div class="col-sm-9 form-inline">
                        <asp:Label ID="sdate" ClientIDMode="Static" Text="" runat="server" Style=" padding-bottom: 10px; vertical-align: super;"></asp:Label>
                        
                    </div>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">End Date:</span></label>
                    <div class="col-sm-9">
                        <asp:Label ID="edate" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">Address:</span></label>
                    <div class="col-sm-9">
                        <asp:Label ID="address" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">Details:</span></label>
                    <div class="col-sm-9">
                        <asp:Label ID="details" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    <label class="col-sm-3" style="font-weight: bold;"><span class="control-label">Assigned technician:</span></label>
                    <div class="col-sm-9">
                        <asp:Label ID="tech" ClientIDMode="Static" Text="" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12">
                    &nbsp;
               
                </div>
                <div class="col-md-12 form-inline">
                    <button class="btn btn-primary" id="btn_etech">Email Tech</button>
                    <button class="btn btn-primary" id="btn_ecustomer">Email Customer</button>
                    <button class="btn btn-primary" id="btn_both">Email Both</button>
                    <asp:Button ID="btnEditDate" runat="server" Text="Reschedule" CssClass="btn btn-primary" OnClick="btnEditDate_Click"></asp:Button>
                </div>
            </div>


        </div>
	
</div>
            </div>
        </div>
    
    
    <div class="form-group row mb-6">
        <div class="col-md-6">
            
            <div id="lblmsg2" class="bg-success"></div>
           <div id="lblmsg1" class="bg-danger"></div>
        <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
            </div>
    </div>
     <div class="form-group row mb-6">
  <div style="width:300px;float:right;padding-left:5px" class="form-inline d-flex d-inline"> <span style="font-weight:bold;padding-top:8px">Filter by <%:sessionKeys.JobDisplayName %> Type:</span>  
      
      <asp:DropDownList ID="ddltype" ClientIDMode="Static" runat="server" SkinID="ddl_125px" ></asp:DropDownList>


  </div>
         </div>
     <%-- <button class="datepicker fc-button fc-state-default fc-corner-left fc-corner-right" id="datepicker1">Change Date</button>--%>
   
  <%--   <div class="datepicker" id=""></div>--%>
  <div id='calendar'></div>

    <div>
        <asp:HiddenField ID="htown" runat="server" ClientIDMode="Static"/>
        <asp:HiddenField ID="hpostcode" runat="server" ClientIDMode="Static"/>
        <asp:HiddenField ID="HFIDnew" runat="server" ClientIDMode="Static"/>
         <asp:HiddenField ID="HFresID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="HFstartdate" runat="server"  ClientIDMode="Static"/>
        <asp:HiddenField ID="HFenddate" runat="server" ClientIDMode="Static"/>
        <asp:HiddenField ID="HFTitle" runat="server" ClientIDMode="Static"/>
        <asp:HiddenField ID="Hrefid" runat="server" ClientIDMode="Static"/>
    </div>


  <style>
      td.New{ 
          background-color:#00B0F0;
          text-align:center;
          vertical-align:middle;
      }
  </style>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
        //setStatusBackColor();
        //grid_responsive_display();
        function setStatusBackColor() {

            
            $('.statuscls').each(function () {

                var s = $(this).html();
                if (s == 'New')
                    $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Cancelled')
                    $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Job Complete')
                    $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Scheduled')
                    $(this).closest("td").css({ "background-color": "#92D050", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Awaiting Schedule')
                    $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Arrived')
                    $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Customer Not Responding')
                    $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                else if (s == ' Feedback Submitted')
                    $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Feedback Received')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Rejected')
                    $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Accepted')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Awaiting Information')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Waiting On Parts')
                    $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Authorised')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Request Feeback')
                    $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Sent')
                    $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
            });


        }

     </script>
  <script type="text/javascript">

      $(document).ready(function () {
          GetAssetRecords(0);
      });
      function GetAssetRecords(id) {
          try {

              var dataObject = JSON.stringify({
                  'id': id,
                  'status': $("[id*=ddlStatus] option:selected").text()
                  
              });

              $.ajax({
                  url: "../../WF/DC/webservices/DCServices.asmx/BindCallAssets",
                  type: "POST",
                  data: dataObject,
                  contentType: 'application/json; charset=utf-8',
                  dataType: "json",
                  async: true,
                  success: function (data) {

                      var NewData = jQuery.parseJSON(data.d);
                      var x = "<thead><tr class='fw-bold fs-6 text-gray-800 border-bottom-2 border-gray-200'><th style='width:5%;'>Event Ref</th><th style='width:5%;'>ID</th>"
                          + "<th  style='width:20%;'>Details</th>"
                          + "<th  style='width:10%;'>Client Name</th>"
                          + "<th  style='width:10%;'>Assigned Sales rep</th>"
                          + "<th  style='width:10%;'>Assigned Smart Tech</th>"
                          + "<th style='width:5%;'>Scheduled Date</th>"
                          + "<th  style='width:10%;'>Status</th>"
                          + "</thead>";
                      x = x + "<tbody>"

                      for (var i = 0; i < NewData.length; i++) {
                          var CCID = NewData[i].CCID
                          var ID = NewData[i].ID
                          var LoggedDate = NewData[i].LoggedDate;
                          var Details = NewData[i].Details;
                          var AssignedTechnician = NewData[i].AssignedTechnician
                          var AssignedSalesRep = NewData[i].AssignedSalesRep
                          var StatusName = NewData[i].StatusName;
                          var ClientName = NewData[i].ClientName;

                          x = x + "<tr><td>" + ButtonHtml(ID, CCID)
                              + "</td><td>" + CCID
                              + "</td><td>" + Details
                              + "</td><td>" + ClientName
                              + "</td><td>" + AssignedSalesRep
                              + "</td><td>" + AssignedTechnician
                              + "</td><td style=direction:rtl>" + LoggedDate
                              + "</td><td class='New'><span class='statuscls' style='color: white;font-weight: bold;'>" + StatusName + "</span></td></tr>";
                      }

                      x = x + "</tbody>";
                      $("#students").empty();
                      $("#students").append(x);
                      BindTable();
                      $("#students").removeClass("no-footer");
                      setStatusBackColor();
                  }
              });
          }
          catch (e) {
              var err = e;
          }
      }
      function BindTable() {
          var table = $('#students').DataTable({
              'Ordering': true,
              "order": [[1, "desc"]],
              'paging': true,
              "pageLength": 10,
              'bFilter': false,
              'lengthChange': false,
              'destroy': true,
              "columnDefs": [{
                  "targets": 0, "orderable": false
              },
              {
                  "targets": 1,
                  "visible": false,
                  "searchable": false
              },
                  //{
                  //    "targets": 7,
                  //    "visible": true
                  //}

              ],
              //"initComplete": function (settings, json) {
              //    setStatusBackColor();
              //}
          });

          //$('#students').on('page.dt', function () {
          //    setStatusBackColor();
          //    //var info = table.page.info();
          //    //$('#pageInfo').html('Showing page: ' + info.page + ' of ' + info.pages);
          //});

          table.on('draw', function () {
              setStatusBackColor();
          });
      }
      //function getQuerystring(stid) {
      //    var stid = Status.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
      //    var regex = new RegExp("[\\?&]" + stid + "=([^&#]*)");
      //    var qs = regex.exec(window.location.href);
      //    if (qs == null)
      //        return default_;
      //    else
      //        return qs[1];
      //}
      //function GetAssetRecords(id) {
      //    try {
      //        $.ajax({
      //            url: "../../WF/DC/webservices/DCServices.asmx/BindCallAssets",
      //            type: "POST",
      //            data: "{'id': '" + id + "'}",
      //            contentType: 'application/json; charset=utf-8',
      //            dataType: "json",
      //            async: true,
      //            success: function (data) {

      //                var NewData = jQuery.parseJSON(data.d);
      //                var x = "<thead><tr><th style='width:8%;'>Job Ref</th><th style='width:5%;'>ID</th>"
      //                    + "<th  style='width:25%;'>Details</th><th  style='width:10%;'>Assigned Smart Tech</th><th style='width:10%;'>Scheduled Date</th><th  style='width:10%;'>Status</th></thead>";
      //                x = x + "<tbody>"

      //                for (var i = 0; i < NewData.length; i++) {
      //                    var CCID = NewData[i].CCID
      //                    var ID = NewData[i].ID
      //                    var LoggedDate = NewData[i].LoggedDate;
      //                    var Details = NewData[i].Details;
      //                    var AssignedTechnician = NewData[i].AssignedTechnician
      //                    var StatusName = NewData[i].StatusName;

      //                    x = x + "<tr><td>" + ButtonHtml(ID, CCID)
      //                        + "</td><td>" + CCID
      //                        + "</td><td>" + Details
      //                        + "</td><td>" + AssignedTechnician
      //                        + "</td><td style=direction:rtl>" + LoggedDate
      //                        + "</td><td class='New'><span class='statuscls' style='color: white;font-weight: bold;'>" + StatusName + "</span></td></tr>";
      //                }

      //                x = x + "</tbody>";
      //                $("#students").empty();
      //                $("#students").append(x);
      //                BindTable();
      //                $("#students").removeClass("no-footer");
      //                setStatusBackColor();
      //            }
      //        });
      //    }
      //    catch (e) {
      //        var err = e;
      //    }
      //}
      //function BindTable() {
      //    var table = $('#students').DataTable({
      //        'Ordering': true,
      //        "order": [[1, "desc"]],
      //        'paging': true,
      //        "pageLength": 10,
      //        'bFilter': false,
      //        'lengthChange': false,
      //        'destroy': true,
      //        "columnDefs": [{
      //            "targets": 0, "orderable": false
      //        },
      //            {
      //                "targets": [1],
      //                "visible": false,
      //                "searchable": false
      //            },
      //            //{
      //            //    "targets": 7,
      //            //    "visible": true
      //            //}

      //        ],
      //        //"initComplete": function (settings, json) {
      //        //    setStatusBackColor();
      //        //}
      //    });

      //    //$('#students').on('page.dt', function () {
      //    //    setStatusBackColor();
      //    //    //var info = table.page.info();
      //    //    //$('#pageInfo').html('Showing page: ' + info.page + ' of ' + info.pages);
      //    //});

      //    table.on('draw', function () {
      //        setStatusBackColor();
      //    });
      //}

      function ButtonHtml(Id, ccid) {
          var HtmlText = " <a target='_blank' id=Link" + Id + " href='/WF/DC/FLSForm.aspx?CCID=" + ccid + "&CallID=" + Id+ "&SDID=" + Id + "' style=' font-weight: bold'>" + ccid + "</a>";
          //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
          return HtmlText;
      }



  </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <%--<script src="../../Content/assets/js/jquery-1.11.1.min.js"></script>--%>
   <%--  <%: System.Web.Optimization.Styles.Render("~/bundles/fullcalendarcss") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/fullcalendar") %>--%>
      <link href='../../Content/assets/js/fullcalendar/lib/fullcalendar.min.css' rel='stylesheet' />
      <link href='../../Content/assets/js/fullcalendar/lib/fullcalendar.print.css' rel='stylesheet' media='print' />
      <link href='../../Content/assets/js/fullcalendar/lib/scheduler.min.css' rel='stylesheet' />
    <script src='../../Content/assets/js/fullcalendar/lib/moment.min.js'></script>
      <script src='../../Content/assets/js/fullcalendar/lib/fullcalendar.min.js'></script>
      <script src='../../Content/assets/js/fullcalendar/lib/scheduler.min.js'></script>
    <script src="../../Content/assets/js/daterangepicker/daterangepicker.js"></script>
    <style>
        /*a.fc-timeline-event{
            height:45px;
        }*/
    </style>
   <script type="text/javascript">
       $(document).ready(function () {
           //ddlStatus
           $("[id*=ddlStatus]").change(function () {
               var c = $("[id*=ddlStatus] option:selected").text();// $(this).text();
               console.log(c);
               GetAssetRecords(0);
              // $('#calendar').fullCalendar('rerenderEvents');
           });
           $("[id*=ddltype]").change(function () {
               var c = $("[id*=ddltype] option:selected").text();// $(this).text();
               console.log(c);

               $('#calendar').fullCalendar('rerenderEvents');
           });
           $('#btnserach').click(function () {
               $('#htown').val(GetTowns());
               $('#hpostcode').val(GetPostCodes());
               GetTotalEvents();
               //alert($('#hexpert').val());
               return false;
           });
       });
       function GetPostCodes()
       {
           var checked_checkboxes = $("[id*=chkPostcode] input:checked");
           var message = "";
           checked_checkboxes.each(function () {
               var value = $(this).val();
               message = message + value + ',';
               // var text = $(this).closest("td").find("label").html();
               //message += "Text: " + text + " Value: " + value;
               //message += "\n";
           });
           return message;
       }
       function GetTowns() {
           var checked_checkboxes = $("[id*=chkTowns] input:checked");
           var message = "";
           checked_checkboxes.each(function () {
               var value = $(this).val();
               message = message + value + ',';
               // var text = $(this).closest("td").find("label").html();
               //message += "Text: " + text + " Value: " + value;
               //message += "\n";
           });
           return message;
       }
    </script>
    <script type="text/javascript">
        Date.prototype.toDDMMYYYYString = function () { return isNaN(this) ? 'NaN' : [this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getFullYear()].join('/') }
        Date.prototype.toMMDDYYYYString = function () { return isNaN(this) ? 'NaN' : [this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getFullYear()].join('/') }
        $('#lblmsg1').hide();
        $('#lblmsg2').hide();
        $('#lblmsg1').html('');
        $('#lblmsg2').html('');
        function DateConvertion(s_date) {

            var b = s_date;
            var temp = new Array();
            temp = b.split('/');
            // var s1 = temp[1] + '/' + temp[0] + '/' + temp[2];
            var s1 = temp[0] + '/' + temp[1] + '/' + temp[2];
            return s1
        }
        $(document).ready(function () {
            $("#pnldetails").hide();
           
            GetTotalEvents();


            $("#btnSearch").click(function (e) {
                //debugger;
                // e.preventdefault();
                GetTotalEvents();

                return false;
            });
            $("#btnClear").click(function (e) {
                e.preventdefault();
                //GetTotalEvents();
            });
            $("#btn_both").click(function (e) {
                //debugger;
                //e.preventdefault();
                //GetTotalEvents();
                SendCustomerMail();
                SendTechMail();
                return false;
            });
            $("#btn_ecustomer").click(function (e) {
                //debugger;
                //e.preventdefault();
                //GetTotalEvents();
                SendCustomerMail();
               
                return false;
            });
            $("#btn_etech").click(function (e) {
               
                //e.preventdefault();
                //GetTotalEvents();
                SendTechMail();
                return false;
            });
          
            //GetTotalResources();
            function GetTotalEvents() {
                $('#calendar').fullCalendar('destroy');
                
                var name = document.getElementById('ddlName');
                var status = document.getElementById('ddlStatus');
                var town = document.getElementById('ddlTown');
                var postcode = document.getElementById('ddlPostcode');
                var n = name.options[name.selectedIndex].innerHTML;
                var s = status.options[status.selectedIndex].innerHTML;
                var t = $('#htown').val(); //town.options[town.selectedIndex].innerHTML;
                var p = $('#hpostcode').val(); //postcode.options[postcode.selectedIndex].innerHTML;

                
                var d = "{name:'" + n + "',status:'" + s + "',town:'"+ t+"',postcode:'"+ p +"'}"
                $.ajax({
                    type: "POST",
                    url: '../DC/Resourceservice.asmx/GetAllEventsScheduled',
                    data: d,//"{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (doc) {
                        var events = [];
                        var resources = [];
                       
                        var obj = $.parseJSON(doc.d);
                        var ev = obj.data;
                        var rv = obj.data2;
                        $.each(ev, function (i, item) {
                            if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
                               // debugger;
                                item.start = item.start.substring(0, 10);
                                item.end = item.end.substring(0, 10);
                            }
                            else {
                                item.start = item.start;// moment(item.start).format('DD/MM/YYYY hh:mm');
                                item.end = item.end;// moment(item.end).format('DD/MM/YYYY hh:mm');
                            }
                        });

                        
                        $(ev).each(function () {
                            events.push({
                                id: $(this).attr('id'),
                                resourceId: $(this).attr('resourceId'),
                                title: $(this).attr('title'),
                                start:  $(this).attr('start'),
                                end:  $(this).attr('end'),
                                tref: $(this).attr('tref'),
                                rname: $(this).attr('rname'),
                                contact: $(this).attr('contact'),
                                address: $(this).attr('address'),
                                details: $(this).attr('details'),
                                backgroundColor: $(this).attr('backcolor'),
                                borderColor: $(this).attr('borderColor'),
                                postCode: $(this).attr('postCode'),
                                status: $(this).attr('status'),
                                spname: $(this).attr('spname'),
                                spcontact: $(this).attr('spcontact'),
                                rtype: $(this).attr('rtype'),
                            });
                        });

                        //$.each(rv, function (i, item) {
                            
                          
                        //    item.title = item.title.append(<img src="../UploadData/Users/ThumbNailsMedium/user_0.png"" alt="Image" height="45px" width="60px">');
                        //        item.end = item.end;
                            
                        //});
                        $(rv).each(function () {
                            resources.push({
                                id: $(this).attr('id'),
                                title: $(this).attr('title')
                            });
                        });

                       
                        $('#calendar').fullCalendar('addEventSource', events);

                        TotalEventsWithCalender(events, resources);
                        $(".fc-timelineDay-button").html('Day');
                        $(".fc-agendaWeek-button").html('Week');
                        $(".fc-today-button").html('Today');
                       // $("#destination").append($("#source"));
                       // $(".fc-today-button").after($("#datepicker1"));
                    }
                });
            }

            function GetTotalResources() {
                var resources = [];
                //debugger;
                $.ajax({
                    type: "POST",
                    url: '../DC/Resourceservice.asmx/GetAllResources',
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    Async: true,
                    success: function (doc) {

                        //debugger;
                        var obj = $.parseJSON(doc.d);
                        //debugger;
                        $(obj).each(function () {
                            resources.push({
                                id: $(this).attr('id'),
                                title: $(this).attr('title'),
                            });
                        });
                        //  $('#calendar').fullCalendar('addResources', resources);
                        //TotalEventsWithCalender(resources);

                    }
                });

                return resources;
            }

            function TotalEventsWithCalender(events, resources) {
                $('#calendar').fullCalendar('destroy');
                //alert($('#datepicker1').datepicker("getDate"));
                $('#calendar').fullCalendar({
                    //defaultDate: new Date(),
                    schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
                    //defaultDate: $('#datepicker1').datepicker("getDate"),
                    editable: true,
                    height: 'auto',
                    scrollTime: '00:00',
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'timelineDay,agendaWeek,year'
                    },
                    defaultView: 'timelineDay',
                     //views: {
                    //    timelineDay:  {
                           
                    //		buttonText: ':15 slots',
                    //		slotDuration: '00:15',
                    //	},
                    	//today: {
                    	//	type: 'today',
                    	//	//duration: { days: 10 }
                    	//}
                   // },
                    events: events,
                    resourceAreaWidth: '20%',
                    resourceLabelText: 'Resource',
                    resources: resources,
                    viewRender: function(view, element) {
                      
                       DispImages();
                        ClearAll();
                    },
                    eventRender: function (event, element) {
                        debugger;
                        //<br><span class='fc-title'>No:" + event.contact + "</span>" +
                        element.find('.fc-title').after("<br><span class='fc-title'>Ref:" + event.tref + "</span>" + "<br><span class='fc-title'>Status:" + event.status + "</span>" + "<br><span class='fc-title'>Type:" + event.rtype +"</span>");
                        //element.append('<img style=color:red src="../UploadData/Users/ThumbNailsMedium/user_0.png" alt="Image" height="25px" width="30px"</img>');

                        var show_type, show_calendar = true;
                        var c = $("[id*=ddltype] option:selected").text();// $(this).text();
                        if (c != "All") {
                            show_type = c.indexOf(event.rtype) >= 0;
                        }

                        return show_type;

                    },
                    eventClick:function(event)
                    {
                        $('#lblmsg1').html('');
                        $('#lblmsg2').html('');
                        myclick(event.postCode);
                        debugger;
                       // google.maps.event.trigger(markers[0], 'click');
                        $("#HFIDnew").val(event.id);
                        //if (confirm("Do you want to delete ?")) {
                        //    DeletefullCalander();
                        //}
                      //  var etitle = "Title:" + event.title
                      ///  var estart = "Start Date:" + event.start.format('DD-M-YYYY hh:mm');
                     //   var estart = "Start Date:" + event.start.format('DD-M-YYYY hh:mm');
                        //debugger;
                        $("#HFTitle").val(event.title);
                        //$("#HFstartdate").val(event.start.format('M-DD-YYYY hh:mm'));
                        //$("#HFenddate").val(event.end.format('M-DD-YYYY hh:mm'));
                        $("#HFstartdate").val(moment(event.start).format('MM/DD/YYYY'));
                        $("#HFenddate").val(moment(event.end).format('MM/DD/YYYY'));
                        $("#Hrefid").val(event.tref);
                        DisplayEventDetails(event);
                    },
                    eventResize: function (event) {
                        //alert('event resize');
                      //  alert(event.title + " end is now " + event.end.format() + "event Id:" + event.id+"res Id"+event.resourceId+" from resize");
                        //$("#HFstartdate").val(event.start.format());
                        //$("#HFenddate").val(event.end.format());
                        //$("#HFIDnew").val(event.id);
                        //$("#HFresID").val(event.resourceId);
                        //$("#Hrefid").val(event.tref);
                        var BtnText = $(".fc-state-active").text();
                        //UpdateCalEvent();

                        
                        //alert(event.title + " end is now " + event.end.format() + "event Id:" + event.id+"res Id"+event.resourceId);
                        //debugger;
                        $("#HFstartdate").val(event.start.format('M-DD-YYYY HH:mm'));
                        $("#HFenddate").val(event.end.format('M-DD-YYYY HH:mm'));
                        $("#HFIDnew").val(event.id);
                        $("#HFresID").val(event.resourceId);
                        $("#Hrefid").val(event.tref);
                        var s = event.status;
                        debugger;
                        if (s == 'Scheduled') {
                            UpdateCalEvent();
                            return false;
                        }
                        else {
                            $('#lblmsg1').html('Scheduled slot cannot be moved').fadeIn('fast').delay(5000).fadeOut('slow');
                            revertFunc();
                            return false;
                        }
                    },

                    eventDrop: function (event, dayDelta, minuteDelta, allDay, revertFunc) {
                        $('#lblmsg1').html('');
                        $('#lblmsg2').html('');
                        //alert(event.title + " end is now " + event.end.format() + "event Id:" + event.id+"res Id"+event.resourceId);
                        //debugger;
                        $("#HFstartdate").val(event.start.format('M-DD-YYYY HH:mm'));
                        $("#HFenddate").val(event.end.format('M-DD-YYYY HH:mm'));
                        $("#HFIDnew").val(event.id);
                        $("#HFresID").val(event.resourceId);
                        $("#Hrefid").val(event.tref);
                        var s = event.status;
                        //debugger;
                       <%-- if (s == 'Scheduled') {
                            UpdateCalEvent();
                        }
                        else {
                            $('#<%=lblmsg.ClientID%>').html('Scheduled slot cannot be moved');
                            revertFunc();
                            //return false;
                            
                        }--%>
                        if (s == 'Scheduled') {
                            UpdateCalEvent();
                        }
                        else {
                            $('#lblmsg1').html('Scheduled slot cannot be moved').fadeIn('fast').delay(5000).fadeOut('slow');
                            revertFunc();
                            return false;
                        }
                      

                    },
                });
              //  DispImages();
            }

            function revertFunc() {
                return false;
            }
            function DispImages() {
               // alert("inside images");
                var res = $('#calendar').fullCalendar('getResources');
                for (var i = 0; i < res.length; i++) {
                    var resid = (res[i].id);
                    var element = $(".fc-body tr").find("[data-resource-id='" + resid + "']");
                    //alert(element.attr('data-resource-id'));
                    if (resid == element.attr('data-resource-id')) {
                        var x = element.find('.fc-cell-text img');
                        //debugger;
                        if (x.length == 0) {
                            element.find('.fc-cell-text').prepend('<img src="../Admin/ImageHandler.ashx?type=user&id=' + element.attr('data-resource-id') + '" alt="" height="45px"> ');
                        }
                    }

                }
            }

            function DisplayEventDetails(event)
            {
                $("#pnldetails").show();
                $("#tref").html((event.tref));
                $("#title").html( event.title);
                //$("#sdate").html( event.start.format('M/DD/YYYY hh:mm'));
                //$("#edate").html(event.end.format('M/DD/YYYY hh:mm'));
                $("#sdate").html(moment(event.start).format('MM/DD/YYYY'));
                $("#edate").html(moment(event.end).format('MM/DD/YYYY'));
                $("#address").html( event.address);
                $("#details").html(event.details);
                $("#tech").html(event.spname + ' (' + event.spcontact+')');
                $("#Hrefid").val(event.tref);
               // $("#Label1").text("Schedule");
             //   ClearAll();
            }

        });


      </script>
   
    <script type="text/javascript">
       
        function UpdateCalEvent() {
            debugger;
            var ID = $("#<%=HFIDnew.ClientID%>").val();
            var StartDate = $("#<%=HFstartdate.ClientID%>").val();
            var EndtDate = $("#<%=HFenddate.ClientID%>").val();
            var ResId = $("#<%=HFresID.ClientID%>").val();
            var callid = $("#<%=Hrefid.ClientID%>").val();
            //debugger;
            //  alert(ID + " " + ResID);
            //debugger;
            $.ajax({
                url: '../DC/ResourceService.asmx/UpdateResEvent',
                type: 'POST',
                data: "{'ID': '" + ID + "','StartDate': '" + StartDate + "','EndtDate': '" + EndtDate + "','ResId': '" + ResId + "','callid': '" + callid + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                Async: true,
                success: function (data) {
                    ClearAll();
                }
            });
        }
            function ClearAll() {

                $("#HFenddate").val('');
                $("#HFstartdate").val('');
                $("#HFresID").val(''); 
                $("#HFIDnew").val('');
                $("#HFTitle").val('');
                $("#title").text('');
                $("#sdate").text('');
                $("#edate").text('');
                $("#details").text('');
                $("#address").text('');
                $("#Label1").text('');
                $("#Hrefid").val('');
            }

            function DeletefullCalander() {
                var id = $("#<%=HFIDnew.ClientID%>").val();

                $.ajax({
                    url: '../DC/ResourceService.asmx/DeleteEvent',
                    type: 'POST',
                    data: "{'id': '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        $("#calendar").fullCalendar("removeEvents", id);
                        ClearAll();
                        //var errmsg = data.d;
                        //if (errmsg == 1) {
                        //    $("#modal_dialog").dialog('close');
                        //    $('#calendar').fullCalendar('removeEvents', id);
                        //}
                        //else {
                        //    $("#lblerrmsg").html('Can not be deleted. Dependency exist');
                        //    // alert("can 't be deleted")
                        //}
                        // GetTotalEvents();
                        //page reload
                    }
                });
            };
        function SendCustomerMail() {
            var id = $("#<%=HFIDnew.ClientID%>").val();
            //debugger;
            $.ajax({
                url: '../DC/ResourceService.asmx/SendCustomerMail',
                type: 'POST',
                data: "{'id': '" + id + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: true,
                success: function (data) {
                    //debugger;
                    $('#lblmsg2').show();
                    $('#lblmsg2').html('Mail has been sent successfully').fadeIn('fast').delay(5000).fadeOut('slow');;
                   // $("#calendar").fullCalendar("removeEvents", id);
                    //ClearAll();
                    //var errmsg = data.d;
                    //if (errmsg == 1) {
                    //    $("#modal_dialog").dialog('close');
                    //    $('#calendar').fullCalendar('removeEvents', id);
                    //}
                    //else {
                    //    $("#lblerrmsg").html('Can not be deleted. Dependency exist');
                    //    // alert("can 't be deleted")
                    //}
                    // GetTotalEvents();
                    //page reload
                }
            });
        };
        function SendTechMail() {
            var id = $("#<%=HFIDnew.ClientID%>").val();
            //debugger;
            $.ajax({
                url: '../DC/ResourceService.asmx/SendTechMail',
                type: 'POST',
                data: "{'id': '" + id + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: true,
                success: function (data) {
                    //debugger;
                    $('#lblmsg2').show();
                    $('#lblmsg2').html('Mail has been sent successfully').fadeIn('fast').delay(5000).fadeOut('slow');;
                    // $("#calendar").fullCalendar("removeEvents", id);
                    //ClearAll();
                    //var errmsg = data.d;
                    //if (errmsg == 1) {
                    //    $("#modal_dialog").dialog('close');
                    //    $('#calendar').fullCalendar('removeEvents', id);
                    //}
                    //else {
                    //    $("#lblerrmsg").html('Can not be deleted. Dependency exist');
                    //    // alert("can 't be deleted")
                    //}
                    // GetTotalEvents();
                    //page reload
                }
            });
        };
    </script>
    <script type="text/javascript">
        $("input").change(function(){
            var date1 = document.getElementById("txtdate").value;
            $('#calendar').fullCalendar('gotoDate', date1);
        });
           
        // $(document).ready(function () {
        //$("#datepicker").select(function(){
        //    $("#datepicker").datepicker({
        //        onSelect: $(function () {
        //            var selecteddate = $("#datepicker").datepicker("getDate");
        //        })
        //    });
        //});

        //$(document).ready(function () {
        //    $(function () {
        //        $("#datepicker").datepicker();
        //        $("#datepicker").select(function () {
        //            var selected = $(this).val();
        //            //alert(selected);
        //        });
        //    });
        //});
        </script>
    <script>
        $(document).ready(function () {
            //$('#datepicker1').datepicker().on('changeDate', function (e) {
            //    $('#calendar').fullCalendar('gotoDate', $('#datepicker1').datepicker("getDate"))
            //});
        });
       
        //setStatusBackColor()
    </script>

    
 
 <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
   <%-- <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">--%>
  <%--  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/fixedcolumns/3.2.4/css/fixedColumns.dataTables.min.css">--%>

  <%--  <link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">--%>
     
<%--    <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>--%>
   <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

    </script>
     
    <%--<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

    </script>
     <script type="text/javascript"  src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
    </script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>
    
     <script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>
    <script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.4.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.2.2/js/dataTables.select.min.js">
    </script>
<script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.4/js/dataTables.fixedColumns.min.js">
    </script>
    <script type="text/javascript" src="/web/js/dataTables.editor.min.js">
    </script>--%>
   
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
    }
        div.dt-buttons{
            float:right;
        }
    </style>

    <script type="text/javascript">
        hidetabs();
    </script>

   
</asp:Content>
