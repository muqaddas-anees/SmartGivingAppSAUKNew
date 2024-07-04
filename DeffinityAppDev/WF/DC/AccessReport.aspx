<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_AccessReport" Codebehind="AccessReport.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
 Access Control
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <a id="A1" href="FLSJlist.aspx?type=AccessControl" runat="server" target="_self"><span>
            Return to Ticket Journal</span></a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
<link href="../Scripts/jqgrid.css" rel="stylesheet" type="text/css" />
     <link href="../../Content/jtable/themes/lightcolor/gray/jtable.css" rel="stylesheet"
    type="text/css" />
     <%: System.Web.Optimization.Scripts.Render("~/bundles/jtable") %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

 
 
 <table cellpadding="0" cellspacing="0" width="98%">
        <tr>
         <td >
                Customer<br />
                 <asp:DropDownList ID="ddlRequestersCompany" runat="server" Width="200px" 
                    ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlRequestersCompany"
                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
                
         </td>       
        <td>Ticket No.<br />
        <input type="text" name="ticketno" id="ticketno" size="20px" />    
       
        </td>
        <td>Visitor Name<br />
        <input type="text" name="VstName" id="VstName" size="25px" />     
      
       </td>
        <td>Visitor Company<br />
        <input type="text" name="VstCmpy" id="VstCmpy" size="25px" />
       
        </td>
        <td>
                                Status<br />
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" ClientIDMode="Static"></asp:DropDownList>
<ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                        Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetStatus" LoadingText="[Loading status...]" /></td>
       
                               

                        
        </tr>
        <tr> 
      <td>
                        Requested Start Date<br />
                        <asp:TextBox ID="txtLoggedStartDate" runat="server" ClientIDMode="Static" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required for arrive date" ControlToValidate="txtLoggedStartDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="ImgStart" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender3" runat="server" TargetControlID="txtLoggedStartDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
             PopupButtonID="ImgStart" TargetControlID="txtLoggedStartDate">
        </ajaxToolkit:CalendarExtender></td>
        <td>
                        Requested End Date<br />
                        <asp:TextBox ID="txtLoggedEndDate" runat="server" ClientIDMode="Static" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required for arrive date" ControlToValidate="txtLoggedEndDate" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="ImgEnd" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender4" runat="server" TargetControlID="txtLoggedEndDate" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="MyCalendar"
             PopupButtonID="ImgEnd" TargetControlID="txtLoggedEndDate">
        </ajaxToolkit:CalendarExtender></td>
       
                        <td>
                        Arrive Date<br />
                        <asp:TextBox ID="txtAtime" runat="server" ClientIDMode="Static" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required for arrive date" ControlToValidate="txtAtime" 
                   Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
         <asp:Label ID="imgAdate" runat="server"  SkinID="Calender" />
                <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAtime" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgAdate" TargetControlID="txtAtime">
        </ajaxToolkit:CalendarExtender></td>
                        <td>
                        Depart Date<br />
                        <asp:TextBox ID="txtDtime" ClientIDMode="Static" runat="server" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ErrorMessage="MM/dd/yyyy format required for depart date" ControlToValidate="txtDtime" 
                    Type="Date" Operator="DataTypeCheck" Text="*" 
                    Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:Label ID="imgDdate" runat="server"  SkinID="Calender" />
                     <ajaxToolkit:FilteredTextBoxExtender ID ="FilteredTextBoxExtender2" runat="server" TargetControlID="txtDtime" ValidChars="0123456789/"></ajaxToolkit:FilteredTextBoxExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
             PopupButtonID="imgDdate" TargetControlID="txtDtime">
        </ajaxToolkit:CalendarExtender></td>
         <td colspan="2">Purpose of Visit<br />
        
        <asp:DropDownList ID="ddlvstngpurp" runat="server" ClientIDMode="Static" Width="300px" >
                            </asp:DropDownList>
                           
                            <ajaxToolkit:CascadingDropDown ID="ccdvp" runat="server" TargetControlID="ddlvstngpurp"
                                Category="Name" PromptText="All" PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetVisitinPurpose" LoadingText="[Loading data...]" /></td>
  
        <td align="left"><asp:LinkButton id="LoadVisitors" ClientIDMode="Static" runat="server" Text="View" SkinID="BtnLinkButton" alt="View"  />
