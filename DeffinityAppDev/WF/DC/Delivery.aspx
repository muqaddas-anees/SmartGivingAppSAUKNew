<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_Delivery" EnableEventValidation="false" Codebehind="Delivery.aspx.cs" %>

<%--<%@ MasterType VirtualPath="~/DeffinityTab.master" %>--%>
<%@ Register Src="controls/DeliveryHistory.ascx" TagName="History" TagPrefix="hi" %>
<%@ Register Src="controls/NotesCtrl.ascx" TagName="Notes" TagPrefix="uc3" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Delivery%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server">
                        </label>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
    <a id="A1" href="FLSJlist.aspx?type=Delivery" runat="server"
            target="_self"><i class="fa fa-arrow-left"></i> <%= Resources.DeffinityRes.ReturntoTicketJournal%></a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <script  type="text/javascript">
        $(document).ready(function () {
            $('#navTab').hide();
        });
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
  
     <script type="text/javascript">
         $(document).ready(function () {
             $(document.body).find("[id$='lblPageHead']").html('Delivery');
         });
    </script>
     <%--<script src="../js/jquery-1.3.2.js" type="text/javascript"></script>--%>
                <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
                <script type="text/javascript" language="javascript">
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

                    $(function () {
                        //hstatus
                        if ($("#hstatus").val() != "0") {
                            recived_pnl($("#hstatus").val());
                        }

                        //disable the all reviced validations
                        validationCheck(false);
                        recived_pnl($("#ddlStatus").val());
                        $("#ddlStatus").change(function () {
                            $("#hstatus").val(this.value);
                            recived_pnl($("#hstatus").val());
                        });
                    });

                    function recived_pnl(sid) {
                        debugger;
                        if (sid == "1" || sid == "36" || sid == "37") {
                            $("#div_received").hide();
                            validationCheck(false);
                        }
                        else {

                            $("#div_received").show();

                            validationCheck(true);
                            //recived part
                            if (sid == "4") {
                                validationReceivedPart(false);
                            }
                                //received
                            else if (sid == "3") {
                                validationReceived(false);
                            }
                                //collected part
                            else if (sid == "6") {
                                validationCollectedPart(false);
                            }
                                //collected
                            else if (sid == "5") {
                                validationCollected(false);
                            }
                                //closed
                            else if (sid == "7") {
                                validationClose(false);
                            }
                        }
                    }
                    function validationReceivedPart(setval) {

                        ValidatorEnable(document.getElementById("<%=cv_recivedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedcheck.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_closecheck.ClientID %>"), setval);

                    }
                    function validationReceived(setval) {
                        ValidatorEnable(document.getElementById("<%=cv_boxesreceived.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedcheck.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_closecheck.ClientID %>"), setval);
                    }
                    //check 
                    function validationCollectedPart(setval) {
                        ValidatorEnable(document.getElementById("<%=cv_recivedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_closecheck.ClientID %>"), setval);
                    }
                    function validationCollected(setval) {
                        //ValidatorEnable(document.getElementById("<%=cv_collectedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_recivedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_closecheck.ClientID %>"), setval);
                    }

                    function validationClose(setval) {
                        //var cerror = document.getElementById(<%=cv_collectedequal.ClientID %>);
                        //cerror.erro
                        //$(sourc).text("* Not a NA Phone #");
                        ValidatorEnable(document.getElementById("<%=cv_recivedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedequal.ClientID %>"), setval);
                        //ValidatorEnable(document.getElementById("<%=cv_closecheck.ClientID %>"), setval);
                    }
                    function validationCheck(setval) {
                        debugger;
                        ValidatorEnable(document.getElementById("<%=rv_condition.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_datereceived.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_datereceived.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_datecheck.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_daysinstorage.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_chargeabledate.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_totalcost.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_fromdate.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_todate.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_datecompare.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_boxesreceived.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_boxesreceived.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_oursite.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_storagelocation.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_recivedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=rv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedcheck.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collected.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_collectedequal.ClientID %>"), setval);
                        ValidatorEnable(document.getElementById("<%=cv_closecheck.ClientID %>"), setval);

                        //set value is true update the
                        if (!setval) {
                            ValidationSummaryOnSubmit();
                        }

                    }

                    function ClientValidationFunction(sender, args) {
                        debugger;
                        var valid = false;
                        // Validation logic..
                        if ($('#txtNumberofBoxesRec').val() == $('#txtCollectedBoxes').val()) {
                            valid = true;
                        }
                        else {
                            //sender.errormessage = "Validation failed";
                            valid = false;
                        }

                        args.IsValid = valid;
                        return;
                    }

                    $(function () {
                        var id = $('#h_cid').val();
                        if (id != 0) {
                            $('#txtDateofArrival').attr('readonly', true);
                            //$('#txtWeight').attr('readonly', true);
                            $('#txtNumberofBoxes').attr('readonly', true);
                            $('#txtDescription').attr('readonly', true);
                            //$('#txtTrackingNumber').attr('readonly', true);
                            $('#txtCourierCompany').attr('readonly', true);
                            $('#imgToDate1').hide();
                            //                 $('#txtRequestersTelephoneNo').attr('readonly', true);
                            //                 $('#txtRequestersEmailAddress').attr('readonly', true);

                        }
                    });
                    function onPopulated() {
                        $get('ddlTypeofRequest').disabled = true;
                        var id = $('#h_cid').val();
                        if (id == 0) {
                            $get('ddlStatus').disabled = true;
                        }
                        else if (id != 0) {
                            $get('ddlRequestersCompany').disabled = true;
                            $get('ddlRequestersName').disabled = true;
                            $get('ddlDeliveryType').disabled = true;
                            $get('ddlSite').disabled = true;
                            $get('ddlItemWeight').disabled = true;
                            //$get('ddlOver1pallet').disabled = true;
                            //$get('ddlOverWeight').disabled = true;
                            var status = $("#ddlStatus option:selected").text();
                            if (status == "Closed") {
                                $get('ddlStatus').disabled = true;
                                $get('ddlCondition').disabled = true;
                                $get('ddlOurSite').disabled = true;
                                $get('ddlStorageLocation').disabled = true;
                                $('#txtNumberofBoxesRec').attr('readonly', true);
                                $('#txtCollectedBoxes').attr('readonly', true);
                                $('#txtNotes').attr('readonly', true);
                                $('#txtTrackingNumber').attr('readonly', true);
                                $('#txtDateReceived').attr('readonly', true);
                                $('#txtFrom').attr('readonly', true);
                                $('#txtTo').attr('readonly', true);
                                $('#btnUpdateCourier').hide();
                                $('#DocumentsUploadnew').hide();
                                $('#ImgDeskImageUpload').hide();
                                $('#imgDatefrom').hide();
                                $('#imgDateto').hide();
                                $('#imgDateReceived').hide();



                            }
                        }
                    }
                    function pageLoad(sender, args) {

                        //            $find("ccdT").add_populated(onPopulated);
                        //            $find("ccdS").add_populated(onPopulated);

                        $find("ccdT").add_populated(onPopulated);
                        $find("ccdC").add_populated(onPopulated);
                        $find("ccdS").add_populated(onPopulated);
                        $find("ccdDT").add_populated(onPopulated);
                        $find("ccdst").add_populated(onPopulated);
                        $find("ccdrn").add_populated(onPopulated);
                        $find("ccdw").add_populated(onPopulated);
                    }

                    $(function () {
                        //$('#txtPeriodCost').attr('readonly', true);
                        $('#txtChargeableDate').attr('readonly', true);
                        $('#txtDaysinStorage').attr('readonly', true);
                        $('#txtTotalCost').attr('readonly', true);


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
                                //$('#lblnotice').val(data.d.HeavyItemNotice);

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
                    $(function () {
                        $('#txtDateReceived').change(function () {

                            var dateText = $('#txtDateReceived').val();
                            var odays = $('#h_Overdue').val();
                            var nob = $('#txtNumberofBoxes').val();
                            var charges = $('#h_Charges').val();
                            if (dateText != null || dateText != "") {
                                $.ajax({
                                    type: "POST",
                                    url: "/WF/DC/webservices/DCServices.asmx/GetData",
                                    data: "{date:'" + dateText + "',days:'" + odays + "',nob:'" + nob + "',charges:'" + charges + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        if (msg.d.length > 0) {
                                            $('#txtChargeableDate').val(msg.d[0]);
                                            $('#txtDaysinStorage').val(msg.d[1]);
                                            $('#txtTotalCost').val(msg.d[2]);
                                        }
                                        else {
                                            $('#txtChargeableDate').val('');
                                            $('#txtDaysinStorage').val('');
                                            $('#txtTotalCost').val('');
                                        }
                                    }
                                });
                            };
                        });
                    });

                    //        $(function () {
                    //            $('#txtFrom').change(function () {
                    //                CalculateStorageCharges();
                    //            });
                    //        });
                    //        $(function () {
                    //            $('#txtTo').change(function () {
                    //                CalculateStorageCharges();
                    //            });
                    //        });
                    //        function CalculateStorageCharges() {
                    //            var from = $('#txtFrom').val();
                    //            var to = $('#txtTo').val();
                    //            if (from != "" && to != "") {
                    //                var startDate = new Date(DateConvertion($('#txtFrom').val()));
                    //                var endDate = new Date(DateConvertion($('#txtTo').val()));
                    //                if (startDate <= endDate) {
                    //                    var charges = $('#h_Charges').val();
                    //                    $.ajax({
                    //                        type: "POST",
                    //                        url: "../webservices/DCServices.asmx/CalculateStorageCharges",
                    //                        data: "{from:'" + from + "',to:'" + to + "',charges:'" + charges + "'}",
                    //                        contentType: "application/json; charset=utf-8",
                    //                        dataType: "json",
                    //                        success: function (msg) {
                    //                            //$('#txtPeriodCost').val(msg.d);
                    //                        }
                    //                    });
                    //                }
                    //                else {
                    //                    //$('#txtPeriodCost').val('');
                    //                }
                    //            }
                    //        }
                    function DateConvertion(s_date) {
                        var b = s_date;
                        var temp = new Array();
                        temp = b.split('/');
                        var s1 = temp[1] + '/' + temp[0] + '/' + temp[2];
                        return s1
                    }
                    $().ready(function () {
                        $("#ddlRequestersCompany").change(function () {
                            $('#txtRequestersTelephoneNo').val('');
                            $('#txtRequestersEmailAddress').val('');
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
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="d"
                                DisplayMode="BulletList" />
                            <asp:Label ID="lblerr" runat="server" ClientIDMode="Static" EnableViewState="false"></asp:Label>
                            <asp:HiddenField ID="hstatus" runat="server" ClientIDMode="Static" Value ="0" />
                            <asp:CustomValidator ID="cv_closecheck" runat="server" Display="None" ClientValidationFunction="ClientValidationFunction" ValidationGroup="d" ErrorMessage="Please note that the ticket cannot be closed until all items received have been collected."></asp:CustomValidator>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%> </label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlRequestersCompany" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetCompany" LoadingText="[Loading Customer...]" BehaviorID="ccdC" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRequestersCompany"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcustomer%>" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%> </label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetOurSite_withoutCustomer" LoadingText="[Loading site...]" BehaviorID="ccdst" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSite"
                                Display="Dynamic" ErrorMessage="Please select site" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <asp:HiddenField ID="h_Overdue" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="h_Charges" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="h_cid" runat="server" Value="0" ClientIDMode="Static" />
                            <asp:HiddenField ID="h_notice" runat="server" ClientIDMode="Static" />
                            <%--<asp:HiddenField ID="h_to" runat="server" />--%>
            </div>
	</div>
</div>
<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequestersName%></label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlRequestersName" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlRequestersName"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetNameByCompanyId" ParentControlID="ddlRequestersCompany" LoadingText="[Loading name...]"
                                BehaviorID="ccdrn" ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlRequestersName"
                                Display="Dynamic" ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.TypeofPermit%>  </label>
           <div class="col-sm-8 form-inline">
                 <asp:DropDownList ID="ddlTypeofRequest" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                                Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]" SelectedValue="1"
                                ClientIDMode="Static" BehaviorID="ccdT" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTypeofRequest"
                                Display="Dynamic" ErrorMessage="Please select permit" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>

    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequestersTelephoneNo%></label>
           <div class="col-sm-8 form-inline">
                 <asp:TextBox ID="txtRequestersTelephoneNo" runat="server" SkinID="txt_90" ClientIDMode="Static"
                                ReadOnly="True" MaxLength="16"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtRequestersTelephoneNo"
                                ValidChars="0123456789+ " />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Status%> </label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                                Display="Dynamic" ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]"
                                SelectedValue="1" BehaviorID="ccdS" />
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersEmailAddress%>  </label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtRequestersEmailAddress" runat="server" SkinID="txt_90" ClientIDMode="Static"
                                ReadOnly="True" MaxLength="200"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.LoggedDateTime%>  </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtLoggedDate" runat="server" Width="90px" ReadOnly="true" SkinID="Date"></asp:TextBox>
                            <asp:TextBox ID="txtLoggedTime" runat="server" Width="50px" ClientIDMode="Static"
                                ReadOnly="true" SkinID="Time"></asp:TextBox>(HH:MM)
            </div>
	</div>
</div>

    
<div class="form-group row">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.DeliveryInformation%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.AnticipatedDateofDelivery%>  </label>
           <div class="col-sm-8 form-inline"><asp:TextBox ID="txtDateofArrival" runat="server" SkinID="Date"
                                ClientIDMode="Static" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDateofArrival"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterarrivaldate%>"
 SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterinadateformat%>"
                                ControlToValidate="txtDateofArrival" ValidationGroup="d" Type="Date" Operator="DataTypeCheck"
                                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareVal_Date" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PleasecheckAnticipatedDateshouldbeonoraftertoday%>"
                                ControlToValidate="txtDateofArrival" ValidationGroup="d" Type="Date" Operator="GreaterThanEqual"
                                Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                            <asp:Label ID="imgToDate1" runat="server" SkinID="Calender"
                                ClientIDMode="Static" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="filterarrival" runat="server" TargetControlID="txtDateofArrival"
                                ValidChars="0123456789/">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <ajaxToolkit:CalendarExtender ID="calToDate1" runat="server" CssClass="MyCalendar"
                                 PopupButtonID="imgToDate1" TargetControlID="txtDateofArrival" ClientIDMode="Static" >
                            </ajaxToolkit:CalendarExtender>

            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CourierTrackingNumber%>  </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtTrackingNumber" runat="server" SkinID="txt_80" ClientIDMode="Static"
                                MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rq_tracknumber" runat="server" ControlToValidate="txtTrackingNumber"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentercouriertrackingnumber%>" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rq_c_trackingnumber" runat="server" ControlToValidate="txtTrackingNumber"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentercouriertrackingnumber%>" SetFocusOnError="True"
                                ValidationGroup="c_val">*</asp:RequiredFieldValidator>
                            <asp:LinkButton ID="btnUpdateCourier" runat="server" SkinID="BtnLinkUpdate"
                                ToolTip="<%$ Resources:DeffinityRes,Updatecouriertrackingnumber%>" OnClick="btnUpdateCourier_Click" Visible="false"
                                ValidationGroup="c_val" ClientIDMode="Static" />
            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.NumberofBoxesexpected%>  </label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtNumberofBoxes" runat="server" SkinID="txt_90" ClientIDMode="Static"
                                Text="1"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                TargetControlID="txtNumberofBoxes" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNumberofBoxes"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenternumberofboxes%>" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtNumberofBoxes"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Numberofboxesshouldbemorethanzero%>" Operator="GreaterThan"
                                SetFocusOnError="True" Type="Integer" ValidationGroup="d" ValueToCompare="0">*</asp:CompareValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CourierCompany%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtCourierCompany" runat="server" SkinID="txt_90" ClientIDMode="Static"
                                MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rq_couriercompany" runat="server" ControlToValidate="txtCourierCompany"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentercouriercompany%>" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>

    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DeliveryType%>  </label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlDeliveryType" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdDeliveryType" runat="server" TargetControlID="ddlDeliveryType"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetDeliveryType" LoadingText="[Loading type...]" BehaviorID="ccdDT"
                                ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeliveryType"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectdeliverytype%>" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           
	</div>
</div>

    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Over1Pallet%>  </label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlOver1pallet" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_overpallet" runat="server" ControlToValidate="ddlOver1pallet"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectover1pallet%>" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Weight%>  </label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlItemWeight" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccditemweight" runat="server" TargetControlID="ddlItemWeight"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetDeliveryItemWeight" LoadingText="[Loading ...]" BehaviorID="ccdw"
                                ClientIDMode="Static" />
                            <asp:RequiredFieldValidator ID="rfv_weight" runat="server" ControlToValidate="ddlItemWeight"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,pleaseselectweight%>" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>

    <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-12 control-label"> <%= Resources.DeffinityRes.Description%>  </label>
           <div class="col-sm-12">
                <asp:TextBox ID="txtDescription" runat="server" Height="100px" TextMode="MultiLine"
                                Width="450px" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Notes%> </label>
           <div class="col-sm-12">
               <asp:TextBox ID="txtNotes" runat="server" Height="100px" TextMode="MultiLine" Width="450px" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
    <div class="form-group row" id="div_received">
          <div class="col-md-12" id="tablereceived" runat="server">
            
              
<div class="form-group row">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.ReceivedItems%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

              <div class="form-group row">
                   <div class="col-md-6">

                       <div class="form-group row">
                        <div class="col-md-12">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Condition%>  </label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlCondition" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdCondition" runat="server" TargetControlID="ddlCondition"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetCondition" LoadingText="[Loading condition...]" />
                            <asp:RequiredFieldValidator ID="rv_condition" runat="server" ControlToValidate="ddlCondition"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcondition%>" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
                            </div>
            </div>

                       <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Numberofboxesreceived%></label>
         <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtNumberofBoxesRec" runat="server" SkinID="txt_90" Text="0" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                TargetControlID="txtNumberofBoxesRec" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rv_boxesreceived" runat="server" ControlToValidate="txtNumberofBoxesRec"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,pleaseenternoofboxesreceived%>" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv_boxesreceived" runat="server" ValueToCompare="0" ControlToValidate="txtNumberofBoxesRec"
                                ErrorMessage="<%$ Resources:DeffinityRes,Receivedboxesshouldbegraterthanzero%>" Operator="GreaterThan" Type="Integer" Display="Dynamic"
                                 ValidationGroup="d">*</asp:CompareValidator>
                            <asp:CompareValidator ID="cv_boxes" runat="server" ControlToCompare="txtNumberofBoxes"
                                ControlToValidate="txtNumberofBoxesRec" Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Receivedboxesshouldbelessthannoofboxes%>"
                                Operator="LessThanEqual" SetFocusOnError="True" Type="Integer" ValidationGroup="d">*</asp:CompareValidator>
                              <asp:CompareValidator ID="cv_recivedequal" runat="server" ControlToCompare="txtNumberofBoxes"
                                ControlToValidate="txtNumberofBoxesRec" Display="Dynamic" ErrorMessage="Please select Recived (Part) as Received boxes are less than no of boxes."
                                Operator="Equal" SetFocusOnError="True" Type="Integer" ValidationGroup="d">*</asp:CompareValidator>
            </div>
	</div>
</div>

                       <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.NumberofboxesCollected%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtCollectedBoxes" runat="server" SkinID="txt_90" Text="0" ClientIDMode="Static"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                TargetControlID="txtCollectedBoxes" ValidChars="0123456789">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="rv_collected" runat="server" ControlToValidate="txtCollectedBoxes"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,pleaseenternoofboxesCollected%>" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv_collectedcheck" runat="server" ValueToCompare="0" ControlToValidate="txtCollectedBoxes"
                                ErrorMessage="<%$ Resources:DeffinityRes,Collectedboxesshouldbegraterthanzero%>" Operator="GreaterThan" Type="Integer" Display="Dynamic"
                                 ValidationGroup="d">*</asp:CompareValidator>
                            <asp:CompareValidator ID="cv_collected" runat="server" ControlToCompare="txtNumberofBoxesRec"
                                ControlToValidate="txtCollectedBoxes" Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,CollectedboxesshouldbelessthannoofReceivedboxes%>"
                                Operator="LessThanEqual" SetFocusOnError="True" Type="Integer" ValidationGroup="d">*</asp:CompareValidator>
                            <asp:CompareValidator ID="cv_collectedequal" runat="server" ControlToCompare="txtNumberofBoxesRec"
                                ControlToValidate="txtCollectedBoxes" Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,PleaseselectCollectedPartasCollectedboxesarelessthannoofReceivedboxes%>"
                                Operator="Equal" SetFocusOnError="True" Type="Integer" ValidationGroup="d">*</asp:CompareValidator>
            </div>
	</div>
</div>

                       <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%>  </label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlOurSite" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdOurSite" runat="server" TargetControlID="ddlOurSite"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetOurSite" LoadingText="[Loading Site...]" />
                            <asp:RequiredFieldValidator ID="rv_oursite" runat="server" ControlToValidate="ddlOurSite"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>

                       <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.StorageLocation%> </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlStorageLocation" runat="server" SkinID="ddl_90" ClientIDMode="Static">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="ccdLocation" runat="server" TargetControlID="ddlStorageLocation"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetStorageLocation" LoadingText="[Loading location...]" />
                            <asp:RequiredFieldValidator ID="rv_storagelocation" runat="server" ControlToValidate="ddlStorageLocation"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectstoragelocation%>" InitialValue="0"
                                SetFocusOnError="True" ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
                  </div>


                
                  
              <div class="col-md-6">
                  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DateReceived%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtDateReceived" runat="server" SkinID="Date" ClientIDMode="Static"
                                            ValidationGroup="d"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rv_datereceived" runat="server" ControlToValidate="txtDateReceived"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterreceiveddate%>" SetFocusOnError="True"
                                            ValidationGroup="d" ClientIDMode="Static">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cv_datereceived" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterinadateformat%>"
                                            ControlToValidate="txtDateReceived" ValidationGroup="d" Type="Date" Operator="DataTypeCheck"
                                            Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                        <asp:CompareValidator ID="cv_datecheck" runat="server" ControlToValidate="txtDateReceived"
                                            ValidationGroup="d" ErrorMessage="%$ Resources:DeffinityRes,DateReceivedshouldrelatetotodaysdatenotadateinthefuture%>"
                                            Operator="LessThanEqual" Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True"> </asp:CompareValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="txtDateReceived" ValidChars="0123456789/">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:Label ID="imgDateReceived" runat="server" SkinID="Calender" ClientIDMode="Static" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                             PopupButtonID="imgDateReceived" TargetControlID="txtDateReceived">
                                        </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
                  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DaysinStorage%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtDaysinStorage" runat="server" SkinID="txt_90" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rv_daysinstorage" runat="server" ControlToValidate="txtDaysinStorage"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdaysinstorage%>" SetFocusOnError="True"
                                            ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
                  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Chargeabledate%> </label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtChargeableDate" runat="server" SkinID="txt_90" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rv_chargeabledate" runat="server" ControlToValidate="txtChargeableDate"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterchargeabledate%>" SetFocusOnError="True"
                                            ValidationGroup="d">*</asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                            TargetControlID="txtChargeableDate" ValidChars="0123456789/">
                                        </ajaxToolkit:FilteredTextBoxExtender>
            </div>
	</div>
</div>
                  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.TotalCost%>    </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtTotalCost" runat="server" SkinID="txt_90" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            TargetControlID="txtTotalCost" ValidChars="0123456789.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rv_totalcost" runat="server" ControlToValidate="txtTotalCost"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentertotalcost%>" SetFocusOnError="True"
                                            ValidationGroup="d">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
                  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StoragePeriod%> </label>
           <div class="col-sm-8 form-inline">
                <%= Resources.DeffinityRes. From%>    
                                                    <asp:TextBox ID="txtFrom" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                                                         PopupButtonID="imgDatefrom" TargetControlID="txtFrom">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        TargetControlID="txtFrom" ValidChars="0123456789/">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:Label ID="imgDatefrom" runat="server" SkinID="Calender" ClientIDMode="Static" />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                   ControlToValidate="txtFrom" Display="Dynamic" 
                   ErrorMessage="Please enter from storage period" SetFocusOnError="True" 
                   ValidationGroup="d">*</asp:RequiredFieldValidator>--%>
                                                    <asp:CompareValidator ID="cv_fromdate" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterinadateformat%>"
                                                        ControlToValidate="txtFrom" ValidationGroup="d" Type="Date" Operator="DataTypeCheck"
                                                        Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>

                <%= Resources.DeffinityRes.To%>   
                                                    <asp:TextBox ID="txtTo" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
                                                         PopupButtonID="imgDateto" TargetControlID="txtTo">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        TargetControlID="txtTo" ValidChars="0123456789/">
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                    <asp:Label ID="imgDateto" runat="server" SkinID="Calender" ClientIDMode="Static" />
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                       ControlToValidate="txtTo" Display="Dynamic" 
                       ErrorMessage="Please enter to storage period" SetFocusOnError="True" 
                       ValidationGroup="d">*</asp:RequiredFieldValidator>--%>
                                                    <asp:CompareValidator ID="cv_todate" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterinadateformat%>"
                                                        ControlToValidate="txtTo" ValidationGroup="d" Type="Date" Operator="DataTypeCheck"
                                                        Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="cv_datecompare" runat="server" ControlToValidate="txtTo"
                                                        ValidationGroup="d" ErrorMessage="<%$ Resources:DeffinityRes,ToDateshouldbegreaterthanorequaltoFromdate%>"
                                                        Operator="GreaterThanEqual" Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True"
                                                        ControlToCompare="txtFrom"></asp:CompareValidator>

                <asp:Label ID="lblPeriod" runat="server" Text="Period Cost" Visible="false"></asp:Label>
                <asp:TextBox ID="txtPeriodCost" runat="server" Width="200px" ClientIDMode="Static"
                                            Visible="false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                            TargetControlID="txtPeriodCost" ValidChars="0123456789.">
                                        </ajaxToolkit:FilteredTextBoxExtender>
            </div>
	</div>
</div>
            </div>
               
</div>



              </div>
        </div>
    
    
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Uploadfiles%>   </label>
           <div class="col-sm-8">
                <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" BorderStyle="None"
                                ScrollBars="Auto">
                                <asp:FileUpload ID="DocumentsUploadnew" runat="server" maxlength="5" class="multi" ClientIDMode="Static" />
                            </asp:Panel>
                            &nbsp;&nbsp;
                            <asp:Button ID="ImgDeskImageUpload" runat="server" SkinID="btnUpload" OnClick="ImgDeskImageUpload_Click"
                                ValidationGroup="d" ClientIDMode="Static" Visible="false" />
            </div>
	</div>
</div>

    <asp:GridView ID="GvDocuments" runat="server" Width="100%" EmptyDataText="" OnRowCommand="GvDocuments_RowCommand"
                                DataKeyNames="ID,ContentType,FileName">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,UploadedDocuments%>">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                                runat="server" CommandName="Download"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImgDel" runat="server" CausesValidation="false" SkinID="BtnLinkDelete"
                                                CommandArgument='<%#Bind("DocumentID")%>' CommandName="DeleteFile" OnClientClick="return confirm('Do you want to delete this Document?');"
                                                ToolTip="delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

    <div class="form-group row">
          <div class="col-md-12">
               <span style="margin-right: 80px">
                                <asp:Label ID="lbl_loading" runat="server" Text="Loading..." ClientIDMode="Static"
                                    Font-Bold="true"></asp:Label></span>
                            <div id="div_buttons">
                                <asp:Button ID="btnSave" runat="server" AlternateText="Save" SkinID="btnSubmit"
                                    OnClick="btnSave_Click" ValidationGroup="d" ClientIDMode="Static" />&nbsp
                                <asp:Button ID="btnCancel" runat="server" AlternateText="Cancel" SkinID="btnCancel"
                                    OnClick="btnCancel_Click" />
                            </div>
	</div>
</div>
     <asp:Panel ID="pnlNotes" runat="server" Visible="false">
                    <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                       <%= Resources.DeffinityRes.Notes%>      </div>
                    <uc3:Notes ID="Notes1" runat="server" />
                </asp:Panel>
                <%-- <div> <hi:History ID="history1" runat="server" />     </div>--%>
                <div>
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
                        PopupControlID="pnlPopup" TargetControlID="btnPopupDum" BackgroundCssClass="modalBackground"
                        DynamicServicePath="~/WF/DC/webservices/DCServices.asmx" DynamicServiceMethod="GetDeliveryDefaults_Notice"
                        DynamicControlID="lblnotice">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Button ID="btnPopupDum" runat="server" Style="display: none" />
                    <asp:Panel ID="pnlPopup" runat="server" BorderStyle="Double" BackColor="White" BorderColor="LightSteelBlue"
                        Width="400px" Height="350px" Style="display: none">
                        <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                            <%= Resources.DeffinityRes. Message%> 
                        </div>
                        <asp:Panel ID="pop_Data" runat="server" Width="400px" Height="270px" ScrollBars="Vertical"
                            Style="overflow: auto;">
                            <label id="lblnotice" runat="server">
                            </label>
                        </asp:Panel>
                        <br />
                        <div style="text-align: center;">
                            <asp:Button ID="btnHide" runat="server" Text="Ok" OnClientClick="return HideNoticePopup()"
                                Width="75px" />
                        </div>
                    </asp:Panel>
                </div>
                <div>
                    <iframe id="iframe1" runat="server" width="100%" height="370px" marginwidth="0" marginheight="0"
                        scrolling="no" frameborder="0"></iframe>
                </div>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
    <script type="text/javascript">
        sideMenuActive('<%= Resources.DeffinityRes.Projects%>');
</script>
</asp:Content>

