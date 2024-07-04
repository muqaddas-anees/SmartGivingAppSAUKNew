<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HealthCheckForm"
    Title="Health Check Form" Codebehind="HealthCheckForm.aspx.cs" %>

<%@ Register src="controls/HealthcheckSubtabs.ascx" tagname="HealthcheckSubtabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <script  type="text/javascript">
       $(document).ready(function () {
           $('#navTab').hide();
       });
       </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Customer%>: <%=sessionKeys.PortfolioName %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="Server" Visible="false">
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
      <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <asp:Label SkinID="Loading" runat="server"></asp:Label>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script language="javascript" type="text/javascript">
    function CheckItemPattern(sender, args) {
        //var RdControlId = '<%=rdPattern.ClientID%> '
        var options = document.getElementById("<%=rdPattern.ClientID%>").getElementsByTagName('input');
        var ischecked = false;
        args.IsValid = false;
        for (i = 0; i < options.length; i++) {
            var opt = options[i];

            if (opt.checked) {
                ischecked = true;
                args.IsValid = true;
            }

        }
    }

    function CheckRange(sender, args) {
        var options = document.getElementById("<%=rdRangeOfRecurrence.ClientID%>").getElementsByTagName('input');
        var ischecked = false;
        args.IsValid = false;
        for (i = 0; i < options.length; i++) {
            var opt = options[i];
            if (opt.checked) {
                ischecked = true;
                args.IsValid = true;
            }
        }

    }

    function MutExRdList() {
        var RDList = document.getElementById("<%=rdPattern.ClientID%>");
        var chkDays = document.getElementById("<%=chkDays.ClientID%>");
        var txtRecur = document.getElementById("<%=txtRecur.ClientID%>");
        var chks = RDList.getElementsByTagName("input");
        chkDays.disabled = true;
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].checked && i == 0) {

                chkDays.disabled = false;

            }
            if (chks[i].checked && i == 1) {
                txtRecur.disabled = false;
                txtRecur.focus();
            }
            if (chks[i].checked && i != 0) {

                chkDays.disabled = true;

            }
            if (chks[i].checked && i != 1) {

                txtRecur.disabled = true;
                txtRecur.value = "";
            }
        }
    }
    function disableItem() {
        var RDList = document.getElementById("<%=rdPattern.ClientID%>");
        var chkDays = document.getElementById("<%=chkDays.ClientID%>");
        var chks = RDList.getElementsByTagName("input");
        alert("sani");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].checked && i == 1) {

                chkDays.disabled.parentElement.disabled = false;

            }
            if (chks[i].checked && i != 0) {

                chkDays.disabled = true;

            }
        }
    }
    function MutExRdListRange() {

        var RDList = document.getElementById("<%=rdRangeOfRecurrence.ClientID%>");
        var EndDate = document.getElementById("<%=txtEndDate.ClientID%>");
        var EndOfOcurrences = document.getElementById("<%=txtEndOfOcurrences.ClientID%>");
        var ImgEndate = document.getElementById("<%=ImgEndate.ClientID%>");
        var chks = RDList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].checked && i == 0) {
                ImgEndate.disabled = true;
                EndDate.disabled = true;
                EndOfOcurrences.disabled = true;
                EndDate.value = "";
                EndOfOcurrences.value = "";
            }
            if (chks[i].checked && i == 1) {

                EndOfOcurrences.disabled = false;
                EndOfOcurrences.focus();
                EndDate.value = "";
                EndDate.disabled = true;
                ImgEndate.disabled = true;

            }
            if (chks[i].checked && i == 2) {

                ImgEndate.disabled = false;
                EndOfOcurrences.disabled = true;
                EndOfOcurrences.value = "";
                EndDate.disabled = false;
                EndDate.focus();

            }
        }
    }    
