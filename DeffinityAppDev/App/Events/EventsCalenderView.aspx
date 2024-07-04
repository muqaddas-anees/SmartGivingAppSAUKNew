<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EventsCalenderView.aspx.cs" Inherits="DeffinityAppDev.App.EventsCalenderView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


   <!--begin::Head-->
	<head><base href="../../../">
		<title>Fullcalendar Basic Examples by Keenthemes</title>
		<meta name="description" content="The most advanced Bootstrap Admin Theme on Themeforest trusted by 94,000 beginners and professionals. Multi-demo, Dark Mode, RTL support and complete React, Angular, Vue &amp; Laravel versions. Grab your copy now and get life-time updates for free." />
		<meta name="keywords" content="Metronic, bootstrap, bootstrap 5, Angular, VueJs, React, Laravel, admin themes, web design, figma, web development, free templates, free admin themes, bootstrap theme, bootstrap template, bootstrap dashboard, bootstrap dak mode, bootstrap button, bootstrap datepicker, bootstrap timepicker, fullcalendar, datatables, flaticon" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<meta property="og:title" content="Metronic - Bootstrap 5 HTML, VueJS, React, Angular &amp; Laravel Admin Dashboard Theme" />
		<meta property="og:url" content="https://keenthemes.com/metronic" />
		<meta property="og:site_name" content="Keenthemes | Metronic" />
		<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Page Vendor Stylesheets(used by this page)-->
		<link href="assets/plugins/custom/prismjs/prismjs.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/plugins/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->

		 <script type="text/jscript"  src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>

		<script type="text/jscript" src="https://cdn.jsdelivr.net/npm/fullcalendar-scheduler@5.3.0/main.min.js"></script>

	</head>
	<!--end::Head-->



  <body>
		<!--begin::Main-->




	  	<!--begin::Content-->
					<div class="docs-content d-flex flex-column flex-column-fluid" id="kt_docs_content">
						
							<!--begin::Card-->
							<div class="card mb-2">
								<!--begin::Card Body-->

								<div class="card-body fs-6 py-15 px-10 py-lg-15 px-lg-15 text-gray-700">
									
									<!--begin::Section-->
									<div class="pt-10">
										
										<!--begin::Block-->
										<div class="py-5">
											<div id="kt_docs_fullcalendar"     style="width:900px; height:600px"      ></div>

											
										</div>


                                       
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                         <div class='demo-topbar'>

  

  
   
  
                                       </div>

 









										<!--end::Block-->
										
									</div>
									<!--end::Section-->
								</div>
								<!--end::Card Body-->
							</div>
							<!--end::Card-->
						</div>
						<!--end::Container-->





	



	



      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>



      <script>

        

         

         



          function testingwebservices() {

              $.ajax({
                  url: './App/WebService/EventDetailsData.asmx/GetEventDetails',
                  method: 'post',
                  dataType: 'json',
                  success: function (obj) {
                      //<a href="WebService/DataServices.asmx">WebService/DataServices.asmx</a>


                    

                     


                      var events = [];

                      $(obj).each(function (index, item) {


						

                          events.push({

                              title: item.title,
                              start: item.start ,
                              end: item.end
                          });


                        

                      });



                     



                      
                 



                     



                      var calendarEl = document.getElementById('kt_docs_fullcalendar');

                      var calendar = new FullCalendar.Calendar(calendarEl, {
                          initialView: 'dayGridMonth',
                          initialDate: '2022-04-07',
                          headerToolbar: {
                              left: 'prev,next today',
                              center: 'title',
                              right: 'dayGridMonth,timeGridWeek,timeGridDay'
                          },


                        

                         
                          



                             


                          events: events



                         
                      });

                     




                    



                      calendar.render();


                    



                     
                     

                     

                     





                  },
                  error: function (err) {

                     

                  }
              });

          }


        







          testingwebservices();




    




















          





                                        </script>









		



	 


		<!--begin::Javascript-->
		<!--begin::Global Javascript Bundle(used by all pages)-->
		<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src="assets/plugins/custom/prismjs/prismjs.bundle.js"></script>
		<script src="assets/plugins/custom/fullcalendar/fullcalendar.bundle.js"></script>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src="assets/js/custom/documentation/documentation.js"></script>
		<script src="assets/js/custom/documentation/search.js"></script>
		<script src="assets/js/custom/documentation/general/fullcalendar/basic.js"></script>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
	</body>
	<!--end::Body-->



</asp:Content>
