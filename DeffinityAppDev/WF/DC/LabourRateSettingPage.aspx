<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="LabourRateSettingPage.aspx.cs" Inherits="DeffinityAppDev.WF.DC.LabourRateSettingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Labor Rate
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/Onboarding/Default.aspx"><i class="fa fa-arrow-left"></i> Return to Onboarding</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row">
        <div class="col-md-12">
            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
            </div>
        </div>

     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-2 control-label">Cost Rate</label>
           <div class="col-sm-4">
               <asp:TextBox ID="txtCost" runat="server" Text="0.00" SkinID="Price_150px" MaxLength="10"></asp:TextBox>
            </div>
	</div>
         
         </div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-2 control-label">Sell-out Rate</label>
           <div class="col-sm-4">
               <asp:TextBox ID="txtSell" runat="server" Text="0.00" SkinID="Price_150px" MaxLength="10"></asp:TextBox>
            </div>
	</div>
         
         </div>
     <div class="form-group row">
          <div class="col-md-6">
           <label class="col-sm-2 control-label"></label>
           <div class="col-sm-4">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" />
               </div>
              </div>
         </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
