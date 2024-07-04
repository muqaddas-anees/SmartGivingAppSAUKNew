<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DC_DeliveryCustomer" EnableEventValidation="false" Codebehind="DeliveryCustomer.aspx.cs" %>
<%@ Register src="controls/DeliveryHistory.ascx" tagname="DeliveryHistory" tagprefix="uc1" %>
<%@ Register src="controls/NotesCtrl.ascx" tagname="Notes" tagprefix="uc3" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server">
                  </label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="panel_options" Runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=Delivery">
<i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<script src="../js/jquery-1.2.6.js" type="text/javascript"></script>
<script src="../js/jquery-1.3.2.js" type="text/javascript"></script>--%>
<script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>
   <%-- <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
  <%--  <script type="text/javascript">
        $(document).ready(function () {
            $(document.body).find("[id$='lblPageHead']").html('Delivery');
        });
    </script>--%>
     <script type="text/javascript" language="javascript">

         function onPopulated() {
             $get('ddlTypeofRequest').disabled = true;
             $get('ddlRequestersCompany').disabled = true;
             $get('ddlStatus').disabled = true;
             $get('ddlRequestersName').disabled = true;

             var id = $('#h_callid').val();
             if (id != 0) {
                 $get('ddlDeliveryType').disabled = true;
                 $get('ddlSite').disabled = true;
                 $get('ddlItemWeight').disabled = true;
                 //$get('ddlOver1pallet').disabled = true;
                 //$get('ddlOverWeight').disabled = true;

             }




         }
         function pageLoad(sender, args) {
             $find("ccdT").add_populated(onPopulated);
             $find("ccdC").add_populated(onPopulated);
             $find("ccdS").add_populated(onPopulated);
             $find("ccdDT").add_populated(onPopulated);
             $find("ccdst").add_populated(onPopulated);
             $find("ccdrn").add_populated(onPopulated);
             $find("ccdw").add_populated(onPopulated);
             BindValues();
         }
         $(function () {
             var id = $('#h_callid').val();
             if (id != 0) {
                 $('#txtDateofArrival').attr('readonly', true);
                 //$('#txtWeight').attr('readonly', true);
                 $('#txtNumberofBoxes').attr('readonly', true);
                 $('#txtDescription').attr('readonly', true);
                 // $('#txtTrackingNumber').attr('readonly', true);
                 $('#txtCourierCompany').attr('readonly', true);
                 //$('#txtNotes').attr('readonly', true);
                 $('#txtRequestersTelephoneNo').attr('readonly', true);
                 $('#txtRequestersEmailAddress').attr('readonly', true);
                 $('#imgToDate').hide();

             }
         });


         $(function () {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "/WF/DC/webservices/DCServices.asmx/GetDeliveryDefaults",
                 data: "{}",
                 dataType: "json",
                 success: function (data) {
                     //$('#lblweight').text(data.d.Weight);
                     $('#h_Overdue').val(data.d.OverdueDays);
                     $('#h_Charges').val(data.d.StorageCharges);
                     //$('#h_notice').val(data.d.HeavyItemNotice);
                 }
             });
         });
         $().ready(function () {
             $("#ddlOver1pallet").change(function () {
                 Showalert();
             });
         });
         //        $().ready(function () {
         //            $("#ddlOverWeight").change(function () {
         //                Showalert();
         //            });
         //        });
         $().ready(function () {
             $("#ddlItemWeight").change(function () {
                 Showalert();
             });
         });

         function getQuerystring(key, default_) {

             if (default_ == null) default_ = "";
             key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
             var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
             var qs = regex.exec(window.location.href.toLowerCase());
             if (qs == null)
                 return default_;
             else
                 return qs[1];
         }
         function Showalert() {
             var pallet = $('#ddlOver1pallet').val();
             //var weight = $('#ddlOverWeight').val();
             var weight = $('#ddlItemWeight').val();
             var notice = $('#h_notice').val();
             var callid = getQuerystring('callid');

             if (pallet == "Yes" && (weight != "1" && weight != "0")) {
                 ShowNoticePopup();
                 //alert(notice);
             }
             function onEnablePopulated() {
                 $get('ddlStatus').disabled = false;
             }
             if (pallet == "No" && weight == "1" && callid == "") {

                 var ccdStat = $find('ccdS');
                 ccdStat.set_SelectedValue('37');
                 $("#ddlStatus").val("37"); // 37 for status "Awaiting Delivery"
             }
             if ((pallet == "Yes" || (weight != "1" && weight != "0")) && callid == "") {
                 var ccdStat = $find('ccdS');
                 ccdStat.set_SelectedValue('1');
                 $("#ddlStatus").val("1");  // 1 for status "Awaiting Approval"
             }
         }
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
                     url: "/WF/DC/webservices/DCServices.asmx/GetContactDetails",
                     data: "{rid:'" + ID + "'}",
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (msg) {
                         document.getElementById('txtRequestersTelephoneNo').value = msg.d.Telephone;
                         document.getElementById('txtRequestersEmailAddress').value = msg.d.Email;
                     }
                 });
             }
             document.getElementById('txtRequestersTelephoneNo').value = "";
             document.getElementById('txtRequestersEmailAddress').value = "";
         }
         $().ready(function () {
             $("#ddlRequestersCompany").change(function () {
                 $('#txtRequestersTelephoneNo').val('');
                 $('#txtRequestersEmailAddress').val('');
             });
         });
