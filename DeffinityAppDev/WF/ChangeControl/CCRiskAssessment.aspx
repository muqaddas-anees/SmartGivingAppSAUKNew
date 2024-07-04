<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CCRiskAssessment" Title="Untitled Page" Codebehind="CCRiskAssessment.aspx.cs" %>
<%@ Register Src="controls/ChangeControlTab.ascx" TagName="Tab" TagPrefix="Deffinity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
 <label id="lblPageTitle" runat="server">
                        </label> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <Deffinity:Tab ID="tabMenu" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
   <asp:Panel ID="pnlRiskAssessment" runat="server" ScrollBars="None">
       <div class="form-group">
        <div class="col-md-12">
           <strong>Risk Assessment </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
       <div class="form-group">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Risk"
                                    Width="100%" />
	</div>
</div>
       <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Risk%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtRisk" runat="server" SkinID="txt_90" MaxLength="500"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRisk"
                                    ErrorMessage="Risk should not be blank" ValidationGroup="Risk">*</asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:CheckBox ID="chkRoleBackPlan" runat="server" Text="Roll Back Plan" />
            </div>
	</div>
</div>
       <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Assignto%></label>
           <div class="col-sm-9">
               <asp:DropDownList AppendDataBoundItems="true" ID="ddlAssignedTo" runat="server" DataSourceID="SqlDataSource1"
                                    DataTextField="Name" DataValueField="ID" SkinID="ddl_90">
                                    <asp:ListItem Text="Please Select.." Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlAssignedTo"
                                    ErrorMessage="Please Select Asigned To" InitialValue="0" ValidationGroup="Risk"
                                    Text="*"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
                <asp:CheckBox ID="chkTestPlan" runat="server" Text="Test Plan" />
            </div>
	</div>
</div>
       <div class="form-group">
     
	<div class="col-md-6">
         <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
          <asp:Button ID="btnAddRisk" SkinID="btnAdd" runat="server" OnClick="btnAddRisk_Click" ValidationGroup="Risk" />
               </div>
	</div>
            <div class="col-md-6">
           
	</div>
</div>
  <div class="form-group">
          <div class="col-md-12">
              
                    <asp:Label ID="lblRiskMessage" runat="server" EnableViewState="false" ForeColor="Red" />
	</div>
</div>                
                 
                    <asp:Panel ID="pnlRisksGrid" runat="server" ScrollBars="Auto" Width="100%" Height="100%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                                DataSourceID="odsRisk"
                                    DataKeyNames="ID">
                                    <Columns>
                                        <asp:BoundField DataField="ID" Visible="False"  />
                                        <asp:BoundField DataField="ChangeControlID" Visible="False" />
                                        <asp:TemplateField HeaderText="Risk" SortExpression="Risk" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="300px"  ControlStyle-Width="300px" FooterStyle-Width="300px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RiskDescription") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("RiskDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="300px" />
                                        </asp:TemplateField>
                                        <asp:CheckBoxField DataField="RollBackPlan" HeaderText="Roll Back Plan" />
                                        <asp:CheckBoxField DataField="TestPlan" HeaderText="Test Plan" />
                                        <asp:BoundField DataField="ResourceName" HeaderText="Assigned To" SortExpression="ResourceName"
                                            ItemStyle-Width="150px" >
                                        <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick='return confirm("Do you really want to delete.")'
                                                    SkinID="BtnLinkDelete" ToolTip="Delete" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                   
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="SELECT ID,ContractorName AS Name FROM Contractors ORDER BY ContractorName">
                    </asp:SqlDataSource>
                    <asp:ObjectDataSource ID="odsRisk" runat="server" TypeName="Incidents.DAL.RiskHelper"
                        DataObjectTypeName="Incidents.Entity.Risk" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="LoadRisksById" UpdateMethod="Update">
                        <SelectParameters>
                            <asp:SessionParameter Name="id" SessionField="changeControlID" Type="Int32" DefaultValue="0" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>

     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>
