<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="testcal.aspx.cs" Inherits="DeffinityAppDev.WF.testcal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
     

       
  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="form-group row">
        <div class="row form-inline">
      <asp:TextBox ID="txtDate" runat="server" SkinID="Date"></asp:TextBox>
                            <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7"   runat="server"
                                PopupButtonID="imgbtnenddate7" TargetControlID="txtDate" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
