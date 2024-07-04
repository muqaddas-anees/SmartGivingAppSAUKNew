<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_MailSendingWithPriorityCntl" Codebehind="MailSendingWithPriorityCntl.ascx.cs" %>

<script type="text/javascript">
    $(document).ready(function () {
        $("#Lblmsg").fadeOut(5000);
    });
</script>
<asp:UpdateProgress ID="upnlProgress" runat="server">
    <ProgressTemplate>
        <asp:Label ID="imgloading" runat="server" SkinID="Loading" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlMailPriority" runat="server">
    <ContentTemplate>

        <div class="form-group row mb-6">
          <div class="col-md-12">
               <asp:Label ID="lblErrorMsg" runat="server" ClientIDMode="Static" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
              <asp:Label ID="Lblmsg" runat="server" ClientIDMode="Static" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
            <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="GropuMails" />
	</div>
</div>

        <div class="form-group row mb-6">
      <div class="col-md-8">
           <label class="col-sm-1 control-label">Priority</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPriority" runat="server" Width="150px"></asp:DropDownList>
          <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="ddlPriority" ValidationGroup="GropuMails" Display="None"
               InitialValue="0" ErrorMessage="Please select priority"></asp:RequiredFieldValidator>
            </div>
	</div>
	
</div>
          <div class="form-group row mb-6">
              <div class="col-md-8">
           <label class="col-sm-1 control-label">Users</label>
           <div class="col-sm-9">
                <div style="height:100px;overflow-y:auto;width:240px;border:3px solid #d3d3d3">
                 <asp:CheckBoxList ID="checkListUsers" runat="server"></asp:CheckBoxList>
             </div>
            </div>
	</div>
              </div>
        <div class="form-group row mb-6">
             <div class="col-md-8">
           <label class="col-sm-1 control-label"></label>
           <div class="col-sm-9 form-inline">
         
               <asp:LinkButton SkinID="btnSave" ID="BtnSave" runat="server" Text="Add" ValidationGroup="GropuMails" OnClick="BtnSave_Click" />
            <asp:LinkButton SkinID="btnUpdate" ID="BtnUpdate" runat="server" Text="Update" Visible="false" ValidationGroup="GropuMails" OnClick="BtnUpdate_Click" />
            <asp:LinkButton SkinID="btnCancel" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"  />
            <asp:Button ID="btnCopyToallCustomer" SkinID="btnOrange" runat="server" Text="Copy to all customers" OnClick="btnCopyToallCustomer_Click" Visible="false" />
               </div>
	</div>
</div>
        <div class="form-group row mb-6">
          <div class="col-md-12">
               <asp:Label ID="lblRecordId" runat="server" Visible="false"></asp:Label>
	</div>
</div>

<div>
    <asp:GridView ID="GvMailManager" runat="server" Width="60%"
         OnRowCommand="GvMailManager_RowCommand" OnRowDeleting="GvMailManager_RowDeleting" OnRowEditing="GvMailManager_RowEditing">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_l" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" CommandArgument='<%# Bind("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Priority">
                        <ItemTemplate>
                            <asp:Label ID="lbl" runat="server" Text='<%#Bind("Priority") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Users">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# Bind("ContractorName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                        <HeaderStyle Width="70px" />
                        <ItemTemplate>
                            <%--<ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" 
                               TargetControlID="ImageButton1" PopupControlID="Panel_portfolio" CancelControlID="btnClose" 
                                      BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
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
                                                 <asp:LinkButton ID="btnClose" runat="server" SkinID="BtnLinkCancel" style="border-width:0px;"/>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style2"><b><%=Resources.DeffinityRes.Deletefromjustthiscustomerorall %></b></td>
                                  </tr>
                                  <tr>
                                      <td class="auto-style2">
                                 <asp:Button ID="btndeleteAll" runat="server" Text="All customers" CommandName="DeleteAll" 
                                      CssClass="button deffinity medium" CommandArgument='<%# Bind("ID") %>'/>
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" Text="Just this customer" CommandName="Delete1"
                                                 CssClass="button deffinity medium" CommandArgument='<%# Bind("ID") %>'/>
                                      </td>
                                  </tr>
                              </table>
                              </asp:Panel>--%>
                     <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandArgument='<%# Bind("ID") %>'
                                SkinID="BtnLinkDelete" CommandName="Delete1" OnClientClick="return confirm('Do you want to delete the record?');" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
</div>

        
    </ContentTemplate>
    <Triggers>
        
    </Triggers>
</asp:UpdatePanel>