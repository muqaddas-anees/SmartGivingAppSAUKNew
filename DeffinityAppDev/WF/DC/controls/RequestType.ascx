<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_RequestType" Codebehind="RequestType.ascx.cs" %>

<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="sec_header"  style="width: 550px">Module</div>
<div><label>This drop down is used to filter the security access report</label></div><br />
<asp:Label ID="lblmsg" runat="server"></asp:Label><br /><br />
<table cellpadding="0" cellspacing="0" width="400px">
    <tr>
        <td width="160px">
            Type of Permit</td>
        <td width="200px">
            <asp:DropDownList ID="ddl_Permit" runat="server" Width="200px">
            </asp:DropDownList>
             <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddl_Permit"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]" />
            <asp:TextBox ID="txt_Permit" runat="server" CssClass="txt_field" Width="200px" 
                ValidationGroup="a"></asp:TextBox>
        </td>
        <td width="40px" align="center">
            <asp:LinkButton ID="imb_DeletePermit" runat="server"  
                SkinID="BtnLinkDelete"
                ToolTip="Delete" 
                OnClientClick="return confirm('Do you want to delete the record?');" 
                onclick="imb_DeletePermit_Click" ValidationGroup="e" Visible="false"/>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td colspan="2" style="padding-top: 10px">
            <asp:Button ID="imb_AddPermit" runat="server" 
                SkinID="btnAdd" 
                onclick="imb_AddPermit_Click"/>
            <asp:Button ID="imb_SubmitPermit" runat="server" 
                SkinID="btnSubmit" 
                onclick="imb_SubmitPermit_Click" ValidationGroup="a" />
            <asp:LinkButton ID="imb_EditPermit" runat="server" 
                SkinID="BtnLinkEdit" 
                ValidationGroup="e" onclick="imb_EditPermit_Click" />
            <asp:LinkButton ID="imb_CancelPermit" runat="server" 
               SkinID="BtnLinkCancel" 
                onclick="imb_CancelPermit_Click" />
        </td>
      
    </tr>
    <tr>
        <td colspan="3" >
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddl_Permit" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="e"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txt_Permit" Display="Dynamic" 
                ErrorMessage="Please enter permit" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_rtId" runat="server" Value="0" />
        </td>
    </tr>
</table>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imb_AddPermit" EventName="click" />
<asp:AsyncPostBackTrigger ControlID="imb_SubmitPermit" EventName="click" />
<asp:AsyncPostBackTrigger ControlID="imb_EditPermit" EventName="click" />
<asp:AsyncPostBackTrigger ControlID="imb_CancelPermit" EventName="click" />

</Triggers>
</asp:UpdatePanel>