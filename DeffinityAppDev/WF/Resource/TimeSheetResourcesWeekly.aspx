<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="TimeSheetResourcesWeekly" Codebehind="TimeSheetResourcesWeekly.aspx.cs" %>

<%@ Register Src="controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.TimesheetResources%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectStatus ID="ProjectStatus1" runat="server"></uc1:ProjectStatus>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
    <div class="form-group">
          
               <div class="col-md-8">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
            <asp:ValidationSummary ID="Getexpenses" runat="server" ValidationGroup="FooterExpensesValidate" />
                   </div>
              
              <div class="col-md-4 form-inline">
                   <select id='ddlReport' class="pull-right" >
    <option value="0">Select View</option>
    <option value="TimeSheetResourcesDaily.aspx" >Timesheet Daily View</option>
    <option value="TimeSheetResourcesWeekly.aspx">Timesheet Weekly View</option>
<%--        <option value="TimeSheetResourcesMonthly.aspx">Timesheet Monthly View</option>--%>
</select>
                   <script type="text/javascript">
                       $('#ddlReport').change(function () {
                           if ($(this).val() != "0") {
                               window.location = $(this).val();
                               $(this).val("0");
                           }
                       });

                </script>
                   </div>
</div>
   <%--  <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.TimesheetResources%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.WeekCommencingDate%> </label>
           <div class="col-sm-5 form-inline">
                <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%>  </label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlStatusupdate" Width="150px" runat="server">
                                        <asp:ListItem Value="0" Text="ALL">ALL</asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="2" Text="Submitted for Approval">Submitted for Approval</asp:ListItem>
                                        <asp:ListItem Value="4" Text="Approved">Approved</asp:ListItem>
                                        <asp:ListItem Value="3" Text="Declined">Declined</asp:ListItem>
                                    </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4 form-inline">
          <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View"
                                        ValidationGroup="Group1" onclick="btn_viewdate_Click"/>
        <asp:Button ID="btn_sendforapproval" runat="server" SkinID="ImgSendApproval"
                                         Visible="false" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7"  runat="server"
                                        FirstDayOfWeek="Monday" PopupButtonID="imgbtnenddate7" TargetControlID="txtweekcommencedate"
                                        CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
	</div>
</div>
    
