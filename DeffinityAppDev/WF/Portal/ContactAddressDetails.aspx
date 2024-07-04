<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" CodeBehind="ContactAddressDetails.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.ContactAddressDetails_portal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Contact Address
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblContact" runat="server"></asp:Label> - Address
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/Portal/Home.aspx" ID="btnBack">
<i class="fa fa-arrow-left"></i>Return to Home</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
         <div class="col-md-12">
             <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
             <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="fls"
                DisplayMode="BulletList" ClientIDMode="Static" />
             </div>
        </div>
    
<div class="form-group row">
      <div class="col-md-5">
          <div class="form-group row">
        <div class="col-md-12">
           <strong>Address </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Address 1</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtAddress1" runat="server" ClientIDMode="Static"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress1"
                                        Display="None" Text="Please enter name" ErrorMessage="Please enter Address 1"
                                        ValidationGroup="fls" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Address 2</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtAddress2" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">City</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtCity" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">State</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtState" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Zipcode</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtZipcode" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div><div class="form-group row" style="display:none;visibility:hidden;">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
             <asp:CheckBox ID="chkCopy" runat="server" ClientIDMode="Static" />   Copy Home Information 
               </div>
              </div>
    </div>
          
         <%-- <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Policy type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPolicyType" runat="server" ClientIDMode="Static"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="ddlPolicyType"
                Display="None" ErrorMessage="Please select Policy type" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Policy Starts</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlPolicyStarts" runat="server" ClientIDMode="Static"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPolicyStarts"
                Display="None" ErrorMessage="Please select Policy Starts" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
            <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Start date</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtStartDate" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgStartDate" runat="server"  SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgStartDate" TargetControlID="txtStartDate">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                Display="None" ErrorMessage="Please enter start date" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid start date"
                ControlToValidate="txtStartDate" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                 Display="None" SetFocusOnError="True"></asp:CompareValidator>
            </div>
	</div>
</div>
            <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Contract Term</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlContractTerm" runat="server" ClientIDMode="Static"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlContractTerm"
                Display="None" ErrorMessage="Please select Contract Term" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
            <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Expiry date</label>
           <div class="col-sm-9  form-inline">
               <asp:TextBox ID="txtExpiryDate" runat="server" SkinID="txtCalender" ClientIDMode="Static"></asp:TextBox>
            <asp:Label ID="imgExpiryDate" runat="server"  SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgExpiryDate" TargetControlID="txtExpiryDate">
            </ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpiryDate"
                Display="None" ErrorMessage="Please enter expiry date" SetFocusOnError="True"
                ValidationGroup="fls"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter valid expiry date"
                ControlToValidate="txtExpiryDate" ValidationGroup="fls" Type="Date" Operator="DataTypeCheck"
                 Display="None" SetFocusOnError="True"></asp:CompareValidator>
            </div>
	</div>
</div>
  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Policy number</label>
           <div class="col-sm-9">
                 <asp:TextBox ID="txtPolicynumber" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
           <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Property type</label>
           <div class="col-sm-9">
                 <%--<asp:TextBox ID="txtPropertyType" runat="server" ClientIDMode="Static"></asp:TextBox>
               <asp:DropDownList ID="ddlPropertyType" runat="server">
                   <asp:ListItem Text="Please select..." Value="" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="Single Family" Value="Single Family"></asp:ListItem>
                   <asp:ListItem Text="Condominium" Value="Condominium"></asp:ListItem>
                   <asp:ListItem Text="Townhouse" Value="Townhouse"></asp:ListItem>
                   <asp:ListItem Text="Manufactured" Value="Manufactured"></asp:ListItem>
                   <asp:ListItem Text="Duplex" Value="Duplex"></asp:ListItem>
                   <asp:ListItem Text="Triplex" Value="Triplex"></asp:ListItem>
                   <asp:ListItem Text="Fourplex" Value="Fourplex"></asp:ListItem>
               </asp:DropDownList>
            </div>
	</div>
</div>
           <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Other</label>
           <div class="col-sm-9">
                 <asp:TextBox ID="txtOther" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
           <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:RadioButtonList ID="btnft" runat="server" ClientIDMode="Static">
                   <asp:ListItem Value="0" Text="LESS THAN 5,000 SQ FT" Selected="True"></asp:ListItem>
                   <asp:ListItem Value="1" Text="MORE THAN 5,000 SQ FT"  ></asp:ListItem>
               </asp:RadioButtonList>
            </div>
	</div>
</div>
            <div class="form-group row" style="display:none;visibility:hidden">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Days Remaining</label>
           <div class="col-sm-9">
                 <asp:TextBox ID="txtDaysRemaining" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
            <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Amount</label>
           <div class="col-sm-9">
                 <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
           <div class="form-group row" id="pnlAddon" runat="server">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Add on Cost</label>
           <div class="col-sm-9">
                 <asp:TextBox ID="txtAddons" runat="server" ClientIDMode="Static" Text="0.00"></asp:TextBox>
            </div>
	</div>
