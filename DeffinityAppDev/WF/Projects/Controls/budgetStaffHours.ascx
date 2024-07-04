<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_budgetStaffHours" Codebehind="budgetStaffHours.ascx.cs" %>
<%@ Register Src="~/WF/Projects/MailControls/ResourceTimesheetAlert.ascx" TagName="MailControl"
    TagPrefix="uc2" %>
<style>
    .round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 300px;
        border-radius: 8px;
    }
</style>
<uc2:MailControl ID="ResourceTimesheetAler1" runat="server" Visible="false" />


<div>
    <div class="form-group">
          <div class="col-md-3 well" style="padding-top:10px;">
             <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Original%></label>
           <div class="col-sm-6" style="text-align: right;">
                  <asp:Label ID="lblOriginal" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
             <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Forecast%></label>
          <div class="col-sm-6" style="text-align: right;">
               <asp:Label ID="lblForecast" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
             <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.Actual1%></label>
           <div class="col-sm-6" style="text-align: right;">
                 <asp:Label ID="lblActual" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
             <div class="form-group">
          <div class="col-md-12">
          <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.CostRemaining%></label>
           <div class="col-sm-6" style="text-align: right;">
                <asp:Label ID="lblCostRemaining" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
    </div>
     </div>
     <div class="form-group">
         <div class="col-md-5 well">
             <div class="form-group">
      <div class="col-md-6">
            
	</div>
	<div class="col-md-3">
         <p class="text-right" style="font-weight:bold;"><%= Resources.DeffinityRes.Hours%></p>
	</div>
	<div class="col-md-3">
          <p class="text-right" style="font-weight:bold;"><%= Resources.DeffinityRes.Cost%></p>
	</div>
                
</div>
           <hr />   
             <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.ForecastedHours%></label>
	</div>
	<div class="col-md-3">
           <div class="col-sm-12" style="text-align: right;">
                <asp:Label ID="lblOriginalPMHoursQuotedUnit" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
	<div class="col-md-3">
           <div class="col-sm-12" style="text-align: right;">
                 <asp:Label ID="lblOriginalPMHoursQuotedTotal" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
             <div class="form-group" style="display:none;">
      <div class="col-md-4">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.VariationstoPMHoursApproved%></label>
	</div>
	<div class="col-md-4">
           <div class="col-sm-12" style="text-align: right;">
                  <asp:Label ID="lblVariationPMHoursQuotedUnit" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
	<div class="col-md-4">
           <div class="col-sm-12" style="text-align: right;">
                 <asp:Label ID="lblVariationPMHoursQuotedTotal" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
             <div class="form-group" style="display:none;">
      <div class="col-md-4">
           <label class="col-sm-12 control-label"><%= Resources.DeffinityRes.BudgetedHours%></label>
	</div>
	<div class="col-md-4">
            <div class="col-sm-12" style="text-align: right;">
                  <asp:Label ID="LblHours" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
	<div class="col-md-4">
           <div class="col-sm-12" style="text-align: right;">
                    <asp:Label ID="LblCost" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
         </div>
      </div>
</div>






