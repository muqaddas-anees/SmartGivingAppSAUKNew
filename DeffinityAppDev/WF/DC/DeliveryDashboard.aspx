<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DeliveryDashboard.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DeliveryDashboard" %>

<%@ Register Src="~/WF/DC/controls/FLSListTabCtrl.ascx" TagPrefix="Pref" TagName="FLSListTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Delivery Scheduler
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:FLSListTabCtrl runat="server" id="FLSListTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="form-group row">
      <div class="col-md-6 well">
          <div class="form-group row">
        <div class="col-md-12">
           <strong>Schedule </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
       <div class="form-group row">
             <div class="col-sm-6">
                 <asp:Label ID="tref" Text="" ClientIDMode="Static" runat="server"></asp:Label><br />
                 <asp:Label ID="title" Text="" ClientIDMode="Static" runat="server"></asp:Label><br />
                 <asp:Label ID="sdate" ClientIDMode="Static" Text="" runat="server"></asp:Label><br />
                  <asp:Label ID="edate" ClientIDMode="Static" Text="" runat="server"></asp:Label>
        </div>

           </div>
        
         
         </div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
        <%--<input type="text" id="txtdate"  value="2016-11-08" class="form-control datepicker" data-format="yyyy-mm-dd ">--%>
           <div class="col-sm-9">
               <div class="datepicker" id="datepicker1"></div>
               
            </div>
	</div>
