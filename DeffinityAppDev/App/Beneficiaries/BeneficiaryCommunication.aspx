<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="BeneficiaryCommunication.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.BeneficiaryCommunication" MasterPageFile="~/App/Beneficiaries/Beneficiaries.master" %>
<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="d-flex justify-content-between align-items-center mb-4">
        <h1>Beneficiary Feedback</h1>
        <button id="addCommunicationButton" class="btn btn-primary" type="button">Add Communication</button>
         
    </div>

   <div class="card-body py-4">
    <div class="table-responsive">
        <table class="table align-middle table-row-dashed fs-6 gy-5" id="feedbackTable" style="width: 100%; text-align: left;">
            <colgroup>
                <col style="width: 15%;">
                <col style="width: 50%;">
                <col style="width: 20%;">
                <col style="width: 15%;">
            </colgroup>
            <thead>
                <tr class="text-start text-muted fw-bold fs-7 text-uppercase gs-0" role="row">
                    <th>Date</th>
                    <th>Beneficiary Feedback</th>
                    <th>Attachments</th>
                    <th>Delete</th>
                </tr>
            </thead>
            <tbody class="text-gray-600 fw-semibold">
                <asp:Repeater ID="RepeaterFeedback" runat="server" OnItemCommand="RepeaterFeedback_ItemCommand">
    <ItemTemplate>
        <tr>
            <td><%# Convert.ToDateTime(Eval("FeedbackDate")).ToString("MM/dd/yyyy") %></td>
            <td><%# Eval("FeedbackText") %></td>
                 
<td>
    <%# Eval("Attachments") != DBNull.Value && Eval("Attachments") != null 
        ? "<a href='/App/Beneficiaries/BeneficiaryCommunication.aspx?beneficiaryId=" + Eval("FeedbackID") + "'>Has Attachments</a>" 
        : "No Attachments" %>
</td>

<td>
    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("FeedbackID") %>' CssClass="btnDelete">
        <i class="fas fa-trash-alt" style="color: red;"></i> <!-- Change color as needed -->
    </asp:LinkButton>
</td>

        </tr>
    </ItemTemplate>
</asp:Repeater>

            </tbody>
            </table>
    </div>
</div>


    <div class="modal fade" id="addCommunicationModal" tabindex="-1" aria-labelledby="addCommunicationModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="addCommunicationModalLabel">Add Communication</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <!-- Date Picker -->
                    <div class="row mb-3">
                        <div class="col-12">
                            <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox ID="txtCommunicationDate" runat="server" CssClass="form-control bg-transparent" TextMode="Date" />
                          
                            </div>
                        </div>
                    </div>

                <!-- Rich Text Editor -->
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Label ID="lblFeedback" runat="server" Text="Beneficiary Feedback"></asp:Label>
                        <asp:TextBox ID="txtCommunicationText" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>
                    </div>
                </div>

                <!-- File Upload with Add/Remove functionality -->
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Label ID="lblUpload" runat="server" Text="Upload file(s)"></asp:Label>
                        <asp:FileUpload ID="fileUploadDocuments" runat="server" CssClass="form-control" onchange="showImagePreview(this);" />
                                                        <div id="imagePreviewContainer" class="mt-3">
                                    <!-- Image preview will be displayed here -->
                                </div>

                    </div>
                </div>
                

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="btnSaveCommunication" CssClass="btn btn-primary" Text="Save" OnClick="btnSaveCommunication_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
    <!-- Modal to display attachment -->
<div class="modal fade" id="attachmentModal" tabindex="-1" aria-labelledby="attachmentModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="attachmentModalLabel">Attachment</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body" id="attachmentContent">
        <!-- The image or content will be loaded here dynamically -->
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
    <!-- Success Modal -->
        <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content">
              <div class="modal-header">
                  <h5 class="modal-title" id="successModalLabel">Success</h5>
                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
           
  </div>
               <asp:Label ID="lblErrorMessage" CssClass="align-content-center text-center" runat="server"></asp:Label>
              <asp:Label ID="lblMessage" runat="server"></asp:Label>
                          <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              </div>
          </div>
      </div>
  </div>
             <script>
                 var hostUrl = "/metronic8/demo1/assets/";
             </script>
 <script src="/metronic8/demo1/assets/plugins/global/plugins.bundle.js"></script>
 <script src="/metronic8/demo1/assets/js/scripts.bundle.js"></script>

    <script type="text/javascript">
        function showImagePreview(input) {
            var previewContainer = document.getElementById('imagePreviewContainer');
            previewContainer.innerHTML = ''; // Clear previous content

            if (input.files && input.files[0]) {
                var file = input.files[0];
                var fileExtension = file.name.split('.').pop().toLowerCase();
                var allowedExtensions = ['jpg', 'jpeg', 'png', 'gif'];

                if (allowedExtensions.includes(fileExtension)) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        var img = document.createElement('img');
                        img.src = e.target.result;
                        img.alt = 'Image Preview';
                        img.style.maxWidth = '200px';
                        img.style.maxHeight = '200px';
                        previewContainer.appendChild(img);
                    };

                    reader.readAsDataURL(file);
                } else {
                    var message = document.createElement('p');
                    message.textContent = 'Selected file: ' + file.name;
                    previewContainer.appendChild(message);
                }
            }
        }

        function showSuccessModal() {
            // Debugging alert
            var successModal = new bootstrap.Modal(document.getElementById('successModal'));
           
            successModal.show();
        }

     
     


        // When the "Add Communication" button is clicked, show the modal
        document.getElementById("addCommunicationButton").addEventListener("click", function () {
            // Use Bootstrap's modal method to show the modal
            var myModal = new bootstrap.Modal(document.getElementById("addCommunicationModal"));
            myModal.show();

        });

    </script>
    </asp:Content>