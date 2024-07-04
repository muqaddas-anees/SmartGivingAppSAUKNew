 <%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Inventory" Codebehind="Inventory.ascx.cs" %>
 
  <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                <ProgressTemplate>
                    <asp:Label ID="imgLoading6" runat="server" SkinID="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>


        <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.0/themes/smoothness/jquery-ui.css">
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.0/jquery-ui.min.js"></script>

 <%--       <script src="../../../ui/jquery-ui.js"></script>
        <link rel="stylesheet" href="../../../ui/jquery-ui.css" />--%>

      <style type="text/css">
    .lab {
        width: 250px;
        float: left;
        margin-right: 5px;
    }
    .FieldCls {
         width: 250px;
         display:block;
    }
    .TxtCount {
        display:none;
    }
    .auto-style1 {
        width: 10px;
    }
    .btn {
        margin-bottom:0px;
    }

         .ui-widget {
                font-family: Verdana,Arial,sans-serif;
                font-size: 0.8em;
            }

          /*.ui-widget {
              font-family: Verdana,Arial,sans-serif;
              font-size: 0.8em;
          }
              .ui-widget .ui-widget {
                  font-size: 1em;
              }
              .ui-widget input,
              .ui-widget select,
              .ui-widget textarea,
              .ui-widget button {
                  font-family: Verdana,Arial,sans-serif;
                  font-size: 1em;
              }*/


</style>

<script language="javascript" type="text/javascript">

            function BindBatchesInMainGrid(productid, Cid) {
                $('#TblBatchgrid' + productid).html("");
                var pleasewaitMsg = "Please wait while data is loaded...";
                $("#DivPleaseWaitMsgInBatch" + productid).html(pleasewaitMsg);
                $("#DivPleaseWaitMsgInBatch" + productid).show();
                $.ajax({
                    url: '/WF/Inventory/Webservices/InventoryMgr.asmx/DataTableToJsonWithJsonNet?productid=' + productid + '&&Cid=' + Cid,
                    type: "POST",
                    data: "{'productid': '" + productid + "','Cid': '" + Cid + "'}",
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    async: true,
                    success: function (data) {
                        debugger;
                        if (data.d != "[]") {
                            var newData = jQuery.parseJSON(data.d);

                            var trHTML = '';
                            var Headerstext = Object.keys(newData[0]);
                            trHTML += '<tr style="background-color:silver;border:thin;color:white;">';
                            for (var i = 0; i < Headerstext.length; i++) {
                                if (Headerstext[i] != 'BatchID') {
                                    trHTML += '<th>' + Headerstext[i] + '</th>';
                                }
                            }
                            trHTML += ' </tr>';

                            $.each(newData, function (i, item) {
                                trHTML += '<tr>';
                                //if (item[Headerstext[4]] != '0') {
                                for (var j = 0; j < Headerstext.length; j++) {
                                    if (Headerstext[j] != 'BatchID') {
                                        if (item[Headerstext[j]] != null) {
                                            trHTML += '<td>' + item[Headerstext[j]] + '</td>';
                                        }
                                        else {
                                            trHTML += '<td></td>';
                                        }
                                    }
                                }
                                trHTML += ' </tr>';
                            });
                            $('#TblBatchgrid' + productid).append(trHTML);
                            $("#DivPleaseWaitMsgInBatch" + productid).hide();
                        }
                        else {
                            $('#TblBatchgrid' + productid).append("<tr><td>No data<td></tr>");
                            $("#DivPleaseWaitMsgInBatch" + productid).hide();
                        }
                    }
                });
            }

            function BindUsageInMainGrid(productid, Cid) {
                $('#TblUsagegrid' + productid).html("");
                $("#DivPleaseWaitMsgInUsage" + productid).html("Please wait while data is loaded...");
                $("#DivPleaseWaitMsgInUsage" + productid).show();
                $.ajax({
                    url: '/WF/Inventory/Webservices/InventoryMgr.asmx/DataTableToJsonWithJsonNetForUsageGrid?productid=' + productid + '&&Cid=' + Cid,
                    type: "POST",
                    data: "{'productid': '" + productid + "','Cid': '" + Cid + "'}",
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    async: true,
                    success: function (data) {
                        debugger;
                        if (data.d != "[]") {
                            var newData = jQuery.parseJSON(data.d);
                            var trHTML = '';

                            var Headerstext = Object.keys(newData[0]);

                            trHTML += '<tr style="background-color:silver;border:thin;color:white;">';
                            for (var i = 0; i < Headerstext.length; i++) {
                                if (Headerstext[i] != '_id') {
                                    trHTML += '<th>' + Headerstext[i] + '</th>';
                                }
                            }
                            trHTML += ' </tr>';

                            $.each(newData, function (i, item) {
                                trHTML += '<tr>';
                                for (var j = 0; j < Headerstext.length; j++) {
                                    if (Headerstext[j] != '_id') {
                                        if (item[Headerstext[j]] != null) {
                                            trHTML += '<td>' + item[Headerstext[j]] + '</td>';
                                        }
                                        else {
                                            trHTML += '<td></td>';
                                        }
                                    }
                                }
                                trHTML += ' </tr>';
                            });
                            $('#TblUsagegrid' + productid).append(trHTML);
                            $("#DivPleaseWaitMsgInUsage" + productid).hide();
                        }
                        else {
                            $('#TblUsagegrid' + productid).append("<tr><td>No data<td></tr>");
                            $("#DivPleaseWaitMsgInUsage" + productid).hide();
                        }
                    }
                });
            }
            

            function expandcollapseUsage(obj, productid, Cid, row) {
            
                var div = document.getElementById(obj);
                var img = document.getElementById('img' + obj);
              //   Close_All(obj);
             
                if (div.style.display == "none") {
                    div.style.display = "block";
                    BindUsageInMainGrid(productid, Cid);
                    if (row == 'alt') {
                        img.src = "minus.gif";
                    }
                    else {
                        img.src = "minus.gif";
                    }
                    img.alt = "Close to view other BOM";
                }
                else {
                    div.style.display = "none";
                    if (row == 'alt') {
                        img.src = "plus.gif";
                    }
                    else {
                        img.src = "plus.gif";
                    }
                    img.alt = "Expand to show BOM";
                }
            }

            function expandcollapse(obj, productid, Cid, row) {
                
                var div = document.getElementById(obj);
                var img = document.getElementById('img' + obj);
              
               // Close_All(obj);
              
                if (div.style.display == "none") {
                   
                    div.style.display = "block";
                    BindBatchesInMainGrid(productid, Cid);
                    if (row == 'alt') {
                        img.src = "minus.gif";
                    }
                    else {
                        img.src = "minus.gif";
                    }
                    img.alt = "Close to view other BOM";
                }
                else {
                    
                    div.style.display = "none";
                    if (row == 'alt') {
                        img.src = "plus.gif";
                    }
                    else {
                        img.src = "plus.gif";
                    }
                    img.alt = "Expand to show BOM";
                }
            }
            function Close_All(obj) {
                var divOld = document.getElementById(obj);
                var getAttribute;
                var str = '';
                var Grid_Table = document.getElementById('<%= grdInventory.ClientID %>');
                for (var row = 1; row < Grid_Table.rows.length - 1; row++) {
                    //expandcollapse(Grid_Table, row)

                    var imageColapsenm;
                    imageColapsenm = Grid_Table.rows[row].cells[0].firstChild.id;
                    if (imageColapsenm != 'imageColapse') {
                        //alert(imageColapsenm);
                        if (imageColapsenm != null) {

                            var div = document.getElementById(imageColapsenm);
                            var img = document.getElementById('img' + imageColapsenm);
                            if (divOld != div) {
                                div.style.display = "none";
                                img.src = "plus.gif";
                                img.alt = "Expand to show Questionarrie";
                            }
                        }
                    }
                }
                return false;
            }
    </script>

        <script type="text/javascript">
            function pageLoad(sender, args) {
                debugger;
                //$("#PaneltblDeployedDate").hide();
                 //$("#hlinkExecption").hide();
                //$('#button').click(function (e) {
                //    debugger;
                //    $("#PaneltblDeployedDate").toggle();
                //});
                //$('#UploadImg').click(function (e) {
                //    debugger;
                //    $("#hlinkExecption").toggle();
                //});
            };
            function CheckOne(obj) {
                var grid = obj.parentNode.parentNode.parentNode;
                var inputs = grid.getElementsByTagName("input");
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == "checkbox") {
                        if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                            inputs[i].checked = false;
                        }
                    }
                }
            }
            $(document).ready(function () {
                debugger;
                $("#PaneltblDeployedDate").hide();
                //$("#hlinkExecption").hide();
                //$("body").on("click", "#reportspan", function () {
                //    $("#PaneltblDeployedDate").toggle();
                //    return false;
                //});

                $("body").on("click", "#UploadImg", function () {
                    //$("#hlinkExecption").toggle();
                    debugger;
                });
            });
</script>
<script>
                var SuccessMessageDiv = "<div class='alert alert-success'></div>";
                var ErrorMessageDiv = "<div class='alert alert-danger'></div>";

                $(document).ready(function () {
                    InIEvent();
                });
                function GetInventoryNames() {
                    $('#<%=TxtproductInNewItem.ClientID%>').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: "InventoryManagerPage.aspx/GetAllInventoryNames",
                                data: "{ 'pre':'" + request.term + "'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return { value: item }
                                    }))
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    alert(textStatus);
                                }
                            });
                        }
                    });
                };
                function InIEvent() {

                    $("#reportspan").click(function () {
                        // $("#PaneltblDeployedDate").toggle();
                        if ($('#PaneltblDeployedDate').css('display') == 'none') {
                            $('#PaneltblDeployedDate').show();
                        } else {
                            $('#PaneltblDeployedDate').hide();
                        }
                        return false;
                    });

                    //lblInventoryAddMsg
                    var lblColor = $('#lblInventoryAddMsg').css('color');
                    $("#lblInventoryAddMsg").css("color", 'white');
                    if (lblColor == "rgb(0, 128, 0)") {
                        $('#lblInventoryAddMsg').wrap(SuccessMessageDiv);
                    }
                    else if (lblColor == "rgb(255, 0, 0)") {
                        $('#lblInventoryAddMsg').wrap(ErrorMessageDiv);
                    }
                    //lblUploadErrorMsg
                    var lblColor = $('#lblUploadErrorMsg').css('color');
                    $("#lblUploadErrorMsg").css("color", 'white');
                    if (lblColor == "rgb(0, 128, 0)") {
                        $('#lblUploadErrorMsg').wrap(SuccessMessageDiv);
                    }
                    else if (lblColor == "rgb(255, 0, 0)") {
                        $('#lblUploadErrorMsg').wrap(ErrorMessageDiv);
                    }
                    //LblListviewMsg
                    var lblColor = $('#LblListviewMsg').css('color');
                    $("#LblListviewMsg").css("color", 'white');
                    if (lblColor == "rgb(0, 128, 0)") {
                        $('#LblListviewMsg').wrap(SuccessMessageDiv);
                    }
                    else if (lblColor == "rgb(255, 0, 0)") {
                        $('#LblListviewMsg').wrap(ErrorMessageDiv);
                    }
                    //lblTransferMsg
                    var lblColor = $('#lblTransferMsg').css('color');
                    $("#lblTransferMsg").css("color", 'white');
                    if (lblColor == "rgb(0, 128, 0)") {
                        $('#lblTransferMsg').wrap(SuccessMessageDiv);
                    }
                    else if (lblColor == "rgb(255, 0, 0)") {
                        $('#lblTransferMsg').wrap(ErrorMessageDiv);
                    }
                    //lblmsgInpopup
                    var lblColor = $('#lblmsgInpopup').css('color');
                    $("#lblmsgInpopup").css("color", 'white');
                    if (lblColor == "rgb(0, 128, 0)") {
                        $('#lblmsgInpopup').wrap(SuccessMessageDiv);
                    }
                    else if (lblColor == "rgb(255, 0, 0)") {
                        $('#lblmsgInpopup').wrap(ErrorMessageDiv);
                    }




                }
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GetInventoryNames);
</script>
        <asp:HiddenField ID="hBatchoption" runat="server" />


 <%--   <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            $("#panelQuickSearch").hide();
            $("#lnkbtn").click(function () {
                event.preventDefault();
                $("#panelQuickSearch").toggle();s
                return false;
            });
        });
    </script>--%>
        <div class="form-group">
             <div class="col-md-12" style="float:right;text-align:right;">
                   <asp:Label ID="UploadImg" Text="Upload" CssClass="btn btn-secondary"  runat="server" ClientIDMode="Static"></asp:Label>
                   <asp:LinkButton ID="hlinkExecption" runat="server" Text="View Exception Reports" OnClick="hlinkExecption_Click" 
                                                                      ClientIDMode="Static" CssClass="btn btn-secondary"></asp:LinkButton>
            </div>
        </div>
       
        <asp:Panel ID="PanelCsv" runat="server" Width="95%">
            <div class="form-group">
                   <div class="col-md-12">
                         <strong>    <%= Resources.DeffinityRes.InventoryUpload%> </strong> 
                         <hr class="no-top-margin" />
                  </div>
            </div>
          <div>
            <%--  <asp:Label ID="lblCustomerErrorMsg" runat="server" ForeColor="red" Width="100%" ></asp:Label>--%>
         
