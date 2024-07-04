<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CustomerEquimentList.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.CustomerEquimentList" %>

<%@ Register Src="~/WF/CustomerAdmin/Controls/ContactTabCtrl.ascx" TagPrefix="Pref" TagName="ContactTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Contact Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:ContactTabCtrl runat="server" ID="ContactTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Literal ID="lblContact" runat="server"></asp:Literal> - <asp:Literal ID="lblAddress" runat="server" Text="Equipment"></asp:Literal>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row" style="display:none;visibility:hidden;">
								<div class="col-md-1">
									<label class="control-label" for="name">Address List</label>
								</div>
								<div class="col-md-8">
                                    <asp:DropDownList ID="ddlAddress" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddress_SelectedIndexChanged"></asp:DropDownList>
								</div>
							</div>
     <asp:HiddenField ID="hsid" runat="server" ClientIDMode="Static" />
      <asp:HiddenField ID="pstatus" runat="server" Value="" ClientIDMode="Static" />
             <asp:HiddenField ID="sid" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="selectedids" runat="server" Value="" ClientIDMode="Static" />
                            <asp:HiddenField ID="haid" runat="server" Value="0" ClientIDMode="Static" />
    <%--<button id="btn">btn</button>--%>
            <script type="text/javascript">
                function ShowModalPopup() {
                    $find("mpe").show();
                    return false;
                }
                function HideModalPopup() {
                    $find("mpe").hide();
                    return false;
                }
