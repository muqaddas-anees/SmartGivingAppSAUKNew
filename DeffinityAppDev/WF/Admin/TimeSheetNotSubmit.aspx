<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="TimeSheetNotSubmit" MaintainScrollPositionOnPostback="true"  EnableEventValidation ="false" Codebehind="TimeSheetNotSubmit.aspx.cs" %>
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
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.Timesheet%> -  <%= Resources.DeffinityRes.Timesheetsnotsubmitted%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">

    <ucReportDropdwon:ReportDropdown ID="ucDropdown" runat="server" />
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

                alert("Please select at least one timesheet Week");
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
        <div class="col-md-12">
    <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </div>
        </div>

  <div class="form-group">
        <div class="col-md-12 form-inline">
             <label>Timesheet not submitted</label>
                             <asp:DropDownList ID="DropDownList1"
                                 runat="server" DataTextFormatString="{0:d}" SkinID="ddl_125px">
                            </asp:DropDownList><asp:Button ID="btn_View" runat="server" SkinID="btnDefault" Text="View"
                                OnClick="btn_View_Click"  />
            </div>
      </div>
    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="false"
                                            PageSize="20" OnSorting="GridView2_Sorting" AllowSorting="true" EmptyDataText="No timesheet data found"
                                            AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" 
                                            onrowcommand="GridView2_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Resource" SortExpression="ContractorName">
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName1" runat="server" Width="160px" Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Week Commencing Date" SortExpression="WCDate">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWCDate" runat="server" Text='<%# Bind("WCDate","{0:d}") %>' Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resource" Visible="false">
                                                    <HeaderStyle CssClass="header_bg_l" />
                                                    <ItemStyle Width="120px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName_ID" runat="server" Width="120px" Text='<%# Bind("ContractorID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Primary Approver" >
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrimary" runat="server" Text='<%# Bind("PrimeApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Secondary Approver" >
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSecondary" runat="server" Text='<%# Bind("SecondApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle Width="50px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_view1" runat="server" CausesValidation="false" CommandName="View"
                                                            CommandArgument='<%# Bind("WCDateID")%>' SkinID="BtnLinkHistory" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Reminder">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_EmialRmainder1" runat="server" CausesValidation="false" SkinID="BtnLinkAdd"
                                                            CommandName="EmailReminder" CommandArgument='<%# Bind("WCDateID")%>'  />
                                                        <%--ImageUrl="media/ico_email_reminder.png"--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
   
     <div class="form-group">
        <div class="col-md-12">
            <asp:Label ID="BtntempSearchModal" runat="server" />
    <asp:Label ID="Label1" runat="server" />
            </div>
         </div>
  
    
        <ajaxToolkit:ModalPopupExtender ID="ModalControlExtender2" 
        BackgroundCssClass="modalBackground" runat="server" TargetControlID="BtntempSearchModal" CancelControlID="Label1"
        PopupControlID="GetViewofSendforApproval">
    </ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel style="display:none;" ID="GetViewofSendforApproval" runat="server" BackColor="White"  BorderStyle="Double" Width="85%" Height="450px" BorderColor="LightSteelBlue" ScrollBars="None">
         
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.TimesheetViewer %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
            <div class="form-group">
        <div class="col-md-12">
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                Width="1050px" EmptyDataText="No timesheet data found" AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging"
                                                OnRowDataBound="GridView4_RowDataBound" OnRowCommand="GridView4_RowCommand" OnRowEditing="GridView4_RowEditing"
                                                OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowUpdating="GridView4_RowUpdating" Font-Size="X-Small" >
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll1" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect2" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="header_bg_l" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle Width="65px" />
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Button ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                                CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="btnUpdate"
                                                                ToolTip="Update"></asp:Button>
                                                            <asp:Button ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                SkinID="btnCancel" ToolTip="Cancel"></asp:Button>
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
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSite111" runat="server" Width="80px" Text='<%# Bind("Site") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pref" Visible="false">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPref" runat="server" Width="80px" Text='<%# Bind("Pref") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Title">
                                                        <HeaderStyle Width="160px" />
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
                                                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
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
                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTask" runat="server" Text='<%# Bind("ProjectTask") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                                 <EditItemTemplate>
                                            <asp:HiddenField id="GetTaskID" runat="server" Value='<%# Bind("Pref") %>'/>
                                            <asp:DropDownList ID="GetProjectTasks" Width="120px" runat="server" 
                                                >
                                        </asp:DropDownList>

                                        
                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SR">
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSR" runat="server" Text='<%# Bind("SRnumber") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemStyle Width="140px" />
                                                        <HeaderStyle Width="140px" />
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
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="header_bg_r" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="server" Width="100px" Text='<%# Bind("Shift") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
            </div>
             </div>
             <div id="GetIDAccept" runat="server" visible="false" class="form-group">
                 <div class="form-group">
      <div class="col-md-4" style="padding-left:35px">
           <asp:TextBox ID="TextBox1" runat="server"  Visible="true"></asp:TextBox> 
	</div>
	<div class="col-md-4 form-inline">
           <asp:Button ID="btn_approve" runat="server" Text="Approve" SkinID="btnDefault" OnClick="btn_approve_Click"
                                           OnClientClick="return TimsheetFun();" />&nbsp;<asp:Button ID="btn_declined"
                                                runat="server" Text="Decline" SkinID="btnDefault" OnClick="btn_declined_Click"  OnClientClick="return TimsheetFun();" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField3" runat="server" /><asp:HiddenField ID="HiddenField4" runat="server" /><asp:HiddenField ID="HiddenField2" runat="server" />
	</div>
	<div class="col-md-4 pull-right" style="padding-right:35px">
           <asp:Button ID="ImgClose"
                                                runat="server" SkinID="btnCancel" style="float:right;"   />
	</div>
</div>
                                   
                                </div>
            
           
        </asp:Panel>  
       
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 

  
</asp:Content>
