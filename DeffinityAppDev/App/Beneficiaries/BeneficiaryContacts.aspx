<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeneficiaryContacts.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.BeneficiaryContacts" MasterPageFile="~/App/Beneficiaries/Beneficiaries.master" %>
<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1 class="me-34"> 
        </h1>
        <button id="addContactsButton" class="btn btn-primary" type="button">Add Contact</button>
    </div>
    <asp:HiddenField ID="hfContactID" runat="server" />
    <asp:HiddenField ID="hfShowModal" runat="server" Value="false" ClientIDMode="Static" />
    <div class="card-body py-4">
        <!--begin::Table-->
        <div id="kt_table_users_wrapper" class="dt-container dt-bootstrap5 dt-empty-footer">
            <div class="table-responsive">
                <table class="table align-middle table-row-dashed fs-6 gy-5 dataTable" id="kt_table_users" style="width: 100%; text-align: left;">
                    <colgroup>
                       
                        <col style="width: 15%;">
                        <col style="width: 15%;">
                        <col style="width: 15%;">
                        <col style="width: 20%;">
                        <col style="width: 15%;">
                        <col style="width: 15%;">
                    </colgroup>
                    <thead>
                        <tr class="text-start text-muted fw-bold fs-7 text-uppercase gs-0" role="row">
                          
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email Address</th>
                            <th>Contact Number</th>
                            <th>Position</th>
                            <th>Notes</th>
                              <th>Edit</th>
                        </tr>
                    </thead>
                    <tbody class="text-gray-600 fw-semibold">
                        <asp:Repeater ID="RepeaterContacts" runat="server">
                            <ItemTemplate>
                                <tr>
                                   
                                 
                                    <td><%# Eval("FirstName") %></td>
                                    <td><%# Eval("LastName") %></td>
                                    <td><%# Eval("EmailAddress") %></td>
                                    <td><%# Eval("CountryCode").ToString() + Eval("PhoneNumber").ToString() %></td>
                                    <td><%# Eval("Position") %></td>
                                    <td><%# Eval("Notes") %></td>
                                     <td class="text-end">
                        <button class="btn btn-primary" 
                                     onclick="editContact('<%# Eval("ContactID") %>');">
                         <i class="fas fa-lg fa-pencil-alt"></i>
     </button>
 </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div>
        <!-- GridView for other contacts -->
        <asp:GridView ID="GridViewContacts" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                <asp:BoundField DataField="EmailAddress" HeaderText="Email" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Contact Number" />
                <asp:BoundField DataField="Position" HeaderText="Position" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" />
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal Structure for Adding Contact -->
  <div class="modal fade" id="addContactModal" tabindex="-1" aria-labelledby="addContactModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="addContactModalLabel">Add Contact</h5>
                <button type="button" class="btn btn-icon btn-sm btn-active-light-primary ms-2" data-bs-dismiss="modal">
                    <i class="fa fa-times"></i>
                </button>
            </div>
            <div class="modal-body py-5 px-lg-10">
                <!-- First Name -->
                <div class="fv-row mb-7">
                    <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" CssClass="form-control form-control-lg form-control-solid" />
                    
                </div>

                <!-- Last Name -->
                <div class="fv-row mb-7">
                    <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" CssClass="form-control form-control-lg form-control-solid" />
                  
                </div>

                <!-- Email Address -->
                <div class="fv-row mb-7">
                    <asp:TextBox ID="txtEmailAddress" runat="server" placeholder="Email Address" CssClass="form-control form-control-lg form-control-solid" />
                  
                </div>


<!-- Phone Number with Country Code -->
<div class="row mb-7">
    <div class="col-lg-4">
        <!-- Country Code Dropdown -->
        <asp:DropDownList 
            ID="ddlPhone" 
            runat="server" 
            CssClass="form-select form-select-lg form-select-solid country-code-dropdown text-dark">
          
        </asp:DropDownList>
        </div><div class="col">
        <!-- Phone Number Input -->
        <asp:TextBox 
            ID="txtPhoneNumber" 
            runat="server" 
            placeholder="Phone Number" 
            CssClass="form-control form-control-lg form-control-solid flex-grow-1" ClientIDMode="Static"/>
    </div>
    
    <!-- Validation Validators -->
  
  
</div>



                <!-- Position -->
                <div class="fv-row mb-7">
                    <asp:TextBox ID="txtPosition" runat="server" placeholder="Position" CssClass="form-control form-control-lg form-control-solid" />
                </div>

                <!-- Notes -->
                <div class="fv-row mb-7">
                    <asp:TextBox ID="txtNotes" runat="server" placeholder="Notes" CssClass="form-control form-control-lg form-control-solid" TextMode="MultiLine" />
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-light" data-bs-dismiss="modal">Close</button>
                <asp:Button ID="btnSaveContact" runat="server" Text="Add Contact" OnClick="btnSaveContact_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</div>

    <!-- Success Model  --> 
          <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="successModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="successModalLabel">Success</h5>
                 <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
  <asp:Label ID="Label1" runat="server"></asp:Label>
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
            $('#addContactsButton').on('click', function () {
                $('#addContactModal').modal('show');

                $(document).ready(function () {
                    $('#addContactModal').on('hidden.bs.modal', function () {
                        $(this).find('form')[0].reset(); // Reset form fields
                        
                        $('#hfContactID').val(''); // Clear hidden field
                    });
                });
            });



            // Check if we need to show the modal after postback
            if ($('#<%= hfShowModal.ClientID %>').val() === 'true') {
       $('#addContactModal').modal('show');
       // Reset the hidden field to prevent the modal from opening again
           $('#<%= hfShowModal.ClientID %>').val('false');
            }


        });


        function showSuccessModal() {
            var successModal = new bootstrap.Modal(document.getElementById('successModal'));
            successModal.show();
        } 
     // Function to format phone number with spaces after 3 and 4 digits
     function formatPhoneNumber(input) {
                              let value = input.value.replace(/\D/g, ''); // Remove all non-digit characters

                              // Format as 'XXX XXXX XXX'
                              if (value.length > 3) {
                                  value = value.slice(0, 3) + ' ' + value.slice(3);
                              }
                              if (value.length > 7) {
                                  value = value.slice(0, 7) + ' ' + value.slice(7);
                              }

                              input.value = value;
                          }
     // Attach the function to the phone number field on input
      document.getElementById('txtPhoneNumber').addEventListener('input', function () {
                              formatPhoneNumber(this);
                          });
     function editContact(contactID) {
         // Set the hidden field value to the ContactID
         document.getElementById('<%= hfContactID.ClientID %>').value = contactID;

         // Trigger a postback to load the contact details
         __doPostBack('EditContact', contactID);
       
       
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

        .country-code-selector {
            width: 30px;
            margin-right: 10px;
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
    </style>
</asp:Content>
