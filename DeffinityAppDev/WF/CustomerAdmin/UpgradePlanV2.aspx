<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="UpgradePlanV2.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.UpgradePlanV2" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		.page-title{
			display:none;
		}

       input[type="checkbox"].iswitch-lg.iswitch-info:checked {
  box-shadow: inset 0 0 0 16px #40bbea !important;


}
       .col-center-block {
    float: none;
    display: block;
    margin: 0 auto;
    /* margin-left: auto; margin-right: auto; */
}
     
	</style>
		<!--     Fonts and icons     -->
	<%--<link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />--%>
	<%--<link href="../../Content/assets1/css/bootstrap.min.css" rel="stylesheet" />--%>
 <%--   <link href="../../Content/assets1/css/material-bootstrap-wizard.css" rel="stylesheet" />--%>
		<!-- CSS Files -->
	<link href="../../Content/assets1/css/material-bootstrap-wizard.css" rel="stylesheet" />
	

	<!-- CSS Just for demo purpose, don't include it in your project -->
	
    <%--<link href="../../Content/assets1/css/demo.css" rel="stylesheet" />--%>
   <asp:HiddenField ID="hamount" runat="server" ClientIDMode="Static" />
     <script type="text/javascript">
         function SelectSingleRadiobutton(rdbtnid) {

            
             var rdBtn = document.getElementById(rdbtnid);
             var rdBtnList = document.getElementsByTagName("input");
             console.log($('#' + rdbtnid).val());
             $('#hamount').val($('#' + rdbtnid).val());
             //update price
             getPrice();
             for (i = 0; i < rdBtnList.length; i++) {
                 if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                     rdBtnList[i].checked = false;
                 }
             }
         }
         function RadioCheck(rb) {

             var gv = document.getElementById("<%=list_Customfields.ClientID%>");

             var rbs = gv.getElementsByTagName("input");



             var row = rb.parentNode.parentNode;

             for (var i = 0; i < rbs.length; i++) {

                 if (rbs[i].type == "radio") {

                     if (rbs[i].checked && rbs[i] != rb) {

                         rbs[i].checked = false;

                         break;

                     }

                 }

             }

         }
     </script>
   <%-- <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>--%>

     <asp:Panel ID="pnlResult" runat="server" Visible="false">
          <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblResultSucess" runat="server"  EnableViewState="false" CssClass="green-alert green-alert-success" Visible="false" style="width:100%"></asp:Label>
               <asp:Label ID="lblResultFail" runat="server" EnableViewState="false" CssClass="red-alert red-alert-danger" Visible="false" style="width:100%"></asp:Label>
              </div>
         </div>
          <div class="form-group row">
          <div class="col-md-12">
              </div>
              </div>
          <div class="form-group row">
          <div class="col-md-12">
              <asp:Button ID="btnBack" runat="server" Text="Back to Home" SkinID="btnDefault" OnClick="btnBack_Click" Visible="false"  />
              </div>
              </div>
         </asp:Panel>
            <div class="row">
		        <div class="col-sm-10 col-sm-offset-1">
		            <!--      Wizard container        -->
		            <div class="wizard-container">
		                <div class="card wizard-card" data-color="red" id="wizard">
		                   <%-- <form action="" method="">--%>
		                <!--        You can switch " data-color="blue" "  with one of the next bright colors: "green", "orange", "red", "purple"             -->

		                    	<div class="wizard-header">
		                        	<h3 class="wizard-title" style="font-size:x-large;">
		                        		Upgrade Plan
		                        	</h3>
									<h5><asp:Label ID="lblTitle" runat="server"></asp:Label> </h5>
		                    	</div>
								<div class="wizard-navigation">
									<ul>
			                            <li><a href="#pnlplans" data-toggle="tab" style="font-weight:bold;"> Plans</a></li>
			                            <li><a href="#pnlusers" data-toggle="tab"  style="font-weight:bold;">No. of Users</a></li>
			                            <li><a href="#pnlpayment" data-toggle="tab"  style="font-weight:bold;">Payment</a></li>
			                        </ul>
								</div>

		                        <div class="tab-content">
		                            <div class="tab-pane" id="pnlplans" >
                                        
		                            	<div class="row">
			                            	<div class="col-sm-12">
			                                	<h4 class="info-text" style="font-size:20px"> Choose plan</h4>
			                            	</div>
		                                	
		                            	</div>

                                        <div class="form-block" style="text-align:center;">
                                        <asp:CheckBox ID="chk" runat="server" CssClass="iswitch iswitch-blue" Text="Year" Checked="true" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" style="font-size:medium;"  />
                                            <asp:CheckBox ID="chk_month" runat="server" CssClass="iswitch iswitch-blue" Text="Month" AutoPostBack="true" OnCheckedChanged="chk_month_CheckedChanged" style="font-size:medium;"  />
                                      </div>
                                            <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="None" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
               <div class="container-fluid">
              <div class="row" id="table1" style="
    text-align: center;
    margin: auto;
    width: max-content;
