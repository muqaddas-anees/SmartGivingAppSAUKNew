

$(document).ready(function () {


    //Engineer graphs
    GetChartData1();
    GetChartData2();
    GetChartData3();
    var Cname = $("#ddlCustomerInEngineer").val();
    if (Cname != undefined) {
        GetChartData4(Cname);
    }
    //Category graphs
    //GetBarchartDataofCategory1();
    //GetBarchartDataofCategory2();
    //GetBarchartDataofCategory3();
    // GetDataOfRevenueByCategory();
    //Site graphs
    var FromDate = $("#txtFromdate").val();
    var ToDate = $("#txttodate").val();
    var CnameInSite = $("#ddlCustomer").val();
    debugger;
    if (CnameInSite != undefined && FromDate != undefined && ToDate != undefined) {
        GetDataInBtnClickSite1(CnameInSite, FromDate, ToDate);
        GetDataInBtnClickSite2(CnameInSite, FromDate, ToDate);
        GetDataInBtnClickSite3(CnameInSite, FromDate, ToDate);
    }
   //site graphs in type of request change
    var TechIds = "";
    var CustomerInCat = $("#ddlCustomerCat1").val();
    var RequestTypeInCat = $("#ddlRequestTypeCat").val();
    if (CustomerInCat != undefined && RequestTypeInCat != undefined) {
        $("#Label1").empty();
        $("#Label2").empty();
        $("#Label3").empty();
        $("#Label1").append("Volume of Open Calls By Category from " + FromDate + " To " + ToDate);
        $("#Label2").append("Volume of Completed Calls By Category from " + FromDate + " To " + ToDate);
        $("#Label3").append("Calls by Category from " + FromDate + " To " + ToDate);

        GetChartData1InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat);
        GetChartData2InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat);
        GetChartData3InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat);
    }


    //Billing report
    var CnameInBilling = $("#ddlCustomerInBilling").val();
    if (CnameInBilling != undefined && FromDate != undefined && ToDate != undefined) {
        GetDataOfRevenueByCategoryInBtnClick(FromDate, ToDate, CnameInBilling);
        GetDataOfRevenueBySiteInBtnClick(FromDate, ToDate, CnameInBilling);
    }

    Customer_charts();
    $("#BtnSearchByCustomer").click(function () {

        if (Page_IsValid) {
            Customer_charts();
            //var ToDate = "";
            //var FromDate = "";
            //debugger;
            //FromDate = $("#txtFromdate").val();
            //ToDate = $("#txttodate").val();
            //var Cname = $("#ddlCustomerChart1").val();
            //var Rtype = $("#ddlRequestType").val();
            //debugger;
            //if (Cname != 0) {
            //    debugger;
            //    GetDataCustomerPiechart(Cname, Rtype, FromDate, ToDate);
            //    GetDataCustomerCategory(Cname, Rtype, FromDate, ToDate);
            //    GetDataCustomerEngineer(Cname, Rtype, FromDate, ToDate);
            //    debugger;
            //    return false;
            //}
        }
        return false;
    });

    function get_checklist(ctrlname) {
        debugger;
        var ResourceIDs = '';
        try {
            var RB1 = document.getElementById(ctrlname);
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");
            ResourceIDs = '';
            for (var i = 0; i < radio.length; i++) {

                if (radio[i].checked == true) {

                    ResourceIDs = ResourceIDs + radio[i].parentNode.getElementsByTagName('label')[0].parentNode.getElementsByTagName('span')[0].title + ',';
                }

            }
        }
        catch (err)
        { }
        return ResourceIDs;
    }

    $("#BtnSearchEngineer").click(function () {
        if (Page_IsValid) {
            debugger;
            var checked_checkboxes = $("[id*=NamesCheckList] input:checked");
            var TechIds = "";
            var ToDate = "";
            var FromDate = "";
            checked_checkboxes.each(function () {
                var value = $(this).val();
                TechIds += +value;
                TechIds += ",";
            });
            FromDate = $("#txtFromdate").val();
            ToDate = $("#txttodate").val();


            $("#Label1").empty();
            $("#Label2").empty();
            $("#Label3").empty();
            $("#Label4").empty();
            $("#Label1").append("Volume of Open Calls By Engineer from " + FromDate + " To " + ToDate);
            $("#Label2").append("Volume of Completed Calls By Engineer from " + FromDate + " To " + ToDate);
            $("#Label3").append("Number of Calls by Status and by Engineer from " + FromDate + " To " + ToDate);
            $("#Label4").append("Calls Completed During " + FromDate + " To " + ToDate + " by Site");



            GetChartData1InBtnClick(TechIds, FromDate, ToDate);
            GetChartData2InBtnClick(TechIds, FromDate, ToDate);
            GetChartData3InBtnClick(TechIds, FromDate, ToDate);
            var Cname = $("#ddlCustomerInEngineer").val();
            if (Cname != 0) {
                GetChartData4InBtnClick(TechIds, FromDate, ToDate, Cname);
            }
        }
        return false;
    });
    $("#BtnSearchCat").click(function () {
        if (Page_IsValid) {
            var checked_checkboxes = $("[id*=checklistforCategory] input:checked");
            var TechIds = "";
            var ToDate = "";
            var FromDate = "";
            checked_checkboxes.each(function () {
                var value = $(this).val();
                TechIds += +value;
                TechIds += ",";
            });
            FromDate = $("#txtFromdate").val();
            ToDate = $("#txttodate").val();

            var CustomerInCat = $("#ddlCustomerCat1").val();
            var RequestTypeInCat = $("#ddlRequestTypeCat").val();



            $("#Label1").empty();
            $("#Label2").empty();
            $("#Label3").empty();
            $("#Label1").append("Volume of Open Calls By Category from " + FromDate + " To " + ToDate);
            $("#Label2").append("Volume of Completed Calls By Category from " + FromDate + " To " + ToDate);
            $("#Label3").append("Calls by Category from " + FromDate + " To " + ToDate);
            
            GetChartData1InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat);
            GetChartData2InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat);
            GetChartData3InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat);
            
        }
        return false;
    });
    $("#BtnSearchInsite").click(function () {
        if (Page_IsValid) {
            var checked_checkboxes = $("[id*=checklistforCategory] input:checked");
            var TechIds = "";
            var ToDate = "";
            var FromDate = "";
            checked_checkboxes.each(function () {
                var value = $(this).val();
                TechIds += +value;
                TechIds += ",";
            });
            FromDate = $("#txtFromdate").val();
            ToDate = $("#txttodate").val();
            var Cname = $("#ddlCustomer").val();

            $("#Label1").empty();
            $("#Label2").empty();
            $("#Label3").empty();
            $("#Label1").append("Volume of Open Calls By Site from " + FromDate + " To " + ToDate);
            $("#Label2").append("Volume of Completed Calls By Site from " + FromDate + " To " + ToDate);
            $("#Label3").append("Calls by Site from " + FromDate + " To " + ToDate);
            
            if (Cname != 0) {
                debugger;
                GetDataInBtnClickSite1(Cname, FromDate, ToDate);
                debugger;
                GetDataInBtnClickSite2(Cname, FromDate, ToDate);
                GetDataInBtnClickSite3(Cname, FromDate, ToDate);
                return false
            }
        }
        return false;

    });
    $("#BtnSearchBilling").click(function () {
        if (Page_IsValid) {
            var ToDate = $("#txttodate").val();
            var FromDate = $("#txtFromdate").val();
            var Cname = $("#ddlCustomerInBilling").val();
            if (Cname != undefined) {
                GetDataOfRevenueBySiteInBtnClick(FromDate, ToDate, Cname);
                GetDataOfRevenueByCategoryInBtnClick(FromDate, ToDate, Cname);
            }
        }
        return false;
    });

    //$("#ddlCustomer").change(function () {
    //    var Cname = $("#ddlCustomer").val();
    //    GetBarchartDataofSite1(Cname);
    //    GetBarchartDataofSite2(Cname);
    //    GetBarchartDataofSite3(Cname);
    //});
    $("#ddlCustomerInBilling").change(function () {
        debugger;
        var Cname = $("#ddlCustomerInBilling").val();
        // if (Cname != 0) {
        GetDataOfRevenueBySite(Cname);
        //}
    });
    $("#ddlCustomerInEngineer").change(function () {
        debugger;
        var Cname = $("#ddlCustomerInEngineer").val();
        //if (Cname != 0) {
        GetChartData4(Cname);
        //}
    })
    $("#ddlCustomerChart1").change(function () {
        Customer_charts();
    });
    $("#ddlRequestType").change(function () {
        Customer_charts();
    });
    $("#ddlCustomerCat1").change(function () {
        $("#checklistforCategory").remove();
    });
});

