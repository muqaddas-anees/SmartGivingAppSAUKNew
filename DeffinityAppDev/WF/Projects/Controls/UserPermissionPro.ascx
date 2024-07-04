<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_UserPermissionPro" Codebehind="UserPermissionPro.ascx.cs" %>
<div class="form-group">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Team" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server"  ValidationGroup="User"/>
</div>
<div class="form-group">
    <asp:Label ID="lblMsg" runat="server" Text="" Visible="false"></asp:Label></div>
 <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Select&nbsp;Group:</label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlTeam" runat="server" Width="120px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  Display="None" ValidationGroup="Team"
                        runat="server" ErrorMessage="Please select team" ControlToValidate="ddlTeam" InitialValue="0">
                        </asp:RequiredFieldValidator>
					                    </div>
				                    </div>
                                        </div>

                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Role:</label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlRole" runat="server" Width="120px">
                       <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                           <%--  <asp:ListItem Value="4" >Administrator</asp:ListItem>--%>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="2" >Manager</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                        </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  Display="None" ValidationGroup="Team"
                        runat="server" ErrorMessage="Please select team role" ControlToValidate="ddlRole" InitialValue="0">
                        </asp:RequiredFieldValidator>
					</div>
				</div>
</div>

<div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
                                           <asp:Button ID="imgRole" runat="server" SkinID="btnApply" 
                            onclick="imgRole_Click" ValidationGroup="Team"/>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                      <asp:GridView ID="grdTeam" Width="100%" runat="server" 
                            AutoGenerateColumns="false" EmptyDataText="No Records Found" 
                            onrowcommand="grdTeam_RowCommand" onrowdeleting="grdTeam_RowDeleting">
                        <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_l" HeaderText="Team Name">



                        <ItemStyle Wrap="True"  />
                        <ItemTemplate>
                            <asp:Label ID="lblTeam" runat="server" Text='<%# Bind("TeamName")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField  HeaderText="Role">
                        
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%#RoleType(DataBinder.Eval(Container.DataItem,"Role").ToString())%>' ></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <%-- <asp:BoundField DataField="Role" HeaderText="Role" />--%>
                         <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                        <ItemStyle HorizontalAlign="Center" Width="40px"  /> 
                        <ItemTemplate >
                            <asp:LinkButton ID="deletebut"  CommandArgument='<%# Bind("ID") %>'  Enabled="<%#CommandField()%>"  runat="server" CommandName="delete" SkinID="BtnLinkDelete"
                            OnClientClick="return confirm('Do you want to delete the record?');" ToolTip="Delete"
                            Visible="True"  />
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
				</div>
</div>
                                    </div>
                                  <div class="col-md-6">

                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Select&nbsp;User:</label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlUser" runat="server" Width="120px">
                        </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  Display="None" ValidationGroup="User"
                        runat="server" ErrorMessage="Please select user" ControlToValidate="ddlUser" InitialValue="0">
                        </asp:RequiredFieldValidator>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Role:</label>
                                      <div class="col-sm-8">  <asp:DropDownList ID="ddlRoleUser" runat="server" Width="120px">
                        <asp:ListItem Value="0" Selected="True">Please select...</asp:ListItem>
                             <%--<asp:ListItem Value="4" >Administrator</asp:ListItem>--%>
                             <asp:ListItem Value="1" >Disabled</asp:ListItem>
                              <asp:ListItem Value="2" >Manager</asp:ListItem>
                              <asp:ListItem Value="3" >Viewer</asp:ListItem>
                        </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  Display="None" ValidationGroup="User"
                        runat="server" ErrorMessage="Please select user role" ControlToValidate="ddlRoleUser" InitialValue="0">
                        </asp:RequiredFieldValidator>
					</div>
				</div>
</div>
                                      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
                                           <asp:Button ID="imgApplyUser" runat="server" SkinID="btnApply" ValidationGroup="User" 
                            onclick="imgApplyUser_Click" />
					</div>
				</div>
</div>
                                      <asp:GridView ID="grdUsers" Width="100%" runat="server" 
                            AutoGenerateColumns="false" EmptyDataText="No Records Found" 
                            onrowcommand="grdUsers_RowCommand" onrowdeleting="grdUsers_RowDeleting">
                        <Columns>
                        <asp:TemplateField HeaderText="User Name">



                        <ItemStyle Wrap="True"  />
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# Bind("ContractorName")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField  HeaderText="Role">
                        
                        <ItemTemplate>
                            <asp:Label ID="lblUserRole" runat="server" Text='<%#RoleType(DataBinder.Eval(Container.DataItem,"Role").ToString())%>' ></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                       
                        <ItemTemplate >
                            <asp:LinkButton ID="deletebut1"  CommandArgument='<%# Bind("ID") %>' Enabled="<%#CommandField()%>"  runat="server" CommandName="delete" SkinID="BtnLinkDelete"
                            OnClientClick="return confirm('Do you want to delete the record?');" ToolTip="Delete"
                            Visible="True" />
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
                                    </div>
  </div>