">
        
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  </div>
              </LayoutTemplate>
          <ItemTemplate>

              <div class="col-md-4" style="text-align:left;width:350px">
			<div style="text-align:center;" >	<asp:RadioButton ID="radiioPlan" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" value='<%# Eval("Price") %>' /> </div>

				  <blockquote  style="min-height:700px;padding: 20px 15px;" class='<%# GetDesign( Eval("PlanName").ToString()) %>' runat="server">
					  <p style="font-size:medium"> <strong> <asp:Literal ID="lblPlantitle" runat="server" Text='<%# Eval("PlanName") %>'></asp:Literal></strong> </p>
                      <div class="form-group row">
                           <div class="col-md-12 form-inline" >
                      <strong style="font-size:xxx-large" class="col-sm-6"> <asp:Literal ID="txtMonth" runat="server" Text='<%# Eval("Price","{0:C2}") %>'></asp:Literal></strong>
                         
                         </div>
                           <div class="col-md-12 form-inline" style="text-align:center;" >
                                <label class="col-sm-10 control-label" style="font-size:18px;text-align:center;"> User/ <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Term") %>'></asp:Literal></label>
                               </div>
                               </div>
					  <div>
            <%--  <div class="well" style="min-height:760px;">--%>
                   <div class="form-group row" style="text-align:right;margin-bottom:-5px;">
       
            <p style="font-size:15px;text-align:right;">
           
                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                </p>
                        </div>

						    <div class="form-group row">
      <div class="col-md-12" style="text-align:center;">
          <asp:HiddenField ID="hMonth" runat="server" Value='<%# Eval("Price","{0:F2}") %>'></asp:HiddenField>
           <asp:HiddenField ID="hYear" runat="server" Value='<%# Eval("Price","{0:F2}") %>'></asp:HiddenField>
              </div>
              </div>
                       
                      
          
                           <div class="form-group row" style="padding-bottom:0px">

                               </div>


                    <div class="form-group row">
      <div class="col-md-12"  style="text-align:center;border-bottom-style: double;
    border-bottom-color: silver;">
           <label class="col-sm-12 control-label" style="font-size:24px;">Modules Included</label>
          
          
          </div>
                       </div>
           <br />
          
                   <div class="form-group row">
                       <div class="col-md-12 form-inline" style="height:300px">
                           
                          <asp:GridView ID="gv" runat="server" Width="100%" ShowHeader="false" SkinID="NewGrid" CellSpacing="15">
                              <Columns>
                                 <%-- <asp:TemplateField Visible="false">
                                      <ItemTemplate>
                                          <asp:CheckBox runat="server" ID="chk" />
                                          <asp:Label ID="lblModuleID" runat="server"  Text='<%# Eval("ModuleID") %>' Visible="false"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>--%>
                                  <asp:TemplateField ItemStyle-Height="35">
                                      <ItemTemplate>
                                        <i class="fa fa-check" style="
    color: silver;
    padding-right: 8px;
