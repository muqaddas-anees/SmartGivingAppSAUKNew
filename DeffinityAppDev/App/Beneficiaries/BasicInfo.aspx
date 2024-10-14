<%@ Page   Language="C#" AutoEventWireup="true" CodeBehind="BasicInfo.aspx.cs" Inherits="DeffinityAppDev.App.Beneficaries.BasicInfo" MasterPageFile="~/App/Beneficiaries/Beneficiaries.Master" %>

<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
 

 
 <!--begin::Fonts(mandatory for all pages)-->
 <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700"/>        

 <link href="/metronic8/demo1/assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css"/>
 <link href="/metronic8/demo1/assets/css/style.bundle.css" rel="stylesheet" type="text/css"/>
 <!--end::Global Stylesheets Bundle-->
 
 <!-- Include Font Awesome -->
 <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet"/>
 



 

    <div class="d-flex flex-column flex-root">
        <div class="d-flex flex-column flex-lg-row flex-column-fluid">
            <div class="d-flex flex-column flex-lg-row-fluid w-lg-75 p-10 order-2 order-lg-1">
                <div class="d-flex flex-center flex-column flex-lg-row-fluid">
                    <div class="w-lg-1600px p-10 shadow-sm bg-body rounded">
                        <div class="card mb-10">
                            <div class="card-header">
                                <h3 class="card-title fw-bold text-muted">Personal Profile</h3>
                              
                            </div>
                            <div class="card-body">
    <div class="row">
        <!-- Dropdown for Type (Institute / Individual) -->
        <div class="col-md-6 mb-8 fv-plugins-icon-container">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select text-muted form-select-lg bg-transparent fw-semibold">
                <asp:ListItem Text="Select Type" Value=""></asp:ListItem>
                <asp:ListItem Text="Institute" Value="Institute"></asp:ListItem>
                <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                InitialValue="" ErrorMessage="Please select a Type" CssClass="text-danger text-muted" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- Date of Birth -->
        <div class="col-md-6 mb-8">
            <div class="input-group" id="dobPicker">
                <!-- Textbox for Date of Birth with Placeholder -->
           
  
         <asp:TextBox ID="txtDOBModal" runat="server" CssClass="form-control bg-transparent" TextMode="Date" />
       

 

                <!-- Required Field Validator for Date of Birth -->
                <asp:RequiredFieldValidator 
                    ID="rfvDOB" 
                    runat="server" 
                    ControlToValidate="txtDOBModal"
                    ErrorMessage="Date of Birth is required" 
                    CssClass="text-danger" 
                    Display="Dynamic">
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <!-- Gender -->
        <div class="col-md-6 mb-8">
            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select form-select-lg text-muted fw-semibold">
                <asp:ListItem Text="Select Gender" Value=""></asp:ListItem>
                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                <asp:ListItem Text="Binary" Value="Binary"></asp:ListItem>
                <asp:ListItem Text="Prefer Not to Mention" Value="PreferNotToMention"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="ddlGender"
                InitialValue="" ErrorMessage="Gender is required" CssClass="text-white text-bg-danger text-muted" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- Internal ID Number -->
        <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtID" runat="server" placeholder="Internal ID Number" CssClass="form-control text-muted fw-semibold form-control-lg  bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvID" runat="server" ControlToValidate="txtID" 
                ErrorMessage="Internal ID Number is required" CssClass="text-white text-bg-danger  fw-semibold" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>

    <!-- Name, Address, and Town Information -->
    <div class="row">
        <!-- Name -->
        <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtName" runat="server" placeholder="Name" CssClass="form-control form-control-lg text-muted fw-semibold  bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="Name is required" CssClass="text-white text-bg-danger  fw-semibold" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- Address -->
        <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtAddress" runat="server" placeholder="Address" CssClass="form-control text-muted fw-semibold form-control-lg  bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                ErrorMessage="Address is required" CssClass="text-white text-bg-danger fw-semibold" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- Town -->
        <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtTown" runat="server" placeholder="Town" CssClass="form-control text-muted fw-semibold form-control-lg bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvTown" runat="server" ControlToValidate="txtTown"
                ErrorMessage="Town is required" CssClass="text-white text-bg-danger  fw-semibold" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- City -->
        <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtCity" runat="server" placeholder="City" CssClass="form-control form-control-lg text-muted fw-semibold bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                ErrorMessage="City is required" CssClass="text-white text-bg-danger  fw-semibold" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <!-- Postcode/Zip Code -->

    </div>

    <div class="row text-muted">
        <!-- Country Dropdown -->
        <div class="col-md-6 mb-8 text-muted">
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-select form-select-lg text-muted fw-semibold">
                <asp:ListItem Text="Pakistan" Value="PK"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                InitialValue="" ErrorMessage="Country is required" CssClass="text-white text-bg-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
                <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtZip" runat="server" placeholder="Postcode / Zip Code" CssClass="form-control text-muted fw-semibold form-control-lg  bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip"
                ErrorMessage="Zip code is required" CssClass="text-white text-bg-danger" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>

    <!-- New Fields: Health Condition, Email, Phone, Employment Status, Note -->
    <div class="row">
        <!-- Email -->
        <div class="col-md-6 mb-8">
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="form-control text-muted fw-semibold form-control-lg  bg-transparent" />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" 
                ErrorMessage="Email is required" CssClass="text-danger fw-semibold" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ValidationExpression="\w+([-+.\w])*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email Format"
                CssClass="text-danger fw-semibold" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cvEmailUniqueModal" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email address already exists" OnServerValidate="cvEmailUniqueModal_ServerValidate" CssClass="text-danger" Display="Dynamic" />
        </div>

        <!-- Phone -->
               <div class="col-md-6 mb-8">
            <asp:DropDownList ID="ddlEmploymentStatus" runat="server" CssClass="form-select form-select-lg text-muted fw-semibold">
                <asp:ListItem Text="Select Employment Status" Value="" />
                <asp:ListItem Text="Employed" Value="Employed"></asp:ListItem>
                <asp:ListItem Text="Unemployed" Value="Unemployed"></asp:ListItem>
                <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
            </asp:DropDownList>
        </div>

    
           </div>


