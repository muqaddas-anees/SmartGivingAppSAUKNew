<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="AddService.aspx.cs" Inherits="DeffinityAppDev.App.AddService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
    Add Service
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card">
    <div class="card-body">

        <!-- First row: Module Title, Availability Switch -->
        <div class="row mb-10">
            <div class="col-md-6">
                <label for="txtModuleTitle">Marketplace Service Name</label>
                <asp:TextBox ID="txtModuleTitle" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6 d-flex align-items-center justify-content-end">
                <label>Module Available?</label>
                <div class="ms-3">
                    <asp:CheckBox ID="chkModuleAvailable" runat="server" CssClass="form-check-input" />
                </div>
            </div>
        </div>

        <!-- Second row: Module Description -->
        <div class="row mb-10">
            <div class="col-6">
                <label for="txtModuleDescription">Description of Service</label>
                <asp:TextBox ID="txtModuleDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>

        <!-- Third row: Banner Image Upload and Video -->
        <div class="row mb-10">
            <div class="col-md-4">
                <label for="fuBannerImage">Banner Image</label>
                <div class="input-group">
                    <asp:FileUpload ID="fuBannerImage" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="previewImage();" />
                </div>
                <!-- Image preview container -->
                <asp:Image ID="imgPreview" runat="server" CssClass="img-thumbnail mt-3" Style="display: none; max-width: 200px; max-height: 150px;" />
            </div>
        </div>

        <!-- Video Explainer URL on the next line -->
        <div class="row mb-10">
            <div class="col-md-8">
                <label for="txtVideoExplainerURL">Button 1 Video Link</label>
                <asp:TextBox ID="txtVideoExplainerURL" runat="server" Style="width: 60%;" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <!-- Fourth row: Prices and Buy Now URLs -->
        <div class="row mb-10">
            <div class="col-md-2">
                <label for="ddlCurrencySmallCharity">Currency (Small Charities)</label>
                <asp:DropDownList ID="ddlCurrencySmallCharity" Width="150px" runat="server" CssClass="form-select">
                    <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                    <asp:ListItem Text="GBP" Value="GBP" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ZAR" Value="ZAR"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="txtPriceSmallCharity">Monthly Price (Small Charities)</label>
                <asp:TextBox ID="txtPriceSmallCharity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtBuyNowSmallCharity">Buy Now Price ID (Small Charities)</label>
                <asp:TextBox ID="txtBuyNowSmallCharity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
            <div class="col-md-2">
                <label for="ddlCurrencyMediumCharity">Currency (Medium Charities)</label>
                <asp:DropDownList ID="ddlCurrencyMediumCharity" runat="server" CssClass="form-select">
                    <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                    <asp:ListItem Text="GBP" Value="GBP" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ZAR" Value="ZAR"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="txtPriceMediumCharity">Monthly Price (Medium Charities)</label>
                <asp:TextBox ID="txtPriceMediumCharity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtBuyNowMediumCharity">Buy Now Price ID (Medium Charities)</label>
                <asp:TextBox ID="txtBuyNowMediumCharity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
            <div class="col-md-2">
                <label for="ddlCurrencyLargeCharity">Currency (Large Charities)</label>
                <asp:DropDownList ID="ddlCurrencyLargeCharity" runat="server" CssClass="form-select">
                    <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                    <asp:ListItem Text="GBP" Value="GBP" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="ZAR" Value="ZAR"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="txtPriceLargeCharity">Monthly Price (Large Charities)</label>
                <asp:TextBox ID="txtPriceLargeCharity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label for="txtBuyNowLargeCharity">Buy Now Price ID (Large Charities)</label>
                <asp:TextBox ID="txtBuyNowLargeCharity" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
    
            <div class="col-md-2">
                <label for="txtDiscountAnnualPrice">Discount for Annual Price</label>
                <asp:TextBox ID="txtDiscountAnnualPrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
            <div class="col-md-8">
                <label for="txtTrialPeriod">Trial Period</label>
                <asp:TextBox ID="txtTrialPeriod" runat="server" Style="width: 60%;" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
            <div class="col-md-8">
                <label for="txtVideoBtn">Button 1 Text</label>
                <asp:TextBox ID="txtVideoBtn" runat="server" Style="width: 60%;" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
            <div class="col-md-8">
                <label for="TxtBuyNow">Button 2 Text</label>
                <asp:TextBox ID="TxtBuyNow" runat="server" Style="width: 60%;" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row mb-10">
            <label class="me-2 mb-0" style="white-space: nowrap;">
                <h4>Please select the type of service</h4>
            </label>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlServices" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select a Service" Value="" />
                    <asp:ListItem Text="Livestream" Value="Livestream" />
                    <asp:ListItem Text="Online Shop" Value="OnlineShop" />
                    <asp:ListItem Text="Peer To Peer Fundraising" Value="PeerToPeerFundraising" />
                    <asp:ListItem Text="Beneficiary Management" Value="BeneficiaryManagement" />
                    <asp:ListItem Text="Project Management" Value="ProjectManagement" />
                    <asp:ListItem Text="AI" Value="AI" />
                    <asp:ListItem Text="Academy" Value="Academy" />
                    <asp:ListItem Text="Other Services" Value="OtherServices" />
                </asp:DropDownList>
            </div>
        </div>

    
</div> <div class="card-footer">
        <asp:Button ID="btnSaveNew" OnClick="btnSaveNew_Click" runat="server" Text="Save" CssClass="btn btn-primary" />
        <asp:Button ID="btnDelete" OnClientClick="return confirmDelete();" OnClick="btnDelete_Click1" runat="server" Text="Delete Module" CssClass="btn btn-danger" />
    </div>
    </div>



    <script type="text/javascript">
        function previewImage() {
            0
            var fileUpload = document.getElementById("<%= fuBannerImage.ClientID %>");
            var imgPreview = document.getElementById("<%= imgPreview.ClientID %>");

            if (fileUpload.files && fileUpload.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    imgPreview.style.display = 'block';
                    imgPreview.src = e.target.result;
                };
                reader.readAsDataURL(fileUpload.files[0]);
            }
        }
        function checkAndShowPreviewOnLoad() {
            var imgPreview = document.getElementById("<%= imgPreview.ClientID %>");
            var urlParams = new URLSearchParams(window.location.search);
            var mid = urlParams.get('MID');  // Check for MID in the query string

            // If MID is present in the query string
            if (mid) {
                imgPreview.style.display = 'block';
            }
        }
        function confirmDelete() {
            var userInput = prompt("Please type 'Plegit' to confirm deletion:");

            // Check if user input matches "Plegit"
            if (userInput === "Plegit") {
                return true; // Allow deletion to proceed
            } else {
                alert("Deletion canceled. You must type 'Plegit' to delete.");
                return false; // Cancel deletion
            }
        }


        // Attach the function to the window's load event
        window.onload = checkAndShowPreviewOnLoad;

    </script>

</asp:Content>