</script>
   
   <ajaxToolkit:ModalPopupExtender TargetControlID="imgaddRecurr" CancelControlID="imgCancel" PopupControlID="pnlRecurrence"
    BackgroundCssClass="modalBackground" runat="server" ID="mpopRecurrence"></ajaxToolkit:ModalPopupExtender>
    
   <asp:Panel ID="pnlRecurrence" runat="server" ScrollBars="None" Width="670px" Height="470px"   Style="display: none" 
   BorderStyle="Double" BackColor="White" BorderColor="LightSteelBlue">
    <div style="width:250px;height:20px" >  <asp:CustomValidator ID="CustomValidator1" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckItemPattern" ErrorMessage="Select recurrence pattern">
                    </asp:CustomValidator><br/>
                      <asp:CustomValidator ID="CustomValidator2" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckRange" ErrorMessage="Select range of recurrence pattern">
                    </asp:CustomValidator>
                    
                    <asp:ValidationSummary ID="HealthCheckRecurr" runat="server"  DisplayMode="BulletList" ForeColor="Red"  /> </div>
   <div  class="tab_subheader"  style="border-bottom:solid 1px Silver; width:98%"  ><span>Recurrance</span></div>
   <div>
     <asp:Label ID="lblRecurrMsg" runat="server" Text=""></asp:Label></div>
 <div style="font-weight:bold;"><label>&nbsp;&nbsp;Recurrence&nbsp;Pattern:</label></div>
      <table  border="0" cellpadding="0" cellspacing="0" width="670px" >
            <tr>
                <td style="width:15%">
                    <asp:RadioButtonList ID="rdPattern" runat="server" onclick="MutExRdList()" >
                        <asp:ListItem Value="1" Selected="True">Daily</asp:ListItem>
                        <asp:ListItem Value="2">Weekly</asp:ListItem>
                        <asp:ListItem Value="3">Monthly</asp:ListItem>
                        <asp:ListItem Value="4">Yearly</asp:ListItem>
                    </asp:RadioButtonList>
         <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdPattern"
                    ErrorMessage="Select recurrence pattern" Display="None" ValidationGroup="HealthCheckRecurr"  ></asp:RequiredFieldValidator>--%>
                </td>
                <td>
                    <asp:CheckBoxList ID="chkDays" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" >
                        <asp:ListItem Selected="True">Monday</asp:ListItem>
                        <asp:ListItem Selected="True">Tuesday</asp:ListItem>
                        <asp:ListItem Selected="True">Wednesday</asp:ListItem>
                        <asp:ListItem Selected="True">Thursday</asp:ListItem>
                        <asp:ListItem Selected="True">Friday</asp:ListItem>
                        <asp:ListItem Selected="True">Saturday</asp:ListItem>
                        <asp:ListItem Selected="True">Sunday</asp:ListItem>
                    </asp:CheckBoxList>
                    <br />
                                        <br />
                   Recur&nbsp;every<asp:TextBox ID="txtRecur" runat="server" Width="40px"></asp:TextBox>Week(s)&nbsp;
                </td>
            </tr>
            </table>
            <div style="font-weight:bold;"><label>&nbsp;&nbsp;Range&nbsp;of&nbsp;Recurrence:</label></div>
            <table  border="0" cellpadding="0" cellspacing="0" width="670px" >
            <tr>
                <td>
                    Start&nbsp;Date</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="ImgStartDate" runat="server" SkinID="Calender"/>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtStartDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form" ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"  CssClass="MyCalendar"
                    TargetControlID="txtStartDate" PopupButtonID="ImgStartDate" />
                </td>
                <td>
                    </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <asp:RadioButtonList ID="rdRangeOfRecurrence" runat="server" onclick="MutExRdListRange()">
                        <asp:ListItem Value="1" Selected="True">No&nbsp;end&nbsp;date</asp:ListItem>
                        <asp:ListItem Value="2">End&nbsp;after</asp:ListItem>
                        <asp:ListItem Value="3">End&nbsp;by</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td valign="bottom">
                    <table border="0" cellpadding="0" cellspacing="0" >
                    <tr><td>&nbsp;</td></tr>
                        
                        
                        <tr>
                            <td valign="bottom">
                                <asp:TextBox ID="txtEndOfOcurrences" runat="server" Width="30px"></asp:TextBox>
&nbsp;Ocurrances</td>
                        </tr>
                        <tr>
                            <td valign="bottom">
                                <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="ImgEndate" runat="server" SkinID="Calender"/>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEndDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form"  ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  CssClass="MyCalendar"
                    TargetControlID="txtEndDate" PopupButtonID="ImgEndate" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
                <td>
                   </td>
            </tr>
            
        </table>
        <div style="padding-left:15px"><asp:Button runat="server" ID="imgaddRecurr" style="display:none"/>
                                <asp:Button ID="ImgApply" runat="server" ValidationGroup="HealthCheckRecurr"
                                SkinID="btnApply" OnClick="ImgApply_Click"/> <asp:Button ID="imgCancel" SkinID="btnCancel" runat="server" /></div>
   </asp:Panel>
  
       <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />
	</div>
