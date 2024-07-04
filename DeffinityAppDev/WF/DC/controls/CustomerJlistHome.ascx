<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_CustomerJlistHome" Codebehind="CustomerJlistHome.ascx.cs" %>
<link href="Scripts/jqgrid.css" rel="stylesheet" type="text/css" />
<!-- jTable style file -->
<link href="Scripts/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery-1.6.4.min.js" type="text/javascript" />
</script>
<script src="Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
<script src="Scripts/modernizr-1.7.min.js" type="text/javascript"></script>
<script src="Scripts/jtablesite.js" type="text/javascript"></script>
<!-- A helper library for JSON serialization -->
<script type="text/javascript" src="Scripts/jtable/external/json2.js"></script>
<!-- Core jTable script file -->
<script type="text/javascript" src="Scripts/jtable/jquery.jtable.js"></script>
<script type="text/javascript" src="Scripts/jtable/localization/jquery.jtable.tr.js"></script>
<!-- ASP.NET Web Forms extension for jTable -->
<script type="text/javascript" src="Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
<div class="filtering">
    <table width="100%">
        <tr>
            <td style="width: 80%">
                Ticket Number:
            </td>
            <td style="width: 10%">
                <input type="text" name="ticketno" id="ticketno" size="10px" />
            </td>
            <td style="width: 10%">
                <img id="LoadTicket" src="media/btn_view.gif" alt="View" style="cursor: pointer" />
            </td>
        </tr>
    </table>
</div>
<div style="width: 100%;overflow:scroll;height:465px;padding-bottom:0px;">
<div id="FLSTableContainer"  >
</div>
<div style="height:15px">&nbsp;</div>
<div id="FLSTableContainerOrder" >
</div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#FLSTableContainer').jtable({
            title: 'List of Tickets',
            paging: true, //Enables paging
            pageSize: 6, //Actually this is not needed since default value is 10.
            sorting: true, //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: 'DC/DCCustomerJlist.aspx/GetCustomerHome'
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
                    title: 'Ref',
                    width: '10%',
                    display: function (data) {
                        //                            return '<a href="FLSForm.aspx?callid=' + data.record.CallID + '"><b>TN:' + data.record.CallID + '</b></a>';
                        return '<a href="DC/DCCustomerNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                    }
                },

                Company: {
                    title: 'Company',
                    width: '45%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },

                RequestType: {
                    title: 'Type',
                    width: '45%'
                }

            }
        });
        $('#FLSTableContainerOrder').jtable({
            title: 'List of Orders',
            paging: true, //Enables paging
            pageSize: 6, //Actually this is not needed since default value is 10.
            sorting: true, //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: 'DC/DCCustomerJlist.aspx/GetCustomerHomeOrder'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: 'Loading tickets...',
                noDataAvailable: 'No orders available!',
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
                    title: 'Ref',
                    width: '10%',
                    display: function (data) {
                        //                            return '<a href="FLSForm.aspx?callid=' + data.record.CallID + '"><b>TN:' + data.record.CallID + '</b></a>';
                        //return '<a href="DC/DCCustomerNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                        return '<a href="DC/DCCustomerService.aspx?callid=' + data.record.CallID + '"><b>TN:' + data.record.CallID + '</b></a>';
                    }
                },

                Company: {
                    title: 'Company',
                    width: '45%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },

                RequestType: {
                    title: 'Type',
                    width: '45%'
                }

            }
        });
        //Re-load records when user click 'load records' button.
        $('#LoadTicket').click(function (e) {
            e.preventDefault();

            $('#FLSTableContainer').jtable('load', {
                ticketno: $('#ticketno').val()

            });
            $('#FLSTableContainerOrder').jtable('load', {
                ticketno: $('#ticketno').val()

            });
        });
        //Load all records when page is first shown
        $('#LoadTicket').click();


    });

</script>
