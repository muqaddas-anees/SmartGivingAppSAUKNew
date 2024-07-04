<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
     Inherits="ProjectGroups" Title="System Defaults" Codebehind="SystemDefaults.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
Admin
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.SystemDefaults%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group row">
<asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
</div>

     <div class="form-group row">
         <asp:ValidationSummary ID="validasumm" runat="server" DisplayMode="List" ValidationGroup="validasumm" ForeColor="Red" />  
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="
<%$ Resources:DeffinityRes,Plsentervalidemailsuffix%>" ControlToValidate="txtEmailSuffix" ValidationExpression="@\w+([-.]\w+)*\.\w+([-.]\w+)*">&#160;</asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtenailaddress"
        ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidemail%>" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">&#160;</asp:RegularExpressionValidator>
   
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Plsenterapplicationname%>"
        ControlToValidate="txtApplicationName" Display="None" ValidationGroup="validasumm"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
             ControlToValidate="txtApproveBoardEmail" Display="None" 
             ErrorMessage="<%$ Resources:DeffinityRes,PlsentervalidApprovalboardemail%>" 
             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
             ValidationGroup="validasumm"></asp:RegularExpressionValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
             ControlToValidate="txtFromEmailAddeess" Display="None" 
             ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidfromemail%> " 
             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
             ValidationGroup="validasumm"></asp:RegularExpressionValidator>
         </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.ProjectOwner%> <span style="color: #ff0000">*<font color="#898989"></font></span></label>
                                      <div class="col-sm-5"><asp:DropDownList ID="ddlprojectdefaults" runat="server" SkinID="ddl_90"></asp:DropDownList> 
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.JobDescription%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtjobdescription" runat="server" SkinID="txt_90"></asp:TextBox> 
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Telephone%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txttelephone" runat="server" 
        SkinID="txt_90" MaxLength="20"></asp:TextBox> 
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Mobile%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtmobile" runat="server" 
        SkinID="txt_90" MaxLength="20"></asp:TextBox>
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.EmailAddress%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtenailaddress" 
         runat="server" SkinID="txt_90"></asp:TextBox> 
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ProjectPrefix%> </label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtProjectPrefix" runat="server" SkinID="txt_90" MaxLength="3"></asp:TextBox>
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.EmailSuffix%> </label>
                                      <div class="col-sm-5 form-inline"><asp:TextBox ID="txtEmailSuffix" runat="server" 
         SkinID="txt_90"></asp:TextBox>
                                          <br />
                                          <%= Resources.DeffinityRes.ExAtdeffinitydotcom%>
					</div>
                                      <div class="col-sm-5">
                                           
                                      </div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.URL%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtURL" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.CUSTOM1%>  </label>
                                      <div class="col-sm-5"><asp:TextBox ID ="txtCustom1" runat="server" SkinID="txt_90" ></asp:TextBox>
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.CUSTOM2%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="txtCustom2" runat="server" SkinID="txt_90" />
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.ApplicationName%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtApplicationName" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.FromEmailAddress%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtFromEmailAddeess" runat="server" SkinID="txt_90"></asp:TextBox>
					</div>
				</div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.FinanceDistributionEmail%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtFinanceDistributionEmail" runat="server" SkinID="txt_90" 
                MaxLength="100"></asp:TextBox>
					</div>
				</div>
        </div>
    <asp:Panel runat="server" ID="ProjPanel" Visible="false">
     <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.NumberofProjects%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtProjects" runat="server" SkinID="txt_50"></asp:TextBox>
					</div>
				</div>
        </div>
        
