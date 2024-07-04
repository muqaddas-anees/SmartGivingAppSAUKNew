<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_AccessNo" Codebehind="AccessNo.ascx.cs" %>

<div class="row">
          <div class="col-md-12">
 <strong>Access Number </strong> 
<hr class="no-top-margin" />
	</div>
</div>
<div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblsuccessmsg" runat="server"></asp:Label>
	</div>
</div>
   
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label">Access Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtaccessno" runat="server"></asp:TextBox>
            </div>
	</div>
</div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
                <asp:Button ID="imgbtnaddno" runat="server" SkinID="btnSubmit"
        onclick="imgbtnaddno_Click" ValidationGroup="s" />
         <asp:Button ID="imgbtnupdateno" runat="server" SkinID="btnUpdate"
        onclick="imgbtnupdateno_Click" ValidationGroup="s" /> 
        <asp:LinkButton ID="imgbtndel" runat="server" SkinID="BtnLinkDelete"
        onclick="imgbtndel_Click" OnClientClick="javascript:return confirm('Are you sure to delete?');" />
            </div>
	</div>
</div>
    
   
<div class="row">
          <div class="col-md-12">
              <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtaccessno" ValidChars="1234567890" FilterType="Custom"></ajaxToolkit:FilteredTextBoxExtender>

    <asp:RequiredFieldValidator ID="rfvacsno" runat="server" 
                    ErrorMessage="Please enter access number." 
                    ControlToValidate="txtaccessno" Display="Dynamic" SetFocusOnError="True" 
                    ValidationGroup="s"></asp:RequiredFieldValidator>
	</div>
</div>

        
