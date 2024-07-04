<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="ContactLeads.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.ContactLeads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Leads
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
  
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="server">
    Leads
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="server">
      <asp:Button ID="btnAddform" runat="server" SkinID="btnDefault" Text="Add Lead" ClientIDMode="Static" style="float:right;" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <style>
       
.modal {
    background: rgba(0,0,0,0.5);
}
    </style>
    <script>

        $(document).ready(function () {
            // invoked when sending ajax request
            $(document).ajaxSend(function () {
                $("#loading").show();
            });

            // invoked when sending ajax completed
            $(document).ajaxComplete(function () {
                $("#loading").hide();
            });

        });

    </script>
    
     <div class="form-group row">
      <div class="col-md-12">
          <label id="lblmsg" style="width:100%"></label>
           <div id="loading">
        <asp:Label SkinID="Loading" ID="lblloading" runat="server" ClientIDMode="Static"></asp:Label>
             </div>
          </div>
         </div>
    

     <div class="modal fade" id="modal-7" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content">
						
                <div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true" id="btnCloseSector11">&times;</button>
					<h4 class="modal-title"><span id="modeltitle1">Add Lead</span> </h4>
				</div>

				<div class="modal-body">
                       <div class="row">
                        <asp:ValidationSummary ID="cvalsum" runat="server" ValidationGroup="cg" />
                    </div>
				  <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Date</label>
          <div class="col-sm-10 form-inline">
            
