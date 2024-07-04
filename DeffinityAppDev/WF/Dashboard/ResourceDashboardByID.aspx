<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ResourceDashboardByID" Codebehind="ResourceDashboardByID.aspx.cs" %>

<%@ Register Assembly="Flan.FutureControls" Namespace="Flan.FutureControls" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="form-group">
      
	<div class="col-xs-12">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Projects%></label>
           <div class="col-sm-4">
                <asp:DropDownList ID="ddlProjects" runat="server">
                    </asp:DropDownList>
            </div>
        <div class="col-sm-4">
            <asp:Button ID="btnProjectView" runat="server" SkinID="btnView" OnClick="btnProjectView_Click" />
            </div>
	</div>
	
</div>
    <div class="form-group">
        <div class="col-md-12">
           <strong> <asp:Literal ID="ltrHeader" runat="server"></asp:Literal></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
   
    <div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="project" />
    </div>
    <div>
        <asp:GridView ID="GridProjects" runat="server" Width="100%" EmptyDataText="No tasks available."
            AllowPaging="True" OnPageIndexChanging="GridProjects_PageIndexChanging" DataKeyNames="ID"
            OnRowCancelingEdit="GridProjects_RowCancelingEdit" OnRowEditing="GridProjects_RowEditing"
            OnRowUpdating="GridProjects_RowUpdating" 
            onrowcommand="GridProjects_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="30px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="Linkedit" runat="server" CausesValidation="false" CommandName="Edit"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkUpdate" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes,Update%>"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>"
                            ValidationGroup="project"></asp:LinkButton>
                        <asp:LinkButton ID="LinkCancelernalExpenses" runat="server" CausesValidation="false"
                            CommandName="Cancel" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>">
                        </asp:LinkButton>
                    </EditItemTemplate>
                  
                </asp:TemplateField>
                <asp:TemplateField Visible="false" >
                    <HeaderTemplate>
                        <cc3:RowDragOverlayExtender ID="RowDragOverlayExtender2" runat="server" TargetControlID="Image2"
                            OnRowDrop="rowDragOE_RowDrop" />
                        <asp:Image ID="Image2" runat="server" SkinID="Drag" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <cc3:RowDragOverlayExtender ID="rowDragOE" runat="server" TargetControlID="Image1"
                            OnRowDrop="rowDragOE_RowDrop" />
                        <asp:Image ID="Image1" runat="server" SkinID="Drag_Header" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Priority">
                <ItemTemplate>
                <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("ListPosition") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
              <%--  <asp:BoundField DataField="ListPosition" HeaderText="Priority" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:TemplateField HeaderText="Project Ref.">
                    <ItemTemplate>
                        <asp:Literal ID="lblProjectRef" runat="server" Text='<%#Eval("ProjectReferenceWithPrefix")%>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Title">
                    <ItemTemplate>
                        <asp:Literal ID="lblProjectTitle" runat="server" Text='<%#Eval("ProjectTitle")%> '></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Task">
                    <ItemTemplate>
                        <asp:Literal ID="lblTask" runat="server" Text='<%#Eval("Task")%> '></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="lblStartDate" runat="server" Text='<%#Eval("StartDate","{0:d}")%>'></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="lblEndDate" runat="server" Text='<%#Eval("EndDate","{0:d}")%> '></asp:Literal>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="% Completed" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Literal ID="lblPercentCompleted" runat="server" Text='<%#Eval("PercentComplete")%> '></asp:Literal>
                    </ItemTemplate>
                    <%--<HeaderStyle CssClass="header_bg_r"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Utilisation %">
                    <ItemTemplate>
                        <asp:Label ID="lblUtilisation" runat="server" Text='<%# Bind("Utilisation") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUtilisation" runat="server" Width="35px" Text='<%# Bind("Utilisation") %>'></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUtilisation"
                                        ErrorMessage="Please enter utilisation" Display="None" ValidationGroup="project"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                   <%-- <asp:CompareValidator ID="CompareValidatorUtilisation" runat="server" ControlToValidate="txtUtilisation"
                                        Display="None" ErrorMessage="Please enter valid utilisation" Operator="DataTypeCheck"
                                        Type="Integer" ValidationGroup="project" SetFocusOnError="true"></asp:CompareValidator>--%>
                                        <asp:RangeValidator ID ="rngvUtilisation" runat="server" MinimumValue="0" MaximumValue="100"  Type="Integer"
                                        ErrorMessage="Please enter valid utilisation" Display="None"  ValidationGroup="project" SetFocusOnError="true" ControlToValidate="txtUtilisation" > 
                                        </asp:RangeValidator>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        &nbsp;</div>
        
    <asp:Panel ID="pnlPending" runat="server" Visible="false">
        <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.PendingProjects%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPendingProjects" runat="server" Width="300px">
                        </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <asp:Button ID="btnPendingProjectView" runat="server" SkinID="btnView" OnClick="btnPendingProjectView_Click" />
	</div>
	<div class="col-md-4">
         
	</div>
