<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="QASummary" Title="QA Summary" Codebehind="QASummary.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.QA%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ProjectSummary%> 
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="Tabs" runat="server">
    <script  type="text/javascript">
        $(document).ready(function () {
            $('#navTab').hide();
        });
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"  Runat="Server">
   
    <div class="form-group">
          <div class="col-md-12">
              <p>The following projects are now complete and require QA approval:</p>
	</div>
</div>
    <asp:GridView ID="GridView1" runat="server" width="100%" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand1" OnRowEditing="GridView1_RowEditing" >
     <Columns>
            <asp:HyperLinkField  DataTextField="ProjectReferenceWithPrefix" DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Projects/QA/QAProjectSummary.aspx?Project={0}" HeaderText="Project Reference" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
           <asp:BoundField DataField="ProjectTitle" HeaderText="Project Title" >
           </asp:BoundField>  
           <asp:BoundField DataField="OwnerName" HeaderText="<%$ Resources:DeffinityRes,Owner%>" >
                 </asp:BoundField> 
                 <asp:BoundField DataField="PortfolioName" HeaderText="<%$ Resources:DeffinityRes,Customer%>" >
                 </asp:BoundField> 
                 <asp:BoundField DataField="SiteName" HeaderText="<%$ Resources:DeffinityRes,Site%>" >
                 </asp:BoundField> 
                 <asp:BoundField DataField="ProjectStatusName" HeaderText="<%$ Resources:DeffinityRes,Status%>" >
                 </asp:BoundField>
                 <asp:BoundField DataField="BudgetaryCost" HeaderText="<%$ Resources:DeffinityRes,Projectvalue%>" HtmlEncode="false" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" >
                 </asp:BoundField> 
                  <asp:BoundField DataField="BuyingPrice" HeaderText="<%$ Resources:DeffinityRes,BuyingValue%>" HtmlEncode="false" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right">
                 </asp:BoundField>
                 <asp:BoundField  DataField="BudgetaryCostLevel3" HeaderText="<%= Resources.DeffinityRes.RevicedProjectvalue%>" HtmlEncode="false" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right">
                 </asp:BoundField> 
                 <asp:TemplateField>
                 <ItemTemplate>
                 <asp:LinkButton id="lbtnArchived" runat="server" CommandArgument='<%# Bind("ProjectReference") %>' CommandName="Archived" Text="<%$ Resources:DeffinityRes,Archive%>"></asp:LinkButton>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                 <ItemTemplate>
                 <asp:LinkButton id="lbtnEmailFinance" runat="server" CommandArgument='<%# Bind("ProjectReference") %>' CommandName="EmailFinance" Text="<%$ Resources:DeffinityRes,EmailFinancetoInvoice%>"></asp:LinkButton>
                 </ItemTemplate>
                 </asp:TemplateField>
                 
          </Columns>
          </asp:GridView>
    
 
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">

        //grid_responsive();
        grid_responsive_display();

           
        });
    </script>
</asp:Content>

