<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Admin_UsersJournal" Codebehind="UsersTrackJournal.aspx.cs" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Journal
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">


  
<div class="form-group" style="display:none;">
                                  <div class="col-md-12">
                                       <label class="col-sm-5 control-label"> Number of Concurrent Users Currently Logged In:</label>
                                      <div class="col-sm-7"><asp:Label ID="lblNoOfUsersOnline" runat="server" Font-Bold="true" > </asp:Label>
					</div>
				</div>
</div>
<!--Search Block //-->
        <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.PageTitle%></label>
                                      <div class="col-sm-8">
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Users%></label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlUsers" runat="server" DataValueField="ID" DataTextField="ContractorName" SkinID="ddl_80" AppendDataBoundItems="true">
<asp:ListItem Text="ALL" Value="0"></asp:ListItem>
</asp:DropDownList> 
    <asp:ObjectDataSource ID="obj_Users" runat="server" 
        SelectMethod="UserSelectAll" TypeName="Deffinity.Bindings.DefaultDatabind">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="Active" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
					</div>
				</div>
</div>
<div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FromDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
<asp:Label ID="imgFromCalender" runat="server" SkinID="Calender" />
<ajaxToolkit:calendarextender id="CalendarExtender3_F" runat="server"  popupbuttonid="imgFromCalender"
                           targetcontrolid="txtFromDate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ToDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txtToDate" runat="server" SkinID="Date"></asp:TextBox>
<asp:Label ID="imgToCalender" runat="server" SkinID="Calender" />
<ajaxToolkit:calendarextender id="CalendarExtender1" runat="server"  popupbuttonid="imgToCalender"
                           targetcontrolid="txtToDate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
					</div>
				</div>
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8 form-inline pull-right"><asp:Button ID="btnView" runat="server" SkinID="btnDefault" Text="View" />
                                          <asp:Button ID="btnClear" runat="server" SkinID="btnClear" 
        onclick="btnClear_Click"  />
					</div>
				</div>
</div>


<!-Add delete button //-->
<div style="padding-left:20px;height:20px;vertical-align:middle;"> <asp:LinkButton ID="btn_delete" runat="server" Text="Delete" Font-Bold="true" 
        onclick="btn_delete_Click"  OnClientClick="return confirm('Do you want to delete the journal record(s)?');"></asp:LinkButton> </div>
<!-Grid view //-->
<asp:GridView ID="Grid_Journal" runat="server" Width="100%" 
        DataSourceID="obj_journal" onrowdatabound="Grid_Journal_RowDataBound" 
        AllowPaging="True" PageSize="20">
<Columns>
<asp:TemplateField HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="50px" >
<HeaderTemplate>
<asp:CheckBox ID="cbSelectAll" runat="server" />
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="cbSelectAll" runat="server" />
<asp:HiddenField ID="HID" runat="server" Value='<%# Eval("ID") %>' />
</ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="PageTitle" HeaderText="Page Title" />
<asp:BoundField DataField="UserName" HeaderText="User Name"  HeaderStyle-Width="250px"  />
<asp:BoundField DataField="AccessedDate" DataFormatString="{0:d}" 
        HtmlEncode="false" HeaderText="Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px"   >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="AccessedDate" DataFormatString="{0:HH:mm}" 
        HtmlEncode="false" HeaderText="Time" HeaderStyle-CssClass="header_bg_r"   
        ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="150px" >
<HeaderStyle CssClass="header_bg_r"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
    </asp:BoundField>
</Columns>
</asp:GridView>
    <asp:ObjectDataSource ID="obj_journal" runat="server" 
        SelectMethod="PageJournal_Select" TypeName="PageJornal">
        <SelectParameters>
        <asp:ControlParameter Name="FromDate" ControlID="txtFromDate" PropertyName="Text" DefaultValue="" />
        <asp:ControlParameter Name="Todate" ControlID="txtToDate" PropertyName="Text" DefaultValue="" />
        <asp:ControlParameter Name="UserID" ControlID="ddlUsers" PropertyName="SelectedValue" DefaultValue="0" />
        <asp:ControlParameter Name="PageTitle" ControlID="txtPageTitle" PropertyName="Text" DefaultValue="" />
        </SelectParameters>
        </asp:ObjectDataSource>
    <script type="text/javascript">
<!--

     function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= Grid_Journal.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
           
        }  
-->
</script>
    <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
            $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>

