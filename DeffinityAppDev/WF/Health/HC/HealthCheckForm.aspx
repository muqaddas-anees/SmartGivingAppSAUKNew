<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HC_HealthCheckForm"
    Title="Health Check Form" Codebehind="HealthCheckForm.aspx.cs" %>

<%@ MasterType VirtualPath="~/WF/MainTab.master" %>
<%@ Register src="controls/HealthcheckSubtabs.ascx" tagname="HealthcheckSubtabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Customer:
                <%=sessionKeys.PortfolioName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <link rel="stylesheet" href="../stylcss/HCstyle.css"/>--%>
      <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
   <%--  <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="jQuery.print.js"></script>
    <link rel="stylesheet" href="../stylcss/ButtonStyle.css"/>
    <script type="text/javascript" src="../Scripts/HCform.js"></script>--%>

   <%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>

 <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>

<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var PanelId = GetParameterValues('PID');
            GetTextboxId(PanelId);
        });
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
        function GetTextboxId(panelid) {
            var el = $()
            $.ajax({
                url: "../HC/HCWebService.asmx/GetTextBoxId?pid=" + panelid,
                type: "POST",
                data: "{'panelid': '" + panelid + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        debugger;
                        var obj = jQuery.parseJSON(data.d);
                        //debugger;
                        //alert(data.CntlID);
                        //debugger;
                        if (obj != '')
                            debugger;
                        $.each(obj, function () {
                            if ($("#MainContent_MainContent_" + this.CntlID + "").val() == '') {
                                $("#MainContent_MainContent_" + this.CntlID + "").datepicker({ dateFormat: 'dd/mm/yy' });
                            }
                        });
                    }
                },
                error: function (msg) { setMsg(Error); }
            })
        }
        function uploadOurlogo(id) {

            var el = $(e).closest('.td_cls');
            var pid = $(el).attr('id');

            var fileUpload = $('#"+id+"').get(0);
            var files = fileUpload.files;

            var fdata = new FormData();
            for (var i = 0; i < files.length; i++) {
                fdata.append(files[i].name, files[i]);
            }

            var options = {};

            options.url = "WF/Health/HC/HCWebService.asmx/UploadLogo?pid=" + pid;
            options.type = "POST";
            options.data = fdata;
            options.contentType = false;
            options.processData = false;
            options.async = true,
            options.success = function (result) {
                var obj = result;

                if (result != '') {
                    setSuccessMsg("Uploaded successfully.");
                    GetControlImageData_our(pid);
                }
            };
            options.error = function (err) { setErrorMsg('Fail to upload.Please try again.'); };

            $.ajax(options);
            return false;
        }



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
    <div class="data_carrier">
       
   <div class="data_carrier_block p_section4">
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
                    <asp:TextBox ID="txtStartDate" runat="server" Width="100px"></asp:TextBox>
                    <asp:Image ID="ImgStartDate" runat="server" SkinID="Calender"
                    ImageAlign="AbsMiddle" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtStartDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form"  ErrorMessage="Invalid Date" />
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
                                <asp:TextBox ID="txtEndDate" runat="server" Width="100px"></asp:TextBox>
                                <asp:Image ID="ImgEndate" runat="server" SkinID="Calender"
                    ImageAlign="AbsMiddle" />
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
                                <asp:ImageButton ID="ImgApply" runat="server" ValidationGroup="HealthCheckRecurr"
                                SkinID="ImgApply" OnClick="ImgApply_Click"/> <asp:ImageButton ID="imgCancel" SkinID="ImgCancel" runat="server" /></div>
   </asp:Panel>
  
     <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="valForm" runat="server" ValidationGroup="Form" />
	</div>
</div>
       <div class="form-group row">
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
     
<div class="form-group row">
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
          <label class="col-sm-3 control-label">
               <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            </label>
            <div class="col-sm-9">
                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80">
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Critical" Value="Critical"></asp:ListItem>
                    <asp:ListItem Text="In Progress" Value="In Progress"></asp:ListItem>
                    <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
	
</div>
       
<div class="form-group row">
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
           <label class="col-sm-3 control-label"><asp:Label ID="lblIssueStatus" runat="server" Text="Issue Status" Visible="false"> </asp:Label> </label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtIssueStatus" runat="server" Enabled="false" Visible="false" />
            </div>
	</div>
	
	<div class="col-md-4">
          
	</div>
</div>
   <div class="form-group row">
      <div class="col-md-8">
           <label class="col-sm-2 control-label"><asp:Label ID="lblNotes" runat="server" Text="General Notes" /></label>
           <div class="col-sm-10">
                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Height="84px" Width="611px" />
            </div>
          </div>
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
                    <asp:ImageButton ID="btnPopupClose" runat="server" ImageUrl="~/images/btn_save.gif" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource TypeName="DataHelperClass" SelectMethod="LoadDistributionMailIDs"
            ID="objMails" runat="server" />
    </asp:Panel>
    <div class="clr"></div>
    <div class="clr"></div>
    <div style="direction:rtl">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkCompleteItems" Text="Mark All Items As Complete" runat="server"
            EnableViewState="false" ToolTip="Marks all the items as complete and returns back"
            OnClick="lnkCompleteItems_Click" Visible="false">
        </asp:LinkButton>
    </div>
    
    
<div class="form-group row">
        <div class="col-md-12">
           <strong>Form </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="row">
         <div class="col-md-12"> 
             
       <uc1:HealthcheckSubtabs ID="HealthcheckSubtabs1" runat="server" />
     </div>
    </div>
   

     <div class="form-group row">
              <div class="col-md-12"> 

     <div id="countrydivcontainer" style="border: 1px solid #d8dee5; margin-bottom: 1em;
                    padding: 10px"> 
    <asp:Panel ID="pnlHealthChecks" runat="server" ScrollBars="None" Width="100%" Height="100%" ClientIDMode="Static">
         <div class="form-group row">
              <div class="col-md-12">
              <div class="pull-right">
         <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" />
                  </div>
                  </div>
             </div>
          
          <asp:UpdatePanel ID="updatepanel_additional" runat="server" ClientIDMode="Static">
                    <ContentTemplate>
                     <asp:PlaceHolder ID="ph" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
                        </ContentTemplate>
              </asp:UpdatePanel>
        <script language="javascript" type="text/javascript">
            function CheckYes(chkYes,chkNo)
            {
                chkNo.checked=false;    
            }
            
            function CheckNo(chkYes,chkNo)
            {
                chkYes.checked=false;
            }
        </script>

      
      
    </asp:Panel>
   
    </div>
   </div>
         </div>
    
     <div class="form-group row">
              <div class="col-md-12">
              <div class="pull-right">
     <asp:Button ID="btnSubmitChanges" runat="server" Text="Save and Return to Forms"
                    OnClick="btnSubmitChanges_Click" ValidationGroup="Form" />
                  </div>
                  </div>
         </div>
      <script type="text/javascript">
          //apply date 
          applyDatePicker();
      </script>
    <script  type="text/javascript">
        $(document).ready(function () {

            sideMenuActive('<%= Resources.DeffinityRes.HealthChecks%>');
        });
       </script>
</asp:Content>
