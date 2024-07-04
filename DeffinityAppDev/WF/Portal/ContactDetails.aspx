<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMain.Master" AutoEventWireup="true" CodeBehind="ContactDetails.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.PortalContactDetailsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Customer Profile
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hsid" runat="server" ClientIDMode="Static" />
    <asp:Panel id="pnl_profile" runat="server">
    	
				<div class="panel panel-headerless" >
					<div class="panel-body">
			
						<div class="member-form-add-header">
                            <div class="row">
                                 <div class="col-sm-10" style="margin-bottom:5px">
                <asp:Literal ID="lblmsg" runat="server" Text="" EnableViewState="false"></asp:Literal>
                <asp:Literal ID="lblerrmsg" runat="server" Text="" EnableViewState="false"></asp:Literal>
                <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="add" Visible="false" />
                                     </div>
            </div>
                            <div class="row" id="divPercent" runat="server" visible="false">
                                <div class="col-sm-10">
                                    <p class="bg-info" id="lblinfo" runat="server">
                                        </p>
                                   
                                    <br />
                                    </div>

                            </div>
							<div class="row">
								
								<div class="col-md-10 col-sm-8">
			                       <div class="user-info">
									<div class="user-img">
                                       <asp:Image ID="imguser" runat="server" CssClass="img-responsive img-circle" ImageUrl="~/WF/Admin/ImageHandler.ashx?type=contact&id=0"  ClientIDMode="Static" AlternateText="user-pic"/>
                                      </div>
                                          <div class="user-name">
                                            <asp:Label ID="lblFullName" runat="server" ClientIDMode="Static" ForeColor="Black" Font-Size="X-Large" Text="[Add New Contact]"></asp:Label> <span id="lblUsertype" runat="server"></span>  <br />
                                               <a id="lbtnUpload" style="font-size:small;cursor:pointer;" class="label label-default" title="Upload Image" runat="server">Upload</a>
                                                    <div id="contactupload" style="display:none">
                                        <asp:FileUpload ID="Fileupload_contact" runat="server" /><br />
                                    <asp:Button ID="btnuser" runat="server" SkinID="btnUpload" OnClick="btnuser_Click" ValidationGroup="add" />
                                              <asp:Button ID="btncancel" runat="server" SkinID="btnCancel"/>
                                            </div>
                                          	</div>	
                                      					
									<br />
                                       <br />
									
			                       </div>
								</div>
                                <div class="col-md-2 col-sm-4 pull-right-sm">
									<div class="action-buttons">
                                        <asp:Button ID="btnupdate"  runat="server" Text="Save Changes" CssClass="btn btn-block btn-secondary" ValidationGroup="add" OnClick="btnupdate_Click" CausesValidation="false"  />
                                        <asp:Button ID="btnreset" runat="server" CssClass="btn btn-block btn-gray" Text="Back to Contact list" OnClick="btnreset_Click" CausesValidation="false" Visible="false"  />
									</div>
								</div>
							</div>
						</div>
						<div class="member-form-inputs">			
							<div class="row">
								<div class="col-md-2">
									<label class="control-label" for="name"><%= Resources.DeffinityRes.Name%></label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtname" runat="server" SkinID="txt_90" MaxLength="250"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="Rf_name" runat="server" ControlToValidate="txtname"
                                                            ErrorMessage="Please enter name" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
								</div>
							</div>
			
							<div class="row">
								<div class="col-md-2">
									<label class="control-label" for="birthdate"><%= Resources.DeffinityRes.EmailAddress%></label>
								</div>
								<div class="col-md-4 form-inline">
		                            	<asp:TextBox ID="txtEmail" runat="server" SkinID="txt_90" MaxLength="500"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfemail" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Please enter email" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                                      <%--<asp:RegularExpressionValidator ID="validmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage=" Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="add"></asp:RegularExpressionValidator>--%>
								</div>
							</div>
                                  <div class="row" style="display:none;">
								<div class="col-md-2">
									<label class="control-label" for="name"><%= Resources.DeffinityRes.Address%></label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtAddress"  SkinID="txt_90" runat="server" MaxLength="1000"></asp:TextBox>
								</div>
                                      <div class="col-sm-4">

                                          </div>
							</div>
                             <div class="" style="display:none;">
									<label class="control-label col-sm-2" for="name">Town</label>
								<div class="col-md-4">
									 <asp:TextBox ID="txtTown" runat="server" SkinID="txt_90"  MaxLength="100"></asp:TextBox>
								</div>
							</div>
                             <div class="" style="display:none">
									<label class="control-label col-sm-2" for="name"><%= Resources.DeffinityRes.City%></label>
								<div class="col-md-4">
									 <asp:TextBox ID="txtCity" runat="server" SkinID="txt_90"  MaxLength="100"></asp:TextBox>
								</div>
							</div>
                                  <div class="" style="display:none;">
								<div class="col-md-2">
									<label class="control-label" for="name">Postcode/Zipcode</label>
								</div>
								<div class="col-md-4">
                                     <asp:TextBox ID="txtPostal" SkinID="txt_90" runat="server" MaxLength="50"></asp:TextBox>
								</div>
							</div>
                            <div class="row">
								<div class="col-md-2">
									<label class="control-label" for="name">Home Phone</label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtTelephone" SkinID="txt_90" ValidationGroup="update" runat="server" MaxLength="20"></asp:TextBox>
                                    <%--<asp:RangeValidator ID="RangeValidator1" ValidationGroup="update" runat="server" ControlToValidate="txtzipcode"
                                         ErrorMessage="Please enter valid zip code" MaximumValue="999999" MinimumValue="100000"></asp:RangeValidator--%>
								</div>
							</div>
                              <div class="row">
								<div class="col-md-2">
									<label class="control-label" for="name">Mobile</label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtmobileno" runat="server" SkinID="txt_90" MaxLength="20"></asp:TextBox>
								</div>
							</div>
                            
                              <div class="" style="display:none;">
          
              <div class="col-md-2">
              <label class="control-label" for="name">Warranty documents</label>
                  </div>
               <div class="col-sm-4">
                    <asp:FileUpload ID="fileupload" runat="server" ClientIDMode="Static" AllowMultiple="true" />
               </div>
            
         
    </div>
    <div class="" style="display:none">
          <div class="col-md-12">
               <div class="col-sm-6">
                   <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand">
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false" />
                       <asp:TemplateField HeaderText="File Name">
                           <ItemTemplate>
                               <asp:LinkButton ID="btndownload" runat="server" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' CommandName="Download"></asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="100px">
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
               </div>
          </div>
    </div>



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
         
 <script type="text/javascript" language="javascript" class="init">

     function getUrlParameter(name) {
         name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
         var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
         var results = regex.exec(location.search.toLowerCase());
         return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
     };

     var editor; // use a global for the submit and return data rendering in the examples

     $(document).ready(function () {

         if ($('#haid').val() == "0")
         {
             $('.pnl').hide();
         }

         $.fn.dataTable.moment('MM/DD/YYYY');
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
                     label: "Address:",
                     name: "PortfolioContactAddress.Address"
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
                       label: "Post code:",
                       name: "PortfolioContactAddress.PostCode"
                   },
             {
                 label: "Policy type:",
                 name: "PortfolioContactAddress.PolicyTypeID",
                 type: "select",
                 placeholder: "Please select..."
             },
             {
                 label: "Policy number:",
                 name: "PortfolioContactAddress.PolicyNumber"
             },
              {
                  label: "Start date:",
                  name: "PortfolioContactAddress.StartDate",
                  type: 'datetime',
                  def: function () {
                      //return new Date();
                      return moment(new Date()).format("MM/DD/YYYY")
                  },
                  dateFormat: "MM/DD/YYYY"

              },
                {
                    label: "Expiry date:",
                    name: "PortfolioContactAddress.ExpiryDate",
                    type: 'datetime',
                    def: function () {
                        //return new Date();
                        return moment(new Date(), "MM/DD/YYYY").add('year', 1).format("MM/DD/YYYY");
                    },
                    dateFormat: "MM/DD/YYYY"
                },
                 {
                     label: "Days Remaining:",
                     name: "PortfolioContactAddress.DaysRemaining",
                     type: "hidden"
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
         var selected = [];
         var table;

         
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
                 { data: "PortfolioContactAddress.City" },
                  { data: "PortfolioContactAddress.State" },
                 { data: "PortfolioContactAddress.PostCode" },
                 { data: "ProductPolicyType.Title", editField: "PortfolioContactAddress.PolicyTypeID" },
                 { data: "PortfolioContactAddress.PolicyNumber" },
                 {
                     data: "PortfolioContactAddress.StartDate",
                     className: "dt-right",
                     render: function (data, type, row) {
                         return (moment(row.PortfolioContactAddress.StartDate).format("MM/DD/YYYY"));
                     }
                 },
                 {
                     data: "PortfolioContactAddress.ExpiryDate",
                     className: "dt-right",
                     render: function (data, type, row) {
                         return (moment(row.PortfolioContactAddress.ExpiryDate).format("MM/DD/YYYY"));
                     }
                 },
                 {
                     data: "PortfolioContactAddress.DaysRemaining",
                     className: "dt-right",
                     render: function (data, type, row) {
                         var a = moment(row.PortfolioContactAddress.ExpiryDate);
                         // var b = moment(row.PortfolioContactAddress.StartDate);
                         var b = moment(new Date());
                         var d = a.diff(b, 'days');
                         debugger;
                         if(d > 0)
                             return (d);
                         else
                             return ("<span style='color:red;'> Expired</span>");
                     }
                 },
                  {
                      orderable: false,
                      data: null,
                      className: "center",
                      defaultContent: '<a href="" class="show_list">List Appliances</a>'
                  },
                  {
                      orderable: false,
                      data: null,
                      className: "center",
                      defaultContent: '<a href="" class="editor_remove"><i class="fa fa-trash"></i></a>'
                  }
             ],
             order: [1, 'asc'],
             select: {
                 style: 'single'
                 //selector: 'td:last-child'
             },
             buttons: [
                 { extend: "create", editor: editor, text: "Add New Address", visibile:"false" }
                 //{ extend: "edit", editor: editor },
                 //{ extend: "remove", editor: editor }
             ]
         });

        // debugger;
         // table.button('0-4').remove();
         $('#example').on('click', 'a.show_edit', function (e) {
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
         $('#example').on('click', 'a.show_list', function (e) {
             e.preventDefault();
             var tr = $(this).closest('tr');
             var row = table.row(tr);

             var data1 = row.data();
             var d = data1['PortfolioContactAddress']["ID"];
             //debugger;
             $('#haid').val(data1['PortfolioContactAddress']["ID"]);
             $('.pnl').show();
             assets_table_load();
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

     });


     function GetTrackUrl(l, m, mi, p, imurl, t) {

         if (l != 0)
             return '<a href="' + 'ProjectLabourTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '"  border="0"></a>';
         else if (m != 0)
             return '<a href="' + 'ProjectMaterialTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';
         else if (mi != 0)
             return '<a href="' + 'ProjectMiscTracker.aspx?project=' + p + '" target="_self" title="' + t + '"><img src="' + imurl + '" border="0"></a>';

     }
     //hide button
     setDeleteButton();
    </script>
             <div class="row" style="display:none;visibility:hidden;">
                 
            <table id="example" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Address</th>
                <th>City</th>
                <th>State</th>
                <th>Postcode</th>
                <th>Policy Type</th>
                <th>Policy Number</th>
                <th>Start Date</th>
                <th>Expiry Date</th>
                <th>Days Remaining </th>
                <th>List Appliances</th>
                <th>&nbsp;</th>
               
            </tr>
        </thead>
       
    </table>
                     
            </div>
                            <script type="text/javascript" language="javascript"  class="init">
                                function setDeleteButton() {
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
                                              {
                                                  label: ":",
                                                  name: "Assets.ContactAddressID",
                                                  type: "hidden",
                                                  def: gethaid()
                                              },

                                            {
                                                label: "Type:",
                                                name: "Assets.Type",
                                                type: "select",
                                                placeholder: "Please select..."
                                            },
                                           {
                                               label: "Make:",
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
                                              label: "Notes:",
                                              name: "Assets.FromNotes"
                                          },
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


                                            { data: "Category.Name", editField: "Assets.Type" },
                                            { data: "SubCategory.Name", editField: "Assets.Make" },
                                            { data: "ProductModel.ModelName", editField: "Assets.Model" },
                                            { data: "Assets.SerialNo" },
                                            {
                                                data: "Assets.PurchasedDate",
                                                render: function (data, type, row) {
                                                    return (moment(row.Assets.PurchasedDate).format("MM/DD/YYYY"));
                                    }},
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
                                            { extend: "create", editor: editor1, text: "Add New Appliance" }

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
                                        debugger;
                                        $.ajax({
                                            url: '/api/GetMakeData',
                                            data: {
                                                typeid: val
                                            },
                                            type: 'post',
                                            dataType: 'json',
                                            success: function (json) {
                                                debugger;
                                                //callback(json.options);
                                                editor1.field('Assets.Make').update(json.options.Assets_Make);
                                            }
                                        });
                                    });
                                    editor1.dependent('Assets.Make', function (val, data, callback) {
                                        debugger;
                                        $.ajax({
                                            url: '/api/getmodeldata',
                                            data: {
                                                makeid: val
                                            },
                                            type: 'post',
                                            dataType: 'json',
                                            success: function (json) {
                                                debugger;
                                                //callback(json.options);
                                                editor1.field('Assets.Model').update(json.options.Assets_Model);
                                            }
                                        });
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
                            </script>
                            <div style="display:none;visibility:hidden;">
                            <div class="row pnl">
                                  <table id="plist" class="col-md-12 display nowrap"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Type</th>
               <th>Make</th>
                <th>Model</th>
                <th>Serial Number</th>
                <th>Purchase Date</th>
                <th>Notes</th>
                <%--<th>&nbsp;</th>--%>
               
            </tr>
        </thead>
                                      </table>
                                  </div>
                            </div>  
						</div>
			
					</div>
				</div>
        
    
        </asp:Panel>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
     
   
     <script type="text/javascript">
         $(document).ready(function () {
             $('#MainContent_lbtnUpload').click(
                 function () {
                     $('#contactupload').toggle();
                     $('#MainContent_lbtnUpload').toggle();
                 });
         });
      </script>	



    
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
</asp:Content>