function Customer_charts() {
    var ToDate = "";
    var FromDate = "";
    //debugger;
    FromDate = $("#txtFromdate").val();
    ToDate = $("#txttodate").val();
    var Cname = $("#ddlCustomerChart1").val();
    var Rtype = $("#ddlRequestType").val();
    //debugger;
    if (Cname != undefined) {
        //debugger;
        GetDataCustomerType(Cname, Rtype, FromDate, ToDate);
        GetDataCustomerPiechart(Cname, Rtype, FromDate, ToDate);
        GetDataCustomerCategory(Cname, Rtype, FromDate, ToDate);
        GetDataCustomerEngineer(Cname, Rtype, FromDate, ToDate);
        //debugger;
        return false;
    }

}


function GetChartData1() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData1',
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
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-1").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Engineer",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetChartData2() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData2',
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
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-2").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Engineer",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });

}
function GetChartData3() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData3',
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
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-3").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                });
            }
        }
    });

}
function GetChartData4(Cname) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData4?Cname' + Cname,
        type: "POST",
        data: "{'Cname': '" + Cname + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                var firstCollection = newData.a;
                var seriesCollection = newData.b;
                $("#bar-4").dxChart({
                    equalBarWidth: false,
                    dataSource:firstCollection,
                    commonSeriesSettings: {
                        argumentField: "state",
                        type: "bar"
                    },
                    series:seriesCollection,
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center"
                    },
                });
            }
        }
    });

}

