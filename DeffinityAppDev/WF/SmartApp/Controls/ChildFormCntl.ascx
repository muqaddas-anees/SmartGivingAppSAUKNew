<%@ Control Language="C#" AutoEventWireup="true" Inherits="App_Controls_ChildFormCntl" Codebehind="ChildFormCntl.ascx.cs" %>
<div>
    <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
         Child form section
     </div>
</div>
<div>
    <table>
        <tr>
            <td colspan="4">
                  <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="Create" />
                   <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server"></asp:Label> Child form name
            </td>
            <td>
                <asp:TextBox ID="txtChildName" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="req1" runat="server" ValidationGroup="Create" ControlToValidate="txtChildName"
                         ErrorMessage="Please enter form name" Display="None"></asp:RequiredFieldValidator>
            </td>
            <td>
                Notes
            </td>
            <td>
                <asp:TextBox ID="TxtNotes" runat="server"></asp:TextBox>
            </td>
            <td>
                 <asp:Button ID="btnCreateNewEntry" runat="server" Text="Create A New Entry" ValidationGroup="Create" OnClick="btnCreateNewEntry_Click" />
            </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="GridCreatedEntries" runat="server" Width="75%" OnRowCommand="GridCreatedEntries_RowCommand" 
            OnRowDeleting="GridCreatedEntries_RowDeleting" OnRowEditing="GridCreatedEntries_RowEditing" OnRowDataBound="GridCreatedEntries_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="ImgSymEdit" CommandArgument='<%#Bind("ID") %>' CommandName="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Data Description">
                    <ItemTemplate>
                        <asp:Label ID="LblMyFormName" runat="server" Text='<%#Bind("FormName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Created By">
                    <ItemTemplate>
                        <asp:Label ID="LblCreatedBy" runat="server" Text='<%#Bind("CreatedBy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Created Date">
                    <ItemTemplate>
                        <asp:Label ID="LblCreatedDate" runat="server" Text='<%#Bind("CreatedDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:Label ID="LblNotes" runat="server" Text='<%#Bind("Notes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                           <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                SkinID="ImgSymDel" CommandArgument='<%#Bind("ID") %>' CommandName="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>