<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMain.Master" AutoEventWireup="true" Inherits="FLSCustomer1" EnableEventValidation="false" Codebehind="FLSCustomer.aspx.cs" %>
<%@ Register src="controls/FlsHistory.ascx" tagname="FlsHistory" tagprefix="uc1" %>
<%@ Register src="controls/FLSCustomerTab.ascx" tagname="ServiceTab" tagprefix="uc2" %>
<%@ Register src="controls/NotesCtrl.ascx" tagname="Notes" tagprefix="uc3" %>

 
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <%--<uc2:ServiceTab ID="Service1" runat="server" />
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
  
</asp:Content>--%>
<%--<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <%--<asp:HyperLink runat="Server" Text="" NavigateUrl="~/WF/DC/DCCustomerJlist.aspx?type=FLS&customer=0"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
    <asp:HyperLink runat="Server" Text="" NavigateUrl="~/WF/Portal/Home.aspx"><i class="fa fa-arrow-left"></i> Return to Jobs</asp:HyperLink>
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server" >


    

    <div class="row">
       <div class="col-md-12">
           <div class="card shadow-sm">
                   <div class="card-header">
                      <label id="lblTitle" runat="server">
                        </label>
                       <div class="card-toolbar">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
                   </div>
                   <div class="panel-body">

   <style>
        /*td{
        padding-bottom:15px;
        padding-left:15px;
    }*/
   </style>
    <style>
    .ralert-danger {
    background-color: #cc3f44;
    border-color: #cc3f44;
    color: #ffffff;
}

.ralert {
    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
    border-radius: 0px;
}
</style>
<%--<script src="<%# ResolveClientUrl("~/Scripts/jquery.MultiFile.js") %>" type="text/javascript"></script>--%>
    
   <script type="text/javascript" >
       function getQuerystring(key, default_) {
           if (default_ == null) default_ = "";
           key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
           var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
           var qs = regex.exec(window.location.href.toLowerCase());
           if (qs == null)
               return default_;
           else
               return qs[1];
       }
       function onPopulated() {
           $get('ddlTypeofRequest').disabled = true;
           $get('ddlCompany').disabled = true;
           $get('ddlStatus').disabled = true;
           $get('ddlName').disabled = true;
       }
       function onCallidPopulated() {
           //$get('ddlSite').disabled = true;
           //$get('ddlSubject').disabled = true;
           $get('ddlRequestType').disabled = true;
           //$get('txtTimeWorked').disabled = true;
          // $get('txtCustomerCostCode').disabled = true;
           //$get('txtPOnumber').disabled = true;
           //$get('txtCustomerRef').disabled = true;
          // $get('txtNotes').disabled = true;
           
       }
       function pageLoad(sender, args) {
           $find("ccdT").add_populated(onPopulated);
           $find("ccdC").add_populated(onPopulated); 
           $find("ccdS").add_populated(onPopulated);
           $find("ccdrn").add_populated(onPopulated);
           BindValues();
          
           var callid = getQuerystring('callid');
           if (callid != "") {
              
//               $("#txtSeheduledDateTime").attr("disabled", "disabled");
//               $("#txtScheduledTime").attr("disabled", "disabled");
               //$("#txtReqTelNo").attr("disabled", "disabled");
               //$("#txtReqEmailAddress").attr("disabled", "disabled");
               $("#txtDetails").attr("disabled", "disabled");
               //$find("ccdsi").add_populated(onCallidPopulated);
              // $find("ccdSub").add_populated(onCallidPopulated);
              // debugger;
              // $get('ddlCategory').disabled = true;
           }
       }
       $().ready(function () {
           $("#ddlName").change(function () {
               //BindValues();
           });
       });
       $().ready(function () {
           //BindValues();
       });
       function BindValues() {
           var ID = $('#ddlName').val();
           if (ID != "0") {
               //$("#txtReqTelNo").html("");
               //$.ajax({
               //    type: "POST",
               //    url: "/WF/DC/webservices/DCServices.asmx/GetReqTelNo",
               //    data: "{ID:'" + ID + "'}",
               //    contentType: "application/json; charset=utf-8",
               //    dataType: "json",
               //    success: function (msg) {
               //        document.getElementById('txtReqTelNo').value = msg.d;
               //    }
               //});

               //$("#txtReqEmailAddress").html("");
               //$.ajax({
               //    type: "POST",
               //    url: "/WF/DC/webservices/DCServices.asmx/GetReqEmail",
               //    data: "{ID:'" + ID + "'}",
               //    contentType: "application/json; charset=utf-8",
               //    dataType: "json",
               //    success: function (msg) {
               //        document.getElementById('txtReqEmailAddress').value = msg.d;
               //    }
               //});
           }
           //document.getElementById('txtReqTelNo').value = "";
           //document.getElementById('txtReqEmailAddress').value = "";
       }
       $().ready(function () {
           //$("#ddlCompany").change(function () {
           //    $('#txtReqTelNo').val('');
           //    $('#txtReqEmailAddress').val('');
           //});
       });
