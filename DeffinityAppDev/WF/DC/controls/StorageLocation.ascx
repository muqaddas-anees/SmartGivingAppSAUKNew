<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_StorageLocation" Codebehind="StorageLocation.ascx.cs" %>
   <asp:UpdatePanel ID="up5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        
<div class="form-group row">
        <div class="col-md-12">
           <strong> <asp:Label ID="lblSectionHeader" runat="server" Text="Storage Location"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

                                       
<div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lbllmsg" runat="server"></asp:Label>
              <asp:RequiredFieldValidator ID="rfv_ddl_validation" runat="server" ControlToValidate="ddl_Location"
                                                        Display="Dynamic" ErrorMessage="Please select location" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="le"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfv_text_validation" runat="server" ControlToValidate="txt_Location"
                                                        Display="Dynamic" ErrorMessage="Please enter location" SetFocusOnError="True"
                                                        ValidationGroup="la"></asp:RequiredFieldValidator>
	</div>
</div>

                                        
                            
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StorageLocation%></label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddl_Location" runat="server" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdLocation" runat="server" TargetControlID="ddl_Location"
                                                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetStorageLocation" LoadingText="[Loading ...]" />
                                                    <asp:TextBox ID="txt_Location" runat="server" CssClass="txt_field" SkinID="txt_80"
                                                        ValidationGroup="la"></asp:TextBox><asp:LinkButton ID="imb_DeleteLocation" runat="server" SkinID="BtnLinkDelete"
                                                         ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                        OnClick="imb_DeleteLocation_Click" ValidationGroup="le" />
            </div>
	</div>
</div>             
                                        
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8 form-inline">
                <asp:Button ID="imb_AddLocation" runat="server" SkinID="btnAdd"
                                                         OnClick="imb_AddLocation_Click" />
                                                    <asp:Button ID="imb_SubmitLocation" runat="server" SkinID="btnSubmit"
                                                         OnClick="imb_SubmitLocation_Click" ValidationGroup="la" />
                                                    <asp:Button ID="imb_EditLocation" runat="server" SkinID="btnEdit"
                                                         ValidationGroup="le" OnClick="imb_EditLocation_Click" />
                                                    <asp:Button ID="imb_CancelLocation" runat="server" SkinID="btnCancel"
                                                         OnClick="imb_CancelLocation_Click" /><asp:HiddenField ID="h_lID" runat="server" Value="0" />
            </div>
	</div>
</div>
                                        
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imb_AddLocation" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_SubmitLocation" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_EditLocation" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_CancelLocation" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>