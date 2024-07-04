<%@ Control Language="C#" AutoEventWireup="true" Inherits="Delivery" Codebehind="Delivery.ascx.cs" %>
<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    $(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/Volta_Underwork/webservices/DCServices.asmx/GetDeliveryDefaults",
            data: "{}",
            dataType: "json",
            success: function (data) {
                $('#lblweight').text(data.d.Weight);
                $('#h_Overdue').val(data.d.OverdueDays);
                $('#h_Charges').val(data.d.StorageCharges);
            }
        });
    });
    $().ready(function () {
        $("#ddlRequestersName").change(function () {         
            BindValues();
        });
    });
    $().ready(function () {
        BindValues();
    });
    function BindValues() {  
        var ID = $('#ddlRequestersName').val();       
        if (ID != "0") {
            $("#txtReqTelNo").html("");
            $.ajax({
                type: "POST",
                url: "/Volta_Underwork/webservices/DCServices.asmx/GetReqTelNo",
                data: "{ID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    document.getElementById('txtRequestersTelephoneNo').value = msg.d;
                }
            });

            $("#txtReqEmailAddress").html("");
            $.ajax({
                type: "POST",
                url: "/Volta_Underwork/webservices/DCServices.asmx/GetReqEmail",
                data: "{ID:'" + ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    document.getElementById('txtRequestersEmailAddress').value = msg.d;
                }
            });
        }
        document.getElementById('txtRequestersTelephoneNo').value = "";
        document.getElementById('txtRequestersEmailAddress').value = "";
    }
    $(function () {
        $('#txtDateReceived').change(function () {
            var dateText = $('#txtDateReceived').val();
            var odays = $('#h_Overdue').val();
            if (dateText != null || dateText != "") {
                $.ajax({
                    type: "POST",
                    url: "/Volta_Underwork/webservices/DCServices.asmx/AddDays",
                    data: "{date:'" + dateText + "',days:'" + odays + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#txtChargeableDate').val(msg.d);
                    }
                });
            };
        });
    });

    $(function () {
        $('#txtFrom').change(function () {
            CalculateStorageCharges();
        });
    });
    $(function () {
        $('#txtTo').change(function () {
            CalculateStorageCharges();
        });
    });
    function CalculateStorageCharges() {
        var from = $('#txtFrom').val();
        var to = $('#txtTo').val();
        if (from != "" && to != "") {
            var charges = $('#h_Charges').val();
            $.ajax({
                type: "POST",
                url: "/Volta_Underwork/webservices/DCServices.asmx/CalculateStorageCharges",
                data: "{from:'" + from + "',to:'" + to + "',charges:'" + charges + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $('#txtPeriodCost').val(msg.d);
                }
            });
        }
    }
    $().ready(function () {
        $("#ddlRequestersCompany").change(function () {
            $('#txtRequestersTelephoneNo').val('');
            $('#txtRequestersEmailAddress').val('');
        });
    });
    
</script>
    <table width="100%">
<tr>
<td colspan="4">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
       ValidationGroup="d" DisplayMode="BulletList" />
    </td>
