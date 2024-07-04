<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_InventoryBarcode" Codebehind="InventoryBarcode.ascx.cs" %>
<div class="sec_header" style="width: 750px">
    Barcode</div>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="b" DisplayMode="BulletList" />
<asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Green" EnableViewState="false"></asp:Label>
<asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" EnableViewState="false"></asp:Label>
<table>
    <tr>
        <td>
            Customer:
        </td>
        <td>
            <asp:DropDownList ID="ddlCustomer" runat="server" Width="150px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlCustomer"
                InitialValue="0" ValidationGroup="b" ErrorMessage="Please select customer">*</asp:RequiredFieldValidator>
        </td>
        <td>
            Category:
        </td>
        <td>
            <asp:Panel ID="pnlcategory" runat="server">
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" Width="150px"
                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory"
                    InitialValue="0" ValidationGroup="b" ErrorMessage="Please select category">*</asp:RequiredFieldValidator>
                <asp:ImageButton ID="btnaddcategory" OnClick="btnaddcategory_Click" runat="server"
                    SkinID="ImgSymAdd" CausesValidation="False"></asp:ImageButton>
                <asp:ImageButton ID="btn_CategoryEdit" runat="server" ValidationGroup="Edit_catelog"
                    ImageUrl="~/media/ico_edit.png" OnClick="btn_CategoryEdit_Click" ImageAlign="AbsMiddle">
                </asp:ImageButton>
                <asp:ImageButton ID="btnDeleteCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete category,associated sub category and item(s)?');"
                    SkinID="ImgSymDel" OnClick="btnDeleteCategory_Click" />
            </asp:Panel>
            <asp:Panel ID="pnladdcategory" runat="server" Visible="false">
                <asp:TextBox ID="txtAddCategory" runat="server" Width="200px" ValidationGroup="cat1"></asp:TextBox>
                <asp:ImageButton ID="btnSaveCategory" runat="server" ToolTip="Add Category" ValidationGroup="cat1"
                    OnClick="btnSaveCategory_Click" SkinID="ImgSymUpdate"></asp:ImageButton>
                <asp:ImageButton ID="btnCancelCategory" runat="server" ToolTip="Cancel" OnClick="btnCancelCategory_Click"
                    CausesValidation="False" SkinID="ImgSymCancel"></asp:ImageButton>
                <div>
                    <asp:RequiredFieldValidator runat="server" ID="ReqCatname" ControlToValidate="txtAddCategory"
                        SetFocusOnError="true" ErrorMessage="Please enter Category" ForeColor="Red" ValidationGroup="cat1"></asp:RequiredFieldValidator></div>
            </asp:Panel>
            <asp:HiddenField ID="HID_Category" runat="server"></asp:HiddenField>
        </td>
    </tr>
    <tr>
        <td style="width: 100px;">
            Sub Category:
        </td>
        <td>
            <asp:Panel ID="pnlsubcategory" runat="server" Width="250px">
                <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" 
                    Width="150px" onselectedindexchanged="ddlSubCategory_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubCategory"
                    InitialValue="0" ValidationGroup="b" ErrorMessage="Please select subcategory">*</asp:RequiredFieldValidator>
                <asp:ImageButton ID="btnaddsubcategory" OnClick="btnaddsubcategory_Click" runat="server"
                    SkinID="ImgSymAdd" ValidationGroup="cat0"></asp:ImageButton>
                <asp:ImageButton ID="btn_editSubCategory" runat="server" ValidationGroup="Edit_subcatelog"
                    ImageUrl="~/media/ico_edit.png" OnClick="btn_editSubCategory_Click" ImageAlign="AbsMiddle">
                </asp:ImageButton>
                <asp:ImageButton ID="btnSubCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete Sub category and associated item(s)?');"
                    OnClick="btnSubCategory_Click" SkinID="ImgSymDel" Style="width: 18px" />
            </asp:Panel>
            <asp:Panel ID="pnladdsubcategory" runat="server" Visible="false">
                <asp:TextBox ID="txtAddSubCategory" runat="server" Width="200px" ValidationGroup="Subcat1"></asp:TextBox>
                <asp:ImageButton ID="btnSaveSubCategory" runat="server" ToolTip="Add SubCategory"
                    ValidationGroup="Subcat1" OnClick="btnSaveSubCategory_Click" SkinID="ImgSymUpdate">
                </asp:ImageButton>
                <asp:ImageButton ID="btnCancelSubCategory" runat="server" ToolTip="Cancel" OnClick="btnCancelSubCategory_Click"
                    SkinID="ImgSymCancel"></asp:ImageButton>
                <div>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAddSubCategory"
                        ErrorMessage="Please enter sub Category" ForeColor="Red" ValidationGroup="Subcat1"></asp:RequiredFieldValidator></div>
            </asp:Panel>
            <asp:HiddenField ID="HID_SubCategory" runat="server"></asp:HiddenField>
        </td>
        <td>
            Associated Barcode
        </td>
        <td>
            <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvBarcode" runat="server" ControlToValidate="txtBarcode"
                ErrorMessage="Please enter barcode" ValidationGroup="b">*</asp:RequiredFieldValidator>
            &nbsp;<asp:ImageButton ID="imgSaveItems" runat="server" SkinID="ImgSave" OnClick="imgSaveItems_Click"
                ImageAlign="AbsMiddle" ValidationGroup="b" />
        </td>
    </tr>
</table>
<div>
    <asp:Label ID="lblGridMsg" runat="server" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvBarcode" runat="server" Width="750px" OnRowCancelingEdit="gvBarcode_RowCancelingEdit"
        OnRowCommand="gvBarcode_RowCommand" OnRowEditing="gvBarcode_RowEditing" 
        onrowupdating="gvBarcode_RowUpdating" AllowPaging="true" PageSize="10" 
        onpageindexchanging="gvBarcode_PageIndexChanging" EmptyDataText="No records found!" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                        CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes, Edit%>">
                    </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes,Update %>"
                                CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkUpdate" CausesValidation="true"
                                ValidationGroup="Group1" ToolTip="<%$ Resources:DeffinityRes,Update %>"></asp:LinkButton></div>
                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes, Cancel%>">
                            </asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Customer">
                <ItemTemplate>
                    <asp:Label ID="lblCustomer" runat="server" Text="<%# Bind('Customer') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:Label ID="lblCategory" runat="server" Text="<%# Bind('Category') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sub Category">
                <ItemTemplate>
                    <asp:Label ID="lblSubCategory" runat="server" Text="<%# Bind('SubCategory') %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Barcode">
                <ItemTemplate>
                    <asp:Label ID="lblBarcode" runat="server" Text="<%# Bind('Barcode') %>"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtBarcode" runat="server" Text="<%# Bind('Barcode') %>"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="70px" />
                <ItemTemplate>
                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete1"
                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
</ContentTemplate>

</asp:UpdatePanel>
