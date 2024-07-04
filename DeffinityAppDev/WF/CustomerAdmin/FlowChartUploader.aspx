<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="FlowChartUploader" Title="Flowchart Uploader" Codebehind="FlowChartUploader.aspx.cs" %>
<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<%@ Register Src="controls/PortfolioMenuTab.ascx" TagName="UserTab" TagPrefix="UC1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.FlowCharts%> - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <div class="form-group row">
             <div class="col-md-12">
                  <asp:ValidationSummary ID="PageValidations" runat="Server" DisplayMode="List" ShowSummary="true"
                    ShowMessageBox="false" />
</div>
</div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <asp:Label ID="lblFlowchartName" runat="server" Text="Title"></asp:Label></label>
                                      <div class="col-sm-6"><asp:TextBox ID="txtFlowChartName" runat="Server" SkinID="txt_80" />
                <asp:RequiredFieldValidator ID="reqFlowChartName" Text="*" ErrorMessage="Title name is required"
                    ControlToValidate="txtFlowChartName" runat="Server" Display="None" />
					</div>
				</div>
                </div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <asp:Label ID="lblReference" runat="server" Text="Reference"></asp:Label></label>
                                      <div class="col-sm-6">
                                          <asp:TextBox ID="txtReferenceNumber" runat="Server" SkinID="txt_80" />
                <asp:RequiredFieldValidator ID="requiredReference" runat="Server" Text="*" ErrorMessage="Reference is required"
                    ControlToValidate="txtReferenceNumber" Display="None" />
					</div>
				</div>
                </div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtDescription" runat="Server" Rows="5" TextMode="MultiLine" SkinID="txtMulti_80" />
					</div>
				</div>
                </div>
     <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <asp:Label ID="lblAttachments" runat="server" Text="Attachments" /></label>
                                      <div class="col-sm-8">
                                          <asp:FileUpload ID="FlowChartUpload" runat="server" />
					</div>
				</div>
                </div>
    <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-8">
                                           <asp:Button runat="Server" ID="btnUpload"
                    AlternateText="Upload Flow chart image" OnClick="btnUpload_Click" 
                    SkinID="btnSubmit" />
                <asp:SqlDataSource ID="sqlPortfolio" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DN_Select_PortFolio" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
					</div>
				</div>
                </div>

    <asp:Panel ID="Panel1" runat="Server">
        <asp:GridView ID="gridFlowChart" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
            EmptyDataText="No Charts Available" OnRowCommand="gridFlowChart_RowCommand" OnRowDeleting="gridFlowChart_RowDeleting"
            OnSelectedIndexChanging="gridFlowChart_SelectedIndexChanging" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Reference">
                    <ItemTemplate>
                        <asp:Label ID="lblReference" runat="Server" Text='<%#Eval("Reference") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Version" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="Server" Text='<%#Eval("Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="Server" SkinID="BtnLinkDownload"
                            AlternateText="Download file" CausesValidation="false" CommandName="DownLoad"
                            CommandArgument='<%#Eval("ID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton2" OnClientClick='return confirm("Do you really want to delete this file?");'
                            SkinID="BtnLinkDelete" runat="Server" CommandName="Delete" CommandArgument='<%#Eval("ID")%>'
                            CausesValidation="False" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
       
        <div class="form-group row">
          <div class="col-md-12">
               <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
	</div>
</div>
    </asp:Panel>


 
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
