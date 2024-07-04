<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
    Title="Shift Details" Inherits="NewShift" Codebehind="NewShift.aspx.cs" %>
<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>

<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    
<%--    <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" /> --%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ManageShifts%> - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
  
      
      <asp:Panel ID="panelShift" runat="server">
          <div class="form-group row">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="addPanel" />
</div>
</div>
<div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <asp:Label ID="lblShift" runat="server" Text="Shift" /></label>
                                      <div class="col-sm-5">
                                          <asp:TextBox ID="txtShift" runat="server" SkinID="txt_90" />
                <asp:RequiredFieldValidator ID="reqShift" runat="Server" Text="*" ErrorMessage="Please enter the shift name"
                    ValidationGroup="addPanel" Display="Dynamic" ControlToValidate="txtShift"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
          <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <asp:Label ID="lblStartTime" runat="server" Text="Start Time"></asp:Label></label>
                                      <div class="col-sm-6"><asp:TextBox ID="txtStartTime" runat="server" SkinID="Time" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="Server" ErrorMessage="Please enter the starting time of the shift"
                    ValidationGroup="addPanel" Display="Dynamic" Text="*" ControlToValidate="txtStartTime">
                </asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
          <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <asp:Label ID="lblEndTime" runat="server" Text="End Time"></asp:Label></label>
                                      <div class="col-sm-6">
                                          <asp:TextBox ID="txtEndTime" runat="server" SkinID="Time" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server" ErrorMessage="Please enter the ending time of the shift"
                    ValidationGroup="addPanel" Display="Dynamic" Text="*" ControlToValidate="txtEndTime">
                </asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
          <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <asp:Label ID="lblColor" runat="server" Text="Colour"></asp:Label></label>
                                      <div class="col-sm-5">
                                          <asp:DropDownList ID="ddlColors" runat="server" CssClass="txt_field" SkinID="ddl_90">
                    <asp:ListItem Text="Please Select" Value="0" />
                    <asp:ListItem Text="" Value="Black" style="background-color: Black; color: white" />
                    <asp:ListItem Text="" Value="Maroon" style="background-color: #E3319D; color: white" />
                    <asp:ListItem Text="" Value="Olive" style="background-color: #667C26; color: white" />
                    <asp:ListItem Text="" Value="Navy" style="background-color: Navy; color: white" />
                    <asp:ListItem Text="" Value="Purple" style="background-color: #8E35EF; color: white" />
                    <asp:ListItem Text="" Value="Gray" style="background-color: #4C4646; color: white" />
                    <asp:ListItem Text="" Value="Silver" style="background-color: Silver; color: white" />
                    <asp:ListItem Text="" Value="Red" style="background-color: #E55451; color: white" />
                    <asp:ListItem Text="" Value="Green" style="background-color: #4CC417; color: white" />
                    <asp:ListItem Text="" Value="Lime" style="background-color: #41A317" />
                    <asp:ListItem Text="" Value="Yellow" style="background-color: #FFF380" />
                    <asp:ListItem Text="" Value="Aqua" style="background-color: #B6FFFF" />
                    <asp:ListItem Text="" Value="Blue" style="background-color: #6698FF" />
                    <asp:ListItem Text="" Value="White" style="background-color: White" />
                    <asp:ListItem Text="" Value="Teal" style="background-color: #4C7D7E" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="Server" ErrorMessage="Please enter the color for the shift"
                    ValidationGroup="addPanel" Display="Dynamic" Text="*" ControlToValidate="ddlColors"
                    InitialValue="0">
                </asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
          <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:Button ID="btnShift" runat="server" ValidationGroup="addPanel"
                    OnClick="btnShift_Click" SkinID="btnAdd" />
                <asp:Button ID="btnUpdateShift" runat="server"
                    ValidationGroup="addPanel" OnClick="btnUpdateShift_Click" 
                    SkinID="btnUpdate" />
                <asp:Button ID="btnCancelTeam" runat="server"
                    CausesValidation="false" SkinID="btnCancel" />
					</div>
				</div>
                </div>
           <div class="form-group row">
             <div class="col-md-12">
                  <asp:Label ID="lblMessage" runat="server" Text="" Style="color: Red" EnableViewState="false" />
                <asp:ObjectDataSource ID="datasourceColors" runat="server" SelectMethod="Enum.GetNames(typeof(System.Drawing.KnownColor))">
                </asp:ObjectDataSource>
                 </div>
               </div>
    
<asp:HiddenField ID="hiddenID" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlShift" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
        <asp:GridView ID="gridShift" runat="server" AutoGenerateColumns="False" DataSourceID="girdShiftDetails"
            DataKeyNames="ID" Width="100%" OnRowDataBound="gridShift_RowDataBound" OnRowDeleted="gridShift_RowDeleted"
            OnSelectedIndexChanging="gridShift_SelectedIndexChanging">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Select" />
                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="100px" ControlStyle-Height="20px" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblShift" runat="server" Text='<%#Eval("Colour")%>' />
                    </ItemTemplate>
                    <ControlStyle Height="20px" Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Shift" HeaderText="Shift" SortExpression="Shift" />
                <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_r">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Do you really want to delete.")'
                            SkinID="BtnLinkDelete" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="girdShiftDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
            DeleteCommand="UPDATE Shift SET Deleted=1 WHERE ID=@ID" SelectCommand="SELECT ID,[Shift], [StartTime], [EndTime], [Colour] FROM [Shift] WHERE ([PortfolioID] = @PortfolioID AND Deleted=0)"
            UpdateCommand="UPDATE Shift SET Shift = @Shift, StartTime = @StartTime, EndTime = @EndTime, Colour = @Colour WHERE ID=@ID">
            <SelectParameters>
                <asp:SessionParameter Name="PortfolioID" SessionField="portfolioID" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="ID" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Shift" />
                <asp:Parameter Name="StartTime" />
                <asp:Parameter Name="EndTime" />
                <asp:Parameter Name="Colour" />
                <asp:Parameter Name="ID" />
            </UpdateParameters>
        </asp:SqlDataSource>
</asp:Panel>
    
    
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
