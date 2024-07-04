<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ResourceVacationRequest" Codebehind="VT.ResourceVacationRequest.aspx.cs" %>

<%@ Register src="controls/MyProjectsTab.ascx" tagname="MyProjectsTab" tagprefix="uc1" %>
<%@ Register src="MailControls/VTRequestMail.ascx" tagname="VT" tagprefix="Mail1" %>


<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.MyTasks%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.VacationRequest%>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:MyProjectsTab ID="MyProjectsTab1" runat="server" />
     <Mail1:VT ID="VTMail1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="form-group">
          <div class="col-md-12">
               <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ValidationGroup="insert" />
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
	</div>
</div>

    <div class="form-group">
      <div class="col-md-4">

          
<div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.NewRequest%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Resource%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txt_edit_resource" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
            </div>
	</div>
</div>

          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.AbsenceType%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlAbsenceType" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqdAbsenceType" runat="server" ControlToValidate="ddlAbsenceType"
                                            Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseselectAbsencetype%>" InitialValue="0"
                                            ValidationGroup="insert" />
            </div>
	</div>
</div>

          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.FromDate%> </label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtDateFrom" runat="server" SkinID="Date"></asp:TextBox>
                                        <asp:Label ID="Img1" runat="server" SkinID="Calender" />
                                        <asp:RequiredFieldValidator ID="rqdDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectFromDate%>" ValidationGroup="insert" />
                                        <asp:CompareValidator ID="cmpDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Invalidfromdate%>" Operator="DataTypeCheck" Type="Date"
                                            ValidationGroup="insert" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                             PopupButtonID="img1" TargetControlID="txtDateFrom" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:DropDownList ID="ddlFromPeriod" runat="server" SkinID="ddl_75px">
                                            <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Half Day" Value="0.5"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlmeridianform" runat="server" SkinID="ddl_75px" >
                                            <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
            </div>
	</div>
</div>

          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtDateTo" runat="server" SkinID="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqdDateTo" runat="server" ControlToValidate="txtDateTo"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,PlsselectToDate%>" ValidationGroup="insert" />
                                        <asp:Label ID="Img2" runat="server" SkinID="Calender" />
                                        <asp:CompareValidator ID="cmpValTo" runat="server" ControlToValidate="txtDateTo"
                                            Display="None" ErrorMessage="<%$ Resources:DeffinityRes,InvalidToDate%>" Operator="DataTypeCheck" Type="Date"
                                          ValidationGroup="insert" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                                             PopupButtonID="img2" TargetControlID="txtDateTo" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:CompareValidator ID="Cmpdates" runat="server" ControlToValidate="txtDateFrom"
                                            ControlToCompare="txtDateTo" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,DateComparison%>"
                                            Operator="LessThanEqual" Type="Date" ValidationGroup="insert" />
                                        <asp:DropDownList ID="ddlToPeriod" runat="server" SkinID="ddl_75px">
                                            <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Half Day" Value="0.5"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlmeridianto" runat="server" SkinID="ddl_75px" >
                                            <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
            </div>
	</div>
</div>

          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Notes%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="80px" Width="200px"></asp:TextBox>
            </div>
	</div>
</div>

          <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:Button ID="btnRequestLeave" runat="server" AlternateText="<%$ Resources:DeffinityRes,RequestforVacation%>"
                                            SkinID="btnApply"  OnClick="btnRequestLeave_Click" 
                                            ValidationGroup="insert" />
            </div>
	</div>
</div>

          
        
	</div>
	<div class="col-md-4">

        
<div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Existingrequests%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
          <asp:GridView ID="grdrequests" runat="server" AllowPaging="True" AutoGenerateColumns="false"
                                            DataSourceID="objRequests" 
                                            EmptyDataText="<%$ Resources:DeffinityRes,Norequestsplaced%>" DataKeyNames="ID"
                                            OnRowCommand="grdrequests_RowCommand" 
                                            onrowdatabound="grdrequests_RowDataBound" Width="100%" >
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="<%$ Resources:DeffinityRes,FromDate%>" DataField="FromDate" DataFormatString="{0:d}"
                                                    HeaderStyle-CssClass="header_bg_l" />--%>
                                               <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,FromDate%> ">
                                                <ItemTemplate>
                                                <asp:Label ID="lblFromDate" runat="server" Text='<% #Bind("FromDate","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle />
                                                </asp:TemplateField>
                                               
                                                   
                                              <%--  <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ToDate%> " DataField="ToDate" DataFormatString="{0:d}" />--%>
                                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ToDate%> ">
                                                <ItemTemplate>
                                                <asp:Label ID="lblToDate" runat="server" Text='<% #Bind("ToDate","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,TotalDays%>" DataField="Days" />
                                                 <asp:BoundField HeaderText="Leave Type" DataField="AbsenseTypeName" />
                                                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Notes%>" DataField="RequestNotes"  />
                                                <%--<asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Status%>" DataField="ApprovalStatus" />--%>
                                                <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<% #Bind("ApprovalStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnReject" runat="server" Text="<%$ Resources:DeffinityRes,Cancel%>" CommandName="Cancel"
                                                            CommandArgument='<%#Eval("ID")%>'  />
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView> 
	</div>
	<div class="col-md-4">

        
