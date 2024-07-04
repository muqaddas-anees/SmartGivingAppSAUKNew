<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
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
</style>
      <div class="row">
      <%--  <div class="col-sm-3">			
					<div class="xe-widget xe-counter xe-counter-info" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-video-camera"></i>
						</div>
						<div class="xe-label">
							<a style="font-size:large;" href="#">Watch Video</a>
						</div>
					</div>
				</div>	--%>
        <div class="col-sm-4">
					
					<div class="xe-widget xe-counter xe-counter-info" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<i class="fa-group"></i>
						</div>
						<div class="xe-label">
                            <strong class="num"><a  href="../DC/FLSJlist.aspx?type=FLS">Create a Customer</a></strong>
							<span><a href="#">Watch Video</a></span>
						</div>
					</div>
				
				</div>
     	<div class="col-sm-4">
					
					<div class="xe-widget xe-counter xe-counter-info" data-count=".num" data-from="1000" data-to="2470" data-duration="4" data-easing="true">
						<div class="xe-icon">
							<i class="fa-wrench"></i>
						</div>
						<div class="xe-label">
								
                            <strong class="num"><a  href="../DC/FLSForm.aspx">Raise a Job</a></strong>
							<span><a  href="#">Watch Video</a></span>
						</div>
					</div>
				
				</div>
	    <div class="col-sm-4">
					
					<div class="xe-widget xe-counter xe-counter-info"
                          data-count=".num" data-from="0" data-to="57" data-prefix="-," data-suffix="%" data-duration="5" data-easing="true" data-delay="1">
						<div class="xe-icon">
							<i class="fa-clock-o"></i>
						</div>
						<div class="xe-label">
								
                            <strong class="num"><a  href="../DC/Timesheets/AddTimesheets.aspx">Enter Timesheet</a></strong>
							<span><a  href="#">Watch Video</a></span>
						</div>
					</div>
				
				</div>
</div>
    <div class="row" runat="server" id="OnlyForPmsandAdmin" >
        <div class="col-sm-4" id="link_customers" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-info">
						<div class="xe-icon">
							<i class="fa-calculator"></i>
						</div>
						<div class="xe-label">
						    
                            <strong class="num"> <a  href="../DC/Expenses/AddExpenses.aspx">Enter Expenses</a></strong>
							<span><a  href="#">Watch Video</a></span>
						</div>
					</div>
					
				</div>
	    <div class="col-sm-4" id="link_timesheets" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-info">
						<div class="xe-icon">
							<i class="fa-cc-visa" ></i>
						</div>
						<div class="xe-label">
							
                            <strong class="num"><a  href="../DC/TakePaymentNow.aspx">Take Payment Now</a></strong>
							<span><a  href="#">Watch Video</a></span>
						</div>
					</div>
				
				</div>
		<div class="col-sm-4" id="link_user" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-info">
						<div class="xe-icon">
							<i class="fa-line-chart"></i>
						</div>
						<div class="xe-label">
							
                            <strong class="num"><a  href="../DC/FRPApprovals.aspx">Track Invoices</a></strong>
							<span><a  href="#">Watch Video</a></span>
						</div>
					</div>
				
				</div>
		<%--<div class="col-sm-3" id="link_suppliers" runat="server">
					
					<div class="xe-widget xe-counter xe-counter-red"
                          data-count=".num" data-from="0" data-to="57">
						<div class="xe-icon">
							<i class="linecons-truck" style="background-color:#944dff;"></i>
						</div>
						<div class="xe-label">
						  	<a style="font-size:large;" href="../Vendors/RFIVendors.aspx">Manage Suppliers</a>
						</div>
					</div>
				
				</div>--%>
