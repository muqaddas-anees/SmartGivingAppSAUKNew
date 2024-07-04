<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="adminmasterlists1" Title="Admin Checklists" Codebehind="adminmasterlists.aspx.cs" %>
    <%@ Register Assembly="Flan.FutureControls" Namespace="Flan.FutureControls" TagPrefix="cc3" %>
 
<%@ Register src="~/WF/Controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>

<asp:Content ID="Content_tabs" ContentPlaceHolderID="Tabs" runat="Server">
  
    <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ManageChecklist%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="form-group">
                                  <div class="col-md-12 form-inline">
                                       <asp:Button id="btnNew" onclick="btnNew_Click" runat="server" SkinID="btnDefault" Text="Add Checklist"></asp:Button>
    <asp:Button id="btndelete" onclick="btndelete_Click" runat="server" SkinID="btnDefault" Text ="Delete Checklist"> </asp:Button>
					
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-12">
                                       <asp:ValidationSummary id="ValidationSummary1" runat="server" ValidationGroup="group1">
                    </asp:ValidationSummary> <asp:ValidationSummary id="ValidationSummary2" runat="server" ValidationGroup="Group2"></asp:ValidationSummary> 
                    <asp:ValidationSummary id="ValidationSummary3" runat="server" ValidationGroup="Group3"></asp:ValidationSummary> 
                    <asp:ValidationSummary id="ValidationSummary4" runat="server" ValidationGroup="Group4"></asp:ValidationSummary> 
                    <asp:ValidationSummary id="ValidationSummary5" runat="server" ValidationGroup="Group5"></asp:ValidationSummary> 
                    <asp:Label id="lblTemplatemsg" runat="server" Visible="False" ForeColor="Red"></asp:Label> 
                    <asp:Label id="lblSuccess" runat="server" ForeColor="#00C000"></asp:Label> 
                    <asp:Label id="lblErrorgrid" runat="server" ForeColor="Red"></asp:Label> 
                    <asp:Label id="lblSuccess2" runat="server" Text="Remember to associate this list to Group Owners!" Visible="False" ForeColor="Red"></asp:Label>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ChecklistType%></label>
                                      <div class="col-sm-5 form-inline">
                                           <asp:DropDownList ID="ChecklistType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChecklistType_SelectedIndexChanged">
            <asp:ListItem Text="Project Template" Value="1"></asp:ListItem>
            <asp:ListItem Text="Project Class Checklist" Value="6"></asp:ListItem>
            <asp:ListItem Text="Health Check/Audit" Value="2"></asp:ListItem>
            <asp:ListItem Text="QA/UAT" Value="3"></asp:ListItem>
             <asp:ListItem Text="Pre-Qualification Questionnaire" Value="4"></asp:ListItem>
              <asp:ListItem Text="Scoring" Value="5"></asp:ListItem>
                <asp:ListItem Text="Permit to Work" Value="7"></asp:ListItem>
                 <asp:ListItem Text="Checkpoint Checklist" Value="8"></asp:ListItem>
                 <asp:ListItem Text="Contract Checklist" Value="9"></asp:ListItem>
            </asp:DropDownList>
					</div>
                                       <div class="col-sm-4">
                                          </div>
				</div>
 
</div>
     <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.SelectChecklist%></label>
                                      <div class="col-sm-6 form-inline">
                                          <asp:DropDownList ID="ddlTemplates" runat="server" AutoPostBack="true" 
                class="txt_field" OnSelectedIndexChanged="ddlTemplates_SelectedIndexChanged" 
                SkinID="ddl_80">
            </asp:DropDownList>
            <asp:TextBox ID="txtNewTemplate" runat="server" class="txt_field" SkinID="txt_80"></asp:TextBox>
            <asp:CheckBox ID="chkLock" runat="server" AutoPostBack="true" 
                OnCheckedChanged="chkLock_CheckedChanged" Text="Lock Template" Visible="false" />
            <asp:RequiredFieldValidator ID="RFV3" runat="server" 
                ControlToValidate="txtNewTemplate" Display="None" 
                ErrorMessage="Please enter new  template" ValidationGroup="Group3"></asp:RequiredFieldValidator>
					</div>
                                      <div class="col-sm-2">
                                          </div>
				</div>
 
</div>
     <div class="form-group">
          <div class="col-md-12">
          <div class="col-sm-2">
                                          </div>
                                  <div class="col-md-6 form-inline">
                                      
                                          <asp:Button id="btnSubmitTemplate" 
            onclick="btnSubmitTemplate_Click" runat="server" ValidationGroup="Group3" 
            SkinID="btnSubmit"></asp:Button> <asp:Button id="btnCancel" 
            onclick="btnCancel_Click" runat="server" SkinID="btnCancel"></asp:Button> 
					
				</div>
              </div>
         <div class="col-md-12">
             <div class="col-sm-2">
                                          </div>
              <div class="col-sm-10 form-inline">
                  <asp:Label id="lblConfirm" runat="server" Text="Confirm?" Visible="False"></asp:Label> <asp:Button id="btnConfirmYes" onclick="btnConfirmYes_Click" runat="server" Text="Y" Visible="False"></asp:Button><asp:Button id="btnConfirmNo" onclick="btnConfirmNo_Click" runat="server" Text="N" Visible="False"></asp:Button>
                                          </div>
             
         </div>
 
