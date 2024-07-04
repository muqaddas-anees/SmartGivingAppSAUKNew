<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CondtionsCntl" Codebehind="CondtionsCntl.ascx.cs" %>
<div class="form-group">
        <div class="col-md-12">
           <strong>Conditions </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

<div class="form-group">
      <div class="col-md-12">
          <asp:Label ID="lblmsg" runat="server" EnableViewState="false"></asp:Label>
        <asp:GridView ID="gridConditions" runat="server" AutoGenerateColumns="false" OnRowCancelingEdit="gridConditions_RowCancelingEdit" Width="25%"
         OnRowCommand="gridConditions_RowCommand" OnRowEditing="gridConditions_RowEditing"
         OnRowUpdating="gridConditions_RowUpdating" ShowFooter="true" OnRowDataBound="gridConditions_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="header_bg_l">
                <ItemTemplate>
                    <asp:Label ID="lblid" Visible="false" runat="server" Text='<%#Bind("id") %>'></asp:Label>
                    <asp:Label ID="lblCName" runat="server" Text='<%#Bind("Condition") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCName" runat="server" Text='<%#Bind("Condition") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFooterCname" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CausesValidation="false"  
                        ToolTip="Edit" SkinID="BtnLinkEdit" CommandName="Edit" CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                      <asp:LinkButton ID="BtnUpdate" runat="server" Text="Update" CausesValidation="false"
                           SkinID="BtnLinkUpdate" ToolTip="Update" CommandName="Update" CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                             <asp:LinkButton ID="BtnCancel" runat="server" Text="Cancel" ToolTip="Cancel" SkinID="BtnLinkCancel" CausesValidation="false"
                                 CommandName="Cancel" CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="BtnInsert" runat="server" Text="Add" SkinID="BtnLinkAdd" ToolTip="Add" CommandName="Add" CausesValidation="false"></asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                       <asp:LinkButton ID="BtnDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');" CausesValidation="false"
                                                                CommandName="Delete1" CommandArgument='<%#Bind("id") %>' SkinID="BtnLinkDelete"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
	</div>

</div>
