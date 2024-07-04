<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" Inherits="CustomerInvoicing" Codebehind="FMInvoicing.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Invoicing%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc2:FMTab ID="fmID" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <script type="text/javascript" language="javascript">
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
    <div class="form-group">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary3" runat="server"  ValidationGroup="Group2"/>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ValidationGroup="Group1"/>
	</div>
</div>

    <div class="form-group">
      <div class="col-md-3">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%> </label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlCustomer" runat="server">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-3">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Project%> </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProject" runat="server"></asp:DropDownList> 
            <ajaxToolkit:CascadingDropDown ID="casCadProjectTile" runat="server"
    TargetControlID="ddlProject"
    Category="Title"
    PromptText="Please select..."
    ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
    ServiceMethod="GetAllProjectRef"
    ParentControlID="ddlCustomer" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectRef%> </label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtPrefix1" runat="server" SkinID="txt_50px" Enabled="false" ></asp:TextBox>
                    <asp:TextBox ID="txtProjectRef" runat="server" SkinID="txt_100px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regex12Labourf111f" runat="server" ControlToValidate="txtProjectRef"
                                        ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group1" Display="None"
                                        ErrorMessage="<%$ Resources:DeffinityRes,Youhaveenteredincorrectdataintofieldswhichonlyacceptnumbers%>"></asp:RegularExpressionValidator>
            </div>
	</div>
        <div class="col-md-2">
            <asp:Button ID="imgSearch" runat="server" SkinID="btnSearch" 
                        onclick="imgSearch_Click" ValidationGroup="Group1" />
        <asp:DropDownList ID="ddlInvoiceStatus"  runat="server" Visible="false" SkinID="ddl_90">
                        <asp:ListItem Value="1">Paid</asp:ListItem>
                        <asp:ListItem Value="2">Pending</asp:ListItem>
                        <asp:ListItem Value="3">Submitted</asp:ListItem>
                    </asp:DropDownList>
       
            </div>
