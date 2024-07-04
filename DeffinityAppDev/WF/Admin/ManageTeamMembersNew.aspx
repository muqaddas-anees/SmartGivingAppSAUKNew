<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ManageTeamMembersNew" Codebehind="ManageTeamMembersNew.aspx.cs" %>

<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>
<%--<%@ Register Src="~/controls/PermissionsTab.ascx" tagname="permissionTab" tagprefix="uc3" %>--%>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <%--<uc3:permissionTab runat="server" ID="PTTab" />
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
       <%= Resources.DeffinityRes.Groups%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
                 <script type="text/javascript">

                     function SelectAllCheckboxes1(spanChk) {
                         var IsChecked = spanChk.checked;
                         var Chk = spanChk;
                         Parent = Chk.form.elements;
                         for (i = 0; i < Parent.length; i++) {
                             if (Parent[i].type == "checkbox" && Parent[i].id != Chk.id) {
                                 if (Parent[i].checked != IsChecked)
                                     Parent[i].click();
                             }
                         }
                     }
                     function SelectAllCheckboxesSpecific1(spanChk) {
                         var IsChecked = spanChk.checked;
                         var Chk = spanChk;
                         Parent = document.getElementById("<%=gridTeams.ClientID %>");
                         var items = Parent.getElementsByTagName('input');
                         for (i = 0; i < items.length; i++) {
                             if (items[i].id != Chk && items[i].type == "checkbox") {
                                 if (items[i].checked != IsChecked) {
                                     items[i].click();
                                 }
                             }
                         }
                     }
                     function SelectAllCheckboxes(spanChk) {
                         var IsChecked = spanChk.checked;
                         var Chk = spanChk;
                         Parent = Chk.form.elements;
                         for (i = 0; i < Parent.length; i++) {
                             if (Parent[i].type == "checkbox" && Parent[i].id != Chk.id) {
                                 if (Parent[i].checked != IsChecked)
                                     Parent[i].click();
                             }
                         }
                     }
                     function SelectAllCheckboxesSpecific(spanChk) {
                         var IsChecked = spanChk.checked;
                         var Chk = spanChk;
                         Parent = document.getElementById("<%=gridMembers.ClientID %>");
                         var items = Parent.getElementsByTagName('input');
                         for (i = 0; i < items.length; i++) {
                             if (items[i].id != Chk && items[i].type == "checkbox") {
                                 if (items[i].checked != IsChecked) {
                                     items[i].click();
                                 }
                             }
                         }
                     }

                     function SelectAllCheckboxesMoreSpecific(spanChk) {
                         var IsChecked = spanChk.checked;
                         var Chk = spanChk;
                         Parent = document.getElementById("<%=gridMembers.ClientID %>");
                         for (i = 0; i < Parent.rows.length; i++) {
                             var tr = Parent.rows[i];
                             //var td = tr.childNodes[0];			   		   
                             var td = tr.firstChild;
                             var item = td.firstChild;
                             if (item.id != Chk && item.type == "checkbox") {
                                 if (item.checked != IsChecked) {
                                     item.click();
                                 }
                             }
                         }
                     }


                     function HighlightRow(chkB) {
                         var IsChecked = chkB.checked;
                         if (IsChecked) {
                             chkB.parentElement.parentElement.style.backgroundColor = 'BlanchedAlmond';  // grdEmployees.SelectedItemStyle.BackColor
                             chkB.parentElement.parentElement.style.color = 'black'; // grdEmployees.SelectedItemStyle.ForeColor
                         }
                         else {
                             chkB.parentElement.parentElement.style.backgroundColor = 'white'; //grdEmployees.ItemStyle.BackColor
                             chkB.parentElement.parentElement.style.color = 'black'; //grdEmployees.ItemStyle.ForeColor
                         }
                     }

                     function HighlightRowTeam(chkB) {
                         var IsChecked = chkB.checked;
                         if (IsChecked) {
                             chkB.parentElement.parentElement.style.backgroundColor = '#CCFFCC';  // grdEmployees.SelectedItemStyle.BackColor
                             chkB.parentElement.parentElement.style.color = 'black'; // grdEmployees.SelectedItemStyle.ForeColor
                         }
                         else {
                             chkB.parentElement.parentElement.style.backgroundColor = 'white'; //grdEmployees.ItemStyle.BackColor
                             chkB.parentElement.parentElement.style.color = 'black'; //grdEmployees.ItemStyle.ForeColor
                         }
                     }
	
	
		</script>
    <asp:HiddenField ID="hdTeamID" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mdlSimpleDetails12" runat="server" CancelControlID="lnkCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnPopUp" PopupControlID="pnlSimpleDetails" />
                    <ajaxToolkit:ModalPopupExtender ID="mdlMembers" runat="server" CancelControlID="btnCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnShowModalPopup" PopupControlID="pnlAddMembers" />

    
  
    <asp:Panel ID="PnlInsert" runat="server" ScrollBars="Auto" Width="100%">

        <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="addPanel" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="Server" 
                                    ControlToValidate="txtTeamName" Display="None" 
                                    ErrorMessage="Please enter the group name" Text="*" ValidationGroup="addPanel"></asp:RequiredFieldValidator>
	</div>
