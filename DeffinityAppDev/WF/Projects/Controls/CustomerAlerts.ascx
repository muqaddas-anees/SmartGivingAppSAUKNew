<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CustomerAlerts" Codebehind="CustomerAlerts.ascx.cs" %>
<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>
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
<div>
    </div>
     <div class="form-group">
                                  <div class="col-md-12">
                                      <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                                      
                                      </div>
         </div>
  <div class="form-group">
                                  <div class="col-md-12">
                                      </div>
         </div>
    <div>
        <asp:Button ID="imgAddAlert" runat="server"  SkinID="btnAddNew" 
            ToolTip="Add new alert" onclick="imgAddAlert_Click" /></div>
     <ajaxToolkit:ModalPopupExtender CancelControlID="ImgCancel" ID="mpopAlert" runat="server"
                 PopupControlID="pnlAlert" TargetControlID="imgItemEdit"  
                 BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
   <asp:Panel ID="pnlAlert" runat="server" BackColor="White" Style="display: none" Width="70%"
                        Height="475px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">   
       <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong> <%= Resources.DeffinityRes.CustomAlert%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
        <div class="form-group">
                                  <div class="col-md-12">
                     <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="save" />
                                      </div>
            </div>
       <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.AlertDescription%></label>
                                      <div class="col-sm-7">
                                          <asp:TextBox ID="txtAlertDescription" runat="server" TextMode="MultiLine" 
                Width="496px" Height="50px"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rv_val" runat="server" ControlToValidate="txtAlertDescription"
                                        Display="None" ErrorMessage="Please enter Name" Text="Please enter alert description" ValidationGroup="save">
                                    </asp:RequiredFieldValidator>
                                          
					</div>
				</div>
           </div>    
       <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.DueDate%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:TextBox ID="txtDueDate" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label ID="Image3_F" runat="server" SkinID="Calender"   />
            <ajaxToolkit:CalendarExtender ID="Due" runat="server" CssClass="MyCalendar"  TargetControlID="txtDueDate" PopupButtonID="Image3_F">
            </ajaxToolkit:CalendarExtender>
					</div>
				</div>
           </div>    
       <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.AlertDate%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:TextBox ID="txtAlertDate" runat="server" SkinID="Date"></asp:TextBox>
             <asp:Label ID="imgAlert" runat="server" SkinID="Calender" style="padding-left:0px;padding-right:0px"   />
             
            <ajaxToolkit:CalendarExtender ID="Alert" runat="server" CssClass="MyCalendar"  TargetControlID="txtAlertDate" PopupButtonID="imgAlert">
            </ajaxToolkit:CalendarExtender>
					</div>
				</div>
           </div>    
       <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.DistributionList%></label>
                                      <div class="col-sm-9">
                                           <asp:Panel Width="270px" runat="server" ID="Panlechklist" Height="85px" CssClass="txt_field" BorderStyle="Inset">
        <evy:ScrollableListBox ID="CheckBoxList2" runat="server" BorderWidth="0px" 
        RepeatLayout="Flow" Height="75px" Width="265px" ></evy:ScrollableListBox>
              </asp:Panel>
					</div>
				</div>
           </div>    
       <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Notes%></label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txtNotes" runat="server" Width="496px" TextMode="MultiLine" 
                 Height="50px"></asp:TextBox>
					</div>
				</div>
           </div>    
       <div class="form-group">
           <div class="col-md-12">
               <div class="col-sm-3"></div>
               <div class="col-sm-9"> <asp:Button ID="imgSave" SkinID="btnSave" runat="server" 
                onclick="imgSave_Click" ValidationGroup="save" /> <asp:Button ID="ImgCancel" runat="server" SkinID="btnCancel" />
                            <asp:Button runat="server" ID="imgItemEdit" Style="display: none" /></div>
           </div>
       </div>