</div>

        <div>
          
        </div>
        <div>
            &nbsp;</div>
        <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 97%;">
            <asp:Literal ID="ltrPendingHeader" runat="server"></asp:Literal>
        </div>
        <div>
            <asp:GridView ID="gvPendingProjects" runat="server" Width="100%" EmptyDataText="No tasks available."
                AllowPaging="True" 
                OnPageIndexChanging="gvPendingProjects_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Project Ref." HeaderStyle-CssClass="header_bg_l">
                        <ItemTemplate>
                            <asp:Literal ID="lblProjectRef" runat="server" Text='<%#Eval("ProjectReferenceWithPrefix")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Title">
                        <ItemTemplate>
                            <asp:Literal ID="lblProjectTitle" runat="server" Text='<%#Eval("ProjectTitle")%> '></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task">
                        <ItemTemplate>
                            <asp:Literal ID="lblTask" runat="server" Text='<%#Eval("Task")%> '></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Literal ID="lblStartDate" runat="server" Text='<%#Eval("StartDate","{0:d}")%>'></asp:Literal>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Literal ID="lblEndDate" runat="server" Text='<%#Eval("EndDate","{0:d}")%> '></asp:Literal>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="% Completed" HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Literal ID="lblPercentCompleted" runat="server" Text='<%#Eval("PercentComplete")%> '></asp:Literal>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
    <div class="clr">
    </div>
    <div>
        <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 97%;">
            <asp:Literal ID="ltrAnnualLeave" runat="server"></asp:Literal>
        </div>
        <asp:GridView ID="grdrequests" runat="server" AllowPaging="True" AutoGenerateColumns="false"
            DataSourceID="objRequests" EmptyDataText="<%$ Resources:DeffinityRes,Norequestsplaced%>"
            DataKeyNames="ID" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,FromDate%> " ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblFromDate" runat="server" Text='<% #Bind("FromDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ToDate%> " ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblToDate" runat="server" Text='<% #Bind("ToDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,TotalDays%>" DataField="Days" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Notes%>" DataField="RequestNotes"
                    ItemStyle-Width="40%" />
                <asp:TemplateField HeaderText="Status"  ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<% #Bind("ApprovalStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="objRequests" runat="server" SelectMethod="SelectByUser"
            TypeName="VT.DAL.LeaveRequestHelper" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:QueryStringParameter Name="UserID" QueryStringField="uid" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="clr"></div>
    <div>
        <div class="form-group">
        <div class="col-md-12">
           <strong><asp:Literal ID="LtrlServiceDesk" runat="server"></asp:Literal></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
      
        <asp:GridView ID="GridServicedeskTickets" runat="server" AutoGenerateColumns="false" Width="100%"
             EmptyDataText="No tickets found." AllowPaging="true" OnPageIndexChanging="GridServicedeskTickets_PageIndexChanging">
           <Columns>
               <asp:BoundField HeaderText="Ticket Ref" DataField="CallID"/>
               <asp:BoundField HeaderText="Description" DataField="Details" />
               <asp:BoundField HeaderText="Customer" DataField="CompanyID" />
               <asp:TemplateField HeaderText="Scheduled Date">
                   <ItemTemplate>
                       <asp:Label ID="lblScheduledDate" runat="server"
                            Text='<%#Bind("ScheduledDate","{0:d}") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField HeaderText="Status" DataField="StatusID"/>
           </Columns>
        </asp:GridView>
    </div>

    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>