<div class="row">
          <div class="col-md-12">
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="up" />
            <asp:Label ID="lblUploadErrorMsg" ClientIDMode="Static" runat="server"></asp:Label>
            <asp:Label ID="lblCustomerMsg" runat="server" Width="100%" BackColor="#F9F9F9"></asp:Label>
            <asp:Label ID="lblCustomerSuccessMsg" runat="server" Width="100%" ForeColor="Green"></asp:Label>
            <asp:Label ID="lblCustomerErrorMsg" runat="server" Width="100%" ForeColor="Red"></asp:Label>
	</div>
</div>
          
        
            <div class="form-group">
                 <div class="col-md-2">
                     <label class="col-sm-12 control-label"> Upload inventory use</label>
                 </div>
                 <div class="col-md-3">
                     <asp:FileUpload ID="fileUpload1" CssClass="col-sm-12 control-label" runat="server" />
                 </div>
                 <div class="col-md-2">
                      <asp:Button ID="ImageButton1" CssClass="btn btn-secondary" SkinID="btnUpload"
                                                  runat="server" OnClick="ImageButton1_Click" ValidationGroup="up" />
                 </div>
            </div>
           </div>
        </asp:Panel>
        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
            TargetControlID="PanelCsv" ExpandControlID="UploadImg" CollapseControlID="UploadImg"
            TextLabelID="Lbl1" 
           Collapsed="true" SuppressPostBack="true"></ajaxToolkit:CollapsiblePanelExtender>

        <asp:Label ID="lblDummy" runat="server"></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopUpUploadList" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="lblDummy" PopupControlID="pnlSearch">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Style="display: none;"
            Width="600px" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue"
            ScrollBars="Auto">
            <div style="float: right">
               </div>
            
<div class="row">
          <div class="col-md-10">
 <strong> <%= Resources.DeffinityRes.InventoryUpload%> </strong> 
<hr class="no-top-margin" />
	</div>
      <div class="col-md-2">
           <asp:LinkButton ID="ImageButton3" runat="server" SkinID="BtnLinkCancel" ToolTip="Close"
                    OnClick="ImageButton3_Click" />
          </div>
</div>
           
<div class="row">
          <div class="col-md-12">
               <asp:Label ID="lblUploadExceptionMsg" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
           
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Label ID="imgLoading_1" runat="server" SkinID="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
          
              <asp:GridView ID="GvUploadList" runat="server" OnRowCommand="GvUploadList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Use %> ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUse" runat="server" Text='<%#Bind("Use") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Bind("InventoryID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUseID" runat="server" Text='<%#Bind("UseID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Category" HeaderText="<%$ Resources:DeffinityRes,Category%>" />
                                <asp:BoundField DataField="SubCategory" HeaderText="<%$ Resources:DeffinityRes,SubCategory%>" />
                                <asp:BoundField DataField="Product" HeaderText="<%$ Resources:DeffinityRes,Product%>" />
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,OpeningQty%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpeningQty" runat="server" Text='<%#Bind("OpeningQty") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,NumberDeployed%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumberDeployed" runat="server" Text='<%#Bind("NumberDeployed") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnConfirm" runat="server" Text="Commit to Database" CommandArgument='<%#Bind("InventoryID") %>'
                                            CommandName="Commit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   
        </asp:Panel>

        <asp:Panel ID="panelQuickSearch" runat="server">
<div class="row">
          <div class="col-md-12">
 <strong> <%= Resources.DeffinityRes.Search%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>

            <div class="row">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="export" />
                 </div>
                </div>
            

                <div class="form-group">
                         <div class="col-md-4" style="display:none;">
                               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                             	<div class="col-sm-8">
                                       <asp:DropDownList ID="ddlcustomerInSearch" runat="server" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlcustomerInSearch_SelectedIndexChanged" SkinID="ddl_80"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="reqSearchInCustomer" runat="server" ControlToValidate="ddlcustomerInSearch" InitialValue="0"
                                                                 ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcustomer%>" ValidationGroup="export">*</asp:RequiredFieldValidator>
                             	</div>
                         </div>
                        <div class="col-md-4">
                               <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Site%> </label>
                             	<div class="col-sm-8">
                                      <asp:DropDownList ID="ddlsiteInSearch" SkinID="ddl_80" runat="server"></asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="ReqddlsiteInSearch" runat="server" ControlToValidate="ddlsiteInSearch" InitialValue="0"
                                                                 ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>" ValidationGroup="export">*</asp:RequiredFieldValidator>
                             	</div>
                         </div>
                        <div class="col-md-4">
                               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                             	<div class="col-sm-8">
                                     <asp:DropDownList ID="ddlcategoryInSearch" runat="server" AutoPostBack="true" SkinID="ddl_80"
                                                                          OnSelectedIndexChanged="ddlcategoryInSearch_SelectedIndexChanged"></asp:DropDownList>
                             	</div>
                         </div>
                       <div class="col-md-4">
                               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SubCategory%></label>
                             	<div class="col-sm-8">
                                      <asp:DropDownList ID="ddlsubcategoryInSearch" SkinID="ddl_80" runat="server"></asp:DropDownList>
                             	</div>
                         </div>
                   </div>
                   <div class="form-group">
                      
                      <div class="col-md-4">
                               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Text%></label>
                             	<div class="col-sm-8">
                                     <asp:TextBox ID="txtText" runat="server" SkinID="txt_90"></asp:TextBox>
                             	</div>
                         </div>
                         <div class="col-md-4">
                              <label class="col-sm-12 control-label"></label>
                        </div>
                      <div class="col-md-4">
                             <asp:Button ID="btnsearch" runat="server" Text="Search" SkinID="btnDefault" OnClick="btnsearch_Click" />
                             <asp:Button ID="reportspan" ClientIDMode="Static" Text="Report" runat="server" SkinID="btnDefault" ></asp:Button>
                             
                         </div>
                 </div>
                   <div class="form-group" id="PaneltblDeployedDate" style="display:none;">
                      <div class="col-md-4">
                            <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FromDeployedDate%></label>
                            <div class="col-sm-8 form-inline">
                                 <asp:TextBox ID="txtFromDeployedDate" runat="server" SkinID="Date"></asp:TextBox>
                                 <asp:Label ID="imgFromDeployedDate" runat="server" SkinID="Calender" ClientIDMode="Static"></asp:Label>
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDeployedDate"
                                            CssClass="MyCalendar" PopupButtonID="imgFromDeployedDate"></ajaxToolkit:CalendarExtender>
                                 <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDeployedDate"
                                           Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidfromdeployeddate%>" Operator="DataTypeCheck"
                                           Type="Date" ValidationGroup="export"></asp:CompareValidator>
                            </div>
                      </div>
                      <div class="col-md-4">
                            <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ToDeployedDate%></label>
                            <div class="col-sm-8 form-inline">
                                 <asp:TextBox ID="txtToDeployedDate" runat="server" SkinID="Date"></asp:TextBox>
                                 <asp:Label ID="imgToDeployedDate" runat="server" SkinID="Calender" ClientIDMode="Static"></asp:Label>
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToDeployedDate"
                                           CssClass="MyCalendar" PopupButtonID="imgToDeployedDate"></ajaxToolkit:CalendarExtender>
                                  <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtToDeployedDate"
                                          Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidtodeployeddate%>" Operator="DataTypeCheck"
                                          Type="Date" ValidationGroup="export"></asp:CompareValidator>
                            </div>
                      </div>
                      <div class="col-md-4">
                             <asp:LinkButton ID="imgbtnExportToExcel" runat="server" SkinID="BtnLinkExcel"
                                 ToolTip="<%$ Resources:DeffinityRes,ExportToExcel%>"
                                OnClick="imgbtnExportToExcel_Click" ValidationGroup="export"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="imgbtnExportToPdf" runat="server"
                                 ToolTip="<%$ Resources:DeffinityRes,ExportToPdf%>" SkinID="BtnLinkPdf"
                                OnClick="imgbtnExportToPdf_Click" ValidationGroup="export"></asp:LinkButton>
                      </div>
                 </div>
	
          
        </asp:Panel> 
     <%--   pnlAddProduct--%>
        <asp:Panel ID="pnlAddProduct" runat="server" Visible="false">
            
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.AddAmendProduct%></strong> 
<hr class="no-top-margin" />
	</div>
</div>
            
