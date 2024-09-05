<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainTab.master" CodeBehind="Marketplace.aspx.cs" Inherits="DeffinityAppDev.App.Marketplace" %>
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
    <div class="card-header border-0 cursor-pointer" role="button" data-bs-toggle="collapse" data-bs-target="#kt_account_profile_details" aria-expanded="true" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">Marketplace Services</h3>
        </div>
        <!--end::Card title-->
        
        <!--begin::Card toolbar-->
        <div class="card-toolbar">
            <a class="btn btn-primary" href="Addservice.aspx" >Add Service</a>
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
<asp:GridView ID="gvMarketplaceServices" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gvMarketplaceServices_RowCommand">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
        
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
        <asp:BoundField DataField="BuyNowLink" HeaderText="Buy Now Link" />
        <asp:BoundField DataField="VideoLink" HeaderText="Video Link" />
        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
        
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditService" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-primary">
                    <i class="fas fa-pencil-alt"></i>
                </asp:LinkButton>
                &nbsp;
               <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteService" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger"
    OnClientClick="return confirm('Are you sure you want to delete this item?');">
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

    
    


    </asp:Content>