</script>
<script language="javascript" type="text/javascript">
    //Fade out buttons when clicked but only if page validate
    $().ready(function () {
        $('#lbl_loading').hide();
        $('#div_buttons').show();
        $('#btnSave').click(function (e) {
            if (Page_ClientValidate()) {
                $('#div_buttons').fadeOut('fast');
                $('#lbl_loading').fadeIn('slow');
            }
            else { return false; }
        });
    });
</script>

    

<div class="form-group row">
      <div class="col-md-12">
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="d" />
	</div>

</div>


     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRequestersCompany" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" BehaviorID="ccdC" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  ControlToValidate="ddlRequestersCompany" Display="Dynamic" 
                  ErrorMessage="Please select company" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="d">*</asp:RequiredFieldValidator> 
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_80" ClientIDMode="Static">              
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetOurSite_withoutCustomer" LoadingText="[Loading site...]" BehaviorID="ccdst" ClientIDMode="Static"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlSite" Display="Dynamic" ErrorMessage="Please select site" 
                InitialValue="0" SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
                <asp:HiddenField ID="h_Overdue" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_Charges" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="h_cid" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="h_callid" runat="server" Value="0" ClientIDMode="Static" />
                  <asp:HiddenField ID="h_cmid" runat="server"  ClientIDMode="Static" Value="0" />
                    <asp:HiddenField ID="h_rid" runat="server"  ClientIDMode="Static" Value="0" />
                      <asp:HiddenField ID="h_notice" runat="server" ClientIDMode="Static" />
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequesterName%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRequestersName" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
              <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlRequestersName"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRequestersCompany" LoadingText="[Loading name...]" BehaviorID="ccdrn" ClientIDMode="Static" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  ControlToValidate="ddlRequestersName" Display="Dynamic" 
                  ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True" 
                  ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.TypeofRequest%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlTypeofRequest" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]"  ClientIDMode="Static" SelectedValue="1" BehaviorID="ccdT" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="ddlTypeofRequest" Display="Dynamic" 
                ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersTelephoneNo%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtRequestersTelephoneNo" runat="server" SkinID="txt_80" ClientIDMode="Static" MaxLength="16"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server"
    TargetControlID="txtRequestersTelephoneNo" ValidChars="0123456789+ "  />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" SelectedValue="1" BehaviorID="ccdS" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="ddlStatus" Display="Dynamic" 
                ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.RequestersEmailAddress%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtRequestersEmailAddress" runat="server" SkinID="txt_80"  ClientIDMode="Static" ></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group row">
        <div class="col-md-12">
           <strong>Delivery Information </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Anticipated Date of Delivery</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtDateofArrival" runat="server" SkinID="Date" 
                style="margin-bottom: 0px" ClientIDMode="Static"></asp:TextBox>
                <asp:Label ID="imgToDate" runat="server"  SkinID="Calender" ClientIDMode="Static" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="txtDateofArrival" Display="Dynamic" 
                ErrorMessage="Please enter arrival date" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="Please enter dd/MM/yyyy format" ControlToValidate="txtDateofArrival" 
                    ValidationGroup="d" Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareVal_Date" runat="server" 
                    ErrorMessage="Please check Anticipated Date should be on or after today" ControlToValidate="txtDateofArrival" 
                    ValidationGroup="d" Type="Date" Operator="GreaterThanEqual" Text="*" 
                    Display="Dynamic" SetFocusOnError="True" ></asp:CompareValidator>
                <ajaxToolkit:FilteredTextBoxExtender ID ="filterarrival" runat="server" TargetControlID="txtDateofArrival" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgToDate" TargetControlID="txtDateofArrival">
        </ajaxToolkit:CalendarExtender>
        
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Courier Tracking Number</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtTrackingNumber" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
             <asp:RequiredFieldValidator ID="rq_tracknumber" runat="server" 
                ControlToValidate="txtTrackingNumber" Display="Dynamic" 
                ErrorMessage="Please enter courier tracking number" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Number of Boxes expected</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtNumberofBoxes" runat="server" SkinID="txt_70" Text="1" ClientIDMode="Static"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender5" runat="server" TargetControlID="txtNumberofBoxes" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ControlToValidate="txtNumberofBoxes" Display="Dynamic" 
                ErrorMessage="Please enter number of boxes" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="CompareValidator6" runat="server" 
                  ControlToValidate="txtNumberofBoxes" 
                    Display="Dynamic" ErrorMessage="Number of boxes should be more than zero" 
                    Operator="GreaterThan" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="d" ValueToCompare="0">*</asp:CompareValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Courier Company</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtCourierCompany" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rq_couriercompany" runat="server" 
                ControlToValidate="txtCourierCompany" Display="Dynamic" 
                ErrorMessage="Please enter courier company" SetFocusOnError="True" 
                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Delivery Type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlDeliveryType" runat="server" SkinID="ddl_80"  ClientIDMode="Static">
            </asp:DropDownList>
              <ajaxToolkit:CascadingDropDown ID="ccdDeliveryType" runat="server" TargetControlID="ddlDeliveryType"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetDeliveryType" LoadingText="[Loading type...]" BehaviorID="ccdDT" ClientIDMode="Static" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ControlToValidate="ddlDeliveryType" Display="Dynamic" 
            ErrorMessage="Please select delivery type" InitialValue="0" 
            SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Over 1 Pallet</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlOver1pallet" runat="server" SkinID="ddl_80" ClientIDMode="Static">
           <asp:ListItem Value="0">Please Select</asp:ListItem>
           <asp:ListItem>Yes</asp:ListItem>
           <asp:ListItem>No</asp:ListItem>
            </asp:DropDownList> 
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                      ControlToValidate="ddlOver1pallet" Display="Dynamic" 
                      ErrorMessage="Please select over 1 pallet" InitialValue="0" 
                      SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
          
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Weight%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlItemWeight" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccditemweight" runat="server" TargetControlID="ddlItemWeight"
                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetDeliveryItemWeight" LoadingText="[Loading ...]" BehaviorID="ccdw" ClientIDMode="Static" />
            <asp:RequiredFieldValidator ID="rfv_weight" runat="server" 
                    ControlToValidate="ddlItemWeight" Display="Dynamic" 
                    ErrorMessage="please select weight" InitialValue="0" 
                    SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Description%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtDescription" runat="server" 
                      Height="100px" TextMode="MultiLine" Width="550px" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           
	</div>
