<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_Checklist" EnableEventValidation="false" Codebehind="Checklist.aspx.cs" %>

<%@ Register Src="~/WF/DC/controls/PermitTab.ascx" TagPrefix="Pref" TagName="PermitTab" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
     <%= Resources.DeffinityRes.PermittoWork%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <label id="lblTitle" runat="server" > Checklist
                  </label>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
      <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=PermittoWork" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <Pref:PermitTab runat="server" ID="PermitTab" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  
  <tr>    
    <td class="p_section1 data_carrier_block" style="width:100%">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
        <asp:GridView ID="gvchecklist" runat="server" Width="100%" 
            EmptyDataText="No Data Found" 
            onrowcancelingedit="gvchecklist_RowCancelingEdit" 
            onrowediting="gvchecklist_RowEditing" AutoGenerateColumns="False" 
            DataKeyNames="ID" AllowPaging="True" 
            onpageindexchanging="gvchecklist_PageIndexChanging" PageSize="10" 
            onrowupdating="gvchecklist_RowUpdating" 
            onrowcommand="gvchecklist_RowCommand" 
            onrowdatabound="gvchecklist_RowDataBound">
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
                            ValidationGroup="group1" SkinID="" ToolTip="Insert"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    
</FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </ContentTemplate> </asp:UpdatePanel>
     </td>
  </tr>
    </table>
     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>


