<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_FLSListCtrl" Codebehind="FLSListCtrl.ascx.cs" %>
<%--<%@ Register Src="~/WF/DC/controls/QuickSearchCtrl.ascx" TagName="QuickSearchCtrl" TagPrefix="QSCtrl"%>--%>
<%--<script src="../js/Utility.js" type="text/javascript" language="javascript"></script>--%>
<script type="text/javascript">
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
    //function reFresh() {
    //    location.reload(true)
    //}
</script>
<%--<link href="../../Content/jqgrid.css" rel="stylesheet" type="text/css" />--%>
<!-- jTable style file -->
<link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />
 
    <link href="../../../Content/css/jquery.ui.theme.css" rel="stylesheet" />
    <script src="../../../Scripts/ui/jquery.ui.core.js"></script>
    <script src="../../../Scripts/ui/jquery.ui.widget.js"></script>    
    <script src="../../../Scripts/ui/jquery.ui.mouse.js"></script>
    <script src="../../../Scripts/ui/jquery.ui.dialog.js"></script>
    <script src="../../../Scripts/ui/jquery.ui.resizable.js"></script>
    <script src="../../../Scripts/ui/jquery.ui.draggable.js"></script>  
<%--<script src="<%:ResolveClientUrl("~/Scripts/jquery.signalR-2.2.1.js")%>"></script>
        <script src="/SignalR/hubs" type="text/javascript"></script>
 <script src="<%:ResolveClientUrl("~/Scripts/ChatScript.js?v2=1")%>"></script>--%>
 <%--<link href="../../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
<link href="../../../Content/css/jquery.ui.theme.css" rel="stylesheet" />
	<script src='<%:ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
--%>
<%--<link href="../../Content/css/custom-theme/jquery-ui-1.9.2.custom.css" rel="stylesheet" />
<script src="../../Content/jquery-ui-1.9.2.custom.min.js"></script>--%>
<%--<script src="../Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>--%>
<%--<script src="../../Content/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
<%--<script src="../../Content/jquery-ui-1.10.0.min.js" type="text/javascript"></script>--%>
<%-- <script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>--%>
<%--<script src="../Scripts/modernizr-1.7.min.js" type="text/javascript"></script>--%>
<%--<script src="../../Content/modernizr-2.6.2.js" type="text/javascript"></script>
<script src="../../Content/jtablesite.js" type="text/javascript"></script>
<!-- A helper library for JSON serialization -->
<script type="text/javascript" src="../../Content/jtable/external/json2.js"></script>
<!-- Core jTable script file -->
<script type="text/javascript" src="../../Content/jtable/jquery.jtable.js"></script>
<script type="text/javascript" src="../../Content/jtable/localization/jquery.jtable.tr.js"></script>
<!-- ASP.NET Web Forms extension for jTable -->
<script type="text/javascript" src="../../Content/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>--%>