</div>


	<div class="row">
       <div class="col-md-4">
           <div class="card shadow-sm">
                            <div class="card-header">
                                 Jobs
                             </div>
                            <div class="panel-body" style="overflow-x:auto;">
                             <table id="students" class="table table-striped table-bordered"></table>
                       </div>
                   </div>
           </div>
        <div class="col-md-4">
            <div class="card shadow-sm">
                     <div class="card-header">
                                 Grand Total Invoiced This Year
                             </div>
                     <div class="panel-body">
                                 <div class="super-large text-black text-center" data-count="this" data-from="0" data-to="16" data-duration="2"><span id="totalbystatus">0.00</span></div> 
                    <div id="InvoiceSum-dnut" style="width:100%;height:370px;"></div>
                    </div>
                            
                    </div>
           </div>
        <div class="col-md-4">
            <div class="card shadow-sm">
                                <div class="card-header">
                                   Card Payments This Month
                                </div>
                                <div class="panel-body" data-max-height="300" style="width:100%;height:485px;overflow-x:auto;">
                                 	
                                     <div id="StatusGraph" style="width:100%;height:300px;"></div>
                                </div>
                          </div>
           </div>
        </div>
    <script>
        $(document).ready(function () {
            GetInvoiceSumBystatus();
            GetCompletedStakeBar();
        });

        function thousands_separators(num) {
            var num_parts = num.toString().split(".");
            num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return num_parts.join(".");
        }
        function GetInvoiceSumBystatus() {
            var xenonPalette = ['#00B0F0', '#92D050', '#0070C0', '#B4c6e7', '#ED7D31', '#00b19d', '#ff6264', '#f7aa47'];


            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetInvoiceSumByStatusThisYear',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        var ds = [];
                        var total1 = 0.00;
                        for (var i = 0; i < newData.length; i++) {
                            ds.push({
                                region: newData[i].name,
                                val: newData[i].value
                            });
                            total1 = total1 + newData[i].value;
                        }
                        debugger;
                        $('#totalbystatus').html(thousands_separators(total1.toFixed(2)));

                        $("#InvoiceSum-dnut").dxPieChart({
                            type: "doughnut",
                            dataSource: ds,
                            tooltip: {
                                enabled: true,
                                format: "fixedpoint",
                                customizeTooltip: function (arg) {
                                    return {
                                        text: arg.argumentText + " - " + arg.valueText
                                    };
                                }
                            },

                            //legend: {
                            //    verticalAlignment: "right",
                            //    horizontalAlignment: "riggh",
                            //    itemTextPosition: 'top'
                            //},
                            series: [{
                                type: "doughnut",
                                argumentField: "region",
                                label: {
                                    visible: true,
                                    format: "fixedPoint",
                                    //format: {
                                    //    type: "fixedPoint",
                                    //   // precision: 2,
                                    //   // percentPrecision: 2
                                    //},
                                    connector: {
                                        visible: true
                                    },

                                }
                            }],
                            palette: xenonPalette
                        });


                    }
                }
            });
        }
        function GetCompletedStakeBar() {
            //var fromdate = $("[id$='txtLoggedStartDate']").val();
            //var todate = $("[id$='txtLoggedEndDate']").val();
            //var search = $("[id$='ticketno']").val();


            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetDataByCardPayment',
                type: "POST",
                data: "{}",
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
                                { valueField: "SalesPrice", name: "Card Payment" },
                                { valueField: "TotalCosts", name: "Non Card Payment" },
                               // { valueField: "Profit", name: "Profit" },
                            ],
                            legend: {
                                //visible:false
                                verticalAlignment: "bottom",
                                horizontalAlignment: "center",
                                itemTextPosition: 'top',

                            },
                            valueAxis: {
                                title: {
                                    text: "Amount"
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
	<script type="text/javascript">

      $(document).ready(function () {
          GetAssetRecords(0);
          GetPayRecords(0);
      });

        function GetPayRecords(id) {
            try {
                $.ajax({
                    url: "../../WF/DC/webservices/DCServices.asmx/BindPayDetails",
                    type: "POST",
                    data: "{'id': '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {

                        var NewData = jQuery.parseJSON(data.d);
                        var x = "<thead><tr><th style='width:10%;color: #ffffff;'>Job Ref</th>" +
                            "<th style='width:20%;color: #ffffff;'>ID</th>"
                            + "<th  style='width:50%;color: #ffffff;'>Details</th>"
                            //+ "<th  style='width:10%;color: #ffffff;'>Assigned Smart Tech</th>"
                            //+ "<th style='width:10%;color: #ffffff;'>Scheduled Date</th>"
                            + "<th  style='width:20%;color: #ffffff;'>Paid Amount</th>"
                            + "</thead>";
                        x = x + "<tbody>"

                        for (var i = 0; i < NewData.length; i++) {
                            var CCID = NewData[i].CCID
                            var ID = NewData[i].ID
                            var LoggedDate = NewData[i].LoggedDate;
                            var Details = NewData[i].Details;
                           // var AssignedTechnician = NewData[i].AssignedTechnician
                           // var StatusName = NewData[i].StatusName;
                            var PaidAmount = NewData[i].PaidAmount;

                            x = x + "<tr><td>" + ButtonHtml(ID, CCID)
                                + "</td><td>" + CCID
                                + "</td><td>" + Details
                                + "</td><td style=direction:rtl;text-align:right;>" + PaidAmount
                                //+ "</td><td>" + AssignedTechnician
                                //+ "</td><td style=direction:rtl>" + LoggedDate
                               // + "</td><td class='New'><span class='statuscls' style='color: white;font-weight: bold;'>" + StatusName + "</span></td>" +
                                "</td></tr>";
                        }

                        x = x + "</tbody>";
                        $("#tblpayment").empty();
                        $("#tblpayment").append(x);
                        BindPayTable();
                        $("#tblpayment").removeClass("no-footer");
                       // setStatusBackColor();
                    }
                });
            }
            catch (e) {
                var err = e;
            }
        }
        function BindPayTable() {
            var table = $('#tblpayment').DataTable({
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
                    "targets": [1],
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
      function GetAssetRecords(id) {
          try {
              $.ajax({
                  url: "../../WF/DC/webservices/DCServices.asmx/BindCallAssets",
                  type: "POST",
                  data: "{'id': '" + id + "'}",
                  contentType: 'application/json; charset=utf-8',
                  dataType: "json",
                  async: true,
                  success: function (data) {

                      var NewData = jQuery.parseJSON(data.d);
                      var x = "<thead><tr><th style='width:8%;color: #ffffff;'>Job Ref</th><th style='width:5%;color: #ffffff;'>ID</th>"
                          + "<th  style='width:25%;color: #ffffff;'>Details</th><th  style='width:10%;color: #ffffff;'>Assigned Smart Tech</th><th style='width:10%;color: #ffffff;'>Scheduled Date</th><th  style='width:10%;color: #ffffff;'>Status</th></thead>";
                      x = x + "<tbody>"

                      for (var i = 0; i < NewData.length; i++) {
                          var CCID = NewData[i].CCID
                          var ID = NewData[i].ID
                          var LoggedDate = NewData[i].LoggedDate;
                          var Details = NewData[i].Details;
                          var AssignedTechnician = NewData[i].AssignedTechnician
                          var StatusName = NewData[i].StatusName;

                          x = x + "<tr><td>" + ButtonHtml(ID, CCID)
                              + "</td><td>" + CCID
                              + "</td><td>" + Details
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
                      "targets": [1],
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

      function ButtonHtml(Id, ccid) {
          var HtmlText = " <a target='_blank' id=Link" + Id + " href='/WF/DC/FLSForm.aspx?CCID=" + ccid + "&CallID=" + Id+ "&SDID=" + Id + "' style=' font-weight: bold'>" + ccid + "</a>";
          //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
          return HtmlText;
      }



    </script>

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
                else if (s == 'Authorized')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
            });


        }

     </script>
 
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/fixedcolumns/3.2.4/css/fixedColumns.dataTables.min.css">

    <link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="/Web/examples/resources/syntax/shCore.css">--%>
   <%-- <link rel="stylesheet" type="text/css" href="/Web/examples/resources/demo.css">--%>
     
   <%-- <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>--%>
   
     
    <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

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
    </script>
   
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
    }
        div.dt-buttons{
            float:right;
        }
    </style>

      <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