</div>

   
    <div class="form-group" id="div2"  runat="server" visible="false">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.ProjectsReadyForInvoicing%>  </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    <asp:GridView ID="grdInvoice" runat="server" AutoGenerateColumns="false"
             Width="100%" EmptyDataText="No Records Found" AllowPaging="True" 
                 PageSize="15" onrowdatabound="grdInvoice_RowDataBound" 
                onpageindexchanging="grdInvoice_PageIndexChanging" 
                onrowcommand="grdInvoice_RowCommand">
             <Columns>
             <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Select%>" Visible="false" >
             <ItemTemplate>
              <asp:Label ID="lblProjectReferenceWithPrefix1" runat="server" Text="<%#Bind('ProjectReferenceWithPrefix') %>" Visible="false"></asp:Label>
              <asp:Label ID="lblProjectTitle1" runat="server" Text="<%#Bind('ProjectTitle') %>" Visible="false"></asp:Label>
                 <asp:Label ID="lblValutionID" runat="server" Text="<%#Bind('ProjectReference') %>" Visible="false"></asp:Label>
                 <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="UpdateSupplyLed" 
                               AutoPostBack="True" />
             </ItemTemplate>
             </asp:TemplateField>
            <%--<asp:BoundField HeaderText="Project Ref" DataField="ProjectReferenceWithPrefix">

                 </asp:BoundField>--%>
                  <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" HeaderStyle-CssClass="header_bg_l">
             <ItemTemplate>
             <a href="ProjectOverviewV4.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">  <%# DataBinder.Eval(Container.DataItem, "ProjectReferenceWithPrefix")%></a>
                <%-- <asp:Label ID="lblProjectReferenceWithPrefix" runat="server" Text="<%#Bind('ProjectReferenceWithPrefix') %>" Visible="true"></asp:Label>--%>
               
             </ItemTemplate>
             </asp:TemplateField>
                  <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Title%>">
                  <ItemStyle Width="200px" />
             <ItemTemplate>
                 <asp:Label ID="lblProjectTitle" runat="server" Text="<%#Bind('ProjectTitle') %>" Visible="true"></asp:Label>
               
             </ItemTemplate>
             </asp:TemplateField>
                <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,Status%>" DataField="ProjectStatusName" ItemStyle-Width="100px" />
                  <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Customer%>">
             <ItemTemplate>
                 <asp:Label ID="lblCustomer" runat="server" Text="<%#Bind('PortfolioName') %>"></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
                 
             <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,PaymentTerms%>" />
              <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ProjectValue%>" DataField="BudgetaryCostLevel3" 
              ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
               <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,InvoicedToDate%>" DataField="CurrentMonthAccrual"
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                 
                 <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,InvoiceAmountOutstanding%>" DataField="GrossProfit"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                  <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,ProjectOwner%>" DataField="OwnerName" />
                   <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,CostToDate%>"  DataField="BuyingPrice" 
                      ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"   />
                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Archive%>" HeaderStyle-CssClass="header_bg_r">
             <ItemTemplate>
                 <asp:LinkButton ID="lnkArchive" runat="server" CommandName="Archive" CommandArgument="<%#Bind('ProjectReference') %>" HeaderStyle-CssClass="header_bg_r" >Archive</asp:LinkButton>
                <%-- <asp:Label ID="lblArchive" runat="server" Text="<%#Bind('PortfolioName') %>"></asp:Label>--%>
             </ItemTemplate>
             </asp:TemplateField>
             </Columns>
             
             </asp:GridView>
    <div class="form-group">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary2" runat="server"  ValidationGroup="Group6"/>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-5">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ProjectRef%> </label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtPrefix" runat="server" SkinID="Price_75px" Enabled="false" ></asp:TextBox>
                    <asp:TextBox ID="txtProjectRefInvoice" runat="server" SkinID="txt_60"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtProjectRefInvoice"
                                        ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ValidationGroup="Group2" Display="None"
                                        ErrorMessage="<%$ Resources:DeffinityRes,Youhaveenteredincorrectdataintofieldswhichonlyacceptnumbers%>"></asp:RegularExpressionValidator>
            </div>
	</div>
	<div class="col-md-6">
           <div class="col-sm-12">
                <asp:Button ID="ImgViewLiveProjects" runat="server" ValidationGroup="Group2" SkinID="btnSearch" onclick="ImgViewLiveProjects_Click" />
          <asp:DropDownList ID="DropDownList3"  runat="server" Visible="false">
                        <asp:ListItem Value="1">Paid</asp:ListItem>
                        <asp:ListItem Value="2">Pending</asp:ListItem>
                        <asp:ListItem Value="3">Submitted</asp:ListItem>
                    </asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">  </label>
           <div class="col-sm-9 form-inline">
               
            </div>
	</div>
	<div class="col-md-4 form-inline pull-right">
        
	</div>
	<div class="col-md-2">
          
	</div>
