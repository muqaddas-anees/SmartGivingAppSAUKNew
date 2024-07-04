<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="DC_FLSReport" EnableEventValidation="false" Codebehind="FLSReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
  <asp:Label ID="lblPageHead" runat="server" ClientIDMode="Static"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to <%= Resources.DeffinityRes.ServiceDesk%></asp:HyperLink>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />

    <style>
         td.cls_status{
        padding: 0 0 0 0;text-align: center;vertical-align: middle;
    }

   

      cls_status {
        padding: 0 0 0 0;text-align: center;vertical-align: middle;
    }
      cls_left {
       text-align: left;
    }
      cls_right {
        text-align: right;
    }
    </style>
    
    <%-- <%: System.Web.Optimization.Styles.Render("~/bundles/jtablecss") %>--%>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>
<%--<link href="../Scripts/jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Content/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />
<%--<link href="../../Content/css/custom-theme/jquery-ui-1.9.2.custom.css" rel="stylesheet" />
<script src="../../Content/jquery-ui-1.9.2.custom.min.js"></script>--%>
<%--<script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>--%>
<%--<script src="../../Content/jquery-1.9.0.min.js" type="text/javascript"></script>
<script src="../../Content/jquery-ui-1.10.0.min.js" type="text/javascript"></script>
<%-- <script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>--%>
<%--<script src="../Scripts/modernizr-1.7.min.js" type="text/javascript"></script>
<script src="../../Content/modernizr-2.6.2.js" type="text/javascript"></script>
<script src="../../Content/jtablesite.js" type="text/javascript"></script>
<!-- A helper library for JSON serialization -->
<script type="text/javascript" src="../../Content/jtable/external/json2.js"></script>
<!-- Core jTable script file -->
<script type="text/javascript" src="../../Content/jtable/jquery.jtable.js"></script>
<script type="text/javascript" src="../../Content/jtable/localization/jquery.jtable.tr.js"></script>
<!-- ASP.NET Web Forms extension for jTable -->
<script type="text/javascript" src="../../Content/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>--%>
    <div class="form-group row">
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
       ValidationGroup="dr" DisplayMode="BulletList" />
        </div>
     <div class="form-group row" >
         <div class="col-md-6">
              <input placeholder="Search" type="text" name="ticketno" id="ticketno" size="10px" class="form-control input_date" style="width:90%" />  
         </div>
           <div class="col-md-3">
                <label class="col-sm-4 control-label">Start Date</label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtLoggedStartDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                  <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtLoggedStartDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="imgToDate" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="filterarrival" runat="server" TargetControlID="txtLoggedStartDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgToDate" TargetControlID="txtLoggedStartDate">
        </ajaxToolkit:CalendarExtender>
                                          </div>
         </div>
           <div class="col-md-3">
                 <label class="col-sm-4 control-label">End Date</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtLoggedEndDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                  <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtLoggedEndDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="imgEndDate" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender2" runat="server" TargetControlID="txtLoggedEndDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgEndDate" TargetControlID="txtLoggedEndDate">
        </ajaxToolkit:CalendarExtender>     
                                          </div>
         </div>
         </div>
    <div class="form-group row" >
                                  <div class="col-md-3" style="display:none;visibility:hidden;">
                                       Company<br />
                 <asp:DropDownList ID="ddlRequestersCompany" runat="server"  SkinID="ddl_80"  
                    ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading ...]" />
                                     
				</div>
 
<div class="col-md-3" style="display:none;visibility:hidden;">
     Requester<br />
                 <asp:DropDownList ID="ddlRequester" runat="server"  SkinID="ddl_80"  
                    ClientIDMode="Static">
            </asp:DropDownList>
             <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlRequester"
                    BehaviorID="ccdNa" Category="Name" PromptText="All" PromptValue="0"
                    ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetNameByCompanyId"
                    ParentControlID="ddlRequestersCompany" LoadingText="[Loading ...]" />
                                     
				</div>
        <div class="col-md-3" style="display:none;visibility:hidden;">
                         Status<br />
                 <asp:DropDownList ID="ddlStatus" runat="server"  SkinID="ddl_80"  
                     ClientIDMode="Static"> </asp:DropDownList>
                      <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="6" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByRequestTypeId" LoadingText="[Loading ...]" />            
				</div>
