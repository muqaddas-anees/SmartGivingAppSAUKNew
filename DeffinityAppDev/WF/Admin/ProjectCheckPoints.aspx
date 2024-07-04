<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="ProjectCheckPoints" Codebehind="ProjectCheckPoints.aspx.cs" %>
<%@ Register Src="controls/ProgrammeManagement.ascx" tagname="ProgrammeManagement" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProgrammeManagement ID="tab" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.CheckPoints%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <script type="text/javascript">
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

    <div class="form-group">
                                  <div class="col-md-5">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Programme%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlProgramme" runat="server" SkinID="ddl_90">
    </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-5">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.SubProgramme%></label>
                                      <div class="col-sm-7">
                                           <asp:DropDownList ID="ddlSubprogramme" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown21" runat="server" TargetControlID="ddlSubprogramme"
                                    Category="Title" PromptText="Please select..." ServicePath="~/WF/DC/Webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetSubProgramme" ParentControlID="ddlProgramme" />
					</div>
				</div>
<div class="col-md-2">
                                      <div class="col-sm-12">
                                           <asp:Button ID="ImageButton1" runat="server" onclick="ImageButton1_Click" 
            SkinID="btnDefault" Text="View" />

					</div>
				</div>
</div>


<asp:Panel ID="pnlRecurrence" runat="server" ScrollBars="None">
    <div class="form-group">
             <div class="col-md-12">
                 <asp:CustomValidator ID="CustomValidator1" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckItemPattern" ErrorMessage="Select recurrence pattern">
                    </asp:CustomValidator>
                 <asp:CustomValidator ID="CustomValidator2" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckRange" ErrorMessage="Select range of recurrence pattern">
                    </asp:CustomValidator>
                 <asp:ValidationSummary ID="HealthCheckRecurr" runat="server"  DisplayMode="BulletList" ForeColor="Red"  />
</div>
</div>
  <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.Recurrance %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
   <div class="row">
<div class="col-md-12">
    <asp:Label ID="lblRecurrMsg" runat="server" Text=""></asp:Label>
</div>
</div>
   <div class="form-group">
             <div class="col-md-12 text-bold">
                 <p><%=Resources.DeffinityRes.RecurrencePattern %></p>
</div>
       </div>
       <div class="form-group">
             <div class="col-md-12">
                 <div class="col-sm-2">
                      <asp:RadioButtonList ID="rdPattern" runat="server" onclick="MutExRdList()" >
                        <asp:ListItem Value="1" Selected="True">Daily</asp:ListItem>
                        <asp:ListItem Value="2">Weekly</asp:ListItem>
                        <asp:ListItem Value="3">Monthly</asp:ListItem>
                        <asp:ListItem Value="4">Yearly</asp:ListItem>
                    </asp:RadioButtonList>
                     </div>
                 <div class="col-sm-10 form-inline">
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
                   Recur&nbsp;every<asp:TextBox ID="txtRecur" runat="server" SkinID="txt_50px"></asp:TextBox>Week(s)&nbsp;
                     </div>
                 </div>
           </div>

     <div class="form-group">
        <div class="col-md-12 text-bold">
             <P>   <%=Resources.DeffinityRes.RangeofReoccurrence %> </P>
            </div>
</div>
         <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Start&nbsp;Date</label>
                                      <div class="col-sm-10 form-inline"><asp:TextBox ID="txtStartDate" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="ImgStartDate" runat="server" SkinID="Calender" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtStartDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form"  ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"  CssClass="MyCalendar"
                    TargetControlID="txtStartDate" PopupButtonID="ImgStartDate" />
					</div>
				</div>
                </div>  
     <div class="row">
                                <div class="col-md-2">
                                    <asp:RadioButtonList ID="rdRangeOfRecurrence" runat="server" onclick="MutExRdListRange()">
                        <asp:ListItem Value="1" Selected="True">No&nbsp;end&nbsp;date</asp:ListItem>
                        <asp:ListItem Value="2">End&nbsp;after</asp:ListItem>
                        <asp:ListItem Value="3">End&nbsp;by</asp:ListItem>
                    </asp:RadioButtonList>
                                    </div>
                                  <div class="col-md-10">
                                      <div class="form-group">
                                            <div class="col-md-12 form-inline">
                                                <asp:TextBox ID="txtEndOfOcurrences" runat="server" SkinID="txt_50px"></asp:TextBox>&nbsp;Ocurrances
                                            </div>
                                            </div>
                                       <div class="form-group">
                                            <div class="col-md-12 form-inline">
                                                <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="ImgEndate" runat="server" SkinID="Calender" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEndDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form"  ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  CssClass="MyCalendar"
                    TargetControlID="txtEndDate" PopupButtonID="ImgEndate" />
                                            </div>
                                            </div>
                                    </div>
                                 </div> 
    <div class="form-group">
             <div class="col-md-12">
                 <asp:Button runat="server" ID="imgaddRecurr" style="display:none"/>
                                <asp:Button ID="ImgApply" runat="server" ValidationGroup="HealthCheckRecurr"
                                SkinID="btnApply" OnClick="ImgApply_Click"/> <asp:Button ID="imgCancel" Visible="false" SkinID="btnCancel" runat="server" />
</div>
</div>

   </asp:Panel>
</asp:Content>


