<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCAssignSalesRep.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCAssignSalesRep" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">Smart Tech</label>  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <link href='../../Content/assets/js/fullcalendar/lib/fullcalendar.min.css' rel='stylesheet' />
      <link href='../../Content/assets/js/fullcalendar/lib/fullcalendar.print.css' rel='stylesheet' media='print' />
      <link href='../../Content/assets/js/fullcalendar/lib/scheduler.min.css' rel='stylesheet' />
    <script src='../../Content/assets/js/fullcalendar/lib/moment.min.js'></script>
      <script src='../../Content/assets/js/fullcalendar/lib/fullcalendar.min.js'></script>
      <script src='../../Content/assets/js/fullcalendar/lib/scheduler.min.js'></script>
    <style>
        a.fc-timeline-event{
            height:45px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnserach').click(function () {
                var checked_checkboxes = $("[id*=chkExpert] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    var value = $(this).val();
                    message = message + value + ',';
                   // var text = $(this).closest("td").find("label").html();
                    //message += "Text: " + text + " Value: " + value;
                    //message += "\n";
                });
                $('#hexpert').val(message);
                GetTotalEvents();
                
                return false;
            });
        });

    </script>
    <script type="text/javascript">
        Date.prototype.toDDMMYYYYString = function () { return isNaN(this) ? 'NaN' : [this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getFullYear()].join('/') }
        Date.prototype.toMMDDYYYYString = function () { return isNaN(this) ? 'NaN' : [this.getMonth() > 8 ? this.getMonth() + 1 : '0' + (this.getMonth() + 1), this.getDate() > 9 ? this.getDate() : '0' + this.getDate(), this.getFullYear()].join('/') }

        function DateConvertion(s_date) {

            var b = s_date;
            var temp = new Array();
            temp = b.split('/');
            // var s1 = temp[1] + '/' + temp[0] + '/' + temp[2];
            var s1 = temp[0] + '/' + temp[1] + '/' + temp[2];
            return s1
        }
        function get_startdate() {
            var val_startdate = document.getElementById("<%=txtSeheduledDateTime.ClientID%>").value;
            return val_startdate;
        }
        function get_enddate() {
            var val_startdate = document.getElementById("<%=txtScheduledEndDateTime.ClientID%>").value;
            return val_startdate;
        }
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
        $.fn.toggleCheckbox = function() {
            this.attr('checked', !this.attr('checked'));
        }
        $(document).ready(function () {
            GetTotalEvents();
            //GetTotalResources();
            $("#selectall").click(function (e) {
                //var checkboxes = $("[id^='id']").find(':checkbox');
                $(":checkbox[id^='id']").each(function () {
                    $(this).attr('checked', 'checked');
                    
                    fncheck($(this).attr("id").substring(3));
                    //GetAllEvents
                });
                return false;
            })
        });

        function GetTotalEvents() {
            $('#calendar').fullCalendar('destroy');
            
            //var d = "{}";
            //if (getQuerystring('callid') != "")
            var d = "{CallID:'" + getQuerystring('callid') + "',pids:'" + $('#hexpert').val() + "'}";

            $.ajax({
                type: "POST",
                url: '../DC/Resourceservice.asmx/GetAllEventsSmartRep',
                //url: '../DC/Resourceservice.asmx/GetAllEventsScheduled',
                data: d,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (doc) {
                    var events = [];
                    var resources = [];
                    
                    var obj = $.parseJSON(doc.d);
                    var ev = obj.data;
                    var rv = obj.data2;
                    debugger;
                    $.each(ev, function (i, item) {
                        if (item.start.substring(11, 16) == item.end.substring(11, 16)) {
                            item.start = item.start.substring(0, 10);
                            item.end = item.end.substring(0, 10);
                        }
                        else {
                            item.start = item.start;// moment(item.start).format('DD/MM/YYYY hh:mm');
                            item.end = item.end;// moment(item.end).format('DD/MM/YYYY hh:mm');
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
                            contact: $(this).attr('contact'),
                            backgroundColor: $(this).attr('backcolor'),
                            borderColor: $(this).attr('borderColor'),
                            spname: $(this).attr('spname')
                        });
                    });

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

                    $(".fc-today-button").after($("#selectall"));
                    
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

                   
                    var obj = $.parseJSON(doc.d);
                   
                    $(obj).each(function () {
                        resources.push({
                            id: $(this).attr('id'),
                            title: $(this).attr('title'),
                        });
                    });
                   
                }
            });

            return resources;
        }

        function TotalEventsWithCalender(events, resources) {
            
            $('#calendar').fullCalendar('destroy');
           
            $('#calendar').fullCalendar({
                //defaultDate: new Date(),
                height: 600,
                schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
                defaultDate:new Date($('#hdate').val()), //new Date(),// new Date(DateConvertion(get_startdate())),// new Date(DateConvertion(get_startdate())),
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
                viewRender: function (view, element) {

                    DispImages();
                    ClearAll();
                },
                eventRender: function (event, element) {
                    element.find('.fc-title').after("<br><span class='fc-title'>No:" + event.contact + "</span>" + "<br><span class='fc-title'>Ref:" + event.tref + "</span>" + "<br><span class='fc-title'>" + event.spname + "</span>");

                },
                eventClick: function (event) {
                    $("#HFIDnew").val(event.id);
                    //if (confirm("Do you want to delete ?")) {
                    //    DeletefullCalander();
                    //}
                    //  var etitle = "Title:" + event.title
                    ///  var estart = "Start Date:" + event.start.format('DD-M-YYYY hh:mm');
                    //   var estart = "Start Date:" + event.start.format('DD-M-YYYY hh:mm');
                    
                    $("#HFTitle").val("Title:" + event.title);
                    $("#HFstartdate").val("Start Date:" + event.start.format('M-DD-YYYY HH:mm'));
                    $("#HFenddate").val("End Date:" + event.end.format('M-DD-YYYY HH:mm'));
                    //$("#Hrefid").val(event.tref);
                    $("#Hrefid").val(event.id);
                    DisplayEventDetails();
                },
                eventResize: function (event) {

                    
                    $("#HFstartdate").val(event.start.format());
                    $("#HFenddate").val(event.end.format());
                    $("#HFIDnew").val(event.id);
                    $("#HFresID").val(event.resourceId);
                    $("#Hrefid").val(event.tref);
                    var BtnText = $(".fc-state-active").text();
                    UpdateCalEvent();

                },

                eventDrop: function (event, dayDelta, minuteDelta, allDay, revertFunc) {
                    
                    //debugger;
                    $("#HFstartdate").val(event.start.format());
                    $("#HFenddate").val(event.end.format());
                    $("#HFIDnew").val(event.id);
                    $("#HFresID").val(event.resourceId);
                    $("#Hrefid").val(event.tref);
                    UpdateCalEvent();

                },
            });

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
                   
                    if (x.length == 0) {
                        if ($("#<%=hstatus.ClientID%>").val() != '0')
                            element.find('.fc-cell-text').prepend('<img src="../Admin/ImageHandler.ashx?type=user&id=' + element.attr('data-resource-id') + '" alt="" height="45px"> ');
                        else
                            element.find('.fc-cell-text').prepend('<input class="chk" type="checkbox" id="id_' + element.attr('data-resource-id') + '" value="' + element.attr('data-resource-id') + '" onchange="fncheck(' + element.attr('data-resource-id') + ');"> <img src="../Admin/ImageHandler.ashx?type=user&id=' + element.attr('data-resource-id') + '" alt="" height="45px"> ');
                    }
                }

            }
        }

        function DisplayEventDetails() {
            $("#title").text($("#<%=HFTitle.ClientID%>").val());
            $("#sdate").text($("#<%=HFstartdate.ClientID%>").val());
            $("#edate").text($("#<%=HFenddate.ClientID%>").val());
            $("#Label1").text("Schedule");
            //   ClearAll();
        }

        function fncheck(e) {
            var s = "";
            //chk
            $('input.chk[type=checkbox]').each(function () {
                var sThisVal = (this.checked ? $(this).val() : "");
                if (this.checked) {
                    s = s + $(this).val()+ ",";
                   
                }
                //if (s != '') {
                    $("#<%=hrid.ClientID%>").val(s);
                    //console.log( $("#<%=hrid.ClientID%>").val());
                //}
               // $('#comment').html(sThisVal);
            });
           <%-- var s = "";
            var rid = $("#<%=hrid.ClientID%>").val();
            if (rid != '')
                s = rid + ',' + e;
            else
                s = e;
            $("#<%=hrid.ClientID%>").val(s);
            //alert($("#<%=hrid.ClientID%>").val());--%>


        }



      </script>

    <script type="text/javascript">

        function UpdateCalEvent() {

            var ID = $("#<%=HFIDnew.ClientID%>").val();
            var StartDate = $("#<%=HFstartdate.ClientID%>").val();
            var EndtDate = $("#<%=HFenddate.ClientID%>").val();
            var ResId = $("#<%=HFresID.ClientID%>").val();
            var callid = $("#<%=Hrefid.ClientID%>").val();
            debugger;
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
                }
            });
        };

    </script>
      <div class="form-group row" style="display:none;visibility:hidden;" >
                                                 <div class="col-md-12">
                                                <label class="col-sm-2 control-label form-inline">
                                                Expertise Type
                                               </label>
                                               <div class="col-sm-4 form-inline">
                                                    <asp:Panel ID="pnlSkill" runat="server" Width="350px" BorderColor="Silver" BorderWidth="1px"
                            Height="100px" ScrollBars="Auto">
                            <asp:CheckBoxList ID="chkExpert" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                                                <%--   <asp:DropDownList ID="ddlExpert" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                   <ajaxToolkit:CascadingDropDown ID="ccdExperts" runat="server" TargetControlID="ddlExpert"
                                                        Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                                        ServiceMethod="GetAssetType" LoadingText="[Loading ...]" />--%>
                                                   </div>
                                                     <div class="col-sm-4">
                                                         <asp:Button ID="btnserach" runat="server" SkinID="btnSearch" ClientIDMode="Static" />
                                                         </div>
                                                     </div>
                                           </div>
     <div class="row">
                  <div class="col-md-12">
                       <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                      </div>
         </div>
     <div class="form-group well" id="pnldetails" style="display:none;visibility:hidden;">
      <div class="col-md-6 ">
          <div class="form-group row">
        <div class="col-md-12">
           <strong>Ticket Details </strong> 
            <hr class="no-top-margin"  style="margin-bottom:0px"/>
            </div>
    </div>
       <div class="form-group row" style="margin-bottom:0px;" >
            <div class="col-md-12">
           <label class="col-sm-2" style="font-weight:bold;"><span class="control-label">Ref:</span></label>
           <div class="col-sm-10">
                <asp:Label ID="lbltref" Text="" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
	</div>
            <div class="col-md-12">
           <label class="col-sm-2"  style="font-weight:bold;"><span class="control-label">Customer:</span></label>
           <div class="col-sm-10">
               <asp:Label ID="lblCustomertitle" Text="" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
	</div>
            <div class="col-md-12" style="display:none;visibility:hidden;">
           <label class="col-sm-2" style="font-weight:bold;"><span class="control-label">Start&nbsp;date:</span></label>
           <div class="col-sm-10">
               <asp:Label ID="lblsdate" ClientIDMode="Static" Text="" runat="server"></asp:Label>
            </div>
	</div>
            <div class="col-md-12" style="display:none;visibility:hidden;">
           <label class="col-sm-2" style="font-weight:bold;"><span class="control-label">End date:</span></label>
           <div class="col-sm-10">
               <asp:Label ID="lbledate" ClientIDMode="Static" Text="" runat="server"></asp:Label>
            </div>
	</div>
            <div class="col-md-12">
           <label class="col-sm-2" style="font-weight:bold;"><span class="control-label">Address:</span></label>
           <div class="col-sm-10">
                <asp:Label ID="lbladdress" ClientIDMode="Static" Text="" runat="server"></asp:Label>
            </div>
	</div>
            <div class="col-md-12">
           <label class="col-sm-2" style="font-weight:bold;"><span class="control-label">Details:</span></label>
           <div class="col-sm-10">
               <asp:Label ID="lbldetails" ClientIDMode="Static" Text="" runat="server"></asp:Label>
            </div>
	</div>
            
           </div>
        
         
         </div>
	<div class="col-md-6">
            <div class="form-group row" style="margin-bottom:0px;">
        <div class="col-md-12">
           <strong>Preferred Date and Times  <asp:HiddenField ID="hdate" runat="server" ClientIDMode="Static" /> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
         <div class="form-group row" >
           <div class="col-sm-12 form-inline">
              <asp:CheckBox ID="chkP1" runat="server" Checked="true"></asp:CheckBox>  <asp:Label ID="txtpdate1" Text="" ClientIDMode="Static" runat="server"></asp:Label>
               <asp:LinkButton ID="btnEditDate" runat="server" SkinID="BtnLinkEdit" OnClick="btnEditDate_Click"></asp:LinkButton>
            </div>
                <div class="col-sm-12 form-inline">
             <asp:CheckBox ID="chkP2" runat="server"></asp:CheckBox>  <asp:Label ID="txtpdate2" Text="" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
              <div class="col-sm-12 form-inline">
             <asp:CheckBox ID="chkP3" runat="server"></asp:CheckBox>  <asp:Label ID="txtpdate3" Text="" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
             </div>
	</div>