<div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblstatus" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                         <asp:Label ID="lblError" Visible="false" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
    <asp:GridView ID="grdTimeSheetViewWeekly" runat="server" AutoGenerateColumns="False" 
                               EmptyDataText="No Timesheet Data with selected Date" 
                            onrowdatabound="grdTimeSheetViewWeekly_RowDataBound" 
                            onrowcommand="grdTimeSheetViewWeekly_RowCommand" onrowupdating="grdTimeSheetViewWeekly_RowUpdating">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>" HeaderStyle-CssClass="header_bg_l">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="200px" />
                                       
                                        <ItemTemplate>
                                        <asp:Label ID="lblCustom" runat="server" Text='<%# Bind("custom") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblProjectRef" runat="server" Text='<%# Bind("projectreferece") %>' Visible="false"></asp:Label>
                                           <asp:DropDownList ID="ddlProjectTitle"  runat="server" AppendDataBoundItems="true"   OnSelectedIndexChanged="ddlProjectTitle_SelectedIndexChanged" Width="140px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Button ID="imgUpdate" CommandName="Update1" SkinID="btnSave" runat="server"  />
                                             <asp:Button ID="imgAddNew" CommandName="Add" SkinID="btnAdd" runat="server" ToolTip="<%$ Resources:DeffinityRes,Addnewrow%>" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTask%>" >
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <ItemTemplate>
                                     <asp:Label ID="lblProjectTask" runat="server" Text='<%# Bind("taskid") %>' Visible="false"></asp:Label>
                                     <asp:DropDownList ID="ddlProjectTask"   runat="server"  SkinID="ddl_125px">
                                            </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselecttask%>"
                                           ControlToValidate="ddlProjectTask" Display="None" ValidationGroup="Group3" InitialValue=""></asp:RequiredFieldValidator>
                                           <ajaxToolkit:CascadingDropDown ID="casCadProjectTask" runat="server"
                                                TargetControlID="ddlProjectTask"
                                                Category="Task1"
                                                PromptText="Please select..."
                                                ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                                ServiceMethod="GetProjectTaskByResource"
                                                ParentControlID="ddlProjectTitle"/>
                                     </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                        
                                    </asp:TemplateField >
                   
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="100px" />
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblEntryType" runat="server" Text='<%# Bind("entrytype") %>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlEntryType" runat="server" SkinID="ddl_100px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredField33" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectentrytype%>"
                                           ControlToValidate="ddlEntryType" Display="None" ValidationGroup="Group3" InitialValue=""></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                       
                                        
                                    </asp:TemplateField>
                                   
                                  
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Site%>">
                                       <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                          <ItemTemplate>
                                          <asp:Label ID="lblSite" runat="server" Text='<%# Bind("site") %>' Visible="false"></asp:Label>
                                           <asp:DropDownList ID="ddlSites" runat="server" SkinID="ddl_100px"  >
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="Required33" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>"
                                           ControlToValidate="ddlSites" Display="None" ValidationGroup="Group3" InitialValue=""></asp:RequiredFieldValidator>
                                           <ajaxToolkit:CascadingDropDown ID="casCadSitesGrid" runat="server"
                                            TargetControlID="ddlSites"
                                            Category="sites"
                                            PromptText="Please select..."
                                            ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                            ServiceMethod="GetSitesByProjectRef"
                                           ParentControlID="ddlProjectTitle"
                                            />
                                           
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lblCustomf" runat="server" Text='<%# Bind("custom") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        
                                       
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours %> " HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                        
                                        <asp:Label ID="lblid0" runat="server" Text='<%# Bind("c0_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours0" runat="server" Text='<%# ChangeHoures(Eval("c0").ToString())%>'
                                                 SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHours0"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidtimeFormat%>"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <FooterTemplate> 
                                        <asp:TextBox ID="txtHoursf1" runat="server" 
                                                Width="40px" SkinID="Price" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderText=" <%$ Resources:DeffinityRes,Hours%> " HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblid1" runat="server" Text='<%# Bind("c1_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours1" runat="server" Text='<%# ChangeHoures(Eval("c1").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex2" runat="server" ControlToValidate="txtHours1"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidtimeFormat%> "></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                          <asp:TextBox ID="txtHoursf2" runat="server" 
                                                SkinID="Time" MaxLength="5" ReadOnly="true"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours%>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblid2" runat="server" Text='<%# Bind("c2_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours2" runat="server" Text='<%# ChangeHoures(Eval("c2").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex3" runat="server" ControlToValidate="txtHours2"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidtimeFormat%> "></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                          <asp:TextBox ID="txtHoursf3" runat="server" 
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours%>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblid3" runat="server" Text='<%# Bind("c3_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours3" runat="server" Text='<%# ChangeHoures(Eval("c3").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex4" runat="server" ControlToValidate="txtHours3"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidtimeFormat%>"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                          <asp:TextBox ID="txtHoursf4" runat="server" 
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText=" <%$Resources:DeffinityRes,Hours%> " HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblid4" runat="server" Text='<%# Bind("c4_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours4" runat="server" Text='<%# ChangeHoures(Eval("c4").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex5" runat="server" ControlToValidate="txtHours4"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$Resources:DeffinityRes,PleaseentervalidtimeFormat%>"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                          <FooterTemplate> 
                                          <asp:TextBox ID="txtHoursf5" runat="server" 
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Hours%>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                        <asp:Label ID="lblid5" runat="server" Text='<%# Bind("c5_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours5" runat="server" Text='<%# ChangeHoures(Eval("c5").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex6" runat="server" ControlToValidate="txtHours5"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$Resources:DeffinityRes,PleaseentervalidtimeFormat%>"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                         <FooterTemplate> 
                                         <asp:TextBox ID="txtHoursf6" runat="server" 
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Hours %>" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblid6" runat="server" Text='<%# Bind("c6_id") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours6" runat="server" Text='<%# ChangeHoures(Eval("c6").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex7" runat="server" ControlToValidate="txtHours6"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$Resources:DeffinityRes,PleaseentervalidtimeFormat%>"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <FooterTemplate> 
                                        <asp:TextBox ID="txtHoursf7" runat="server" 
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Total%>" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_r">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtHours8" runat="server" Text='<%# ChangeHoures(Eval("ctotal").ToString())%>'
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                       
                                            <asp:RegularExpressionValidator ID="regex8" runat="server" ControlToValidate="txtHours8"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                                Display="None" Text="*" ErrorMessage="<%$Resources:DeffinityRes,PleaseentervalidtimeFormat%>"></asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                         <asp:TextBox ID="txtHoursf8" runat="server" 
                                                SkinID="Time" MaxLength="5"></asp:TextBox>
                                            
                                       </FooterTemplate>
                                    </asp:TemplateField>
                                   
                             </Columns>
                            </asp:GridView>

     <asp:Panel ID="VTGrid" runat="server">
                        
     <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.VacationRequests%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

    <asp:GridView ID="GridVT" runat="server" Width="100%" 
                                                 EmptyDataText="No request(s) available">
                                            <Columns>
                                           
                                                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,ID%>" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="FromDate" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="<%$Resources:DeffinityRes,FromDate%>" HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ToDate" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="<%$Resources:DeffinityRes,ToDate%>" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="AbsentTypeName" HeaderText="<%$Resources:DeffinityRes,AbsenceType%>" />
                                                <asp:BoundField DataField="Days" HeaderText="<%$Resources:DeffinityRes,Days%>" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ApprovalStatusName" HeaderText="<%$Resources:DeffinityRes,ApprovalStatus%>" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="RequestNotes" HeaderText="<%$Resources:DeffinityRes,RequestNotes%>" HeaderStyle-CssClass="header_bg_r" />
                                            </Columns>
                                            </asp:GridView>
    </asp:Panel>

    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Expenses%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                        EmptyDataText="No Expenses Data entered" OnRowCancelingEdit="GridView2_RowCancelingEdit"
                                        OnRowCommand="GridView2_RowCommand" OnRowEditing="GridView2_RowEditing" OnRowDeleting="GridView2_RowDeleting"
                                        OnRowUpdating="GridView2_RowUpdating" OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField  ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                                                <HeaderStyle Width="45px" />
                                                <ItemStyle Width="45px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkeditTandE" runat="server" CausesValidation="false" CommandName="Edit"
                                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkUpdateTandE" runat="server" CommandName="Update" Text="Update"
                                                        CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                                                        ToolTip="Update"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkCancelTandE" runat="server" CausesValidation="false" CommandName="Cancel"
                                                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblentryidTandE" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Date%>" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEndDateTandE" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'
                                                        Width="65px"></asp:TextBox><asp:Label ID="imgbtnenddate9" runat="server" SkinID="Calender"/>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                                        PopupButtonID="imgbtnenddate9" TargetControlID="txtEndDateTandE" CssClass="MyCalendar">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDateTandE"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,EndDatecannotbeblank%>" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEndDateTandE"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,Pleaseentervaliddateindatefield%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                        ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="Date_footerexpenses" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'
                                                        Width="65px"></asp:TextBox><asp:Label ID="imgbtnenddateexpensesDate" runat="server"
                                                            SkinID="Calender" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderexpensesDate" 
                                                        runat="server" PopupButtonID="imgbtnenddateexpensesDate" TargetControlID="Date_footerexpenses"
                                                        CssClass="MyCalendar">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="R6e" runat="server" ControlToValidate="Date_footerexpenses"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,EndDatecannotbeblank%>" ValidationGroup="FooterExpensesValidate"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="Re6e" runat="server" ControlToValidate="Date_footerexpenses"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,Pleaseentervaliddateindatefield%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                        ValidationGroup="FooterExpensesValidate">*</asp:RegularExpressionValidator>
                                                </FooterTemplate>
                                                <FooterStyle Width="100px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDateTandE" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,AssociatedTo%>">
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTitleTandE" runat="server" Width="130px" DataSourceID="SqlDataSourceTitle2"
                                                        DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTitleTandE"
                                                        ErrorMessage="<%$Resources:DeffinityRes,Pleaseselectproject%>" InitialValue="0" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectTanbE" runat="server" Text='<%# Bind("ProjectTitle") %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlTitle_footerexpenses" runat="server" Width="130px" DataSourceID="SqlDataSourceTitle2"
                                                        DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="R5e" runat="server" ControlToValidate="ddlTitle_footerexpenses"
                                                        ErrorMessage="<%$Resources:DeffinityRes,Pleaseselectproject%>" InitialValue="0" Display="None" ValidationGroup="FooterExpensesValidate"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                                <FooterStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,EntryType%>"  ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
                                                <HeaderStyle HorizontalAlign="Center" Width="135px" />
                                                <ItemStyle Width="135px" />
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEntryTandE" Width="120px" runat="server" DataSourceID="SqlDataSourceEntry2TandE"
                                                        AutoPostBack="true" DataTextField="ExpensesentryType" DataValueField="EntryTypeID"
                                                        OnSelectedIndexChanged="ddlEntryTandE_SelectedIndexChanged" SelectedValue='<%# Bind("EntryTypeID") %>'>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSourceEntry2TandE" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                        SelectCommand="SELECT ID as EntryTypeID,ExpensesentryType,BuyingPrice,sellingPrice FROM [ExpensesentryType]">
                                                    </asp:SqlDataSource>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryTandE" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <FooterStyle Width="135px" />
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlEntry_footerexpenses" SkinID="ddl_70" runat="server" DataSourceID="SqlDataSourceEntry2footer"
                                                        AutoPostBack="true" DataTextField="ExpensesentryType" DataValueField="EntryTypeID"
                                                        SelectedValue='<%# Bind("EntryTypeID") %>' OnSelectedIndexChanged="ddlEntry_footerexpenses_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:LinkButton ID="btn_expensesadd1" runat="server" SkinID="BtnLinkAdd"
                                                        CommandName="ExtraExpensesType" />
                                                    <asp:SqlDataSource ID="SqlDataSourceEntry2footer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                        SelectCommand="DEFFINITY_ExpensesType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    <asp:RequiredFieldValidator ID="R5eT" runat="server" ControlToValidate="ddlEntry_footerexpenses"
                                                        ErrorMessage="<%$Resources:DeffinityRes,Pleaseselectentrytype%>" InitialValue="0" Display="None" ValidationGroup="FooterExpensesValidate"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                               
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Qty%>">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Width="60px" Text='<%# Bind("Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQty_footerexpenses" runat="server" Text='<%# Bind("Qty") %>'
                                                        Width="50px"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator2e5" runat="server" ControlToValidate="txtQty_footerexpenses"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,PleaseentervalidQty%>" Operator="DataTypeCheck"
                                                        Type="Double" ValidationGroup="FooterExpensesValidate"></asp:CompareValidator>
                                                </FooterTemplate>
                                                <FooterStyle Width="60px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQtyTandE" runat="server" Text='<%# Bind("Qty") %>' Width="50px"
                                                        SkinID="Price"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator2e1" runat="server" ControlToValidate="txtQtyTandE"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,PleaseentervalidQty%>" Operator="DataTypeCheck"
                                                        Type="Double" ValidationGroup="Group2"></asp:CompareValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Cost%>">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("BuyingPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtUnitprice_expenses" runat="server" Text='<%# Bind("BuyingPrice") %>'
                                                        SkinID="Price_50px"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator2e5" runat="server" ControlToValidate="txtUnitprice_expenses"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,Pleaseentervalidunitprice%>" Operator="DataTypeCheck"
                                                        Type="Double" ValidationGroup="Group2"></asp:CompareValidator>
                                                </EditItemTemplate>
                                                <FooterStyle Width="40px" />
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtUnitprice_footerexpenses" runat="server" SkinID="Price_50px">
                                                    </asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator2e765" runat="server" ControlToValidate="txtUnitprice_footerexpenses"
                                                        Display="None" ErrorMessage="<%$Resources:DeffinityRes,Pleaseentervalidunitprice%>" Operator="DataTypeCheck"
                                                        Type="Double" ValidationGroup="FooterExpensesValidate"></asp:CompareValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,TotalCost%>" ItemStyle-Width="10%" FooterStyle-Width="10%">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmountTandE" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Notes%>">
                                                <HeaderStyle HorizontalAlign="Center"  />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNotesTandE" runat="server" Width="140px" TextMode="MultiLine"
                                                        Text='<%# Bind("Notes") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotesTandE" runat="server" Width="130px" Text='<%# Bind("Notes") %>' ToolTip='<%#Bind("Notes") %>'></asp:Label>
                                                </ItemTemplate>
                                               <FooterStyle Width="120px" />
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNotes_footerexpenses" runat="server" Width="140px"></asp:TextBox>
                                                </FooterTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_delete" runat="server" CausesValidation="false" CommandName="Delete"
                                                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                                </ItemTemplate>
                                                <FooterStyle Width="50px" />
                                                <FooterTemplate>
                                                    <asp:LinkButton CommandName="Insert_FooterExpenses" ID="ImageButton5a" runat="server"
                                                        SkinID="BtnLinkUpdate" ValidationGroup="FooterExpensesValidate" />&nbsp;<asp:LinkButton
                                                            CommandName="EmptyInsert_FooterExpenses" ID="ImageButton22" runat="server" SkinID="BtnLinkCancel" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                     <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="DN_TimeSheet_ProjectTile" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="ContractorID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
    
<script type="text/javascript">
    activeTab('Timesheet');
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>


