<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUsersCertificates" Title="Untitled Page" Codebehind="AdminUsersCertificates.aspx.cs" %>

<%@ Register src="controls/MangeUserTab.ascx" tagname="MangeUserTab" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Manage&nbsp;Users&nbsp; - Documents
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="form-group row">
             <div class="col-md-12 form-inline">
                  <%= Resources.DeffinityRes.UserAdminfor%> <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
     <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
    
<div class="form-group row">
             <div class="col-md-12">
                  <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label><asp:ValidationSummary
                                                                ID="ValidationSummary3" runat="server" ValidationGroup="Submit" DisplayMode="List"
                                                                HeaderText="<b>Please check following:</b>"></asp:ValidationSummary>
</div>
</div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Username%></label>
                                      <div class="col-sm-6"> <asp:Label ID="lblcertificateUserName" runat="server"  Font-Bold="true"></asp:Label>
					</div>
				</div>
                </div>
    <div class="form-group row" style="display:none;visibility:hidden;">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.CertificationAccrediation%> </label>
                                      <div class="col-sm-6">
                                          <asp:TextBox ID="txtCertificate" runat="server" SkinID="txt_90"></asp:TextBox><%--<asp:RequiredFieldValidator
                                                                ID="reqtxtCertificate" runat="server" ControlToValidate="txtCertificate" Display="None"
                                                                ErrorMessage="Please enter certificate name" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
					</div>
				</div>
                </div>
    <div class="form-group row" style="display:none;visibility:hidden;">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.CertifiedFrom%></label>
                                      <div class="col-sm-6 form-inline">
                                          <asp:TextBox ID="txtCertifiedFrom" runat="server" SkinID="Date"></asp:TextBox>
                                          <%--<asp:RequiredFieldValidator
                                                                runat="server" ID="reqtxtCertifiedFrom" ControlToValidate="txtCertifiedFrom"
                                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please enter certificate from"></asp:RequiredFieldValidator>--%>
                                          <asp:Label
                                                                    ID="Image2" runat="server" SkinID="Calender" ToolTip="Pick a date" />
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCertifiedFrom"
                                                                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                                ValidationGroup="Submit" ErrorMessage="Please enter valid from date"></asp:RegularExpressionValidator>--%>
                                          <%--<asp:CompareValidator
                                                                    ID="CompareValidator3" runat="server" ControlToCompare="txtCertificationExp"
                                                                    ControlToValidate="txtCertifiedFrom" Text="*" Type="Date" Operator="LessThanEqual"
                                                                    ErrorMessage="Certificate expiry date must be greater than certified from date"
                                                                    SetFocusOnError="true" Display="None" ValidationGroup="Submit"></asp:CompareValidator>--%>
					</div>
				</div>
                </div>
    <div class="form-group row" style="display:none;visibility:hidden;">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.CertificationExpiry%></label>
                                      <div class="col-sm-6 form-inline">
                                          <asp:TextBox ID="txtCertificationExp" runat="server" SkinID="Date"></asp:TextBox><asp:Label
                                                                ID="Image1" runat="server" SkinID="Calender" ToolTip="Pick a date" />
                                                           <%-- <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtCertificationExp"
                                                                ValidationGroup="Submit" Text="*" ErrorMessage="Please enter certification expiry date"
                                                                SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>--%>
                                          
                                         <%-- <asp:RegularExpressionValidator
                                                                    ID="regrdate" runat="server" ControlToValidate="txtCertificationExp" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                                    ValidationGroup="Submit" ErrorMessage="Please enter valid expiry date"></asp:RegularExpressionValidator>--%>
					</div>
				</div>
                </div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.UploadCertificate%></label>
                                      <div class="col-sm-6">
                                           <asp:FileUpload ID="FileUploadCertificate" runat="server"  /><asp:RequiredFieldValidator
                                                                ID="Re323" runat="server" ControlToValidate="FileUploadCertificate" ValidationGroup="Submit"
                                                                 ErrorMessage="Please upload a document " SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:Button SkinID="btnUpload" runat="server" ID="imgBtnUpload" ValidationGroup="Submit"
                                                                OnClick="imgBtnUpload_Click" />
                                                            &nbsp;<asp:Button  SkinID="btnCancel" runat="server" ID="Imgbtn_Cancel12" OnClick="Imgbtn_Cancel12_Click" />
					</div>
				</div>
                </div>
   <div class="form-group row">
             <div class="col-md-6">
     <asp:GridView ID="GridViewCertification" runat="server" AutoGenerateColumns="false"
                                                                Width="100%" EmptyDataText="No certificates uploaded " DataSourceID="objds_Certificates"
                                                                OnRowCommand="GridViewCertification_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Certificate" DataField="Certification" Visible="false"  />
                                                                    <asp:TemplateField HeaderText="Certificated From" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEndDateTandE" runat="server" Text='<%# Bind("CertifiedFrom","{0:d}") %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Certification Expiry" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEndDateTandE1" runat="server" Text='<%# Bind("CertificateExpiry","{0:d}") %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="Documents" >
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="imgDownload"  runat="server"
                                                                                CommandName="Download" CommandArgument='<%# Bind("CertificateImage") %>' Text='<%# Bind("Certification") %>' />
                                                                        </ItemTemplate>
                                                                        <%--<ItemStyle HorizontalAlign="Center" />--%>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:ObjectDataSource ID="objds_Certificates" runat="server" TypeName="Certifications.Usercertification"
                                                                SelectMethod="Fill">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter Type="Int32" ControlID="getUserId" Name="ContractorID" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
     <asp:HiddenField ID="getUserId" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender44" runat="server" TargetControlID="txtCertificationExp"
                                         CssClass="MyCalendar" PopupButtonID="Image1">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender33" runat="server" TargetControlID="txtCertifiedFrom"
                                         CssClass="MyCalendar" PopupButtonID="Image2">
                                    </ajaxToolkit:CalendarExtender>
    </div>
       </div>
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

