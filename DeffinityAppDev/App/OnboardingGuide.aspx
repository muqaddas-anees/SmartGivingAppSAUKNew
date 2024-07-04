<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="OnboardingGuide.aspx.cs" Inherits="DeffinityAppDev.App.OnboardingGuide" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function myFunction() {
            /* Get the text field */
            var copyText = document.getElementById("MainContent_myInput");

            /* Select the text field */
            copyText.select();
            copyText.setSelectionRange(0, 99999); /* For mobile devices */

            /* Copy the text inside the text field */
            navigator.clipboard.writeText(copyText.value);

            /* Alert the copied text */
            //alert("Copied the text: " + copyText.value);
            
            return false;
        }
    </script>
    <input type="text" value="" id="myInput" runat="server" style="visibility:hidden;"/>
    <div class="row mb-5">
           <div class="col-lg-12">
                 <div class="card ">
                        <!--begin::Header-->
                        <div class="card-header py-5 d-flex justify-content-center">
                            <!--begin::Title-->
                            <h3 class="card-title">
                                <span class="card-label fw-bolder text-dark text-center" style="font-size:35px">	<asp:Literal ID="lit_html" runat="server" Text="Getting Started"></asp:Literal></span>
                               
                            </h3>
                         
                        </div>
                   <div class="card-body">
                       

					   <div class="row mb-6">
						   <div class="col-lg-5  mx-15 px-15">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Step 1: Brand The Portal</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-300px"
    style="background-image:url('../../assets/images/AddingYourLogoToThePortal.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo1"
    href="#vimeo1">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="link_brand" runat="server" NavigateUrl="~/WF/CustomerAdmin/PortalBranding.aspx?type=PortalBranding" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
                           <div class="col-lg-5  mx-15 px-15">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Step 2: Activate Payment Processing</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-300px"
    style="background-image:url('../../assets/images/PaymentIntegrationSettings.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo2"
                                
    href="#vimeo2">
   
  <%--  <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>--%>
   
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/App/FLSDefault.aspx?tab=fls&type=paymentsettings&back=~/App/Settings.aspx&pnl=Integration" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                        
                                         </div>
                                  <%--  <div class=" d-flex justify-content-center my-5">
                                           <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="https://stripe.com/gb/payments" Text="New To Stripe? - Create an Account" CssClass="btn btn-primary"></asp:HyperLink>
                                        </div>--%>
                                  <%--  <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/WF/DC/FLSDefault.aspx?tab=fls&type=paymentsettings" Text="Already a Stripe Customer? - Let's Configure" CssClass="btn btn-primary"></asp:HyperLink>
                                        </div>--%>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
					   </div>

                          <div class="row mb-6">
						   <div class="col-lg-5  mx-15 px-15">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Step 3: Add Team Members</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-300px"
    style="background-image:url('../../assets/images/AddingTeamMembers.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo3"
    href="#vimeo3">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/App/Members.aspx?type=members" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
                           <div class="col-lg-5  mx-15 px-15">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Step 4: Customise Thank You Emails</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-300px"
    style="background-image:url('../../assets/images/CustomizingDonationThankYouEmails.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo4"
    href="#vimeo4">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/App/ThankYouMailSettings.aspx?type=active&back=~/App/Settings.aspx&pnl=tithing" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
					   </div>
                       <div class="row mt-10 mb-5">
                            <div class=" d-flex justify-content-center">
                                       
																<span class="fs-2  fw-bold d-block mb-5"> Congratulations, now that the core platform is ready, you can start taking donations. You now have a choice of the following:</span><br />
																
															</div>
                           </div>
   <div class="row mb-6 d-flex justify-content-center">
						   <div class="col-lg-3  mx-5 px-5">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Share My Landing Page</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-200px"
    style="background-image:url('../../assets/images/PageBuilder.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo5"
    href="#vimeo5">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="linkLanding" runat="server" NavigateUrl="~/Web/plegittest3" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="#" Text="Copy URL" CssClass="btn btn-primary" onclick="return myFunction();"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
                           <div class="col-lg-3  mx-5 px-5">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5">Create a Fundraiser</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-200px"
    style="background-image:url('../../assets/images/Fundraisers.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo6"
    href="#vimeo6">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/App/FundraiserListView.aspx" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
                           <div class="col-lg-3  mx-5 px-5">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Create an Event</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-200px"
    style="background-image:url('../../assets/images/EventsManagement.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo7"
     href="#vimeo7">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>
                                      
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/App/Events/EventList.aspx" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
					   </div>

                       <div class="row mb-6 d-flex justify-content-center">
						   <div class="col-lg-3  mx-5 px-5">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5">  Making a donation via donation dashboard</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-200px"
    style="background-image:url('../../assets/images/Donation Dashboard.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo5"
    href="#vimeoDashbord">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/App/Dashboard.aspx" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                    <%-- <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="#" Text="Copy URL" CssClass="btn btn-primary" onclick="return myFunction();"></asp:HyperLink>
                                         </div>--%>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
                           <div class="col-lg-3  mx-5 px-5">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> In Kind Donations</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-200px"
    style="background-image:url('../../assets/images/InKindDonations.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo6"
    href="#vimeoInKind">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>

                                           <%--<a id="link_brand" class="btn btn-video" style="background-color:#78A607;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo"  >
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                 --%>
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/App/OtherDonationList.aspx?type=inkind" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
                           <div class="col-lg-3  mx-5 px-5">

							      <div class="card card-flush shadow-sm">
                               
                                <div class="card-body">
                                    <div class=" d-flex justify-content-center">
                                       
																<span class="fs-1  fw-bold text-primary d-block mb-5"> Cash Donations</span><br />
																
															</div>
                                    
                                  
                                     <div class="row">
                                                             
                               <a class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-200px"
    style="background-image:url('../../assets/images/CashDonations.png')"
    data-class="d-block"
    data-fslightbox="lightbox-vimeo7"
     href="#vimeoCash">
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>
                                      
                                             
                                         </div>
                                     <div class=" d-flex justify-content-center my-5">
                                         <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/App/OtherDonationList.aspx?type=cash" Text="Take Me There" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
                                </div>
                                <!--end::Card body-->
                            </div>
						   </div>
					   </div>

                         <div class=" d-flex justify-content-center my-15">
                                         <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="https://calendly.com/d/g39-yhg-n5k/30-min-onboarding-call" Text="Need Onboarding Support? Click here to Schedule a Call" CssClass="btn btn-primary"></asp:HyperLink>
                                         </div>
          
                       </div>
                  </div>
            </div>


								 </div>



     <iframe id="vimeo1" style="display:none" src="https://player.vimeo.com/video/820469971?h=5ccc332cc3" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <iframe id="vimeo2" style="display:none" src="https://player.vimeo.com/video/773765036?h=14ad471b2b" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <iframe id="vimeo3" style="display:none" src="https://player.vimeo.com/video/820469208?h=724598fbcf" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <iframe id="vimeo4" style="display:none" src="https://player.vimeo.com/video/773365031?h=70181f3e39" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <iframe id="vimeo5" style="display:none" src="https://player.vimeo.com/video/820573975?h=f0526679b7" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

      <iframe id="vimeo6" style="display:none" src="https://player.vimeo.com/video/823122038?h=3badc39413" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <iframe id="vimeo7" style="display:none" src="https://player.vimeo.com/video/776144934?h=210370eaa3" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
     <iframe id="vimeoDashbord" style="display:none" src="https://player.vimeo.com/video/823839303?h=fca285dd7e" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

      <iframe id="vimeoInKind" style="display:none" src="https://player.vimeo.com/video/821001709?h=2993828955" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <iframe id="vimeoCash" style="display:none" src="https://player.vimeo.com/video/820980062?h=37d47f7bbe" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

    <script type="text/javascript">

       

        var page = document.getElementById("vimeo");

            page.addEventListener("load", displayMessage)

            function displayMessage() {

               // alert("Page Loaded Successfully!");

              //  loadIframe('vimeo', 'https://player.vimeo.com/video/773764751?h=ef4223761e');

}

    
   
        function setvideo(d) {
            alert('brand');
            if (d == "brand") {
                alert('dd');
                document.getElementById('iframeid').src = "https://player.vimeo.com/video/773765491?h=d01a6c69e3";
               
            }

        }
      
        function loadIframe(iframeName, url) {
            var $iframe = $('#' + iframeName);
            console.log("ready!");
            if ($iframe.length) {
                $iframe.attr('src', url);
               // console.log("ready!");// here you can change src
                return false;
            }
            return true;
        }
        $(document).ready(function () {
           
            function setvideo(d) {
                alert('brand');
                if (d == "brand") {
                   
                }

            }
            function loadIframe(iframeName, url) {
                var $iframe = $('#' + iframeName);
                console.log("ready!");
                if ($iframe.length) {
                    $iframe.attr('src', url);
                    $("#link_brand").attr("href", "#vimeo");
                    console.log("ready!");// here you can change src
                    return false;
                }
                return true;
            }

        });

    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">

   
</asp:Content>
