<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectRisks" Title="Risks" Codebehind="ProjectRisks.aspx.cs" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.Risks%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline" ></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
                                  <div class="col-md-12">
                                      <div class="pull-right">
                                          <asp:Button ID="ImgbtnRiskReport" runat="server" SkinID="btnDefault" Text="Create Risk Report" 
                     OnClick="ImgbtnRiskReport_Click" />
					</div>
				</div>
        </div>
   <%--  <div class="form-group">
                                  <div class="col-md-12">
                                      <h3><%= Resources.DeffinityRes.RiskRprtsLoggedforProjRef%></h3>
                                      <hr />
                                      </div>
         </div>--%>
      <div class="form-group">
                                  <div class="col-md-12">
                                       <asp:GridView ID="GridView1" Runat="server" DataKeyNames="ID" width="100%"
                AutoGenerateColumns="False" AllowPaging="True" >                           
        <Columns>
           <asp:HyperLinkField HeaderStyle-CssClass="header_bg_l"  DataTextField="RiskReference" DataNavigateUrlFields="ProjectReference,RiskReference" DataNavigateUrlFormatString="~/WF/Projects/ProjectRiskItems.aspx?Project={0}&amp;RiskRef={1}" HeaderText="<%$ Resources:DeffinityRes,RiskReference%>"  SortExpression="RiskReference" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%" >
           </asp:HyperLinkField>
            <asp:BoundField DataField="RiskTitle" HeaderText="<%$ Resources:DeffinityRes,RiskTitle%>" SortExpression="RiskTitle" ItemStyle-Width="50%"/>
            <asp:BoundField DataField="Status" HeaderText="<%$ Resources:DeffinityRes,ReportStatus%>" SortExpression="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"/>
            <asp:BoundField DataField="Co_ordinator" HeaderText="<%$ Resources:DeffinityRes,RiskCoordinator%>" SortExpression="Co_ordinator" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="header_bg_r" ItemStyle-Width="20%" />
             <asp:TemplateField Visible="False">
                <ItemTemplate>
                       <%# getRiskRef(DataBinder.Eval(Container.DataItem,"RiskReference").ToString())%>     
                </ItemTemplate>
             </asp:TemplateField>
             
        </Columns>
        <EmptyDataTemplate>  <%= Resources.DeffinityRes.Nodataexist%>
       
        </EmptyDataTemplate>
      </asp:GridView>
      <asp:SqlDataSource ID="MysqlSource" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DN_SelectRisk" SelectCommandType="StoredProcedure" >
                 <SelectParameters>
                    
                    <asp:QueryStringParameter Name="Project" Type="string" QueryStringField="Project" DefaultValue="0"/>
                    
                 </SelectParameters>
                 </asp:SqlDataSource>
                                      </div>
          </div>

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

