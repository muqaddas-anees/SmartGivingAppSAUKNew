<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CCAddApproval" Title="Untitled Page" Codebehind="CCAddApproval.aspx.cs" %>
<%@ Register Src="controls/ChangeControlTab.ascx" TagName="Tab" TagPrefix="Deffinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblPageTitle" runat="server">
                        </label>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <Deffinity:Tab ID="tabMenu" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pnlApprovals" runat="server" ScrollBars="None">
       <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Approval" Width="100%" />
	</div>
</div>
      <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Name%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlName1" runat="server" DataSourceID="SqlDataSource2" DataTextField="ContractorName"
                                    DataValueField="ID" AppendDataBoundItems="true" SkinID="ddl_90">
                                    <asp:ListItem Text="Please Select.." Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlName1"
                                    ErrorMessage="Please Select Name" InitialValue="0" ValidationGroup="Approval">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlName1"
                                    ErrorMessage="Please Select Name" InitialValue="0" ValidationGroup="Page" Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Title%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtTitle1" runat="server" SkinID="txt_90"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTitle1"
                                    ErrorMessage="Approval Title should not be blank" ValidationGroup="Approval">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtTitle1"
                                    ErrorMessage="Approval Title should not be blank" ValidationGroup="Page" Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
  <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Comments%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtComments1" runat="server" TextMode="MultiLine" SkinID="txtMulti_100"
                                    Rows="6" ></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
                 <asp:Button ID="btnAddApproval" SkinID="btnAdd" runat="server" OnClick="btnAddApproval_Click"
                            ValidationGroup="Approval" />&nbsp;
                        <asp:Button ID="btnSendApproval" runat="server" SkinID="btnDefault" Text="Send for Approval"
                            OnClick="btnSendApproval_Click" />
            </div>
	</div>
</div>

     <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblApproval" runat="server" EnableViewState="false" ForeColor="Red" />
	</div>
</div>
                   
                    <asp:Panel ID="pnlApprovalGrid" runat="server" ScrollBars="None" Width="100%" Height="100%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    DataSourceID="odsApproval" DataKeyNames="Id" OnRowCommand="GridView3_RowCommand"
                                    EnableViewState="False" onrowdatabound="GridView3_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approver">
                                            <ItemTemplate>
                                                <a href="mailto:<%#Eval("EmailAddress").ToString() %>">
                                                    <%#Eval("ApprovalName")%>
                                                </a>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="header_bg_l" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" Visible="false" />
                                        <asp:BoundField DataField="ChangeControlID" Visible="false" />
                                        <asp:TemplateField HeaderText="Title" SortExpression="Title" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px"  ControlStyle-Width="250px" FooterStyle-Width="250px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%#checkEmptyText(Eval("Comments").ToString()) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved/Denied">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblApprovingStatus" runat="server" Text='<%#approvedOrDenied(Eval("Approved")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Approved/Denied" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="DateApproved" runat="server" Text='<%#GetApprovedDate(Convert.ToDateTime(Eval("DateApproved"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <%-- <asp:ImageButton ID="btnFooterApprove" runat="server" CommandName="cmdApprove" CommandArgument='<%# Bind("ID") %> '
                                            SkinID="ImgApprove" />--%>
                                                <asp:LinkButton ID="btnFooterApprove" Text="Approve" runat="server" CommandName="cmdApprove"
                                                    CommandArgument='<%# Bind("ID") %> ' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deny">
                                            <ItemTemplate>
                                                <%--<asp:ImageButton ID="btnFooterDeny" runat="server" CommandName="cmdDeny" CommandArgument='<%# Bind("ID") %> '
                                            SkinID="ImgCancel" />--%>
                                                <asp:LinkButton ID="btnFooterDeny" Text="Deny" runat="server" CommandName="cmdDeny"
                                                    CommandArgument='<%# Bind("ID") %> ' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" SkinID="BtnLinkDelete"
                                                    OnClientClick='return confirm("Do you really want to delete.")' 
                                                    ToolTip="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="clr">
                        </div>
                    </asp:Panel>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="SELECT [ID], [ContractorName] FROM [Contractors]  where status='Active' and sid in (1,2,3) Order By ContractorName">
                    </asp:SqlDataSource>
                    <asp:ObjectDataSource ID="odsApproval" runat="server" TypeName="Incidents.DAL.ApprovalHelper"
                        DataObjectTypeName="Incidents.Entity.Approval" DeleteMethod="Delete" OldValuesParameterFormatString="{0}"
                        SelectMethod="LoadApprovalsById" UpdateMethod="Update">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="changeControlID" Type="Int32" DefaultValue="0" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
</asp:Content>
