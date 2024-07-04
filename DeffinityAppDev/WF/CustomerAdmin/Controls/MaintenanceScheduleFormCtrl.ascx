<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceScheduleFormCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Controls.MaintenanceScheduleFormCtrl" %>
<div class="form-group row" id="pnlSearch" runat="server">
      <div class="col-md-3">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtFromdate" runat="server" SkinID="Date"></asp:TextBox>
                <asp:Label ID="lblfromImg" runat="server" SkinID="Calender"  />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                        PopupButtonID="lblfromImg" TargetControlID="txtFromdate" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtTodate" runat="server" SkinID="Date"></asp:TextBox>
                <asp:Label ID="lblToImg" runat="server" SkinID="Calender"  />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  runat="server"
                        PopupButtonID="lblToImg" TargetControlID="txtTodate" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Search%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </div>
	</div>
        <div class="col-md-2">
            <asp:Button ID="btnSearch" runat="server" SkinID="btnDefault" Text="Search" OnClick="btnSearch_Click" />
            </div>
</div>
     <div class="form-group row">
        
                <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
               
        </div>
 <div class="form-group row">
      <div class="col-md-12">
     <asp:HiddenField ID="hcontactid" runat="server" Value ="0" />
     <asp:Button ID="btnRemainder" runat="server" style="float:right;" Text="Set Reminder" OnClick="btnRemainder_Click"></asp:Button>
          </div>
     </div>
    <asp:GridView ID="GridList" runat="server" OnPageIndexChanging="GridList_PageIndexChanging" OnRowCommand="GridList_RowCommand" PageSize="20" AllowPaging="true" AllowSorting="true" OnSorting="GridList_Sorting" >
        <Columns>
            <asp:TemplateField >
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit1" SkinID="BtnLinkEdit" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Customer" SortExpression="RequesterName">
                <ItemTemplate>
                    <asp:Label ID="lblRequesterName" runat="server" Text='<%# Bind("RequesterName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Equipment" SortExpression="EquipmentName">
                <ItemTemplate>
                    <asp:Label ID="lblEquipmentName" runat="server" Text='<%# Bind("EquipmentName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Maintenance Type" SortExpression="MaintenanceTypeName">
                <ItemTemplate>
                    <asp:Label ID="lblMaintenanceTypeName" runat="server" Text='<%# Bind("MaintenanceTypeName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Description" SortExpression="ReminderDescription">
                <ItemTemplate>
                    <asp:Label ID="lblReminderDescription" runat="server" Text='<%# Bind("ReminderDescription") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Date of Reminder" ItemStyle-HorizontalAlign="Right" SortExpression="DateOfReminder">
                <ItemTemplate>
                    <asp:Label ID="lblReorderLevelColor" runat="server" Text='<%# GetDateRemainderCheck(Eval("DateOfReminder").ToString())%>' CssClass="statuscls" style="visibility:hidden;"></asp:Label>
                    <asp:Label ID="lblDateOfReminder" runat="server" Text='<%# Bind("DateOfReminder","{0:d}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Renewal Amount" ItemStyle-HorizontalAlign="Right" SortExpression="RenewalAmount">
                <ItemTemplate>
                    <asp:Label ID="lblRenewalAmount" runat="server" Text='<%# Bind("RenewalAmount","{0:F2}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Assigned To" ItemStyle-HorizontalAlign="Right" SortExpression="AssignToName">
                <ItemTemplate>
                    <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Bind("AssignToName") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnSendMail" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="SendMail" SkinID="btnDefault" Text="Send Mail"  ></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Button ID="btnrecurr" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="recurr" SkinID="btnDefault" Text="Recurrence"></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete1" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>
      <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblStorageNew" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="460px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body"> Maintenance Reminder</h3>
							
							<div class="card-toolbar">
								<%--<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">–</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>--%>
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								<%--<a href="#" data-toggle="remove">
									×
								</a>--%>
							</div>
						</div>
    <div class="panel-body">
     <div class="form-group row">
          <div class="col-md-12" >
     
               <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vd" />
              </div>
                   </div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Equipment</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtEquipment" runat="server" SkinID="txt_90" Width="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEquipment"
                                        Display="None" ErrorMessage="Please enter equipment" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    
    
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Reminder Description</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtReminderDescription" runat="server" SkinID="txt_90"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReminderDescription"
                                        Display="None" ErrorMessage="Please enter reminder description" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
   
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Maintenance Type</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlMaintenanceType" runat="server" SkinID="ddl_90"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMaintenanceType" InitialValue="0"
                                        Display="None" ErrorMessage="Please enter maintenance type" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
               <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Date Of Reminder</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtDateOfReminder" runat="server" SkinID="Date"></asp:TextBox>
                <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"  /></div>
              <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtDateOfReminder"
                                        Display="None" ErrorMessage="Please enter date of reminder" ValidationGroup="vd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorDateReceived" runat="server" ControlToValidate="txtDateOfReminder"
                        ErrorMessage="Please enter valid date" Operator="DataTypeCheck" Type="Date" ValidationGroup="vd" Display="None"></asp:CompareValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                        PopupButtonID="imgbtnenddate6" TargetControlID="txtDateOfReminder" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
                <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Renewal Amount</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtRenewalAmount" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
                
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRenewalAmount"
                                        Display="None" ErrorMessage="Please enter date of reminder" ValidationGroup="vd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtRenewalAmount"
                        ErrorMessage="Please enter valid renewal amount" Operator="DataTypeCheck" Type="Double" ValidationGroup="vd" Display="None"></asp:CompareValidator>
                   
            </div>
              </div>
	</div>
               <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Assigned to </label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList ID="ddlAssignedTo" runat="server" SkinID="ddl_80">
                   <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                   <asp:ListItem Text="Internal" Value="1"></asp:ListItem>
               </asp:DropDownList>
                
             
                   
            </div>
              </div>
	</div>
     <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:HiddenField ID="hbomid" runat="server" Value="0" />
               <asp:Button ID="btnSelect" runat="server" SkinID="btnSubmit" OnClick="btnSelect_OnClick" ValidationGroup="vd" />
              
               </div>
              </div>
         </div>

              </div>
         </div>
        </div>
