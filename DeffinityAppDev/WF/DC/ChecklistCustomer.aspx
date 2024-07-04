<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DC_ChecklistCustomer" EnableEventValidation="false" Codebehind="ChecklistCustomer.aspx.cs" %>
<%@ Register Src="~/WF/DC/controls/PermitCustomerTab.ascx" TagPrefix="uc1" TagName="PermitCustomerTab" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:PermitCustomerTab runat="server" id="PermitCustomerTab" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="panel_options" Runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=permittowork">
<i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
        <asp:GridView ID="gvchecklist" runat="server" Width="100%" 
            EmptyDataText="No Data Found" 
            onrowcancelingedit="gvchecklist_RowCancelingEdit" 
            onrowediting="gvchecklist_RowEditing" AutoGenerateColumns="False" 
            DataKeyNames="ID" AllowPaging="True" 
            onpageindexchanging="gvchecklist_PageIndexChanging" PageSize="10" 
            onrowupdating="gvchecklist_RowUpdating" 
            onrowcommand="gvchecklist_RowCommand" onrowdatabound="gvchecklist_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Item Description">
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" Width="600px"></asp:TextBox>
                </EditItemTemplate>                    
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="55%" />
                    <FooterTemplate>
                    <asp:TextBox ID="txtDescriptionFooter" runat="server" Width="600px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>                       
                        <asp:DropDownList ID="ddlstatus" runat="server" Width="150px">
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>In Progress</asp:ListItem>
                            <asp:ListItem>Closed</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
                    <FooterTemplate>                                     
                        <asp:DropDownList ID="ddlstatusFooter" runat="server" Width="150px">
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>In Progress</asp:ListItem>
                            <asp:ListItem>Closed</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ClosedDate" HeaderText="Closed Date" 
                    NullDisplayText="------" ReadOnly="True" >              
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:TemplateField>
                    <EditItemTemplate>
                     <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="update" Text="Update"
                            CommandArgument='<%# Bind("CallID")%>' SkinID="BtnLinkUpdate"
                            ToolTip="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                    <FooterTemplate>
                        <asp:LinkButton ID="LinkButtonInsert" runat="server" CommandName="Insert" Text="Insert"
                            ValidationGroup="group1" SkinID="BtnLinkAdd" ToolTip="Insert"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel"  ToolTip="Cancel"></asp:LinkButton>
                    
</FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </ContentTemplate> </asp:UpdatePanel>

</asp:Content>

