<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectPlan_ProjectPipleline" Title="Deffinity" Codebehind="ProjectPipeline.aspx.cs" %>
<%@ Register Src="~/WF/Projects/controls/PipelineTab.ascx" TagName="PipelineTab" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:PipelineTab ID="PipelineTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectProposal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.SearchbyRating%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="PanelPln" runat="server" Width="100%">
        
        <div class="form-group">
            <asp:Label ID="lblProjectPlanMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        <div class="form-group">
            <div class="col-md-12">
              <asp:Button ID="lbtnPlan" runat="server" OnClick="lbtnPlan_Click" SkinID="btnDefault" Text="<%$ Resources:DeffinityRes,AddNewProjProposal%>"> 
                            </asp:Button>
                </div>
            </div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.ContributionAttribute%></label>
                                      <div class="col-sm-9">
                                           <asp:DropDownList ID="ddlContribution" runat="server" SkinID="ddl_50">
                            </asp:DropDownList>
					</div>
				</div>
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.RiskScore%></label>
                                      <div class="col-sm-9">
                                          <asp:DropDownList ID="ddlRisk" runat="server" SkinID="ddl_50">
                                <asp:ListItem Value="0">Select...</asp:ListItem>
                                <asp:ListItem Text="1 - Low" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5 - High" Value="5"></asp:ListItem>
                            </asp:DropDownList>
					</div>
				</div>
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.FinancialImpact%></label>
                                      <div class="col-sm-9">
                                          <asp:DropDownList ID="ddlFinance" runat="server" SkinID="ddl_50">
                                <asp:ListItem Value="0">Select...</asp:ListItem>
                                <asp:ListItem Text="1 - Low" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5 - High" Value="5"></asp:ListItem>
                            </asp:DropDownList>
					</div>
				</div>
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.BusinesImpact%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:DropDownList ID="ddlBiz" runat="server" SkinID="ddl_50">
                                <asp:ListItem Value="0">Select...</asp:ListItem>
                                <asp:ListItem Text="1 - Low" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5 - High" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                                           <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" SkinID="BtnLinkSearch" />
					</div>
				</div>
</div>
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.NewProjectProposal%></label>
                                      <div class="col-sm-9 form-inline"> <asp:TextBox ID="txtNewProjectProposal" runat="server" SkinID="txt_50" ValidationGroup="GroupCopyProjectPraposal"></asp:TextBox>
                                          <asp:Button ID="btnCopy" runat="server" OnClick="btnCopy_Click1" ImageAlign="AbsMiddle"
                                ValidationGroup="GroupCopyProjectPraposal" SkinID="btnDefault" Text="Copy" />
					</div>
				</div>
</div>
         <div class="form-group">
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,PlsenterProjproposalname%>"
                                ControlToValidate="txtNewProjectProposal" ValidationGroup="GroupCopyProjectPraposal"></asp:RequiredFieldValidator>
             </div>
      
       <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.ListofProposedProjects%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
               
                <asp:Panel ID="Panel1" runat="server" Width="100%">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" EmptyDataText="<%$ Resources:DeffinityRes,NoProposedPrjsavailable%>">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="rdbGVRow" runat="server" />
                                    <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ProjectPlanID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:HyperLinkField HeaderText="<%$ Resources:DeffinityRes,PrjProposal%>" DataTextField="ProjectPlanID"
                                DataNavigateUrlFields="ProjectPlanID" DataNavigateUrlFormatString="~/WF/ProjectPlan/Projectplan.aspx?ProjectPlanID={0}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>">
                                <ControlStyle Width="250px" />
                                <HeaderStyle Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Department" HeaderText="<%$ Resources:DeffinityRes,Department%>">
                                <ControlStyle Width="200px" />
                                <HeaderStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pStatus" HeaderText="<%$ Resources:DeffinityRes,Status%>">
                                <ControlStyle Width="150px" />
                                <HeaderStyle Width="150px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ProjectPlanID", "~/ProjectOverview.aspx?ProjectPlanID={0}") %>'
                                        Text="<%$ Resources:DeffinityRes,ChangetoPending%>" Visible='<%# getVisible(DataBinder.Eval(Container.DataItem, "StatusID").ToString()) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:HyperLinkField HeaderText="<%$ Resources:DeffinityRes,ViewReport%>" Text="Report"
                                DataNavigateUrlFields="ProjectPlanID" DataNavigateUrlFormatString="~/WF/ProjectPlan/Reports/ProjectPlanRpt.aspx?Id={0}"
                                Target="_blank">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
               
      </asp:Panel>
     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
   
</asp:Content>
