<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CompletedSalesJournal.aspx.cs" Inherits="DeffinityAppDev.WF.DC.openclaims" EnableEventValidation="false" %>

<%@ Register Src="~/WF/DC/controls/FLSListTabCtrl.ascx" TagPrefix="Pref" TagName="FLSListTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Dashboard 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:FLSListTabCtrl runat="server" ID="FLSListTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Completed Sales
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
      <div class="col-md-3">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-7 form-inline">
                <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgFromDate" runat="server" SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar" 
                                 PopupButtonID="imgFromDate" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtTodate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgTodate" runat="server" SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar" 
                                 PopupButtonID="imgTodate" TargetControlID="txtTodate"></ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Search%></label>
           <div class="col-sm-10">
               <asp:TextBox ID="txtSearch" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
         <div class="col-md-2">
             <div class="col-sm-12 form-inline">
                 <asp:DropDownList ID="ddlPeriod" runat="server" ClientIDMode="Static">
                     <asp:ListItem Text="Select option" Value="0"></asp:ListItem>
                     <asp:ListItem Text="Last 7 Days" Value="7 days"></asp:ListItem>
                     <asp:ListItem Text="This Month" Value="this month"></asp:ListItem>
                     <asp:ListItem Text="Last Month" Value="last month"></asp:ListItem>
                 </asp:DropDownList>
                 </div>
            </div>
</div>
    <div class="form-group row">
      <div class="col-md-3">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               
            </div>
	</div>
        <div class="col-md-2">
             <div class="col-sm-12 form-inline">
                <asp:LinkButton SkinID="btnSearch" runat="server" ID="btnSearch" ClientIDMode="Static"></asp:LinkButton>
                 </div>
            </div>
</div>
    <%-- <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>History of Tickets </strong> 
<hr class="no-top-margin" />
	</div>
</div>--%>
     <div class="form-group row">
          <div class="col-md-12">
               <div id="IssuesGraph" style="width:100%;height:300px;"></div>
              </div>
         </div>
   <%--  <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>History of Tickets </strong> 
<hr class="no-top-margin" />
	</div>
