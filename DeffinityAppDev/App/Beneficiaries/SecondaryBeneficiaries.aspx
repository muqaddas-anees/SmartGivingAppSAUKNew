

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecondaryBeneficiaries.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.SecondaryBeneficiaries" MasterPageFile="~/App/Beneficiaries/Beneficiaries.master" %>

<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <style>
    .pencil-button {
        background: none; /* Remove background */
        border: none; /* Remove border */
        padding: 0; /* Remove padding */
        cursor: pointer; /* Change cursor to pointer */
        display: inline-flex; /* Use flexbox for alignment */
        align-items: center;
        color: #421474;
    }
        input[type=number]::-webkit-inner-spin-button, 
    input[type=number]::-webkit-outer-spin-button { 
        -webkit-appearance: none; 
        margin: 0; 
    }

    input[type=number] {
        -moz-appearance: textfield;
    }
</style>

    <asp:HiddenField ID="hfBeneficiaryID" runat="server" />

    <!-- Container for button and table -->
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1 class="me-34"> 
      
        </h1>
        <asp:Label runat="server" ID="lblMessageMe"></asp:Label>
        <button id="addSecondaryBeneficiaryButton" class="btn btn-primary" type="button" onclick="clearForm()">Add Secondary Beneficiary</button>
    </div>
    
   <div class="card-body py-4">
    <div id="kt_table_users_wrapper" class="dt-container dt-bootstrap5 dt-empty-footer">
        <div class="table-responsive">
            <table class="table align-middle table-row-dashed fs-6 gy-5 dataTable" id="kt_table_users" style="width: 100%; text-align: left;">
                <colgroup>
                    <col style="width: 10%;">
                    <col style="width: 10%;">
                    <col style="width: 15%;">
                    <col style="width: 25%;">
                    <col style="width: 20%;">
                    <col style="width: 15%;">
                    <col style="width: 15%;">
                  
                </colgroup>
                <thead>
                    <tr class="text-start text-muted fw-bold fs-7 text-uppercase gs-0" role="row">
                        <th>Image</th>
                        <th>Gender</th>
                        <th>First Name</th>
                        <th>Email Address</th>
                        <th>Phone Number</th>
                        <th>Internal ID Number</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody class="text-gray-600 fw-semibold">
           <asp:HiddenField ID="hfShowModal" runat="server" Value="false" ClientIDMode="Static" />

                <asp:Repeater ID="rptSecondaryBeneficiaries" runat="server">
    <ItemTemplate>
        <tr>
           <td>
                <img src='<%# Eval("ProfileImageBase64") %>'  
                     alt="Image" style="width: 50px; border-radius: 50%; height: auto;" />
            </td>
            <td><%# Eval("Gender") %></td>
            <td><%# Eval("Name") %></td>
            <td><%# Eval("Email") %></td>
            <td><%# Eval("PhoneNumber") %></td>
            <td><%# Eval("InternalIDNumber") %></td>
            <td class="text-end">
                     <a href="#" class="btn btn-light btn-active-light-primary btn-flex btn-center btn-sm" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
         Actions
         <i class="ki-duotone ki-down fs-5 ms-1"></i>
     </a>
     <!--begin::Menu-->
     <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-125px py-4" data-kt-menu="true">
         <div class="menu-item px-3">
             <a   onclick="editBeneficiary('<%# Eval("SecondaryBeneficiaryID") %>');" class="menu-link px-3">Edit</a>
         </div>
         
     </div>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>



                </tbody>
            </table>
        </div>
    </div>
