<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_NotesCtrl" Codebehind="NotesCtrl.ascx.cs" %>


<div class="form-group row">
<div class="col-md-12">
        <asp:UpdateProgress ID="updateprgs" runat="server" AssociatedUpdatePanelID="upNotes">
<ProgressTemplate>
<asp:Label ID="lblLoading" runat="server" SkinID="Loading" />

</ProgressTemplate>
</asp:UpdateProgress>
        <asp:UpdatePanel ID="upNotes" runat="server">
        <ContentTemplate>  
            <asp:HiddenField ID="h_statusid" runat="server" Value="0" />
            <asp:GridView ID="gvNotes" runat="server" Width="70%" OnRowCommand="gvNotes_RowCommand"
                OnRowCancelingEdit="gvNotes_RowCancelingEdit" OnRowDeleting="gvNotes_RowDeleting"
                OnRowEditing="gvNotes_RowEditing" OnRowUpdating="gvNotes_RowUpdating" EmptyDataText="No notes found"
                OnRowDataBound="gvNotes_RowDataBound" HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_l" ItemStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditNotes" runat="server" CausesValidation="false" CommandName="Edit"
                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="lnkUpdateNotes" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes,Update%>"
                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>">
                            </asp:LinkButton>
                            <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CausesValidation="false"
                                CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                            </asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnsave" runat="server" SkinID="btnSave"  CommandName="InsertNotes" Text="<%$ Resources:DeffinityRes,Update%>"
                                 ToolTip="Insert"/>
                             <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CommandName="Cancel"
                                SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkfUpdateNotes" runat="server" CommandName="InsertNotes" Text="<%$ Resources:DeffinityRes,Update%>"
                                SkinID="BtnLinkAdd" ToolTip="Insert" Visible="false"></asp:LinkButton>
                           
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Notes">
                        <ItemTemplate>
                            <asp:Label ID="lblNotes" runat="server" Text='<% #Bind("Notes") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNotes" runat="server" Text='<% #Bind("Notes") %>' SkinID="txtMulti"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtfNotes" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<% #Bind("UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Logged Date">
                        <ItemTemplate>
                            <asp:Label ID="LblDate" runat="server" Text='<% #Bind("DateTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="delete"
                                SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                        </ItemTemplate>
                        <FooterStyle Width="45px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </ContentTemplate>
        </asp:UpdatePanel>
          
       </div>
    </div>