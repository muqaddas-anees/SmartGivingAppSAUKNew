<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="PortfolioHealthcheckPage" Title="Health Check" Codebehind="PortfolioHealthcheck.aspx.cs" %>
<%--<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>--%>
<%@ Register src="controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
  Health Checks
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Health Checks Associated with this Customer - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
   <%-- <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server" Visible="false">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <asp:Label SkinID="Loading" runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
   
<div class="form-group row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="From" />
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"> <asp:Label ID="lblCheckList" runat="server" EnableViewState="false">
                        Check List<span style="color:Red">*</span> </asp:Label></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCheckList" runat="server" DataSourceID="objCheckListFiller"
                        DataTextField="Description" DataValueField="ID" AppendDataBoundItems="true">
                        <asp:ListItem Text="Please Select.." Value="0" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqdCheckList" runat="server" InitialValue="0" Display="Dynamic"
                        ControlToValidate="ddlCheckList" ErrorMessage="Select the CheckList" Text="*"
                        ValidationGroup="From" />
                    <asp:ObjectDataSource ID="objCheckListFiller" TypeName="DataHelperClass" runat="server"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="LoadCheckLists" />
            </div>
	</div>
	<div class="col-md-4">
           
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"><asp:Label ID="lblTitle" runat="server">
                     Health Check Title<span style="color:Red">*</span> </asp:Label></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtTitle" runat="server" Width="600px" />
                    <asp:RequiredFieldValidator ID="rqdTitle" runat="server" Display="Dynamic" ErrorMessage="Title is Required"
                        Text="*" ControlToValidate="txtTitle" ValidationGroup="From" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"><asp:Label ID="lblImportAddress" runat="server" Text="Import Email Address" /></label>
           <div class="col-sm-8">
                 <asp:UpdatePanel ID="uPnlImportMailIDs" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlHealthCheckListID" runat="server" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
	</div>
	<div class="col-md-4">
         
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-8 form-inline">
               <asp:Button ID="btnAdd" runat="server" SkinID="btnApply" OnClick="btnAdd_Click" ValidationGroup="From" />
                    <asp:HyperLink ID="HyperLink1" SkinID="Button" Text="Clear" NavigateUrl="~/WF/CustomerAdmin/PortfolioHealthcheck.aspx"
                        runat="server" Style="vertical-align:baseline" />
            </div>
	</div>
        </div>
    <div class="form-group row">
          <div class="col-md-12">
        <asp:UpdatePanel ID="uPnlMessage" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Red" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
        </div>
    
   <div class="form-group row">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.HealthChecks%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    

     <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" Width="100%" Height="100%">
                <asp:UpdatePanel ID="UpdatePanelOuter" runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:GridView ID="gridHealthChecks" runat="server" DataSourceID="objGridHealthChecksHelper"
                            Width="100%" OnRowUpdating="gridHealthChecks_RowUpdating" OnRowDeleting="gridHealthChecks_RowDeleting"
                            OnSelectedIndexChanging="gridHealthCheck_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Bind("ID")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Bind("ID")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Health Check" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Literal ID="litHealthCheck" runat="server" Text='<%#Eval("CheckListText")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblHealthCheckID" runat="server" Text='<%Eval("CheckListID")%>' />
                                        <asp:Literal ID="litHealthCheckDisplay" runat="server" Text='<%#Eval("CheckListText")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Literal ID="litTitle" runat="server" Text='<%#Eval("Title")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Literal ID="litTitleEdit" runat="server" Text='<%#Eval("Title")%>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" ItemStyle-Width="270px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"><asp:Label ID="lblName" Text="Name" runat="server" /></label>
           <div class="col-sm-10 form-inline">
                <asp:TextBox ID="txtName" runat="server" SkinID="txt_90" />
                                                    <asp:RequiredFieldValidator ID="rqdName" runat="server" ControlToValidate="txtName"
                                                        Text="*" ToolTip="Name is required" EnableViewState="false" ValidationGroup='<%#Eval("ID").ToString()%>' />
            </div>
	</div>
</div>
                                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"> <asp:Label ID="lblEmailID" runat="server" Text="Email" /></label>
           <div class="col-sm-10 form-inline">
               <asp:TextBox ID="txtEmailID" runat="server" SkinID="txt_90" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailID"
                                                        ErrorMessage="*" ToolTip="Invalid Mail Address" ValidationExpression="\w+([-+.'’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="Dynamic" ValidationGroup='<%#Eval("ID").ToString()%>' />
                                                    <asp:RequiredFieldValidator ID="rqdMailID" runat="server" ControlToValidate="txtEmailID"
                                                        Text="*" ToolTip="Email Address is required" EnableViewState="false" Display="Dynamic"
                                                        ValidationGroup='<%#Eval("ID").ToString()%>' />
            </div>
	</div>
                                              <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-10">
                <asp:CheckBox ID="chkAddToAll" runat="server" Text=" Add email to all Health Check's" />
                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
            </div>
	</div>
                                             <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-10 pull-left">
               <asp:Button ID="btnAddEmailID" runat="server" SkinID="btnAdd"
                                                        CommandName="Select" ValidationGroup='<%#Eval("ID").ToString()%>' />
            </div>
	</div>
