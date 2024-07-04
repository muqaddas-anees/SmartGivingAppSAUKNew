<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="TaithingDashboardV2.aspx.cs" Inherits="DeffinityAppDev.App.TaithingDashboardV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <asp:Label ID="lblPagetitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
   
    <style>
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        /* .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }*/
    </style>

    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Transaction  Details</h3>
            </div>
            <div class="card-toolbar" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">

                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Add New Category" OnClick="btnAddOrganization_Click"       />--%>
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->













    </div>



    <!--begin::Content-->
    <div class="content d-flex flex-column flex-column-fluid" id="kt_content">




        <!--begin::Container-->
        <div class="container-xxl" id="kt_content_container">
            <!--begin::Row-->
            <div class="row g-5 g-xl-10 mb-xl-10">
                <div class="col-md-6 col-lg-6 col-xl-6 col-xxl-3 mb-md-5 mb-xl-10">
                            <!--begin::Card widget 4-->
                            <div class="card card-flush h-md-50 mb-5 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                           Total Transaction amount
                                                
                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 1</span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body pt-2 pb-4 d-flex align-items-center">
                                    <!--begin::Chart-->
                                    <div class="d-flex flex-center me-5 pt-2">
                                        <div id="kt_card_widget_4_chart" style="min-width: 70px; min-height: 70px" data-kt-size="70" data-kt-line="11"></div>
                                    </div>
                                    <!--end::Chart-->
                                    
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 4-->
                            <!--begin::Card widget 5-->
                            <div class="card card-flush h-md-50 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                           

                                            Transactions


                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 5 </span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body d-flex align-items-end pt-0">
                                   
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 5-->
                        </div>


                        <div class="col-md-6 col-lg-6 col-xl-6 col-xxl-3 mb-md-5 mb-xl-10">
                            <!--begin::Card widget 4-->
                            <div class="card card-flush h-md-50 mb-5 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                            <!--begin::Currency-->

                                            Largest Transaction

                                           
                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 2</span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body pt-2 pb-4 d-flex align-items-center">
                                    <!--begin::Chart-->
                                    <div class="d-flex flex-center me-5 pt-2">
                                        <div id="kt_card_widget_4_chart" style="min-width: 70px; min-height: 70px" data-kt-size="70" data-kt-line="11"></div>
                                    </div>
                                    <!--end::Chart-->
                                    <!--begin::Labels-->
                                    <div class="d-flex flex-column content-justify-center w-100">
                                       
                                        <!--begin::Label-->
                                        <div class="d-flex fs-6 fw-bold align-items-center">
                                            <!--begin::Bullet-->
                                            <div class="bullet w-8px h-6px rounded-2 me-3" style="background-color: #E4E6EF"></div>
                                            <!--end::Bullet-->
                                            <!--begin::Label-->
                                            <div class="text-gray-500 flex-grow-1 me-4">Others</div>
                                            <!--end::Label-->
                                            <!--begin::Stats-->
                                            <div class="fw-boldest text-gray-700 text-xxl-end">$45,257</div>
                                            <!--end::Stats-->
                                        </div>
                                        <!--end::Label-->
                                    </div>
                                    <!--end::Labels-->
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 4-->
                            <!--begin::Card widget 5-->
                            <div class="card card-flush h-md-50 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                           
                                            Total Doners 
                                            <!--end::Badge-->
                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 6</span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body d-flex align-items-end pt-0">
                                    <!--begin::Progress-->
                                    <div class="d-flex align-items-center flex-column mt-3 w-100">
                                        <div class="d-flex justify-content-between w-100 mt-auto mb-2">
                                            <span class="fw-boldest fs-6 text-dark">1,048 to Goal</span>
                                            <span class="fw-bolder fs-6 text-gray-400">62%</span>
                                        </div>
                                        <div class="h-8px mx-3 w-100 bg-light-success rounded">
                                            <div class="bg-success rounded h-8px" role="progressbar" style="width: 62%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </div>
                                    <!--end::Progress-->
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 5-->
                        </div>











                        <div class="col-md-6 col-lg-6 col-xl-6 col-xxl-3 mb-md-5 mb-xl-10">
                            <!--begin::Card widget 4-->
                            <div class="card card-flush h-md-50 mb-5 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                           Average Transaction
                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 3</span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body pt-2 pb-4 d-flex align-items-center">
                                    <!--begin::Chart-->
                                    <div class="d-flex flex-center me-5 pt-2">
                                        <div id="kt_card_widget_4_chart" style="min-width: 70px; min-height: 70px" data-kt-size="70" data-kt-line="11"></div>
                                    </div>
                                    <!--end::Chart-->
                                    <!--begin::Labels-->
                                    <div class="d-flex flex-column content-justify-center w-100">
                                        
                                        <!--begin::Label-->
                                        <div class="d-flex fs-6 fw-bold align-items-center">
                                            <!--begin::Bullet-->
                                            <div class="bullet w-8px h-6px rounded-2 me-3" style="background-color: #E4E6EF"></div>
                                            <!--end::Bullet-->
                                            <!--begin::Label-->
                                            <div class="text-gray-500 flex-grow-1 me-4">Others</div>
                                            <!--end::Label-->
                                            <!--begin::Stats-->
                                            <div class="fw-boldest text-gray-700 text-xxl-end">$45,257</div>
                                            <!--end::Stats-->
                                        </div>
                                        <!--end::Label-->
                                    </div>
                                    <!--end::Labels-->
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 4-->
                            <!--begin::Card widget 5-->
                            <div class="card card-flush h-md-50 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                           New Doners 
                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 7 </span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body d-flex align-items-end pt-0">
                                    <!--begin::Progress-->
                                    <div class="d-flex align-items-center flex-column mt-3 w-100">
                                        
                                        
                                    </div>
                                    <!--end::Progress-->
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 5-->
                        </div>







                        <div class="col-md-6 col-lg-6 col-xl-6 col-xxl-3 mb-md-5 mb-xl-10">
                            <!--begin::Card widget 6-->
                            <div class="card card-flush h-md-50 mb-5 mb-xl-10">
                                <!--begin::Header-->
                                <div class="card-header pt-5">
                                    <!--begin::Title-->
                                    <div class="card-title d-flex flex-column">
                                        <!--begin::Info-->
                                        <div class="d-flex align-items-center">
                                            Smallest Transaction
                                        </div>
                                        <!--end::Info-->
                                        <!--begin::Subtitle-->
                                        <span class="text-gray-400 pt-1 fw-bold fs-6">Box 4    </span>
                                        <!--end::Subtitle-->
                                    </div>
                                    <!--end::Title-->
                                </div>
                                <!--end::Header-->
                                <!--begin::Card body-->
                                <div class="card-body d-flex align-items-end px-0 pb-0">
                                    <!--begin::Chart-->
                                    <div id="kt_card_widget_6_chart" class="w-100" style="height: 80px"></div>
                                    <!--end::Chart-->
                                </div>
                                <!--end::Card body-->
                            </div>
                            <!--end::Card widget 6-->
                            <!--begin::Card widget 7-->
                            <div class="card card-flush h-md-50 mb-xl-10">
                                bar graph    Box 8


										<%--<div id="piechart"   style="width:50px"     ></div>--%>

                                <%--<div id="piechart"   style="height: 30%; width: 40%;"     ></div>--%>
                            </div>
                            <!--end::Card widget 7-->
                        </div>





                  <!--begin::Col-->
                <div class="col-lg-12 col-xl-12 col-xxl-6 mb-5 mb-xl-0">
                    <!--begin::Chart widget 3-->
                    <div class="card card-flush overflow-hidden h-md-100">
                        <!--begin::Header-->
                        <div class="card-header py-5">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bolder text-dark">One-Time vs. Recurring transaction 1</span>
                                <span class="text-gray-400 mt-1 fw-bold fs-6">Users from all channels</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                           
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Card body-->
                        <div class="card-body d-flex justify-content-between flex-column pb-1 px-0">



                            <div class="px-9 mb-5">

                                <div id="piechart" style="height: 300px; width: 100%;"></div>

                            </div>










                        </div>
                        <!--end::Card body-->
                    </div>
                    <!--end::Chart widget 3-->
                </div>
                <!--end::Col-->










                
                <div class="col-lg-12 col-xl-12 col-xxl-6 mb-5 mb-xl-0">
                    <!--begin::Chart widget 3-->
                    <div class="card card-flush overflow-hidden h-md-100">
                        <!--begin::Header-->
                        <div class="card-header py-5">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bolder text-dark">Cumulative Transaction Totals</span>
                                <span class="text-gray-400 mt-1 fw-bold fs-6">Users from all channels</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Card body-->
                        <div class="card-body d-flex justify-content-between flex-column pb-1 px-0">
                            <!--begin::Statistics-->
                            <div class="px-9 mb-5">

                                <div id="chartContainer1" style="height: 50%; width: 100%;"></div>

                            </div>
                            <!--end::Statistics-->
                            <!--begin::Chart-->
                            <div id="kt_charts_widget_3" class="min-h-auto ps-4 pe-6" style="height: 300px"></div>
                            <!--end::Chart-->
                        </div>
                        <!--end::Card body-->
                    </div>
                    <!--end::Chart widget 3-->
                </div>







                <div class="col-lg-12 col-xl-12 col-xxl-6 mb-5 mb-xl-0">
                    <!--begin::Chart widget 3-->
                    <div class="card card-flush overflow-hidden h-md-100">
                        <!--begin::Header-->
                        <div class="card-header py-5">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bolder text-dark">Daily Transaction Totals </span>
                                <span class="text-gray-400 mt-1 fw-bold fs-6">Users from all channels</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <a href="/metronic8/demo4/../demo4/apps/ecommerce/catalog/products.html" class="btn btn-sm btn-light-primary fw-bolder">Launch eCommerce App</a>
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Card body-->
                        <div class="card-body d-flex justify-content-between flex-column pb-1 px-0">
                            Pai chart

											<div id="kt_docs_google_chart_column"></div>

                            <div id="chartContainer" style="height: 300px; width: 100%;"></div>






                        </div>
                        <!--end::Card body-->
                    </div>
                    <!--end::Chart widget 3-->
                </div>
                <!--end::Col-->










              







            </div>

        </div>
        <!--end::Container-->







    </div>
    <!--end::Content-->





























    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        // Load google charts
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        // Draw the chart and set the chart values
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['Task', 'Hours per Day'],
                ['Work', 8],
                ['Eat', 2],
                ['TV', 4],
                ['Gym', 2],
                ['Sleep', 8]
            ]);

            // Optional; add a title and set the width and height of the chart
            var options = { 'title': 'My Average Day', 'width': 550, 'height': 400 };

            // Display the chart inside the <div> element with id="piechart"
            var chart = new google.visualization.PieChart(document.getElementById('piechart'));
            chart.draw(data, options);
        }
    </script>




    <script type="text/javascript">
        window.onload = function () {

            var dps = [{ x: 1, y: 10 }, { x: 2, y: 13 }, { x: 3, y: 18 }, { x: 4, y: 20 }, { x: 5, y: 17 }, { x: 6, y: 10 }, { x: 7, y: 13 }, { x: 8, y: 18 }, { x: 9, y: 20 }, { x: 10, y: 17 }];   //dataPoints. 

            var chart = new CanvasJS.Chart("chartContainer", {
                title: {
                    text: "Live Data"
                },
                axisX: {
                    title: "Axis X Title"
                },
                axisY: {
                    title: "Units"
                },
                data: [{
                    type: "line",
                    dataPoints: dps
                }]
            });

            chart.render();
            var xVal = dps.length + 1;
            var yVal = 15;
            var updateInterval = 1000;

            var updateChart = function () {


                yVal = yVal + Math.round(5 + Math.random() * (-5 - 5));
                dps.push({ x: xVal, y: yVal });

                xVal++;
                if (dps.length > 10) {
                    dps.shift();
                }

                chart.render();

                // update chart after specified time. 

            };

            setInterval(function () { updateChart() }, updateInterval);
        }
    </script>






    <script type="text/javascript">
        window.onload = function () {

            var dps = [{ x: 1, y: 10 }, { x: 2, y: 13 }, { x: 3, y: 18 }, { x: 4, y: 20 }, { x: 5, y: 17 }, { x: 6, y: 10 }, { x: 7, y: 13 }, { x: 8, y: 18 }, { x: 9, y: 20 }, { x: 10, y: 17 }];   //dataPoints. 

            var chart = new CanvasJS.Chart("chartContainer1", {
                title: {
                    text: "Live Data"
                },
                axisX: {
                    title: "Axis X Title"
                },
                axisY: {
                    title: "Units"
                },
                data: [{
                    type: "line",
                    dataPoints: dps
                }]
            });

            chart.render();
            var xVal = dps.length + 1;
            var yVal = 15;
            var updateInterval = 1000;

            var updateChart = function () {


                yVal = yVal + Math.round(5 + Math.random() * (-5 - 5));
                dps.push({ x: xVal, y: yVal });

                xVal++;
                if (dps.length > 10) {
                    dps.shift();
                }

                chart.render();

                // update chart after specified time. 

            };

            setInterval(function () { updateChart() }, updateInterval);
        }
    </script>









    <script type="text/javascript" src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <%--<script src="jquery-1.7.1.js"></script>--%>



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
