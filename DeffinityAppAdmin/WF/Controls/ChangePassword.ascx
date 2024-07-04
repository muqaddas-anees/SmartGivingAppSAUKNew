<%@ Control Language="C#" AutoEventWireup="true"
        Inherits="controls_ChangePassword" Codebehind="ChangePassword.ascx.cs" %>
   

<div class="form-group">
    <div class="col-md-12">
         <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
        <asp:Label ID="lblError" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
    </div>
</div>

 <asp:Panel ID="Panel1" runat="server" ForeColor="Black" Visible="true">

     <div class="form-group">
          <div class="col-md-5 well">
<div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-4 control-label"> Old Password:</label>
          <div class="col-sm-8">
              <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="OldpwdReq" runat="server" ControlToValidate="txtOldPwd"
                  ErrorMessage="Please enter old password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
          </div>
      </div>
</div>
    
<div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-4 control-label"> New Password:</label>
          <div class="col-sm-8">
              <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="newPasswordReq" runat="server" ControlToValidate="txtNewPwd"
                  ErrorMessage="Please enter new password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
          </div>
      </div>
</div>

<div class="form-group">
      <div class="col-md-12">
          <label class="col-sm-4 control-label">Confirm New Password:</label>
          <div class="col-sm-8">
               <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password"></asp:TextBox><br />
               <asp:RequiredFieldValidator ID="confirmPasswordReq" runat="server" ControlToValidate="txtConfirmPwd"
                   ErrorMessage="Please enter confirmation password" SetFocusOnError="True" Display="Dynamic"
                   ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPwd"
                    ControlToValidate="txtConfirmPwd" ErrorMessage="Your passwords do not match up"
                    Display="Dynamic" ValidationGroup="ValInsert"></asp:CompareValidator>
          </div>
      </div>
</div>
<div class="form-group">
     <div class="col-md-12">
          <label class="col-sm-4 control-label"></label>
          <div class="col-sm-8">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" ValidationGroup="ValInsert"
                                OnClick="btnSubmit_Click" />
                            &nbsp;
              <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel" />
          </div>
     </div>
</div> 
              </div>
     </div>
</asp:Panel>
