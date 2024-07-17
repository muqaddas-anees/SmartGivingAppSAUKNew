<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="TaithingDashboardV2.aspx.cs" Inherits="DeffinityAppDev.App.TaithingDashboardV2" %>

<%@ Register Src="~/App/controls/VideoCtrl.ascx" TagPrefix="Pref" TagName="VideoCtrl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <asp:Label ID="lblPagetitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
      <div class="notice d-flex bg-light-warning rounded border-warning border border-dashed p-6 mb-6" id="pnlmidcheck" runat="server">
										
										<span class="svg-icon svg-icon-2tx svg-icon-warning me-4">
											<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
												<rect opacity="0.3" x="2" y="2" width="20" height="20" rx="10" fill="black"></rect>
												<rect x="11" y="14" width="7" height="2" rx="1" transform="rotate(-90 11 14)" fill="black"></rect>
												<rect x="11" y="17" width="2" height="2" rx="1" transform="rotate(-90 11 17)" fill="black"></rect>
											</svg>
										</span>
										
										<div class="d-flex flex-stack flex-grow-1">
											
											<div class="fw-bold">
												<h4 class="text-gray-900 fw-bolder"><asp:Label ID="lblMIDTitle" runat="server" Text="We need your attention!"></asp:Label></h4>
												<div class="fs-6 text-gray-700"><asp:Label ID="lblMIDDescription" runat="server"></asp:Label><asp:Label ID="lblnofdays" runat="server" Text="Stripe Activation Required. Please click here to get started"></asp:Label> 
													<asp:Button ID="btnView" runat="server" SkinID="btnDefault" Text="Get Started with Stripe" ClientIDMode="Static" OnClick="btnView_Click" />
													</div>
											</div>
											
										</div>
										
									</div>


    <div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/Dashboard.aspx?type=1">Introduction</a>
											</li>
										
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/Dashboard.aspx?type=2" >Dashboard</a>
											</li>
                                           
										</ul>
									</div>
									<!--begin::Navs-->
								</div>
							</div>



<script type="text/javascript">

    function activeNewTab(name) {
        $(".nav-stretch a").each(function (index, element) {
            //  $(element).attr('class', 'nav-link text-active-primary me-6');
            var cu = name.toLowerCase();
            var ck = $(element).html().toLowerCase();

            //var cu = $(location).attr('href').toLowerCase();
            //var ck = $(element).attr('href').toLowerCase().replace('..', '');
            console.log('cu:' + cu);
            console.log('ck:' + ck);
            // if (cu.indexOf($.trim(ck)) > -1) {
            if (cu === ck) {
                debugger;
                $(element).attr('class', 'nav-link text-active-primary me-6 active');
                //$(element).closest('li').attr('class', 'active');
            }


        });
    }
    $(document).ready(function () {
        var _type = getQuerystring('type');
        //alert(_type);
        if (_type == "2") {
            activeNewTab("Dashboard");
        }
        else {
            activeNewTab("Introduction");
        }

    });

    function showtab(t) {
        if (t == "2") {
            activeNewTab("Dashboard");
        }
        else {
            activeNewTab("Introduction");
        }
    }
