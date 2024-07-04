<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TithingCategoryTabs.ascx.cs" Inherits="DeffinityAppDev.App.controls.TithingCategoryTabs" %>
<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="TithingCategorySettings.aspx?type=active">All (Active)</a>
											</li>
										
										<%--	<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="TithingCategorySettings.aspx?type=hidden">Hidden</a>
											</li>--%>

											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="TithingCategorySettings.aspx?type=inactive"> Inactive</a>
											</li>
											
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