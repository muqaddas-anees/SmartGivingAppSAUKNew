<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EventInteraction.aspx.cs" Inherits="DeffinityAppDev.App.Events.EventInteraction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Event Interaction
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    

     <style>
           .mycheckBig input {width:18px; height:18px;}
           .mycheckBig label {padding-left:8px;font-size:16px}
       </style>


    <div class="row g-5 g-xl-8">
										<div class="col-xl-4">


                                        

											<!--begin::Statistics Widget 1-->
											<div class="card bgi-no-repeat card-xl-stretch mb-xl-8 overlay overflow-hidden my-12" style="background-position: right top; background-size: 30% auto; background-image: url(../../assets/media/svg/shapes/abstract-4.svg">
												<!--begin::Body-->
												<div class="card-body">
                                                     <div class="overlay-wrapper">

													<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-1">Fundraisers</a>
													<div class="fw-bold text-primary my-6"></div>
													<p class="text-dark-75 fw-semibold fs-5 m-0"> Use this section to create on-the-spot fundraisers. Members scan a QR code and donate from their smartphones.</p>

</div>
                                                      <div class="overlay-layer bg-dark bg-opacity-25 align-items-end justify-content-center">
                     <div class="d-flex flex-grow-1 flex-center  py-5">
                    <a href="../../App/AddFundraiser.aspx?eventunid=<%=QueryStringValues.EVENTUNID %>" class="btn btn-primary btn-shadow">Create</a>
                    <a href="../../App/FundraiserList.aspx?eventunid=<%=QueryStringValues.EVENTUNID %>" class="btn btn-light-primary btn-shadow ms-2">View</a>
                </div></div>
												</div>
												<!--end::Body-->
											</div>
											<!--end::Statistics Widget 1-->
										</div>
										<div class="col-xl-4">
											<!--begin::Statistics Widget 1-->
											<div class="card bgi-no-repeat card-xl-stretch mb-xl-8 overlay overflow-hidden my-12" style="background-position: right top; background-size: 30% auto; background-image: url(../../assets/media/svg/shapes/abstract-2.svg">
												<!--begin::Body-->
												<div class="card-body">
                                                     <div class="overlay-wrapper">


													<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-1">Polls</a>
													<div class="fw-bold text-primary my-6"></div>
													<p class="text-dark-75 fw-semibold fs-5 m-0">Launch an opinion poll. Members scan a QR code to respond to your poll from their smartphones.</p>
                                                         </div>
                                                       <div class="overlay-layer bg-dark bg-opacity-25 align-items-end justify-content-center">
                    <div class="d-flex flex-grow-1 flex-center  py-5">
                        <a href="../../App/Events/PollAndSurvey/Polls/Set_up_a_Poll.aspx?eventunid=<%=QueryStringValues.EVENTUNID %>" class="btn btn-primary btn-shadow">Create</a>
                        <a href="../../App/Events/PollAndSurvey/Polls/Grid_Set_Up_New_Poll.aspx?eventunid=<%=QueryStringValues.EVENTUNID %>" class="btn btn-light-primary btn-shadow ms-2">View</a>
                    </div>
                </div>
												</div>
												<!--end::Body-->
											</div>
											<!--end::Statistics Widget 1-->
										</div>
										<div class="col-xl-4">
											<!--begin::Statistics Widget 1-->
											<div class="card bgi-no-repeat card-xl-stretch mb-xl-8 overlay overflow-hidden my-12" style="background-position: right top; background-size: 30% auto; background-image: url(../../assets/media/svg/shapes/abstract-1.svg">
												<!--begin::Body-->
												<div class="card-body">
                                                     <div class="overlay-wrapper">
													<a href="#" class="card-title fw-bold text-muted text-hover-primary fs-1">Survey</a>
													<div class="fw-bold text-primary my-6"></div>
													<p class="text-dark-75 fw-semibold fs-5 m-0">An easy way to collect feedback in just minutes designed to get you accurate results you can rely on. Members scan a QR code to respond using their smartphones.</p>
												</div>
                                                     <div class="overlay-layer bg-dark bg-opacity-25 align-items-end justify-content-center">
                    <div class="d-flex flex-grow-1 flex-center py-5">
                        <a href="../../App/Events/PollAndSurvey/Survey/Set_Up_a_Survey.aspx?eventunid=<%=QueryStringValues.EVENTUNID %>" class="btn btn-primary btn-shadow">Create</a>
                        <a href="../../App/Events/PollAndSurvey/Survey/Grid_Set_Up_New_Survey.aspx?eventunid=<%=QueryStringValues.EVENTUNID %>" class="btn btn-light-primary btn-shadow ms-2">View</a>
                    </div>
                </div>
                                                    </div>
												<!--end::Body-->
											</div>
											<!--end::Statistics Widget 1-->
										</div>
									</div>

     <div class="row mb-6 ">

         <asp:Label ID="lblPageTitle" runat="server" Text="Create event interaction" Font-Size="20px" Visible="false"></asp:Label>

     </div>
  

    <div class="row mb-6 mx-2" style="display:none;visibility:hidden;">
        <asp:RadioButtonList ID="rdlist" runat="server" CssClass="mycheckBig">
            <asp:ListItem Text="Fundraiser" Value="Fundraiser"></asp:ListItem>
             <asp:ListItem Text="Poll" Value="Poll"></asp:ListItem>
             <asp:ListItem Text="Survey" Value="Survey"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

   <div class="row mb-6"  style="display:none;visibility:hidden;">
       <div class="col-lg-12">
       <asp:Button ID="btnCreate" runat="server" SkinID="btnDefault" Text="Create" OnClick="btnCreate_Click" style="margin-right:5px"/>
            <asp:Button ID="btnView" runat="server" CssClass="btn btn-light p-4" style="width:75px" Text="View" OnClick="btnView_Click" />
           </div>
       </div>
	

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
