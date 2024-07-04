<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUsers_1"
     Title="Manage users" EnableEventValidation="false" Codebehind="AdminUsers.aspx.cs" %>

<%@ Register Src="controls/MangeUserTab.ascx" TagName="MangeUserTab" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ManageUsers%> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_options" runat="Server">
      
    <asp:HyperLink ID="btngohome" runat="server" NavigateUrl="~/WF/Admin/UserManagement.aspx?Type=Administrators">
        <i class="fa fa-arrow-left"></i> Return to Member list</asp:HyperLink>
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="Server">
     
     <script type="text/javascript">
         hidetabs();
         $(document).ready(function () {
             $(".expand_fields").hide();
             var i = 0;
             $("#div_expand").click(function () {
                 $(".expand_fields").fadeToggle();
                 if (i == 0) {
                     $("#div_expand").html("Hide Additional Information");
                     i = 1;
                 }
                 else {
                     $("#div_expand").html("Expand for Additional Information");
                     i = 0;
                 }
             });
         });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="form-group row">
<div class="col-md-12">
    
    </div>
         </div>
     <asp:Panel ID="pnluserbuttons" runat='server'>
         <div class="row">
                                <div class="col-md-8 form-inline">
                                    <asp:Button ID="imgactiveuser" runat="server" Visible="false" CausesValidation="False"
                                        SkinID="btnDefault" OnClick="imgactiveuser_Click" Text="Show Active Users" />
                                    <asp:Button ID="btn_showInActiveUser" Visible="false"
                                            runat="server" CausesValidation="False" SkinID="btnDefault" OnClick="btn_showInActiveUser_Click" Text="Show Inactive Users" />
                                    <asp:Button ID="imgAddNew" runat="server" CausesValidation="False" SkinID="btnDefault" Visible="false"
                                        OnClick="imgAddNew_Click" Text="Add New User" />
                                    <asp:Button ID="btn_Managecontacts" runat="server" CausesValidation="False" Visible="false"
                                        SkinID="btnDefault" OnClick="btn_Managecontacts_Click" ValidationGroup="valSubmit" Text="User Contacts" />
                                    </div>
                                  <div class="col-md-4 ">
                                      
                                 </div>
                   </div>
                </asp:Panel>
                <asp:Panel ID="pnlusername" runat="server">
                      <div class="row">
                                <div class="col-md-8 form-inline">
                                    User Admin for :
                                    <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
                                    </div>
                                <div class="col-md-4 pull-right">
                                   
                                    </div>
                          </div>
                    <asp:Panel ID="pnl" runat="server">
                        <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
                    </asp:Panel>
                </asp:Panel>

                <div id="countrydivcontainer">
                    <asp:Panel ID="UserData" runat="server">
                         <div class="form-group row">
<div class="col-md-12">
   
    </div>
                             </div>
                        <div class="form-group row">
