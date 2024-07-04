<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="OurInformation.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.OurInformation" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" id="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Our Information
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      <div class="form-group">
     <div class="col-md-8">
          <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
          </div>
         </div>
      <div class="form-group">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> Address</label>
          <div class="col-sm-8 form-inline">
              <asp:TextBox ID="txtAddress" runat="server" SkinID="txtMulti_80" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
              </div>
          </div>
          </div>
     <div class="form-group">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> Town</label>
          <div class="col-sm-10 form-inline">
              <asp:TextBox ID="txtTown" runat="server" SkinID="txt_80" MaxLength="200"></asp:TextBox>
              </div>
          </div>
         </div>
     <div class="form-group">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> City</label>
          <div class="col-sm-10 form-inline">
              <asp:TextBox ID="txtCity" runat="server" SkinID="txt_80" MaxLength="200"></asp:TextBox>
              </div>
          </div>
         </div>
     <div class="form-group">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> Zipcode</label>
          <div class="col-sm-10 form-inline">
              <asp:TextBox ID="txtZipcode" runat="server" SkinID="txt_80" MaxLength="50"></asp:TextBox>
              </div>
          </div>
         </div>
     <div class="form-group">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> Tax Reference</label>
          <div class="col-sm-10 form-inline">
              <asp:TextBox ID="txtTaxReference" runat="server" SkinID="txt_80" MaxLength="255"></asp:TextBox>
              </div>
          </div>
         </div>

      <div class="form-group">
    <div class="col-md-8 text-bold">
        <strong>Payment settings </strong>
        <hr class="no-top-margin" />
    </div>
</div>
    <div class="form-group" id="pnlVendor" runat="server">
                                  <div class="col-md-8">
                                       <label class="col-sm-2 control-label"><asp:Label ID="lblVendor" runat="server" Text="MID"></asp:Label> </label>
                                      <div class="col-sm-10 form-inline">
                                          <asp:TextBox ID="txtVendor" runat="server" MaxLength="250" SkinID="txt_80"></asp:TextBox>
                                         
                                          </div>
                                      </div>
    </div>
    <div class="form-group" id="pnlUsername" runat="server">
                                  <div class="col-md-8">
                                       <label class="col-sm-2 control-label">Username</label>
                                      <div class="col-sm-10 form-inline">
                                          <asp:TextBox ID="txtUsername" runat="server" MaxLength="250" SkinID="txt_80"></asp:TextBox>
                                          
                                          </div>
                                      </div>
    </div>
<div class="form-group" id="pnlPassword" runat="server">
                                  <div class="col-md-8">
                                       <label class="col-sm-2 control-label">Password</label>
                                      <div class="col-sm-10 form-inline">
                                          <asp:TextBox ID="txtPassword" runat="server" MaxLength="250" SkinID="txt_80"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
    <div class="form-group"  id="pnlHost" runat="server">
                                  <div class="col-md-8">
                                       <label class="col-sm-2 control-label">Host</label>
                                      <div class="col-sm-10 form-inline">
                                          <asp:TextBox ID="txtHost" runat="server" MaxLength="500" Text="pilot-payflowpro.paypal.com" SkinID="txt_80"></asp:TextBox>
                                         
                                          </div>
                                      </div>
    </div>
     <div class="form-group">
      <div class="col-md-8">
          <label class="col-sm-2 control-label"> </label>
          <div class="col-sm-8 form-inline">
              <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
              </div>
          </div>
         </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
   
    
</asp:Content>
