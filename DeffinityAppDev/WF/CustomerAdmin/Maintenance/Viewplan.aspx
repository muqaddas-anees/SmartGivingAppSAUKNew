<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="Viewplan.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.Viewplan" %>

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
    View Plan
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
<style>
    #MainContent_MainContent_GridView1_gvInnerTimeSheet_0  tbody> tr>th{
    background-color:white;
    color:black;
    font-weight:normal;
}
</style>
    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" PageSize="5" 
                                            AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"  GridLines="None"
                                             OnRowDataBound="GridView1_RowDataBound"
                                            EmptyDataText="No timesheet data found.">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
                                                        <a id="imageColapse" href="javascript:expandcollapse('div<%# Eval("EquipmentID") %>', 'one');">
                                                            <img id='imgdiv<%# Eval("EquipmentID") %>' alt="Click to show/hide <%# Eval("EquipmentID") %>"
                                                                width="9px" border="0" src="../../../Content/images/minus.gif" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeName" runat="server"  Text='<%# Bind("TypeOfEquipmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Manufacturer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblManufacturer" runat="server" Text='<%# Bind("ManufacturerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Model" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModelNumber" runat="server" Text='<%# Bind("ModelNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Serial">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Bind("SerialNumber") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Visits Per Year" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTimePerYear" runat="server" Text='<%# Bind("TimePerYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Starting" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStartMonth" runat="server" Text='<%# Bind("StartMonth") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="70%">
                                                                <div id='div<%# Eval("EquipmentID") %>' style="overflow-x:auto;width:80%">
                                                                    <asp:GridView ID="gvInnerTimeSheet" SkinID="gv_responsive"  runat="server"  AutoGenerateColumns="false" 
                                                                         DataKeyNames="EquipmentID"
                                                                         ShowFooter="true">
                                                                        <Columns>
                                                                           
                                                                            <asp:TemplateField HeaderText="Material" HeaderStyle-ForeColor="White" ItemStyle-BackColor="White">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMaterial" runat="server" Text='<%# Bind("Material") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Qty Per Visit" ItemStyle-HorizontalAlign="Right" HeaderStyle-ForeColor="White" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQtyPerVisit" runat="server" Text='<%# Bind("QtyPerVisit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" HeaderStyle-ForeColor="White"  ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price","{0:N2}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="2%" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>


        <script type="text/javascript">
        function expandcollapse(obj, row) {
            debugger;
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
            Close_All(obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "../../../Content/images/minus.gif";
                }
                else {
                    img.src = "../../../Content/images/minus.gif";
                }
                img.alt = "Close";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "../../../Content/images/plus.gif";
                }
                else {
                    img.src = "../../../Content/images/plus.gif";
                }
                img.alt = "Expand";
            }
        }
        function Close_All(obj) {
            var divOld = document.getElementById(obj);
            var getAttribute;
            var str = '';
            var Grid_Table = document.getElementById('<%= GridView1.ClientID %>');
            for (var row = 1; row < Grid_Table.rows.length - 1; row++) {
                //expandcollapse(Grid_Table, row)

                var imageColapsenm;
                imageColapsenm = Grid_Table.rows[row].cells[0].firstChild.id;
                if (imageColapsenm != 'imageColapse') {
                    //alert(imageColapsenm);
                    if (imageColapsenm != null) {

                        var div = document.getElementById(imageColapsenm);
                        var img = document.getElementById('img' + imageColapsenm);
                        if (divOld != div) {
                            div.style.display = "none";
                            img.src = "../../Content/images/plus.gif";
                            img.alt = "Expand to show Questionarrie";
                        }
                    }
                }

            }


            return false;
        }
    </script>
    <script type="text/javascript">

        function GetTimeSheet() {

            var grid = document.getElementById("<%= GridView1.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            var Flag = 0;
            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid

                            if (cell.childNodes[j].checked) {
                                Flag = 1;
                            }

                        }
                    }
                }
            }
            if (Flag == 0) {

                alert("Please select at least one timesheet Week..");
                return false;
            }
            else {
                return true;
            }

        }


        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= GridView1.ClientID %>");
            //variable to contain the cell of the grid
            var cell;


            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;


                        }
                    }
                }
            }

        }
        function SelectAllTimeSheet(id) {
            debugger;
            var str = id;
            str = str.substring(0, str.indexOf("gvInnerTimeSheet"));
            var tbl = $('#' + id).parents("table:first");
            debugger;
            var tblid = tbl.get(0).id;
            //get reference of GridView control
            var grid = document.getElementById(tblid); //document.getElementById(str + "gvInnerTimeSheet");
            //variable to contain the cell of the grid
            var cell;


            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;


                        }
                    }
                }
            }

        }



      
       
       
    </script>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    NestedGridResponsiveCss();
    
 </script> 
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
