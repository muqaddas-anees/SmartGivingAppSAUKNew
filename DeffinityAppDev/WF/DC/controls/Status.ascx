<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_Status" Codebehind="Status.ascx.cs" %>
<asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  <%=Resources.DeffinityRes.Status %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
    <div class="form-group row">
             <div class="col-md-12">
                 <asp:Label ID="lblmsg" runat="server" EnableViewState="true" SkinID="GreenBackcolor"></asp:Label>
                 <asp:Label ID="lblEror" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddl_status" Display="Dynamic" 
                ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="s"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txt_status" Display="Dynamic" 
                ErrorMessage="Please enter status" SetFocusOnError="True" 
                ValidationGroup="as"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlPermit" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="p"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="ddlPermit" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="s"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_sId" runat="server" Value="0" />
</div>
</div>
     <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%=Resources.DeffinityRes.TypeofPermit %></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlPermit" runat="server" SkinID="ddl_80" ValidationGroup="s">
            </asp:DropDownList>
             <ajaxToolkit:CascadingDropDown ID="cddType" runat="server" TargetControlID="ddlPermit"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]" />
					</div>
				</div>
                </div>
     <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%=Resources.DeffinityRes.Status %></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddl_status" runat="server" SkinID="ddl_80">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddl_status"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlPermit" LoadingText="[Loading status...]" />
            <asp:TextBox ID="txt_status" runat="server" CssClass="txt_field" SkinID="txt_80" 
                ValidationGroup="as"></asp:TextBox>
                                           <asp:LinkButton ID="imb_DeletePermit" runat="server" 
                SkinID="BtnLinkDelete" 
                ToolTip="Delete" 
                OnClientClick="return confirm('Do you want to delete the record?');" 
                onclick="imb_DeletePermit_Click" ValidationGroup="s" Visible="true"/>
					</div>
				</div>
                </div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">  <asp:Button ID="imb_AddStatus" runat="server" 
                SkinID="btnAdd" 
                onclick="imb_AddStatus_Click" ValidationGroup="p"/>
            <asp:Button ID="imb_SubmitStatus" runat="server" 
                SkinID="btnSubmit" 
                onclick="imb_SubmitStatus_Click" ValidationGroup="as" />
            <asp:Button ID="imb_EditStatus" runat="server" 
                SkinID="btnEdit" 
                ValidationGroup="s" onclick="imb_EditStatus_Click"  />
            <asp:Button ID="imb_CancelStatus" runat="server" 
                SkinID="btnCancel"  
                onclick="imb_CancelStatus_Click" />
					</div>
				</div>
                </div>

</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="imb_AddStatus" EventName="click" />
<asp:AsyncPostBackTrigger ControlID="imb_SubmitStatus" EventName="click" />
<asp:AsyncPostBackTrigger ControlID="imb_EditStatus" EventName="click" />
<asp:AsyncPostBackTrigger ControlID="imb_CancelStatus" EventName="click" />

</Triggers>
</asp:UpdatePanel>