function GetBarchartDataofSite1(Cname)
{
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofSite1?Cname=' + Cname,
        type: "POST",
        data: "{'Cname': '" + Cname + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-site-1").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Site",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetBarchartDataofSite2(Cname) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofSite2?Cname=' + Cname,
        type: "POST",
        data: "{'Cname': '" + Cname + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-site-2").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Site",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetBarchartDataofSite3(Cname) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofSite3?Cname=' + Cname,
        type: "POST",
        data: "{'Cname': '" + Cname + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-site-3").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                });
            }
        }
    });

}

function GetBarchartDataofCategory1() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofCategory1',
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
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-Category-1").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Site",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });

}
function GetBarchartDataofCategory2() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofCategory2',
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
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-Category-2").dxChart({
                    rotated: false,
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Site",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });

}
function GetBarchartDataofCategory3() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofCategory3',
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
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-Category-3").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                });
            }
        }
    });

}
function GetBarchartDataofCategory4() {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetBarchartDataofCategory4',
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
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-Category-2").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                });
            }
        }
    });
}
function GetDataOfRevenueByCategory()
{
    debugger;
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataOfRevenueByCategory',
        type: 'POST',
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
                $("#Billing-1").dxPieChart({
                    dataSource: ds,
                    tooltip: {
                        enabled: false,
                        //format: "millions",
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    pointClick: function (point) {
                        point.showTooltip();
                        clearTimeout(timer);
                        timer = setTimeout(function () { point.hideTooltip(); }, 2000);
                        $("select option:contains(" + point.argument + ")").prop("selected", true);
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "region"
                    }],
                    legend: {
                        visible: true
                    },
                    palette: ['#68b828','#7c38bc','#0e62c7','#fcd036','#4fcdfc','#00b19d','#ff6264','#f7aa47']
                });
            }
        }
    });
}