</div>
      <div id="div_SaveAs" runat="server">
    <div  class="form-group" >
         <div class="col-md-12">
        <label class="col-sm-2 control-label">Save Checklist as</label>
         
         <div class="col-sm-5 form-inline">
             <asp:TextBox id="txtSaveAsName" class="txt_field" runat="server" SkinID="txt_80"></asp:TextBox> <asp:RequiredFieldValidator id="RFV4" runat="server" ValidationGroup="Group4" ErrorMessage="Please enter template name" Display="None" ControlToValidate="txtSaveAsName"></asp:RequiredFieldValidator>
             </div>
         <div class="col-sm-4">
                                          </div>
             </div>
        </div>
    <div  class="form-group">
         <div class="col-md-12">
         <div class="col-sm-2">
                                          </div>
        <div class="col-md-5">
             <asp:Button id="btnSaveAsName" onclick="btnSaveAsName_Click" 
            runat="server" ValidationGroup="Group4" SkinID="btnSave"></asp:Button> 
        <asp:Button id="btnSaveAsQA" onclick="btnSaveAsQA_Click" runat="server" 
            SkinID="btnDefault" Text="Save As"></asp:Button>&nbsp;
        </div>
         <div class="col-sm-5">
                                          </div>
             </div>
    </div>
          </div>
    <div class="form-group">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<asp:GridView id="GridView1" runat="server" Width="100%" DataKeyNames="ID" PageSize="50" AutoGenerateColumns="False" 
    OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" 
    OnRowUpdating="GridView1_RowUpdating" OnPageIndexChanged="GridView1_PageIndexChanged" 
    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCreated="GridView1_RowCreated" 
    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
     FooterStyle-CssClass="footer_css" PagerStyle-CssClass="pager_css"
    ><Columns>
<asp:TemplateField ItemStyle-Width="50px"  Visible="false" ><HeaderTemplate>
                            <cc3:RowDragOverlayExtender ID="RowDragOverlayExtender2" runat="server" TargetControlID="Image2"
                                OnRowDrop="rowDragOE_RowDrop" />
                            <asp:LinkButton ID="Image2" runat="server" SkinID="BtnLinkDrag" />
                        
</HeaderTemplate>
<ItemTemplate>
                            <cc3:RowDragOverlayExtender ID="rowDragOE" runat="server" TargetControlID="Image1"
                                OnRowDrop="rowDragOE_RowDrop" />
                            <asp:LinkButton ID="Image1" runat="server" SkinID="BtnLinkDrag" />
                        
</ItemTemplate>

</asp:TemplateField>
<asp:BoundField DataField="ID" HeaderText="ID" Visible="False"></asp:BoundField>

<asp:TemplateField HeaderText="Indent Level" HeaderStyle-Width="80px" ItemStyle-CssClass="form-inline" >
<ItemTemplate>
  <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>

<asp:LinkButton CommandArgument='<%# Bind("IndentLevel")%>' ID="btnDecrease"  runat="server" onclick="btnDecrease_Click" SkinID="BtnLinkOutIndent"     />
    <asp:LinkButton ID="btnIncrease" CommandArgument='<%# Bind("IndentLevel")%>' onclick="btnIncrease_Click" runat="server" SkinID="BtnLinkIndent" />
 
</ItemTemplate>
<EditItemTemplate>
  </EditItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" ><EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                            CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group5" SkinID="BtnLinkUpdate"
                            ToolTip="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    
</EditItemTemplate>
<FooterTemplate>
                        <asp:LinkButton ID="LinkButtonInsert" runat="server" CommandName="Insert" Text="Insert"
                            ValidationGroup="group1" SkinID="BtnLinkAdd" ToolTip="Insert"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    
</FooterTemplate>
<ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Item Description"><EditItemTemplate>
                        <asp:TextBox ID="txtdesc" runat="server" Text='<%# Bind("ItemDescription") %>' SkinID="txt_80"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtdesc"
                            Display="None" ErrorMessage="Please enter Item description " ValidationGroup="Group5"></asp:RequiredFieldValidator>
                    
</EditItemTemplate>
<FooterTemplate>
                        <asp:TextBox ID="txtItem" runat="server" Text='' SkinID="txt_80"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItem"
                            Display="None" ErrorMessage="Please enter Item description" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    
</FooterTemplate>
<ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%#getItemDes(DataBinder.Eval(Container.DataItem, "IndentLevel").ToString(),DataBinder.Eval(Container.DataItem, "ItemDescription").ToString()) %>'></asp:Label>
                    
</ItemTemplate>

</asp:TemplateField>
<asp:TemplateField HeaderText="RAG Required" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" FooterStyle-HorizontalAlign="Center" Visible="false"><EditItemTemplate>
                        <asp:DropDownList ID="ddlRAG" runat="server" Width="50px">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    
</EditItemTemplate>
<FooterTemplate>
                        <asp:DropDownList ID="ddlRAGRequired" runat="server" Width="50px">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    
