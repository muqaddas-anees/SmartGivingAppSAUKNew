<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" EnableEventValidation="false"
    AutoEventWireup="true" Inherits="Resource_TimeSheetResourcesDaily"
    MaintainScrollPositionOnPostback="true" Codebehind="TimeSheetResourcesDaily.aspx.cs" %>

<%@ Register Src="controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<%@ Register Src="MailControls/ResourceTimesheetAler.ascx" TagName="MailControl"
    TagPrefix="uc2" %>
<%@ Register Src="Mailcontrols/TimesheetApproveDetails.ascx" TagName="Timesheet"
    TagPrefix="uc2" %>
<%@ Register Src="Mailcontrols/TimesheetProjectsub.ascx" TagName="TimesheetProject"
    TagPrefix="uc3" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <asp:Label ID="lblTitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
   <asp:HyperLink ID="linkReport" runat="server" NavigateUrl="~/WF/Reports/MyTasksTimeSheetReport.aspx"
                Font-Bold="true" Text="View Timesheet Report" Target="_blank"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectStatus ID="ProjectStatus1" runat="server"></uc1:ProjectStatus>
    <uc2:Timesheet ID="Timesheet1" runat="server" Visible="false"></uc2:Timesheet>
    <uc3:TimesheetProject ID="TimesheetProject1" runat="server" Visible="false"></uc3:TimesheetProject>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="form-group">
          
              <div class="col-md-8">
                  <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="Group1" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group2" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group3" />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group4" />
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="FooterExpensesValidate" />
                <asp:Label ID="lblMsg" runat="server" Visible="False" ForeColor="Red" EnableViewState="false"></asp:Label>
                <asp:Label ID="lblstatus" Visible="false" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                <asp:Label ID="lblError" Visible="false" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                <asp:Label ID="lblResultTEST" runat="server" Visible="false" ForeColor="Red" EnableViewState="false"></asp:Label>
                <asp:HiddenField ID="hidDate" runat="server" />
                  </div>
             <div class="col-md-4 pull-right" id="pnlShowDropdown" runat="server" style="float:right">
                <select id='ddlReport' class="pull-right">
                    <option value="0">Select View</option>
                    <option value="TimeSheetResourcesDaily.aspx">Timesheet Daily View</option>
                    <option value="TimeSheetResourcesWeekly.aspx">Timesheet Weekly View</option>
                    <!-- <option value="TimeSheetResourcesMonthly.aspx">Timesheet Monthly View</option> -->
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

    <div class="form-group well">
      <div class="col-md-4">
           <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.WeekCommencingDate%>:</label>
           <div class="col-sm-5 form-inline"> <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="Date"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender" />

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%> :</label>
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
        <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />
           <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View" ValidationGroup="Group1"
                                        OnClick="btn_viewdate_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate8" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                        Display="None" ValidationGroup="Group1" ControlToValidate="txtweekcommencedate"></asp:RequiredFieldValidator>
         <asp:Button ID="imgSubmit" runat="server" SkinID="btnDefault" OnClick="imgSubmit_Click" ToolTip="<%$ Resources:DeffinityRes,SendforApproval%>"
                                       Text="<%$ Resources:DeffinityRes,SendforApproval%>" />
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Date%> :</label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtDate" runat="server" SkinID="Date"></asp:TextBox>
                            <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7"   runat="server"
                                PopupButtonID="imgbtnenddate7" TargetControlID="txtDate" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ServiceRequest%> :</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtServiceRequest" runat="server" SkinID="txt_90"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="reserviceReq" runat="server" ErrorMessage="Please enter service request"
                                           ControlToValidate="txtServiceRequest" Display="None" ValidationGroup="Group2" ></asp:RequiredFieldValidator>--%>
            </div>
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectTitle%> :</label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlProjectTile" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredProjectTile" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectprojecttitle%>"
                                ControlToValidate="ddlProjectTile" Display="None" ValidationGroup="Group2" InitialValue="Please select..."></asp:RequiredFieldValidator>
                            <%--<ajaxToolkit:CascadingDropDown ID="casCadProjectTile" runat="server"
    TargetControlID="ddlProjectTile"
    Category="Title"
    PromptText="Please select..."
    ServicePath="~/ServiceMgr.asmx"
    ServiceMethod="GetProjectsTitle2"
    ParentControlID="ddlCustomers"
    />  --%>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Site%> :</label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select site"
                                           ControlToValidate="ddlSite" Display="None"
                                            ValidationGroup="Group2" InitialValue="">
                                            </asp:RequiredFieldValidator>--%>
                            <ajaxToolkit:CascadingDropDown ID="casCadSite" runat="server" TargetControlID="ddlSite"
                                Category="Task" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                ServiceMethod="GetSitesByProjRef" ParentControlID="ddlProjectTile" />
            </div>
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ProjectTask%> :</label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlTasks" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="RequiredTasks" runat="server" ErrorMessage="Please select task"
                                           ControlToValidate="ddlTasks" Display="None"
                                            ValidationGroup="Group2" InitialValue=""></asp:RequiredFieldValidator>--%>
                            <ajaxToolkit:CascadingDropDown ID="casCadTasks" runat="server" TargetControlID="ddlTasks"
                                Category="Task" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                ServiceMethod="GetProjectsTasksbyPortFolio" ParentControlID="ddlProjectTile" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Notes%> :</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtAddNotes" runat="server" SkinID="txt_90" Height="40px" TextMode="MultiLine"></asp:TextBox>
            </div>
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.EntryType%> :</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlType" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please select entry type"
                                           ControlToValidate="ddlType" Display="None"
                                            ValidationGroup="Group2" InitialValue="0">
                                            </asp:RequiredFieldValidator>--%>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Hours%> :</label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtHours" runat="server" SkinID="Time" MaxLength="5"></asp:TextBox><span>(hh:mm)</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtime%>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtHours"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHours"
                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtime%>"></asp:RegularExpressionValidator>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>

    
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
               <asp:Button ID="imgBtnAdd" runat="server" SkinID="btnAdd" ValidationGroup="Group2" ToolTip="<%$ Resources:DeffinityRes,Addanewentry%>"
                                OnClick="imgBtnAdd_Click" />
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblSubmit" runat="server" Visible="false"></asp:Label>
	</div>
