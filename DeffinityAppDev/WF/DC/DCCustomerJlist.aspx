<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="DCCustomerJlist1" EnableEventValidation="false" Codebehind="DCCustomerJlist.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server" Visible="false">
<%--<div class="navbar-header" >
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation
						<i class="fa-bars"></i></span>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav" >
    <li id="tab1" ><a href="~/WF/DC/FLSCustomerDashboard.aspx" runat="server" id="link_Dashboard"  title="Dashboard"><span>Dashboard</span> </a></li>
   
</ul>
</div>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
  
    sideMenuActive('<%= Resources.DeffinityRes.ServiceDesk%>');
</script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
  <asp:Label ID="lblPageHead" runat="server" ClientIDMode="Static"></asp:Label>
   <%-- <%= Resources.DeffinityRes.ServiceDesk%>--%>
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" Text="Back to Customer Home" NavigateUrl="~/WF/Portal/Home.aspx?customer=0"><i class="fa fa-arrow-left"></i> Back to Customer Home</asp:HyperLink>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     
    <link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />

        <div class="form-group row" style="display:none;visibility:hidden;">
                                  <div class="col-md-6" style="margin-bottom:10px">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Ticketref%>:</label>
                                      <div class="col-sm-8"> <input type="text" name="ticketno" id="ticketno" size="10px" class="form-control" />
					</div>
				</div>
            <asp:Panel ID="pnlFLStype" runat="server" Visible="false" CssClass="col-md-6" style="margin-bottom:10px">
                 <div id="td1" runat="server" class="col-sm-4 control-label">
                    Type of Request:
                </div>
                  <div id="td2" runat="server" class="col-sm-8 control-label">
                    <asp:DropDownList ID="ddlFlsTypeofRequest" runat="server" ClientIDMode="Static" SkinID="ddl_80">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlFlsTypeofRequest"
                        LoadingText="[Loading...]" BehaviorID="ccdFT" Category="type" PromptText="ALL"
                        PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestTypeByCustomerSession"
                        ClientIDMode="Static" />
                </div>
                     </asp:Panel>
           <div class="col-md-6" id="div_RT" runat="server" style="margin-bottom:10px">
            <div id="tdRTLable" runat="server" class="col-sm-4 control-label">
                    Request Type:
                </div>
                <div id="tdRTField" runat="server" class="col-sm-8">
                    <asp:DropDownList ID="ddlTypeofRequest" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                    <%--<ajaxToolkit:CascadingDropDown ID="ccdType1" runat="server" TargetControlID="ddlTypeofRequest" BehaviorID="ccdTy" LoadingText="[Loading request...]"
                        Category="type" PromptText="ALL" PromptValue="0" ServicePath="~/webservices/DCServices.asmx"
                        ServiceMethod="GetTypeofRequest" ClientIDMode="Static" />--%>
                    <ajaxToolkit:CascadingDropDown ID="ccdType1" runat="server" TargetControlID="ddlTypeofRequest"
                        LoadingText="[Loading request...]" BehaviorID="ccdT" Category="type" PromptText="ALL"
                        PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetTypeofRequest"
                        ClientIDMode="Static" />
                </div>
               </div>
             <%--</div>
             <div class="form-group row">--%>
                <asp:Panel ID="pnlAccessNo" runat="server" Visible="false" CssClass="col-md-6"  style="margin-bottom:10px">
                    <div class="col-sm-4 control-label">
                        Access Number:
                    </div>
                    <div class="col-sm-8">
                        <input type="text" name="accessno" id="txtAccessNo" class="form-control" style="width:50%" />
                    </div>
                </asp:Panel>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Status%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                        LoadingText="[Loading status...]" Category="Name" PromptText="ALL ACTIVE" PromptValue="0"
                        ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetStatusByTypeId"
                        ParentControlID="ddlTypeofRequest" />
					</div>
				</div>
           