</script>

<script language="javascript" type="text/javascript">
    function compareDate(fromdate, todate) {
        var sTime = moment(fromdate, "MM/dd/yyyy");
        var tTime = moment(todate, "MM/dd/yyyy");
        debugger;
        return tTime.isBefore(sTime)
    }
    function comparetime(fromtime, totime) {


        var sTime = moment(fromtime, "HH:mm");
        var tTime = moment(totime, "HH:mm");
        debugger;

        if (fromtime == "")
            return false;
        else if (tTime == "")
            return false
        else
            return tTime.isBefore(sTime)
    }

    function Save_click()
    {
        if (Page_ClientValidate()) {

            //prefered date1 compare time
            var p1 = comparetime($("[id$='txtScheduledTime']").val(), $("[id$='txtScheduledToTime']").val());
            debugger;
            if (p1 == false) {
                var p2 = comparetime($("[id$='txtPreferreddatetime2']").val(), $("[id$='txtPreferreddatetimeto2']").val());
                if (p2 == false) {
                    //compare preferend date & preferend date1
                    var p3 = comparetime($("[id$='txtPreferreddatetime3']").val(), $("[id$='txtPreferreddatetimeto3']").val());
                    if (p3 == false) {
                        $('#div_buttons').fadeOut('fast');
                        $('#lbl_loading').fadeIn('slow');
                    }
                    else {
                        $("[id$='lblMsg']").show();
                        $("[id$='lblMsg']").html('Please enter valid Preferred 3 to time');
                        return false;
                    }
                }
                else {
                    $("[id$='lblMsg']").show();
                    $("[id$='lblMsg']").html('Please enter valid Preferred 2 to time');
                    return false;
                }
            }
            else {
                $("[id$='lblMsg']").show();
                $("[id$='lblMsg']").html('Please enter valid Preferred to time');
                return false;
            }
        }
        else { return false; }

    }
    //Fade out buttons when clicked but only if page validate
    $().ready(function () {
        $('#lbl_loading').hide();
        $('#div_buttons').show();
        $('#btnSave').click(function (e) {
           
            if (Page_ClientValidate()) {

                //prefered date1 compare time
                var p1 = comparetime($("[id$='txtScheduledTime']").val(), $("[id$='txtScheduledToTime']").val());
                debugger;
                if (p1 == false) {
                    var p2 = comparetime($("[id$='txtPreferreddatetime2']").val(), $("[id$='txtPreferreddatetimeto2']").val());
                    if (p2 == false) {
                        //compare preferend date & preferend date1
                        var p3 = comparetime($("[id$='txtPreferreddatetime3']").val(), $("[id$='txtPreferreddatetimeto3']").val());
                        if (p3 == false) {
                            $('#div_buttons').fadeOut('fast');
                            $('#lbl_loading').fadeIn('slow');
                        }
                        else {
                            $("[id$='lblMsg']").show();
                            $("[id$='lblMsg']").html('Please enter valid Preferred 3 to time');
                            return false;
                        }
                    }
                    else {
                        $("[id$='lblMsg']").show();
                        $("[id$='lblMsg']").html('Please enter valid Preferred 2 to time');
                        return false;
                    }
                }
                else {
                    $("[id$='lblMsg']").show();
                    $("[id$='lblMsg']").html('Please enter valid Preferred to time');
                    return false;
                }
            }
            else { return false; }
        });
        $('#btnSave2').click(function (e) {
            if (Page_ClientValidate()) {

                //prefered date1 compare time
                var p1 = comparetime($("[id$='txtScheduledTime']").val(), $("[id$='txtScheduledToTime']").val());
                debugger;
                if (p1 == false) {
                    var p2 = comparetime($("[id$='txtPreferreddatetime2']").val(), $("[id$='txtPreferreddatetimeto2']").val());
                    if (p2 == false) {
                        //compare preferend date & preferend date1
                        var p3 = comparetime($("[id$='txtPreferreddatetime3']").val(), $("[id$='txtPreferreddatetimeto3']").val());
                        if (p3 == false) {
                            $('#div_buttons').fadeOut('fast');
                            $('#lbl_loading').fadeIn('slow');
                        }
                        else {
                            $("[id$='lblMsg']").show();
                            $("[id$='lblMsg']").html('Please enter valid Preferred 3 to time');
                            return false;
                        }
                    }
                    else {
                        $("[id$='lblMsg']").show();
                        $("[id$='lblMsg']").html('Please enter valid Preferred 2 to time');
                        return false;
                    }
                }
                else {
                    $("[id$='lblMsg']").show();
                    $("[id$='lblMsg']").html('Please enter valid Preferred to time');
                    return false;
                }
            }
            else { return false; }
        });
    });