</div>--%>
    <div class="row">
                  <div class="col-md-12">
                      <br />
                      </div>
        </div>
 <div class="row">
                  <div class="col-md-12">
        <table id="students" class="table table-striped table-bordered"></table>
    </div>
     </div>
  <script type="text/javascript">
      $(document).ready(function () {
          GetAssetRecords();
          GetIssuesData();
          $('#btnSearch').click(function()
          {
              GetAssetRecords();
              GetIssuesData();
              return false;
          });
      });
      //function getQuerystring(stid) {
      //    var stid = Status.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
      //    var regex = new RegExp("[\\?&]" + stid + "=([^&#]*)");
      //    var qs = regex.exec(window.location.href);
      //    if (qs == null)
      //        return default_;
      //    else
      //        return qs[1];
      //}
      function GetIssuesData() {
          var fromdate = $("[id$='txtFromDate']").val();
          var todate = $("[id$='txtTodate']").val();
          var search = $("[id$='txtSearch']").val();
          var period = $("[id$='ddlPeriod']").val();
          $.ajax({
              url: '../../WF/DC/webservices/DCServices.asmx/GetOpenClaimsChat',
              type: "POST",
              data: "{'fromdate': '" + fromdate + "','todate':'" + todate + "','search':'" + search + "','period':'" + period + "'}",
              dataType: "json",
              contentType: 'application/json; charset=utf-8',
              async: true,
              success: function (data) {
                  if (data.d != '') {
                      var newData = jQuery.parseJSON(data.d);
                      var ds = [];
                      for (var i = 0; i < newData.length; i++) {
                          ds.push({
                              Name: newData[i].Name,
                              Value: newData[i].Value
                          });
                      }
                      $("#IssuesGraph").dxChart({
                          dataSource: ds,
                          series: {
                              argumentField: "Name",
                              valueField: "Value",
                              name: "Category",
                              type: "bar",
                              color: '#68b828'
                          },
                          valueAxis: {
                              tickInterval: 1
                          },
                          tooltip: {
                              enabled: true
                          },
                          legend: {
                              visible: false
                          }
                      });
                  }
              }
          });
      }
      function GetAssetRecords() {
          var fromdate = $("[id$='txtFromDate']").val();
          var todate = $("[id$='txtTodate']").val();
          var search = $("[id$='txtSearch']").val();
          var period = $("[id$='ddlPeriod']").val();
          debugger;
          $.ajax({
              url: "../../WF/DC/webservices/DCServices.asmx/BindOpenClaims",
              type: "POST",
              data: "{'fromdate': '" + fromdate + "','todate':'" + todate + "','search':'" +search+ "','period':'" + period  +"'}",
              contentType: 'application/json; charset=utf-8',
              dataType: "json",
              async: true,
              success: function (data) {
                  debugger;
                  var NewData = jQuery.parseJSON(data.d);
                  var x = "<thead><tr>"
                      + "<th>Date Closed</th>><th>Sales Person</th><th>Policy Type</th><th>Contract Term</th><th>Policy Number</th><th>Customer</th><th>Address</th><th>Address 1</th><th>City</th><th>Zip Code</th><th>Invoice Value</th></thead>";
                  x = x + "<tbody>"

                  for (var i = 0 ; i < NewData.length; i++) {
                      var Callid = NewData[i].Callid
                      var DateClosedDis = NewData[i].DateClosedDis;
                      var SalesPerson = NewData[i].SalesPerson;
                      var PolicyType = NewData[i].PolicyType;
                      var ContractTerm = NewData[i].ContractTerm;
                      var PolicyNumber = NewData[i].PolicyNumber;
                      var Customer = NewData[i].Customer;
                      var Address = NewData[i].Address;
                      var Address1 = NewData[i].Address1;
                      var City = NewData[i].City;
                      var ZipCode = NewData[i].ZipCode;
                      var InvoiceValue = NewData[i].InvoiceValue;

                      x = x + "<tr><td style=direction:rtl>" + DateClosedDis
                          + "</td><td>" + SalesPerson
                          + "</td><td>" + PolicyType
                          + "</td><td>" + ContractTerm
                          + "</td><td>" + PolicyNumber
                          + "</td><td>" + Customer
                          + "</td><td>" + Address
                          + "</td><td>" + Address1
                          + "</td><td>" + City
                          + "</td><td>" + ZipCode
                          + "</td><td style=direction:rtl>" + InvoiceValue + "</td></tr>";
                  }

                  x = x + "</tbody>";
                  $("#students").empty();
                  $("#students").append(x);
                  BindTable();
                  $("#students").removeClass("no-footer");
                 
              }
          });
      }
      function BindTable() {
          var table = $('#students').DataTable({
              'Ordering': true,
              "order": [[1, "asc"]],
              'paging': false,
              'bFilter': false,
              'lengthChange': false,
              'destroy': true,
              "columnDefs": [{
                  "targets": 0, "orderable": false
              },
              //{
              //    "targets": 7,
              //    "visible": true
              //}

              ]
          });
      }

      function ButtonHtml(Id) {
          var HtmlText = " <a target='_blank' id=Link" + Id + " href='/WF/DC/FLSForm.aspx?CallID=" + Id + "' style=' font-weight: bold'>TN:" + Id + "</a>";
          //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
          return HtmlText;
      }



    </script>
   
     <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
     <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/fixedcolumns/3.2.4/css/fixedColumns.dataTables.min.css">

    <link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="/Web/examples/resources/syntax/shCore.css">--%>
   <%-- <link rel="stylesheet" type="text/css" href="/Web/examples/resources/demo.css">--%>
     
   <%-- <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>--%>
   
     
    <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

    </script>
     <script type="text/javascript"  src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
    </script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>
    
     <script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
    </script>
    <%--<script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>--%>
    <script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.4.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.2.2/js/dataTables.select.min.js">
    </script>
<script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.4/js/dataTables.fixedColumns.min.js">
    </script>
    <script type="text/javascript" src="/web/js/dataTables.editor.min.js">
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
       <script type="text/javascript">
           //hidetabs();
    </script>
</asp:Content>
