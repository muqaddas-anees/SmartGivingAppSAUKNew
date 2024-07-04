<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_EmailDistributionList" Codebehind="EmailDistributionList.ascx.cs" %>

<div class="form-group">
        <div class="col-md-12">
           <strong>Inventory Distribution List </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>


<div class="row">
          <div class="col-md-12">
              <asp:UpdatePanel ID="upd5" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
               <div class="panel-body">
        <div>
                 <asp:Label ID="lblMsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
                 <asp:Label ID="lblEmailDisList" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label><br />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUsers"
                                                     ErrorMessage="Please select user" InitialValue="0" ValidationGroup="user" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
                      <div class="col-md-5">
                           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Users%></label>
                           <div class="col-sm-10 form-inline">
                                  <asp:DropDownList ID="ddlUsers" runat="server" SkinID="ddl_60"></asp:DropDownList>
                                  <asp:Button ID="btnaddUser" runat="server" Text="Submit" OnClick="btnaddUser_Click" ValidationGroup="user" />
                                  <asp:Button ID="btnsendMail" runat="server" Text="Send" Visible="false" OnClick="btnsendMail_Click" />
                                  <asp:Button ID="btnCopyToAllCustomers" runat="server" Text="Copy To All Customers" OnClick="btnCopyToAllCustomers_Click" Visible="false" />
                           </div>
                      </div>
        </div>
         <div class="form-group">
               <div class="col-md-5">
                   <asp:GridView ID="GvMailManager" runat="server" Width="100%" onrowcommand="GvMailManager_RowCommand" AutoGenerateColumns="false" EmptyDataText="No users found.">
                                <Columns>
                                   <asp:TemplateField HeaderText="Users" HeaderStyle-CssClass="header_bg_l">
                                         <ItemTemplate>
                                               <asp:Label ID="lblUser" runat="server" Text='<%# Bind("ContractorName")%>'></asp:Label>
                                         </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                     <ItemTemplate>
                                     <%--    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                                                                            TargetControlID="ImageButton1" PopupControlID="Panel_portfolio" CancelControlID="btnClose"
                                                                                   BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
                                         <asp:Panel ID="Panel_portfolio" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" 
                                                                                 Style="display: none" ScrollBars="Auto" >
                                        <table>
                                             <tr>
                                                <td class="auto-style2">
                                                        <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                                           <%=Resources.DeffinityRes.Alert %></div>
                                                </td>
                                                <td class="auto-style1">
                                                 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                   <asp:ImageButton ID="btnClose" runat="server" SkinID="ImgSymCancel" style="border-width:0px;"/>
                                                </td>
                                           </tr>
                                           <tr>
                                                   <td class="auto-style2"><b><%=Resources.DeffinityRes.Deletefromjustthiscustomerorall %></b></td>
                                           </tr>
                                            <tr>
                                               <td class="auto-style2"><asp:Button ID="btndeleteAll" runat="server" Text="All customers" CommandName="DeleteAll" 
                                                                                        CssClass="button deffinity medium" CommandArgument='<%# Bind("ID") %>'/>
                                                     &nbsp;<asp:Button ID="BtnDelete" runat="server" Text="Just this customer" CommandName="Delete1"
                                                         CssClass="button deffinity medium" CommandArgument='<%# Bind("ID") %>'/>
                                              </td>
                                          </tr>
                                     </table>
                              </asp:Panel>
                                         <asp:LinkButton ID="ImageButton1" runat="server" OnClientClick="return confirm('Do you want to delete the record?');"
                                                                                           CausesValidation="false" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete"></asp:LinkButton>
                                     </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
               </div>
         </div>
    </div>
       </ContentTemplate>
        <Triggers>
               <asp:PostBackTrigger ControlID="btnaddUser" />
                     <asp:PostBackTrigger ControlID="GvMailManager" />
        </Triggers>
</asp:UpdatePanel>
	</div>
</div>
