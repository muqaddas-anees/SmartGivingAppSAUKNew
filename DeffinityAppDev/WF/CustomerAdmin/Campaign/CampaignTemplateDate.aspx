<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="CampaignTemplateDate.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Campaign.CampaignTemplateDate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
       Campaign
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblTemplateName" runat="server"></asp:Label>  - Scheduled Date
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row mb-6">
         
             <asp:Label ID="lblError" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
             
        </div>
    <div class="form-group row mb-6">
         <div class="col-sm-2">
             Scheduled Date
             </div>

         <div class="col-sm-6 form-inline d-flex d-inline" mb-6>
             <asp:TextBox ID="txtDate" runat="server" SkinID="DateNew" MaxLength="10" style="margin-right:10px"></asp:TextBox>
                <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" Visible="false"  />
              <asp:TextBox ID="txtTime" runat="server" Text="00:00" SkinID="Time" MaxLength="5"></asp:TextBox> <asp:Label ID="lblh" runat="server" Text="(HH:MM)" style="padding-top:10px"></asp:Label>  
                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                        PopupButtonID="imgbtnenddate6" TargetControlID="txtDate" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>--%>
             </div>
    </div>

     <div class="form-group row mb-6" style="padding-top:100px;">
         <div class="col-sm-2">
                    <asp:Button ID="btnBack" runat="server" ClientIDMode="Static"
                          OnClick="btnBack_Click" Text="Back" SkinID="btnDefault"/>
                   </div>
      <div class="col-sm-6">
          </div>
             <div class="col-sm-4  gap-3">
                   <asp:Button ID="btnSendNow" runat="server" ClientIDMode="Static"
                          OnClick="btnSendNow_Click" Text="Send Now" SkinID="btnDefault"/>
                    <asp:Button ID="btnNext" runat="server" ClientIDMode="Static"
                          OnClick="btnSave_Click" Text="Finish"  SkinID="btnDefault"/>
                   </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