<div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Summary%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
         <ajaxToolkit:TabContainer ID="tabSummary" runat="server" Width="350px" ActiveTabIndex="1" >
                                <ajaxToolkit:TabPanel ID="tab1" runat="server">
                                <HeaderTemplate>
                                Previous year
                                </HeaderTemplate>
                                <ContentTemplate>
                                  <asp:DataList ID="dl_previous" runat="server" SkinID="ProgrammeList" Width="100%" Font-Size="Small">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="95%" border="0" cellpadding="0" cellspacing="1">
                                                    <tr>
                                                        <td >
                                                            <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("Titles") %>' Font-Bold="true" />
                                                        </td>
                                                        <td  style="float:right;text-align:right;">
                                                            <asp:Label ID="lblsum_values" runat="server" Text='<%# Eval("sum_values") %>' Width="100%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                           
                                        </asp:DataList>
                                </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                 <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" >
                                <HeaderTemplate>
                                <span>Current year</span>
                                </HeaderTemplate>
                                <ContentTemplate>
                                  <asp:Label ID="lblthisyear" runat="server"></asp:Label>
                                        <asp:DataList ID="dlist_summary" runat="server" SkinID="ProgrammeList" Width="100%" Font-Size="Small" >
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="95%" border="0" cellpadding="0" cellspacing="1">
                                                    <tr>
                                                        <td >
                                                            <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("Titles") %>' Font-Bold="true" />
                                                        </td>
                                                        <td  style="float:right;text-align:right;">
                                                            <asp:Label ID="lblsum_values" runat="server" Text='<%# Eval("sum_values") %>' Width="100%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                           
                                        </asp:DataList>
                                </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                 <ajaxToolkit:TabPanel ID="TabPanel2" runat="server">
                                <HeaderTemplate>
                                <span>Next year</span>
                                </HeaderTemplate>
                                <ContentTemplate>
                                <asp:DataList ID="dl_nextyear" runat="server" SkinID="ProgrammeList" Width="100%" Font-Size="Small">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="95%" border="0" cellpadding="0" cellspacing="1">
                                                    <tr>
                                                        <td >
                                                            <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("Titles") %>' Font-Bold="true" />
                                                        </td>
                                                        <td  style="float:right;text-align:right;">
                                                            <asp:Label ID="lblsum_values" runat="server" Text='<%# Eval("sum_values") %>' Width="100%" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                           
                                        </asp:DataList>
                                </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                
                                </ajaxToolkit:TabContainer> 
	</div>
</div>

   
  
      <asp:ObjectDataSource ID="objTeam" runat="server" TypeName="DataHelperClass" OldValuesParameterFormatString="original_{0}"
          SelectMethod="LoadAllTeamsByPortfolio" />
      <asp:ObjectDataSource ID="objRequests" runat="server" SelectMethod="SelectByUser"
          TypeName="VT.DAL.LeaveRequestHelper" OldValuesParameterFormatString="original_{0}">
          <SelectParameters>
              <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
          </SelectParameters>
      </asp:ObjectDataSource>
          <asp:HiddenField ID="HD_TeamType" runat="server" />
          <asp:HiddenField ID="h_teamtype" runat="server" />
          <asp:HiddenField ID="h_memberid" runat="server" />
          <asp:HiddenField ID="h_requestid" runat="server" />
          <asp:HiddenField ID="h_periodid" runat="server" />
     
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
    
 <script type="text/javascript">

       // grid_responsive();
         grid_responsive_display();

        $(window).load(function () {
          

            $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
           
        });
    </script>
   </asp:Content>
 
 <asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="Server">
</asp:Content>