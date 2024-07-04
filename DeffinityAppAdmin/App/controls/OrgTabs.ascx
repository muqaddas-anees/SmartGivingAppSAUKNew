<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrgTabs.ascx.cs" Inherits="DeffinityAppDev.App.controls.OrgTabs" %>
<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6 " href="<%=getUrl(0)%>"><span> Organization</span></a>
											</li>
											<!--end::Nav item-->
											<!--begin::Nav item-->
										<%--	<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="<%=getUrl(1)%>">Contacts</a>
											</li>--%>
											<!--end::Nav item-->
											<!--begin::Nav item-->
											<%--<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="<%=getUrl(2)%>">Associated Sites</a>
											</li>
											<!--end::Nav item-->
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="<%=getUrl(3)%>">Denomination</a>
											</li>
											<!--end::Nav item-->
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="<%=getUrl(4)%>">Activities</a>
											</li>
											<!--end::Nav item-->
											<!--begin::Nav item-->
											<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="<%=getUrl(5)%>">Images</a>
											</li>--%>
											<!--end::Nav item-->
											<!--begin::Nav item-->
											<%--<li class="nav-item">
												<a class="nav-link text-active-primary me-6" href="../../demo4/dist/account/api-keys.html">API Keys</a>
											</li>--%>
											<!--end::Nav item-->
										</ul>
									</div>
									<!--begin::Navs-->
								</div>
							</div>
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
