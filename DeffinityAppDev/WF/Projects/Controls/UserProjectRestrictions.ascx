<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_UserProjectRestrictions" Codebehind="UserProjectRestrictions.ascx.cs" %>

<div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-12 control-label"> The following Customers have permissions to view this project in the Customer Portal</label>
				</div>
</div>
<div class="form-group">
                                  <div class="col-md-8">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.SelectCustomer%></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_50" >
                                       </asp:DropDownList> &nbsp; <asp:Button ID="btnAllowView" runat="server" SkinID="btnApply" 
        onclick="btnAllowView_Click" />
					</div>
				</div>
</div>
<div class="form-group">
                                  <div class="col-md-6">
<asp:GridView ID="grdUsersPermissions" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="ID" HorizontalAlign="Left"  
        GridLines="None" BorderStyle="None" CellPadding="0" CellSpacing="1" 
             Width="100%" AllowPaging="True" BorderWidth="0px"  
        EmptyDataText="No data exist." 
        onpageindexchanging="grdUsersPermissions_PageIndexChanging" 
        onrowcommand="grdUsersPermissions_RowCommand" 
         PageSize="10" onrowdeleting="grdUsersPermissions_RowDeleting" >
                 
            <Columns>                
            <asp:TemplateField Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblID" runat="server" Text ='<%# Bind ("ID") %>'  Visible="false"></asp:Label>
            </ItemTemplate>
            
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Customer Name" SortExpression="Customer Name">
            <ItemTemplate>
                <asp:Label ID="lblCName" runat="server" Text='<%# Bind("ContractorName") %>' ></asp:Label>
            </ItemTemplate>
            <HeaderStyle CssClass="header_bg_l" />
            </asp:TemplateField>
         
                
                
                <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" Width="40px" /> 
                        <ItemTemplate >
                            <asp:LinkButton ID="deletebut" runat="server" CommandName="delete" SkinID="BtnLinkDelete" Enabled="<%#CommandField()%>"
                            OnClientClick="return confirm('Do you want to delete the record?');" ToolTip="Delete"
                            Visible="True" CommandArgument='<%# Eval("ID")%>' />
                            </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
        </asp:GridView>
                                      </div>
    </div>