<%: System.Web.Optimization.Styles.Render("~/bundles/jtablecss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>
 <script src="../../Content/jtable/jquery.jtable.js"></script>
 <style>
      cls_status {
        padding: 0 0 0 0;text-align: center;vertical-align: middle;
    }
      cls_left {
       text-align: left;
    }
      cls_right {
        text-align: right;
    }
 </style>





   <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="card-body"> 
                                 <asp:Literal ID="lit_paneltitle" runat="server"></asp:Literal> </h3>
							<div class="card-toolbar" style="width:400px">
                                
							 <asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static" style="width:125px">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                    LoadingText="[Loading status...]" Category="Name" PromptText="ALL ACTIVE" PromptValue="0"
                    ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetStatusByTypeId"
                    ParentControlID="ddlTypeofRequest" />	
                                <input type="text" name="txtsearch" id="txtsearch" style="width:125px;margin-bottom:0px"  class="form-control" maxlength="50" /> 
                                <asp:LinkButton ClientIDMode="Static" SkinID="btnSearch" runat="server" id="LoadTicket" Text="Search" style="margin-bottom:0px"  />
                                   
							</div>
						</div>
						<div class="card-body">
                            <div class="row">


                                
<%--<div class="form-group row" >
    <div id="map_canvas" style="width: 99%; height: 400px" ></div>
</div>--%>

<script type="text/javascript">
    $(document).ready(function () {
        //initialize();
    });

    function getbuttonid(btn) {

        var v = $(btn).attr("value");
        //onwaymail
        //alert($(btn).attr("value"));
        url = '/WF/DC/FLSJlist.aspx/onwaymail';
        data = "{ticketno:'" + v + "'}";
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: OnCheckUserNameAvailabilitySuccess,
            error: OnCheckUserNameAvailabilityError
        });
        function OnCheckUserNameAvailabilitySuccess(response) {

            //$("#lblMessage").text("Configuration saved successfully");
        }
        function OnCheckUserNameAvailabilityError(xhr, ajaxOptions, thrownError) {
            //alert(xhr.statusText);
        }
        return false;
    }
    function getParameterByName(name, url) {
        if (!url) {
            url = window.location.href;
        }
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
  
</script>
 <asp:LinkButton ID="BtnS" runat="server" ToolTip="Quick Search" ClientIDMode="Static"
                  ImageAlign="TextTop" SkinID="BtnLinkSearch" Visible="false" style="display:none;visibility:hidden;" />

            
<div class="form-group row" style="display:none;visibility:hidden;" >
             <div class="col-md-4">
                                    <label class="col-sm-3 control-label">Search:</label>
                                      <div class="col-sm-7"></div>   
				</div>
     <div class="col-md-4" style="display:none;visibility:hidden;" >
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%>:</label>
                                      <div class="col-sm-8 form-inline"> 
                                          <asp:DropDownList ID="ddlCompany" runat="server" ClientIDMode="Static" SkinID="ddl_70"
                                                                                                      CausesValidation="false"></asp:DropDownList>
                                          <%--<asp:Button ID="BtnHistory" runat="server" SkinID="btnDefault" OnClick="BtnHistory_Click" Text="History" CausesValidation="false" />--%>


                                          <asp:LinkButton ID="LinkButton1" SkinID="BtnLinkHistory"  Text="History" runat="server" OnClick="BtnHistory_Click" Visible="false"></asp:LinkButton>
                                                                                 
                                          <asp:Label ID="lblForPopUp" runat="server"></asp:Label>  

                                           <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlCompany"
                    BehaviorID="ccdCom" Category="company" PromptText="Please select..." PromptValue="0"
                    ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
					</div>
				</div>
    
     <div class="col-md-4">
          <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Status%>:</label>
         <div class="col-sm-9">  </div>
         </div>
     <div class="col-md-4" style="display:none;visibility:hidden;">
         <label class="col-sm-4 control-label">Ticket Ref:</label>
                                      <div class="col-sm-8"><input type="text" name="ticketno" id="ticketno" style="width:75px"  class="form-control"  maxlength="20"/>
					</div>
    </div>
    <div class="col-md-4 form-inline">
        
          <asp:HyperLink SkinID="btnLogNewTicket" runat="server" Text="Log New Job" id="Img1" NavigateUrl="~/WF/DC/DCNavigation.ashx?type=FLS" Style="vertical-align:bottom"/>
        </div>
                </div>
        <div class="col-md-4" style="display:none;visibility:hidden;">
                                        <div class="col-sm-4 control-label" id="tdRequestTypeLable" runat="server"><%= Resources.DeffinityRes.RequestType%>: </div>
                                     <div id="tdRequestTypeField" class="col-sm-8" runat="server">
                        <asp:DropDownList ID="ddlRequestType" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                        <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestType" ParentControlID="ddlCompany"
                            BehaviorID="ccdTypeNew" Category="type" PromptText="Please select..." PromptValue="0"
                            ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestTypeByCustomer" LoadingText="[Loading...]" />
                   
					</div>
				</div>
<div class="form-group row" style="display:none;visibility:hidden;">
        
     
          <div class="col-md-4" id="tdRTpanel" runat="server">
                                       <label class="col-sm-4 control-label" id="tdRTLable" runat="server"> <%= Resources.DeffinityRes.RequestType%>:</label>
                                      <div class="col-sm-8" id="tdRTField" runat="server">  <asp:DropDownList ID="ddlTypeofRequest" runat="server" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest"
                    LoadingText="[Loading request...]" BehaviorID="ccdT" Category="type" PromptText="ALL"
                    PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetTypeofRequest"
                    ClientIDMode="Static" />
					</div>
				</div>

         <div class="col-md-4" ID="pnlAccessNo" runat="server" Visible="false">
                                       <label class="col-sm-5 control-label"> <%= Resources.DeffinityRes.AccessNumber%>:</label>
                                      <div class="col-sm-7"> <input type="text" name="accessno" id="txtAccessNo" style="width:75px" class="form-control"  maxlength="50"/>
					</div>
				</div>
    </div>


<div class="row " style="display:none;visibility:hidden;">
   <div class="col-md-4">
       </div>
     <div class="col-md-8 form-inline pull-right" style="text-align:right;">
        
       &nbsp;
         <span id="pnlExport" runat="server" visible="false" >
    <asp:LinkButton ID="imgBtnExportToExcel" runat="server" SkinID="btnExporttoExcel" Text="Export to Excel"  OnClick="imgBtnExportToExcel_Click" Style="display:none;visibility:hidden;" />
    <asp:LinkButton ID="imgBtnExportToPDF" runat="server" SkinID="btnExporttoPDF" Text="Export to PDF" OnClick="imgBtnExportToPDF_Click" Style="display:none;visibility:hidden;" />
     <asp:LinkButton ID="ImgConfig" SkinID="BtnLinkConfig" Text="Config" runat="server" Style="vertical-align:baseline;display:none;visibility:hidden;" ToolTip="Configure Grid View" />
</span>
         </div>
    </div>
<script type="text/javascript">
    Sys.Application.add_load(MyLoad);
    function MyLoad() {
        // SDServiceRequestTable
        //alert("UC is loaded.")
        $find("ccdT").add_populated(onPopulated);
    }
    function pageLoad(sender, args) {
        var be = $find("ccdT");
        be.add_populated(onPopulated);
        loadConfigField();
        load_data();
        //setStatusBackColor();
    }
    function onPopulated() {
        //$get("ddlTypeofRequest").disabled = true;

    }
                </script>
 <ajaxToolkit:ModalPopupExtender ID="popIssues" BackgroundCssClass="modalBackground"
                    runat="server" PopupControlID="PaneladdNew" TargetControlID="ImgConfig" />
<ajaxToolkit:ModalPopupExtender ID="ModelPopUpForHistoryRecords" BackgroundCssClass="modalBackground"
    runat="server" PopupControlID="PnlHistoryRecords" TargetControlID="lblForPopUp"></ajaxToolkit:ModalPopupExtender>
 

<%--<div class="filtering">
    

</div>--%>

 <%-- <div id="QSearch">
               <%-- <QSCtrl:QuickSearchCtrl ID="QCCntl" runat="server"></QSCtrl:QuickSearchCtrl>
                     </div>--%>
<input id="hdId1" type="hidden" />
            <input id="hduId1" type="hidden" />
            <input id="hdUserName1" type="hidden" />
 <div id="dataContainer" style="display:none;visibility:hidden;">
 <div id="divLogin" class="login">
                <div>

                    <input id="txtNickName" type="text" class="textBox" value="<%= Session["uname"]%>" />
                    <input id="txtID" type="text" class="textBox" value="<%= Session["userid"]%>" />

                </div>

            </div>

          <%-- <div id="divChat" class="chatRoom">
                <div  class="chat-group">

 </div>
                </div>--%>
     </div>
<div class="row">
     <div class="col-md-12">
          
                  
<div id="SDServiceRequestTable" style="width: auto; overflow: auto;">
</div>
 
<div id="FLSTableContainer">
</div>
<div id="SDFaultsTable">
</div>
<div id="WithoutFLSTableContainer">
</div>
<div id="FLSOrderTableContainer">
</div>
<div id="AccessControlJtable"></div>
    </div>
    </div>






<asp:Panel ID="PnlHistoryRecords" runat="server" BackColor="White" Width="800Px"
                                                        BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none"
                                               ScrollBars="Auto" ClientIDMode="Static">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelHistoryRecords">
                                   <ProgressTemplate>
                                       <asp:Label ID="lblLoadingImageServices" runat="server" SkinID="Loading"></asp:Label>
                                   </ProgressTemplate>
     </asp:UpdateProgress>

      <asp:UpdatePanel ID="UpdatePanelHistoryRecords" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

                                              <div class="form-group row">
                                                          <div class="col-md-10">
                                                              <strong>Historical calls </strong> 
                                                              <hr class="no-top-margin" />
                                                          </div>
                                                           <div class="col-md-2" style="float:right;text-align:right;">
                                                                 <asp:LinkButton ID="LinBtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" SkinID="BtnLinkCancel"></asp:LinkButton>
                                                           </div>
                                              </div>
                                              <div class="form-group row">
                                                   <div class="col-md-12">
                                                       <asp:GridView ID="GridRecords" runat="server" OnPageIndexChanging="GridServices_PageIndexChanging"
                                                            Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                                                                                                  EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>"> 
                                                      <Columns>
                                                          <asp:TemplateField HeaderText="Ticket Ref">
                                                              <ItemTemplate>
                                                                  <asp:HyperLink if="LinkCallid" runat="server" Text='<%#Eval("TicketRef") %>' Font-Bold="true"
                                                                      NavigateUrl='<%#"~/WF/DC/FLSForm.aspx?callid=" + Eval("TicketRef1")+"&SDID="+Eval("TicketRef1") %>' ForeColor="Green"></asp:HyperLink>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Requester Name">
                                                              <ItemTemplate>
                                                                   <asp:Label ID="LblRequesterName" runat="server" Text='<%#Bind("RequesterName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Details">
                                                              <ItemTemplate>
                                                                 <asp:Label ID="LblDetails" runat="server" Text='<%#Bind("Details") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Type of Request">
                                                              <ItemTemplate>
                                                                 <asp:Label ID="LblTypeofRequest" runat="server" Text='<%#Bind("TypeofRequest") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Status">
                                                              <ItemTemplate>
                                                                 <asp:Label ID="LblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Logged Date/Time">
                                                              <ItemTemplate>
                                                                <asp:Label ID="LblLoggedDateTime" runat="server" Text='<%#Bind("LoggedDateTime") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>

                                                      </Columns>
                                                  </asp:GridView>
                                                    </div>
                                              </div>
              </ContentTemplate>
          </asp:UpdatePanel>


                                          </asp:Panel>






<asp:Panel ID="PaneladdNew" runat="server" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none" ScrollBars="Auto" ClientIDMode="Static">
    <asp:UpdateProgress runat="server" ID="PaneladdNew2" DisplayAfter="10" AssociatedUpdatePanelID="panelupdate" ClientIDMode="Static">
        <ProgressTemplate>
            <asp:Label ID="image1" runat="server" SkinID="Loading" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="panelupdate" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
        <ContentTemplate>

            <table>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblMessage" ClientIDMode="Static" runat="server" ForeColor="Green"></asp:Label></div>
                        <asp:Label ID="lblscreen" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                    </td>
                    <td></td>
                    <td>

                        <div style="text-align: right;">
                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" SkinID="BtnLinkCancel" OnClick="lnkCancel_Click" />
                        </div>

                    </td>
                </tr>
                <tr>
                    <td>
                        <b><%= Resources.DeffinityRes.CurrentColumnsintheGrid%></b>
                        <br />
                        <asp:ListBox ID="gridlist" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                    </td>
                    <td>
                        <button id="btn" type="button">Up</button>
                        <button id="Btn2" type="button">Down</button>
                        <button id="save" type="button">Save</button>

                        <br />
                        <asp:Button ID="add" Text="<<Add Field" runat="server" OnClick="add_Click" />

                        <br />
                        <asp:Button ID="remove" Text="Remove Field>>" runat="server" OnClick="remove_Click" />
                    </td>
                    <td>
                        <b><%= Resources.DeffinityRes.AdditionalFields%></b>
                        <br />
                        <asp:ListBox ID="list" runat="server" ClientIDMode="Static" Height="300" Width="200"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="add" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="remove" EventName="Click" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Panel>

<script type="text/javascript">
    function loadConfigField() {
        $("#btn").click(function () {

            var listbox = document.getElementById("gridlist");
            var selIndex = listbox.selectedIndex;
            if (-1 == selIndex) { alert("Please select an option to move."); return; }
            var increment = -1;
            if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }

            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;
            listbox.selectedIndex = selIndex + increment;
        });
        $("#Btn2").click(function () {

            var listbox = document.getElementById("gridlist");
            var selIndex = listbox.selectedIndex;
            if (-1 == selIndex) { alert("Please select an option to move."); return; }
            var increment = 1;
            if ((selIndex + increment) < 0 || (selIndex + increment) > (listbox.options.length - 1)) { return; }
            var selValue = listbox.options[selIndex].value;
            var selText = listbox.options[selIndex].text;
            listbox.options[selIndex].value = listbox.options[selIndex + increment].value;
            listbox.options[selIndex].text = listbox.options[selIndex + increment].text;
            listbox.options[selIndex + increment].value = selValue;
            listbox.options[selIndex + increment].text = selText;
            listbox.selectedIndex = selIndex + increment;
        });
        $("#save").click(function () {

            var listbox = document.getElementById("gridlist");

            var index1 = '';


            for (var i = 0; i < listbox.length; i++) {

                var selValue = listbox.options[i].value;

                index1 = index1 + selValue + ",";
            }
            url = '/WF/DC/webservices/DCServices.asmx/InsertSDColumnPosition';
            data = "{value:'" + index1 + "'}";
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: OnCheckUserNameAvailabilitySuccess,
                error: OnCheckUserNameAvailabilityError
            });
            function OnCheckUserNameAvailabilitySuccess(response) {

                $("#lblMessage").text("Configuration saved successfully");
            }
            function OnCheckUserNameAvailabilityError(xhr, ajaxOptions, thrownError) {
                alert(xhr.statusText);
            }
        });
    }
