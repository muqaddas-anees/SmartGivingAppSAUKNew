<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="AddTimesheets.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Timesheets.AddTimesheets" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.Timesheet %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Add Timesheet
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    
<div class="form-group row">
          
              <div class="col-md-12">

                  <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                   <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                  </div>
    </div>
    <div class="row">
      <div class="col-md-4 d-flex d-inline">
           <label class="col-sm-4 control-label">Week Starting Date (Monday):</label>
           <div class="col-sm-6 d-flex d-inline"> <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="DateNew"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender" />
               <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View" ValidationGroup="Group1"
                                        OnClick="btn_viewdate_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" />

            </div>
	</div>
        <div class="col-md-4 d-flex d-inline">
        <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />
           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate8" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                        Display="None" ValidationGroup="Group1" ControlToValidate="txtweekcommencedate"></asp:RequiredFieldValidator>
         <asp:Button ID="imgSubmit" runat="server" SkinID="btnDefault" ToolTip="<%$ Resources:DeffinityRes,SendforApproval%>"
                                       Text="<%$ Resources:DeffinityRes,SendforApproval%>" style="display:none;visibility:hidden;"/>
	</div>
	<div class="col-md-4" style="display:none;visibility:hidden;">
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
	
</div>


     <ajaxToolkit:ModalPopupExtender ID="mdlAddTimesheet" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlAddTimesheet" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>

    <asp:Panel ID="pnlAddTimesheet" runat="server" BackColor="White" Style="display:none;"
                       Width="750px" Height="460px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Add Timesheet"></asp:Label> <asp:HiddenField ID="hid" runat="server" Value="0" />  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row">

 <div class="form-group row mb-5">
      
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Date%> :</label>
           <div class="col-sm-9 d-flex d-inline">
                <asp:TextBox ID="txtDate" runat="server" SkinID="DateNew"></asp:TextBox>
                            <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7"   runat="server"
                                PopupButtonID="imgbtnenddate7" TargetControlID="txtDate" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
            
	</div>
	
</div>

    
 <div class="form-group row mb-5">
     
           <label class="col-sm-2 control-label"><%:sessionKeys.JobsDisplayName %> </label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlJobs" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredProjectTile" runat="server" ErrorMessage="Please select"
                                ControlToValidate="ddlJobs" Display="None" ValidationGroup="Group2" InitialValue="0"></asp:RequiredFieldValidator>
                           
            </div>
	
	
</div>
            <div class="form-group row" style="display:none;visibility:hidden;">
     
           <label class="col-sm-2 control-label">Smart Tech</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSmartTech" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select smart tech"
                                ControlToValidate="ddlSmartTech" Display="None" ValidationGroup="Group2" InitialValue="0"></asp:RequiredFieldValidator>
                           
            </div>
	
	
</div>

   <div class="form-group row mb-5" >
     
           <label class="col-sm-2 control-label">From Time</label>
           <div class="col-sm-6 d-flex d-inline">
               <asp:TextBox ID="txtFromTime" runat="server" MaxLength="5" Text="" SkinID="Time"></asp:TextBox><span class="p-3">(hh:mm)</span>   
               <asp:DropDownList ID="ddlStartTime" runat="server" SkinID="ddl_100px">
                                <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
               <asp:RegularExpressionValidator ID="rvFromTime" runat="server" ControlToValidate="txtFromTime"
                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtime%>"></asp:RegularExpressionValidator> 
               <asp:Button ID="btnStart" runat="server" SkinID="btnDefault" ValidationGroup="Group2" ToolTip="<%$ Resources:DeffinityRes,Addanewentry%>"
                                OnClick="imgBtnAdd_Click" Text="Start" Visible="false" />
               </div>
          
        </div>
          <div class="form-group row mb-5">
     
    <label class="col-sm-2 control-label">To Time</label>
           <div class="col-sm-6 d-flex d-inline">
               <asp:TextBox ID="txtToTime" runat="server" MaxLength="5" Text="" SkinID="Time"></asp:TextBox><span class="p-3">(hh:mm)</span>    
               <asp:DropDownList ID="ddlEndtime" runat="server" SkinID="ddl_100px">
                                <asp:ListItem Text="AM" Value="AM" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
                <asp:RegularExpressionValidator ID="revToTime" runat="server" ControlToValidate="txtToTime"
                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtime%>"></asp:RegularExpressionValidator>
               </div>
         
        </div>
            <div class="form-group row mb-5">
     
           <label class="col-sm-2 control-label"></label>
           <div class="col-sm-6">
               <asp:Button ID="imgBtnAdd" runat="server" SkinID="btnSubmit" ValidationGroup="Group2" ToolTip="<%$ Resources:DeffinityRes,Addanewentry%>"
                                OnClick="imgBtnAdd_Click" />
            </div>
	</div>
	<div class="col-md-6">
          
	</div>

	
</div>

    

 
        </div>
        
    </asp:Panel>
    <div class="form-group row mb-5">
              <div class="col-md-12">
                  <asp:Button ID="btnAddTimesheet" runat="server" SkinID="btnDefault" Text="Add Timesheet" style="text-align:right;float:right;" OnClick="btnAddTimesheet_Click"  />
                  </div>
        </div>
    <asp:HiddenField ID="htimeid" runat="server" />
    <asp:GridView ID="grdTimeSheetEntry" runat="server" Width="100%" AutoGenerateColumns="False"
                    EmptyDataText="No Timesheet(s) available" OnRowDataBound="grdTimeSheetEntry_RowDataBound"
                    OnRowCommand="grdTimeSheetEntry_RowCommand">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="form-inline">
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                 <div style="width:40px;padding-bottom:5px">       <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="edit1"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>" 
                                            Visible='<%# GetTimeSheetStatusCheck(Eval("TimesheetstatusID").ToString())%>'>
                                        </asp:LinkButton> </div>
                                <asp:Button ID="btnStop" runat="server"  Text="Stop Timer" CommandArgument='<%# Bind("ID")%>' SkinID="btnDefault" CommandName="stopped" />
                                <asp:Button ID="btnResumit" runat="server" Visible='<%# GetTimeSheetStatusDeclineCheck(Eval("TimesheetstatusID").ToString())%>' Text="Resubmit" CommandArgument='<%# Bind("ID")%>' SkinID="btnDefault" CommandName="resubmit" />
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Date%>" ItemStyle-CssClass="form-inline">
                           
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                            
                            <ItemTemplate>
                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Job" ItemStyle-CssClass="col-nowrap">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="55%" />
                            <ItemTemplate>
                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,EntryType%>" ItemStyle-CssClass="col-nowrap" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            
                            <ItemTemplate>
                                <asp:Label ID="lblEntry" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="From Time (HH:MM)" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridStartTime" runat="server" ></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText=" To Time (HH:MM)" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridEndTime" runat="server"></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duration" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>" Visible="false">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblStaus_Time"  runat="server" Text='<%# TimesheetStatus(Eval("TimesheetstatusID").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="del" 
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"
                                    Visible='<%# GetTimeSheetStatusCheck(Eval("TimesheetstatusID").ToString())%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