<asp:Panel ID="pnlBreakdownHours" runat="server" Visible="false">
<div style="width: 50%; float: right;">
    <div class="sec_header">
        <%= Resources.DeffinityRes.AdditionalStaffHours %> </div>
    <br />
    <asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="b" />
    <asp:HiddenField ID="hfVariationID" runat="server" Value="0" />
    <table>
        <tr>
            
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" Width="140">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUser"
                    InitialValue="0" ErrorMessage="Please select user" ValidationGroup="b">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">
               <%= Resources.DeffinityRes.AdditionalHours %>
                <asp:TextBox ID="txtAdditionalHours" runat="server" Width="50px" SkinID="Price" Text="0:00"></asp:TextBox><span
                    style="color: Gray">(HH:MM)</span>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtAdditionalHours"
                    ErrorMessage="Please enter additional hours " ValidationGroup="b">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regex122" runat="server" ControlToValidate="txtAdditionalHours"
                    ValidationExpression="^((-?\d+):([0-5][0-9]))$" ValidationGroup="b" SetFocusOnError="True"
                    Display="None" Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
            </td>
            </tr>
        <tr>
            <td>
                &nbsp;&nbsp;  <%= Resources.DeffinityRes.Description %>
                <asp:TextBox ID="txtdescription" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnApprovalForManager" runat="server" Text="Manager Approval" SkinID="btnDefault"
                    ValidationGroup="b" OnClick="btnApprovalForManager_Click" />
            </td>
            <td>
                <asp:Button ID="btnApprovalForCustomer" runat="server" Text="Submit Email Customer Approval" SkinID="btnDefault"
                    ValidationGroup="b" OnClick="btnApprovalForCustomer_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvBreakdownHours" runat="server" AutoGenerateColumns="False"  OnRowCommand="gvBreakdownHours_RowCommand">
        <Columns>
            <asp:BoundField DataField="ContractorName" HeaderText="<%$ Resources:DeffinityRes,Name%>" HeaderStyle-CssClass="header_bg_l" />
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,AdditionalHours%>>">
                <ItemTemplate>
                    <asp:Label ID="lblChange" runat="server" Text='<%# ChangeHours(Eval("AdditionalHours").ToString())%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Width="60px" />
            </asp:TemplateField>
               <asp:BoundField DataField="Description" HeaderText="<%$ Resources:DeffinityRes,Description%>" />
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDelete1" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                        CommandName="Delete1" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkDelete" ToolTip="Delete">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
</asp:Panel>
<div class="clr">
</div>
<br />
<table>
    <tr>
        <td style="padding-left:380px;">
            <b> <%= Resources.DeffinityRes.PreviousMonth %></b>&nbsp;
            <asp:LinkButton ID="imgPreviousMonth" runat="server" SkinID="BtnLinkBack" ToolTip="Previous Month" OnClick="imgPreviousMonth_Click"></asp:LinkButton>
        </td>
        <td id="tdBackgroundWidth" runat="server">
        </td>
        <td>
            <asp:LinkButton ID="imgNextMonth" runat="server" SkinID="BtnLinkNext"
                                ToolTip="Next Month" OnClick="imgNextMonth_Click"></asp:LinkButton>
            &nbsp;<b><%= Resources.DeffinityRes.NextMonth %></b>
        </td>
    </tr>
