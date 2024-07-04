<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="fc.aspx.cs" Inherits="DeffinityAppDev.fc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <script src='<%:ResolveClientUrl("~/Content/assets/js/moment.min.js")%>'></script>
   
    <link href='<%:ResolveClientUrl("~/assets/plugins/custom/fullcalendar/fullcalendar.bundle.css")%>' rel="stylesheet" type="text/css" />
     <script src='<%:ResolveClientUrl("~/assets/plugins/custom/fullcalendar/fullcalendar.bundle.js")%>' type="text/javascript" ></script>

    <div class="py-5">
   <!--begin::Draggable heading-->
<h4 class="mb-3">Email Templates</h4>
<!--end::Draggable heading-->

<!--begin::Events listing-->
    <div class="card card-bordered mb-10">
												<div class="card-body fc">
<div id="kt_docs_fullcalendar_events_list" class="d-flex flex-wrap">
 
</div>

                                                    </div>
        </div>
        </div>
<!--end::Events listing-->

<!--begin::Checkbox-->
<div class="mt-2 my-5" style="display:none;visibility:hidden;">
    <div class="form-check form-check-custom form-check-solid">
        <input class="form-check-input" type="checkbox" value="" id="drop-remove" />
        <label class="form-check-label" for="drop-remove">
            Remove event after drop
        </label>
    </div>
</div>
<!--end::Checkbox-->

