<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PTStaffHours" Codebehind="PTStaffHours.ascx.cs" %>
<%@ Register Src="~/WF/Projects/MailControls/ResourceTimesheetAlert.ascx" TagName="MailControl"
    TagPrefix="uc2" %>
<style>
    .round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 80%;
        border-radius: 8px;
    }
</style>
<div class="row">
<div class="col-md-12">
    &nbsp;
</div>
</div>
<uc2:MailControl ID="ResourceTimesheetAler1" runat="server" Visible="false" />
<div style="width: 38%; float: left;">
   
        <div class="well">
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Original %>:</label>
                                      <div class="col-sm-5 pull-right control-label"> <asp:Label ID="lblOriginal" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Forecast %>:</label>
                                      <div class="col-sm-5 pull-right control-label"> <asp:Label ID="lblForecast" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Actual1 %>:</label>
                                      <div class="col-sm-5 pull-right control-label"> <asp:Label ID="lblActual" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.CostRemaining %>:</label>
                                      <div class="col-sm-5 pull-right control-label"><asp:Label ID="lblCostRemaining" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
        
    
    </div>
    <div class="clr">
    </div>
    <br />
     <div class="well">
         <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-6 control-label" style="border-top-left-radius: 8px"> &nbsp;</label>
                                      <div class="col-sm-3  control-label"><%= Resources.DeffinityRes.Hours %>
					</div>
                  <div class="col-sm-3  control-label" style="border-top-right-radius: 8px"> <%= Resources.DeffinityRes.Cost %>
					</div>
				</div>
                </div>
         <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.ForecastedHours %></label>
                   <div class="col-sm-3 pull-right control-label">
                      <asp:Label ID="lblOriginalPMHoursQuotedTotal" runat="server" Font-Bold="true"></asp:Label>
					</div>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblOriginalPMHoursQuotedUnit" runat="server" Font-Bold="true"></asp:Label>
					</div>
                
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.VariationstoPMHoursApproved %></label>
                  <div class="col-sm-3 pull-right control-label">  <asp:Label ID="lblVariationPMHoursQuotedTotal" runat="server" Font-Bold="true"></asp:Label>
					</div>
                                      <div class="col-sm-3 pull-right control-label"><asp:Label ID="lblVariationPMHoursQuotedUnit" runat="server" Font-Bold="true"></asp:Label>
					</div>
                 
				</div>
                </div>
        <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.BudgetedHours %></label>
                                      <div class="col-sm-3 control-label"><asp:Label ID="Lblhours" Font-Bold="true" runat="server"></asp:Label>
					</div>
                  <div class="col-sm-3 pull-right control-label"><asp:Label ID="LblCost" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
    </div>
</div>
<asp:Panel ID="pnlBreakdownHours" runat="server">
<div style="width: 58%; float: right;margin-left:5px">

        <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.AdditionalStaffHours %>  </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
       
        <asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="b" />
        <asp:HiddenField ID="hfVariationID" runat="server" Value="0" />
         <div class="form-group">
             <div class="col-md-12">
                                       <div class="col-sm-4 control-label">
                                             <asp:DropDownList ID="ddlUser" runat="server" Width="180">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlUser"
                        InitialValue="0" ErrorMessage="Please select user" ValidationGroup="b">*</asp:RequiredFieldValidator>
					</div>
                                      <div class="col-sm-3 control-label"> <%= Resources.DeffinityRes.AdditionalHours %>
					</div>
                  <div class="col-sm-5 control-label form-inline">
                       <asp:TextBox ID="txtAdditionalHours" runat="server" SkinID="Time" Text="0:00"></asp:TextBox><span
                        style="color: Gray">(HH:MM)</span>
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtAdditionalHours"
                        ErrorMessage="Please enter additional hours " ValidationGroup="b">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regex122" runat="server" ControlToValidate="txtAdditionalHours"
                        ValidationExpression="^((-?\d+):([0-5][0-9]))$" ValidationGroup="b" SetFocusOnError="True"
                        Display="None" Text="*" ErrorMessage="Please enter valid time and miniues "></asp:RegularExpressionValidator>
					</div>
				</div>
                </div>
         <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.Description %></label>
                                      <div class="col-sm-6">  <asp:TextBox ID="txtdescription" runat="server"></asp:TextBox>
					</div>
				</div>
                </div>

          <div class="form-group">
             <div class="col-md-12 form-inline">
                  <asp:Button ID="btnApprovalForManager" runat="server" Text="Submit to manager for approval"
                        ValidationGroup="b"  OnClick="btnApprovalForManager_Click" />
                      <asp:Button ID="btnApprovalForCustomer" runat="server" SkinID="btnDefault" Text="Submit and Email the Customer for Approval" 
                        ValidationGroup="b" OnClick="btnApprovalForCustomer_Click" />
                 </div>
              </div>
       
        <asp:GridView ID="gvBreakdownHours" runat="server" AutoGenerateColumns="False"
            OnRowCommand="gvBreakdownHours_RowCommand">
            <Columns>
                <asp:BoundField DataField="ContractorName" HeaderText="Name" />
                <asp:TemplateField HeaderText="Additional Hours">
                    <ItemTemplate>
                        <asp:Label ID="lblChange" runat="server" Text='<%# ChangeHours(Eval("AdditionalHours").ToString())%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="Description" />
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

<div class="form-group">
<div class="col-md-12 col-md-offset-2">