<div class="col-md-12">
     <ajaxToolkit:ModalPopupExtender ID="mdlArea" CancelControlID="imgAreaCancel" runat="server" BackgroundCssClass="modalBackground"
                                            TargetControlID="hid_area" PopupControlID="pnlArea">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlArea" runat="server" BackColor="White" Style="display: none" Width="230px"
                                            BorderStyle="Double" BorderColor="LightSteelBlue">
                                            <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                                Area
                                            </div>

                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtArea" runat="server" Width="210px"></asp:TextBox>
                                                        <asp:HiddenField ID="H_Area" runat="server" Value="0" />
                                                        <asp:Label ID="lblArea" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtArea"
                                                            ErrorMessage="Please enter area" ForeColor="Red" ValidationGroup="Group_Area"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="imgAreaSubmit" runat="server" Text="OK" SkinID="btnSubmit"
                                                            OnClick="imgAreaSubmit_Click" ValidationGroup="Group_Area" />
                                                        <asp:LinkButton ID="imgAreaCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                                    </td>

                                                </tr>
                                            </table>

                                        </asp:Panel>

    
                                    
                                        <asp:Label ID="lblError1" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                                        <asp:Label ID="lblMsg1" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valSubmit" DisplayMode="List"  />
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Please enter email" Display="None" ValidationGroup="valSubmit"></asp:RegularExpressionValidator>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUser"
                                        Display="None" ErrorMessage="Please enter full name" ValidationGroup="valSubmit" style="display:none;"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drContractors"
                                        Display="None" ErrorMessage="Please select full name" InitialValue="Please select..."
                                        ValidationGroup="valSubmit"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="validmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="valSubmit"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="emailblank" runat="server" ErrorMessage="Please enter email"
                                        ControlToValidate="txtEmail" Display="None" ValidationGroup="valSubmit"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtStartDate"
                                        Display="None" ErrorMessage="Please enter valid Start date" Operator="DataTypeCheck"
                                        Type="Date" ValidationGroup="valSubmit"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtReleaseDate"
                                        Display="None" ErrorMessage="Please enter valid  Release date" Operator="DataTypeCheck"
                                        Type="Date" ValidationGroup="valSubmit"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Please enter valid date"
                                        ControlToValidate="txtDOB" ValidationGroup="valSubmit" Type="Date" Operator="DataTypeCheck"
                                         Display="None" SetFocusOnError="True"></asp:CompareValidator>