</div>--%>
	</div>
	<%--<div class="col-md-4">
          <div class="form-group row">
        <div class="col-md-12">
           <strong>Billing Information </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
           <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Name</label>
           <div class="col-sm-9">
                 <asp:TextBox ID="txtbName" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>

        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Address 1</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtbAddress1" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Address 2</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtbAddress2" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">City</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtbCity" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">State</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtbState" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
          <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Zipcode</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtbZipcode" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
	</div>
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> Payment Status</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPaymentStatus" runat="server" ClientIDMode="Static" >
                   <asp:ListItem Text="Not Processed" Value="Not Processed"></asp:ListItem>
                   <asp:ListItem Text="Paid" Value="Paid"></asp:ListItem>
                   <asp:ListItem Text="Failed Payment" Value="Failed Payment"></asp:ListItem>
               </asp:DropDownList>
            </div>
	</div>
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> Paypal reference</label>
           <div class="col-sm-9">
             <asp:TextBox ID="txtPaypalreference" runat="server" ></asp:TextBox>
            </div>
	</div>
</div>
       
          <div class="form-group row" id="pnlWebSiteRef" runat="server" >
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Order reference: </label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtOrderReference" runat="server" ></asp:TextBox>
            </div>
	</div>
</div>
       
        
	</div>--%>
   
	<%--<div class="col-md-4">
          <div class="form-group row">
        <div class="col-md-12">
           <strong>Cover + Items </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

<div class="form-group row">
          <div class="col-md-12">
             <asp:ListView ID="listAppliances" runat="server" OnItemDataBound="listAppliances_ItemDataBound" >
           <LayoutTemplate>
            <table style="width:100%" class="table table-small-font table-bordered table-striped datatable" >
                <thead>
                    <tr class="tab_header" style="font-weight:bold;margin:5px 5px 5px 5px;height:30px;">
                        <td>Add on</td>
                        <td>Montly Cost</td>
                        <td>Yearly Cost</td>
                        <%-- <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceholder" runat="server"></tr>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
              </LayoutTemplate>
          <ItemTemplate>
              <tr class="even_row">  
                 <td class="form-inline">
                     <asp:CheckBox ID="chkID" runat="server" onclick="tValue(this)" CssClass="chkcls" />
                     <asp:Label ID="lblID" runat="server" Text='<%# Eval("PAPID")%>' Visible="false"></asp:Label>
                      <asp:Label ID="lblType" runat="server" Text='<%# Eval("AddOnDetails")%>'></asp:Label>
                 </td>
                   <td style="text-align:right;">
                      <asp:Label ID="lblMontlyCost" runat="server" Text='<%# Eval("MontlyCost")%>' CssClass="control-label mn"></asp:Label>
                 </td>
                  <td style="text-align:right;">
                      <asp:Label ID="lblYearlyCost" runat="server" Text='<%# Eval("YearlyCost")%>' CssClass="control-label ye"></asp:Label>
                  </td>
                  <%-- <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("PAPID") %>' />
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("PAPID") %>' CommandName="Del" SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete" ImageAlign="AbsMiddle" />
                   </td>
                </tr>     
          </ItemTemplate>
          
          
      </asp:ListView>

              </div>
    </div>


	</div>--%>
   
       <%-- <div class="col-md-8">
             <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-12 control-label"> Notes</label>
              <div class="col-sm-12">
                  <asp:TextBox ID="txtNotes" runat="server" SkinID="txtMulti_150" TextMode="MultiLine"></asp:TextBox>
                  </div>
              </div>
        </div>
            </div>--%>
        
