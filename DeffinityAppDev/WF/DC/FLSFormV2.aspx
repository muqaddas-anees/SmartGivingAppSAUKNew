<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="FLSFormV2.aspx.cs" Inherits="DeffinityAppDev.WF.DC.FLSFormV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="card-body"> 
                                <%= Resources.DeffinityRes.ServiceDesk %>
                            </h3>
							<div class="card-toolbar">
								
                              
							</div>
						</div>
						<div class="card-body">
                            <div class="row">
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

    /*a.fc-timeline-event {
    height: 52px;
}*/

    /*table.dataTable thead tr {
  background-color: #4160a0; /*#40bbea;
  color:#ffffff;
}*/

    table.dataTable tbody th, table.dataTable tbody td {
    padding: 8px 10px;
    max-width: 0;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  text-align:left;
}

    /*td.fc-widget-content div{
        height:70px;
    }*/
</style>
    <style>
      td.New{ 
          background-color:#00B0F0;
          text-align:center;
          vertical-align:middle;
      }
  </style>
        
    <div class="form-group row" >
           <div class="col-md-12">
 <div class="row clshistory">
                  <div class="col-md-12">
                   <div style="width:135px;padding-bottom:5px"><asp:DropDownList ID="ddlStatus" runat="server" ClientIDMode="Static" SkinID="ddl_125px">
            </asp:DropDownList> <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus"
                BehaviorID="ccdS" Category="Name" PromptText="All" PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetStatusByFLS"
                 LoadingText="[Loading status...]" ClientIDMode="Static" />             </div>  
                      <div class="table-responsive">
        <table id="students" class="table table-striped table-bordered gy-7 gs-7" ></table>
                          </div>
    </div>
     </div>

               
               </div>
           <div class="col-md-5">
                 <div class="form-group row" >
                      <div style="width:135px;padding-bottom:5px;visibility:hidden" ><asp:TextBox runat="server" ClientIDMode="Static"  >
            </asp:TextBox></div>  
                     
    <div id="map_canvas" style="width: 99%; height: 415px" ></div>