</div>

    
   <div class="modal fade" id="FormModal" ClientIDMode="Static" tabindex="-1" aria-labelledby="FormModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
          
            <div class="modal-header">
                <h5 class="modal-title" id="addBeneficiaryModalLabel">Add Secondary Beneficiary</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>

            <div class="modal-body">
          
              
                    <div class="row">
                       
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="ddlTypeModal">Type</label>
                                <asp:DropDownList ID="ddlTypeModal" runat="server" CssClass="form-select form-select-lg">
                                    <asp:ListItem Text="Select Type" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Institute" Value="Institute"></asp:ListItem>
                                    <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTypeModal" runat="server" ControlToValidate="ddlTypeModal" InitialValue="" ErrorMessage="Please select a Type" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                  
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtDOBModal">Date of Birth</label>
                                <asp:TextBox ID="txtDOBModal" runat="server" CssClass="form-control bg-transparent" TextMode="Date" />
                                <asp:RequiredFieldValidator ID="rfvDOBModal" runat="server" ControlToValidate="txtDOBModal" ErrorMessage="Date of Birth is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="ddlPhone">Country Code</label>
                                <asp:DropDownList ID="ddlPhone" runat="server" CssClass="form-select form-select-lg form-select-solid country-code-dropdown text-dark"></asp:DropDownList>
                               
                            </div>
                        </div>

                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtPhone">Phone Number</label>
                                <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone Number" CssClass="form-control form-control-lg flex-grow-1" ClientIDMode="Static" inputmode="numeric" TextMode="Number" />
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="ddlGenderModal">Gender</label>
                                <asp:DropDownList ID="ddlGenderModal" runat="server" CssClass="form-select form-select-lg">
                                     <asp:ListItem Text="Select Gender" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                    <asp:ListItem Text="Binary" Value="Binary"></asp:ListItem>
                                    <asp:ListItem Text="Prefer Not to Mention" Value="PreferNotToMention"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvGenderModal" runat="server" ControlToValidate="ddlGenderModal" InitialValue="" ErrorMessage="Gender is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                      
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtIDModal">Internal ID Number</label>
                                <asp:TextBox ID="txtIDModal" runat="server" TextMode="Number" placeholder="Internal ID Number" CssClass="form-control bg-transparent" />
                                <asp:RequiredFieldValidator ID="rfvIDModal" runat="server" ControlToValidate="txtIDModal" ErrorMessage="Internal ID Number is required" CssClass="text-danger" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                       
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtNameModal">Name</label>
                                <asp:TextBox ID="txtNameModal" runat="server" placeholder="Name" CssClass="form-control bg-transparent" />
                                <asp:RequiredFieldValidator ID="rfvNameModal" runat="server" ControlToValidate="txtNameModal" ErrorMessage="Name is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    

                 
                      
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtEmailModal">Email Address</label>
                                <asp:TextBox ID="txtEmailModal" runat="server" placeholder="Email Address" CssClass="form-control bg-transparent" ClientIDMode="Static" />
                                <asp:RegularExpressionValidator ID="revEmailModal" runat="server" ControlToValidate="txtEmailModal" ValidationExpression="^[^\s@]+@[^\s@]+\.[^\s@]+$" ErrorMessage="Invalid email format" CssClass="text-danger" Display="Dynamic" />
                                <asp:CustomValidator ID="cvEmailUniqueModal" runat="server" ControlToValidate="txtEmailModal" ErrorMessage="Email address already exists" OnServerValidate="cvEmailUniqueModal_ServerValidate" CssClass="text-danger" Display="Dynamic" />
                            </div>
                        </div>
                   </div>

                    <div class="row">
                      
                        <div class="col-md-12 mb-4">
                            <div class="form-group">
                                <label for="txtAddressModal">Address</label>
                                <asp:TextBox ID="txtAddressModal" runat="server" placeholder="Address" CssClass="form-control bg-transparent" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                   
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtTownModal">Town</label>
                                <asp:TextBox ID="txtTownModal" runat="server" placeholder="Town" CssClass="form-control bg-transparent" />
                            </div>
                        </div>

                   
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtCityModal">City</label>
                                <asp:TextBox ID="txtCityModal" runat="server" placeholder="City" CssClass="form-control bg-transparent" />
                            </div>
                        </div>

                       
                     
                    </div>

                    <div class="row">
                     
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="ddlCountryModal">Country</label>
                                <asp:DropDownList ID="ddlCountryModal" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
                            </div>
                        </div>
                           <div class="col-md-6 mb-4">
       <div class="form-group">
           <label for="txtZipModal">Postcode / Zip Code</label>
           <asp:TextBox ID="txtZipModal" runat="server" placeholder="Postcode / Zip Code" CssClass="form-control bg-transparent" />
       </div>
   </div>
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtBackgroundModal">Background</label>
                                <asp:TextBox ID="txtBackgroundModal" runat="server" placeholder="Background" CssClass="form-control bg-transparent" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>

                    
                        <div class="col-md-6 mb-4">
                            <div class="form-group">
                                <label for="txtHealthConditionModal">Health Condition</label>
                                <asp:TextBox ID="txtHealthConditionModal" runat="server" placeholder="Health Condition" CssClass="form-control  bg-transparent" TextMode="MultiLine" Rows="3" />
                            </div>
                        </div>

                      <div class="col-md-6 mb-4">
    <div class="form-group">
        <label for="ddlEmploymentStatusModal">Employment Status</label>
        <asp:DropDownList 
            ID="ddlEmploymentStatusModal" 
            runat="server" 
            CssClass="form-select bg-transparent">
            <asp:ListItem Text="Select Employment Status" Value="" />
            <asp:ListItem Text="Employed" Value="Employed"></asp:ListItem>
            <asp:ListItem Text="Unemployed" Value="Unemployed"></asp:ListItem>
            <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
        </asp:DropDownList>
      
    </div>
