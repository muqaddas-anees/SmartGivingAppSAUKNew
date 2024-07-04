<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_PriorityLevelCntl" Codebehind="PriorityLevelCntl.ascx.cs" %>
  <%--<div class="tab_header_Bold">
      Priority Level
  </div>--%>
<div class="row mb-6">
    <asp:Label ID="lblmsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
    <asp:Label ID="lblEror" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
</div>
 <asp:GridView ID="gridPriorityLevel" runat="server" AutoGenerateColumns="false" ShowFooter="true"
      OnRowCancelingEdit="gridPriorityLevel_RowCancelingEdit" OnRowCommand="gridPriorityLevel_RowCommand" OnRowDataBound="gridPriorityLevel_RowDataBound"
      OnRowEditing="gridPriorityLevel_RowEditing" OnRowUpdating="gridPriorityLevel_RowUpdating">
       <Columns>
           <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="header_bg_l">
               <ItemTemplate>
                    <asp:Label ID="lblid" Visible="false" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                    <asp:Label ID="lblPName" runat="server" Text='<%#Bind("Value") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPName" runat="server" Text='<%#Bind("Value") %>'></asp:TextBox>
                </EditItemTemplate>
                 <FooterTemplate>
                    <asp:TextBox ID="txtFooterPname" runat="server"></asp:TextBox>
                </FooterTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Description">
                 <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" Text='<%#Bind("Description") %>'></asp:TextBox>
                </EditItemTemplate>
                 <FooterTemplate>
                    <asp:TextBox ID="txtFooterDescription" runat="server"></asp:TextBox>
                </FooterTemplate>
           </asp:TemplateField>
           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                 <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CausesValidation="false" 
                        ToolTip="Edit" SkinID="BtnLinkEdit" CommandName="Edit" CommandArgument='<%#Bind("Id") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                      <asp:LinkButton ID="BtnUpdate" runat="server" Text="Update" CausesValidation="false"
                           SkinID="BtnLinkUpdate" ToolTip="Update" CommandName="Update" CommandArgument='<%#Bind("Id") %>'></asp:LinkButton>
                      <asp:LinkButton ID="BtnCancel" runat="server" Text="Cancel" ToolTip="Cancel" SkinID="BtnLinkCancel" CausesValidation="false"
                                 CommandName="Cancel" CommandArgument='<%#Bind("Id") %>'></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="BtnInsert" runat="server" Text="Add" SkinID="BtnLinkAdd" ToolTip="Add" CommandName="Add" CausesValidation="false" />
                </FooterTemplate>
           </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                 <ItemTemplate>
                       <asp:LinkButton ID="BtnDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');" CausesValidation="false"
                                                                CommandName="Delete1" CommandArgument='<%#Bind("Id") %>' SkinID="BtnLinkDelete"/>
                </ItemTemplate>
           </asp:TemplateField>
       </Columns>
 </asp:GridView>