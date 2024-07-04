<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_PTLabourTracker" Codebehind="PTLabourTracker.ascx.cs" %>
<style>
    .round
    {
        border: 1px solid Silver;
        padding: 5px 5px;
        background: #d1e7ed;
        width: 40%;
        border-radius: 8px;
    }
</style>
<div class="row">
     <div class="col-md-12">
         &nbsp;
         </div>
    </div>
<div class="row">
     <div class="col-md-4 well">
     <div class="form-group ">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Original Labour Cost:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblOriginalLabourCost" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Spent To Date:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblSpentToDate" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
     <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Cost Remaining:</label>
                                      <div class="col-sm-3 pull-right control-label"> <asp:Label ID="lblCostRemaining" runat="server" Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
         </div>
</div>

<div class="row">
     <div class="col-md-12">
         &nbsp;
         </div>
    </div>
    <div class="form-group form-inline">
                                  <div class="col-md-12">
                                       <div class="col-sm-4">
                                            <label class="col-sm-5 control-label"> Worksheet</label>
                                      <div class="col-sm-7">
                                           <asp:DropDownList ID="ddlWorksheet" runat="server" SkinID="ddl_90" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlWorksheet_SelectedIndexChanged">
                </asp:DropDownList>
					</div>
                                           </div>
                                       <div class="col-sm-5">
                                            <label class="col-sm-6 control-label"> Search by Description</label>
                                      <div class="col-sm-6">
                                           <asp:TextBox ID="txtDescription" runat="server" SkinID="txt_90" ></asp:TextBox>
					</div>
                                           </div>
                                       <div class="col-sm-3 form-inline">
                                           <asp:Button ID="imgSearch" runat="server" SkinID="btnSearch" OnClick="imgSearch_Click" /> <asp:Button ID="imgViewAll" runat="server" SkinID="btnDefault" Text="View All" 
                    OnClick="imgViewAll_Click" />
                                           </div>
				</div>
</div>


<asp:ValidationSummary ID="Val1" runat="server" DisplayMode="BulletList" ValidationGroup="lab"  />

<div style="width:100%">
<asp:GridView ID="gvLabour" runat="server" OnRowCommand="gvLabour_RowCommand" 
    AllowPaging="true" OnRowDataBound="gvLabour_RowDataBound" Width="100%" EmptyDataText="No records found"
    OnPageIndexChanging="gvLabour_PageIndexChanging">
    <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="WorksheetID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblWorksheetID" runat="server" Text='<%# Bind("WorkSheetID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
           <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DateReceived%>" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline" FooterStyle-CssClass="form-inline">
            <ItemStyle Width="120px" />
            <ItemTemplate>
               
                        <asp:TextBox ID="txtDateReceived" runat="server" Text='<%# Bind("ExpectedShipmentDate","{0:d}")%>'
                            SkinID="Date"></asp:TextBox>
                   
                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender" /></div>
                    <asp:CompareValidator ID="CompareValidatorDateReceived" runat="server" ControlToValidate="txtDateReceived"
                        ErrorMessage="Please enter valid date" Operator="DataTypeCheck" Type="Date" ValidationGroup="lab">*</asp:CompareValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                        PopupButtonID="imgbtnenddate6" TargetControlID="txtDateReceived" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
                    <%-- <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtDateReceived"
                                        Display="None" ErrorMessage="Please Enter Received Date" ValidationGroup="Group1"></asp:RequiredFieldValidator>--%>
                   
                        <asp:LinkButton ID="imgApplyDate" SkinID="BtnLinkDownload" OnClick="btn_ApplyDate_OnClick"
                            runat="server" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Worksheet Name" >
            <ItemTemplate>
                <asp:Label ID="lblWorksheet" runat="server" Text='<%# Bind("Worksheet") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfWorksheet" runat="server" Font-Bold="true" Text="Total"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--  <asp:BoundField DataField="Qty" HeaderText="QTY" DataFormatString="{0:F2}" ItemStyle-Width="75px"
                            ItemStyle-HorizontalAlign="Right" />--%>
        <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Right">
            <ItemStyle Width="75px" />
            <ItemTemplate>
                <asp:Label ID="lblQTY" runat="server" Text='<%# Bind("Qty","{0:F2}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Labour" HeaderText="Price" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
        <asp:TemplateField HeaderText="Total">
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfTotal" runat="server" Font-Bold="true"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Number Complete" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:TextBox ID="txtNumberComplete" runat="server" Width="50px" Text='<%# Bind("NumberComplete") %>'
                    SkinID="Price">
                </asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter a valid number complete"
                    ControlToValidate="txtNumberComplete" Type="Double" Operator="DataTypeCheck"
                    ValidationGroup="lab">*</asp:CompareValidator>
             
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="imgHis" SkinID="BtnLinkHistory" runat="server" CausesValidation="false" CommandName="History"
                    CommandArgument='<%# Bind("ID") %>' ToolTip="View History" />
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="20px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Spent to Date">
            <ItemStyle HorizontalAlign="Right" />
            <FooterStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblSpentToDate" runat="server" Text='<%# Bind("SpentToDate","{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfSpentToDate" runat="server" Font-Bold="true"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total Budget Remaining" HeaderStyle-CssClass="header_bg_r">
            <FooterStyle HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblTotalBudgetRemaining" runat="server" Text='<%# Bind("TotalBudgetRemaining","{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblfTotalBudgetRemaining" runat="server" Font-Bold="true"></asp:Label>
            </FooterTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
<div class="clr">
</div>

<div style="width:100%">
    <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label></div>
<div class="clr">
</div>
<br />
<asp:Button ID="imgUpdate" SkinID="btnUpdate" runat="server" OnClick="imgUpdate_Click"
    ValidationGroup="lab" />
<ajaxToolkit:ModalPopupExtender ID="mdlpopupHisstory" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="ImgHistory" PopupControlID="pnlHistory" CancelControlID="imgClose">
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="ImgHistory" runat="server" />
<asp:Panel ID="pnlHistory" runat="server" BackColor="White" Style="display: none;"
    Width="700px" Height="400px" BorderStyle="Double" BorderColor="LightSteelBlue"
    ScrollBars="Auto">
    <div class="form-group">
        <div class="col-md-10">
           <strong>History </strong> 
            <hr class="no-top-margin" />
            </div>
        <div class="col-md-2">
            <asp:LinkButton ID="imgClose" runat="server" SkinID="BtnLinkClose" ToolTip="Close" />
            </div>
    </div>
   
   
   
                <asp:GridView ID="GvHistory" runat="server" AutoGenerateColumns="False" GridLines="None"
                    ShowHeaderWhenEmpty="true" HorizontalAlign="Left" BorderStyle="None" CellPadding="2"
                    CellSpacing="2" EmptyDataText="No histoy found!" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ModifiedDate" HeaderText="Date Time" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="Worksheet" HeaderText="Worksheet Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="PreviousValue" HeaderText="Previous Value" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="ValueNow" HeaderText="Value Now" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
          
</asp:Panel>
