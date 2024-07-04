<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectBOMSupplierRequisitions" Codebehind="ProjectBOMSupplierRequisitions.aspx.cs" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="MailControls/BOMSupplierReq.ascx" TagName="SupplierMail" TagPrefix="QT1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
    <QT1:SupplierMail ID="SupplierMail1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     Supplier Requisition - <Pref:ProjectRef ID="ProjectRef1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group3" />
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Group4" />
                <asp:Label ID="lblError" runat="server" Text="" Visible="false" EnableViewState="false"></asp:Label>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>
            </div>
  
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Select&nbsp;Supplier:</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="ddl_80" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="imgView" runat="server" SkinID="btnDefault" Text="View all items for this supplier"
                            ValidationGroup="Group2" ToolTip="View items from supplier" OnClick="imgView_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Group2"
                            ErrorMessage="Please select supplier" ControlToValidate="ddlSupplier" InitialValue="0"
                            Display="None"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Group3"
                            ErrorMessage="Please select supplier" ControlToValidate="ddlSupplier" InitialValue="0"
                            Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"> Sites:</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlSites" runat="server" SkinID="ddl_80" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlSites_SelectedIndexChanged">
                        </asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"> PO Number:</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlPonumber" runat="server" SkinID="ddl_70" Visible="false">
                        </asp:DropDownList>
            </div>
	</div>
	 <div class="col-md-6">
           <label class="col-sm-3 control-label">Delivery&nbsp;Address:</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtAddress" runat="server" SkinID="txtMulti" TextMode="MultiLine" Height="50px"></asp:TextBox>
            </div>
	</div>
</div>
    <div class="form-group">
        <div class="col-md-6">
           <label class="col-sm-3 control-label">Date Required:</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtDateRequired" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Group1"
                            ErrorMessage="Please enter Date" ControlToValidate="txtDateRequired" Display="None"></asp:RequiredFieldValidator>
                        <asp:Label ID="Image5" runat="server" SkinID="Calender" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender41" runat="server" CssClass="MyCalendar"
                             PopupButtonID="Image5" TargetControlID="txtDateRequired">
                        </ajaxToolkit:CalendarExtender>
            </div>
	</div>
     
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>

    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label"> Notes:</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtNotes" runat="server" SkinID="txtMulti" TextMode="MultiLine"  Height="50px"></asp:TextBox>
               </div>
              </div>
         </div>
     <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label">Header:</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtNotes1" runat="server" SkinID="txtMulti" TextMode="MultiLine" Height="50px"></asp:TextBox>
               </div>
              </div>
         </div>

    <asp:GridView ID="gridBOM" runat="server" Width="100%" EmptyDataText="No Records Found"
                    AutoGenerateColumns="false" OnRowCommand="gridBOM_RowCommand" OnRowDataBound="gridBOM_RowDataBound"
                    OnRowDeleting="gridBOM_RowDeleting" OnRowEditing="gridBOM_RowEditing" OnRowUpdating="gridBOM_RowUpdating"
                    OnRowCancelingEdit="gridBOM_RowCancelingEdit">
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text="<%# Bind('ID')%>" Visible="false"> </asp:Label>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                    CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkEdit" ToolTip="Edit">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument="<%# Bind('ID')%>" SkinID="BtnLinkAdd" ToolTip="Update"
                                            ValidationGroup="Group2"></asp:LinkButton>
                                 <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                              
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Worksheet">
                            <ItemStyle Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text="<%# Bind('Worksheet')%>"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" Width="100px" runat="server" Text="<%# Bind('Worksheet')%>"></asp:TextBox>
                                <asp:LinkButton ID="imgDescription" runat="server" SkinID="BtnLinkAdd" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescription"
                                    ErrorMessage="Please enter Description " Display="None" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Description">
                            <ItemStyle Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescription1" runat="server" Text="<%# Bind('Description')%>"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription1" Width="100px" runat="server" Text="<%# Bind('Description')%>"></asp:TextBox>
                                <asp:ImageButton ID="imgDescription1" runat="server" SkinID="ImgSymAdd" Visible="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtDescription1"
                                    ErrorMessage="Please enter Description " Display="None" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Part Number">
                            <ItemStyle Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPartNumber1" runat="server" Text="<%# Bind('PartNumber')%>"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPartNumber1" Width="100px" runat="server" Text="<%# Bind('PartNumber')%>"></asp:TextBox>
                                <asp:ImageButton ID="imgDescription12" runat="server" SkinID="ImgSymAdd" Visible="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator132" runat="server" ControlToValidate="txtPartNumber1"
                                    ErrorMessage="Please enter part number " Display="None" ValidationGroup="Group4"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField ItemStyle-HorizontalAlign="Center" DataField="PONumber" HeaderText="PO Number" ReadOnly="true" />--%>
                        <asp:TemplateField HeaderText="Unit Price" Visible="true">
                            <ItemStyle Width="75px" HorizontalAlign="Right" />
                            <FooterStyle Width="75px" />
                            <ItemTemplate>
                                <asp:Label ID="lblUnit" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUnit" runat="server"  Width="75px"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Ordered">
                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text="<%#Bind('Qty','{0:F2}')%>"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" Text="<%#Bind('Qty','{0:F2}')%>" Width="75px"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label ID="lblVatText" runat="server" Text=" " Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label ID="lblSum1" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Left" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="30px">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="<%# Bind('Total','{0:F2}')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSum" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblVat" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="20px"  HeaderStyle-CssClass="header_bg_r"/>--%>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r" Visible="true">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="DateRaised" HeaderText="From Date" DataFormatString="{0:d}" />
              <asp:BoundField DataField="POExpiryDate" HeaderText="To Date" DataFormatString="{0:d}"/>
              <asp:BoundField DataField="DDays" HeaderText="Number of Days" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="DetailsOfPO" HeaderText="Notes" HeaderStyle-CssClass="header_bg_r" />--%>
                    </Columns>
                </asp:GridView>
    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-1 control-label">Footer:</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtNotes2" runat="server" SkinID="txtMulti" TextMode="MultiLine" Height="50px"></asp:TextBox>
            </div>
	</div>
</div>
    <div>
         <asp:Button ID="imgSave" runat="server" SkinID="btnSave" ValidationGroup="Group1"
                                    OnClick="imgSave_Click" Visible="False" />
    </div>
    <div class="form-group">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"> Supplier Contact&nbsp;Name:</label>
           <div class="col-sm-2">
               <asp:TextBox ID="txtReceiverName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ControlToValidate="txtReceiverName" ValidationGroup="Group4"
                                    ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter receiver name"></asp:RequiredFieldValidator>
            </div>
               <div class="col-sm-2">
                    Email:
                   </div>
               <div class="col-sm-2 form-inline">
                  
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ControlToValidate="txtEmail" ValidationGroup="Group4"
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter email address"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ValidationGroup="Group4" Display="None"></asp:RegularExpressionValidator>
            </div>
               <div class="col-sm-2">
                    <asp:Button ID="imgSaveEmail" runat="server" SkinID="btnDefault" Text="Send to Supplier"
                                    OnClick="imgSaveEmail_Click" ValidationGroup="Group4" OnClientClick="return confirm('Are you sure you would like to submit this supplier requisition?');" />
            </div>
	</div>
</div>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>