function GetDataOfRevenueByCategoryInBtnClick(FromDate, ToDate, CnameInBilling) {
    debugger;
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataOfRevenueByCategoryInBtnClick?FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: 'POST',
        data: "{'Cname': '" + CnameInBilling + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
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
                $("#Billing-1").dxPieChart({
                    dataSource: ds,
                    tooltip: {
                        enabled: false,
                        //format: "millions",
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    pointClick: function (point) {
                        point.showTooltip();
                        clearTimeout(timer);
                        timer = setTimeout(function () { point.hideTooltip(); }, 2000);
                        $("select option:contains(" + point.argument + ")").prop("selected", true);
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "region"
                    }],
                    legend: {
                        visible: true
                    },
                    palette: ['#68b828', '#7c38bc', '#0e62c7', '#fcd036', '#4fcdfc', '#00b19d', '#ff6264', '#f7aa47']
                });
            }
        }
    });
}
function GetDataOfRevenueBySite(Cname)
{
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataOfRevenueBySite?Cname='+Cname,
        type: 'POST',
        data: "{'Cname': '" + Cname + "'}",
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
                $("#Billing-2").dxPieChart({
                    dataSource: ds,
                    tooltip: {
                        enabled: false,
                       // format: "millions",
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "region"
                    }],
                    pointClick: function (point) {
                        point.showTooltip();
                        clearTimeout(timer);
                        timer = setTimeout(function () { point.hideTooltip(); }, 2000);
                        $("select option:contains(" + point.argument + ")").prop("selected", true);
                    },
                    legend: {
                        visible: true
                    },
                    palette: ['#68b828', '#7c38bc', '#0e62c7', '#fcd036', '#4fcdfc', '#00b19d', '#ff6264', '#f7aa47']
                });
            }
        }
    });
}
function GetDataOfRevenueBySiteInBtnClick(FromDate, ToDate, Cname) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataOfRevenueBySiteInBtnClick?Cname=' + Cname +'&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: 'POST',
        data: "{'Cname': '" + Cname + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
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
                $("#Billing-2").dxPieChart({
                    dataSource: ds,
                    tooltip: {
                        enabled: false,
                        // format: "millions",
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "region"
                    }],
                    pointClick: function (point) {
                        point.showTooltip();
                        clearTimeout(timer);
                        timer = setTimeout(function () { point.hideTooltip(); }, 2000);
                        $("select option:contains(" + point.argument + ")").prop("selected", true);
                    },
                    legend: {
                        visible: true
                    },
                    palette: ['#68b828', '#7c38bc', '#0e62c7', '#fcd036', '#4fcdfc', '#00b19d', '#ff6264', '#f7aa47']
                });
            }
        }
    });
}



function GetChartData1InBtnClick(TechIds, FromDate, ToDate)
{
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData1InBtnClick?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-1").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Sales",
                        type: "bar",
                        color: '#68b828'
                    }
                });
            }
        }
    });
}
function GetChartData2InBtnClick(TechIds, FromDate, ToDate)
{
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData2InBtnClick?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-2").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Sales",
                        type: "bar",
                        color: '#68b828'
                    }
                });
            }
        }
    });
}
function GetChartData3InBtnClick(TechIds, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData3InBtnClick?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-3").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetChartData4InBtnClick(TechIds, FromDate, ToDate, Cname) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData4InBtnClick?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate + '&&Cname' + Cname,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "','Cname':'" + Cname + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                var firstCollection = newData.a;
                var seriesCollection = newData.b;
                $("#bar-4").dxChart({
                    equalBarWidth: false,
                    dataSource: firstCollection,
                    commonSeriesSettings: {
                        argumentField: "state",
                        type: "bar"
                    },
                    series: seriesCollection,
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center"
                    },
                });
            }
        }
    });
}



function GetChartData1InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData1InBtnClickCategory?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate + '&&CustomerInCat=' + CustomerInCat + '&&RequestTypeInCat' + RequestTypeInCat,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "','CustomerInCat': '" + CustomerInCat + "','RequestTypeInCat': '" + RequestTypeInCat + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-Category-1").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Sales",
                        type: "bar",
                        color: '#68b828'
                    }
                });
            }
        }
    });
}
function GetChartData2InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData1InBtnClickCategory?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate + '&&CustomerInCat=' + CustomerInCat + '&&RequestTypeInCat' + RequestTypeInCat,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "','CustomerInCat': '" + CustomerInCat + "','RequestTypeInCat': '" + RequestTypeInCat + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-Category-2").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Sales",
                        type: "bar",
                        color: '#68b828'
                    }
                });
            }
        }
    });
}
function GetChartData3InBtnClickCategory(TechIds, FromDate, ToDate, CustomerInCat, RequestTypeInCat) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartData1InBtnClickCategory?TechIds=' + TechIds + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate + '&&CustomerInCat=' + CustomerInCat + '&&RequestTypeInCat' + RequestTypeInCat,
        type: "POST",
        data: "{'TechIds': '" + TechIds + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "','CustomerInCat': '" + CustomerInCat + "','RequestTypeInCat': '" + RequestTypeInCat + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-Category-3").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}