</div>
        
        <div class="form-group">
          <div class="col-md-12">
               <asp:Button ID="btnPopUp" runat="server" SkinID="ImgLookUp" 
                                EnableViewState="false" AlternateText="Look up" Visible="False" />
                                 <asp:Panel ID="pnlSimpleDetails" runat="server" BackColor="White"
                                    Style="display: none" Height="300px" ScrollBars="Both"  Width="300px"  BorderStyle="Double" BorderColor="LightSteelBlue">
                                  
                                            <asp:GridView ID="grdTeams" runat="server" DataSourceID="DS_Otherteams" DataKeyNames="ID" AutoGenerateColumns="false" EmptyDataText="No Teams found" Width="100%">
                                            <Columns>
                                                <asp:TemplateField >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkItem" runat="server" Checked="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField Visible="false"><ItemTemplate><asp:Label ID="lblID" runat='server' Text='<%# Eval("ID") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                            <asp:TemplateField Visible="false"><ItemTemplate><asp:Label ID="lblPortfolioID" runat='server' Text='<%# Eval("PortfolioID") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Portfolio"><ItemTemplate><asp:Label ID="lblPortfolioName" runat='server' Text='<%# Eval("PortfolioName") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="50%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Team"><ItemTemplate><asp:Label ID="lblTeamName" runat='server' Text='<%# Eval("TeamName") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="80%" />
                                            </asp:TemplateField>
                                            </Columns>
                                            </asp:GridView>                                           
                                              <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
	</div>
</div>
                                     <div class="form-group">
          <div class="col-md-12 form-inline">
                <asp:Button ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" SkinID="btnSubmit"
                                                    ValidationGroup="Group11" />
                                               
                                                <asp:Button ID="lnkCancel" runat="server" Text="Close" SkinID="btnCancel" />
	</div>
</div>
                                               
                                           
                                              
                                           
                                    <asp:SqlDataSource ID="DS_Otherteams" runat="server" SelectCommand="Deffinity_Team_GetOTherPortfolio"
                                        SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        ProviderName="<%$ ConnectionStrings:DBstring.ProviderName %>">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="Portfolio" Name="PortfolioID" DefaultValue="0"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </asp:Panel>
	</div>
</div>

        <div class="form-group">
      <div class="col-md-8">
           <label class="col-sm-2 control-label"> <asp:Label ID="lblTeamNames1" runat="server" Text="Group Name"></asp:Label></label>
           <div class="col-sm-6">
                <asp:TextBox ID="txtTeamName" runat="server" CssClass="txt_field" SkinID="txt_90"></asp:TextBox>
            </div>
           <div class="col-sm-4 form-inline">
                 <asp:Button ID="btnAddTeam" runat="server" OnClick="btnAddTeam_Click"
                                    ValidationGroup="addPanel" SkinID="btnAdd" />
                                <asp:Button ID="btnCancelTeam" runat="server"
                                    CausesValidation="false" SkinID="btnCancel" />
               </div>
	</div>
	
</div>
       

        <div class="form-group">
          <div class="col-md-12">
               <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" onclick="LinkButton1_Click">Show&nbsp;All&nbsp;Groups</asp:LinkButton>
	</div>
</div>
      <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblMessage" runat="server"  EnableViewState="False"></asp:Label><asp:HiddenField
                            ID="HiddenField1" runat="server" />
	</div>
</div>
        <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="Label1" runat="server" EnableViewState="False"></asp:Label>
              <asp:LinkButton ID="lnkDeleteTeam" runat="server" OnClientClick='return confirm("Are you sure you would like to delete the selected group(s)?")'  Font-Bold="true" 
                             onclick="lnkDeleteTeam_Click">Delete</asp:LinkButton>
	</div>
</div>
       <asp:GridView ID="gridTeams" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gridTeams_PageIndexChanging"
                HeaderStyle-HorizontalAlign="Center" OnRowDeleting="gridTeams_RowDeleting" Width="100%">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="20px">
                        <HeaderStyle Width="20px" />
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSelectAll1" onclick="javascript:SelectAllCheckboxesSpecific1(this);"
                                runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelectAll1" onclick="javascript:HighlightRowTeam(this);" runat="server" />
                            <asp:Label ID="lblID1" Text='<%# Bind("ID") %>' runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Group Name">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                            <asp:LinkButton ID="btnViewMembers1" runat="server" CommandArgument='<%# Bind("ID") %>'
                                Text='<%#Eval("TeamName")%>' OnClick="btnViewMembers_Click"></asp:LinkButton>
                            <asp:Label ID="litTeamName" Text='<%#Eval("TeamName")%>' runat="server" Width="100%"
                                Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Number of Members" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Literal ID="litTeamSize" Text='<%#Eval("TeamCount")%>' runat="server" />
                            <asp:HiddenField runat="server" ID="hidTeamID" Value='<%# Bind("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            
                                        <asp:LinkButton ID="btnAddResource" runat="server" CommandArgument='<%# Bind("ID") %>'
                                            Text="Add Resources" OnClick="btnAddResource_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_r">
                        <ItemTemplate>
                           
                                        <asp:LinkButton ID="btnViewMembers" runat="server" CommandArgument='<%# Bind("ID") %>'
                                            Text="View Members" OnClick="btnViewMembers_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r" Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDeleteTeam" runat="server" SkinID="BtnLinkDelete"
                                CommandName="Delete" OnClientClick='return confirm("Team and its related members will be deleted.\nAre you sure to delete?")' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </asp:GridView>
                    
                    
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlTeamMembers">
                        <div class="form-group">
        <div class="col-md-12">
           <strong>Members within this Group</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                    <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
	</div>