</script>
         <asp:HiddenField ID="huid" runat="server" ClientIDMode="Static" />
 <script type="text/javascript" language="javascript" class="init">

     $(document).ready(function () {
         $('#haid').val(getUrlParameter('addid'));
         $('.pnl').show();
         assets_table_load();
         setStatusBackColor();
     });

     function getUser() {
         return $("[id$='huid']").val();
     };
     function getUrlParameter(name) {
         name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
         var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
         var results = regex.exec(location.search.toLowerCase());

         return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
     };

     var editor; // use a global for the submit and return data rendering in the examples
     var selected = [];
     var table;
     function BindAddress() {
         editor = new $.fn.dataTable.Editor({
             ajax: "/api/ContactAdress",
             table: "#example",
             fields: [
                  {
                      label: "ContactID:",
                      name: "PortfolioContactAddress.ContactID",
                      type: "hidden",
                      def: getUrlParameter('ContactID')
                  },
                   {
                       label: "LoggedBy:",
                       name: "PortfolioContactAddress.LoggedBy",
                       type: "hidden",
                       def: getUser()
                   },

                 {
                     label: "Address1:",
                     name: "PortfolioContactAddress.Address"
                 },
                  {
                      label: "Address2:",
                      name: "PortfolioContactAddress.Address2"
                  },
                 {
                     label: "City:",
                     name: "PortfolioContactAddress.City"
                 },
                  {
                      label: "State:",
                      name: "PortfolioContactAddress.State"
                  },
                   {
                       label: "Zipcode:",
                       name: "PortfolioContactAddress.PostCode"
                   },
             
             ]
         }
         );

       
         //editor.on('preSubmit', validation);  //execute presubmit
         // Activate an inline edit on click of a table cell
         $('#example').on('click', 'tbody td:not(:first-child)', function (e) {

             editor.inline(this, {
                 onBlur: 'submit',
                 submit: 'all'

             });
         });

         editor.on('preSubmit', function (e, d, action) {
             if (action === 'create' || action === 'edit') {

             }


             if (action == 'edit') {
                 //alert(data.ProjectBOM.Material);
             }

         });


         table = $('#example').DataTable({
             "iDisplayLength": "400",
             "scrollX": true,
             responsive: true,
             destroy: true,
             paging: true,
             searching: false,
             dom: "Bfrtip",
             ajax: {
                 url: "/api/ContactAdress",
                 type: 'POST',
                 data: function (d) {
                     d.ContactID = getUrlParameter('ContactID')
                 }
             },
             columns: [
                 //{
                 //    data: null,
                 //    className: "center",
                 //    defaultContent: '<a href="" class="editor_edit"><i class="fa fa-pencil"></i></a>',
                 //    orderable: false
                 //},
                  {
                      data: null,
                      className: "center",
                      defaultContent: '<a href="" class="show_edit"><i class="fa fa-pencil"></i></a>',
                      orderable: false
                  },

                 { data: "PortfolioContactAddress.Address" },
                  { data: "PortfolioContactAddress.Address2" },
                 { data: "PortfolioContactAddress.City" },
                  { data: "PortfolioContactAddress.State" },
                 { data: "PortfolioContactAddress.PostCode" },
                 {
                     data: "ProductPolicyType.Title", editField: "PortfolioContactAddress.PolicyTypeID",
                     "visible": false
                 },
                 {
                     data: "PortfolioContactAddress.PolicyNumber",
                     "visible": false
                 },
                 {
                     data: "PortfolioContactAddress.StartDate",
                     className: "dt-right",
                     render: function (data, type, row) {
                         return (moment(row.PortfolioContactAddress.StartDate).format("MM/DD/YYYY"));
                     },
                     "visible": false
                 },
                 {
                     data: "PortfolioContactAddress.ExpiryDate",
                     className: "dt-right",
                     render: function (data, type, row) {
                         return (moment(row.PortfolioContactAddress.ExpiryDate).format("MM/DD/YYYY"));
                     },
                     "visible": false
                 },
                 {
                     data: "PortfolioContactAddress.DaysRemaining",
                     className: "dt-right",
                     
                     render: function (data, type, row) {
                         var a = moment(row.PortfolioContactAddress.ExpiryDate);
                         // var b = moment(row.PortfolioContactAddress.StartDate);
                         var b = moment(new Date());
                         var d = a.diff(b, 'days');
                         $(row).find($(this)).css('color', 'red');
                         if (d > 0)
                             return (d);
                         else
                             return ("<span style='color:white;font-weight:bold;' class='statuscls1'>Expired</span>");
                     },
                     "visible": false
                 },
                 {
                     orderable: false,
                     data: null,
                     className: "center",
                     defaultContent: '<a href="" class="show_edit1 btn btn-secondary" style="margin-bottom:0px">Warranty</a>',
                     //"visible": false
                 },
                  {
                      orderable: false,
                      data: null,
                      className: "center",
                      defaultContent: '<a href="" class="show_list btn btn-secondary" style="margin-bottom:0px">Equipment</a>',
                      //"visible": false
                  },
                  {
                      orderable: false,
                      data: null,
                      className: "center",
                      defaultContent: '<a href="" class="editor_remove"><i class="fa fa-trash"></i></a>'
                      
                  },
             { data: "PortfolioContactAddress.Amount", "visible": false },
             { data: "PortfolioContactAddress.BillingName", "visible": false },
             { data: "PortfolioContactAddress.BillingAddress1", "visible": false },
             { data: "PortfolioContactAddress.BillingAddress2", "visible": false },
             { data: "PortfolioContactAddress.BillingCity", "visible": false },
             { data: "PortfolioContactAddress.BillingState", "visible": false },
             { data: "PortfolioContactAddress.BillingZipcode", "visible": false },
              { data: "PortfolioContactAddress.LoggedBy", "visible": false }
             
             ],
             order: [1, 'asc'],
             select: {
                 style: 'single'
                 //selector: 'td:last-child'
             },
             buttons: [
                 { extend: "create", editor: editor, text: "Add New Address" }
                 //{ extend: "edit", editor: editor },
                 //{ extend: "remove", editor: editor }
             ]
         });

         //editor.dependent('PortfolioContactAddress.PolicyTypeID', function (val, data, callback) {
         //    setLable();
         //    setPolicyNumber(data);
         //});
         //editor.dependent('PortfolioContactAddress.PolicyStartsID', function (val, data, callback) {
         //    setstartDate(data);
         //});
         //editor.dependent('PortfolioContactAddress.StartDate', function (val, data, callback) {
         //    setendDate(data);
         //    setPolicyNumber(data);
         //});
         //editor.dependent('PortfolioContactAddress.ContractTermID', function (val, data, callback) {
         //    setendDate(data);
         //});

         $('#example').on('click', 'tbody td:first-child', function (e) {
             //editor.inline(this);
             if ($(this).parents('tr').hasClass('selected')) {
                 $(this).parents('tr').removeClass('selected');
             }
             else {
                 //table.$('tr.selected').removeClass('selected');
                 $(this).parents('tr').addClass('selected');
             }

             var id = table.row(this).id();
             //alert(id);
             var dataArr = [];
             $.each($("#example tbody tr.selected"), function () {
                 var sid = $(this).attr('id');

                 dataArr.push(sid.replace('row_', ''));
             });
             $("#selectedids").val(dataArr);
         });


         // Edit record
         $('#example').on('click', 'a.save_saving', function (e) {
             e.preventDefault();
             ShowModalPopup();
             //alert($(this).closest('tr').attr('id').replace('row_', ''));
             ShowCurrentTime($(this).closest('tr').attr('id').replace('row_', ''));
         });
         // Edit record
         $('#example').on('click', 'a.editor_edit', function (e) {
             e.preventDefault();

             editor.edit($(this).closest('tr'), {
                 title: 'Edit record',
                 buttons: 'Update'
             });
         });

        
         // Delete a record
         $('#example').on('click', 'a.editor_remove', function (e) {
             e.preventDefault();

             editor.remove($(this).closest('tr'), {
                 title: 'Delete record',
                 message: 'Are you sure you wish to remove this record?',
                 buttons: 'Delete'
             });
         });

         //
         //$('#example').on('click', 'a.show_list', function (e) {
         //    e.preventDefault();
         //    var tr = $(this).closest('tr');
         //    var row = table.row(tr);

         //    var data1 = row.data();
         //    var d = data1['PortfolioContactAddress']["ID"];
             
         //    $('#haid').val(data1['PortfolioContactAddress']["ID"]);
         //    $('.pnl').show();
         //    assets_table_load();
         //    //$('#plist').DataTable().ajax.reload();
         //    //editor.remove($(this).closest('tr'), {
         //    //    title: 'Delete record',
         //    //    message: 'Are you sure you wish to remove this record?',
         //    //    buttons: 'Delete'
         //    //});
         //});
         $('#example').on('click', 'a.show_edit', function (e) {
             e.preventDefault();
             var tr = $(this).closest('tr');
             var row = table.row(tr);

             var data1 = row.data();
             var d = data1['PortfolioContactAddress']["ID"];
             window.location.href = "ContactAddressDetailsBasic.aspx?ContactID=" + getUrlParameter('ContactID') + "&addid=" + d;
             //debugger;
             //$('#haid').val(data1['PortfolioContactAddress']["ID"]);
             //$('.pnl').show();
             //assets_table_load();
             //$('#plist').DataTable().ajax.reload();
             //editor.remove($(this).closest('tr'), {
             //    title: 'Delete record',
             //    message: 'Are you sure you wish to remove this record?',
             //    buttons: 'Delete'
             //});
         });
         $('#example').on('click', 'a.show_edit1', function (e) {
             e.preventDefault();
             var tr = $(this).closest('tr');
             var row = table.row(tr);

             var data1 = row.data();
             var d = data1['PortfolioContactAddress']["ID"];
             window.location.href = "ContactAddressDetails.aspx?ContactID=" + getUrlParameter('ContactID') + "&addid=" + d;
             //debugger;
             //$('#haid').val(data1['PortfolioContactAddress']["ID"]);
             //$('.pnl').show();
             //assets_table_load();
             //$('#plist').DataTable().ajax.reload();
             //editor.remove($(this).closest('tr'), {
             //    title: 'Delete record',
             //    message: 'Are you sure you wish to remove this record?',
             //    buttons: 'Delete'
             //});
         });
         //selectall
         $('#selectall').on('click', 'input.selectall', function (e) {
             e.preventDefault();
             //alert('t');
             table.rows().select();
         });


         $('#selectall').click(function (e) {
             //alert('d');
             if ($(this).is(':checked'))
                 table.rows().select();
             else
                 table.rows().deselect();


             var dataArr = [];
             $.each($("#example tbody tr.selected"), function () {
                 var sid = $(this).attr('id');

                 dataArr.push(sid.replace('row_', ''));
             });

             //alert(dataArr);
             //var sid = id.replace('row_', '');
             $("#selectedids").val(dataArr);
         });


         $(".buttons-create").hide();

         //$('#test').find('div').each(function () {
         //    var innerDivId = $(this).attr('id');
         //});
         debugger;
         setDeleteButton();
     }


     function setDeleteButton()
     {
         $(window).load(function () {
             $(".editor_remove").each(function (index) {
                 if ($('#hsid').val() != "1") {
                     $(this).hide();
                 }
             });
         });
        
         //$('.editor_remove').each(function(){
         //    // var atId = this.id;
         //    //$(this).hide();
         //    // do something with it
         //    //if ($('#hsid').val() != "1") {
         //    //    debugger;
         //    //   // $('.editor_remove').hide()
         //    //}
         //});​
       
     }
     //function setPolicyNumber(data) {
     //    try {
            
     //        $.ajax({
     //            url: '/api/getpolicyno',
     //            data: {
     //                typeid: data
     //            },
     //            type: 'post',
     //            dataType: 'json',
     //            success: function (json) {
                    
     //                //callback(json.options);
     //                var tt = json.options.policyno;
     //                if (tt != "")
     //                    editor.set('PortfolioContactAddress.PolicyNumber', tt);
     //                //editor.field('PortfolioContactAddress.PolicyNumber').update('test');
     //            }
     //        });
     //    }
     //    catch (err) {

     //    }
     //}
     //function setendDate(data)
     //{
     //    try{
         
     //    $.ajax({
     //        url: '/api/getenddate',
     //        data: {
     //            typeid: data
     //        },
     //        type: 'post',
     //        dataType: 'json',
     //        success: function (json) {
                 
     //            //callback(json.options);
     //            var tt = json.options.enddate;
     //            if (tt != "") {

     //                editor.set('PortfolioContactAddress.ExpiryDate', moment(tt).format("MM/DD/YYYY"));
     //            }
     //            //editor.field('PortfolioContactAddress.PolicyNumber').update('test');
     //        }
     //    });
     //    }
     //    catch (err) {

     //    }
     //}

     //function setstartDate(data) {
     //    try {
             
     //        $.ajax({
     //            url: '/api/getstartdate',
     //            data: {
     //                typeid: data
     //            },
     //            type: 'post',
     //            dataType: 'json',
     //            success: function (json) {
                     
     //                //callback(json.options);
     //                var tt = json.options.startdate;
     //                if (tt != "") {

     //                    editor.set('PortfolioContactAddress.StartDate', moment(tt).format("MM/DD/YYYY"));
     //                }
     //                //editor.field('PortfolioContactAddress.PolicyNumber').update('test');
     //            }
     //        });
     //    }
     //    catch (err) {
            
     //    }
     //}

     $(document).ready(function () {

         if ($('#haid').val() == "0")
         {
             $('.pnl').hide();
         }

         $.fn.dataTable.moment('MM/DD/YYYY');
         //BindAddress();
         $('#btn').click(function (e) {
             e.preventDefault();
             // editor.inline($('#example tbody tr:first-child td:first-child'));
             var dataArr = [];
             $.each($("#example tbody tr.selected"), function () {
                 //alert($(this).attr('id').replace('row_', ''));
                 dataArr.push($(this).attr('id').replace('row_', ''));
             });
             //alert(dataArr);
             //alert(rowCount);
             return false;
         });
       
         
        



        

     });


     function GetTrackUrl(l, m, mi, p, imurl, t) {

         if (l != 0)
             return '<a href="' + 'ProjectLabourTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '"  border="0"></a>';
         else if (m != 0)
             return '<a href="' + 'ProjectMaterialTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';
         else if (mi != 0)
             return '<a href="' + 'ProjectMiscTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';

     }

   
    </script>
             <div class="row" id="pnladdress" runat="server">
                 

                 <asp:HyperLink style="display:none;visibility:hidden;" ID="btnAddNewAddress" runat="server" Text="Add New Address" SkinID="Button" NavigateUrl="~/WF/CustomerAdmin/ContactAddressDetailsBasic.aspx"   />
            <table id="example" class="col-md-12 display nowrap"  cellspacing="0" width="100%" style="display:none;visibility:hidden;">
        <thead>
            <tr>
                <th></th>
                <th>Address1</th>
                 <th>Address2</th>
                <th>City</th>
                <th>State</th>
                <th>Zipcode</th>
                <th>Policy Type</th>
                <th>Policy Number</th>
                <th>Start Date</th>
                
                <th>Expiry Date</th>
                <th>Days Remaining </th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
               
            </tr>
        </thead>
       
    </table>
                     
            </div>
                            <script type="text/javascript" language="javascript"  class="init">

                                function gethaid()
                                {
                                    return $('#haid').val();
                                }

                                var editor1;
                                var selected1 = [];
                                var table1;
                                function assets_table_load()
                                {
                                    editor1 = new $.fn.dataTable.Editor({
                                        ajax: "/api/ContactAdressAssets",
                                        table: "#plist",
                                        fields: [
                                             {
                                                 label: ":",
                                                 name: "Assets.ContactID",
                                                 type: "hidden",
                                                 def: getUrlParameter('ContactID')
                                             },
                                              //{
                                              //    label: ":",
                                              //    name: "Assets.ContactAddressID",
                                              //    type: "hidden",
                                              //    def: gethaid()
                                              //},
                                            {
                                                label: "Address",
                                                name: "Assets.ContactAddressID",
                                                type: "select",
                                                placeholder: "Please select..."
                                            },
                                            {
                                                label: "<%= Deffinity.systemdefaults.GetCategoryName() %>:",
                                                name: "Assets.Type",
                                                type: "select",
                                                placeholder: "Please select..."
                                            },
                                           {
                                               label: "<%= Deffinity.systemdefaults.GetSubCategoryName() %>:",
                                               name: "Assets.Make",
                                               type: "select",
                                               placeholder: "Please select..."
                                           },
                                             {
                                                 label: "Model:",
                                                 name: "Assets.Model",
                                                 type: "select",
                                                 placeholder: "Please select..."
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
                                                label: "Warranty Term:",
                                                name: "Assets.AssetsTypeID",
                                                type: "select",
                                                placeholder: "Please select..."
                                            },
                                            {
                                                label: "Expiry Date:",
                                                name: "Assets.ExpDate",
                                                type: 'datetime',
                                                def: function () { return new Date(); },
                                                dateFormat: 'MM/DD/YYYY'
                                               

                                            },
                                          {
                                              label: "Notes:",
                                              name: "Assets.FromNotes"
                                            },
                                            {
                                                label: "Image:",
                                                name: "Assets.Image",
                                                type: "upload",
                                                display: function (file_id) {
                                                    debugger;
                                                    //var f = editor.file('files', file_id).web_path;
                                                    debugger;
                                                    return '<img src="' + '\\WF\\UploadData\\Assets\\' + file_id + '.png' + '" height=75px/>';
                                                },
                                                clearText: "Clear",
                                                noImageText: 'No image'
                                            }
                                          //{
                                          //    label: 'Image:',
                                          //    name: 'Assets.image',
                                          //    type: 'upload',
                                          //    noFileText: 'No image'
                                          //},

                                        ]
                                    }
                                        );


                                    table1 = $('#plist').DataTable({
                                        "iDisplayLength": "400",
                                        "scrollX": true,
                                        responsive: true,
                                        destroy: true,
                                        paging: true,
                                        searching: false,
                                        dom: "Bfrtip",
                                        ajax: {
                                            url: "/api/ContactAdressAssets",
                                            type: 'POST',
                                            data: function (d) {
                                                d.ContactID = getUrlParameter('ContactID'),
                                                d.ContactAddressID = gethaid()
                                            }
                                        },
                                        columns: [

                                            {
                                                data: null,
                                                className: "center",
                                                defaultContent: '<a href="" class="editor_edit_asset"><i class="fa fa-pencil"></i></a>',
                                                orderable: false
                                            },
                                            {
                                                data: "Assets.Image",
                                                render: function (file_id) {
                                                    debugger;
                                                    // var v = editor.file('files', file_id).web_path;
                                                    //return file_id ?
                                                    //    '<img src="' + editor.file('files', file_id).web_path + '"/>' :
                                                    //    null;
                                                    return file_id ?
                                                        '<img src="' + '\\WF\\UploadData\\Assets\\' + file_id + '.png' + '" height=75px/>' :
                                                        null;
                                                },
                                                defaultContent: "No image",
                                                title: "Image"
                                            },
                                            { data: "PortfolioContactAddress.Address", editField: "Assets.ContactAddressID" },
                                            { data: "Category.Name", editField: "Assets.Type", title: "<%= Deffinity.systemdefaults.GetCategoryName() %>" },
                                            { data: "SubCategory.Name", editField: "Assets.Make", title: "<%= Deffinity.systemdefaults.GetSubCategoryName() %>"},
                                            { data: "ProductModel.ModelName", editField: "Assets.Model" },
                                            { data: "Assets.SerialNo" },
                                            {
                                                data: "Assets.PurchasedDate",
                                                render: function (data, type, row) {
                                                    return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                                                }
                                            },
                                            { data: "WarrantyTerm.Name", editField: "Assets.AssetsTypeID" },
                                            {
                                                data: "Assets.ExpDate",
                                                //render: function (data, type, row) {
                                                //    return (moment(row.Assets.ExpDate).format("MM/DD/YYYY"));
                                                //}
                                                render: function (data, type, row) {
                                                    debugger;
                                                    var a = moment(row.Assets.ExpDate);
                                                    // var b = moment(row.PortfolioContactAddress.StartDate);
                                                    var b = moment(new Date());
                                                    var d = a.diff(b, 'days');
                                                   // $(row).find('td: eq(3)').css('color', 'red');
                                                    if (d > 0)
                                                        return ((moment(row.Assets.ExpDate).format("MM/DD/YYYY")));
                                                    else
                                                        return ("<span style='color:white;font-weight:bold;' class='statuscls1'>Expired</span>");
                                                },
                                            },
                                            { data: "Assets.FromNotes" },
                                           
                                            // {
                                            //     orderable: false,
                                            //     data: null,
                                            //     className: "center",
                                            //     defaultContent: '<a href="" class="show_list">List Appliances</a>'
                                            // },
                                             //{
                                             //    data: 'Assets.image',
                                             //    render: function (data, type, row) {
                                             //        return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                                             //    },
                                             //    defaultContent: 'No image'
                                             //},
                                        ],
                                        order: [1, 'asc'],
                                        select: {
                                            style: 'multi',
                                            selector: 'td:first-child'
                                        },
                                        buttons: [
                                            { extend: "create", editor: editor1, text: "Add New Equipment" }

                                        ]
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
                                    editor1.dependent('Assets.Type', function (val, data, callback) {
                                        
                                        $.ajax({
                                            url: '/api/GetMakeData',
                                            data: {
                                                typeid: val
                                            },
                                            type: 'post',
                                            dataType: 'json',
                                            success: function (json) {
                                                
                                                //callback(json.options);
                                                editor1.field('Assets.Make').update(json.options.Assets_Make);
                                            }
                                        });
                                    });
                                    editor1.dependent('Assets.Make', function (val, data, callback) {
                                        
                                        $.ajax({
                                            url: '/api/getmodeldata',
                                            data: {
                                                makeid: val
                                            },
                                            type: 'post',
                                            dataType: 'json',
                                            success: function (json) {
                                                
                                                //callback(json.options);
                                                editor1.field('Assets.Model').update(json.options.Assets_Model);
                                            }
                                        });
                                    });
                                    editor1.dependent('Assets.AssetsTypeID', function (val, data, callback) {
                                        getexpiredatenew(data);
         });
                                    //editor1.dependent('Assets.Type', '/api/countries');
                                   // editor1.dependent("department", "work_cell");
                                }
                                $(document).ready(function () {
                                    //$('#plist').on('postCreate', function (e, json) {
                                    //    alert('New row added');
                                    //})

                                    //postCreate
                                    $('#plist').on('create', function (e, json, data) {
                                        alert('New row added');
                                    });
                                    //assets_table_load();
                                   
                                    //editor.on('preSubmit', validation);  //execute presubmit
                                    // Activate an inline edit on click of a table cell
                                    //$('#plist').on('click', 'tbody td:not(:first-child)', function (e) {

                                    //    editor1.inline(this, {
                                    //        onBlur: 'submit',
                                    //        submit: 'all'

                                    //    });
                                    //});

                                });

                                function getexpiredatenew(data)
     {
         try{

         $.ajax({
             url: '/api/getexpiredatenew',
             data: {
                 typeid: data
             },
             type: 'post',
             dataType: 'json',
             success: function (json) {

                 try {
                     var tt = json.options.enddate;
                     if (tt != "") {
                         debugger;
                         editor1.set('Assets.ExpDate', moment(tt).format("MM/DD/YYYY"));
                     }
                     //editor.field('PortfolioContactAddress.PolicyNumber').update('test');
                 }
                 catch (error) {
                     var e = error;
                 }
             }
         });
         }
         catch (err) {

         }
     }
                            </script>
                              <div class="row pnl">
                                  <table id="plist" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Address</th>
                <th>Type</th>
               <th>Make</th>
                <th>Model</th>
                <th>Serial Number</th>
                <th>Purchase Date</th>
                <th>Warranty Term</th>
                <th>Expiry Date</th>
                <th>Notes</th>
                <th>&nbsp;</th>
               
            </tr>
        </thead>
                                      </table>
                                  </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
     
   


    
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.dataTables.min.css">--%>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
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
  <%--  <script type="text/javascript" language="javascript" src="/web/examples/resources/syntax/shCore.js">
    </script>--%>
    <%--<script type="text/javascript" language="javascript" src="/Web/examples/resources/demo.js">
    </script>--%>
    <%--<script type="text/javascript" language="javascript" src="/Web/examples/resources/editor-demo.js">
    </script>--%>

    <script>
        $(window).load(function () {
            $("#MainContent_MainContent_pnlUploadtemplate").each(function (index) {

                $(this).parent('div').css("overflow-x", "hidden");
            });
        });
        //$(window).load(function () {
        //    $('#MainContent_MainContent_pnlUploadtemplate').closest('div').css("overflow-y", "visible");
        //});
        //$('#MainContent_MainContent_pnlUploadtemplate').closest('div').css("overflow-y", "visible");
        //MainContent_MainContent_pnlUploadtemplate
    </script>
     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setLable);
         setStatusBackColor();
         setLable();
         //DTE_Field DTE_Field_Type_text DTE_Field_Name_PortfolioContactAddress.Amount
         //DTE_Field DTE_Field_Type_text DTE_Field_Name_PortfolioContactAddress.BillingName
         $(window).load(function () {
             $('.statuscls1').each(function () {
                  var s = $(this).html();
                 if (s == 'Expired')
                     
                     $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });

             });
         });

         function setStatusBackColor() {

             $('.statuscls').each(function () {

                 var s = $(this).html();
                 if (s == 'Expired') {
                     debugger;
                     $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle", "color": "white", "font-weight": "bold" });
                 }
             });

         }

         function setLable() {
             var i = 0;
             $('.DTE_Field').each(function () {
                 i = i + 1;

                 if (i == 15) {
                     if ($("p").hasClass("bclass").toString() == "false")
                         $(this).prepend('<p> <input type="checkbox" name="cinfo" value="1" class="cinfo" onclick="cinfoclick();"> Copy Home Information </p><p class="bclass" style="font-size: medium;">Billing Information</p><hr>');
                 }
             });
         }

         function cinfoclick() {

             $('#DTE_Field_PortfolioContactAddress-BillingAddress1').val($('#DTE_Field_PortfolioContactAddress-Address').val());
             $('#DTE_Field_PortfolioContactAddress-BillingAddress2').val($('#DTE_Field_PortfolioContactAddress-Address2').val());
             $('#DTE_Field_PortfolioContactAddress-BillingCity').val($('#DTE_Field_PortfolioContactAddress-City').val());
             $('#DTE_Field_PortfolioContactAddress-BillingState').val($('#DTE_Field_PortfolioContactAddress-State').val());
             $('#DTE_Field_PortfolioContactAddress-BillingZipcode').val($('#DTE_Field_PortfolioContactAddress-PostCode').val());
             //alert('checked');
             return false;
         }


</script>
</asp:Content>

