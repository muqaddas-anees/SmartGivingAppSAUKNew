<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CCCategoryCtrl" Codebehind="CCCategoryCtrl.ascx.cs" %>
<asp:UpdateProgress ID="uprogress1" runat="server" AssociatedUpdatePanelID="upCat">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server" ID="lblloading"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upCat" runat="server" UpdateMode="Conditional">
    
    <ContentTemplate>
         <div class="sec_header" style="width: 550px">
           Category </div>
        <div>
        <table cellpadding="0" cellspacing="0" width="510px">
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="vsForCategory" runat="server" ValidationGroup="c" DisplayMode="BulletList" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="s" DisplayMode="BulletList" />
        </td>
    </tr>
    <tr>
        <td>Customer
        </td>
        <td>
            <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True"
                Width="200px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select customer" SetFocusOnError="true" ValidationGroup="c" ControlToValidate="ddlCustomer" InitialValue="0">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" ValidationGroup="Submit" DataSourceID="objCategory"
                DataTextField="CategoryName" DataValueField="ID" Width="200px">
            </asp:DropDownList>
            <asp:Button ID="btn_popup2" runat="server" SkinID="btnAdd" AlternateText="Add category"
                ValidationGroup="c" OnClick="btn_popup2_Click"  />&nbsp;
            <asp:LinkButton ID="btnDeleteCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete category, associated sub category and item(s)?');"
                    SkinID="BtnLinkDelete" ImageAlign="AbsMiddle" OnClick="btnDeleteCategory_Click" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory" ValidationGroup="s" ErrorMessage="Please select category" InitialValue="0">*</asp:RequiredFieldValidator>
            <label id="imgCat" runat="server" style="display: none"></label>
            <asp:ObjectDataSource ID="objCategory" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetData" TypeName="DeffinityManager.DAL.DBChangeControlTableAdapters.dtCategoryTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCustomer" DefaultValue="0" Name="ID" PropertyName="SelectedValue"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblSubcategory" Text="Sub Category" runat="server"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlSubcategory" runat="server" SkinID="ddl_80">
            </asp:DropDownList>
            <asp:Button ID="imgsubcat" runat="server" AlternateText="Add Sub category"
                SkinID="btnAdd" ValidationGroup="s" OnClick="imgsubcat_Click"  />
            <asp:LinkButton ID="imgDeleteSubCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete this item?');"
                SkinID="BtnLinkDelete" ImageAlign="AbsMiddle" OnClick="imgDeleteSubCategory_Click" CssClass="img_align" />
            <label id="imgSubCategory" runat="server" style="display: none"></label>
            <ajaxToolkit:CascadingDropDown ID="casCadSubCategory" runat="server" TargetControlID="ddlSubcategory"
                Category="Task1" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/ServiceMgr.asmx"
                ServiceMethod="GetProjectSubCategory" ParentControlID="ddlCategory" />
        </td>
    </tr>


</table>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="img_mcat_cancel"
    BackgroundCssClass="modalBackground" TargetControlID="imgCat" PopupControlID="pnlMcategory" />
<ajaxToolkit:ModalPopupExtender ID="modleSubcategory" runat="server" CancelControlID="imgSubCancel"
    BackgroundCssClass="modalBackground" TargetControlID="imgSubCategory" PopupControlID="pnlSubcategory" />
<asp:Panel ID="pnlMcategory" runat="server" BackColor="White" Style="display: none"
    Width="350px" BorderStyle="Double" BorderColor="LightSteelBlue">
    <table>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmastercategory"
                    Text="Please enter Category" ErrorMessage="Please enter Category" ForeColor="Red"
                    ValidationGroup="Group11"></asp:RequiredFieldValidator><br />
                Category :<asp:TextBox ID="txtmastercategory" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="imgAddCategory" runat="server" Text="OK"
                    SkinID="btnSubmit" ValidationGroup="Group11" OnClick="imgAddCategory_Click" />
                <asp:Button ID="img_mcat_cancel" runat="server" Text="Close" SkinID="btnCancel" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlSubcategory" runat="server" BackColor="White" Style="display: none"
    Width="350px" BorderStyle="Double" BorderColor="LightSteelBlue">
    <table>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubCategory"
                    Text="Please enter sub Category" ErrorMessage="Please enter sub Category" ForeColor="Red"
                    ValidationGroup="gp_sub"></asp:RequiredFieldValidator><br />
                <asp:Label ID="lblSubCategory1" Text="Sub Category" runat="server"></asp:Label><asp:TextBox ID="txtSubCategory" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="imgAddSubCategory" runat="server" Text="OK"
                    SkinID="btnSubmit" ValidationGroup="gp_sub" OnClick="imgAddSubCategory_Click" />
                <asp:Button ID="imgSubCancel" runat="server" Text="Close" SkinID="btnCancel" />
            </td>
        </tr>
    </table>
</asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>



