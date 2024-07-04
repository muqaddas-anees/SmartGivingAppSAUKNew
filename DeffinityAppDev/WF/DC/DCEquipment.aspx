<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DCEquipment.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCEquipment" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
        <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <nav class="navbar navbar-default" role="navigation" id="navTab">
    <uc2:FlsTab ID="flstab1" runat="server" />
        </nav>
    
            <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 
                                 <label id="lblTitle" runat="server"></label>  </h3>
							<div class="card-toolbar">
								
                               <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
     <asp:LinkButton ID="btnSave" runat="server" AlternateText="Save" SkinID="btnSubmit" CausesValidation="false"
                            OnClick="btnSave_Click" ClientIDMode="Static" />
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
               <asp:HiddenField ID="haddressid" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hcid" runat="server" ClientIDMode="Static" />
           <asp:HiddenField ID="hapid" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="haid" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="hpid" runat="server" ClientIDMode="Static" />
    <div class="form-group row">
        <div class="col-md-12">
            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
            </div>
        </div>
   

    
    <asp:Panel ID="PanelProducts" runat="server">
    <%--  <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong> </strong> 
<hr class="no-top-margin" />
	</div>
</div>--%>

      <script type="text/javascript" language="javascript"  class="init">
          $(document).ready(function () {
              debugger;

              loadByAddressID();
          });
          function GetParameterValues(param) {
              var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
              for (var i = 0; i < url.length; i++) {
                  var urlparam = url[i].split('=');
                  if (urlparam[0] == param) {
                      return urlparam[1];
                  }
              }
          }
          function getUrlParameter(name) {
              name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
              var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
              var results = regex.exec(location.search.toLowerCase());
              return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
          };
          function getAssignhaid() {
              return $("[id$='hapid']").val();
          }
          function gethaid() {
              return $("[id$='haddressid']").val();
          }
          function getcontactid() {
              return $("[id$='hcid']").val();
          }

          var editor1;
          var selected1 = [];
          var table1;
          function assets_table_load() {
              try {
                  $('#plist').empty();
                  //alert('test');
                  editor1 = new $.fn.dataTable.Editor({
                      ajax: "/api/ContactAdressAssetsByJob",
                      table: "#plist",
                      fields: [
                          {
                              label: ":",
                              name: "Assets.ContactID",
                              type: "hidden",
                              def: getcontactid()
                          },
                          {
                              label: ":",
                              name: "Assets.ContactAddressID",
                              type: "hidden",
                              def: gethaid()
                          },
                          {
                              label: "Type:",
                              name: "Assets.Type",
                              type: "select"
                          },
                          {
                              label: "Make:",
                              name: "Assets.Make",
                              type: "select"
                          },
                          {
                              label: "Model:",
                              name: "Assets.Model",
                              type: "select"
                          },
                          {
                              label: "Serial number:",
                              name: "Assets.SerialNo"
                          },
                          {
                              label: "Purchase Date:",
                              name: "Assets.PurchasedDate",
                              type: 'datetime',
                              def: function () { return new Date(); },
                              dateFormat: 'MM/DD/YYYY'
                          },
                          {
                              label: "Notes:",
                              name: "Assets.FromNotes"
                          },
                      ]
                  }
                  );


                  table1 = $('#plist').DataTable({
                      "fnInitComplete": function (oSettings, json) {
                          var Q_callid = GetParameterValues('callid');
                          if (Q_callid == undefined) {
                          }
                          else if (Q_callid == "") {
                          }
                          else {
                              $('#plist tbody tr:eq(0)').addClass('selected');
                          }
                      },
                      //"iDisplayLength": "400",
                      "scrollX": true,
                      responsive: true,
                      destroy: true,
                      paging: false,
                      searching: false,
                      dom: "Bfrtip",
                      ajax: {
                          url: "/api/ContactAdressAssetsByJob",
                          type: 'POST',
                          async: true,
                          data: function (d) {
                              d.ContactID = getcontactid(),
                                  d.ContactAddressID = gethaid(),
                                  d.AssetID = getAssignhaid()
                          }
                      },
                      timeout: 120000,
                      columns: [
                          {
                              data: null,
                              defaultContent: '',
                              className: 'select-checkbox',
                              orderable: false
                          },
                          { data: "Category.Name", editField: "Assets.Type", title: "<%= Deffinity.systemdefaults.GetCategoryName() %>" },
                          { data: "SubCategory.Name", editField: "Assets.Make", title: "<%= Deffinity.systemdefaults.GetSubCategoryName() %>" },
                          { data: "ProductModel.ModelName", editField: "Assets.Model", title: "Model" },
                          { data: "Assets.SerialNo", title: "Serial Number" },
                          {
                              title: "Purchase Date",
                              data: "Assets.PurchasedDate",
                              render: function (data, type, row) {
                                  return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                              }
                          },
                          { data: "Assets.FromNotes", title: "Notes" },
                      ],
                      order: [1, 'asc'],
                      select: true,
                      buttons: [
                          { extend: "create", editor: editor1, text: "Add New Equipment" }
                      ]
                  });

                  $('#plist').on('click', 'tbody td:first-child', function (e) {
                      //editor.inline(this);
                      if ($(this).parents('tr').hasClass('selected')) {
                          $(this).parents('tr').removeClass('selected');
                      }
                      else {
                          $(this).parents('tr').addClass('selected');
                      }
                      var id = table1.row(this).id();

                      var dataArr = [];
                      $.each($("#plist tbody tr.selected"), function () {
                          var sid = $(this).attr('id');
                          //
                          $("#<%=hpid.ClientID%>").val(sid.replace('row_', ''));
                          dataArr.push(sid.replace('row_', ''));
                      });
                      $("#selectedids").val(dataArr);
                  });
                  $('#plist').on('click', 'tbody td:not(:first-child)', function (e) {
                      editor1.inline(this, {
                          onBlur: 'submit',
                          submit: 'all'
                      });
                  });
                  $('#plist').on('click', 'a.editor_edit_asset', function (e) {
                      e.preventDefault();

                      editor1.edit($(this).closest('tr'), {
                          title: 'Edit record',
                          buttons: 'Update'
                      });
                  });
                  $(".buttons-create").hide();
              }
              catch (e) {
                  var err = e;
              }
          }

                            </script>
                              <div class="row pnl">
                                  <table id="plist" class="col-md-12 display nowrap" style="padding-left:0px;padding-right:0px"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th><%= Deffinity.systemdefaults.GetCategoryName() %></th>
               <th><%= Deffinity.systemdefaults.GetSubCategoryName() %></th>
                <th>Model</th>
                <th>Serial Number</th>
                <th>Purchase Date</th>
                <th>Notes</th>
                <%--<th>&nbsp;</th>--%>
               
            </tr>
        </thead>
                                      </table>
                                  </div>


    <div>
        <script type ="text/javascript">
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
            function CheckOneProduct(obj) {
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
        </script>
       
    </div>

</asp:Panel>

                                </div>
                </div>
            </div>
       



    <asp:Panel ID="pnlOrder" runat="server" Width="100%">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" Visible="false" />
    
    </asp:Panel>
   


    <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 
                                Maintenance Plan Information </h3>
							<div class="card-toolbar">
								
                                
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
              <asp:Panel ID="pnlPolicyInfo" runat="server">
     
    <div class="form-group row">
        <div class="col-md-3">
           <label class="col-sm-5 control-label">Maintenance Plan Type</label>
           <div class="col-sm-7">
               <asp:TextBox ID="txtPolicyType" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
      <div class="col-md-3">
           <label class="col-sm-5 control-label">Maintenance Plan Number</label>
           <div class="col-sm-7">
               <asp:TextBox ID="txtPolicyNumber" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-5 control-label">Start Date</label>
           <div class="col-sm-7">
               <asp:TextBox ID="txtpStartDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-5 control-label">Expiry Date</label>
           <div class="col-sm-7">
               <asp:TextBox ID="txtpExpirtyDate" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
       
</div>
     <div class="form-group row">
          <div class="col-md-3">
             <label class="col-sm-5 control-label">Days Remaining</label>
           <div class="col-sm-7">
               <asp:TextBox ID="txtpDays" runat="server" SkinID="txt_100px" ClientIDMode="Static"></asp:TextBox>
            </div>
            </div>
         </div>
    <div class="form-group row">
        <label class="col-sm-12 control-label">Maintenance Plan Notes</label>
         <div class="col-sm-12" style="min-height:150px;max-height:350px;overflow-y:auto;border-style:groove;">
             <asp:Label ID="lblPolicyNotes" runat="server" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
  
    </asp:Panel>
                                </div>
                </div>
            </div>

   

    <script type="text/javascript">

        //$(window).load(function () {

        //    if ($("[id$='haddressid']").val() != "") {
        //        //alert($("[id$='haddressid']").val());
        //        assets_table_load();
        //        loadByAddressID();

        //    }


        //});

        </script>
     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadByAddressID);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(assets_table_load);
         function loadByAddressID() {

             var id = $("[id$='haddressid']").val();

             if (id != "") {
                 //$("#txtReqTelNo").html("");
                 //$("#txtReqEmailAddress").html("");
                 //$("#txtRequestersDepartment").html("");
                 //$("#txtRequestersJobTitle").html("");
                 //$("#txtRequestersAddress").html("");
                 //$("#txtRequestersCity").html("");
                 //$("#txtRequestersTown").html("");
                 //$("#txtRequestersPostcode").html("");
                 $("[id$='txtPolicyType']").val("");
                 $("[id$='txtPolicyNumber']").val("");
                 $("[id$='txtpStartDate']").val("");
                 $("[id$='txtpExpirtyDate']").val("");
                 $("[id$='txtpDays']").val("");
                 $("[id$='lblPolicyNotes']").html("");
                 //$("[id$='haddressid']").val("");
                 //
                 //$("#txtLocation").html("");
                 try {
                     $.ajax({
                         type: "POST",
                         url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetailsByAddressID",
                         data: "{id:'" + id + "'}",
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (msg) {
                             debugger;
                             //document.getElementById('txtReqTelNo').value = msg.d.Telephone;
                             //document.getElementById('txtReqEmailAddress').value = msg.d.RequesterEmail;
                             //document.getElementById('txtRequestersDepartment').value = msg.d.Department;
                             //document.getElementById('txtRequestersJobTitle').value = msg.d.Title;
                             //document.getElementById('txtRequestersAddress').value = msg.d.Address;
                             //document.getElementById('txtRequestersCity').value = msg.d.City;
                             //document.getElementById('txtRequestersTown').value = msg.d.Town;
                             //document.getElementById('txtRequestersPostcode').value = msg.d.PostCode;
                             //document.getElementById('txtLocation').value = msg.d.Location;
                             $("[id$='txtPolicyType']").val(msg.d.PolicyType);
                             $("[id$='txtPolicyNumber']").val(msg.d.PolicyNumber);
                             $("[id$='txtpStartDate']").val(msg.d.StartDate);
                             $("[id$='txtpExpirtyDate']").val(msg.d.ExpiryDate);

                             //var a = moment(msg.d.ExpiryDate);
                             //var b = moment(new Date());
                             //var d = a.diff(b, 'days');
                             //$("[id$='lblExpiredMsg']").hide();
                             //if (d > 0) {
                             //    $("[id$='txtpDays']").val(d);
                             //    $("[id$='txtpDays']").css("background-color", "green");
                             //    $("[id$='txtpDays']").css("text-align", "right");
                             //}
                             //else {
                             //    $("[id$='txtpDays']").val('Expired');
                             //    $("[id$='txtpDays']").css("background-color", "red");
                             //    $("[id$='lblExpiredMsg']").show();
                             //}
                             $("[id$='txtpDays']").css("color", "white");
                             //$("[id$='txtpDays']").val(msg.d.DaysRemaining);
                             $("[id$='lblPolicyNotes']").html(msg.d.PolicyNotes);
                             //var dd = $find("ccdNa");
                             //alert(msg.d.ID);
                             //dd.add_populated(onCallidPopulated);
                             //dd.set_SelectedValue(msg.d.ID);
                             // dd._onParentChange(null, true);
                             $("[id$='haddressid']").val(msg.d.AddressID);
                             $("[id$='hcid']").val(msg.d.ID);
                             //ddlNameSetVal(msg.d.ID);



                             //var ddl1 = $get('ddlName');
                             //var ccdNa = $find('ccdNa');
                             //ddl1.set_SelectedValue = msg.d.ID
                             //if (ccdNa != null) {
                             //    ccdNa.set_SelectedValue("dev test", msg.d.ID);
                             //}
                             debugger;

                             assets_table_load();
                             //GetAssetRecords(msg.d.ID);

                             //$find("ccdkey").set_contextKey(msg.d.ID);
                             debugger;


                             //SetKeyContactData();
                             debugger;

                             //GetAddressRecords(msg.d.AddressID);
                         }
                     });
                 }
                 catch (err) {
                     var er = err;
                 }
             }
             else {
                 //document.getElementById('txtReqTelNo').value = "";
                 //document.getElementById('txtReqEmailAddress').value = "";
                 //document.getElementById('txtRequestersDepartment').value = "";
                 //document.getElementById('txtRequestersJobTitle').value = "";
                 //document.getElementById('txtRequestersAddress').value = "";
                 //document.getElementById('txtRequestersCity').value = "";
                 //document.getElementById('txtRequestersTown').value = "";
                 //document.getElementById('txtRequestersPostcode').value = "";
                 //document.getElementById('txtLocation').value = "";
             }
             return false;
         }
    </script>

    
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/fixedcolumns/3.2.4/css/fixedColumns.dataTables.min.css">

    <link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="/Web/examples/resources/syntax/shCore.css">--%>
   <%-- <link rel="stylesheet" type="text/css" href="/Web/examples/resources/demo.css">--%>
     
    <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>
   
     
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
   
    
    <style type="text/css" class="init">
        div.dataTables_wrapper {
        /*width: 800px;*/
        margin: 0 auto;
    }
        div.dt-buttons{
            float:right;
        }
        table.dataTable.stripe tbody > tr.odd.selected, table.dataTable.stripe tbody > tr.odd > .selected, table.dataTable.display tbody > tr.odd.selected, table.dataTable.display tbody > tr.odd > .selected {
    background-color: #ffffff;
}
table.dataTable.display tbody > tr.selected:hover > .sorting_1, table.dataTable.order-column.hover tbody > tr.selected:hover > .sorting_1 {
    background-color: #ffffff;
}
table.dataTable.display tbody > tr.odd.selected > .sorting_1, table.dataTable.order-column.stripe tbody > tr.odd.selected > .sorting_1 {
    background-color: #ffffff;
}
    </style>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
