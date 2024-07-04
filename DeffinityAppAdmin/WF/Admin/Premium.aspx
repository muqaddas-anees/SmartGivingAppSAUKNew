<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Premium.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.Premium" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
  <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" id="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
      Premium
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
      <div class="col-md-6">
           <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Premium Price</strong> 
<hr class="no-top-margin" />
	</div>
</div>
          <div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="lblMsgPrice" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
              </div>


          <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-6 control-label"> Price Per User Per Month</label>
          <div class="col-sm-6">
              <asp:TextBox ID="txtPrice" runat="server" SkinID="Price_125px" MaxLength="10" Text="0.00"></asp:TextBox>
              <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterPrice" runat="server" ValidChars="0123456789." TargetControlID="txtPrice"></ajaxToolkit:FilteredTextBoxExtender>
              </div>
          </div>
              </div>
            <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-6 control-label"> Currency</label>
          <div class="col-sm-6">
             <asp:DropDownList ID="ddlCurrency" runat="server">

             </asp:DropDownList>
              </div>
          </div>
              </div>
           <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-6 control-label"> </label>
          <div class="col-sm-6">
              <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="btnSave" />

              </div>
          </div>
               </div>

          </div>
         <div class="col-md-6">
              <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Features Disabled for FREE Version</strong> 
<hr class="no-top-margin" />
	</div>
</div>
              <div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="lblMsgModule" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
              </div>

             
              <div class="form-group row">
      <div class="col-md-12">
          <asp:CheckBoxList ID="chkModuleList" runat="server">

          </asp:CheckBoxList>

          </div>
                  </div>


               <div class="form-group row">
      <div class="col-md-12">
           <asp:Button ID="btnModuleApply" runat="server" OnClick="btnModuleApply_Click" SkinID="btnApply" />
          </div>
                   </div>
          </div>
        </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
