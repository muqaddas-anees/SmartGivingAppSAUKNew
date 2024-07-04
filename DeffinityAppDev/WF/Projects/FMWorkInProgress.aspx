<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="FMWorkInProgress" Codebehind="FMWorkInProgress.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.WorkInProgress%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc2:FMTab ID="fmID" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
        
           <asp:GridView ID="grdLiveProjects" runat="server" Width="100%" AllowSorting="true"
            ShowFooter="true" AutoGenerateColumns="false"
             EmptyDataText="No Records Found" 
            onrowdatabound="grdLiveProjects_RowDataBound" 
                onsorting="grdLiveProjects_Sorting" onrowcommand="grdLiveProjects_RowCommand" 
                >
             <Columns>
              <asp:TemplateField > 
                 <HeaderStyle Width="150px" />
                  <HeaderTemplate>
                     <asp:Label ID="lblJob" ToolTip="<%$ Resources:DeffinityRes,ProjectNameandRef%>" runat="server" Text="Project Reference"></asp:Label>
                 </HeaderTemplate>
                 <ItemStyle Width="150px"/>    
                 
            <ItemTemplate>
                <%--<asp:Label ID="lblProjectRef" runat="server" Text="<%#Bind('JobName') %>"></asp:Label>--%>
                 <a href="./WF/Projects/ProjectFinancials.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">  <%# DataBinder.Eval(Container.DataItem, "JobName")%></a>
            </ItemTemplate>
            
             
            </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Right" > 
                 <HeaderStyle Width="150px" />
                  <HeaderTemplate>
                   <asp:LinkButton ID="lnkSchDate" ToolTip="<%$ Resources:DeffinityRes,Expectedcompletiondateoftheproject%>" CommandArgument="ProjectEndDate" CommandName="sort" runat="server">Scheduled Completion Date</asp:LinkButton>
                  <%--<asp:Label ID="lblSchDate" ToolTip="expected completion date of the project" runat="server" Text="Scheduled Completion Date"></asp:Label>--%>
                </HeaderTemplate>
                 <ItemStyle Width="150px"/>    
                 
            <ItemTemplate>
                <asp:Label ID="lblschDate" runat="server" Text="<%#Bind('ProjectEndDate','{0:dd/MM/yyyy}') %>"></asp:Label>
                
            </ItemTemplate>
            
             
            </asp:TemplateField>
              <%--<asp:BoundField  HeaderText="Scheduled Completion Date" DataField="ProjectEndDate" ItemStyle-HorizontalAlign="Right"   DataFormatString="{0:d}"  ReadOnly="true"  ></asp:BoundField>--%>
              
               <asp:TemplateField ItemStyle-HorizontalAlign="Right"  >
                <HeaderTemplate>
              <asp:LinkButton ID="lnkVariations" ToolTip="<%$ Resources:DeffinityRes,Budgetedprojectrevenueplusanyvariations%>" CommandName="sort" CommandArgument="BudgetaryCostLevel3"  runat="server">Project Value Incl. Variations</asp:LinkButton>
                  <%--  <asp:Label ID="lblVariations1"  runat="server" Text="Project Value Incl. Variations"></asp:Label>SortExpression="BudgetaryCostLevel3"--%>
              </HeaderTemplate>
                     <ItemTemplate>
                         <asp:Label ID="lblVariations" runat="server" Text="<%#Bind('BudgetaryCostLevel3','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                     <FooterTemplate>
                         <asp:Label ID="lblVariationsf" runat="server" Font-Bold="true"></asp:Label>
                     </FooterTemplate>
                      <FooterStyle HorizontalAlign="Right" />
                 </asp:TemplateField>
              
                 <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                 <HeaderTemplate>
                     <asp:Label ID="lblCurrentEstimate1" ToolTip="<%$ Resources:DeffinityRes,BudgetedProjectCost%>" runat="server" Text="Current Estimate of Total Costs"></asp:Label>
                 </HeaderTemplate>
                     <HeaderStyle Width="100px" />
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblestimateTotalcost" runat="server" Text="<%#Bind('BuyingPrice','{0:N2}') %>"></asp:Label>
                     </ItemTemplate>
                     <FooterTemplate>
                     <asp:Label ID="lblestimateTotalcostf" runat="server" Font-Bold="true" ></asp:Label>
                     </FooterTemplate>
                      <FooterStyle HorizontalAlign="Right" />
                 </asp:TemplateField>
                
                 <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                  <HeaderTemplate>
                      <asp:LinkButton ID="lnkCostToDate" ToolTip="<%$ Resources:DeffinityRes,ActualcostsincurredtodatewhichincludetimematerialsthathavebeengoodsreceivedexpensesandexternalcostsThisisfoundintheFinanceActualssectionoftheproject%>" CommandArgument="ActualCost" CommandName="sort" runat="server">Cost to Date</asp:LinkButton>
                     <%--<asp:Label ID="lblCurrentEstimate1" ToolTip="" runat="server" Text="Current Estimate of Total Costs"></asp:Label>--%>
                 </HeaderTemplate>
                  <HeaderStyle Width="100px" />
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblCostToDate" runat="server" Text="<%#Bind('ActualCost','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                     <FooterTemplate>
                         <asp:Label ID="lblCostToDatef" runat="server" Font-Bold="true"></asp:Label>
                     </FooterTemplate>
                      <FooterStyle HorizontalAlign="Right" />
                 </asp:TemplateField>
                 
                 <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                 <HeaderStyle Width="100px" />
                 <HeaderTemplate>
                     <%-- <asp:LinkButton ID="lnkCostToDate" ToolTip="" runat="server">Cost to Date</asp:LinkButton>--%>
                    <asp:Label ID="lblInvDate" ToolTip="<%$ Resources:DeffinityRes,Valueofinvoicesraisedtodate%>" runat="server" Text="Invoiced to Date"></asp:Label>
                 </HeaderTemplate>
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblBillToDate" runat="server" Text="<%#Bind('invoice','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                     <FooterTemplate>
                         <asp:Label ID="lblBillToDatef" runat="server" Font-Bold="true" ></asp:Label>
                     </FooterTemplate>
                      <FooterStyle HorizontalAlign="Right" />
                 </asp:TemplateField>
                 
                 
                 <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                 <HeaderStyle Width="100px" />
                 <HeaderTemplate>
                    <asp:LinkButton ID="lnkinoccured" CommandArgument="inoccured" CommandName="sort"  ToolTip="The percentage of costs incurred to date in relation to the estimated costs for the project. This will flag as amber if the figure is over 80% and under 100% and red if the figure exceeds 100%" runat="server">% of Costs Incurred</asp:LinkButton>
                    <%--<asp:Label ID="lblInvDate" ToolTip="Value of invoices raised to date" runat="server" Text="Invoiced to Date"></asp:Label>--%>
                 </HeaderTemplate>
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label SkinID="Amber_circle" ID="imgRed" runat="server"  Visible='<%#visibleAmberIcon(DataBinder.Eval(Container.DataItem,"inoccured").ToString())%>'  />
                                    <asp:Label SkinID="Red_circle" ID="imgAmber" runat="server"  Visible='<%#visibleRedIcon(DataBinder.Eval(Container.DataItem,"inoccured").ToString())%>'  />
                         <asp:Label ID="lblinoccured" runat="server" Text="<%#Bind('inoccured','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                    
                 </asp:TemplateField>
                  <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                 <HeaderStyle Width="100px" />
                 <HeaderTemplate>
                    
                    <asp:Label ID="lblER" ToolTip="Calculated using Project Value Including Variations x % of Costs Incurred.  This relates to the physical work accomplished plus the authorised budget for this work" runat="server" Text="Earned Revenue"></asp:Label>
                 </HeaderTemplate>
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblinoccured" runat="server" Text="<%#Bind('ER','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ER  ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                    
                 </asp:TemplateField>
      
                <%-- <asp:BoundField HeaderText="" DataField="" ReadOnly="true" 
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />--%>
                   <%--<asp:BoundField HeaderText="Earned Revenue" DataField="ER"  ReadOnly="true" 
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />--%>
                  
                   <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                     <HeaderTemplate>
                         <asp:LinkButton ID="lblUnd" CommandArgument="UN" CommandName="sort" ToolTip="<%$ Resources:DeffinityRes,TheamountinvoicedtodateinrelationtotheEarnedRevenuefortheproject%>" runat="server">Under (Over) Invoiced</asp:LinkButton>
                     <%--<asp:Label ID="lblUnd" ToolTip="" runat="server" Text="Under (Over) Invoiced"></asp:Label>--%>
                 </HeaderTemplate>
                 <HeaderStyle Width="100px" />
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblover" runat="server" Text="<%#Bind('UN','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                     <FooterTemplate>
                         <asp:Label ID="lbloverf" runat="server" Font-Bold="true" Text="Total Backlog" ></asp:Label>
                     </FooterTemplate>
                      <FooterStyle  Width="100px" HorizontalAlign="Left" />
                      
                 </asp:TemplateField>
                  <%-- <asp:BoundField HeaderText="Under (Over) Billings"  ReadOnly="true" 
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                  --%>
                  
                  
                  
                 <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                 <HeaderTemplate>
                     <asp:Label ID="lblEst" ToolTip="<%$ Resources:DeffinityRes,Theestimatedcoststheprojectislikelytoincurattheendoftheproject%>" runat="server" Text="Estimated Cost to Completion"></asp:Label>
                 </HeaderTemplate>
                 <HeaderStyle Width="100px" />
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblCosttoComplete" runat="server" Text="<%#Bind('costComp','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                     
                     <FooterTemplate>
                         <asp:Label ID="lblCosttoCompletef" runat="server" Font-Bold="true"></asp:Label>
                     </FooterTemplate>
                     <FooterStyle HorizontalAlign="Right" />
                 </asp:TemplateField>
           
                  
                  <asp:TemplateField  ItemStyle-HorizontalAlign="Right">
                 <HeaderTemplate>
                    <asp:LinkButton ID="lblEGP" CommandArgument="costComp" CommandName="sort" ToolTip="<%$ Resources:DeffinityRes,Estimatedgrossprofitbasedonbudgetaryvalues%>" runat="server">Expected Gross Profit </asp:LinkButton>
                 </HeaderTemplate>
                 <HeaderStyle Width="100px" />
                     <ItemStyle Width="100px" />
                     <ItemTemplate>
                         <asp:Label ID="lblExpectedGrossProfit" runat="server" Text="<%#Bind('costComp','{0:N2}') %>"></asp:Label>
                         <%--<asp:CheckBox ID="chkSelect" runat="server"  />--%>
                     </ItemTemplate>
                     
                 </asp:TemplateField>
               
             </Columns>
             
             </asp:GridView>
       

    
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


