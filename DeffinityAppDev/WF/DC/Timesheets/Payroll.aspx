<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Payroll.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Timesheets.Payroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Payroll
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Payroll
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row mb-5">
      <div class="col-md-3">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Date%>:</label>
           <div class="col-sm-8 d-flex d-inline"> <asp:TextBox ID="txtweekcommencedate" runat="server" SkinID="DateNew"></asp:TextBox>
                                    <asp:Label ID="imgbtnenddate8" runat="server" SkinID="Calender" />
               
                <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />
           
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                        PopupButtonID="imgbtnenddate8" TargetControlID="txtweekcommencedate" >
                                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
       
	<div class="col-md-3" >
           <label class="col-sm-2 control-label">Users</label>
           <div class="col-sm-8">
                  <asp:DropDownList ID="ddlUsers" runat="server"></asp:DropDownList>
                                          
            </div>
	</div>
         <div class="col-md-4 d-flex d-inline">
        <asp:Button ID="btn_viewdate" runat="server" SkinID="btnDefault" Text="View" 
                                        OnClick="btn_viewdate_Click" ToolTip="<%$ Resources:DeffinityRes,ViewTimesheet%>" />  
	</div>
	
</div>
     <div class="form-group row mb-5">
            <div class="col-md-12 text-bold">
                <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
            </div>
        </div>
     <div class="form-group row mb-5">

         <div class="col-md-6">
             <div class="form-group row">
            <div class="col-md-12 text-bold">
                <strong>Timesheets</strong>
                <hr class="no-top-margin" />
            </div>
        </div>
             <asp:GridView ID="GridPartner" runat="server" Width="100%" AutoGenerateColumns="false"  OnRowDataBound="GridPartner_RowDataBound" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="3%" Visible="false" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Users" ItemStyle-Width="70%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("ResourceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Date" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblTimeExpensesDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="From Time (HH:MM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" Visible="false" >
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblGridStartTime" runat="server" ></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText=" To Time (HH:MM)" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%" Visible="false" >
                           
                            <ItemTemplate>
                                <asp:Label ID="lblGridEndTime" runat="server"></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="15%" >
                           
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
             <%-- <asp:TemplateField HeaderText="Approver" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="8%" Visible="false">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPrimeApproverName" runat="server" Text='<%# Eval("PrimeApproverName").ToString()%>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>--%>
                       
            <%--  <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" Visible="false" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
         </div>
         <div class="col-md-6  mb-5">
                <div class="form-group row  mb-5">
            <div class="col-md-12 text-bold">
                <strong>Expenses</strong>
                <hr class="no-top-margin" />
            </div>
        </div>
             <asp:GridView ID="gridExpenses" runat="server" Width="100%" AutoGenerateColumns="false"   >
        <Columns>
            <asp:TemplateField ItemStyle-Width="3%">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="3%" Visible="false" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="User" ItemStyle-Width="25%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContractorName" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Date" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Right" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblTimeExpensesDate" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Item" ItemStyle-Width="25%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="Details" ItemStyle-Width="20%"  >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Bind("amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="Reimburse To" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReimburseToName" runat="server" Text='<%# Bind("ReimburseToName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Job" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Accounting Code" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountingCodesName" runat="server" Text='<%# Bind("AccountingCodesName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" Visible="false" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>

         </div>
         </div>

    
             <div class="form-group row  mb-5">
            <div class="col-md-12 text-bold row  mb-5">
                <strong>Payroll Summary</strong>
                <hr class="no-top-margin" />
            </div>
                 </div>
     <div class="form-group row">

         <div class="col-md-6">
               <div class="form-group row">
                                  <div class="col-md-6 form-inline" style="padding-left:50px">
                                       <asp:Label id="lblhoursTitle" runat="server" Text="Total hours:" Font-Bold="true"> </asp:Label> <asp:Label ID="lblHours" runat="server" Text="00:00" BackColor="LightYellow"></asp:Label> 
                                      
                                      </div>
                   </div>
         </div>
         <div class="col-md-6">
               <div class="form-group row">
                                  <div class="col-md-12 form-inline">
                                     <asp:Label ID="lblamounttitle" runat="server" Text="Total expenses:" Font-Bold="true"></asp:Label>   
                                      <asp:Label ID="lblTotalAmount" runat="server" Text="0.00" BackColor="LightYellow"></asp:Label> 
                                      </div>
                   </div>
         </div>
         </div>
      <div class="form-group row">
           <div class="col-md-12" style="text-align:right;">
               <asp:Button ID="btnMarkedPaid" runat="server" SkinID="btnDefault" Text="Mark Paid" OnClick="btnMarkedPaid_Click" />
               <asp:Button ID="btnCancel" runat="server" SkinID="btnDefault" Text="Cancel" OnClick="btnCancel_Click" />
               </div>
          </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
