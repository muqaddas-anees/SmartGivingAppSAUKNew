<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FRPApprovalCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.FRPApprovalCtrl" %>
<style>
     .hcenter{
             text-align:center;
          }
           .hright{
             text-align:right;
          }
</style>
<div class="row p-0 mb-5 px-9">
														<!--begin::Col-->
														<div class="col-lg-3">
															<div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-4 fw-bold text-success d-block">Total Pending</span>
																<span class="fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true" ><label id="lblPending" runat="server">0</label> </span>
															</div>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-3">
															<div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-4 fw-bold text-success d-block">Sent to Customer</span>
																<span class="fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true"><label id="lblUnpaid" runat="server">0</label> </span>
															</div>
														</div>
														<!--end::Col-->
														<!--begin::Col-->
														<div class="col-lg-3">
															<div class="border border-dashed border-gray-300 text-center min-w-125px rounded pt-4 pb-2 my-3">
																<span class="fs-4 fw-bold text-success d-block">Total Paid This Month </span>
																<span class="fs-2hx fw-bolder text-gray-900 counted" data-kt-countup="true" > <label id="lblPaidthismonth" runat="server">0</label> </span>
															</div>
														</div>
														<!--end::Col-->
													</div>

<div class="row" style="display:none;visibility:hidden;">



        <div class="col-sm-4">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class=" fa-arrows-h"></i>
						</div>
                        <div class="xe-label">
							<strong class="num"> </strong>
							<span>Total Pending </span>
						</div>
						
					</div>
					 </div>
        <div class="col-sm-4">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class="fa-location-arrow"></i>
						</div>
                        <div class="xe-label">
							<strong class="num"> </strong>
							<span>Sent to Customer </span>
						</div>
						
					</div>
					
				</div>	
        <div class="col-sm-4">
					<div class="xe-widget xe-counter" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon ">
							<i class=" fa-line-chart"></i> 
						</div>
						 <div class="xe-label">
							<strong class="num"></strong>
							<span>Total Paid This Month </span>
						</div>
					</div>
				
				</div>
        
	  
  
				
        
        
	  
   </div> 
<div class="row">
          <div class="col-md-12">
              &nbsp;
              </div>
    </div>
<div class="row mb-6">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
          </div>
                 </div>
<div class="row  mb-6">
          <div class="col-md-4">
              <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" >
                <%--  <asp:ListItem>All</asp:ListItem>
                  <asp:ListItem>Sent</asp:ListItem>
                  <asp:ListItem>Pending</asp:ListItem>
                  <asp:ListItem>Paid</asp:ListItem>--%>
              </asp:DropDownList>
             
              </div>
     <div class="col-md-4">
         <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
         </div>
    <div class="col-md-4">
        <asp:Button ID="btnSearch" runat="server"  SkinID="btnSearch" OnClick="btnSearch_Click"></asp:Button>
         </div>
    </div>
              <div class="row  mb-6">
          <div class="col-md-12">
              <asp:GridView ID="GridDisplay" runat="server" OnRowCommand="GridDisplay_RowCommand" OnRowDataBound="GridDisplay_RowDataBound" OnRowEditing="GridDisplay_RowEditing" OnRowCancelingEdit="GridDisplay_RowCancelingEdit" PageSize="20" OnPageIndexChanging="GridDisplay_PageIndexChanging" AllowPaging="true">
                  <Columns>
                       <asp:TemplateField ItemStyle-CssClass="col-nowrap form-inline" FooterStyle-CssClass="form-inline"  ControlStyle-CssClass="form-inline" ItemStyle-Width="3%">
                   
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="update1" Text="Update"
                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate"
                            ToolTip="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemStyle Width="3%" />
                </asp:TemplateField>
                      <asp:hyperlinkfield HeaderText="Job Ref" DataTextField="CallRef"
datanavigateurlfields="CallID,CCID"
datanavigateurlformatstring="~/WF/DC/FLSForm.aspx?ccid={1}&callid={0}&SDID={0}" Visible="false">
</asp:hyperlinkfield>
                    <%--  <asp:TemplateField HeaderText="Ticket Ref" Visible="false">
                          <ItemTemplate>
                               <asp:LinkButton ID="linkbtnTicketRef" Text='<%#Bind("CallRef") %>' 
                                                                               CommandArgument='<%#Bind("ID") %>' CommandName="Url" runat="server"></asp:LinkButton>
                              <asp:Label id="lblCallID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                              <asp:Label id="lblCallRef" runat="server" Text='<%# Bind("CallRef") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:Label id="lblCallID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                              <asp:Label id="lblCallRef1" runat="server" Text='<%# Bind("CallRef") %>'></asp:Label>
                          </EditItemTemplate>
                      </asp:TemplateField>--%>
                       <asp:hyperlinkfield HeaderText="Invoice" 
