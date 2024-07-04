<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_DeliveryReport"  EnableEventValidation="false" Codebehind="DeliveryReport.aspx.cs" %>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Delivery%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.DeliveryReport%> 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
   <a id="A1" href="FLSJlist.aspx?type=Delivery" runat="server" target="_self"><i class="fa fa-arrow-left"></i>
            Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <script  type="text/javascript">
     $(document).ready(function () {
         $('#navTab').hide();
     });
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--<link href="../../Content/jqgrid.css" rel="stylesheet" type="text/css" />--%>
<!-- jTable style file -->
<link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />
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
    
<%--<%: System.Web.Optimization.Styles.Render("~/bundles/jtablecss") %>--%>
<%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>


    <div class="filtering">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="7">
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
       ValidationGroup="dr" DisplayMode="BulletList" /></td>         
        </tr>
        <tr>
            <td width="230px">
                Customer<br />
                 <asp:DropDownList ID="ddlRequestersCompany" runat="server" Width="200px" 
                    ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
                 
         </td>         
            <td>
                 Ticket No<br />
                 <input type="text" name="ticketno" id="ticketno" size="10px" />                
            </td>           
            <td>
                 Courier Tracking No<br />
                <input type="text" name="cticketno" id="cticketno" size="20px" />
            </td>          
            <td>
                 Status<br />
                 <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" 
                     ClientIDMode="Static"> </asp:DropDownList>
                      <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                Category="1" PromptText="ALL" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetStatusByRequestTypeId" LoadingText="[Loading status...]" />
                     
           
            </td>
            <td>
                 Received Start Date<br />
                 <asp:TextBox ID="txtReceivedStartDate" runat="server" ClientIDMode="Static" SkinID="Date"></asp:TextBox>
                  <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtReceivedStartDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="imgToDate" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="filterarrival" runat="server" TargetControlID="txtReceivedStartDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgToDate" TargetControlID="txtReceivedStartDate">
        </ajaxToolkit:CalendarExtender>
            </td>
              <td>
                 Received End Date<br />
                 <asp:TextBox ID="txtReceivedEndDate" runat="server" SkinID="Date" ClientIDMode="Static" ></asp:TextBox>
                  <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required" ControlToValidate="txtReceivedEndDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="ImgEnd" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender1" runat="server" TargetControlID="txtReceivedEndDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
             PopupButtonID="ImgEnd" TargetControlID="txtReceivedEndDate">
        </ajaxToolkit:CalendarExtender>
            </td>
               <td>
                   <asp:CheckBox ID="chkOverdue" runat="server" Text="  Overdue" ClientIDMode="Static" />
            </td>
               <td>          
                 <asp:Button SkinID="btnDefault" Text="View" ClientIDMode="Static" id="LoadTicket"  />
            </td>
        </tr>
        </table>
        </div>
        <div class="clr"></div>
        
         <div align="right">
        <asp:Button ID="ImgDeliveryExport" runat="server"
                 Text="Export to Excel" onclick="ImgDeliveryExport_Click" />
        </div>
        <div id="DeliveryTableContainer">
    </div>
    <script type="text/javascript" >
        $(document).ready(function () {

            $('#DeliveryTableContainer').jtable({

                title: 'List of Tickets',
                paging: true, //Enables paging
                pageSize: 30, //Actually this is not needed since default value is 10.
                sorting: true,
                //Enables sorting
                defaultSorting: 'CallID ASC',
                actions: {
                    listAction: '/WF/DC/DeliveryReport.aspx/Get'
                },

                messages: {
                    serverCommunicationError: 'An error occured while communicating to the server.',
                    loadingMessage: 'Loading tickets...',
                    noDataAvailable: 'No tickets available!',
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
                    canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
                    deleteProggress: 'Deleted {0} of {1} records, processing...'
                },
                fields: {
                    CallID: {
                        key: true,
                        title: 'Ticket&nbsp;No',
                        height: '5%',
                        display: function (data) {
                            //                            return '<a href="FLSForm.aspx?callid=' + data.record.CallID + '"><b>TN:' + data.record.CallID + '</b></a>';
                            return '<a href="Delivery.aspx?callid=' + data.record.CallID + '"><b>TN:' + data.record.CallID + '</b></a>';
                        }
                    },

                    CourierNumber: {
                        title: 'Courier&nbsp;Tracking&nbsp;No',
                        width: '14%',
                        display: function (data) {
                            return '<b>' + data.record.CourierNumber + '</b>';
                        }
                    },
                    Status: {
                        title: 'Status',
                        width: '10%',
                        display: function (data) {
                            return '<b>' + data.record.Status + '</b>';
                        }
                    },
                    NumofBoxesRec: {
                        title: 'Received',
                        width: '7%'
                    },
                    DateRecieved: {
                        title: 'Date',
                        width: '7%',
                        type: 'date',
                        display: function (data) {

                            if (data.record.DateRecieved != null) {

                                    var yr = moment(data.record.DateRecieved).format('YYYY');
                                    if (yr == '1900')
                                        return null;
                                    else
                                        return moment(data.record.DateRecieved).format('DD/MM/YYYY');
                            }
                            else
                                return null;

                        }
                    },
                    BoxesRemaining: {
                        title: 'Remaining',
                        width: '7%'
                    },
                    StorageLocation: {
                        title: 'Location',
                        width: '12%'
                    },
                    OverdueDays: {
                        title: 'Overdue&nbsp;',
                        width: '7%'
                    },
                    TotalCost: {
                        title: 'Total&nbsp;Charge',
                        width: '9%'
                    },
                    PeriodCost: {
                        title: 'Monthly&nbsp;Charge',
                        width: '10%'

                    }
                }
            });

            //Re-load records when user click 'load records' button.
            $('#LoadTicket').click(function (e) {
                e.preventDefault();
                var company = document.getElementById('ddlRequestersCompany');
                var status = document.getElementById('ddlStatus');
                var od = document.getElementById('chkOverdue');
                var overdue;
                if (od.checked) {
                    overdue = true;
                }
                else {
                    overdue = false;
                }
                var cnt = 0;
                $('#DeliveryTableContainer').jtable('load', {
                    ticketno: $('#ticketno').val(),
                    company: company.options[company.selectedIndex].innerHTML,
                    ctno: $('#cticketno').val(),
                    status: status.options[status.selectedIndex].innerHTML,
                    receivedStartDate: $('#txtReceivedStartDate').val(),
                    receivedEndDate: $('#txtReceivedEndDate').val(),
                    overdue: overdue
                });
                debugger;
                if (cnt == 0) {
                    $('#DeliveryTableContainer table:first').addClass("table table-small-font table-bordered table-striped dataTable responsive");
                    $("#DeliveryTableContainer table:first").wrap("<div class='table-responsive' data-pattern='priority-columns' data-focus-btn-icon='fa-asterisk' data-sticky-table-header='true' data-add-display-all-btn='true' data-add-focus-btn='false'></div>");
                    $("#DeliveryTableContainer").attr("style", "");
                    //$("sticky-table-header fixed-solution").
                    //$("div").remove(".sticky-table-header");

                    cnt = cnt + 1;
                }
            });
            //Load all records when page is first shown
            $('#LoadTicket').click();
           
           

        });
       
    </script>


  <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

</asp:Content>


