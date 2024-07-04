<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="KPITarget" Codebehind="KPITarget.aspx.cs" %>

<%@ Register Src="controls/KPITab.ascx" TagName="KPITabs" TagPrefix="UCKpi" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.KPI%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.UpdateTargtes%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
   <a id="lbtn_Navigate" target="_self" href="FMResources.aspx"><span id="lbtn_NavigateText" runat="server"><i class="fa fa-arrow-left"></i> Return to Finance Section</span></a>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<UCKpi:KPITabs ID="kpiTab" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
 <asp:Label ID="lblMessage" runat="server" Visible="false" ></asp:Label>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.SelectKPICategory%></label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlPageType" runat="server"
                    AutoPostBack="false" 
                    onselectedindexchanged="ddlPageType_SelectedIndexChanged">
                    <%--<asp:ListItem Selected="True" Text="Please select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Please select" Value="0"></asp:ListItem>--%>
                    <asp:ListItem Text="KPI-Financial" Value="1"></asp:ListItem>
                    <asp:ListItem Text="KPI-Resources" Value="2"></asp:ListItem>
                    <asp:ListItem Text="KPI-Customers" Value="3"></asp:ListItem>
                    <asp:ListItem Text="KPI-Service" Value="4"></asp:ListItem>
                    <asp:ListItem Text="KPI-Internal Perspective" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.Year%></label>
           <div class="col-sm-7 form-inline">
               <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtFromDate"
                                    Display="None" ErrorMessage="Please enter year" ValidationGroup="Group1">
                                   </asp:RequiredFieldValidator>
                                <asp:Label ID="imgbtnenddate" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender"  Format="yyyy" runat="server"
                                    PopupButtonID="imgbtnenddate"  TargetControlID="txtFromDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
          <asp:Button ID="imgView" SkinID="btnDefault" Text="Filter" runat="server"  ValidationGroup="Group1"
                    onclick="imgView_Click" />
	</div>
</div>
<asp:GridView ID="gridKPILables" runat="server" AutoGenerateColumns="false" 
        EmptyDataText="No Records Found" onrowcommand="gridKPILables_RowCommand" 
        onrowediting="gridKPILables_RowEditing" 
        onrowupdating="gridKPILables_RowUpdating" >
        <Columns>
            <asp:TemplateField Visible="false">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" />
                <ItemTemplate>
                    <div style="width: 45px">
                        <div style="width: 20px; float: left">
                            <asp:Label ID="lblID" runat="server" Text="<%# Bind('LabelID')%>" Visible="false"> </asp:Label>
                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                CommandArgument="<%# Bind('LabelID')%>" SkinID="BtnLinkEdit" ToolTip="Edit">
                            </asp:LinkButton></div>
                    </div>
                </ItemTemplate>
              <%--  <EditItemTemplate>
                    <div style="width: 45px">
                        <div style="width: 20px; float: left">
                            <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                CommandArgument="<%# Bind('LabelID')%>" ImageUrl="~/media/ico_update.png" ToolTip="Update"
                                ValidationGroup="KPIGrid"></asp:ImageButton>
                        </div>
                        <div style="width: 20px; float: left">
                            <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton></div>
                    </div>
                </EditItemTemplate>--%>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
            <ItemStyle Width="400px" />
                <ItemTemplate>
                    <asp:Label ID="lblLabelName" runat="server" Text='<%# Bind("LabelsName") %>'></asp:Label>
                </ItemTemplate>
               <%-- <EditItemTemplate>
                    <asp:TextBox ID="txtKpiLabelName" runat="server" Width="150px" Text='<%#Bind("LabelsName")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator15grid" runat="server"
                        ControlToValidate="txtKpiLabelName" ErrorMessage="Please enter label name" ValidationGroup="KPIGrid"></asp:RequiredFieldValidator>
                </EditItemTemplate>--%>
                <FooterTemplate>
                <asp:Button ID="imgUpdate" CommandName="Update" Visible="<%#showButton()%>"  SkinID="btnUpdate" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Target Values" HeaderStyle-CssClass="header_bg_r">
            <ItemStyle Width="100px" />
                <ItemTemplate>
                   <asp:TextBox ID="txtDescription" runat="server" Text="<%# Bind('value')%>" width="75px"></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r" Visible="false">
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                <ItemTemplate>
                    <asp:LinkButton ID="deletebut0" runat="server" CommandArgument='<%# Bind("LabelID") %>'
                        CommandName="delete" OnClientClick="return confirm('Do you want to delete ?');"
                        SkinID="BtnLinkDelete" ToolTip="<%$ Resources:DeffinityRes,Delete%>" Visible="True" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
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


