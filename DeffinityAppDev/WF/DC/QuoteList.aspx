<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="QuoteList.aspx.cs" Inherits="DeffinityAppDev.WF.DC_page.QuoteList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.Quotations%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <%= Resources.DeffinityRes.Quotations%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="display:none;visibility:hidden;" >
        <div class="col-sm-4">			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<i class=" fa-arrows-h"></i>
						</div>
                        <div class="xe-label">
							<strong class="num"> <label id="lblPending" runat="server">0</label> </strong>
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
							<strong class="num"> <label id="lblUnpaid" runat="server">0</label> </strong>
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
							<strong class="num"> <label id="lblPaidthismonth" runat="server">0</label> </strong>
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
<div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
          </div>
                 </div>
<div class="row">
          <div class="col-md-4" style="display:none;visibility:hidden;">
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
        <asp:LinkButton ID="btnSearch" runat="server" SkinID="BtnLinkButtonSearch" OnClick="btnSearch_Click"></asp:LinkButton>
         </div>
    </div>
              <div class="row">
          <div class="col-md-12">
              <asp:GridView ID="GridDisplay" runat="server" OnRowCommand="GridDisplay_RowCommandNew" OnRowDataBound="GridDisplay_RowDataBound" OnRowEditing="GridDisplay_RowEditing" OnRowCancelingEdit="GridDisplay_RowCancelingEdit" PageSize="20" OnPageIndexChanging="GridDisplay_PageIndexChanging" AllowPaging="true">
                  <Columns>
                       <asp:TemplateField ItemStyle-CssClass="col-nowrap form-inline" FooterStyle-CssClass="form-inline"  ControlStyle-CssClass="form-inline" ItemStyle-Width="125px">
                   
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
                    <ItemStyle Width="7%" />
                </asp:TemplateField>
                      <asp:hyperlinkfield HeaderText="Job Ref" DataTextField="CallRef"
datanavigateurlfields="CallID,CCID"
datanavigateurlformatstring="~/WF/DC/DCQuotationCompare.aspx?CCID={1}&callid={0}&SDID={0}&Option=0&tab=quote">
</asp:hyperlinkfield>
                      <asp:TemplateField HeaderText="Ticket Ref" Visible="false">
                          <ItemTemplate>
                               <asp:LinkButton ID="linkbtnTicketRef" Text='<%#Bind("CallID") %>' 
                                                                               CommandArgument='<%#Bind("ID") %>' CommandName="Url" runat="server"></asp:LinkButton>
                              <asp:Label id="lblCallID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                               <asp:Label id="lblCCID" runat="server" Text='<%# Bind("CCID") %>' Visible="false"></asp:Label>
                              <asp:Label id="lblCallRef" runat="server" Text='<%# Bind("CallID") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:Label id="lblCallID1" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                              <asp:Label id="lblCallRef1" runat="server" Text='<%# Bind("CallID") %>'></asp:Label>
                          </EditItemTemplate>
                      </asp:TemplateField>
                       
                      
                       <asp:TemplateField HeaderText="Requester Name">
                          <ItemTemplate>
                              <asp:Label id="lblRequester" runat="server" Text='<%# Bind("RequesterName") %>'></asp:Label>
                          </ItemTemplate>
                           <EditItemTemplate>
                                <asp:Label id="lblRequester1" runat="server" Text='<%# Bind("RequesterName") %>'></asp:Label>
                           </EditItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="Name">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                         <b> Address:</b> <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label><br />
                                                     <b>  City:</b>  <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label> <br />
                                                      <b>  State: </b> <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label> <br />
                                                      <b>  Zipcode: </b>  <asp:Label ID="lblPostCode" runat="server" Text='<%# Bind("PostCode") %>'></asp:Label> <br />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact">
                          <ItemTemplate>
                              <asp:Label id="lblContactNumber" runat="server" Text='<%# Bind("ContactNumber") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label id="lblContactNumber1" runat="server" Text='<%# Bind("ContactNumber") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                       
                      
                       <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                              <asp:Label id="lblTotal" runat="server" Text='<%# Bind("QuoteAmount","{0:F2}") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblTotal1" runat="server" Text='<%# Bind("QuoteAmount","{0:F2}") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="10%" >
                          <ItemTemplate>
                              <asp:Button ID="btnInvoiceCustomer" runat="server" Text="Invoice Customer" OnClick="btn_Click" CommandName="Approve" CommandArgument='<%# Bind("ID") %>' />
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnInvoiceCustomer1" runat="server" Text="Invoice Customer" OnClick="btn_Click" CommandName="Approve" CommandArgument='<%# Bind("ID") %>' />
                            </EditItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
               <ajaxToolkit:ModalPopupExtender ID="mdlInvoice" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblAddInvoice" PopupControlID="pnlManageOptions" CancelControlID="lbtnCloseOptions" >

