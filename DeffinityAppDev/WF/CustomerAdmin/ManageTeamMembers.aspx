<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" Inherits="Manage_TeamMembers" Codebehind="ManageTeamMembers.aspx.cs" %>
<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>
<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>
<%@ Register src="controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.Team%> - <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:HiddenField ID="hdTeamID" runat="server" />

    <ajaxToolkit:ModalPopupExtender ID="mdlSimpleDetails12" runat="server" CancelControlID="lnkCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnPopUp" PopupControlID="pnlSimpleDetails" />
                    <ajaxToolkit:ModalPopupExtender ID="mdlMembers" runat="server" CancelControlID="btnCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnShowModalPopup" PopupControlID="pnlAddMembers" />
    <asp:Panel ID="PnlInsert" runat="server" Width="100%">
        <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="addPanel" />
              <asp:Label ID="lblmsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
	</div>
</div>
        <div class="form-group pull-right">
          <div class="col-md-12 pull-right">
              <asp:Button ID="btnPopUp" runat="server" SkinID="btnDefault" Text="Look Up"
                                EnableViewState="false" AlternateText="Look up" />
                                 <asp:Panel ID="pnlSimpleDetails" runat="server" BackColor="White"
                                    Style="display: none" Height="400px" ScrollBars="Both"  Width="450px"  BorderStyle="Double" BorderColor="LightSteelBlue">
                                     <asp:GridView ID="grdTeams" runat="server" DataSourceID="DS_Otherteams" DataKeyNames="ID" AutoGenerateColumns="false" EmptyDataText="No Teams found" Width="100%">
                                            <Columns>
                                                <asp:TemplateField >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkItem" runat="server" Checked="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField Visible="false"><ItemTemplate><asp:Label ID="lblID" runat='server' Text='<%# Eval("ID") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                            <asp:TemplateField Visible="false"><ItemTemplate><asp:Label ID="lblPortfolioID" runat='server' Text='<%# Eval("PortfolioID") %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer"><ItemTemplate><asp:Label ID="lblPortfolioName" runat='server' Text='<%# Eval("PortfolioName") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="50%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Team"><ItemTemplate><asp:Label ID="lblTeamName" runat='server' Text='<%# Eval("TeamName") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="80%" />
                                            </asp:TemplateField>
                                            </Columns>
                                            </asp:GridView>
                                     <div class="form-group row">
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
        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"> <asp:Label ID="lblTeamNames1" runat="server" Text="Team Name"></asp:Label>
                                <span style="color: red">*</span></label>
           <div class="col-sm-6">
               <asp:TextBox ID="txtTeamName" runat="server" CssClass="txt_field" SkinID="txt_90"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="Server" 
                                    ControlToValidate="txtTeamName" Display="Dynamic" 
                                    ErrorMessage="Please enter the team name" Text="*" ValidationGroup="addPanel"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>

        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-9 form-inline">
                <asp:Button ID="btnAddTeam" runat="server" OnClick="btnAddTeam_Click"
                                    ValidationGroup="addPanel" SkinID="btnAdd" />
                                &nbsp;
                                <asp:Button ID="btnCancelTeam" runat="server"
                                    CausesValidation="false" SkinID="btnCancel" />
            </div>
	</div>
</div>
        
                   
                     <asp:GridView ID="gridTeams" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                    OnPageIndexChanging="gridTeams_PageIndexChanging" 
                                    HeaderStyle-HorizontalAlign="Center" onrowdeleting="gridTeams_RowDeleting" 
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Available Team(s)" HeaderStyle-CssClass="header_bg_l">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                                             
                                                <asp:Label ID="litTeamName" Text='<%#Eval("TeamName")%>' runat="server" Width="100%" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="header_bg_l" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Team Size">
                                            <ItemTemplate>
                                                <asp:Literal ID="litTeamSize" Text='<%#Eval("TeamCount")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hidTeamID" Value='<%# Bind("ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="btnAddResource" runat="server" CommandArgument='<%# Bind("ID") %>' Text="Add Resources" OnClick="btnAddResource_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="btnViewMembers" runat="server" CommandArgument='<%# Bind("ID") %>' Text="View Members" OnClick="btnViewMembers_Click" ></asp:LinkButton>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDeleteTeam" runat="server" SkinID="BtnLinkDelete"
                                                 CommandName="Delete" OnClientClick='return confirm("Team and its related members will be deleted.\nAre you sure to delete?")'/>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="header_bg_r" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:GridView>
                   
        <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label>
	</div>