</div>
   <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label></label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlLocation" runat="server" DataSourceID="objLocationDDLFiller"
                    DataTextField="Site" DataValueField="ID" AppendDataBoundItems="true">
                    <asp:ListItem Text="Please Select.." Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rqdLocation" runat="server" ControlToValidate="ddlLocation"
                    InitialValue="0" Text="*" Display="Dynamic" ErrorMessage="Please select a location"
                    ValidationGroup="Form" />
                <asp:ObjectDataSource ID="objLocationDDLFiller" runat="server" TypeName="DataHelperClass"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="LoadSiteByPortfolio" >
                    <SelectParameters>
                    <asp:Parameter Name="portfolioID" DefaultValue="0" />
                    </SelectParameters>
                    </asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label">  <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label></label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                <asp:Label ID="imgCalendar" runat="server" SkinID="Calender" />
                <asp:RequiredFieldValidator ID="rqdDate" runat="server" ControlToValidate="txtDate"
                    ErrorMessage="Date is required" Text="*" ValidationGroup="Form" Display="Dynamic" />
                <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="txtDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form" Text="*" ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="calDate" runat="server"  CssClass="MyCalendar"
                    TargetControlID="txtDate" PopupButtonID="imgCalendar" />
               <asp:Button ID="imgRecurrence" runat="server" 
                    onclick="imgRecurrence_Click" SkinID="btnDefault" Text="Recurrence"  />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><asp:Label ID="lblTime" runat="server" Text="Time" ></asp:Label> </label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtTime" runat="server" MaxLength="5" SkinID="Time" />
                <asp:RequiredFieldValidator ID="rqdTime" runat="server" Text="*" ErrorMessage="Time Required"
                    ControlToValidate="txtTime" ValidationGroup="Form" />
                <asp:RegularExpressionValidator ID="rgeTime" runat="server" ControlToValidate="txtTime"
                    Display="Dynamic" ErrorMessage="Invalid Time" Text="*" ValidationExpression="^((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ M))$|^([01]\d|2[0-3])(:[0-5]\d){1,2}$"
                    ValidationGroup="Form" />
                <span style="color: Gray">&nbsp;(HH:MM)</span>
            </div>
	</div>
</div>
     
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblAssignedTeam" runat="server" Text="Assigned Team"></asp:Label></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlAssignedTeam" runat="server" DataSourceID="objTeams" DataTextField="TeamName"
                    DataValueField="ID" AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Text="Please Select.." Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rqdAssignedTeam" runat="server" ControlToValidate="ddlAssignedTeam"
                    Text="*" ErrorMessage="Please select the team" InitialValue="0" ValidationGroup="Form" />
                <asp:ObjectDataSource ID="objTeams" runat="server" TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="LoadTeamsByPortfolio" >
                    <SelectParameters>
                    <asp:Parameter Name="portfolioID" DefaultValue="0" />
                    </SelectParameters>
                    </asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><asp:Label ID="lblIssueStatus" runat="server" Text="Issue Status"> </asp:Label> </label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtIssueStatus" runat="server" Enabled="false" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
       
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblAssignmembers" runat="server" Text="Assigned Resource"></asp:Label></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlTeammember" runat="server" 
                    DataSourceID="objTeammember" DataTextField="ContractorName"
                    DataValueField="ID" AppendDataBoundItems="true" Width="175px">
                    <asp:ListItem Text="All" Value="0" />
                </asp:DropDownList>
                 <asp:ObjectDataSource ID="objTeammember" runat="server" TypeName="Health.DAL.HealthCheckListItemsHelper" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetMembers" >
                    <SelectParameters>
                        <asp:ControlParameter Name="teamid" ControlID="ddlAssignedTeam" DefaultValue="0" PropertyName="SelectedValue" />
                    </SelectParameters>
                    </asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          
	</div>
</div>
   <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-2 control-label"><asp:Label ID="lblNotes" runat="server" Text="General Notes" /></label>
           <div class="col-sm-10">
                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="84px" Width="611px" />
            </div>
          </div>
       </div>
     
    
     <asp:Panel ID="pnlEmailGrid" runat="server" Style="display: none" BackColor="White"
        BorderStyle="Double" BorderColor="LightSteelBlue">
        <table>
            <tr>
                <td colspan="2">
                 <asp:Panel ID="pnlInnerEmail" runat="server" Height="400px" ScrollBars="Both">
                    <asp:GridView ID="gridInner" runat="server" AutoGenerateColumns="False" DataSourceID="objMails"
                        BackColor="White" HorizontalAlign="Justify" DataKeyNames="ID" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="litMailID" runat="server" Text='<%#Eval("ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMailable" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailID" runat="server" Text='<%#Eval("EmailID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="sec_header" style="width: 100%">
                                No Email Addresses
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="btnPopupClose" runat="server" SkinID="btnSave" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource TypeName="DataHelperClass" SelectMethod="LoadDistributionMailIDs"
            ID="objMails" runat="server" />
    </asp:Panel>
   
    <div style="direction:rtl">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="lnkCompleteItems" SkinID="btnDefault" Text="Mark All Items As Complete" runat="server"
            EnableViewState="false" ToolTip="Marks all the items as complete and returns back"
            OnClick="lnkCompleteItems_Click">
        </asp:Button>
    </div>

    
