<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="TimesheetReport.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Timesheets.TimesheetReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.Timesheet %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Timesheet Report
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      <div class="form-group row mb-5">
      <div class="col-md-2 d-flex d-inline">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Date%>:</label>
           <div class="col-sm-8 d-flex d-inline"> <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="DateNew"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender" Visible="false" />
               
                <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />
           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate8" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
       
	<div class="col-md-4 d-flex d-inline" >
           <label class="col-sm-1 control-label">Users</label>
           <div class="col-sm-10">
                  <asp:DropDownList ID="ddlUsers" runat="server"></asp:DropDownList>
                                          
            </div>
	</div>
         <div class="col-md-4 d-flex d-inline">
      
                        <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View" 
                                        OnClick="btn_viewdate_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" style="margin-right:10px" />      
              <asp:Button ID="Button1" runat="server" SkinID="btnDefault" Text="Export to Excel" 
                                        OnClick="btn_ExportExcel_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" />        
       
            
	</div>
	
</div>
    <asp:GridView ID="GridPartner" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowDataBound="GridPartner_RowDataBound" OnPageIndexChanging="GridPartner_PageIndexChanging" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="3%" Visible="false" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Job" ItemStyle-Width="30%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Users" ItemStyle-Width="7%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName1" runat="server" Text='<%# Bind("ResourceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Date" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblTimeExpensesDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="From Time (HH:MM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" >
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridStartTime" runat="server" Text='<%# ChangeTimeDisplay(Eval("fromtime").ToString())%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText=" To Time (HH:MM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" >
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridEndTime" runat="server" Text='<%#ChangeTimeDisplay( Eval("totime").ToString())%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="8%" >
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblhours" runat="server" Text='<%# ToHours(Eval("Hours").ToString())%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
              <asp:TemplateField HeaderText="Approver" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPrimeApproverName" runat="server" Text='<%# Eval("PrimeApproverName").ToString()%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
             <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPrimeApproverName1" runat="server" Text='<%# Eval("TimeSheetStatusName").ToString()%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                       
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" Visible="false" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>
      <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