<!--begin::Fullcalendar-->
<div id="kt_docs_fullcalendar_drag"></div>
<!--end::Fullcalendar-->


    <script>
        var events_data = [];
        //events_data.push({
        //    id: '10001',
        //    title: 'availableForMeeting',
        //    start: '2022-11-10',
        //    end: '2022-11-12'//,
        //    //backgroundColor: $(this).attr('color'),
        //    // borderColor: $(this).attr('color')
        //});
        jQuery(document).ready(function ($) {
            GetTemplates();
            GetTotalEvents();
        });
        function UpdateCalEventTime() {

          
             //var EndtDate = DateConvertion(ed);
             //alert(ID);

             //$.ajax({
             //    url: '../Campaign/CalenderService.asmx/WorkflowTemplateConfig_UpdateTime',
             //    type: 'POST',
             //    //   data: "{'ID': '" + ID + "','EndtDate': '" + EndtDate + "','Stime': '" + Stime + "','StartDate': '" + StartDate + "','Etime': '" + Etime + "'}",
             //    data: "{'ID': '" + ID + "','StartDate': '" + StartDate + "'}",
             //    contentType: 'application/json; charset=utf-8',
             //    dataType: "json",
             //    async: true,
             //    success: function (data) {
             //        var events = [];
             //        var obj = $.parseJSON(data.d);

             //        $.each(obj, function (i, item) {
             //            if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
             //                item.start = item.start.substring(0, 10);
             //                item.end = item.end.substring(0, 10);
             //            }
             //            else {
             //                item.start = item.start;
             //                item.end = item.end;
             //            }
             //        });

             //        console.log(obj);
             //        $(obj).each(function () {
             //            events.push({
             //                id: $(this).attr('id'),
             //                title: $(this).attr('title'),
             //                start: $(this).attr('start'),
             //                end: $(this).attr('end')//,
             //                //backgroundColor: $(this).attr('color'),
             //                //borderColor: $(this).attr('color')
             //            });
             //            events: [events];
             //        });
             //        $("#calendar").fullCalendar("refetchEvents");
             //        $("#calendar").fullCalendar("removeEvents", ID);
             //        $('#calendar').fullCalendar('addEventSource', events);
             //        //ClearAll();
             //    },

             //});
             // }
         }
        function GetTotalEvents() {
            $.ajax({
                type: "POST",
                url: '<%:ResolveClientUrl("~/WF/CustomerAdmin/Campaign/CalenderService.asmx/GetAllEvents")%>',
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (doc) {
                    
                  
                    var obj = $.parseJSON(doc.d);


                    $.each(obj, function (i, item) {
                        if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
                            item.start = item.start.substring(0, 10);
                            item.end = item.end.substring(0, 10);
                        }
                        else {
                            item.start = item.start;
                            item.end = item.end;
                        }
                    });

                    //debugger;
                    $(obj).each(function () {
                        events_data.push({
                            id: $(this).attr('id'),
                            title: $(this).attr('title'),
                            start: $(this).attr('start'),
                            end: $(this).attr('end')//,
                            //backgroundColor: $(this).attr('color'),
                            // borderColor: $(this).attr('color')
                        });
                    });
                    console.log(events_data);
                  //  $('#calendar').fullCalendar('addEventSource', events);
                   // TotalEventsWithCalender(events);

                }
            });


            return events_data;
        }
        function AddCalEvent( tname, sdate, edate) {

            $.ajax({
                url: '<%:ResolveClientUrl("~/WF/CustomerAdmin/Campaign/CalenderService.asmx/ts_add")%>',
                type: 'POST',
                data: "{'tname': '" + tname + "','sdate': '" + sdate + "','edate': '" + edate + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: true,
                success: function (data) {
                    debugger;
                    var events = [];
                    var obj = $.parseJSON(data.d);

                    $.each(obj, function (i, item) {
                        //if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
                        //    item.start = item.start.substring(0, 10);
                        //    item.end = item.end.substring(0, 10);
                        //}
                        //else {
                        //    item.start = item.start;
                        //    item.end = item.end;
                        //}

                        //eventObject.id = item.id;
                    });


                 
                },
            });
        }

        function GetTemplates() {

            $.ajax({
                type: "POST",
                url: '<%:ResolveClientUrl("~/WF/CustomerAdmin/Campaign/CalenderService.asmx/GetAllTemplates")%>',
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (doc) {
                    var events = [];
                 
                    var obj = $.parseJSON(doc.d);
                  
                    var colors = ['red', 'blue', 'primary', 'success', 'warning', 'info', 'danger', 'purple', 'black', 'gray'];


                    $.each(obj, function (i, item) {
                        var event_name = item;
                      
                        //if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
                        //    item.start = item.start.substring(0, 10);
                        //    item.end = item.end.substring(0, 10);
                        //}
                        //else {
                        //    item.start = item.start;
                        //    item.end = item.end;
                        //}
                        if (event_name.length >= 1) {
                            var color = colors[Math.floor(Math.random() * colors.length)];
                            $("#kt_docs_fullcalendar_events_list").append("<div class='fc-event fc-h-event fc-daygrid-event fc-daygrid-block-event badge me-3 my-1'><div class='fc-event-main'>" + event_name + "</div></div>");
                          //  $("#kt_docs_fullcalendar_events_list").append("<li><a href='#' data-event-class='event-color-" + color + "'><span class='badge badge-" + color + " badge-roundless upper'>" + event_name + "</span></a></li>");
                        }
                        // Reset draggable
                        //$("#events-list li").draggable({
                        //    revert: true,
                        //    revertDuration: 50,
                        //    zIndex: 999
                        //});

                        // Reset input
                        // $event.val('').focus();

                    });


                }
            });
        }
        // Initialize the external events -- for more info please visit the official site: https://fullcalendar.io/demos
        var containerEl = document.getElementById("kt_docs_fullcalendar_events_list");
        new FullCalendar.Draggable(containerEl, {
            itemSelector: ".fc-event",
            eventData: function (eventEl) {
                return {
                    title: eventEl.innerText.trim()
                }
            }
        });

      //  console.log(e)
        // initialize the calendar -- for more info please visit the official site: https://fullcalendar.io/demos
        var calendarEl = document.getElementById("kt_docs_fullcalendar_drag");
        var calendar = new FullCalendar.Calendar(calendarEl, {
            headerToolbar: {
                left: "prev,next today",
                center: "title",
                right: "dayGridMonth,timeGridWeek,timeGridDay,listWeek"
            },
            events: GetTotalEvents(),
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar
            drop: function (arg) {
                //debugger;

                //console.log('datestr:' + arg.dateStr);
                //console.log('datestr:' + arg.draggedEl.innerText);
                //arg.draggedEl
                AddCalEvent(arg.draggedEl.innerText, arg.dateStr, arg.dateStr);
                // is the "remove after drop" checkbox checked?
                if (document.getElementById("drop-remove").checked) {
                    // if so, remove the element from the "Draggable Events" list
                    arg.draggedEl.parentNode.removeChild(arg.draggedEl);
                }
            },
            eventClick: function (event) {
                debugger;
                var id = event.id;
                var url_1 = "/WF/CustomerAdmin/Campaign/CampaignNavigation.aspx?eventid=" + event.id ;
                window.location.href = url_1;
               

            },
        });

        calendar.render();
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
