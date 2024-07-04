<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="QACheckList" Title="QACheckList" Codebehind="QACheckList.aspx.cs" %>

<%@ Register Src="controls/QAtabs.ascx" TagName="QATab" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.QA%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Checklists%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:QATab ID="QATab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="form-group">
          <div class="col-md-12">
              <p>  Use this section to create a QA Checklist for the project. Simply select from the available templates below</p>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>  
               <asp:ValidationSummary ID="p1" runat="server" ValidationGroup="GROUP1" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group2" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group3" />
          <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group4" /> 
	</div>
</div>
    <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.QAChecklists%></label>
           <div class="col-sm-10 form-inline">
               <asp:DropDownList ID="ddQAAssign" runat="server" SkinID="ddl_80" />
               <asp:Button ID="btn_assign" runat="server" SkinID="btnDefault" Text="Assign" OnClick="btn_assign_Click" ValidationGroup="GROUP1" OnClientClick="test();" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
              <asp:RequiredFieldValidator ID="RFV" runat="server" ControlToValidate="ddQAAssign"
                    ErrorMessage="Please select QA Checklist" InitialValue="0" Display="None" ValidationGroup="GROUP1"></asp:RequiredFieldValidator>
	</div>
</div>


 <asp:Panel ID="pnl" runat="server"  Width="100%" >
     
<div class="form-group pull-right">
          <div class="col-md-12 pull-right">
              <asp:LinkButton ID="BtnCheckAll" runat="server" Text="Complete All" Font-Bold="true" OnClick="BtnCheckAll_Click"></asp:LinkButton>
	</div>
</div>

     <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" Width="100%" OnRowDeleting="GridView1_RowDeleting"  AutoGenerateColumns="False"    OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" >
 <Columns>
 
   <asp:BoundField  HeaderText="AC2PID" DataField="AC2PID" Visible="False" />
  <asp:BoundField  HeaderText="ProjectReference" DataField="ProjectReference" Visible="False" />
  
     <asp:BoundField  HeaderText="ID" DataField="ID" Visible="False" />
                       <asp:TemplateField FooterStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" >  
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"  CommandArgument='<%# Bind("ID")%>'  SkinID="BtnLinkUpdate" ValidationGroup="Group3"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                            <asp:LinkButton ID="LinkButtonInsert" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="Group2" SkinID="BtnLinkAdd"></asp:LinkButton>
                            </FooterTemplate>
                       </asp:TemplateField>
   
   <asp:TemplateField HeaderText="List Item" >
   <EditItemTemplate>
<asp:TextBox ID="txtListItem" SkinID="txt_90"  TextMode="MultiLine"  runat="server" Text='<%# Bind("ListItem") %>'></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtListItem"
                        Display="None" ErrorMessage="Please Enter List Item" ValidationGroup="Group3"></asp:RequiredFieldValidator>
            

</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblListItem" runat="server" Text='<%# Bind("ListItem") %>'></asp:Label>
</ItemTemplate>
 <FooterTemplate>
                    <asp:TextBox ID="txtListItem1" TextMode="MultiLine"  width="260px"    runat="server" Text=''></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtListItem1"
                        Display="None" ErrorMessage="Please enter List item" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                </FooterTemplate>

  </asp:TemplateField>
     <asp:TemplateField  HeaderText="Due Date" ControlStyle-CssClass="form-inline">
            <ItemStyle HorizontalAlign ="Center"/>
                <ItemTemplate>
                        <asp:Label ID="LlblDueDate" runat="server" Text='<%# Bind("DueDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDueDate" runat="server" Text='<%# Bind("DueDate","{0:d}") %>' SkinID="Date"></asp:TextBox><asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"   runat="server" PopupButtonID="imgbtnenddate6" TargetControlID="txtDueDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFdV6" runat="server" ControlToValidate="txtDueDate"
                            Display="None" ErrorMessage="Please enter Due date" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REV6" runat="server" ControlToValidate="txtDueDate"
                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="Group3">*</asp:RegularExpressionValidator>
                                </EditItemTemplate>   
                                <FooterStyle CssClass="form-inline"/>  
                                <FooterTemplate>
                    <asp:TextBox ID="txtDueDate1" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox> <asp:Label ID="imgbtnenddate9" runat="server" SkinID="Calender" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9"   runat="server" PopupButtonID="imgbtnenddate9" TargetControlID="txtDueDate1" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFV9" runat="server" ControlToValidate="txtDueDate1"
                            Display="None" ErrorMessage="Please Enter Due Date" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REV9" runat="server" ControlToValidate="txtDueDate1"
                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                              </FooterTemplate>
                    </asp:TemplateField>
     <asp:TemplateField  HeaderText="Notes">
      <ItemStyle Width="200px"  />
                    <ItemTemplate>
                   <asp:Label ID="lblFromSite" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>                    
                    </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox ID="txtNotes" width="220px" TextMode="MultiLine"  runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox></EditItemTemplate>
         <HeaderStyle HorizontalAlign="Center" />
          <FooterTemplate>
           <asp:TextBox ID="txtNotes1" width="220px" TextMode="MultiLine" Text ='' runat="server" ></asp:TextBox>
           </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField >
             <ItemTemplate>
         <asp:LinkButton  ID="LinkButtonUpdate2" runat="server" CommandName="Checked" Text="Audited"  CommandArgument='<%#Bind("ID") %>' ToolTip="Checked"></asp:LinkButton>
             </ItemTemplate>
             </asp:TemplateField>
  
 <asp:TemplateField HeaderText="Date Audited">
   <ItemStyle Width="80px"  />
 <ItemTemplate>
 <asp:Label ID="LBLDateQAApprove" runat="server" Text='<%# Bind("DateQAApproved","{0:d}") %>'></asp:Label>
 </ItemTemplate>
 </asp:TemplateField>
                    <asp:TemplateField>
  <ItemStyle Width="40px"  />
                      <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="delete"
                            SkinID="BtnLinkDelete"  CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to Delete item from the list?');" ToolTip="delete" />
                    </ItemTemplate>
                </asp:TemplateField>
 </Columns>
  <EmptyDataTemplate>
            <table width="100%">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td >List Item </td>
                    <td >Due Date </td>
                    <td>Notes </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="btSend" runat="server" CommandName="EmptyInsert" UseSubmitBehavior="False" ValidationGroup="Group4" SkinID="BtnLinkUpdate"></asp:LinkButton></td>
                    <td> 
                      <asp:TextBox ID="txtListItem2" TextMode="MultiLine" SkinID="txt_90" runat="server" Text=''></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtListItem2"
                        Display="None" ErrorMessage="Please Enter List Item" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                    </td>
                    <td>        
                    <table>
                        <tr>
                            <td class="form-inline">
                    <asp:TextBox ID="txtDueDate2" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="imgbtnenddate10" runat="server" SkinID="Calender"  /></td>
                         
                        </tr>
                    </table>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10"   runat="server" PopupButtonID="imgbtnenddate10" TargetControlID="txtDueDate2" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDueDate2"
                            Display="None" ErrorMessage="Please Enter Due Date" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtDueDate2"
                                Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                ValidationGroup="Group4">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                      <asp:TextBox ID="txtNotes2" SkinID="txt_90" TextMode="MultiLine" Text ='' runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
 </asp:GridView>
</asp:Panel>
</asp:Content>

