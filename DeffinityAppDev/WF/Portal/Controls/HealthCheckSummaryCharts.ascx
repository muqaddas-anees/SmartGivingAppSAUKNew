<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Customer_Health_controls_Summary" Codebehind="HealthCheckSummaryCharts.ascx.cs" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>

<%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
<script>
    var xenonPalette = ['#68b828', '#7c38bc', '#0e62c7', '#fcd036', '#4fcdfc', '#00b19d', '#ff6264', '#f7aa47'];
</script>
<script>
    $(document).ready(function () {
        GetlastWeekCompletedHcData();
        GetlastMonthCompletedHcData();
        GetlastYearCompletedHcData();
    });
    function GetlastWeekCompletedHcData() {
        $.ajax({
            url: '../Health/HC/HCWebService.asmx/lastWeekCompletedHcData',
            type: "POST",
            data: "{}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var datatable1 = [];
                debugger;
                var Newdt = jQuery.parseJSON(data.d);
                for (var i = 0; i < Newdt.length; i++) {
                    datatable1.push({ Name: Newdt[i].Name, val: Newdt[i].Value });
                }
                debugger;
                $("#DivlastWeekCompletedHc").dxPieChart({
                    dataSource: datatable1,
                    title: "",
                    palette: xenonPalette,
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: this.argumentText + "<br/>" + this.valueText
                            };
                        }
                    },
                    legend: {
                        visible: true,
                        horizontalAlignment: "right",
                        verticalAlignment: "bottom",
                    },
                    size: {
                        height: 220
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "Name",
                        label: {
                            visible: true,
                            connector: {
                                visible: true
                            }
                        }
                    }]
                });
            }
        });
    };
    function GetlastMonthCompletedHcData() {
        $.ajax({
            url: '../Health/HC/HCWebService.asmx/lastMonthCompletedHcData',
            type: "POST",
            data: "{}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var datatable1 = [];
                debugger;
                var Newdt = jQuery.parseJSON(data.d);
                for (var i = 0; i < Newdt.length; i++) {
                    datatable1.push({ Name: Newdt[i].Name, val: Newdt[i].Value });
                }
                debugger;
                $("#DivlastMonthCompletedHc").dxPieChart({
                    dataSource: datatable1,
                    title: "",
                    tooltip: {
                        enabled: true,
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    size: {
                        height: 220
                    },
                    legend: {
                        visible: true,
                        horizontalAlignment: "right",
                        verticalAlignment: "bottom",
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "Name",
                        label: {
                            visible: true,
                            connector: {
                                visible: true
                            }
                        }
                    }],
                    palette: xenonPalette
                });
            }
        });
    };
    function GetlastYearCompletedHcData() {
        $.ajax({
            url: '../Health/HC/HCWebService.asmx/lastYearCompletedHcData',
            type: "POST",
            data: "{}",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var datatable1 = [];
                debugger;
                var Newdt = jQuery.parseJSON(data.d);
                for (var i = 0; i < Newdt.length; i++) {
                    datatable1.push({ Name: Newdt[i].Name, val: Newdt[i].Value });
                }
                debugger;
                $("#DivlastYearCompletedHc").dxPieChart({
                    dataSource: datatable1,
                    title: "",
                    tooltip: {
                        enabled: true,
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    size: {
                        height: 220
                    },
                    legend: {
                        visible: true,
                        horizontalAlignment: "right",
                        verticalAlignment: "bottom",
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "Name",
                        label: {
                            visible: true,
                            connector: {
                                visible: true
                            }
                        }
                    }],
                    palette: xenonPalette
                });
            }
        });
    };
</script>
<div class="form-group">
      <div class="col-md-4">
          <div style="font-size:large;">Completed Health Checks for this Week</div>
           <br /><br />
          <div id="DivlastWeekCompletedHc"></div>
          <div id="DivlastWeekCompletedHcSpan" style="height:60px;max-width:500px;margin:5px auto;text-align:center"></div>
      </div>
	<div class="col-md-4">
         <div style="font-size:large;">Completed Health Checks for this Month</div>
          <br /><br />
          <div id="DivlastMonthCompletedHc"></div>
	</div>
	<div class="col-md-4">
         <div style="font-size:large;">Completed Health Checks for this Year</div>
          <br /><br />
            <div id="DivlastYearCompletedHc"></div>
	</div>
</div>