</div>
</div>

                         <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                       <label class="col-sm-4 control-label form-inline"> Full Name<span style="color: #ff0000">*</span></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="drContractors" runat="server" SkinID="ddl_80"
                                                                      OnSelectedIndexChanged="drContractors_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    <asp:TextBox ID="txtUser" runat="server" Visible="false" SkinID="txt_70" MaxLength="50" />
                                    <asp:LinkButton ID="imgCancel" runat="server" Visible="false" OnClick="btnCancel_Click"
                                         CausesValidation="False" SkinID="btnCancel" style="margin-bottom:0px;" />&nbsp;<span
                                            style="color: #ff0000"></span>
					                        </div>
				                            </div>
                                        </div>

                                      <div class="form-group row" id="PanleEditName" runat="server" visible="false">
                                        <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Full Name<span style="color: #ff0000">*</span></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtEditName" runat="server" SkinID="txt_80" MaxLength="50" />
					                        </div>
				                            </div>
                                        </div>

                                      <div class="form-group row">
                                        <div class="col-md-12">
                                       <label class="col-sm-4 control-label form-inline"> Permission Level<span style="color: #ff0000">*</span></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="drpermission" runat="server" SkinID="ddl_80" AutoPostBack="true" OnSelectedIndexChanged="drpermission_SelectedIndexChanged">
                                    </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFP" runat="server"
                                        ControlToValidate="drpermission" Display="None"
                                        ErrorMessage="Please select Permission level" InitialValue="0"
                                        ValidationGroup="valSubmit"></asp:RequiredFieldValidator>
                                    <asp:Button ID="btnListUsers" runat="server" CausesValidation="False"
                                        OnClick="btnListUsers_Click" Visible="false" SkinID="btnDefault" Text="Vendor Management"></asp:Button>
                                  
					                        </div>
				                            </div>
                                        </div>
                                    
                            <asp:Panel ID="GetID" runat="server" Visible="false">
                                 <div class="form-group row">
                                <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">Type</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="ddlCasual_Labour" SkinID="ddl_80" runat="server">
                                            <asp:ListItem Value="0" Text="Please select..."></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btn_Casuallobour" runat="server"
                                            SkinID="BtnLinkAdd" CausesValidation="False"
                                            OnClick="btn_Casuallobour_Click" /></div>

                                </div>
                                     </div>
                                <asp:Panel ID="CasPass" runat="server" Visible="false">
                                    <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">New&nbsp;Password<span style="color: #ff0000">*</span></label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtCasulaPas" runat="server" TextMode="Password" SkinID="txt_80" MaxLength="100"></asp:TextBox>
                                            <asp:TextBox ID="TextBox3" runat="server" SkinID="txt_80" Visible="false" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter password"
                                                ControlToValidate="txtCasulaPas" Display="None" ValidationGroup="valSubmit"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter password 
                                                20 characters" Display="None" ValidationGroup="valSubmit" ControlToValidate="txtCasulaPas" ValidationExpression="[a-zA-Z0-9'@&amp;_#.\s]{6,20}"></asp:RegularExpressionValidator>--%>
                                             <asp:RegularExpressionValidator ID="Regex3" runat="server" ValidationGroup="valSubmit" ControlToValidate="txtCasulaPas"
ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
ErrorMessage="Please enter password minimum 8 characters at least 1 upperCase alphabet, 1 lowerCase alphabet and 1 number" />
                                        </div>
                                    </div>
                                        </div>
                                     <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Confirm&nbsp;Password<span style="color: #ff0000">*</span></label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtCasulaPasc" runat="server" TextMode="Password" SkinID="txt_80" MaxLength="100"></asp:TextBox>
                                            
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please enter confirm password"
                                        ControlToValidate="txtCasulaPas" Display="None" ValidationGroup="valSubmit">
                                    </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" Display="None"
                                                ErrorMessage="Passwords do not match" ValidationGroup="valSubmit" ControlToCompare="txtCasulaPas"
                                                ControlToValidate="txtCasulaPasc"></asp:CompareValidator></div>
                                    </div>
                                         </div>
                                </asp:Panel>
                                <div class="form-group row">
                               <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">Email<span style="color: #ff0000">*</span>
                                    </label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtCasualEmail" runat="server" SkinID="txt_90" ValidationGroup="valSubmit" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox></div>
                               </div>
                                    </div>
                                <div class="form-group row">
                                <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">Status
                                    </label>
                                     <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="ddlCasualLab" runat="server" SkinID="ddl_80">
                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                            <asp:ListItem Value="InActive">Inactive</asp:ListItem>
                                        </asp:DropDownList></div>

                                </div>
                                    </div>
                            </asp:Panel>

                                    <asp:Panel ID="GetUser" runat="server">
                                         <div class="form-group row">
                                <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">Username<span style="color: #ff0000">*</span></label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtusername" runat="server" SkinID="txt_80" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqUser" runat="server" ErrorMessage="Please enter user name"
                                            ControlToValidate="txtusername" Display="None" ValidationGroup="valSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                    </div>
                                </div>
                                         <div class="form-group row">
                                 <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">New&nbsp;Password<span style="color: #ff0000">*</span></label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="txt_80" MaxLength="100"></asp:TextBox>
                                        <asp:TextBox ID="TextBox1" runat="server" SkinID="txt_80" Visible="false" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter password"
                                            ControlToValidate="txtPassword" Display="None" ValidationGroup="valSubmit"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="valSubmit" ControlToValidate="txtCasulaPas"
ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
ErrorMessage="Please enter password minimum 8 characters at least 1 upperCase alphabet, 1 lowerCase alphabet and 1 number" Display="None" />

                                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter password 6 to 20 characters" Display="None" ValidationGroup="valSubmit" ControlToValidate="txtPassword" ValidationExpression="^[a-zA-Z0-9'@&amp;#_.\s]{6,20}$"></asp:RegularExpressionValidator>--%>
                                       
                                        <asp:CheckBox ID="chkReset" runat="server" Text="Reset Password" Visible="false" />
                                    </div>
                                </div>
                                             </div>
                                        <div class="form-group row">
                                <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">Confirm&nbsp;Password<span style="color: #ff0000">*</span></label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" SkinID="txt_80" MaxLength="100"></asp:TextBox>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter confirm password"
                                            ControlToValidate="txtConfirmPassword" Display="None" ValidationGroup="valSubmit">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None"
                                            ErrorMessage="Passwords do not match" ValidationGroup="valSubmit" ControlToCompare="txtPassword"
                                            ControlToValidate="txtConfirmPassword"></asp:CompareValidator></div>
                                </div>
                                            </div>
                                         <div class="form-group row">
                                 <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline">Email<span style="color: #ff0000">*</span>
                                    </label>
                                   <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="50" AutoCompleteType="Disabled"></asp:TextBox></div>
                                </div>
                                             </div>
                                         <div class="form-group row">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Contact Number</label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtContactNumber" runat="server" SkinID="txt_80" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                        </div>
                                        <div class="form-group row">

                              <div class="col-md-12">
                                     <label class="col-sm-4 control-label form-inline">Status
                                    </label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="drStatus" runat="server" SkinID="ddl_80">
                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                            <asp:ListItem Value="InActive">Inactive</asp:ListItem>
                                        </asp:DropDownList></div>

                                </div>
                                            </div>
                                         <div class="form-group row" style="display:none;visibility:hidden;">
                              <div class="col-md-12">
                                         <div id="div_expand" style="cursor: pointer;font-weight:bold;">Expand for Additional Information</div>
                                  </div>
                                             </div>
                                        <div class="form-group expand_fields">
                                  <div class="col-md-12">
                                    <label class="col-sm-4 control-label form-inline"></label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:CheckBox ID="chk_disable_customerportal" runat="server" Text="Disable Customer Portal Access" /></div>
                                </div>
                                            </div>
                                <asp:Panel ID="GetCustomer" runat="server" Visible="true">
                                   
                                     <div class="form-group expand_fields">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Company</label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddlCompany" runat="server" SkinID="ddl_80">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCompany" runat="server" Visible="False" SkinID="txt_80"></asp:TextBox>
                                            <asp:LinkButton ID="btnAddCompany" runat="server" OnClick="btnAddCompany_Click"
                                                SkinID="BtnLinkAdd" CausesValidation="False" /></div>
                                    </div>
                                         </div>

                                     <div class="form-group row" style="display:none;visibility:hidden;">
                                        <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Timesheet approver</label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddrTimesheetApprove" runat="server" SkinID="ddl_80">
                                            </asp:DropDownList></div>

                                   </div>
                                         </div>
                                     <div class="form-group row"  style="display:none;visibility:hidden;">
                                   <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Secondary Timesheet Approver</label>
                                       <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="timesheetsecondapprover" runat="server" SkinID="ddl_80">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                         </div>
                                    <div class="form-group row"  style="display:none;visibility:hidden;">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Department</label>
                                       <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" SkinID="ddl_80" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:LinkButton ID="BtnAddDepartment" runat="server" SkinID="BtnLinkAdd"  />

                                       </div>

                                   </div>
                                        </div>
                                     <div class="form-group row"  style="display:none;visibility:hidden;">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Area</label>
                                       <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddlArea" runat="server" SkinID="ddl_80">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="imgBtnArea" runat="server" SkinID="BtnLinkAdd" OnClick="imgBtnArea_Click" ValidationGroup="grp_dep" />
                                            <asp:LinkButton ID="hid_area" runat="server" SkinID="ImgAdd" Style="display: none" />
                                        </div>
                                   </div>
                                         </div>
                                    <div class="form-group expand_fields">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Employment Start Date</label>
                                         <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtStartDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                            <asp:Label ID="imgStartDate" runat="server" SkinID="Calender" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                                 CssClass="MyCalendar" PopupButtonID="imgStartDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                   </div>
                                        </div>
                                   <div class="form-group expand_fields">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Release Date</label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtReleaseDate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox>
                                            <asp:Label ID="imgReleaseDate" runat="server" SkinID="Calender" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReleaseDate"
                                                 CssClass="MyCalendar" PopupButtonID="imgReleaseDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                   </div>
                                       </div>
                                     <div class="form-group expand_fields">
                                    <div class="col-md-12">
                                        <label class="col-sm-4 control-label form-inline">Experience Classification
                                        </label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddlExperienceClassification" runat="server" SkinID="ddl_80">
                                            </asp:DropDownList></div>
                                    </div>
                                         </div>

                                     <div class="form-group expand_fields">
                                    <div class="col-md-12">
                                       <label class="col-sm-4 control-label form-inline">Details/Qualifications
                                        </label>
                                        <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtDetails" runat="server" SkinID="txtMulti"></asp:TextBox></div>
                                   </div>
                                         </div>
                                    <div class="form-group expand_fields">
                                    <div class="col-md-12">
                                         <label class="col-sm-4 control-label form-inline"></label>

                                       <div class="col-sm-8 form-inline">
                                            <asp:CheckBox ID="chkFinance" runat="server" Checked="true" />Show Costs in Financial Actuals</div>
                                    </div>
                                        </div>
                                </asp:Panel>
                            </asp:Panel>
                                     

                                    <div class="form-group row">
                                    <div class="col-md-12 form-inline col-md-offset-4">
                                        <asp:Button  ID="btnSubmitt" runat="server" OnClick="imgsubmitt_Click"
                                            SkinID="btnSubmit" 
                                            ValidationGroup="valSubmit" />
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                            OnClick="btnCancel_Click" SkinID="btnCancel" />
                                         <asp:HiddenField ID="getUserId" runat="server" />
                            <asp:TextBox ID="txtsid" runat="server" Visible="False"
                                Width="250px"></asp:TextBox>
                                        </div>
                              </div>

                                    </div>
                                  <div class="col-md-4">
                                    <div>

                                        <asp:Panel ID="panelUser" runat="server" BackColor="White"
                                            Width="110px" Height="110px" BorderStyle="Solid" BorderWidth="1" BorderColor="LightSteelBlue">
                                            <asp:HyperLink ID="imgUser" runat="server" Visible="false"></asp:HyperLink>
                                            <%--<asp:Image ID="imgUser" runat="server" Visible="false"  />--%>
                                        </asp:Panel>
                                    </div>
                                      <div class="form-group row">
             <div class="col-md-12">
                                      <asp:FileUpload ID="Fileupload_user" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                            runat="server" ControlToValidate="Fileupload_user" Display="None"
                                            ErrorMessage=""
                                            ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                                            ValidationGroup="Group_upload">File</asp:RegularExpressionValidator>
					</div>
                                           </div>
                                      <div class="form-group row">
                                          <div class="col-md-12">
                                              <asp:Button ID="btnImgUpload" runat="server" SkinID="btnUpload"
                                            ValidationGroup="Group_upload" OnClick="btnImgUpload_Click" />
                                              </div>
                                          
				</div>
               
                                     <div class="form-group expand_fields">
             <div class="col-md-12">
                 <label><b>Delegate Responsibility </b></label>
                 <hr class="no-top-margin" />
                 </div>
                                         </div>
               
                                     <div class="form-group expand_fields">
                                          <div class="col-md-12">
                                               <asp:DropDownList ID="ddlUser_respos" runat="server" SkinID="ddl_80">
                                            </asp:DropDownList>
                                              </div>
                                         </div>
               
                                     <div class="form-group expand_fields">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnChange_response" runat="server" SkinID="btnSubmit"
                                                OnClick="btnChange_response_Click" CausesValidation="false" />
                                              </div>
                                         </div>
                                  
                                     
                                    <div class="form-group no-left-padding expand_fields">
                                    <div class="col-md-12">
                                            <label class="col-sm-4 control-label form-inline">Gender</label>
                                        <div class="col-sm-8 form-inline">
                                        <asp:RadioButtonList ID="rbGender" runat="server"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Male</asp:ListItem>
                                            <asp:ListItem Value="1">Female</asp:ListItem>
                                        </asp:RadioButtonList>
                                            </div>
                                     </div>
                                      </div>
                                            <div class="form-group no-left-padding expand_fields">
                                    <div class="col-md-12">
                                               <label class="col-sm-4 control-label form-inline">
                                                    Date of Birth</label>
                                                 <div class="col-sm-8 form-inline">
                                                    <asp:TextBox ID="txtDOB" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                                    <asp:Label ID="imgDOB" runat="server"  SkinID="Calender" />
                                                    <ajaxToolkit:CalendarExtender ID="calDOB" runat="server" CssClass="MyCalendar"
                                                         PopupButtonID="imgDOB" TargetControlID="txtDOB">
                                                    </ajaxToolkit:CalendarExtender>
                                                </div>
                                           </div>
                                                </div>
                                           <div class="form-group no-left-padding expand_fields">
                                    <div class="col-md-12">
                                                <label class="col-sm-4 control-label form-inline">City
                                                </label>
                                                 <div class="col-sm-8 form-inline">
                                                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                              </div>
                                            <div class="form-group no-left-padding expand_fields">
                                    <div class="col-md-12">
                                               <label class="col-sm-4 control-label form-inline">Country</label>
                                                <div class="col-sm-8 form-inline">
                                                    <asp:DropDownList ID="ddlCountry1" runat="server" SkinID="ddl_80">
                                                    </asp:DropDownList></div>
                                            </div>
                                                </div>
                                            <div class="form-group no-left-padding expand_fields">
                                    <div class="col-md-12">
                                                <label class="col-sm-4 control-label form-inline">
                                                Default Customer Site
                                               </label>
                                               <div class="col-sm-8 form-inline">
                                                    <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                    <ajaxToolkit:CascadingDropDown ID="ccdSite" runat="server" TargetControlID="ddlSite"
                                                        Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                                        ServiceMethod="GetOurSite" LoadingText="[Loading site...]" />
                                                </div>
                                           </div>
                                               
                                                </div>
                                       <div class="form-group no-left-padding expand_fields">
                                        <div class="col-md-12">
                                                <label class="col-sm-4 control-label form-inline">
                                                Postcode
                                               </label>
                                               <div class="col-sm-8 form-inline">
                                                   <asp:TextBox ID="txtPostcode" runat="server" SkinID="Date" MaxLength="20"></asp:TextBox>
                                                   </div>
                                                     </div>
                                           </div>
                                       <div class="form-group no-left-padding expand_fields">
                                                 <div class="col-md-12">
                                                <label class="col-sm-4 control-label form-inline">
                                                Expertise Type
                                               </label>
                                               <div class="col-sm-8 form-inline">
                                                    <asp:Panel ID="pnlSkill" runat="server" Width="220px" BorderColor="Silver" BorderWidth="1px"
                            Height="100px" ScrollBars="Auto">
                            <asp:CheckBoxList ID="chkExpert" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                                                <%--   <asp:DropDownList ID="ddlExpert" runat="server" SkinID="ddl_80" ClientIDMode="Static">
                                                    </asp:DropDownList>
                                                   <ajaxToolkit:CascadingDropDown ID="ccdExperts" runat="server" TargetControlID="ddlExpert"
                                                        Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                                        ServiceMethod="GetAssetType" LoadingText="[Loading ...]" />--%>
                                                   </div>
                                                     </div>
                                           </div>
                                      
                                    </div>
                                 </div>

                        <div>
                            <ajaxToolkit:ModalPopupExtender ID="mdlDepartment" runat="server" CancelControlID="btnModelDepartmentCancel"
                                BackgroundCssClass="modalBackground" TargetControlID="BtnAddDepartment" PopupControlID="pnlDepartment" />
                            <asp:Panel ID="pnlDepartment" runat="server" BackColor="White"
                                Style="display: none" Width="230px" BorderStyle="Double" BorderColor="LightSteelBlue">
                                <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 96%;">
                                    Department
                                </div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtModelDepartment" runat="server" Width="210px"></asp:TextBox>
                                            <asp:HiddenField ID="H_Department" runat="server" Value="0" />
                                            <asp:Label ID="lblMsgDepartment" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtModelDepartment"
                                                ErrorMessage="Please enter department" ForeColor="Red" ValidationGroup="Group_Department"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnModelDepartmentInsert" runat="server" Text="OK" OnClick="btnModelDepartmentInsert_Click" SkinID="btnSubmit"
                                                ValidationGroup="Group_Department" />
                                            <asp:Button ID="btnModelDepartmentCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                        </td>

                                    </tr>
                                </table>

                            </asp:Panel>
                        </div>


                    </asp:Panel>

                </div>



    <script>

       

    </script>

   
</asp:Content>
