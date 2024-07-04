<%@ Control Language="C#"  AutoEventWireup="true" Inherits="controls_CustomerPO" Codebehind="CustomerPO.ascx.cs" %>
<div class="form-group">
          <div class="col-md-12">
              <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group1" />
	</div>
</div>
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.PONumber%></label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtPONumber" runat="server" Width="75"></asp:TextBox>  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ErrorMessage="Please enter PO Number to search" ValidationGroup="Group1" 
    ControlToValidate="txtPONumber" Display="None"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-4 form-inline">
        
     <asp:Button ID="imgSearch" runat="server" SkinID="btnSearch"  
        ValidationGroup="Group1" onclick="imgSearch_Click"
        />
  
   
  
     <asp:Button ID="imgAddNew" runat="server" SkinID="btnAddNew" 
         ToolTip="Add PO" onclick="imgAddNew_Click" />
           
	</div>
	<div class="col-md-4">
          
	</div>
</div>
<asp:GridView ID="grdPODetails" runat="server"  AutoGenerateColumns="False" 
                               
                       
        EmptyDataText="No Records Found" 
         onrowcommand="grdPODetails_RowCommand"        
       
                                >
                                <Columns>
                                   
                                     
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,PONumber%>"  ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px" ControlStyle-Width="200px">
                                       
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" CommandName="Show" runat="server" 
                                            Text='<%# Bind("PONumber") %>' CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                           
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Customer%>"  ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px" ControlStyle-Width="200px">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrencey" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                    
                                    
                                    
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectRef%>" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="250px" ControlStyle-Width="250px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                       
                                        <ItemTemplate>
                                        <a href="ProjectOverviewV4.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectRef")%>">  <%# DataBinder.Eval(Container.DataItem, "ProjectTitle")%></a>
                                            <%--<asp:Label ID="lblProject" runat="server"  Text='<%# Bind("ProjectTitle") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                     
                                       
                                    </asp:TemplateField>
                                    
                                    
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DateRaised%>">
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle Width="75px" />
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate1" runat="server" Text='<%# Bind("Date","{0:d}") %>'
                                                Width="75px"></asp:Label>
                                     </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ExpiryDate%>">
                                        <HeaderStyle Width="75px" />
                                        <ItemStyle Width="75px" />
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("POExpiryDate","{0:d}") %>'
                                                Width="75px"></asp:Label>
                                     </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,HoursBooked%>" >
                                      <HeaderStyle HorizontalAlign="Center" />
                                       
                                       <ItemStyle HorizontalAlign="Right" />
                                     <ItemTemplate>
                                     <asp:Label ID="lblhours" runat="server" Text='<%# ChangeHoues(Eval("HoursBooked").ToString())%>'></asp:Label>
                                     </ItemTemplate>
                                       
                                       
                                    </asp:TemplateField >
                                                                     
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalAmount%>" >
                                      <HeaderStyle HorizontalAlign="Center" />
                                       
                                       <ItemStyle HorizontalAlign="Right" />
                                     <ItemTemplate>
                                     <asp:Label ID="lblTotalAmt" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label>
                                     </ItemTemplate>
                                       
                                       
                                    </asp:TemplateField >
                                
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TotalPaid%>">
                                       <HeaderStyle HorizontalAlign="Center" />
                                       
                                       <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalPaid" runat="server"  Text='<%# Bind("TotalPaid") %>'></asp:Label>
                                        </ItemTemplate>
                                     
                                    </asp:TemplateField>
                                    
                                  <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Balance%>">
                                       <HeaderStyle HorizontalAlign="Center" />
                                       
                                       <ItemStyle HorizontalAlign="Right" />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server"  Text='<%# Bind("Balance") %>'></asp:Label>
                                        </ItemTemplate>
                                     <%-- <EditItemTemplate>
                                          <asp:TextBox ID="txtBalance" runat="server" Width="75px"  Text='<%# Bind("TotalPaid") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="ReqBalance" runat="server" ErrorMessage="Please enter b"
                                           ControlToValidate="txtBalance" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                        <asp:TextBox ID="txtTotalPaidf" runat="server" Width="75px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqTotalPaidf" runat="server" ErrorMessage="Please enter total paid"
                                           ControlToValidate="txtTotalPaidf" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                        </FooterTemplate>--%> 
                                        
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px"  ControlStyle-Width="200px">
                                       <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="75px" />
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server"  Text='<%# Bind("Notes") %>'></asp:Label>
                                        </ItemTemplate>
                                     
                                        
                                    </asp:TemplateField>
                                    
                                    
                                   
                                    
                              
                                 <%--  <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                SkinID="ImgSymDel" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>--%>
                             </Columns>
                            </asp:GridView>


 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 