<div class="row well">
          <div class="col-md-12">
                <asp:UpdateProgress ID="uProgress" runat="server">
                <ProgressTemplate>
                    <asp:Label ID="imgloading_0" runat="server" SkinID="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>

                <div class="form-group">
                   <div class="col-md-12">
            <asp:ValidationSummary ID="InvntValidation" runat="server" Visible="false" ValidationGroup="AddInvnt" />
            <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Green" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblMsgDepartment" runat="server" ForeColor="Green" Visible="false"></asp:Label>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="true" ValidationGroup="Group1" />
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" Visible="true" ValidationGroup="Group2" />
                       </div>
               </div>

                <asp:Panel ID="PnlNewItem" runat="server" Visible="false">
                  <div class="form-group">
                         <div class="col-md-4">
                             <div class="form-group">
                              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%>  :</label>
                              <div class="col-sm-8">
                                 <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" SkinID="ddl_80"
                                                             AutoPostBack="true"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlCustomer"
                                                             InitialValue="0" ValidationGroup="Group2" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcustomer%>">*</asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCustomer"
                                                             InitialValue="0" ValidationGroup="up" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcustomer%>">*</asp:RequiredFieldValidator>
                           </div>
                                 </div>
                             <div class="form-group">
                           <label class="col-sm-4 control-label"> <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label></label>
                           <div class="col-sm-8">
                                <asp:DropDownList ID="ddllocation" runat="server" SkinID="ddl_80"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged"></asp:DropDownList>
                           </div>
                           </div>
                             <div class="form-group">
                           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Category%> :</label>
                           <div class="col-sm-8 form-inline">
                               <asp:Panel ID="pnlcategory" runat="server" ClientIDMode="Static" CssClass="form-inline">
                                     <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" SkinID="ddl_50"
                                                                      OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategory"
                                InitialValue="0" ValidationGroup="Group2" ErrorMessage="Please select category">*</asp:RequiredFieldValidator>--%>
                            <asp:LinkButton ID="btnaddcategory" OnClick="btnaddcategory_Click" runat="server"
                                                                SkinID="BtnLinkAdd" CausesValidation="False"></asp:LinkButton>
                            <asp:LinkButton ID="btn_CategoryEdit" runat="server" ValidationGroup="Edit_catelog" SkinID="BtnLinkEdit"
                                                                 OnClick="btn_CategoryEdit_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete category,associated sub category and item(s)?');"
                                SkinID="BtnLinkDelete" OnClick="btnDeleteCategory_Click" />
                        </asp:Panel>

                               <asp:Panel ID="pnladdcategory" runat="server" Visible="false">
                            <asp:TextBox ID="txtAddCategory" runat="server" ValidationGroup="cat1" SkinID="txt_70"></asp:TextBox>
                            <asp:LinkButton ID="btnSaveCategory" runat="server" ToolTip="<%$ Resources:DeffinityRes,AddCategory%>" ValidationGroup="cat1"
                                OnClick="btnSaveCategory_Click" SkinID="BtnLinkUpdate"></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelCategory" runat="server" ToolTip="<%$ Resources:DeffinityRes,Cancel%>" OnClick="btnCancelCategory_Click"
                                CausesValidation="False" SkinID="BtnLinkCancel"></asp:LinkButton>
                            <div>
                                <asp:RequiredFieldValidator runat="server" ID="ReqCatname" ControlToValidate="txtAddCategory"
                                    SetFocusOnError="true" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterCategory%>" ForeColor="Red" ValidationGroup="cat1"></asp:RequiredFieldValidator></div>
                        </asp:Panel>
                                 <asp:HiddenField ID="HID_Category" runat="server"></asp:HiddenField>
                           </div>
                                 </div>
                       </div>
                         <div class="col-md-4">
                                <div class="form-group">
                           <label class="col-sm-4 control-label"><asp:Label ID="lblSite" runat="server" Text="Site:"></asp:Label></label>
                           <div class="col-sm-8">
                                  <asp:DropDownList ID="ddlSite" runat="server" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged" SkinID="ddl_80"
                                                                  AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSite"
                                                                  InitialValue="0" ValidationGroup="Group2" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>">*</asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSite"
                                                                   InitialValue="0" ValidationGroup="up" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>">*</asp:RequiredFieldValidator>
                           </div>
                                    </div>
                                <div class="form-group">
                           <label class="col-sm-4 control-label"><asp:Label ID="lblShelf" runat="server" Text="Shelf"></asp:Label></label>
                           <div class="col-sm-8">
                               <asp:DropDownList ID="ddlshelf" runat="server" SkinID="ddl_80"
                                                             AutoPostBack="true" OnSelectedIndexChanged="ddlshelf_SelectedIndexChanged1"></asp:DropDownList>
                           </div>
                                  </div>
                                <div class="form-group">
                           <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.SubCategory%> :</label>
                           <div class="col-sm-8 form-inline">
                                <asp:Panel ID="pnlsubcategory" runat="server">
                            <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" SkinID="ddl_50"
                                OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubCategory"
                                InitialValue="0" ValidationGroup="Group2" ErrorMessage="Please select subcategory">*</asp:RequiredFieldValidator>--%>
                            <asp:LinkButton ID="btnaddsubcategory" OnClick="btnaddsubcategory_Click" runat="server"
                                SkinID="BtnLinkAdd" ValidationGroup="cat0"></asp:LinkButton>
                            <asp:LinkButton ID="btn_editSubCategory" runat="server" ValidationGroup="Edit_subcatelog" SkinID="BtnLinkEdit"
                                                                OnClick="btn_editSubCategory_Click">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSubCategory" runat="server" OnClientClick="javascript:alert('Do you want to delete Sub category and associated item(s)?');"
                                OnClick="btnSubCategory_Click" SkinID="BtnLinkDelete" />
                        </asp:Panel>
                        <asp:Panel ID="pnladdsubcategory" runat="server" Visible="false">
                            <asp:TextBox ID="txtAddSubCategory" runat="server" ValidationGroup="Subcat1" SkinID="txt_60"></asp:TextBox>
                            <asp:LinkButton ID="btnSaveSubCategory" runat="server" ToolTip="<%$ Resources:DeffinityRes,AddSubCategory%>"
                                ValidationGroup="Subcat1" OnClick="btnSaveSubCategory_Click" SkinID="BtnLinkUpdate">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancelSubCategory" runat="server"
                                ToolTip="<%$ Resources:DeffinityRes,Cancel%>" OnClick="btnCancelSubCategory_Click"
                                SkinID="BtnLinkCancel"></asp:LinkButton>
                            <div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAddSubCategory"
                                    ErrorMessage="<%$ Resources:DeffinityRes,PleaseentersubCategory%>"
                                     ForeColor="Red" ValidationGroup="Subcat1"></asp:RequiredFieldValidator>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField ID="HID_SubCategory" runat="server"></asp:HiddenField>
                           </div>
                                   </div>
                       </div>
                         <div class="col-md-4">
                                <div class="form-group">
                           <label class="col-sm-4 control-label"><asp:Label ID="lblArea" Text="Area" runat="server"></asp:Label></label>
                           <div class="col-sm-8">
                                  <asp:DropDownList ID="ddlarea" runat="server" SkinID="ddl_80"
                                       AutoPostBack="true" OnSelectedIndexChanged="ddlarea_SelectedIndexChanged"></asp:DropDownList>
                           </div>
                                    </div>
                                <div class="form-group">
                           <label class="col-sm-4 control-label"><asp:Label ID="lblBin" Text="Bin" runat="server"></asp:Label></label>
                           <div class="col-sm-8 form-inline">
                                  <asp:DropDownList ID="ddlbin" runat="server" SkinID="ddl_60"></asp:DropDownList>
                                  <asp:LinkButton ID="ImageButtonbinAdd" runat="server" SkinID="BtnLinkAdd" OnClick="ImageButtonbinAdd_Click"></asp:LinkButton>
                                  <asp:LinkButton ID="ImageButtonBinEdit" runat="server" SkinID="BtnLinkEdit" OnClick="ImageButtonBinEdit_Click"></asp:LinkButton>
                                  
                                  <asp:Panel ID="paneladd" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none"
                                                                         ScrollBars="Auto" ClientIDMode="Static" Width="450px">
                                           <asp:UpdateProgress runat="server" ID="PaneladdNew2" DisplayAfter="10" AssociatedUpdatePanelID="panelupdate" ClientIDMode="Static">
                                                  <ProgressTemplate>
                                                      <asp:Label ID="image1" runat="server" SkinID="Loading"></asp:Label>
                                                  </ProgressTemplate>
                                              </asp:UpdateProgress>
                                 <asp:UpdatePanel ID="panelupdate" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                                      <ContentTemplate>
                                          <div class="panel panel-default">
                                              <div class="panel-heading">
                                                      <asp:Label ID="lbladd" runat="server"></asp:Label>
                                                       <div style="float:right;">
                                                          <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel"></asp:LinkButton>
                                                     </div>
                                              </div>
                                               <div class="panel-body">
                                                          <div class="form-group">
                                                                     <div class="col-md-12">
                                                          <asp:Label ID="lblerror1" EnableViewState="false" runat="server"></asp:Label>
                                                          <asp:ValidationSummary ID="catSummary" runat="server" ValidationGroup="popup" />
                                                          <asp:RequiredFieldValidator ID="rfvtxt" runat="server" ForeColor="Red" ControlToValidate="txtBinName" Display="None"
                                                                                     ErrorMessage="Please enter name" ValidationGroup="popup"></asp:RequiredFieldValidator>
                                                 </div>
                                                           </div>
                                                          <div class="form-group">
                                                <div class="col-md-3">
                                                       <label class="col-sm-12 control-label">   <%= Resources.DeffinityRes.Name%>  </label>
                                                 </div>
                                                <div class="col-md-6">
                                                    	<div class="col-sm-12">
                                                            <asp:TextBox ID="txtBinName" runat="server" ValidationGroup="popup"></asp:TextBox>
                                                         </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="col-sm-12">
                                                         <asp:HiddenField ID="hdForChecking" runat="server" />
                                                         <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="button deffinity medium"
                                                                                          ValidationGroup="popup" OnClick="btnsubmit_Click" />
                                                          <asp:Label ID="lblspace3" runat="server"></asp:Label>
                                                         <asp:Label ID="l1" runat="server"></asp:Label>
                                                         <ajaxToolkit:ModalPopupExtender ID="popupAdd" runat="server" BackgroundCssClass="modalBackground"
                                                             TargetControlID="l1" PopupControlID="paneladd" CancelControlID="lnkCancel"></ajaxToolkit:ModalPopupExtender>
                                                    </div>
                                                </div>
                                            </div>
                                               </div>
                                            </div>
                                      </ContentTemplate>
                                 </asp:UpdatePanel>
                          </asp:Panel>
                           </div>
                       </div>
                                <div class="form-group">
                           <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Product%> :</label>
                           <div class="col-sm-8 form-inline">
                                <asp:DropDownList ID="ddlProduct" runat="server" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged1" SkinID="ddl_70"
                                                                AutoPostBack="true"></asp:DropDownList>
                                 <asp:CheckBox ID="chkAddSCMCatalogue" runat="server" Checked="false" Visible="false" />
                                 <asp:LinkButton ID="imgAddInventory" runat="server" SkinID="BtnLinkAdd" OnClick="imgAddInventory_Click"
                                                                                       ToolTip="<%$ Resources:DeffinityRes,AddtoServiceCatalogue%>"></asp:LinkButton>
                           </div>
                                   </div>

                       </div>
                  </div>
                  </asp:Panel>
                <asp:Panel ID="pnlAddItems" runat="server" Visible="false">
                    <div class="form-group">
                             <div class="col-md-4">
                                 <div class="form-group">
                                 <label class="col-sm-4 control-label">
                                         <%= Resources.DeffinityRes.OpeningStock%>     
                                 </label>
                                 <div class="col-sm-8 form-inline">
                                       <asp:TextBox ID="txtStock" runat="server" SkinID="txt_50" ReadOnly="true"></asp:TextBox>
                                 </div>
                                 </div>
                                   <div class="form-group">
                                 <label class="col-sm-4 control-label">
                                       <%= Resources.DeffinityRes.Stocklevel%>     
                                 </label>
                                 <div class="col-sm-8 form-inline">
                                       <asp:TextBox ID="txtStockLevel" runat="server" SkinID="txt_50" ReadOnly="true"></asp:TextBox>
                                       <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtStockLevel"
                                               Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidStockLevel%>" Operator="DataTypeCheck"
                                                                           SetFocusOnError="True" Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                                 </div>
                                 </div>
                                 <div class="form-group">
                                 <label class="col-sm-4 control-label">
                                       <%= Resources.DeffinityRes.QTYInUse%> 
                                 </label>
                                 <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtQtyInUse" runat="server" Enabled="false" SkinID="txt_50"></asp:TextBox>
                                 </div>
                                 </div>
                                 <div class="form-group">
                                 <label class="col-sm-4 control-label">
                                        <%= Resources.DeffinityRes. QuantityonOrder%> 
                                 </label>
                                 <div class="col-sm-8 form-inline">
                                       <asp:TextBox ID="txtQntyOrder" runat="server" SkinID="txt_50"></asp:TextBox>
                                       <asp:CompareValidator ID="CompareValidator16" runat="server" ControlToValidate="txtQntyOrder"
                                                      Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidQuantity%>" Operator="DataTypeCheck"
                                                                           Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                                 </div>
                              </div>
                             <div class="form-group">
                                 <label class="col-sm-4 control-label">
                                        <%= Resources.DeffinityRes.Reorderlevel%>  
                                 </label>
                                 <div class="col-sm-8 form-inline">
                                         <asp:TextBox ID="txtReorderlevel" runat="server" SkinID="txt_50"></asp:TextBox>
                                          <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtReorderlevel"
                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidReorderLevel%>" Operator="DataTypeCheck"
                                SetFocusOnError="True" Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                                 </div>
                              </div>
                                  <div class="form-group">
                                <div class="col-sm-4">
                                        <asp:Button ID="btnaddnewstock" runat="server" CausesValidation="false" Text="Add Stock" />
                                </div>
                                      </div>
                          </div>
                             <div class="col-md-4">
                                 <div class="form-group">
                                 <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Barcode%> </label>
                                 <div class="col-sm-8">
                                     <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                                 </div>
                                     </div>
                                 <div class="form-group">
                                    <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Partnumber%>   </label>
                                 <div class="col-sm-8">
                                       <asp:TextBox ID="txtPartNumber" runat="server"></asp:TextBox>
                                 </div>
                                     </div>
                                 <div class="form-group">
                                    <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.Notes%>   </label>
                                 <div class="col-sm-8">
                                     <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine"></asp:TextBox>
                                 </div>
                                     </div>
                                 <div class="form-group">
                                    <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.UploadImage%>   </label>
                                 <div class="col-sm-8">
                                       <asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                            ControlToValidate="FileUploadMaterial" Display="None" ErrorMessage=""
                                                            ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                                                            ValidationGroup="Group2">File</asp:RegularExpressionValidator>                      
                                 </div>
                                     </div>
                                 <div class="form-group">
                                    <label class="col-sm-4 control-label"></label>
                                 <div class="col-sm-8">
                                        <asp:Image ID="ImgProfile" runat="server" Width="50" Height="70" />
                                 </div>
                                     </div>
                             </div>
                             <div class="col-md-4">
                                  <div class="form-group">
                                  <label class="col-sm-4 control-label">
                                       <%= Resources.DeffinityRes.Supplier%>    
                                  </label>
                                  <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList>
                            <asp:TextBox ID="txtSupplier" runat="server" Visible="False"></asp:TextBox>
                            <asp:LinkButton ID="btnAddSupplier" runat="server" Visible="false" OnClick="btnAddSupplier_Click"
                                SkinID="BtnLinkAdd"></asp:LinkButton>
                            <asp:LinkButton ID="btnCancelSupplier" runat="server" OnClick="btnCancelSupplier_Click"
                                Visible="False" SkinID="BtnLinkCancel"></asp:LinkButton>
                                   </div>
                                      </div>
                                  <div class="form-group">
                                  <label class="col-sm-4 control-label"> 
                                        <%= Resources.DeffinityRes.Manufacturer%>
                                  </label>
                                  <div class="col-sm-8 form-inline">
                                       <asp:DropDownList ID="ddlManufacturer" SkinID="ddl_80" runat="server"></asp:DropDownList>
                                       <asp:LinkButton ID="BtnAddManufacturer" runat="server" SkinID="BtnLinkAdd" ></asp:LinkButton>
                                       <ajaxToolkit:ModalPopupExtender ID="mdlManufacturer" runat="server" CancelControlID="btnManufacturerCancel"
                                                           BackgroundCssClass="modalBackground" TargetControlID="BtnAddManufacturer" PopupControlID="pnlManufacturer" />
                                      <asp:Panel ID="pnlManufacturer" runat="server" BackColor="White" Style="display: none"
                                                                          Width="500px" BorderStyle="Double" BorderColor="LightSteelBlue">
                                            <div class="panel panel-default">
                                              <div class="panel-heading">
                                                   <%= Resources.DeffinityRes. Manufacturer%>
                                                  <div style="float:right;">
                                                       <asp:LinkButton ID="btnManufacturerCancel" runat="server" SkinID="BtnLinkCancel"></asp:LinkButton>
                                                  </div>
                                              </div>
                                               <div class="panel-body">
                                                   <div class="form-group">
                                                         <div class="col-md-12">
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtManufacturer"
                                                                ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentermanufacturer%>" ForeColor="Red" Visible="true" ValidationGroup="Group_Department"></asp:RequiredFieldValidator>
                                                         </div>
                                                    </div>
                                                    <div class="form-group">
                                                         <div class="col-md-12">
                                                              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes. Manufacturer%></label>
                                                              <div class="col-md-8 form-inline">
                                                                     <asp:TextBox ID="txtManufacturer" runat="server" SkinID="txt_60"></asp:TextBox>
                                                                     <asp:HiddenField ID="H_Department" runat="server" Value="0" />
                                                                     <asp:Button ID="btnManufacturerInsert" runat="server" Text="Submit" OnClick="btnManufacturer_Click"
                                                                                                                                       ValidationGroup="Group_Department" />
                                                              </div>
                                                         </div>
                                                    </div>
                                              </div>
                                          </div>
                                      </asp:Panel>
                                   </div>
                                      </div>
                                  <div class="form-group">
                                  <label class="col-sm-4 control-label">
                                         <%= Resources.DeffinityRes.LeadTime%>    
                                  </label>
                                  <div class="col-sm-8">
                                         <asp:TextBox ID="txtLeadTime" runat="server"></asp:TextBox>
                                         <asp:CompareValidator ID="CompareValidator18" runat="server" ControlToValidate="txtLeadTime"
                                                       Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidLeadTime%>" Operator="DataTypeCheck"
                                                                             Type="Integer" ValidationGroup="Group2"></asp:CompareValidator>
                                   </div>
                                      </div>
                                  <div class="form-group">
                                  <label class="col-sm-4 control-label">
                                       <%= Resources.DeffinityRes.Dateordered%>    
                                  </label>
                                  <div class="col-sm-8 form-inline">
                                      <asp:TextBox ID="txtDateOrdered" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                      <asp:Label ID="imgOrderDate" runat="server" SkinID="Calender"></asp:Label>
                                      <ajaxToolkit:CalendarExtender ID="ClndrExtDateOrdered" runat="server" TargetControlID="txtDateOrdered"
                                                                 CssClass="MyCalendar" PopupButtonID="imgOrderDate"></ajaxToolkit:CalendarExtender>
                                      <asp:CompareValidator ID="CmpValOrderDate" runat="server" ControlToValidate="txtDateOrdered"
                                                                          Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidOrderdate%>" Operator="DataTypeCheck"
                                                                          Type="Date" ValidationGroup="Group2"></asp:CompareValidator>
                                   </div>
                                      </div>
                                  <div class="form-group">
                                  <label class="col-sm-4 control-label">
                                      <%= Resources.DeffinityRes.ExpectedArrivalDate%>
                                  </label>
                                  <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtArrivalDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                        <asp:Label ID="imgArrivalDate" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtArrivalDate"
                                                                    CssClass="MyCalendar" PopupButtonID="imgArrivalDate"></ajaxToolkit:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtArrivalDate"
                                                              ControlToCompare="txtDateOrdered" Operator="GreaterThanEqual" ErrorMessage="<%$ Resources:DeffinityRes,ArrivaldatemustbelaterthanOrderdate%>"
                                                                           Type="Date" ValidationGroup="Group2"></asp:CompareValidator>
                                   </div>
                                      </div>
                                  <div class="form-group">
                                  <label class="col-sm-4 control-label">
                                        <%= Resources.DeffinityRes.Description%>   
                                  </label>
                                  <div class="col-sm-8">
                                        <asp:TextBox ID="txtItemDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemDesc"
                                                     Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterDescription%>" SetFocusOnError="True"
                                                                            ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                   </div>
                                      </div>
                              </div>         
                    </div>
                      <div class="form-group">
                          <div class="col-md-12">
                              <asp:Panel ID="pnlCustomFields" runat="server">
                                  
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.CustomFieldsfor %> <asp:Label ID="lblCustomFiledCustomer" runat="server"></asp:Label></strong> 
<hr class="no-top-margin" />
	</div>
