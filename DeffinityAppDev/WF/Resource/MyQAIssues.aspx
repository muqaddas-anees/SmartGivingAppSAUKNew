<%@ Page Language="C#" MasterPageFile="~/DeffinityTab.master" AutoEventWireup="true" Inherits="MyQAIssuesaspx" Title="My QAIssuse" Codebehind="MyQAIssues.aspx.cs" %>

<%@ Register Src="~/WF/Resource/Controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:ProjectStatus id="ProjectStatus1" runat="server">
    </uc1:ProjectStatus>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
    <td><h1 class="section1"><span>My Issues</span></h1>
    
    </td>
  </tr>
  <tr>    
    <td class="p_section1 data_carrier_block">
    <div>
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
<td colspan="7">
    <asp:CompareValidator ID="CV_date" runat="server" 
        ErrorMessage="Please enter valid Raised Date" Type="Date" 
        ValidationGroup="GridSearch" ControlToValidate="txtdudate" 
        Operator="DataTypeCheck"></asp:CompareValidator>
</td>
</tr>
<tr>
<td width="6%">Status</td>
<td width="8%">
<asp:DropDownList ID="ddlIssueStatus"  Width="150px"    runat="server" DataSourceID="objDS_IssueStatus" DataTextField="Status" DataValueField="ID" >
                        </asp:DropDownList>
 <asp:ObjectDataSource ID="objDS_IssueStatus" runat="server" TypeName="Deffinity.Bindings.DefaultDatabind" SelectMethod="b_Issue_ItemStatus_withSelect">
</asp:ObjectDataSource> 
</td>
<td width="6%">
    Type</td>
<td width="8%">
 <asp:DropDownList ID="ddlType" runat="server"  DataTextField="IssueTypeName" DataValueField="ID" DataSourceID="objDS_IssueType" Width="150px">
      </asp:DropDownList>
<asp:ObjectDataSource ID="objDS_IssueType" runat="server" TypeName="Deffinity.Bindings.DefaultDatabind" SelectMethod="b_IssueTypeWithALL">
</asp:ObjectDataSource>      
    </td>
<td width="14%">
   &nbsp;&nbsp;Raised&nbsp;Date
 </td>
    <td width="22%">
 <asp:TextBox ID="txtdudate" runat="server" Width="120px" ></asp:TextBox>&nbsp;
<asp:Image ID ="imgdatecomm2" runat="server" SkinID="Calender"  />

    </td>
      <td width="8%">
         <asp:ImageButton ID="btn_search" runat="server" SkinID="ImgSearch" OnClick="btn_search_Click" ValidationGroup="GridSearch" /></td>
   <td width="26%">
   </td>
</tr>
    <tr>
        <td colspan="8">
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                PopupButtonID="imgdatecomm2" TargetControlID="txtdudate" CssClass="MyCalendar">
            </ajaxToolkit:CalendarExtender>
        </td>
    </tr>
</table>
</div>

<div class="sec_table">
  <asp:GridView ID="Grid_Issues" runat="server"  Width="100%" DataKeyNames="ID" 
        AutoGenerateColumns="False" DataSourceID="objDS_SelectIssues" 
        onrowupdated="Grid_Issues_RowUpdated" onrowupdating="Grid_Issues_RowUpdating" EnableViewState="false" >
      <Columns>                 
                <asp:BoundField Visible="False" DataField="ID" />   
               <asp:TemplateField HeaderStyle-CssClass="header_bg_l" >  
                <ItemStyle  Width="55px" />
                <ItemTemplate>
                    <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" CommandArgument="<%# Bind('ID')%>" ImageUrl="~/media/ico_edit.png" ToolTip="Edit" ></asp:ImageButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"  CommandArgument="<%# Bind('ID')%>" ValidationGroup="Group2" ImageUrl="~/media/ico_update.png" ToolTip="Update"></asp:ImageButton>
                    <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel" ></asp:ImageButton>
                    
                </EditItemTemplate>
              
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Project Reference">
                
                <ItemTemplate>
                        <asp:Label ID="lblProjectReference" runat="server" Text='<%# Bind("ProjectReferenceWithPrefix") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Title">
                 <ItemStyle  Width="125px" />
                <ItemTemplate>
                        <asp:Label ID="lblProjectTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue">
                <ItemStyle  Width="175px" />
                <ItemTemplate>
                        <asp:Label ID="lblIssue" runat="server" Text='<%# Bind("Issue") %>'></asp:Label>
                    </ItemTemplate>
                
                   
                    <HeaderStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date Raised"> 
                                <ItemTemplate>
                                  <%# DataBinder.Eval(Container.DataItem, "ScheduledDate", "{0:d}")%>
                                </ItemTemplate> 
                                
                         <HeaderStyle HorizontalAlign="Center"  />
                               </asp:TemplateField> 
                
                <asp:TemplateField  HeaderText="Assigned To" SortExpression="AssignedTo">
                    <ItemTemplate>
                   <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Bind("AssignToName") %>'></asp:Label>                    
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Type "  SortExpression="IssuesList">
                <ItemStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="lblIssue1" runat="server" Text='<%# Bind("IssuseTypeName") %>'></asp:Label>
                </ItemTemplate>         
                </asp:TemplateField>    
                <asp:TemplateField  HeaderText="Status" SortExpression="StatusID">
                <ItemTemplate>
                        <asp:Label ID="lblstat" runat="server" Text='<%# Bind("ProjectStatus") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGdStatus"  Width="90px"    runat="server" DataSourceID="objDS_IssueStatus" DataTextField="Status" DataValueField="ID" SelectedValue='<%# Bind("Status") %>' >
                        </asp:DropDownList>
                       
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                  </asp:TemplateField>
                 <asp:TemplateField HeaderText="Notes" HeaderStyle-CssClass="header_bg_r">
                  
                  <ItemTemplate>
                   <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>' ></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                 <asp:TextBox ID="txtNotes"  runat="server" width="150px" Text='<%# Bind("Notes") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle Width="150px" />
                  </asp:TemplateField>
            </Columns>
                  
          </asp:GridView>
  <div>
   <asp:ObjectDataSource ID="objDS_SelectIssues" runat="server" TypeName="Deffinity.IssuesManager.IssuesManager" SelectMethod="Dashboard_Search_DisplayIssues" UpdateMethod="Dashboard_UpdateIssues">
    <SelectParameters>
     <asp:ControlParameter Name="status" ControlID="ddlIssueStatus" PropertyName="SelectedValue" DefaultValue="0" />
     <asp:SessionParameter Name="ContractorID" SessionField="UID" DefaultValue="0" />
     <asp:ControlParameter Name="ScheduleDate" ControlID="txtdudate" PropertyName="Text" ConvertEmptyStringToNull="true" DefaultValue="" Type="String" />
     <asp:ControlParameter Name="IssuseType" ControlID="ddlType" DefaultValue="0" PropertyName="SelectedValue"/>
      
    </SelectParameters>
    <UpdateParameters>
     <asp:Parameter Name="ID" Type="Int32" />
     <asp:Parameter Name="Status" Type="Int32" />
     <asp:Parameter Name="Notes" Type="String" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
    </asp:ObjectDataSource>   
  </div>
        </div>
    </td>
  </tr>
</table>
<br /><br />



</asp:Content>

