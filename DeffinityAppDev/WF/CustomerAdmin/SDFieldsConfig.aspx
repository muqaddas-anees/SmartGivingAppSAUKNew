<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_SDFieldsConfig" Codebehind="SDFieldsConfig.aspx.cs" %>
<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<%@ Register Src="controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Configure Fields - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    <asp:GridView ID="gvConfig" runat="server" Width="100%" OnRowCancelingEdit="gvConfig_RowCancelingEdit" OnRowCommand="gvConfig_RowCommand" OnRowEditing="gvConfig_RowEditing" OnRowUpdating="gvConfig_RowUpdating" OnRowDataBound="gvConfig_RowDataBound">
                    <Columns>
                          <asp:TemplateField>
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
                                        SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Update%>"
                                        ValidationGroup="expenseUpdate"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CausesValidation="false"
                                        CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                                    </asp:LinkButton>
                                </EditItemTemplate>
                             
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Field">
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultField" runat="server" Text='<%# Bind("DefaultName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Instance Field">
                            <ItemTemplate>
                                <asp:Label ID="lblInstanceName" runat="server" Text='<%# Bind("InstanceName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtInstanceName" runat="server" Text='<%# Bind("InstanceName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visible">
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
                        <asp:TemplateField HeaderText="Mandatory">
                            <ItemTemplate>
                                <asp:Label ID="lblIsMandatory" runat="server" Text='<%# Eval("IsMandatory").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:HiddenField ID="hfIsMandatory" runat="server" Value='<%# Eval("IsMandatory") %>' />
                                <asp:DropDownList ID="ddlIsMandatory" runat="server">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                     <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Default Value" >
                            <ItemTemplate>
                                <asp:Label ID="lblDefaultValue" runat="server" Text='<%# Bind("DefaultValue") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:TextBox ID="txtDefaultValue" runat="server" Text='<%# Bind("DefaultValue") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  >
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy to all Customers" CommandArgument="<%# Bind('ID')%>" CommandName="Copy"></asp:LinkButton>
                              
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
<asp:HiddenField ID="HiddenFiled1" runat="server" />
     
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>


