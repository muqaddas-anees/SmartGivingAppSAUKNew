<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Instances.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.Instances" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
      <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" id="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    123 Admin Users
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function SelectSingleCheckbox(Chkid) {
            var chkbid = document.getElementById(Chkid);
            var chkList = document.getElementsByTagName("input");
            for (i = 0; i < chkList.length; i++) {
                if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                    chkList[i].checked = false;
                }
            }
        }
</script>

     <div class="form-group row">
      <div class="col-md-5 form-inline">
          <asp:TextBox ID="txtSearch" runat="server" SkinID="txt_80"></asp:TextBox> <asp:LinkButton ID="btnSearch" runat="server" SkinID="BtnLinkSearch" OnClick="btnSearch_Click"></asp:LinkButton>
          </div>
          <div class="col-md-3 form-inline well" style="padding:10px;margin-left:5px;margin-right:5px">
              <div class="col-sm-12 control-label" style="font-weight:bold;font-size:medium;text-align:center;"> Number of Instances</div>

              <div class="col-sm-12" style="text-align:center"><asp:Label ID="lblNumberofInstances" runat="server" Text="0"></asp:Label></div>
                
              </div>
          <div class="col-md-3 form-inline well" style="padding:10px;margin-left:5px;margin-right:5px">
               <div class="col-sm-12 control-label" style="font-weight:bold;font-size:medium;text-align:center;"> Number of Users</div>
                  <div class="col-sm-12" style="text-align:center"> <asp:Label ID="lblNumberofUsers" runat="server" Text="0"></asp:Label>
                      </div>

              </div>
         
         </div>
      <div class="form-group row">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
    <div class="form-group row pull-right" style="float:right;" >
        <div class="btn-group">
									<button type="button" class="btn btn-gray">More</button>
									<button type="button" class="btn btn-gray dropdown-toggle" data-toggle="dropdown" >
										<span class="caret"></span>
									</button>
									<ul class="dropdown-menu dropdown-green dropdown-menu-right" role="menu">
										<li>
											 <asp:LinkButton ID="btnDeleteContacts" runat="server" Text="Delete all contacts" OnClick="btnDeleteContacts_Click" OnClientClick="javasscript:return confirm('Do you want to delete all the contacts?');" CausesValidation="false"></asp:LinkButton> 
										</li>
                                       
									</ul>
	           </div>
    </div>
    <asp:GridView ID="GridInstances" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridInstances_RowCommand" OnRowDataBound="GridInstances_RowDataBound" OnPageIndexChanging="GridInstances_PageIndexChanging">
        <Columns>
              <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
                                                        <a id="imageColapse" href="javascript:expandcollapse('div<%# Eval("PortfolioID") %>', 'one');">
                                                            <img id='imgdiv<%# Eval("PortfolioID") %>' alt="Click to show/hide <%# Eval("PortfolioID") %>"
                                                                width="9px" border="0" src="../../Content/images/plus.gif" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
                                                         <asp:Label ID="lblPortfolioID" runat="server" Width="40px" Text='<%# Bind("PortfolioID") %>' Visible="false"></asp:Label>
                                                       <asp:CheckBox ID="chk" runat="server" OnClick="javascript:SelectSingleCheckbox(this.id)"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Instance" SortExpression="Instance">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstance" runat="server" Text='<%# Bind("InstanceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Sector" SortExpression="Sector">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSector" runat="server" Text='<%# Bind("Sector") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Administrator" SortExpression="Administrator">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAdministrator" runat="server"  Text='<%# Bind("Administrator") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Email Address" SortExpression="EmailAddress">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Contact" SortExpression="Contact">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("Contact") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Town" SortExpression="Town">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTown" runat="server" Text='<%# Bind("Town") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Zipcode" SortExpression="Zipcode">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZipcode" runat="server" Text='<%# Bind("Zipcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CommandName="password" CommandArgument='<%# Bind("ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEnableInstance" runat="server" CssClass='<%# SetCssActiveInstance(Eval("Visibility").ToString())  %>' Text='<%# Eval("Visibility").ToString().ToLower() == "false"? "Enable Instance":"Disable Instance" %>' CommandName="Instance" CommandArgument='<%# Bind("PortfolioID") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnAllowModules" runat="server" CssClass='<%# SetCssActiveInstance(Eval("AllowModules").ToString())  %>' Text='<%# Eval("AllowModules").ToString().ToLower() == "false"? "Enable Modules":"Disable Modules" %>' CommandName="Modules" CommandArgument='<%# Bind("PortfolioID") %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

             <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id='div<%# Eval("PortfolioID") %>' style="overflow-x:auto;display:none;">
                                                                    <asp:GridView ID="gvInnerUsers" SkinID="gv_nested" runat="server" 
                                                                         AutoGenerateColumns="false" PageSize="10" DataKeyNames="CompanyID" >
                                                                        <Columns>
                                                                          
                                                                         
                                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Width="40px" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="User" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContractorName" runat="server"  Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Type" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContractorType" runat="server"  Text='<%# Bind("ContractorType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Email" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Contact" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContactNumber" runat="server" Text='<%# Bind("ContactNumber") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="" >
                                                                                <ItemTemplate>
                                                                                   <asp:Button ID="btnChangePassword" runat="server" SkinID="btnDefault" Text="Change Password" CommandName="password" CommandArgument='<%# Bind("ID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="" >
                                                                                <ItemTemplate>
                                                                                   <asp:Button ID="btnEnableUser" runat="server" SkinID="btnDefault" CssClass='<%# SetCssActiveUsers(Eval("Status").ToString())  %>' Text='<%# Eval("Status").ToString() == "Active"? "Disable User":"Enable User" %>' CommandName="status" CommandArgument='<%# Bind("ID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                          
                                                                         
                                                                          
                                                                        </Columns>
                                                                    </asp:GridView>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="2%" />
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="700px" Height="270px" CssClass="panel panel-color panel-info" ScrollBars="None">
           <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="panel-heading">
							<h3 class="panel-title"><asp:Label ID="lblOptions" runat="server" Text="Reset Password"></asp:Label>  </h3>
							
							<div class="panel-options">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       <asp:Label ID="lblMsgPop" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                       <asp:Label ID="lblErrorPop" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="valSubmit" />
                       </div>
            </div>

         
        
        
    <div class="form-group row">
                   <div class="col-md-12">
                         <label class="col-sm-2 control-label">Password</label>
                  <div class="col-sm-10 form-inline">
                      <asp:TextBox ID="txtPassword" runat="server"  SkinID="txt_60" MaxLength="20"></asp:TextBox>
                      <asp:Button ID="btnGenaratePassword" runat="server" OnClick="btnGenaratePassword_Click" SkinID="btnDefault" Text="Generate Password"  />
                       <asp:RegularExpressionValidator ID="Regex3" runat="server" Display="None" ValidationGroup="valSubmit" ControlToValidate="txtPassword"
ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
ErrorMessage="Please enter password minimum 8 characters at least 1 upperCase alphabet, 1 lowerCase alphabet, 1 number and 1 special character" />
                      
                      </div>
                       </div>
                        </div>
 

           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <div class="col-sm-12 form-inline">
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Send to User" OnClick="btnSubmitPop_Click" ValidationGroup="valSubmit" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
                           </div>
                       </div>
               </div>
                     </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>
           </asp:Panel>


    
    <script type="text/javascript">
        function expandcollapse(obj, row) {
            debugger;
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
            Close_All(obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "../../Content/images/minus.gif";
                }
                else {
                    img.src = "../../Content/images/minus.gif";
                }
                img.alt = "Close";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "../../Content/images/plus.gif";
                }
                else {
                    img.src = "../../Content/images/plus.gif";
                }
                img.alt = "Expand";
            }
        }
        function Close_All(obj) {
            var divOld = document.getElementById(obj);
            var getAttribute;
            var str = '';
            var Grid_Table = document.getElementById('<%= GridInstances.ClientID %>');
            for (var row = 1; row < Grid_Table.rows.length - 1; row++) {
                //expandcollapse(Grid_Table, row)

                var imageColapsenm;
                imageColapsenm = Grid_Table.rows[row].cells[0].firstChild.id;
                if (imageColapsenm != 'imageColapse') {
                    //alert(imageColapsenm);
                    if (imageColapsenm != null) {

                        var div = document.getElementById(imageColapsenm);
                        var img = document.getElementById('img' + imageColapsenm);
                        if (divOld != div) {
                            div.style.display = "none";
                            img.src = "../../Content/images/plus.gif";
                            img.alt = "Expand to show Questionarrie";
                        }
                    }
                }

            }


            return false;
        }
    </script>

      <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    NestedGridResponsiveCss();
 </script> 
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