</div>
                   
                     <div>&nbsp;<asp:LinkButton ID="lnkDelete" runat="server" OnClick="ImgDelete_Click" Font-Bold="true">Delete</asp:LinkButton></div><div style="width:130px;float:right">
                        <asp:LinkButton ID="LinkButton2" runat="server"  Font-Bold="true" Visible="false"
                            onclick="LinkButton2_Click">Show All Members</asp:LinkButton></div>
                        <asp:GridView ID="gridMembers" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center"
                            Width="100%" EmptyDataText="No Records Found" PageSize="10" OnPageIndexChanging="gridMembers_PageIndexChanging"
                            OnRowDataBound="gridMembers_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="20px">
                                    <HeaderStyle CssClass="header_bg_l" Width="20px" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbSelectAll" onclick="javascript:SelectAllCheckboxesSpecific(this);"
                                            runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelectAll" onclick="javascript:HighlightRow(this);" runat="server" />
                                        <asp:Label ID="lblID" Text='<%# Bind("Name") %>' runat="server" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemStyle Width="100px" />
                                    <ItemTemplate>
                                        <asp:Literal ID="litTeamSize" Text='<%#Eval("memName")%>' runat="server" Visible="false" />
                                        <asp:LinkButton ID="btnViewMembers11" runat="server" Text='<%#Eval("memName")%>'
                                            OnClick="btnViewMembers11_Click"></asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hidTeamID11" Value='<%# Bind("CID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Email" DataField="EmailAddress" ItemStyle-Width="75px">
                                    <ItemStyle Width="75px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Contact Number" DataField="ContactNumber"
                                    ItemStyle-Width="75px">
                                    <ItemStyle Width="75px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:GridView>
   </asp:Panel>
            <div class="clr"></div>
        <asp:Panel runat="server" ID="Panel2" Visible="false">
            <div class="tab_header_Bold">
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>-is a member of the following Customers and Groups:
            </div>
            <div>
                <asp:GridView ID="grdCustomers" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
                    <Columns>
                        <asp:TemplateField HeaderText="Customers" HeaderStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("PortFolio") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
           
                <asp:GridView ID="grdGroups" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
                    <Columns>
                        <asp:TemplateField HeaderText="Groups" HeaderStyle-Width="200px">
                            <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("TeamName") %>'></asp:Label></ItemTemplate>
                           
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            
        </asp:Panel>
           
                    <asp:Panel runat="server"  ID="pnlAddMembers" BackColor="White"  Style="display: none" Height="350px" ScrollBars="None"
                     Width="550px"  BorderStyle="Double" BorderColor="LightSteelBlue">
                        <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.SelectTeamMembers%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
               <div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="pnlInsert" />
               <asp:Label ID="lblErrMsg" runat="server"  ForeColor="Red" EnableViewState="false"></asp:Label>
	</div>
</div>   
                        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><asp:Label ID="lblTeam" runat="server" Text="Team"></asp:Label></label>
           <div class="col-sm-8">
               <asp:Label ID="lblTeamsName" runat="server" Text="" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
                        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Search%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtSearch" runat="server" Text=""></asp:TextBox> 
                            <asp:Button ID="imgSearch"   OnClick="imgSearch_Click" runat="server" SkinID="btnSearch" />
                             <asp:Button ID="ImageButton2"   OnClick="lnkShowAll_Click" runat="server" SkinID="btnDefault" Text="View All" />
            </div>
	</div>
</div>
                        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                <span style="color: red">*</span></label>
           <div class="col-sm-8">
               <asp:Panel Width="300px" runat="server" ID="Panel1" Height="75px" CssClass="txt_field" BorderStyle="Inset">
        <evy:ScrollableListBox ID="CheckBoxList2" runat="server" BorderWidth="0px" 
        RepeatLayout="Flow" Height="75px" Width="300px" ></evy:ScrollableListBox>
              </asp:Panel>
            </div>
	</div>
</div>
                        <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8 form-inline">
               <asp:Button runat="server" ID="btnShowModalPopup" style="display:none"/>
                <asp:Button ID="btnAddTeamMember" runat="server"
                                    OnClick="btnAddTeamMember_Click" ValidationGroup="pnlInsert" 
                                    SkinID="btnAdd" />
                               
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" 
                                    SkinID="btnCancel" />
            </div>
	</div>
</div>
                   
                    
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