</div>
                                  
<div class="row">
          <div class="col-md-12">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatepanel_additional">
                                        <ProgressTemplate>
                                              <asp:Label ID="imgloading_2" runat="server" SkinID="Loading"></asp:Label>
                                        </ProgressTemplate>
                                </asp:UpdateProgress>
                <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                                    <ContentTemplate>
                                          <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
          </div>
</div>
                                  
                            </asp:Panel>
                          </div>
                      </div>
                       <div class="form-group">
                              <div class="col-md-12">
                                  <asp:HiddenField ID="hdnItemID" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnImageID" runat="server"></asp:HiddenField>
                            <asp:LinkButton ID="btnSaveMaterial" runat="server" OnClick="btnSaveMaterial_Click" CssClass="btn btn-secondary"
                                 Text="Save" ValidationGroup="Group2"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="imgbtnUpdateMaterial" runat="server" OnClick="imgbtnUpdateMaterial_Click" CssClass="btn btn-secondary"
                               Text="Update" ValidationGroup="Group2" Visible="False" />
                            <asp:LinkButton ID="ImageButton2" runat="server" AlternateText="Cancel" CausesValidation="False" CssClass="btn btn-secondary"
                                OnClick="btnCancelrow_Click" Text="Cancel"></asp:LinkButton>
                              </div>
                       </div>
        </asp:Panel>
	</div>