</div>
     
    <div class="form-group row">
        <div class="col-md-6">
              <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                 <asp:Button ID="btnOnlySave" runat="server" SkinID="btnDefault" Text="Save"  ValidationGroup="fls" OnClick="btnOnlySave_Click" />
            <asp:Button ID="btnSave" runat="server" SkinID="btnDefault" Text="Process Payment" OnClick="btnSave_Click"  ValidationGroup="fls" style="display:none;visibility:hidden" />

               </div>
           </div>
                  </div>
           <%-- <asp:Button ID="btnSendPolicy" runat="server" SkinID="btnDefault" Text="Send Policy" ValidationGroup="fls" OnClick="btnSendPolicy_Click" Visible="false" />
            <asp:Button ID="btnDownloadPolicy" runat="server" SkinID="btnDefault" Text="Download Policy" ValidationGroup="fls" OnClick="btnDownloadPolicy_Click" Visible="false" />
            --%></div>
        </div>



    <%-- <div class="form-group row" id="pnlSentMailJournal" runat="server">
        <div class="col-md-6">
            <div class="form-group row">
        <div class="col-md-12">
           <strong>Mail Journal </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            <asp:GridView ID="GridMailTrack" runat="server" ShowHeader="false" ShowFooter="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Eval("index")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("DisplayData")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            </div>
         </div>--%>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">
        hidetabs();
        <%--        function tValue(txt) {
            //alert($('#ddlContractTerm').val());
            //alert($(txt).closest('tr').find('.mn').html());
            //alert($(txt).closest('tr').find('.ye').html());
            var rval = parseFloat("0.00");
            var s = $('#ddlContractTerm').val();
            $('.chkcls').each(function () {
                
                //alert($(this).closest('tr').find('[type=checkbox]').is(':checked'));
                if($(this).closest('tr').find('[type=checkbox]').is(':checked') == true)
                {
                    if (s == 1) {
                        //alert($(this).closest('tr').find('.mn').html());
                        rval = rval + parseFloat($(this).closest('tr').find('.mn').html());
                    }
                    else {
                        rval = rval + parseFloat($(this).closest('tr').find('.ye').html());
                    }
                }
                //alert($(this).closest('tr').find('.mn').html());
                //alert($(this).closest('tr').find('.ye').html());
            });
            //alert(rval);
            //var amt = parseFloat($('#txtAmount').val()) + rval;
            var amt = rval.toFixed(2);
            $('#txtAddons').val(amt);
        }
        $(document).ready(function () {
            hidetabs();

            $("#chkCopy").click(function () {

                $("#txtbAddress1").val($("#txtAddress1").val());
                $("#txtbAddress2").val($("#txtAddress2").val());
                $("#txtbCity").val($("#txtCity").val());
                $("#txtbState").val($("#txtState").val());
                $("#txtbZipcode").val($("#txtZipcode").val());
            });

            $('#ddlPolicyType').change(function () {
                //alert("Handler for .change() called.");
               
                //var s = $('#ddlPolicyType').val();
                debugger;
                setPolicyNumber();
                
            });
            $('#ddlPolicyStarts').change(function () {
                setstartDate();
            });
            $('#ddlContractTerm').change(function () {
                setstartDate();
                setendDate();
                setPolicyCost();
            });
            $('#<%=btnft.ClientID %> input').change(function () {
                setPolicyCost();
            });
        });



        function setPolicyNumber() {
            try {
                debugger;
                $.ajax({
                    url: '/api/GetPolicynonew',
                    data: {
                        typeid: $('#ddlPolicyType').val()
                    },
                    type: 'post',
                    dataType: 'json',
                    success: function (json) {
                        debugger;
                        //callback(json.options);
                        var tt = json.options.policyno;
                        if (tt != "")
                            $('#txtPolicynumber').val(tt);
                         //   editor.set('PortfolioContactAddress.PolicyNumber', tt);
                        //editor.field('PortfolioContactAddress.PolicyNumber').update('test');
                    }
                });
            }
            catch (err) {

            }
        }
        function setPolicyCost() {
            try {
                debugger;
                var checked_radio = $("[id*=btnft] input:checked");
                var value = checked_radio.val();
                $.ajax({
                    url: '/api/GetPolicycost',
                    data: {
                        typeid: $('#ddlPolicyType').val(),
                        ContractTermID: $('#ddlContractTerm').val(),
                        more: value,
                       
                    },
                    type: 'post',
                    dataType: 'json',
                    success: function (json) {
                        debugger;
                        //callback(json.options);
                        var tt = json.options.policycost;
                        if (tt != "")
                            $('#txtAmount').val(tt);
                       
                    }
                });
            }
            catch (err) {

            }
        }
        function setendDate() {
            try {
                debugger;
                $.ajax({
                    url: '/api/getenddatenew',
                    data: {
                        ContractTermID: $('#ddlContractTerm').val(),
                        StartDate: $('#txtStartDate').val(),
                       
                        
                    },
                    type: 'post',
                    dataType: 'json',
                    success: function (json) {
                        debugger;
                        var tt = json.options.enddate;
                        if (tt != "") {
                            $('#txtExpiryDate').val(tt);
                        }
                    }
                });
            }
            catch (err) {

            }
        }

        function setstartDate() {
            try {
                debugger;
                $.ajax({
                    url: '/api/getstartdatenew',
                    data: {
                        PolicyStartsID: $('#ddlPolicyStarts').val(),
                        StartDate: $('#txtStartDate').val()
                        
                    },
                    type: 'post',
                    dataType: 'json',
                    success: function (json) {
                        debugger;
                        //callback(json.options);
                        var tt = json.options.startdate;
                        if (tt != "") {

                            $('#txtStartDate').val(tt);
                        }
                        //editor.field('PortfolioContactAddress.PolicyNumber').update('test');
                    }
                });
            }
            catch (err) {

            }
        }--%>
    </script>
</asp:Content>