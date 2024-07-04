<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="Default11" Codebehind="ProjectChecklist.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" runat="server" ValidationGroup="Group1" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Green"></asp:Label>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlTasks"
                    runat="server" Display="None" ErrorMessage="Please select a task" InitialValue="0"
                    ValidationGroup="Group1"></asp:RequiredFieldValidator>
	</div>
</div>
   
    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Tasks%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlTasks" runat="server" AutoPostBack="true" SkinID="ddl_90" OnSelectedIndexChanged="ddlTasks_SelectedIndexChanged">
                </asp:DropDownList>
               
            </div>
	</div>
</div>
     <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label">Assigned to Customer User</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCustomerUser" runat="server" SkinID="ddl_90">
                </asp:DropDownList>
            </div>
	</div>
</div>
     <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label">Assigned to PM</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlAssigntoPM" runat="server" SkinID="ddl_90">
                </asp:DropDownList>
            </div>
	</div>
</div>
     <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
               <asp:RadioButtonList ID="rdlist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdlist_SelectedIndexChanged">
                <asp:ListItem Text="Checkpoint" Value="Checkpoint" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
	</div>
</div>
     <div class="form-group" id="pnlCheckpoints" runat="server" visible="true">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CheckList%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlChecklist" runat="server" SkinID="ddl_90">
                </asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group" id="pnlForms" runat="server" visible="false">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Forms%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlForms" runat="server" SkinID="ddl_90"></asp:DropDownList>
            </div>
	</div>
</div>
     <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
                <asp:Button ID="imgApplyChecklist" runat="server" SkinID="btnApply" ValidationGroup="Group1" ImageAlign="AbsMiddle"
                    OnClick="imgApplyChecklist_Click" />
               </div>
              </div>
         </div>

    <div>
        <asp:GridView ID="Grid_assignto" runat="server" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Task" HeaderStyle-CssClass="header_bg_l">
                    <ItemTemplate>
                        <asp:Label ID="lblTaskName" runat="server" Text='<%#Bind("TaskName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Date" >
                    <ItemTemplate>
                        <asp:Label ID="lblTaskDate" runat="server" Text='<%#Bind("TaskDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Check Point">
                    <ItemTemplate>
                        <asp:Label ID="lblCheckpoint" runat="server" Text='<%#Bind("Checkpoint") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Form" >
                    <ItemTemplate>
                        <asp:Label ID="lblForm" runat="server" Text='<%#Bind("Form") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Assigned To" HeaderStyle-CssClass="header_bg_r">
                    <ItemTemplate>
                        <asp:Label ID="lblAssignto" runat="server" Text='<%#Bind("AssignTo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
    </div>
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
</asp:Content>
