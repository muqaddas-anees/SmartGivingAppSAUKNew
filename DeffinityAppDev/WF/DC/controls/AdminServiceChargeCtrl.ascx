<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminServiceChargeCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.AdminServiceChargeCtrl" %>

   <%-- <div class="row">
          <div class="col-md-12">
 <strong> Service Charge Admin</strong> 
<hr class="no-top-margin" />
	</div>
</div>--%>
 <div class="row">
          <div class="col-md-12">
 <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState ="false"></asp:Label>
	</div>
</div>
     <asp:Panel ID="pnlLogin" runat="server">
<div class="row">
          <div class="col-md-12">
 <strong>This area is restricted for Admin only</strong> 

	</div>
</div>
         <div class="row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="vallogin" runat="server" ValidationGroup="ad" />
              </div>
             </div>

     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"><%=Resources.DeffinityRes.Password %></label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtPassword" runat="server"  TextMode="Password" ValidationGroup="ad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCustomerRef" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="Please enter password" SetFocusOnError="true" ValidationGroup="ad"  Display="None"></asp:RequiredFieldValidator>
            </div>
              
	</div>
</div>
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-5">
                <asp:Button ID="btnAccess" runat="server" SkinID="btnDefault" Text="Access" OnClick="btnAccess_Click" ValidationGroup="ad"/>
            </div>
              
	</div>
</div>

         </asp:Panel>
     <asp:Panel ID="pnlServiceCharge" runat="server">
          <div class="row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="valServiceDesc" runat="server" ValidationGroup="ads" />
              </div>
             </div>
 <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"><%=Resources.DeffinityRes.Description %></label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtDescription" runat="server" Width="250px" ValidationGroup="ads"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                ErrorMessage="Please enter description" SetFocusOnError="true" ValidationGroup="ads" Display="None"></asp:RequiredFieldValidator>
            </div>
              
	</div>
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"><%=Resources.DeffinityRes.Value %></label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
            </div>
              
	</div>
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-5">
                <asp:CheckBox ID="chkApply" runat="server" Text="Apply TAX" />
            </div>
              
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-5 form-inline">
                <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="ads" />
               <%--<asp:Button ID="btnDelete" runat="server" SkinID="btnDefault" Text="Delete" />--%>
            </div>
              
	</div>
</div>
         </asp:Panel>