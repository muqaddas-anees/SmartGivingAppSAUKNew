<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DC_controls_SecurityAccessMail" Codebehind="SecurityAccessMail.ascx.cs" %>
<asp:UpdateProgress ID="uprogressDistri" runat="server" AssociatedUpdatePanelID="upd5">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>
      <asp:UpdatePanel ID="upd5" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
           <%--<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Service Desk Distribution List</strong>
            <hr class="no-top-margin" />
            </div>
</div>--%>
<div class="form-group row mb-6">
    <div class="col-md-12">
        <asp:Label ID="lblErrorMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
     <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblEmailDisList" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUsers"
                ErrorMessage="Please select user" InitialValue="0" ValidationGroup="user" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_rtid" runat="server" /> 
        </div>
    </div>
<div class="form-group row mb-6">
           
                                       <label class="col-sm-1 control-label"> <%= Resources.DeffinityRes.Users%></label>
                                      <div class="col-sm-9"> <asp:DropDownList ID="ddlUsers" runat="server" SkinID="ddl_80">
            </asp:DropDownList>
					</div>
				
                </div>
           <div class="form-group row mb-6">
            
                  <label class="col-sm-1 control-label"> </label>
                 <div class="col-sm-9 form-inline">
                     <asp:LinkButton ID="btnaddUser" runat="server" SkinID="btnAdd" OnClick="btnaddUser_Click" ValidationGroup="user" />
           <asp:Button ID="btnCopyToAllCustomers" runat="server" SkinID="btnDefault" Text="Copy to All Customers" OnClick="btnCopyToAllCustomers_Click" Visible="false" />
                     </div>
                
               </div>
           <asp:GridView ID="GvMailManager" runat="server" Width="70%"
                onrowcommand="GvMailManager_RowCommand" EmptyDataText="No users found.">
                <Columns>
                    <asp:TemplateField HeaderText="Users">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# Bind("ContractorName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <%--<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                               TargetControlID="ImageButton1" PopupControlID="Panel_portfolio" CancelControlID="btnClose" 
                                      BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="Panel_portfolio" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                    Style="display: none" ScrollBars="Auto" >
                                <div class="form-group row mb-6">
        <div class="col-md-11 text-bold">
             <strong>   <%=Resources.DeffinityRes.Alert %> </strong>
            <hr class="no-top-margin" />
            </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkClose" style="border-width:0px;"/>
                                    </div>
</div>
                                 <div class="form-group row mb-6">
                                    <b>Delete from just this customer or all? </b>
                                    </div>
                                <div class="form-group row mb-6">
                                    <div class="col-md-12">
                                        <asp:Button ID="btndeleteAll" runat="server" Text="All customers" CommandName="DeleteAll" 
                                      SkinID="btnDefault" CommandArgument='<%# Bind("ID") %>'/>
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" Text="Just this customer" CommandName="Delete1"
                                                 SkinID="btnDefault" CommandArgument='<%# Bind("ID") %>'/>
                                        </div>
                                    </div>
                              </asp:Panel>--%>
                     <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"
                                 SkinID="BtnLinkDelete" CommandName="Delete1"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

  </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnaddUser" EventName="click" />                                      
                                       
                                    </Triggers>
                                </asp:UpdatePanel>