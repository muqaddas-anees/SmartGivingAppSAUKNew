<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DC_FLSAdminDropDown" Codebehind="FLSAdminDropDown.ascx.cs" %>

<asp:Label ID="lblMsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
<asp:Panel ID="pnlFLS" runat="server">
    <div class="form-group row">
                                <div class="col-md-12">
                                    <asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <asp:Label  SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                                    <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>Subject</strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                                <div class="form-group row">     <asp:Label ID="LblSubjectMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                    <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                                    <asp:RequiredFieldValidator ID="Rfv1" runat="server" ControlToValidate="ddlSubject"
                                ErrorMessage="Please select Subject" InitialValue="0" ValidationGroup="B" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="Rfv2" runat="server" ControlToValidate="txtSubject"
                                ErrorMessage="Please enter Subject" ValidationGroup="A"  Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                             
                                    
                                    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-1 control-label">Subject</label>
                                      <div class="col-sm-5 form-inline"> <asp:TextBox ID="txtSubject" runat="server" Visible="False" ValidationGroup="A" SkinID="txt_80"></asp:TextBox>
                            <asp:DropDownList ID="ddlSubject" runat="server" ValidationGroup="B">
                            </asp:DropDownList>
                                          
					        </div>
                                       <div class="col-md-6 form-inline">
                                           <asp:LinkButton runat="server" ID="btndeleteSubject" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete this record?');" OnClick="btndeleteSubject_Click" />
                            <asp:HiddenField ID="hid" runat="server" Value="0" />
                                          <asp:LinkButton ID="btnaddSubject" runat="server" SkinID="BtnLinkButtonAdd" OnClick="btnaddSubject_Click" />
                            <asp:LinkButton ID="btneditSubject" runat="server" ValidationGroup="B" SkinID="BtnLinkButtonEdit"
                                OnClick="btneditSubject_Click" />
                            <asp:LinkButton ID="btnsubmitSubject" runat="server" Visible="false" ValidationGroup="A"
                                SkinID="BtnLinkButtonSubmit" OnClick="btnsubmitSubject_Click" />
                            <asp:LinkButton ID="btncancelSubject" runat="server" SkinID="BtnLinkButtonCancel" OnClick="btncancelSubject_Click"
                                 Visible="false" />
                                           </div>
				            </div>
                        </div>
                                    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                          
                            <asp:Button ID="btnCopyToAllCustomer" runat="server" SkinID="btnOrange" Text="Copy to All Customers" OnClick="btnCopyToAllCustomer_Click" Visible="false"/>
					</div>
				</div>
                </div>
                                    <div class="form-group row">
                                        <%-- <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                               TargetControlID="btndeleteSubject" PopupControlID="Panel_portfolio" CancelControlID="btnClose" 
                                      BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel_portfolio" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                    Style="display: none" ScrollBars="Auto" >
                                <div class="form-group row">
        <div class="col-md-11 text-bold">
        <strong> Alert</strong>
            <hr class="no-top-margin" />
            </div>
                                     <div class="col-md-1 pull-right">
                                     <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkClose" />
                                         </div>
    </div>
                              <table>
                                 
                                  <tr>
                                      <td><b>Delete from just this customer or all? </b></td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style2">
                                 <asp:Button ID="btndeleteAll" runat="server" SkinID="btnDefault" Text="All customers" OnClick="btndeleteAll_Click"/>
                                            <asp:Button ID="BtnDelete" runat="server" Text="Just this customer"
                                                 SkinID="btnDefault" 
                                                 OnClick="BtnDelete_Click"/>
                                      </td>
                                  </tr>
                              </table>
                              </asp:Panel>--%>
                                        </div>
        </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnCopyToAllCustomer"  EventName="Click"   />--%>
                                            <asp:AsyncPostBackTrigger ControlID="btnsubmitSubject"  EventName="Click"  />
                                           <%-- <asp:AsyncPostBackTrigger ControlID="btndeleteAll"  EventName="Click"  />
                                            <asp:AsyncPostBackTrigger ControlID="BtnDelete"  EventName="Click"  />--%>
                                            <asp:AsyncPostBackTrigger ControlID="btndeleteSubject"  EventName="Click"  />
                                            
                                        </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                 
     </div>
</asp:Panel>
