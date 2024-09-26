6<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainTab.master" CodeBehind="Marketplace.aspx.cs" Inherits="DeffinityAppDev.App.Marketplace" %>
<%@ Register Src="~/App/controls/OrgMainTabs.ascx" TagPrefix="Pref" TagName="OrgMainTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:OrgMainTabs runat="server" ID="OrgMainTabs" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card mb-5 mb-xl-10">
        <!--begin::Card header-->
        <div class="card-header border-0 cursor-pointer" role="button" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
            <!--begin::Card title-->
            <div class="card-title m-0">
                <h3 class="fw-bolder m-0">Marketplace Services</h3>
            </div>
            <!--end::Card title-->
            
            <!--begin::Card toolbar-->
            <div class="card-toolbar">
                                <a class="btn btn-primary" style="margin-right:10px" href="AddService.aspx">Add Service</a>

                <asp:Button ID="btnSaveSize" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveSize_Click" />

            </div>
            <!--end::Card toolbar-->
        </div>
        <!--end::Card header-->

        <!--begin::Content-->
        <div id="kt_account_profile_details" class="">
            <!--begin::Form-->
            <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
                <!--begin::Card body-->
                <div class="card-body border-top p-9">
               <div class="row mb-3">
    <!-- Small Range -->
    <div class="col-md-4">
        <label for="smallFrom" class="form-label">Small From</label>
        <asp:TextBox ID="txtSmallFrom" runat="server" CssClass="form-control" oninput="formatNumber(this)" />
    </div>
    <div class="col-md-4">
        <label for="smallTo" class="form-label">Small To</label>
        <asp:TextBox ID="txtSmallTo" runat="server" CssClass="form-control" oninput="formatNumber(this)" />
    </div>
</div>

<div class="row mb-3">
    <!-- Medium Range -->
    <div class="col-md-4">
        <label for="mediumFrom" class="form-label">Medium From</label>
        <asp:TextBox ID="txtMediumFrom" runat="server" CssClass="form-control" oninput="formatNumber(this)" />
    </div>
    <div class="col-md-4">
        <label for="mediumTo" class="form-label">Medium To</label>
        <asp:TextBox ID="txtMediumTo" runat="server" CssClass="form-control" oninput="formatNumber(this)" />
    </div>
</div>

<div class="row mb-3">
    <!-- Large Range -->
    <div class="col-md-4">
        <label for="largeFrom" class="form-label">Large From</label>
        <asp:TextBox ID="txtLargeFrom" runat="server" CssClass="form-control" oninput="formatNumber(this)" />
    </div>
    <div class="col-md-4">
        <label for="largeTo" class="form-label">Large To</label>
        <asp:TextBox ID="txtLargeTo" runat="server" CssClass="form-control" oninput="formatNumber(this)" />
    </div>
</div>

                    <!-- Marketplace Services Table -->
                    <div class="mb-3 row">
                        <asp:GridView ID="gvMarketplaceServices" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gvMarketplaceServices_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />

                                <asp:TemplateField HeaderText="Price (Small Charities)">
                                    <ItemTemplate>
                                        <%# Eval("CurrencyForSmallCharities") + " " +  Eval("PriceForSmallCharities") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Price (Medium Charities)">
                                    <ItemTemplate>
                                        <%# Eval("CurrencyForMediumCharities") + " " +  Eval("PriceForMediumCharities") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Price (Large Charities)">
                                    <ItemTemplate>
                                        <%# Eval("CurrencyForLargeCharities") + " " + Eval("PriceForLargeCharities") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Annual Discount">
                                    <ItemTemplate>
                                        <%# Eval("CurrencyForAnnualDiscount") + " " + Eval("AnnualDiscount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="VideoDescriptionUrl" HeaderText="Video Description URL" />
                                <asp:BoundField DataField="TrialPeriod" HeaderText="Trial Period" />
                                <asp:CheckBoxField DataField="IsModuleAvailable" HeaderText="Module Available" />

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditService" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-primary">
                                            <i class="fas fa-pencil-alt"></i>
                                        </asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteService" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirmDelete();">
                                            <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!--end::Card body-->
            </form>
            <!--end::Form-->
        </div>
        <!--end::Content-->
    </div>
 <script>
     // JavaScript to format the input with commas
     function formatNumber(input) {
         // Remove any non-numeric characters (except for commas) from the input value
         let value = input.value.replace(/,/g, '');

         // Add commas to the numeric part
         input.value = Number(value).toLocaleString();
     }

     // Ensure formatting is applied on page load for prepopulated values
     window.onload = function () {
         // Get all the input fields that require formatting
         const fieldsToFormat = [
             document.getElementById('<%= txtSmallFrom.ClientID %>'),
            document.getElementById('<%= txtSmallTo.ClientID %>'),
            document.getElementById('<%= txtMediumFrom.ClientID %>'),
            document.getElementById('<%= txtMediumTo.ClientID %>'),
            document.getElementById('<%= txtLargeFrom.ClientID %>'),
            document.getElementById('<%= txtLargeTo.ClientID %>')
         ];

         // Apply formatting to each prepopulated input field
         fieldsToFormat.forEach(function (field) {
             if (field && field.value) {
                 formatNumber(field); // Format the value on load
             }
         });
     };
</script>
    <script>
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
    </script>
</asp:Content>