</script>





    <div class="row" id="pnl1" runat="server">
     <Pref:VideoCtrl runat="server" id="VideoCtrl" />

        </div>
     <style>
        
        /* Custom styles for the checkmarks */
        .condition {
            display: flex;
            align-items: center;
            margin-top: 5px;
        }

        .condition .icon {
            color: #dc3545; /* red by default */
            margin-right: 5px;
        }

        .condition.valid .icon {
            color: #28a745; /* green when valid */
        }
          #password-conditions {
            display: none; /* Initially hidden */
        }
    </style>

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

 <%--   <style>
        .btn-video{
            background-color:#78A607;
            color:white;
        }

        .btn-video:hover{
            background-color:#78A607;
            color:white;
            
        }
         .btn-weight {
            color:white;
           
            
        }
    </style>--%>

    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" />
    <script src="../assets/plugins/global/plugins.bundle.js"></script>
   
   <div class="row" id="pnl2" runat="server">

    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer gap-3" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Dashboard</h3>
            </div>
            <div class="card-toolbar gap-3" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-trigger="hover" title="">
                 <a class="btn btn-video" style="background-color:#50CD89;color:white;display:none;visibility:hidden"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/823463217?h=13ce42bbd5" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
                <asp:Button ID="btnTithing" runat="server" CssClass="btn btn-primary" Text="Donate" OnClick="btnTithing_Click"  Visible="false"    />
                 <asp:Button ID="btnDonation_kind" runat="server" CssClass="btn btn-primary" Text="In-Kind Donations" OnClick="btnDonation_kind_Click" Visible="false"     />
                 <asp:Button ID="btnDonation_Cash" runat="server" CssClass="btn btn-primary" Text="Cash Donations" OnClick="btnDonation_Cash_Click"  Visible="false"    />
               <%--  <asp:Button ID="btnCategory" runat="server" CssClass="btn btn-light" Text="Donation Categories" OnClick="btnCategory_Click" style="margin-right:10px"     />--%>
                 <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Text="View Report" OnClick="btnReport_Click"    Visible="false"  />
              
                <%-- <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Add New Category" OnClick="btnAddOrganization_Click"       />--%>
            </div>
            <!--end::Card title-->
        </div>
        <!--begin::Card header-->

    </div>
      <div class="card-header border-0 cursor-pointer mb-6" >
             <div class="card-body ">

                

      <div class="row">
          <label class="col-lg-1 col-form-label fw-bold fs-6">From Date</label>
           <div class="col-lg-2">
               <asp:TextBox ID="txtFromDate" runat="server" SkinID="DateNew" ClientIDMode="Static"></asp:TextBox>
               </div>
            <label class="col-lg-1 col-form-label fw-bold fs-6">To Date</label>
           <div class="col-lg-2">
               <asp:TextBox ID="txtToDate" runat="server" SkinID="DateNew" ClientIDMode="Static"></asp:TextBox>
               </div>
           <div class="col-lg-1">
               <asp:Button ID="btnSearch" runat="server" SkinID="btnDefault" Text="Search" OnClick="btnSearch_Click" />
               </div>
          </div>
                 </div>
          </div>
    <div class="row mb-5">
        <div class="col-lg-4">
            <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Total Transaction Amount</span>

																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    
                                                                    <label id="lbltotal_transaction" runat="server">0.00</label> 
                                                                    <asp:Label ID="icon_totalicon" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>
        <div class="col-lg-4">
              <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Largest Transaction</span>
																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    <label id="lblLarget_transaction" runat="server">0.00</label> 
                                                                     <asp:Label ID="icon_larget_transaction" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>

        </div>
        <div class="col-lg-4">
              <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Average Transaction</span>
																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true">
                                                                    <label id="lblAverage_transaction" runat="server">0.00</label> 
                                                                     <asp:Label ID="icon_average_transaction_icon" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
        </div>

    </div>

     <div class="row mb-5">
        <div class="col-lg-4">
            <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block"> Transactions</span>
																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true"><label id="lblTransactions" runat="server">0.00</label> 
                                                                     <asp:Label ID="icon_transactions" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
            

        </div>
        <div class="col-lg-4">
              <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">Total Donors</span>
																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true"><label id="lblDonors" runat="server">0</label> 
                                                                     <asp:Label ID="icon_donors" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>

        </div>
        <div class="col-lg-4">
              <div class="card card-flush mb-5 mb-xl-10">
                               
                                <div class="card-body ">
                                    <div class=" text-center rounded pt-4 pb-2 my-3">
																<span class="fs-1  fw-bold text-primary d-block">New Donors</span>
																<span class="d-flex justify-content-center d-inline fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true"><label id="lblNewDonors" runat="server">0</label> 
                                                                     <asp:Label ID="icon_newdonors" runat="server" style="width:80px;padding-top:15px;padding-left:10px"></asp:Label>

																</span>
															</div>
                                </div>
                                <!--end::Card body-->
                            </div>
        </div>

    </div>
     <div class="row mb-5">
           <div class="col-lg-4">
                 <div class="card ">
                        <!--begin::Header-->
                        <div class="card-header py-5">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bolder text-dark">Daily Transaction Totals</span>
                                <span class="text-gray-400 mt-1 fw-bold fs-6"></span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                           
                            <!--end::Toolbar-->
                        </div>
                   <div class="card-body d-flex justify-content-center d-inline flex-column pb-1 px-0">
                        <div id="barchart" class="d-flex justify-content-center d-inline" style="height: 300px; width: 100%;"></div>
            <%-- <div id="piechart" style="height: 300px; width: 100%;"></div>--%>
                       </div>
                  </div>
            </div>
          <div class="col-lg-4">
                <div class="card ">
                        <!--begin::Header-->
                        <div class="card-header py-5">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bolder text-dark">Cumulative Transaction Totals</span>
                                <span class="text-gray-400 mt-1 fw-bold fs-6"></span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                           
                            <!--end::Toolbar-->
                        </div>
                   <div class="card-body d-flex justify-content-center d-inline flex-column pb-1 px-0">
                       <div id="linechart" class="d-flex justify-content-center d-inline" style="height: 300px; width: 100%;"></div>
            <%-- <div id="piechart" style="height: 300px; width: 100%;"></div>--%>
                       </div>
                  </div>
            </div>
        <div class="col-lg-4">
              <div class="card ">
                        <!--begin::Header-->
                        <div class="card-header py-5">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bolder text-dark">One-Time vs. Recurring Transactions</span>
                                <span class="text-gray-400 mt-1 fw-bold fs-6"></span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                           
                            <!--end::Toolbar-->
                        </div>
                   <div class="card-body d-flex justify-content-center d-inline flex-column pb-1 px-0">
             <div id="piechart" class="d-flex justify-content-center d-inline" style="height: 300px; width: 100%;"></div>
                       </div>
                  </div>
            </div>
        
         </div>
    <!--begin::Content-->
    <div class="content d-flex flex-column flex-column-fluid" id="kt_content">




       


    </div>
    <!--end::Content-->

    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
    TargetControlID="btnPop_open" PopupControlID="Panel_portfolio" 
    CancelControlID="lbtnCloseOptions" 
    BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
