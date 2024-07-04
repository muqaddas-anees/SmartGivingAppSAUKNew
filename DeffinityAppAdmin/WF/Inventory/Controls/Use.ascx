<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Use_inventory" Codebehind="Use.ascx.cs" %>
<asp:UpdateProgress ID="uprogress1" runat="server">
    <ProgressTemplate>
    <asp:Label ID="imgLoad" SkinID="Loading"  runat="server"/>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        
<div class="row">
          <div class="col-md-12">
 <strong>USE</strong> 
<hr class="no-top-margin" />
	</div>
</div>
        

<div class="form-group">
      <div class="col-md-12">
          <div class="form-group">
                    <div class="col-md-12">
                         <asp:Label ID="lblmsg" runat="server" ></asp:Label>
                    </div>
                   <div class="col-md-5">
                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Use%></label>
                       <div class="col-sm-9 form-inline">
                    <asp:DropDownList ID="ddlUse" runat="server" SkinID="ddl_80"></asp:DropDownList>
                    <asp:TextBox ID="txtUse" runat="server" SkinID="txt_40" ValidationGroup="site"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server"
                                                    TargetControlID="txtUse" WatermarkText="Type Use Here" WatermarkCssClass="watermarked" />
                    <asp:TextBox ID="txtCode" runat="server" SkinID="txt_40" ValidationGroup="site"></asp:TextBox>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                    TargetControlID="txtCode" WatermarkText="Type Code Here" WatermarkCssClass="watermarked" />
                    <asp:LinkButton ID="imb_DeleteUse" runat="server" SkinID="BtnLinkDelete" ToolTip="Delete"
                                                 OnClientClick="return confirm('Do you want to delete the record?');"
                                                ValidationGroup="sedit" OsnClick="imb_DeleteUse_Click"></asp:LinkButton>
                             
                    <ajaxToolkit:CascadingDropDown ID="ccdUse" runat="server" TargetControlID="ddlUse"
                        Category="Use" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/Inventory/Webservices/InventoryMgr.asmx"
                        ServiceMethod="GetInventoryUse" LoadingText="[Loading ...]" />
                       </div>
                   </div>
                </div>
               <div class="form-group">
                    <div class="col-md-1"></div>
                    <div class="col-md-5">
                         <asp:Button ID="imb_AddUse" runat="server" OnClick="imb_AddUse_Click" Text="Add" CausesValidation="false" />
                         <asp:Button ID="imb_SubmitUse" runat="server" Text="Submit" ValidationGroup="site" OnClick="imb_SubmitUse_Click" />
                         <asp:Button ID="imb_EditUse" runat="server" Text="Edit" ValidationGroup="sedit" OnClick="imb_EditUse_Click" />
                         <asp:Button ID="imb_CancelUse" runat="server" Text="Cancel" OnClick="imb_CancelUse_Click"  />
                    </div>
               </div>
                  <div class="form-group">
                       <div class="col-md-12">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUse" ForeColor="Red"
                        Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,PleaseselectUse%>" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="sedit"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUse" ForeColor="Red"
                        Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterUse%>" SetFocusOnError="True"
                         ValidationGroup="site"></asp:RequiredFieldValidator>
                        <br />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCode" ForeColor="Red"
                         Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentercode%>" SetFocusOnError="True"
                              ValidationGroup="site"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hfUseId" runat="server" Value="0" />
                       </div>
                  </div>
	</div>

</div>
       
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imb_AddUse" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_SubmitUse" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_EditUse" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_CancelUse" EventName="click" />
        
    </Triggers>
</asp:UpdatePanel>
