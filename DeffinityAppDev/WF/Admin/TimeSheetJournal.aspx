<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="TimeSheetJournal" MaintainScrollPositionOnPostback="true"  EnableEventValidation ="false" Codebehind="TimeSheetJournal.aspx.cs" %>
<%@ Register Src="MailControls/Timesheet_AdminMail.ascx" TagName="MailControl" TagPrefix="uc2" %>
<%@ Register src="controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<%@ Register Src="MailControls/Timesheet_Pendingmail.ascx" TagName="Pendingmail" TagPrefix="uc5" %>
<%@ Register src="controls/TimesheetSubTabs.ascx" tagname="TimesheetSubTabs" tagprefix="uc1" %>
<%@ Register Src="controls/TimesheetReportsDropdown.ascx" TagName="ReportDropdown" TagPrefix="ucReportDropdwon" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Tabs" runat="Server">
    <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Resources%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
   <ucReportDropdwon:ReportDropdown ID="ucDropdown" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Timesheet%> - <%= Resources.DeffinityRes.TimesheetJournal%> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <uc2:MailControl ID="TimeMail" runat="server" Visible="false"></uc2:MailControl>
     <uc5:Pendingmail ID="Pendingmail" runat="server" Visible="false"></uc5:Pendingmail>
    <script type="text/javascript">
      
  
        function TimsheetFun() {

            var grid1 = document.getElementById("<%=GridView4.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            var Flag = 0;
            if (grid1.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid1.rows.length; i++) {
                    //get the reference of first column
                    cell = grid1.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid

                            if (cell.childNodes[j].checked) {
                                Flag = 1;
                            }

                        }
                    }
                }
            }
            if (Flag == 0) {

                alert("Please select at least one timesheet Week..");
                return false;
            }
            else {
                return true;
            }

        }

        function SelectAll1(id) {
            //get reference of GridView control
            var grid1 = document.getElementById("<%=GridView4.ClientID %>");
            //variable to contain the cell of the grid
            var cell;


            if (grid1.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid1.rows.length; i++) {
                    //get the reference of first column
                    cell = grid1.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;


                        }
                    }
                }
            }

        }
    </script>
     <uc1:TimesheetSubTabs ID="TimesheetSubTabs1" runat="server" />
    <div class="form-group">
     <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
        </div>
    
    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
    <div class="form-group">
                                  <div class="col-md-4">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Resource%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="Dropdownlist4" SkinID="ddl_80" runat="server">
                                        </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-3">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.FromDate%></label>
                                      <div class="col-sm-7 form-inline"> <asp:TextBox ID="txt_startDate" runat="server" SkinID="Date"> </asp:TextBox> <asp:Label ID="img_from" runat="server" SkinID="Calender" />
					</div>
				</div>
<div class="col-md-3">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ToDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txt_EndDate" runat="server" SkinID="Date"></asp:TextBox><asp:Label ID="Image2" runat="server" SkinID="Calender" />
					</div>
				</div>
        <div class="col-md-2">
              <asp:Button ID="btn_filter" runat="server" SkinID="btnDefault" Text="Filter"
                                            OnClick="btn_filter_Click" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                            PopupButtonID="img_from" TargetControlID="txt_startDate" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  runat="server"
                                            PopupButtonID="Image2" TargetControlID="txt_EndDate" CssClass="MyCalendar">
                                        </ajaxToolkit:CalendarExtender>
            </div>
