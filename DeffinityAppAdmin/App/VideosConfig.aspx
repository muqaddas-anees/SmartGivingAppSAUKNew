<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="VideosConfig.aspx.cs" Inherits="DeffinityAppDev.App.VideosConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin - Videos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    


        <div class="container">
         
            <div class="row">
                <div class="col-lg-12 mx-auto mt-5">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Add New Video</h3>
                        </div>
                        <div class="card-body">


                            <div class="row mb-6">

                                <div class="col-lg-7">
                                     <div class="form-group">
                        <label for="videoTitle">Video Title</label>
                        <asp:TextBox ID="videoTitle" CssClass="form-control" runat="server" placeholder="Enter video title" />
                       
                    </div>
                    <div class="form-group">
                        <label for="videoURL">Video URL</label>
                        <asp:TextBox ID="videoURL" CssClass="form-control" runat="server" placeholder="Enter video URL" />
                        
                    </div>
 <div class="form-group">
                <label for="videoOrder">Order</label>
                <asp:TextBox ID="videoOrder" CssClass="form-control" runat="server" placeholder="Enter order" TextMode="Number" />
               
            </div>
            <div class="form-group">
                <label for="thumbnailUpload">Upload Thumbnail</label>
            <asp:FileUpload ID="thumbnailUpload" runat="server" CssClass="form-control" ClientIDMode="Static" />
            </div>
            <asp:Button ID="saveVideoButton" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="videoForm_ServerSubmit" ClientIDMode="Static" />

                                </div>
                                  <div class="col-lg-5">
                                       <div class="form-group">
                    <label>Video Preview</label>
                    <div class="video-preview" style="border: 1px solid #ddd; padding: 10px;">
                        <img id="videoThumbnail" style="width: 500px; height: 300px;" alt="Video Thumbnail" />
                    </div>
                </div>
                                      </div>
                            </div>

      

                        </div>
                    </div>



    <div class="card mt-5">
        <div class="card-header">
            <h3 class="card-title">Video List</h3>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <table class="table table-row-dashed table-row-gray-300 align-middle gs-0 gy-4">
                    <thead>
                        <tr class="border-0">
                            <th class="p-0">Edit</th>
                            <th class="p-0 min-w-150px">Title</th>
                            <th class="p-0 min-w-200px">URL</th>
                            <th class="p-0 min-w-100px">Order</th>
                            <th class="p-0 min-w-100px text-end">Delete</th>
                        </tr>
                    </thead>
                    <tbody id="videoList" runat="server">
                        <!-- This will be populated dynamically from code-behind -->
                    </tbody>
                </table>
            </div>
        </div>
    </div>

                </div>
            </div>
        </div>
        <!-- Modal for Edit Video -->
<!-- Modal -->
<div class="modal fade" id="modal-7" aria-hidden="true" data-backdrop="false" style="display: none;">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="editVideoLabel">Edit Video Details</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                 <div class="mb-3" style="display:none;visibility:hidden">
                        <label for="editVideoTitle" class="form-label">id</label>
                        <input runat="server" type="text" class="form-control" id="edittextid" name="edittextid"  >
                    </div>
                    <div class="mb-3">
                        <label for="editVideoTitle" class="form-label">Video Title</label>
                        <input runat="server" type="text" class="form-control" id="editVideoTitle" name="editVideoTitle"  placeholder="Enter video title">
                    </div>
                    <div class="mb-3">
                        <label for="editVideoURL" class="form-label">Video URL</label>
                        <input runat="server" type="text" class="form-control" id="editVideoURL" name="editVideoURL"  placeholder="Enter video URL" >
                    </div>
                    <div class="mb-3">
                        <label for="editVideoOrder" class="form-label">Order</label>
<input type="number" runat="server" class="form-control" id="editVideoOrder" name="editVideoOrder" placeholder="Enter video order">
                    </div>
                  <div class="mb-3">
                <label for="thumbnailUpload">Upload Thumbnail</label>
            <asp:FileUpload ID="thumbnailUpload1" runat="server" CssClass="form-control" ClientIDMode="Static" />
            </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <asp:Button ID="saveChangesBtn" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="SaveChanges_Click" ClientIDMode="Static" />
                                            <asp:Button ID="Delete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="Delete_Click" ClientIDMode="Static" />

            </div>
        </div>
    </div>
</div>
    <script>//var hostUrl = "assets/";
            // Function to preview image
            function previewImage() {
                var fileInput = document.getElementById('thumbnailUpload');
                var preview = document.getElementById('videoThumbnail');
                var dummyImagePath = '/assets/dummy.jpg'; // Path to your dummy image

                // Function to update preview image
                var updatePreview = function (imageSrc) {
                    preview.src = imageSrc;
                };

                // Function to handle file input change
                var handleFileInputChange = function () {
                    if (fileInput.files && fileInput.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            updatePreview(e.target.result);
                        };
                        reader.readAsDataURL(fileInput.files[0]);
                    } else {
                        updatePreview(dummyImagePath); // Show dummy image if no file selected
                    }
                };

                // Handle initial page load
                window.onload = function () {
                    updatePreview(dummyImagePath); // Show dummy image on page load
                };

                // Handle file input change event
                fileInput.addEventListener('change', handleFileInputChange);
            }

            // Run previewImage() when the page loads
            previewImage();

        //document.addEventListener('DOMContentLoaded', function () {
        //    var modalElement = document.querySelector("[id$='editUpdateVideo']");
        //    if (modalElement) {
        //        var editVideoModal = new bootstrap.Modal(modalElement);
        //        editVideoModal.show();
        //    } else {
        //        console.log("Element not found");
        //    }
        //});

            // Function to open edit modal with pre-filled values
        function openEditModal(title, url, order, id) {
          //  alert(id);
                // Populate modal fields with data
                //document.getElementById('editVideoTitle').value = title;
                //document.getElementById('editVideoURL').value = url;
                //document.getElementById('editVideoOrder').value = order;
                //edittextid
                document.querySelector("[id$='textid']").value = id;
                document.querySelector("[id$='VideoTitle']").value = title;
                document.querySelector("[id$='VideoURL']").value = url;
                document.querySelector("[id$='VideoOrder']").value = order;

                // Show the modal
               // var editVideoModal = new bootstrap.Modal(document.getElementById('editUpdateVideo'));
                var modalElement = document.querySelector("[id$='modal-7']");
                if (modalElement) {
                    var editVideoModal = new bootstrap.Modal(modalElement);
                    editVideoModal.show();
                } else {
                    console.log("Element not found");
                }
            }

            // Example usage when edit button is clicked
            document.addEventListener('click', function (event) {
                if (event.target.classList.contains('edit-btn')) {
                    // Extract data from the row or from wherever you store it
                    var title = event.target.dataset.title;
                    var url = event.target.dataset.url;
                    var order = event.target.dataset.order;
                    var id = event.target.dataset.id;

                    // Open the edit modal with pre-filled values
                    openEditModal(title, url, order, id);
                }
            });

            // Function to handle form submission and save changes
            document.getElementById('saveChangesBtn').addEventListener('click', function () {
                // Perform AJAX request to save changes
                var formData = new FormData(document.getElementById('editVideoForm'));
                fetch('Video.aspx/UpdateVideo', {
                    method: 'POST',
                    body: formData
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        alert('Video details updated successfully.');
                        var modalElement = document.querySelector("[id$='modal-7']");
                        modalElement.hide(); // Close the modal after successful update
                        BindVideosData(); // Refresh video list
                    })
                    .catch(error => {
                        console.error('There was an error updating the video details:', error);
                        alert('An error occurred while updating the video details.');
                    });
            });




       


        </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