</script>
<br />
<asp:Label ID="lblFooter" runat="server"></asp:Label>
<script type="text/javascript">
    function GetType(id) {
        if (id == "1")
            return "Delivery";
        else if (id == "2")
            return "Permit to Work";
        else if (id == "3")
            return "Access Control";
        else if (id == "4")
            return "Collection";
        else if (id == "6")
            return "FLS";
        else
            return "ALL ACTIVE";
    }


    function New() {
        var typeVal = $('#ddlTypeofRequest').val();
        var typeText = $("#ddlTypeofRequest option:selected").text();
        //debugger;
        if (typeVal == 0) {
            alert('Please select request type');
        }
        else {
            window.location = "/WF/DC/DCNavigation.ashx?type=FLS";// + typeText;
        }
    }


    $(document).ready(function () {

        load_data();
        // window.setInterval("reFresh()", 60000);


    });

    function load_data() {

        var type = getQuerystring('type');
        var Actrlid = getQuerystring('Actrlid');
        //order table
        //$('#FLSOrderTableContainer').hide();
        //$('#FLSTableContainer').hide();
        $('#AccessControlJtable').hide();
        $('#WithoutFLSTableContainer').hide();
        $('#SDServiceRequestTable').hide();
        //$('#SDFaultsTable').hide();
        var ccdType1 = $find('ccdT');
        $find('ccdT').add_populated(onPopulated);
        //debugger;
        if (type.toLowerCase() == "delivery") {
            ccdType1.set_SelectedValue('1');
            $('#WithoutFLSTableContainer').show();
            $('#AccessControlJtable').hide();
            $('#SDServiceRequestTable').hide();
        }
        else if (type.toLowerCase() == "permittowork") {
            ccdType1.set_SelectedValue('2');
            $('#WithoutFLSTableContainer').show();

        }
        else if (type.toLowerCase() == "accesscontrol") {
            ccdType1.set_SelectedValue('3');
            $('#WithoutFLSTableContainer').hide();//kishore
            $('#AccessControlJtable').show();
        }
        else if (type.toLowerCase() == "collection") {
            ccdType1.set_SelectedValue('4');
        }
        else if (type.toLowerCase() == "fls") {
            ccdType1.set_SelectedValue('6');
            //debugger;
            $('#SDServiceRequestTable').show();
            $('#AccessControlJtable').hide();
            //$('#SDFaultsTable').show();
            //$('#FLSOrderTableContainer').hide();
            //$('#FLSTableContainer').hide();
        }
        else if (Actrlid != null)
            ccdType1.set_SelectedValue('3');
        else
            ccdType1.set_SelectedValue('0');

        $('#WithoutFLSTableContainer').jtable({
            //title: 'List of Tickets',
            paging: true, //Enables paging
            pageSize: 30, //Actually this is not needed since default value is 10.
            sorting: true,
            useBootstrap: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: '/WF/DC/FLSJlist.aspx/Get'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: '.',
                noDataAvailable: 'No tickets available',
                addNewRecord: '+ Add new',
                editRecord: 'Edit',
                areYouSure: 'Are you sure?',
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                deleteText: 'Delete',
                deleting: 'Deleting',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}',
                pagingInfo: 'Showing {0} to {1} of {2} records',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {

                CallID: {
                    key: true,
                    width: '10%',
                    title: 'Ticket ref',
                    display: function (data) {
                        return '<a href="DCNavigation.ashx?CCID=' + data.record.CCID + '&callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>' + data.record.CCID + '</b></a>';
                    }
                },
                Company: {
                    title: '<%= Resources.DeffinityRes.Customer%>',
                    width: '15%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },
                Description: {
                    title: '<%= Resources.DeffinityRes.Description%>',
                    sorting: true,
                    width: '15%',
                    display: function (data) {
                        var dv = data.record.Description;
                        var len = dv.length;
                        var aptext = '';
                        if (len > 75) {
                            aptext = dv.substr(0, 75);

                            aptext = aptext + ' <a href="DCNavigation.ashx?CCID=' + data.record.CCID + '&callid=' + data.record.CallID + '&type=' + data.record.RequestType + '">Read more...</a>';
                        }
                        else {
                            aptext = dv;
                        }

                        return aptext;
                    }
                },
                ScheduleDate: {
                    title: '<%= Resources.DeffinityRes.ScheduleDate%>',
                    width: '7%'
                },

                Name: {
                    title: '<%= Resources.DeffinityRes.Requester%>',
                    width: '10%'
                },

                RequestType: {
                    title: '<%= Resources.DeffinityRes.RequestType%>',
                    sorting: true,
                    width: '10%'
                },
                Status: {
                    title: '<%= Resources.DeffinityRes.Status%>',
                    width: '10%',
                    display: function (data) {
                        return '<b>' + data.record.Status + '</b>';
                    }
                },
                Note: {
                    title: '<%= Resources.DeffinityRes.Notes%>',
                    sorting: false,
                    width: '10%'
                }

            }
        });

        $('#AccessControlJtable').jtable({
            //title: 'List of Tickets',
            paging: true, //Enables paging
            pageSize: 30, //Actually this is not needed since default value is 10.
            sorting: true,
            useBootstrap: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: '/WF/DC/FLSJlist.aspx/Get1'
            },
            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: '.',
                noDataAvailable: 'No tickets available',
                addNewRecord: '+ Add new',
                editRecord: 'Edit',
                areYouSure: 'Are you sure?',
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                deleteText: 'Delete',
                deleting: 'Deleting',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}',
                pagingInfo: 'Showing {0} to {1} of {2} records',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {

                CallID: {
                    key: true,
                    width: '10%',
                    title: 'Ticket ref',
                    display: function (data) {
                        return '<a href="DCNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>' + data.record.CallID + '</b></a>';
                    }
                },
                Company: {
                    title: '<%= Resources.DeffinityRes.Customer%>',
                    width: '15%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },

                ScheduleDate: {
                    title: '<%= Resources.DeffinityRes.ScheduledDate%>',
                    width: '7%'

                },
                Note: {
                    title: '<%= Resources.DeffinityRes.Notes%>',
                    sorting: false,
                    width: '10%'
                },
                Name: {
                    title: '<%= Resources.DeffinityRes.Requester%>',
                    width: '15%'
                },
                PurposeofVisit: {
                    title: '<%= Resources.DeffinityRes.PurposeofVisit%>',
                    width: '15%'
                },
                Status: {
                    title: '<%= Resources.DeffinityRes.Status%>',
                    width: '10%',
                    display: function (data) {
                        return '<b>' + data.record.Status + '</b>';
                    }
                }
            }
        });

        function serviceDeskFields() {

            var fields = {

            };
            var url = document.URL;
            $.ajax({
                type: "POST",
                url: "/WF/DC/FLSJlist.aspx/GetColumns",
                data: '{url:"' + url + '"}',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function OnSuccess(response) {
                    //debugger;
                    var r = 0;
                    var myArray = response.d;
                    var k = 0;
                    var myArrayNew = response.d;
                    var arrayLength = myArray.length;

                    //var r1 = 0;
                    //var myArray1 = response.d;
                    //var k1 = 0;
                    //var myArrayNew1 = response.d;
                    //var arrayLength1 = myArray.length;
                    for (var i = 0; i < arrayLength; i++) {

                        //var f1 = myArray[r].FieldName;

                        fields[myArray[i].FieldName] = {
                            title: myArray[i].ColumnName,
                            listClass: function () {
                                return "cls_status";
                            },
                            display: function (data) {
                                if (arrayLength == r) {
                                    r = 0;
                                }
                                var result = myArray[r].FieldName;
                                r++;
                                if (result == "CallID") {
                                    if (data.record.Priority == "High")
                                        return '<a href="/WF/DC/DCNavigation.ashx?CCID=' + data.record.CCID + '&callid=' + data.record.CallID + '&type=FLS"><b>' + data.record.CCID + '</b></a> <i class="fa fa-flag" style="color: red;"></i>';
                                    else
                                        return '<a href="/WF/DC/DCNavigation.ashx?CCID=' + data.record.CCID + '&callid=' + data.record.CallID + '&type=FLS"><b>' + data.record.CCID + '</b></a>';

                                }
                                else if (result == "RequestersAddress") {
                                    return "<label style='float: left;'>" + data.record.RequestersAddress + "</label>";
                                }
                                else if (result == "Details") {
                                    var dv = data.record.Details;
                                    var len = dv.length;
                                    var aptext = '';
                                    if (len > 75) {
                                        aptext = dv.substr(0, 75);

                                        aptext = "<label style='float: left;'>" + aptext + ' <a href="/WF/DC/DCNavigation.ashx?CCID=' + data.record.CCID + '&callid=' + data.record.CallID + '&type=FLS">Read more...</a>' + "</label>";
                                    }
                                    else {
                                        aptext = "<label style='float: left;'>" + dv + "</label>";
                                    }
                                    return aptext;
                                }
                                else if (result == "InHandSLA") {

                                    var inHandSLAStatus = data.record.InHandSLA;

                                    if (inHandSLAStatus == "False" || inHandSLAStatus == "Red") {
                                        return '<label style="color:red;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (inHandSLAStatus == "True" || inHandSLAStatus == "Green") {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (inHandSLAStatus == "Amber") {
                                        return '<label style="color:yellow;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                }
                                else if (result == "ResolutionSLA") {
                                    var resolutionSLA = data.record.ResolutionSLA;
                                    if (resolutionSLA == "False" || resolutionSLA == "Red") {
                                        return '<label style="color:red;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (resolutionSLA == "True" || resolutionSLA == "Green") {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else if (resolutionSLA == "Amber") {
                                        return '<label style="color:yellow;"><i class="fa fa-circle"></i></label>';
                                    }
                                    else {
                                        return '<label style="color:green;"><i class="fa fa-circle"></i></label>';
                                    }
                                }
                                else if (result == "Image") {
                                    return '<img style="height:50px;margin-left:auto;margin-right:auto;display:block;" src="../../WF/Admin/ImageHandler.ashx?v=1&type=user&id='
                                        + data.record.AssignedTechnicianID + '" title="' + data.record.AssignedTechnician + '"/>';
                                }
                                else if (result == "Email") {
                                    return '<a href=mailto:' + data.record.AssignedTechnicianEmail + '><i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="linecons-mail" title="' + data.record.AssignedTechnicianEmail + '"></i></a>';
                                }
                                else if (result == "Contact") {
                                    return '<i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="fa fa-phone" title="' + data.record.AssignedTechnicianContact + '"></i>';
                                }
                                //else if (result == "Chat") {
                                //    return '<a href="' + '/WF/DC/DCChat.aspx?callid=' + data.record.CallID + '&SDID=' + data.record.CallID + '" id="' + data.record.AssignedTechnicianID + ' name="' + data.record.AssignedTechnician + '"> <span class="user-status is-online" ></span><em><i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="linecons-comment" title="' + data.record.AssignedTechnician + '"></i></em></a>'
                                //    //return '<a href="#dataContainer" id="' + data.record.AssignedTechnicianID + '" onclick="getid(this)" name="' + data.record.AssignedTechnician + '"> <span class="user-status is-online" ></span><em><i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="linecons-comment" title="' + data.record.AssignedTechnician + '"></i></em></a>'
                                //    //return '<i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="fa fa-phone" title="' + data.record.AssignedTechnician + '"></i>';
                                //}
                                else if (result == "I’m On My Way") {
                                    return '<button class="btn btn-secondary" value="' + data.record.CallID + '" onclick="getbuttonid(this)">I’m On My Way</button>'
                                    //return '<a href="" id="' + data.record.AssignedTechnicianID + '" onclick="getid(this)" name="' + data.record.AssignedTechnician + '"> <span class="user-status is-online" ></span><em><i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="linecons-comment" title="' + data.record.AssignedTechnician + '"></i></em></a>'
                                    //return '<i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="fa fa-phone" title="' + data.record.AssignedTechnician + '"></i>';
                                }
                                else if (result == "Status") {

                                    var s = data.record.Status;
                                    var retval = data.record.Status;
                                    var cls = 'color: white;font-weight: bold;text-align: center;vertical-align: middle;height: 50px;padding-top: 20px;';
                                    if (s == 'New')
                                        retval = '<div class="statuscls" style="background-color:#00B0F0;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Cancelled')
                                        retval = '<div class="statuscls" style="background-color:#44546a;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Resolved')
                                        retval = '<div class="statuscls" style="background-color:#00B0F0;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Job Complete')
                                        retval = '<div class="statuscls" style="background-color:#0070C0;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Scheduled')
                                        retval = '<div class="statuscls" style="background-color:#92D050;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Awaiting Schedule')
                                        retval = '<div class="statuscls" style="background-color:#B4c6e7;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Arrived')
                                        retval = '<div class="statuscls" style="background-color:#0070C0;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Customer Not Responding')
                                        retval = '<div class="statuscls" style="background-color:#FF0000;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Feedback Submitted')
                                        retval = '<div class="statuscls" style="background-color:#002060;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Feedback Received')
                                        retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Quote Rejected')
                                        retval = '<div class="statuscls" style="background-color:#002060;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Quote Accepted')
                                        retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Awaiting Information')
                                        retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Waiting On Parts')
                                        retval = '<div class="statuscls" style="background-color:#002060;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Authorised')
                                        retval = '<div class="statuscls" style="background-color:#ED7D31;' + cls + '">' + data.record.Status + '</div>';
                                    else if (s == 'Quote Sent')
                                        retval = '<div class="statuscls" style="background-color:#B4c6e7;' + cls + '">' + data.record.Status + '</div>';
                                    return retval;
                                    //return '<button class="btn btn-secondary" value="' + data.record.Status + '" onclick="getbuttonid(this)">I’m On My Way</button>'
                                    //return '<a href="" id="' + data.record.AssignedTechnicianID + '" onclick="getid(this)" name="' + data.record.AssignedTechnician + '"> <span class="user-status is-online" ></span><em><i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="linecons-comment" title="' + data.record.AssignedTechnician + '"></i></em></a>'
                                    //return '<i style="font-size:2em;margin-left:auto;margin-right:auto;display:block;" class="fa fa-phone" title="' + data.record.AssignedTechnician + '"></i>';
                                }
                                else {
                                    return data.record[result];
                                }
                            }
                        };

                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

            return fields;
        }
        $('#SDServiceRequestTable').jtable({
            //title: 'List of Tickets',
            paging: true,
            width: '100%',//Enables paging
            pageSize: 10, //Actually this is not needed since default value is 10.
            sorting: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: '/WF/DC/FLSJlist.aspx/GetSDList'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: '.',
                noDataAvailable: 'No tickets have been logged',
                addNewRecord: '+ Add new',
                editRecord: 'Edit',
                areYouSure: 'Are you sure?',
                deleteConfirmation: 'This record will be deleted. Are you sure?',
                save: 'Save',
                saving: 'Saving',
                cancel: 'Cancel',
                deleteText: 'Delete',
                deleting: 'Deleting',
                error: 'Error',
                close: 'Close',
                cannotLoadOptionsFor: 'Can not load options for field {0}',
                pagingInfo: 'Showing {0} to {1} of {2} records',
                canNotDeletedRecords: 'Can not deleted {0} of {1} records',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: serviceDeskFields(),

            rowInserted: function (event, data) {
                setStatusBackColor();
            }
        });
        var cnt = 0;
        //Re-load records when user click 'load records' button.
        $('#LoadTicket').click(function (e) {
            e.preventDefault();

            var type = document.getElementById('ddlTypeofRequest');
            var status = document.getElementById('ddlStatus');

            var company = document.getElementById('ddlCompany');
            var requestType = document.getElementById('ddlRequestType');
            var accessNo = $('#txtAccessNo').val();
            if (accessNo != null) {
                accessNo = accessNo;
            }
            else {
                accessNo = "";
            }
            var qval = getQuerystring('type')
            var url = document.URL;
            if (qval.toLowerCase() == 'fls') {
                //$('#FLSTableContainer').jtable('load', {
                //    ticketno: $('#ticketno').val(),
                //    type: GetType(ccdType.get_SelectedValue()),
                //    status: status.options[status.selectedIndex].innerHTML,
                //    accessno: accessNo

                //});
                // $('#SDServiceRequestTable').empty();
                $('#SDServiceRequestTable').jtable('load', {
                    company: company.options[company.selectedIndex].innerHTML,
                    ticketno: $('#ticketno').val(),
                    type: GetType(ccdType1.get_SelectedValue()),
                    status: status.options[status.selectedIndex].innerHTML,
                    accessno: accessNo,
                    requestType: requestType.options[requestType.selectedIndex].innerHTML,
                    url: url,
                    txtsearch: $('#txtsearch').val()
                });
                //$('#SDFaultsTable').jtable('load', {
                //    ticketno: $('#ticketno').val(),
                //    type: GetType(ccdType.get_SelectedValue()),
                //    status: status.options[status.selectedIndex].innerHTML,
                //    accessno: accessNo,
                //    requestType:"faults"

                //});
                //debugger;
                if (cnt == 0) {
                    $('#SDServiceRequestTable table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                    $("#SDServiceRequestTable table:first").wrap("<div id='divChat' class='chatRoom'><div  class='chat-group'><div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div></div></div>");
                    $("#SDServiceRequestTable").attr("style", "");
                    //$("sticky-table-header fixed-solution").
                    //$("div").remove(".sticky-table-header");

                    cnt = cnt + 1;


                }

                setStatusBackColor();

            }
            //else if (qval.toLowerCase() == 'delivery' || qval.toLowerCase() == 'permittowork') {
            //    cnt = 0;
            //   // $('#WithoutFLSTableContainer').empty();
            //    $('#WithoutFLSTableContainer').jtable('load', {
            //        company: company.options[company.selectedIndex].innerHTML,
            //        ticketno: $('#ticketno').val(),
            //        type: GetType(ccdType1.get_SelectedValue()),
            //        status: status.options[status.selectedIndex].innerHTML,
            //        accessno: accessNo
            //    });


            //    //debugger;
            //    if (cnt == 0) {
            //        $('#WithoutFLSTableContainer table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
            //        $("#WithoutFLSTableContainer table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
            //        $("#WithoutFLSTableContainer").attr("style", "");
            //        //$("sticky-table-header fixed-solution").
            //        //$("div").remove(".sticky-table-header");

            //        cnt = cnt + 1;
            //    }
            //}
            ////else
            ////{
            ////    //$('#AccessControlJtable').empty();

            ////    $('#AccessControlJtable').jtable('load', {
            ////        company: company.options[company.selectedIndex].innerHTML,
            ////        ticketno: $('#ticketno').val(),
            ////        type: GetType(ccdType1.get_SelectedValue()),
            ////        status: status.options[status.selectedIndex].innerHTML,
            ////        accessno: accessNo
            ////    });
            ////    cnt = 0;
            ////    //debugger;
            ////    if (cnt == 0) {
            ////        $('#AccessControlJtable table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
            ////        $("#AccessControlJtable table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
            ////        $("#AccessControlJtable").attr("style", "");
            ////        //$("sticky-table-header fixed-solution").
            ////        //$("div").remove(".sticky-table-header");

            ////        cnt = cnt + 1;
            ////    }
            ////}
            //$('#FLSOrderTableContainer').jtable('load', {
            //    ticketno: $('#ticketno').val(),
            //    type: GetType(ccdType.get_SelectedValue()),
            //    status: status.options[status.selectedIndex].innerHTML,
            //    accessno: accessNo
            //});

            setStatusBackColor();
        });
        //Load all records when page is first shown
        $('#LoadTicket').click();

        setStatusBackColor();
    }