</div>
         
            </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mdlpopupHisstory" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="ImgHistory" PopupControlID="pnlHistory" CancelControlID="imgClose">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Label ID="ImgHistory" runat="server" />
        <asp:Panel ID="pnlHistory" runat="server" BackColor="White" Style="display: none;"
                        Width="1000px" Height="600px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Auto">
            <div class="panel panel-default">
                 <div class="panel-heading">
                       <%= Resources.DeffinityRes.History%> 
                       <div style="float: right">
                           <asp:LinkButton ID="imgClose" runat="server" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Close%>"></asp:LinkButton>
                       </div>
                 </div>
                 <div class="panel-body">
                  <asp:GridView ID="GvHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                            ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                            CellSpacing="2" EmptyDataText="No histoy found!" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="ItemDescription" HeaderText="<%$ Resources:DeffinityRes,Product%>" />
                                <asp:BoundField DataField="OpeningStock" HeaderText="<%$ Resources:DeffinityRes,OpeningStock%>" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="NoDeployed" HeaderText="<%$ Resources:DeffinityRes,QtyDeployed%>" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="UseCode" HeaderText="<%$ Resources:DeffinityRes,UseCode%>"  />
                                <asp:BoundField DataField="ReasonCode" HeaderText="<%$ Resources:DeffinityRes,ReasonCode%>" />
                                <asp:BoundField DataField="TransferQty" HeaderText="<%$ Resources:DeffinityRes,TransferQTY%>" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Notes" HeaderText="<%$ Resources:DeffinityRes,Notes%>" />
                                <asp:BoundField DataField="Qty" HeaderText="<%$ Resources:DeffinityRes,ClosingStock%>" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="ModifiedBy" HeaderText="<%$ Resources:DeffinityRes,ModifiedBy%>" />
                                <asp:BoundField DataField="MdofiedDate" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="<%$ Resources:DeffinityRes,ModifiedDate%>" />
                            </Columns>
                        </asp:GridView>
                 </div>
                </div>
        </asp:Panel>
         <ajaxToolkit:ModalPopupExtender ID="mdlpopupaddnewstock" runat="server" BackgroundCssClass="modalBackground"
              TargetControlID="btnaddnewstock" PopupControlID="paneladdstock"></ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel ID="paneladdstock" runat="server" BackColor="White"
                        Style="display:none" Width="1000px" Height="700px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Auto">
              <div class="panel panel-default">
                   <div class="panel-heading">
                       Adding New Stock "<asp:Label ID="lblproductName" runat="server"></asp:Label>"
                       <div style="float:right;">
                       <asp:LinkButton ID="imgBtnCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Close%>"
                                                                            OnClick="imgBtnCancel_Click"></asp:LinkButton>
                        </div>
                    </div>
                   <div class="panel-body">
                           <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="popupStockAdd">
                <ProgressTemplate>
                    <asp:Label ID="imgloading_4" runat="server" SkinID="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
                           <asp:UpdatePanel ID="popupStockAdd" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                                              <ContentTemplate>
                                                  <div>
                                                       <div class="col-md-12">
                                                           <asp:Label ID="lblmsgInpopup" EnableViewState="false" runat="server"></asp:Label>
                                                           <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="AddStock" />
                                                           <asp:ValidationSummary ID="VS2" runat="server" ValidationGroup="BinEdit" />
                                                       </div>
                                                  </div>
                                                  <div class="form-group">
                                                       
                                                        
                                                       <asp:RequiredFieldValidator ID="reqsite" InitialValue="0" runat="server" ControlToValidate="ddlsiteStockadd" ValidationGroup="AddStock"
                         ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>" Display="None"></asp:RequiredFieldValidator>

                    <%-- <asp:RequiredFieldValidator ID="reqarea" InitialValue="0" runat="server" ControlToValidate="ddlareaStockadd"
                         ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectarea%>" ValidationGroup="AddStock" Display="None"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="Reqlocation" InitialValue="0" runat="server" Display="None"
                              ValidationGroup="AddStock" ControlToValidate="ddllocationStockadd"
                         ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectlocation%>"></asp:RequiredFieldValidator>
                                                       <asp:RequiredFieldValidator ID="reqshelf" InitialValue="0" ValidationGroup="AddStock"
                               runat="server" ControlToValidate="ddlshelfStockadd" Display="None"
                         ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectshelf%>"></asp:RequiredFieldValidator>--%>

                           <asp:RegularExpressionValidator ID="reqTxtBin" runat="server" ControlToValidate="txtBinInPopup" Display="None"
                             ValidationGroup="BinEdit" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterbinname%>"></asp:RegularExpressionValidator>

                         <%-- <asp:RequiredFieldValidator ID="ReqBin" InitialValue="0" ValidationGroup="AddStock"
                               runat="server" ControlToValidate="ddlbinStockadd" Display="None"
                         ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectbin%>"></asp:RequiredFieldValidator>--%>

                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQuantity" ValidationGroup="AddStock"
                         ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterquantity%>" Display="None"></asp:RequiredFieldValidator>

                                                  </div>
                                  <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlsiteStockadd" ValidationGroup="AddStock" runat="server"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Area%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlareaStockadd" runat="server" ValidationGroup="AddStock" AutoPostBack="true"
                     OnSelectedIndexChanged="ddlareaStockadd_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Location%></label>
           <div class="col-sm-8">
                 <asp:DropDownList ID="ddllocationStockadd" runat="server" ValidationGroup="AddStock" AutoPostBack="true"
                     OnSelectedIndexChanged="ddllocationStockadd_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
  </div>
                                                  <div class="form-group">
          <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Shelf%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlshelfStockadd" ValidationGroup="AddStock" runat="server" AutoPostBack="true"
                     OnSelectedIndexChanged="ddlshelfStockadd_SelectedIndexChanged"></asp:DropDownList>
            </div>
          </div>
           <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Bin%></label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList id="ddlbinStockadd" ValidationGroup="AddStock" SkinID="ddl_70" runat="server"></asp:DropDownList>
                <asp:TextBox ID="txtBinInPopup" runat="server" Visible="false" SkinID="txt_50" ValidationGroup="BinEdit"></asp:TextBox>
                 <asp:LinkButton ID="Imgbtnbinadd" runat="server" SkinID="BtnLinkAdd" OnClick="Imgbtnbinadd_Click"></asp:LinkButton>
                    <asp:LinkButton ID="ImgAddBin" runat="server" SkinID="BtnLinkAdd" Visible="false" OnClick="ImgAddBin_Click"></asp:LinkButton>
                    <asp:LinkButton ID="btnCaneclBin" runat="server" SkinID="BtnLinkCancel" Visible="false" OnClick="btnCaneclBin_Click"></asp:LinkButton>
            </div>
          </div>
            <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Quantity%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtQuantity" runat="server" ValidationGroup="AddStock" SkinID="txt_50"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQuantity"
                            ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>
                       <asp:RangeValidator runat="server" id="RangeValidator1" controltovalidate="txtQuantity" type="Integer" ValidationGroup="AddStock" ForeColor="Red"
                                                    minimumvalue="1" maximumvalue="10000000" Display="None" errormessage="<%$ Resources:DeffinityRes,PleaseentervalidQty%>"></asp:RangeValidator>
            </div>
            </div>
                                                      </div>
                                                      <div class="form-group">
           <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Notes%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtnotesinpopup" runat="server"></asp:TextBox>
            </div>
	</div>

                                                           <div class="col-md-4">
                                                                <label class="col-sm-6 control-label">Batch Control Y/N</label>
                                                                <div class="col-sm-6">
                                                                    <asp:RadioButtonList ID="BatchControlRblist" runat="server"
                                                                         RepeatDirection="Horizontal" CssClass="control-label">
                                                                        <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                                                        <asp:ListItem Value="False" Text="No" Selected="True"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                           </div>

           <div class="col-md-4">
           <div class="col-sm-9">
                <asp:Button ID="btnSubmitInPopUp" runat="server" Text="Submit"
                        ValidationGroup="AddStock" OnClick="btnSubmitInPopUp_Click"/>
            </div>
	</div>
                                                      </div>                
                   
                    <asp:GridView ID="gridNewStockItems" runat="server" AutoGenerateColumns="false" GridLines="None"
                            ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                            CellSpacing="2" EmptyDataText="No histoy found!" Width="100%" 
                        OnRowCommand="gridNewStockItems_RowCommand" OnRowDeleting="gridNewStockItems_RowDeleting">
            <Columns>
                <asp:BoundField Visible="false" DataField="CustomerName" HeaderText="<%$ Resources:DeffinityRes,Customer%>" HeaderStyle-CssClass="header_bg_l" />
               <asp:BoundField Visible="false" DataField="ProductName" HeaderText="<%$ Resources:DeffinityRes,ProductName%>" />
                <asp:BoundField DataField="Sitename" HeaderText="<%$ Resources:DeffinityRes,Site%>" HeaderStyle-CssClass="header_bg_l" />
                <asp:BoundField DataField="AreaName" HeaderText="<%$ Resources:DeffinityRes,Area%>" />
                <asp:BoundField DataField="LocationName" HeaderText="<%$ Resources:DeffinityRes,Location%>" />
                <asp:BoundField DataField="ShelfName" HeaderText="<%$ Resources:DeffinityRes,Shelf%>" />
                <asp:BoundField DataField="BinName" HeaderText="<%$ Resources:DeffinityRes,Bin%>" />
                   <asp:BoundField DataField="Qty" HeaderText="<%$ Resources:DeffinityRes,Qty%>" />
                <asp:BoundField DataField="Notes" HeaderText="<%$ Resources:DeffinityRes,notes%>" />
                <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:DeffinityRes,UserName%>" />
                <asp:TemplateField HeaderText="Batch Control">
                    <ItemTemplate>
                        <asp:Label ID="lblBatchControl" runat="server" Text='<%#Bind("BatchControlYesOrNo") %>'>"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btndelete" runat="server" SkinID="BtnLinkDelete"
                                                     CommandName="Delete" CommandArgument='<%# Bind("Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="header_bg_r" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                    </ContentTemplate>
                           </asp:UpdatePanel>
                   </div>
              </div>
        </asp:Panel>


        
        <asp:Label ID="l111" runat="server"></asp:Label>
           <ajaxToolkit:ModalPopupExtender ID="mdlpopupinGrid" runat="server" BackgroundCssClass="modalBackground"
             TargetControlID="l111" PopupControlID="PnlPopUpInGrid" CancelControlID="ImageButton5"></ajaxToolkit:ModalPopupExtender>

          <ajaxToolkit:ModalPopupExtender ID="mdlpopupForNewItem" runat="server" BackgroundCssClass="modalBackground"
             TargetControlID="l11" PopupControlID="PanelForNewItem" CancelControlID="ImageButton4"></ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="PanelForNewItem" runat="server" BackColor="White"
                        Style="display:none;overflow-y:auto;" Width="890px" Height="97%" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">
             
             
             
                <div class="form-group">
                     <div class="col-md-10 text-bold">
                              <strong> <asp:Label ID="Label7" Text="New Inventory" Font-Size="X-Large" runat="server"></asp:Label></strong>
                          <hr class="no-top-margin" />
                       </div>
                    <div class="col-md-2" style="text-align:right;float:right;">
                          <asp:LinkButton ID="ImageButton4" runat="server" SkinID="BtnLinkCancel" 
                              ToolTip="<%$ Resources:DeffinityRes,Close%>" OnClick="imgBtnCancel_Click"></asp:LinkButton>
                    </div>
                </div>
             <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanel_AddItem">
                <ProgressTemplate>
                    <asp:Label ID="imgloading_51" runat="server" SkinID="Loading"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel_AddItem" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <div class="row">
                             <div class="col-md-6">
                                 <div class="col-sm-12">
                                     <asp:ValidationSummary ID="val11" runat="server" ForeColor="White" ValidationGroup="NewItem"  />
                                     <asp:Label ID="lblInventoryAddMsg" runat="server" ClientIDMode="Static" EnableViewState="false"></asp:Label>
                                     </div>
                                 </div>
                     </div>
                      <div class="form-group" runat="server" id="DivRbnBatchControlInNewInventoryInNewInventory">
                          <div class="col-md-6">
                            <label class="col-sm-4 control-label">Batch Control Y/N</label>
                            <div class="col-sm-8">
                                <asp:RadioButtonList ID="RbnBatchControlInNewInventory"
                                                   runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                    OnSelectedIndexChanged="RbnBatchControlInNewInventory_SelectedIndexChanged">
                                    <asp:ListItem Value="True" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="False" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                      </div>
                     <div class="form-group" runat="server" id="DivBatchReference">
                        <div class="col-md-6">
                            <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.BatchReference%></label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtBatchreference" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                     <div class="form-group" id="pnl_add_Customer" runat="server">
                         <div class="col-md-6">
                              <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Customer%> </label>
                             <div class="col-sm-8">
                                  <asp:DropDownList ID="ddlcustomerInNewItem" runat="server" AutoPostBack="true"
                                       OnSelectedIndexChanged="ddlcustomerInNewItem_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectcustomer%> "
                                                                 ControlToValidate="ddlcustomerInNewItem" InitialValue="0" Display="None" ValidationGroup="NewItem"
                                                                           ></asp:RequiredFieldValidator>
                             </div>
                         </div>
                        <div class="col-md-6">
                             <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.Site%>  </label>
                             <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlsiteInNewItem" Width="200px" runat="server"></asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectsite%>"
                                                                 ControlToValidate="ddlsiteInNewItem" InitialValue="0" Display="None" ValidationGroup="NewItem" ForeColor="White"
                                                                          ></asp:RequiredFieldValidator>
                             </div>
                        </div>
                    </div>
                     <div class="form-group" id="pnl_add_Category" runat="server">
                         <div class="col-md-6">
                              <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.Category%>   </label>
                             <div class="col-sm-8 form-inline" id="DivCategoryInNewInventory" runat="server">
                                 <asp:DropDownList ID="ddlcategoryInNewItem" runat="server" AutoPostBack="True" SkinID="ddl_70"
                                      OnSelectedIndexChanged="ddlcategoryInNewItem_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:LinkButton ID="LnkBtnCategoryAddInNewInventory" runat="server"
                                                            SkinID="BtnLinkAdd" CausesValidation="false" ToolTip="Add Category"
                                                                           OnClick="LnkBtnCategoryAddInNewInventory_Click"></asp:LinkButton>
                                 <asp:LinkButton ID="LnkBtnCategoryEditInNewInventory" ToolTip="Edit Category"
                                     runat="server" SkinID="BtnLinkEdit" OnClick="LnkBtnCategoryEditInNewInventory_Click"></asp:LinkButton>
                             </div>
                             <div class="col-sm-8 form-inline" id="DivCategoryAddEditInNewInventory" runat="server" visible="false">
                                  <asp:HiddenField ID="HiddenFieldCategoryValue" runat="server" />
                                 <asp:TextBox ID="txtCategoryInNewInventory" runat="server" SkinID="txt_70"></asp:TextBox>
                                 <asp:LinkButton ID="lnkSubmitCategoryInNewInventory" runat="server" ToolTip="Submit"
                                            SkinID="BtnLinkAdd" OnClick="lnkSubmitCategoryInNewInventory_Click"></asp:LinkButton>
                                 <asp:LinkButton ID="lnkBtnCategoryCancelInInventory" CausesValidation="false" ToolTip="Cancel"
                                            runat="server" SkinID="BtnLinkCancel" OnClick="lnkBtnCategoryCancelInInventory_Click"></asp:LinkButton>
                             </div>
                         </div>
                        <div class="col-md-6">
                             <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.SubCategory%>   </label>
                             <div class="col-sm-8 form-inline" runat="server" id="DivSubCategoryInNewInventory">
                                  <asp:DropDownList ID="ddlSubcategoryInNewItem" runat="server" SkinID="ddl_70"></asp:DropDownList>
                                  <asp:LinkButton ID="LnkBtnSubCategoryAddInNewInventory" runat="server" ToolTip="Add sub category"
                                       SkinID="BtnLinkAdd" OnClick="LnkBtnSubCategoryAddInNewInventory_Click"></asp:LinkButton>
                                  <asp:LinkButton ID="LnkBtnSubCategoryEditInNewInventory" runat="server" ToolTip="Edit sub category"
                                       SkinID="BtnLinkEdit" OnClick="LnkBtnSubCategoryEditInNewInventory_Click"></asp:LinkButton>
                             </div>
                             <div class="col-sm-8 form-inline" id="DivSubCategoryAddEditInNewInventory" runat="server" visible="false">
                                 <asp:HiddenField ID="HiddenFieldSubCategoryValue" runat="server" />
                                 <asp:TextBox ID="txtSubCategoryInNewInventory" runat="server" SkinID="txt_70"></asp:TextBox>
                                 <asp:LinkButton ID="lnkSubmitSubCategoryInNewInventory" runat="server" ToolTip="Submit"
                                      SkinID="BtnLinkAdd" OnClick="lnkSubmitSubCategoryInNewInventory_Click"></asp:LinkButton>
                                 <asp:LinkButton ID="lnkBtnSubCategoryCancelInInventory" runat="server" ToolTip="Cancel"
                                      SkinID="BtnLinkCancel" OnClick="lnkBtnSubCategoryCancelInInventory_Click"></asp:LinkButton>
                             </div>
                        </div>
                    </div>
                     <div class="form-group" id="pnl_add_Product" runat="server">
                         <div class="col-md-6">
                              <label class="col-sm-4 control-label">
                                      <%= Resources.DeffinityRes.Product%>    
                              </label>
                             <div class="col-sm-8">
                                  <asp:TextBox ID="TxtproductInNewItem" runat="server" ClientIDMode="Static"></asp:TextBox>
                                 <asp:HiddenField ID="hfCustomerId" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterproductname%>"
                                                                 ControlToValidate="TxtproductInNewItem" InitialValue="0" Display="None" ValidationGroup="NewItem"
                                                                          ></asp:RequiredFieldValidator>
                              <%--   <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="GetAllInventoryNames"
                                      UseContextKey="true" ContextKey="Country" ServicePath="InventoryManagerPage.aspx" TargetControlID="TxtproductInNewItem"></ajaxToolkit:AutoCompleteExtender>--%>
                             </div>
                         </div>
                        <div class="col-md-6">
                             <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.QTY %>   </label>
                             <div class="col-sm-8">
                                   <asp:TextBox ID="txtQtyNewItem" runat="server" Width="75px"></asp:TextBox>
                                  <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtQtyNewItem"></ajaxToolkit:FilteredTextBoxExtender>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterQuantity%>"
                                                                 ControlToValidate="txtQtyNewItem" Display="None" ValidationGroup="NewItem" ForeColor="White"
                                                                          ></asp:RequiredFieldValidator>
                             </div>
                        </div>
                    </div>
                     <div class="form-group" id="pnl_add_location" runat="server">
                         <div class="col-md-6">
                              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Area %></label>
                             <div class="col-sm-8">
                                 <asp:DropDownList ID="ddlareaInNewItem" runat="server" AutoPostBack="true"
                                       OnSelectedIndexChanged="ddlareaInNewItem_SelectedIndexChanged"></asp:DropDownList>
                             </div>
                         </div>
                        <div class="col-md-6">
                             <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Location %></label>
                             <div class="col-sm-8">
                                 <asp:DropDownList ID="ddlLocationInNewItem" runat="server" AutoPostBack="true"
                                      OnSelectedIndexChanged="ddlLocationInNewItem_SelectedIndexChanged"></asp:DropDownList>
                             </div>
                        </div>
                    </div>
                     <div class="form-group" id="pnl_add_location1" runat="server">
                         <div class="col-md-6">
                              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Shelf %></label>
                             <div class="col-sm-8">
                                  <asp:DropDownList ID="ddlShelfInNewItem" Width="150px" runat="server"
                                       AutoPostBack="true" OnSelectedIndexChanged="ddlShelfInNewItem_SelectedIndexChanged"></asp:DropDownList>
                             </div>
                         </div>
                        <div class="col-md-6">
                             <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Bin%></label>
                             <div class="col-sm-8">
                                   <asp:DropDownList ID="ddlBinInNewItem" Width="150px" runat="server" ></asp:DropDownList>
                             </div>
                        </div>
                    </div>
                     <div class="form-group" id="pnl_edit_product" runat="server">
                         <div class="col-md-6">
                              <label class="col-sm-4 control-label">
                                   <%= Resources.DeffinityRes.Product%> 
                                   <asp:HiddenField ID="hid_batchproduct" runat="server" Value="0" /><asp:HiddenField ID="hid_batchid" runat="server" Value="0" />
                              </label>
                               <div class="col-sm-8">
                                   <asp:TextBox ID="txtProductEdit" runat="server" ReadOnly="true"></asp:TextBox>
                               </div>
                         </div>
                        <div class="col-md-6">
                             <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.QTY%>  </label>
                             <div class="col-sm-8">
                                  <asp:TextBox ID="txtQtyEdit" runat="server" Width="75px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtQtyEdit"></ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQtyEdit" ValidationGroup="NewItem"
                                                             ErrorMessage="Please enter quantity" Display="None"></asp:RequiredFieldValidator>
                               </div>
                        </div>
                    </div>

                      

                     <div class="form-group" runat="server" id="DivBatchReferenceDetailsInNewInventory">
                         <div class="col-md-12">
                               <div class="col-sm-12">
                                    <div class="form-group">
                                          <div class="col-md-12">
                                                <asp:Panel ID="pnlBatch" runat="server" CssClass="tab_subheader"> Batch Reference Details </asp:Panel>
                                                 <hr class="no-top-margin" />
                                          </div>
                                    </div>
                                    <asp:Panel ID="pnlBatchColumns" runat="server" Width="850px">
                                        <asp:PlaceHolder ID="ph_batchcolumns" runat="server"></asp:PlaceHolder>
                                    </asp:Panel>
                               </div>
                         </div>
                        </div>
                     <div class="form-group">
                         <div class="col-md-12">
                              <div class="col-sm-12">
                                  <asp:Panel ID="btnPnlAddButtons" runat="server" Height="75px">
                                           <asp:Button ID="btnSaveInNewItem" runat="server" Text="Add" ValidationGroup="NewItem" OnClick="btnSaveInNewItem_Click" />
                                           <asp:Button ID="BtncancelInNewItem" runat="server" Text="Cancel" CausesValidation="false" OnClick="BtncancelInNewItem_Click" />
                               </asp:Panel>
                              </div>
                         </div>
                      </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveInNewItem" />
                    <asp:AsyncPostBackTrigger ControlID="ddlcustomerInNewItem" EventName="SelectedIndexChanged" />
                     <asp:AsyncPostBackTrigger ControlID="ddlareaInNewItem" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlLocationInNewItem" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlShelfInNewItem" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            </asp:Panel>

        <!-- Edit Batch -->
        <asp:Panel ID="PnlPopUpInGrid" runat="server" BackColor="White"
                        Style="display:none" Width="950px" Height="650px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="None">

            <div class="form-group">
        <div class="col-md-12">
           <strong>    <asp:Label ID="Label6" runat="server"></asp:Label> </strong> 
              <div style="float: right">
               <asp:LinkButton ID="ImageButton5" runat="server" SkinID="BtnLinkCancel" ToolTip="Close" OnClick="imgBtnCancel_Click"></asp:LinkButton>
             </div>
            <hr class="no-top-margin" />
            </div>
            </div>
             <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePnlPopUpInGrid">
                <ProgressTemplate>
                    <asp:Label ID="imgloading_5" runat="server"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePnlPopUpInGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <div class="form-group">
                            <div class="col-md-12">
                                <div class="col-sm-12">
                                 <asp:Label ID="lblProductidInPOPUP" runat="server" Visible="false" ClientIDMode="Static"></asp:Label>
                                  <asp:Label ID="LblListviewMsg" runat="server" ClientIDMode="Static" EnableViewState="false"></asp:Label>
                                  <asp:ValidationSummary ID="InsertSum1" runat="server" DisplayMode="List" ValidationGroup="InsertSum" />
                                  <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ValidationGroup="UpdateSum" />
                                    </div>
                            </div>
                     </div>

                    <div class="form-group">
      <div class="col-md-3">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.QtyUsed%> </label>
           <div class="col-sm-7">
                 <asp:Label ID="lblEid" runat="server" Visible="false"></asp:Label>
                                 <asp:Label ID="LblProductId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LblPid" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblQtyUsed" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblBatchId" runat="server" Visible="false"></asp:Label>
                 <asp:TextBox ID="txtQtyUsed" runat="server" SkinID="txt_80"></asp:TextBox>
                                   <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtQtyUsed" type="Integer" ForeColor="Red" ValidationGroup="InsertSum"
                                                                               Display="None" minimumvalue="1" maximumvalue="10000000" errormessage="<%$ Resources:DeffinityRes,PleaseentervalidQty%>" />
                                 <asp:RangeValidator runat="server" id="RangeValidator2" controltovalidate="txtQtyUsed" type="Integer" ForeColor="Red" ValidationGroup="UpdateSum"
                                                                               Display="None" minimumvalue="1" maximumvalue="10000000" errormessage="<%$ Resources:DeffinityRes,PleaseentervalidQty%>" />

                              <ajaxToolkit:FilteredTextBoxExtender id="txtfilter" runat="server" FilterType="Numbers" TargetControlID="txtQtyUsed"></ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterQty%>"
                                               ControlToValidate="txtQtyUsed" Display="None" ValidationGroup="UpdateSum" Width="225px">
                                </asp:RequiredFieldValidator>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterQty%>"
                                               ControlToValidate="txtQtyUsed" Display="None" ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.Status%> </label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11112" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectstatus%>"
                                                                    ControlToValidate="ddlStatus" InitialValue="0" Display="None"
                                                                                 ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectstatus%>"
                                                                         ControlToValidate="ddlStatus" InitialValue="0" Display="None"
                                              ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-5">
           <label class="col-sm-3 control-label">   <%= Resources.DeffinityRes.Condtion%> </label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="ddlcondition" runat="server"></asp:DropDownList>
            </div>
	</div>
