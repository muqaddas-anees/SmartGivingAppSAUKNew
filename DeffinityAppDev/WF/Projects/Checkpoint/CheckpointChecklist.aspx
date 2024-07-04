<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CheckpointChecklist" EnableEventValidation="false" Codebehind="CheckpointChecklist.aspx.cs" %>

<%@ Register Src="~/WF/Portal/Controls/CheckpointCustomerTab.ascx" TagName="CheckpointCustomerTab"
    TagPrefix="uc1" %>
<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="checkpoint_admin" TagPrefix="uc1" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server">
                        </label>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:CheckpointCustomerTab ID="CheckpointCustomerTab1" runat="server" />
     <uc1:checkpoint_admin ID="checkpoint_admin1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        

<div class="form-group">
      <div class="col-md-3">
         View QA Audit for the following Task:
	</div>
	<div class="col-md-4">
           <asp:DropDownList ID="ddlTasks" runat="server" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"
                            Width="500px" AutoPostBack="true">
              </asp:DropDownList>
	</div>
    <div class="col-md-5">
        </div>
	
</div>
                        
                        
                        
                        <asp:GridView ID="gvchecklist" runat="server" Width="100%" EmptyDataText="No items found"
                            OnRowCancelingEdit="gvchecklist_RowCancelingEdit" OnRowEditing="gvchecklist_RowEditing"
                            AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" OnPageIndexChanging="gvchecklist_PageIndexChanging"
                            PageSize="10" OnRowUpdating="gvchecklist_RowUpdating" OnRowCommand="gvchecklist_RowCommand"
                            OnRowDataBound="gvchecklist_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="update" Text="Update"
                                            CommandArgument='<%# Bind("ProjectReference")%>' SkinID="BtnLinkUpdate"
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
                                            ValidationGroup="group1" SkinID="BtnLinkAdd" ToolTip="Insert">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblTaskId" runat="server" Text='<%# Bind("TaskID") %>' Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtDescription" runat="server" Width="350px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="35%" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDescriptionFooter" runat="server" Width="350px"></asp:TextBox>
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
                                 <asp:TemplateField HeaderText="Notes">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNotes" runat="server" Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNotesFooter" runat="server" Width="200px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                              <%--  <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkComments" runat="server" Text="Customer Comments" CommandName="Comments"
                                             CommandArgument='<%# Bind("ID") %> ' Visible='<%#CommentsVisible(DataBinder.Eval(Container.DataItem, "Status").ToString())%>' ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="ClosedDate" HeaderText="Closed Date" NullDisplayText="------"
                                    ReadOnly="True">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                
                            </Columns>
                        </asp:GridView>
                         <asp:HiddenField ID="hfItemId" runat="server" Value="0" />
   
                    </ContentTemplate>
                </asp:UpdatePanel>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