</div>
    <div class="form-group" id="div1"  runat="server" visible="false">
        <div class="col-md-12" >
           <strong><asp:Label ID="lblTitle" runat="server" Visible="false"></asp:Label> <asp:HiddenField ID="hdnRef" runat="server" Value="0"/>
           </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

       <asp:GridView ID="grdInvoiceRaised" runat="server" AutoGenerateColumns="false"
             EmptyDataText="No Records Found" AllowPaging="True" 
                 PageSize="15" onrowcommand="grdInvoiceRaised_RowCommand" 
                onrowdatabound="grdInvoiceRaised_RowDataBound" 
                onrowediting="grdInvoiceRaised_RowEditing" 
                onrowupdating="grdInvoiceRaised_RowUpdating" 
                onrowcancelingedit="grdInvoiceRaised_RowCancelingEdit">
             <Columns>
              <asp:TemplateField HeaderStyle-CssClass="header_bg_l"> 
                 <HeaderStyle Width="40px" />
                 <ItemStyle Width="40px"/>    
                 
            <ItemTemplate>
                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" CommandArgument="<%# Bind('ValuationID')%>" SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes, Edit%>" ></asp:LinkButton>
            </ItemTemplate>
             <EditItemTemplate>
             <div style="width:45px"><div style="width:20px;float:left">
                  <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="<%$ Resources:DeffinityRes,Update %>"
                    CommandArgument="<%# Bind('ValuationID')%>"  SkinID="BtnLinkUpdate" CausesValidation="true"
                              ValidationGroup="Group1"                  ToolTip="<%$ Resources:DeffinityRes,Update %>"></asp:LinkButton></div><div style="width:20px;float:left">
                   <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                     SkinID="BtnLinkCancel" ToolTip="<%$ Resources:DeffinityRes, Cancel%>"></asp:LinkButton></div></div>
                                        </EditItemTemplate>
             
            </asp:TemplateField>
              <asp:BoundField HeaderText="<%$Resources:DeffinityRes,ProjectRef%>" DataField="ProjectReferenceWithPrefix" ReadOnly="true"  ></asp:BoundField>
              <asp:BoundField HeaderText="<%$Resources:DeffinityRes,Title%>" DataField="ProjectTitle" ReadOnly="true"  ></asp:BoundField>
             <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,InvoiceNumber%>" >
             <ItemTemplate>
                 <asp:Label ID="lblValutionID" runat="server" Text="<%#Bind('InvoiceReference') %>" ></asp:Label>
                 <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
             </ItemTemplate>
             </asp:TemplateField>
            <asp:BoundField HeaderText="<%$Resources:DeffinityRes,DateRaised%>" DataField="DateRaised" ReadOnly="true" 
             DataFormatString="{0:d}" >

                 </asp:BoundField>
                 
                 <asp:BoundField HeaderText="<%$Resources:DeffinityRes,ValuebeforeVAT%>" DataField="Value" ReadOnly="true" 
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                   <asp:BoundField HeaderText="<%$Resources:DeffinityRes,VAT%>" DataField="VATPercentage"  ReadOnly="true" 
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                   <asp:BoundField HeaderText="<%$Resources:DeffinityRes,Total%>" DataField="SubTotal"  ReadOnly="true" 
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                 <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Status%>">
                     <HeaderStyle Width="60px" />
                     <ItemStyle Width="60px" />
                     <ItemTemplate>
                         <asp:Label ID="lblInvoiceStatus" runat="server" Text='<%#InvoiceStatus(DataBinder.Eval(Container.DataItem,"InvoiceStatus").ToString())%>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                     <asp:Label ID="lblInvoiceStatus1" runat="server" Text="<%#Bind('InvoiceStatus')%>" Visible="false"></asp:Label>
                         <asp:DropDownList ID="ddlInvoiceStatus" runat="server" Width="100px">
                         </asp:DropDownList>
                     </EditItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,ViewInvoice%>" Visible="true">
             <ItemTemplate>
                 <asp:Label ID="lblCustomer" runat="server" Text="<%#Bind('ValuationID') %>" Visible="false"></asp:Label>
                 <asp:LinkButton ID="lnkView" runat="server" CommandName="View" CommandArgument="<%#Bind('ValuationID') %>">View</asp:LinkButton>
             </ItemTemplate>
             </asp:TemplateField>
                 <asp:BoundField HeaderText="<%$Resources:DeffinityRes,ExpectedPaymentDate%>" DataField="expectedate" ReadOnly="true" 
             DataFormatString="{0:d}" >

                 </asp:BoundField>
                 
                <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,ActualDate%>">
                     <HeaderStyle Width="100px" />
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblActualDate" runat="server" Text="<%#Bind('ActualDate','{0:dd/MM/yyyy}') %>"></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                      <div style="width:90px"><div style="width:70px;float:left">
                         <asp:TextBox ID="txtActualDate" Text="<%#Bind('ActualDate','{0:dd/MM/yyyy}') %>" Width="60px"  runat="server"></asp:TextBox></div>
                         <div style="width:20px;float:left">
                         <asp:Label ID="imgbtnenddate1" runat="server" SkinID="Calender" /></div></div>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                    PopupButtonID="imgbtnenddate1" TargetControlID="txtActualDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
                         
                     </EditItemTemplate>
                 </asp:TemplateField>  
             <asp:BoundField HeaderText="<%$Resources:DeffinityRes,Comments%>" ReadOnly="true"   DataField="Notes" HeaderStyle-CssClass="header_bg_r"/>
            
                    
             </Columns>
             
             </asp:GridView>
       
           <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblFocus" runat="server" Text="Label" ForeColor="White"></asp:Label>
         <div id="div3" runat="server" class="clr"></div>
	</div>
