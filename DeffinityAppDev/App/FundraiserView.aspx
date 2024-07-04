<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="FundraiserView.aspx.cs" Inherits="DeffinityAppDev.App.FundraiserView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Fundraiser
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <asp:Label ID="lblTitle" Text=""  runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-light" OnClick="btnBack_Click" Text="Back to list" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
<%--  <link href="assets/main.css?v=5" type="text/css" rel="stylesheet">--%>
 

 
    <style>
        .header_right{
            text-align:right;
        }
    </style>
       <div class="row p-5">
            <div class="col-lg-8">
            <div class="row  mt-15">
                                                  <asp:Image ID="imgcenterimage" runat="server" CssClass="img-fluid" />
                </div>
               </div>
            <div class="col-lg-4">
                  <div class="row">
                 <asp:Image ID="imgQR" runat="server" CssClass="img-fluid" />
                      </div>
                </div>
                                                 </div>
<div class="row">

                                        <div class="col-lg-6">
                                          

                                            <div class="row">
                                                <br /><br /><br />
                                               <asp:HiddenField ID="hmoney" runat="server" />
                                            </div>

                                              <div class="row">
                                                   <asp:Label ID="lblDescription" Text=""  runat="server"  Font-Size="30px"/><br /><br />
                                                  </div>

                                             <div class="row">
                                                  <div class="col-lg-10">
                                             
                                                      </div>
                                                    </div>
                                             <div class="row">
                                                  <div class="col-lg-6">
                                              <%--   <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" SkinID="btnDefault" Text="View Result" Visible="false" />--%>
                                                      </div>
                                                 </div>
                                          


                                        </div>

                                          <div class="col-lg-6">
                                               <div class="row">
                                                  <div class="col-lg-12  d-flex flex-wrap flex-center mt-20">
                                                  <h1 class="anchor fw-bold mb-5" id="target1" data-kt-scroll-offset="50">
										Target amount</h1>    
                                                     
                                                      </div>

             </div>

                                               <div class="row mx-10">
                                                  <div class="col-lg-12">
                                                      
                                                    <div class="d-flex flex-wrap flex-center">
												<!--begin::Chart-->
												<div class="position-relative d-flex flex-center h-400px w-400px me-15 mb-7">
													<div class="position-absolute translate-middle start-50 top-75 d-flex flex-column flex-center" >
														<span class="fs-2qx fw-bolder"><asp:Label ID="lblTarget" runat="server" Text="Target"></asp:Label>  </span>
														<span class="fs-6 fw-bold text-gray-400">Target amount</span>
													</div>
													<canvas id="project_overview_chart"></canvas>
												</div>
                                                      <div id="preview-textfield"></div>
                                                      </div>
                                                   </div>
                                               <%-- <div class="row">
                                                  <div class="col-lg-12">
                                                         <asp:Label ID="lblRaised" runat="server" Text="Raised"></asp:Label>  <asp:Label ID="lblRaisedAmount" runat="server" Text="0.00"></asp:Label> 

                                                      </div>
                                                   </div>--%>
                                        </div>
                                              
                                    </div>

    <div class="row">
           <div class="row mt-20">
                                                  <div class="col-lg-12">
                                                  <h1 class="anchor fw-bold mb-5" id="t1" data-kt-scroll-offset="50">
										Top donors</h1>    
                                                     
                                                      </div>

             </div>


        <asp:GridView ID="gridtopdonors" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Cell number">
                    <ItemTemplate>
                        <asp:Label ID="lblContact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="header_right" ItemStyle-CssClass="header_right" >
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:C2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
        
        </div>

<%--    <script src="dist/gauge.js"></script>

    <script type="text/javascript">
        var opts = {
            // color configs
            colorStart: "#6fadcf",
            colorStop: void 0,
            gradientType: 0,
            strokeColor: "#e0e0e0",
            generateGradient: true,
            percentColors: [[0.0, "#a9d70b"], [0.50, "#f9c802"], [1.0, "#ff0000"]],
            // customize pointer
            pointer: {
                length: 0.8,
                strokeWidth: 0.035,
                iconScale: 1.0
            },
            // static labels
            staticLabels: {
                font: "10px sans-serif",
                labels: [200, 500, 2100, 2800],
                fractionDigits: 0
            },
            // static zones
            staticZones: [
                { strokeStyle: "#F03E3E", min: 0, max: 200 },
                { strokeStyle: "#FFDD00", min: 200, max: 500 },
                { strokeStyle: "#30B32D", min: 500, max: 2100 },
                { strokeStyle: "#FFDD00", min: 2100, max: 2800 },
                { strokeStyle: "#F03E3E", min: 2800, max: 3000 }
            ],
            // render ticks
            renderTicks: {
                divisions: 5,
                divWidth: 1.1,
                divLength: 0.7,
                divColor: "#333333",
                subDivisions: 3,
                subLength: 0.5,
                subWidth: 0.6,
                subColor: "#666666"
    },
    // the span of the gauge arc
    angle: 0.15,
            // line thickness
            lineWidth: 0.44,
            // radius scale
            radiusScale: 1.0,
            // font size
            fontSize: 40,
            // if false, max value increases automatically if value > maxValue
            limitMax: false,
            // if true, the min value of the gauge will be fixed
            limitMin: false,
            // High resolution support
            highDpiSupport: true
        };
        var target = document.getElementById('demo');
        var gauge = new Gauge(target).setOptions(opts);

        document.getElementById("preview-textfield").className = "preview-textfield";
        gauge.setTextField(document.getElementById("preview-textfield"));

        gauge.maxValue = 3000;
        gauge.setMinValue(0);
        gauge.set(1250);
        gauge.animationSpeed = 32
    </script>--%>

    <asp:HiddenField ID="hraised" runat="server" Value="30.00" ClientIDMode="Static" />
    
    <asp:HiddenField ID="hremaing" runat="server" Value="40.00" ClientIDMode="Static" />
    <script>
        dchart();

        function dchart () {
            // init chart
            var element = document.getElementById("project_overview_chart");

            if (!element) {
                return;
            }
            const darray = [];
            darray[0] = $("#hraised").val();
            darray[1] = $("#hremaing").val();
            var config = {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: darray,
                        backgroundColor: ['#00A3FF', '#E4E6EF']
                    }],
                    labels: ['Raised amount', 'Remaining amount']
                },
                options: {
                    chart: {
                        fontFamily: 'inherit'
                    },
                    rotation: -90,
                    circumference: 180,
                    cutoutPercentage: 75,
                    responsive: true,
                    maintainAspectRatio: false,
                    cutout: '75%',
                    title: {
                        display: false
                    },
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    },
                    tooltips: {
                        enabled: true,
                        intersect: false,
                        mode: 'nearest',
                        bodySpacing: 5,
                        yPadding: 10,
                        xPadding: 10,
                        caretPadding: 0,
                        displayColors: false,
                        backgroundColor: '#20D489',
                        titleFontColor: '#ffffff',
                        cornerRadius: 4,
                        footerSpacing: 0,
                        titleSpacing: 0
                    },
                    plugins: {
                        legend: {
                            display: true,
                            position: 'bottom',
                            textStyle: {  fontSize: 34 },
                            labels: {
                                generateLabels: (chart) => {
                                    const datasets = chart.data.datasets;
                                    return datasets[0].data.map((data, i) => ({
                                        text: `${chart.data.labels[i]} ${data}`,
                                        fillStyle: datasets[0].backgroundColor[i],
                                    }))
                                }
                            }
                        }
                    }
                }
            };

            var ctx = element.getContext('2d');
            var myDoughnut = new Chart(ctx, config);
        }
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
