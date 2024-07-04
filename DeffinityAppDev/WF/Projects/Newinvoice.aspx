<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="NewValuation" Title="New Project Valuation" EnableEventValidation="false" Codebehind="Newinvoice.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>

<%@ Register src="MailControls/ProjectInvoiceMail.ascx" tagname="ProjectInvoiceMail" tagprefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.NewInvoice%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="row">
          <div class="col-md-12">
 <strong>Interim Invoice for Project </strong> 
<hr class="no-top-margin" />
	</div>
</div>
    
<div class="row">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="Invoice" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter invoice reference" Display="None" ValidationGroup="Invoice" ControlToValidate="txtInvoiceRef"></asp:RequiredFieldValidator>  
        <asp:CompareValidator ID="cmprValVAT" runat="server" ControlToValidate="txtVatper" Type="Double" Width="6"  ValidationGroup="Invoice" ErrorMessage="Please enter Valid VAT Percentage" Display="None"></asp:CompareValidator>
   <asp:RangeValidator ID="percentageRangeValidator" runat="server"
    ControlToValidate="txtVatper" Display="none" 
    ErrorMessage="Invalid percentage entered. Please enter a value between 0-100" MaximumValue="100.00" MinimumValue="0.00" 
    Type="Double" ValidationGroup="Invoice"></asp:RangeValidator>
    <asp:Label ID="lblMsg1" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Invoice&nbsp;Reference</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtInvoiceRef" runat="server" MaxLength="50" SkinID="txt_70"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">VAT % </label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtVatper" runat="server" SkinID="Price_100px" MaxLength="15" style="text-align:right;" ></asp:TextBox> 
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DateRaised%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtDateRaised" runat="server" SkinID="Date"></asp:TextBox>
         <asp:Label ID="Image1" runat="server" SkinID="Calender"  />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="Image1" TargetControlID="txtDateRaised" CssClass="MyCalendar">
                                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlInvoiceStatus" runat="server" SkinID="ddl_70"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">VAT Number</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtVatNumber" runat="server" SkinID="txt_70"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">Bank Details</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtBankDetails" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
	</div>
</div>
  
<div class="row">
          <div class="col-md-12">
              <div class="col-sm-12">
                  <asp:Button ID="btnAddInvoice" runat="server" 
            onclick="btnAddInvoice_Click" ValidationGroup="Invoice" SkinID="btnAdd" /> 
	</div>
	</div>
</div>
   
   
<div class="row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="summery1" ValidationGroup="GridValid" runat="server" />
	</div>
