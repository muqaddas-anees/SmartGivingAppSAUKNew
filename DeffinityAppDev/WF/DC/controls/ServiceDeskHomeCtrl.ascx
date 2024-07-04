<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DC_controls_ServiceDeskHomeCtrl" Codebehind="ServiceDeskHomeCtrl.ascx.cs" %>
      <img id="LoadTicket" src="../media/btn_view.gif" alt="View" style="display:none;" />
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

<div style="width: 100%; overflow: scroll; height: 465px; padding-bottom: 0px;">
<div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                   Tickets
                    </div>
    <div id="SDServiceRequestTable">
    </div>
    <div id="SDFaultsTable">
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#SDServiceRequestTable').jtable({
            title: 'List of Tickets - Service Request',
            paging: true, //Enables paging
            pageSize: 30, //Actually this is not needed since default value is 10.
            sorting: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: 'DC/FLSJlist.aspx/GetSDHome'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: 'Loading tickets...',
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
                canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {
                CallID: {
                    key: true,
                    width: '10%',
                    title: 'Ticket ref',
                    display: function (data) {
                        return '<a href="DC/DCNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                    }
                },
                Icon: {
                    title: '',
                    width: '2%',
                    sorting: false,
                    display: function (data) {
                        if (data.record.Status == 'New') {
                            return '<img src="media/ico_indcate_blue.gif" alt=""/>';
                        }
                    }
                },

                Company: {
                    title: 'Company',
                    width: '15%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },
                Name: {
                    title: 'Requester Name',
                    width: '15%'
                },
                Description: {
                    title: 'Description',
                    width: '28%'
                },
               
                Status: {
                    title: 'Status',
                    width: '10%',
                    display: function (data) {
                        return '<b>' + data.record.Status + '</b>';
                    }
                },
                DateLogged: {
                    title: 'Date Logged',
                    width: '12%'
                }
                
               
                
            }
        });
        $('#SDFaultsTable').jtable({
            title: 'List of Tickets - Faults',
            paging: true, //Enables paging
            pageSize: 30, //Actually this is not needed since default value is 10.
            sorting: true,
            //Enables sorting
            defaultSorting: 'CallID DESC',
            actions: {
                listAction: 'DC/FLSJlist.aspx/GetSDHome'
            },

            messages: {
                serverCommunicationError: 'An error occured while communicating to the server.',
                loadingMessage: 'Loading tickets...',
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
                canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
                deleteProggress: 'Deleted {0} of {1} records, processing...'
            },
            fields: {
                CallID: {
                    key: true,
                    width: '10%',
                    title: 'Ticket ref',
                    display: function (data) {
                        return '<a href="DC/DCNavigation.ashx?callid=' + data.record.CallID + '&type=' + data.record.RequestType + '"><b>TN:' + data.record.CallID + '</b></a>';
                    }
                },
                Icon: {
                    title: '',
                    width: '2%',
                    sorting: false,
                    display: function (data) {
                        if (data.record.Status == 'New') {
                            return '<img src="media/ico_indcate_blue.gif" alt=""/>';
                        }
                    }
                },

                Company: {
                    title: 'Company',
                    width: '15%',
                    display: function (data) {
                        return '<b>' + data.record.Company + '</b>';
                    }
                },
                Name: {
                    title: 'Requester Name',
                    width: '15%'
                },
                Description: {
                    title: 'Description',
                    width: '28%'
                },
               
                Status: {
                    title: 'Status',
                    width: '10%',
                    display: function (data) {
                        return '<b>' + data.record.Status + '</b>';
                    }
                },
                DateLogged: {
                    title: 'Date Logged',
                    width: '12%'
                }
               
              
                
            }
        });
        //Re-load records when user click 'load records' button.
        $('#LoadTicket').click(function (e) {
            e.preventDefault();

            $('#SDServiceRequestTable').jtable('load', {
                requestType: "service request"

            });
            $('#SDFaultsTable').jtable('load', {
                requestType: "faults"

            });
        });
        //Load all records when page is first shown
        $('#LoadTicket').click();
//        $('#SDServiceRequestTable').jtable('load');
//        $('#SDFaultsTable').jtable('load');
    });
  
</script>