<asp:TextBox ID="txtDate" runat="server" SkinID="txtCalender" ClientIDMode="Static" ></asp:TextBox>
              
              <asp:Label ID="imgCalender" runat="server" SkinID="Calender" ClientIDMode="Static" />
              <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                                PopupButtonID="imgCalender" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ValidationGroup="cg" ErrorMessage="Please enter date" Display="None"></asp:RequiredFieldValidator>
              </div>
          </div>
              </div>
                      <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Name</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" SkinID="txt_70"></asp:TextBox>
             <asp:HiddenField ID="hfdid" runat="server" ClientIDMode="Static" />
              <label style="color:red" id="lblError2"></label>
               <asp:RequiredFieldValidator ID="rvcname" runat="server" ControlToValidate="txtName" ValidationGroup="cg" ErrorMessage="Please enter name" Display="None"></asp:RequiredFieldValidator>

              </div>
          </div>
              </div>
					
                    <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Email</label>
          <div class="col-sm-10">            
			<asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" SkinID="txt_70"></asp:TextBox>             
              <label style="color:red" id="lblError3"></label>

              </div>
          </div>
				</div>

        <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Cell</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtCell" runat="server" ClientIDMode="Static" SkinID="txt_70"></asp:TextBox>
             
              <label style="color:red" id="lblError4"></label>

              </div>
          </div>
				</div>

                     <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Address</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtAdress" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
              <label style="color:red" id="lblError5"></label>

              </div>
          </div>
				</div>

                      <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Lead Description</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtLead" runat="server" ClientIDMode="Static" SkinID="txt_90"></asp:TextBox>
             
              <label style="color:red" id="lblError6"></label>

              </div>
          </div>
				</div>


                      <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label"> Price Quoted</label>
          <div class="col-sm-10">
            
			<asp:TextBox ID="txtPrice" runat="server" ClientIDMode="Static" SkinID="Price_150px" MaxLength="10"></asp:TextBox>
             
              <label style="color:red" id="lblError7"></label>

              </div>
          </div>
				</div>

                     <div class="form-group row">
      <div class="col-md-12">
          <label class="col-sm-2 control-label">Reminder Date</label>
          <div class="col-sm-10 form-inline">
            
			<asp:TextBox ID="txtReminder" runat="server" ClientIDMode="Static" SkinID="txtCalender"></asp:TextBox>
             
            <asp:Label ID="imgCalender1" runat="server" SkinID="Calender" ClientIDMode="Static" />
              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                PopupButtonID="imgCalender1" TargetControlID="txtReminder"></ajaxToolkit:CalendarExtender>

              </div>
          </div>
				</div>

				
				<div class="modal-footer">
					<button type="button" class="btn btn-white" data-dismiss="modal" id="btnCloseSector1" style="display:none;">Close</button>
					<asp:Button SkinID="btnSubmit" CssClass="btn btn-info" runat="server" id="btnSaveTask" ValidationGroup="cg"></asp:Button>
				</div>
			</div>
		</div>
	</div>
  </div>
    
    
    <script type="text/javascript">
        $(function () {
            bindGrid();
           
        });
    </script>
    <script type="text/javascript">
        function displayMsg(element, msg, msgtype) {
            if (msgtype == 'error') {
                $('[id*=' + element + ']').html('<p class="bg-danger">' + msg + '</p>');
            }
            else if (msgtype == 'success') {
                $('[id*=' + element + ']').html('<p class="bg-success">' + msg + '</p>');
            }
            else if (msgtype == 'clear') {
                $('[id*=' + element + ']').html('');
            }
            else {
                $('[id*=' + element + ']').html('');
            }

        }
        function getfID() {
            return $("[id*=hfdid]").val();
        }
        function setfID(value) {
            $("[id*=hfdid]").val(value);
        }

        
        function showAjaxModalform() {
            $('#modal-7').modal('show', { backdrop: 'fade' });
        }
        function hideAjaxModalform() {
            $('#modal-7').modal('hide');
        }
        function clearFormData() {
            setfID('0');

             $("[id*=txtDate]").val('');
           $("[id*=txtName]").val('');
            $("[id*=txtEmail]").val('');
            $("[id*=txtCell]").val('');
            $("[id*=txtAdress]").val('');
            $("[id*=txtLead]").val('');
            $("[id*=txtPrice]").val('');
            $("[id*=txtReminder]").val('');      
        }
        function bindGrid() {
            var id = $("[id*=hfdid]").val();
            GetFormData(id);
        }


        $("#btnAddform").click(function () {
            $("[id*=modeltitle1]").html("Add Lead");
            setfID('0');
            clearFormData();
            showAjaxModalform();
            return false;
        });
        //button to save
        $("[id*=btnSaveTask]").click(function () {

            if (!Page_ClientValidate("cg"))
                return false;
            else {

                var fid = getfID();
                if (fid != '0') {
                    UpdateServiceContactData();
                    hideAjaxModalform();
                    displayMsg('lblmsg', 'Updated successfully', 'success');
                }
                else {
                    debugger;
                    AddFormData();
                    debugger;
                    clearFormData();
                    hideAjaxModalform();
                    displayMsg('lblmsg', 'Added successfully', 'success');
                }
            }

            return false;
        });

        //ajax fuction for add a meterials

        function AddFormData() {
            var LeadID = getfID();
            var CreatedDate = $("[id*=txtDate]").val();
            var CustomerName = $("[id*=txtName]").val();
            var Email = $("[id*=txtEmail]").val();
            var Cell = $("[id*=txtCell]").val();  
            var Address = $("[id*=txtAdress]").val();          
            var LeadDescription = $("[id*=txtLead]").val();  
            var PriceQuoted = $("[id*=txtPrice]").val();  
            var ReminderDate = $("[id*=txtReminder]").val();  

            var dataObject = JSON.stringify({
                'LeadID': LeadID,
                'CreatedDate': CreatedDate,
                'CustomerName': CustomerName,
                'Email': Email,
                'Cell': Cell,
                'Address': Address,
                'LeadDescription': LeadDescription,
                'PriceQuoted': PriceQuoted,
                'ReminderDate': ReminderDate,

            });
            //alert(dataObject)
            $.ajax({
                type: "POST",
                url: "../../WF/CustomerAdmin/webservices/ContactLeadsServices.asmx/AddContactLeads",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {

                   
                    debugger;
                    
                    bindGrid();
                    clearFormData();

                }
            });
        }

        //Get form data
        function editFormdata(LeadID, CustomerName, Email, LeadDescription, Address) {
            $("[id*=modeltitle1]").html("Edit Contact");
            clearFormData();
            setfID(LeadID);
            GetFormDatabyID();
            showAjaxModalform();
            return false;
        }

        function GetFormDatabyID() {
            var id = $("[id*=hfdid]").val();

            var dataObject = JSON.stringify({
                'id': id,
            });
            //alert(id)
            try {
                $.ajax({
                    url: "../../WF/CustomerAdmin/webservices/ContactLeadsServices.asmx/BindFromDataByID",
                    type: "POST",
                    data: dataObject,
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        debugger;
                        var NewData = jQuery.parseJSON(data.d);

                        for (var i = 0; i < NewData.length; i++) {
                            var LeadID = NewData[i].LeadID;
                            var CreatedDate = NewData[i].CreatedDate;
                            var CustomerName = NewData[i].CustomerName;
                            var Email = NewData[i].Email;
                            var Cell = NewData[i].Cell;
                            var Address = NewData[i].Address;
                            var LeadDescription = NewData[i].LeadDescription;
                            var PriceQuoted = NewData[i].PriceQuoted;
                            var ReminderDate = NewData[i].ReminderDate;

                            $("[id*=txtDate]").val(CreatedDate);
                            $("[id*=txtName]").val(CustomerName);
                            $("[id*=txtEmail]").val(Email);
                            $("[id*=txtCell]").val(Cell);
                            $("[id*=txtAdress]").val(Address);
                            $("[id*=txtLead]").val(LeadDescription);
                            $("[id*=txtPrice]").val(PriceQuoted);
                            $("[id*=txtReminder]").val(ReminderDate);
                        }


                    }
                });
            }
            catch (e) {
                var err = e;
            }
        }
        //delete form date

        function deleteFormdata(id) {
            var r = ConfirmDelete();
            if (r == true) {
                setfID(id);
                deleteFormDatabyMethod();
            }
        }
        function ConfirmDelete() {
            return confirm("Are you sure you want to delete?");
        }

        function deleteFormDatabyMethod() {
            var id = $("[id*=hfdid]").val();

            var dataObject = JSON.stringify({
                'id': id,
            });
            $.ajax({
                type: "POST",
                url: "../../WF/CustomerAdmin/webservices/ContactLeadsServices.asmx/FromDataDelete",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    bindGrid();
                    displayMsg('lblmsg', 'Deleted successfully', 'success');
                }
            });
        }
        //edit form data
        function UpdateServiceContactData() {           
            var LeadID = getfID();
            var CreatedDate = $("[id*=txtDate]").val();
            var CustomerName = $("[id*=txtName]").val();
            var Email = $("[id*=txtEmail]").val();
            var Cell = $("[id*=txtCell]").val();
            var Address = $("[id*=txtAdress]").val();
            var LeadDescription = $("[id*=txtLead]").val();
            var PriceQuoted = $("[id*=txtPrice]").val();
            var ReminderDate = $("[id*=txtReminder]").val();

            var dataObject = JSON.stringify({
                'LeadID': LeadID,
                'CreatedDate': CreatedDate,
                'CustomerName': CustomerName,
                'Email': Email,
                'Cell': Cell,
                'Address': Address,
                'LeadDescription': LeadDescription,
                'PriceQuoted': PriceQuoted,
                'ReminderDate': ReminderDate,

            });
            $.ajax({
                type: "POST",
                url: "../../WF/CustomerAdmin/webservices/ContactLeadsServices.asmx/ContactUpdate",
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (r) {
                    var t = r.d;
                    debugger;
                    bindGrid();
                }
            });
        }


    </script>
     <div class="row" >
    <div class="col-md-12">
     <table id="students1" class="table table-striped table-bordered"></table>
        </div>
         </div>
    <script type="text/javascript">
        function GetFormData(id) {
            try {

                $.ajax({
                    url: "../../WF/CustomerAdmin/webservices/ContactLeadsServices.asmx/BindFromData",
                    type: "POST",
                    data: "{'id': '" + id + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        debugger;
                        var NewData = jQuery.parseJSON(data.d);
                        var x = "<thead><tr>"
                            + "<th style='width:3%;color:white;'></th>"
                            + "<th  style='width:10%;color:white;'>Name</th>"
                            + "<th  style='width:10%;color:white;'>Email</th>"
                            + "<th  style='width:5%;color:white;'>Cell</th>"
                            + "<th  style='width:15%;color:white;'>Address</th>"
                            + "<th  style='width:15%;color:white;'>Description</th>"
                            + "<th  style='width:5%;color:white;'>Price Quoted</th>"
                            + "<th  style='width:5%;color:white;'>Reminder Date</th>"
                            + "<th  style='width:5%;color:white;'>Created Date</th>"
                            + "<th style='width:3%;color:white;'></th> "
                            + "</tr></thead>";

                        x = x + "<tbody>"

                        for (var i = 0; i < NewData.length; i++) {
                            //var CCID = NewData[i].CCID
                            var LeadID = NewData[i].LeadID;
                            var CustomerName = NewData[i].CustomerName;
                            var Email = NewData[i].Email;
                            var CreatedDate = NewData[i].CreatedDate;
                            var Address = NewData[i].Address;
                            var Cell = NewData[i].Cell;
                            var LeadDescription = NewData[i].LeadDescription;
                            var PriceQuoted = NewData[i].PriceQuoted;
                            var ReminderDate = NewData[i].ReminderDate;



                            x = x + "<tr><td>" + ButtonHtml(LeadID, CustomerName, Email, CreatedDate, Address)
                                + "</td><td>" + CustomerName
                                + "</td><td>" + Email
                                + "</td><td>" + Cell
                                + "</td><td>" + Address
                                + "</td><td>" + LeadDescription
                                + "</td><td style='text-align:right;'>" + PriceQuoted
                                + "</td><td style='text-align:right;'>" + ReminderDate
                                + "</td><td style='text-align:right;'>" + CreatedDate
                                + "</td><td>" + ButtondeleteHtml(LeadID)
                                + "</td></tr>";
                        }

                        x = x + "</tbody>";
                        $("#students1").empty();
                        $("#students1").append(x);
                        BindTable();
                        $("#students1").removeClass("no-footer");
                        //setStatusBackColor();
                    }
                });
            }
            catch (e) {
                var err = e;
            }
        }


        function BindTable() {
            var table = $('#students1').DataTable({
                'Ordering': true,
                "order": [[0, "asc"]],
                'paging': true,
                'bFilter': false,
                'lengthChange': false,
                'destroy': true,
                
                "oLanguage": {
                    "sEmptyTable": "You currently do not have any leads"
                },
                //'rowReorder': true,
                "columnDefs": [{
                    "targets": 0, "orderable": false
                }

                ]
            });


        }

        function DisplayValues(ListValue, TypeOfField) {
            var HtmlText = "";
            if (TypeOfField != 'Textbox') {
                HtmlText = "<ul>"
                var array = ListValue.split(',');

                $.each(array, function (index, value) {
                    HtmlText = HtmlText + "<li>" + value + "</li>";
                });
                HtmlText = HtmlText + "</ul>";
            }
            else {
                HtmlText = ListValue;
            }
            return HtmlText;
        }

        function ButtonHtml(LeadID, CustomerName, Email, CreatedDate, Address) {
            var HtmlText = " <a target='_blank' id=Link" + LeadID + " onclick=editFormdata('" + LeadID + "') style=' font-weight: bold'><span class='fa-edit' style='font-size:1.2em'></span></a>";
            //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
            return HtmlText;
        }

        function ButtondeleteHtml(id) {
            var HtmlText = " <a target='_blank' id=Linkdel" + id + " onclick=deleteFormdata('" + id + "') style=' font-weight: bold'><span class='fa-trash' style='font-size:1.2em'></span></a>";
            //  var HtmlText = " <a id=" + Id + " onclick='BindpopUp(this)' style='font-weight: bold;cursor:pointer;'>" + "<span class='fa-edit' style='font-size:1.2em'>" + "</span></a>";
            return HtmlText;
        }
    </script>
            <script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js">
</script>
    
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" />


    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/rowreorder/1.2.7/css/rowReorder.dataTables.min.css" />
    <link href="../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />



<script type="text/javascript" src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js">

</script>
    <script type="text/javascript" src="https://cdn.datatables.net/rowreorder/1.2.7/js/dataTables.rowReorder.min.js"></script>
  

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