</asp:Panel>
    <asp:HiddenField ID="hid" runat="server" />


    <script type="text/javascript">
                                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
                                 setStatusBackColor();
                                 function setStatusBackColor() {
                                    
                                         $('.statuscls').each(function () {
                                             
                                             var s = $(this).html();
                                             if (s == 'Red')
                                                 $(this).closest("td").css({ "background-color": "#FF3333", "text-align": "right", "color": "white" });
                                         });
                                    
                                 }
</script>


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
   
   <ajaxToolkit:ModalPopupExtender TargetControlID="lblPnlRecurrence" CancelControlID="btnRecurrenceClose" PopupControlID="pnlRecurrence"
    BackgroundCssClass="modalBackground" runat="server" ID="mpopRecurrence"></ajaxToolkit:ModalPopupExtender>
    <asp:Label ID="lblPnlRecurrence" runat="server"></asp:Label>
   <asp:Panel ID="pnlRecurrence" runat="server" ScrollBars="None" Width="670px" Height="610px"   Style="display: none" 
   BorderStyle="Double" BackColor="White" CssClass="panel panel-color panel-info">
   <div class="card-header">
							<h3 class="card-body">Recurrance</h3>
							
							<div class="card-toolbar">
								<%--<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">–</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>--%>
								 <asp:LinkButton ID="btnRecurrenceClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								<%--<a href="#" data-toggle="remove">
									×
								</a>--%>
							</div>
						</div>
        <div class="panel-body">
       <div style="height:20px" > 
        
        <asp:CustomValidator ID="CustomValidator1" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckItemPattern" ErrorMessage="Select recurrence pattern">
                    </asp:CustomValidator><br/>
                      <asp:CustomValidator ID="CustomValidator2" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckRange" ErrorMessage="Select range of recurrence pattern">
                    </asp:CustomValidator>
                    
                    <asp:ValidationSummary ID="HealthCheckRecurr" runat="server"  DisplayMode="BulletList" ForeColor="Red"  /> </div>
   
   <div>
     <asp:Label ID="lblRecurrMsg" runat="server" Text=""></asp:Label></div>
             <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Appointment time:</strong> 
<hr class="no-top-margin" />
	</div>
</div>
             <div class="form-group row">
      <div class="col-md-4">
          
           <label class="col-sm-4 control-label">Start</label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtStart" runat="server" Text="00:00"></asp:TextBox>
               </div>
          </div>
                  <div class="col-md-4">
          
           <label class="col-sm-4 control-label">End</label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtEnd" runat="server" Text="00:00"></asp:TextBox>
               </div>
          </div>
                  <div class="col-md-4">
          
           <label class="col-sm-4 control-label">Duration</label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlDuration" runat="server">
                 <asp:ListItem Value="0 minutes">  0 minutes</asp:ListItem>
