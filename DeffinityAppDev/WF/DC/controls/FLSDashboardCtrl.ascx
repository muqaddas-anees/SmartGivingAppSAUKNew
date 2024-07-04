<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DC_controls_FLSDashboardCtrl" Codebehind="FLSDashboardCtrl.ascx.cs" %>
<%--<script src="Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
<script src="https://www.google.com/jsapi" type="text/javascript"></script>
<script type="text/javascript">
    // Global variable to hold data
    google.load('visualization', '1.1', { packages: ['corechart'] });


    //Category by Site - chart
    google.setOnLoadCallback(drawchart1);
    //Service Request by Site - chart
    google.setOnLoadCallback(drawchart2);
    //Tasks by Source  - chart
    google.setOnLoadCallback(drawchart3);
    //Tasks by Team - chart
    google.setOnLoadCallback(drawchart4);
    
   
    function drawchart1() {
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        var requestType = $('#ddlRequestType').val();
        var customerId = $('#ddlCustomer').val();
        var statusId = $('#ddlStatus').val();
        var typeName = $("#ddlRequestType option:selected").text();
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'FLSDashboard.aspx/GetChartData1',
                data: "{fromDate:'" + fromDate + "',toDate:'" + toDate + "',requestType:'" + requestType + "',customerId:'" + customerId + "',statusId:'" + statusId + "'}",
                success:
        function (response) {
            //alert(response.d);

            var data1 = new google.visualization.DataTable(response.d);
            var noOfRows = data1.getNumberOfRows()
            var lblText = data1.getValue(noOfRows - 1, 0);
            data1.removeRow(noOfRows - 1);

            $('#lblChart1Text').text(lblText);
            var options1 = {
                //width: 600,
                width: '100%',
                height: 400,
                title: typeName + " Category by Site", titleTextStyle: { color: 'gray', fontSize: '13' },
                isStacked: true
            }
            new google.visualization.ColumnChart(document.getElementById('chart1')).
                            draw(data1, options1);
        },
                error: function () {
                    alert("Error loading data.Please try again.");
                }
            });
        });
    }

    function drawchart2() {
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        var requestType = $('#ddlRequestType').val();
        var customerId = $('#ddlCustomer').val();
        var statusId = $('#ddlStatus').val();
        var typeName = $("#ddlRequestType option:selected").text();
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'FLSDashboard.aspx/GetChartData2',
                data: "{fromDate:'" + fromDate + "',toDate:'" + toDate + "',requestType:'" + requestType + "',customerId:'" + customerId + "',statusId:'" + statusId + "',requestName:'" + typeName + "'}",
                success:
function (response) {

    var bottomText = '';
    var dataValues = response.d;
    var data2 = new google.visualization.DataTable();
    data2.addColumn('string', 'Column Name');
    data2.addColumn('number', 'Column Value');
    for (var i = 0; i < dataValues.length; i++) {
        if (i == dataValues.length - 1) {
            bottomText = dataValues[i].Name;
        }
        else {
            data2.addRow([dataValues[i].Name, dataValues[i].Count]);
        }
    }
    $('#lblChart2Text').text(bottomText);
    var options2 = {
        //width: 600,
        width: '100%',
        height: 400,
        title: typeName + " by Site", titleTextStyle: { color: 'gray', fontSize: '13' },
        legend: { position: 'bottom', maxLines: 3 },
        bar: { groupWidth: '75%' },
        isStacked: true
    }
    new google.visualization.PieChart(document.getElementById('chart2')).
draw(data2, options2);
},
                error: function () {
                    alert("Error loading data.Please try again.");
                }
            });
        })
    }

    function drawchart3() {
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        var requestType = $('#ddlRequestType').val();
        var customerId = $('#ddlCustomer').val();
        var statusId = $('#ddlStatus').val();
        var typeName = $("#ddlRequestType option:selected").text();
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'FLSDashboard.aspx/GetChartData3',
                data: "{fromDate:'" + fromDate + "',toDate:'" + toDate + "',requestType:'" + requestType + "',customerId:'" + customerId + "',statusId:'" + statusId + "'}",
                success:
function (response) {
    var bottomText = '';
    var dataValues = response.d;
    var data3 = new google.visualization.DataTable();
    data3.addColumn('string', 'Column Name');
    data3.addColumn('number', 'Column Value');
    for (var i = 0; i < dataValues.length; i++) {
        if (i == dataValues.length - 1) {
            bottomText = dataValues[i].Name;
        }
        else {
            data3.addRow([dataValues[i].Name, dataValues[i].Count]);
        }
    }
    $('#lblChart3Text').text(bottomText);
    var options3 = {
        //width: 600,
        width: '100%',
        height: 400,
        title: typeName + " Tasks by Source", titleTextStyle: { color: 'gray', fontSize: '13' },
        legend: { position: 'right', maxLines: 3 },
        vAxis: { minValue: 0, maxValue: 100, format: '#\'%\'' }

    }
    new google.visualization.PieChart(document.getElementById('chart3')).
draw(data3, options3);
},
                error: function () {
                    alert("Error loading data.Please try again.");
                }
            });
        })
    }

    function drawchart4() {
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        var requestType = $('#ddlRequestType').val();
        var customerId = $('#ddlCustomer').val();
        var statusId = $('#ddlStatus').val();
        var typeName = $("#ddlRequestType option:selected").text();
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'FLSDashboard.aspx/GetChartData4',
                data: "{fromDate:'" + fromDate + "',toDate:'" + toDate + "',requestType:'" + requestType + "',customerId:'" + customerId + "',statusId:'" + statusId + "'}",
                success:
function (response) {
    var bottomText = '';
    var dataValues = response.d;
    var data4 = new google.visualization.DataTable();
    data4.addColumn('string', 'Column Name');
    data4.addColumn('number', 'Tasks');
    for (var i = 0; i < dataValues.length; i++) {
        if (i == dataValues.length - 1) {
            bottomText = dataValues[i].Name;
        }
        else {
            data4.addRow([dataValues[i].Name, dataValues[i].Count]);
        }
    }
    $('#lblChart4Text').text(bottomText);
    var options4 = {
        //width: 600,
        width: '100%',
        height: 400,
        title: typeName + " Tasks by Team", titleTextStyle: { color: 'gray', fontSize: '13' },
        legend: { position: 'right', maxLines: 3 },
        bar: { groupWidth: '75%' }
    }
    new google.visualization.ColumnChart(document.getElementById('chart4')).
draw(data4, options4);
},
                error: function () {
                    alert("Error loading data.Please try again.");
                }
            });
        })
    }
         
   