</div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mails">
                                    <ItemTemplate>
                                        <asp:Label ID="imgPopUp" runat="server" SkinID="Mail" />
                                        <asp:Literal ID="litID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                                        <asp:Panel ID="pnlGridInner" runat="server" style="max-height:300px" ScrollBars="Vertical">
                                        <asp:GridView ID="gridInner" SkinID="gv_nested" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                            BackColor="White" HorizontalAlign="Justify" DataKeyNames="ID" EnableViewState="false">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" CommandName="Edit" runat="server" SkinID="BtnLinkEdit"
                                                            CausesValidation="false" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnUpdate" CommandName="Update" runat="server" SkinID="BtnLinkUpdate"
                                                            ValidationGroup="InnerEdit" />
                                                        <asp:LinkButton ID="btnCancel" CommandName="Cancel" runat="server" SkinID="BtnLinkCancel"
                                                            CausesValidation="false" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtName" runat="server" Text='<%#Bind("Name")%>' />
                                                        <asp:RequiredFieldValidator ID="rqdName" runat="server" ControlToValidate="txtName"
                                                            ErrorMessage="*" ToolTip="Name is required" Display="Dynamic" ValidationGroup="InnerEdit" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailID" runat="server" Text='<%#Eval("EmailID")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEmailID" runat="server" Text='<%#Bind("EmailID")%>' />
                                                        <asp:RequiredFieldValidator ID="rqdEmailID" runat="server" ControlToValidate="txtEmailID"
                                                            ErrorMessage="*" ToolTip="Mail Address is required" Display="Dynamic" ValidationGroup="InnerEdit" />
                                                        <asp:RegularExpressionValidator ID="regEmailID" runat="server" ControlToValidate="txtEmailID"
                                                            ErrorMessage="*" ToolTip="Invalid Mail Address" Display="Dynamic" ValidationGroup="InnerEdit"
                                                            ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Do you really want to delete")'
                                                            SkinID="BtnLinkDelete" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div style="width: 100%">
                                                    No Email Addresses.
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        </asp:Panel>
                                        <ajaxToolkit:HoverMenuExtender ID="hme2" runat="Server" PopupControlID="pnlGridInner"
                                            PopupPosition="Left" PopDelay="25" TargetControlID="imgPopUp" />
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="SELECT ID,[Name], [EmailID] FROM [HealthCheckNameMailID] WHERE ([PortfolioHealthCheckID] = @PortfolioHealthCheckID)"
                                            UpdateCommand="UPDATE HealthCheckNameMailID SET Name =@Name, EmailID =@EmailID WHERE ID=@ID"
                                            DeleteCommand="DELETE FROM HealthCheckNameMailID WHERE (ID = @ID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="litID" Name="PortfolioHealthCheckID" PropertyName="Text"
                                                    Type="Int32" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ID" />
                                            </DeleteParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="Name" />
                                                <asp:Parameter Name="EmailID" />
                                                <asp:Parameter Name="ID" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete"
                                            CommandName="Delete" OnClientClick='return confirm("Do you really want to delete.")' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="objGridHealthChecksHelper" runat="server" TypeName="Health.DAL.PortfolioHealthCheckHelper"
                            OldValuesParameterFormatString="original_{0}" SelectMethod="LoadAllPortfolioHealthChecks"
                            DataObjectTypeName="Health.Entity.PortfolioHealthCheck" UpdateMethod="Update"
                            DeleteMethod="Delete" EnableViewState="false"></asp:ObjectDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>

    <div class="form-group row">
          <div class="col-md-12 pull-right">
                <asp:Button ID="btnAddEmails" runat="server"
                    OnClick="btnAddEmails_Click" EnableViewState="false" SkinID="btnUpdate" />
	</div>
</div>
         <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    NestedGridResponsiveCss();
 </script>
<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
     <script type="text/javascript">
         grid_responsive_parent_display();
         grid_responsive_nested_display();

         $(window).load(function () {
             $(".dropdown-menu li")
           .find("input[type='checkbox']")
           .prop('checked', 'checked').trigger('change');
             $(".btn-toolbar").hide();
         });
        

         function pageLoad() {
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(grid_responsive_parent_display);
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(grid_responsive_nested_display);
         }

        

    </script>--%>


</asp:Content>
