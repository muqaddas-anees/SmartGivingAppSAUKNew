<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="GiftAid.aspx.cs" Inherits="DeffinityAppDev.App.GiftAid" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <asp:Label ID="lblPageTitle" runat="server" Text="Team"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    
    
    
    
    
 <div class="card mb-5 mb-xl-10">
    <!--begin::Card header-->
    <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">Gift Aid Settings</h3>
        </div>
        <!--end::Card title-->
        
        <!--begin::Card toolbar-->
        <div class="card-toolbar">
            <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn btn-primary" Text="Save" />
        </div>
        <!--end::Card toolbar-->
    </div>
    <!--end::Card header-->
    
    <!--begin::Content-->
    <div id="kt_account_profile_details" class="collapse show">
        <!--begin::Form-->
        <form id="kt_account_profile_details_form" class="form fv-plugins-bootstrap5 fv-plugins-framework" novalidate="novalidate">
            <!--begin::Card body-->
            <div class="card-body border-top p-9">
                <div class="mb-3 row">
                    <label for="txtDonateButtonColor" class="col-sm-2 col-form-label" style="
    font-size: 16px;
">Enable Gift Aid</label>
                    <div class="col-sm-2" style="
    margin: auto 0;">
                        <asp:Checkbox ID="chkGiftAid" runat="server" CssClass="form-check"  />
                        <style>
                            .form-check{
                                margin:0;
                            }
                        </style>
                    </div>
                </div>
            </div>
            <!--end::Card body-->
        </form>
        <!--end::Form-->
    </div>
    <!--end::Content-->
</div>

    
    
    
    
    
    
    
    
    
    
    
    </asp:Content>