</tr>
<tr id="trticket" runat="server">
<td colspan="4">Ticket Number<br /><asp:Literal ID="ltTicket" runat="server"></asp:Literal></td>
</tr>
    <tr>
        <td valign="top" style="padding-top: 5px" width="250px">
              Requesters company
            <br />
             <asp:DropDownList ID="ddlRequestersCompany" runat="server" Width="220px" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading company...]" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="ddlRequestersCompany" Display="Dynamic" 
                  ErrorMessage="Please select company" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="d">*</asp:RequiredFieldValidator>
              <br />
              Requesters name
            <br />
             <asp:DropDownList ID="ddlRequestersName" runat="server" Width="220px" ClientIDMode="Static">
            </asp:DropDownList>
              <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlRequestersName"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRequestersCompany" LoadingText="[Loading name...]" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  ControlToValidate="ddlRequestersName" Display="Dynamic" 
                  ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="d">*</asp:RequiredFieldValidator>
              <br />
            Requesters Telephone No
            <br />
            <asp:TextBox ID="txtRequestersTelephoneNo" runat="server" Width="200px" ClientIDMode="Static"></asp:TextBox>
              <br />
            Requesters Email Address
            <br />
            <asp:TextBox ID="txtRequestersEmailAddress" runat="server" Width="200px"  ClientIDMode="Static"></asp:TextBox>
        </td>
        <td valign="top" align="center" width="150px">
            Site
        </td>
        <td valign="top" width="170px">
            <asp:DropDownList ID="ddlSite" runat="server" Width="160px">              
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetSiteByCusomerId"  ParentControlID="ddlRequestersCompany" LoadingText="[Loading site...]" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlSite" Display="Dynamic" ErrorMessage="Please select site" 
                InitialValue="0" SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_Overdue" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_Charges" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_cid" runat="server" Value="0" />
        </td>
        <td valign="top">
            &nbsp;</td>
    </tr>
    <tr>
        <td valign="top" style="padding-top: 5px">
            Type of Permit</td>
        <td valign="top">
            <asp:DropDownList ID="ddlTypeofRequest" runat="server" Width="140px">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="ddlTypeofRequest" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
        </td>
        <td valign="top" style="padding-top: 5px" align="center">
            &nbsp;Status
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlStatus" runat="server" Width="180px">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlStatus" Display="Dynamic" 
                ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    </table>
    <table width="70%">
    <tr>
    <td style="padding-bottom: 15px" colspan="5"><b>Delivery Information</b>
        </td>
    </tr>
    <tr>
        <td>
            Anticipated Date of Arrival
        </td>
        <td width="230px">
          <asp:TextBox ID="txtDateofArrival" runat="server" Width="200px" 
                style="margin-bottom: 0px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="txtDateofArrival" Display="Dynamic" 
                ErrorMessage="Please enter arrival date" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtDateofArrival" 
                    ValidationGroup="d" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID ="filterarrival" runat="server" TargetControlID="txtDateofArrival" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgToDate" TargetControlID="txtDateofArrival">
        </ajaxToolkit:CalendarExtender>
        </td>
        <td>
         <asp:Label ID="imgToDate" runat="server" SkinID="Calender" />
        </td>
    </tr>
    <tr>
        <td>
            Anticipated Weight
        </td>
        <td>
            <asp:TextBox ID="txtWeight" runat="server" Width="200px"></asp:TextBox>
             <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender7" runat="server" TargetControlID="txtWeight" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="txtWeight" Display="Dynamic" 
                ErrorMessage="Please enter weight" SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
        </td>
        <td>&nbsp;
          </td>
            <td>  Courier Tracking Number </td>
            <td> <asp:TextBox ID="txtTrackingNumber" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Number of Boxes</td>
        <td>
            <asp:TextBox ID="txtNumberofBoxes" runat="server" Width="201px"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender5" runat="server" TargetControlID="txtNumberofBoxes" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ControlToValidate="txtNumberofBoxes" Display="Dynamic" 
                ErrorMessage="Please enter number of boxes" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
        </td>
        <td>&nbsp;
           </td>
            <td> Courier Company</td>
            <td><asp:TextBox ID="txtCourierCompany" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    </table>
    <table width="100%">
    <tr>
    <td valign="top" width="220px">Delivery Type</td>
    <td valign="top">           
      <asp:DropDownList ID="ddlDeliveryType" runat="server" Width="210px">
            </asp:DropDownList>
              <ajaxToolkit:CascadingDropDown ID="ccdDeliveryType" runat="server" TargetControlID="ddlDeliveryType"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetDeliveryType" LoadingText="[Loading type...]" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ControlToValidate="ddlDeliveryType" Display="Dynamic" 
            ErrorMessage="Please select delivery type" InitialValue="0" 
            SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
              </td>
              <td valign="top">Description<br /><asp:TextBox ID="txtDescription" runat="server" Height="100px" TextMode="MultiLine" Width="450px"></asp:TextBox></td>
              <td valign="top">Over 1 Pallet<br />
       <asp:DropDownList ID="ddlOver1pallet" runat="server" Width="150px">
           <asp:ListItem Value="0">Please Select</asp:ListItem>
           <asp:ListItem>Yes</asp:ListItem>
           <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList> 
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                      ControlToValidate="ddlOver1pallet" Display="Dynamic" 
                      ErrorMessage="Please select over 1 pallet" InitialValue="0" 
                      SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
        </td>
            <td valign="top">Over <asp:Label ID="lblweight" runat="server" ClientIDMode="Static"></asp:Label>kg Weight<br />
             <asp:DropDownList ID="ddlOverWeight" runat="server" Width="150px">
               <asp:ListItem Value="0">Please Select</asp:ListItem>
           <asp:ListItem>Yes</asp:ListItem>
           <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ControlToValidate="ddlOverWeight" Display="Dynamic" 
                    ErrorMessage="please select over weight" InitialValue="0" 
                    SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr><td colspan="6"><hr /></td></tr>
    </table>
    <table width="100%">
    <tr id="trreceived" runat="server"><td style="padding-bottom: 15px"><b>Recieved Items</b></td></tr>
    <tr><td valign="top" id="tdreceived" runat="server">Condition<br />
    <asp:DropDownList ID="ddlCondition" runat="server" Width="220px">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
            ControlToValidate="ddlCondition" Display="Dynamic" 
            ErrorMessage="Please select condition" InitialValue="0" SetFocusOnError="True" 
            ValidationGroup="d">*</asp:RequiredFieldValidator>
             <ajaxToolkit:CascadingDropDown ID="ccdCondition" runat="server" TargetControlID="ddlCondition"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetCondition" LoadingText="[Loading condition...]" /><br />
            Number of boxes rec<br />
            <asp:TextBox ID="txtNumberofBoxesRec" runat="server" Width="200px"></asp:TextBox>
              <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender6" runat="server" TargetControlID="txtNumberofBoxesRec" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
            ControlToValidate="txtNumberofBoxesRec" Display="Dynamic" 
            ErrorMessage="please enter no.of boxes received" SetFocusOnError="True" 
            ValidationGroup="d">*</asp:RequiredFieldValidator>
        <br />
            Storage Location<br />
             <asp:DropDownList ID="ddlStorageLocation" runat="server" Width="220px">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
            ControlToValidate="ddlStorageLocation" Display="Dynamic" 
            ErrorMessage="Please select storage location" InitialValue="0" 
            SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
             <ajaxToolkit:CascadingDropDown ID="ccdLocation" runat="server" TargetControlID="ddlStorageLocation"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                ServiceMethod="GetStorageLocation" LoadingText="[Loading location...]" /></td>
            <td valign="top">Notes<br /><asp:TextBox ID="txtNotes" runat="server" Height="100px" TextMode="MultiLine" Width="450px"></asp:TextBox><br />
            History<br /><asp:TextBox ID="txtHistory" runat="server" Height="100px" TextMode="MultiLine" Width="450px"></asp:TextBox></td>
            <td valign="top" id="tdreceived1" runat="server">           
            <table width="100%">
            <tr><td width="150px">Date Recieved</td>
            <td colspan="2">
           
                <asp:TextBox ID="txtDateReceived" runat="server" Width="200px" 
                    ClientIDMode="Static" 
                    ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                    ControlToValidate="txtDateReceived" Display="Dynamic" 
                    ErrorMessage="Please enter received date" SetFocusOnError="True" 
                    ValidationGroup="d">*</asp:RequiredFieldValidator>
                     <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtDateReceived" 
                    ValidationGroup="d" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                     <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDateReceived" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="imgDateReceived" runat="server" SkinID="Calender" />
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDateReceived" TargetControlID="txtDateReceived">
        </ajaxToolkit:CalendarExtender>
        
        </td></tr>
            <tr><td>Days in Storage</td>
           <td colspan="2"><asp:TextBox ID="txtDaysinStorage" runat="server" Width="200px"></asp:TextBox>
           <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender8" runat="server" TargetControlID="txtDaysinStorage" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                   ControlToValidate="txtDaysinStorage" Display="Dynamic" 
                   ErrorMessage="Please enter storage days" SetFocusOnError="True" 
                   ValidationGroup="d">*</asp:RequiredFieldValidator>
                </td> </tr>
           <tr><td>Chargeable date</td>
           <td colspan="2"><asp:TextBox ID="txtChargeableDate" runat="server" Width="200px" ClientIDMode="Static"></asp:TextBox>
           <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender9" runat="server" TargetControlID="txtChargeableDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender></td></tr>
           <tr><td>Total Cost</td>
           <td colspan="2"><asp:TextBox ID="txtTotalCost" runat="server" Width="200px"></asp:TextBox>
           <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender4" runat="server" TargetControlID="txtTotalCost" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                   ControlToValidate="txtTotalCost" Display="Dynamic" 
                   ErrorMessage="Please enter total cost" SetFocusOnError="True" 
                   ValidationGroup="d">*</asp:RequiredFieldValidator>
               </td></tr>
           <tr><td>Storage Period</td>
           <td width="20px">From<br /><asp:TextBox ID="txtFrom" runat="server" Width="100px" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgDatefrom" runat="server"  
                   SkinID="Calender" />
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDatefrom" TargetControlID="txtFrom">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFrom" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                   ControlToValidate="txtFrom" Display="Dynamic" 
                   ErrorMessage="Please enter from storage period" SetFocusOnError="True" 
                   ValidationGroup="d">*</asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtFrom" 
                    ValidationGroup="d" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
               </td>
               <td width="80px">To<br /><asp:TextBox ID="txtTo" runat="server" Width="100px" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgDateto" runat="server" ImageAlign="AbsMiddle" 
                       SkinID="Calender" />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDateto" TargetControlID="txtTo">
        </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTo" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                       ControlToValidate="txtTo" Display="Dynamic" 
                       ErrorMessage="Please enter to storage period" SetFocusOnError="True" 
                       ValidationGroup="d">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvdate" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtTo" 
                    ValidationGroup="d" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
               </td></tr>
           <tr><td>Period Cost</td>
           <td colspan="2"><asp:TextBox ID="txtPeriodCost" runat="server" Width="200px" ClientIDMode="Static"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender10" runat="server" TargetControlID="txtPeriodCost" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                   ControlToValidate="txtPeriodCost" Display="Dynamic" 
                   ErrorMessage="Please enter period cost" SetFocusOnError="True" 
                   ValidationGroup="d">*</asp:RequiredFieldValidator>
               </td></tr>
            </table>          
            </td>
            </tr>
            <tr><td style="padding-top: 15px" align="center"> Upload File(s)</td>
            <td style="padding-top: 15px"> 
            <asp:FileUpload ID="DocumentsUpload" runat="server" />                       
             <asp:Button ID="ImgDeskImageUpload" runat="server" SkinID="upload" 
                    onclick="ImgDeskImageUpload_Click" ValidationGroup="d" />                             
                    </td></tr>
             <tr><td>
                 <asp:Button ID="btnSave" runat="server" AlternateText="Save" 
                     SkinID="save" onclick="btnSave_Click" ValidationGroup="d" />&nbsp
                    <asp:Button  ID="btnCancel" runat="server" AlternateText="Cancel" 
                     SkinID="Cancel" onclick="btnCancel_Click" /></td>
                    <td>File Location<br />
                  <asp:GridView ID="GvDocuments" runat="server" Width="100%" EmptyDataText="No Documents found"
                OnRowCommand="GvDocuments_RowCommand" DataKeyNames="ContentType,FileName">
                <Columns>
                    <asp:TemplateField HeaderText="Uploaded Documents">
                        <ItemTemplate>                            
                            <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                runat="server" CommandName="Download"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView></td></tr>
    </table>