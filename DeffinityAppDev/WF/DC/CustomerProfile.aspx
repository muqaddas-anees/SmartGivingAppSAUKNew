<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CustomerProfile.aspx.cs" Inherits="DeffinityAppDev.WF.DC.CustomerProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
         <div class="col-md-9">
               <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Name%></label>
           <div class="col-sm-9">
               <asp:TextBox id="txtName" runat="server" SkinID="txt_80" MaxLength="250"></asp:TextBox>
            </div>
	</div>
</div>

    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.EmailAddress%></label>
           <div class="col-sm-9">
                <asp:TextBox id="txtEmail" runat="server" SkinID="txt_80" MaxLength="500"></asp:TextBox>
            </div>
	</div>
</div>

    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Home Phone</label>
           <div class="col-sm-9">
                <asp:TextBox id="txtPhone" runat="server" SkinID="txt_80" MaxLength="50"></asp:TextBox>
            </div>
	</div>
</div>

     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Mobile%></label>
           <div class="col-sm-9">
                <asp:TextBox id="lblMobile" runat="server" SkinID="txt_80" MaxLength="50"></asp:TextBox>
            </div>
	</div>
</div>
             </div>
          <div class="col-md-3">

              <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
             </div>
        </div>

  
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
