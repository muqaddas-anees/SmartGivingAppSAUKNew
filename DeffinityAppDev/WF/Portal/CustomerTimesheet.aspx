<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="CustomerPortalTimesheet" Codebehind="CustomerTimesheet.aspx.cs" %>
<%--<%@ MasterType VirtualPath="~/DeffinityTab.master" %>--%>
<%--<%@ Register Src="~/controls/CustomerHomeTabs.ascx" TagName="CustomerTabs" TagPrefix="uc3" %>--%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ProgressImage" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Timesheet%> - <%= Resources.DeffinityRes.AwaitingApproval%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
<%-- <uc3:CustomerTabs ID="CustomerTabs1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <script type="text/javascript">

        function GetTimeSheet() {

            var grid = document.getElementById("<%= GridCustomerWcdate.ClientID %>");
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
        function GetTimeSheet_sub() {

            var grid = document.getElementById("<%= GridView4.ClientID %>");
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
            var grid = document.getElementById("<%= GridCustomerWcdate.ClientID %>");
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
        function SelectAll_sub(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= GridView4.ClientID %>");
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
       


<div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
	</div>

</div>
  <%--  <div class="form-group row">
        <div class="col-md-12">
         <h5>  <strong></strong> </h5>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
   
    
<div class="form-group row">
      <div class="col-md-12">
          <asp:Button ID="btn_ApprovalAll" runat="server" Text="Approve All"
                                             OnClientClick="return GetTimeSheet();" 
                                 onclick="btn_ApprovalAll_Click"></asp:Button>
	</div>

</div>
           
          <asp:GridView ID="GridCustomerWcdate" runat="server" AutoGenerateColumns="false" 
                              Width="100%" PageSize="20" 
                                             EmptyDataText="No timesheet data found." 
                              onrowcommand="GridCustomerWcdate_RowCommand" 
                              onrowdatabound="GridCustomerWcdate_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderStyle CssClass="header_bg_l" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                        <asp:HiddenField ID="Hwcdateid" runat="server" Value='<%# Bind("WCDateID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWCDateID" runat="server" Width="30px" Text='<%# Bind("WCDateID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resource" SortExpression="ContractorName">
                                                    <HeaderStyle  />                                                    
                                                    <ItemStyle Width="200px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName" runat="server" Width="250px" Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resource Name" SortExpression="ContractorName" Visible="false">
                                                    <ItemStyle Width="120px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorID1" runat="server" Text='<%# Bind("ContractorID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Week Commencing Date" SortExpression="WCDate">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWCDate" runat="server" Text='<%# Bind("WCDate","{0:d}") %>' Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Hours">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                          <asp:Label ID="lblTotalHours" runat="server" Text='<%# ChangeHours(Eval("TotalHours").ToString())%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="View">
                                                    <HeaderStyle Width="50px" CssClass="header_bg_r" />
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_view" runat="server" CausesValidation="false" CommandName="View"
                                                            CommandArgument="<%# Bind('WCDateID')%>" SkinID="BtnLinkView" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
              <asp:Label ID="BtntempSearchModal" runat="server" />
    <asp:Label ID="Label1" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="ModalControlExtender2" 
        BackgroundCssClass="modalBackground" runat="server" TargetControlID="BtntempSearchModal" CancelControlID="Label1"
        PopupControlID="GetViewofSendforApproval">
    </ajaxToolkit:ModalPopupExtender>
    
        <asp:Panel style="display:none; overflow:scroll;" ID="GetViewofSendforApproval" runat="server" BackColor="White"  BorderStyle="Double" Width="85%" Height="350px"  BorderColor="LightSteelBlue" ScrollBars="Auto">
            <div class="form-group row">
        <div class="col-md-12">
           <strong>Timesheet Viewer </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
             <asp:Panel ID="gridPanel" runat="server" ScrollBars="Auto"></asp:Panel>
                                            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" PageSize="10"
                                                Width="100%" EmptyDataText="No timesheet data found"                                                 
                                                 Font-Size="Smaller" onrowdatabound="GridView4_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll1" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect2" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="header_bg_l" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
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
                                                    <asp:TemplateField HeaderText="Project Title">
                                                        <HeaderStyle Width="160px" />
                                                        <ItemStyle Width="160px" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTitle" runat="server" Width="140px" Text='<%# Bind("Projectreference") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hours">
                                                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                                                       
                                                        <ItemTemplate>
                                                            
                                                            
                                                              <asp:Label ID="lblHours" runat="server" Text='<%# ChangeHours(Eval("Hours").ToString())%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <HeaderStyle Width="140px" />
                                                        <ItemStyle Width="130px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectTask" runat="server" Text='<%# Bind("ProjectTask") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                                 <EditItemTemplate>
                                            <asp:HiddenField id="GetTaskID" runat="server" Value='<%# Bind("Pref") %>'/>
                                            <asp:DropDownList ID="GetProjectTasks" Width="120px" runat="server" 
                                                >
                                        </asp:DropDownList>

                                        
                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemStyle Width="140px" />
                                                        <HeaderStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnotes" runat="server" Text='<%# Bind("Notes") %>' Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        
             <asp:HiddenField ID="HiddenField3" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" runat="server" />

            
<div class="form-group row">
      <div class="col-md-4">
           <asp:TextBox ID="txtComments" runat="server" Width="300px"  Visible="true"></asp:TextBox> 
	</div>
	<div class="col-md-4">
          <asp:Button ID="btn_approve" runat="server" SkinID="btnDefault" Text="Approve" OnClick="btn_approve_Click"
                                          OnClientClick="return TimsheetFun();" /><asp:Button ID="btn_declined"
                                                runat="server" Text="Decline" SkinID="btnDefault" OnClientClick="return TimsheetFun();" OnClick="btn_declined_Click"  /> 
        <asp:Button ID="ImgClose"
                                                runat="server" SkinID="btnCancel"  />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
	</div>
	<div class="col-md-4">
          
	</div>
</div>
       
        </asp:Panel>  
       
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
  
       
</asp:Content>

