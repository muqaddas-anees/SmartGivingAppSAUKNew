<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCDonation.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCDonation" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagPrefix="Pref" TagName="FLSTab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
      <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:FLSTab runat="server" ID="FLSTab" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
   <style>
       .header_right{
           text-align:right;
           
       }

   </style>
    <link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css?id=100")%>' rel="stylesheet" type="text/css" />
    <script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
    <div class="row mb-6">
        

    </div>
    
<div class="form-group row mb-6">
          
              <div class="col-md-12 d-flex d-inline">

                  <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                   <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                  </div>
    </div>
    


   
    
    <asp:HiddenField ID="htimeid" runat="server" />
    

    <div class="row mb-6">
        <div class="col-md-4">

            </div>

        <div class="col-md-4">

            <div class="card card-flush h-md-50 mb-5 mb-xl-10">
										<!--begin::Header-->
										<div class="card-header pt-5">
											<!--begin::Title-->
											<div class="card-title d-flex flex-column">
												<!--begin::Info-->
												<div class="d-flex align-items-center">
													<!--begin::Currency-->
													<span class="fs-4 fw-semibold text-gray-400 me-1 align-self-start"></span>
													<!--end::Currency-->
													<!--begin::Amount-->
													<span class="fs-2hx fw-bold text-dark me-2 lh-1 ls-n2"><asp:Literal ID="lblTarget" runat="server"></asp:Literal>  </span>
													<!--end::Amount-->
													<!--begin::Badge-->
													<%--<span class="badge badge-light-success fs-base">--%>
													<!--begin::Svg Icon | path: icons/duotune/arrows/arr066.svg-->
													<%--<span class="svg-icon svg-icon-5 svg-icon-success ms-n1">
														<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
															<rect opacity="0.5" x="13" y="6" width="13" height="2" rx="1" transform="rotate(90 13 6)" fill="currentColor" />
															<path d="M12.5657 8.56569L16.75 12.75C17.1642 13.1642 17.8358 13.1642 18.25 12.75C18.6642 12.3358 18.6642 11.6642 18.25 11.25L12.7071 5.70711C12.3166 5.31658 11.6834 5.31658 11.2929 5.70711L5.75 11.25C5.33579 11.6642 5.33579 12.3358 5.75 12.75C6.16421 13.1642 6.83579 13.1642 7.25 12.75L11.4343 8.56569C11.7467 8.25327 12.2533 8.25327 12.5657 8.56569Z" fill="currentColor" />
														</svg>
													</span>
													<!--end::Svg Icon-->2.2%</span>--%>
													<!--end::Badge-->
												</div>
												<!--end::Info-->
												<!--begin::Subtitle-->
												<span class="text-gray-400 pt-1 fw-semibold fs-6">Target Amount</span>
												<!--end::Subtitle-->
											</div>
											<!--end::Title-->
										</div>
										<!--end::Header-->
										<!--begin::Card body-->
										<div class="card-body pt-2 pb-4 d-flex flex-wrap align-items-center">
											<!--begin::Chart-->
											<div class="d-flex flex-center me-5 pt-2">
												<%--<div id="kt_card_widget_17_chart" style="min-width: 70px; min-height: 70px" data-kt-size="70" data-kt-line="11"></div>--%>
                                                 <canvas id="project_overview_chart" style="height:70px"></canvas>
											</div>
											<!--end::Chart-->
											<!--begin::Labels-->
											<div class="d-flex flex-column content-justify-center flex-row-fluid">
												<!--begin::Label-->
												<div class="d-flex fw-semibold align-items-center">
													<!--begin::Bullet-->
													<div class="bullet w-8px h-3px rounded-2 bg-success me-3"></div>
													<!--end::Bullet-->
													<!--begin::Label-->
													<div class="text-gray-500 flex-grow-1 me-4">Target</div>
													<!--end::Label-->
													<!--begin::Stats-->
													<div class="fw-bolder text-gray-700 text-xxl-end"><asp:Literal ID="lblTargetAmount" runat="server" Text="0.00"></asp:Literal> </div>
													<!--end::Stats-->
												</div>
												<!--end::Label-->
												<!--begin::Label-->
												<div class="d-flex fw-semibold align-items-center my-3">
													<!--begin::Bullet-->
													<div class="bullet w-8px h-3px rounded-2 bg-primary me-3"></div>
													<!--end::Bullet-->
													<!--begin::Label-->
													<div class="text-gray-500 flex-grow-1 me-4">Raised </div>
													<!--end::Label-->
													<!--begin::Stats-->
													<div class="fw-bolder text-gray-700 text-xxl-end"><asp:Literal ID="lblRaised" runat="server" Text="0.00"></asp:Literal></div>
													<!--end::Stats-->
												</div>
												<!--end::Label-->
												<!--begin::Label-->
												<div class="d-flex fw-semibold align-items-center">
													<!--begin::Bullet-->
													<div class="bullet w-8px h-3px rounded-2 me-3" style="background-color: #E4E6EF"></div>
													<!--end::Bullet-->
													<!--begin::Label-->
													<div class="text-gray-500 flex-grow-1 me-4">Remaining</div>
													<!--end::Label-->
													<!--begin::Stats-->
													<div class="fw-bolder text-gray-700 text-xxl-end"><asp:Literal ID="lblRemainig" runat="server" Text="0.00"></asp:Literal></div>
													<!--end::Stats-->
												</div>
												<!--end::Label-->
											</div>
											<!--end::Labels-->
										</div>
										<!--end::Card body-->
									</div>
        </div>

          <div class="col-md-4">

              </div>
    </div>

  
    

<asp:HiddenField ID="hraised" runat="server" Value="30.00" ClientIDMode="Static" />

<asp:HiddenField ID="hremaing" runat="server" Value="40.00" ClientIDMode="Static" />
<script>
    dchart();

    function dchart() {
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
               // circumference: 180,
                cutoutPercentage: 75,
                responsive: true,
                maintainAspectRatio: true,
                cutout: '85%',
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
                        display: false,
                        position: 'bottom',
                        textStyle: { fontSize: 34 },
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


 <%--   <script src="assets/js/custom/widgets.js"></script>--%>


    <div class="row mb-6">
        <asp:GridView ID="gridtopdonors" runat="server">
                            <Columns>
                                  <asp:TemplateField HeaderText="Date & Time"  ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTime" runat="server" Text='<%# Eval("PaidDate","{0:dd/MM/yyyy HH:mm}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name"  ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" HeaderStyle-CssClass="header_right"  ItemStyle-CssClass="header_right" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:C2}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-Width="25%">
                                    <ItemTemplate>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
