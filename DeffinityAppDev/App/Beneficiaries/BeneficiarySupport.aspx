<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeneficiarySupport.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.BeneficiarySupport" MasterPageFile="~/App/Beneficiaries/Beneficiaries.master" %>

<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1 class="me-34">Support Received</h1>
        <button id="addDonationButton" class="btn btn-primary" type="button">Add Donation</button>
    </div>

    <div class="card-body py-4">
        <!--begin::Table-->
        <div id="kt_table_donations_wrapper" class="dt-container dt-bootstrap5 dt-empty-footer">
            <div class="table-responsive">
                <table class="table align-middle table-row-dashed fs-6 gy-5 dataTable" id="kt_table_donations" style="width: 100%; text-align: left;">
                    <colgroup>
                        
                        <col style="width: 20%;">
                        <col style="width: 20%;">
                        <col style="width: 20%;">
                        <col style="width: 20%;">
                        <col style="width: 20%;">
                        
                    </colgroup>
                    <thead>
                        <tr class="text-start text-muted fw-bold fs-7 text-uppercase gs-0" role="row">
                            
                            <th>Date</th>
                            <th>Associated Fundraiser</th>
                            <th>Supported Donor</th>
                            <th>Beneficiary Target</th>
                         
                            <th>Notes</th>
                        </tr>
                    </thead>
                    <tbody class="text-gray-600 fw-semibold">
                      <asp:Repeater ID="RepeaterDonations" runat="server">
    <ItemTemplate>
        <tr>
            

            <!-- Display Date formatted -->
            <td class="text-muted fw-semi-bold"><%# Eval("DonationDate", "{0:MM/dd/yyyy}") %></td>
            
            <!-- Display Associated Fundraiser -->
            <td class="text-muted fw-semi-bold"><%# Eval("AssociatedFundraiser") %></td>
            
            <!-- Display Supported Donor -->
            <td class="text-muted fw-semi-bold"><%# Eval("DonatedBy") %></td>
            
            <!-- Display Donation Amount + Currency -->
            <td class="text-muted fw-semi-bold"><%# Eval("DonationAmount") %> <%# Eval("Currency") %></td>
            
            <!-- Empty Progress to Date -->
         
            
            <!-- Display Notes -->
            <td class="text-muted fw-semi-bold"><%# Eval("Notes") %></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>



                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <!-- Modal Structure for Adding Donation -->

<div class="modal fade" id="addDonationModal" tabindex="-1" aria-labelledby="addDonationModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="addDonationModalLabel">Add Donation</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <!-- Form fields to add donation -->
                
                <!-- Donation Date -->
                <div class="row mb-3">
                    <div class="col-12">
                        <label for="txtDonationDate" class="form-label">Donation Date</label>
                        <asp:TextBox ID="txtDonationDate" runat="server" CssClass="form-control" TextMode="Date" placeholder="Donated Date" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDonationDate" ErrorMessage="Date is Required" CssClass="text-bg-danger text-white"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <!-- Logged By and Associated Fundraiser -->
                <div class="row mb-3">
                    <div class="col-6">
                        <label for="ddlLoggedBy" class="form-label">Logged By</label>
                          <asp:DropDownList ID="ddlLogged" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="false">
      <asp:ListItem Text="Select a person" Value=""></asp:ListItem>

  </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLoggedBy" runat="server" ControlToValidate="ddlLogged" InitialValue="" ErrorMessage="Please select a person." CssClass="text-danger" Display="Dynamic" ValidationGroup="DonationGroup" />

                    </div>
                    <div class="col-6">
                        <label for="ddlAssociated" class="form-label">Associated Fundraiser</label>
                        <asp:DropDownList ID="ddlAssociatedFundraise" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="false">
    <asp:ListItem Text="Select a Fundraiser" Value=""></asp:ListItem>
</asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvAssociatedFundraise" runat="server" ControlToValidate="ddlAssociatedFundraise" InitialValue="" ErrorMessage="Please select a fundraiser." CssClass="text-danger" Display="Dynamic" ValidationGroup="DonationGroup" />

                    </div>
                </div>

                <!-- Currency and Donation Amount -->
                <div class="row mb-3">
                    <div class="col-6">
                        <label for="ddlCurrency" class="form-label">Currency</label>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-select">
                            <asp:ListItem Text="United States Dollar (&#36;)" Value="&#36;"></asp:ListItem> 
                            <asp:ListItem Text="British Pound (&#163;)" Value="&#163;"></asp:ListItem> 
                            <asp:ListItem Text="Indian Rupee (&#8377;)" Value="&#8377;"></asp:ListItem> 
                            <asp:ListItem Text="Pakistani Rupee (&#8360;)" Value="&#8360;"></asp:ListItem> 
                            <asp:ListItem Text="Chinese Yuan (&#165;)" Value="&#165;"></asp:ListItem> 
                            <asp:ListItem Text="Japanese Yen (&#165;)" Value="&#165;"></asp:ListItem> 
                            <asp:ListItem Text="Canadian Dollar (&#36;)" Value="&#36;"></asp:ListItem> 
                            <asp:ListItem Text="Australian Dollar (&#36;)" Value="&#36;"></asp:ListItem> 
                            <asp:ListItem Text="Swiss Franc (&#8355;)" Value="&#8355;"></asp:ListItem> 
                            <asp:ListItem Text="Russian Ruble (&#8381;)" Value="&#8381;"></asp:ListItem> 
                            <asp:ListItem Text="Saudi Riyal (&#xFDFC;)" Value="&#xFDFC;"></asp:ListItem> 
                            <asp:ListItem Text="South African Rand (&#82;)" Value="&#82;"></asp:ListItem> 
                            <asp:ListItem Text="Turkish Lira (&#8378;)" Value="&#8378;"></asp:ListItem> 
                            <asp:ListItem Text="Swedish Krona (&#107;&#114;)" Value="&#107;&#114;"></asp:ListItem> 
                        </asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <label for="txtDonationAmount" class="form-label">Donation Amount</label>
                        <asp:TextBox ID="txtDonationAmount" runat="server" CssClass="form-control" TextMode="Number" placeholder="Donation Amount" oninput="validatePositiveNumber(this)" min="0" />
                        <span id="donationAmountError" class="text-white text-bg-danger" style="display:none;">Please enter a positive number.</span>
                        <asp:RequiredFieldValidator ID="rfvDonationAmount" runat="server" ControlToValidate="txtDonationAmount" ErrorMessage="Donation amount is required." CssClass="text-danger" Display="Dynamic" EnableClientScript="true" ValidationGroup="DonationGroup" />

                    </div>
                </div>

                <!-- Payment Type -->
    <div class="row mb-3">
        <div class="col-12 d-flex flex-wrap gap-3">
            <div class="form-check">
                <asp:RadioButton ID="rbOneOff" runat="server" GroupName="PaymentType" CssClass="form-check-input" />
                <label class="form-check-label ms-2" for="rbOneOff">One Off Payment</label>
            </div>

            <div class="form-check">
                <asp:RadioButton ID="rbMonthly" runat="server" GroupName="PaymentType" CssClass="form-check-input" />
                <label class="form-check-label ms-2" for="rbMonthly">Monthly</label>
            </div>

            <div class="form-check">
                <asp:RadioButton ID="rbAnnual" runat="server" GroupName="PaymentType" CssClass="form-check-input" />
                <label class="form-check-label ms-2" for="rbAnnual">Annual</label>
            </div>

            
        </div>
    </div>



                <!-- Donated By and Notes -->
                <div class="row mb-3">
                    <div class="col-6">
                        <label for="ddlDonatedBy" class="form-label">Donated By</label>
                       <asp:DropDownList ID="ddlDonated" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" AutoPostBack="false">
                           <asp:ListItem Text="Select a person" Value=""></asp:ListItem>
                           </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDonatedBy" runat="server" ControlToValidate="ddlDonated" InitialValue="" ErrorMessage="Please select a donor." CssClass="text-danger" Display="Dynamic" ValidationGroup="DonationGroup" />

                    </div>
                    <div class="col-6">
                        <label for="txtDonationNotes" class="form-label">Notes</label>
                        <asp:TextBox ID="txtDonationNotes" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Notes" />
                    </div>
                </div>

                <!-- File Upload -->
                <div class="row mb-3">
                    <div class="col-12">
                        <label for="FileUploadDocuments" class="form-label">Upload Document</label>
                        <asp:FileUpload ID="FileUploadDocuments" runat="server" CssClass="form-control" onchange="showImagePreview(this);" />
                    </div>
                </div>

                <!-- Image Preview Container -->
                <div class="row">
                    <div class="col-12">
                        <div id="imagePreviewContainer" class="mt-3">
                            <!-- Image preview will be displayed here -->
                        </div>
                    </div>
                </div>

                <!-- Display Message -->
                

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <asp:Button runat="server" ID="btnSaveDonation" class="btn btn-primary" type="button" Text="Save" OnClick="btnSaveDonation_Click"></asp:Button>
            </div>
        </div>
    </div>
