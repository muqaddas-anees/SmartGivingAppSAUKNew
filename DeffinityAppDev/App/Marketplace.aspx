<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Marketplace.aspx.cs" Inherits="DeffinityAppDev.Marketplace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <asp:Label ID="lblPagetitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">



<style>
        .modal-dialog.modal-xl {
        max-width: 90%; /* Adjust the width to 90% of the viewport width */
    }
    
    /* Set a minimum height for the modal */
    .modal-body {
        min-height: 80vh; /* Make sure the modal body takes at least 80% of the viewport height */
    }

    /* Make the iframe take up the full height of the modal body */
    #videoIframe {
        width: 100%;
        height: 100%;
    }
 .pnlheight{
	 min-height:400px;
 }
 h2.page-header{
     font-size:21px;
 }
  .card-body {
     height: 240px; /* Adjust card body height */
 }

</style>
	  <div class="card mb-5 mb-xl-10">
        <div class="card-body pt-9 pb-0" style="height:83px">
            <!--begin::Navs-->
            <div class="d-flex overflow-auto h-55px">
                <ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
                    <!--begin::Nav item-->
                    <li class="nav-item">
                        <a class="nav-link text-active-primary me-6 active" id="buy-service-tab" data-bs-toggle="tab" href="#buyService" role="tab" aria-controls="buyService" aria-selected="true">Buy a Service</a>
                    </li>
                    <!--begin::Nav item-->
                    <li class="nav-item">
                        <a class="nav-link text-active-primary me-6" id="bought-services-tab" data-bs-toggle="tab" href="#boughtServices" role="tab" aria-controls="boughtServices" aria-selected="false">Bought Services</a>
                    </li>
                </ul>
            </div>
            <!--begin::Navs-->
        </div>
          </div>
      <div class="tab-content mt-3">
        <!-- Buy a Service Tab -->
        <div class="tab-pane fade show active" id="buyService" role="tabpanel" aria-labelledby="buy-service-tab">
      
	<asp:ListView ID="lvCards" runat="server" OnItemCommand="lvCards_ItemCommand1">
    <LayoutTemplate>
        <div class="row d-flex flex-wrap">
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div class="col-md-4 d-flex">
            <!-- Make the card container a flex column to push footer to the bottom -->
            <div class="card card-bordered mb-6 h-100 w-100 d-flex flex-column">
                <!--begin::Publications post-->
                <div class="card-xl-stretch me-md-6">
                    <!--begin::Overlay-->
                    <a class="d-block overlay mb-4" style="margin-left:20px;margin-top:20px" data-fslightbox="lightbox-hot-sales" href='<%# Eval("VideoLink") %>'>
                        <!--begin::Image-->
                        <div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded" 
                             style='background-image:url(<%# Eval("ImageUrl") %>);min-height:278px;'></div>
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
                        <a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base">
                            <%# Eval("Title") %>
                        </a>
                        <!--end::Title-->
                        <!--begin::Text-->
                        <div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5" style="min-height:140px">
                            <%# Eval("Description") %>
                        </div>
                        <!--end::Text-->
                    </div>
                    <!--end::Body-->
                </div>
                <!--begin::Footer-->
                <!-- Add mt-auto to push the footer to the bottom and add 10px margin from the bottom -->
                <div class="card-footer d-flex justify-content-between mt-auto" style="margin-bottom: 10px;">
                    <a class="btn btn-primary" style="padding:10px 40px" onclick="showModalwithVideo('<%# Eval("VideoLink") %>')">
                        <%# Eval("videoText") %>
                    </a>

                    <!-- Replace buy now link with button -->
                    <asp:LinkButton ID="btnBuyNow" runat="server" 
                                CommandName="BuyNow" CommandArgument='<%# Eval("Id") %>' 
                                CssClass="btn btn-primary"><%# Eval("buyText") %></asp:LinkButton>
                </div>
                <!--end::Footer-->
            </div>
        </div>
    </ItemTemplate>