<table>
    <tr>
        <td>
            <b><%= Resources.DeffinityRes.PreviousMonth %> </b>&nbsp;
            <asp:LinkButton ID="imgPreviousMonth" runat="server" SkinID="BtnLinkBack"
                ToolTip="Previous Month" OnClick="imgPreviousMonth_Click" />
        </td>
        <td id="tdBackgroundWidth" runat="server">
        </td>
        <td>
            <asp:LinkButton ID="imgNextMonth" runat="server" SkinID="BtnLinkNext"
                ToolTip="Next Month" OnClick="imgNextMonth_Click" />&nbsp;<b> <%= Resources.DeffinityRes.NextMonth %></b>
        </td>
    </tr>
</table>
    </div>
    </div>
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
                <HeaderStyle Width="10px" CssClass="header_bg_l" />
                <ItemStyle Width="6%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
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
            <asp:TemplateField HeaderText="Assigned Resource">
                <ItemTemplate>
                    <asp:Label ID="lblResourceName" runat="server" Text='<%# Eval("ResourceName")+"   (" +Eval("SectionType") +")" %>'></asp:Label>
                    <asp:Label ID="lblResourceID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblContractorID" runat="server" Text='<%# Bind("ContractorID") %>'
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("ResourceName") %>' Visible="false" ></asp:Label>
                    <asp:Label ID="lblSectionType" runat="server" Text='<%# Bind("SectionType") %>' Visible="false"></asp:Label>
                    <asp:Label ID="LblprojectRef" runat="server" Text='<%#Bind("ProjectReference") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                   <ItemStyle Width="250px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WC1" ItemStyle-HorizontalAlign="Right">
              <HeaderStyle HorizontalAlign="Center" BackColor="#D7E3DA" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours2" runat="server" Width="50px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtPMHours2"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
                 <ItemStyle Width="70px" />
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WC2" ItemStyle-HorizontalAlign="Right">
          <HeaderStyle HorizontalAlign="Center" BackColor="#D7E3DA" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours3" runat="server" Width="50px" SkinID="Price"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regex2" runat="server" ControlToValidate="txtPMHours3"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
                 <ItemStyle Width="70px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WC3" ItemStyle-HorizontalAlign="Right">
              <HeaderStyle HorizontalAlign="Center" BackColor="#D7E3DA" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours4" runat="server" Width="50px" SkinID="Price"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regex3" runat="server" ControlToValidate="txtPMHours4"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
                 <ItemStyle Width="70px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WC4" ItemStyle-HorizontalAlign="Right">
             <HeaderStyle HorizontalAlign="Center" BackColor="#D7E3DA" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours5" runat="server" Width="50px" SkinID="Price"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regex4" runat="server" ControlToValidate="txtPMHours5"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
                 <ItemStyle Width="70px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="WC5" ItemStyle-HorizontalAlign="Right">
              <HeaderStyle HorizontalAlign="Center" BackColor="#D7E3DA" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPMHours6" runat="server" Width="50px" SkinID="Price"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regex5" runat="server" ControlToValidate="txtPMHours6"
                        ValidationExpression="^((\d+):([0-5][0-9]))$" ValidationGroup="p" SetFocusOnError="True"
                        ErrorMessage="Please enter valid time and miniues ">*</asp:RegularExpressionValidator>
                </ItemTemplate>
                 <ItemStyle Width="70px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
            <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" SkinID="Price"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Utilisation" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="hlUtilization" runat="server" SkinID="Linkutilisation"
                        Target="_blank" NavigateUrl='<%# Eval("ContractorID","~/WF/reports/ResourceProjectSchedule.aspx?CID={0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Hours <br/>Budgeted (HH:MM)">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                   <%-- <asp:Label ID="lblMaxHours" runat="server" Text='<%# ChangeHours(Eval("MaxHoursAllocated","{0:F2}").ToString())%>'></asp:Label>--%>
                        <asp:TextBox ID="TMaxHours" runat="server" Width="50px" SkinID="Price" Text='<%# ChangeHours(Eval("MaxHoursAllocated","{0:F2}").ToString())%>'></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMaxHours" runat="server" Width="50px" MaxLength="10" Text='<%# ChangeHours(Eval("MaxHoursAllocated","{0:F2}").ToString())%>'></asp:TextBox>
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
            <asp:TemplateField HeaderText="Notify me when <br/> Hours Exceed(HH:MM)" ItemStyle-Width="7%">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:Label ID="lblnotificationreminingHours" runat="server" Text='<%# ChangeHours(Eval("NotificationRemainingHours","{0:F2}").ToString())%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNotificationHours" runat="server" Text='<%# ChangeHours(Eval("NotificationRemainingHours","{0:F2}").ToString())%>'
                        Width="50px" MaxLength="10" ClientIDMode="Static"></asp:TextBox>
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

<asp:Button ID="imgUpdate" runat="server" CommandName="Update" SkinID="btnSave"
    CommandArgument="0" OnClick="imgUpdate_Click" ValidationGroup="p" />
<ajaxToolkit:ModalPopupExtender ID="mdlCustomerMail" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="imgPopupCustomerMail" PopupControlID="pnlCustomerMail" CancelControlID="imghistoryCancel">
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="imgPopupCustomerMail" runat="server" />
<asp:Panel ID="pnlCustomerMail" runat="server" BackColor="White" Style="display: none;"
    Width="400px" Height="150px" BorderStyle="Double" BorderColor="LightSteelBlue"
    ScrollBars="Auto">
    <div style="float: right">
        <asp:LinkButton ID="imghistoryCancel" runat="server" SkinID="BtnLinkClose" ToolTip="Close" /></div>
    
   
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