</div>
   
    <asp:Panel ID="Panel1" runat="server"  Width="100%" >
                <asp:GridView ID="GridView1" runat="server" Width="100%" 
                    OnSelectedIndexChanging="GridView1_SelectedIndexChanging" 
                    OnRowCommand="GridView1_RowCommand" OnRowUpdated="GridView1_RowUpdated" 
                    onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                    onrowupdating="GridView1_RowUpdating" 
                    onrowcancelingedit="GridView1_RowCancelingEdit">
                 <Columns>
                 
                     <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                         <EditItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate" ></asp:LinkButton>
                             <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                         </ItemTemplate>
                         
                         <ItemStyle Width="70px" />
                     </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Task">
                    <ItemStyle Width="300px" />
                    <HeaderStyle Width="300px" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                            <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtItemDes" runat="server" TextMode="MultiLine"  Text='<%# Bind("ItemDescription") %>'></asp:TextBox>
                            <asp:HiddenField ID="HID1" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:RequiredFieldValidator ControlToValidate="txtItemDes" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter task" ValidationGroup="GridValid" Display="None"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQty" runat="server" Width="50px"  Text='<%# Bind("QTY") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter qty" ValidationGroup="GridValid" ControlToValidate="txtQty" Display="None"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtQty"
                                ErrorMessage="Please enter valid qty" Operator="DataTypeCheck" Type="Integer"
                                ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Selling Price">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("Price","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Width="50px"  Text='<%# Bind("Price","{0:F2}") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ControlToValidate="txtPrice" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter price" ValidationGroup="GridValid"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPrice"
                                ErrorMessage="Please enter valid price" Operator="DataTypeCheck" Type="Double"
                                ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" >
                     <ItemStyle HorizontalAlign="Right" Width="100px"/>
                     <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="Label41" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="% Complete">
                     <ItemStyle HorizontalAlign="Center" Width="100px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("PercentComplete") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPercentage" runat="server" SelectedValue='<%# Bind("PercentComplete") %>'>
                          
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="75" Text="75"></asp:ListItem>
                            <asp:ListItem Value="100" Text="100"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Claimed Total" >
                     <ItemStyle HorizontalAlign="Right" Width="100px"/>
                     <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("ClaimedTotal","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:Label ID="Label61" runat="server" Text='<%# Bind("ClaimedTotal","{0:F2}") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="Deffinity_DisplayInvoice" SelectCommandType="storedProcedure">
            <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="ProjectReference" QueryStringField="Project" />
                    <asp:QueryStringParameter DefaultValue="0" Name="ProjectValuationID" QueryStringField="ValuationID" />
                    <asp:ControlParameter ControlID="hdnRefresh" Name="Counter" DefaultValue="0" Type="int32" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>   
                <asp:HiddenField ID="hdnRefresh" Value="0" runat="Server" />
    </asp:Panel>
    
<div class="row">
          <div class="col-md-12">
 <strong> Add new item </strong> 
<hr class="no-top-margin" />
	</div>
</div>
 
    <asp:Panel ID="p1" runat="server">
        
<div class="row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTasks"
                        Display="None" ErrorMessage="Please select task" InitialValue="Please select..."
                        ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                        Display="None" ErrorMessage="Please enter qty" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSellingPrice"
                        Display="None" ErrorMessage="Please enter selling price" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQty"
                        Display="None" ErrorMessage="Please enter valid data in qty" Operator="DataTypeCheck"
                        Type="Integer" ValidationGroup="Group1"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtSellingPrice"
                        Display="None" ErrorMessage="Please enter valid selling price" Operator="DataTypeCheck"
                        Type="Double" ValidationGroup="Group1"></asp:CompareValidator>
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Task</label>
           <div class="col-sm-8 form-inline">
               <asp:DropDownList ID="ddlTasks" runat="server" SkinID="ddl_80" AutoPostBack="True" 
                    onselectedindexchanged="ddlTasks_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:TextBox ID="txtDesc" runat="server" SkinID="txt_80" Visible="false"></asp:TextBox>
                &nbsp;<asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" 
                     SkinID="BtnLinkAdd" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">%&nbsp;complete</label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlPercent" runat="server" SkinID="ddl_70">            
           <%-- <asp:ListItem Text="0" Value="0"></asp:ListItem>--%>
            <asp:ListItem  Text="25" Value="25"></asp:ListItem>
            <asp:ListItem  Text="50" Value="50"></asp:ListItem>
            <asp:ListItem  Text="75" Value="75"></asp:ListItem>
            <asp:ListItem  Text="100" Value="100"></asp:ListItem>
            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">QTY</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtQty" runat="server" Width="50px" SkinID="txt_75px"></asp:TextBox>
            </div>
	</div>
</div>
        <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Selling price</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtSellingPrice" runat="server" SkinID="Price_75px" MaxLength="10"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:Button ID="btnAddNew" runat="server" ValidationGroup="Group1" 
                    OnClick="btnAddNew_Click" SkinID="btnSubmit" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>

   
    </asp:Panel>
  
     
<div class="row">
          <div class="col-md-12">
 <strong>Summary </strong> 
<hr class="no-top-margin" />
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-6 control-label">Total&nbsp;amount&nbsp;for&nbsp;this&nbsp;Invoice :</label>
           <div class="col-sm-6">
               <asp:Label ID="lblTotalWithoutVat" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-6 control-label">VAT(<asp:Label ID="lblVat" runat="server" Font-Bold="true"></asp:Label>%)</label>
           <div class="col-sm-6">
               <asp:Label ID="lblVatT" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
	<div class="col-md-6">
         
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-6 control-label">Total :</label>
           <div class="col-sm-6">
                <asp:Label ID="lblTotalInvoice" runat="server" Font-Bold="true"></asp:Label>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-6 control-label"> Email To :</label>
           <div class="col-sm-6">
               <asp:DropDownList ID="ddlCustomerContrator" runat="server" SkinID="ddl_70" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlCustomerContrator_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-6 control-label"></label>
           <div class="col-sm-6">
               <asp:TextBox ID="txtEmailAddress" runat="server" SkinID="txt_70"></asp:TextBox>
               <asp:Button ID="btnSubmit"
                    runat="server" OnClick="btnSubmit_Click" SkinID="btnSubmit" ValidationGroup="Invoice" /> 
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" 
                    ValidationGroup="Invoice" SkinID="btnSave"  />
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-6 control-label"></label>
           <div class="col-sm-6">
               <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="200px" 
                    Visible="False"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>


    
<div class="row">
          <div class="col-md-12">
              <div class="col-sm-12">
                  <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
	</div>
	</div>
</div>
    <div class="row">
          <div class="col-md-12">
              <div class="col-sm-12">
                  <asp:Button ID="ImageButton1" runat="server" SkinID="btnDefault" Text="Back" OnClick="ImageButton1_Click"  />
	</div>
	</div>
</div>
    
    <uc2:ProjectInvoiceMail ID="ProjectInvoiceMail1" runat="server" Visible="false" />
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>