</div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlTeamMembers">
                        <div class="form-group row">
        <div class="col-md-12">
           <strong> Team Members</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                    <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label><br />
              <asp:LinkButton ID="lnkDelete" runat="server" OnClick="ImgDelete_Click" Font-Bold="true">Delete</asp:LinkButton>
	</div>
</div>
                 
                    
                     <asp:GridView ID="gridMembers" runat="server" AutoGenerateColumns="false"
                                    
                                    AllowPaging="true" 
            Width="100%" EmptyDataText="No Records Found" PageSize="10" 
                            onpageindexchanging="gridMembers_PageIndexChanging" 
                            onrowdatabound="gridMembers_RowDataBound"  >
                                    <Columns>
                        
                                        
<asp:TemplateField  HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">

   
       <ItemStyle HorizontalAlign="Center" Width="20px" />
                  <HeaderTemplate>
                <asp:CheckBox ID="cbSelectAll" onclick="javascript:SelectAllCheckboxesSpecific(this);"   runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                <asp:CheckBox ID="cbSelectAll" onclick="javascript:HighlightRow(this);"  runat="server" />
                <asp:Label ID="lblID" Text='<%# Bind("Name") %>' runat="server" Visible="false"></asp:Label>
                </ItemTemplate>

</asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">



                                          <ItemStyle Width="100px" />
                                            <ItemTemplate>
                                                <asp:Literal ID="litTeamSize" Text='<%#Eval("memName")%>' runat="server" />
                                                <asp:HiddenField runat="server" ID="hidTeamID" Value='<%# Bind("ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Email" DataField="EmailAddress" ItemStyle-Width="75px" />
                                        <asp:BoundField  HeaderStyle-CssClass="header_bg_r" HeaderText="Contact Number" DataField="ContactNumber" ItemStyle-Width="75px" />
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:GridView>
                       
       
            </asp:Panel>
           
            <script type="text/javascript">
<!--

     function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%=gridMembers.ClientID %>");
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
<script language="javascript" type="text/javascript">
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
            chkB.parentElement.parentElement.style.backgroundColor = 'yellow';  // grdEmployees.SelectedItemStyle.BackColor
            chkB.parentElement.parentElement.style.color = 'black'; // grdEmployees.SelectedItemStyle.ForeColor
        }
        else {
            chkB.parentElement.parentElement.style.backgroundColor = 'white'; //grdEmployees.ItemStyle.BackColor
            chkB.parentElement.parentElement.style.color = 'black'; //grdEmployees.ItemStyle.ForeColor
        }
    }
	
		</script>
            
                  
  <asp:Panel runat="server"  ID="pnlAddMembers" BackColor="White"  Style="display: none" Height="320px" ScrollBars="None"
                     Width="450px"  BorderStyle="Double" BorderColor="LightSteelBlue">
                        <div class="form-group row">
        <div class="col-md-12">
           <strong>Select Team Members</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

                <div class="form-group row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="pnlInsert" />
                    <asp:Label ID="lblErrMsg" runat="server"  ForeColor="Red" EnableViewState="false"></asp:Label>
	</div>
</div>   
                   
                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"> <asp:Label ID="lblTeam" runat="server" Text="Team"></asp:Label></label>
           <div class="col-sm-9">
                <asp:Label ID="lblTeamsName" runat="server" Text="" Font-Bold="true"></asp:Label>
            </div>
	</div>
</div>
                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                <span style="color: red">*</span></label>
           <div class="col-sm-9">
               <asp:Panel Width="250px" runat="server" ID="Panel1" Height="80px" CssClass="txt_field" BorderStyle="Inset">
        <evy:ScrollableListBox ID="CheckBoxList2" runat="server" BorderWidth="0px" 
        RepeatLayout="Flow" Height="75px" Width="250px" ></evy:ScrollableListBox>
              </asp:Panel>
            </div>
	</div>
</div>
                        <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:Button runat="server" ID="btnShowModalPopup" style="display:none"/>
                <asp:Button ID="btnAddTeamMember" runat="server"
                                    OnClick="btnAddTeamMember_Click" ValidationGroup="pnlInsert" 
                                    SkinID="btnAdd" />
                                &nbsp;
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