</div>
                     <div class="form-group">
                          <div class="col-md-6">
           <label class="col-sm-3 control-label">    <%= Resources.DeffinityRes.Requester%>   </label>
           <div class="col-sm-9">
                  <asp:TextBox ID="TxtRequester" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterRequester%>"
                                               ControlToValidate="TxtRequester" Display="None" ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterRequester%>"
                                               ControlToValidate="TxtRequester" Display="None" ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>
            </div>
	</div>
	                      <div class="col-md-6">
           <label class="col-sm-4 control-label">Record Number</label>
           <div class="col-sm-8">
                    <asp:TextBox ID="txtRecordNumber" runat="server"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please enter record number"
                                               ControlToValidate="txtRecordNumber" Display="None" ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Please enter record number"
                                               ControlToValidate="txtRecordNumber" Display="None" ValidationGroup="InsertSum" Width="225px"></asp:RequiredFieldValidator>--%>
            </div>
	</div>

                     </div>
                    <div class="form-group">
                            <div class="col-md-12">
 <label class="col-sm-1 control-label">   <%= Resources.DeffinityRes.Notes%></label>
           <div class="col-sm-7">
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
                              <div class="col-sm-4">
                                     <asp:CheckBox ID="ChcekFromBacthControl" runat="server"
                                         AutoPostBack="true" Text="Take quantity from non-batch related stock" OnCheckedChanged="ChcekFromBacthControl_CheckedChanged" />
                              </div>
	</div>
                    </div>
                    <div class="form-group" id="DivSearchforaBatch" runat="server">
          <div class="col-md-6">
           <label class="col-sm-4 control-label">
               <%= Resources.DeffinityRes.SearchforaBatch%>  
           </label>
           <div class="col-sm-8 form-inline">
                 <asp:TextBox ID="txtBatchSearch" runat="server" SkinID="txt_70"></asp:TextBox>
                 <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click" />
            </div>
          </div>
          <div class="col-md-6">
               <div class="col-sm-9">
                
              </div>
          </div>