</div>
    <div class="form-group row"  style="display:none;visibility:hidden;">
                                  <div class="col-md-3" style="display:none;visibility:hidden;">
                                       Site<br />
                 <asp:DropDownList ID="ddlSite" runat="server"  SkinID="ddl_80"  
                    ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="casecadeSite" runat="server" TargetControlID="ddlSite"
                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetOurSite" LoadingText="[Loading ...]" />
                                     
				</div>
 <div class="col-md-3">
      Logged Start Date<br />
     <div class="form-inline">
                
         </div>
                                      
				</div>
<div class="col-md-3">
                           Logged End Date<br />
    <div class="form-inline">
                 
        </div>      
				</div>
        <div class="col-md-3">
                            Scheduled Date<br />
            <div class="form-inline">
                 <asp:TextBox ID="txtScheduledDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                  <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtScheduledDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="imgScheduledate" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender1" runat="server" TargetControlID="txtScheduledDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="calenderScheduledate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgScheduledate" TargetControlID="txtScheduledDate">
        </ajaxToolkit:CalendarExtender>    
                </div>      
				</div>
</div>
     <div class="form-group row">
                                  <div class="col-md-3" style="display:none;visibility:hidden;">
                                       Assign Department<br />
                 <asp:DropDownList ID="ddlDepratment" runat="server" SkinID="ddl_80"  
                    ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="CasecadeDepartment" runat="server" TargetControlID="ddlDepratment"
                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetAssignedtoDepartment" LoadingText="[Loading ...]" />
                                      </div>
           <div class="col-md-3"  style="display:none;visibility:hidden;">
                Assign Technician<br />
                 <asp:DropDownList ID="ddlTechnician" runat="server" SkinID="ddl_80" 
                    ClientIDMode="Static">
            </asp:DropDownList>
           <%-- <ajaxToolkit:CascadingDropDown ID="CascadingTech" runat="server" TargetControlID="ddlTechnician"
                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetAssignedtoName" LoadingText="[Loading ...]" />--%>
                                      </div>
           <div class="col-md-3">
               
                                      </div>
          <div class="col-md-10 pull-right" style="text-align:right">
               <asp:Button SkinID="btnDefault" Text="View" ClientIDMode="Static" id="LoadTicket" runat="server"  ></asp:Button>
                <asp:Button ID="ImgFLSExport" runat="server" 
                SkinID="btnDefault" Text="Export to Excel" onclick="ImgFLSExport_Click" />
              </div>
         </div>
    <div class="row">
      <div class="col-md-6">

          <div class="card shadow-sm">
                     <div class="card-header">
                               Job Profitability of Completed Jobs
                             </div>
                     <div class="panel-body">
                                <div id="StatusGraph" style="width:100%;height:300px;"></div>
                            </div>
                    </div>

          </div>

           <div class="col-md-6">

          <div class="card shadow-sm">
                     <div class="card-header">
                                   Number of Completed Jobs By Type
                             </div>
                     <div class="panel-body">
                                <div id="StatusGraphMonth" style="width:100%;height:300px;"></div>
                            </div>
                    </div>

          </div>
         </div>
    <div class="filtering">
    
        </div>
        <div id="FLSTableContainer">
    </div>
    <script type="text/javascript" >
        $(document).ready(function () {

            $('#FLSTableContainer').jtable({

                //title: 'List of Tickets',
                paging: true, //Enables paging
                pageSize: 30, //Actually this is not needed since default value is 10.
                sorting: true,
                //Enables sorting
                defaultSorting: 'CallID ASC',
                actions: {
                    listAction: '/WF/DC/FLSReport.aspx/Get'
                },

                messages: {
                    serverCommunicationError: 'An error occured while communicating to the server.',
                    loadingMessage: 'Loading tickets...',
                    noDataAvailable: 'No tickets available!',
                    addNewRecord: '+ Add new',
                    editRecord: 'Edit',
                    areYouSure: 'Are you sure?',
                    deleteConfirmation: 'This record will be deleted. Are you sure?',
                    save: 'Save',
                    saving: 'Saving',
                    cancel: 'Cancel',
                    deleteText: 'Delete',
                    deleting: 'Deleting',
                    error: 'Error',
                    close: 'Close',
                    cannotLoadOptionsFor: 'Can not load options for field {0}',
                    pagingInfo: 'Showing {0} to {1} of {2} records',
                    canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
                    deleteProggress: 'Deleted {0} of {1} records, processing...'
                },
                fields: {
                    CallID: {
                        key: true,
                        title: 'Job Ref',
                        width: '5%',
                        display: function (data) {
                           
                            return '<a href="FLSForm.aspx?CCID = ' + data.record.CCID + '&callid=' + data.record.CallID + '"><b>' + data.record.CCID + '</b></a>';
                        }
                    },
                    RequesterID: {
                        title: 'Requester',
                        height: '5%',
                        display: function (data) {

                            return '<a href="/WF/CustomerAdmin/ContactDetails.aspx?ContactID= ' + data.record.RequesterID + '">' + data.record.RequesterName + '</a>';
                        }
                    },
                    RequestersAddress: {
                        title: 'Address',
                        width: '8%'
                    },
                    RequestersPostCode: {
                        title: 'Zipcode',
                        width: '5%'
                    },
                    TypeofRequest: {
                        title: 'Job Type',
                        width: '5%'
                    },
                    Cost: {
                        title: 'Cost',
                        listClass :'cls_right',
                        width: '5%'
                    },
                    TotalCost: {
                        title: 'Sales Price',
                        listClass: 'cls_right',
                        width: '5%'
                    },
                    VAT: {
                        title: 'Profit',
                        width: '5%',
                        listClass: 'cls_right',
                        display: function (data) {
                            return  data.record.TotalCost - data.record.Cost ;
                        }
                    },
                    StatusName: {
                        title: 'Status',
                        width: '10%',
                        listClass:'cls_status statuscls',
                        display: function (data) {
                            //return  data.record.Status;
                            var s = data.record.Status;
                            var retval = data.record.Status;
                            var cls = 'color: white;font-weight: bold;text-align: center;vertical-align: middle;height: 50px;padding-top: 20px;';
                            if (s == 'New')
                                retval = '<div class="statuscls" style="background-color:#00B0F0;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Cancelled')
                                retval = '<div class="statuscls" style="background-color:#44546a;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Resolved')
                                retval = '<div class="statuscls" style="background-color:#00B0F0;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Closed')
                                retval = '<div class="statuscls" style="background-color:#0070C0;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Scheduled')
                                retval = '<div class="statuscls" style="background-color:#92D050;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Awaiting Schedule')
                                retval = '<div class="statuscls" style="background-color:#B4c6e7;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Arrived')
                                retval = '<div class="statuscls" style="background-color:#0070C0;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Customer Not Responding')
                                retval = '<div class="statuscls" style="background-color:#FF0000;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Feedback Submitted')
                                retval = '<div class="statuscls" style="background-color:#002060;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Feedback Received')
                                retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Quote Rejected')
                                retval = '<div class="statuscls" style="background-color:#002060;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Quote Accepted')
                                retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Awaiting Information')
                                retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Waiting On Parts')
                                retval = '<div class="statuscls" style="background-color:#002060;' + cls + '">' + data.record.Status + '</div>';
                            else if (s == 'Authorised')
                                retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                            return retval;
                        }
                    },
                   
                    
                    LoggedDate: {
                        title: 'Logged Date',
                        width: '7%',
                        type: 'date',
                        listClass: 'cls_right',
                        display: function (data) {

                            if (data.record.LoggedDate != null) {

                                var yr = moment(data.record.LoggedDate).format('YYYY');
                                if (yr == '1900')
                                    return null;
                                else
                                    return moment(data.record.LoggedDate).format('MM/DD/YYYY');
                            }
                            else
                                return null;

                        }
                    },
                    ScheduleDate: {
                        title: 'Scheduled Date',
                        width: '7%',
                        type: 'date',
                        listClass: 'cls_right',
                        display: function (data) {
                            return data.record.ScheduledDateTime;
                            //if (data.record.ScheduleDate != null) {
                            //    return data.record.ScheduledDateTime;
                            //    //var yr = moment(data.record.ScheduleDate).format('YYYY');
                            //    //if (yr == '1900')
                            //    //    return null;
                            //    //else
                            //    //    return moment(data.record.ScheduleDate).format('MM/DD/YYYY');


                            //}
                            //else
                            //    return null;

                        }
                    },
                   
                    AssignedTechnician: {
                        title: 'Service Tech',
                        width: '5%'
                    }             
                }
            });
            var cnt = 0;
            //Re-load records when user click 'load records' button.
            $('#LoadTicket').click(function (e) {
                e.preventDefault();


                GetCompletedStakeBar();
                GetThismonthDnut();
                //var company = document.getElementById('ddlRequestersCompany');
                //var name = document.getElementById('ddlRequester');
                //var status = document.getElementById('ddlStatus');
                //var site = document.getElementById('ddlSite');
                //var department = document.getElementById('ddlDepratment');
                //var technician = '0';//document.getElementById('ddlTechnician');
                
                $('#FLSTableContainer').jtable('load', {
                    callid: $('#ticketno').val(),
                    company: '',
                    name: '',
                    status: '',
                    loggedStartDate: $('#txtLoggedStartDate').val(),
                    loggedEndDate: $('#txtLoggedEndDate').val(),
                    scheduledate: '',
                    site: '',
                    department: '',
                    technician: '0'
                });
                debugger;
                if (cnt == 0) {
                    $('#FLSTableContainer table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                    $("#FLSTableContainer table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                    $("#FLSTableContainer").attr("style", "")

                    cnt = cnt + 1;
                }
            });
            //Load all records when page is first shown
            $('#LoadTicket').click();



        });
       
    </script>

     <%-- <script type="text/javascript">
          $(document).ready(function () {
              $(document.body).find("[id$='lblPageHead']").html('Service Desk');
          });
    </script>--%>

    <style>
        .cls_right{
            text-align:right;
        }
    </style>


     <style>
        .card {
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  transition: 0.3s;
  padding:10px;
}