datanavigateurlfields="CallID,CCID,ID"
datanavigateurlformatstring="~/WF/DC/DCServices.aspx?CCID={1}&callid={0}&SDID={0}&ivref={2}" ControlStyle-CssClass="btn btn-secondary" Text="View" ItemStyle-Width="5%">
</asp:hyperlinkfield>
                      <%-- <asp:TemplateField HeaderText="Invoice Ref" Visible="false">
                          <ItemTemplate>
                              
                              <asp:Label id="lblInvoiceRef" runat="server" Text='<%# Bind("InvoiceRef") %>'></asp:Label>
                          </ItemTemplate>
                           <EditItemTemplate>
                                <asp:Label id="lblInvoiceRef1" runat="server" Text='<%# Bind("InvoiceRef") %>'></asp:Label>
                           </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Requester" Visible="false">
                          <ItemTemplate>
                              <asp:Label id="lblRequester" runat="server" Text='<%# Bind("Requester") %>'></asp:Label>
                          </ItemTemplate>
                           <EditItemTemplate>
                                <asp:Label id="lblRequester1" runat="server" Text='<%# Bind("Requester") %>'></asp:Label>
                           </EditItemTemplate>
                      </asp:TemplateField>--%>
                      <%-- <asp:TemplateField HeaderText="Invoice Date">
                          <ItemTemplate>
                             
                          </ItemTemplate>
                           <EditItemTemplate>
                               <asp:Label id="lblDateLogged1" runat="server" Text='<%# Bind("DateLogged") %>'></asp:Label>
                           </EditItemTemplate>
                      </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Details" >
                          <ItemTemplate>

                             <b> Job Number:</b> <asp:Label id="lblRCallRef" runat="server" Text='<%# Bind("CallRef") %>'></asp:Label><br />
                             <b> Description:</b> <asp:Label id="lblRDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label><br /> 
                              <b>Invoice Date: </b> <asp:Label id="lblRDateLogged" runat="server" Text='<%# Bind("DateLogged") %>'></asp:Label> <br /> 
                              <b>Assigned Technician:</b><asp:Label id="lblServiceTechnician" runat="server" Text='<%# Bind("ServiceTechnician") %>'></asp:Label>

                              
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblServiceTechnician1" runat="server" Text='<%# Bind("ServiceTechnician") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Details" >
                          <ItemTemplate>
                              <asp:Label id="lblDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label id="lblDetails1" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="Service Technician Mobile" Visible="false">
                          <ItemTemplate>
                              <asp:Label id="lblMobile" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label id="lblMobile1" runat="server" Text='<%# Bind("Mobile") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Net" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="hright">
                          <ItemTemplate>
                              <asp:Label id="lblFixedRatePrice" runat="server" Text='<%# Bind("FixedRatePrice","{0:F2}") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblFixedRatePrice1" runat="server" Text='<%# Bind("FixedRatePrice","{0:F2}") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>

                      
                       <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right"  HeaderStyle-CssClass="hright">
                          <ItemTemplate>
                              <asp:Label id="lblTotal" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblTotal1" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="VAT" ItemStyle-HorizontalAlign="Right"  HeaderStyle-CssClass="hright">
                          <ItemTemplate>
                              <asp:Label id="lblVAT" runat="server" Text='<%# Bind("VAT","{0:F2}") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblVAT1" runat="server" Text='<%# Bind("VAT","{0:F2}") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount Price" ItemStyle-HorizontalAlign="Right"  HeaderStyle-CssClass="hright">
                          <ItemTemplate>
                              <asp:Label id="lblDiscountPrice1" runat="server" Text='<%# Bind("DiscountPrice","{0:F2}") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblDiscountPrice1" runat="server" Text='<%# Bind("DiscountPrice","{0:F2}") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Status" ControlStyle-Width="125px"  HeaderStyle-CssClass="hcenter">
                          <ItemStyle Width="7%" />
                          <ItemTemplate>
                              <asp:Label id="lblStatus" runat="server" Text='<%# Bind("Status") %>'  CssClass="statuscls"  ForeColor="White" Font-Bold="true"></asp:Label>
                          </ItemTemplate>
                           <EditItemTemplate>
                               <asp:Label id="lblStatus" runat="server" Text='<%# Bind("Status") %>'  CssClass="statuscls"  ForeColor="White" Font-Bold="true" Visible="false"></asp:Label>
                               <asp:DropDownList ID="ddlStatus" runat="server">
                                  <%-- <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Sent to Customer" Value="Sent to Customer"></asp:ListItem>
                                    <asp:ListItem Text="Paid" Value="Paid"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                    <asp:ListItem Text="Credit Note Issued" Value="Credit Note Issued"></asp:ListItem>--%>
                               </asp:DropDownList>
                           </EditItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                          <ItemTemplate>
                              <asp:Button ID="btnApprove" runat="server" Text="Paid" OnClick="btn_Click" CommandName="Approve" CommandArgument='<%# Bind("ID") %>' />
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnApprove" runat="server" Text="Paid" OnClick="btn_Click" CommandName="Approve" CommandArgument='<%# Bind("ID") %>' />
                            </EditItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="" Visible="false">
                          <ItemTemplate>
                             <asp:Button ID="btnDeny" runat="server" Text="Deny" CommandName="Deny" CommandArgument='<%# Bind("ID") %>' OnClick="btndeny_Click" />
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnDeny" runat="server" Text="Deny" CommandName="Deny" CommandArgument='<%# Bind("ID") %>' OnClick="btndeny_Click" />
                            </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Payment Ref" >
                          <ItemTemplate>
                              <asp:Label id="lblpaymentref" runat="server" Text='<%# Bind("payref") %>'></asp:Label>
                          </ItemTemplate>
                           <EditItemTemplate>
                               <asp:TextBox ID="lblpaymentref1" runat="server" Text='<%# Bind("payref") %>'></asp:TextBox>
                           </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="Notes" ControlStyle-Width="100px">
                          <ItemTemplate>
                              <asp:Label id="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                          </ItemTemplate>
                           <EditItemTemplate>
                               <asp:TextBox ID="txtNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:TextBox>
                           </EditItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField >
                    <ItemTemplate>
                        <asp:Button ID="btnSendToCustomer" runat="server" CommandArgument='<%# Bind("ID") %>' SkinID="btnDefault" Text="Send To Client" CommandName="send"></asp:Button>
                    </ItemTemplate>
                     <ItemStyle Width="8%" />
                </asp:TemplateField>
                  </Columns>
              </asp:GridView>


              <asp:Button ID="btn" runat="server" Text="submit" OnClick="btn_Click" Visible="false" />

               <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
               
              <script type="text/javascript">
                  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
                  NestedGridResponsiveCss();
                                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
                                 setStatusBackColor();
                                 function setStatusBackColor() {
                                    
                                         $('.statuscls').each(function () {
                                             
                                             var s = $(this).html();
                                             if (s == 'Sent to Customer')
                                                 $(this).closest("td").css({ "background-color": "#FFBF00", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Pending')
                                                 $(this).closest("td").css({ "background-color": "#FF3333", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Paid')
                                                 $(this).closest("td").css({ "background-color": "#00CC00", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Cancelled')
                                                 $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                                             else if (s == 'Credit Note Issued')
                                                 $(this).closest("td").css({ "background-color": "#228B22", "text-align": "center", "vertical-align": "middle" });
                                            
                                         });
                                    
                                 }
</script>


               <ajaxToolkit:ModalPopupExtender ID="mdlContacts" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblshowcontacts" PopupControlID="pnlContacts" CancelControlID="btnCloseContacts" >
</ajaxToolkit:ModalPopupExtender>
               <asp:HiddenField ID="hpriceid" runat="server" Value="0" />
     <asp:Label ID="lblshowcontacts" runat="server"></asp:Label>
        <asp:Label ID="Label4" runat="server"></asp:Label>
       <asp:Panel ID="pnlContacts" runat="server" BackColor="White" Style="display:none;"
                       Width="950px" Height="630px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="Label5" runat="server" Text="Select contacts to send mail"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="btnCloseContacts" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
       
 
        <div class="form-group row" style="height:480px;overflow-y:auto;overflow-x:hidden;">
           <div class="form-group row">
               <div class="col-md-12 form-inline">
									<asp:GridView ID="gridContacts" runat="server" Width="100%">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkContact" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30%" HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContact" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-Width="50%" HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactEmail" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
									</asp:GridView>
                   </div>
								</div>
    </div>
       
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       
                      <asp:HiddenField ID="HiddenField2" runat="server" />
                       
                       
                          
                                        <asp:Button ID="Button2" runat="server" SkinID="btnDefault" Text="Send" OnClick="btnSendMailContacts_Click" />
                       <asp:Button ID="Button3" runat="server" SkinID="btnDefault" Text="Save"  Visible="false" />
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>
              </div>
                  </div>