</FooterTemplate>
<ItemTemplate>
                        <asp:Label ID="lblRAG" runat="server" Text='<%# Bind("RAGRequired") %>'></asp:Label>
                    
</ItemTemplate>
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Amber Days" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px"  FooterStyle-HorizontalAlign="Center" Visible="false"><EditItemTemplate>
                        <asp:TextBox ID="txtAmber" runat="server" Text='<%# Bind("AmberDays") %>' Width="30px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                            ControlToValidate="txtAmber" Display="None" ErrorMessage="Please enter valid amber days"
                            ValidationExpression="^\d+(?:\.\d{0})?$" ValidationGroup="Group5"></asp:RegularExpressionValidator>
                    
</EditItemTemplate>
<FooterTemplate>
                        <asp:TextBox ID="txtAmberDays" runat="server" Text='3' Width="30px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                            ControlToValidate="txtAmberDays" Display="None" ErrorMessage="Please enter valid amber days"
                            ValidationExpression="^\d+(?:\.\d{0})?$" ValidationGroup="group1"></asp:RegularExpressionValidator>
                    
</FooterTemplate>
<ItemTemplate>
                        <asp:Label ID="lblAmber" runat="server" Text='<%# Bind("AmberDays") %>' Width="50px"></asp:Label>
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Red Days" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px"  FooterStyle-HorizontalAlign="Center" Visible="false"><EditItemTemplate>
                        <asp:TextBox ID="txtred" runat="server" Text='<%# Bind("RedDays") %>' Width="30px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                            ControlToValidate="txtred" Display="None" ErrorMessage="Please enter valid red days"
                            ValidationExpression="^\d+(?:\.\d{0})?$" ValidationGroup="Group5"></asp:RegularExpressionValidator>
                    
</EditItemTemplate>
<FooterTemplate>
                        <asp:TextBox ID="txtRedDays" runat="server" Text='1' Width="30px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                            ControlToValidate="txtRedDays" Display="None" ErrorMessage="Please enter valid red days"
                            ValidationExpression="^\d+(?:\.\d{0})?$" ValidationGroup="group1"></asp:RegularExpressionValidator>
                    
</FooterTemplate>
<ItemTemplate>
                        <asp:Label ID="lblRed" runat="server" Text='<%# Bind("RedDays") %>' Width="50px"></asp:Label>
                    
</ItemTemplate>
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>

<asp:BoundField DataField="PositionInList" HeaderText="PositionInList" Visible="False"></asp:BoundField>
<asp:BoundField DataField="ItemDescription" HeaderText="ItemDescription" Visible="False"></asp:BoundField>
<asp:BoundField DataField="MasterTemplateID" HeaderText="MasterTemplateID" Visible="False"></asp:BoundField>
<asp:TemplateField  HeaderStyle-CssClass="header_bg_r">
 <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                            SkinID="BtnLinkDelete"  CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the Checklist item?');" ToolTip="delete" />
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
</asp:TemplateField>
</Columns>
<EmptyDataTemplate>
                <table border="0" width="100%">
                    <tr height="20" class="tab_header">
                        <td >
                        </td>
                        <td style="font-weight:bold;">
                            Item Description</td>
                       <%-- <td >
                            RAG Required</td>
                        <td >
                            Amber Days</td>
                        <td >
                            Red Days</td>--%>
                       
                    </tr>
                    <tr>
                        <td class="even_row" style="height: 38px">
                            <asp:LinkButton ID="btSend" Text="Insert" runat="server" CommandName="EmptyInsert" 
                                OnClick="lbInsert_Click" UseSubmitBehavior="False" ValidationGroup="Group2" SkinID="BtnLinkAdd"
                                ToolTip="Insert"  /></td>
                        <td class="even_row" style="height: 38px">
                            <asp:TextBox ID="txtItem1" runat="server" SkinID="txt_80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItem1"
                                Display="None" ErrorMessage="Please enter Item description" ValidationGroup="Group2"></asp:RequiredFieldValidator></td>
                       <%-- <td class="even_row" style="height: 38px">
                            <asp:DropDownList ID="ddlRAG1" runat="server" Width="70px">
                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="even_row" style="height: 38px">
                            <asp:TextBox ID="txtAmberDays1" runat="server" Text='3' Width="30px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter valid amber days"
                                ControlToValidate="txtAmberDays1" Display="None" ValidationExpression="^\d+(?:\.\d{0,4})?$"
                                ValidationGroup="Group2"></asp:RegularExpressionValidator></td>
                        <td class="even_row" style="height: 38px">
                            <asp:TextBox ID="txtRedDays1" runat="server" Text='1' Width="30px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                ErrorMessage="Please enter valid red days" ControlToValidate="txtAmberDays1"
                                Display="None" ValidationExpression="^\d+(?:\.\d{0,4})?$" ValidationGroup="Group2"></asp:RegularExpressionValidator></td>--%>
                       
                    </tr>
                </table>
            
</EmptyDataTemplate>
</asp:GridView> 
</ContentTemplate> </asp:UpdatePanel>
    </div>
 
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
 </script> 


</asp:Content>
