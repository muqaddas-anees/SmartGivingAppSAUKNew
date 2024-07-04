<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ApproveTimesheets.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Timesheets.ApproveTimesheets" %>
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
    Approve Timesheets
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
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
                             img.src = "../../../Content/images/plus.gif";
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

        function TimsheetFun() {

            var grid1 = document.getElementById("<%=GridView4.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            var Flag = 0;
            if (grid1.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid1.rows.length; i++) {
                    //get the reference of first column
                    cell = grid1.rows[i].cells[0];

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

        function SelectAll1(id) {
            //get reference of GridView control
            var grid1 = document.getElementById("<%=GridView4.ClientID %>");
            //variable to contain the cell of the grid
            var cell;


            if (grid1.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid1.rows.length; i++) {
                    //get the reference of first column
                    cell = grid1.rows[i].cells[0];

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
    <div class="form-group row">
        <div class="col-md-12">
     <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
            </div>
        </div>
    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" PageSize="5" 
                                            AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" GridLines="None"
                                            AllowSorting="True" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound"
                                            EmptyDataText="No timesheet data found.">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="9px">
                                                    <ItemTemplate>
                                                        <a id="imageColapse" href="javascript:expandcollapse('div<%# Eval("WCDateID") %>', 'one');">
                                                            <img id='imgdiv<%# Eval("WCDateID") %>' alt="Click to show/hide <%# Eval("WCDateID") %>"
                                                                width="9px" border="0" src="../../../../Content/images/plus.gif" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWCDateID" runat="server" Width="30px" Text='<%# Bind("WCDateID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User" SortExpression="ContractorName">
                                                    <HeaderStyle />
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName" runat="server" Width="120px" Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resource Name" SortExpression="ContractorName" Visible="false">
                                                    <ItemStyle Width="25%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorID1" runat="server" Text='<%# Bind("ContractorID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" SortExpression="WCDate">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("WCDate","{0:d}") %>' Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Hours">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalHours" runat="server" Text='<%# ChangeToHours(Eval("TotalHours").ToString())%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Approver">
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrimary" runat="server" Text='<%# Bind("PrimeApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Secondary Approver" Visible="false">
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSecondary" runat="server" Text='<%# Bind("SecondApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Approver" Visible="false">
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerApproverName" runat="server" Text='<%# Bind("CustomerApproverName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" Visible="false">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_view" runat="server" CausesValidation="false" CommandName="View"
                                                            CommandArgument='<%# Bind("WCDateID")%>'  SkinID="BtnLinkHistory" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                               <%-- <div id='div<%# Eval("WCDateID") %>' style="overflow-x:auto;display:none;">--%>
                                                                 <div id='div<%# Eval("WCDateID") %>' style="overflow-x:auto;width:75%">
                                                                    <asp:GridView ID="gvInnerTimeSheet" SkinID="gv_nested" runat="server" OnRowDataBound="gvInnerTimeSheet_RowDataBound" 
                                                                        OnRowCommand="gvInnerTimeSheet_RowCommand" AutoGenerateColumns="false" PageSize="10" 
                                                                        OnRowEditing="gvInnerTimeSheet_RowEditing" DataKeyNames="WCDateID"
                                                                        OnRowCancelingEdit="gvInnerTimeSheet_RowCancelingEdit" OnRowUpdating="gvInnerTimeSheet_RowUpdating" ShowFooter="true">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAll1" runat="server" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect2" runat="server" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <HeaderStyle Width="65px" />
                                                                                <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                                                        CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                                                                                        ToolTip="Update"></asp:LinkButton>
                                                                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStatusName" runat="server" Width="40px" Text='<%# Bind("StatusName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Width="40px" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Resource Name" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContractorID" runat="server" Width="40px" Text='<%# Bind("ContractorID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Resource Name" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntryType" runat="server" Width="40px" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Date">

                                                                                <HeaderStyle Width="80px" />
                                                                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                                                                        ></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField HeaderText="Pref" Visible="false">
                                                                                <HeaderStyle Width="60px" />
                                                                                <ItemStyle Width="80px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPref" runat="server" Width="80px" Text='<%# Bind("Pref") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Job" ItemStyle-CssClass="col-nowrap" FooterStyle-CssClass="form-inline">
                                                                                <HeaderStyle Width="160px" />
                                                                                <ItemStyle Width="50%" />
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblProjectTitle" runat="server" Width="140px" Text='<%# Bind("ProjectReference") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:TextBox ID="txtComments" runat="server" SkinID="txt_50" Visible="true"></asp:TextBox>
                                                                                     <asp:Button ID="btn_approve1" runat="server" SkinID="btnDefault" Text="Approve" CommandName="ApproveClick"/>
                                                                                    <asp:Button ID="btn_declined1" CommandName="DeclineClick"
                                                                                        runat="server" SkinID="btnDefault" Text="Decline" Visible="false" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                          <%--  <asp:TemplateField HeaderText="Hours for this T/S" Visible="false">
                                                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHours" runat="server" Text='<%# ChangeToHours(Eval("Hours").ToString())%>'></asp:Label>
                                                                                   
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                   
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>--%>
                                                                           <%-- <asp:TemplateField HeaderText="Signed Off Hrs" Visible="false">
                                                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSignedOffHrs" runat="server" Text='<%#  ChangeToHours(Eval("SignedOffHours").ToString()) %>'></asp:Label>
                                                                                    <asp:LinkButton ID="lnkSignedoff" runat="server" CommandName="viewSignoff" CommandArgument='<%# Eval("WCDateID").ToString() + "," + Eval("ContractorID").ToString() %>' Text='<%#  ChangeToHours(Eval("SignedOffHours").ToString()) %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Duration">
                                                                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                                                                <ItemTemplate>
                                                                                    <%--<asp:Label ID="lblTotalHoursBooked" runat="server" Text='<%#  ChangeToHours(Eval("TotalHoursBooked").ToString()) %>'></asp:Label>--%>
                                                                                     <asp:LinkButton ID="lnkHours" runat="server" CommandName="viewSignoff" CommandArgument='<%# Eval("WCDateID").ToString() + "," + Eval("ContractorID").ToString() %>' Text='<%# ChangeToHours(Eval("TotalHoursBooked").ToString())%>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                          <%--  <asp:TemplateField HeaderText="Task" ItemStyle-CssClass="col-nowrap">
                                                                                <HeaderStyle Width="140px" />
                                                                                <ItemStyle Width="130px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblProjectTask" runat="server" Text='<%# Bind("ProjectTask") %>' Width="140px"></asp:Label>
                                                                                </ItemTemplate>


                                                                            </asp:TemplateField>--%>
                                                                             <asp:TemplateField HeaderText="Status">
                                                                            <ItemStyle Width="60px" />
                                                                            <HeaderStyle Width="60px" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatusName1" runat="server" Text='<%# Bind("StatusName") %>' Width="75px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Notes">
                                                                                <ItemStyle Width="140px" />
                                                                                <HeaderStyle Width="140px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblnotes" runat="server" Text='<%# Bind("Notes") %>' Width="140px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Approver's Comments">
                                                                                <ItemStyle Width="140px" />
                                                                                <HeaderStyle Width="140px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAprComnts" runat="server" Text='<%# Bind("ApproverComments") %>' Width="140px"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtApproveComments" runat="server" Text='<%# Bind("ApproverComments") %>' Width="140px"></asp:TextBox>

                                                                                </EditItemTemplate>
                                                                            </asp:TemplateField>
                                                                          <%--  <asp:TemplateField HeaderText="Planner" Visible="false">
                                                                                <ItemStyle Width="25px" />
                                                                                <HeaderStyle Width="25px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblplanner" runat="server" Text='<%# ChangePlanner(Eval("Planner").ToString())%>'
                                                                                        Width="25px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Shift" Visible="false">
                                                                                <HeaderStyle Width="100px" HorizontalAlign="Center"  />
                                                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShift" runat="server" Width="100px" Text='<%# Bind("Shift") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>

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


     <asp:Label ID="BtntempSearchModal" runat="server" />
    <asp:Label ID="Label1" runat="server" />
     <ajaxToolkit:ModalPopupExtender ID="ModalControlExtender2"
        BackgroundCssClass="modalBackground" runat="server" TargetControlID="BtntempSearchModal" CancelControlID="Label1"
        PopupControlID="GetViewofSendforApproval">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel Style="display: none; overflow: scroll;" ID="GetViewofSendforApproval" runat="server" BackColor="White" BorderStyle="Double" Width="85%" Height="350px" BorderColor="LightSteelBlue" ScrollBars="Auto">
       <div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.TimesheetViewer %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                Width="100%" EmptyDataText="No timesheet data found" AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging"
                                                OnRowDataBound="GridView4_RowDataBound" OnRowCommand="GridView4_RowCommand" OnRowEditing="GridView4_RowEditing"
                                                OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowUpdating="GridView4_RowUpdating" Font-Size="Smaller">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll1" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect2" runat="server" />
                                                        </ItemTemplate>
                                                       
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle Width="65px" />
                                                        <ItemStyle Width="65px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                                CommandArgument='<%# Bind("ID")%>' ValidationGroup="Group2" SkinID="BtnLinkUpdate"
                                                                ToolTip="Update"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Width="40px" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Resource Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContractorID" runat="server" Width="40px" Text='<%# Bind("ContractorID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Resource Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEntryType" runat="server" Width="40px" Text='<%# Bind("EntryType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="60px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSite111" runat="server" Width="80px" Text='<%# Bind("Site") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pref" Visible="false">
                                                        <HeaderStyle Width="60px" />
                                                        <ItemStyle Width="80px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPref" runat="server" Width="80px" Text='<%# Bind("Pref") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Title" ItemStyle-CssClass="col-nowrap">
                                                        <HeaderStyle Width="160px" />
                                                        <ItemStyle Width="160px" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlTitle" runat="server" Width="140px" AutoPostBack="true"
                                                                >
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTitle" runat="server" Width="140px" Text='<%# Bind("ProjectReference") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hours">
                                                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtHours" runat="server" Text='<%# ChangeToHours(Eval("Hours").ToString())%>'
                                                                Width="40px" SkinID="Price" MaxLength="5"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtHours"
                                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="Group2"
                                                                Display="None" Text="*" ErrorMessage="Please enter valid time. Format hh:mm "></asp:RegularExpressionValidator>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>


                                                            <asp:Label ID="lblHours" runat="server" Text='<%# ChangeToHours(Eval("Hours").ToString())%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task" ItemStyle-CssClass="col-nowrap" Visible="false">
                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTask" runat="server" Text='<%# Bind("ProjectTask") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:HiddenField ID="GetTaskID" runat="server" Value='<%# Bind("Pref") %>' />
                                                            <asp:DropDownList ID="GetProjectTasks" Width="120px" runat="server">
                                                            </asp:DropDownList>


                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SR">
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle Width="60px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSR" runat="server" Text='<%# Bind("SRnumber") %>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemStyle Width="140px" />
                                                        <HeaderStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotes" runat="server" Text='<%# Bind("Notes") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approver's Comments">
                                                        <ItemStyle Width="140px" />
                                                        <HeaderStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAprComnts" runat="server" Text='<%# Bind("ApproverComments") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Planner">
                                                        <ItemStyle Width="25px" />
                                                        <HeaderStyle Width="25px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblplanner" runat="server" Text='<%# ChangePlanner(Eval("Planner").ToString())%>'
                                                                Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shift">
                                                        <HeaderStyle Width="100px" HorizontalAlign="Center" CssClass="header_bg_r" />
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShift" runat="server" Width="100px" Text='<%# Bind("Shift") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
          <div id="GetIDAccept" runat="server" visible="false" class="form-group row" >
                                        <div class="col-md-12 form-inline">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="300px" Visible="true"></asp:TextBox>
                                           <asp:Button ID="btn_approve" runat="server" SkinID="btnDefault" Text="Approve" OnClick="btn_approve_Click"
                                                 OnClientClick="return TimsheetFun();" /><asp:Button ID="btn_declined"
                                                    runat="server" SkinID="btnDefault" Text="Decline" OnClick="btn_declined_Click" OnClientClick="return TimsheetFun();" />
                                           
                            
                                        </div>
                                        
                                    </div>

                                    <div  class="form-group row" >
                                        <div class="col-md-12 form-inline" style="text-align: right">
                                            <asp:Button ID="ImgClose"
                                                runat="server" SkinID="btnCancel"  />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                             <asp:HiddenField ID="HiddenField3" runat="server" />
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                        </div>
                                    </div>
    </asp:Panel>
    <div>
        <asp:Button ID="btnshow" runat="server" ClientIDMode="Static" style="display:none;" />
         <ajaxtoolkit:modalpopupextender id="mdlSignoff" cancelcontrolid="imgSignoffClose"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="btnshow"
                        popupcontrolid="pnlTimesheetSignoffhours"></ajaxtoolkit:modalpopupextender>
          <asp:Panel ID="pnlTimesheetSignoffhours" runat="server" BackColor="White" Style="display: none"
                        Width="700px" Height="400px" BorderStyle="Double" BorderColor="LightSteelBlue">
              <div class="form-group row">
        <div class="col-md-10">
           <strong>Timesheet sign off & booked details </strong> 
            <hr class="no-top-margin" />
            </div>

<div class="col-md-2">
     <asp:LinkButton ID="imgSignoffClose" runat="server" SkinID="BtnLinkCancel" />
</div>
    </div>
                       
                       <asp:Panel ID="pnlInl" runat="server" Height="350px" Width="100%">
                                <asp:GridView ID="gvSignoff" runat="server" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Project Reference">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectRef" runat="server" Text='<%# Bind("ProjectReference") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Title" ItemStyle-CssClass="col-nowrap">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Project Owner">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectOwner" runat="server" Text='<%# Bind("OwnerName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Hours Booked" ItemStyle-HorizontalAlign="Right" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblBooked" runat="server" Text='<%# ChangeToHours(Eval("BookedHours").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Hours Signed" HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSigned" runat="server" Text='<%# ChangeToHours(Eval("SignoffHours").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>    
              </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