</script>
    
          
        <asp:Panel ID="pnl" runat="server" Width="100%">

            
    <div class="form-group row">
         <asp:Label ID="lblAlert" runat="server" CssClass="ralert ralert-danger" EnableViewState="true" Visible="false" style="display:inline;"></asp:Label>
         <asp:Label ID="lblMsg" runat="server" SkinID="RedBackcolor" ></asp:Label>
         <asp:Label ID="lblSuccessMsg" runat="server" SkinID="GreenBackcolor" Visible="false"></asp:Label>
         <asp:Panel style="display:none;" ID="lblExpiredMsg" runat="server" CssClass="ralert ralert-danger">
             Sorry but your policy has expired. Please call the Customer Services Department to discuss your renewal
    </asp:Panel>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fls" DisplayMode="List"/>
        </div>
              <div class="form-group row" style="float:right;margin-bottom:0px">
        <div  class="col-md-12" style="float:right;">
      <%--  <span style="margin-right:80px">
             <asp:Label ID="lbl_loading" runat="server" Text="Loading..." ClientIDMode="Static" Font-Bold="true"></asp:Label>

        </span>--%>
  <%--<div id="div_buttons">--%>
            <asp:Button ID="btnSave2" runat="server" AlternateText="Save" SkinID="btnSave"
                OnClick="btnSave_Click" ValidationGroup="fls" ClientIDMode="Static" />
            <asp:Button ID="LinkButton2" runat="server" AlternateText="Cancel" SkinID="btnCancel"
                OnClick="btnCancel_Click" />
                <%--</div>--%>
        </div>
    </div>
           
            <asp:panel runat="server" id="div_search">
<div class="form-group row">
      <div class="col-md-5 form-inline">
           <asp:TextBox ID="txtSearch" runat="server" SkinID="txt_90" MaxLength="500" Text="v" style="display:none;visibility:hidden;"></asp:TextBox>
                      <asp:LinkButton ID="btnSearch" runat="server" SkinID="BtnLinkSearch" ClientIDMode="Static" style="display:none;visibility:hidden;"/>
          <asp:HiddenField ID="haddressid" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hcid" runat="server" ClientIDMode="Static" />
           <asp:HiddenField ID="hapid" runat="server" ClientIDMode="Static" />
           <asp:HiddenField ID="hpid" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hstatusid" runat="server" ClientIDMode="Static" />
	</div>
    </div>

