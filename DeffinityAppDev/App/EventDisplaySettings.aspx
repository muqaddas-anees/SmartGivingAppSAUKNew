<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EventDisplaySettings.aspx.cs" Inherits="DeffinityAppDev.App.EventDisplaySettings" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <asp:Label ID="lblPageTitle" runat="server" Text="Team"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    
    
    
    
    
<!--begin::Card for List View Options-->
<div class="card mb-5 mb-xl-10">
    <div class="card-header border-0">
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">List View Options</h3>
        </div>
        <div class="crd-toolbar">
            <asp:Button runat="server" ID="btnSave" style="margin-top:15px;" Text="Save" OnClick="btnSave_Click" />
        </div>
    </div>
    <div class="card-body border-top p-9">
        <div class="mb-3 row">
            <label for="txtListHeaderBackgroundColour" class="col-sm-3 col-form-label">Header Background Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListHeaderBackgroundColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListHeaderFontSize" class="col-sm-3 col-form-label">Header Font Size</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListHeaderFontSize" TextMode="Number" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListHeaderFontColour" class="col-sm-3 col-form-label">Header Font Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListHeaderFontColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListTimeSlotBackgroundColour" class="col-sm-3 col-form-label">Time Slot Background Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListTimeSlotBackgroundColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListTimeSlotFontColour" class="col-sm-3 col-form-label">Time Slot Font Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListTimeSlotFontColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListTimeSlotFontSize" class="col-sm-3 col-form-label">Time Slot Font Size</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListTimeSlotFontSize" TextMode="Number" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListEventTitleColour" class="col-sm-3 col-form-label">Event Title Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListEventTitleColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListEventTitleSize" class="col-sm-3 col-form-label">Event Title Size</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListEventTitleSize" TextMode="Number" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListEventSubjectColour" class="col-sm-3 col-form-label">Event Subject Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListEventSubjectColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListEventSubjectFontSize" class="col-sm-3 col-form-label">Event Subject Font Size</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListEventSubjectFontSize" TextMode="Number" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListEventPanelBackgroundColour" class="col-sm-3 col-form-label">Event Panel Background Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListEventPanelBackgroundColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtListDatePickerColour" class="col-sm-3 col-form-label">Date Picker Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtListDatePickerColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
    </div>
</div>
<!--end::Card for List View Options-->

<!--begin::Card for Panel View Options-->
<div class="card mb-5 mb-xl-10">
    <div class="card-header border-0">
        <div class="card-title m-0">
            <h3 class="fw-bolder m-0">Panel View Options</h3>
        </div>
    </div>
    <div class="card-body border-top p-9">
        <div class="mb-3 row">
            <label for="txtPanelBookTicketsButtonColour" class="col-sm-3 col-form-label">Book Tickets Button Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtPanelBookTicketsButtonColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtPanelBookTicketsButtonFontColour" class="col-sm-3 col-form-label">Book Tickets Button Font Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtPanelBookTicketsButtonFontColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtPanelViewLiveButtonColour" class="col-sm-3 col-form-label">View Live Button Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtPanelViewLiveButtonColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3 row">
            <label for="txtPanelViewLiveButtonFontColour" class="col-sm-3 col-form-label">View Live Button Font Colour</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtPanelViewLiveButtonFontColour" TextMode="Color" runat="server" CssClass="form-control" />
            </div>
        </div>
    </div>
</div>
<!--end::Card for Panel View Options-->
 
    
    
    
    
    
    
    
    
    
    </asp:Content>