</td>
<td></td>
</tr>
        
       
        </table>
        <br />
        <div align="right">
         <asp:Button ID="ImgAccessExport" runat="server" 
                 SkinID="btnDefault" Text="Export to Excel" onclick="ImgAccessExport_Click"   
                         />
        </div>
        <div id="AccessTableContainer">
    </div>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#AccessTableContainer').jtable({

                title: 'List of Visitors',
                paging: true, //Enables paging
                pageSize: 30, 
                sorting: true,
                //Enables sorting
                defaultSorting: 'CallID ASC',
                actions: {
                    listAction: '/WF/DC/AccessReport.aspx/GetVisitors'
                },

                messages: {
                    serverCommunicationError: 'An error occured while communicating to the server.',
                    loadingMessage: 'Loading visitors...',
                    noDataAvailable: 'No visitors available!',
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
                    ID: {
                        key: true,
                        type: 'hidden',
                        height: '10%'

                    },
                    CallID: {

                        title: 'Ticket No',
                        height: '10%',
                          display: function (data) {
                                                       
                              return '<a href="AccessControl.aspx?callid=' + data.record.CallID + '"><b>TN:' + data.record.CallID + '</b></a>';
                            }

                    },
                    Company: {
                        title: 'Customer'
                      },
                    Name: {
                        title: 'Visitor Name'


                    },

                    VisitorCompany: {
                        title: 'Visitor Company'
                    },
                    RequestedDate: {
                        title: 'Requested Date',
                        type: 'date',
                        width: '9%',
                        display: function (data) {

                            if (data.record.RequestedDate != null) {

                                var yr = moment(data.record.RequestedDate).format('YYYY');
                                if (yr == '1900')
                                    return null;
                                else
                                    return moment(data.record.RequestedDate).format('DD/MM/YYYY');
                            }
                            else
                                return null;

                        }
                    },
                    ArriveDate: {
                        title: 'Arrive Date',
                        type: 'date',
                        
                        display: function (data) {

                            if (data.record.ArriveDate != null) {

                                var yr = moment(data.record.ArriveDate).format('YYYY');
                                if (yr == '1900')
                                    return null;
                                else
                                    return moment(data.record.ArriveDate).format('DD/MM/YYYY');
                            }
                            else
                                return null;

                        }
                    },
                    DepartDate: {
                        title: 'Depart Date',
                       
                        type: 'date',
                        display: function (data) {

                            if (data.record.DepartDate != null) {

                                var yr = moment(data.record.DepartDate).format('YYYY');
                                if (yr == '1900')
                                    return null;
                                else
                                    return moment(data.record.DepartDate).format('DD/MM/YYYY');
                            }
                            else
                                return null;

                        }
                    },
                    EmailAddress: {
                        title: 'Email Address'


                    },
                    PhoneNumber: {
                        title: 'Phone No'
                    },
                    AccessNumber: {
                        title: 'Access No'
                         
                    }
                   
                    



                }
            });

            //Re-load records when user click 'load records' button.
            $('#LoadVisitors').click(function (e) {
                e.preventDefault();
                //                var pofvisit = document.getElementById('ddlvstngpurp');
                //                var status = document.getElementById('ddlStatus');
                var company = document.getElementById('ddlRequestersCompany');
                $('#AccessTableContainer').jtable('load', {
                    company: company.options[company.selectedIndex].innerHTML,
                    ticketno: $('#ticketno').val(),
                    purposeofvisit: $('#ddlvstngpurp').val(),
                    VstName: $('#VstName').val(),
                    VstCmpy: $('#VstCmpy').val(),
                    loggedStartDate: $('#txtLoggedStartDate').val(),
                    loggedEndDate: $('#txtLoggedEndDate').val(),
                    adate: $('#txtAtime').val(),
                    ddate: $('#txtDtime').val(),
                    status: $('#ddlStatus').val()

                });
            });
            //Load all records when page is first shown
            $('#LoadVisitors').click();

        });
       
    </script>
    
</asp:Content>

