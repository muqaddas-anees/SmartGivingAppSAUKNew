<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ProjectTaskRAGStatusConfig" Codebehind="ProjectTaskRAGStatusConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label runat="server" ID="lblErrMsg" Visible="false"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" runat="server" ValidationGroup="Group1" />
    <table width="100%" border="0">
        <tr>
            <td>
                Tasks
            </td>
            <td>
                <asp:DropDownList ID="ddlltTasks" runat="server" Width="150" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlltTasks_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlltTasks"
                    runat="server" Display="None" ErrorMessage="Please select task to update" InitialValue="0"
                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div>
        <div>
            <b>Task Alerts</b></div>
        <asp:Panel ID="Panel3" runat="server" Width="98%" BorderStyle="Solid" BorderWidth="1"
            BorderColor="Gray" >
            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-width: 0px;">
                <tr>
                    <td>
                        <img src="images/indcate_red.png" alt="Red" style="display:none;" />
                    </td>
                    <td>
                        Flag as Red: when task is due in
                    </td>
                    <td>
                        <asp:TextBox ID="txtRedDays" runat="server" Width="40px" MaxLength="10" SkinID="txt_90"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRed" runat="server" ControlToValidate="txtRedDays"
                            SetFocusOnError="true" ValidationGroup="Group1" ErrorMessage="Please enter days">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filter_RedDays" runat="server" TargetControlID="txtRedDays"
                            ValidChars="0123456789" />
                    </td>
                    <td>
                        days and less than
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRedPercent" runat="server" SkinID="ddl_90">
                            <asp:ListItem Text="100" Value="100" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        % complete
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="images/indcate_yellow.png" alt="Amber" style="display:none;" />
                    </td>
                    <td>
                        Flag as Amber: when task is due in
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmberDays" runat="server" Width="40px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAmber" runat="server" ControlToValidate="txtAmberDays"
                            SetFocusOnError="true" ValidationGroup="Group1" ErrorMessage="Please enter days">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            TargetControlID="txtAmberDays" ValidChars="0123456789" />
                    </td>
                    <td>
                        days and less than
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAmberPercent" runat="server">
                            <asp:ListItem Text="100" Value="100" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="75" Value="75"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        % complete
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="clr"></div>
        <br />
    <div style="text-align: center">
        <asp:Button ID="btnUpdateItem" runat="server" SkinID="btnSubmit" OnClick="btnUpdateItem_Click"
            ValidationGroup="Group1" />
        &nbsp;&nbsp;
        <asp:Button ID="ImageButton2" runat="server" SkinID="btnCancel" OnClick="ImageButton2_Click" />
    </div>
</asp:Content>