</div>


                    <div class="row">
                    
                        <div class="col-md-12 mb-4 ">
                            <div class="form-group">
                                <label for="fuProfileImage">Upload Profile Image</label>
                                <asp:FileUpload ID="fuProfileImage" runat="server" CssClass="form-control bg-transparent" onchange="showImagePreview(this);" />
                                <div id="imagePreviewContainer" class="mt-3">
                                   
                                </div>
                            </div>
                        </div>
                    </div>
             
       </div>


            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</div>
    </div> 


    <!-- Success Model -->
      <div class="modal fade" id="MysuccessModal" tabindex="-1" aria-labelledby="MysuccessModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="MysuccessModalLabel">Success</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
  <asp:Label ID="lblMessage" runat="server"></asp:Label>
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
       
    <script type="text/javascript">
        $(document).ready(function () {
            // O the modal when the "Add Secondary Beneficiary" button is clicked
            $('#addSecondaryBeneficiaryButton').on('click', function () {
                $('#FormModal').modal('show');
            });

            // Close the modal when the "Close" button is clicked
            $('#closeModalButton').on('click', function () {
                $('#FormModal').modal('hide');
            });

            // Check if we need to show the modal after postback
            if ($('#<%= hfShowModal.ClientID %>').val() === 'true') {
            $('#FormModal').modal('show');
            // Reset the hidden field to prevent the modal from opening again
            $('#<%= hfShowModal.ClientID %>').val('false');
        }
    });

        function editBeneficiary(beneficiaryID) {
            document.getElementById('<%= hfBeneficiaryID.ClientID %>').value = beneficiaryID;
            __doPostBack('EditBeneficiary', beneficiaryID);
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


        function showSuccessModal() {
            window.location.href = window.location;
            var successModal = new bootstrap.Modal(document.getElementById('MysuccessModal'));
            successModal.show();

        }
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
        function clearForm() {
            document.getElementById('<%= txtNameModal.ClientID %>').value = '';
            document.getElementById('<%= txtDOBModal.ClientID %>').value = '';
            document.getElementById('<%= txtPhone.ClientID %>').value = '';
            document.getElementById('<%= txtEmailModal.ClientID %>').value = '';
            document.getElementById('<%= txtAddressModal.ClientID %>').value = '';
            document.getElementById('<%= txtTownModal.ClientID %>').value = '';
            document.getElementById('<%= txtCityModal.ClientID %>').value = '';
    document.getElementById('<%= txtZipModal.ClientID %>').value = '';
    document.getElementById('<%= txtBackgroundModal.ClientID %>').value = '';
            document.getElementById('<%= txtHealthConditionModal.ClientID %>').value = '';
            document.getElementById('<%= txtIDModal.ClientID %>').value = '';

    // Reset dropdowns
    document.getElementById('<%= ddlTypeModal.ClientID %>').selectedIndex = 0;
    document.getElementById('<%= ddlPhone.ClientID %>').selectedIndex = 0;
    document.getElementById('<%= ddlGenderModal.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlCountryModal.ClientID %>').selectedIndex = 0;
        }


    </script>


</asp:Content>  