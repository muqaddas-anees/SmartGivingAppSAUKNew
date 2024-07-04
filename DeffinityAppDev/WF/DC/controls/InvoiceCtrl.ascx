<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.InvoiceCtrl" %>
<section class="invoice-env">
                <!-- Invoice header -->
						<div class="invoice-header">
							
							<!-- Invoice Options Buttons -->
							<div class="invoice-options hidden-print">
								<a href="#" class="btn btn-block btn-gray btn-icon btn-icon-standalone btn-icon-standalone-right text-left">
									<i class="fa-envelope-o"></i>
									<span>Send</span>
								</a>
								
								<a href="#" class="btn btn-block btn-secondary btn-icon btn-icon-standalone btn-icon-standalone-right btn-single text-left">
									<i class="fa-print"></i>
									<span>Print</span>
								</a>
							</div>
							
							<!-- Invoice Data Header -->
							<div class="invoice-logo">
								<a class="logo">
									<img id="ImgContractor" src="~/WF/Admin/ImageHandler.ashx" runat="server" class="img-responsive" />
								</a>
								<ul class="list-unstyled">
									<li class="upper"> Invoice No. <strong><asp:Label ID="lblInvoiceNo" runat="server" Text="#5652256"></asp:Label></strong></li>
									<li><asp:Label ID="lblToDayDate" runat="server" Text="06 December 14"></asp:Label></li>
									<li><asp:Label ID="LblLocation" runat="server" Text="Bangalore,India"></asp:Label></li>
								</ul>
							</div>
							
						</div>
						
						
						<!-- Client and Payment Details -->
						<div class="invoice-details">
							
							<div class="invoice-client-info">
								<strong>Client</strong>
								
								<ul class="list-unstyled">
									<li><asp:Label ID="lblClientInfo" runat="server" Text="First Data<br /> PO Box 850<br /> Lincolnshire, Illinois 60069-0850"></asp:Label>    </li>
								</ul>
								<%--<ul class="list-unstyled">		
									<li><asp:Label ID="lblClientInfo1" runat="server" Text=" 1982 OOP<br />Madrid, Spain <br />+1 (151) 225-4183"></asp:Label>  </li>
								</ul>--%>
							</div>
							
							<div class="invoice-payment-info">
								<strong>Payment Details</strong>
								
								<ul class="list-unstyled">
									<li>TAX Reg #: <strong><asp:Label ID="lblVATReg" runat="server" Text="542554(DEMO)78"></asp:Label></strong></li>
									<li>Account Name: <strong><asp:Label ID="lblAccountName" runat="server" Text="First Data"></asp:Label></strong> </li>
									<li>SWIFT code: <strong><asp:Label ID="lblSWIFTCode" runat="server" Text="45454DEMO545DEMO"></asp:Label></strong></li>
								</ul>
							</div>
							
						</div>
						
						
						<!-- Invoice Entries -->
                       <asp:GridView ID="GridInvoiceEntries" runat="server" CssClass="table table-bordered">
                           <Columns>
                               <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="text-center hidden-xs" ItemStyle-CssClass="text-center hidden-xs">
                                   <ItemTemplate>
                                       <asp:Label ID="lblOrderSerialNumber" Text='<%#Bind("Id") %>' runat="server"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="text-center">
                                   <ItemTemplate>
                                       <asp:Label ID="lblProductName" Text='<%#Bind("ProductName") %>' runat="server"></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="text-center hidden-xs" ItemStyle-CssClass="text-center hidden-xs" HeaderStyle-Width="100px">
                                   <ItemTemplate>
                                       <asp:Label ID="LblProductQty" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right text-primary text-bold">
                                   <ItemTemplate>
                                       <asp:Label ID="LblProductPrice" runat="server" Text='<%#Bind("Price","{0:F2}") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                       </asp:GridView>
                       
						<!-- Invoice Subtotals and Totals -->
						<div class="invoice-totals">
							
							<div class="invoice-subtotals-totals">
								<span>
									Sub - Total amount: 
									<strong><asp:Label ID="lblSubTotalamount" runat="server" Text="$6,487"></asp:Label> </strong>
								</span>
								<span>
									VAT: 
									<strong> <asp:Label ID="lblVAT" runat="server" Text="0.00"></asp:Label></strong>
								</span>
								
								<span>
									Discount: 
									<strong><asp:Label ID="LblDiscount" runat="server" Text="--------"></asp:Label></strong>
								</span>
								
								<hr />
								
								<span>
									Grand Total: 
									<strong><asp:Label ID="lblGrandTotal" runat="server" Text="$7,304"></asp:Label> </strong>
								</span>
							</div>
							
							<div class="invoice-bill-info">
								<%--<address>

                                    <asp:Label ID="lbladdress" runat="server" Text="795 Park Ave, Suite 120<br />San Francisco, CA 94107<br />P: (234) 145-1810 <br />Full Name"></asp:Label>
									 <br />
									<a href="#"><asp:Label ID="LblEmalidInAddress" Text="first.last@email.com" runat="server"></asp:Label></a>
								</address>--%>
							</div>
							
						</div>
            </section>