</div>
                      <div class="form-group">
                           <div class="col-md-12">
                              
                          <asp:Panel ID="PanelFilterdBatchGrid" runat="server" ScrollBars="Auto" Width="920px" Height="250px">
                                <asp:GridView ID="FilterdBatchGrid" runat="server" SkinID="Custom_Grid" Width="100%" AutoGenerateColumns="true" ShowHeader="true"
                                     ShowFooter="false" OnRowDataBound="FilterdBatchGrid_RowDataBound">
                                    <Columns>
                                          <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                              <ItemTemplate>
                                                  <asp:CheckBox ID="checkedBatch" ClientIDMode="Static" runat="server" onclick="CheckOne(this)" />
                                                  <asp:Label ID="lblBatchId" runat="server" Visible="false" Text ='<%#Bind("BatchID") %>'></asp:Label>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                  </asp:Panel>
                            </div>
                      </div>
                      <div class="form-group" id="DivNonBatchControlValues" runat="server" visible="false">
                          <div class="col-md-6">
                              <label class="col-sm-3 control-label">Opening QTY</label>
                              <div class="col-sm-3">
                                  <asp:TextBox ID="TxtDefaultOpeningQTY" runat="server" ReadOnly="true" SkinID="txt_60"></asp:TextBox>
                              </div>
                              <label class="col-sm-3 control-label"> Available QTY</label>
                              <div class="col-sm-3">
                                  <asp:Label ID="lblDefaultBatchId" runat="server" Visible="false"></asp:Label>
                                  <asp:TextBox ID="TxtDefaultAvailableQTY" ReadOnly="true" runat="server" SkinID="txt_60"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                    <div class="form-group">
                          <div class="col-md-12">
                               <div class="col-sm-3">
                         <asp:HiddenField ID="hUsage" runat="server" Value="" />
                                  <asp:Button ID="btnupdate" runat="server" Text="Update" SkinID="btnUpdate" ValidationGroup="UpdateSum" Visible="false"
                                     CommandName="Update" OnClick="btnupdate_Click" />
                                   <asp:Button ID="Btninsert" runat="server" Text="Submit" ValidationGroup="InsertSum" Visible="false"
                                             CommandName="Insert" OnClick="Btninsert_Click" />
                                  <asp:Button ID="btncancel" runat="server" SkinID="btnCancel"
                                                                             CommandName="Cancel" OnClick="btncancel_Click" />
                                   </div>
                         </div>
                    </div>
                    <div class="form-group" style="display:none;">
                          <div class="col-md-12">
                               <%= Resources.DeffinityRes.Project%> 
                               <asp:DropDownList ID="ddlProject" runat="server" DataValueField="value" DataTextField="name" Width="200px"></asp:DropDownList>
                              </div>
                        </div>
                    <div class="form-group" style="display:none;">
                          <div class="col-md-12">
                               <%= Resources.DeffinityRes.Batch%>
                               <asp:DropDownList ID="ddlBatch" runat="server" Width="200px" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                              </div>
                        </div>

                     <div class="form-group">
                          <div class="col-md-12">
                              <asp:Panel ID="pnlUsageColumns" runat="server" ScrollBars="Vertical" Height="200px" Width="590px" Visible="false">
                                <asp:PlaceHolder ID="pUsageCustomer" runat="server"></asp:PlaceHolder>
                                    </asp:Panel>
                              </div>
                         </div>
                  
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            </asp:Panel>
        <asp:Label ID="l11" runat="server" EnableViewState="false"></asp:Label>
     
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupTransferItem" runat="server" BackgroundCssClass="modalBackground"
            TargetControlID="imgPopupTransferItem" PopupControlID="pnlTransferItem" CancelControlID="imghistoryCancel">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Label ID="imgPopupTransferItem" runat="server" />
        <asp:Panel ID="pnlTransferItem" runat="server" BackColor="White" Style="display: none;"
                                            Width="75%" Height="500px" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Auto">

            
<div class="row">
          <div class="col-md-10">
 <strong> <%= Resources.DeffinityRes.TransferItems%> </strong> 
<hr class="no-top-margin" />
	</div>
    <div class="col-md-2" style="float:right;text-align:right;">
        <asp:LinkButton ID="imghistoryCancel" runat="server" SkinID="BtnLinkCancel" ToolTip="Close"></asp:LinkButton>
        </div>
</div>

            
<div class="row">
          <div class="col-md-12">
                 <asp:UpdateProgress ID="uprogress1" runat="server">
                <ProgressTemplate>
                    <asp:Label ID="imgLoad" runat="server"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
<div class="row">
          <div class="col-md-12">
              <div class="col-sm-12">
                  <asp:Label ID="lblTransferMsg" runat="server" ClientIDMode="Static"></asp:Label>
              </div>
          </div>
      <div class="col-md-12">
              <div class="col-sm-12">
                    <asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="tr" />
                  <asp:HiddenField ID="hfInventoryId" runat="server" Value="0" />
                  <asp:HiddenField ID="hfCategoryId" runat="server" Value="0" />
                  <asp:HiddenField ID="hfSubcategoryId" runat="server" Value="0" />
                  <asp:HiddenField ID="hfPartNumber" runat="server" Value="" />
                  <asp:HiddenField ID="hfDescription" runat="server" Value="" />
                  <asp:HiddenField ID="hfBarcode" runat="server" Value="" />
              </div>
          </div>
</div>

            <div class="form-group">
                       <div class="col-md-2">
                           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Date%></label>
                       </div>
                       <div class="col-md-2">
                           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Current%></label>
                       </div>
                        <div class="col-md-2">
                           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Amendment%></label>
                       </div>
                      <div class="col-md-2">
                           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.TransfertoSite%></label>
                       </div>
                        <div class="col-md-2">
                           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.Notes%></label>
                       </div>
               </div>
            <div class="form-group">
                 <div class="col-md-2">
                     <div class="col-sm-12">
                         <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                     </div>
                 </div>
                  <div class="col-md-2">
                       <div class="col-sm-12">
                     <asp:Label ID="lblCurrentQty" runat="server" Font-Bold="true"></asp:Label>
                           </div>
                       </div>
                  <div class="col-md-2">
                       <div class="col-sm-12">
                     <asp:TextBox ID="txtAmendment" runat="server" Width="60px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenteramendment%>"
                            ControlToValidate="txtAmendment" ValidationGroup="tr">*</asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filterAmendment" runat="server" TargetControlID="txtAmendment"
                            ValidChars="0123456789-+/"></ajaxToolkit:FilteredTextBoxExtender>
                           </div>
                 </div>
                  <div class="col-md-2">
                       <div class="col-sm-12">
                     <asp:DropDownList ID="ddlTransferSite" runat="server"></asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdtransferSite" runat="server" TargetControlID="ddlTransferSite"
                            Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/Inventory/Webservices/InventoryMgr.asmx"
                            ServiceMethod="GetInventorySite" LoadingText="[Loading...]" />
                           </div>
                 </div>
                  <div class="col-md-2">
                       <div class="col-sm-12">
                           <asp:TextBox ID="txtTransferNotes" runat="server" TextMode="MultiLine"></asp:TextBox>
                           </div>
                 </div>
                  <div class="col-md-2">
                        <div class="col-sm-12">
                            <asp:Button ID="imgTransferApply" runat="server" SkinID="btnApply"
                                OnClick="imgTransferApply_Click" ValidationGroup="tr" />
                           </div>
                 </div>
             </div>
               <div class="form-group">
                    <div class="col-md-12">
                         <div class="col-sm-12">
                             <asp:GridView ID="gvTransferItems" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="false">
                <Columns>
                    <asp:BoundField DataField="EntryDate" HeaderText="Date and Time" />
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Change%>">
                        <ItemTemplate>
                            <asp:Label ID="lblChange" runat="server" Text='<%# FormateQty(DataBinder.Eval(Container.DataItem,"Qty").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ReasonCode" HeaderText="<%$ Resources:DeffinityRes,ReasonCode%>" />
                    <asp:BoundField DataField="Notes" HeaderText="<%$ Resources:DeffinityRes,Notes%>" ItemStyle-Width="250px" />
                    <asp:BoundField DataField="ContractorName" HeaderText="<%$ Resources:DeffinityRes,ModifiedBy%>" />
                </Columns>
            </asp:GridView>
                               </div>
                    </div>
               </div>
	</div>
</div>
              
        </asp:Panel>


        <asp:Panel ID="pnlUse" runat="server" Visible="false">
                  <asp:Panel ID="pnlUseShow" runat="server" Width="100%">
                      
<div class="row" id="DivBatchSummaryTitle" runat="server">
          <div class="col-md-12">
               <strong><asp:Label ID="lblBatchSummary" runat="server" Text="Stock Summary"></asp:Label></strong> 
              <hr class="no-top-margin" />
          </div>
</div>      
<div class="row" id="DivBatchSummary" runat="server">
    <div class="col-md-12">
        <asp:Panel ID="pnlBatchGrid" runat="server" ScrollBars="Horizontal" Width="100%">
            <asp:GridView ID="GridBatch" SkinID="Custom_Grid" runat="server" AutoGenerateColumns="true" ShowHeader="true" ShowFooter="false"
                                Width="100%" OnPreRender="GridBatch_PreRender" OnRowCommand="GridBatch_RowCommand" OnRowDataBound="GridBatch_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField>
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btnEdit" SkinID="BtnLinkEdit" runat="server"
                                                                                            CommandName="Edit1" CommandArgument='<%#Bind("BatchID") %>'></asp:LinkButton>
                                                
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
        </asp:Panel>
    </div>
</div>
                    
<div class="row">
          <div class="col-md-12">
 <strong> <%= Resources.DeffinityRes.SummaryofUsage%><br />
                                   <asp:Label ID="lblItemMsg" runat="server" ForeColor="Green" Visible="false" Width="100%"></asp:Label>
                                   <span style="color: Gray;">The following grid displays a number of items deployed for
                                                        the particular product:<b>&nbsp<asp:Literal ID="ltlProduct" runat="server"></asp:Literal>&nbsp
                                                         -</b> Site: &nbsp<b><asp:Literal ID="ltlSite" runat="server"></asp:Literal> </strong> 
<hr class="no-top-margin" />
	</div>
</div>
                      