</asp:Panel>
     
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.VATRATEPercentage%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtVAT" runat="server" SkinID="Price_100px" MaxLength="6"></asp:TextBox>
      <asp:RangeValidator ID="valRange" runat="server" Display="None" ErrorMessage="Enter VAT Percentage less than 100 "
      ControlToValidate="txtVAT" ValidationGroup="validasumm"  Type="Double"  MinimumValue="0" MaximumValue="100" ></asp:RangeValidator>
					</div>
				</div>
        </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.EnableJournal%></label>
                                      <div class="col-sm-5"> <asp:CheckBox ID="chk_enable" runat="server" />
					</div>
				</div>
        </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> Language</label>
                                      <div class="col-sm-5"> <asp:DropDownList ID="ddlCulture" runat="server" SkinID="ddl_90">
     <asp:ListItem Text="Dutch" Value="nl-NL"></asp:ListItem>
     <asp:ListItem Text="English" Value="en-GB"></asp:ListItem>
     <asp:ListItem Text="French" Value="fr-FR"></asp:ListItem>     
     <asp:ListItem Text="German" Value="de-DE"></asp:ListItem>
     <asp:ListItem Text="Greek" Value="el-GR"></asp:ListItem>
     <asp:ListItem Text="Portuguese" Value="pt-PT"></asp:ListItem>
     <asp:ListItem Text="Swedish" Value="sv-SE"></asp:ListItem>