</table>
<div style="width: 100%;">
    <asp:HiddenField ID="hfStartDate" runat="server" />
    <asp:HiddenField ID="hfEndDate" runat="server" />
    <asp:HiddenField ID="hfMonth" runat="server" />
    <asp:GridView ID="gvPMHours" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvPMHours_RowDataBound"
        Width="100%" OnRowCancelingEdit="gvPMHours_RowCancelingEdit" OnRowEditing="gvPMHours_RowEditing"
        AllowPaging="true" PageSize="15" OnRowUpdating="gvPMHours_RowUpdating" OnPageIndexChanging="gvPMHours_PageIndexChanging"
        OnDataBound="gvPMHours_DataBound">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="10px" />
                <ItemTemplate>
                    <asp:LinkButton  ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                    </asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update"
                        ValidationGroup="ValidResource"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,AssignedResource%>">
                <ItemTemplate>
                    <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName")+"   (" +Eval("SectionType") +")" %>'></asp:Label>
                    <asp:Label ID="lblResourceID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblContractorID" runat="server" Text='<%# Bind("ContractorID") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("ResourceName") %>' Visible="false" ></asp:Label>
                    <asp:Label ID="lblSectionType" runat="server" Text='<%# Bind("SectionType") %>' Visible="false"></asp:Label>
                    <asp:Label ID="LblprojectRef" runat="server" Text='<%#Bind("ProjectReference") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,WC1%>" ItemStyle-HorizontalAlign="Right">
              <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <div class="form-inline">

                    <asp:TextBox ID="txtPMHours2" runat="server" SkinID="Time" ></asp:TextBox>
                    <asp:LinkButton ID="ImgBtnCopy" runat="server" SkinID="BtnLinkNext" OnClick="btn_ApplyDate_OnClick"
                                                 CommandName="Copy" CausesValidation="false" />
                   <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter" runat="server" ValidChars="0123456789:"
                                                                 TargetControlID="txtPMHours2"></ajaxToolkit:FilteredTextBoxExtender>--%>
                    <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtPMHours2"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,WC2%>" ItemStyle-HorizontalAlign="Right">
          <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours3" runat="server" SkinID="Time" ></asp:TextBox>
                     <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter" runat="server" ValidChars="0123456789:"
                                                                 TargetControlID="txtPMHours3"></ajaxToolkit:FilteredTextBoxExtender>--%>
                    <asp:RegularExpressionValidator ID="regex2" runat="server" ControlToValidate="txtPMHours3"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,WC3%>" ItemStyle-HorizontalAlign="Right">
              <HeaderStyle HorizontalAlign="Center"/>
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours4" runat="server" SkinID="Time" ></asp:TextBox>
                  
                      <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter12" runat="server" ValidChars="0123456789:"
                                                                 TargetControlID="txtPMHours4"></ajaxToolkit:FilteredTextBoxExtender>--%>
                    <asp:RegularExpressionValidator ID="regex3" runat="server" ControlToValidate="txtPMHours4"
                                                ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                                                ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,WC4%>" ItemStyle-HorizontalAlign="Right">
             <HeaderStyle HorizontalAlign="Center"/>
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours5" runat="server" SkinID="Time" ></asp:TextBox>
                   <%--   <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter2" runat="server" ValidChars="0123456789:"
                                                                 TargetControlID="txtPMHours5"></ajaxToolkit:FilteredTextBoxExtender>--%>
                    <asp:RegularExpressionValidator ID="regex4" runat="server" ControlToValidate="txtPMHours5"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,WC5%>" ItemStyle-HorizontalAlign="Right">
              <HeaderStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours6" runat="server" SkinID="Time" ></asp:TextBox>
                   <%--   <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter3" runat="server" ValidChars="0123456789:"
                                                                 TargetControlID="txtPMHours6"></ajaxToolkit:FilteredTextBoxExtender>--%>
                    <asp:RegularExpressionValidator ID="regex5" runat="server" ControlToValidate="txtPMHours6"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Total%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" SkinID="Price"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Cost%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Utilisation%>" ItemStyle-HorizontalAlign="Center" >
                   <HeaderStyle />
                <ItemTemplate>
                    <asp:LinkButton ID="hlUtilization" runat="server" SkinID="Linkutilisation"
                         NavigateUrl='<%# Eval("ContractorID","~/WF/reports/ResourceProjectSchedule.aspx?CID={0}") %>'
                        Target="_blank"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalHoursBudgeted%>" Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                   <%-- <asp:Label ID="lblMaxHours" runat="server" Text='<%# ChangeHours(Eval("MaxHoursAllocated","{0:F2}").ToString())%>'></asp:Label>--%>
                        <asp:TextBox ID="TMaxHours" runat="server" SkinID="Time" Text='<%# ChangeHours(Eval("MaxHoursAllocated","{0:F2}").ToString())%>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMaxHours" runat="server" MaxLength="10" SkinID="Time" Text='<%# ChangeHours(Eval("MaxHoursAllocated","{0:F2}").ToString())%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CV_Grid3_SetupBuy11" runat="server" ControlToValidate="txtMaxHours"
                        Display="None" ValidationGroup="ValidResource" ErrorMessage="Please enter maximum hours"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexMax1" runat="server" ControlToValidate="txtMaxHours"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="ValidResource"
                        SetFocusOnError="True" Display="None" Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalHrsBooked%>" Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblTotalHoursBooked" runat="server" Text='<%# ChangeHours(Eval("TotalHoursBooked","{0:F2}").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,NotifymewhenHrsExceed%>" Visible="false">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblnotificationreminingHours" runat="server" Text='<%# ChangeHours(Eval("NotificationRemainingHours","{0:F2}").ToString())%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNotificationHours" runat="server" SkinID="Time"  Text='<%# ChangeHours(Eval("NotificationRemainingHours","{0:F2}").ToString())%>'
                        MaxLength="10" ClientIDMode="Static"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtNotificationHours"
                        Display="None" ValidationGroup="ValidResource" ErrorMessage="Please enter Notification when hrs remaining"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regex1N22" runat="server" ControlToValidate="txtNotificationHours"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="ValidResource"
                        SetFocusOnError="True" Display="None" Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<asp:Button ID="imgUpdate" runat="server" CommandName="Update" SkinID="btnSave" CommandArgument="0" OnClick="imgUpdate_Click" ValidationGroup="p" />
<ajaxToolkit:ModalPopupExtender ID="mdlCustomerMail" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="imgPopupCustomerMail" PopupControlID="pnlCustomerMail" CancelControlID="imghistoryCancel">
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="imgPopupCustomerMail" runat="server" />
<asp:Panel ID="pnlCustomerMail" runat="server" BackColor="White" Style="display: none;"
    Width="400px" Height="150px" BorderStyle="Double" BorderColor="LightSteelBlue"
    ScrollBars="Auto">
    <div style="float: right">
        <asp:LinkButton ID="imghistoryCancel" runat="server" SkinID="BtnLinkCancel" ToolTip="Close" /></div>
    
   
<div class="row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
        ValidationGroup="c" />
	</div>
</div>
    
    
<div class="row">
          <div class="col-md-12">
               <asp:Label ID="lblCustomerMsg" runat="server" ForeColor="Green"></asp:Label>
	</div>
</div>
   
     <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer %>:</label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_90">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCustomer"
                    InitialValue="0" ErrorMessage="Please select customer" ValidationGroup="c">*</asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
                <asp:Button ID="imgSend" runat="server" SkinID="btnDefault" Text="Save"
                    ValidationGroup="c" OnClick="imgSend_Click" />
            </div>
	</div>
</div>
</asp:Panel>