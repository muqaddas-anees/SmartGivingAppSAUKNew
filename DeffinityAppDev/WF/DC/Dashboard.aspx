<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
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
     <div class="row">
      <div class="col-md-6">

          <div class="card shadow-sm">
                     <div class="card-header">
                                 Completed Jobs By Month & Type
                             </div>
                     <div class="panel-body">
                                <div id="StatusGraph" style="width:100%;height:300px;"></div>
                            </div>
                    </div>

          </div>

           <div class="col-md-6">

          <div class="card shadow-sm">
                     <div class="card-header">
                                 Current Job Status this Month
                             </div>
                     <div class="panel-body">
                                <div id="StatusGraphMonth" style="width:100%;height:300px;"></div>
                            </div>
                    </div>

          </div>
         </div>

     <div class="row" >
        <div class="col-sm-4">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-lightbulb-o" style="background-color: #FF6264;color:#fff;"></i>
						</div>
                        <div class="xe-label">
							<strong class="num"> <label id="lblInvoicedToday" runat="server">0</label> </strong>
							<span>Invoices Raised Today</span>
						</div>
						<%--<div class="xe-label">
							<a style="font-size:large;" href="../../WF/CustomerAdmin/PortfolioContacts.aspx">Manage CRM</a>
						</div>--%>
					</div>
					
				</div>	
        <div class="col-sm-4">
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon ">
							<i class="fa-phone" style="background-color: #FF6264;color:#fff;"></i> 
						</div>
						 <div class="xe-label">
							<strong class="num"> <label id="lblInvoicedWeek" runat="server">0</label> </strong>
							<span>Invoiced Week to Date</span>
						</div>
					</div>
				
				</div>
        <div class="col-sm-4">
					
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<i class="fa-check-circle" style="background-color: #FF6264;color:#fff;"></i>
						</div>
						 <div class="xe-label">
							<strong class="num"> <label id="lblInvoicedMonth" runat="server">0.00</label> </strong>
							<span>Invoiced Month to Date</span>
						</div>
					</div>
				
				</div>
	  
   </div>
    
    <div class="row" >
        <div class="col-sm-4">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-bar-chart" style="background-color: #FF6264;color:#fff;"></i>
						</div>
                        <div class="xe-label">
							<strong class="num"> <label id="lblInvoicedYear" runat="server">0.00</label> </strong>
							<span>Invoiced Year to Date</span>
						</div>
						
					</div>
					
				</div>	
        <div class="col-sm-4">
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<i class="fa-line-chart" style="background-color: #FF6264;color:#fff;"></i> 
						</div>
						 <div class="xe-label">
							<strong class="num"> <label id="lblInvoicedAwaiting" runat="server">0.00</label> </strong>
							<span>Total Value of Invoices Awaiting Payment</span>
						</div>
					</div>
				
				</div>
        <div class="col-sm-4">
					
					<div class="xe-widget xe-counter xe-counter-blue" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<i class="fa-wrench" style="background-color: #FF6264;color:#fff;"></i>
						</div>
						 <div class="xe-label">
							<strong class="num"> <label id="lblInvoicedPaid" runat="server"></label> </strong>
							<span>YTD Value of Jobs using Card Payments</span>
						</div>
					</div>
				
				</div>
	  
   </div>


    <div class="row">
      <div class="col-md-6">

          <div class="card shadow-sm" style="height: 465px;">
                     <div class="card-header">
                                 Team Performance This Month
                             </div>
                     <div class="panel-body">
                               <div class="scrollable ps-container ps-active-y" data-max-height="350" style="max-height:350px;">
                         <div class='row' id="div_items">
                             </div> 
                         <%--<div class='col-sm-6'>	
                             <div class='xe-widget xe-counter card' >
						<div class='xe-icon cardpad' style='padding:10px'>
							<img src='http://localhost:55411/WF/Admin/ImageHandler.ashx?type=user&id=0' class='img-circle' width='60'>
						</div>
						 <div class='xe-label cardpad'>
							<strong class='num'> <label class='namefont'>Money Due from</label> </strong>
							<span class='valuefont'>10 </span>
						</div>
					</div>
                             </div>--%>
                         
                             </div>

                            </div>
                    </div>

          </div>
        <div class="col-md-6">

            <div class="card shadow-sm">
				<div class="card-header">
					Grand Total Invoiced This Year
				</div>
				<div class="panel-body">
                    <%--<div class="chart-item-bg-2">
						<div class="chart-item-num" data-count="this" data-from="0" data-to="98" data-suffix="%" data-duration="2"> </div>
						<%--<div class="chart-item-desc">
							<p class="col-lg-5"></p>
						</div>
						<div class="chart-item-env">
							
						</div>
					</div>--%>

                   <div class="super-large text-black text-center" data-count="this" data-from="0" data-to="16" data-duration="2"><span id="totalbystatus">0.00</span></div> 
                    <div id="InvoiceSum-dnut" style="width:100%;height:270px;"></div>
                    </div>
                </div>
            </div>
          
        </div>
    <div class="row" style="display:none;visibility:hidden;">
        <div class="col-md-12">
        <div class="card shadow-sm">
				<div class="card-header">
					Total Invoiced
				</div>
				<div class="panel-body">
					
					<div class="row">
						<div class="col-sm-3">
							<p class="text-medium">View of how much you have invoiced over the past 12 months</p>
							<div class="super-large text-purple" data-count="this" data-from="0" data-to="16" data-duration="2"><span id="lbltotal">0</span></div>
						</div>
						<%--<div class="col-sm-3">
							<div id="cpu-usage" style="height: 150px;"></div>
						</div>--%>
						<div class="col-sm-9">
							<div id="totalinvoiced-chart" style="height: 150px;"></div>
						</div>
					</div>
					
				</div>
			</div>
            </div>
    </div>

    	<div class="row">
				<div class="col-sm-12">
				
					
				</div>
			</div>

    <script type="text/javascript">

        function thousands_separators(num) {
            var num_parts = num.toString().split(".");
            num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return num_parts.join(".");
        }
        $(document).ready(function () {
            GetCompletedStakeBar();
            GetThismonthDnut();
            GetTeamPerformance();
            GetInvoiceGraph();
            GetInvoiceSumBystatus();

        });

      

        function GetCompletedStakeBar() {
            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetDataByStatus',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        //var ds = [];
                        //for (var i = 0; i < newData.length; i++) {
                        //    ds.push({
                        //        Name: newData[i].Name,
                        //        Value: newData[i].Value
                        //    });
                        //}
                        var xenonPalette = ['#83D3F1', '#B7DD98', '#BE9CDE', '#FFBA00', '#55ACEE', '#3B5998', '#68B828'];
                        $("#StatusGraph").dxChart({
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
                                // customizeText: function (pointInfo) {
                                //    debugger;
                                //    if (pointInfo.seriesName === "Fault") {
                                //        return { seriesColor: "#83D3F1", seriesIndex: 0, seriesName: 'Fault' };
                                //    } else if (pointInfo.seriesName === "Inspection") {
                                //        return { seriesColor: "#B7DD98", seriesIndex: 1, seriesName: SmartGivingDB };
                                //    } else if (pointInfo.seriesName === "Maintenance") {
                                //        return { seriesColor: "#BE9CDE", seriesIndex: 2, seriesName: 'Maintenance' };
                                //    } else if (pointInfo.seriesName === "Installation") {
                                //        return { color: "#FFBA00", hoverStyle: { color: "#FFBA00" } };
                                //    } else if (pointInfo.seriesName === "Repair") {
                                //        return { color: "#55ACEE", hoverStyle: { color: "#55ACEE" } };
                                //    } else if (pointInfo.seriesName === "Service") {
                                //        return { color: "#3B5998", hoverStyle: { color: "#3B5998" } };
                                //    } else if (pointInfo.seriesName === "Upgrade") {
                                //        return { color: "#68B828", hoverStyle: { color: "#68B828" } };
                                //    }
                                //}
                                //,
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

        function GetThismonthDnut() {
            var xenonPalette = ['#00B0F0', '#92D050', '#0070C0', '#B4c6e7', '#ED7D31', '#00b19d', '#ff6264', '#f7aa47'];


            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetDataByStatusthisMonth',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        var ds = [];
                        for (var i = 0; i < newData.length; i++) {
                            ds.push({
                                region: newData[i].Name,
                                val: newData[i].Value
                            });
                        }
                        //debugger;
                        $("#StatusGraphMonth").dxPieChart({
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
                            customizePoint: function (pointInfo) {
                                //debugger;
                                if (pointInfo.argument === "New") {
                                    return { color: "#00B0F0", hoverStyle: { color: "#00B0F0" } };
                                } else if (pointInfo.argument === "Scheduled") {
                                    return { color: "#92D050", hoverStyle: { color: "#92D050" } };
                                } else if (pointInfo.argument === "Closed") {
                                    return { color: "#0070C0", hoverStyle: { color: "#0070C0" } };
                                } else if (pointInfo.argument === "Awaiting Accepted") {
                                    return { color: "#B4c6e7", hoverStyle: { color: "#B4c6e7" } };
                                } else if (pointInfo.argument === "Quote Accepted") {
                                    return { color: "#ED7D31", hoverStyle: { color: "#ED7D31" } };
                                } 
                            },
                            customizeLabel: function () {
                                return {
                                    visible: true,
                                    customizeText: function () {
                                        //debugger;
                                        return this.argumentText + " - " + this.valueText
                                    }
                                };
                            },
                            legend: {
                                visible: false
                                //verticalAlignment: "bottom",
                                //horizontalAlignment: "center",
                                //itemTextPosition: 'top'
                            },
                            series: [{
                                type: "doughnut",
                                argumentField: "region",
                                label: {
                                    visible: true,
                                    format: "fixedpoint",
                                    connector: {
                                        visible: true
                                    }
                                }
                            }],
                            palette: xenonPalette
                        });


                    }
                }
            });
        }

        function GetHtmlString(img,name,value) {
            var r = "<div class='col-sm-6'><div class='xe-widget xe-counter card'><div class='xe-icon cardpad' style='padding:10px'>";
            r = r + "<img src='" + img + "' class='img-circle' width='60'></div><div class='xe-label cardpad'>";
            r = r + "<strong class='num'> <label class='namefont'>"+name+"</label> </strong>";
            r = r + "<span class='valuefont'>Jobs Completed: <b>" + value + "</b> </span></div></div></div>";
            return $($.parseHTML(r));
        }

        function GetTeamPerformance() {

            
            // div - TeamPerfomance
            //GetDataByTeamthisMonth


            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetDataByTeamthisMonth',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        //var ds = [];
                        for (var i = 0; i < newData.length; i++) {

                            //debugger;
                            $("#div_items").append(GetHtmlString('../Admin/ImageHandler.ashx?type=user&id=' + newData[i].Value2, newData[i].Name, newData[i].Value1));
                        }
                    }
                }
            });
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
                                    format:"fixedPoint",
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

        function GetInvoiceGraph() {
            //var cpu_usage_data = [
            //    { time: new Date("October 02, 2014 01:00:00"), usage: 75 },

            //];

            $.ajax({
                url: '../DC/webservices/DCServices.asmx/GetInvoiceDataThisYear',
                type: "POST",
                data: "{}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        var newData = jQuery.parseJSON(data.d);
                        var ds = [];
                        var total = 0.00;
                        for (var i = 0; i < newData.length; i++) {
                            ds.push({
                                time: new Date(newData[i].date),
                                usage: newData[i].value
                            });
                            total = total + newData[i].value;
                        }
                        $('#lbltotal').html(thousands_separators(total.toFixed(2)));
                        debugger;
                        $("#totalinvoiced-chart").dxChart({
                            dataSource: ds,
                            commonPaneSettings: {
                                border: {
                                    visible: true,
                                    color: '#f5f5f5'
                                }
                            },

                            commonSeriesSettings: {
                                type: "area",
                                argumentField: "time",
                                border: {
                                    color: '#7c38bc',
                                    width: 1,
                                    visible: true
                                }
                            },
                            series: [
                                { valueField: "usage", name: "Invoice amount", color: '#7c38bc', opacity: .5 },
                            ],
                            commonAxisSettings: {
                                label: {
                                    visible: true
                                },
                                grid: {
                                    visible: true,
                                    color: '#f5f5f5'
                                }
                            },
                            argumentAxis: {
                                valueMarginsEnabled: false,
                                label: {
                                    customizeText: function (arg) {
                                        return date('m/d/yy', arg.value);
                                        //return date('h:i A', arg.value);
                                    }
                                },
                            },
                            legend: {
                                visible: false
                            }
                        });


                    }
                }
            });

        }
    </script>

     <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">

</asp:Content>