<asp:ListItem Text="Turkey" Value="tr"></asp:ListItem>
     </asp:DropDownList> 
					</div>
				</div>
        </div>
    <div class="form-group row" style="display:none;">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.DefaultProjectCurrency%></label>
                                      <div class="col-sm-5"><asp:DropDownList ID="ddlDefaultProjectCurrency" runat="server" SkinID="ddl_90"></asp:DropDownList> <asp:LinkButton id="lnkCurrencyConversion" runat="server" OnClick="lnkCurrencyConversion_Click" Visible="false"><%= Resources.DeffinityRes.ManageCurrencyConversion%></asp:LinkButton>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDefaultProjectCurrency"
                    ErrorMessage="<%$ Resources:DeffinityRes,Plsselectcurrency%>" InitialValue="Select..." Display="None" ValidationGroup="validasumm"></asp:RequiredFieldValidator>
					</div>
				</div>
        </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ApprovalBoardEmailAddress%></label>
                                      <div class="col-sm-5 form-inline"> <asp:TextBox ID="txtApproveBoardEmail" runat="server" SkinID="txt_90"
                MaxLength="100"></asp:TextBox><br />&nbsp;  <%= Resources.DeffinityRes.EmailAddUsed_PrjPropsalSmbt%>
					</div>
				</div>
        </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                      <div class="col-md-2">
                                          <%= Resources.DeffinityRes.AnnualLeavePeriod%>
                                          </div>
                                        <div class="col-md-3">
                                       <label class="col-sm-5 control-label form-inline"> <%= Resources.DeffinityRes.StartDate%></label>
                                      <div class="col-sm-7 form-inline">
                                          <asp:TextBox ID="txtannualstart" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label ID="Img1" runat="server" SkinID="Calender" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtannualstart"
                Display="Dynamic" ValidationGroup="validasumm" ErrorMessage="<%$ Resources:DeffinityRes,PlsentertheAnnualyearstart%>"
                Text="*" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                 PopupButtonID="img1" TargetControlID="txtannualstart" Enabled="True">
            </ajaxToolkit:CalendarExtender>
					</div>
				</div>
 <div class="col-md-3">
                                       <label class="col-sm-5 control-label form-inline"> <%= Resources.DeffinityRes.EndDate%></label>
                                      <div class="col-sm-7 form-inline">
                                          <asp:TextBox ID="txtannualend" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label ID="Img2" runat="server" SkinID="Calender" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtannualend"
                Display="Dynamic" ValidationGroup="validasumm" ErrorMessage="<%$ Resources:DeffinityRes,PlsentertheAnnualyearend%>"
                Text="*" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                 PopupButtonID="img2" TargetControlID="txtannualend" Enabled="True">
            </ajaxToolkit:CalendarExtender>
            <asp:CompareValidator ID="Cmpdates" runat="server" ControlToValidate="txtannualstart"
                ControlToCompare="txtannualend" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,StartDateshldbegrthanEndDate%>"
                Operator="LessThanEqual" Type="Date" ValidationGroup="validasumm" />
               
				</div>
				</div>
                                      <div class="col-md-2">
                                          <asp:Button ID="imgbtnApplyToall" runat="server" Text="Copy Previous Annual Leave To All Users"
                 onclick="imgbtnApplyToall_Click" /> 
                                          </div>
        </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                      <label class="col-sm-2 control-label"></label>
                                       <div class="col-sm-7" style="padding-left:33px;">
                                           <%= Resources.DeffinityRes.Annulleaveappliedfrthisprdeachyr%>  
                                       </div>
					</div>
                                      </div>
         </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                        <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.FinancialYear %>
                                          </label>
                                      <div class="col-md-3">
                                       <label class="col-sm-5 control-label form-inline"> <%= Resources.DeffinityRes.StartDate%> </label>
                                      <div class="col-sm-7 form-inline"> <asp:TextBox ID="txtFinancialFromDate" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label ID="ImgAnnualFromDate" runat="server" SkinID="Calender" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFinancialFromDate"
                Display="Dynamic" ValidationGroup="validasumm" ErrorMessage="<%$ Resources:DeffinityRes,PlsentertheAnnualyearstart%>"
                Text="*" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
                 PopupButtonID="ImgAnnualFromDate" TargetControlID="txtFinancialFromDate" Enabled="True">
            </ajaxToolkit:CalendarExtender>
					</div>
				</div>
 <div class="col-md-3">
                                       <label class="col-sm-5 control-label form-inline"> <%= Resources.DeffinityRes.EndDate%></label>
                                      <div class="col-sm-7 form-inline"> <asp:TextBox ID="txtFinancialToDate" runat="server" SkinID="Date"></asp:TextBox>
            <asp:Label ID="ImgAnnualToDate" runat="server" SkinID="Calender" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFinancialToDate"
                Display="Dynamic" ValidationGroup="validasumm" ErrorMessage="<%$ Resources:DeffinityRes,PlsentertheAnnualyearend%>"
                Text="*" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="MyCalendar"
                 PopupButtonID="ImgAnnualToDate" TargetControlID="txtFinancialToDate" Enabled="True">
            </ajaxToolkit:CalendarExtender>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFinancialFromDate"
                ControlToCompare="txtFinancialToDate" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,StartDateshldbegrthanEndDate%>"
                Operator="LessThanEqual" Type="Date" ValidationGroup="validasumm" />
					</div>
				</div>
				</div>
        </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Allow Project Managers to Cancel Projects</label>
                                      <div class="col-sm-5"> <asp:CheckBox ID="chkEnablePM" runat="server" />
					</div>
				</div>
        </div>
    <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Make Programme Selection Mandatory &nbsp;&nbsp;</label>
                                      <div class="col-sm-5"> <asp:CheckBox ID="chkEnablePrgSele" runat="server" />
					</div>
				</div>
        </div>

    <div class="form-group row">
         <div class="col-md-12">
        <label class="col-sm-2 control-label">Set Internal PO Number
            </label>
                                  <div class="col-sm-8">
                                       <div class="col-sm-4 control-label form-inline">PO Number Prefix:<asp:TextBox ID="txtPOPrefix" runat="server" MaxLength="5" SkinID="txt_75px"></asp:TextBox></div>
                                      <div class="col-sm-4 control-label form-inline">PO Starting Point:<asp:TextBox ID="txtPOStartPoint" runat="server" MaxLength="5" SkinID="txt_75px"></asp:TextBox>
					</div>
                                <div class="col-sm-4 control-label form-inline">Increment by:<asp:TextBox ID="txtIncBy" runat="server" MaxLength="5" SkinID="txt_75px"></asp:TextBox>   </div>
				</div>
 </div>
</div>
    <div class="form-group row">
         <div class="col-md-12">
              <label class="col-md-2 control-label">
            </label>
             <div class="col-md-5">
    <asp:Button ID="imgbtnsave" runat="server" OnClick="imgbtnsave_Click" 
         ValidationGroup="validasumm" 
        SkinID="btnUpdate" /> 
                 </div>
             </div>
        </div>
    
         
</asp:Content>

