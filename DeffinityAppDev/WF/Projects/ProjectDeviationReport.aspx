<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="deviationreport" Title="Variation Repoert" Codebehind="ProjectDeviationReport.aspx.cs" %>

<%--<%@ Register Src="controls/ResourceTabs.ascx" TagName="updateTabs" TagPrefix="uc2" %>--%>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%--<%@ Register Src="controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc3" %>--%>
<%----%>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:BuildProjectTabs ID="project_tabs" runat="server" />
   <%-- <uc2:updateTabs ID="resource_tabs" runat="server" Visible="false" />--%>
   <%-- <uc3:OpsViewTabs ID="checkpoint_tabs" runat="server" Visible="false" />--%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.VarianceReport%> - <Pref:ProjectRef ID="ProjectRef2" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                            <asp:Label ID="lblerror1" runat="server" ForeColor="Red" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtIndirectCost"
                                Display="None" ErrorMessage="Please enter valid Cost of Variation" Type="Double"
                                ValidationGroup="Group1" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtvariation"
                                Display="None" ErrorMessage="Please enter valid Variation Value" Type="Double"
                                ValidationGroup="Group1" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtvariation"
                                Display="None" ErrorMessage="Please enter Variation Value" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRequesterName"
                                Display="None" ErrorMessage="Please enter requester name" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemediationDate"
                                Display="None" ErrorMessage="Please enter expected remediation date" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid date in date field"
                                ControlToValidate="txtRemediationDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                Display="None" ValidationGroup="Group1"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="REF" runat="server" ValidationGroup="Group1"
                                Display="None" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ErrorMessage="Please enter valid email"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtApproveemail"
                                Display="None" ErrorMessage="Please enter valid email in approvers email  " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="Group1"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtApproveemail"
                                Display="None" ErrorMessage="Please enter approvers email" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtvariationFC"
                                Display="None" ErrorMessage="Please enter variation forecast" Operator="DataTypeCheck"
                                Type="Double" ValidationGroup="Group1"></asp:CompareValidator>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIndirectCost"
                Display="None" ErrorMessage="Please enter indirect cost" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                Display="None" ErrorMessage="Please enter email" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtApproversName"
                                Display="None" ErrorMessage="Please enter approvers name" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtvariationFC"
                Display="None" ErrorMessage="Please enter variation forecast" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtIndirectCost"
                                Display="None" ErrorMessage="Please enter Cost of Variation" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtenddate"
                                Display="None" ErrorMessage="Please enter end date" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDesctiption"
                                Display="None" ErrorMessage="Please enter description" ValidationGroup="Group1"></asp:RequiredFieldValidator>
    </div>

    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label"> Requester's name  <span class="alert">*</span></label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtRequesterName" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label"> Project name</label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txtProjectName" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
</div>

    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label form-inline">  Requesters email adress<span class="alert">*</span></label>
                                      <div class="col-sm-7"> <asp:TextBox ID="txtEmail" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label"> Expected remediation&nbsp;date</label>
                                      <div class="col-sm-7 form-inline">
                                           <asp:TextBox ID="txtRemediationDate" runat="server"  SkinID="Date" MaxLength="10"></asp:TextBox>
                            <asp:Label ID="Image1" runat="server" SkinID="Calender" />
					</div>
				</div>
</div>

    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label form-inline">Variation value<span style="color: Red">*</span></label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtvariation" runat="server" ValidationGroup="Group1" SkinID="Price" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Cost of variation<span class="alert">*</span></label>
                                      <div class="col-sm-7">
                                          <asp:TextBox ID="txtIndirectCost" runat="server" MaxLength="15" ValidationGroup="Group1"
                                SkinID="Price"></asp:TextBox>
					</div>
				</div>
</div>

    <div class="form-group" style="display: none;">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Forecasted value at completion</label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtvariationFC" runat="server" SkinID="Price" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Forecast cost at completion</label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtForecastCost" runat="server" SkinID="Price" />
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Telephone</label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Project location</label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtProjectLocation" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Customer Instruction Number</label>
                                      <div class="col-sm-7"> <asp:TextBox ID="txtCustomerInstructionNumber" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Project Manager</label>
                                      <div class="col-sm-7"><asp:TextBox ID="txtCRSProjectManager" runat="server" Width="250px"></asp:TextBox>
                                           <div style="display: none;">
                            Mobile number
                        </div>
                        <div style="display: none;">
                            <asp:TextBox ID="txtMobile" runat="server" Width="140px"></asp:TextBox>
                        </div>
					</div>
				</div>
