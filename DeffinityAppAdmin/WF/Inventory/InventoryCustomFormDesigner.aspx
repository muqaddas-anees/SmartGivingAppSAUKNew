<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
     Inherits="InventoryCustomFormDesigner" EnableEventValidation="false" Codebehind="InventoryCustomFormDesigner.aspx.cs" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
      <ul class="tabs_list5" style="float:right;">
    <li class="current5"><a href="InventoryManagerPage.aspx?status=0&project.aspx" target="_self"><span>Back to Inventory</span></a></li>
      </ul>
    
    <script type="text/javascript">
        //CheckboxList Single selection using jquery
        $(document).ready(function () {
            var checkboxes = $('#<%=chkListPostion.ClientID %>').find('input:checkbox');
            checkboxes.click(function () {
                var selectedIndex = checkboxes.index($(this));

                var items = $('#<% = chkListPostion.ClientID %> input:checkbox');
                for (i = 0; i < items.length; i++) {
                    if (i == selectedIndex)
                        items[i].checked = true;
                    else
                        items[i].checked = false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <h1 class="section1">
                    <span>Custom Form Designer</span></h1>
            </td>
        </tr>
        <tr>
            <td class="p_section1 data_carrier_block">
                <table width="50%">
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="form" DisplayMode="BulletList" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCustomer" runat="server" Width="250px" ClientIDMode="Static"
                                OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCustomer"
                                InitialValue="0" ErrorMessage="Please select customer" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdCustomer" runat="server" TargetControlID="ddlCustomer"
                                BehaviorID="ccdCom" Category="company" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/webservices/DCServices.asmx" ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
                        </td>
                    </tr>
                </table>
                <div class="clr">
                </div>
                <div style="width: 100%">
                    <asp:GridView ID="gvForm" runat="server" Width="100%" OnRowCommand="gvForm_RowCommand"
                        EmptyDataText="No fields found" OnRowDeleting="gvForm_RowDeleting" OnRowEditing="gvForm_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="Linkedit" runat="server" CausesValidation="false" CommandName="Edit"
                                        CommandArgument='<%# Bind("ID")%>' ImageUrl="~/media/ico_edit.png" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Label">
                                <ItemTemplate>
                                    <asp:Label ID="lblLabel" runat="server" Text='<% #Bind("LabelName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfField" runat="server" Text='<% #Bind("TypeOfField") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Values">
                            <ItemStyle Width="400px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblValues" runat="server" Text='<% #Eval("ListValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Default Text">
                                <ItemTemplate>
                                    <asp:Label ID="lblDefaultText" runat="server" Text='<% #Eval("DefaultText") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Minimum Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinimumValue" runat="server" Text='<% #Eval("MinimumValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Maximum Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaximumValue" runat="server" Text='<% #Eval("MaximumValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mandatory">
                                <ItemTemplate>
                                    <asp:Label ID="lblMandatory" runat="server" Text='<% #Eval("Mandatory").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Position">
                                <ItemTemplate>
                                    <asp:Label ID="lblPosition" runat="server" Text='<% #Bind("FieldPosition") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                        SkinID="ImgSymDel" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                </ItemTemplate>
                                <FooterStyle Width="45px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="clr">
                </div>
                <br />
                <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 100%;">
                    Add / Update Field
                </div>
                <table width="500px">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Select Type of Field
                            <br />
                            <asp:DropDownList ID="ddlTypeOfField" runat="server" Width="170px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTypeOfField_SelectedIndexChanged">
                                <asp:ListItem Value="Text Box" Text="Text Box"> </asp:ListItem>
                                <asp:ListItem Value="Text Area" Text="Text Area"> </asp:ListItem>
                                <%--  <asp:ListItem Value="Instruction" Text="Instruction"> </asp:ListItem>--%>
                                <asp:ListItem Value="Dropdown List" Text="Dropdown List"> </asp:ListItem>
                                <asp:ListItem Value="Radio Button" Text="Radio Button"> </asp:ListItem>
                                <asp:ListItem Value="Checkbox" Text="Checkbox"> </asp:ListItem>
                                <asp:ListItem Value="Date" Text="Date"> </asp:ListItem>
                                <asp:ListItem Value="Number Field" Text="Number Field"> </asp:ListItem>
                                <asp:ListItem Value="Url" Text="Url"> </asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLableName" runat="server" Text="Label"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtLabelName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLabel" runat="server" ControlToValidate="txtLabelName"
                                ErrorMessage="Please enter label" ValidationGroup="form" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblDefaultText" runat="server" Text="Default Value/Text"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtDefaultText" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMinimumValue" runat="server" Text="Minimum Value"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtMinimumValue" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMin" runat="server" ControlToValidate="txtMinimumValue"
                                ErrorMessage="Please enter minimum value" Display="None" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvMin" runat="server" ControlToValidate="txtMinimumValue"
                                Display="None" ErrorMessage="Please enter valid mimimum value" Operator="DataTypeCheck"
                                Type="Double" SetFocusOnError="true" ValidationGroup="form">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblMaximumValue" runat="server" Text="Maximum Value"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtMaximumValue" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMax" runat="server" ControlToValidate="txtMaximumValue"
                                ErrorMessage="Please enter maximum value" Display="None" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvMax" runat="server" ControlToValidate="txtMaximumValue"
                                Display="None" ErrorMessage="Please enter valid maximum value" Operator="DataTypeCheck"
                                Type="Double" SetFocusOnError="true" ValidationGroup="form">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkMandatoryField" runat="server" />
                            <br />
                            <asp:Label ID="lblMandatoryField" runat="server" Text="Mandatory field"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblListValues" runat="server" Text="List Values"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtListValues" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <%--     <tr>
                        <td colspan="2">
                            Select postion on this page within the custom selection
                        </td>
                    </tr>--%>
                    <asp:Panel ID="pnlPosition" runat="server" Visible="false">
                        <tr>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chkListPostion" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="28">
                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="F" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="G" Value="G"></asp:ListItem>
                                    <asp:ListItem Text="H" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="J" Value="J"></asp:ListItem>
                                    <asp:ListItem Text="K" Value="K"></asp:ListItem>
                                    <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="O" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="P" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Q" Value="Q"></asp:ListItem>
                                    <asp:ListItem Text="R" Value="R"></asp:ListItem>
                                    <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="T" Value="T"></asp:ListItem>
                                    <asp:ListItem Text="U" Value="U"></asp:ListItem>
                                    <asp:ListItem Text="V" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="W" Value="W"></asp:ListItem>
                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:HiddenField ID="hfId" runat="server" Value="0" />
                            <asp:ImageButton ID="imgAdd" runat="server" SkinID="ImgAdd" OnClick="imgAdd_Click"
                                ValidationGroup="form" />&nbsp;
                            <asp:ImageButton ID="imgUpdate" runat="server" SkinID="ImgUpdate" OnClick="imgUpdate_Click"
                                ValidationGroup="form" />&nbsp;
                            <asp:ImageButton ID="imgCancel" runat="server" SkinID="ImgCancel" OnClick="imgCancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


