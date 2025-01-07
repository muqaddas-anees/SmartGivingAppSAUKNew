<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EventScroller.aspx.cs" Inherits="DeffinityAppDev.App.EventScroller" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="server">
	Wordpress Event Scroller
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


	 <div class="card mb-5 mb-xl-10">
    <!--begin::Card header-->
    <div class="card-header border-0 cursor-pointer" role="button"  aria-expanded="true" aria-controls="kt_account_profile_details">
        <!--begin::Card title-->
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">Wordpress Event Scroller</h3>
        </div>
        <!--end::Card title-->
        
        <!--begin::Card toolbar-->
        <div class="card-toolbar">
            <a href="AddEventScroller.aspx" class="btn btn-primary" style="margin-right:10px;">Add Event Scroller</a>
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
                    
                    <asp:GridView runat="server" ID="grid_Scrollers" OnRowCommand="grid_Scrollers_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Scroller Name">
                                  <ItemStyle Width="75%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblScroller" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>

                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <div class="me-0">
                            <button class="btn btn-secondary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">
                               Actions
                        <i class="ki-duotone ki-down fs-5 ms-1"></i>                                                </button>
                            
<!--begin::Menu 3-->
<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-800 menu-state-bg-light-primary fw-semibold w-200px py-3" data-kt-menu="true" style="">
    <!--begin::Heading-->
  
    <!--end::Heading-->

    <!--begin::Menu item-->
    <div class="menu-item px-3">
        <asp:LinkButton runat="server" CssClass="menu-link px-3" ID="Delete" CommandName="del" CommandArgument='<%#Eval("ID")%>' >
            Delete
        </asp:LinkButton>
    </div>

     <div class="menu-item px-3">
    <a class="menu-link px-3" target="_blank" href='<%# "AddEventScroller.aspx?MID=" + Eval("ID") %>'>Edit</a>
</div>
     
  
   
</div>

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