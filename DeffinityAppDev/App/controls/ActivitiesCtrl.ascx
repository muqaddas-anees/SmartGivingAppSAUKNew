<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.ActivitiesCtrl" %>
<style>
	#ListActivitess, #ListofEvents {
    transition: opacity 0.3s ease-in-out;
}
.hidden {
    opacity: 0;
    pointer-events: none;
    height: 0;
    overflow: hidden;
}

</style>
<%--<asp:UpdatePanel ID="upanelactivites" runat="server" UpdateMode="Conditional">
	<ContentTemplate>--%>
<div style="
    right: 25px;
    top: 25px;
    position: absolute;
	display:flex;
"><div data-kt-daterangepicker="true" style="
    width: 213px;
" data-kt-daterangepicker-opens="left" class="btn btn-sm btn-light d-flex align-items-center px-4">
															<!--begin::Display range-->
															<div id="dateRange" class="text-gray-600 fw-bold">Loading date range...</div>
															<!--end::Display range-->
															<i class="ki-outline ki-calendar-8 text-gray-500 lh-0 fs-2 ms-2 me-0"></i>
														</div>



	<div style="margin-left:10px;">
														<!--begin::Menu-->
														<button class="btn btn-icon btn-color-gray-500 btn-active-color-primary justify-content-end" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-overflow="true">
															<i class="ki-outline ki-dots-square fs-1 text-gray-500 me-n1"></i>
														</button><div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-semibold w-200px" data-kt-menu="true" style="">
															<!--begin::Menu item-->
															<div class="menu-item px-3">
																<div class="menu-content fs-6 text-gray-900 fw-bold px-3 py-4">Quick Actions</div>
															</div>
															<!--end::Menu item-->
															<!--begin::Menu separator-->
															<div class="separator mb-3 opacity-75"></div>
															<!--end::Menu separator-->
															<!--begin::Menu item-->
															<div class="menu-item px-3">
																<a id="panellink" onclick="switchView('panel')" class="menu-link px-3 ">Panel View</a>
															</div>
															<!--end::Menu item-->
															<!--begin::Menu item-->
															<div class="menu-item px-3">
																<a id="listlink" onclick="switchView('list')" class="menu-link active px-3">List View</a>
															</div>
															<!--end::Menu item-->
															<!--begin::Menu item-->
															<!--end::Menu item-->
														</div>
														<!--begin::Menu 2-->
														
														<!--end::Menu 2-->
														<!--end::Menu-->
													
	</div>
</div>

			<div class="scroll-y mh-560px">
<div class="row g-5 mb-6" style="overflow-y:scroll;max-height:560px">
                                           
		<div  class="row d-flex d-inline">

									<div class="row d-flex d-inline hidden" id="ListActivitess">		<asp:ListView style="display:block" ID="ListActivites" runat="server" OnItemCommand="ListActivites_ItemCommand" >
														<LayoutTemplate>
       	<div runat="server"  id="itemPlaceholder" class="row d-flex d-inline"></div>
			
															</LayoutTemplate>

														<ItemTemplate>

                                                            <div data-start-time='<%# Eval("StartDateTime") %>' data-end-time='<%# Eval("EndDateTime") %>' class="col-md-4 mb-6">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<!--begin::Overlay-->
													<a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href="#">
														<!--begin::Image-->
														<asp:Image ID="img_event" runat="server" ImageUrl='<%# GetImmage(Eval("unid").ToString()) %>' CssClass="img-fluid img-thumbnail min-h-175px" />
														<%--<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" style="background-image:url('<%# GetImmage(Eval("unid").ToString()) %>')"></div>--%>
														<!--end::Image-->
														<!--begin::Action-->
														<div class="overlay-layer bg-dark card-rounded bg-opacity-25">
															<i class="bi bi-eye-fill fs-2x text-white"></i>
														</div>
														<!--end::Action-->
													</a>
													<!--end::Overlay-->
													<!--begin::Body-->
													<div class="m-0 p-2">
														<!--begin::Title-->
														<a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base" style="min-height:125px;max-height:150px"> <%# Eval("Title") %> </a>
														<!--end::Title-->
														<!--begin::Text-->
														<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5 overflow-scroll" style="min-height:125px;max-height:150px"><%# GetAddress(Eval("Description")) %></div>
													
													</div>
													<!--end::Body-->
												
																 <div class="card-footer">