</div>
          <div class="modal fade" Id="MysuccessModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="successModalLabel">Success</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
  <asp:Label ID="lblMessage" runat="server" CssClass=""></asp:Label>
</div>
                        <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
                 <script type="text/javascript">
                     var hostUrl = "/assets/";
                 </script>
<script type="text/javascript" src="/assets/plugins/global/plugins.bundle.js"></script>
<script type="text/javascript" src="/assets/js/scripts.bundle.js"></script>
<script>
    function showSuccessModal() {

        var successModal = new bootstrap.Modal(document.getElementById('MysuccessModal'));
        successModal.show();
    }

    function validatePositiveNumber(input) {
        const errorMessage = document.getElementById("donationAmountError");

        if (input.value === "" || parseFloat(input.value) <= 0) {
            errorMessage.style.display = "block";  // Show the error message
            input.classList.add("is-invalid");     // Highlight the input field with an error class
        } else {
            errorMessage.style.display = "none";   // Hide the error message if the input is valid
            input.classList.remove("is-invalid");  // Remove the error class if the input is valid
        }
    }
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



    $(document).ready(function () {
        $('#addDonationButton').on('click', function () {
            $('#addDonationModal').modal('show');
        });
    });


    let uploadedFiles = [];
    const maxFiles = 2;

    function uploadFiles() {
        const fileInput = document.getElementById('<%= FileUploadDocuments.ClientID %>');
        const fileFeedback = document.getElementById('fileUploadFeedback');

        // Check if more files are uploaded than allowed
        if (fileInput.files.length > maxFiles - uploadedFiles.length) {
            alert(`You can only upload up to ${maxFiles} files.`);
            return;
        }

        // Add files to the array and display them
        Array.from(fileInput.files).forEach(file => {
            if (uploadedFiles.length < maxFiles) {
                uploadedFiles.push(file.name); // Add file name to array
            }
        });

        // Clear the file input to allow re-selection of the same file
        fileInput.value = '';

        // Update file feedback to show list of uploaded files
        updateFileFeedback();
    }

    function updateFileFeedback() {
        const fileFeedback = document.getElementById('fileUploadFeedback');
        fileFeedback.innerHTML = ''; // Clear previous feedback

        // Show uploaded files and remove buttons
        uploadedFiles.forEach((file, index) => {
            fileFeedback.innerHTML += `
        <div class="file-entry">
            ${file} 
            <button type="button" class="btn btn-sm btn-danger ms-2" onclick="removeFile(${index})">Remove</button>
        </div>`;
        });
    }

    function removeFile(index) {
        // Remove file from array and update feedback
        uploadedFiles.splice(index, 1);
        updateFileFeedback();
    }
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
</script>

<style>
    .modal-dialog-centered {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .form-control {
        border-radius: 5px;
        padding: 10px;
    }

    .modal-content {
        padding: 20px;
    }

    .text-danger {
        font-size: 12px;
        margin-top: 5px;
    }

    .gap-4 {
        gap: 1rem; /* Adds spacing between radio buttons */
    }

    .radio-label {
        display: flex;
        align-items: center;
        margin-right: 20px;
        cursor: pointer;
    }

    .file-entry {
        margin-top: 5px;
        display: flex;
        align-items: center;
    }

    .btn-danger {
        border-radius: 5px;
        padding: 5px 10px;
    }
</style>


</asp:Content>