"></i>  <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("ModuleName") %>' style="font-size:20px" ForeColor="Silver" ></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                       </div>
                       </div>
                
    <%--</div>--%></div>
					  </blockquote>
                  </div>
              </ItemTemplate>
               </asp:ListView>
		                            </div>
		                            <div class="tab-pane" id="pnlusers">

		                                <h4 class="info-text"><%--Select User--%> The current number of users on the portal</h4>
		                                <div class="row">
		                                    <div class="col-sm-10 col-sm-offset-1">
                                                  <div class="col-sm-4">
                                                      </div>
		                                        <div class="col-sm-4">
		                                            <div class="choice" data-toggle="wizard-radio" rel="tooltip" title="">
		                                               <asp:DropDownList ID="ddlUsers" runat="server" Width="150px">
														  
		                                               </asp:DropDownList>
		                                            </div>
		                                        </div>
		                                        <div class="col-sm-4">
                                                      </div>
		                                    </div>
		                                </div>
		                            </div>
		                            <div class="tab-pane" id="pnlpayment">
		                                <div class="row">
		                                  <%--  <h4 class="info-text"> Payment</h4>--%>
		                                    <div class="col-sm-8">
	                                    		 <script language="JavaScript">
         function showMe() {
             var mytoken = document.getElementById('mytoken');
             alert("Token=" + mytoken.value);
         }
         </script>
    <script type="text/javascript">
        window.addEventListener('message', function (event) {
            var token = JSON.parse(event.data);
            var mytoken = document.getElementById('mytoken');
            mytoken.value = token.message;
        }, false);

    </script>
      <div class="form-group row">
           <div class="col-md-1">
               </div>
          <div class="col-md-10">
    <img src="../../../Content/images/icon_cardconnect_old1.png">
              </div>
           </div>
    <asp:Panel ID="pnlPaymentDetails" runat="server">
     <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
               <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              <asp:ValidationSummary ID="pSUm" runat="server" ValidationGroup="p" />
               <asp:Label ID="lblCardERROR" runat="server" EnableViewState="false"></asp:Label>
              </div>
         </div>
        
    <div class="form-group row" style="display:none;visibility:hidden;">
          <div class="col-md-12">
 <label class="col-sm-3 control-label" style="font-size:medium">Amount to Charge</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" 
                            Width="150px">0.00</asp:TextBox>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label" style="font-size:medium">Card Type</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCardType" runat="server" SkinID="ddl_200px">
                           <asp:ListItem Value="MASTERCARD" Text="MASTERCARD"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="VISA" Text="VISA"></asp:ListItem>
                   <asp:ListItem Value="DISCOVER" Text="DISCOVER"></asp:ListItem>
                    <asp:ListItem Value="AMEX" Text="AMEX"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlCardType" Display="Dynamic" CssClass="error-text" ErrorMessage="Required" 
                            ></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label" style="font-size:medium">Card Number</label>
           <div class="col-sm-9">
               <div id="pnlCreditCard" runat="server">
                   <asp:TextBox ID="txtCardConnectNumber" runat="server" CssClass="paymentinfo-text" SkinID="txt_50" Visible="false"></asp:TextBox>
                   <asp:TextBox ID="txtCardNumber" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfCardnumber" runat="server" 
                            ControlToValidate="txtCardNumber" Display="Dynamic"  ErrorMessage="Please enter Card Number"></asp:RequiredFieldValidator>
                   </div>
               <div id="pnlCardConnect" runat="server">
                   <iframe id="tokenframe" name="tokenframe"  
                       src="https://boltgw.cardconnect.com/itoke/ajax-tokenizer.html?css%3D%252Eerror%7Bcolor%3A%2520red%3B%7D" 
                       frameborder="0" scrolling="no" width="200" height="35" runat="server"></iframe>
                <asp:HiddenField ID="mytoken" runat="server" ClientIDMode="Static" />
                   </div>
               <p>e.g: 4111111111111111</p>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label" style="font-size:medium"> Name on Card</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_200px" MaxLength="250"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="None" ErrorMessage="Please enter name on card" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label" style="font-size:medium">Expiration</label>
           <div class="col-sm-9 form-inline">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="paymentinfo-text" SkinID="ddl_125px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="paymentinfo-text"  SkinID="ddl_125px">
                        </asp:DropDownList>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlMonth" Display="None"  
                            ErrorMessage="Please select month" ValidationGroup="p"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlYear" Display="None" 
                            ErrorMessage="Please select year" ValidationGroup="p"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label" style="font-size:medium">Card Security Code</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            SkinID="txt_75px" MaxLength="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="None" 
                            ErrorMessage="Please enter CVV" ValidationGroup="p" ></asp:RequiredFieldValidator>
                         <%-- <p>  A code that is printed (not imprinted) on the back of 
                                a credit card. It consist of 3 or 4 digits. </p>--%>
               <p>e.g: 123 </p>
            </div>
	</div>