</script>

<%-- 
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>--%>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
        setStatusBackColor();
        //grid_responsive_display();
        function setStatusBackColor() {
            $(window).load(function () {

                $('.statuscls').each(function () {

                    var s = $(this).html();
                    if (s == 'New')
                        $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Cancelled')
                        $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Resolved')
                        $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Job Complete')
                        $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Scheduled')
                        $(this).closest("td").css({ "background-color": "#92D050", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Awaiting Schedule')
                        $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Arrived')
                        $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Customer Not Responding')
                        $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                    else if (s == ' Feedback Submitted')
                        $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Feedback Received')
                        $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Quote Rejected')
                        $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Quote Accepted')
                        $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Awaiting Information')
                        $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Waiting On Parts')
                        $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Authorised')
                        $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Authorised')
                        $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                    else if (s == 'Quote Sent')
                        $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                });
            });
            $('.statuscls').each(function () {

                var s = $(this).html();
                if (s == 'New')
                    $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Cancelled')
                    $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Resolved')
                    $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Job Complete')
                    $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Scheduled')
                    $(this).closest("td").css({ "background-color": "#92D050", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Awaiting Schedule')
                    $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Arrived')
                    $(this).closest("td").css({ "background-color": "#0070C0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Customer Not Responding')
                    $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Feedback Submitted')
                    $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Feedback Received')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Rejected')
                    $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Accepted')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Awaiting Information')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Waiting On Parts')
                    $(this).closest("td").css({ "background-color": "#002060", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Authorised')
                    $(this).closest("td").css({ "background-color": "#ED7D31", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Sent')
                    $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
            });


        }
        $(window).load(function () {

            $("div").remove(".sticky-table-header");
            $("span").remove(".jtable-page-info");
            //jtable_css();
            //grid_responsive();
            $(".dropdown-menu li")
                .find("input[type='checkbox']")
                .prop('checked', 'checked').trigger('change');
            //$(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });


           // setStatusBackColor();

    </script>

 

<style type="text/css">
    td.cls_status{
        padding: 0 0 0 0;text-align: center;vertical-align: middle;
    }

    cls_status {
        padding: 0 0 0 0;text-align: center;vertical-align: middle;
    }
     /*span.statuscls td{
        color: white;
    font-weight: bold;
    text-align: center;
    vertical-align: middle;
    }*/
    /*div.statuscls{
        color: white;
    font-weight: bold;
    text-align: center;
    vertical-align: middle;
    }*/
            .li12{
                margin-top: 0px;
                margin-bottom: 0px;
                border: 0;
                border-top: 1px solid #eee;
            }

            .draggable {
                position: absolute;
                border: #7c807f solid 1px !important;
                width: 250px;
            }

                .draggable .header {
                    cursor: move;
                    background-color: #7c807f;
                    border-bottom: #7c807f solid 1px;
                    color: #7c807f;
                    height: 25px;
                }

                .draggable .selText {
                    color: white;
                    padding: 1em;
                    font-size: medium;
                    font-family: cursive;
                }

                .draggable .messageArea {
                    /*width: 250px;*/
                    overflow-y: scroll;
                    height: 230px;
                    border-bottom: #999C99 solid 1px;
                    background-color: white;
                }

                    .draggable .messageArea .message {
                        padding: 4px;
                    }

                .draggable .buttonBar {
                    width: 250px;
                    padding: 4px;
                }

                    .draggable .buttonBar .msgText {
                        width: 180px;
                    }

                    .draggable .buttonBar .button {
                        margin-left: 4px;
                        width: 55px;
                    }


        </style>


               
                                </div>
                </div>
            </div>