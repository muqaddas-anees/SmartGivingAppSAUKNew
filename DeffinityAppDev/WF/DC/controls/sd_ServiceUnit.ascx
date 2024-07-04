<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Servicedesk_sdcontrols_sd_ServiceUnit"  Codebehind="sd_ServiceUnit.ascx.cs" %>
 <div class="form-group row">
<asp:ValidationSummary ID="IncidentValidations" runat="server" ValidationGroup="Incident"
    Width="100%" />

<asp:Label ID="lblerrormsg" Text="" runat="server" ForeColor="Red"></asp:Label>
     </div>
<table style="width: 100%;">
    <tr>
        <td style="width: 5%;">
            <asp:Label ID="lbltype" runat="server" Text="SR Type"></asp:Label>
        </td>
        <td style="width: 12%;">
            <asp:DropDownList ID="ddlSRType" runat="server" Width="120px" OnSelectedIndexChanged="ddlSRType_SelectedIndexChanged"
                AutoPostBack="True">
                <asp:ListItem Text="Please Select.." Value="0" />
                <asp:ListItem Text="Fault" Value="Fault" />
                <asp:ListItem Text="Service Request" Value="Service Request" />
                <asp:ListItem Text="Change Control" Value="Change Control" />
                <asp:ListItem Text="FLS" Value="FLS" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rqdDDLType" runat="server" ControlToValidate="ddlSRType"
                Display="Dynamic" ErrorMessage="Please select SR Type" InitialValue="0" Text="*"
                ValidationGroup="Incident" />
        </td>
        <td style="width: 8%;">
            Type of Hours
        </td>
        <td style="width: 21%;">
            <asp:DropDownList ID="ddlTypeOfHours" runat="server" Width="230px" OnSelectedIndexChanged="ddlTypeOfHours_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTypeOfHours" runat="server" ControlToValidate="ddlTypeOfHours"
                ErrorMessage="Please select Type of hours" Text="*" ValidationGroup="Incident"
                InitialValue="0" />
        </td>
        <td style="width: 6%;">
            Category
        </td>
        <td style="width: 12%;">
            <asp:DropDownList ID="ddlmastercategory" runat="server" DataValueField="ID" DataTextField="Name"
                AutoPostBack="True" OnSelectedIndexChanged="ddlmastercategory_SelectedIndexChanged"
                Width="120px">
                <asp:ListItem Value="0" Text=" Please select..."></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="ReqMaster1" runat="server" Text="*" InitialValue="0"
                ErrorMessage="Please select category" ControlToValidate="ddlmastercategory" ValidationGroup="Incident"></asp:RequiredFieldValidator>
        </td>
        <td style="width: 8%;">
            Sub Category
        </td>
        <td style="width: 12%;">
            <asp:DropDownList ID="ddlCategory" runat="server" >
            </asp:DropDownList>
          
        </td>
        <td style="width: 6%;">
            Qty Used
        </td>
        <td style="width: 10%;">
            <asp:TextBox ID="txtQtyUsed" runat="server" SkinID="txt_50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvQtyUsed" ControlToValidate="txtQtyUsed" runat="server"
                Text="*" ErrorMessage="Please enter Qty Used" ValidationGroup="Incident"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmpRate11" runat="server" 
                         ControlToValidate="txtQtyUsed"
                         ErrorMessage="Qty Used must be an integer" Operator="DataTypeCheck" 
                         SetFocusOnError="True" Text="*" Type="Double" 
                         ValidationGroup="Incident" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp; Notes
        </td>
        <td colspan="5">
            <asp:TextBox ID="txtNotes" runat="server" SkinID="txtMulti"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td colspan="5">
            <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit"
                OnClick="btnSubmit_Click" ValidationGroup="Incident" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Grid"
    Width="100%" />
<div id="sd">
    
    <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowEditing="GridView1_RowEditing" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" CommandArgument='<%# Bind("ID")%>'
                        SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update1" Text="Update"
                        ValidationGroup="Grid" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate"
                        ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <HeaderStyle CssClass="header_bg_l" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Applied" InsertVisible="False">
                
                <ItemTemplate>
                    <asp:Label ID="lbldateapplied" runat="server" Text='<%# Bind("DateApplied", "{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SRType">
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlSrtype" runat="server">
                        <asp:ListItem Text="Fault" Value="Fault" />
                        <asp:ListItem Text="Service Request" Value="Service Request" />
                        <asp:ListItem Text="Change Control" Value="Change Control" />
                        <asp:ListItem Text="FLS" Value="FLS" />
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblsrtype" runat="server" Text='<%# Bind("SRType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type of Hours">
                <%--<HeaderStyle Width="240px" />
                <ItemStyle Width="240px" />--%>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlTypeofhours" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlTypeofhours"
                        ErrorMessage="Please Select Type Of Hours" Text="*" ValidationGroup="Grid" InitialValue="0" />
                    <ajaxToolkit:CascadingDropDown ID="casCadTypeofhour" runat="server" TargetControlID="ddlTypeofhours"
                        Category="Task2" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                        ServiceMethod="GetTypeOfHours" ParentControlID="ddlSrtype" ContextKey='<%# Bind("PortfolioID") %>'
                        UseContextKey="true" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTypeOfhurs" runat="server" Text='<%# Bind("TypeOfHours") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">
               <%-- <HeaderStyle Width="160px" />
                <ItemStyle Width="160px" />--%>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlmastercategory" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvcategory" runat="server" ControlToValidate="ddlmastercategory"
                        ErrorMessage="Please Select Category" Text="*" ValidationGroup="Grid" InitialValue="0" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sub Category">
                <%--<HeaderStyle Width="160px" />
                <ItemStyle Width="160px" />--%>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="rfvsubcategory" runat="server" ControlToValidate="ddlCategory"
                        ErrorMessage="Please Select Sub Category" Text="*" ValidationGroup="Grid" InitialValue="0" />--%>
                    <ajaxToolkit:CascadingDropDown ID="casCadSubCattegory" runat="server" TargetControlID="ddlCategory"
                        Category="Task1" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                        ServiceMethod="GetSubCategory" ParentControlID="ddlmastercategory" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty Of Units">
               <%-- <ItemStyle Width="150px" />--%>
                <EditItemTemplate>
                    <asp:TextBox ID="txtQtyUsed" runat="server" Text='<%# Bind("QtyUsed") %>' Width="80px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvqty" runat="server" ControlToValidate="txtQtyUsed"
                        ErrorMessage="Please Enter Qty Of Units" Text="*" ValidationGroup="Grid" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("QtyUsed") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes">
             <HeaderStyle Width="250px" />
                <ItemStyle Width="250px" />
                <EditItemTemplate>
                    <asp:TextBox ID="txtNotes" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Applied By">
                <EditItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
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
