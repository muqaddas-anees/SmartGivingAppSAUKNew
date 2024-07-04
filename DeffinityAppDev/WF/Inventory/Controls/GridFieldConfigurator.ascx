<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_GridFieldConfigurator" Codebehind="GridFieldConfigurator.ascx.cs" %>

<div class="row">
          <div class="col-md-12">
 <strong> Custom Fields for Usage Grid </strong> 
<hr class="no-top-margin" />
	</div>
</div>


<div class="row">
          <div class="col-md-12">
                <div>
            <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="G1" />
            <asp:Label ID="lblmsg" runat="server" EnableViewState="false"></asp:Label>
        </div>
     <div style="float:right">
       <asp:Button ID="btnUpdateVisibility" runat="server" Text="Update Visibility"  CssClass="btn btn-secondary" OnClick="btnUpdateVisibility_Click" />
     </div>
     <div>
         <asp:GridView ID="grid" runat="server" AutoGenerateColumns="false" Visible="false" Width="100%" OnRowCancelingEdit="grid_RowCancelingEdit"
              OnRowCommand="grid_RowCommand" OnRowEditing="grid_RowEditing" OnRowUpdating="grid_RowUpdating" 
             OnDataBound="grid_DataBound" OnRowDataBound="grid_RowDataBound" ShowFooter="true" OnRowDeleting="grid_RowDeleting">
                <Columns>
                   <asp:TemplateField HeaderText="Default Name" HeaderStyle-CssClass="header_bg_l">
                       <ItemTemplate>
                           <asp:Label ID="lblDeafultName" Text='<%# Bind("DeafaultName") %>' runat="server"></asp:Label>
                       </ItemTemplate>
                       <EditItemTemplate>
                            <asp:Label ID="lblDeafultNameEdit" Text='<%# Bind("DeafaultName") %>' runat="server"></asp:Label>
                       </EditItemTemplate>
                       <FooterTemplate>
                           <asp:TextBox ID="txtDnamefooter" runat="server"></asp:TextBox>
                       </FooterTemplate>
                   </asp:TemplateField>
                     <asp:TemplateField HeaderText="Display Name">
                       <ItemTemplate>
                           <asp:Label ID="LblDisplayName" Text='<%# Bind("DisplayName") %>' runat="server"></asp:Label>
                       </ItemTemplate>
                         <EditItemTemplate>
                             <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("DisplayName") %>'></asp:TextBox>
                         </EditItemTemplate>
                          <FooterTemplate>
                           <asp:TextBox ID="txtDisnamefooter" runat="server"></asp:TextBox>
                       </FooterTemplate>
                   </asp:TemplateField>
                 
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                       <ItemTemplate>
                          <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" ToolTip="Edit" SkinID="BtnLinkEdit"
                                                            CommandName="Edit" CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                       </ItemTemplate>
                         <EditItemTemplate>
                              <asp:LinkButton ID="BtnUpdate" runat="server" Text="Update" SkinID="BtnLinkUpdate"
                                                                     ToolTip="Update" CommandName="Update" CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                             <asp:LinkButton ID="BtnCancel" runat="server" Text="Cancel" ToolTip="Cancel" SkinID="BtnLinkCancel"
                                                                   CommandName="Cancel" CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                         </EditItemTemplate>
                         <FooterTemplate>
                             <asp:LinkButton ID="BtnInsert" runat="server" Text="Add" SkinID="BtnLinkAdd" ToolTip="Add" CommandName="Add"></asp:LinkButton>
                         </FooterTemplate>
                   </asp:TemplateField>
                       <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Visibility">
                        <ItemTemplate>
                            <asp:Label ID="lblid" runat="server" Visible="false" Text='<%# Bind("id") %>'></asp:Label>
                            <asp:Label ID="lblcheckBoxStatus" runat="server" Visible="false" Text='<%# Bind("Visibility") %>'></asp:Label>
                            <asp:CheckBox ID="chkBox" runat="server" Enabled="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="ImgBtnUP" runat="server" ToolTip="Up" CommandName="Up" SkinID="BtnLinkUp"
                                                                                                CommandArgument='<%#Bind("id") %>'></asp:LinkButton>
                            <asp:LinkButton ID="ImgBtnDown" runat="server" ToolTip="Down" CommandName="Down"
                                                                 CommandArgument='<%#Bind("id")%>' SkinID="BtnLinkDown"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnDelete" runat="server"
                                 OnClientClick="return confirm('Are you sure you would like to delete the column? There may be data in the database that is associated with this field.');"
                                                           CommandName="Delete1" CommandArgument='<%#Bind("id") %>' SkinID="BtnLinkDelete"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
   </div>
	</div>
</div>

<table>
    <tr runat="server" id="pnlCustomer" visible="false">
        <td>
            Customer
        </td>
        <td>
               <asp:DropDownList ID="ddlcustomer" runat="server" AutoPostBack="true"
                                      OnSelectedIndexChanged="ddlcustomer_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqc" runat="server" ControlToValidate="ddlcustomer" InitialValue="0" ValidationGroup="G1"
                                ErrorMessage="Please select customer">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    </table>
 




