<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PolicyNumberFormatCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.PolicyNumberFormatCtrl" %>
<div class="form-group row">
     <div class="col-md-12">
         <asp:Label  id="lblSuccess" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label  id="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
         <asp:HiddenField ID="hid" runat="server" Value ="0" />
         </div>
    </div>

<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-2 control-label">Prefix</label>
           <div class="col-sm-9">
             
               <asp:TextBox ID="txtPrefix" runat="server" MaxLength="20" Text="LHG" SkinID="txt_200px"></asp:TextBox>
            </div>
	</div>
    </div>
<div class="form-group row">
     <div class="col-md-6">
           <label class="col-sm-2 control-label">Seed</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtSeed" runat="server" MaxLength="5" SkinID="txt_125px"></asp:TextBox>
               <ajaxToolkit:FilteredTextBoxExtender ID="txtFilterMobileNumber" runat="server" ValidChars="0123456789" TargetControlID="txtSeed"></ajaxToolkit:FilteredTextBoxExtender>
            </div>
	</div>
    </div>

	<div class="col-md-6 form-inline">
        <label class="col-sm-2 control-label"></label>
           <div class="col-sm-9">
           <asp:LinkButton ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" />
        
	</div>
	
</div>