</asp:ListView>


            </div>
                      <div class="tab-pane fade" id="boughtServices" role="tabpanel" aria-labelledby="bought-services-tab">
                	<asp:ListView ID="lv_BoughtCards" runat="server">
    <LayoutTemplate>
        <div class="row d-flex flex-wrap">
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div class="col-md-4 d-flex">
            <!-- Make the card container a flex column to push footer to the bottom -->
            <div class="card card-bordered mb-6 h-100 w-100 d-flex flex-column">
                <!--begin::Publications post-->
                <div class="card-xl-stretch me-md-6">
                    <!--begin::Overlay-->
                    <a class="d-block overlay mb-4" style="margin-left:20px;margin-top:20px" data-fslightbox="lightbox-hot-sales" href='<%# Eval("VideoLink") %>'>
                        <!--begin::Image-->
                        <div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded" 
                             style='background-image:url(<%# Eval("ImageUrl") %>);min-height:278px;'></div>
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
                        <a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base">
                            <%# Eval("Title") %>
                        </a>
                        <!--end::Title-->
                        <!--begin::Text-->
                        <div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5" style="min-height:140px">
                            <%# Eval("Description") %>
                        </div>
                        <!--end::Text-->
                    </div>
                    <!--end::Body-->
                </div>
                <!--begin::Footer-->
                <!-- Add mt-auto to push the footer to the bottom and add 10px margin from the bottom -->
                <div class="card-footer d-flex justify-content-between mt-auto" style="margin-bottom: 10px;">
                    
                </div>
                <!--end::Footer-->
            </div>
        </div>
    </ItemTemplate>
</asp:ListView>


                </div>


      </div>
     
















    <!-- Modal -->
<!-- Modal -->
<div class="modal fade" id="videoModal" tabindex="-1" aria-labelledby="videoModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl"> <!-- Use modal-xl for a larger modal -->
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="videoModalLabel">YouTube Video</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <!-- YouTube video will be embedded here -->
        <div class="ratio ratio-16x9">
          <iframe id="videoIframe" class="embed-responsive-item" src="" allowfullscreen></iframe>
        </div>
      </div>
    </div>
  </div>
</div>

























    <script>
        function showModalwithVideo(videoLink) {
            // Extract the video ID from the YouTube link
            let videoId;

            // Check if the link is a regular YouTube link (e.g., https://www.youtube.com/watch?v=VIDEO_ID)
            if (videoLink.includes("youtube.com")) {
                let urlParams = new URLSearchParams(new URL(videoLink).search);
                videoId = urlParams.get('v'); // Get the 'v' parameter (video ID)
            }
            // Check if the link is a shortened YouTube link (e.g., https://youtu.be/VIDEO_ID)
            else if (videoLink.includes("youtu.be")) {
                videoId = videoLink.split("youtu.be/")[1]; // Extract the ID from the youtu.be link
            }

            // Convert the video ID to the embed link format
            let embedLink = `https://www.youtube.com/embed/${videoId}`;

            // Get the iframe element in the modal
            var iframe = document.getElementById("videoIframe");

            // Set the iframe source to the converted embed link
            iframe.src = embedLink;

            // Show the modal using Bootstrap's modal method
            var videoModal = new bootstrap.Modal(document.getElementById("videoModal"));
            videoModal.show();
        }

        // Clear the iframe source when the modal is closed to stop the video
        document.getElementById('videoModal').addEventListener('hidden.bs.modal', function (event) {
            var iframe = document.getElementById("videoIframe");
            iframe.src = "";
        });
        function showModalwithVideo(videoLink) {
            // Extract the video ID from the YouTube link
            let videoId;

            // Check if the link is a regular YouTube link (e.g., https://www.youtube.com/watch?v=VIDEO_ID)
            if (videoLink.includes("youtube.com")) {
                let urlParams = new URLSearchParams(new URL(videoLink).search);
                videoId = urlParams.get('v'); // Get the 'v' parameter (video ID)
            }
            // Check if the link is a shortened YouTube link (e.g., https://youtu.be/VIDEO_ID)
            else if (videoLink.includes("youtu.be")) {
                videoId = videoLink.split("youtu.be/")[1]; // Extract the ID from the youtu.be link
            }

            // Convert the video ID to the embed link format
            let embedLink = `https://www.youtube.com/embed/${videoId}`;

            // Get the iframe element in the modal
            var iframe = document.getElementById("videoIframe");

            // Set the iframe source to the converted embed link
            iframe.src = embedLink;

            // Show the modal using Bootstrap's modal method
            var videoModal = new bootstrap.Modal(document.getElementById("videoModal"));
            videoModal.show();
        }

        // Clear the iframe source when the modal is closed to stop the video
        document.getElementById('videoModal').addEventListener('hidden.bs.modal', function (event) {
            var iframe = document.getElementById("videoIframe");
            iframe.src = "";
        });



    </script>

    </asp:Content>