<asp:Label ID="btnPop_open" runat="server"></asp:Label>
    <asp:Label ID="lbtnCloseOptions" runat="server"></asp:Label>
<asp:Panel ID="Panel_portfolio" ClientIDMode="Static" runat="server" Width="50%" CssClass="card shadow-sm">
   <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Change Password"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions1" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" Visible="false" />
								
							</div>
						</div>
    <div class="card-body">


  <%-- <asp:UpdatePanel ID="UpdatePanle_PortfolioDDL" runat="server">
<ContentTemplate>--%>
   <%-- <div class="modal-body">--%>
 
<div class="form-group row mb-6">
    <div class="col-md-12">
         <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
        <asp:Label ID="lblError" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
    </div>
</div>

 <asp:Panel ID="Panel1" runat="server" ForeColor="Black" Visible="true">

     <div class="form-group row mb-6">
          <div class="col-md-5 well">
<div class="form-group row mb-6">
     
          <label class="col-sm-4 control-label"> Old Password:</label>
          <div class="col-sm-8">
              <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="OldpwdReq" runat="server" ControlToValidate="txtOldPwd"
                  ErrorMessage="Please enter old password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
          </div>
      
</div>
    
<div class="form-group row mb-6">
    
          <label class="col-sm-4 control-label"> New Password:</label>
          <div class="col-sm-8">
              <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="newPasswordReq" runat="server" ControlToValidate="txtNewPwd"
                  ErrorMessage="Please enter new password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator 
    ID="regexPasswordLength" 
    runat="server" 
    ControlToValidate="txtNewPwd"
    ValidationExpression="^.{8,20}$" 
    ErrorMessage="Password must be between 8 to 20 characters long." SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert">
</asp:RegularExpressionValidator>

                 <div id="password-conditions">
                    <div class="condition" id="length-condition">
                        <span class="icon">&#10060;</span> 8-20 characters
                    </div>
                    <div class="condition" id="uppercase-condition">
                        <span class="icon">&#10060;</span> One uppercase letter
                    </div>
                    <div class="condition" id="special-char-condition">
                        <span class="icon">&#10060;</span> One special character
                    </div>
                </div>
          </div>
      
