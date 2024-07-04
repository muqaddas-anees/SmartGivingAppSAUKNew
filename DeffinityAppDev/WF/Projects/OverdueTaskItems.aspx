<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="OverdueTaskItems" Codebehind="OverdueTaskItems.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group">
        <div class="col-md-12">
           <strong>Project Reference:
            <asp:Label runat="server" ID="lblProject"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    
    <div class="pro_madatory" style="padding-top: 10px">
    </div>
    <div class="clr">
    </div>
    <div class="form-group">
        <div class="col-md-12">
           <strong> Overdue Task Items </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
   
    <div>
    </div>
    <asp:GridView ID="gvOverdueTasks" runat="server" Width="732px">
        <Columns>
            <asp:BoundField HeaderText="Task" DataField="Task" />
            <asp:BoundField HeaderText="Start Date" DataField="StartDate" DataFormatString="{0:d}" ItemStyle-Width="135px" />
            <asp:BoundField HeaderText="End Date" DataField="EndDate" DataFormatString="{0:d}" ItemStyle-Width="135px" />
        </Columns>
    </asp:GridView>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>