</div>

    <asp:GridView ID="grdTimeSheetEntry" runat="server" Width="100%" AutoGenerateColumns="False"
                    EmptyDataText="No Timesheet(s) available" OnRowDataBound="grdTimeSheetEntry_RowDataBound"
                    OnRowCommand="grdTimeSheetEntry_RowCommand" OnRowDeleting="grdTimeSheetEntry_RowDeleting"
                    OnRowEditing="grdTimeSheetEntry_RowEditing" OnRowUpdating="grdTimeSheetEntry_RowUpdating"
                    OnRowCancelingEdit="grdTimeSheetEntry_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="form-inline">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>" 
                                            Visible='<%# GetTimeSheetStatusCheck(Eval("TimesheetstatusID").ToString())%>'>
                                        </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group3" SkinID="BtnLinkUpdate"
                                            ToolTip="<%$ Resources:DeffinityRes,Update%>"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>"></asp:LinkButton></div>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>" ItemStyle-CssClass="form-inline">
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                            <EditItemTemplate>
                                        <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                            SkinID="Date"></asp:TextBox>
                                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                            PopupButtonID="imgbtnenddate6" TargetControlID="txtEndDate" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Datecannotbeblank%>" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervaliddateindatefield%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                            ValidationGroup="Group3">*</asp:RegularExpressionValidator></div>
                              
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                    Width="75px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectReference%>">
                            <ItemTemplate>
                                <asp:Label ID="lblRef" runat="server" Text='<%# Bind("ProjectRef") %>' Width="75px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlProjectTitle" runat="server" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="ddlProjectTitle_SelectedIndexChanged" Width="140px">
                                    
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTask%>">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblProjectTasks" runat="server" Text='<%# Eval("ProjectTask") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="250px" />
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlProjectTask" runat="server" Width="140px">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="casCadProjectTask" runat="server" TargetControlID="ddlProjectTask"
                                    Category="Task1" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetProjectsTasksbyPortFolio" ParentControlID="ddlProjectTitle" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ServiceRequest%>">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="75px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="txtActivity" runat="server" Text='<%# TimeSheetActivity(Eval("Activity").ToString())%>'
                                    Width="75px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblActivity" runat="server" Text='<%# GetTimeSheetActivity(Eval("Activity").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <%-- <FooterTemplate>
                                            <asp:TextBox ID="TimeSheet_txtActivity" runat="server" Text='<%# Bind("Activity")%>'
                                                Width="75px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Width="75px" />--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblEntry" runat="server" Width="100px" Text='<%# Bind("EntryType") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEntryType" runat="server" Width="140px">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredField33" runat="server" ErrorMessage="Please select entry type"
                                           ControlToValidate="ddlEntryType" Display="None" ValidationGroup="Group3" InitialValue=""></asp:RequiredFieldValidator>--%>
                                <ajaxToolkit:CascadingDropDown ID="casCadEntryType" runat="server" TargetControlID="ddlEntryType"
                                    Category="Type" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx" ServiceMethod="GetEntryTypes" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours%>" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHoursE" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'
                                    Width="40px" SkinID="Price" MaxLength="5"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHoursE"
                                    ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group3"
                                    Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidtimeFormat%> "></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Site%>">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                            <ItemTemplate>
                                <asp:Label ID="lblsite" runat="server" Text='<%# Bind("sitename") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSites" runat="server" Width="140px">
                                </asp:DropDownList>
                                <%-- <asp:RequiredFieldValidator ID="Required33" runat="server" ErrorMessage="Please select site"
                                           ControlToValidate="ddlSites" Display="None" ValidationGroup="Group3" InitialValue=""></asp:RequiredFieldValidator>--%>
                                <ajaxToolkit:CascadingDropDown ID="casCadSitesGrid" runat="server" TargetControlID="ddlSites"
                                    Category="sites" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetSitesByProjectRef" ParentControlID="ddlProjectTitle" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblStaus_Time"  runat="server" Text='<%# TimesheetStatus(Eval("TimesheetstatusID").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>">
                            <ItemStyle Width="100px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Notes") %>' ToolTip='<%#Bind("Notes") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete" 
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"
                                    Visible='<%# GetTimeSheetStatusCheck(Eval("TimesheetstatusID").ToString())%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

    <asp:Panel ID="VTGrid" runat="server">
        
