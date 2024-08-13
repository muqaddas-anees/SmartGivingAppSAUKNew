<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="mailchimp.aspx.cs" Inherits="DeffinityAppDev.App.mailchimp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Donor CRM
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">











       <div class="card mb-5 mb-xl-10" id="Div2" runat="server">
    <!--begin::Card header-->
    <div class="card-header border-0 d-flex justify-content-between align-items-center">
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">MailChimp</h3>
        </div>
        <!-- Save Settings Button -->
        <asp:Button ID="savesttings" runat="server" Text="Save Settings" CssClass="btn btn-primary" OnClick="savesttings_Click" />
    </div>

    <!-- Card Body -->
    <div class="card-body border-top p-9">
        <div class="row">
            <div class="col-lg-8">
                <!-- Mailchimp API Key TextBox -->
            <asp:Label ID="lblApiKey" runat="server" Text="Mailchimp API Key:" AssociatedControlID="txtApiKey"></asp:Label>
<asp:TextBox ID="txtApiKey" runat="server" CssClass="form-control" placeholder="Enter your Mailchimp API Key"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvApiKey" runat="server" ControlToValidate="txtApiKey" 
    ErrorMessage="Mailchimp API Key is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
<br />

<asp:Label ID="lblTeamId" runat="server" Text="Mailchimp Audience ID:" AssociatedControlID="txtTeamId"></asp:Label>
<asp:TextBox ID="txtTeamId" runat="server" CssClass="form-control" placeholder="Enter your Mailchimp Audience ID"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvTeamId" runat="server" ControlToValidate="txtTeamId" 
    ErrorMessage="Mailchimp Audience ID is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
<br />

            </div>
        </div>
    </div>

    <!-- Card Footer -->
    <div class="card-footer d-flex justify-content-end py-6 px-9">
    </div>
</div>





    </asp:Content>