<div class="form-group row">
    <table id="slist" class="col-md-12 display nowrap" style="padding-left:0px;padding-right:0px"  cellspacing="0" width="100%">
        <thead>
            <tr>
                <th></th>
                <th>Name</th>
               <th>Email</th>
                <th>Address</th>
                <th>City</th>
                <th>State</th>
                <th>Zip Code / Post Code</th>
                <%--<th>Policy Type</th>
                <th>Policy Number</th>
                <th>Start Date</th>
                <th>Expiry Date</th>--%>
            </tr>
        </thead>
                                      </table>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(loadByAddressID);
        function loadByAddressID() {

            var id = $("[id$='haddressid']").val();

            if (id != "") {
                
                try {
                    $.ajax({
                        type: "POST",
                        url: "../../WF/DC/webservices/DCServices.asmx/GetPortfolioContactDetailsByAddressID",
                        data: "{id:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            
                           
                           
                            $("[id$='haddressid']").val(msg.d.AddressID);
                            $("[id$='hcid']").val(msg.d.ID);
                            
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
    <script type="text/javascript">
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
        //$('#slist').hide();
        var editorSearch;
        var selectedSearch = [];
        var tableSearch;
        //Search_load();

        function Search_load() {
            $('#slist').show();
            editorSearch = new $.fn.dataTable.Editor({
                ajax: "/api/ContactAdressDetailsByContactID",
                table: "#slist",
                fields: [
                     {
                         label: "",
                         name: "ID",
                         type: "hidden"
                     }
                ]
            }
                );
            tableSearch = $('#slist').DataTable({
               
                //"iDisplayLength": "400",
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "scrollX": true,
                fixedHeader: true,
                fixedColumns: {
                    leftColumns: 2
                },
                responsive: true,
                destroy: true,
                // paging: true,
                searching: false,
                dom: "Bfrtip",
                ajax: {
                    url: "/api/ContactAdressDetailsByContactID",
                    type: 'POST',
                    data: function (d) {
                        d.search = $("[id$='txtSearch']").val(),
                        d.ContactID = $("[id$='hcid']").val()
                    }
                },
               
                columns: [
                     {
                         data: null,
                         defaultContent: '',
                         className: 'select-checkbox',
                         orderable: false
                        
                     },
                    { data: "Name" },
                    { data: "Email" },
                    { data: "Address" },
                    { data: "City" },
                    { data: "State" },
                    { data: "PostCode" },
                    { data: "PolicyTitle", visible: false, },
                    { data: "PolicyNumber", visible: false, },
                    {
                        data: "StartDate",
                        visible: false,
                        render: function (data, type, row) {
                            return (moment(row.StartDate).format("MM/DD/YYYY"));
                        }
                    },
                     {
                         data: "ExpiryDate",
                         visible: false,
                         render: function (data, type, row) {
                             return (moment(row.ExpiryDate).format("MM/DD/YYYY"));
                         }
                     },
                ],
                order: [1, 'asc'],
                select: true,
                visible:false,
                buttons: [
                    { extend: "create", editor: editorSearch, text: "Add New Equipment" }
                ]
            });

            $('#slist').on('click', 'tbody td:first-child', function (e) {
                //editor.inline(this);
                if ($(this).parents('tr').hasClass('selected')) {
                    $(this).parents('tr').removeClass('selected');
                }
                else {
                    $(this).parents('tr').addClass('selected');
                }
                var id = tableSearch.row(this).id();
                $("[id$='haddressid']").val(id.replace('row_', ''));
                //alert($("[id$='haddressid']").val());
                loadByAddressID();
                
               
            });

            
           
           
            $(".buttons-create").hide();


        }

       
        $(document).ready(function () {

            $("[id$='txtSearch']").keyup(function () {
                Search_load();
            });

        });

        $(window).load(function () {

            var Q_callid = GetParameterValues('callid');
            debugger;
            if (Q_callid == undefined) {
                Search_load();
            }
            else if (Q_callid == "") {
                Search_load();
            }
            else {
                $("[id$='slist']").hide();
            }
            //Search_load();
        })
    </script>

    
    </div>

</asp:panel>

           

    <div class="form-group row" style="display:none;visibility:hidden;">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlCompany" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdCompany" runat="server" TargetControlID="ddlCompany" BehaviorID="ccdC"
                    Category="company" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetCompany" LoadingText="[Loading company...]" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCompany"
                    Display="Dynamic" ErrorMessage="Please select company" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite" BehaviorID="ccdsi"
                                Category="Site" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetOurSite" LoadingText="[Loading site...]" />
               <%-- <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite" BehaviorID="ccdsi"
                    Category="type"  ServicePath="~/webservices/DCServices.asmx"
                    ServiceMethod="GetSiteByCusomerId" ParentControlID="ddlCompany" LoadingText="[Loading site...]" />--%>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSite"
                    Display="Dynamic" ErrorMessage="Please select site" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>--%>
                <asp:HiddenField ID="htid" runat="server" Value="0" ClientIDMode="Static"/>
					</div>
				</div>
</div>


            <div class="form-group row" >
                                  <div class="col-md-6">
                                      <div class="form-group row" style="display:none;visibility:hidden;">
                                        <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Name%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlName" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlName"
                    Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetNameByCompanyId" ParentControlID="ddlCompany" LoadingText="[Loading name...]" BehaviorID="ccdrn" ClientIDMode="Static" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlName"
                    Display="Dynamic" ErrorMessage="Please select name" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
					</div>
				</div>

</div>
                                       <div class="form-group row" style="display:none;visibility:hidden;">
                                        <div class="col-md-12" >
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ContactNumber%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtReqTelNo" runat="server" SkinID="txt_80" ClientIDMode="Static" MaxLength="16"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtReqTelNo" ValidChars="0123456789+ "  />
					</div>
				</div>
                                            </div>


<div class="form-group row" style="display:none;visibility:hidden;">
     <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequesterEmailID%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtReqEmailAddress" runat="server" SkinID="txt_80" ClientIDMode="Static"></asp:TextBox>
					</div>
				</div>
    </div>

                                           <div class="form-group row" style="display:none;visibility:hidden;">
                                                <div class="col-md-12">
       
             <div class="col-md-4" >
          <span class="lab" id="li_lblRequestersAddress" runat="server">
            <asp:Label ID="lblRequestersAddress" runat="server" Text="Address"  ReadOnly="true"></asp:Label>
          </span>
                 </div>
        <div class="col-md-8 form-inline">
            <asp:TextBox ID="txtRequestersAddress" runat="server" ClientIDMode="Static"
                 MaxLength="500" SkinID="txt_80"></asp:TextBox>
            </div>
       
            </div>
         
         
          
    </div>

                                           <div class="form-group row" style="display:none;visibility:hidden;">
                                               <div class="col-md-12">
        <div id="li_txtRequestersCity" runat="server">
             <div class="col-md-4">
          <span class="lab" id="li_lblRequestersCity" runat="server">
            <asp:Label ID="lblRequestersCity" runat="server" Text="City"  ReadOnly="true"></asp:Label>
          </span>
                 </div>
        <div class="col-md-8 form-inline">
            <asp:TextBox ID="txtRequestersCity" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_80"></asp:TextBox>
            </div>
        </div>
            </div>

    </div>

                                           <div class="form-group row" style="display:none;visibility:hidden;">
                                               <div class="col-md-12">
        <div id="li_txtRequestersTown" runat="server">
             <div class="col-md-4">
          <span class="lab" id="li_lblRequestersTown" runat="server">
            <asp:Label ID="lblRequestersTown" runat="server" Text="Town"  ReadOnly="true"></asp:Label>
          </span>
                 </div>
        <div class="col-md-8 form-inline">
            <asp:TextBox ID="txtRequestersTown" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_80"></asp:TextBox>
            </div>
        </div>
            </div>

    </div>

                                           <div class="form-group row" style="display:none;visibility:hidden;">
                                               <div class="col-md-12">
        <div id="li_txtRequestersPostcode" runat="server">
             <div class="col-md-4">
          <span class="lab" id="li_lblRequestersPostcode" runat="server">
            <asp:Label ID="lblRequestersPostcode" runat="server" Text="Postcode/ Zipcode"  ReadOnly="true"></asp:Label>
          </span>
                 </div>
        <div class="col-md-8 form-inline">
            <asp:TextBox ID="txtRequestersPostcode" runat="server" ClientIDMode="Static" MaxLength="200" SkinID="txt_80"></asp:TextBox>
            </div>
        </div>
            </div>

    </div>

                                           <div class="form-group row" style="display:none;visibility:hidden;">
                                                <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Subject%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlSubject" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdSubject" runat="server" TargetControlID="ddlSubject" BehaviorID="ccdSub"
                    Category="Subject" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetSubject" LoadingText="[Loading subject...]" ClientIDMode="Static" />
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubject"
                    Display="Dynamic" ErrorMessage="Please select subject" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>--%>
					</div>
				</div>
    </div>
                                       <div class="form-group row" style="display:none;visibility:hidden;">
                                                 <div class="col-md-12">
                                                      <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Category%></label>
                                                      <div class="col-sm-10">
                                                           <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
          
           <%-- <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                Display="None" ErrorMessage="Please select Category" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>--%>
                                                           <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory" BehaviorID="ccdCate"
                Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading category...]" ParentControlID="ddlRequestType" ClientIDMode="Static" />
                                                          </div>
                                                     </div>
                                           </div>
                                            <div class="form-group row">
                                                 <div class="col-md-12">
                                       <label class="col-sm-12 control-label" style="font-weight:bold;font-size:20px"> <%--<%= Resources.DeffinityRes.Details%>--%> Job Description</label>
                                      <div class="col-sm-12"> <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" style="height:120px"  ClientIDMode="Static"></asp:TextBox>
					</div>

                                                </div>

</div>
                                        <div class="form-group row" style="display: none;visibility:hidden;">
                                
 <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.TypeofRequest%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlRequestType" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                   <%-- <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                    <asp:ListItem Text="Faults" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Service Request" Value="2"></asp:ListItem>--%>
                </asp:DropDownList>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlRequestType"
                    Display="Dynamic" ErrorMessage="Please select type of request" InitialValue="0"
                    SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>--%>
                  <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestType"
                BehaviorID="ccdType" Category="type" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestType" LoadingText="[Loading...]" />
					</div>
				</div>
</div>

    <div class="form-group row" style="display:none;visibility:hidden;">
                                
 <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Status%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdStatus" runat="server" TargetControlID="ddlStatus" BehaviorID="ccdS"
                    Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetStatusByTypeId" ParentControlID="ddlTypeofRequest" LoadingText="[Loading status...]" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                    Display="Dynamic" ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="fls">*</asp:RequiredFieldValidator>
                    <asp:HiddenField ID="h_status" runat="server" Value="0" />
					</div>
				</div>
</div>
                                     </div>
                  <div class="col-md-6">

                     

    <div class="form-group row" style="display:none;visibility:hidden;">
                                 
 <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Loggedby%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtLoggedName" runat="server" SkinID="txt_80" ReadOnly="true"></asp:TextBox>
					</div>
				</div>
</div>

    <div class="form-group row" style="display:none;visibility:hidden;">
                                 
 <div class="col-md-12">
                                       <label class="col-sm-4 control-label">Logged Date/Time</label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtCreatedDate" runat="server" SkinID="Date" ReadOnly="true"></asp:TextBox>
                 <asp:TextBox ID="txtCreatedTime" runat="server" SkinID="Time" ClientIDMode="Static"
                    ReadOnly="true"></asp:TextBox>(HH:MM)
					</div>
				</div>
</div>

    <div class="form-group row">
        <span class="col-md-12 col-md-offset-0" style="margin-top: 10px;margin-bottom: 5px;font-weight: bold;float: right;padding-left: 30px;">Please input all times in 24 Hour Format (e.g. 3:30pm = 15:30)</span>
                     <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Scheduled Date/Time <%--for Service 1--%> </label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtSeheduledDateTime" runat="server" SkinID="Date" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" />
            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgSeheduledDate" TargetControlID="txtSeheduledDateTime">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSeheduledDateTime" ClientIDMode="Static"
                Display="None" ErrorMessage="Please enter scheduled date " SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid date"
                ControlToValidate="txtSeheduledDateTime" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                 Display="None" SetFocusOnError="True"></asp:CompareValidator>
           <%-- <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtSeheduledDateTime"
                ValidationGroup="fls" ErrorMessage="Scheduled Date must be a future date" Operator="GreaterThanEqual"
                Type="Date" Text="*" Display="Dynamic" SetFocusOnError="True">
            </asp:CompareValidator>--%>
             <asp:TextBox ID="txtScheduledTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox><%--(HH:MM)--%>
                                           <asp:TextBox ID="txtScheduledToTime" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox>
                                            <asp:LinkButton ID="btnAddPrefdate" runat="server" SkinID="BtnLinkAdd" ClientIDMode="Static" style="display:none;visibility:hidden;" />
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtScheduledTime"
                    Display="None" ErrorMessage="Please enter valid time" ValidationExpression="^(\d{2}):(\d{2})"
                    ValidationGroup="fls" />
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtScheduledToTime"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
					</div>
				</div>            
				</div>


                       <div class="form-group row" style="display:none;visibility:hidden;">
                 <div class="col-md-12">
                     <div id="DivPreferreddate2" runat="server">
         <div id="li_txtPreferreddate2" runat="server">
              <div class="col-md-4">
         <span class="lab" id="li_lblPreferreddate2" runat="server">
            <asp:Label ID="lblPreferreddate2" Text="Preferred Date/Time for Service 2" runat="server"></asp:Label>
        </span>
       </div>
              <div class="col-md-8 form-inline">
            <asp:TextBox ID="txtPreferreddate2" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>

            <asp:Label ID="ImgPreferreddate2" runat="server" SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="cxPreferreddate2" runat="server" CssClass="MyCalendar" 
                                 PopupButtonID="ImgPreferreddate2" TargetControlID="txtPreferreddate2"></ajaxToolkit:CalendarExtender>
               <asp:TextBox ID="txtPreferreddatetime2" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox><%--(HH:MM)--%>
                   <asp:TextBox ID="txtPreferreddatetimeto2" runat="server" SkinID="Time" ClientIDMode="Static" Text="00:00"></asp:TextBox>
             <asp:RegularExpressionValidator ID="revPreferreddate2" runat="server" ControlToValidate="txtPreferreddatetime2"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPreferreddatetimeto2"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Preferred Date/Time 2 should be greater than Preferred Date/Time" ControlToCompare="txtSeheduledDateTime"
                ControlToValidate="txtPreferreddate2" ValidationGroup="fls" Type="Date" Operator="GreaterThanEqual"
                 Display="None"></asp:CompareValidator>
                  </div>
        </div>
            </div>
                     </div>
                 </div>
            <div class="form-group row"  style="display:none;visibility:hidden;">
                 <div class="col-md-12">
                  <div id="DivPreferreddate3" runat="server">
         <div id="li_txtPreferreddate3" runat="server">
              <div class="col-md-4">
         <span class="lab" id="li_lblPreferreddate3" runat="server">
            <asp:Label ID="lblPreferreddate3" Text="Preferred Date/Time for Service 3" runat="server"></asp:Label>
        </span>
       </div>
              <div class="col-md-8 form-inline">
            <asp:TextBox ID="txtPreferreddate3" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>

            <asp:Label ID="ImgPreferreddate3" runat="server" SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="cxPreferreddate3" runat="server" CssClass="MyCalendar" 
                                 PopupButtonID="ImgPreferreddate3" TargetControlID="txtPreferreddate3"></ajaxToolkit:CalendarExtender>
               <asp:TextBox ID="txtPreferreddatetime3" runat="server" SkinID="Time" ClientIDMode="Static"></asp:TextBox><%--(HH:MM)--%>
                   <asp:TextBox ID="txtPreferreddatetimeto3" runat="server" SkinID="Time" ClientIDMode="Static" Text="00:00"></asp:TextBox>
             <asp:RegularExpressionValidator ID="revPreferreddate3" runat="server" ControlToValidate="txtPreferreddatetime3"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPreferreddatetimeto3"
                                Display="None" ErrorMessage="Please enter valid time"  ValidationExpression="^(\d{2}):(\d{2})"
                                ValidationGroup="fls" SetFocusOnError="true" />
                   <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Preferred Date/Time 3 should be greater than Preferred Date/Time 2" ControlToCompare="txtPreferreddate2"
                ControlToValidate="txtPreferreddate3" ValidationGroup="fls" Type="Date" Operator="GreaterThanEqual"
                 Display="None" ></asp:CompareValidator>
                  </div>
        </div>
            </div>
                     </div>
         
            </div>


                                      </div>
</div>
            
            
    <div style="display:none;visibility:hidden;">
            <div>
                Type of Request
            </div>
            <div >
                <asp:DropDownList ID="ddlTypeofRequest" runat="server" Width="180px" ClientIDMode="Static">
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="ccdType" runat="server" TargetControlID="ddlTypeofRequest" 
                    Category="type" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                    ServiceMethod="GetTypeofRequest" LoadingText="[Loading permit...]"  BehaviorID="ccdT" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTypeofRequest"
                    Display="Dynamic" ErrorMessage="Please select type of request" InitialValue="0"
                    SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>
            </div>
             
        </div>
             <div class="form-group row">
        <div class="col-md-12">
 &nbsp;
	</div>
</div>
              <div class="form-group row">
        <div class="col-md-12">
 &nbsp;
	</div>
</div>
            <asp:Panel ID="pnlSmarttech" runat="server" Visible="false">
     <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Assigned Smart Tech </strong> 
<hr class="no-top-margin" />
	</div>
</div>

                <div class="form-group row">
        <div class="col-md-6">
            <asp:GridView ID="gridAssigned" runat="server" Width="100%" OnRowCommand="gridAssigned_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="imguser" runat="server" ImageUrl='<%#Bind("ImageUrl") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Scheduled Date & Time">
                        <ItemTemplate>
                            <asp:Label ID="lblScheduled" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnTracker" runat="server" Text="Track" CommandName="Track" CommandArgument='<%#Bind("UserID") %>'></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            </div>
                    </div>

</asp:Panel>
</asp:Panel>
     <div style="display:none;visibility:hidden;">
     <br />     
<asp:Panel ID="PanelProducts" runat="server">
    <div class="form-group row">
        <div class="col-md-12 text-bold">
 <strong>Registered Equipment(s) </strong> 
<hr class="no-top-margin" />
	</div>
</div>

      <script type="text/javascript" language="javascript"  class="init">
          $(document).ready(function () {
              $('#div_plist').hide();
          })
         

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
        
          
         
          $(document).ready(function () {
            

              //postCreate
              $('#plist').on('create', function (e, json, data) {
                  alert('New row added');
              });
             

          });
                            </script>
                             


    <div>
       
        
    </div>

</asp:Panel>
    <br />

   
             
             


      
           
                <div class="form-group row">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.Documents%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">   <%= Resources.DeffinityRes.Uploadfiles%> </label>
                                      <div class="col-sm-8 form-inline"> <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" BorderStyle="None" ScrollBars="Auto">
                <asp:FileUpload ID="DocumentsUploadnew" runat="server" maxlength="5" class="multi" />
            </asp:Panel>  
            <asp:Button ID="ImgDocumentsUpload" runat="server" SkinID="btnUpload" OnClick="ImgDocumentsUpload_Click"
                ValidationGroup="fls" Visible="false"/>
					</div>
				</div>
                </div>
                
          
            <div class="form-group row">
                <div class="col-md-6">
                <asp:GridView ID="GvDocuments" runat="server" Width="100%" EmptyDataText="No Documents found"
                OnRowCommand="GvDocuments_RowCommand" DataKeyNames="ID,ContentType,FileName">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Uploaded Documents">
                        <ItemTemplate>
                             <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />
                             <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl(DataBinder.Eval(Container.DataItem,"DocumentID").ToString()) %>'
                                            Visible='<%# CheckImageVisibility(DataBinder.Eval(Container.DataItem,"DocumentID").ToString())%>' />
                            <asp:HiddenField ID="hid" runat="server" Value='<%#Bind("ID")%>' />
                            <asp:LinkButton ID="lnkDocuments" CommandArgument='<%#Bind("DocumentID")%>' Text='<%#Bind("FileName") %>'
                                runat="server" CommandName="Download"></asp:LinkButton>

                            <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" CssClass="img-responsive" ImageUrl='<%# GetImageUrlOriginal(DataBinder.Eval(Container.DataItem,"DocumentID").ToString()) %>'
                                                Visible='<%# CheckImageVisibility(DataBinder.Eval(Container.DataItem,"DocumentID").ToString())%>' />
                                        </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDocdelete" runat="server" CommandName="del" SkinID="BtnLinkDelete" CommandArgument='<%#Bind("ID")%>' OnClientClick="return confirm('Do you want to delete document?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                   </div>
               </div>

    </div>
      <div class="form-group row">
        <div  class="col-md-12">
        <span style="margin-right:80px">
             

        </span>
  <div id="div_buttons1">
            <asp:Button ID="btnSave" runat="server" AlternateText="Save" SkinID="btnSave"
                OnClick="btnSave_Click" ValidationGroup="fls" ClientIDMode="Static" />
            <asp:Button ID="btnCancel" runat="server" AlternateText="Cancel" SkinID="btnCancel"
                OnClick="btnCancel_Click" />
                </div>
        </div>
    </div>
  <div class="form-group row">
        <div class="col-md-12">
 &nbsp;
	</div>
</div>
                         <div class="form-group row">
        <div class="col-md-12">
 &nbsp;
	</div>
</div>

 <div class="form-group row">
     <div  class="col-md-12">
    <uc1:FlsHistory ID="ctr_history" runat="server" />
         </div>
    </div>
            <script  type="text/javascript">

               

                function loadPdate() {
                  
                    var r = 0;
                  
                }
                $(document).ready(function () {
                    //$(document.body).find("[id$='lblPageHead']").html('Service Desk');
                });



                $(window).load(function () {

                    if ($("[id$='haddressid']").val() != "") {
                        //alert("aid");
                        loadByAddressID()
                    }


                });
    </script>
    <%-- <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>--%>

    
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css">
   
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.1/css/buttons.dataTables.min.css">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.2.2/css/select.dataTables.min.css">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/fixedcolumns/3.2.4/css/fixedColumns.dataTables.min.css">

    <link rel="stylesheet" type="text/css" href="/Web/css/editor.dataTables.min.css">
   
     
    <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
    </script>
   
     
    <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">

    </script>
     <script type="text/javascript"  src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js">
    </script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.15/sorting/datetime-moment.js"></script>
    
     <script type="text/javascript" src="https://cdn.datatables.net/plug-ins/1.10.16/dataRender/datetime.js">
    </script>
   
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

</div>
               </div>
           </div>
        </div>
    
</asp:Content>
