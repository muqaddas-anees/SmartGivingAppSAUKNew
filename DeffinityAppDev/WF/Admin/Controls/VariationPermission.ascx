<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_VariationPermission" Codebehind="VariationPermission.ascx.cs" %>
<asp:UpdateProgress ID="uprogress1" runat="server">
    <ProgressTemplate>
        <asp:Label ID="imgLoad" SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="form-group">
            <asp:Label ID="lblmsg" runat="server" EnableViewState="false"></asp:Label>
            <asp:ValidationSummary ID="vsPermission" runat="server" ValidationGroup="v" DisplayMode="BulletList" />
        </div>
        
            <div class="form-group">
                 <div class="col-md-12">
                <div class="col-sm-3 control-label">
                    Manager
                </div>
                <div class="col-sm-9">
                    <asp:DropDownList ID="ddlManager" runat="server" SkinID="ddl_80" OnSelectedIndexChanged="ddlManager_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvManager" runat="server" ErrorMessage="Please select manager"
                        ControlToValidate="ddlManager" InitialValue="0" ValidationGroup="v">*</asp:RequiredFieldValidator>
                </div>
                     </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                   <p>This Manager can sign-off variations for the following users</p> 
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                <div class="col-sm-3 control-label">
                    User
                </div>
                <div class="col-sm-9 form-inline">
                    <asp:DropDownList ID="ddlUser" runat="server" SkinID="ddl_80">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="Please select user"
                        ControlToValidate="ddlUser" InitialValue="0" ValidationGroup="v">*</asp:RequiredFieldValidator>
                    <asp:Button ID="imgAssignUser" runat="server" AlternateText="Add"
                        SkinID="btnAdd" ValidationGroup="v" OnClick="imgAssignUser_Click"  />
                </div>
                    </div>
            </div>
        
        <asp:GridView ID="gvAssignedUser" runat="server" EmptyDataText="No data found!" Width="100%"
            OnRowCommand="gvAssignedUser_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAssignedUser_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="ID" Visible="false" />
                <asp:BoundField DataField="ManagerName" HeaderText="Manager" />
                <asp:BoundField DataField="UserName" HeaderText="User" />
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="deletebut" runat="server" CommandName="Deleterow" SkinID="BtnLinkDelete"
                            OnClientClick="return confirm('Do you want to delete this record?');" ToolTip="delete"
                            CommandArgument='<%# Bind("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgAssignUser" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="ddlManager" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="gvAssignedUser" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
