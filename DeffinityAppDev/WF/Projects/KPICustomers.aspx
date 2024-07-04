<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="KPICustomers" Codebehind="KPICustomers.aspx.cs" %>

<%@ Register Src="controls/KPITab.ascx" TagName="KPITabs" TagPrefix="UCKpi" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.KPI%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.KPICustomer%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <UCKpi:KPITabs ID="kpiTab" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_options" runat="Server">
   <a id="lbtn_Navigate" target="_self" href="FMResources.aspx"><span id="lbtn_NavigateText" runat="server"><i class="fa fa-arrow-left"></i> Return to Finance Section</span></a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCustomer" runat="server">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Programme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProgramme" AutoPostBack="false"    runat="server">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlSubprogramme" runat="server">
                </asp:DropDownList>
                 <ajaxToolkit:CascadingDropDown ID="CascadingDropDown21" runat="server" TargetControlID="ddlSubprogramme"
                                    Category="Title" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetSubProgramme" ParentControlID="ddlProgramme" />
                 <asp:SqlDataSource ID="SqlDataSourcesubprogram" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProgramme" DefaultValue="0" 
                                        Name="PROGRAMMEID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                        </asp:SqlDataSource>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FilterbyYear%></label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                                <asp:Image ID="imgbtnenddate" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender"  Format="yyyy" runat="server"
                                    PopupButtonID="imgbtnenddate" TargetControlID="txtFromDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
	<div class="col-md-2">
        <span class="pull-right">
        <asp:Button ID="imgView" SkinID="btnDefault" Text="View" runat="server" 
                    onclick="imgView_Click" />
           <asp:TextBox ID="txtToDate" runat="server" Width="80px" Visible="False"></asp:TextBox>
                <asp:Image ID="imgbtnenddate1" runat="server" SkinID="Calender" 
                    Visible="False" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    CssClass="MyCalendar"  PopupButtonID="imgbtnenddate1" 
                    TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            </span>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
              <asp:Literal ID="ltlDisplay" runat="server"></asp:Literal>
	</div>
</div>
   
</asp:Content>


