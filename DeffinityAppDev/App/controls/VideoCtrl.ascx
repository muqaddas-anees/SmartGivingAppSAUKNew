<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.VideoCtrl" %>
 <%--<style>
        .video-container {
            max-width: 100%;
            overflow: hidden;
            position: relative;
            width: 700px; /* Adjust width as needed */
            height: 400px; /* Adjust height as needed */
            margin-bottom: 20px;
        }
        .video-container video {
            width: 100%;
            height: 100%;
            object-fit: cover;
        }
        .steps, .related-videos {
            margin-top: 20px;
        }
        .steps .step, .related-videos .next-video {
            margin-bottom: 20px;
        }
        .related-videos img {
            width: 200px;
            height: 140px;
            margin-bottom: 10px;
        }
        .related-videos .next-video {
            margin-right: 10px;
        }
    </style>--%>

<style>
    .list-group-item {
    border: none;  /* Removes the border */
    margin-bottom: 20px;  /* Adds spacing between list group items */
}

.list-group {
    margin-bottom: 20px;  /* Additional spacing at the bottom of the list group */
}

.video-thumbnail {
    position: relative;
    display: block;
}

.play-icon {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    color: #007bff; /* Change the color to blue */
    font-size: 2rem; /* Adjust the size as necessary */
    z-index: 10;
}

.video-thumbnail:hover .play-icon {
    opacity: 0.8;
}

</style>

 <%--<div class="container mt-3">--%>
      <div class="card">
           <div class="card-header">
               <div class="card-title">
                    <h1 id="videoTitle" class="mb-5"></h1>
               </div>
  <div class="card-toolbar">
            <button type="button" class="btn btn-sm btn-light" style="display:none;visibility:hidden">
                Close
            </button>
        </div>
  </div>
           <div class="card-body">
            <div class="row">
                <!-- Main Video and Related Videos Column -->
                <div class="col-md-7">
                     <div class="container mt-4">
        <div id="video-player" class="mb-3">
            <!-- Main video display area will update here -->
        </div>
        <div class="d-flex justify-content-start d-inline gap-5" style=" overflow-y:scroll;" runat="server" id="videoList" >
                <!-- Generated video thumbnails will appear here -->
            </div>
                         </div>
                </div>
                <!-- Steps and Video Title Column -->
                <div class="col-md-5">
  
    <div class="steps">
        <div class="card">
                    <div class="card-body p-0">
                        <h5 class="card-title fs-3x text-success mx-5">Let's Get You Started</h5>
                        <div class="list-group">
                            <div class="list-group-item mb-10">
                                <h6 class="mb-1 fs-1">Step 1: Create Account</h6>
                                <p class="fs-2">Create a Stripe Account or Connect Your Existing Account to Plegit</p>
                                <asp:Button ID="btnActivate" runat="server" Text="Activate Stripe" OnClick="btnActivate_Click" Cssclass="btn btn-success w-100"></asp:Button>
                            </div>
                            <div class="list-group-item mb-10">
                                <h5 class="mb-1 fs-1">Step 2: Book an Onboarding Call</h5>
                                <p class="fs-2">You'll be notified once your account is active and you can begin fundraising.</p>
                                <a class="btn btn-success w-100 " id="btnstep2" href="https://calendly.com/d/cpgp-hd5-vt5/free-consultation-with-a-charity-champion">Book a Call with a Charity Champion</a>
                            </div>
                        </div>
                    </div>
                </div>


       <%-- <h3>Steps to Follow</h3>--%>
        <asp:Repeater ID="rptSteps" runat="server" Visible="false">
            <ItemTemplate>
                <div class="step">
                    <h1><%# Eval("heading") %></h1>
                    <p><%# Eval("description") %></p>
                    <!-- Example: You can add buttons here -->
                    <div>
                        <button class="btn btn-success">Example Button</button>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

        </div>



           
               </div>
          </div>
          <%--  </div>--%>


    <script src="https://cdn.jsdelivr.net/npm/fslightbox/index.js"></script>
   <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>--%>
    <script>

        function onbtn2() {
           // alert('Redirecting to the consultation page...');
            window.location.href = "https://calendly.com/d/cpgp-hd5-vt5/free-consultation-with-a-charity-champion";
        }



     $(document).ready(function() {
         // Initialize fsLightbox
         new FsLightbox();
    //btnstep2
         //$('#btnstep2').on('click', function (e) {
         //    window.location = "https://calendly.com/d/cpgp-hd5-vt5/free-consultation-with-a-charity-champion";
         //});

    

     // When a thumbnail is clicked
     $('[data-fslightbox="gallery"]').on('click', 'a', function(e) {
         e.preventDefault();
         const videoUrl = $(this).attr('href');
         const t = $(this).attr('data-title');
         //alert(t)
        
         document.getElementById('videoTitle').innerText = t;

     const embedUrl = videoUrl.replace('watch?v=', 'embed/');
     const iframe = `<iframe src="${embedUrl}" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen class="w-100" style="height: 500px;"></iframe>`;
     $('#video-player').html(iframe);
            });

     // Trigger click on the first thumbnail to load it by default
     $('[data-fslightbox="gallery"] a').first().trigger('click');
        });
    </script>