</div>

<div class="form-group row mb-6">
     
          <label class="col-sm-4 control-label">Confirm New Password:</label>
          <div class="col-sm-8">
               <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password"></asp:TextBox><br />
               <asp:RequiredFieldValidator ID="confirmPasswordReq" runat="server" ControlToValidate="txtConfirmPwd"
                   ErrorMessage="Please enter confirmation password" SetFocusOnError="True" Display="Dynamic"
                   ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPwd"
                    ControlToValidate="txtConfirmPwd" ErrorMessage="Your passwords do not match up"
                    Display="Dynamic" ValidationGroup="ValInsert"></asp:CompareValidator>
          </div>
     
</div>
<div class="form-group row mb-6">
    
          <label class="col-sm-4 control-label"></label>
          <div class="col-sm-8">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" ValidationGroup="ValInsert"
                                OnClick="btnSubmit_Click" disabled />
                            &nbsp;
              <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel" />
          </div>
     
</div> 
              </div>
     </div>
</asp:Panel>

      <%-- </div>--%>
        </div>
</asp:Panel>


</div>

    <script>
        $(document).ready(function () {
           <%-- $('#<%=txtNewPwd.ClientID%>').on('input', function(){
                var password = $(this).val();
                $('#password-conditions').show();

                var hasUpperCase = /[A-Z]/.test(password);
                var hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);
                var isValidLength = password.length >= 8 && password.length <= 20;
                var isPasswordValid = hasUpperCase && hasSpecialChar && isValidLength;

                // Update condition indicators
                updateConditionIndicator('#length-condition', isValidLength);
                updateConditionIndicator('#uppercase-condition', hasUpperCase);
                updateConditionIndicator('#special-char-condition', hasSpecialChar);

                // Update the validity of the password field and submit button
                $(this).toggleClass('is-invalid', !isPasswordValid);
                $(this).toggleClass('is-valid', isPasswordValid);
                $('#<%=btnSubmit.ClientID%>').prop('disabled', !isPasswordValid);
            });--%>

            function updateConditionIndicator(elementId, isValid) {
                if (isValid) {
                    $(elementId).addClass('valid');
                    $(elementId + ' .icon').html('&#10004;'); // Checkmark
                } else {
                    $(elementId).removeClass('valid');
                    $(elementId + ' .icon').html('&#10060;'); // Cross
                }
            }
        });
    </script>

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        // Load google charts
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        // Draw the chart and set the chart values
        function drawChart() {
            //var data = google.visualization.arrayToDataTable([
            //    ['Task', 'Hours per Day'],
            //    ['One-Time', 8],
            //    ['Recurring', 2],
            //]);

            // Optional; add a title and set the width and height of the chart
            var options = {
                'width': 400, 'height': 300, legend: {
                    position: 'bottom',
                  
                },
                colors: ['#009EF7', '#8E60EE']
                };

            // Display the chart inside the <div> element with id="piechart"
            //var chart = new google.visualization.PieChart(document.getElementById('piechart'));




            //chart.draw(data, options);

            var d1 = {
                'startdate': $('#txtFromDate').val(),
                'enddate': $('#txtToDate').val()
            };

            $.ajax({
                type: "POST",
                url: "chartdata.asmx/GetPieData",
                data: JSON.stringify(d1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                   // var chart = new google.visualization.ColumnChart($("#chart")[0]);
                    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
                    chart.draw(data, options);
                },
                failure: function (r) {
                    console.log(r.d);
                },
                error: function (r) {
                    console.log(r.d);
                }
            });
        }
    </script>

    <script type="text/javascript">
        //google.load("visualization", "1", { packages: ["corechart"] });
        //google.setOnLoadCallback(drawChart);
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartBar);
        function drawChartBar() {
            var options = {
                title: '',
                width: 500,
                height: 300,
                bar: { groupWidth: "95%" },
                legend: { position: "none" },
                isStacked: true,
                colors: ['#009EF7']
            };
            debugger;
            var d1 = {
                'startdate': $('#txtFromDate').val(),
                'enddate': $('#txtToDate').val()
            };

            
            $.ajax({
                type: "POST",
                url: "chartdata.asmx/GetBarData",
                data: JSON.stringify(d1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.ColumnChart($("#barchart")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    console.log(r.d);
                },
                error: function (r) {
                    console.log(r.d);
                }
            });
        }
    </script>

    <script type="text/javascript">
        //google.load("visualization", "1", { packages: ["corechart"] });
        //google.setOnLoadCallback(drawChart);
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartLine);
        function drawChartLine() {
            var options = {
                title: '',
                width: 500,
                height: 300,
                bar: { groupWidth: "95%" },
                legend: { position: "none" },
                isStacked: true,
                colors: ['#009EF7']
            };

            var d1 = {
                'startdate': $('#txtFromDate').val(),
                'enddate': $('#txtToDate').val()
            };
            $.ajax({
                type: "POST",
                url: "chartdata.asmx/GeLineData",
                data: JSON.stringify(d1),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.AreaChart($("#linechart")[0]);
                    chart.draw(data, options);
                },
                failure: function (r) {
                    console.log(r.d);
                },
                error: function (r) {
                    console.log(r.d);
                }
            });
        }
    </script>
    <script type="text/javascript">
        //window.onload = function () {

        //    var dps = [{ x: 1, y: 10 }, { x: 2, y: 13 }, { x: 3, y: 18 }, { x: 4, y: 20 }, { x: 5, y: 17 }, { x: 6, y: 10 }, { x: 7, y: 13 }, { x: 8, y: 18 }, { x: 9, y: 20 }, { x: 10, y: 17 }];   //dataPoints. 

        //    var chart = new CanvasJS.Chart("chartContainer", {
        //        title: {
        //            text: "Live Data"
        //        },
        //        axisX: {
        //            title: "Axis X Title"
        //        },
        //        axisY: {
        //            title: "Units"
        //        },
        //        data: [{
        //            type: "line",
        //            dataPoints: dps
        //        }]
        //    });

        //    chart.render();
        //    var xVal = dps.length + 1;
        //    var yVal = 15;
        //    var updateInterval = 1000;

        //    var updateChart = function () {


        //        yVal = yVal + Math.round(5 + Math.random() * (-5 - 5));
        //        dps.push({ x: xVal, y: yVal });

        //        xVal++;
        //        if (dps.length > 10) {
        //            dps.shift();
        //        }

        //        chart.render();

        //        // update chart after specified time. 

        //    };

        //    setInterval(function () { updateChart() }, updateInterval);
        //}
    </script>






    <script type="text/javascript">
        //window.onload = function () {

        //    var dps = [{ x: 1, y: 10 }, { x: 2, y: 13 }, { x: 3, y: 18 }, { x: 4, y: 20 }, { x: 5, y: 17 }, { x: 6, y: 10 }, { x: 7, y: 13 }, { x: 8, y: 18 }, { x: 9, y: 20 }, { x: 10, y: 17 }];   //dataPoints. 

        //    var chart = new CanvasJS.Chart("chartContainer1", {
        //        title: {
        //            text: "Live Data"
        //        },
        //        axisX: {
        //            title: "Axis X Title"
        //        },
        //        axisY: {
        //            title: "Units"
        //        },
        //        data: [{
        //            type: "line",
        //            dataPoints: dps
        //        }]
        //    });

        //    chart.render();
        //    var xVal = dps.length + 1;
        //    var yVal = 15;
        //    var updateInterval = 1000;

        //    var updateChart = function () {


        //        yVal = yVal + Math.round(5 + Math.random() * (-5 - 5));
        //        dps.push({ x: xVal, y: yVal });

        //        xVal++;
        //        if (dps.length > 10) {
        //            dps.shift();
        //        }

        //        chart.render();

        //        // update chart after specified time. 

        //    };

        //    setInterval(function () { updateChart() }, updateInterval);
        //}
    </script>









    <script type="text/javascript" src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <%--<script src="jquery-1.7.1.js"></script>--%>



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

   <script src='<%:ResolveClientUrl("~/assets/plugins/custom/fslightbox/fslightbox.bundle.js") %>' type="text/javascript" ></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
