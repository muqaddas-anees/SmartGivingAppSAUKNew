<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_InventoryConfigureFields" Codebehind="InventoryConfigureFields.ascx.cs" %>


<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.ConfigureFields%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>


<div class="form-group">
      <div class="col-md-12">
           <asp:Label ID="lblmsg" runat="server" ForeColor="Green"></asp:Label>
	</div>

</div>
<div class="form-group" id="pnlCustomer" runat="server" visible="false">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                 <asp:DropDownList ID="ddlCustomer" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	
</div>


<asp:GridView ID="gvInventoryConfig" runat="server" Width="100%" OnRowCancelingEdit="gvInventoryConfig_RowCancelingEdit" OnRowCommand="gvInventoryConfig_RowCommand" OnRowDataBound="gvInventoryConfig_RowDataBound" OnRowEditing="gvInventoryConfig_RowEditing" OnRowUpdating="gvInventoryConfig_RowUpdating">
                    <Columns>
                          <asp:TemplateField >
                                <HeaderStyle Width="54px" />
                                <ItemStyle Width="54px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Linkedit" runat="server" CausesValidation="false"
                                         CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                                        SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkUpdate" runat="server" CommandName="Update"
                                        Text="<%$ Resources:DeffinityRes,Update%>" CommandArgument='<%# Bind("ID")%>'
                                         SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>"
                                        ValidationGroup="expenseUpdate"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CausesValidation="false"
                                        CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                    </asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Name%>">
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultField" runat="server" Text='<%# Bind("DefaultName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                    <asp:Label ID="lblDefaultFieldEdit" runat="server" Text='<%# Bind("DefaultName") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Visible%>" >
                            <ItemTemplate>
                                <asp:Label ID="lblIsVisible" runat="server" Text='<%# Eval("IsVisible").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="hfIsVisible" runat="server" Value='<%# Eval("IsVisible") %>' />
                                <asp:DropDownList ID="ddlIsVisible" runat="server">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                     <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  Visible="false">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy to all Customers"
                                     CommandArgument='<%# Bind("ID")%>' CommandName="Copy"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