<div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.VacationRequests%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                       

                                            <asp:GridView ID="GridVT" runat="server" Width="100%" 
                                                 EmptyDataText="No request(s) available">
                                            <Columns>
                                           
                                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ID%>" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="FromDate" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="<%$ Resources:DeffinityRes,FromDate%>" HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ToDate" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="<%$ Resources:DeffinityRes,ToDate%>" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="AbsentTypeName" HeaderText="<%$ Resources:DeffinityRes,AbsenceType%>" />
                                                <asp:BoundField DataField="Days" HeaderText="<%$ Resources:DeffinityRes,Days%>" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ApprovalStatusName" HeaderText="<%$ Resources:DeffinityRes,ApprovalStatus%>" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="RequestNotes" HeaderText="<%$ Resources:DeffinityRes,RequestNotes%>" HeaderStyle-CssClass="header_bg_r" />
                                            </Columns>
                                            </asp:GridView>
                        </asp:Panel>
    
<div class="form-group">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.Expenses%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <asp:UpdatePanel ID="pnlExpenses" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                            EmptyDataText="No Expenses Data entered" OnRowCancelingEdit="GridView2_RowCancelingEdit"
                            OnRowCommand="GridView2_RowCommand" OnRowEditing="GridView2_RowEditing" OnRowDeleting="GridView2_RowDeleting"
                            OnRowUpdating="GridView2_RowUpdating" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                    <HeaderStyle Width="45px" />
                                    <ItemStyle Width="45px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkeditTandE" runat="server" CausesValidation="false" CommandName="Edit" 
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkUpdateTandE" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group4" SkinID="BtnLinkUpdate"
                                            ToolTip="<%$ Resources:DeffinityRes,Update%>"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkCancelTandE" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblentryidTandE" runat="server" Visible="false" Text='<%# Eval("ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>" FooterStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEndDateTandE" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'
                                            SkinID="Date"></asp:TextBox><asp:Label ID="imgbtnenddate9" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                            PopupButtonID="imgbtnenddate9" TargetControlID="txtEndDateTandE" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndDateTandE"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,EndDatecannotbeblank%>" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEndDateTandE"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervaliddateindatefield%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                            ValidationGroup="Group4">*</asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="Date_footerexpenses" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'
                                            SkinID="Date"></asp:TextBox><asp:Label ID="imgbtnenddateexpensesDate" runat="server"
                                                SkinID="Calender"  />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderexpensesDate" 
                                            runat="server" PopupButtonID="imgbtnenddateexpensesDate" TargetControlID="Date_footerexpenses"
                                            CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="R6e" runat="server" ControlToValidate="Date_footerexpenses"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,EndDatecannotbeblank%>" ValidationGroup="FooterExpensesValidate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="Re6e" runat="server" ControlToValidate="Date_footerexpenses"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervaliddateindatefield%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                            ValidationGroup="FooterExpensesValidate">*</asp:RegularExpressionValidator>
                                    </FooterTemplate>
                                    <FooterStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDateTandE" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,AssociatedTo%>">
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="150px" />
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTitleTandE" runat="server" Width="150px" DataSourceID="SqlDataSourceTitle2"
                                            DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTitleTandE"
                                            ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectproject%>" InitialValue="0" Display="None" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectTanbE" runat="server" Text='<%# Bind("ProjectTitle") %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlTitle_footerexpenses" runat="server" Width="150px" DataSourceID="SqlDataSourceTitle2"
                                            DataTextField="ProjectTitle" DataValueField="ProjectReference" SelectedValue='<%# Bind("ProjectReference") %>'>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="R5e" runat="server" ControlToValidate="ddlTitle_footerexpenses"
                                            ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectproject%>" InitialValue="0" Display="None" ValidationGroup="FooterExpensesValidate"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                    <FooterStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle Width="150px" />
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlEntryTandE" Width="150px" runat="server" DataSourceID="SqlDataSourceEntry2TandE"
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
                                    <FooterStyle Width="150px" />
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlEntry_footerexpenses" Width="150px" runat="server" DataSourceID="SqlDataSourceEntry2footer"
                                            AutoPostBack="true" DataTextField="ExpensesentryType" DataValueField="EntryTypeID"
                                            SelectedValue='<%# Bind("EntryTypeID") %>' OnSelectedIndexChanged="ddlEntry_footerexpenses_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<asp:ImageButton ID="btn_expensesadd1" runat="server" SkinID="ImgSymAdd" ImageAlign="Middle"
                                            CommandName="ExtraExpensesType" />--%>
                                        <asp:SqlDataSource ID="SqlDataSourceEntry2footer" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="DEFFINITY_ExpensesType" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="R5eT" runat="server" ControlToValidate="ddlEntry_footerexpenses"
                                            ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectentrytype%>" InitialValue="0" Display="None" ValidationGroup="FooterExpensesValidate"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Qty%>">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Width="60px" Text='<%# Bind("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtQty_footerexpenses" runat="server" Text='<%# Bind("Qty") %>'
                                            Width="50px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2e5" runat="server" ControlToValidate="txtQty_footerexpenses"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidQty%>" Operator="DataTypeCheck"
                                            Type="Double" ValidationGroup="FooterExpensesValidate"></asp:CompareValidator>
                                    </FooterTemplate>
                                    <FooterStyle Width="60px" />
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQtyTandE" runat="server" Text='<%# Bind("Qty") %>' Width="50px"
                                            SkinID="Price"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2e1" runat="server" ControlToValidate="txtQtyTandE"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidQty%>" Operator="DataTypeCheck"
                                            Type="Double" ValidationGroup="Group4"></asp:CompareValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Cost%>">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("BuyingPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUnitprice_expenses" runat="server" Text='<%# Bind("BuyingPrice") %>'
                                            Width="40px" SkinID="Price"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2e5" runat="server" ControlToValidate="txtUnitprice_expenses"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidunitprice%>" Operator="DataTypeCheck"
                                            Type="Double" ValidationGroup="Group4"></asp:CompareValidator>
                                    </EditItemTemplate>
                                    <FooterStyle Width="40px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtUnitprice_footerexpenses" runat="server" Width="40px" SkinID="Price">
                                        </asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2e765" runat="server" ControlToValidate="txtUnitprice_footerexpenses"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidunitprice%>" Operator="DataTypeCheck"
                                            Type="Double" ValidationGroup="FooterExpensesValidate"></asp:CompareValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalCost%>">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountTandE" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,Notes%>">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNotesTandE" runat="server" Width="140px" TextMode="MultiLine"
                                            Text='<%# Bind("Notes") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotesTandE" runat="server" Width="130px" Text='<%# Bind("Notes") %>'
                                            ToolTip='<%#Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Width="120px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNotes_footerexpenses" runat="server" Width="140px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_delete" runat="server" CausesValidation="false" CommandName="Delete"
                                            SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                    </ItemTemplate>
                                    <FooterStyle Width="50px" />
                                    <FooterTemplate>
                                        <asp:LinkButton CommandName="Insert_FooterExpenses" ID="ImageButton5a"  runat="server" ToolTip="<%$ Resources:DeffinityRes,Insertnew%>"
                                            SkinID="BtnLinkUpdate" ValidationGroup="FooterExpensesValidate" />&nbsp;<asp:LinkButton
                                                CommandName="EmptyInsert_FooterExpenses" ID="ImageButton22" runat="server" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DN_TimeSheet_ProjectTile" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="ContractorID" SessionField="UID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
   
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

