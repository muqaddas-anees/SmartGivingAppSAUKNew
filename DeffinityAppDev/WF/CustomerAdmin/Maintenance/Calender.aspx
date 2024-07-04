<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="Calender.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.Calender" %>

<%@ Register Src="~/WF/CustomerAdmin/Maintenance/Controls/MaintenancePlanTabCtrl.ascx" TagPrefix="Pref" TagName="MaintenancePlanTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Maintenance Plan
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:MaintenancePlanTabCtrl runat="server" ID="MaintenancePlanTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    
Calendar
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .groupheader td{
            font-weight:bold;
            font-size:17px;
        }
    </style>
    <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="LastItem" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
              <table class="table table-striped" id="TestTable">
                    <thead>  <tr>
                        <th></th>
                        <th>January</th>
                         <th>February</th>
                         <th>March</th>
                         <th>April</th>
                         <th>May</th>
                         <th>June</th>
                         <th>July</th>
                         <th>August</th>
                         <th>September</th>
                         <th>October</th>
                         <th>November</th>
                         <th>December</th>
                        
                      
                      
                    </tr></thead>
                  <tbody>
                       <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                  </tbody>
                  
                </table>
              </LayoutTemplate>
                                    <InsertItemTemplate>
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                    </InsertItemTemplate>
          <ItemTemplate>
               <%# AddGroupingRowIfBoardingHasChanged() %>
              <tr>
                  <td><asp:Label ID="lblMaterial" runat="server" Text='<%# Eval("Material") %>'></asp:Label></td>
                  <td> <asp:Label ID="lblJanuary" runat="server" Text='<%# Eval("January") %>'></asp:Label></td>
                  <td><asp:Label ID="lblFebruary" runat="server" Text='<%# Eval("February") %>'></asp:Label></td>
                  <td><asp:Label ID="lblMarch" runat="server" Text='<%# Eval("March") %>'></asp:Label></td>
                  <td><asp:Label ID="lblApril" runat="server" Text='<%# Eval("April") %>'></asp:Label></td>
                  <td><asp:Label ID="lblMay" runat="server" Text='<%# Eval("May") %>'></asp:Label></td>
                  <td><asp:Label ID="lblJune" runat="server" Text='<%# Eval("June") %>'></asp:Label></td>
                  <td><asp:Label ID="lblJuly" runat="server" Text='<%# Eval("July") %>'></asp:Label></td>
                  <td><asp:Label ID="lblAugust" runat="server" Text='<%# Eval("August") %>'></asp:Label></td>
                  <td><asp:Label ID="lblSeptember" runat="server" Text='<%# Eval("September") %>'></asp:Label></td>
                  <td><asp:Label ID="lblOctober" runat="server" Text='<%# Eval("October") %>'></asp:Label></td>
                  <td><asp:Label ID="lblNovember" runat="server" Text='<%# Eval("November") %>'></asp:Label></td>
                  <td><asp:Label ID="lblDecember" runat="server" Text='<%# Eval("December") %>'></asp:Label></td>


              </tr>


              

              </ItemTemplate>
               </asp:ListView>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>

