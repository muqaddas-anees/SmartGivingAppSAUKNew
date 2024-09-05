<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrgMainTabs.ascx.cs" Inherits="DeffinityAppDev.App.controls.OrgMainTabs" %>
<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/Organizations.aspx">Organization</a>
											</li>
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/PendingOrganizations.aspx">Organization (Pending)</a>
											</li>
												<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/EmailSettings.aspx">Email Template</a>
											</li>
												<%--<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/PlatformSupportReport.aspx">Platform Support Report</a>
											</li>--%>
										
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/OrganizationAdmin.aspx">Admin Team</a>
											</li>
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/OrganizationPaymentSettings.aspx">Payment Settings</a>
											</li>
												<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/SMSsettings.aspx">SMS Settings</a>
											</li>
								<li class="nav-item">
								<a class="nav-link text-active-primary me-6" href="../App/Wordpress.aspx">Wordpress</a>
				</li>
																		<li class="nav-item">
							<a class="nav-link text-active-primary me-6" href="../App/Marketplace.aspx">Marketplace</a>
</li>

										<%--	<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../App/CardAdmin.aspx"> Card Connect Team</a>
											</li>--%>
											
										</ul>
									</div>
									<!--begin::Navs-->
								</div>
							</div>
<%--<script src="../../Scripts/tabactive.js"></script>--%>

<script type="text/javascript">
       $(document).ready(function () {
           $(".nav-stretch a").each(function (index, element) {

               var cu = $(location).attr('href').toLowerCase();
			   var ck = $(element).attr('href').toLowerCase().replace('..','');
			    
               console.log(ck);
               console.log(cu);
			   if (cu.indexOf($.trim(ck)) > -1) {
				   console.log(ck);
                   $(element).attr('class', 'nav-link text-active-primary me-6 active');
                   //$(element).parents('li').attr('class', 'active');
                   return false;
               }
           });


       });

       function activeTab(name) {
           $(".nav-stretch a").each(function (index, element) {
               var cu = name.toLowerCase();
               var ck = $(element).html().toLowerCase();
			   if (cu.indexOf($.trim(ck)) > -1) {
                   $(element).attr('class', 'active');
                   //$(element).closest('li').attr('class', 'active');
               }
           });
       }

</script>