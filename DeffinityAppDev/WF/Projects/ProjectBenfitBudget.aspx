<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectBenfitBudget" Codebehind="ProjectBenfitBudget.aspx.cs" %>
<%-- --%>
<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ProjectBenefitBudget%> - <Pref:ProjectRef ID="ProjectRef2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <script type="text/javascript">
     function uncheckOthers(id) {
         var elm = document.getElementsByTagName('input');
         for (var i = 0; i < elm.length; i++) {
             if (elm.item(i).id.substring(id.id.lastIndexOf('_')) == id.id.substring(id.id.lastIndexOf('_'))) {
                 if (elm.item(i).type == "checkbox" && elm.item(i) != id)
                     elm.item(i).checked = false;
             }
         }
     }        
    </script>
    <uc4:BudgetTab ID="BudgetTab1" runat="server" />
    <br />
    <div class="form-group">
      <uc2:ProjectCost ID="ProjectCost1" runat="server" />
        </div>
    <div class="form-group">
                                <ajaxToolkit:ModalPopupExtender ID="modelPopupAddSoftware" CancelControlID="imgCancelCourse" BackgroundCssClass="modalBackground"
        TargetControlID="imgAdd" PopupControlID="pnlAddType" runat="server"></ajaxToolkit:ModalPopupExtender>
    <asp:HiddenField ID="hdnID" runat="server" />
     <asp:HiddenField ID="hdnBID" runat="server" />
     <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2"/>
         <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group1"/>
       
     </div>
    <div class="form-group form-inline">
      <div class="col-md-5">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.BenefitDescription%></label>
           <div class="col-sm-8 form-inline">
                 <asp:DropDownList ID="ddlBenefitType" runat="server" SkinID="ddl_70" >
                      </asp:DropDownList>
                      <asp:LinkButton ID="imgAdd" runat="server" SkinID="BtnLinkAdd" />
                      <asp:LinkButton ID="imgDelete" runat="server" SkinID="BtnLinkDelete" 
                          onclick="imgDelete_Click" Visible="False" CausesValidation="false" />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                      ErrorMessage="Please select benefit type" ControlToValidate="ddlBenefitType"
                       InitialValue="0" Display="None" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-2">
           <label class="col-sm-6 control-label"> <%= Resources.DeffinityRes.Target%></label>
           <div class="col-sm-6 form-inline">
                <asp:TextBox ID="txtTarget" runat="server" SkinID="txt_100px" MaxLength="10"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                      ErrorMessage="Please enter target" ControlToValidate="txtTarget"
                      Display="None" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="cv_Target_top" runat="server" ControlToValidate="txtTarget" Operator="DataTypeCheck"
                                   Type="Double" Display="None" ValidationGroup="Group1" ErrorMessage="Only numbers are accepted in this target field"></asp:CompareValidator>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-6 control-label"><%= Resources.DeffinityRes.ReportingCycle%></label>
           <div class="col-sm-6">
                 <asp:DropDownList ID="ddlRpeortingCycle" runat="server">
                      <asp:ListItem Selected="True" Value="Monthly">Monthly</asp:ListItem>
                      <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                      </asp:DropDownList>
            </div>
	</div>
        <div class="col-md-2">
             <asp:Button ID="imgApply" runat="server" SkinID="btnApply" 
                          onclick="imgApply_Click" ValidationGroup="Group1" />
            </div>
</div>

    


     <div class="form-group">
          <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Group3"/>
     <asp:Label ID="lblMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label></div> 

<asp:UpdatePanel runat="server" ID="shw">
<ContentTemplate>
  <div><asp:CheckBox ID="chkShowBenefit" runat="server" Checked="true"
            Text="<%$ Resources:DeffinityRes, Showbenefittrackingondashboard%>"  AutoPostBack="True" 
            oncheckedchanged="chkShowBenefit_CheckedChanged" /></div>