<div class="form-group">
        <div class="col-md-12">
           <strong>Health Check - Items </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
  
   
    <div class="row">
         <div class="col-md-12"> 
             
         <uc1:HealthcheckSubtabs ID="HealthcheckSubtabs1" runat="server" />
     </div>
    </div>
    <br />
         <div class="form-group">
              <div class="col-md-12"> 
    <asp:Panel ID="pnlHealthChecks" runat="server" ScrollBars="None" Width="100%" Height="100%">
        <asp:GridView ID="gridHealthChecks" runat="server" Width="100%" AutoGenerateColumns="False"
            DataSourceID="objGridFiller" OnRowDataBound="gridHealthChecks_RowDataBound" OnRowCommand="gridHealthChecks_RowCommand"
            OnRowUpdated="gridHealthChecks_RowUpdated" >
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Health Check" ItemStyle-Wrap="true" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblHealthCheck" runat="server" Text='<%#Eval("HealthCheck")%>' />
                    </ItemTemplate>
                    <ItemStyle Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkYes" runat="server" Text="Yes" Checked='<%#getYesOrNo((System.Data.SqlTypes.SqlBoolean)Eval("IsChecked"),true)%>' />
                        <asp:CheckBox ID="chkNo" runat="server" Text="&nbsp;No" Checked='<%#getYesOrNo((System.Data.SqlTypes.SqlBoolean)Eval("IsChecked"),false)%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issues" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtIssues" runat="server" Text='<%#Eval("Issues")%>' Width="150px"
                            TextMode="MultiLine" />
                        <asp:Literal ID="litPreviousIssues" runat="server" Text='<%#Eval("Issues")%>' Visible="false" />
                    </ItemTemplate>
                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RAG" ControlStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlRAG" runat="server">
                        <asp:ListItem Value="0" Enabled ="true" Text="Select..."></asp:ListItem>
                        <asp:ListItem Value="1" Enabled ="true" Text ="Red"></asp:ListItem>
                        <asp:ListItem Value="2" Enabled ="true" Text ="Amber"></asp:ListItem>
                        <asp:ListItem Value="3" Enabled ="true" Text ="Green"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblRAG" runat="server" Text='<%# Eval("RAG")%>' Visible="false" />
                    </ItemTemplate>
                    
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtNotes" runat="server" Text='<%#Eval("Notes")%>' Width="150px"
                            TextMode="MultiLine" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="form-inline">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDuedate" runat="server" Text='<%#FormatDefaultDate(Convert.ToDateTime(Eval("Duedate")))%>'
                            SkinID="Date" />
                        <asp:Label ID="imgCalendar1" runat="server" SkinID="Calender" />
                        <ajaxToolkit:CalendarExtender ID="calDueDate" runat="server" 
                            CssClass="MyCalendar" TargetControlID="txtDuedate" PopupButtonID="imgCalendar1" />
                    </ItemTemplate>
                    <ItemStyle Width="110px" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ControlStyle-Width="90px">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="Please Select.." Value="0" />
                            <asp:ListItem Text="Pending" Value="Pending" />
                            <asp:ListItem Text="In Progress" Value="In Progress" />
                            <asp:ListItem Text="Complete" Value="Complete" />
                            <asp:ListItem Text="Critical" Value="Critical" />
                            <asp:ListItem Text="Not Applicable" Value="Not Applicable" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Literal ID="litHealthCheckListID" runat="server" Text='<%#Eval("HealthCheckListID")%>'
                            Visible="false" />
                        <asp:Literal ID="litMainID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                        <ajaxToolkit:ModalPopupExtender ID="popUp" TargetControlID="imgPopUp" PopupControlID="pnlEmailGrid"
                            BackgroundCssClass="modalBackground" runat="server" CancelControlID="btnPopupClose" />
                        <asp:Label ID="imgPopUp" SkinID="Mail" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Link" CommandName="Mail" Text="Save & Mail" />
            </Columns>
        </asp:GridView>

        <script type="text/javascript">
            function CheckYes(chkYes,chkNo)
            {
                chkNo.checked=false;    
            }
            
            function CheckNo(chkYes,chkNo)
            {
                chkYes.checked=false;
            }
        </script>

       
        <asp:ObjectDataSource ID="objGridFiller" runat="server" TypeName="Health.DAL.HealthCheckListItemsHelper"
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetHealthCheckListItemsById"
            DataObjectTypeName="Health.Entity.HealthCheckListItems" DeleteMethod="Delete">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="healthCheckId" QueryStringField="HealthCheckId"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
                  </div>
             </div>

         <div class="form-group">
              <div class="col-md-12">
              <div class="pull-right">
                   <asp:Button ID="btnSubmitChanges" runat="server" Text="Save and Return to Health Check Schedule" 
                    OnClick="btnSubmitChanges_Click" ValidationGroup="Form" />
                  </div>
                  </div>
         </div>
    
   
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
    <script  type="text/javascript">
        $(document).ready(function () {
           
            sideMenuActive('<%= Resources.DeffinityRes.HealthChecks%>');
       });
       </script>
    
</asp:Content>