<div class="row">
    <!-- Mobile Section -->
    <div class="col-md-6">
        <div class="d-flex">
            <!-- Country Code Dropdown -->
            <div class="col-4"> <!-- Adjust width as needed -->
                <asp:DropDownList 
                    ID="ddlPhone" 
                    runat="server" 
                    CssClass="form-select form-select-lg form-select-solid country-code-dropdown text-dark" 
                    onchange="updatePhoneCode();">
                </asp:DropDownList>
            </div>

            <!-- Phone Number Textbox -->
            <div class="col-8">
                <asp:TextBox 
                    ID="txtPhone" 
                    runat="server" 
                    placeholder="Phone Number" 
                    CssClass="form-control form-control-lg flex-grow-1" 
                    ClientIDMode="Static" />
            </div>
        </div>
    </div>

    <!-- Health Condition Section -->
    <div class="col-md-6">
        <asp:TextBox 
            ID="txtHealthCondition" 
            runat="server" 
            placeholder="Health Condition" 
            CssClass="form-control text-muted fw-semibold form-control-lg bg-transparent" />
    </div>
    
        <div class="col-md-6 ">
            <asp:TextBox ID="txtNotes" runat="server" placeholder="Notes" CssClass="form-control text-muted fw-semibold form-control-lg bg-transparent" TextMode="MultiLine" />
        </div>
</div>

                        
                            </div>
                    
    </div>
</div>

                        </div>

                        <!-- Document Upload Section -->
                        <div class=" d-flex flex-column card mb-10 text-muted fw-semibold">
                            <div class="card-header">
                                <h3 class="card-title">Documents</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <!-- Document Type Dropdown -->
                                    <div class="col-md-12 mb-8">
                                        <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-select text-muted fw-semibold form-select-lg">
                                            <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                            <asp:ListItem Text="ID Card" Value="IdCard"></asp:ListItem>
                                            <asp:ListItem Text="License" Value="License"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- Government ID Number -->
                                    <div class="col-md-6 mb-8 ">
                                        <asp:TextBox ID="txtGovID" runat="server" placeholder="Government ID Number" CssClass="form-control form-control-lg text-muted fw-semibold bg-transparent" />
                                    </div>
<div class="col-md-12 mb-8"> 
        <div class="input-group">
            <asp:FileUpload ID="fileUploadFront" runat="server" CssClass="form-control text-muted fw-semibold form-control-lg" onchange="showImagePreview(this);" />
    <asp:Image ID="imgFrontPreview" runat="server" CssClass="img-thumbnail" />

        </div>
        <div class="imagePreviewContainer"></div> 
    </div>

   
    <div class="col-md-12 mb-8"> 
        <div class="input-group">
            <asp:FileUpload ID="fileUploadBack" runat="server" CssClass="form-control text-muted fw-semibold form-control-lg" onchange="showImagePreview(this);" />
            <asp:Image ID="imgBackPreview" runat="server" CssClass="img-thumbnail" />

        </div>
        <div class="imagePreviewContainer">
        </div> 
    </div>
                                </div>

                            </div>  
                        </div>
                      
                        <div class="d-flex justify-content-end">
                        <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary btn-lg text-white active" onClick="btnSave_Click" />
                    </div>
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
    function showSuccessModal() {
        var successModal = new bootstrap.Modal(document.getElementById('successModal'));
        successModal.show();
      
    }
    function showImagePreview(input) {
        // Find the nearest .input-group, then get the next sibling with class .imagePreviewContainer
        var parent = input.closest('.input-group');
        var previewContainer = parent.nextElementSibling;

        // Clear the previous preview
        previewContainer.innerHTML = '';

        if (input.files && input.files[0]) {
            var file = input.files[0];
            var fileExtension = file.name.split('.').pop().toLowerCase();
            var allowedExtensions = ['jpg', 'jpeg', 'png', 'gif', 'pdf','doc'];

            if (allowedExtensions.includes(fileExtension)) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    // If it's an image, create an img element, otherwise handle PDF
                    if (['jpg', 'jpeg', 'png', 'gif'].includes(fileExtension)) {
                        var img = document.createElement('img');
                        img.src = e.target.result;
                        img.alt = 'Image Preview';
                        img.style.maxWidth = '200px';
                        img.style.maxHeight = '200px';
                        img.style.marginLeft = "10px";
                        previewContainer.appendChild(img);
                    } else if (fileExtension === 'pdf') {
                        var pdfLink = document.createElement('a');
                        pdfLink.href = e.target.result;
                        pdfLink.target = '_blank';
                        pdfLink.textContent = 'View PDF';
                        previewContainer.appendChild(pdfLink);
                    }
                };

                reader.readAsDataURL(file);
            } else {
                var message = document.createElement('p');
                message.textContent = 'Selected file: ' + file.name;
                previewContainer.appendChild(message);
            }
        }
    }
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
</script>



     

</asp:Content>