<asp:ListItem Value="5 minutes"> 5 minutes</asp:ListItem>
<asp:ListItem Value="10 minutes"> 10 minutes</asp:ListItem>
<asp:ListItem Value="15 minutes"> 15 minutes</asp:ListItem>
<asp:ListItem Value="30 minutes"> 30 minutes</asp:ListItem>
<asp:ListItem Value="1 hour"> 1 hour</asp:ListItem>
<asp:ListItem Value="2 hours"> 2 hours</asp:ListItem>
<asp:ListItem Value="3 hours"> 3 hours</asp:ListItem>
<asp:ListItem Value="4 hours"> 4 hours</asp:ListItem>
<asp:ListItem Value="5 hours"> 5 hours</asp:ListItem>
<asp:ListItem Value="6 hours"> 6 hours</asp:ListItem>
<asp:ListItem Value="7 hours"> 7 hours</asp:ListItem>
<asp:ListItem Value="8 hours"> 8 hours</asp:ListItem>
<asp:ListItem Value="9 hours"> 9 hours</asp:ListItem>
<asp:ListItem Value="10 hours"> 10 hours</asp:ListItem>
<asp:ListItem Value="11 hours"> 11 hours</asp:ListItem>
<asp:ListItem Value="12 hours"> 12 hours</asp:ListItem>
<asp:ListItem Value="18 hours"> 18 hours</asp:ListItem>
<asp:ListItem Value="1 day"> 1 day</asp:ListItem>
<asp:ListItem Value="2 days"> 2 days</asp:ListItem>
<asp:ListItem Value="3 days"> 3 days</asp:ListItem>
<asp:ListItem Value="4 days"> 4 days</asp:ListItem>
<asp:ListItem Value="1 week"> 1 week</asp:ListItem>
<asp:ListItem Value="2 weeks"> 2 weeks</asp:ListItem>
               </asp:DropDownList>
               </div>
          </div>
                 </div>

             <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Recurrence&nbsp;Pattern:</strong> 
<hr class="no-top-margin" />
	</div>
</div>
 
            <div class="form-group row">
      <div class="col-md-2">
           <asp:RadioButtonList ID="rdPattern" runat="server" onclick="MutExRdList()" >
                        <asp:ListItem Value="1" Selected="True">Daily</asp:ListItem>
                        <asp:ListItem Value="2">Weekly</asp:ListItem>
                        <asp:ListItem Value="3">Monthly</asp:ListItem>
                        <asp:ListItem Value="4">Yearly</asp:ListItem>
                    </asp:RadioButtonList>
          </div>

                 <div class="col-md-10">
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
                                       <div class="form-group row">
      <div class="col-md-12 form-inline">
                   Recur&nbsp;every<asp:TextBox ID="txtRecur" runat="server" SkinID="txt_75px" Text="0"></asp:TextBox>Week(s)&nbsp;
          </div>
                                           </div>

          </div>
                </div>
     
             <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Range&nbsp;of&nbsp;Recurrence:</strong> 
<hr class="no-top-margin" />
	</div>
</div>
             <div class="form-group row">
      <div class="col-md-5">
          
           <label class="col-sm-4 control-label">Start&nbsp;Date</label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtStartDate" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="ImgStartDate" runat="server" SkinID="Calender"/>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtStartDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form" ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" CssClass="MyCalendar"
                    TargetControlID="txtStartDate" PopupButtonID="ImgStartDate" />
               </div>
              
          </div>
                 <div class="col-md-3">
                     <div class="col-md-12">
           <asp:RadioButtonList ID="rdRangeOfRecurrence" runat="server" onclick="MutExRdListRange()">
                        <asp:ListItem Value="1" Selected="True">No&nbsp;end&nbsp;date</asp:ListItem>
                        <asp:ListItem Value="2">End&nbsp;after</asp:ListItem>
                        <asp:ListItem Value="3">End&nbsp;by</asp:ListItem>
                    </asp:RadioButtonList>
               </div>
          </div>
                  <div class="col-md-4">

                      <div class="col-md-12 form-inline">
                          &nbsp;
                          </div>
                       <div class="col-md-12 form-inline">
                           <asp:TextBox ID="txtEndOfOcurrences" runat="server" SkinID="txt_50px"></asp:TextBox>
&nbsp;Occurances
                           </div>
                      <br />
                      <div class="col-md-12 form-inline">
                           <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="ImgEndate" runat="server" SkinID="Calender"/>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtEndDate" Operator="DataTypeCheck"
                    Type="Date" ValidationGroup="Form"  ErrorMessage="Invalid Date" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" CssClass="MyCalendar"
                    TargetControlID="txtEndDate" PopupButtonID="ImgEndate" />
                          </div>
          </div>
                 </div>
            
        <div></div>

            </div>

        <div class="form-group row">
      <div class="col-md-12 form-inline">

          <asp:Button runat="server" ID="imgaddRecurr" style="display:none"/>
                                <asp:Button ID="ImgApply" runat="server" ValidationGroup="HealthCheckRecurr"
                                SkinID="btnApply" OnClick="ImgApply_Click"/> <asp:Button ID="imgCancel" SkinID="btnCancel" runat="server" />
          </div>
            </div>


   </asp:Panel>

<%-- <style>
     .navbar.horizontal-menu {
  position: relative;
  height: 85px;
  background: #ffffff;
  margin: 0;
  /*/* padding: 0;*/ 
  z-index: 1;
  min-height: 0px;
  -webkit-box-shadow: 0 0px 1px rgba(0, 0, 0, 0.15);
  -moz-box-shadow: 0 0px 1px rgba(0, 0, 0, 0.15);
  box-shadow: 0 0px 1px rgba(0, 0, 0, 0.15);
}
	</style>--%>