function GetDataInBtnClickSite1(Cname, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataInBtnClickSite1?Cname=' + Cname + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-site-1").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Site",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetDataInBtnClickSite2(Cname, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataInBtnClickSite2?Cname=' + Cname + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-site-2").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Site",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetDataInBtnClickSite3(Cname, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataInBtnClickSite3?Cname=' + Cname + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Pending: newData[i].Pending,
                        InHand: newData[i].InHand,
                        Completed: newData[i].Completed,
                        Resolved: newData[i].Resolved
                    });
                }
                $("#bar-site-3").dxChart({
                    rotated: false,
                    dataSource: ds,
                    commonSeriesSettings: {
                        argumentField: "Name",
                        type: "stackedbar",
                        selectionStyle: {
                            hatching: {
                                direction: "left"
                            }
                        }
                    },
                    series: [
                             { valueField: "Pending", name: "Pending", color: "#ffd700" },
                             { valueField: "InHand", name: "In Hand", color: "#c0c0c0" },
                             { valueField: "Completed", name: "Completed", color: "#cd7f32" },
                             { valueField: "Resolved", name: "Resolved", color: "#68b828" }
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                    },
                });
            }
        }
    });
}



function GetDataCustomerCategory(Cname, Rtype, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartCustomerCategoryBtnClick?Cname=' + Cname + '&&Rtype' + Rtype + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','Rtype': '" + Rtype + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-customer-category-3").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Category",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}
function GetDataCustomerEngineer(Cname, Rtype, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetChartCustomerEngineerBtnClick?Cname=' + Cname + '&&Rtype' + Rtype + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','Rtype': '" + Rtype + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                var newData = jQuery.parseJSON(data.d);
                var ds = [];
                for (var i = 0; i < newData.length; i++) {
                    ds.push({
                        Name: newData[i].Name,
                        Value: newData[i].Value
                    });
                }
                $("#bar-customer-engineer-3").dxChart({
                    dataSource: ds,
                    series: {
                        argumentField: "Name",
                        valueField: "Value",
                        name: "Engineer",
                        type: "bar",
                        color: '#68b828'
                    },
                    tooltip: {
                        enabled: true
                    }
                });
            }
        }
    });
}

function GetDataCustomerPiechart(Cname, Rtype, FromDate, ToDate) {
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataCustomerPiechartBtnClick?Cname=' + Cname + '&&Rtype' + Rtype + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','Rtype': '" + Rtype + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
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
                $("#bar-customer-pie-1").dxPieChart({
                    dataSource: ds,
                    tooltip: {
                        enabled: false,
                        //format: "millions",
                        customizeText: function () {
                            return this.argumentText + "<br/>" + this.valueText;
                        }
                    },
                    pointClick: function (point) {
                        point.showTooltip();
                        clearTimeout(timer);
                        timer = setTimeout(function () { point.hideTooltip(); }, 2000);
                        $("select option:contains(" + point.argument + ")").prop("selected", true);
                    },
                    series: [{
                        type: "doughnut",
                        argumentField: "region"
                    }],
                    legend: {
                        visible: true
                    },
                    tooltip: {
                        enabled: true
                    },
                    palette: ['#68b828', '#7c38bc', '#0e62c7', '#fcd036', '#4fcdfc', '#00b19d', '#ff6264', '#f7aa47']
                });
            }
        }
    });
}
function GetDataCustomerType(Cname, Rtype, FromDate, ToDate) {
    $("#divsum").html('');
    $.ajax({
        url: '../DC/webservices/DCServices.asmx/GetDataCustomerTypeBtnClick?Cname=' + Cname + '&&Rtype' + Rtype + '&&FromDate=' + FromDate + '&&ToDate=' + ToDate,
        type: "POST",
        data: "{'Cname': '" + Cname + "','Rtype': '" + Rtype + "','FromDate': '" + FromDate + "','ToDate': '" + ToDate + "'}",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            if (data.d != '') {
                //debugger;
                var newData = jQuery.parseJSON(data.d);
                var s = newData;
                $("#divsum").append("<table style='width:50%'>");
                $.each(newData, function (index) {
                    $("#divsum").append("<tr><td>Total Number of " + newData[index].Name + " Reported:</td><td><strong>" + newData[index].Value + "</strong></td></tr>");
                    //debugger;
                    //alert(newData[index].Name);
                    //alert(newData[index].Value);
                });
                $("#divsum").append('</table>');

            }
        }
    });
}