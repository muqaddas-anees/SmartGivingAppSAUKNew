<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProgrammeWorkstream" Codebehind="ProgrammeWorkstream.ascx.cs" %>
<asp:RadioButtonList ID="Radiolist" runat="server" AutoPostBack="true" 
    onselectedindexchanged="Radiolist_SelectedIndexChanged" RepeatDirection="Horizontal">
<asp:ListItem Text="Workstream by Project" Value="1" Selected="True"></asp:ListItem>
<asp:ListItem Text="Workstream by Period" Value="2"></asp:ListItem>
</asp:RadioButtonList>
<asp:HiddenField ID="hid" runat="server" />
<asp:Literal ID="litDisplayHtml" runat="server"></asp:Literal>