</div>
      <div style="display: none;">
                        <div>
                            Business head
                        </div>
                        <div>
                            <asp:TextBox ID="txtBusinessHead" runat="server" Width="250px"></asp:TextBox>
                        </div>
                        <div>
                            Business group
                        </div>
                        <div>
                            &nbsp;<asp:TextBox ID="txtBusinessGroup" runat="server" Width="250px"></asp:TextBox>
                        </div>
                    </div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Status</label>
                                      <div class="col-sm-7">
                                           <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="" Text="Please select..."> </asp:ListItem>
                                <asp:ListItem Value="Live" Text="Live"> </asp:ListItem>
                                <asp:ListItem Value="In Progress" Text="In Progress"> </asp:ListItem>
                                <asp:ListItem Value="Awaiting Instruction" Text="Awaiting Instruction"> </asp:ListItem>
                            </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-5 control-label">Percentage Complete (%)</label>
                                      <div class="col-sm-7">
                                           <asp:TextBox ID="txtPercentageComplete" runat="server" MaxLength="3" SkinID="Price"
                                Width="70px"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Please enter a valid percentage complete."
                                ControlToValidate="txtPercentageComplete" Type="Integer" Operator="DataTypeCheck"
                                ValidationGroup="Group1">*</asp:CompareValidator>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label form-inline">Description<span class="alert">*</span></label>
                                       <div class="col-md-9">
                                      <asp:TextBox ID="txtDesctiption" runat="server" TextMode="MultiLine" SkinID="txtMulti" 
                                MaxLength="500"></asp:TextBox>
                                          </div>
				</div>
        </div>
         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label form-inline"> Scope</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtScope" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox></div>
                                      </div>
             </div>
         <div style="display: none;">
                        <div>
                            Detailed&nbsp;Explanation&nbsp;&nbsp;for&nbsp;the&nbsp;Request
                        </div>
                        <div>
                            <asp:TextBox ID="txtDetailedExplanation" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
                        </div>
                    </div>
         <div style="display: none;">
                        <div>
                            Justification&nbsp;Financial&nbsp;impact
                        </div>
                        <div>
                            <asp:TextBox ID="txtJustification" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
                        </div>
                    </div>
        <div style="display: none;">
                        <div>
                            Proposed Compensation controls, Financial Impact and Rollback plan
                        </div>
                        <div>
                            <asp:TextBox ID="txtProposedCompensation" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
                        </div>
                    </div>
       

 <asp:Panel runat="server" ID="Panel_PM" BorderWidth="1" BorderColor="YellowGreen"
                            Width="100%" Visible="false" CssClass="form-group">
      <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> Approvers name<span class="alert">*</span></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtApproversName" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
      <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  Approvers email<span class="alert">*</span></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtApproveemail" runat="server" Width="250px"></asp:TextBox>
					</div>
				</div>
                           
                        </asp:Panel>
    <div class="form-group">
             <div class="col-md-12">
                                       <div class="col-sm-4 control-label"> <asp:Button ID="btnSubmitAndApprove" runat="server" SkinID="btnDefault" Text="Submit to manager for approval"
                                ValidationGroup="Group1" OnClick="btnSubmitAndApprove_Click"  /></div>
                                      <div class="col-sm-8 form-inline vcenter">
                                            &nbsp;Your variation will be approved by:&nbsp;
                            <asp:ListBox ID="lstManager" runat="server" Width="230px"></asp:ListBox>
					</div>
				</div>
     </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <asp:Button ID="btnSubmitAndEmail" runat="server" SkinID="btnDefault" Text="Submit and Email the Customer for Approval"
                                ValidationGroup="Group1" OnClick="btnSubmitAndEmail_Click" /></label>
                                      <div class="col-sm-8 form-inline control-label">
                                           &nbsp;Your variation will be approved by:&nbsp;
                            <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_50">
                            </asp:DropDownList>
					</div>
				</div>
     </div>
    <div class="form-group">
             <div class="col-md-12">
                 <label class="col-sm-4 control-label"> <asp:Button ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"
                                SkinID="btnCancel" /></label>
                 <div class="col-sm-8">
                    
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                TargetControlID="txtRemediationDate" PopupButtonID="Image1" >
                            </ajaxToolkit:CalendarExtender>
                      <asp:HiddenField ID="HiddenField1" runat="server" />
                     </div>
                 </div>
        </div>
    <script type="text/javascript">
        activeTab('Project Tracker');
    </script>
</asp:Content>