</div>
        </asp:Panel>
		                                    </div>
		                                    <div class="col-sm-4">
                                               <div class="form-group row" style="height:60px">
           <div class="col-md-1">
               </div>
          <div class="col-md-10">
    <label>  </label>
              </div>
           </div>
                                                    <div class="form-group row">
                                                     <div class="col-md-12">
                                                          <label class="col-sm-8 control-label" style="font-size:medium"><asp:Literal ID="lblSelectedPlan" runat="server" Text="ANNUAL PLAN COST"></asp:Literal> </label>
                                                            <div class="col-sm-4  pull-right">
                                                                <asp:Label ID="lblPrice" runat="server" Text="0.00" style="float:right;padding-top:20px" Font-Size="X-Large"></asp:Label>
                                                              </div>
                                                         </div>
                                                </div>

                                                  <%--  <div class="form-group row">
                                                     <div class="col-md-12">
                                                         &nbsp;
                                                         </div>
                                                </div>--%>
                                                <%-- <div class="form-group row">
                                                     <hr />
                                                     </div>--%>
                                                    <div class="form-group row" style="display:none;visibility:visible;">
                                                    <div class="col-md-12">
                                                          <label class="col-sm-8 control-label" style="font-size:medium;"><asp:Literal ID="lblDisplayTotal" runat="server" Text="Total"></asp:Literal> </label>
                                                            <div class="col-sm-4 pull-right">
                                                                <asp:Label ID="lblTotal" runat="server" Text="0.00" style="float:right;" Font-Size="X-Large"></asp:Label>
                                                              </div>
                                                         </div>
                                                </div>
                                                   <div class="form-group row">
                                                     <div class="col-md-12">
                                                        <asp:Button ID="btnCompleteProcess" runat="server" SkinID="btnDefault" style="width:100%" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="p" />
                                                         </div>
                                                </div>
		                                    	<div class="form-group form-inline">
                                                      <asp:HiddenField ID="hterm" runat="server" />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                            onclick="btnSubmit_Click" ValidationGroup="p" Visible="false" />
                        &nbsp;
                        <asp:Button id="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" Visible="false" />
		                                          <%--  <label class="control-label">Example</label>--%>
                                                    

		                                         <%--    <div class="form-group row">
          <div class="col-md-8">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
             
            </div>
	</div>
</div>--%>
		                                        </div>
		                                    </div>
		                                </div>
		                            </div>
		                        </div>
	                        	<div class="wizard-footer">
	                            	<div class="pull-right">
	                                    <input type='button' class='btn btn-next btn-fill btn-danger btn-wd' name='next' value='Next' />
	                                    <input type='button' class='btn btn-finish btn-fill btn-danger btn-wd' name='finish' value='Finish' />
                                       
	                                </div>
	                                <div class="pull-left">
	                                    <input type='button' class='btn btn-previous btn-fill btn-default btn-wd' name='previous' value='Previous' />

										<div class="footer-checkbox">
											<div class="col-sm-12">
											 <%-- <div class="checkbox">
												  <label>
													  <input type="checkbox" name="optionsCheckboxes">
												  </label>
												  Subscribe to our newsletter
											  </div>--%>
										  </div>
										</div>
	                                </div>
	                                <div class="clearfix"></div>
	                        	</div>
		                  <%--  </form>--%>
		                </div>
		            </div> <!-- wizard container -->
		        </div>
	    	</div>


            <script src="../../Content/assets1/js/jquery-2.2.4.min.js"></script>
    <script src="../../Content/assets1/js/bootstrap.min.js"></script>
    <script src="../../Content/assets1/js/jquery.bootstrap.js"></script>
    <script src="../../Content/assets1/js/material-bootstrap-wizard.js?i=20"></script>
            <!--  More information about jquery.validate here: http://jqueryvalidation.org/	 -->
	<script src="../../Content/assets1/js/jquery.validate.min.js"></script>
      <%--  </ContentTemplate>
        <Triggers>

        </Triggers>

    </asp:UpdatePanel>
    --%>
    
	<script>

        $(document).ready(function () {
            getPrice();
            $("select[id*='ddlUsers']").click(function () {
               // $(".target").change();
               // console.log($("select[id*='ddlUsers']").val());
                getPrice();
            });
           // $("#MainContent_lblPrice").html('new data');
        });

        function getPrice() {
            var d = $("select[id*='ddlUsers']").val();
            var p = $('#hamount').val();
            //console.log(d);
            //console.log(p);
            var t = (parseInt(d) * parseInt(p)).toFixed(2);
            //console.log(t);
            $("span[id*='lblPrice']").html(t);
            //txtAmount
            $("[id*='txtAmount']").val(t);
            $('#MainContent_lblPrice').html(t);

            // $("span[id*='lblPrice']").html('new data1111');
            //console.log(d);
        }

    </script>
<link href="../../Content/assets/css/xenon-forms.css" rel="stylesheet" />
    <script src="../../Content/assets/js/xenon-toggles.js"></script>
	<script>
        //$(function () { $('#MainContent_chk').bootstrapToggle() });
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
