<%@ Control Language="C#" AutoEventWireup="true"
        Inherits="controls_ChangePassword" Codebehind="ChangePassword.ascx.cs" %>
    <style>
        
        /* Custom styles for the checkmarks */
        .condition {
            display: flex;
            align-items: center;
            margin-top: 5px;
        }

        .condition .icon {
            color: #dc3545; /* red by default */
            margin-right: 5px;
        }

        .condition.valid .icon {
            color: #28a745; /* green when valid */
        }
          #password-conditions {
            display: none; /* Initially hidden */
        }
    </style>
<div class="form-group row mb-6">
    <div class="col-md-12">
         <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
        <asp:Label ID="lblError" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
    </div>
</div>

 <asp:Panel ID="Panel1" runat="server" ForeColor="Black" Visible="true">

     <div class="form-group row mb-6">
          <div class="col-md-5 well">
<div class="form-group row mb-6">
     
          <label class="col-sm-4 control-label"> Old Password:</label>
          <div class="col-sm-8">
              <asp:TextBox ID="txtOldPwd" runat="server" TextMode="Password"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="OldpwdReq" runat="server" ControlToValidate="txtOldPwd"
                  ErrorMessage="Please enter old password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
          </div>
      
</div>
    
<div class="form-group row mb-6">
    
          <label class="col-sm-4 control-label"> New Password:</label>
          <div class="col-sm-8">
              <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox><br />
              <asp:RequiredFieldValidator ID="newPasswordReq" runat="server" ControlToValidate="txtNewPwd"
                  ErrorMessage="Please enter new password" SetFocusOnError="True" Display="Dynamic"
                  ValidationGroup="ValInsert"></asp:RequiredFieldValidator>

               <div id="password-conditions">
                    <div class="condition" id="length-condition">
                        <span class="icon">&#10060;</span> 8-20 characters
                    </div>
                    <div class="condition" id="uppercase-condition">
                        <span class="icon">&#10060;</span> One uppercase letter
                    </div>
                    <div class="condition" id="special-char-condition">
                        <span class="icon">&#10060;</span> One special character
                    </div>
                </div>
          </div>
      
</div>

<div class="form-group row mb-6">
     
          <label class="col-sm-4 control-label">Confirm New Password:</label>
          <div class="col-sm-8">
               <asp:TextBox ID="txtConfirmPwd" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox><br />
               <asp:RequiredFieldValidator ID="confirmPasswordReq" runat="server" ControlToValidate="txtConfirmPwd"
                   ErrorMessage="Please enter confirmation password" SetFocusOnError="True" Display="Dynamic"
                   ValidationGroup="ValInsert"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPwd"
                    ControlToValidate="txtConfirmPwd" ErrorMessage="Your passwords do not match up"
                    Display="Dynamic" ValidationGroup="ValInsert"></asp:CompareValidator>
          </div>
     
</div>
<div class="form-group row mb-6">
    
          <label class="col-sm-4 control-label"></label>
          <div class="col-sm-8">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" ValidationGroup="ValInsert"
                                OnClick="btnSubmit_Click" disabled />
                            &nbsp;
              <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel" />
          </div>
     
</div> 
              </div>
     </div>
</asp:Panel>
 <script>
        $(document).ready(function(){
            $('#<%=txtNewPwd.ClientID%>').on('input', function(){
                var password = $(this).val();
                $('#password-conditions').show();

                var hasUpperCase = /[A-Z]/.test(password);
                var hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);
                var isValidLength = password.length >= 8 && password.length <= 20;
                var isPasswordValid = hasUpperCase && hasSpecialChar && isValidLength;

                // Update condition indicators
                updateConditionIndicator('#length-condition', isValidLength);
                updateConditionIndicator('#uppercase-condition', hasUpperCase);
                updateConditionIndicator('#special-char-condition', hasSpecialChar);

                // Update the validity of the password field and submit button
                $(this).toggleClass('is-invalid', !isPasswordValid);
                $(this).toggleClass('is-valid', isPasswordValid);
                $('#<%=btnSubmit.ClientID%>').prop('disabled', !isPasswordValid);
            });

            function updateConditionIndicator(elementId, isValid) {
                if(isValid) {
                    $(elementId).addClass('valid');
                    $(elementId + ' .icon').html('&#10004;'); // Checkmark
                } else {
                    $(elementId).removeClass('valid');
                    $(elementId + ' .icon').html('&#10060;'); // Cross
                }
            }
        });
 </script>