</div>
   
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" Width="100%"
                                            AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging" PageSize="20"
                                            OnRowCommand="GridView3_RowCommand" EmptyDataText="No timsheet data found.">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Resource">
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="170px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName11" runat="server" Width="160px" Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Week Commencing Date">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEndDate1" runat="server" Text='<%# Bind("WCDate","{0:d}") %>' Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Submitted Date">
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                <asp:Label ID="lblSSubmitDate" runat="server" Text='<%#  FormateDate(Eval("SubmittedDate", "{0:dd/MM/yyyy H:mm:ss}")) %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Hours">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                          <asp:Label ID="lblTotalHours1" runat="server" Text='<%# ChangeHoues(Eval("TotalHours").ToString())%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Primary Approver" >
                                                    <ItemStyle Width="170px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrimary" runat="server" Text='<%# Bind("PrimeApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date and Time">
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                <asp:Label ID="lblPrimeApDate" runat="server" Text='<%#  FormateDate(Eval("PrimeApprovedDate", "{0:dd/MM/yyyy H:mm:ss} ")) %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Secondary Approver" >
                                                    <ItemStyle Width="170px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSecondary" runat="server" Text='<%# Bind("SecondApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date and Time">
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                <asp:Label ID="lblSecondApDate" runat="server" Text='<%#  FormateDate(Eval("SecondApprovedDate", "{0:dd/MM/yyyy H:mm:ss}")) %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Approver" >
                                                    <ItemStyle Width="170px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerApprover" runat="server" Text='<%# Bind("CustomerApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date and Time">
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                <asp:Label ID="lblCustomer_date" runat="server" Text='<%#  FormateDate(Eval("Customer_date", "{0:dd/MM/yyyy H:mm:ss}")) %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("TimeSheetStatusName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_view1" runat="server" CausesValidation="false" CommandName="View_journal"
                                                            CommandArgument='<%# Bind("WCDateID")%>' SkinID="BtnLinkHistory" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

  
  <div class="form-group">
    <asp:Label ID="BtntempSearchModal" runat="server" />
    <asp:Label ID="Label1" runat="server" />
      </div>
        <ajaxToolkit:ModalPopupExtender ID="ModalControlExtender2" 
        BackgroundCssClass="modalBackground" runat="server" TargetControlID="BtntempSearchModal" CancelControlID="Label1"
        PopupControlID="GetViewofSendforApproval">
    </ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel style="display:none;" ID="GetViewofSendforApproval" runat="server" BackColor="White"  BorderStyle="Double" Width="85%" Height="450px"  BorderColor="LightSteelBlue" ScrollBars="None">
            <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.TimesheetViewer %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                Width="100%" EmptyDataText="No timesheet data found" AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging"
                                                OnRowDataBound="GridView4_RowDataBound" OnRowCommand="GridView4_RowCommand" OnRowEditing="GridView4_RowEditing"
                                                OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowUpdating="GridView4_RowUpdating" Font-Size="X-Small">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll1" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect2" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle />
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                                CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                                                                ToolTip="Update"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Width="40px" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Resource Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContractorID" runat="server" Width="40px" Text='<%# Bind("ContractorID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Resource Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEntryType" runat="server" Width="40px" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemStyle Width="60px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site">
                                                        <HeaderStyle />
                                                        <ItemStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSite111" runat="server" Width="80px" Text='<%# Bind("Site") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pref" Visible="false">
                                                        <HeaderStyle  />
                                                        <ItemStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPref" runat="server" Width="80px" Text='<%# Bind("Pref") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Title">
                                                        <HeaderStyle  />
                                                        <ItemStyle Width="160px" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlTitle" runat="server" Width="140px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTitle" runat="server" Width="140px" Text='<%# Bind("ProjectReference") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hours">
                                                        <HeaderStyle  HorizontalAlign="Center" />
                                                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtHours" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'
                                                                SkinID="Price_60px" MaxLength="5"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHours"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                                                Display="None" Text="*" ErrorMessage="Please enter valid time. Format hh:mm "></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                              <asp:Label ID="lblHours" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <HeaderStyle  />
                                                        <ItemStyle Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTask" runat="server" Text='<%# Bind("ProjectTask") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                                 <EditItemTemplate>
                                            <asp:HiddenField id="GetTaskID" runat="server" Value='<%# Bind("Pref") %>'/>
                                            <asp:DropDownList ID="GetProjectTasks" Width="120px" runat="server" >
                                        </asp:DropDownList>
                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SR">
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle  />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSR" runat="server" Text='<%# Bind("SRnumber") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemStyle Width="140px" />
                                                        <HeaderStyle  />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotes" runat="server" Text='<%# Bind("Notes") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Approver's Comments">
                                                        <ItemStyle Width ="140px" />
                                                        <HeaderStyle Width="140px" />
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblAprComnts" runat="server" Text='<%# Bind("ApproverComments") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Planner">
                                                        <ItemStyle Width="25px" />
                                                        <HeaderStyle Width="25px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplanner" runat="server" Text='<%# ChangePlanner(Eval("Planner").ToString())%>'
                                                                Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                        <HeaderStyle  CssClass="header_bg_r" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="server" Width="100px" Text='<%# Bind("Shift") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
            <div id="GetIDAccept" runat="server" visible="false" class="form-group">
                <div class="form-group">
      <div class="col-md-4" style="padding-left:35px">
            <asp:TextBox ID="TextBox1" runat="server" SkinID="txt_50"  Visible="true"></asp:TextBox> 
	</div>
	<div class="col-md-4 form-inline">
            <asp:Button ID="btn_approve" runat="server" SkinID="btnDefault" Text="Approve" OnClick="btn_approve_Click"
                                           OnClientClick="return TimsheetFun();" />
                                        <asp:Button ID="btn_declined"
                                                runat="server" SkinID="btnDefault" Text="Decline" OnClick="btn_declined_Click" OnClientClick="return TimsheetFun();" />
	</div>
	<div class="col-md-4">
          
	</div>
</div>
                                  
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12 form-inline">
                                    <asp:Button ID="ImgClose" runat="server" SkinID="btnCancel"  />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                    </div>
                                </div>
       
        </asp:Panel>  
       
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 

  
</asp:Content>