</div>
    
  <div id='calendar'></div>

    <div>
        <asp:HiddenField ID="HFIDnew" runat="server" ClientIDMode="Static"/>
         <asp:HiddenField ID="HFresID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="HFstartdate" runat="server"  ClientIDMode="Static"/>
        <asp:HiddenField ID="HFenddate" runat="server" ClientIDMode="Static"/>
        <asp:HiddenField ID="HFTitle" runat="server" ClientIDMode="Static"/>
    </div>
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="Scripts_Section" runat="server">
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
	<script src="../../Content/assets/js/datepicker/bootstrap-datepicker.js"></script>
    <style>
        a.fc-timeline-event{
            height:45px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            GetTotalEvents();
            //GetTotalResources();
            function GetTotalEvents() {
                debugger;
                $.ajax({
                    type: "POST",
                    url: '../DC/Resourceservice.asmx/GetAllEventsScheduled',
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (doc) {
                        var events = [];
                        var resources = [];
                        debugger;
                        var obj = $.parseJSON(doc.d);
                        var ev = obj.data;
                        var rv = obj.data2;
                        $.each(ev, function (i, item) {
                            if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
                                item.start = item.start.substring(0, 10);
                                item.end = item.end.substring(0, 10);
                            }
                            else {
                                item.start = item.start;
                                item.end = item.end;
                            }
                        });

                        debugger;
                        $(ev).each(function () {
                            events.push({
                                id: $(this).attr('id'),
                                resourceId: $(this).attr('resourceId'),
                                title: $(this).attr('title'),
                                start: $(this).attr('start'),
                                end: $(this).attr('end'),
                                tref: $(this).attr('tref'),
                                rname: $(this).attr('rname'),
                                contact: $(this).attr('contact')
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

                    }
                });
            }

            function GetTotalResources() {
                var resources = [];
                debugger;
                $.ajax({
                    type: "POST",
                    url: '../DC/Resourceservice.asmx/GetAllResources',
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    Async: true,
                    success: function (doc) {

                        debugger;
                        var obj = $.parseJSON(doc.d);
                        debugger;
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
                //alert($('#datepicker1').datepicker("getDate"));
                $('#calendar').fullCalendar({
                    //defaultDate: new Date(),
                    schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
                    defaultDate: $('#datepicker1').datepicker("getDate"),
                    editable: true,
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
                        element.find('.fc-title').after("<br><span>no:" + event.contact + "</span>" + "<br><span>ref:" + event.tref + "</span>");
                        //element.append('<img style=color:red src="../UploadData/Users/ThumbNailsMedium/user_0.png" alt="Image" height="25px" width="30px"</img>');
                    },
                    eventClick:function(event)
                    {
                        $("#HFIDnew").val(event.id);
                        //if (confirm("Do you want to delete ?")) {
                        //    DeletefullCalander();
                        //}
                      //  var etitle = "Title:" + event.title
                      ///  var estart = "Start Date:" + event.start.format('DD-M-YYYY hh:mm');
                     //   var estart = "Start Date:" + event.start.format('DD-M-YYYY hh:mm');
                        debugger;
                        $("#HFTitle").val("Title: " + event.title);
                        $("#HFstartdate").val("Startdate: " + event.start.format('DD-M-YYYY hh:mm'));
                        $("#HFenddate").val("Enddate:" +event.end.format('DD-M-YYYY hh:mm'));
                        DisplayEventDetails(event);
                    },
                    eventResize: function (event) {

                      //  alert(event.title + " end is now " + event.end.format() + "event Id:" + event.id+"res Id"+event.resourceId+" from resize");
                        $("#HFstartdate").val(event.start.format());
                        $("#HFenddate").val(event.end.format());
                        $("#HFIDnew").val(event.id);
                        $("#HFresID").val(event.resourceId);
                        var BtnText = $(".fc-state-active").text();
                        UpdateCalEvent();

                    },

                    eventDrop: function (event, dayDelta, minuteDelta, allDay, revertFunc) {
                        //alert(event.title + " end is now " + event.end.format() + "event Id:" + event.id+"res Id"+event.resourceId);
                        debugger;
                        $("#HFstartdate").val(event.start.format());
                        $("#HFenddate").val(event.end.format());
                        $("#HFIDnew").val(event.id);
                        $("#HFresID").val(event.resourceId);
                        UpdateCalEvent();

                    },
                });
              //  DispImages();
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
                        debugger;
                        if (x.length == 0) {
                            element.find('.fc-cell-text').prepend('<img src="../Admin/ImageHandler.ashx?type=user&id=' + element.attr('data-resource-id') + '" alt="" height="45px"> ');
                        }
                    }

                }
            }

            function DisplayEventDetails(event)
            {
                $("#tref").html(("<strong>Ticket ref:</strong>: " + event.tref));
                $("#title").html(" <strong>Customer:</strong>: " + event.title);
                $("#sdate").html(" <strong>Startdate:</strong>: " + event.start.format('DD/M/YYYY hh:mm'));
                $("#edate").html(" <strong>Enddate:</strong>: " + event.end.format('DD/M/YYYY hh:mm'));
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
            debugger;
            //  alert(ID + " " + ResID);
            debugger;
            $.ajax({
                url: '../DC/ResourceService.asmx/UpdateResEvent',
                type: 'POST',
                data: "{'ID': '" + ID + "','StartDate': '" + StartDate + "','EndtDate': '" + EndtDate + "','ResId': '" + ResId + "'}",
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
                $("#Label1").text('');
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
      
    </script>
    <script type="text/javascript">
        $("input").change(function(){
            var date1 = document.getElementById("txtdate").value;
            $('#calendar').fullCalendar('gotoDate', date1);
        });
           
        // $(document).ready(function () {
        $("#datepicker").select(function(){
            $("#datepicker").datepicker({
                onSelect: $(function () {
                    var selecteddate = $("#datepicker").datepicker("getDate");
                    debugger;
                    //alert(selecteddate);
                    //do your processing here
                })
            });
        });

        $(document).ready(function () {
            $(function () {
                $("#datepicker").datepicker();
                $("#datepicker").select(function () {
                    var selected = $(this).val();
                    //alert(selected);
                });
            });
        });
        </script>
    <script>
        $(document).ready(function () {
            $('#datepicker1').datepicker().on('changeDate', function (e) {
                //alert($('#datepicker1').datepicker("getDate"));
                $('#calendar').fullCalendar('gotoDate', $('#datepicker1').datepicker("getDate"))
              //GetTotalEvents();
               // alert('t');
               // $('#datepicker1').change();
            });
            //$('#datepicker1').val('0000-00-00');
            //$('#datepicker1').change(function () {
            //    console.log($('#datepicker1').val());
            //});

        });
        
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
