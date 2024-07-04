<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_OurSite" Codebehind="OurSite.ascx.cs" %>

<asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="up1">
    <ProgressTemplate>
        <asp:Label  SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>Our Sites </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
         <div class="form-group row">
             <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
              <asp:Label ID="lblerror" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOurSite"
                        Display="Dynamic" ErrorMessage="Please select site" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="sedit"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOurSite"
                        Display="Dynamic" ErrorMessage="Please enter site" SetFocusOnError="True" ValidationGroup="site"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="h_sId" runat="server" Value="0" />
             </div>
       <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-5 form-inline"> <asp:DropDownList ID="ddlOurSite" runat="server" SkinID="ddl_80" 
                        onselectedindexchanged="ddlOurSite_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <%--<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                               TargetControlID="imb_DeleteSite" PopupControlID="Panel_portfolio" CancelControlID="btnClose" 
                                      BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel_portfolio" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                    Style="display: none" ScrollBars="Auto" >
                              <table>
                                  <tr>
                                      <td class="auto-style2">
                                           <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                                Alert</div>
                                      </td>
                                      <td class="auto-style1">
                                            <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkClose" />
                                      </td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style2"><b>Delete from just this customer or all? </b></td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style2">
                                 <asp:Button ID="btndeleteAll" runat="server" Text="All customers"
                                      SkinID="btnDefault" OnClick="btndeleteAll_Click" />
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" Text="Just this customer"
                                                SkinID="btnDefault" OnClick="BtnDelete_Click"/>
                                      </td>
                                  </tr>
                              </table>
                              </asp:Panel>--%>

                    <asp:TextBox ID="txtOurSite" runat="server" SkinID="txt_80" ValidationGroup="site"></asp:TextBox> <asp:LinkButton ID="imb_DeleteSite" runat="server" SkinID="BtnLinkDelete"
                        ImageAlign="AbsMiddle" ToolTip="Delete" CausesValidation="false" OnClick="imb_DeleteSite_Click" OnClientClick="return confirm('Do you want to delete the record?');" />
                   
                    <ajaxToolkit:CascadingDropDown ID="ccdOurSite" runat="server" TargetControlID="ddlOurSite"
                        Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetOurSite" LoadingText="[Loading site...]" />
					</div>
                                       <div class="col-sm-5 form-inline">
                                       <asp:LinkButton ID="imb_AddSite" runat="server" SkinID="btnAdd" onclick="imb_AddSite_Click" />
                    <asp:LinkButton ID="imb_SubmitSite" runat="server" SkinID="btnSubmit" ValidationGroup="site" 
                        onclick="imb_SubmitSite_Click" />
                    <asp:LinkButton ID="imb_EditSite" runat="server" SkinID="btnEdit" ValidationGroup="sedit" 
                        onclick="imb_EditSite_Click" />
                    <asp:LinkButton ID="imb_CancelSite" runat="server" SkinID="btnCancel" onclick="imb_CancelSite_Click" />
                                           </div>
				</div>
</div>
        <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"></label>
                                      <div class="col-sm-10">  <asp:CheckBox ID="chksite" runat="server" 
                        Text="Set as Default" AutoPostBack="true" 
                        oncheckedchanged="chksite_CheckedChanged"/>
					</div>
				</div>
</div>
        <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"></label>
                                      <div class="col-sm-10 form-inline">
                                         
                    <asp:Button ID="btnCopyToAllCustomers" runat="server" Text="Copy To All Customers" OnClick="btnCopyToAllCustomers_Click" Visible="false" />
					</div>
				</div>
</div>
        
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imb_AddSite" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_SubmitSite" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_EditSite" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="imb_CancelSite" EventName="click" />
        <asp:AsyncPostBackTrigger ControlID="chksite" EventName="checkedchanged" />
    </Triggers>
</asp:UpdatePanel>
