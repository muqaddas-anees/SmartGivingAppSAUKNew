<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedRateTypeCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.FixedRateTypeCtrl" %>


<asp:Panel ID="pnlFLS" runat="server">
    <div class="form-group row">
                                <div class="col-md-12">
                                    <asp:UpdateProgress ID="uprogress_stype1" runat="server" AssociatedUpdatePanelID="UpdatePanelStype1">
    <ProgressTemplate>
        <asp:Label  SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
                                    <asp:UpdatePanel ID="UpdatePanelStype1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                                    <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>Service type</strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                                <div class="form-group row">    
                                    
                                    <asp:Label ID="lblErrorServicetype" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
<asp:Label ID="lblSuccessServicetype" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                     
                                    <asp:RequiredFieldValidator ID="Rfv1" runat="server" ControlToValidate="ddlSubject"
                                ErrorMessage="Please select Subject" InitialValue="0" ValidationGroup="B" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="Rfv2" runat="server" ControlToValidate="txtSubject"
                                ErrorMessage="Please enter service type" ValidationGroup="A"  Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                             
                                    
                                    <div class="form-group row">
                                  <div class="col-md-10">
                                       <label class="col-sm-2 control-label">Service type</label>
                                      <div class="col-sm-4 form-inline"> <asp:TextBox ID="txtSubject" runat="server" Visible="False" ValidationGroup="A" SkinID="txt_90"></asp:TextBox>
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
                                SkinID="btnSubmit" OnClick="btnsubmitSubject_Click" />
                            <asp:LinkButton ID="btncancelSubject" runat="server" SkinID="BtnLinkButtonCancel" OnClick="btncancelSubject_Click"
                                 Visible="false" />
                                           </div>
				            </div>
                        </div>
                                  
                                    <div class="form-group row">
                                        
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