</div>
    <ajaxToolkit:ModalPopupExtender ID="mdlPopViewInvoice" runat="server"
             PopupControlID="pnlViewInvoice" TargetControlID="btnShowModalPopup"  CancelControlID="imgCancel"
              BackgroundCssClass="modalBackground">
             </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlViewInvoice"  runat="server"  BackColor="White" style="display:none" Width="600px" 
BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true">
 <div style="float:right" >
    <asp:LinkButton ID="imgCancel" runat="server"  SkinID="BtnLinkCancel" /></div>
                <div class="form-group">
        <div class="col-md-12">
           <strong><asp:Label ID="lblRef" runat="server" Text=""></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

           
            <asp:GridView ID="grdViewInvoice" runat="server" Width="100%">
                 <Columns>
                 
                   <%--  <asp:CommandField HeaderStyle-CssClass="header_bg_l" ItemStyle-Width="100px" ShowEditButton="True" EditText="&lt;img src='media/ico_edit.png' border=0 title='Edit'&gt;" UpdateText="&lt;img src='media/ico_update.png' border=0 title='Update'&gt;" CancelText="&lt;img src='media/ico_cancel.png' border=0 title='Cancel'&gt;" ValidationGroup="GridValid"  />                     --%>
                    
                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Task%>" HeaderStyle-CssClass="header_bg_l">
                    <ItemStyle Width="200px" />
                    <HeaderStyle Width="200px" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                            <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtItemDes" runat="server" TextMode="MultiLine"  Text='<%# Bind("ItemDescription") %>'></asp:TextBox>
                            <asp:HiddenField ID="HID1" runat="server" Value='<%# Bind("ID") %>' />
                            <asp:RequiredFieldValidator ControlToValidate="txtItemDes" ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$Resources:DeffinityRes,Pleaseentertask%>" ValidationGroup="GridValid" Display="None"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,QTY%>">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQty" runat="server" Width="50px"  Text='<%# Bind("QTY") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<%$Resources:DeffinityRes,Pleaseenterqty%>" ValidationGroup="GridValid" ControlToValidate="txtQty" Display="None"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtQty"
                                ErrorMessage="<%$Resources:DeffinityRes,Pleaseentervalidqty%>" Operator="DataTypeCheck" Type="Integer"
                                ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,SellingPrice%>">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("Price","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Width="50px"  Text='<%# Bind("Price","{0:F2}") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator Display="None" ControlToValidate="txtPrice" ID="RequiredFieldValidator6" runat="server" ErrorMessage="<%$Resources:DeffinityRes,Pleaseenterprice%>" ValidationGroup="GridValid"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPrice"
                                ErrorMessage="<%$Resources:DeffinityRes,Pleaseentervalidprice%>" Operator="DataTypeCheck" Type="Double"
                                ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Total%>" >
                     <ItemStyle HorizontalAlign="Right" Width="100px"/>
                     <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="Label41" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Complete%> ">
                     <ItemStyle HorizontalAlign="Center" Width="100px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("PercentComplete") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlPercentage" runat="server" SelectedValue='<%# Bind("PercentComplete") %>'>
                            <asp:ListItem Value="0" Text="0"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="75" Text="75"></asp:ListItem>
                            <asp:ListItem Value="100" Text="100"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,ClaimedTotal%>" HeaderStyle-CssClass="header_bg_r">
                     <ItemStyle HorizontalAlign="Right" Width="100px"/>
                     <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("ClaimedTotal","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:Label ID="Label61" runat="server" Text='<%# Bind("ClaimedTotal","{0:F2}") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
        <%--   <div> <asp:ImageButton ID="imgCancel" SkinID="ImgCancel" runat="server" /></div>--%>
               
             <asp:Button runat="server" ID="btnShowModalPopup" style="display:none"/>
            
            </asp:Panel>
          
            <div id="div4" runat="server" class="clr"></div> 
  
         
           
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