</script>
<asp:ValidationSummary ID="val1" runat="server" DisplayMode="BulletList" ValidationGroup="fls" />
<div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FromDate%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgFromDate" runat="server" SkinID="Calender" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="rfvStartedDate" runat="server" ControlToValidate="txtFromDate"
                Display="Dynamic" ErrorMessage="Please enter from date" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter valid date"
                ControlToValidate="txtFromDate" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ToDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" SkinID="Date">
            </asp:TextBox>
            <asp:Label ID="imgEndDate" runat="server" SkinID="Calender" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgEndDate" TargetControlID="txtToDate">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                Display="Dynamic" ErrorMessage="Please enter to date" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid date"
                ControlToValidate="txtToDate" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
					</div>
				</div>
</div>
<div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_80" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
            </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequestType%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlRequestType" SkinID="ddl_80" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
					</div>
				</div>
</div>
<div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Status%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8"> <asp:Button ID="BtnSearch" runat="server" SkinID="btnSearch"  
                 ValidationGroup="fls" />
					</div>
				</div>
</div>
<div class="row">
                                <div class="col-md-6">
                                     <div id="chart1">
            </div>
            <asp:Label ID="lblChart1Text" runat="server" ClientIDMode="Static"></asp:Label>
                                    </div>
                                  <div class="col-md-6">
                                       <div id="chart2">
            </div>
            <asp:Label ID="lblChart2Text" runat="server" ClientIDMode="Static"></asp:Label>
                                    </div>
                                 </div>
<div class="row">
                                <div class="col-md-6">
                                     <div id="chart3">
            </div>
            <asp:Label ID="lblChart3Text" runat="server" ClientIDMode="Static"></asp:Label>
                                    </div>
                                  <div class="col-md-6">
                                       <div id="chart4">
            </div>
            <asp:Label ID="lblChart4Text" runat="server" ClientIDMode="Static"></asp:Label>
                                    </div>
                                 </div>