</div>

     <div class="form-group row" id="trupload" runat="server">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Upload File(s)</label>
           <div class="col-sm-9">
                 <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" BorderStyle="None" ScrollBars="Auto">
                <asp:FileUpload ID="DocumentsUploadnew" runat="server" maxlength="5" class="multi" />
            </asp:Panel>                        
             <asp:Button ID="ImgDeskImageUpload" runat="server" SkinID="ImgUpload" 
                    onclick="ImgDeskImageUpload_Click" ValidationGroup="d" 
                    ClientIDMode="Static" Visible="False" />   
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"> </label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
     <div class="form-group row" runat="server" id="tdbuttons">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <span style="margin-right:80px"> <asp:Label ID="lbl_loading" runat="server" Text="Loading..." ClientIDMode="Static" Font-Bold="true"></asp:Label></span>
  <div id="div_buttons">
                 <asp:Button ID="btnSave" runat="server" AlternateText="Save" 
                     SkinID="btnSubmit" onclick="btnSave_Click" ValidationGroup="d" ClientIDMode="Static" />&nbsp
                    <asp:Button ID="btnCancel" runat="server" AlternateText="Cancel" 
                     SkinID="btnCancel" onclick="btnCancel_Click" />
                     </div>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>

     <div class="form-group row">
      <div class="col-md-6">
          File Location<br />
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
            </asp:GridView>
	</div>
	<div class="col-md-6">
          
	</div>
</div>



  
     <script type="text/javascript">
         function ShowNoticePopup() {
             $find("mpe").show();
             return false;
         }
         function HideNoticePopup() {
             $find("mpe").hide();
             return false;
         }

</script>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
    PopupControlID="pnlPopup" TargetControlID="btnPopupDum" BackgroundCssClass="modalBackground" DynamicServicePath="~/WF/DC/webservices/DCServices.asmx" DynamicServiceMethod="GetDeliveryDefaults_Notice" DynamicControlID="lblnotice">
</ajaxToolkit:ModalPopupExtender>
<asp:Button ID="btnPopupDum" runat="server" Style="display: none"/>
<asp:Panel ID="pnlPopup" runat="server" BorderStyle="Double" BackColor="White" BorderColor="LightSteelBlue" Width="400px" Height="350px" Style="display: none">
<div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                           Message
                        </div>
                      
        <asp:Panel ID="pop_Data" runat="server" Width="400px" Height="270px" ScrollBars="Vertical" Style="overflow:auto;" >
        
        <label id="lblnotice" runat="server"></label>
        </asp:Panel>
        <br />
        <div style="text-align:center;" >
        <asp:Button ID="btnHide" runat="server" Text="Ok" OnClientClick="return HideNoticePopup()" Width="75px" />
    </div>
</asp:Panel>
    <asp:Panel ID="pnlNotes" runat="server" Visible="false">
  <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
       Notes</div>
 <uc3:Notes ID="Notes1" runat="server" />
        </asp:Panel>
 
    <div>
    <uc1:DeliveryHistory ID="ctr_history" runat="server" />
    </div>
</asp:Content>

