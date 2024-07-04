<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketManagerCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.TicketManagerCtrl" %>
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong><asp:Label ID="lblHeader" runat="server" Text="Ticket Management Notification Group"></asp:Label></strong>
            <hr class="no-top-margin" />
            </div>
</div>


<asp:UpdateProgress ID="uprogressDistri" runat="server" AssociatedUpdatePanelID="upd5">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>
      <asp:UpdatePanel ID="upd5" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
           
<div class="form-group row">
    <div class="col-md-12">
        <asp:Label ID="lblErrorMsg" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
     <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblEmailDisList" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUsers"
                ErrorMessage="Please select user" InitialValue="0" ValidationGroup="user" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_rtid" runat="server" /> 
        </div>
    </div>
<div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-1 control-label"> <%= Resources.DeffinityRes.Users%></label>
                                      <div class="col-sm-9"> <asp:DropDownList ID="ddlUsers" runat="server" SkinID="ddl_80">
            </asp:DropDownList>
					</div>
				</div>
                </div>
           <div class="form-group row">
             <div class="col-md-12">
                  <label class="col-sm-1 control-label"> </label>
                 <div class="col-sm-9 form-inline">
                     <asp:LinkButton ID="btnaddUser" runat="server" SkinID="btnAdd" OnClick="btnaddUser_Click" ValidationGroup="user" />
           
                     </div>
                 </div>
               </div>
           <asp:GridView ID="GvMailManager" runat="server" Width="70%"
                onrowcommand="GvMailManager_RowCommand" EmptyDataText="No users found">
                <Columns>
                    <asp:TemplateField HeaderText="Users">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# Bind("ContractorName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            
                     <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"
                                 SkinID="BtnLinkDelete" CommandName="Delete1" CommandArgument='<%# Bind("ID")%>' OnClientClick="return confirm('Do you want to delete the record?');"/>
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