</div>
    <asp:Panel ID="pnlResources" runat="server">
    <div class="row">
                 <%-- <div class="col-md-12">
 <strong>Resources </strong> 
<hr class="no-top-margin" />
	</div>--%>
</div>
     <div class="row" style="padding-bottom:5px;" id="pnlButtons" runat="server">
         <div class="col-md-6">
             </div>
     <div class="col-md-6 d-flex d-line text-right">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="ol-md-12 d-flex d-line text-right">
         <asp:TextBox ID="txtDate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox>
                                          <asp:Label ID="imgDate" runat="server" SkinID="Calender" ClientIDMode="Static" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                PopupButtonID="imgDate" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                         <asp:TextBox ID="txtTime" runat="server" MaxLength="10" SkinID="Time"></asp:TextBox>
                        <asp:DropDownList ID="ddlStartTime" runat="server" SkinID="ddl_75px">
                                <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
         <asp:Button ID="btnAssign" runat="server" SkinID="btnDefault" Text="Assign To Rep" OnClick="btnAssign_Click" />
         <asp:Button ID="btnEmailRequest" runat="server" SkinID="btnDefault" Text="Email Request" OnClick="btnEmailRequest_Click" Visible="false" />
                         <asp:Button ID="btnReopen" runat="server" SkinID="btnDefault" Text="Delete Rep and Reopen" OnClick="btnReopen_Click" />
                            </div>
                        </ContentTemplate>
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnAssign" />
                  <asp:PostBackTrigger ControlID="btnEmailRequest" />
                  <asp:PostBackTrigger ControlID="btnReopen" />
              </Triggers>
              </asp:UpdatePanel>
         </div>
    </div>
    <%-- <asp:UpdatePanel ID="upResources" runat="server">
            <ContentTemplate>--%>
         <asp:HiddenField ID="hexpert" runat="server" ClientIDMode="Static" Value="0" />
                <asp:HiddenField ID="hstatus" runat="server" ClientIDMode="Static" Value="0" />
     <asp:HiddenField ID="txtSeheduledDateTime" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="txtScheduledEndDateTime" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hrid" runat="server" ClientIDMode="Static" Value=""/>
    <asp:HiddenField ID="HFIDnew" runat="server" ClientIDMode="Static"/>
         <asp:HiddenField ID="HFresID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="HFstartdate" runat="server"  ClientIDMode="Static"/>
        <asp:HiddenField ID="HFenddate" runat="server" ClientIDMode="Static"/>
        <asp:HiddenField ID="HFTitle" runat="server" ClientIDMode="Static"/>
                <asp:HiddenField ID="Hrefid" runat="server" ClientIDMode="Static"/>
        <button class="fc-button fc-state-default fc-corner-left fc-corner-right" id="selectall">Select All</button>
  <div id="calendar"></div>
               
           <%-- </ContentTemplate>
         </asp:UpdatePanel>--%>
</asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblStorageNew" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="300px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"> Update Scheduled Dates</h3>
							
							<div class="card-toolbar">
								<%--<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">–</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>--%>
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								<%--<a href="#" data-toggle="remove">
									×
								</a>--%>
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
 <label class="col-sm-3 control-label"><asp:Label ID="lblScheduledDateTime" runat="server" Text="Preferred Date / Time"></asp:Label></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtSeheduledDateTime1" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" ClientIDMode="Static" />
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
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <style>
        a.fc-timeline-event{
            height:60px;
        }
    </style>

    <script>
        activeTab('Quote &amp; Assign Sales');
    </script>
</asp:Content>
