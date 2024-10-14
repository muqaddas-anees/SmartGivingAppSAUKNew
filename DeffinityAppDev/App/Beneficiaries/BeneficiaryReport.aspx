<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BeneficiaryReport.aspx.cs" Inherits="DeffinityAppDev.App.Beneficiaries.BeneficiaryReport" MasterPageFile="~/App/Beneficiaries/Beneficiaries.master" %>

<asp:Content ID="BeneficiaryReport" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container">
<div class="row mb-3">
    <div class="col-12">
        <asp:Label ID="lblFromDate" runat="server" Text="From Date" CssClass="form-label" AssociatedControlID="fromDate" />
        <div class="input-group">
            <asp:TextBox ID="fromDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
    </div>
</div>
<div class="row mb-3">
    <div class="col-12">
        <asp:Label ID="lblToDate" runat="server" Text="To Date" CssClass="form-label" AssociatedControlID="toDate" />
        <div class="input-group">
            <asp:TextBox ID="toDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
    </div>
</div>


    <div class="row mb-3">
        <div class="col-12">
            <p class="fw-bold text-dark">Include the following sections in the report:</p>
        </div>
    </div>

<div class="row mb-3 text-muted fw-semibold">
    <div class="col-12">
        <div class="form-check form-check-custom form-check-solid">
            <input class="form-check-input" type="checkbox" id="personalInfo" runat="server" />
            <label class="form-check-label" for="personalInfo">Personal Information</label>
        </div>
        <div class="form-check form-check-custom form-check-solid my-5">
            <input class="form-check-input" type="checkbox" id="contacts" runat="server" />
            <label class="form-check-label" for="contacts">Contacts</label>
        </div>
        <div class="form-check form-check-custom form-check-solid my-5">
            <input class="form-check-input" type="checkbox" id="supportReceived" runat="server" />
            <label class="form-check-label" for="supportReceived">Support Received</label>
        </div>
        <div class="form-check form-check-custom form-check-solid my-5">
            <input class="form-check-input" type="checkbox" id="activity" runat="server" />
            <label class="form-check-label" for="activity">Activity</label>
        </div>

        <div class="form-check form-check-custom form-check-solid my-5">
            <input class="form-check-input" type="checkbox" id="communication" runat="server" />
            <label class="form-check-label" for="communication">Communication</label>
        </div>
    </div>
</div>
<div class="row mt-4">
   
    <div class="col-lg-12 ">
        <asp:Button ID="producePdf" CssClass="btn btn-primary w-lg-100 w-sm-50 text-white" runat="server" OnClick="ProducePdfReport_Click" Text="Produce in PDF" />
    </div>
</div>

</div>

                 <script type="text/javascript">
                     var hostUrl = "/assets/";
                 </script>
<script type="text/javascript" src="/assets/plugins/global/plugins.bundle.js"></script>
<script type="text/javascript" src="/assets/js/scripts.bundle.js"></script>
    <script type="text/javascript">

        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
</script>
</asp:Content>
