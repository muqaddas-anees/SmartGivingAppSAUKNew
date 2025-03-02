﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonationReportTabs.ascx.cs" Inherits="DeffinityAppDev.App.controls.DonationReportTabs" %>
<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/DashboardReport.aspx?type=dona">Donations</a>
											</li>
										
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/DashboardReport.aspx?type=inkind">In-Kind Donations</a>
											</li>

											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/DashboardReport.aspx?type=cash">Cash Donations</a>
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

        if (_type == "inkind") {
            activeNewTab("In-Kind Donations");
        }
        else if (_type == "cash") {
            activeNewTab("Cash Donations");
        }
        else {
            activeNewTab("Donations");
        }

    });


</script>