</div>
               </div>
         </div>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
        //setStatusBackColor();
        //grid_responsive_display();
        function setStatusBackColor() {


            $('.statuscls').each(function () {

                var s = $(this).html();
                if (s == 'New')
                    $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Cancelled')
                    $(this).closest("td").css({ "background-color": "#44546a", "text-align": "center", "vertical-align": "middle" });
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
                else if (s == 'Request Feeback')
                    $(this).closest("td").css({ "background-color": "#00B0F0", "text-align": "center", "vertical-align": "middle" });
                else if (s == 'Quote Sent')
                    $(this).closest("td").css({ "background-color": "#B4c6e7", "text-align": "center", "vertical-align": "middle" });
            });


        }

    </script>
  <script type="text/javascript">

      $(document).ready(function () {
          GetAssetRecords(0);
      });
      function GetAssetRecords(id) {
          try {

              var dataObject = JSON.stringify({
                  'id': id,
                  'status': $("[id*=ddlStatus] option:selected").text()

              });

              $.ajax({
                  url: "../../WF/DC/webservices/DCServices.asmx/BindCallAssets",
                  type: "POST",
                  data: dataObject,
                  contentType: 'application/json; charset=utf-8',
                  dataType: "json",
                  async: true,
                  success: function (data) {

                      var NewData = jQuery.parseJSON(data.d);
                      var x = "<thead><tr class='fw-bold fs-6 text-gray-800 border-bottom-2 border-gray-200'><th style='width:5%;'>Event Ref</th><th style='width:5%;'>ID</th>"
                          + "<th  style='width:20%;'><%=Resources.DeffinityRes.Details%> </th>"
                          + "<th  style='width:10%;'><%=Resources.DeffinityRes.ClientName%></th>"
                          + "<th  style='width:10%;'><%=Resources.DeffinityRes.AssignedSalesrep%></th>"
                          + "<th  style='width:10%;'><%=Resources.DeffinityRes.AssignedSmartTech%></th>"
                          + "<th style='width:5%;'><%=Resources.DeffinityRes.ScheduledDate%></th>"
                          + "<th  style='width:10%;'><%=Resources.DeffinityRes.Status%></th>"
                          + "</thead>";
                      x = x + "<tbody>"

                      for (var i = 0; i < NewData.length; i++) {
                          var CCID = NewData[i].CCID
                          var ID = NewData[i].ID
                          var LoggedDate = NewData[i].LoggedDate;
                          var Details = NewData[i].Details;
                          var AssignedTechnician = NewData[i].AssignedTechnician
                          var AssignedSalesRep = NewData[i].AssignedSalesRep
                          var StatusName = NewData[i].StatusName;
                          var ClientName = NewData[i].ClientName;

                          x = x + "<tr><td>" + ButtonHtml(ID, CCID)
                              + "</td><td>" + CCID
                              + "</td><td>" + Details
                              + "</td><td>" + ClientName
                              + "</td><td>" + AssignedSalesRep
                              + "</td><td>" + AssignedTechnician
                              + "</td><td style=direction:rtl>" + LoggedDate
                              + "</td><td class='New'><span class='statuscls' style='color: white;font-weight: bold;'>" + StatusName + "</span></td></tr>";
                      }

                      x = x + "</tbody>";
                      $("#students").empty();
                      $("#students").append(x);
                      BindTable();
                      $("#students").removeClass("no-footer");
                      setStatusBackColor();
                  }
              });
          }
          catch (e) {
              var err = e;
          }
      }
      function BindTable() {
          var table = $('#students').DataTable({
              'Ordering': true,
              "order": [[1, "desc"]],
              'paging': true,
              "pageLength": 10,
              'bFilter': false,
              'lengthChange': false,
              'destroy': true,
              "columnDefs": [{
                  "targets": 0, "orderable": false
              },
              {
                  "targets": 1,
                  "visible": false,
                  "searchable": false
              },
                  //{
                  //    "targets": 7,
                  //    "visible": true
                  //}

              ],
              //"initComplete": function (settings, json) {
              //    setStatusBackColor();
              //}
          });

          //$('#students').on('page.dt', function () {
          //    setStatusBackColor();
          //    //var info = table.page.info();
          //    //$('#pageInfo').html('Showing page: ' + info.page + ' of ' + info.pages);
          //});

          table.on('draw', function () {
              setStatusBackColor();
          });
      }


      function ButtonHtml(Id, ccid) {
          var HtmlText = " <a target='_blank' id=Link" + Id + " href='/WF/DC/FLSForm.aspx?CCID=" + ccid + "&CallID=" + Id + "&SDID=" + Id + "' style=' font-weight: bold'>" + ccid + "</a>";
          //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
          return HtmlText;
      }



  </script>

                                </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
    
 
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
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>
    <script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.4.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.2.2/js/dataTables.select.min.js">
    </script>
<script type="text/javascript" src="https://cdn.datatables.net/fixedcolumns/3.2.4/js/dataTables.fixedColumns.min.js">
    </script>
    <script type="text/javascript" src="/web/js/dataTables.editor.min.js">
    </script>
   
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
    }
        div.dt-buttons{
            float:right;
        }
    </style>

    <script type="text/javascript">
        hidetabs();
    </script>

    <style>
        /*a.fc-timeline-event{
            height:45px;
        }*/
    </style>
   <script type="text/javascript">
       $(document).ready(function () {
           //ddlStatus
           $("[id*=ddlStatus]").change(function () {
               var c = $("[id*=ddlStatus] option:selected").text();// $(this).text();
               console.log(c);
               GetAssetRecords(0);
               // $('#calendar').fullCalendar('rerenderEvents');
           });
           $("[id*=ddltype]").change(function () {
               var c = $("[id*=ddltype] option:selected").text();// $(this).text();
               console.log(c);

               $('#calendar').fullCalendar('rerenderEvents');
           });
           $('#btnserach').click(function () {
               $('#htown').val(GetTowns());
               $('#hpostcode').val(GetPostCodes());
               GetTotalEvents();
               //alert($('#hexpert').val());
               return false;
           });
       });
       function GetPostCodes() {
           var checked_checkboxes = $("[id*=chkPostcode] input:checked");
           var message = "";
           checked_checkboxes.each(function () {
               var value = $(this).val();
               message = message + value + ',';
               // var text = $(this).closest("td").find("label").html();
               //message += "Text: " + text + " Value: " + value;
               //message += "\n";
           });
           return message;
       }
       function GetTowns() {
           var checked_checkboxes = $("[id*=chkTowns] input:checked");
           var message = "";
           checked_checkboxes.each(function () {
               var value = $(this).val();
               message = message + value + ',';
               // var text = $(this).closest("td").find("label").html();
               //message += "Text: " + text + " Value: " + value;
               //message += "\n";
           });
           return message;
       }
   </script>
    
</asp:Content>