<asp:Button 
    style='<%# "float:right;background-color:" + Eval("BookTicketsButtonColor") + ";color:" + Eval("BookTicketsButtonFontColor") %>' 
    ID="Button1" 
    runat="server" 
    CssClass="btn" 
    CommandName="amount" 
    Text="Book Tickets" 
    CommandArgument='<%# Eval("unid") %>' 
/>
													<a 
    id="link" 
    style='<%# (bool)Eval("isInPerson") 
            ? "display:none;float:left;" 
            : "display:block;float:left;background-color:" + Eval("ViewLiveButtonColor") + ";color:" + Eval("ViewLiveButtonFontColor") %>' 
    href="../LiveEvent.aspx?unid=<%# Eval("unid") %>" 
    class="btn btn-primary">
    Livestream
</a>

    </div></div>
												<!--end::Publications post-->
											</div>







														</ItemTemplate>
													</asp:ListView></div>
							<div id="ListofEvents" class="">

								<asp:Literal runat="server" ID="ltrEvents" ></asp:Literal>

							</div>
			</div>									
																		</div>
</div>


<script>
    document.addEventListener("DOMContentLoaded", function () {
        const dateRangePickerElement = document.querySelector('[data-kt-daterangepicker="true"]');

        if (dateRangePickerElement) {
            // Initialize the date range picker and listen to changes
            $(dateRangePickerElement).on('apply.daterangepicker', function (ev, picker) {
                // Get selected date range
                const startDate = picker.startDate.startOf('day');
                const endDate = picker.endDate.endOf('day');

                // Filter cards based on the selected range
                filterCardsByDateRange(startDate, endDate);
            });

            // On page load, get the current date range and filter
            const daterangepickerInstance = $(dateRangePickerElement).data('daterangepicker');
            if (daterangepickerInstance) {
                const currentStartDate = daterangepickerInstance.startDate.startOf('day');
                const currentEndDate = daterangepickerInstance.endDate.endOf('day');
                filterCardsByDateRange(currentStartDate, currentEndDate);
            }
        }

        function filterCardsByDateRange(startDate, endDate) {
            // Select all cards
            const cards = document.querySelectorAll('[data-start-time]');

            cards.forEach(card => {
                const cardStartTime = moment(card.getAttribute('data-start-time'), 'DD/MM/YYYY HH:mm:ss');
                const cardEndTime = moment(card.getAttribute('data-end-time'), 'DD/MM/YYYY HH:mm:ss');

                // Check if the card is within the selected date range
                if (cardStartTime.isBetween(startDate, endDate, undefined, '[]') ||
                    cardEndTime.isBetween(startDate, endDate, undefined, '[]')) {
                    card.style.display = 'block'; // Show card
                } else {
                    card.style.display = 'none'; // Hide card
                }
			});





            const listcards = document.querySelectorAll('[list-data-start-time]');

            listcards.forEach(card => {
                const cardStartTime = moment(card.getAttribute('list-data-start-time'), 'DD/MM/YYYY HH:mm:ss');
                const cardEndTime = moment(card.getAttribute('list-data-end-time'), 'DD/MM/YYYY HH:mm:ss');

                // Check if the card is within the selected date range
                if (cardStartTime.isBetween(startDate, endDate, undefined, '[]') ||
                    cardEndTime.isBetween(startDate, endDate, undefined, '[]')) {
                    card.style.display = 'flex'; // Show card
                } else {
                    card.style.display = 'none'; // Hide card
                }
            });


        }
    });

                            </script>
<script>
    function switchView(view) {
        const panelView = document.getElementById('ListActivitess');
        const listView = document.getElementById('ListofEvents');

        if (view === 'panel') {
            panelView.classList.remove('hidden');
            listView.classList.add('hidden');
        } else {
            panelView.classList.add('hidden');
            listView.classList.remove('hidden');
        }

        document.querySelectorAll('.menu-link').forEach(link => link.classList.remove('active'));
        const activeLink = view === 'panel'
            ? document.getElementById('panellink')
            : document.getElementById('listlink');
        if (activeLink) activeLink.classList.add('active');
    }
                            </script>

	