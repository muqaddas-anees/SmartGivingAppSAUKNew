<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trDepartmentTarget" Codebehind="trDepartmentTarget.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
   Dashboard - <label id="Label1" runat="server"></label><br />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server">
       <uc2:DropDownList ID="dropDownList1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div  class="form-group">
       <div class="col-xs-6">
             <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Department%></label>
             <div class="col-sm-9">
                  <asp:DropDownList ID="ddlDepartment" runat="server" SkinID="ddl_70"
                       AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
             </div>
       </div>
        <div class="col-xs-6">
              <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Area%></label>
             <div class="col-sm-9">
                  <asp:DropDownList ID="ddlArea" runat="server" SkinID="ddl_70"></asp:DropDownList>
             </div>
       </div>
         </div>
          <div  class="form-group">
        <div class="col-xs-6 form-inline">
              <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.FromDate%></label>
             <div class="col-sm-9">
                   <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                   <asp:Label ID="imgFromDate" runat="server" SkinID="Calender"></asp:Label>
                   <ajaxToolkit:CalendarExtender runat="server" ID="calFromDate" TargetControlID="txtFromDate" PopupButtonID="imgFromDate"
                   CssClass="MyCalendar" ></ajaxToolkit:CalendarExtender>
                   
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid from date" 
                   ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1" 
                       Display="None" ControlToValidate="txtFromDate"></asp:RegularExpressionValidator>
             </div>
       </div>
        <div class="col-xs-6">
              <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ToDate%></label>
             <div class="col-sm-9 form-inline">
                  <asp:TextBox ID="txtToDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                   <asp:Label ID="imgToDate" runat="server"  SkinID="Calender"></asp:Label>
                   <ajaxToolkit:CalendarExtender runat="server" ID="calToDate" TargetControlID="txtToDate" PopupButtonID="imgToDate"
                   CssClass="MyCalendar" ></ajaxToolkit:CalendarExtender>
                   
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid to date" 
                   ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1"
                        Display="None" ControlToValidate="txtToDate"></asp:RegularExpressionValidator>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter to date greater than from date" 
                   Display="None" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" 
                       Operator="GreaterThan" ValidationGroup="Group1" Type="Date"></asp:CompareValidator>
                    <asp:Button ID="btnView" runat="server" SkinID="btnDefault" Text="View"
                                          ValidationGroup="Group1" onclick="btnView_Click" />
             </div>
       </div>
        <div class="col-xs-2">
            
       </div>
    </div>
    <div class="form-group">
         <asp:Literal ID="ltlDeptAreaTarget" runat="server"></asp:Literal>
    </div>
</asp:Content>


