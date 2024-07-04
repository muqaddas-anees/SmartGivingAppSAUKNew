<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trAdminNotification" Codebehind="trAdminNotification.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register src="controls/TrainingSubTabs.ascx" tagname="TrainingSubTabs" tagprefix="uc2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
  <%= Resources.DeffinityRes.DepartmentNotification%>
        <uc2:trainingsubtabs ID="TrainingSubTabs2" runat="server" />
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="form-group">
        <asp:Label ID="lblException" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
    </div>
    <div class="form-group">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
         <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
    </div>
    <div class="form-group">
        <div class="col-xs-2">
            <label><%= Resources.DeffinityRes.MainTrainingAdministrator%></label>
        </div>
        <div class="col-xs-10">
             <asp:DropDownList ID="ddlUserList" runat="server" AutoPostBack="True" SkinID="ddl_30"
                  onselectedindexchanged="ddlUserList_SelectedIndexChanged"></asp:DropDownList> 
        </div>
    </div>
    <div class="form-group">
<asp:GridView ID="Grid_DepartmentUsers" runat="server" Width="100%" 
        Font-Size="X-Small" EmptyDataText="No records found" 
        onrowcommand="Grid_DepartmentUsers_RowCommand" 
        onrowdatabound="Grid_DepartmentUsers_RowDataBound" 
        onrowcancelingedit="Grid_DepartmentUsers_RowCancelingEdit" 
        onrowediting="Grid_DepartmentUsers_RowEditing" 
        onrowupdating="Grid_DepartmentUsers_RowUpdating" 
        onrowdeleting="Grid_DepartmentUsers_RowDeleting" DataKeyNames="ID">
<Columns>
 <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                <ItemTemplate>
                                     <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                         CommandArgument='<%#Bind("ID")%>' ToolTip="Edit" SkinID="BtnLinkEdit" />
                                </ItemTemplate>
                                 <EditItemTemplate>
             
                  <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate"
                                              ToolTip="Update" CausesValidation="true" ValidationGroup="Group2"></asp:LinkButton>
                   <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" SkinID="BtnLinkCancel"
                        ToolTip="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                        <%-- <asp:ImageButton ID="LinkButtonAdd" runat="server" CausesValidation="true" ValidationGroup="Group2" CommandName="AddNew" 
                CommandArgument="<%#Bind('ID')%>" ToolTip="Edit" ImageUrl="~/media/btn_add_new.gif" />--%>
                 <asp:LinkButton ID="LinkButtonAddNew" runat="server" CommandName="AddNew" 
                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkAdd"
                                     ToolTip="AddNew" CausesValidation="true" ValidationGroup="Group1"></asp:LinkButton>
                              <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" SkinID="BtnLinkCancel"
                                   CommandName="Clear" ToolTip="Cancel"></asp:LinkButton>
                                        </FooterTemplate>
                                </asp:TemplateField>
<asp:TemplateField HeaderText="Department">
<ItemTemplate>
    <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
    </asp:DropDownList>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDepartment"
     ErrorMessage="Please select department" Display="None" InitialValue="0" ValidationGroup="Group2"></asp:RequiredFieldValidator>
</EditItemTemplate>
<FooterTemplate> 
 <asp:DropDownList ID="ddlDepartmentFooter" runat="server" AutoPostBack="true" 
 OnSelectedIndexChanged="ddlDepartmentFooter_SelectedIndexChanged" > 
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartmentFooter"
     ErrorMessage="Please select department" Display="None" InitialValue="0" ValidationGroup="Group1"></asp:RequiredFieldValidator>
    </FooterTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="User" ItemStyle-Width="100px">
<ItemTemplate>
    <asp:Label ID="lblUser" runat="server" Text='<%#Bind("UserName")%>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
    <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
    </asp:DropDownList>
    
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUser"
     ErrorMessage="Please select user" Display="None" InitialValue="0" ValidationGroup="Group2"></asp:RequiredFieldValidator>
</EditItemTemplate>
<FooterTemplate>  <asp:DropDownList ID="ddlUserFooter" runat="server" 
 OnSelectedIndexChanged="ddlUserFooter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUserFooter"
     ErrorMessage="Please select user" Display="None" ValidationGroup="Group1" InitialValue="0"></asp:RequiredFieldValidator>
    </FooterTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="Email">
<ItemTemplate>
    <asp:Label ID="lblEmail" runat="server" Text='<%#Bind("Email")%>'></asp:Label>
</ItemTemplate>
<EditItemTemplate>
  
    <asp:TextBox ID="txtEmail" runat="server" Text='<%#Bind("Email")%>' SkinID="txt_80"></asp:TextBox>
    <asp:RequiredFieldValidator Display="None" ControlToValidate="txtEmail" ValidationGroup="Group2"
    ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter email address"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" 
    ErrorMessage="Please enter valid email"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
          ValidationGroup="Group2" Display="None"></asp:RegularExpressionValidator>
</EditItemTemplate>
<FooterTemplate>    
<asp:TextBox ID="txtEmailFooter" runat="server" SkinID="txt_80"></asp:TextBox>
<asp:RequiredFieldValidator Display="None" ControlToValidate="txtEmailFooter" ValidationGroup="Group1"
    ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter email address"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailFooter" 
    ErrorMessage="Please enter valid email"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Group1"
         Display="None"></asp:RegularExpressionValidator>
</FooterTemplate>


</asp:TemplateField>
<asp:TemplateField  HeaderStyle-CssClass="header_bg_r">
<ItemTemplate>
    <asp:LinkButton ID="imgDelete" runat="server" CommandName="Delete" CommandArgument='<%# Bind("ID")%>'
         SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete selected item?');"  />
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
        </div>
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