</asp:Panel>
 <ajaxToolkit:ModalPopupExtender TargetControlID="imgaddRecurr" CancelControlID="imgCancel" PopupControlID="pnlRecurrence"
    BackgroundCssClass="modalBackground" runat="server" ID="mpopRecurrence"></ajaxToolkit:ModalPopupExtender>
    
   <asp:Panel ID="pnlRecurrence" runat="server" ScrollBars="None" Width="670px" Height="470px"   Style="display: none" 
   BorderStyle="Double" BackColor="White" BorderColor="LightSteelBlue">
    <div style="width:250px;height:20px" >  <asp:CustomValidator ID="CustomValidator1" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckItemPattern" ErrorMessage="Select recurrence pattern">
                    </asp:CustomValidator><br/>
                      <asp:CustomValidator ID="CustomValidator2" ValidationGroup="HealthCheckRecurr"  runat="server" ClientValidationFunction="CheckRange" ErrorMessage="Select range of recurrence pattern">
                    </asp:CustomValidator>
                    
                    <asp:ValidationSummary ID="HealthCheckRecurr" runat="server"  DisplayMode="BulletList" ForeColor="Red"  /> </div>
       <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.Recurrance%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
   <div class="form-group">
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
                    <asp:Label ID="ImgStartDate" runat="server" SkinID="Calender" />
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
                                <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="ImgEndate" runat="server" SkinID="Calender" />
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
                                SkinID="btnApply" OnClick="ImgApply_Click"/> <asp:Button ID="ImageButton1" SkinID="btnCancel" runat="server" /></div>
   </asp:Panel>
  

<div>
    <asp:HiddenField ID="hdnID" runat="server" Value="0" />
     <asp:HiddenField ID="hdnAlertID" runat="server" Value="0" />
    <asp:GridView ID="gridAlert" runat="server" AutoGenerateColumns="false" width="100%"
        onrowcommand="gridAlert_RowCommand" EmptyDataText="No Records Found">
    <Columns>
    <asp:TemplateField>
     <HeaderStyle  CssClass="header_bg_l" />
     <ItemTemplate>
       <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" Enabled='<%#CommandField()%>' CommandName="AlertAdd"
                                                CommandArgument='<%# Bind("AlertID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                            </asp:LinkButton></div>
     </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Description">
    
     <ItemTemplate>
         <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("AlertDescription")%>'></asp:Label>
     </ItemTemplate>
     </asp:TemplateField>
      <asp:TemplateField HeaderText="Alert Date">
       
     <ItemTemplate>
         <asp:Label ID="lblAlertDate" runat="server" Text='<%# Bind("AlertDate","{0:d}")%>'></asp:Label>
     </ItemTemplate>
     </asp:TemplateField>
      <asp:TemplateField HeaderText="Due Date">
        
     <ItemTemplate>
         <asp:Label ID="lblDueDate" runat="server" Text='<%# Bind("DueDate","{0:d}")%>'></asp:Label>
     </ItemTemplate>
     </asp:TemplateField>
      <asp:TemplateField HeaderText="Distribution List">
        
     <ItemTemplate>
     <%-- '<%#SetImageGray(DataBinder.Eval(Container.DataItem,"ID").ToString())%>'<asp:Label ID="lblResources" Width="150px" runat="server" Text='<%# lblResource(DataBinder.Eval(Container.DataItem, "Resources").ToString()) %>' ></asp:Label>--%>
         <asp:Label ID="lblDistribution" runat="server" Text='<%#GetResources(DataBinder.Eval(Container.DataItem,"DistributionList").ToString())%>'></asp:Label>
     </ItemTemplate>
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Notes">
      
     <ItemTemplate>
         <asp:Label ID="lblList" runat="server" Text='<%# Bind("Notes")%>'></asp:Label>
     </ItemTemplate>
     </asp:TemplateField>
       <asp:TemplateField HeaderText="Recurrence" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
           <asp:Label ID="lblIDhd" runat="server" Text='<%# Bind("AlertID")%>' Visible="false"></asp:Label>
         <asp:LinkButton ID="imgAdd" runat="server" SkinID="BtnLinkAdd" ToolTip="Add recurrence"
                           CommandArgument='<%# Bind("AlertID")%>' CommandName="Reccurrence" Enabled='<%#CommandField()%>' />
     </ItemTemplate>
     </asp:TemplateField>
    </Columns>
    </asp:GridView>
</div>
