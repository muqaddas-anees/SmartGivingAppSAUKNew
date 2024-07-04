<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Training_controls_SkillSearchCtrl" Codebehind="SkillSearchCtrl.ascx.cs" %>
<asp:ValidationSummary ID="validSummrySearch" runat="server" ValidationGroup="Group1"
    ShowSummary="true" />
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.User%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlUser" runat="server" SkinID="ddl_90">
            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Skills%></label>
           <div class="col-sm-9">
                <asp:Panel ID="pnlSkill" runat="server" Width="220px" BorderColor="Silver" BorderWidth="1px"
                            Height="100px" ScrollBars="Auto">
                            <asp:CheckBoxList ID="chkSkills" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Country%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlCountry" runat="server" SkinID="ddl_90">
            </asp:DropDownList>
            </div>
	</div>
</div>
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.From%></label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
            <asp:Label ID="imgFromDate" runat="server" SkinID="Calender" />
            <ajaxToolkit:CalendarExtender runat="server" ID="calFromDate" TargetControlID="txtFromDate"
                PopupButtonID="imgFromDate" CssClass="MyCalendar" >
            </ajaxToolkit:CalendarExtender>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid from date"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                ValidationGroup="Group1" Display="None" ControlToValidate="txtFromDate">
            </asp:RegularExpressionValidator>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.To%></label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtToDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
            <asp:Label ID="imgToDate" runat="server" SkinID="Calender" />
            <ajaxToolkit:CalendarExtender runat="server" ID="calToDate" TargetControlID="txtToDate"
                PopupButtonID="imgToDate" CssClass="MyCalendar" >
            </ajaxToolkit:CalendarExtender>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid to date"
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                ValidationGroup="Group1" Display="None" ControlToValidate="txtToDate">
            </asp:RegularExpressionValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter to date greater than from date"
                Display="None" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                ValidationGroup="Group1"></asp:CompareValidator>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <asp:Button ID="btnFilter" runat="server" SkinID="btnSearch" 
                ValidationGroup="Group1" OnClick="btnFilter_Click" />
            </div>
	</div>
</div>



<asp:GridView ID="gvSkills" runat="server" Width="100%" AllowPaging="true" 
    PageSize="30" onpageindexchanging="gvSkills_PageIndexChanging" EmptyDataText="No records found" >
    <Columns>
        <asp:BoundField DataField="User" HeaderText="User" />
        <asp:BoundField DataField="CourseSkill" HeaderText="Skill" ItemStyle-Width="700px" />
        <asp:BoundField DataField="Country" HeaderText="Country" />
        <asp:BoundField DataField="From" HeaderText="From" DataFormatString="{0:d}" />
        <asp:BoundField DataField="To" HeaderText="To" DataFormatString="{0:d}" />
    </Columns>
</asp:GridView>