.card:hover {
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
}
        .cardpad{
            padding:10px;
        }
        .container {
            padding: 2px 16px;
        }
.namefont {
  font-size:16px;
}
.valuefont {
  font-size:15px;
}
.chart-item-bg-2 .chart-item-num {
    /* padding-left: 40px; */
    font-size: 40px;
    color: #434444;
    /* padding-right: 30px; */
    white-space: nowrap;
    </style>
     


    <script type="text/javascript">

        function thousands_separators(num) {
            var num_parts = num.toString().split(".");
            num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return num_parts.join(".");
        }
        $(document).ready(function () {
            GetCompletedStakeBar();
            GetThismonthDnut();
           

        });



        function GetCompletedStakeBar() {
            var fromdate = $("[id$='txtLoggedStartDate']").val();
            var todate = $("[id$='txtLoggedEndDate']").val();
            var search = $("[id$='ticketno']").val();
            

            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetDataByPriceAll',
                type: "POST",
                data: "{'fromdate': '" + fromdate + "','todate':'" + todate + "','search':'" + search + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                       
                        var xenonPalette = ['#83D3F1', '#B7DD98', '#BE9CDE', '#FFBA00', '#55ACEE', '#3B5998', '#68B828'];
                        $("#StatusGraph").dxChart({
                            dataSource: newData,
                            commonSeriesSettings: {
                                argumentField: "dateitem",

                                type: "bar"
                            },

                            series: [
                                { valueField: "SalesPrice", name: "Sales Price" },
                                { valueField: "TotalCosts", name: "Total Costs" },
                                { valueField: "Profit", name: "Profit" },
                            ],
                            legend: {
                                //visible:false
                                verticalAlignment: "bottom",
                                horizontalAlignment: "center",
                                itemTextPosition: 'top',
                              
                            },
                            valueAxis: {
                                title: {
                                    text: "Price"
                                },
                                position: "left"
                            },
                            //title: "Male Age Structure",
                            //"export": {
                            //    enabled: true
                            //},
                            tooltip: {
                                enabled: true,
                                location: "edge",
                                customizeTooltip: function (arg) {
                                    return {
                                        text: arg.seriesName + " : " + arg.valueText
                                    };
                                }
                            },
                            palette: xenonPalette
                        });


                    }
                }
            });
        }

        function GetThismonthDnut() {
            var xenonPalette = ['#00B0F0', '#92D050', '#0070C0', '#B4c6e7', '#ED7D31', '#00b19d', '#ff6264', '#f7aa47'];
            var fromdate = $("[id$='txtLoggedStartDate']").val();
            var todate = $("[id$='txtLoggedEndDate']").val();
            var search = $("[id$='ticketno']").val();

            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetDataByStatusAll',
                type: "POST",
                data: "{'fromdate': '" + fromdate + "','todate':'" + todate + "','search':'" + search + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);

                        var xenonPalette = ['#83D3F1', '#B7DD98', '#BE9CDE', '#FFBA00', '#55ACEE', '#3B5998', '#68B828'];
                        $("#StatusGraphMonth").dxChart({
                            dataSource: newData,
                            commonSeriesSettings: {
                                argumentField: "dateitem",
                                type: "stackedBar"
                            },

                            series: [
                                { valueField: "Fault", name: "Fault" },
                                { valueField: "Inspection", name: "Inspection" },
                                { valueField: "Maintenance", name: "Maintenance" },
                                { valueField: "Installation", name: "Installation" },
                                { valueField: "Repair", name: "Repair" },
                                { valueField: "Service", name: "Service" },
                                { valueField: "Upgrade", name: "Upgrade" }
                            ],
                            customizePoint: function (pointInfo) {
                                // debugger;
                                if (pointInfo.seriesName === "Fault") {
                                    return { color: "#83D3F1", hoverStyle: { color: "#83D3F1" } };
                                } else if (pointInfo.seriesName === "Inspection") {
                                    return { color: "#B7DD98", hoverStyle: { color: "#B7DD98" } };
                                } else if (pointInfo.seriesName === "Maintenance") {
                                    return { color: "#BE9CDE", hoverStyle: { color: "#BE9CDE" } };
                                } else if (pointInfo.seriesName === "Installation") {
                                    return { color: "#FFBA00", hoverStyle: { color: "#FFBA00" } };
                                } else if (pointInfo.seriesName === "Repair") {
                                    return { color: "#55ACEE", hoverStyle: { color: "#55ACEE" } };
                                } else if (pointInfo.seriesName === "Service") {
                                    return { color: "#3B5998", hoverStyle: { color: "#3B5998" } };
                                } else if (pointInfo.seriesName === "Upgrade") {
                                    return { color: "#68B828", hoverStyle: { color: "#68B828" } };
                                }
                            },
                            legend: {
                                //visible:false
                                verticalAlignment: "bottom",
                                horizontalAlignment: "center",
                                itemTextPosition: 'top',

                            },
                            valueAxis: {
                                title: {
                                    text: "No. of Jobs"
                                },
                                position: "left"
                            },
                            //title: "Male Age Structure",
                            //"export": {
                            //    enabled: true
                            //},
                            tooltip: {
                                enabled: true,
                                location: "edge",
                                customizeTooltip: function (arg) {
                                    return {
                                        text: arg.seriesName + " : " + arg.valueText
                                    };
                                }
                            },
                            palette: xenonPalette
                        });


                    }
                }
            });
        }

      

    </script>

     <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>


     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
         setStatusBackColor();
         //grid_responsive_display();
         function setStatusBackColor() {
             $(window).load(function () {

                 $('.statuscls').each(function () {

                     var s = $(this).html();
                     if (s == 'New')
                         $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                     else if (s == 'Cancelled')
                         $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
                     else if (s == 'Resolved')
                         $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                     else if (s == 'Closed')
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
                 });
             });
             $('.statuscls').each(function () {

                 var s = $(this).html();
                 if (s == 'New')
                     $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Cancelled')
                     $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Resolved')
                     $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Closed')
                     $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Scheduled')
                     $(this).closest("td").css({ "background-color": "#92D050", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Awaiting Schedule')
                     $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Arrived')
                     $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Customer Not Responding')
                     $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                 else if (s == 'Feedback Submitted')
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
             });


         }
        


           // setStatusBackColor();

    </script>

</asp:Content>


