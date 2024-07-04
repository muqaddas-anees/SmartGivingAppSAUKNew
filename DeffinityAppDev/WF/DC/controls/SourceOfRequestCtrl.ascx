<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_SourceOfRequestCtrl" Codebehind="SourceOfRequestCtrl.ascx.cs" %>
 <asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="upnlSourceOfRequest">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlSourceOfRequest" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                       <%-- <div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>   Source of Request </strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>
                                        <div class="form-group row mb-6">
                                            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
                                            <asp:Label ID="lblError" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSourceOfRequest"
                                                        Display="Dynamic" ErrorMessage="Please select source of request" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="sr_e"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSourceOfRequest"
                                                        Display="Dynamic" ErrorMessage="Please enter source of request" SetFocusOnError="True" ValidationGroup="sr"></asp:RequiredFieldValidator>
                                            </div>
<div class="form-group row mb-6">
                                
                                       <label class="col-sm-4 control-label"> Source of Request</label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlSourceOfRequest" runat="server" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdSourceOfRequest" runat="server" TargetControlID="ddlSourceOfRequest"
                                                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetSourceOfRequest" LoadingText="[Loading...]" />
                                                    <asp:TextBox ID="txtSourceOfRequest" runat="server" CssClass="txt_field" Width="200px" ValidationGroup="sr"></asp:TextBox>
                                                    <%--<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                               TargetControlID="imb_Delete" PopupControlID="Panel_portfolio" CancelControlID="btnClose" 
                                      BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel_portfolio" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                    Style="display: none" ScrollBars="Auto" >
                                 <div class="form-group row mb-6">
        <div class="col-md-11 text-bold">
        <strong>  <%= Resources.DeffinityRes.Alert%> </strong>
            <hr class="no-top-margin" />
            </div>
                                     <div class="col-md-1">
                                         <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkClose" />
                                         </div>
    </div>
                                <div class="form-group row mb-6">
             <div class="col-md-12">
                 Delete from just this customer or all?
</div>
</div>
                                <div class="form-group row mb-6">
             <div class="col-md-12">
                    <asp:Button ID="btndeleteAll" runat="server" Text="All customers" OnClick="btndeleteAll_Click" />
                                            <asp:Button ID="BtnDelete" runat="server" Text="Just this customer"
                                                 SkinID="btnDefault"
                                                 OnClick="BtnDelete_Click"/>
</div>
</div>
                            
                              </asp:Panel>--%>

                                                    <asp:LinkButton ID="imb_Delete" runat="server" SkinID="BtnLinkDelete"
                                                        ToolTip="Delete" CausesValidation="false" OnClick="imb_Delete_Click" OnClientClick="return confirm('Do you want to delete the record?');"  />
                                          <asp:HiddenField ID="hfId" runat="server" Value="0" />
					
				</div>
</div>
                                        <div class="form-group row mb-6">
             
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:LinkButton ID="imb_Add" runat="server" SkinID="btnAdd" onclick="imb_Add_Click"/>
                                                    <asp:LinkButton ID="imb_Submit" runat="server" SkinID="btnSubmit"  ValidationGroup="sr" 
                                                        onclick="imb_Submit_Click" />
                                                    <asp:LinkButton ID="imb_Edit" runat="server" SkinID="btnEdit" ValidationGroup="sr_e" OnClick="imb_Edit_Click" />
                                                    <asp:LinkButton ID="imb_Cancel" runat="server" SkinID="btnCancel" onclick="imb_Cancel_Click"  />
                                                     <asp:Button ID="btnCopyToAllCustomers" runat="server" SkinID="btnCopytoAllCustomers" Text="Copy to All Customers" OnClick="btnCopyToAllCustomers_Click" Visible="false" />
					</div>
				
                </div>
                                      
                                       
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="imb_Add" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_Submit" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_Edit" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="imb_Cancel" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>