<div class="row">
          <div class="col-md-12">
               <asp:Panel ID="pnl1" runat="server" Width="100%">
                                            <asp:GridView ID="gvUsageSummary" runat="server" EmptyDataText="No items deployed." ShowFooter="false" Width="20%">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Use%>" HeaderStyle-CssClass="header_bg_l" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUse" runat="server" Text='<%#Bind("Use") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,NumberDeployed%>" HeaderStyle-CssClass="header_bg_r" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNoDeployed" runat="server" Text='<%#Bind("NumberDeployed") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                  </asp:Panel>

                                  <asp:UpdatePanel ID="upanleUsage" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                        <asp:Panel ID="PnlListview" runat="server" Visible="false" Width="100%">
                            <asp:Label ID="Label5" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
                            <div style="float:right;">
                               <asp:Button ID="btnadd" runat="server" Text="Add Usage Entry" OnClick="btnadd_Click" />
                               <asp:Button ID="btnFieldConfigurator" runat="server" Text="Field Configurator" OnClick="btnFieldConfigurator_Click" Visible="false" />
                            </div>
                            <div>
                                <asp:Panel ID="pnlGridUsage" runat="server" ScrollBars="Horizontal" Width="100%">
                            <asp:GridView ID="GridUsage" runat="server" SkinID="Custom_Grid" AutoGenerateColumns="true" ShowHeader="true" ShowFooter="false"
                                OnRowDataBound="GridUsage_RowDataBound" Width="100%" OnRowCommand="GridUsage_RowCommand" OnRowEditing="GridUsage_RowEditing"
                                 OnPreRender="GridUsage_PreRender">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btnEdit" SkinID="BtnLinkEdit" runat="server" Text="Edit"
                                                                                                     CommandArgument='<%#Bind("_id") %>' CommandName="Edit1"></asp:LinkButton>
                                                <asp:LinkButton ID="ImgDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                                     Visible="false" CommandArgument='<%#Bind("_id") %>' SkinID="BtnLinkDelete" CommandName="Delete1"></asp:LinkButton>
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                    </asp:Panel>
                                </div>
               
            </asp:Panel>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
	</div>
</div>

                   
                      </b></span>

                   
                </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnltab_subheader" runat="server">
            
            <div class="form-group">
                <div class="col-md-7 form-inline">
                    </div>
                <div class="col-md-5 form-inline pull-right" style="float:right">
                    <div style="float:right;" >
           <asp:Button ID="btnAddNewItem" runat="server" SkinID="btnOrange" Text="Add New Item" OnClick="btnAddNewItem_Click" />
                                               <asp:Button ID="btnViewInventory" runat="server" CssClass="btn btn-secondary"  Text="View Inventory"
                                                                                     ClientIDMode="Static" OnClick="btnViewInventory_Click"  />
                                                <asp:Button ID="btnViewUsageSummary0" runat="server" CssClass="btn btn-secondary"
                                                                                     Text="View Usage Summary" ClientIDMode="Static" OnClick="btnViewUsageSummary0_Click"   /></div>
	</div>
                </div>
<div class="form-group">
      <div class="col-md-12 form-inline">
           <strong> <%= Resources.DeffinityRes.InventorybySite%> </strong> 
                           
                      
                            <asp:Button ID="imgbtnViewInven" runat="server" OnClick="imgbtnViewInven_Click" CssClass="btn btn-secondary"
                                                                 ToolTip="<%$ Resources:DeffinityRes,ViewAllInventory%>" Visible="false" />
	</div>
    <hr />
</div>
            
<div class="form-group">
      <div class="col-md-12">
           <asp:Label ID="lblbtnViewUsageSummary0" runat="server"></asp:Label>
	</div>

</div>
                  
                                 
                                <asp:Panel ID="PnlSummary"  runat="server"
                                    Width="100%" ClientIDMode="Static" Visible="false" >
                                    
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.UsageSummaryDetails%></strong> 
<hr class="no-top-margin" />
	</div>
</div>
             
              <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePnlSummary">
                <ProgressTemplate>
                    <asp:Label ID="imgloading_3" SkinID="Loading" runat="server"></asp:Label>
                </ProgressTemplate>
            </asp:UpdateProgress>
              <asp:UpdatePanel ID="UpdatePnlSummary" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                  <ContentTemplate>
                        <asp:GridView ID="GridUsageSummary" runat="server" AutoGenerateColumns="false" Width="100%" Visible="false">
                            <Columns>
                               <asp:BoundField DataField="ItemDescription" HeaderText="<%$ Resources:DeffinityRes,Product%>"/>
                                <asp:BoundField DataField="OpeningStock" HeaderText="<%$ Resources:DeffinityRes,OpeningStock%>" ItemStyle-HorizontalAlign="Right" />
                                   <asp:BoundField DataField="QtyUsed" HeaderText="<%$ Resources:DeffinityRes,QtyUsed%>" ItemStyle-HorizontalAlign="Right" />
                                   <asp:BoundField DataField="Status" HeaderText="<%$ Resources:DeffinityRes,Status%>" />
                                   <asp:BoundField DataField="ModifiedBy" HeaderText="<%$ Resources:DeffinityRes,ModifiedBy%>" />
                                   <asp:BoundField DataField="ProjectName" HeaderText="<%$ Resources:DeffinityRes,Project%>"/>
                            </Columns>
                        </asp:GridView>
                      <asp:Panel ID="pnlUsagesummary" runat="server" ScrollBars="Horizontal" Width="100%">
                    <asp:GridView ID="GridUsageSummary1" runat="server" SkinID="Custom_Grid"
                         AutoGenerateColumns="true" Width="100%" OnRowDataBound="GridUsageSummary1_RowDataBound"></asp:GridView>
                          </asp:Panel>
                  </ContentTemplate>
              </asp:UpdatePanel>
        </asp:Panel>
                         
<asp:Panel ID="pnlInventory" runat="server" ClientIDMode="Static" Width="100%">
                        <asp:ValidationSummary ID="grdValidationSummery" runat="server" ValidationGroup="AddInvnt" />
                        <asp:HiddenField ID="hfID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfSiteID" runat="server" Value="0" />
                        <asp:HiddenField ID="hfSubcatID" runat="server" Value="0" />
                                  <div style="float:right;text-align:right;margin-bottom:3px;">
                                      <asp:LinkButton ID="btnViewAll" runat="server" 
                                          Text="View All" CausesValidation="false" CssClass="btn btn-gray"
                                           Font-Bold="true" OnClick="btnViewAll_Click"></asp:LinkButton>
                                  </div>

                        <asp:GridView ID="grdInventory" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID" Width="100%" OnPageIndexChanging="grdInventory_PageIndexChanging" PageSize="10"
                            Visible="true" OnRowCancelingEdit="grdInventory_RowCancelingEdit" OnRowCommand="grdInventory_RowCommand"
                            OnRowEditing="grdInventory_RowEditing" OnRowUpdating="grdInventory_RowUpdating"
                            OnRowDeleting="grdInventory_RowDeleting"
                            EmptyDataText="No products available" 
                            onrowdatabound="grdInventory_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Edit%>">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandArgument='<%# Bind("id")%>'
                                            CommandName="MoreOptions" SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandArgument='<%# Bind("id")%>'
                                            CommandName="Update" SkinID="BtnLinkUpdate" ToolTip="<%$ Resources:DeffinityRes,Update%>" ValidationGroup="AddInvnt" />
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes,Cancel%>" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                     
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        <div id="pnlOriginalMaterial" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Category%>" SortExpression="CategoryName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,SubCategory%>" SortExpression="SubCategoryName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubCategory" runat="server" Text='<%# Bind("SubCategoryName") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Description%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text='<% #Bind("ItemDescription")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Manufacturer%>" SortExpression="ManufacturerName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManufcturer" runat="server" Text='<%# Bind("ManufacturerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Site%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSite" runat="server" Text='<%# Bind("Sitename") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,OpeningStock%>" SortExpression="QTY" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QTYAvailable%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTYAvailable" runat="server"
                                             Text='<%# getActualStackLaevelInMainGrid(Eval("Id").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="QTY<br/>Available<br/>(Default batch)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTY_Available_Default_batch" runat="server"
                                             Text='<%#GetAvailable_Qty_In_Default_batch(Eval("Id").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,QTYDeployed%> " Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQTYDeployed" runat="server" Text='<%# GetQtyInUseFromIm_PSDProducts(Eval("Id").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Usage%>">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnkUsage" runat="server" CausesValidation="false" Text="Usage" CommandName="Usage"
                                                                                       CommandArgument='<%# Bind("id")%>'></asp:LinkButton>
                                              &nbsp;
                                        <a id="imageColapse" href="javascript:expandcollapseUsage('divUsage<%# Eval("id") %>','<%# Eval("id") %>','<%# Eval("Cid") %>','one');">
                                              <img title='View Usage' id='imgdivUsage<%# Eval("id") %>' alt='Click to show/hide <%# Eval("id") %>' style="width:9px;border:0px" src='plus.gif' />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <%--visible='<%#BatchVisibilityInMainGrid(Eval("Id").ToString())%>'--%>
                                        <div id="GridBtnInMainGrid" runat="server">
                                            <asp:LinkButton ID="lnkBatch" runat="server" CausesValidation="false" Text="Add Stock" CommandName="Batch"
                                                                                       CommandArgument='<%# Bind("id")%>'></asp:LinkButton>
                                           &nbsp;
                                            <a id="imageColapse" href="javascript:expandcollapse('div<%# Eval("id") %>','<%# Eval("id") %>','<%# Eval("Cid") %>','one');">
                                                <img title='View Stock Levels' id='imgdiv<%# Eval("id") %>' alt='Click to show/hide <%# Eval("id") %>' style="width:9px;border:0px" src='plus.gif' />
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                              
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TransferItems%>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgTransfer" runat="server" CausesValidation="false" CommandName="Transfer" SkinID="BtnLinkTransferItems"
                                            CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"/>
                                    <ItemStyle VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgHis" runat="server" CausesValidation="false" CommandName="History"
                                             SkinID="BtnLinkHistory" CommandArgument='<%# Bind("ID") %>'
                                             ToolTip="<%$ Resources:DeffinityRes,ViewHistory%>"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete1" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                            CommandName="Delete" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkDelete" ToolTip="<%$ Resources:DeffinityRes,Delete%>">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                  <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding:0px">
                                                 <div id='divUsage<%# Eval("id") %>' style="display:none; position: relative; left: 15px;padding-bottom:5px;overflow: auto; width: 97%">
                                                    <strong>Usage </strong>    
                                                       <div style="color:green;" id='DivPleaseWaitMsgInUsage<%# Eval("id") %>'></div>
                                                         <table style="width:100%;border-collapse: collapse;font-size:smaller;border-color:whitesmoke;"
                                                              class="table responsive"
                                                              id="TblUsagegrid<%# Eval("id")%>" border="1"></table>
                                                    </div>


                                                <div id='div<%# Eval("id") %>' style="display:none; position: relative; left: 15px; overflow: auto; width: 97%">
                                                    <strong>Stock </strong>
                                                     <div style="color:green;" id='DivPleaseWaitMsgInBatch<%# Eval("id") %>'></div>
                                                     <table style="width:100%;border-collapse: collapse;font-size:smaller;border-color:whitesmoke;"
                                                          class="table responsive"
                                                          id="TblBatchgrid<%# Eval("id")%>" border="1"></table>
                                                     </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                      <ItemStyle Width="2%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                              
                         </asp:Panel>
               
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="imgbtnUpdateMaterial" />
        <asp:PostBackTrigger ControlID="btnSaveMaterial"  />
        <asp:PostBackTrigger ControlID="ImageButton1" />
        <asp:PostBackTrigger ControlID="imgbtnExportToExcel" />
        <asp:PostBackTrigger ControlID="imgbtnExportToPdf" />
        <asp:PostBackTrigger ControlID="btnsearch" />
        <asp:PostBackTrigger ControlID="ddlcustomerInSearch" />
        <asp:AsyncPostBackTrigger ControlID="ddlSite" EventName="SelectedIndexChanged" />
     <%--  <asp:AsyncPostBackTrigger ControlID="grdInventory" EventName="grdInventory_RowEditing" />--%>
       <%-- <asp:PostBackTrigger ControlID="grdInventory" />--%>
    </Triggers> 
</asp:UpdatePanel>