</ajaxToolkit:ModalPopupExtender>
              <asp:Label ID="lblAddInvoice" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnCloseOptions" runat="server"></asp:Label>
       <asp:Panel ID="pnlManageOptions" runat="server" BackColor="White" Style="display:none;"
                       Width="500px" Height="370px" CssClass="panel panel-color panel-info" ScrollBars="None">
           <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblJobRef" runat="server" Text="Quotations"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnCloseOptions" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <asp:Label ID="lblMsgOptions" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                       <asp:Label ID="lblErrorOptions" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                       </div>
            </div>
 <div class="form-group row">
                   <div class="col-md-12 form-inline">
                         <div class="col-sm-12  form-inline">
                      <asp:GridView ID="gridQuotations" runat="server" OnRowCommand="gridQuotations_RowCommandNew">
                          <Columns>
                               <asp:TemplateField HeaderText="Quotation">
                          <ItemTemplate>
                              <asp:Label id="lblName" runat="server" Text='<%# Bind("OptionName") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblName1" runat="server" Text='<%# Bind("OptionName") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                               <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                              <asp:Label id="lblTotal" runat="server" Text='<%# Bind("QuoteAmount","{0:F2}") %>'></asp:Label>
                          </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:Label id="lblTotal1" runat="server" Text='<%# Bind("QuoteAmount","{0:F2}") %>'></asp:Label>
                            </EditItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="10%" >
                          <ItemTemplate>
                              <asp:Button ID="btnInvoiceCustomer" runat="server" Text="Select" CommandName="Invoice" CommandArgument='<%# Bind("ID") %>' />
                          </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnInvoiceCustomer1" runat="server" Text="Select" CommandName="Invoice" CommandArgument='<%# Bind("ID") %>' />
                            </EditItemTemplate>
                      </asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                             </div>
                       </div>
     </div>
         
</div>
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <div class="col-sm-12 form-inline">
                       <asp:Button ID="btnSubmitOptions" runat="server" SkinID="btnSubmit" Visible="false" />
                       <asp:Button ID="btnCancelOptions" runat="server" SkinID="btnCancel" Visible="false" />
                           </div>
                       </div>
               </div>
                     </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnCloseOptions" />
               </Triggers>
           </asp:UpdatePanel>
           </asp:Panel>

              <asp:Button ID="btn" runat="server" Text="submit" OnClick="btn_Click" Visible="false" />

               <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
               
              <script type="text/javascript">
                  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
                  NestedGridResponsiveCss();
                                 //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
                                 //setStatusBackColor();
                                 //function setStatusBackColor() {
                                    
                                 //        $('.statuscls').each(function () {
                                             
                                 //            var s = $(this).html();
                                 //            if (s == 'Sent to Customer')
                                 //                $(this).closest("td").css({ "background-color": "#FFBF00", "text-align": "center", "vertical-align": "middle" });
                                 //            else if (s == 'Pending')
                                 //                $(this).closest("td").css({ "background-color": "#FF3333", "text-align": "center", "vertical-align": "middle" });
                                 //            else if (s == 'Paid')
                                 //                $(this).closest("td").css({ "background-color": "#00CC00", "text-align": "center", "vertical-align": "middle" });
                                 //            else if (s == 'Cancelled')
                                 //                $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle" });
                                 //            else if (s == 'Credit Note Issued')
                                 //                $(this).closest("td").css({ "background-color": "#228B22", "text-align": "center", "vertical-align": "middle" });
                                            
                                 //        });
                                    
                                 //}
</script>
              </div>
                  </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