</div>
        <div class="form-group row">
      <div class="col-md-12 pull-right" style="float:right;">
          <div style="float:right">
           <asp:Button id="LoadTicket" SkinID="btnDefault" Text="View" ClientIDMode="Static" runat="server" style="display:none;" />        
     <asp:Button ID="btnNew" SkinID="btnOrange"  runat="server" Text="Log New Ticket " OnClick="btnNew_Click" CausesValidation="false" />
     </div>          
	</div>

</div>
    <div id="FLSOrderTableContainer">
    </div>
    
    <div id="FLSTableContainer">
    </div>
    <div id="SDServiceRequestTable">
    </div>
    <div id="SDFaultsTable">
    </div>
  
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
        function onPopulated() {
            $get("ddlTypeofRequest").disabled = true;
        }

        function pageLoad(sender, args) {
            var be = $find("ccdT");
            be.add_populated(onPopulated);

            $(document).ready(function () {
                var be = $find("ccdT");
                be.add_populated(onPopulated);

                var type = getQuerystring('type');

                var ccdType = $find('ccdT');
                //order table

                $('#FLSOrderTableContainer').hide();
                $('#SDServiceRequestTable').hide();
                $('#SDFaultsTable').hide();
               
                if (type.toLowerCase() == "delivery") {
                    ccdType.set_SelectedValue('1');
                   
                }
                else if (type.toLowerCase() == "permittowork") {
                    ccdType.set_SelectedValue('2');
                   
                }
                else if (type.toLowerCase() == "accesscontrol") {
                    ccdType.set_SelectedValue('3');
                    
                }
                else if (type.toLowerCase() == "fls") {
                    debugger;
                    ccdType.set_SelectedValue('6');
                    $('#FLSTableContainer').hide();
                    //$('#FLSOrderTableContainer').show();
                    $('#SDServiceRequestTable').show();
                    $('#SDFaultsTable').show();
                   
                }
                else
                    ccdType.set_SelectedValue('0');

                $('#FLSTableContainer').jtable({
                    //title: 'List of Tickets',
                    paging: true, //Enables paging
                    pageSize: 30, //Actually this is not needed since default value is 10.
                    sorting: true, //Enables sorting
                    defaultSorting: 'RequestType ASC',
                    actions: {
                        listAction: '/WF/DC/DCCustomerJlist.aspx/Get?type=' + getQuerystring('type')
                    },

                    messages: {
                        serverCommunicationError: 'An error occured while communicating to the server.',
                        loadingMessage: 'Loading tickets...',
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
                            title: 'Ticket ref',
                            display: function (data) {
                                return '<a href="/WF/DC/DCCustomerNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                            }
                        },
                        Icon: {
                            title: '',
                            width: '1%',
                            sorting: false,
                            display: function (data) {
                                if (data.record.Status == 'New') {
                                    //return '<img src="../media/ico_indcate_blue.gif" alt=""/>';
                                    return '';
                                }
                            }
                        },
                        Company: {
                            title: 'Company',

                            display: function (data) {
                                return '<b>' + data.record.Company + '</b>';
                            }
                        },
                        Name: {
                            title: 'Name'
                        },
                        //                    Site: {
                        //                        title: 'Site'
                        //                    },
                        RequestType: {
                            title: 'Request Type'
                        },
                        Status: {
                            title: 'Status',
                            display: function (data) {
                                return '<b>' + data.record.Status + '</b>';
                            }
                        }
                    }
                });
                
                $('#SDServiceRequestTable').jtable({
                    //title: 'List of Tickets - Service Request',
                    paging: true, //Enables paging
                    pageSize: 30, //Actually this is not needed since default value is 10.
                    sorting: true, //Enables sorting
                    defaultSorting: 'CallID DESC',
                    actions: {
                        listAction: '/WF/DC/DCCustomerJlist.aspx/GetService?type=' + getQuerystring('type')
                    },

                    messages: {
                        serverCommunicationError: 'An error occured while communicating to the server.',
                        loadingMessage: 'Loading tickets...',
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
                            title: 'Ticket Ref',
                            display: function (data) {
                                return '<a href="/WF/DC/DCCustomerNavigation.ashx?callid=' + data.record.CallID + '&type=FLS"><b>TN:' + data.record.CallID + '</b></a>';
                            }
                        },
                        //Icon: {
                        //    title: '',
                        //    width: '1%',
                        //    sorting: false,
                        //    display: function (data) {
                        //        if (data.record.Status == 'New' || data.record.Status == 'Quote Requested' || data.record.Status == 'Order Received' || data.record.Status == 'Awaiting Information') {
                        //           // return '<img src="../media/ico_indcate_blue.gif" alt=""/>';
                        //            return '';
                        //        }
                        //    }
                        //},
                        LoggedDateTime: {
                            title: 'Logged Date Time'
                        },
                        Details: {
                            title: 'Details'
                            },
                        AssignedTechnician: {
                            title: 'Assigned Technician'
                        },
                        Status: {
                            title: 'Status',
                            display: function (data) {
                                return '<b>' + data.record.Status + '</b>';
                            }
                        }
                    }
                });
                
                //Re-load records when user click 'load records' button.
                var cnt = 0;
                $('#LoadTicket').click(function (e) {
                    cnt = 0;
                    e.preventDefault();
                    var type = document.getElementById('ddlTypeofRequest');
                    var status = document.getElementById('ddlStatus');
                    var flstype = document.getElementById('ddlFlsTypeofRequest');
                    var accessNo = $('#txtAccessNo').val();
                    
                    if (accessNo != null) {
                        accessNo = accessNo;
                    }
                    else {
                        accessNo = "";
                    }
                    var t = GetType(ccdType.get_SelectedValue());
                    //alert(t);
                    if (t != "FLS") {
                        $('#FLSTableContainer').jtable('load', {
                            ticketno: $('#ticketno').val(),
                            type: GetType(ccdType.get_SelectedValue()),
                            status: status.options[status.selectedIndex].innerHTML,
                            accessno: accessNo

                        });

                        $('#FLSTableContainer table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                        $("#FLSTableContainer table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                        $("#FLSTableContainer").attr("style", "");
                        $('#SDServiceRequestTable').hide();
                        cnt = cnt + 1;

                    }
                    else {


                        //$('#FLSOrderTableContainer').jtable('load', {
                        //    ticketno: $('#ticketno').val(),
                        //    type: GetType(ccdType.get_SelectedValue()),
                        //    status: status.options[status.selectedIndex].innerHTML,
                        //    accessno: accessNo

                        //});
                        try {
                            $('#SDServiceRequestTable').jtable('load', {
                                ticketno: $('#ticketno').val(),
                                type: GetType(ccdType.get_SelectedValue()),
                                status: status.options[status.selectedIndex].innerHTML,
                                accessno: accessNo,
                                requestType: flstype.options[flstype.selectedIndex].innerHTML

                            });
                        }
                        catch (e)
                        { var err = e; }
                        //$('#SDFaultsTable').jtable('load', {
                        //    ticketno: $('#ticketno').val(),
                        //    type: GetType(ccdType.get_SelectedValue()),
                        //    status: status.options[status.selectedIndex].innerHTML,
                        //    accessno: accessNo,
                        //    requestType: "faults"

                        //});
                        debugger;
                        if (cnt == 0) {
                            $('#SDServiceRequestTable').show();
                            $('#SDServiceRequestTable table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                            $("#SDServiceRequestTable table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                            $("#SDServiceRequestTable").attr("style", "")


                            //$("sticky-table-header fixed-solution").
                            //$("div").remove(".sticky-table-header");
                            $('#FLSTableContainer').hide();
                            
                        }
                    }

                    cnt = cnt + 1;
                });
                //Load all records when page is first shown
                $('#LoadTicket').click();


            });
        }
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
       

    </script>

    
    <%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>
   
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
   // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(jtable_css);
   
    $(window).load(function () {

        $("div").remove(".sticky-table-header");
        $("span").remove(".jtable-page-info");
        jtable_css();
    });
</script> 
    
 <script  type="text/javascript">
     //$(window).load(function () {
     //    $('#navTab').hide();
     //});
       </script>
</asp:Content>