</ContentTemplate>
</asp:UpdatePanel>
        
          <asp:GridView ID="grdBenefitBudget" runat="server" AutoGenerateColumns="false"
             EmptyDataText="No Records Found"
              onrowcommand="grdBenefitBudget_RowCommand" 
              onrowdeleting="grdBenefitBudget_RowDeleting" 
              onrowdatabound="grdBenefitBudget_RowDataBound" 
              onrowcancelingedit="grdBenefitBudget_RowCancelingEdit" 
              onrowediting="grdBenefitBudget_RowEditing" 
              onrowupdating="grdBenefitBudget_RowUpdating">
                
             <Columns>
              <asp:TemplateField ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" ItemStyle-Width="10%">
                                     <ItemStyle Width="10px" />
                                        <ItemTemplate>
                                       
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                            <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" Enabled="<%#CommandField()%>"
                                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit" >
                                            </asp:LinkButton>
                                           
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate"
                                                ToolTip="Update" ValidationGroup="Group3"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                       
                                    </asp:TemplateField> 
             <asp:TemplateField >
             <ItemStyle Width="10px" />
             <ItemTemplate>
              <asp:Label ID="lblBenefitID" runat="server" Text='<%#Bind("ID") %>' Visible="false"></asp:Label>
              
                 
                 <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="UpdateSupplyLed" 
                               AutoPostBack="True" />
             </ItemTemplate>
             </asp:TemplateField>
           
                 
                 
                 <asp:BoundField HeaderText="<%$ Resources:DeffinityRes, BenefitDescription%>" DataField="Description"
                    ItemStyle-Width="100px"  ReadOnly="true" />
                    <asp:TemplateField HeaderText="Target">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Target","{0:f}") %>'></asp:Label>
                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditTarget" Width="50px" runat="server" Text='<%# Bind("Target","{0:f}") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
                      ErrorMessage="<%$ Resources:DeffinityRes, Plsentertarget%>" ControlToValidate="txtEditTarget"
                      Display="None" ValidationGroup="Group3" ></asp:RequiredFieldValidator>
                       <%--<asp:RegularExpressionValidator ID="regex12" runat="server" ControlToValidate="txtEditTarget"
                                                ValidationExpression="^\d{1,3}(\.\d{0,2})?$" ValidationGroup="Group3"
                                                Display="None"  ErrorMessage="Only numbers are accepted in this target field"></asp:RegularExpressionValidator>--%>
                    <asp:CompareValidator ID="cv_Target" runat="server" ControlToValidate="txtEditTarget" Operator="DataTypeCheck"
                                   Type="Double" Display="None" ValidationGroup="Group3" ErrorMessage="Only numbers are accepted in this target field"></asp:CompareValidator>
                                  
                    </EditItemTemplate>
                    </asp:TemplateField>
                  
                   <asp:BoundField HeaderText="<%$ Resources:DeffinityRes, ReportingCycle%>"  DataField="ReportCycle" ReadOnly="true"   ItemStyle-Width="100px" />
                     <asp:TemplateField >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false"  CommandName="Delete"
                                                SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
             </Columns>
             
             </asp:GridView>
         
          <asp:GridView ID="grdBenefitItems" runat="server" AutoGenerateColumns="False" 
                               EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsFound%>"
                  onrowcommand="grdBenefitItems_RowCommand" onrowupdating="grdBenefitItems_RowUpdating" >
                                <Columns>
                                    <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,Period%>" >
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="120px" />
                                       
                                        <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCustom" runat="server" Text='<%# Bind("Period","{0:d}") %>' Visible="true"></asp:Label>
                                           <asp:Label ID="lblPeriod" runat="server" Text='<%# Bind("Period","{0:d}") %>' Visible="false"></asp:Label>
                                            
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Button ID="imgUpdate" CommandName="Update1" SkinID="btnUpdate" runat="server"  Enabled="<%#CommandField()%>" ValidationGroup="Group2"  />
                                             <asp:Button ID="imgAddNew" CommandName="Add" SkinID="btnAdd" runat="server" ToolTip="Add new row" Visible="false" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes,Planned%>" >
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <ItemStyle HorizontalAlign="Right"  Width="120px" />
                                     <ItemTemplate>
                                    
                                   <asp:TextBox ID="txtPlannedDate" runat="server"  SkinID="Price_75px" Text='<%# Bind("Planned","{0:N2}") %>' ></asp:TextBox>
                                   <asp:CompareValidator ID="cv_planned" runat="server" ControlToValidate="txtPlannedDate" Operator="DataTypeCheck"
                                   Type="Double" ValidationGroup="Group2" Display="None" 
                                   ErrorMessage="<%$ Resources:DeffinityRes,Onlynumacceptedinthisfield%>"></asp:CompareValidator>
                                     </ItemTemplate>
                                    </asp:TemplateField >
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Actual1%>">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="120px" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                        
                                             <asp:TextBox ID="txtDateRaised" runat="server"  SkinID="Price_75px" Text='<%# Bind("Actual","{0:N2}") %>'  ></asp:TextBox>
                                              <asp:CompareValidator ID="cv_dateraised" runat="server" ControlToValidate="txtDateRaised" Operator="DataTypeCheck"
                                   Type="Double" ValidationGroup="Group2" Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Onlynumacceptedinthisfield%>"></asp:CompareValidator>
                                              
                                        </ItemTemplate>
                                       
                                        
                                    </asp:TemplateField>
                                  
                                    
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalProjecttoDate%>" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Right" Width="30px" />
                                        
                                        <ItemTemplate>
                                        
                                        <asp:Label ID="lblCumulativeTotal" runat="server" Text='<%# Bind("CumulativeTotal") %>' Visible="false"></asp:Label>
                                             <asp:TextBox ID="txtHours0" runat="server"
                                                 SkinID="Price_75px" MaxLength="5" Text='<%# Bind("CumulativeTotal") %>'></asp:TextBox>
                                       
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle Width="30px"  />
                                        <ItemStyle HorizontalAlign="Right" Width="200px" />
                                        <ItemTemplate>
                                         <asp:TextBox ID="txtnotes" runat="server" TextMode="MultiLine" Height="50px" Width="200px" Text='<%# Bind("Notes") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                             </Columns>
                            </asp:GridView>
       
          <asp:Panel ID="pnlAddType" runat="server"  BackColor="White" style="display:none" Width="300px" 
BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true">
              <div class="form-group">
        <div class="col-md-12">
        <strong>  <%= Resources.DeffinityRes.BenefitType%></strong>
            <hr class="no-top-margin" />
            </div>
    </div>
  <div class="form-group">
      <div class="col-md-12 ">
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group5" />
 <asp:Label ID="lblError" runat="server" Visible="true" ForeColor="Red"></asp:Label>
          </div>
      </div>
              <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.BenefitType%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtAddBenefit" runat="server" SkinID="txt_90"></asp:TextBox>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterbenefittype%>"
       Display="None" ControlToValidate="txtAddBenefit"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
           <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8 form-inline">
               <asp:Button ID="imgBtnAdd" runat="server" SkinID="btnAdd" ValidationGroup="Group5" OnClick="btnAddNenefitType_Click" />
             <asp:Button ID="imgCancelCourse" runat="server" SkinID="btnCancel" />
            <asp:Button runat="server" ID="imgAddDtypes" style="display:none"/>
            </div>
	</div>
</div>
              

</asp:Panel>
       
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>


