<%@ Control Language="C#" AutoEventWireup="true" Inherits="ProjectSalesStaff" Codebehind="ProjectSalesStaff.ascx.cs" %>
<asp:UpdatePanel ID="up1"  runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="form-group">
             <div class="col-md-12">
                 <asp:Label ID="lblmsg" runat="server" EnableViewState="false"></asp:Label>
</div>
</div>


 <asp:UpdateProgress ID="uProgress" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="imgloading" runat="server" SkinID="Loading" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Sales Staff</label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:DropDownList ID="ddlSalesStaff" runat="server" SkinID="ddl_80">
    </asp:DropDownList>
    <asp:Button ID="imb_AddSite" runat="server" SkinID="btnAdd"
        OnClick="imb_AddSite_Click"  />
					</div>
				</div>
                </div>
    
    <asp:GridView ID="gvSalesStaff" runat="server" Width="100%" 
        AllowPaging="true"  EmptyDataText="No records exist"
        PageSize="10" onpageindexchanging="gvSalesStaff_PageIndexChanging" 
        onrowcommand="gvSalesStaff_RowCommand" 
        onrowdeleting="gvSalesStaff_RowDeleting"  >
        <Columns>
            <asp:BoundField DataField="SalesStaff" HeaderText="Sales Staff" ItemStyle-Width="90%" />
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                    <asp:LinkButton ID="imgDelete" OnClientClick="return confirm('Do you want to delete the item?');"
                        ToolTip="delete" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID")%>' runat="server"
                        CommandName="Deleterow" />
                        
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</ContentTemplate></asp:UpdatePanel>
